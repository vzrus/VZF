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
//   The Firebird data access layer.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Firebird
{
    using System;
    using System.Data;
    using System.Security;
    using System.Text;

    using FirebirdSql.Data.FirebirdClient;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;
  

    /// <summary>
    /// All the Database functions for VZF
    /// </summary>
    [SecuritySafeCritical]
    public static class Db
    {
        // added by vzrus

        #region ConnectionStringOptions

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

        /// <summary>
        /// Gets the provider assembly name.
        /// </summary>
        public static string ProviderAssemblyName
        {
            get
            {
                return "FirebirdSql.Data.FirebirdClient";
            }
        }

        #endregion
     
        #region yaf_Poll


        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save(
            [NotNull] string connectionString,
            [NotNull] System.Collections.Generic.List<PollSaveList> pollList)
        {

            foreach (PollSaveList question in pollList)
            {
                var sb = new System.Text.StringBuilder();
                var paramSb = new System.Text.StringBuilder("EXECUTE BLOCK (");
                sb.Append(" RETURNS(OUT_POLLGROUPID INTEGER) AS  DECLARE VARIABLE OUT_POLLID INTEGER;  BEGIN ");
                // Check if the group already exists

                if (question.CategoryId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(FbDbAccess.GetObjectName("CATEGORY"));
                    sb.Append(" WHERE CATEGORYID = :I_CATEGORYID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_CATEGORYID INTEGER = ?,");
                }
                if (question.ForumId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(FbDbAccess.GetObjectName("FORUM"));
                    sb.Append(" WHERE FORUMID = :I_FORUMID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_FORUMID INTEGER = ?,");
                }

                if (question.TopicId > 0)
                {
                    sb.Append(" SELECT POLLID FROM ");
                    sb.Append(FbDbAccess.GetObjectName("TOPIC"));
                    sb.Append(" WHERE TOPICID = :I_TOPICID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_TOPICID INTEGER = ?,");
                }


                // if the poll group doesn't exists, create a new one
                sb.Append("IF (OUT_POLLGROUPID IS NULL) THEN BEGIN ");

                sb.Append("INSERT INTO ");
                sb.Append(FbDbAccess.GetObjectName("POLLGROUPCLUSTER"));
                sb.AppendFormat(
                    "(POLLGROUPID, USERID, FLAGS) VALUES((SELECT NEXT VALUE FOR SEQ_{0}PGC_POLLGROUPID FROM RDB$DATABASE), :GROUPUSERID, :POLLGROUPFLAGS) RETURNING POLLGROUPID INTO :OUT_POLLGROUPID;  END ",
                    FbDbAccess.ObjectQualifier.ToUpper());

                paramSb.Append("GROUPUSERID INTEGER = ?,");
                paramSb.Append("POLLGROUPFLAGS INTEGER = ?,");
                if (question.CategoryId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("CATEGORY"));
                    sb.Append(" SET POLLGROUPID = :OUT_POLLGROUPID WHERE CATEGORYID = :I_CATEGORYID; ");

                }
                if (question.ForumId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("FORUM"));
                    sb.Append(" SET POLLGROUPID = :OUT_POLLGROUPID WHERE FORUMID = :I_FORUMID; ");
                }

                if (question.TopicId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("TOPIC"));
                    sb.Append(" SET POLLID = :OUT_POLLGROUPID WHERE TOPICID = :I_TOPICID; ");
                }

                // System.Text.StringBuilder paramSb = new System.Text.StringBuilder("EXECUTE BLOCK ("); 
                // INSERT in poll
                sb.Append(" INSERT INTO ");
                sb.Append(FbDbAccess.GetObjectName("POLL"));
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
                    FbDbAccess.ObjectQualifier.ToUpper());
                sb.Append(":QUESTION");

                paramSb.Append(" QUESTION VARCHAR(255) = ?,");

                sb.Append(",:USERID");
                paramSb.Append("USERID INTEGER = ?,");

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    sb.Append(",:CLOSES");
                    paramSb.Append("CLOSES TIMESTAMP = ?,");
                }
                sb.Append(",:OUT_POLLGROUPID");

                sb.Append(",:FLAGS");
                paramSb.Append("FLAGS INTEGER = ?,");
                if (question.QuestionObjectPath.IsSet())
                {
                    sb.Append(",:QUESTIONOBJECTPATH");
                    paramSb.Append("OBJECTPATH VARCHAR(255) = ?,");
                }
                if (question.QuestionMimeType.IsSet())
                {
                    sb.Append(",:QUESTIONMIMETYPE");
                    paramSb.Append("MIMETYPE VARCHAR(50) = ?,");
                }

                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.Append(") RETURNING POLLID INTO :OUT_POLLID; ");

                // The cycle through question reply choices to create prepare statement

                // The cycle through question reply choices            
                for (uint choiceCount = 0; choiceCount < question.Choice.GetLength(0); choiceCount++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                    {

                        sb.Append("INSERT INTO ");
                        sb.Append(FbDbAccess.GetObjectName("CHOICE"));

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
                            FbDbAccess.ObjectQualifier.ToUpper());
                        sb.AppendFormat(":OUT_POLLID,:CHOICE{0},:VOTES{0}", choiceCount);
                        if (question.QuestionObjectPath.IsSet())
                        {
                            sb.AppendFormat(",:CHOICEOBJECTPATH{0}", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            sb.AppendFormat(",:CHOICEMIMETYPE{0}", choiceCount);
                        }
                        sb.Append("); ");
                        paramSb.AppendFormat("CHOICE{0} VARCHAR(255) = ?,", choiceCount);
                        paramSb.AppendFormat("VOTES{0} INTEGER = ?,", choiceCount);

                        if (question.QuestionObjectPath.IsSet())
                        {
                            paramSb.AppendFormat("CHOICEOBJECTPATH{0} VARCHAR(255) = ?,", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            paramSb.AppendFormat("CHOICEMIMETYPE{0} VARCHAR(50) = ?,", choiceCount);
                        }

                    }

                }

                sb.Append(" SUSPEND; END;");
                var cmd = new FbCommand { CommandText = paramSb.ToString().TrimEnd(',') + ") " + sb.ToString() };
                var connMan = new FbDbConnectionManager(connectionString);
                FbConnection con = connMan.OpenDBConnection(connectionString);
                FbTransaction trans = con.BeginTransaction(FbDbAccess.IsolationLevel);

                cmd.Transaction = trans;
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
                    cmd.Parameters.Add(new FbParameter("I_CATEGORYID", FbDbType.Integer)).Value = question.CategoryId
                                                                                                  ?? categoryId;
                }
                if (question.ForumId > 0)
                {
                    cmd.Parameters.Add(new FbParameter("I_FORUMID", FbDbType.Integer)).Value = question.ForumId
                                                                                               ?? forumId;
                }
                if (question.TopicId > 0)
                {
                    cmd.Parameters.Add(new FbParameter("I_TOPICID", FbDbType.Integer)).Value = question.TopicId
                                                                                               ?? topicId;
                }
                cmd.Parameters.Add(new FbParameter("GROUPUSERID", FbDbType.Integer)).Value = question.UserId;
                int pollGroupFlags = question.IsBound ? 0 | 2 : 0;
                cmd.Parameters.Add(new FbParameter("POLLGROUPFLAGS", FbDbType.Integer)).Value = pollGroupFlags;
                cmd.Parameters.Add(new FbParameter("QUESTION", FbDbType.VarChar)).Value = question.Question;
                cmd.Parameters.Add(new FbParameter("USERID", FbDbType.Integer)).Value = question.UserId;

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
                {
                    cmd.Parameters.Add(new FbParameter("CLOSES", FbDbType.TimeStamp)).Value = question.Closes;
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


                cmd.Parameters.Add(new FbParameter("FLAGS", FbDbType.Integer)).Value = pollFlags;
                if (question.QuestionObjectPath.IsSet())
                {
                    cmd.Parameters.Add(new FbParameter("QUESTIONOBJECTPATH", FbDbType.VarChar)).Value =
                        question.QuestionObjectPath;
                }
                if (question.QuestionMimeType.IsSet())
                {
                    cmd.Parameters.Add(new FbParameter("QUESTIONMIMETYPE", FbDbType.VarChar)).Value =
                        question.QuestionMimeType;
                }

                for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetLength(0); choiceCount1++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                    {
                        cmd.Parameters.Add(new FbParameter(String.Format("CHOICE{0}", choiceCount1), FbDbType.VarChar))
                            .Value = question.Choice[0, choiceCount1];
                        cmd.Parameters.Add(new FbParameter(String.Format("VOTES{0}", choiceCount1), FbDbType.Integer))
                            .Value = 0;
                        if (question.Choice[1, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(
                                new FbParameter(String.Format("CHOICEOBJECTPATH{0}", choiceCount1), FbDbType.VarChar))
                                .Value = question.Choice[1, choiceCount1].IsNotSet()
                                             ? String.Empty
                                             : question.Choice[1, choiceCount1];
                        }
                        if (question.Choice[2, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(
                                new FbParameter(String.Format("CHOICEMIMETYPE{0}", choiceCount1), FbDbType.VarChar))
                                .Value = question.Choice[2, choiceCount1].IsNotSet()
                                             ? String.Empty
                                             : question.Choice[2, choiceCount1];
                        }
                    }
                }



                // cmd.Prepare();   
                int? result = Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, true, connectionString));
                trans.Commit();
                con.Close();
                return result;

            }
            return null;
        }

        #endregion
        

        #region vzrus addons

        /// <summary>
        /// Gets the btn get stats name.
        /// </summary>
        public static string btnGetStatsName
        {
            get
            {
                return "Recalculate YAF Table Index Statistics";
            }
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

        /// <summary>
        /// Gets the btn reindex name.
        /// </summary>
        public static string btnReindexName
        {
            get
            {
                return "Recreate YAF Tables indice";
            }
        }

        // DB Maintenance page buttons name

        /// <summary>
        /// Gets the btn shrink name.
        /// </summary>
        public static string btnShrinkName
        {
            get
            {
                return "Shrink Database";
            }
        }

        /// <summary>
        /// Gets the btn recovery mode name.
        /// </summary>
        public static string btnRecoveryModeName
        {
            get
            {
                return "Set Recovery Mode";
            }
        }

        // DB Maintenance page panels visibility

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

        #endregion

        #region reindex page controls



        /// <summary>
        /// The db_index_simplelist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable db_index_simplelist([NotNull] string connectionString)
        {
            using (
                var cmd =
                    FbDbAccess.GetCommand(
                        String.Format(
                            "SELECT a.RDB$INDEX_NAME FROM RDB$INDICES a WHERE a.RDB$FOREIGN_KEY IS NULL AND a.RDB$SYSTEM_FLAG=0 AND a.RDB$UNIQUE_FLAG IS NULL AND a.RDB$RELATION_NAME LIKE '%{0}%'",
                            FbDbAccess.ObjectQualifier.ToUpper()),
                        true))
            {
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

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
                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);

                    DataTable indexList = Db.db_index_simplelist(connectionString);
                    foreach (DataRow indexName in indexList.Rows)
                    {
                        using (var cmd1 = new FbCommand(String.Format("SET STATISTICS INDEX {0}", indexName[0])))
                        {
                            cmd1.CommandType = CommandType.Text;
                            // up the command timeout...
                            cmd1.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                            // run it...
                            FbDbAccess.ExecuteNonQuery(cmd1, false, connectionString);
                        }
                    }

                    return getStatsMessage;
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

        /* public static void db_getstats([NotNull] string connectionString, FbDbConnectionManager conn)
        {
            DataTable indexList = Db.db_index_simplelist(connectionString);
            foreach (DataRow indexName in indexList.Rows)
            {
                using (var cmd1 = new FbCommand(String.Format("SET STATISTICS INDEX {0}", indexName[0])))
                {
                    cmd1.CommandType = CommandType.Text;
                    // up the command timeout...
                    cmd1.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                    // run it...
                    FbDbAccess.ExecuteNonQuery(cmd1, false, connectionString);
                }
            }
        } */

        public static string db_getstats_warning()
        {
            return "Recalculate index statistics is made or in progress.";
        }

        private static string reindexDbMessage;

        public static string db_reindex_new([NotNull] string connectionString)
        {
            DataTable indexList = Db.db_index_simplelist(connectionString);
            foreach (DataRow indexName in indexList.Rows)
            {
                // using (var cmd = new FbCommand(String.Format("EXECUTE BLOCK AS BEGIN EXECUTE STATEMENT 'ALTER INDEX {0} INACTIVE'; EXECUTE STATEMENT 'ALTER INDEX {0} ACTIVE';END", indexName[0])))
                try
                {
                    using (var connMan = new FbDbConnectionManager(connectionString))
                    {
                        connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(reindexDb_InfoMessage);
                        using (
                            var cmd =
                                new FbCommand(
                                    string.Format(
                                        "EXECUTE BLOCK AS BEGIN EXECUTE STATEMENT 'ALTER INDEX {0} INACTIVE'; END",
                                        indexName[0])))
                        {
                            cmd.CommandType = CommandType.Text;

                            // up the command timeout...
                            cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                            // run it...
                            FbDbAccess.ExecuteNonQuery(cmd, false, connectionString);
                        }
                    }
                }
                finally
                {

                }
            }
            string reindexDbMessageRet = reindexDbMessage;
            reindexDbMessage = string.Empty;
            return reindexDbMessageRet;
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
            return "Indexes recreating.";
        }

        private static string messageRunSql;

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
        public static string db_runsql_new([NotNull] string connectionString, string sql, bool useTransaction)
        {
            var results = new System.Text.StringBuilder();

            try
            {
                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(fb_runSql_InfoMessage);

                    using (var cmd = new FbCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        FbDataReader reader = null;

                        using (
                            FbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                        {
                            try
                            {
                                cmd.Connection = connMan.DBConnection(connectionString);
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
                                        results.Append(drd["ColumnName"].ToString());
                                        gg++;
                                    }

                                    results.AppendLine();

                                    while (reader.Read())
                                    {
                                        results.AppendFormat(@"""{0}""", rowIndex++);

                                        // dump all columns...
                                        foreach (var col in columnNames)
                                        {
                                            results.AppendFormat(
                                                @",""{0}""",
                                                reader[col].ToString().Replace("\"", "\"\""));
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
                                trans.Commit();
                            }
                            catch (Exception x)
                            {

                                // rollback...
                                trans.Rollback();
                                results.AppendLine();
                                results.AppendFormat("SQL ERROR: {0}", x);
                            }

                            if (reader != null)
                            {
                                reader.Close();
                            }

                            return results.ToString();
                        }
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
        private static void fb_runSql_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            messageRunSql = "\r\n" + e.Message;
        }


        // MS SQL Support fulltext....
        private static bool _fullTextSupported;

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

        private static string _fullTextScript = "firebird/fulltext.sql";

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


        private static readonly string[] _scriptList =
            {
                "firebird/procedures_drop.sql",
                "firebird/providers/procedures_drop.sql",
                "firebird/functions_drop.sql", "firebird/test_drop.sql",
                "firebird/views_drop.sql", "firebird/exceptions_drop.sql",
                "firebird/domains.sql", "firebird/sequences.sql",
                "firebird/tables.sql", "firebird/tablesupgrade.sql",
                "firebird/pkeys.sql", "firebird/indexes.sql",
                "firebird/ukeys.sql", "firebird/fkeys.sql",
                "firebird/triggers.sql", "firebird/views.sql",
                "firebird/exceptions.sql", "firebird/functions.sql",
                "firebird/providers/tables.sql", "firebird/providers/pkeys.sql",
                "firebird/providers/indexes.sql",
                "firebird/providers/procedures.sql", 
                // "firebird/nestedsets.sql",                                     
                "firebird/procedures.sql", "firebird/procedures1.sql",
                "firebird/procedures2.sql"
            };

        public static string[] ScriptList
        {
            get
            {
                return _scriptList;
            }
        }

        public static void system_initialize_executescripts(
            [NotNull] string connectionString,
            string script,
            string scriptFile,
            bool useTransactions)
        {
            CreateDatabase(connectionString);
            script = FbDbAccess.GetCommandTextReplaced(script);
            // apply database owner
            if (!String.IsNullOrEmpty(FbDbAccess.SchemaName))
            {
                script = script.Replace("dbN", FbDbAccess.DBName.ToUpper());
            }
            else
            {
                script = script.Replace("dbN", "YAFNET");
            }

            // apply grantee name
            if (!String.IsNullOrEmpty(FbDbAccess.GranteeName))
            {
                script = script.Replace("grantName", FbDbAccess.GranteeName.ToUpper());
            }
            else
            {
                script = script.Replace("grantName", "PUBLIC");
            }
            // apply host name
            script = script.Replace("hostName", FbDbAccess.HostName);



            //Scripts separation regexp
            string[] statements = System.Text.RegularExpressions.Regex.Split(
                script,
                "(?:--GO)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Here comes add SET ARITHABORT ON for MSSQL amd Linq class
            // statements.Insert(0, "SET ARITHABORT ON");

            using (var connMan = new FbDbConnectionManager(connectionString))
            {

                // use transactions...
                if (useTransactions)
                {
                    using (
                        FbTransaction trans =
                            connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                    {
                        foreach (string sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser") >= 0) continue;

                                if (sql.Length > 0)
                                {
                                    using (var cmd = new FbCommand())
                                    {
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
                                    String.Format(
                                        "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}",
                                        scriptFile,
                                        sql,
                                        x.Message));
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

                        try
                        {
                            if (sql.ToLower().IndexOf("setuser") >= 0) continue;

                            if (sql.Length > 0)
                            {
                                using (var cmd = new FbCommand())
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
                                    "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}",
                                    scriptFile,
                                    sql,
                                    x.Message));
                        }
                    }
                }
            }
        }

        public static void system_initialize_fixaccess([NotNull] string connectionString, bool bGrant)
        {
            // USED FOR UPGRADE FROM VERY OLD VERSIONS

        }

        #endregion

        /// <summary>
        /// The create database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public static void CreateDatabase([NotNull] string connectionString)
        {
            CreateDatabase(connectionString, true);
        }

        /// <summary>
        /// The create database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="embeded">
        /// The embeded.
        /// </param>
        public static void CreateDatabase([NotNull] string connectionString, bool embeded)
        {
            //FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
            // csb.ServerType = FbServerType.Embedded;
            // csb.Database = @I_"C:\Documents and Settings\bob\My Documents\Projects\yaffirebird\YetAnotherForum.NET\App_Data\yafnet.fdb";
            // csb.UserID = "SYSDBA";
            // csb.Password = "myfirebird";

            //if (System.IO.File.Exists(csb.Database))
            // {
            //System.IO.File.Delete(csb.Database);

            //  }
            // FbConnection.CreateDatabase(csb.ToString());  
        }

        #region Touradg Mods

        //Shinking Operation

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_warning()
        {
            return null;
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
            /* String ShrinkSql = "DBCC SHRINKDATABASE(N'" + DBName.DBConnection.Database + "')";
            FbConnection ShrinkConn = new FbConnection(VZF.Classes.Config.ConnectionString);
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
            return string.Empty;
        }

        //Set Recovery
        public static string db_recovery_mode_warning()
        {
            return string.Empty;
        }

        public static string db_recovery_mode_new([NotNull] string connectionString, string dbRecoveryMode)
        {
            /* String RecoveryMode = "ALTER DATABASE " + DBName.DBConnection.Database + " SET RECOVERY " + dbRecoveryMode;
             FbConnection RecoveryModeConn = new FbConnection(VZF.Classes.Config.ConnectionString);
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