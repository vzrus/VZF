// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="MySqlDbAccess.cs">
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
//   The Db Access.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Mysql
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web;

    using MySql.Data.MySqlClient;

    using VZF.Data.Utils;
    using VZF.Types.Objects;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;

    /// <summary>
    /// Provides open/close management for DB Connections
    /// </summary>
    public static class MySqlDbAccess
    {
        #region Constants and Fields

        /// <summary>
        ///   Result filter list
        /// </summary>
        private static readonly IList<IDataTableResultFilter> _resultFilterList = new List<IDataTableResultFilter>();

        /// <summary>
        ///   The _isolation level.
        /// </summary>
        private const IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;

        /// <summary>
        /// The _schema name.
        /// </summary>
        private static string _schemaName;

        /// <summary>
        /// The _database object qualifier.
        /// </summary>
        private static string _databaseObjectQualifier;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Current IDbAccess -- needs to be switched to direct injection into all DB classes.
        /// </summary>
        public static IDbAccess Current
        {
            get
            {
                return ServiceLocatorAccess.CurrentServiceProvider.Get<IDbAccess>();
            }
        }

        /// <summary>
        /// Gets the connection parameters.
        /// </summary>
        public static List<ConnectionStringParameter> ConnectionParameters
        {
            get
            {
                var cstr = new MySqlConnectionStringBuilder();

                var connectionParametersBuilder = new List<ConnectionStringParameter>
                                                      {
                                                          new ConnectionStringParameter(
                                                              "OldGuids",
                                                              cstr.OldGuids.GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowBatch",
                                                              cstr.AllowBatch.GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "Server",
                                                              cstr.Server.GetType(),
                                                              "localhost",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              cstr.Database.GetType(),
                                                              "yafnet",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CharacterSet",
                                                              cstr.CharacterSet.GetType(
                                                                  ),
                                                              "utf8",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Port",
                                                              cstr.Port.GetType(),
                                                              "3306",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "TreatTinyAsBoolean",
                                                              cstr.TreatTinyAsBoolean
                                                              .GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "TreatBlobsAsUTF8",
                                                              cstr.TreatBlobsAsUTF8
                                                              .GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "DefaultCommandTimeout",
                                                              cstr.DefaultCommandTimeout
                                                              .GetType(),
                                                              "120",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CheckParameters",
                                                              cstr.CheckParameters
                                                              .GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Pooling",
                                                              cstr.Pooling.GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseCompression",
                                                              cstr.UseCompression
                                                              .GetType(),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseAffectedRows",
                                                              cstr.UseAffectedRows
                                                              .GetType(),
                                                              "false",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "PersistSecurityInfo",
                                                              cstr.PersistSecurityInfo
                                                              .GetType(),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "AllowBatch",
                                                              cstr.AllowBatch.GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowUserVariables",
                                                              cstr.AllowUserVariables
                                                              .GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "AllowZeroDateTime",
                                                              cstr.AllowZeroDateTime
                                                              .GetType(),
                                                              "true",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "IgnorePrepare",
                                                              cstr.IgnorePrepare.GetType
                                                              (),
                                                              "false",
                                                              true),
                                                          new ConnectionStringParameter(
                                                              "ProcedureCacheSize",
                                                              cstr.ProcedureCacheSize
                                                              .GetType(),
                                                              "50",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UserID",
                                                              cstr.UserID.GetType(),
                                                              "admin",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Password",
                                                              cstr.Password.GetType(),
                                                              "password",
                                                              false)
                                                      };

                return connectionParametersBuilder;
            }
        }


        /// <summary>
        ///   Gets IsolationLevel.
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get
            {
                return _isolationLevel;
            }
        }

        /// <summary>
        /// Gets the schema name.
        /// </summary>
        public static string DatabaseSchemaName
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = Config.DatabaseSchemaName;
                }

                if (string.IsNullOrEmpty(_schemaName) || _schemaName == "dbo")
                {
                    _schemaName = "public";
                }

                return _schemaName;
            }
        }

        /// <summary>
        /// Gets the database object qualifier.
        /// </summary>
        public static string DatabaseObjectQualifier
        {
            get
            {
                if (string.IsNullOrEmpty(_databaseObjectQualifier))
                {
                    _databaseObjectQualifier = Config.DatabaseObjectQualifier;
                }

                if (string.IsNullOrEmpty(_databaseObjectQualifier) || _databaseObjectQualifier == "dbo")
                {
                    _databaseObjectQualifier = "vzf_";
                }

                return _schemaName;
            }
        }


        /// <summary>
        /// Gets the result filter list.
        /// </summary>
        public static IList<IDataTableResultFilter> ResultFilterList
        {
            get
            {
                return _resultFilterList;
            }
        }

        #endregion

        /// <summary>
        /// Gets command text replaced with {databaseName} and {objectQualifier}.
        /// </summary>
        /// <param name="commandText">
        /// Test to transform.
        /// </param>
        /// <returns>
        /// The get command text replaced.
        /// </returns>
        public static string GetCommandTextReplaced(string commandText)
        {
            /* commandText = commandText.Replace("{databaseName}", Config.DatabaseOwner);
             commandText = commandText.Replace("{objectQualifier}", Config.DatabaseObjectQualifier); */

            return commandText;
        }

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseName}.{objectQualifier}name</returns>
        public static string GetObjectName(string name)
        {
            return string.Format("`{0}`.`{1}{2}`", DatabaseSchemaName, Config.DatabaseObjectQualifier, name);
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString()
        {
            var connBuilder = new MySqlConnectionStringBuilder();

            foreach (var parameter in ConnectionParameters)
            {
                connBuilder.Add(parameter.Name, parameter.Value);
            }

            return connBuilder.ConnectionString;
        }

        /// <summary>
        /// The get connection params.
        /// </summary>
        /// <returns>
        /// The <see cref="ConcurrentDictionary"/>.
        /// </returns>
        public static ConcurrentDictionary<string, Type> GetConnectionParams()
        {
            // this doesn't work in Medium Trust.
            if (General.GetCurrentTrustLevel() <= AspNetHostingPermissionLevel.Medium)
            {
                return null;
            }

            var myType = typeof(MySqlConnectionStringBuilder);

            var myPropertyInfo = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Display information for all properties. 
            var cd = new ConcurrentDictionary<string, Type>();
            foreach (var t in myPropertyInfo)
            {
                var dd = t.GetCustomAttributesData();
                if (dd == null || dd.Count <= 0)
                {
                    continue;
                }

                foreach (var customAttributeData in dd)
                {
                    try
                    {
                        if (customAttributeData == null || customAttributeData.ConstructorArguments.Count <= 0
                            || (customAttributeData.ConstructorArguments[0].Value.ToString() != "Connection"
                                && customAttributeData.ConstructorArguments[0].Value.ToString() != "Pooling"
                                && customAttributeData.ConstructorArguments[0].Value.ToString() != "Security"
                                && customAttributeData.ConstructorArguments[0].Value.ToString() != "Advanced"
                                && customAttributeData.ConstructorArguments[0].Value.ToString() != "Authentication"))
                        {
                            continue;
                        }

                        cd.AddOrUpdate(t.Name, t.PropertyType, (key, value) => value);
                        break;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return cd;
        }

        /// <summary>
        /// The test connection.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="exceptionMessage">
        /// The exception message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool TestConnection(string connectionString, [NotNull] out string exceptionMessage)
        {
            exceptionMessage = string.Empty;
            bool success = false;

            try
            {
                try
                {
                    using (var connMan = new MySqlDbConnectionManager(connectionString))
                    {
                        // just attempt to open the connection to test if a DB is available.
                        MySqlConnection getConn = connMan.OpenDBConnection(connectionString);
                    }
                }
                catch (MySqlException ex)
                {
                    // errorStr = "Unable to connect to the Database. Exception Message: " + ex.Message + " (" + ex.Number + ")";
                    return false;
                }

                // success
                success = true;
            }
            catch (Exception x)
            {
                // unable to connect...
                exceptionMessage = x.Message;
            }

            return success;
        }

        /// <summary>
        /// Creates new SqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <returns>New SqlCommand</returns>
        public static MySqlCommand GetCommand(string commandText, bool isText)
        {
            return GetCommand(commandText, isText, null);
        }

        /// <summary>
        /// Creates new MySqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <param name="connection">Connection to use with command.</param>
        /// <returns>New MySqlCommand</returns>

        public static MySqlCommand GetCommand(string commandText, bool isText, MySqlConnection connection)
        {
            if (isText)
            {
                // commandText = commandText.Replace("{databaseName}", DatabaseOwner);
                commandText = commandText.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);
                commandText = commandText.Replace("{databaseName}", Config.DatabaseSchemaName);

                var cmd = new MySqlCommand
                              {
                                  CommandType = CommandType.Text,
                                  CommandText = commandText,
                                  Connection = connection
                              };

                return cmd;
            }

            return GetCommand(commandText);
        }

        /// <summary>
        /// The get command.
        /// </summary>
        /// <param name="storedProcedure">
        /// The stored procedure.
        /// </param>
        /// <returns>
        /// The <see cref="MySqlCommand"/>.
        /// </returns>
        public static MySqlCommand GetCommand(string storedProcedure)
        {
            return GetCommand(storedProcedure, null);
        }

        /// <summary>
        /// The get command.
        /// </summary>
        /// <param name="storedProcedure">
        /// The stored procedure.
        /// </param>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <returns>
        /// The <see cref="MySqlCommand"/>.
        /// </returns>
        public static MySqlCommand GetCommand(string storedProcedure, MySqlConnection connection)
        {
            var cmd = new MySqlCommand
                          {
                              CommandType = CommandType.StoredProcedure,
                              CommandText = GetObjectName(storedProcedure),
                              Connection = connection
                          };

            return cmd;
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetData(IDbCommand cmd, string connectionString)
        {
            return GetDataTableFromReader(cmd, false, true, connectionString);
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetData(IDbCommand cmd, bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true, connectionString);
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="acceptChanges">
        /// The accept changes.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetData(IDbCommand cmd, bool transaction, bool acceptChanges, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, acceptChanges, connectionString);
        }

        /// <summary>
        /// Gets data out of database using a plain text string command
        /// </summary>
        /// <param name="commandText">command text to be executed</param>
        /// <returns>DataTable with results</returns>
        /// <remarks>Without transaction.</remarks>
        public static DataTable GetData(string commandText, string connectionString)
        {
            return GetData(commandText, false, connectionString);
        }

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="commandText">
        /// The command text.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetData(string commandText, bool transaction, string connectionString)
        {
            var qc = new QueryCounter(commandText);
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commandText;

                    return GetDataTableFromReader(cmd, transaction, connectionString);
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// Executes a NonQuery
        /// </summary>
        /// <param name="cmd">NonQuery to execute</param>
        /// <remarks>Without transaction</remarks>
        public static void ExecuteNonQuery(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            ExecuteNonQuery(cmd, true, connectionString);
        }

        public static void ExecuteNonQuery(IDbCommand cmd, bool transaction, string connectionString)
        {
            QueryCounter qc = new QueryCounter(cmd.CommandText);
            try
            {
                Trace.WriteLine(cmd.ToString(), "DbAccess");
                using (MySqlDbConnectionManager connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        // execute using a transaction
                        using (
                            IDbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                    else
                    {
                        // don't use a transaction
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// The execute non query int.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int ExecuteNonQueryInt(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            return ExecuteNonQueryInt(cmd, true, connectionString);
        }

        /// <summary>
        /// The execute non query int.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int ExecuteNonQueryInt(IDbCommand cmd, bool transaction, string connectionString)
        {
            QueryCounter qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (MySqlDbConnectionManager connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        int result = -1;
                        // execute using a transaction
                        using (
                            IDbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            result = cmd.ExecuteNonQuery();
                            trans.Commit();
                            return result;
                        }
                    }
                    else
                    {
                        // don't use a transaction
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// The execute scalar.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object ExecuteScalar(IDbCommand cmd, string connectionString)
        {
            // default to using a transaction for scaler commands
            return ExecuteScalar(cmd, true, connectionString);
        }

        /// <summary>
        /// The execute scalar.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object ExecuteScalar(IDbCommand cmd, bool transaction, string connectionString)
        {
            QueryCounter qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (!transaction)
                    {
                        // get scalar regular
                        return cmd.ExecuteScalar();
                    }
                    else
                    {
                        // get scalar using a transaction
                        using (
                            IDbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            object results = cmd.ExecuteScalar();
                            trans.Commit();
                            return results;
                        }
                    }
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// The get data table from reader.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetDataTableFromReader(IDbCommand cmd, string connectionString)
        {
            return GetDataTableFromReader(cmd, false, true, connectionString);
        }

        /// <summary>
        /// The get data table from reader.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetDataTableFromReader(IDbCommand cmd, bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true, connectionString);
        }

        /// <summary>
        /// The get data table from reader.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="acceptChanges">
        /// The accept changes.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetDataTableFromReader(
            IDbCommand cmd,
            bool transaction,
            bool acceptChanges,
            string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                var dt = new DataTable();

                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        using (
                            IDbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            IDataReader reader = cmd.ExecuteReader();

                            // Retrieve column schema into our DataTable.                   
                            dt = GetTableColumns(dt, reader);
                            if (reader.FieldCount > 0)
                            {
                                while (reader.Read())
                                {
                                    DataRow dr = dt.NewRow();

                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        dr[column] = ReaderValueConverter(column, reader[column.Ordinal]);
                                    }

                                    dt.Rows.Add(dr);
                                }
                            }

                            reader.Close();
                            trans.Commit();

                            if (acceptChanges)
                            {
                                dt.AcceptChanges();
                            }

                            return dt;
                        }
                    }
                    else
                    {
                        // get scalar regular
                        IDataReader reader = cmd.ExecuteReader();

                        // Retrieve column schema into our DataTable.              
                        dt = GetTableColumns(dt, reader);
                        if (reader.FieldCount > 0)
                        {
                            while (reader.Read())
                            {
                                DataRow dr = dt.NewRow();
                                foreach (DataColumn column in dt.Columns)
                                {
                                    dr[column] = ReaderValueConverter(column, reader[column.Ordinal]);
                                }

                                dt.Rows.Add(dr);
                            }
                        }

                        reader.Close();
                        if (acceptChanges)
                        {
                            dt.AcceptChanges();
                        }

                        return dt;
                    }
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// The get dataset.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        public static DataSet GetDataset([NotNull] IDbCommand cmd, bool transaction, string connectionString)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                return GetDatasetBasic(cmd, transaction, connectionString);
            }
        }

        /// <summary>
        /// Converts values if necessary.
        /// </summary>
        /// <param name="column">DataColumn</param>
        /// <param name="readerValue"></param>
        /// <returns></returns>
        private static object ReaderValueConverter(DataColumn column, object readerValue)
        {
            // It's currently implemented in the methods to make work faster
            // if (column.DataType.Name == "Guid") return GuidConverter((Guid)readerValue);
            return readerValue;
        }

        /// <summary>
        /// Here we get table structure in the most simple way - only type.
        /// </summary>
        /// <param name="dt">
        /// The dt.
        /// </param>
        /// <param name="reader">
        /// </param>
        /// <returns>
        /// </returns>
        private static DataTable GetTableColumns(DataTable dt, IDataReader reader)
        {
            return GetTableColumns(dt, reader, true);
        }

        /// <summary>
        /// The get table columns.
        /// </summary>
        /// <param name="dummyTable">
        /// The dummy table.
        /// </param>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="convertFromUInt64ToInt32">
        /// The convert from u int 64 to int 32.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable GetTableColumns(
            DataTable dummyTable,
            IDataReader reader,
            bool convertFromUInt64ToInt32)
        {
            DataTable schemaTable = reader.GetSchemaTable();

            foreach (DataRow myField in schemaTable.Rows)
            {
                string ts = myField["DataType"].ToString();
                if ((ts == "System.UInt64" || ts == "System.Int64") && convertFromUInt64ToInt32)
                {
                    ts = "System.Int32";
                }

                if (ts == "System.SByte")
                {
                    ts = "System.Boolean";
                }

                if (!dummyTable.Columns.Contains(myField["ColumnName"].ToString()))
                {
                    dummyTable.Columns.Add(myField["ColumnName"].ToString(), Type.GetType(ts));
                    if (myField["ColumnName"].ToString().ToLowerInvariant() == "isapproved")
                    {
                        string gg = ts;
                    }
                }
                else
                {
                    if (!myField["ColumnName"].ToString().Contains("81_18"))
                    {
                        dummyTable.Columns.Add(myField["ColumnName"].ToString() + "81_18", Type.GetType(ts));
                    }
                }
            }

            return dummyTable;
        }

        /*
        /// <summary>
        /// The table schema reader.
        /// </summary>
        /// <param name="dt">
        /// The dt.
        /// </param>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="convertFromUInt64ToInt32">
        /// The convert from u int 64 to int 32.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable TableSchemaReader(DataTable dt, IDataReader reader, bool convertFromUInt64ToInt32)
        {
            DataTable schemaTable = reader.GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                foreach (DataColumn myColumn in schemaTable.Columns)
                {
                    if (myColumn.ColumnName == "ColumnName")
                    {
                        string ts = myField["DataType"].ToString();

                        if (ts == "System.UInt64" && convertFromUInt64ToInt32)
                        {
                            ts = "System.Int32";
                        }

                        if (!dt.Columns.Contains(myField[myColumn].ToString()))
                        {
                            dt.Columns.Add(myField[myColumn].ToString(), Type.GetType(ts));
                        }
                        else
                        {
                            if (!myField[myColumn].ToString().Contains("81_18"))
                            {
                                dt.Columns.Add(myField[myColumn].ToString() + "81_18", Type.GetType(ts));
                            }
                        }
                    }
                }
            }

            return dt;
        }
*/

       

        #region Methods

        /// <summary>
        /// Used internally to get data for all the other functions.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        [NotNull]
        private static DataSet GetDatasetBasic([NotNull] IDbCommand cmd, bool transaction, string connectionString)
        {
            using (var connectionManager = new MySqlConnection(connectionString))
            {
                // see if an existing connection is present
                if (cmd.Connection == null)
                {
                    connectionManager.Open();
                }
                else if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                // create the adapters
                using (var ds = new DataSet())
                {
                    using (var da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = (MySqlCommand)cmd;
                        da.SelectCommand.Connection = (MySqlConnection)cmd.Connection;
                        Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                        // use a transaction
                        if (transaction)
                        {
                            using (var trans = connectionManager.BeginTransaction(_isolationLevel))
                            {
                                try
                                {
                                    da.SelectCommand.Transaction = (MySqlTransaction)trans;
                                    da.Fill(ds);
                                }
                                finally
                                {
                                    trans.Commit();
                                }
                            }
                        }
                        else
                        {
                            // no transaction
                            da.Fill(ds);
                        }

                        // return the dataset
                        return ds;
                    }
                }
            }
        }


        #endregion
    }

}
