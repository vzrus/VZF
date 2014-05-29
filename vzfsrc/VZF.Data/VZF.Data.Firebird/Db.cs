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
    using System.Collections.Generic;
    using System.Security;

    using VZF.Types.Objects;

    using YAF.Classes;
    using YAF.Types;

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

        #endregion

        #region reindex page controls

        /// <summary>
        /// The db_getstats_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_warning()
        {
            return "Recalculate index statistics is made or in progress.";
        }

        /// <summary>
        /// The db_reindex_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_warning()
        {
            return "Indexes recreating.";
        }

        // MS SQL Support fulltext....

        /// <summary>
        /// The _full text supported.
        /// </summary>
        private static bool _fullTextSupported;

        /// <summary>
        /// Gets or sets a value indicating whether full text supported.
        /// </summary>
        public static bool FullTextSupported
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets or sets the full text script.
        /// </summary>
        public static string FullTextScript
        {
            get
            {
                return  "firebird/fulltext.sql";
            }
        }

        /// <summary>
        /// The _script list.
        /// </summary>
        private static readonly string[] _scriptList =
            {
                "firebird/procedures_drop.sql",
                "firebird/providers/procedures_drop.sql",
                "firebird/functions_drop.sql", 
                "firebird/test_drop.sql",
                "firebird/views_drop.sql", 
                "firebird/exceptions_drop.sql",
                "firebird/preinstall.sql",
                "firebird/domains.sql", 
                "firebird/sequences.sql",
                "firebird/tables.sql", 
                "firebird/tablesupgrade.sql",
                "firebird/pkeys.sql", 
                "firebird/indexes.sql",
                "firebird/ukeys.sql", 
                "firebird/fkeys.sql",
                "firebird/triggers.sql", 
                "firebird/views.sql",
                "firebird/exceptions.sql", 
                "firebird/functions.sql",
                "firebird/providers/tables.sql", 
                "firebird/providers/pkeys.sql",
                "firebird/providers/indexes.sql",
                "firebird/providers/procedures.sql",
 
                // "firebird/nestedsets.sql",                                     
                "firebird/procedures.sql", 
                "firebird/procedures1.sql",
                "firebird/procedures2.sql",
                "firebird/postinstall.sql"
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

        #endregion

        #region Touradg Mods

        // Shinking Operation

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
        /// Gets the connection parameters.
        /// </summary>
        public static List<ConnectionStringParameter> ConnectionParameters
        {
            get
            {
                // var cstr = new FbConnectionStringBuilder();
                var connectionParametersBuilder = new List<ConnectionStringParameter>
                                                      {
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              typeof(string),
                                                              "~\\App_Data\\yafnet.fdb",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Charset",
                                                              typeof(string),
                                                              "UTF8",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Port",
                                                              typeof(string),
                                                              "3050",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "ServerType",
                                                              typeof(string),
                                                              "1",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "PacketSize",
                                                              typeof(string),
                                                              "8192",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Role",
                                                              typeof(string),
                                                              string.Empty,
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "ConnectionTimeout",
                                                              typeof(string),
                                                              "150",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "ConnectionLifeTime",
                                                              typeof(string),
                                                              "0",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Pooling",
                                                              typeof(string),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Dialect",
                                                              typeof(string),
                                                              "3",
                                                              true),
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
        /// The db_reindex_new.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new()
        {
           return string.Format(
                            "execute block as BEGIN FOR SELECT a.RDB$INDEX_NAME FROM RDB$INDICES a WHERE a.RDB$FOREIGN_KEY IS NULL "
                            + "AND a.RDB$SYSTEM_FLAG=0 AND a.RDB$UNIQUE_FLAG IS NULL AND a.RDB$RELATION_NAME LIKE '%{0}%' "
                            + "INTO :sql " + "DO BEGIN " + "sql='ALTER INDEX a.RDB$INDEX_NAME INACTIVE; "
                            + "execute statement :sql; END" + " END",
                            Config.DatabaseObjectQualifier.ToUpper());
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
            message = "Reindexing completed.";
            return string.Format(
                  "execute block as BEGIN FOR SELECT a.RDB$INDEX_NAME FROM RDB$INDICES a WHERE a.RDB$FOREIGN_KEY IS NULL "
                  + "AND a.RDB$SYSTEM_FLAG=0 AND a.RDB$UNIQUE_FLAG IS NULL AND a.RDB$RELATION_NAME LIKE '%{0}%' "
                  + "INTO :sql " + "DO BEGIN " + "sql='SET STATISTICS INDEX ' || sql; "
                  + "execute statement :sql; END" + " END",
                  Config.DatabaseObjectQualifier.ToUpper());
        }

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        public static string GetObjectName(string name)
        {
            return string.Format("{0}{1}", Config.DatabaseObjectQualifier, name);
        }

        #endregion
    }
}