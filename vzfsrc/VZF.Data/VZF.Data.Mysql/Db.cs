// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="Db.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The MySQL Db Access.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Mysql
{
    using System;
    using System.Collections.Generic;
 
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;


    using MySql.Data.MySqlClient;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;
  

    /// <summary>
    /// The Db.
    /// </summary>
    public static class Db
    {
        // added by vzrus
        #region ConnectionStringOptions

        /// <summary>
        /// Gets the provider assembly name.
        /// </summary>
        public static string ProviderAssemblyName
        {
            get
            {
                return "MySql.Data.MySqlClient";
            }
        }

        public static bool PasswordPlaceholderVisible
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Common

        /// <summary>
        /// The db size old.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DbSizeOld([NotNull] string connectionString)
        {
            string version;
            using (var cmd1 = new MySqlCommand("SELECT VERSION()"))
            {
                cmd1.CommandType = CommandType.Text;
                version = MySqlDbAccess.ExecuteScalar(cmd1, false, connectionString).ToString();
            }

            var sb = new StringBuilder();
            sb.Append("SELECT s.schema_name, ");
            sb.Append(
                "(SELECT COUNT(table_name) FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine ='InnoDB'  AND t.TABLE_TYPE='BASE TABLE') total_tables, ");
            sb.Append(
                "(SELECT COUNT(table_name) FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.TABLE_TYPE='VIEW') total_views, ");
            sb.Append(
                "(SELECT COUNT(ROUTINE_NAME) FROM INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.ROUTINES r ON s.schema_name = r.ROUTINE_SCHEMA WHERE r.ROUTINE_TYPE='PROCEDURE') total_procedures, ");
            sb.Append(
                "(SELECT COUNT(ROUTINE_NAME) FROM INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.ROUTINES r ON s.schema_name = r.ROUTINE_SCHEMA WHERE r.ROUTINE_TYPE='FUNCTION') total_functions, ");
            sb.Append("CAST(CONCAT(IFNULL(ROUND((SUM(t.data_length)+SUM(t.index_length))/1024/1024,2),0.00),");
            sb.Append("'Mb'");
            sb.Append(") AS CHAR) total_size, ");
            sb.Append("CAST(CONCAT(IFNULL(ROUND(SUM(t.index_length)/1024/1024,2),0.00),");
            sb.Append("'Mb'");
            sb.Append(") AS CHAR) index_size, ");
            sb.Append(
                " CAST(CONCAT(IFNULL(ROUND(((SUM(t.data_length)+SUM(t.index_length))-SUM(t.data_free))/1024/1024,2),0.00),");
            sb.Append("'Mb'");
            sb.Append(") AS CHAR) data_used, ");
            sb.Append(" CAST(CONCAT(IFNULL(ROUND(SUM(data_free)/1024/1024,2),0.00),");
            sb.Append("'Mb'");
            sb.Append(") AS CHAR) data_free, ");
            if (version.Contains("5.1"))
            {
                sb.Append(
                    " CAST(CONCAT(IFNULL(ROUND((((SUM(t.data_length)+SUM(t.index_length))-SUM(t.data_free))/((SUM(t.data_length)+SUM(t.index_length)))*100),2),0.00),");
                sb.Append(@"'Mb'");
                sb.Append(") AS CHAR) pct_used ");
            }
            else
            {
                sb.Append(
                    " CAST(CONCAT(IFNULL(ROUND((((SUM(t.data_length)+SUM(t.index_length))-SUM(t.data_free))/((SUM(t.data_length)+SUM(t.index_length)))*100),2),0.00),");
                sb.Append(@"'%'");
                sb.Append(") AS CHAR) pct_used ");
            }

            sb.Append(
                "FROM INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine = ");
            sb.Append("'InnoDB' ");
            sb.Append("GROUP BY s.schema_name ORDER BY pct_used DESC");
            sb.Append(";");
            using (var cmd = new MySqlCommand(sb.ToString()))
            {
                var sb1 = new StringBuilder();
                cmd.CommandType = CommandType.Text;
                DataTable dt = MySqlDbAccess.GetData(cmd, connectionString);
                int cnt = dt.Columns.Count - 1;
                DataRow dr = dt.Rows[0];

                for (int i = 0; i <= cnt; i++)
                {
                    sb1.Append(dt.Columns[i].ColumnName);
                    sb1.Append(" = ");
                    sb1.Append(dr[i]);
                    sb1.Append(" | ");
                }

                return sb1.ToString();
            }
        }

        
        // MySQL InnoDB engine currently don't support fulltext....

        /// <summary>
        /// The _full text supported.
        /// </summary>
        private static bool _fullTextSupported;

        /// <summary>
        /// Gets a value indicating whether full text supported.
        /// </summary>
        public static bool FullTextSupported
        {
            get
            {
                return _fullTextSupported;
            }
        }

        /// <summary>
        /// The _full text script.
        /// </summary>
        private const string _fullTextScript = "mysql/fulltext.sql";

        /// <summary>
        /// Gets the full text script.
        /// </summary>
        public static string FullTextScript
        {
            get
            {
                return _fullTextScript;
            }
        }

        /// <summary>
        /// The _script list.
        /// </summary>
        private static readonly string[] _scriptList =
            {
                "mysql/tables.sql", "mysql/pkeys.sql", "mysql/indexes.sql",
                "mysql/fkeys.sql", "mysql/constraints.sql",
                "mysql/triggers.sql", "mysql/types.sql", "mysql/views.sql",
                "mysql/procedures.sql", "mysql/functions.sql",
                "mysql/providers/tables.sql", "mysql/providers/pkeys.sql",
                "mysql/providers/indexes.sql", "mysql/providers/procedures.sql"
            };

        /// <summary>
        /// Gets the script list.
        /// </summary>
        public static string[] ScriptList
        {
            get
            {
                return _scriptList;
            }
        }

        /// <summary>
        /// The get boolean registry value.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool GetBooleanRegistryValue([NotNull] string connectionString, string name)
        {
            using (DataTable dt = Db.registry_list(connectionString, name, DBNull.Value))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i;
                    return int.TryParse(dr["Value"].ToString(), out i)
                               ? Convert.ToBoolean(i)
                               : Convert.ToBoolean(dr["Value"]);
                }
            }
            return false;
        }

        #endregion
        
        #region yaf_Message
      
        private static void message_deleteRecursively(
            [NotNull] string connectionString,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool isLinked,
            bool eraseMessages)
        {
            bool UseFileTable = GetBooleanRegistryValue(connectionString, "UseFileTable");

            if (DeleteLinked)
            {
                //Delete replies
                using (var cmd = MySqlDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_MessageID", MySqlDbType.Int32).Value = messageID;

                    DataTable tbReplies = MySqlDbAccess.GetData(cmd, connectionString);

                    foreach (DataRow row in tbReplies.Rows)
                        message_deleteRecursively(
                            connectionString,
                            row["MessageID"],
                            isModeratorChanged,
                            deleteReason,
                            isDeleteAction,
                            DeleteLinked,
                            true,
                            eraseMessages);
                }
            }

            //If the files are actually saved in the Hard Drive
            if (!UseFileTable)
            {
                using (var cmd = MySqlDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_MessageID", MySqlDbType.Int32).Value = messageID;
                    cmd.Parameters.Add("i_AttachmentID", MySqlDbType.Int32).Value = DBNull.Value;
                    cmd.Parameters.Add("i_BoardID", MySqlDbType.Int32).Value = DBNull.Value;
                    cmd.Parameters.Add("i_PageIndex", MySqlDbType.Int32).Value = DBNull.Value;
                    cmd.Parameters.Add("i_PageSize", MySqlDbType.Int32).Value = DBNull.Value;

                    DataTable tbAttachments = MySqlDbAccess.GetData(cmd, connectionString);
                    string uploadDir =
                        HostingEnvironment.MapPath(
                            String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads));

                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format(
                                "{0}/{1}.{2}.yafupload", uploadDir, row["MessageID"], row["FileName"]);

                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch
                        {
                            // error deleting that file... 
                        }
                    }
                }
            }

            // Ederon : erase message for good
            if (eraseMessages)
            {
                using (var cmd = MySqlDbAccess.GetCommand("message_delete"))
                {
                    //if (eraseMessages == null) { eraseMessages = false; }                   

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_MessageID", MySqlDbType.Int32).Value = messageID;
                    cmd.Parameters.Add("i_EraseMessage", MySqlDbType.Byte).Value = eraseMessages;

                    MySqlDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            else
            {
                //Delete Message
                // undelete function added
                using (var cmd = MySqlDbAccess.GetCommand("message_deleteundelete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_MessageID", MySqlDbType.Int32).Value = messageID;
                    cmd.Parameters.Add("i_isModeratorChanged", MySqlDbType.Byte).Value = isModeratorChanged;
                    cmd.Parameters.Add("i_DeleteReason", MySqlDbType.VarChar).Value = deleteReason;
                    cmd.Parameters.Add("i_isDeleteAction", MySqlDbType.Byte).Value = isDeleteAction;

                    MySqlDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
        }
       

        #endregion        

        #region yaf_Poll

     
        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save([NotNull] string connectionString, List<PollSaveList> pollList)
        {


            foreach (PollSaveList question in pollList)
            {
                using (MySqlDbConnectionManager connMan = new MySqlDbConnectionManager(connectionString))
                {
                    using (
                        MySqlTransaction trans =
                            connMan.OpenDBConnection(connectionString).BeginTransaction(MySqlDbAccess.IsolationLevel))
                    {
                        try
                        {
                            int? myPollID = null;

                            StringBuilder sb = new StringBuilder();

                            // Check if the group already exists
                            if (question.TopicId > 0)
                            {
                                sb.Append("select PollID  from ");
                                sb.Append(MySqlDbAccess.GetObjectName("Topic"));
                                sb.Append(" WHERE TopicID = ?TopicID; ");
                            }
                            else if (question.ForumId > 0)
                            {

                                sb.Append("select PollGroupID  from ");
                                sb.Append(MySqlDbAccess.GetObjectName("Forum"));
                                sb.Append(" WHERE ForumID = ?ForumID;");
                            }
                            else if (question.CategoryId > 0)
                            {

                                sb.Append("select PollGroupID  from ");
                                sb.Append(MySqlDbAccess.GetObjectName("Category"));
                                sb.Append(" WHERE CategoryID = ?CategoryID;");
                            }
                            int? pollGroupId = null;
                            object pollGroupIdObj = null;
                            using (var cmdPoll = MySqlDbAccess.GetCommand(sb.ToString(), true))
                            {
                                cmdPoll.Transaction = trans;
                                if (question.TopicId > 0)
                                {
                                    cmdPoll.Parameters.Add("?TopicID", MySqlDbType.Int32).Value = question.TopicId;
                                }
                                else if (question.ForumId > 0)
                                {
                                    cmdPoll.Parameters.Add("?ForumID", MySqlDbType.Int32).Value = question.ForumId;
                                }
                                else if (question.CategoryId > 0)
                                {
                                    cmdPoll.Parameters.Add("?CategoryID", MySqlDbType.Int32).Value = question.CategoryId;
                                }
                                pollGroupIdObj = MySqlDbAccess.ExecuteScalar(cmdPoll, false, connectionString);

                            }
                            sb = new StringBuilder();
                            // the group doesn't exists, create a new one
                            int pgIdcheck = 0;
                            if (!int.TryParse(pollGroupIdObj.ToString(), out pgIdcheck))
                            {
                                sb.Append(
                                    string.Format(
                                        "INSERT INTO {0}(UserID,Flags ) VALUES(?UserID, ?Flags); ",
                                        MySqlDbAccess.GetObjectName("PollGroupCluster")));
                                //  sb.Append("SELECT PollGroupID FROM ");
                                sb.Append("SELECT LAST_INSERT_ID(); ");
                                //  sb.Append(MySqlDbAccess.GetObjectName("PollGroupCluster"));
                                //   sb.Append(" WHERE PollGroupID = LAST_INSERT_ID(); ");
                                using (var cmdPoll = MySqlDbAccess.GetCommand(sb.ToString(), true))
                                {
                                    cmdPoll.Transaction = trans;
                                    cmdPoll.Parameters.Add("?UserID", MySqlDbType.Int32).Value = question.UserId;
                                    // set poll group flags
                                    int groupFlags = 0;
                                    if (question.IsBound)
                                    {
                                        groupFlags = groupFlags | 2;
                                    }
                                    cmdPoll.Parameters.Add("?Flags", MySqlDbType.Int32).Value = groupFlags;

                                    pollGroupId =
                                        Convert.ToInt32(MySqlDbAccess.ExecuteScalar(cmdPoll, false, connectionString));

                                }
                            }
                            else
                            {

                                sb.Append(
                                    String.Format(
                                        "UPDATE {0} SET Flags = (CASE WHEN Flags <> 0 AND (?Flags & 2) = 2 THEN Flags = Flags | 2 ELSE ?Flags END)  WHERE PollGroupID = ?PollGroupID; ",
                                        MySqlDbAccess.GetObjectName("PollGroupCluster")));
                                using (var cmdPollUpdate = MySqlDbAccess.GetCommand(sb.ToString(), true))
                                {
                                    cmdPollUpdate.Transaction = trans;
                                    cmdPollUpdate.Parameters.Add("?UserID", MySqlDbType.Int32).Value = question.UserId;
                                    // set poll group flags
                                    int groupFlags = 0;
                                    if (question.IsBound)
                                    {
                                        groupFlags = groupFlags | 2;
                                    }
                                    cmdPollUpdate.Parameters.Add("?Flags", MySqlDbType.Int32).Value = groupFlags;
                                    cmdPollUpdate.Parameters.Add("?PollGroupID", MySqlDbType.Int32).Value = pollGroupId;
                                    MySqlDbAccess.ExecuteNonQuery(cmdPollUpdate, false, connectionString);

                                }
                                pollGroupId = (int?)pollGroupIdObj;
                            }



                            sb =
                                new System.Text.StringBuilder(
                                    string.Format("INSERT INTO {0}", MySqlDbAccess.GetObjectName("Poll")));

                            if (question.Closes > DateTimeHelper.SqlDbMinTime())
                            {
                                sb.Append("(Question,Closes, UserID,PollGroupID,ObjectPath,MimeType,Flags) ");
                            }
                            else
                            {
                                sb.Append("(Question,UserID,PollGroupID,ObjectPath,MimeType,Flags) ");
                            }

                            sb.Append(" VALUES(");
                            sb.Append("?Question");

                            if (question.Closes > DateTimeHelper.SqlDbMinTime())
                            {
                                sb.Append(",?Closes");
                            }
                            sb.Append(",?UserID,?PollGroupID,?QuestionObjectPath,?QuestionMimeType,?PollFlags");
                            sb.Append("); ");

                            sb.Append("SELECT ");
                            sb.Append(" LAST_INSERT_ID(); ");
                            using (var cmdPoll = MySqlDbAccess.GetCommand(sb.ToString(), true))
                            {
                                cmdPoll.Transaction = trans;
                                cmdPoll.CommandType = CommandType.Text;
                                cmdPoll.Parameters.Add("?Question", MySqlDbType.VarChar).Value = question.Question;

                                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                                {
                                    cmdPoll.Parameters.Add("?Closes", MySqlDbType.DateTime).Value = question.Closes;
                                }
                                cmdPoll.Parameters.Add("?UserID", MySqlDbType.VarChar).Value = question.UserId;
                                cmdPoll.Parameters.Add("?PollGroupID", MySqlDbType.VarChar).Value = pollGroupId;
                                cmdPoll.Parameters.Add("?QuestionObjectPath", MySqlDbType.VarChar).Value =
                                    question.QuestionObjectPath;
                                cmdPoll.Parameters.Add("?QuestionMimeType", MySqlDbType.VarChar).Value =
                                    question.QuestionMimeType;
                                int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                                pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                                pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                                pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;

                                cmdPoll.Parameters.Add("?PollFlags", MySqlDbType.VarChar).Value = pollFlags;
                                object dd = MySqlDbAccess.ExecuteScalar(cmdPoll, false, connectionString);
                                myPollID = Convert.ToInt32(dd);

                            }

                            sb = new System.Text.StringBuilder();

                            // The cycle through question reply choices            
                            for (uint choiceCount = 0;
                                 choiceCount < question.Choice.GetUpperBound(1) + 1;
                                 choiceCount++)
                            {
                                if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                                {
                                    // sb.Append(string.Format(" INSERT INTO  "));
                                    //  sb.Append(MySqlDbAccess.GetObjectName("Choice"));
                                    //   sb.Append(string.Format("("));
                                    // sb.Append(string.Format(" INSERT INTO {0}(",MySqlDbAccess.GetObjectName("Choice")));
                                    //  sb.AppendFormat("PollID,Choice,Votes) VALUES(?PollID{0},?Choice{0},?Votes{0}); ",
                                    //                  choiceCount);
                                    sb.AppendFormat(
                                        "INSERT INTO  {0}(PollID,Choice,Votes,ObjectPath,MimeType) VALUES(?PollID{1},?Choice{1},?Votes{1},?ChoiceObjectPath{1},?ChoiceMimeType{1}); ",
                                        MySqlDbAccess.GetObjectName("Choice"),
                                        choiceCount);
                                }
                            }
                            using (var cmd = MySqlDbAccess.GetCommand(sb.ToString(), true))
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
                                cmd.Transaction = trans;
                                for (uint choiceCount1 = 0;
                                     choiceCount1 < question.Choice.GetUpperBound(1) + 1;
                                     choiceCount1++)
                                {
                                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                                    {

                                        cmd.Parameters.Add("?PollID{0}".FormatWith(choiceCount1), MySqlDbType.Int32)
                                           .Value = myPollID;
                                        cmd.Parameters.Add(
                                            String.Format("?Choice{0}", choiceCount1), MySqlDbType.VarChar).Value =
                                            question.Choice[0, choiceCount1];
                                        cmd.Parameters.Add(String.Format("?Votes{0}", choiceCount1), MySqlDbType.Int32)
                                           .Value = 0;
                                        cmd.Parameters.Add(
                                            String.Format("?ChoiceObjectPath{0}", choiceCount1), MySqlDbType.VarChar)
                                           .Value = question.Choice[1, choiceCount1].IsNotSet()
                                                        ? String.Empty
                                                        : question.Choice[1, choiceCount1];
                                        cmd.Parameters.Add(
                                            String.Format("?ChoiceMimeType{0}", choiceCount1), MySqlDbType.VarChar)
                                           .Value = question.Choice[2, choiceCount1].IsNotSet()
                                                        ? String.Empty
                                                        : question.Choice[2, choiceCount1];
                                    }
                                }
                                MySqlDbAccess.ExecuteNonQuery(cmd, false, connectionString);

                            }

                            sb = new StringBuilder();
                            // fill a pollgroup field - double work if a poll exists 
                            if (question.TopicId > 0)
                            {

                                sb.Append("UPDATE ");
                                sb.Append(MySqlDbAccess.GetObjectName("Topic"));
                                sb.Append(" SET PollID = ?NewPollGroupID WHERE TopicID =?TopicID; ");

                            }

                            // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                            if (question.ForumId > 0)
                            {
                                sb.Append("UPDATE ");
                                sb.Append(MySqlDbAccess.GetObjectName("Forum"));
                                sb.Append(" SET PollGroupID= ?NewPollGroupID WHERE ForumID= ?ForumID; ");
                            }

                            // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                            if (question.CategoryId > 0)
                            {
                                sb.Append("UPDATE ");
                                sb.Append(MySqlDbAccess.GetObjectName("Category"));
                                sb.Append(" SET PollGroupID = ?NewPollGroupID WHERE CategoryID= ?CategoryID; ");
                            }

                            using (var cmdPoll = MySqlDbAccess.GetCommand(sb.ToString(), true))
                            {
                                cmdPoll.Transaction = trans;
                                cmdPoll.Parameters.Add("?NewPollGroupID", MySqlDbType.Int32).Value = pollGroupId;
                                if (question.TopicId > 0)
                                {
                                    cmdPoll.Parameters.Add("?TopicID", MySqlDbType.Int32).Value = question.TopicId;
                                }

                                // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                                if (question.ForumId > 0)
                                {
                                    cmdPoll.Parameters.Add("?ForumID", MySqlDbType.Int32).Value = question.ForumId;
                                }

                                // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                                if (question.CategoryId > 0)
                                {
                                    cmdPoll.Parameters.Add("?CategoryID", MySqlDbType.Int32).Value = question.CategoryId;
                                }

                                MySqlDbAccess.ExecuteNonQuery(cmdPoll, false, connectionString);

                            }

                            /*if (ret.Value != DBNull.Value)
                       {
                           return (int?)ret.Value;
                       }*/
                            trans.Commit();
                            return pollGroupId;
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            throw new Exception(e.Message);

                        }
                        finally
                        {
                            // connMan.CloseConnection();
                        }
                    }
                }
            }
            return null;

        }

        #endregion

        #region yaf_Registry

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="Name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, object name, object boardId)
        {
            using (var cmd = MySqlDbAccess.GetCommand("registry_list"))
            {
                if (name == null)
                {
                    name = DBNull.Value;
                }
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_Name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("i_BoardID", MySqlDbType.Int32).Value = boardId;

                return MySqlDbAccess.GetData(cmd, connectionString);
            }
        }
      
        #endregion
        public static void system_initialize_fixaccess([NotNull] string connectionString, bool bGrant)
        {
            /*   using (VZF.Classes.Data.IDbConnectionManager connMan = new IDbConnectionManager())
            {
                using (IDbTransaction trans = connMan.OpenDBConnection.BeginTransaction(VZF.Classes.Data.PostgreDbAccess.IsolationLevel))
                {
                    // select * from  pg_tables where schemaname tableowner
                  //  select * from  pg_views where schemaname viewowner
                    //  select * from  pg_proc where proname like {objectQualifier} prowner(oid)
                   // select * from pg_user usesysoid <-oid
                    // REVIEW : Ederon - would "{databaseOwner}.{objectQualifier}" work, might need only "{objectQualifier}"
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select Name, OBJECTPROPERTY(id, N'IsUserTable') AS IsUserTable,IsScalarFunction = OBJECTPROPERTY(id, N'IsScalarFunction'),IsProcedure = OBJECTPROPERTY(id, N'IsProcedure'),IsView = OBJECTPROPERTY(id, N'IsView') from dbo.sysobjects where Name like '{databaseOwner}.{objectQualifier}%'", connMan.OpenDBConnection))
                    {
                        da.SelectCommand.Transaction = trans;
                        using (DataTable dt = new DataTable("sysobjects"))
                        {
                            da.Fill(dt);
                            using (var cmd = connMan.DBConnection.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "select current_user";
                                string userName = (string)cmd.ExecuteScalar();

                                if (bGrant)
                                {
                                    cmd.CommandType = CommandType.Text;
                                    foreach (DataRow row in dt.Select("IsProcedure=1 or IsScalarFunction=1"))
                                    {
                                        cmd.CommandText = string.Format("grant execute on \"{0}\" to \"{1}\"", row["Name"], userName);
                                        cmd.ExecuteNonQuery();
                                    }
                                    foreach (DataRow row in dt.Select("IsUserTable=1 or IsView=1"))
                                    {
                                        cmd.CommandText = string.Format("grant select,update on \"{0}\" to \"{1}\"", row["Name"], userName);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    cmd.CommandText = "sp_changeobjectowner";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    foreach (DataRow row in dt.Select("IsUserTable=1"))
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add(new NpgsqlParameter("i_objname", NpgsqlDbType.Varchar));
                                        cmd.Parameters[0].Value = row["Name"];
                                        cmd.Parameters.Add(new NpgsqlParameter("i_newowner", NpgsqlDbType.Varchar));
                                        cmd.Parameters[1].Value = PostgreDbAccess.SchemaName;                                        
                                        
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (NpgsqlException)
                                        {
                                        }
                                    }
                                    foreach (DataRow row in dt.Select("IsView=1"))
                                    {
                                        cmd.Parameters.Clear();
                                      
                                        cmd.Parameters.Add(new NpgsqlParameter("i_objname", NpgsqlDbType.Varchar));
                                        cmd.Parameters[0].Value = row["Name"];
                                        cmd.Parameters.Add(new NpgsqlParameter("i_newowner", NpgsqlDbType.Varchar));
                                        cmd.Parameters[1].Value = PostgreDbAccess.SchemaName;  
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (NpgsqlException)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
            }*/

        }
       
        # region VZ-Team additions
      

        /// <summary>
        /// The get stats message.
        /// </summary>
        private static string getStatsMessage;

        /// <summary>
        /// The db_getstats_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_new([NotNull] string connectionString)
        {
            try
            {
                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);
                    using (
                        var cmd =
                            new MySqlCommand(
                                string.Format(
                                    "ANALYZE TABLE {0}.{1}user;",
                                    MySqlDbAccess.DatabaseSchemaName,
                                    Config.DatabaseObjectQualifier)))
                    {
                        cmd.CommandType = CommandType.Text;

                        // up the command timeout...
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it...
                        MySqlDbAccess.ExecuteNonQuery(cmd, false, connectionString);
                        return getStatsMessage;
                    }
                }
            }
            finally
            {
                getStatsMessage = string.Empty;
            }
        }

        /// <summary>
        /// The reindexDb_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void getStats_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            getStatsMessage += "\r\n{0}".FormatWith(e.Message);
        }

        /// <summary>
        /// The db_getstats_table.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable db_getstats_table([NotNull] string connectionString)
        {
            using (var cmd = new MySqlCommand(string.Format("SHOW TABLE STATUS FROM {0};", MySqlDbAccess.DatabaseSchemaName)))
            {
                cmd.CommandType = CommandType.Text;

                return MySqlDbAccess.GetData(cmd, false, connectionString);
            }
        }

        /// <summary>
        /// The db_getstats_alltables.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable db_getstats_alltables([NotNull] string connectionString)
        {
            var sb = new StringBuilder();
            sb.Append(
                string.Format(
                    "SELECT table_name FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine ='InnoDB'  AND t.TABLE_TYPE='BASE TABLE' AND t.table_schema='{0}' ",
                    MySqlDbAccess.DatabaseSchemaName));
            sb.Append(";");

            using (var cmd = new MySqlCommand(sb.ToString()))
            {
                cmd.CommandType = CommandType.Text;
                return MySqlDbAccess.GetData(cmd, false, connectionString);
            }
        }

        /// <summary>
        /// The db_getstats_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_warning()
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_getstats_tablex.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_tablex([NotNull] string connectionString)
        {
            int offset = 15;
            var sb = new StringBuilder();
            sb.Append("___________________________________________________________________________________      ");
            DataTable tables = Db.db_getstats_alltables(connectionString);
            foreach (DataRow drtables in tables.Rows)
            {
                using (
                    var cmd =
                        new MySqlCommand(String.Format("ANALYZE TABLE {0}.{1};", MySqlDbAccess.DatabaseSchemaName, drtables[0])))
                {
                    cmd.CommandType = CommandType.Text;

                    // up the command timeout...
                    cmd.CommandTimeout = 9999;

                    // run it...
                    DataTable dt = MySqlDbAccess.GetData(cmd, false, connectionString);
                    foreach (DataRow dr in dt.Rows)
                    {
                        object[] oa = dr.ItemArray;
                        for (int i = 0; i < oa.Length; i++)
                        {
                            sb.Append("|");

                            switch (i)
                            {
                                case 0:
                                    sb.Append(" Table=");
                                    offset = 30;
                                    break;
                                case 1:
                                    sb.Append(" Op=");
                                    offset = 10;
                                    break;
                                case 2:
                                    sb.Append(" Msg_type=");
                                    offset = 10;
                                    break;
                                case 3:
                                    sb.Append(" Msg_text=");
                                    offset = 10;
                                    break;
                            }

                            sb.Append(oa[i]);
                            int strl = offset - oa[i].ToString().Length;
                            for (int i1 = 1; i1 < strl; i1++)
                            {
                                sb.Append(" ");
                            }
                        }

                        sb.Append("\r\n");
                    }

                    sb.Append("___________________________________________________________________________________");
                    sb.Append("\r\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets a value indicating whether btn reindex visible.
        /// </summary>
        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }


        // DB Maintenance page panel visibility
        /// <summary>
        /// Gets a value indicating whether panel get stats.
        /// </summary>
        public static bool PanelGetStats
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel recovery mode.
        /// </summary>
        public static bool PanelRecoveryMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel reindex.
        /// </summary>
        public static bool PanelReindex
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel shrink.
        /// </summary>
        public static bool PanelShrink
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The my_reindex db message.
        /// </summary>
        private static string my_reindexDbMessage;

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new([NotNull] string connectionString)
        {
            try
            {
                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(my_reindexDb_InfoMessage);
                    using (
                        var cmd =
                            new MySqlCommand(
                                string.Format(
                                    "ANALYZE TABLE {0}.{1}user;",
                                    MySqlDbAccess.DatabaseSchemaName,
                                    Config.DatabaseObjectQualifier)))
                    {
                        var sb = new StringBuilder();
                        cmd.CommandType = CommandType.Text;

                        // up the command timeout...
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it...
                        sb.Append(
                            "SELECT table_name FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine ='InnoDB'  AND t.TABLE_TYPE='BASE TABLE' ");
                        sb.Append(";");
                        MySqlDbAccess.ExecuteNonQuery(cmd, false, connectionString);
                        return my_reindexDbMessage;
                    }
                }
            }
            finally
            {
                my_reindexDbMessage = string.Empty;
            }
        }

        /// <summary>
        /// The reindexDb_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void my_reindexDb_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            my_reindexDbMessage += "\r\n{0}".FormatWith(e.Message);
        }

        /// <summary>
        /// The db_reindex_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_warning()
        {
            return "InnoDB data engine keeps indexes in the same table";
        }

        /// <summary>
        /// The db_reindex_table.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable db_reindex_table([NotNull] string connectionString)
        {
            var sb = new StringBuilder();
            sb.Append(
                "SELECT table_name FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine ='InnoDB'  AND t.TABLE_TYPE='BASE TABLE' ");
            sb.Append(";");
            DataTable dtc;
            using (var cmd1 = new MySqlCommand(sb.ToString()))
            {
                cmd1.CommandType = CommandType.Text;
                dtc = MySqlDbAccess.GetData(cmd1, connectionString);
            }

            var dtt = new DataTable();
            for (int i = 0; i < dtc.Rows.Count; i++)
            {
                using (
                    var cmd =
                        new MySqlCommand(
                            string.Format(
                                "ANALYZE TABLE {0}.{1}user;", MySqlDbAccess.DatabaseSchemaName, Config.DatabaseObjectQualifier)))
                                {
                    cmd.CommandType = CommandType.Text;
                    DataTable dttmp = MySqlDbAccess.GetData(cmd, false, connectionString);
                    DataRow drow = dttmp.Rows[0];
                    if (dtt.Rows.Count < 1)
                    {
                        dtt = dttmp.Clone();

                    }

                    DataRow ddd = dtt.NewRow();
                    ddd[0] = drow[0];
                    ddd[1] = drow[1];
                    ddd[2] = drow[2];
                    ddd[3] = drow[3];
                    dtt.Rows.Add(ddd);
                }
            }

            return dtt;
        }

        /// <summary>
        /// The my_message run sql.
        /// </summary>
        private static string my_messageRunSql;

        /// <summary>
        /// The db_runsql_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="useTransaction">
        /// The use transaction.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_runsql_new([NotNull] string connectionString, string sql, bool useTransaction)
        {
            try
            {
                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(my_runSql_InfoMessage);

                    sql = MySqlDbAccess.GetCommandTextReplaced(sql.Trim());

                    using (var command = new MySqlCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        command.CommandTimeout = 9999;
                        command.Connection = connMan.OpenDBConnection(connectionString);

                        return InnerRunSqlExecuteReader(command, useTransaction);
                    }
                }
            }
            finally
            {
                my_messageRunSql = string.Empty;
            }
        }

        /// <summary>
        /// The runSql_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void my_runSql_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            my_messageRunSql = "\r\n" + e.Message;
        }

        /// <summary>
        /// Called from db_runsql -- just runs a sql command according to specificiations.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        private static string InnerRunSqlExecuteReader(MySqlCommand command, bool useTransaction)
        {
            var results = new StringBuilder();

            try
            {
                   command.Transaction = useTransaction
                                              ? command.Connection.BeginTransaction(MySqlDbAccess.IsolationLevel)
                                              : null;
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            int rowIndex = 1;
                            var columnNames =
                                reader.GetSchemaTable()
                                      .Rows.Cast<DataRow>()
                                      .Select(r => r["ColumnName"].ToString())
                                      .ToList();

                            results.Append("RowNumber");

                            columnNames.ForEach(
                                n =>
                                    {
                                        results.Append(",");
                                        results.Append(n);
                                    });

                            results.AppendLine();

                            while (reader.Read())
                            {
                                results.AppendFormat(@"""{0}""", rowIndex++);

                                // dump all columns...
                                foreach (var col in columnNames)
                                {
                                    results.AppendFormat(@",""{0}""", reader[col].ToString().Replace("\"", "\"\""));
                                }

                                results.AppendLine();
                            }
                        }
                        else if (reader.RecordsAffected > 0)
                        {
                            results.AppendFormat("{0} Record(s) Affected", reader.RecordsAffected);
                            results.AppendLine();
                        }
                        else
                        {
                            if (my_messageRunSql.IsSet())
                            {
                                results.AppendLine(my_messageRunSql);
                                results.AppendLine();
                            }
                            results.AppendLine("No Results Returned.");
                        }

                        reader.Close();
                      
                    }
            }
            catch (Exception x)
            {
                results.AppendLine();
                results.AppendFormat("SQL ERROR: {0}", x);
                if (command.Transaction != null)
                {
                    command.Transaction.Rollback();
                }
            }
           finally
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Commit();
                    }
                } 

            return results.ToString();
        }

       
        /// <summary>
        /// The system_initialize_replace_entries.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="script">
        /// The script.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string system_initialize_replace_entries([NotNull] string connectionString, string script)
        {
            bool conEncoding = false;
            string[] options;

            // apply object qualifier
            if (!string.IsNullOrEmpty(Config.DatabaseObjectQualifier))
            {
                script = script.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);
            }
            else
            {
                script = script.Replace("{objectQualifier}", "yaf_");
            }

            string dbcharset = null;
            string dbcollation = null;
            script = MySqlDbAccess.GetCommandTextReplaced(script);

            using (var connMan = new MySqlDbConnectionManager(connectionString))
            {
                options = connMan.DbConnection(connectionString).ConnectionString.Split(';');
            }

            foreach (string str in options)
            {
                string[] optionValue = str.Split('=');

                // apply database name
                if (optionValue[0].Trim().ToLower() == "database")
                {
                    if (optionValue[1].Trim() != MySqlDbAccess.DatabaseSchemaName
                        || !string.IsNullOrEmpty(optionValue[1].Trim()))
                    {
                        script = script.Replace("{databaseName}", optionValue[1].Trim());
                    }
                    else
                    {
                        script = script.Replace("{databaseName}", MySqlDbAccess.DatabaseSchemaName);
                    }
                }

                // apply user name from connection string to override defaults in config
                // currently it's not used
                if (optionValue[0].Trim().ToLowerInvariant().Contains("user id")
                    || optionValue[0].Trim().ToLowerInvariant().Contains("Username")
                    || optionValue[0].Trim().ToLowerInvariant().Contains("User name")
                    || optionValue[0].Trim().ToLowerInvariant().Contains("Uid"))
                {
                    if (optionValue[1].Trim() != Config.DatabaseOwner || !string.IsNullOrEmpty(optionValue[1].Trim()))
                    {
                        script = script.Replace("{databaseOwner}", optionValue[1].Trim());
                    }
                    else
                    {
                        script = script.Replace("{databaseOwner}", Config.DatabaseOwner.Trim());
                    }
                }

                // Encodings
                // apply charset
                if ((str.Contains("Charset") || str.Contains("Character Set")) && string.IsNullOrEmpty(optionValue[1]))
                {
                    // Verify if it's valid       
                    using (var cmd = MySqlDbAccess.GetCommand("SHOW VARIABLES LIKE 'character_set_database'", true))
                    {
                        DataTable dtt = MySqlDbAccess.GetData(cmd, connectionString);
                        if (dtt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtt.Rows)
                            {
                                if (dr["Variable_name"].ToString() == optionValue[1].Trim())
                                {
                                    dbcharset = dr["Value"].ToString();
                                }
                            }

                            conEncoding = true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(Config.DatabaseEncoding))
                {
                    // Verify if it's valid       
                    using (var cmd = MySqlDbAccess.GetCommand("SHOW VARIABLES LIKE 'character_set_database'", true))
                    {
                        DataTable dtt1 = MySqlDbAccess.GetData(cmd, connectionString);
                        if (dtt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtt1.Rows)
                            {
                                if (dr["Variable_name"].ToString() == "character_set_database")
                                {
                                    dbcharset = dr["Value"].ToString();
                                }
                            }

                            conEncoding = true;
                        }
                    }
                }
            }

            if (conEncoding)
            {
                if (Config.DatabaseCollation.Contains(dbcharset))
                {
                    dbcollation = Config.DatabaseCollation;
                }

                if (string.IsNullOrEmpty(dbcollation))
                {
                    using (var cmd = MySqlDbAccess.GetCommand("SHOW CHARACTER SET;", true))
                    {
                        DataTable dttt = MySqlDbAccess.GetData(cmd, connectionString);
                        foreach (DataRow dr in dttt.Rows)
                        {
                            if (dr["Charset"].ToString() == dbcharset)
                            {
                                dbcollation = dr["Default collation"].ToString();
                            }
                        }
                    }
                }
            }

            // No entry for encoding in connection string or app.config
            if (!conEncoding)
            {
                using (var cmd = MySqlDbAccess.GetCommand("SHOW VARIABLES LIKE 'collation_database'", true))
                {
                    dbcollation = MySqlDbAccess.GetData(cmd, connectionString).Rows[0]["Value"].ToString();
                }

                using (var cmd = MySqlDbAccess.GetCommand("SHOW VARIABLES LIKE 'character_set_database'", true))
                {
                    dbcharset = MySqlDbAccess.GetData(cmd, connectionString).Rows[0]["Value"].ToString();
                }
            }

            script = script.Replace("{databaseEncoding}_{databaseCollation}", dbcollation);
            script = script.Replace("{databaseEncoding}", dbcharset);

            return script;
        }

        /// <summary>
        /// The system_initialize_executescripts.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="script">
        /// The script.
        /// </param>
        /// <param name="scriptFile">
        /// The script file.
        /// </param>
        /// <param name="useTransactions">
        /// The use transactions.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public static void system_initialize_executescripts(
            [NotNull] string connectionString, string script, string scriptFile, bool useTransactions)
        {
            script = system_initialize_replace_entries(connectionString, script);

            using (var connMan = new MySqlDbConnectionManager(connectionString))
            {
                // Now we separate string to array
                List<string> statements =
                    System.Text.RegularExpressions.Regex.Split(
                        script, "(?:--GO)", System.Text.RegularExpressions.RegexOptions.IgnoreCase).ToList();

                // use transactions...
                if (useTransactions)
                {
                    var con = connMan.OpenDBConnection(connectionString);
                    using (var trans = con.BeginTransaction(MySqlDbAccess.IsolationLevel))
                    {
                        foreach (string sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                                {
                                    continue;
                                }

                                if (sql.Length <= 0)
                                {
                                    continue;
                                }

                                using (var cmd = new MySqlCommand())
                                {
                                    cmd.Transaction = trans;
                                    cmd.Connection = con;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = sql.Trim();

                                    // added so command won't timeout anymore...
                                    cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception x)
                            {
                                trans.Rollback();
                                throw new Exception(
                                    string.Format(
                                        "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                            }
                        }

                        trans.Commit();
                    }
                }
                else
                {
                    using (var connect = new MySqlConnection(connectionString))
                    {
                        connect.Open();

                        // don't use transactions
                        foreach (string sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0
                                    || sql.Length <= 0)
                                {
                                    continue;
                                }

                                using (var cmd = new MySqlCommand())
                                {
                                    cmd.Connection = connect;
                                    if (cmd.Connection.State != ConnectionState.Open)
                                    {
                                        cmd.Connection.Open();
                                    }
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = sql.Trim();

                                    // added so command won't timeout anymore...
                                    cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception x)
                            {
                                throw new Exception(
                                    string.Format(
                                        "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                            }
                        }
                    }
                }
            }
        }

      
     
        #endregion

        #region Touradg Mods

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_warning()
        {
            // Shinking Operation is not applicable to the db.
            return string.Empty;
        }

        /// <summary>
        /// The db_shrink_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_new([NotNull] string connectionString)
        {
            // Shinking Operation is not applicable to the db.
            return string.Empty;
        }

        // Set Recovery

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_warning()
        {
            // Recovery operation is not aaplicable to the data layer.
            return string.Empty;
        }

        /// <summary>
        /// The db_recovery_mode_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="dbRecoveryMode">
        /// The db recovery mode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_new([NotNull] string connectionString, string dbRecoveryMode)
        {
            // Recovery operation is not aaplicable to the data layer.
            return string.Empty;
        }

        #endregion  

    }
}
