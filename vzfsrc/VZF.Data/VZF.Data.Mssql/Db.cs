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
   
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Text;

    using VZF.Types.Objects;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Types;

    #endregion

    /// <summary>
    /// All the Database functions for YAF
    /// </summary>
    public static class Db
    {
        // Parameter 10
        #region Constants and Fields


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

        public static bool DbInstallTransactions
        {
            get { return true; }
        }

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
        public static string ProviderAssemblyName
        {
            get
            {
                return "System.Data.SqlClient";
            }
        }

        /// <summary>
        ///   Gets ScriptFolder.
        /// </summary>
        public static string ScriptFolder
        {
            get
            {
                return "mssql";
            }
        }       

        public static string SqlScriptsDelimiterRegexPattern
        {
            get
            {
                return "\\sGO\\s";
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the connection parameters.
        /// </summary>
        public static List<ConnectionStringParameter> ConnectionParameters
        {
            get
            {
                var cstr = new DbConnectionStringBuilder();
                var connectionParametersBuilder = new List<ConnectionStringParameter>();
                bool locals = false;

                if (locals)
                {
                    connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", typeof(string), @".\SQLExpress", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("IntegratedSecurity", typeof(bool), "true", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("UserInstance", typeof(bool), "True", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("AttachDBFilename", typeof(string), @"|DataDirectory|Database1.mdf", false));
                }
                else
                {
                    connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", typeof(string), "(local)", false));

                    // connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", cstr.DataSource.GetType(), "190.190.200.100,1433", false));

                    //connectionParametersBuilder.Add(new ConnectionStringParameter("NetworkLibrary", cstr.NetworkLibrary.GetType(), "DBMSSOCN", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("InitialCatalog", typeof(string), "yafnet", false));
                    //connectionParametersBuilder.Add(new ConnectionStringParameter("IntegratedSecurity", cstr.IntegratedSecurity.GetType(), "True", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("UserID", typeof(string), "admin", false));
                    connectionParametersBuilder.Add(new ConnectionStringParameter("Password", typeof(string), "password", false));
                }

                connectionParametersBuilder.Add(new ConnectionStringParameter("Pooling", typeof(bool), "True", false));
                connectionParametersBuilder.Add(new ConnectionStringParameter("ApplicationName", typeof(string), string.Empty, false));
                // connectionParametersBuilder.Add(new ConnectionStringParameter("MultipleActiveResultSets", cstr.MultipleActiveResultSets.GetType(), "false", false));
                // connectionParametersBuilder.Add(new ConnectionStringParameter("TrustServerCertificate", cstr.TrustServerCertificate.GetType(), "true", false));

                return connectionParametersBuilder;
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
        /// The db_reindex_new.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new()
        {
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

            return sb.ToString();
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

            message = "Stats completed successfully and is not verbose.";

            return sb.ToString();
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
        public static string db_reindex_warning()
        {
            return string.Empty;
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
            message = "Shrink operation is completed";
            return "DBCC SHRINKDATABASE(N'{0}')";
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
        public static string db_recovery_mode_new([NotNull] string connectionString, [NotNull] string dbRecoveryMode)
        {
            return string.Empty;
         /*  try
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
                /* string expressDb = string.Empty;
                 if (error.Message.ToUpperInvariant().Contains("'SET'"))
                 {
                     expressDb = "MS SQL Server Express Editions are not supported by the application.";
                 }
                 recoveryDbModeMessage += "\r\n{0}\r\n{1}".FormatWith(error.Message, expressDb);
                 return recoveryDbModeMessage; */
          /*  }

            finally
            {
                // recoveryDbModeMessage = string.Empty;
            }
        */
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

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        public static string GetProfileStructure()
        {
            return "select top 1 * from {0};";
        }
      
        #endregion
    }
}