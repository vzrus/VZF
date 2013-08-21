// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="PostgreDbAccess.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
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
//   The Postgre Db Access.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Postgre
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;

    using Npgsql;

    using VZF.Data.Utils;
    using VZF.Types.Objects;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;

    /// <summary>
    /// The yaf db access for PostgreSQL.
    /// </summary>
    public static class PostgreDbAccess
    {
        #region Constants and Fields

        /// <summary>
        /// The _db owner.
        /// </summary>
        private static string _dbOwner;

        /// <summary>
        /// The object qualifier.
        /// </summary>
        private static string objectQualifier;

        /// <summary>
        /// The _database encoding.
        /// </summary>
        private static string _databaseEncoding;

        /// <summary>
        /// The _schema name.
        /// </summary>
        private static string _schemaName;

        /// <summary>
        /// The _database name.
        /// </summary>
        private static string _databaseName;

        /// <summary>
        /// The _grantee name.
        /// </summary>
        private static string _granteeName;

        /// <summary>
        /// The _host name.
        /// </summary>
        private static string _hostName;

        /// <summary>
        /// The _with oi ds.
        /// </summary>
        private static string _withOIDs = "false";

        /// <summary>
        /// The _large forum tree.
        /// </summary>
        private static bool _largeForumTree;

        /// <summary>
        ///   The _isolation level.
        /// </summary>
        private static IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;

        #endregion

        #region Properties

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
        /// Gets the connection parameters.
        /// </summary>
        public static List<ConnectionStringParameter> ConnectionParameters
        {
            get
            {
                var cstr = new NpgsqlConnectionStringBuilder();
                var connectionParametersBuilder = new List<ConnectionStringParameter>
                                                      {
                                                          new ConnectionStringParameter(
                                                              "Host",
                                                              cstr.Host.GetType(),
                                                              "localhost",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Port",
                                                              cstr.Port.GetType(),
                                                              "5432",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              cstr.Database.GetType(),
                                                              "yafnet",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "CommandTimeout",
                                                              cstr.CommandTimeout.GetType(),
                                                              "120",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Pooling",
                                                              cstr.Pooling.GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "PreloadReader",
                                                              cstr.PreloadReader.GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "Database",
                                                              cstr.Database.GetType(),
                                                              "5432",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "SyncNotification",
                                                              cstr.SyncNotification.GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UseExtendedTypes",
                                                              cstr.UseExtendedTypes.GetType(),
                                                              "true",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "SSL",
                                                              cstr.SSL.GetType(),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "IntegratedSecurity",
                                                              cstr.IntegratedSecurity.GetType(),
                                                              "false",
                                                              false),
                                                          new ConnectionStringParameter(
                                                              "UserName",
                                                              cstr.UserName.GetType(),
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
        /// Gets the database owner.
        /// </summary>
        public static string DatabaseOwner
        {
            get
            {
                if (_dbOwner == null || _dbOwner == "dbo")
                {
                    _dbOwner = Config.DatabaseOwner;
                }

                return _dbOwner;
            }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        public static string ObjectQualifier
        {
            get
            {
                return objectQualifier ?? (objectQualifier = Config.DatabaseObjectQualifier);
            }
        }

        /// <summary>
        /// Gets the schema name.
        /// </summary>
        public static string SchemaName
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
        /// Gets the database encoding.
        /// </summary>
        public static string DatabaseEncoding
        {
            get
            {
                return _databaseEncoding ?? (_databaseEncoding = Config.DatabaseEncoding);
            }
        }

        /// <summary>
        /// Gets the grantee name.
        /// </summary>
        public static string GranteeName
        {
            get
            {
                return _granteeName ?? (_granteeName = ConfigurationManager.AppSettings["VZF.DatabaseGranteeName"]);
            }
        }

        /// <summary>
        /// Gets the db name.
        /// </summary>
        public static string DBName
        {
            get
            {
                string[] parmRows = Config.ConnectionString.Split(';');

                foreach (string t in parmRows)
                {
                    string[] parmParams = t.Split('=');
                    for (int j = 0; j < parmRows.Length; j++)
                    {
                        if (parmParams[0].Trim().ToLower() == "database")
                        {
                            _databaseName = parmParams[1];
                            break;
                        }
                    }
                }

                return _databaseName;
            }
        }

        /// <summary>
        /// Gets the host name.
        /// </summary>
        public static string HostName
        {
            get
            {
                return _hostName ?? (_hostName = ConfigurationManager.AppSettings["VZF.DatabaseHostName"]);
            }
        }

        /// <summary>
        /// Gets the with oi ds.
        /// </summary>
        public static string WithOIDs
        {
            get
            {
                _withOIDs = Config.WithOIDs;
                if (string.IsNullOrEmpty(_withOIDs))
                {
                    _withOIDs = "false";
                }
                else
                {
                    if (_withOIDs.Trim() == "true" || _withOIDs.Trim() == "false")
                    {
                        _withOIDs = Config.WithOIDs;
                    }
                }

                return _withOIDs;
            }
        }

        /// <summary>
        /// Gets a value indicating whether large forum tree.
        /// </summary>
        public static bool LargeForumTree
        {
            get
            {
                _largeForumTree = Config.LargeForumTree;
                if (_largeForumTree == false)
                {
                    return false;
                }

                return _largeForumTree;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        public static string GetObjectName(string name)
        {
            return string.Format("{0}.{1}{2}", SchemaName, ObjectQualifier, name);
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString()
        {
            var connBuilder = new NpgsqlConnectionStringBuilder();

            foreach (var parameter in ConnectionParameters)
            {
                connBuilder.Add(parameter.Name, parameter.Value);
            }

            return connBuilder.ConnectionString;
        }


        /// <summary>
        /// Test the DB Connection.
        /// </summary>
        /// <param name="exceptionMessage">
        /// outbound ExceptionMessage
        /// </param>
        /// <returns>
        /// true if successfully connected
        /// </returns>
        public static bool TestConnection(string connectionString, [NotNull] out string exceptionMessage)
        {
            exceptionMessage = string.Empty;
            bool success = false;

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    // attempt to connect to the db...
                    connection.Open();
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
        /// Creates new NpgsqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <returns>New NpgsqlCommand</returns>
        public static NpgsqlCommand GetCommand(string commandText, bool isText)
        {

            return GetCommand(commandText, isText, null);
        }

        /// <summary>
        /// Creates new NpgsqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <param name="connection">Connection to use with command.</param>
        /// <returns>New NpgsqlCommand</returns>
        public static NpgsqlCommand GetCommand(string commandText, bool isText, NpgsqlConnection connection)
        {
            if (isText)
            {
                commandText = commandText.Replace("databaseOwner", DatabaseOwner);
                commandText = commandText.Replace("objectQualifier", ObjectQualifier);
                commandText = commandText.Replace("databaseSchema", SchemaName);

                var cmd = new NpgsqlCommand
                              {
                                  CommandType = CommandType.Text,
                                  CommandText = commandText,
                                  Connection = connection
                              };

                return cmd;
            }
            else
            {
                return GetCommand(commandText);
            }
        }

        /// <summary>
        /// Creates new NpgsqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">Base of stored procedure name.</param>
        /// <returns>New NpgsqlCommand</returns>
        public static NpgsqlCommand GetCommand(string storedProcedure)
        {
            return GetCommand(storedProcedure, null);
        }

        /// <summary>
        /// Creates new NpgsqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">Base of stored procedure name.</param>
        /// <param name="connection">Connection to use with command.</param>
        /// <returns>New NpgsqlCommand</returns>
        public static NpgsqlCommand GetCommand(string storedProcedure, NpgsqlConnection connection)
        {
            var cmd = new NpgsqlCommand
                          {
                              CommandType = CommandType.StoredProcedure,
                              CommandText = GetObjectName(storedProcedure),
                              Connection = connection
                          };

            return cmd;
        }

        /// <summary>
        /// Gets command text replaced with {databaseOwner} and {objectQualifier}.
        /// </summary>
        /// <param name="commandText">Test to transform.</param>
        /// <returns></returns>
        public static string GetCommandTextReplaced(string commandText)
        {
            // apply database owner
            commandText = commandText.Replace(
                "databaseSchema",
                !string.IsNullOrEmpty(PostgreDbAccess.SchemaName) ? PostgreDbAccess.SchemaName : "public");

            // apply object qualifier
            commandText = commandText.Replace(
                "objectQualifier_",
                !string.IsNullOrEmpty(PostgreDbAccess.ObjectQualifier) ? PostgreDbAccess.ObjectQualifier : "yaf_");

            // apply grantee name
            commandText = commandText.Replace(
                "granteeName",
                !string.IsNullOrEmpty(GranteeName) ? PostgreDbAccess.GranteeName : "public");

            // apply host name
            commandText = commandText.Replace("hostName", HostName);

            commandText = commandText.Replace(
                "databaseOwner",
                !string.IsNullOrEmpty(DatabaseOwner) ? DatabaseOwner : "yafuser");

            // apply OIDs setting
            commandText = !string.IsNullOrEmpty(WithOIDs)
                              ? commandText.Replace("withOIDs", WithOIDs.ToLower() == "true" ? WithOIDs : "FALSE")
                              : commandText.Replace("withOIDs", "FALSE");
            return commandText;
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
        public static DataSet GetDataset([NotNull] IDbCommand cmd, bool transaction, [NotNull] string connectionString)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                return GetDatasetBasic(cmd, transaction, connectionString);
            }
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
        /// The <see cref="DataTable"/> with results.
        /// </returns>
        /// <remarks>
        /// Without transaction.
        /// </remarks> 
        public static DataTable GetData(IDbCommand cmd, [NotNull] string connectionString)
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
        public static DataTable GetData(IDbCommand cmd, bool transaction, [NotNull] string connectionString)
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
        public static DataTable GetData(
            IDbCommand cmd,
            bool transaction,
            bool acceptChanges,
            [NotNull] string connectionString)
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
                using (var cmd = new NpgsqlCommand())
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
        public static void ExecuteNonQuery(IDbCommand cmd, [NotNull] string connectionString)
        {
            // defaults to using a transaction for non-queries
            ExecuteNonQuery(cmd, true, connectionString);
        }

        /// <summary>
        /// The execute non query.
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
        public static void ExecuteNonQuery(IDbCommand cmd, bool transaction, [NotNull] string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    Trace.WriteLine(cmd.ToString(), "DbAccess");

                    if (transaction)
                    {
                        // execute using a transaction
                        using (var trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
        public static int ExecuteNonQueryInt(IDbCommand cmd, [NotNull] string connectionString)
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
        public static int ExecuteNonQueryInt(IDbCommand cmd, bool transaction, [NotNull] string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);
                    Trace.WriteLine(cmd.ToString(), "DbAccess");
                    if (transaction)
                    {
                        // execute using a transaction
                        using (
                            var trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            int result = cmd.ExecuteNonQuery();
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

        public static object ExecuteScalar(IDbCommand cmd, bool transaction, string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);
                    Trace.WriteLine(cmd.ToString(), "DbAccess");
                    if (transaction)
                    {
                        // get scalar using a transaction
                        using (
                            NpgsqlTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            object results = cmd.ExecuteScalar();
                            trans.Commit();
                            return results;
                        }
                    }
                    else
                    {
                        // get scalar regular
                        return cmd.ExecuteScalar();
                    }
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        // vzrus addons - to make casts workarounds

        /// <summary>
        /// Returns DataTable from DataReader.
        /// </summary>
        /// <param name="cmd">MySql command returning selected values</param>
        /// <param name="dca">Array of DataColumn values. The correspond to select columns names in a query or sp.</param>
        /// <returns></returns>
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

            QueryCounter qc = new QueryCounter(cmd.CommandText);
            try
            {
                var dt = new DataTable();

                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);
                    Trace.WriteLine(cmd.ToString(), "DbAccess");
                    if (transaction)
                    {
                        using (
                            var trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            var reader = cmd.ExecuteReader();

                            // Retrieve column schema into our DataTable.                                                  
                            dt = GetTableColumns(dt, reader);
                            if (reader.FieldCount > 0)
                            {
                                while (reader.Read())
                                {
                                    var dr = dt.NewRow();

                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        dr[column] = TypeChecker(column, reader[column.Ordinal]);

                                        // dr[column] = reader[column.Ordinal];                                                                                 
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
                                var dr = dt.NewRow();
                                foreach (DataColumn column in dt.Columns)
                                {
                                    dr[column] = TypeChecker(column, reader[column.Ordinal]);

                                    // dr[column] = reader[column.Ordinal]; 
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
        /// The add values to data table from reader.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="dt">
        /// The dt.
        /// </param>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="acceptChanges">
        /// The accept changes.
        /// </param>
        /// <param name="firstColumnIndex">
        /// The first column index.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable AddValuesToDataTableFromReader(
            IDbCommand cmd,
            DataTable dt,
            bool transaction,
            bool acceptChanges,
            int firstColumnIndex,
            string connectionString)
        {
            return AddValuesToDataTableFromReader(
                cmd,
                dt,
                transaction,
                acceptChanges,
                firstColumnIndex,
                0,
                connectionString);
        }

        public static DataTable AddValuesToDataTableFromReader(
            IDbCommand cmd,
            DataTable dt,
            bool transaction,
            bool acceptChanges,
            int firstColumnIndex,
            int currentRow,
            string connectionString)
        {

            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        // get scalar using a transaction
                        using (
                            IDbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            IDataReader reader = cmd.ExecuteReader();
                            if (currentRow == 0)
                            {
                                firstColumnIndex = dt.Columns.Count;

                                // Retrieve column schema into a DataTable.                           
                                dt = GetTableColumns(dt, reader);
                            }

                            if (reader.FieldCount > 0)
                            {
                                while (reader.Read())
                                {
                                    int dd = 0;

                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        dd = column.Ordinal;
                                        if (dd >= firstColumnIndex && dd <= dt.Columns.Count - 1)
                                        {
                                            // dt.Rows[currentRow][column] = GetDataTableFromReaderAddValue(dt.Rows[currentRow], column, reader[column.Ordinal - firstColumnIndex]);
                                            //  dt.Rows[currentRow][column] = reader[column.Ordinal - firstColumnIndex];
                                            dt.Rows[currentRow][column] = TypeChecker(
                                                column,
                                                reader[column.Ordinal - firstColumnIndex]);
                                        }
                                    }
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
                        // get DataReader
                        IDataReader reader = cmd.ExecuteReader();

                        if (currentRow == 0)
                        {
                            firstColumnIndex = dt.Columns.Count;

                            // Retrieve column schema into a DataTable.                            
                            dt = GetTableColumns(dt, reader);
                        }

                        if (reader.FieldCount > 0)
                        {
                            while (reader.Read())
                            {
                                int dd = 0;
                                foreach (DataColumn column in dt.Columns)
                                {
                                    dd = column.Ordinal;
                                    if (dd >= firstColumnIndex && dd <= dt.Columns.Count - 1)
                                    {
                                        // dt.Rows[currentRow][column] = GetDataTableFromReaderAddValue(dt.Rows[currentRow], column, reader[column.Ordinal - firstColumnIndex]);
                                        //  dt.Rows[currentRow][column] = reader[column.Ordinal - firstColumnIndex];
                                        dt.Rows[currentRow][column] = TypeChecker(
                                            column,
                                            reader[column.Ordinal - firstColumnIndex]);
                                    }

                                }
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Used internally to get data for all the other functions
        /// </summary>
        /// <param name="cmd">
        /// </param>
        /// <param name="transaction">
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        private static DataSet GetDatasetBasic(
            [NotNull] IDbCommand cmd,
            bool transaction,
            [NotNull] string connectionString)
        {
            using (var connectionManager = new NpgsqlConnection(connectionString))
            {
                // see if an existing connection is present
                if (cmd.Connection == null)
                {
                    connectionManager.Open();
                }
                else if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                    // connectionManager.Open();
                }

                // create the adapters
                using (var ds = new DataSet())
                {
                    using (var da = new NpgsqlDataAdapter())
                    {
                        da.SelectCommand = (NpgsqlCommand)cmd;
                        da.SelectCommand.Connection = (NpgsqlConnection)cmd.Connection;
                        Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                        // use a transaction
                        if (transaction)
                        {

                            using (var trans = connectionManager.BeginTransaction(_isolationLevel))
                            {
                                try
                                {
                                    da.SelectCommand.Transaction = (NpgsqlTransaction)trans;
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

        /// <summary>
        /// We check here reader values. It's noe in use for a while
        /// </summary>
        /// <param name="column"></param>
        /// <param name="readerValue"></param>
        /// <returns></returns>
        private static object TypeChecker(DataColumn column, object readerValue)
        {
            object o = readerValue;
            return o;
        }

        /// <summary>
        /// Returns schema from DataReader(only column "ColumnName")
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DataTable GetTableColumns(IDataReader reader)
        {
            return GetTableColumns(new DataTable(), reader);
        }

        /// <summary>
        /// Returns schema from DataReader(only column "ColumnName")
        /// </summary>
        /// <param name="dummyTable"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DataTable GetTableColumns(DataTable dummyTable, IDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();

            foreach (DataRow myField in schemaTable.Rows)
            {

                string ts = myField["DataType"].ToString();
                if (ts == "System.UInt64") ts = "System.Int32";
                if (!dummyTable.Columns.Contains(myField["ColumnName"].ToString()))
                {
                    dummyTable.Columns.Add(myField["ColumnName"].ToString(), Type.GetType(ts));
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

        /// <summary>
        /// Returns schema from DataReader(looping throught all columns)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static DataTable TableSchemaReader(DataTable dt, IDataReader reader)
        {


            DataTable schemaTable = reader.GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                foreach (DataColumn myColumn in schemaTable.Columns)
                {
                    if (myColumn.ColumnName == "ColumnName")
                    {
                        string ts = myField["DataType"].ToString();

                        if (ts == "UInt64")
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
                                dt.Columns.Add(myField[myColumn] + "81_18", Type.GetType(ts));
                            }
                        }
                    }
                }
            }

            return dt;
        }

        #endregion
    }
}