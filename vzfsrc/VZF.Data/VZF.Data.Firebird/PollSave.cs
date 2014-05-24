using System;
using System.Text;


namespace VZF.Data.Firebird
{
    using System.Data;

    using VZF.Data.DAL;
 
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Objects;

    public static partial class Db
    {
        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save_firebird(
            [NotNull] int? mid,
            [NotNull] System.Collections.Generic.List<PollSaveList> pollList)
        {

            foreach (PollSaveList question in pollList)
            {
                var sb = new System.Text.StringBuilder();
                var paramSb = new System.Text.StringBuilder("EXECUTE BLOCK (");
                sb.Append(" RETURNS(i_POLLGROUPID INTEGER) AS  DECLARE VARIABLE i_POLLID INTEGER;  BEGIN ");
                // Check if the group already exists

                if (question.CategoryId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(ObjectName.GetVzfObjectName("CATEGORY", mid));
                    sb.Append(" WHERE CATEGORYID = :i_CATEGORYID INTO :i_POLLGROUPID; ");
                    paramSb.Append("i_CATEGORYID INTEGER = ?,");
                }
                if (question.ForumId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(ObjectName.GetVzfObjectName("FORUM", mid));
                    sb.Append(" WHERE FORUMID = :i_FORUMID INTO :i_POLLGROUPID; ");
                    paramSb.Append("i_FORUMID INTEGER = ?,");
                }

                if (question.TopicId > 0)
                {
                    sb.Append(" SELECT POLLID FROM ");
                    sb.Append(ObjectName.GetVzfObjectName("TOPIC", mid));
                    sb.Append(" WHERE TOPICID = :i_TOPICID INTO :i_POLLGROUPID; ");
                    paramSb.Append("i_TOPICID INTEGER = ?,");
                }


                // if the poll group doesn't exists, create a new one
                sb.Append("IF (i_POLLGROUPID IS NULL) THEN BEGIN ");

                sb.Append("INSERT INTO ");
                sb.Append(ObjectName.GetVzfObjectName("POLLGROUPCLUSTER", mid));
                sb.AppendFormat(
                    "(POLLGROUPID, USERID, FLAGS) VALUES((SELECT NEXT VALUE FOR SEQ_{0}PGC_POLLGROUPID FROM RDB$DATABASE), :i_GROUPUSERID, :i_POLLGROUPFLAGS) RETURNING POLLGROUPID INTO :i_POLLGROUPID;  END ",
                    Config.DatabaseObjectQualifier.ToUpper());

                paramSb.Append("i_GROUPUSERID INTEGER = ?,");
                paramSb.Append("i_POLLGROUPFLAGS INTEGER = ?,");
                if (question.CategoryId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("CATEGORY", mid));
                    sb.Append(" SET POLLGROUPID = :i_POLLGROUPID WHERE CATEGORYID = :i_CATEGORYID; ");

                }
                if (question.ForumId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("FORUM", mid));
                    sb.Append(" SET POLLGROUPID = :i_POLLGROUPID WHERE FORUMID = :i_FORUMID; ");
                }

                if (question.TopicId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("TOPIC", mid));
                    sb.Append(" SET POLLID = :i_POLLGROUPID WHERE TOPICID = :i_TOPICID; ");
                }

                // System.Text.StringBuilder paramSb = new System.Text.StringBuilder("EXECUTE BLOCK ("); 
                // INSERT in poll
                sb.Append(" INSERT INTO ");
                sb.Append(ObjectName.GetVzfObjectName("POLL", mid));
                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {

                    sb.Append("(POLLID,QUESTION, USERID, CLOSES,POLLGROUPID,FLAGS");
                }
                else
                {
                    sb.Append("(POLLID,QUESTION, USERID,POLLGROUPID,FLAGS");
                }

                if (question.QuestionObjectPath.IsSet())
                {
                    sb.Append(", OBJECTPATH");
                }
                if (question.QuestionMimeType.IsSet())
                {
                    sb.Append(", MIMETYPE");
                }
                sb.Append(") VALUES(");

                sb.AppendFormat(
                    "(SELECT NEXT VALUE FOR SEQ_{0}POLL_POLLID FROM RDB$DATABASE),",
                    Config.DatabaseObjectQualifier.ToUpper());
                sb.Append(":i_QUESTION");

                paramSb.Append(" i_QUESTION VARCHAR(255) = ?,");

