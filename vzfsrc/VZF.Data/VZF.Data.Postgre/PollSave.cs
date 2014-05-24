using System;
using System.Collections.Generic;
using System.Text;


namespace VZF.Data.Postgre
{
    using System.Data;

    using VZF.Data.DAL;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Types;
    using YAF.Types.Objects;

    public static partial class Db
    {
        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save_pgsql([NotNull] int? mid, List<PollSaveList> pollList)
        {
            int? currPoll;
            int? pollGroup = null;
            foreach (PollSaveList question in pollList)
            {
                StringBuilder pgStr = new StringBuilder();
                // Check if the group already exists
                if (question.TopicId > 0)
                {
                    pgStr.Append("select pollid  from ");
                    pgStr.Append(ObjectName.GetVzfObjectName("topic", mid));
                    pgStr.Append(" WHERE topicid = :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    pgStr.Append("select  pollgroupid  from ");
                    pgStr.Append(ObjectName.GetVzfObjectName("forum", mid));
                    pgStr.Append(" WHERE forumid =:i_forumid");
                }
                else if (question.CategoryId > 0)
                {
                    pgStr.Append("select pollgroupid  from ");
                    pgStr.Append(ObjectName.GetVzfObjectName("category", mid));
                    pgStr.Append(" WHERE categoryid = :i_categoryid");
                }

                using (var cmdPg = new SQLCommand(mid))
                {
                    // Add parameters
                    if (question.TopicId > 0)
                    {
                        cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_topicid", question.TopicId));
                    }
                    else if (question.ForumId > 0)
                    {
                        cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_forumid", question.ForumId));
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_categoryid", question.CategoryId));
                    }

                    cmdPg.CommandText.AppendQuery(pgStr.ToString());

                    object pgexist = cmdPg.ExecuteScalar(CommandType.Text,true);
                    if (pgexist != DBNull.Value)
                    {
                        pollGroup = Convert.ToInt32(pgexist);
                    }
                }

                // buck stops here
                // the group doesn't exists, create a new one
                if (pollGroup == null)
                {
                    pgStr = new StringBuilder();
                    pgStr.Append("INSERT INTO ");
                    pgStr.Append(ObjectName.GetVzfObjectName("pollgroupcluster", mid));
                    pgStr.Append("(userid,flags ) VALUES(:i_userid, :i_flags) RETURNING pollgroupid; ");
                    using (var cmdPgIns = new SQLCommand(mid))
                    {
                        // set poll group flags
                        int pollGroupFlags = 0;
                        if (question.IsBound)
                        {
                            pollGroupFlags = pollGroupFlags | 2;
                        }

                        // Add parameters
                        cmdPgIns.Parameters.Add(cmdPgIns.CreateParameter(DbType.Int32, "i_userid", question.UserId));
                        cmdPgIns.Parameters.Add(cmdPgIns.CreateParameter(DbType.Int32, "i_flags", pollGroupFlags));

                        cmdPgIns.CommandText.AppendQuery(pgStr.ToString());
                        pollGroup = (int?)cmdPgIns.ExecuteScalar(CommandType.Text, true);
                    }
                }

                using (var cmd = new SQLCommand(mid))
                {
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_question", question.Question));

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_closes", question.Closes));
                    }
                    else
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_closes", null));
                    }

                    int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                    pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                    pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                    pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;

                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_userid", question.UserId));
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_pollgroupid", pollGroup));
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_objectpath", question.QuestionObjectPath));
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_mimetype", question.QuestionMimeType));
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_flags", pollFlags));
                 
                    cmd.CommandText.AppendObjectQuery("poll_save", mid);
                    currPoll = cmd.ExecuteScalar(CommandType.StoredProcedure, true).ToType<int>();
                }


                // The cycle through question reply choices  
                int chl = question.Choice.GetUpperBound(1) + 1;
                for (uint choiceCount = 0; choiceCount < chl; choiceCount++)
                {
                    if (question.Choice[0, choiceCount].Trim().Length > 0)
                    {
                        StringBuilder sbChoice = new StringBuilder();
                        sbChoice.Append("INSERT INTO ");
                        sbChoice.Append(ObjectName.GetVzfObjectName("choice", mid));
                        sbChoice.AppendFormat(
                            "(pollid,choice,votes,objectpath,mimetype) VALUES (:i_pollid{0}, :i_choice{0}, :i_votes{0}, :i_objectpath{0}, :i_mimetype{0}); ",
                            choiceCount);
                        using (var cmdChoice = new SQLCommand(mid))
                        {
                            cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.Int32, String.Format("i_pollid{0}", choiceCount), currPoll));
                            cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.String, String.Format("i_choice{0}", choiceCount), 
                                question.Choice[0, choiceCount]));
                            cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.Int32, String.Format("i_votes{0}", choiceCount), 0));
                            cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.String, String.Format("i_objectpath{0}", choiceCount),
                                question.Choice[1, choiceCount].IsNotSet() ? String.Empty : question.Choice[1, choiceCount]));
                            cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.String, String.Format("i_mimetype{0}", choiceCount),
                                question.Choice[2, choiceCount].IsNotSet() ? String.Empty : question.Choice[2, choiceCount]));
                           

                            cmdChoice.CommandText.AppendQuery(sbChoice.ToString());
                            cmdChoice.ExecuteNonQuery(CommandType.Text, true);
                        }

                    }

                }
                //   var cmd = new NpgsqlCommand();
                //  cmd.CommandText = paramSb.ToString() + ")" + sb.ToString();
                //   NpgsqlConnection con = PostgreDbAccess.Current.GetConnectionManager().DBConnection;
                // con.Open();
                //  cmd.Connection = con;
                //   IDbTransaction trans = cmd.Connection.BeginTransaction();
                //    cmd.Transaction = trans;
                //    cmd.CommandText = sb.ToString();


                /* using (var cmd1 = PostgreDbAccess.GetCommand(sb.ToString(), true))
                {


                    // Add parameters
                     cmd1.Parameters.Add(new NpgsqlParameter("question", NpgsqlDbType.Varchar));

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd1.Parameters.Add(new NpgsqlParameter("closes", NpgsqlDbType.Timestamp));
                    } 
                    for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetLength(0); choiceCount1++)
                    {
                        if (question.Choice[0, choiceCount1].Trim().Length > 0)
                        {
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("choice{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[0, choiceCount1];
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("votes{0}", choiceCount1),
                                                                    NpgsqlDbType.Integer)).Value = 0;
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("objectpath{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[1, choiceCount1].IsNotSet() ? String.Empty : question.Choice[1, choiceCount1];
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("mimetype{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[2, choiceCount1].IsNotSet() ? String.Empty : question.Choice[2, choiceCount1];
                        }
                    }
                     int? result = (int?)PostgreDbAccess.ExecuteNonQueryInt(cmd1, true);
                }
            */

                // Add pollgroup id to an object
                StringBuilder Usb = new StringBuilder();
                //cmd2.Parameters.Add(new NpgsqlParameter(":i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                if (question.TopicId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(ObjectName.GetVzfObjectName("topic", mid));
                    Usb.Append(" SET pollid = :i_pollid WHERE topicid= :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(ObjectName.GetVzfObjectName("forum", mid));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where forumid = :i_forumid; ");

                }
                else if (question.CategoryId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(ObjectName.GetVzfObjectName("category", mid));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where categoryid = :i_categoryid; ");
                }


                using (var cmd2 = new SQLCommand(mid))
                {
                    cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_pollid", pollGroup));
                   
                    //cmd2.Parameters.Add(new NpgsqlParameter(":i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                    if (question.TopicId > 0)
                    {
                        cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_topicid", question.TopicId));
                    }
                    else if (question.ForumId > 0)
                    {
                        cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_forumid", question.ForumId));
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_categoryid", question.CategoryId));
                    }

                    cmd2.CommandText.AppendQuery(Usb.ToString());
                    cmd2.ExecuteNonQuery(CommandType.Text, true);
                }


                /* if (ret.Value != DBNull.Value)
                     {
                         return (int?)ret.Value;
                     }*/

                //  }
                //   trans.Commit();
                //   con.Close();

            }

            return pollGroup;
        }
    }
}
