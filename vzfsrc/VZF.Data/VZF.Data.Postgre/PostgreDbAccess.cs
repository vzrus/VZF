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
//   The common db.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Postgre
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;

    using Npgsql;

    using VZF.Data.Utils;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    /// <summary>
    /// The yaf db access for PostgreSQL.
    /// </summary>
    public static class PostgreDbAccess
    {
        #region Constants and Fields

        /// <summary>
        /// The _db owner.
        /// </summary>
        static private string _dbOwner;

        /// <summary>
        /// The _object qualifier.
        /// </summary>
        static private string _objectQualifier;

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
        private static string _withOIDs ="false";

        /// <summary>
        /// The _large forum tree.
        /// </summary>
        private static bool _largeForumTree ;

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

        // TODO: Move it to Config
        
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
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = Config.DatabaseScheme;
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
                return _granteeName ?? (_granteeName = ConfigurationManager.AppSettings["YAF.DatabaseGranteeName"]);
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

                for (int i = 0; i < parmRows.Length; i++)
                {
                    string[] parmParams = parmRows[i].Split('=');
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
                return _hostName ?? (_hostName = ConfigurationManager.AppSettings["YAF.DatabaseHostName"]);
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
                return _largeForumTree != false && _largeForumTree;
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
            return string.Format(
                           "{0}.{1}{2}",
                           SchemaName,
                           ObjectQualifier,
                name);
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <param name="parm1">
        /// The parm 1.
        /// </param>
        /// <param name="parm2">
        /// The parm 2.
        /// </param>
        /// <param name="parm3">
        /// The parm 3.
        /// </param>
        /// <param name="parm4">
        /// The parm 4.
        /// </param>
        /// <param name="parm5">
        /// The parm 5.
        /// </param>
        /// <param name="parm6">
        /// The parm 6.
        /// </param>
        /// <param name="parm7">
        /// The parm 7.
        /// </param>
        /// <param name="parm8">
        /// The parm 8.
        /// </param>
        /// <param name="parm9">
        /// The parm 9.
        /// </param>
        /// <param name="parm10">
        /// The parm 10.
        /// </param>
        /// <param name="parm11">
        /// The parm 11.
        /// </param>
        /// <param name="parm12">
        /// The parm 12.
        /// </param>
        /// <param name="parm13">
        /// The parm 13.
        /// </param>
        /// <param name="parm14">
        /// The parm 14.
        /// </param>
        /// <param name="parm15">
        /// The parm 15.
        /// </param>
        /// <param name="parm16">
        /// The parm 16.
        /// </param>
        /// <param name="parm17">
        /// The parm 17.
        /// </param>
        /// <param name="parm18">
        /// The parm 18.
        /// </param>
        /// <param name="parm19">
        /// The parm 19.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userPassword">
        /// The user password.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString(
           [NotNull] string parm1,
           [NotNull] string parm2,
           [NotNull] string parm3,
           [NotNull] string parm4,
           [NotNull] string parm5,
           [NotNull] string parm6,
           [NotNull] string parm7,
           [NotNull] string parm8,
           [NotNull] string parm9,
           [NotNull] string parm10,
                     bool parm11,
                     bool parm12,
                     bool parm13,
                     bool parm14,
                     bool parm15,
                     bool parm16,
                     bool parm17,
                     bool parm18,
                     bool parm19,
           [NotNull] string userId,
           [NotNull] string userPassword)
        {
            var connBuilder = new StringBuilder();

            connBuilder.AppendFormat("Host = {0}".FormatWith(parm1));
            connBuilder.AppendFormat("Port = {0}".FormatWith(parm2));
         // connBuilder.AppendFormat("Encoding = {0}".FormatWith(parm3;
            connBuilder.AppendFormat("Database = {0}".FormatWith(parm4));
            connBuilder.AppendFormat("CommandTimeout = {0}".FormatWith(Convert.ToInt32(parm5)));
         // connBuilder.AppendFormat("Compatible ={0}".FormatWith(
         // connBuilder.AppendFormat("ConnectionLifeTime ={0}".FormatWith(
         // connBuilder.AppendFormat("Enlist={0}".FormatWith(
         // connBuilder.AppendFormat("Protocol =ProtocolVersion.Version3");
         // connBuilder.AppendFormat("SslMode =SslMode.Allow");
         // connBuilder.AppendFormat("SearchPath = {0}".FormatWith(
         // connBuilder.AppendFormat("Timeout = {0}".FormatWith(
            connBuilder.AppendFormat("Pooling= {0}".FormatWith(parm13));
            connBuilder.AppendFormat("PreloadReader =  {0}".FormatWith(parm14));
            connBuilder.AppendFormat("SyncNotification =  {0}".FormatWith(parm15));
            connBuilder.AppendFormat("UseExtendedTypes =  {0}".FormatWith(parm16));
            connBuilder.AppendFormat("SSL =  {0}".FormatWith(parm17));
            connBuilder.AppendFormat("IntegratedSecurity =  {0}".FormatWith(parm18));                      
            connBuilder.AppendFormat("UserName =  {0}".FormatWith(userId));
            connBuilder.AppendFormat("Password =  {0}".FormatWith(userPassword));            

            return connBuilder.ToString();
        }

        /// <summary>
        /// The get connection params.
        /// </summary>
        /// <returns>
        /// The <see cref="ConcurrentDictionary"/>.
        /// </returns>
        public static ConcurrentDictionary<string, Type> GetConnectionParams()
        {
            var myType = typeof(NpgsqlConnectionStringBuilder);
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
                        cd.AddOrUpdate(t.Name, t.PropertyType, (key, value) => value);
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
                    catch (Exception e)
                    {

                    }
                }
            }
            return cd;
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

            return GetCommand( commandText, isText, null );
        }

        /// <summary>
        /// Creates new NpgsqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.
        /// </param>
        /// <param name="isText">Determines whether command text is text or stored procedure.
        /// </param>
        /// <param name="connection">Connection to use with command.
        /// </param>
        /// <returns>
        /// New NpgsqlCommand
        /// </returns>
        public static NpgsqlCommand GetCommand(string commandText, bool isText, NpgsqlConnection connection)
        {
            if (!isText)
            {
                return GetCommand(commandText);
            }

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

        /// <summary>
        /// Creates new NpgsqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">
        /// Base of stored procedure name.
        /// </param>
        /// <returns>
        /// New NpgsqlCommand
        /// </returns>
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
            var cmd = new NpgsqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = GetObjectName(storedProcedure);
            cmd.Connection = connection;

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
            commandText = commandText.Replace("databaseSchema", !string.IsNullOrEmpty(SchemaName) ? PostgreDbAccess.SchemaName : "public");
            
            // apply object qualifier
            commandText = commandText.Replace("objectQualifier_", !string.IsNullOrEmpty(PostgreDbAccess.ObjectQualifier) ? PostgreDbAccess.ObjectQualifier : "yaf_");
           
            // apply grantee name
            commandText = commandText.Replace("granteeName", !string.IsNullOrEmpty(PostgreDbAccess.GranteeName) ? PostgreDbAccess.GranteeName : "public");
           
            // apply host name
            commandText = commandText.Replace("hostName", PostgreDbAccess.HostName);

            commandText = commandText.Replace("databaseOwner", !string.IsNullOrEmpty(PostgreDbAccess.DatabaseOwner) ? PostgreDbAccess.DatabaseOwner : "yafuser");
            
            // apply OIDs setting
            commandText = !string.IsNullOrEmpty(PostgreDbAccess.WithOIDs) ? commandText.Replace("withOIDs", PostgreDbAccess.WithOIDs.ToLower() == "true" ? PostgreDbAccess.WithOIDs : "FALSE") : commandText.Replace("withOIDs", "FALSE");
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
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                return GetDatasetBasic(cmd, transaction, connectionString);
            }
            finally
            {
                qc.Dispose();
            }
        }

        /// <summary>
        /// Gets data out of the database
        /// </summary>
        /// <param name="cmd">
        /// The SQL Command
        /// </param>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        /// <returns>
        /// DataTable with the results
        /// </returns>
        /// <remarks>
        /// Without transaction.
        /// </remarks>
        /// <summary>
        /// Gets data out of the database
        /// </summary>
        /// <returns>
        /// DataTable with the results
        /// </returns>
        /// <remarks>
        /// Without transaction.
        /// </remarks>
        [NotNull] 
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
        [NotNull]
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
        public static DataTable GetData(IDbCommand cmd, bool transaction, bool acceptChanges, [NotNull] string connectionString)
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
             using (var cmd = new NpgsqlCommand())
             {
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = commandText;
                 return GetDataTableFromReader(cmd, transaction, connectionString);
             }
         }

        /// <summary>
        /// Executes a NonQuery without a transaction.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
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
                using (var conn = PostgreDbConnectionManager.OpenDBConnection(connectionString))
                {
                    // get an open connection
                    cmd.Connection = conn;

                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

                    if (transaction)
                    {
                        // execute using a transaction
                        using (NpgsqlTransaction trans = conn.BeginTransaction(_isolationLevel))
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
                using (var conn = PostgreDbConnectionManager.OpenDBConnection(connectionString))
                {
                    // get an open connection
                    cmd.Connection = conn;
                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");
                    if (!transaction)
                    {
                        // don't use a transaction
                        return cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // execute using a transaction
                        using (
                            NpgsqlTransaction trans =
                                conn.BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            int result = cmd.ExecuteNonQuery();
                            trans.Commit();
                            return result;
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
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var conn = PostgreDbConnectionManager.OpenDBConnection(connectionString))
                {
                    // get an open connection
                    cmd.Connection = conn;
                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");
                    if (!transaction)
                    {
                        // get scalar regular
                        return cmd.ExecuteScalar();
                    }
                    else
                    {
                        // get scalar using a transaction
                        using (var trans = conn.BeginTransaction(_isolationLevel))
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
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable GetDataTableFromReader(IDbCommand cmd,
               bool transaction, string connectionString)
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

                using (var conn = PostgreDbConnectionManager.OpenDBConnection(connectionString))
                {
                    // get an open connection
                    cmd.Connection = conn;
                    Trace.WriteLine(cmd.ToDebugString(), "DbAccess");
                    if (transaction)
                    {
                        using (NpgsqlTransaction trans = conn.BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            var reader = cmd.ExecuteReader();
                           
                            // Retrieve column schema into our DataTable.                                                  
                            dt = GetTableColumns(dt, reader);
                            if (reader != null && reader.FieldCount > 0)
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

                            if (reader != null)
                            {
                                reader.Close();
                            }

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
                        if (reader != null && reader.FieldCount > 0)
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

                        if (reader != null)
                        {
                            reader.Close();
                        }

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
        public static DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, string connectionString)
        {
            return AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, 0, connectionString);
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
        /// <param name="currentRow">
        /// The current row.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, int currentRow, string connectionString)
        {
            var qc = new QueryCounter(cmd.CommandText);
            try
            {
                using (var conn = PostgreDbConnectionManager.OpenDBConnection(connectionString))
                {
                    // get an open connection
                    cmd.Connection = conn;

                    if (transaction)
                    {
                        // get scalar using a transaction
                        using (IDbTransaction trans = conn.BeginTransaction(_isolationLevel))
                        {
                            cmd.Transaction = trans;
                            IDataReader reader = cmd.ExecuteReader();
                            if (currentRow == 0)
                            {
                                firstColumnIndex = dt.Columns.Count;

                                // Retrieve column schema into a DataTable.                           
                                dt = GetTableColumns(dt, reader);
                            }

                            if (reader != null && reader.FieldCount > 0)
                            {
                                while (reader.Read())
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        int dd = column.Ordinal;
                                        if (dd >= firstColumnIndex && dd <= dt.Columns.Count - 1)
                                        {
                                            // dt.Rows[currentRow][column] = GetDataTableFromReaderAddValue(dt.Rows[currentRow], column, reader[column.Ordinal - firstColumnIndex]);
                                            //  dt.Rows[currentRow][column] = reader[column.Ordinal - firstColumnIndex];
                                            dt.Rows[currentRow][column] = TypeChecker(column, reader[column.Ordinal - firstColumnIndex]);
                                        }
                                    }
                                }
                            }

                            if (reader != null)
                            {
                                reader.Close();
                            }

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
                        var reader = cmd.ExecuteReader();

                        if (currentRow == 0)
                        {
                            firstColumnIndex = dt.Columns.Count;
                           
                            // Retrieve column schema into a DataTable.                            
                            dt = GetTableColumns(dt, reader);
                        }

                        if (reader != null && reader.FieldCount > 0)
                        {
                            while (reader.Read())
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    int dd = column.Ordinal;
                                    if (dd >= firstColumnIndex && dd <= dt.Columns.Count - 1)
                                    {
                                        // dt.Rows[currentRow][column] = GetDataTableFromReaderAddValue(dt.Rows[currentRow], column, reader[column.Ordinal - firstColumnIndex]);
                                        //  dt.Rows[currentRow][column] = reader[column.Ordinal - firstColumnIndex];
                                        dt.Rows[currentRow][column] = TypeChecker(column, reader[column.Ordinal - firstColumnIndex]);
                                    }
                                }
                            }
                        }

                        if (reader != null)
                        {
                            reader.Close();
                        }

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
        /// The get dataset basic.
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
        private static DataSet GetDatasetBasic(
            [NotNull] IDbCommand cmd, bool transaction, [NotNull] string connectionString)
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
        /// We check here reader values. It's not in use for a while.
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
        static private object TypeChecker(DataColumn column, object readerValue)
        {
            object o = readerValue;
            return o;
        }

        /// <summary>
        /// The get table columns.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        static private DataTable GetTableColumns(IDataReader reader)
        {
            return GetTableColumns(new DataTable(), reader);
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
        static private DataTable GetTableColumns(DataTable dummyTable, IDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();
          
            foreach (DataRow myField in schemaTable.Rows)
            {
                string ts = myField["DataType"].ToString();
                if (ts == "System.UInt64")
                {
                    ts = "System.Int32";
                }

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
        /// The table schema reader.
        /// </summary>
        /// <param name="dt">
        /// The dt.
        /// </param>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        static private DataTable TableSchemaReader(DataTable dt, IDataReader reader)
        {
            var schemaTable = reader.GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                foreach (DataColumn myColumn in schemaTable.Columns)
                {
                    if (myColumn.ColumnName != "ColumnName")
                    {
                        continue;
                    }

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
                            dt.Columns.Add(myField[myColumn].ToString() + "81_18", Type.GetType(ts));
                        }
                    }

                    break;
                }
            }

            return dt;
        }
     #endregion
    }
}