                sb.Append(",:i_USERID");
                paramSb.Append("i_USERID INTEGER = ?,");

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    sb.Append(",:i_CLOSES");
                    paramSb.Append("i_CLOSES TIMESTAMP = ?,");
                }
                sb.Append(",:i_POLLGROUPID");

                sb.Append(",:i_FLAGS");
                paramSb.Append("i_FLAGS INTEGER = ?,");
                if (question.QuestionObjectPath.IsSet())
                {
                    sb.Append(",:i_QUESTIONOBJECTPATH");
                    paramSb.Append("i_OBJECTPATH VARCHAR(255) = ?,");
                }
                if (question.QuestionMimeType.IsSet())
                {
                    sb.Append(",:i_QUESTIONMIMETYPE");
                    paramSb.Append("i_MIMETYPE VARCHAR(50) = ?,");
                }

                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.Append(") RETURNING POLLID INTO :i_POLLID; ");

                // The cycle through question reply choices to create prepare statement

                // The cycle through question reply choices            
                for (uint choiceCount = 0; choiceCount < question.Choice.GetLength(0); choiceCount++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                    {

                        sb.Append("INSERT INTO ");
                        sb.Append(ObjectName.GetVzfObjectName("CHOICE", mid));

                        sb.Append("(CHOICEID, POLLID,CHOICE,VOTES");
                        if (question.QuestionObjectPath.IsSet())
                        {
                            sb.Append(",OBJECTPATH");
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            sb.Append(",MIMETYPE");
                        }
                        sb.Append(") VALUES(");
                        sb.AppendFormat(
                            "(SELECT NEXT VALUE FOR SEQ_{0}CHOICE_CHOICEID FROM RDB$DATABASE),",
                            Config.DatabaseObjectQualifier.ToUpper());
                        sb.AppendFormat(":i_POLLID,:i_CHOICE{0},:i_VOTES{0}", choiceCount);
                        if (question.QuestionObjectPath.IsSet())
                        {
                            sb.AppendFormat(",:i_CHOICEOBJECTPATH{0}", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            sb.AppendFormat(",:i_CHOICEMIMETYPE{0}", choiceCount);
                        }
                        sb.Append("); ");
                        paramSb.AppendFormat("i_CHOICE{0} VARCHAR(255) = ?,", choiceCount);
                        paramSb.AppendFormat("i_VOTES{0} INTEGER = ?,", choiceCount);

                        if (question.QuestionObjectPath.IsSet())
                        {
                            paramSb.AppendFormat("i_CHOICEOBJECTPATH{0} VARCHAR(255) = ?,", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            paramSb.AppendFormat("i_CHOICEMIMETYPE{0} VARCHAR(50) = ?,", choiceCount);
                        }

                    }

                }

                sb.Append(" SUSPEND; END;");
                using (var cmd = new SQLCommand(mid))
                {

                // cmd.Prepare();   
                /* FbParameter ret = new FbParameter();
                ret.ParameterName = "@OUT_POLLID";
                ret.FbDbType = FbDbType.Integer;
                ret.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ret); */
                object categoryId = DBNull.Value;
                object forumId = DBNull.Value;
                object topicId = DBNull.Value;

               
                if (question.CategoryId > 0)
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_CategoryID", question.CategoryId
                                                                                                  ?? categoryId));
                }
                else if (question.ForumId > 0)
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_ForumID", question.ForumId
                                                                                               ?? forumId));
                }
                else if (question.TopicId > 0)
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_TopicID", question.TopicId
                                                                                               ?? topicId));
                }

                cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_GROUPUSERID", question.UserId));
               
                int pollGroupFlags = question.IsBound ? 0 | 2 : 0;

                cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_POLLGROUPFLAGS", pollGroupFlags));
                cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "QUESTION", question.Question));
                cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_USERID", question.UserId));

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_CLOSES", question.Closes));
                }

                // set poll  flags
                int pollFlags = 0;
                if (question.IsClosedBound)
                {
                    pollFlags = pollFlags | 4;
                }
                if (question.AllowMultipleChoices)
                {
                    pollFlags = pollFlags | 8;
                }
                if (question.ShowVoters)
                {
                    pollFlags = pollFlags | 16;
                }
                if (question.AllowSkipVote)
                {
                    pollFlags = pollFlags | 32;
                }
              
                cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_FLAGS", pollFlags));

                if (question.QuestionObjectPath.IsSet())
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_QUESTIONOBJECTPATH", question.QuestionObjectPath));
                }
                if (question.QuestionMimeType.IsSet())
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_QUESTIONMIMETYPE", question.QuestionMimeType));
                }

                for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetLength(0); choiceCount1++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("i_CHOICE{0}", choiceCount1), question.Choice[0, choiceCount1]));
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, String.Format("i_VOTES{0}", choiceCount1), 0));
                       
                        if (question.Choice[1, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("i_CHOICEOBJECTPATH{0}", choiceCount1), question.Choice[1, choiceCount1].IsNotSet()
                                             ? String.Empty
                                             : question.Choice[1, choiceCount1]));
                        }
                        if (question.Choice[2, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("i_CHOICEMIMETYPE{0}", choiceCount1), question.Choice[2, choiceCount1].IsNotSet()
                                            ? String.Empty
                                            : question.Choice[2, choiceCount1]));
                        }
                    }
                }



                // cmd.Prepare();  
                cmd.CommandText.AppendQuery(paramSb.ToString().TrimEnd(',') + ") " + sb);

                int? result = Convert.ToInt32(cmd.ExecuteScalar(CommandType.Text, true));



                return result;
            }

        }
            return null;
        }

     
    }
}
