/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
namespace VZF.Data.MsSql
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Hosting;
    using System.Web.Security;

    using VZF.Data.MsSql.Search;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;

    #endregion

    /// <summary>
    /// All the Database functions for YAF
    /// </summary>
    public static class Db
    {
        // Parameter 10
        #region Constants and Fields

        /// <summary>
        ///   The _script list.
        /// </summary>
        private static readonly string[] _scriptList = 
        {
            "mssql/tables.sql", 
            "mssql/indexes.sql", 
            "mssql/views.sql",
            "mssql/constraints.sql", 
            "mssql/triggers.sql",
            "mssql/functions.sql", 
            "mssql/procedures.sql",
            "mssql/forum_ns.sql",
            "mssql/providers/tables.sql",
            "mssql/providers/indexes.sql",
            "mssql/providers/procedures.sql" 
        };

        /// <summary>
        ///   The _full text script.
        /// </summary>
        private static string _fullTextScript = "mssql/fulltext.sql";

        /// <summary>
        ///   The _full text supported.
        /// </summary>
        private static bool _fullTextSupported = true;

        #endregion

        #region Properties


        /// <summary>
        ///   Gets or sets FullTextScript.
        /// </summary>
        public static string FullTextScript
        {
            get
            {
                return _fullTextScript;
            }

            set
            {
                _fullTextScript = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether FullTextSupported.
        /// </summary>
        public static bool FullTextSupported
        {
            get
            {
                return _fullTextSupported;
            }

            set
            {
                _fullTextSupported = value;
            }
        }
        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }  

        /// <summary>
        ///   Gets a value indicating whether PanelGetStats.
        /// </summary>
        public static bool PanelGetStats
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PanelRecoveryMode.
        /// </summary>
        public static bool PanelRecoveryMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PanelReindex.
        /// </summary>
        public static bool PanelReindex
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PanelShrink.
        /// </summary>
        public static bool PanelShrink
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PasswordPlaceholderVisible.
        /// </summary>
        public static bool PasswordPlaceholderVisible
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Gets ProviderAssemblyName.
        /// </summary>
        [NotNull]
        public static string ProviderAssemblyName
        {
            get
            {
                return "System.Data.SqlClient";
            }
        }

        /// <summary>
        ///   Gets ScriptList.
        /// </summary>
        public static string[] ScriptList
        {
            get
            {
                return _scriptList;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The sql server major version as short.
        /// </summary>
        /// <returns>
        /// The sql server major version as short.
        /// </returns>
        public static ushort SqlServerMajorVersionAsShort(string connectionString)
        {
            using (
              var cmd =
                MsSqlDbAccess.GetCommand(
                  "SELECT SUBSTRING(CONVERT(VARCHAR(20), SERVERPROPERTY('productversion')), 1, PATINDEX('%.%', CONVERT(VARCHAR(20), SERVERPROPERTY('productversion')))-1)",
                  true))
            {
                return Convert.ToUInt16(MsSqlDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }    
       

        private static string getStatsMessage;
        /// <summary>
        /// The db_getstats_new.
        /// </summary>
        public static string db_getstats_new(string connectionString)
        {
            try
            {
                using (var connMan = new MsSqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);

                    connMan.DBConnection(connectionString).FireInfoMessageEventOnUserErrors = true;

                    // create statistic getting SQL...
                    var sb = new StringBuilder();

                    sb.AppendLine("DECLARE @TableName sysname");
                    sb.AppendLine("DECLARE cur_showfragmentation CURSOR FOR");
                    sb.AppendFormat(
                        "SELECT table_name FROM information_schema.tables WHERE table_type = 'base table' AND table_name LIKE '{0}%'",
                        Config.DatabaseObjectQualifier);
                    sb.AppendLine("OPEN cur_showfragmentation");
                    sb.AppendLine("FETCH NEXT FROM cur_showfragmentation INTO @TableName");
                    sb.AppendLine("WHILE @@FETCH_STATUS = 0");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("DBCC SHOWCONTIG (@TableName)");
                    sb.AppendLine("FETCH NEXT FROM cur_showfragmentation INTO @TableName");
                    sb.AppendLine("END");
                    sb.AppendLine("CLOSE cur_showfragmentation");
                    sb.AppendLine("DEALLOCATE cur_showfragmentation");

                    using (var cmd = new SqlCommand(sb.ToString(), connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.Connection = connMan.DBConnection(connectionString);

                        // up the command timeout...
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it...
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
        /// The db_getstats_warning.
        /// </summary>
        /// <param name="connectionManager">
        /// The conn man.
        /// </param>
        /// <returns>
        /// The db_getstats_warning.
        /// </returns>
        [NotNull]
        public static string db_getstats_warning()
        {
            return string.Empty;
        }


        private static string recoveryDbModeMessage;

        /// <summary>
        /// The db_recovery_mode.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        /// <param name="dbRecoveryMode">
        /// The db recovery mode.
        /// </param>
        public static string db_recovery_mode_new([NotNull] string connectionString, [NotNull] string dbRecoveryMode)
        {
            try
            {
                using (var connMan = new MsSqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(recoveryDbMode_InfoMessage);
                    var RecoveryModeConn = new SqlConnection(Config.ConnectionString);
                    RecoveryModeConn.Open();
                   
                    string RecoveryMode = "ALTER DATABASE " + connMan.DBConnection(connectionString).Database + " SET RECOVERY " + dbRecoveryMode;
                    var RecoveryModeCmd = new SqlCommand(RecoveryMode, RecoveryModeConn);
                   
                    RecoveryModeCmd.ExecuteNonQuery();
                    RecoveryModeConn.Close();
                    using (var cmd = new SqlCommand(RecoveryMode, connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.Connection = connMan.DBConnection(connectionString);
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                        cmd.ExecuteNonQuery();
                        return recoveryDbModeMessage;
                    }

                }
            }
            catch (Exception error)
            {
                string expressDb = string.Empty;
                if (error.Message.ToUpperInvariant().Contains("'SET'"))
                {
                    expressDb = "MS SQL Server Express Editions are not supported by the application.";
                }
                recoveryDbModeMessage += "\r\n{0}\r\n{1}".FormatWith(error.Message, expressDb);
                return recoveryDbModeMessage;
            }
           
            finally
            {
                recoveryDbModeMessage = string.Empty;
            }

            
            
        }
        /// <summary>
        /// The recoveryDbMode_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void recoveryDbMode_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            recoveryDbModeMessage += "\r\n{0}".FormatWith(e.Message);
        }

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        /// <returns>
        /// The db_recovery_mode_warning.
        /// </returns>
        [NotNull]
        public static string db_recovery_mode_warning([NotNull] string connectionString, [NotNull] MsSqlDbConnectionManager DBName)
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_reindex.
        /// </summary>
        /// <param name="connectionManager">
        /// The conn man.
        /// </param>
        public static void db_reindex([NotNull] string connectionString, [NotNull] MsSqlDbConnectionManager connectionManager)
        {
           
            // create statistic getting SQL...
            var sb = new StringBuilder();

            sb.AppendLine("DECLARE @MyTable VARCHAR(255)");
            sb.AppendLine("DECLARE myCursor");
            sb.AppendLine("CURSOR FOR");
            sb.AppendFormat(
              "SELECT table_name FROM information_schema.tables WHERE table_type = 'base table' AND table_name LIKE '{0}%'",
              Config.DatabaseObjectQualifier);
            sb.AppendLine("OPEN myCursor");
            sb.AppendLine("FETCH NEXT");
            sb.AppendLine("FROM myCursor INTO @MyTable");
            sb.AppendLine("WHILE @@FETCH_STATUS = 0");
            sb.AppendLine("BEGIN");
            sb.AppendLine("PRINT 'Reindexing Table:  ' + @MyTable");
            sb.AppendLine("DBCC DBREINDEX(@MyTable, '', 80)");
            sb.AppendLine("FETCH NEXT");
            sb.AppendLine("FROM myCursor INTO @MyTable");
            sb.AppendLine("END");
            sb.AppendLine("CLOSE myCursor");
            sb.AppendLine("DEALLOCATE myCursor");

            using (var cmd = new SqlCommand(sb.ToString(), connectionManager.OpenDBConnection(connectionString)))
            {
                cmd.Connection = connectionManager.DBConnection(connectionString);

                // up the command timeout...
                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                // run it...
                cmd.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        /// <returns>
        /// The db_recovery_mode_warning.
        /// </returns>
        [NotNull]
        public static string db_recovery_mode_warning()
        {
            return string.Empty;
        }

        private static string reindexDbMessage;

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        public static string db_reindex_new(string connectionString)
        {
            try
            {
                using (var connMan = new MsSqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(reindexDb_InfoMessage);
                    connMan.DBConnection(connectionString).FireInfoMessageEventOnUserErrors = true;
                    // create statistic getting SQL...
                    var sb = new StringBuilder();

                    sb.AppendLine("DECLARE @MyTable VARCHAR(255)");
                    sb.AppendLine("DECLARE myCursor");
                    sb.AppendLine("CURSOR FOR");
                    sb.AppendFormat(
                      "SELECT table_name FROM information_schema.tables WHERE table_type = 'base table' AND table_name LIKE '{0}%'",
                      Config.DatabaseObjectQualifier);
                    sb.AppendLine("OPEN myCursor");
                    sb.AppendLine("FETCH NEXT");
                    sb.AppendLine("FROM myCursor INTO @MyTable");
                    sb.AppendLine("WHILE @@FETCH_STATUS = 0");
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("PRINT 'Reindexing Table:  ' + @MyTable");
                    sb.AppendLine("DBCC DBREINDEX(@MyTable, '', 80)");
                    sb.AppendLine("FETCH NEXT");
                    sb.AppendLine("FROM myCursor INTO @MyTable");
                    sb.AppendLine("END");
                    sb.AppendLine("CLOSE myCursor");
                    sb.AppendLine("DEALLOCATE myCursor");

                    using (var cmd = new SqlCommand(sb.ToString(), connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.Connection = connMan.DBConnection(connectionString);

                        // up the command timeout...
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it...
                        cmd.ExecuteNonQuery();
                    }
                    return reindexDbMessage;
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

        /// <summary>
        /// The db_reindex_warning.
        /// </summary>
        /// <param name="connectionManager">
        /// The conn man.
        /// </param>
        /// <returns>
        /// The db_reindex_warning.
        /// </returns>
        [NotNull]
        public static string db_reindex_warning()
        {
            return string.Empty;
        }


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
                using (var connMan = new MsSqlDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(runSql_InfoMessage);
                    connMan.DBConnection(connectionString).FireInfoMessageEventOnUserErrors = true;
                    sql = MsSqlDbAccess.GetCommandTextReplaced(sql.Trim());

                    using (var command = new SqlCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        command.CommandTimeout = 9999;
                        command.Connection = connMan.OpenDBConnection(connectionString);

                        return InnerRunSqlExecuteReader(connectionString,command, useTransaction);
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
        /// <summary>
        /// The db_shrink.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        public static void db_shrink([NotNull] string connectionString, [NotNull] MsSqlDbConnectionManager DBName)
        {
            string ShrinkSql = "DBCC SHRINKDATABASE(N'" + DBName.DBConnection(connectionString).Database + "')";
            var ShrinkConn = new SqlConnection(Config.ConnectionString);
            var ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
            ShrinkConn.Open();
            ShrinkCmd.ExecuteNonQuery();
            ShrinkConn.Close();
            using (var cmd = new SqlCommand(ShrinkSql, DBName.OpenDBConnection(connectionString)))
            {
                cmd.Connection = DBName.DBConnection(connectionString);
                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                cmd.ExecuteNonQuery();
            }
        }

        private static string dbShinkMessage;
     

        /// <summary>
        /// The db_shrink.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        public static string db_shrink_new(string connectionString)
        {
            try
            {
                using (var conn = new MsSqlDbConnectionManager(connectionString))
                {
                    conn.InfoMessage += new YafDBConnInfoMessageEventHandler(dbShink_InfoMessage);
                    conn.DBConnection(connectionString).FireInfoMessageEventOnUserErrors = true;
                    string ShrinkSql = "DBCC SHRINKDATABASE(N'" + conn.DBConnection(connectionString).Database + "')";
                    var ShrinkConn = new SqlConnection(Config.ConnectionString);
                    var ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
                    ShrinkConn.Open();
                    ShrinkCmd.ExecuteNonQuery();
                    ShrinkConn.Close();
                    using (var cmd = new SqlCommand(ShrinkSql, conn.OpenDBConnection(connectionString)))
                    {
                        cmd.Connection = conn.DBConnection(connectionString);
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                        cmd.ExecuteNonQuery();
                    }
                }
                return dbShinkMessage;
            }
            finally
            {
                dbShinkMessage = string.Empty;
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
        private static void dbShink_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            dbShinkMessage = "\r\n" + e.Message;
        }

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        /// <returns>
        /// The db_shrink_warning.
        /// </returns>
        [NotNull]
        public static string db_shrink_warning()
        {
            return string.Empty;
        }

        /// <summary>
        /// The eventlog_create.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public static void eventlog_create([NotNull] string connectionString, [NotNull] object userID, [NotNull] object source, [NotNull] object description, [NotNull] object type)
        {
            try
            {
                if (userID == null)
                {
                    userID = DBNull.Value;
                }

                using (var cmd = MsSqlDbAccess.GetCommand("eventlog_create"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Type", type);
                    cmd.Parameters.AddWithValue("UserID", userID);
                    cmd.Parameters.AddWithValue("Source", source.ToString());
                    cmd.Parameters.AddWithValue("Description", description.ToString());
                    cmd.Parameters.AddWithValue("UTCTIMESTAMP", DateTime.UtcNow);
                    MsSqlDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        } 
        
        /// <summary>
        /// The method saves many questions and answers to them in a single transaction
        /// </summary>
        /// <param name="pollList">
        /// List to hold all polls data
        /// </param>
        /// <returns>
        /// Last saved poll id.
        /// </returns>
        public static int? poll_save([NotNull] string connectionString, [NotNull] List<PollSaveList> pollList)
        {
            foreach (PollSaveList question in pollList)
            {
                var sb = new StringBuilder();

                // Check if the group already exists
                if (question.TopicId > 0)
                {
                    sb.Append("select @PollGroupID = PollID  from ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Topic"));
                    sb.Append(" WHERE TopicID = @TopicID; ");
                }
                else if (question.ForumId > 0)
                {
                    sb.Append("select @PollGroupID = PollGroupID  from ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Forum"));
                    sb.Append(" WHERE ForumID = @ForumID");
                }
                else if (question.CategoryId > 0)
                {
                    sb.Append("select @PollGroupID = PollGroupID  from ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Category"));
                    sb.Append(" WHERE CategoryID = @CategoryID");
                }

                // the group doesn't exists, create a new one
                sb.Append("IF @PollGroupID IS NULL BEGIN INSERT INTO ");
                sb.Append(MsSqlDbAccess.GetObjectName("PollGroupCluster"));
                sb.Append("(UserID,Flags ) VALUES(@UserID, @Flags) SET @NewPollGroupID = SCOPE_IDENTITY(); END; ");

                sb.Append("INSERT INTO ");
                sb.Append(MsSqlDbAccess.GetObjectName("Poll"));

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
                        sb.Append(MsSqlDbAccess.GetObjectName("Choice"));
                        sb.Append("(PollID,Choice,Votes,ObjectPath,MimeType) VALUES (");
                        sb.AppendFormat("@PollID,@Choice{0},@Votes{0},@ChoiceObjectPath{0}, @ChoiceMimeType{0}", choiceCount);
                        sb.Append("); ");
                    }
                }

                // we don't update if no new group is created 
                sb.Append("IF  @PollGroupID IS NULL BEGIN  ");

                // fill a pollgroup field - double work if a poll exists 
                if (question.TopicId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Topic"));
                    sb.Append(" SET PollID = @NewPollGroupID WHERE TopicID = @TopicID; ");
                }

                // fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
                if (question.ForumId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Forum"));
                    sb.Append(" SET PollGroupID= @NewPollGroupID WHERE ForumID= @ForumID; ");
                }

                // fill a pollgroup field in Category Table if the call comes from a category's topic list 
                if (question.CategoryId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(MsSqlDbAccess.GetObjectName("Category"));
                    sb.Append(" SET PollGroupID= @NewPollGroupID WHERE CategoryID= @CategoryID; ");
                }

                // fill a pollgroup field in Board Table if the call comes from the main page poll 
                sb.Append("END;  ");

                using (var cmd = MsSqlDbAccess.GetCommand(sb.ToString(), true))
                {
                    var ret = new SqlParameter();
                    ret.ParameterName = "@PollID";
                    ret.SqlDbType = SqlDbType.Int;
                    ret.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ret);

                    var ret2 = new SqlParameter();
                    ret2.ParameterName = "@PollGroupID";
                    ret2.SqlDbType = SqlDbType.Int;
                    ret2.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ret2);

                    var ret3 = new SqlParameter();
                    ret3.ParameterName = "@NewPollGroupID";
                    ret3.SqlDbType = SqlDbType.Int;
                    ret3.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ret3);

                    cmd.Parameters.AddWithValue("@Question", question.Question);

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd.Parameters.AddWithValue("@Closes", question.Closes);
                    }

                    // set poll group flags
                    int groupFlags = 0;
                    if (question.IsBound)
                    {
                        groupFlags = groupFlags | 2;
                    }

                    cmd.Parameters.AddWithValue("@UserID", question.UserId);
                    cmd.Parameters.AddWithValue("@Flags", groupFlags);
                    cmd.Parameters.AddWithValue(
                      "@QuestionObjectPath",
                      string.IsNullOrEmpty(question.QuestionObjectPath) ? String.Empty : question.QuestionObjectPath);
                    cmd.Parameters.AddWithValue(
                      "@QuestionMimeType",
                      string.IsNullOrEmpty(question.QuestionMimeType) ? String.Empty : question.QuestionMimeType);

                    int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                    pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                    pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                    pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;

                    cmd.Parameters.AddWithValue("@PollFlags", pollFlags);

                    for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetUpperBound(1) + 1; choiceCount1++)
                    {
                        if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                        {
                            cmd.Parameters.AddWithValue(String.Format("@Choice{0}", choiceCount1), question.Choice[0, choiceCount1]);
                            cmd.Parameters.AddWithValue(String.Format("@Votes{0}", choiceCount1), 0);

                            cmd.Parameters.AddWithValue(
                              String.Format("@ChoiceObjectPath{0}", choiceCount1),
                              question.Choice[1, choiceCount1].IsNotSet() ? String.Empty : question.Choice[1, choiceCount1]);
                            cmd.Parameters.AddWithValue(
                              String.Format("@ChoiceMimeType{0}", choiceCount1),
                              question.Choice[2, choiceCount1].IsNotSet() ? String.Empty : question.Choice[2, choiceCount1]);
                        }
                    }

                    if (question.TopicId > 0)
                    {
                        cmd.Parameters.AddWithValue("@TopicID", question.TopicId);
                    }

                    if (question.ForumId > 0)
                    {
                        cmd.Parameters.AddWithValue("@ForumID", question.ForumId);
                    }

                    if (question.CategoryId > 0)
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", question.CategoryId);
                    }

                    MsSqlDbAccess.ExecuteNonQuery(cmd, true,connectionString);
                    if (ret.Value != DBNull.Value)
                    {
                        return (int?)ret.Value;
                    }
                }
            }

            return null;
        }

     
      
        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="boardID">
        /// The board ID.
        /// </param>
        /// <returns>
        /// DataTable filled will registry entries
        /// </returns>
        public static DataTable registry_list([NotNull] string connectionString, [NotNull] object name, [NotNull] object boardID)
        {
            using (var cmd = MsSqlDbAccess.GetCommand("registry_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("BoardID", boardID);
                return MsSqlDbAccess.GetData(cmd, connectionString);
            }
        }
        
     
        /// <summary>
        /// The system_initialize_executescripts.
        /// </summary>
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
        public static void system_initialize_executescripts([NotNull] string connectionString, [NotNull] string script, [NotNull] string scriptFile, bool useTransactions)
        {
            script = MsSqlDbAccess.GetCommandTextReplaced(script);

            List<string> statements = Regex.Split(script, "\\sGO\\s", RegexOptions.IgnoreCase).ToList();
            ushort sqlMajVersion = SqlServerMajorVersionAsShort(connectionString);
            using (var connMan = new MsSqlDbConnectionManager(connectionString))
            {
                // use transactions...
                if (useTransactions)
                {
                    using (SqlTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(MsSqlDbAccess.IsolationLevel))
                    {
                        foreach (string sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            sql = MsSqlDbAccess.CleanForSQLServerVersion(sql, sqlMajVersion);

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser") >= 0)
                                {
                                    continue;
                                }

                                if (sql.Length > 0)
                                {
                                    using (var cmd = new SqlCommand())
                                    {
                                        // added so command won't timeout anymore...
                                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                                        cmd.Transaction = trans;
                                        cmd.Connection = connMan.DBConnection(connectionString);
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = sql.Trim();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception x)
                            {
                                trans.Rollback();
                                throw new Exception(
                                  String.Format("FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                            }
                        }

                        trans.Commit();
                    }
                }
                else
                {
                    // don't use transactions
                    foreach (string sql0 in statements)
                    {
                        string sql = sql0.Trim();

                        // add ARITHABORT option
                        // sql = "SET ARITHABORT ON\r\nGO\r\n" + sql;

                        try
                        {
                            if (sql.ToLower().IndexOf("setuser") >= 0)
                            {
                                continue;
                            }

                            if (sql.Length > 0)
                            {
                                using (var cmd = new SqlCommand())
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
                              String.Format("FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The system_initialize_fixaccess.
        /// </summary>
        /// <param name="grant">
        /// The grant.
        /// </param>
        public static void system_initialize_fixaccess([NotNull] string connectionString, bool grant)
        {
            using (var connMan = new MsSqlDbConnectionManager(connectionString))
            {
                using (SqlTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(MsSqlDbAccess.IsolationLevel))
                {
                    // REVIEW : Ederon - would "{databaseOwner}.{objectQualifier}" work, might need only "{objectQualifier}"
                    using (
                      var da =
                        new SqlDataAdapter(
                          "select Name,IsUserTable = OBJECTPROPERTY(id, N'IsUserTable'),IsScalarFunction = OBJECTPROPERTY(id, N'IsScalarFunction'),IsProcedure = OBJECTPROPERTY(id, N'IsProcedure'),IsView = OBJECTPROPERTY(id, N'IsView') from dbo.sysobjects where Name like '{databaseOwner}.{objectQualifier}%'",
                          connMan.OpenDBConnection(connectionString)))
                    {
                        da.SelectCommand.Transaction = trans;
                        using (var dt = new DataTable("sysobjects"))
                        {
                            da.Fill(dt);
                            using (var cmd = connMan.DBConnection(connectionString).CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "select current_user";
                                var userName = (string)cmd.ExecuteScalar();

                                if (grant)
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
                                        cmd.Parameters.AddWithValue("@objname", row["Name"]);
                                        cmd.Parameters.AddWithValue("@newowner", "dbo");
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (SqlException)
                                        {
                                        }
                                    }

                                    foreach (DataRow row in dt.Select("IsView=1"))
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("@objname", row["Name"]);
                                        cmd.Parameters.AddWithValue("@newowner", "dbo");
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (SqlException)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }

                    trans.Commit();
                }
            }
        }
        
        /// <summary>
        /// The topic_delete.
        /// </summary>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="eraseTopic">
        /// The erase topic.
        /// </param>
        public static void topic_delete([NotNull] string connectionString, [NotNull] object topicID, [NotNull] object eraseTopic)
        {
            if (eraseTopic == null)
            {
                eraseTopic = 0;
            }

            if (eraseTopic.ToType<bool>())
            {
                topic_deleteAttachments(connectionString, topicID);

                topic_deleteimages(connectionString, (int)topicID);
            }
            using (var cmd = MsSqlDbAccess.GetCommand("topic_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TopicID", topicID);
                cmd.Parameters.AddWithValue("EraseTopic", eraseTopic);
                MsSqlDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }
       
        /// <summary>
        /// The topic_imagesave.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        public static DataRow topic_info([NotNull] string connectionString, [NotNull] object topicID, [NotNull] object getTags)
        {
            using (var cmd = MsSqlDbAccess.GetCommand("topic_info"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TopicID", topicID);
                cmd.Parameters.AddWithValue("GetTags", getTags);
                using (DataTable dt = MsSqlDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }
        

        #endregion

        #region Methods

        /// <summary>
        /// The get boolean registry value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The get boolean registry value.
        /// </returns>
        private static bool GetBooleanRegistryValue([NotNull] string connectionString, [NotNull] string name)
        {
            using (DataTable dt = registry_list(connectionString,name,null))
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

        /// <summary>
        /// Called from db_runsql -- just runs a sql command according to specificiations.
        /// </summary>
        /// <param name="command">
        /// </param>
        /// <param name="useTransaction">
        /// </param>
        /// <returns>
        /// The inner run sql execute reader.
        /// </returns>
        [NotNull]
        private static string InnerRunSqlExecuteReader([NotNull] string connectionString, [NotNull] SqlCommand command, bool useTransaction)
        {
            SqlDataReader reader = null;
            var results = new StringBuilder();

            try
            {
                try
                {
                    command.Transaction = useTransaction
                                            ? command.Connection.BeginTransaction(MsSqlDbAccess.IsolationLevel)
                                            : null;
                    reader = command.ExecuteReader();

                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            int rowIndex = 1;
                            var columnNames =
                              reader.GetSchemaTable().Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();

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
                            if (messageRunSql.IsSet())
                            {
                                results.AppendLine(messageRunSql);
                                results.AppendLine();
                            }
                            results.AppendLine("No Results Returned.");
                        }

                        reader.Close();

                        if (command.Transaction != null)
                        {
                            command.Transaction.Commit();
                        }
                    }
                }
                finally
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
            }
            catch (Exception x)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                
                results.AppendLine();
                results.AppendFormat("SQL ERROR: {0}", x);
            }

            return results.ToString();
        }
       
        /// <summary>
        /// Delete message and all subsequent releated messages to that ID
        /// </summary>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="deleteReason">
        /// The delete reason.
        /// </param>
        /// <param name="isDeleteAction">
        /// The is delete action.
        /// </param>
        /// <param name="DeleteLinked">
        /// The delete linked.
        /// </param>
        /// <param name="isLinked">
        /// The is linked.
        /// </param>
        private static void message_deleteRecursively([NotNull] string connectionString, [NotNull] object messageID,
                                                      bool isModeratorChanged, [NotNull] string deleteReason,
                                                      int isDeleteAction,
                                                      bool DeleteLinked,
                                                      bool isLinked)
        {
            message_deleteRecursively(connectionString,
              messageID, isModeratorChanged, deleteReason, isDeleteAction, DeleteLinked, isLinked, false);
        }

        /// <summary>
        /// The message_delete recursively.
        /// </summary>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="deleteReason">
        /// The delete reason.
        /// </param>
        /// <param name="isDeleteAction">
        /// The is delete action.
        /// </param>
        /// <param name="deleteLinked">
        /// The delete linked.
        /// </param>
        /// <param name="isLinked">
        /// The is linked.
        /// </param>
        /// <param name="eraseMessages">
        /// The erase messages.
        /// </param>
        private static void message_deleteRecursively([NotNull] string connectionString, [NotNull] object messageID,
                                                      bool isModeratorChanged, [NotNull] string deleteReason,
                                                      int isDeleteAction,
                                                      bool deleteLinked,
                                                      bool isLinked,
                                                      bool eraseMessages)
        {
            bool useFileTable = GetBooleanRegistryValue(connectionString,"UseFileTable");

            if (deleteLinked)
            {
                // Delete replies
                using (var cmd = MsSqlDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MessageID", messageID);
                    DataTable tbReplies = MsSqlDbAccess.GetData(cmd, connectionString);

                    foreach (DataRow row in tbReplies.Rows)
                    {
                        message_deleteRecursively(connectionString,
                          row["MessageID"], isModeratorChanged, deleteReason, isDeleteAction, deleteLinked, true, eraseMessages);
                    }
                }
            }

            // If the files are actually saved in the Hard Drive
            if (!useFileTable)
            {
                using (var cmd = MsSqlDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MessageID", messageID);
                    DataTable tbAttachments = MsSqlDbAccess.GetData(cmd, connectionString);

                    string uploadDir =
                      HostingEnvironment.MapPath(String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads));

                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format("{0}/{1}.{2}.yafupload", uploadDir, messageID, row["FileName"]);
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
                using (var cmd = MsSqlDbAccess.GetCommand("message_delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MessageID", messageID);
                    cmd.Parameters.AddWithValue("EraseMessage", eraseMessages);
                    MsSqlDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            else
            {
                // Delete Message
                // undelete function added
                using (var cmd = MsSqlDbAccess.GetCommand("message_deleteundelete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MessageID", messageID);
                    cmd.Parameters.AddWithValue("isModeratorChanged", isModeratorChanged);
                    cmd.Parameters.AddWithValue("DeleteReason", deleteReason);
                    cmd.Parameters.AddWithValue("isDeleteAction", isDeleteAction);
                    MsSqlDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
        }
        
       
        /// <summary>
        /// The topic_delete attachments.
        /// </summary>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        private static void topic_deleteAttachments([NotNull] string connectionString, [NotNull] object topicID)
        {
            using (var cmd = MsSqlDbAccess.GetCommand("topic_listmessages"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TopicID", topicID);
                using (DataTable dt = MsSqlDbAccess.GetData(cmd, connectionString))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        message_deleteRecursively(connectionString,row["MessageID"], true, string.Empty, 0, true, false);
                    }
                }
            }
        }

        private static void topic_deleteimages([NotNull] string connectionString, int topicID)
        {
           
                string uploadDir = HostingEnvironment.MapPath(String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads, "/", YafBoardFolders.Current.Topics));

                try
                {
                    string topicImage = string.Empty;
                    var dt = topic_info(
                     connectionString, topicID, false);
                    if (dt != null)
                    {
                        topicImage = dt["TopicImage"].ToString();
                    }

                    string fileName = string.Format("{0}/{1}.{2}.yafupload", uploadDir, topicID, topicImage);
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }

                    string fileNameThumb = string.Format("{0}/{1}.thumb.{2}.yafupload", uploadDir, topicID, topicImage);
                    if (System.IO.File.Exists(fileNameThumb))
                    {
                        System.IO.File.Delete(fileNameThumb);
                    }
                }
                catch
                {
                    // error deleting that file... 
                }
        }

        #endregion
    }
}