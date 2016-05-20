#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File Db.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:19 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

namespace VZF.Data.Mysql
{
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    using VZF.Types.Objects;

    using YAF.Classes;
    using YAF.Types;

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

        /// <summary>
        /// Gets the sql scripts delimiter regex pattern.
        /// </summary>
        public static string SqlScriptsDelimiterRegexPattern
        {
            get
            {
                return "(?:--GO)";
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
       /* public static string DbSizeOld([NotNull] string connectionString)
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
        */

        public static bool DbInstallTransactions
        {
            get { return true; }
        }
        
        // MySQL InnoDB engine currently don't support fulltext....

        /// <summary>
        /// Gets a value indicating whether full text supported.
        /// </summary>
        public static bool FullTextSupported
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the full text script.
        /// </summary>
        public static string FullTextScript
        {
            get
            {
                return ScriptFolder + "/fulltext.sql";
            }
        }

        /// <summary>
        ///   Gets ScriptFolder.
        /// </summary>
        public static string ScriptFolder
        {
            get
            {
                return "mysql";
            }
        }     
   
        #endregion
      
        #region VZ-Team additions
        
        /// <summary>
        /// The db_getstats_table.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
       /* public static DataTable db_getstats_table([NotNull] string connectionString)
        {
            using (var cmd = new MySqlCommand(string.Format("SHOW TABLE STATUS FROM {0};", MySqlDbAccess.DatabaseSchemaName)))
            {
                cmd.CommandType = CommandType.Text;

                return MySqlDbAccess.GetData(cmd, false, connectionString);
            }
        } */

        /// <summary>
        /// The db_getstats_alltables.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
     /*   public static DataTable db_getstats_alltables([NotNull] string connectionString)
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
        } */

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
     /*   public static string db_getstats_tablex([NotNull] string connectionString)
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
        } */

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
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel reindex.
        /// </summary>
        public static bool PanelReindex
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel shrink.
        /// </summary>
        public static bool PanelShrink
        {
            get
            {
                return false;
            }
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
      /*  public static DataTable db_reindex_table([NotNull] string connectionString)
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
        } */
       
        /// <summary>
        /// Gets the connection parameters.
        /// </summary>
        public static List<ConnectionStringParameter> ConnectionParameters
        {
            get
            {
                var cstr = new StringBuilder();

                var connectionParametersBuilder = new List<ConnectionStringParameter>
                                                      {
                                                          new ConnectionStringParameter(
                                                              "OldGuids",
                                                              typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowBatch",
                                                             typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Server",
                                                              typeof(string),
                                                              "localhost",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              typeof(string),
                                                              "yafnet",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CharacterSet",
                                                              typeof(string),
                                                              "utf8",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Port",
                                                              typeof(string),
                                                              "3306",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "TreatTinyAsBoolean",
                                                              typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "TreatBlobsAsUTF8",
                                                             typeof(bool),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "DefaultCommandTimeout",
                                                             typeof(int),
                                                              "120",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CheckParameters",
                                                              typeof(bool),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Pooling",
                                                             typeof(bool),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseCompression",
                                                              typeof(bool),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseAffectedRows",
                                                              typeof(bool),
                                                              "false",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "PersistSecurityInfo",
                                                              typeof(bool),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "AllowBatch",
                                                              typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowUserVariables",
                                                              typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowZeroDateTime",
                                                             typeof(bool),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "IgnorePrepare",
                                                              typeof(bool),
                                                              "false",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "ProcedureCacheSize",
                                                              typeof(int),
                                                              "50",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UserID",
                                                              typeof(string),
                                                              "admin",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Password",
                                                              typeof(string),
                                                              "password",
                                                              false)
                                                      };
                return connectionParametersBuilder;
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
            return string.Empty;
        }

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new()
        {
            return string.Empty;
                    var sb = new StringBuilder();
                    sb.Append(string.Format(
                                    "ANALYZE TABLE {0}.{1}user;",
                                    Config.DatabaseSchemaName,
                                    Config.DatabaseObjectQualifier));
                    sb.Append(
                          "SELECT table_name FROM  INFORMATION_SCHEMA.SCHEMATA s LEFT JOIN INFORMATION_SCHEMA.TABLES t ON s.schema_name = t.table_schema WHERE t.engine ='InnoDB'  AND t.TABLE_TYPE='BASE TABLE' ");
                    sb.Append(";");
                    return string.Format(@"REINDEX DATABASE {0};", Config.DatabaseSchemaName);
        }

        /// <summary>
        /// The db_getstats.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats(out string message)
        {
            message = "Analizing comleted. Unfortunately it's not verbose.";
            return string.Format(
                            "ANALYZE TABLE {0}.{1}user;",
                             Config.DatabaseSchemaName,
                            Config.DatabaseObjectQualifier);
        }

        /// <summary>
        /// The db_getfirstcharset.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getfirstcharset()
        {
            return "SHOW VARIABLES LIKE 'character_set_database'";
        }

        /// <summary>
        /// The db_getfirstcollation.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getfirstcollation()
        {
            return "SHOW VARIABLES LIKE 'collation_database'";
        }

        /// <summary>
        /// The db_collations_data.
        /// </summary>
        /// <param name="charsetColumn">
        /// The charset column.
        /// </param>
        /// <param name="collationColumn">
        /// The collation column.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_collations_data(out string charsetColumn, out string collationColumn)
        {
            charsetColumn = "Charset";
            collationColumn = "Default collation";
            return "SHOW CHARACTER SET;";
        }

        /// <summary>
        /// The db_checkvalidcharset.
        /// </summary>
        /// <param name="charsetColumn">
        /// The charset column.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_checkvalidcharset(out string charsetColumn, out string value)
        {
            charsetColumn = "Variable_name";
            value = "Value";
            return "SHOW VARIABLES LIKE 'character_set_database'";
        }

        public static string GetProfileStructure()
        {
            return "select * from {0} limit 1;";
        }

        #endregion 
    }
}
