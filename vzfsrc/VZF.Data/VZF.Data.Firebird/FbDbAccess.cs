// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="FbDbAccess.cs">
//   VZF by vzrus
//   Copyright (C) 2008-2013 Vladimir Zakharov
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
//   The Fb Db Access.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Firebird
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Security;

    using FirebirdSql.Data.FirebirdClient;

    using VZF.Data.Utils;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The yaf db access for SQL Server.
    /// </summary>
    [SecuritySafeCritical]
    public static class FbDbAccess
    {
        #region Constants and Fields

        /// <summary>
        /// The _db owner.
        /// </summary>
        private static string _dbOwner;

        /// <summary>
        /// The _object qualifier.
        /// </summary>
        private static string _objectQualifier;

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
        /// The _database encoding.
        /// </summary>
        private static string _databaseEncoding;

        /// <summary>
        /// The database collation.
        /// </summary>
        private static string databaseCollation;

        /// <summary>
        ///   The _isolation level.
        /// </summary>
        private static IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;

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
                                                              FbServerType.Default.ToString(),
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
        /// Gets the isolation level.
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get
            {
                return _isolationLevel;
            }
        }

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        public static string DatabaseOwner
        {
            get
            {
                return _dbOwner ?? (_dbOwner = Config.DatabaseOwner);
            }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        public static string ObjectQualifier
        {
            get
            {
                return _objectQualifier ?? (_objectQualifier = Config.DatabaseObjectQualifier);
            }
        }

        /// <summary>
        /// Gets the schema name.
        /// </summary>
        public static string SchemaName
        {
            get
            {
                return _schemaName ?? (_schemaName = Config.DatabaseSchemaName);
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
        /// Gets the database collation.
        /// </summary>
        public static string DatabaseCollation
        {
            get
            {
                return databaseCollation
                       ?? (databaseCollation = Config.GetConfigValueAsString("YAF.DatabaseCollation"));
            }
        }

        /// <summary>
        /// Gets the grantee name.
        /// </summary>
        public static string GranteeName
        {
            get
            {
                return _granteeName ?? (_granteeName = ConfigurationManager.AppSettings["YAF.DatabaseGranteeName"]);
            }
        }

        /// <summary>
        /// Gets the DB name.
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
                            string[] addrSplit = parmParams[1].Split(@"\".ToCharArray());
                            _databaseName = addrSplit[addrSplit.Length - 1].Substring(
                                0, addrSplit[addrSplit.Length - 1].Length - 4);
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
                return _hostName ?? (_hostName = ConfigurationManager.AppSettings["YAF.DatabaseHostName"]);
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
            return String.Format("{0}{1}", ObjectQualifier, name);
        }


        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString()
        {
            var connBuilder = new FbConnectionStringBuilder();

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
                using (var connection = Current.GetConnectionManager(connectionString))
                {
                    // attempt to connect to the db...
                    var conn = connection.OpenDBConnection;
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
        /// The get dataset.
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
        /// <exception cref="Exception">
        /// </exception>
        [NotNull]
        public static DataSet GetDataset([NotNull] IDbCommand cmd, bool transaction, string connectionString)
        {
            throw new Exception("Not in use for the data layer.");
        }

        /// <summary>
        /// The get command.
        /// </summary>
        /// <param name="commandText">
        /// The command text.
        /// </param>
        /// <param name="isText">
        /// The is text.
        /// </param>
        /// <returns>
        /// The <see cref="FbCommand"/>.
        /// </returns>
        public static FbCommand GetCommand(string commandText, bool isText)
        {
            return GetCommand(commandText, isText, null);
        }

        /// <summary>
        /// The get command.
        /// </summary>
        /// <param name="commandText">
        /// The command text.
        /// </param>
        /// <param name="isText">
        /// The is text.
        /// </param>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <returns>
        /// The <see cref="FbCommand"/>.
        /// </returns>
        public static FbCommand GetCommand(string commandText, bool isText, FbConnection connection)
        {
            if (isText)
            {
                var cmd = new FbCommand
                              {
                                  CommandType = CommandType.Text,
                                  CommandText = GetCommandTextReplaced(commandText),
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
        /// The <see cref="FbCommand"/>.
        /// </returns>
        public static FbCommand GetCommand(string storedProcedure)
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
        /// The <see cref="FbCommand"/>.
        /// </returns>
        public static FbCommand GetCommand(string storedProcedure, FbConnection connection)
        {
            var cmd = new FbCommand
                          {
                              CommandType = CommandType.StoredProcedure,
                              CommandText = GetObjectName(storedProcedure),
                              Connection = connection
                          };

            return cmd;
        }

        /// <summary>
        /// The get command text replaced.
        /// </summary>
        /// <param name="commandText">
        /// The command text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCommandTextReplaced(string commandText)
        {
            commandText = commandText.Replace("{databaseOwner}", DatabaseOwner);
            
            // apply object qualifier
            commandText = commandText.Replace("objQual_", !string.IsNullOrEmpty(Config.DatabaseObjectQualifier) ? Config.DatabaseObjectQualifier.ToUpper() : "YAF_");

            return commandText;
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
        /// <param name="commandText">
        /// The command text.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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
                using (FbCommand cmd = new FbCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commandText;
                    // return GetDatasetBasic(cmd, transaction).Tables[0];
                    return GetDataTableFromReader(cmd, transaction, connectionString);
                }
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public static void ExecuteNonQuery(IDbCommand cmd, string connectionString)
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
        public static void ExecuteNonQuery(IDbCommand cmd, bool transaction, string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

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

        /* public int ExecuteNonQueryInt(FbCommand cmd)
         {
             // defaults to using a transaction for non-queries
             return ExecuteNonQueryInt(cmd, true);
         }
         public int ExecuteNonQueryInt(FbCommand cmd, bool transaction)
         {
             QueryCounter qc = new QueryCounter(cmd.CommandText);
             try
             {
                 using (YafDBConnManager connMan = new YafDBConnManager())
                 {
                     // get an open connection
                     cmd.Connection = connMan.OpenDBConnection;

                     if (transaction)
                     {
                         int result = -1;
                         // execute using a transaction
                         using (FbTransaction trans = connMan.OpenDBConnection.BeginTransaction(_isolationLevel))
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
         } */

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
        public static object ExecuteScalar(FbCommand cmd, string connectionString)
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
        public static object ExecuteScalar([NotNull] IDbCommand cmd, bool transaction, string connectionString)
        {
            using (var qc = new QueryCounter(cmd.CommandText))
            {
                using (var connectionManager = new FbDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connectionManager.OpenDBConnection(connectionString);

                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                    if (!transaction)
                    {
                        // get scalar regular
                        return cmd.ExecuteScalar();
                    }
                       
                    // get scalar using a transaction
                        using (
                            var trans =
                                connectionManager.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            object results = cmd.ExecuteScalar();
                            trans.Commit();
                            return results;
                        }
                    
                }
            }
        }

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
            IDbCommand cmd, bool transaction, bool acceptChanges, string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                var dt = new DataTable();

                using (var connectionManager = new FbDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connectionManager.OpenDBConnection(connectionString);

                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                    if (transaction)
                    {
                        using (
                            IDbTransaction trans =
                                connectionManager.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            IDataReader reader = cmd.ExecuteReader();

                            // Retrieve column schema into our DataTable.                          
                            // dt = TableSchemaReader(dt, reader);
                            dt = GetTableColumns(dt, reader);
                            if (reader.FieldCount > 0)
                            {
                                while (reader.Read())
                                {
                                    DataRow dr = dt.NewRow();

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
                                DataRow dr = dt.NewRow();

                                foreach (DataColumn column in dt.Columns)
                                {
                                    dr[column] = TypeChecker(column, reader[column.Ordinal]);
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

        #endregion

        #region Private Methods

        /// <summary>
        /// The type checker.
        /// </summary>
        /// <param name="column">
        /// The column.
        /// </param>
        /// <param name="readerValue">
        /// The reader value.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private static object TypeChecker(DataColumn column, object readerValue)
        {
            var o = readerValue;

            if (column.DataType.ToString() == "System.Boolean")
            {
                if (readerValue.ToType<int>() == 1)
                {
                    return true;
                }

                return false;
            }

            return o;
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable GetTableColumns(DataTable dummyTable, IDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();

            foreach (DataRow myField in schemaTable.Rows)
            {
                string ts = myField["DataType"].ToString();
                if (ts == "System.Int16")
                {
                    ts = "System.Boolean";
                }

                if (ts == "System.String" && (myField["ColumnSize"].ToType<int>() == 1))
                {
                    ts = "System.Boolean";
                }

                if (!dummyTable.Columns.Contains(myField["ColumnName"].ToString()))
                {
                    dummyTable.Columns.Add(myField["ColumnName"].ToString(), Type.GetType(ts));
                }
                else
                {
                    if (!myField["ColumnName"].ToString().Contains("81_18"))
                    {
                        dummyTable.Columns.Add(myField["ColumnName"] + "81_18", Type.GetType(ts));
                    }
                }
            }

            return dummyTable;
        }

/*
        private static DataTable TableSchemaReader(DataTable dt, IDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                foreach (DataColumn myColumn in schemaTable.Columns)
                {
                    if (myColumn.ColumnName != "ColumnName")
                    {
                        continue;
                    }

                    string ts = myField["DataType"].ToString();
                    if (ts == "System.Int16")
                    {
                        ts = "System.Boolean";
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

            return dt;
        }
*/

        #endregion
    }
}