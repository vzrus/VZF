#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File Db.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:20 PM.
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

namespace VZF.Data.Postgre
{
    using System.Collections.Generic;
    using System.Text;

    using VZF.Types.Objects;

    using YAF.Classes;
    using YAF.Types;

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

        public static bool DbInstallTransactions
        {
            get { return true; }
        }

        /// <summary>
        /// The full text supported.
        /// </summary>
        public static bool FullTextSupported = false;

        /// <summary>
        /// The full text script.
        /// </summary>
        public static string FullTextScript = "postgre/fulltext.sql";

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
                return false;
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

        /// <summary>
        /// The db_reindex_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_warning()
        {
            return "Operation completed. Database tables reindexing can take a lot of time.";
        }

        /// <summary>
        /// The db_getstats_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_warning()
        {
            return
                "Operation completed. Vacuum operation blocks all database objects! If there is a lot of indexes, the database can be blocked for a long time. Choose a right timing!";
        }

        /// <summary>
        ///   Gets ScriptFolder.
        /// </summary>
        public static string ScriptFolder
        {
            get
            {
                return "pgsql";
            }
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
        public static string db_shrink_warning()
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_shrink_new.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_new(out string message)
        {
            message = "Vacuum full operation is completed";
            return "VACUUM FULL;";
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
            return string.Empty;
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
                                                              "Host",
                                                              typeof(string),
                                                              "localhost",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Port",
                                                              typeof(string),
                                                              "5432",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              typeof(string),
                                                              "yafnet",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CommandTimeout",
                                                              typeof(string),
                                                              "120",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Pooling",
                                                             typeof(string),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "PreloadReader",
                                                             typeof(string),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              typeof(string),
                                                              "5432",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "SyncNotification",
                                                              typeof(string),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseExtendedTypes",
                                                              typeof(string),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "SSL",
                                                              typeof(string),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "IntegratedSecurity",
                                                              typeof(string),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UserName",
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

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new()
        {
            // VACUUM ANALYZE VERBOSE;VACUUM cannot be implemeneted within function or multiline command line string 

          return string.Format(@"REINDEX DATABASE {0};", Config.DatabaseSchemaName);
           //  ex.Message + "\r\n" + ex.StackTrace
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
        public static string db_getstats(out  string message)
        {
            message = "Vacuume analize completed successfully.";
            return "VACUUM ANALYZE VERBOSE;";
        }

        /// <summary>
        /// The db_getfirstcharset.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getfirstcharset()
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_getfirstcollation.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getfirstcollation()
        {
            return string.Empty;
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
            charsetColumn = value = string.Empty;
            return string.Empty;
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
            charsetColumn = collationColumn = string.Empty;
            return string.Empty;
        }

        public static string GetProfileStructure()
        {
            return "select * from {0} limit 1;";
        }

        #endregion        
    }
}
