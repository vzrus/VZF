using System;
using System.Collections.Generic;
using System.Text;


namespace VZF.Data.MsSql
{
    using System.Data;

    using VZF.Data.DAL;
 
    using VZF.Utils;
    using VZF.Utils.Helpers;
   
    using YAF.Types;
    using YAF.Types.Objects;

    public static partial class Db
    {
        public static int? poll_save_mssql([NotNull] int? mid, [NotNull] List<PollSaveList> pollList)
        {
            foreach (PollSaveList question in pollList)
            {
                var sb = new StringBuilder();

                // Check if the group already exists
                if (question.TopicId > 0)
                {
                    sb.Append("select @PollGroupID = PollID  from ");
                    sb.Append(ObjectName.GetVzfObjectName("Topic", mid));
                    sb.Append(" WHERE TopicID = @TopicID; ");
                }
                else if (question.ForumId > 0)
                {
                    sb.Append("select @PollGroupID = PollGroupID  from ");
                    sb.Append(ObjectName.GetVzfObjectName("Forum", mid));
                    sb.Append(" WHERE ForumID = @ForumID");
                }
                else if (question.CategoryId > 0)
                {
                    sb.Append("select @PollGroupID = PollGroupID  from ");
                    sb.Append(ObjectName.GetVzfObjectName("Category", mid));
                    sb.Append(" WHERE CategoryID = @CategoryID");
                }

                // the group doesn't exists, create a new one
                sb.Append("IF @PollGroupID IS NULL BEGIN INSERT INTO ");
                sb.Append(ObjectName.GetVzfObjectName("PollGroupCluster", mid));
                sb.Append("(UserID,Flags ) VALUES(@UserID, @Flags) SET @NewPollGroupID = SCOPE_IDENTITY(); END; ");

                sb.Append("INSERT INTO ");
                sb.Append(ObjectName.GetVzfObjectName("Poll", mid));

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    sb.Append("(Question,Closes, UserID,PollGroupID,ObjectPath,MimeType,Flags) ");
                }
                else
                {
                    sb.Append("(Question,UserID, PollGroupID, ObjectPath, MimeType,Flags) ");
                }

                sb.Append(" VALUES(");
                sb.Append("@Question");

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    sb.Append(",@Closes");
                }

                sb.Append(
                    ",@UserID, (CASE WHEN  @NewPollGroupID IS NULL THEN @PollGroupID ELSE @NewPollGroupID END), @QuestionObjectPath,@QuestionMimeType,@PollFlags");
                sb.Append("); ");
                sb.Append("SET @PollID = SCOPE_IDENTITY(); ");

                // The cycle through question reply choices
                for (uint choiceCount = 0; choiceCount < question.Choice.GetUpperBound(1) + 1; choiceCount++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                    {
                        sb.Append("INSERT INTO ");
                        sb.Append(ObjectName.GetVzfObjectName("Choice", mid));
                        sb.Append("(PollID,Choice,Votes,ObjectPath,MimeType) VALUES (");
                        sb.AppendFormat(
                            "@PollID,@Choice{0},@Votes{0},@ChoiceObjectPath{0}, @ChoiceMimeType{0}",
                            choiceCount);
                        sb.Append("); ");
                    }
                }

                // we don't update if no new group is created 
                sb.Append("IF  @PollGroupID IS NULL BEGIN  ");

                // fill a pollgroup field - double work if a poll exists 
                if (question.TopicId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("Topic", mid));
                    sb.Append(" SET PollID = @NewPollGroupID WHERE TopicID = @TopicID; ");
                }

                // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                if (question.ForumId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("Forum", mid));
                    sb.Append(" SET PollGroupID= @NewPollGroupID WHERE ForumID= @ForumID; ");
                }

                // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                if (question.CategoryId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(ObjectName.GetVzfObjectName("Category", mid));
                    sb.Append(" SET PollGroupID= @NewPollGroupID WHERE CategoryID= @CategoryID; ");
                }

                // fill a pollgroup field in Board Table if the call comes from the main page poll 
                sb.Append("END;  ");

                using (var cmd = new SQLCommand(mid))
                {
                    var ret = cmd.CreateParameter(DbType.Int32, "i_PollID", null, ParameterDirection.Output);
                    cmd.Parameters.Add(ret);

                    var ret2 = cmd.CreateParameter(DbType.Int32, "i_PollGroupID", null, ParameterDirection.Output);
                    cmd.Parameters.Add(ret2);

                    var ret3 = cmd.CreateParameter(DbType.Int32, "i_NewPollGroupID", null, ParameterDirection.Output);
                    cmd.Parameters.Add(ret3);

                    cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_Question", question.Question));

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_Closes", question.Closes));
                    }

                    // set poll group flags
                    int groupFlags = 0;
                    if (question.IsBound)
                    {
                        groupFlags = groupFlags | 2;
                    }

                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_UserID", question.UserId));
                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_Flags", groupFlags));
                    cmd.Parameters.Add(
                        cmd.CreateParameter(
                            DbType.String,
                            "i_QuestionObjectPath",
                            string.IsNullOrEmpty(question.QuestionObjectPath)
                                ? String.Empty
                                : question.QuestionObjectPath));
                    cmd.Parameters.Add(
                        cmd.CreateParameter(
                            DbType.String,
                            "i_QuestionMimeType",
                            string.IsNullOrEmpty(question.QuestionMimeType) ? String.Empty : question.QuestionMimeType));

                    int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                    pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                    pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                    pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;

                    cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_PollFlags", pollFlags));

                    for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetUpperBound(1) + 1; choiceCount1++)
                    {
                        if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                        {
                            cmd.Parameters.Add(
                                cmd.CreateParameter(
                                    DbType.String,
                                    String.Format("i_Choice{0}", choiceCount1),
                                    question.Choice[0, choiceCount1]));
                            cmd.Parameters.Add(
                                cmd.CreateParameter(DbType.Int32, String.Format("i_Votes{0}", choiceCount1), 0));
                            cmd.Parameters.Add(
                                cmd.CreateParameter(
                                    DbType.String,
                                    String.Format("i_ChoiceObjectPath{0}", choiceCount1),
                                    question.Choice[1, choiceCount1].IsNotSet()
                                        ? String.Empty
                                        : question.Choice[1, choiceCount1]));
                            cmd.Parameters.Add(
                                cmd.CreateParameter(
                                    DbType.String,
                                    String.Format("i_ChoiceMimeType{0}", choiceCount1),
                                    question.Choice[2, choiceCount1].IsNotSet()
                                        ? String.Empty
                                        : question.Choice[2, choiceCount1]));
                        }
                    }

                    if (question.TopicId > 0)
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_TopicID", question.TopicId));
                    }

                    if (question.ForumId > 0)
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_ForumID", question.ForumId));
                    }

                    if (question.CategoryId > 0)
                    {
                        cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_CategoryID", question.CategoryId));
                    }

                    cmd.CommandText.AppendQuery(sb.ToString());
                    cmd.ExecuteNonQuery(CommandType.Text, true);

                    if (ret.Value != DBNull.Value)
                    {
                        return (int?)ret.Value;
                    }
                }
            }

            return null;
        }
    }
}
