using System;
using System.Collections.Generic;
using System.Text;


namespace VZF.Data.Mysql
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
        public static int? poll_save_mysql([NotNull] int? mid, List<PollSaveList> pollList)
{
            foreach (PollSaveList question in pollList)
            {
                        try
                        {
                            int? myPollID = null;

                            StringBuilder sb = new StringBuilder();

                            // Check if the group already exists
                            if (question.TopicId > 0)
                            {
                                sb.Append("select PollID  from ");
                                sb.Append(ObjectName.GetVzfObjectName("Topic", mid));
                                sb.Append(" WHERE TopicID = ?i_TopicID; ");
                            }
                            else if (question.ForumId > 0)
                            {

                                sb.Append("select PollGroupID  from ");
                                sb.Append(ObjectName.GetVzfObjectName("Forum", mid));
                                sb.Append(" WHERE ForumID = ?i_ForumID;");
                            }
                            else if (question.CategoryId > 0)
                            {

                                sb.Append("select PollGroupID  from ");
                                sb.Append(ObjectName.GetVzfObjectName("Category", mid));
                                sb.Append(" WHERE CategoryID = ?i_CategoryID;");
                            }
                            int? pollGroupId = null;
                            object pollGroupIdObj;
                            using (var cmdPoll = new SQLCommand(mid))
                            {
                                if (question.TopicId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_TopicID", question.TopicId));
                                }
                                else if (question.ForumId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_ForumID", question.ForumId));
                                }
                                else if (question.CategoryId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_CategoryID", question.CategoryId));
                                }

                                cmdPoll.CommandText.AppendQuery(sb.ToString());

                                pollGroupIdObj = cmdPoll.ExecuteScalar(CommandType.Text, true);
                            }

                            sb = new StringBuilder();
                            // the group doesn't exists, create a new one
                            int pgIdcheck = 0;
                            if (!int.TryParse(pollGroupIdObj.ToString(), out pgIdcheck))
                            {
                                sb.Append(
                                    string.Format(
                                        "INSERT INTO {0}(UserID,Flags ) VALUES(?i_UserID, ?i_Flags); ",
                                        ObjectName.GetVzfObjectName("PollGroupCluster", mid)));
                                //  sb.Append("SELECT PollGroupID FROM ");
                                sb.Append("SELECT LAST_INSERT_ID(); ");
                                //  sb.Append(ObjectName.GetVzfObjectName("PollGroupCluster"));
                                //   sb.Append(" WHERE PollGroupID = LAST_INSERT_ID(); ");
                                using (var cmdPoll = new SQLCommand(mid))
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_UserID", question.UserId));

                                    // set poll group flags
                                    int groupFlags = 0;
                                    if (question.IsBound)
                                    {
                                        groupFlags = groupFlags | 2;
                                    }

                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_Flags", groupFlags));
                                   
                                    cmdPoll.CommandText.AppendQuery(sb.ToString());
                                  
                                    pollGroupId =
                                        Convert.ToInt32(cmdPoll.ExecuteScalar(CommandType.Text, true));
                                }
                            }
                            else
                            {

                                sb.Append(
                                    String.Format(
                                        "UPDATE {0} SET Flags = (CASE WHEN Flags <> 0 AND (?i_Flags & 2) = 2 THEN Flags = Flags | 2 ELSE ?i_Flags END)  WHERE PollGroupID = ?i_PollGroupID; ",
                                        ObjectName.GetVzfObjectName("PollGroupCluster", mid)));
                                using (var cmdPollUpdate = new SQLCommand(mid))
                                {
                                    cmdPollUpdate.Parameters.Add(cmdPollUpdate.CreateParameter(DbType.Int32, "?i_UserID", question.UserId));
                                  
                                    // set poll group flags
                                    int groupFlags = 0;
                                    if (question.IsBound)
                                    {
                                        groupFlags = groupFlags | 2;
                                    }
                                    cmdPollUpdate.Parameters.Add(cmdPollUpdate.CreateParameter(DbType.Int32, "?i_Flags", groupFlags));
                                    cmdPollUpdate.Parameters.Add(cmdPollUpdate.CreateParameter(DbType.Int32, "?i_PollGroupID", pollGroupId));
                                    
                                    cmdPollUpdate.CommandText.AppendQuery(sb.ToString());

                                    cmdPollUpdate.ExecuteNonQuery(CommandType.Text, true);
                                   

                                }
                                pollGroupId = (int?)pollGroupIdObj;
                            }



                            sb =
                                new System.Text.StringBuilder(
                                    string.Format("INSERT INTO {0}", ObjectName.GetVzfObjectName("Poll", mid)));

                            if (question.Closes > DateTimeHelper.SqlDbMinTime())
                            {
                                sb.Append("(Question,Closes, UserID,PollGroupID,ObjectPath,MimeType,Flags) ");
                            }
                            else
                            {
                                sb.Append("(Question,UserID,PollGroupID,ObjectPath,MimeType,Flags) ");
                            }

                            sb.Append(" VALUES(");
                            sb.Append("?i_Question");

                            if (question.Closes > DateTimeHelper.SqlDbMinTime())
                            {
                                sb.Append(",?i_Closes");
                            }
                            sb.Append(",?i_UserID,?i_PollGroupID,?i_QuestionObjectPath,?i_QuestionMimeType,?i_PollFlags");
                            sb.Append("); ");

                            sb.Append("SELECT ");
                            sb.Append(" LAST_INSERT_ID(); ");
                            using (var cmdPoll = new SQLCommand(mid))
                            {
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.String, "?i_Question", question.Question));

                                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.DateTime, "?i_Closes", question.Closes));
                                }
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_UserID", question.UserId));
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_PollGroupID", pollGroupId));

                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.String, "?i_QuestionObjectPath", question.QuestionObjectPath));
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.String, "?i_QuestionMimeType", question.QuestionMimeType));
                              
                                int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                                pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                                pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                                pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;
                             
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_PollFlags", pollFlags));
                       
                                cmdPoll.CommandText.AppendQuery(sb.ToString());

                                myPollID = Convert.ToInt32(cmdPoll.ExecuteScalar(CommandType.Text, true));

                            }

                            sb = new StringBuilder();

                            // The cycle through question reply choices            
                            for (uint choiceCount = 0;
                                 choiceCount < question.Choice.GetUpperBound(1) + 1;
                                 choiceCount++)
                            {
                                if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                                {
                                    // sb.Append(string.Format(" INSERT INTO  "));
                                    //  sb.Append(ObjectName.GetVzfObjectName("Choice"));
                                    //   sb.Append(string.Format("("));
                                    // sb.Append(string.Format(" INSERT INTO {0}(",ObjectName.GetVzfObjectName("Choice")));
                                    //  sb.AppendFormat("PollID,Choice,Votes) VALUES(?PollID{0},?Choice{0},?Votes{0}); ",
                                    //                  choiceCount);
                                    sb.AppendFormat(
                                        "INSERT INTO  {0}(PollID,Choice,Votes,ObjectPath,MimeType) VALUES(?i_PollID{1},?i_Choice{1},?i_Votes{1},?i_ChoiceObjectPath{1},?i_ChoiceMimeType{1}); ",
                                        ObjectName.GetVzfObjectName("Choice", mid),
                                        choiceCount);
                                }
                            }
                            using (var cmd = new SQLCommand(mid))
                            {
                                /* MySqlParameter ret = new MySqlParameter();
                        ret.ParameterName = "?PollIDOut";
                        ret.MySqlDbType = MySqlDbType.Int32;
                        ret.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ret); 
                        cmd.Parameters.Add("?Question", MySqlDbType.VarChar ).Value = question.Question;

                        if (question.Closes > DateTimeHelper.SqlDbMinTime())
                        {
                            cmd.Parameters.Add("?Closes", MySqlDbType.DateTime ).Value = question.Closes;
                        }
                       */
                              
                                for (uint choiceCount1 = 0;
                                     choiceCount1 < question.Choice.GetUpperBound(1) + 1;
                                     choiceCount1++)
                                {
                                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                                    {
                                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "?i_PollID", myPollID));
                                        cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("?i_Choice{0}", choiceCount1), question.Choice[0, choiceCount1]));
                                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, String.Format("?i_Votes{0}", choiceCount1), 0));
                                        cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("?i_ChoiceObjectPath{0}", choiceCount1), question.Choice[1, choiceCount1].IsNotSet()
                                                        ? String.Empty
                                                        : question.Choice[1, choiceCount1]));
                                        cmd.Parameters.Add(cmd.CreateParameter(DbType.String, String.Format("?i_ChoiceMimeType{0}", choiceCount1), question.Choice[2, choiceCount1].IsNotSet()
                                                        ? String.Empty
                                                        : question.Choice[2, choiceCount1]));
                                    }
                                }
                                cmd.CommandText.AppendQuery(sb.ToString());

                                cmd.ExecuteNonQuery(CommandType.Text, true);

                            }

                            sb = new StringBuilder();
                            // fill a pollgroup field - double work if a poll exists 
                            if (question.TopicId > 0)
                            {

                                sb.Append("UPDATE ");
                                sb.Append(ObjectName.GetVzfObjectName("Topic", mid));
                                sb.Append(" SET PollID = ?i_NewPollGroupID WHERE TopicID =?i_TopicID; ");

                            }

                            // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                            if (question.ForumId > 0)
                            {
                                sb.Append("UPDATE ");
                                sb.Append(ObjectName.GetVzfObjectName("Forum", mid));
                                sb.Append(" SET PollGroupID= ?i_NewPollGroupID WHERE ForumID= ?i_ForumID; ");
                            }

                            // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                            if (question.CategoryId > 0)
                            {
                                sb.Append("UPDATE ");
                                sb.Append(ObjectName.GetVzfObjectName("Category", mid));
                                sb.Append(" SET PollGroupID = ?i_NewPollGroupID WHERE CategoryID= ?i_CategoryID; ");
                            }

                            using (var cmdPoll = new SQLCommand(mid))
                            {
                                cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_NewPollGroupID", pollGroupId));

                                if (question.TopicId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_TopicID", question.TopicId));
                                }

                                // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                                if (question.ForumId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_ForumID", question.ForumId));
                                }

                                // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                                if (question.CategoryId > 0)
                                {
                                    cmdPoll.Parameters.Add(cmdPoll.CreateParameter(DbType.Int32, "?i_CategoryID", question.CategoryId));
                                }

                                cmdPoll.CommandText.AppendQuery(sb.ToString());

                                cmdPoll.ExecuteNonQuery(CommandType.Text, true);


                            }

                            /*if (ret.Value != DBNull.Value)
                       {
                           return (int?)ret.Value;
                       }*/
                           
                            return pollGroupId;
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);

                        }
                        finally
                        {
                            // connMan.CloseConnection();
                        }
                   
            }
            return null;

        }
    }
}
