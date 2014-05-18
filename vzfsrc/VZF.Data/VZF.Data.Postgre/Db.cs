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
//   The common db.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Postgre
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Security;

    using Npgsql;

    using NpgsqlTypes;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;

    /// <summary>
    /// All the Database functions for YAF
    /// </summary>
    public static class Db
    {
        // added vzrus
        #region ConnectionStringOptions

        /// <summary>
        /// Gets the provider assembly name.
        /// </summary>
        public static string ProviderAssemblyName
        {
            get
            {
                return "Npgsql";
            }
        }

        /// <summary>
        /// Gets a value indicating whether password placeholder visible.
        /// </summary>
        public static bool PasswordPlaceholderVisible
        {
            get
            {
                return true;
            }
        }

        #endregion        

        public static bool FullTextSupported = false;

        public static string FullTextScript = "postgre/fulltext.sql";

       
        #region DataSets

        public static void forum_list_sort_basic(DataTable listsource, DataTable list, int parentid, int currentLvl)
        {
            for (int i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentid)
                {
                    string sIndent = string.Empty;
                    int iIndent = Convert.ToInt32(currentLvl);
                    for (int j = 0; j < iIndent; j++) sIndent += "--";
                    row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                    list.Rows.Add(row.ItemArray);
                    forum_list_sort_basic(listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
            }
        }
       
        #endregion    

        #region yaf_Forum
       
        public static void forum_sort_list_recursive(
            DataTable listSource, DataTable listDestination, int parentID, int categoryID, int currentIndent)
        {
            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] != parentID) continue;

                DataRow newRow;
                if ((int)row["CategoryID"] != categoryID)
                {
                    categoryID = (int)row["CategoryID"];

                    newRow = listDestination.NewRow();
                    newRow["ForumID"] = -categoryID; // Ederon : 9/4/2007
                    newRow["Title"] = string.Format("{0}", row["Category"]);
                    newRow["CanHavePersForums"] = row["CanHavePersForums"].ToType<bool>();
                    listDestination.Rows.Add(newRow);
                }

                string sIndent = string.Empty;

                for (int j = 0; j < currentIndent; j++) sIndent += "--";

                // import the row into the destination
                newRow = listDestination.NewRow();

                newRow["ForumID"] = row["ForumID"];
                newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["Forum"]);
                newRow["CanHavePersForums"] = row["CanHavePersForums"].ToType<bool>();

                listDestination.Rows.Add(newRow);

                // recurse through the list..
                forum_sort_list_recursive(
                    listSource, listDestination, (int)row["ForumID"], categoryID, currentIndent + 1);
            }
        }

      
        #endregion     

        #region yaf_Message
     
       
      
      
        #endregion        

        #region yaf_Poll
     
        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save([NotNull] string connectionString, List<PollSaveList> pollList)
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
                    pgStr.Append(PostgreDbAccess.GetObjectName("topic"));
                    pgStr.Append(" WHERE topicid = :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    pgStr.Append("select  pollgroupid  from ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("forum"));
                    pgStr.Append(" WHERE forumid =:i_forumid");
                }
                else if (question.CategoryId > 0)
                {
                    pgStr.Append("select pollgroupid  from ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("category"));
                    pgStr.Append(" WHERE categoryid = :i_categoryid");
                }

                using (var cmdPg = PostgreDbAccess.GetCommand(pgStr.ToString(), true))
                {
                    // Add parameters
                    if (question.TopicId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value =
                            question.TopicId;
                    }
                    else if (question.ForumId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value =
                            question.ForumId;
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value =
                            question.CategoryId;
                    }

                    object pgexist = PostgreDbAccess.ExecuteScalar(cmdPg, true, connectionString);
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
                    pgStr.Append(PostgreDbAccess.GetObjectName("pollgroupcluster"));
                    pgStr.Append("(userid,flags ) VALUES(:i_userid, :i_flags) RETURNING pollgroupid; ");
                    using (var cmdPgIns = PostgreDbAccess.GetCommand(pgStr.ToString(), true))
                    {
                        // set poll group flags
                        int pollGroupFlags = 0;
                        if (question.IsBound)
                        {
                            pollGroupFlags = pollGroupFlags | 2;
                        }

                        // Add parameters
                        cmdPgIns.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value =
                            question.UserId;
                        cmdPgIns.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value =
                            pollGroupFlags;
                        pollGroup = (int?)PostgreDbAccess.ExecuteScalar(cmdPgIns, true, connectionString);
                    }
                }




                using (var cmd = PostgreDbAccess.GetCommand("poll_save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("i_question", NpgsqlDbType.Varchar)).Value =
                        question.Question;

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("i_closes", NpgsqlDbType.Timestamp)).Value =
                            question.Closes;
                    }
                    else
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("i_closes", NpgsqlDbType.Timestamp)).Value =
                            DBNull.Value;
                    }
                    int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                    pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                    pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                    pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;
                    cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = question.UserId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                    cmd.Parameters.Add(new NpgsqlParameter("i_objectpath", NpgsqlDbType.Varchar)).Value =
                        question.QuestionObjectPath;
                    cmd.Parameters.Add(new NpgsqlParameter("i_mimetype", NpgsqlDbType.Varchar)).Value =
                        question.QuestionMimeType;
                    cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = pollFlags;


                    currPoll = (int?)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
                }


                // The cycle through question reply choices  
                int chl = question.Choice.GetUpperBound(1) + 1;
                for (uint choiceCount = 0; choiceCount < chl; choiceCount++)
                {
                    if (question.Choice[0, choiceCount].Trim().Length > 0)
                    {
                        StringBuilder sbChoice = new StringBuilder();
                        sbChoice.Append("INSERT INTO ");
                        sbChoice.Append(PostgreDbAccess.GetObjectName("choice"));
                        sbChoice.AppendFormat(
                            "(pollid,choice,votes,objectpath,mimetype) VALUES (:pollid{0}, :choice{0}, :votes{0}, :objectpath{0}, :mimetype{0}); ",
                            choiceCount);
                        using (var cmdChoice = PostgreDbAccess.GetCommand(sbChoice.ToString(), true))
                        {
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("pollid{0}", choiceCount), NpgsqlDbType.Integer))
                                     .Value = currPoll;
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("choice{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[0, choiceCount];
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("votes{0}", choiceCount), NpgsqlDbType.Integer)).Value
                                = 0;
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("objectpath{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[1, choiceCount].IsNotSet()
                                                  ? String.Empty
                                                  : question.Choice[1, choiceCount];
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("mimetype{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[2, choiceCount].IsNotSet()
                                                  ? String.Empty
                                                  : question.Choice[2, choiceCount];
                            PostgreDbAccess.ExecuteNonQuery(cmdChoice, true, connectionString);
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
                    Usb.Append(PostgreDbAccess.GetObjectName("topic"));
                    Usb.Append(" SET pollid = :i_pollid WHERE topicid= :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(PostgreDbAccess.GetObjectName("forum"));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where forumid = :i_forumid; ");

                }
                else if (question.CategoryId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(PostgreDbAccess.GetObjectName("category"));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where categoryid = :i_categoryid; ");
                }


                using (var cmd2 = PostgreDbAccess.GetCommand(Usb.ToString(), true))
                {
                    cmd2.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollGroup;
                    //cmd2.Parameters.Add(new NpgsqlParameter(":i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                    if (question.TopicId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value =
                            question.TopicId;
                    }
                    else if (question.ForumId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value =
                            question.ForumId;
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value =
                            question.CategoryId;
                    }
                    PostgreDbAccess.ExecuteNonQuery(cmd2, connectionString);
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


       
        #endregion

        #region yaf_Registry

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="name">Use to specify return of specific entry only. Setting this to null returns all entries.
        /// </param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, object name, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("registry_list"))
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

                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, [NotNull] object name)
        {
            return registry_list(connectionString, name, null);
        }
       
    
        #endregion
        
       
        #region Miscelaneous vzrus addons

        #region reindex page controls

        // DB Maintenance page buttons name    

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

        public static bool PanelRecoveryMode
        {
            get
            {
                return true;
            }
        }

        public static bool PanelReindex
        {
            get
            {
                return true;
            }
        }

        public static bool PanelShrink
        {
            get
            {
                return true;
            }
        }


        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }

        #endregion

       
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
        public static string db_getstats_new([NotNull] string connectionString )
        {
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);
                    using (var cmd = new NpgsqlCommand("VACUUM ANALYZE VERBOSE;", connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandType = CommandType.Text;

                        // up the command timeout..
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it..
                        cmd.ExecuteNonQuery();
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
        /// The reindex db message.
        /// </summary>
        private static string reindexDbMessage;

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new([NotNull] string connectionString )
        {
            // VACUUM ANALYZE VERBOSE;VACUUM cannot be implemeneted within function or multiline command line string 
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(reindexDb_InfoMessage);

                    using (
                        var cmd =
                            new NpgsqlCommand(
                                String.Format(
                                    @"REINDEX DATABASE {0};",
                                    Config.DatabaseSchemaName.IsSet() ? Config.DatabaseSchemaName : "public"),
                                connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandType = CommandType.Text;
                        
                        // up the command timeout..
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it..                   
                        cmd.ExecuteNonQuery();
                        return reindexDbMessage;
                    }
                }
            }
            finally
            {
                reindexDbMessage = string.Empty;
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
        private static void reindexDb_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            reindexDbMessage += "\r\n{0}".FormatWith(e.Message);
        }


        public static string db_reindex_warning()
        {
            return "Operation completed. Database tables reindexing can take a lot of time.";
        }

        public static string db_getstats_warning()
        {
            return
                "Operation completed. Vacuum operation blocks all database objects! If there is a lot of indexes, the database can be blocked for a long time. Choose a right timing!";
        }

        /// <summary>
        /// The db_runsql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="connMan">
        /// The conn man.
        /// </param>
        /// <returns>
        /// The db_runsql.
        /// </returns>
        /*  public static string db_runsql(string sql, IDbConnectionManager connMan, bool useTransaction)
        {
            using (var command = new SqlCommand(sql, connMan.OpenDBConnection))
            {
                command.CommandTimeout = 9999;
                command.Connection = connMan.OpenDBConnection;

                return InnerRunSqlExecuteReader(command, useTransaction);
            }
        } */

        /*   public static string db_runsql( string sql, bool useTransaction)
        {
            string txtResult = "";
            var results = new System.Text.StringBuilder();

            using (var cmd = new NpgsqlCommand(sql, connMan.OpenDBConnection))
            {
                cmd.CommandTimeout = 9999;
                NpgsqlDataReader reader = null;

                using (NpgsqlTransaction trans = connMan.OpenDBConnection.BeginTransaction(PostgreDbAccess.IsolationLevel))
                {
                    try
                    {
                        cmd.Connection = connMan.DBConnection;
                        cmd.Transaction = trans;
                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            int rowIndex = 1;

                            results.Append("RowNumber");
                            int gg = 0;
                            var columnNames = new string[reader.GetSchemaTable().Rows.Count-1];
                            foreach (DataRow drd in reader.GetSchemaTable().Rows)
                            {
                                  columnNames[gg] = drd["ColumnName"].ToString();
                                  results.Append(",");
                                  results.Append(drd["ColumnName"].ToString());
                                  gg++;
                                
                            }
                         //   var columnNames = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();

                            
                           

                            results.AppendLine();

                            while (reader.Read())
                            {
                                results.AppendFormat(@"""{0}""", rowIndex++);

                                // dump all columns..
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
                            results.AppendLine("No Results Returned.");
                        }


                        reader.Close();
                        trans.Commit();
                    }
                    catch (Exception x)
          {
            if (reader != null)
            {
              reader.Close();
            }

            // rollback..
            trans.Rollback();
            results.AppendLine();
            results.AppendFormat("SQL ERROR: {0}", x);
          }

          return results.ToString();
        }
               
            }

        } 
       */

        private static string messageRunSql;

        /// <summary>
        /// The db_runsql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="connectionManager">
        /// The conn man.
        /// </param>
        /// <param name="useTransaction">
        /// The use Transaction.
        /// </param>
        /// <returns>
        /// The db_runsql.
        /// </returns>
        public static string db_runsql_new([NotNull] string connectionString, [NotNull] string sql, bool useTransaction)
        {

            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(runSql_InfoMessage);
                    sql = PostgreDbAccess.GetCommandTextReplaced(sql.Trim());

                    var results = new System.Text.StringBuilder();

                    using (var cmd = new NpgsqlCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandTimeout = 9999;
                        NpgsqlDataReader reader = null;
                        var trans = useTransaction ? cmd.Connection.BeginTransaction(PostgreDbAccess.IsolationLevel)
                                                                   : null;
                        try
                        {
                            cmd.Connection = connMan.DBConnection;
                            cmd.Transaction = trans;
                            reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                int rowIndex = 1;

                                results.Append("RowNumber");
                                int gg = 0;
                                var columnNames = new string[reader.GetSchemaTable().Rows.Count];
                                foreach (DataRow drd in reader.GetSchemaTable().Rows)
                                {
                                    columnNames[gg] = drd["ColumnName"].ToString();
                                    results.Append(",");
                                    results.Append(drd["ColumnName"]);
                                    gg++;
                                }
                                
                                //   var columnNames = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();

                                results.AppendLine();

                                while (reader.Read())
                                {
                                    results.AppendFormat(@"""{0}""", rowIndex++);

                                    // dump all columns..
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
                                if (messageRunSql.IsSet())
                                {
                                    results.AppendLine(messageRunSql);
                                    results.AppendLine();
                                }
                                results.AppendLine("No Results Returned.");
                            }


                            reader.Close();
                            trans.Commit();
                        }
                        catch (Exception x)
                        {
                            if (reader != null)
                            {
                                reader.Close();
                            }

                            // rollback..
                            trans.Rollback();
                            results.AppendLine();
                            results.AppendFormat("SQL ERROR: {0}", x);
                        }

                        return results.ToString();


                    }
                }
            }
            finally
            {
                messageRunSql = string.Empty;
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
        private static void runSql_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            messageRunSql = "\r\n" + e.Message;
        }      
     

        public static readonly string[] _scriptList =
            {
                "pgsql/preinstall.sql", "pgsql/domains.sql", "pgsql/tables.sql",
                "pgsql/sequences.sql", "pgsql/pkeys.sql", "pgsql/indexes.sql",
                "pgsql/fkeys.sql", "pgsql/triggers.sql", "pgsql/rules.sql",
                "pgsql/views.sql", "pgsql/types.sql", "pgsql/procedures.sql",
                "pgsql/procedures1.sql", "pgsql/functions.sql",
                "pgsql/providers/tables.sql", "pgsql/providers/pkeys.sql",
                "pgsql/providers/indexes.sql", "pgsql/providers/types.sql",
                "pgsql/providers/procedures.sql", "pgsql/postinstall.sql",
                "pgsql/nestedsets.sql", "pgsql/nestedsets_sp.sql",
                "pgsql/fulltext_ru.sql"
            };

        public static string[] ScriptList
        {
            get
            {
                return _scriptList;
            }
        }

        private static bool GetBooleanRegistryValue([NotNull] string connectionString, string name)
        {
            using (DataTable dt = Db.registry_list(connectionString, name))
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

      
        public static void system_initialize_executescripts(
            [NotNull] string connectionString, string script, string scriptFile, bool useTransactions)
        {
            script = PostgreDbAccess.GetCommandTextReplaced(script);


            //Scripts separation regexp
            var statements = System.Text.RegularExpressions.Regex.Split(script, "(?:--GO)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            using (var connMan = new PostgreDbConnectionManager(connectionString))
            {              
                // use transactions..
                if (useTransactions)
                {
                    using (NpgsqlTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(PostgreDbAccess.IsolationLevel))
                    {
                        foreach (var sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                                {
                                    continue;
                                }

                                if (sql.Length > 0)
                                {
                                    using (var cmd = new NpgsqlCommand())
                                    {
                                        cmd.Transaction = trans;
                                        cmd.Connection = connMan.DBConnection;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = sql.Trim();

                                        // added so command won't timeout anymore..
                                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception x)
                            {
                                sql = sql0;
                                trans.Rollback();
                                throw new Exception(
                                    String.Format(
                                        "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                            }
                        }

                        trans.Commit();
                    }
                }
                else
                {
                    // don't use transactions
                    foreach (string sql in statements.Select(sql0 => sql0.Trim()))
                    {
                        try
                        {
                            if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                            {
                                continue;
                            }

                            if (sql.Length > 0)
                            {
                                using (var cmd = new NpgsqlCommand())
                                {
                                    cmd.Connection = connMan.OpenDBConnection(connectionString);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = sql.Trim();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception x)
                        {
                            throw new Exception(
                                String.Format(
                                    "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                        }
                    }
                }
            }


        }

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

       

        #endregion

        #region Touradg Mods

        // Shinking Operation

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_warning([NotNull] string connectionString )
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_shrink.
        /// </summary>
        public static void db_shrink()
        {
            /*  String ShrinkSql = "DBCC SHRINKDATABASE(N'" + DBName.DBConnection.Database + "')";
            SqlConnection ShrinkConn = new SqlConnection(VZF.Classes.Config.ConnectionString);
            SqlCommand ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
            ShrinkConn.Open();
            ShrinkCmd.ExecuteNonQuery();
            ShrinkConn.Close();
            using (SqlCommand cmd = new SqlCommand(ShrinkSql.ToString(), DBName.OpenDBConnection))
            {
                cmd.Connection = DBName.DBConnection;
                cmd.CommandTimeout = 9999;
                cmd.ExecuteNonQuery();
            }*/
        }

        /// <summary>
        /// The db shink message.
        /// </summary>
        private static string dbShinkMessage;

        /// <summary>
        /// The db_shrink.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        public static string db_shrink_new([NotNull] string connectionString )
        {
            /* try
             {
                 using (var conn = new PostgreDbConnectionManager(connectionString))
                 {
                     conn.InfoMessage += new YafDBConnInfoMessageEventHandler(dbShink_InfoMessage);
                   
                     string ShrinkSql = "DBCC SHRINKDATABASE(N'" + conn.DBConnection.Database + "')";
                     var ShrinkConn = new SqlConnection(Config.ConnectionString);
                     var ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
                     ShrinkConn.Open();
                     ShrinkCmd.ExecuteNonQuery();
                     ShrinkConn.Close();
                     using (var cmd = new SqlCommand(ShrinkSql, conn.OpenDBConnection(connectionString)))
                     {
                         cmd.Connection = conn.DBConnection;
                         cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                         cmd.ExecuteNonQuery();
                     }
                 }
                 return dbShinkMessage;
             }
             finally
             {
                 dbShinkMessage = string.Empty;
             } */
            return string.Empty;
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
        private static void dbShink_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            dbShinkMessage = "\r\n" + e.Message;
        }

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_warning()
        {
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
            /* String RecoveryMode = "ALTER DATABASE " + DBName.DBConnection.Database + " SET RECOVERY " + dbRecoveryMode;
            SqlConnection RecoveryModeConn = new SqlConnection(VZF.Classes.Config.ConnectionString);
            SqlCommand RecoveryModeCmd = new SqlCommand(RecoveryMode, RecoveryModeConn);
            RecoveryModeConn.Open();
            RecoveryModeCmd.ExecuteNonQuery();
            RecoveryModeConn.Close();
            using (SqlCommand cmd = new SqlCommand(RecoveryMode.ToString(), DBName.OpenDBConnection))
            {
                cmd.Connection = DBName.DBConnection;
                cmd.CommandTimeout = 9999;
                cmd.ExecuteNonQuery();
            }*/
            return string.Empty;
        }

        #endregion        
    }
}
