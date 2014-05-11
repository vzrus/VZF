namespace VZF.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;

    using VZF.Data.Firebird;
    using VZF.Data.MsSql;
    using VZF.Data.Mysql;
    using VZF.Data.Postgre;
    using VZF.Utils;

    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Data.DAL;

    public partial class CommonSqlDbAccess: IDbAccess 
    { 
        /// <summary>
        ///   The _connection manager type.
        /// </summary>
        private Type _connectionManagerType;
        /// <summary>
        ///   Result filter list
        /// </summary>
        private readonly IList<IDataTableResultFilter> _resultFilterList = new List<IDataTableResultFilter>();
        #region Properties

        /// <summary>
        ///   Gets Current IDbAccess -- needs to be switched to direct injection into all DB classes.
        /// </summary>
        private static string ConnectionToken
        {
            get
            {
                return string.Empty;
            }
        }

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
        ///   Gets IsolationLevel.
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                        return MsSqlDbAccess.IsolationLevel;
                    case "Npgsql":
                        return PostgreDbAccess.IsolationLevel;
                    case "MySql.Data.MySqlClient":
                        return MySqlDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                        return FbDbAccess.IsolationLevel;
                    case "oracle":
                     //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                     //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                }
            }
        }


        // TODO: Move it to Config
        public static string DatabaseOwner
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.DatabaseOwner;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                }
            }
        }

        public static string ObjectQualifier
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.ObjectQualifier;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                }
            }
        }

        /// <summary>
        /// Gets the schema name.
        /// </summary>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static string SchemaName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.SchemaName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;
                }
            }
        }
        public static string DatabaseEncoding
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.DatabaseEncoding;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        public static string GranteeName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.GranteeName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        public static string DBName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.DBName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                }
            }
        }

        public static string HostName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    // return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDbAccess.HostName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDbAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDbAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDbAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDbAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDbAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                }
            }
        }

        public static string WithOIDs
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    return string.Empty;
                    case "Npgsql":
                        return PostgreDbAccess.HostName;
                    case "MySql.Data.MySqlClient":
                        return string.Empty;
                    case "FirebirdSql.Data.FirebirdClient":
                        return string.Empty;
                    case "oracle":
                        return string.Empty;
                    case "db2":
                        return string.Empty;
                    case "other":
                        return string.Empty;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// The get connection manager.
        /// </summary>
        /// <returns>
        /// </returns>
        /* [CanBeNull]
         public IDbConnectionManager GetConnectionManager()
         {
             return Activator.CreateInstance(this._connectionManagerType).ToClass<IDbConnectionManager>();
         /* switch (GetProviderNameFromConnectionString(connectionString))
             {
                 case "System.Data.SqlClient":
                     return string.Empty;
                 case "Npgsql":
                     return PostgreDbAccess.GetConnectionManager();
                 case "MySql.Data.MySqlClient":
                     return string.Empty;
                 case "FirebirdSql.Data.FirebirdClient":
                     return string.Empty;
                 case "oracle":
                     return string.Empty;
                 case "db2":
                     return string.Empty;
                 case "other":
                     return string.Empty;
                 default:
                     throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                     break;

             } 
            
         } */ 
        public void SetConnectionManagerAdapter<TManager>()
     where TManager : IDbConnectionManager
        {
            Type newConnectionManager = typeof(TManager);

            if (typeof(IDbConnectionManager).IsAssignableFrom(newConnectionManager))
            {
                this._connectionManagerType = newConnectionManager;
            }
        }
        /// <summary>
        /// Change the Connection Manager used in all DB operations.
        /// </summary>
        /// <typeparam name="TManager">
        /// </typeparam>
      /*  public void SetConnectionManagerAdapter<TManager>()
          where TManager : IDbConnectionManager
        {
           switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return string.Empty;
                case "Npgsql":
                    return PostgreDbAccess.GetConnectionManager();
                case "MySql.Data.MySqlClient":
                    return string.Empty;
                case "FirebirdSql.Data.FirebirdClient":
                    return string.Empty;
                case "oracle":
                    return string.Empty;
                case "db2":
                    return string.Empty;
                case "other":
                    return string.Empty;
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;

            }
        } */

        /// <summary>
        /// The get connection manager.
        /// </summary>
        /// <returns>
        /// </returns>
        [CanBeNull]
        public IDbConnectionManager GetConnectionManager(int boardOrObject)
        {
            string connectionString;
            string dataEngine;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(boardOrObject, namePattern, out dataEngine, out connectionString);
            this._connectionManagerType = Type.GetType(dataEngine);
            return Activator.CreateInstance(this._connectionManagerType).ToClass<IDbConnectionManager>();

        }

        /// <summary>
        /// The get connection manager.
        /// </summary>
        /// <returns>
        /// </returns>
        [CanBeNull]
        public IDbConnectionManager GetConnectionManager(string connectionString)
        {
            this._connectionManagerType = Type.GetType(GetProviderNameFromConnectionString(connectionString));
            return Activator.CreateInstance(this._connectionManagerType).ToClass<IDbConnectionManager>();
        }

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        public static string GetObjectName(int boardOrObject, string name)
        {

            int boardId = 99;
            string connectionString;
            string dataEngine;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(boardOrObject, namePattern, out dataEngine, out connectionString);

            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetObjectName(name);
                case "Npgsql":
                    return PostgreDbAccess.GetObjectName(name);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.GetObjectName(name);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.GetObjectName(name);
                case "oracle":
                    return string.Empty;
                case "db2":
                    return string.Empty;
                case "other":
                    return string.Empty;
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
            }
        }

        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        public static string GetObjectName(string name)
        {
            return GetObjectName(0, name);
        }

        /// <summary>
        /// The test connection.
        /// </summary>
        /// <param name="exceptionMessage">
        /// The exception message.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool TestConnection([NotNull] out string exceptionMessage, string connectionString)
        {
            int boardId = 1;
            exceptionMessage = string.Empty;
            string dataEngine = GetProviderNameFromConnectionString(connectionString);
           
            // dataEngine = "Npgsql";
            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.TestConnection(connectionString, out exceptionMessage);
                case "Npgsql":
                    return PostgreDbAccess.TestConnection(connectionString, out exceptionMessage);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.TestConnection(connectionString, out exceptionMessage);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.TestConnection(connectionString, out exceptionMessage);
                case "oracle":
                    return false;
                case "db2":
                    return false;
                case "other":
                    return false;
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
            }
        }

        /// <summary>
        /// Gets command text replaced with {databaseOwner} and {objectQualifier}.
        /// </summary>
        /// <param name="commandText">Test to transform.</param>
        /// <returns></returns>
        public static string GetCommandTextReplaced(string commandText, string connectionString)
        {
            int boardId = 99;
              switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                  return MsSqlDbAccess.GetCommandTextReplaced(commandText);
                case "Npgsql":
                    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.GetCommandTextReplaced(commandText);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.GetCommandTextReplaced(commandText);
                case "oracle":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
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
        public DataSet GetDataset([NotNull] IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                return MsSqlDbAccess.GetDataset(cmd,transaction,connectionString);
                case "Npgsql":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "MySql.Data.MySqlClient":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "FirebirdSql.Data.FirebirdClient":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "oracle":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "db2":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "other":
                    throw new ApplicationException("Not implemented for the data layer.");
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;

            }
        }

        /// <summary>
        /// Gets data out of the database
        /// </summary>
        /// <param name="cmd">The SQL Command</param>
        /// <returns>DataTable with the results</returns>
        /// <remarks>Without transaction.</remarks>
        /// <summary>
        /// Gets data out of the database
        /// </summary>
        /// <param name="cmd">The SQL Command</param>
        /// <returns>DataTable with the results</returns>
        /// <remarks>Without transaction.</remarks>
        /// 
        public DataTable GetData(IDbCommand cmd, string connectionString)
        {
            return this.GetDataTableFromReader(cmd, false, true, connectionString);
        }
        public DataTable GetData(IDbCommand cmd, bool transaction, string connectionString)
        {
            return this.GetDataTableFromReader(cmd, transaction, true,  connectionString);
        }
        public DataTable GetData(IDbCommand cmd, bool transaction, bool acceptChanges, string connectionString)
        {
            return this.GetDataTableFromReader(cmd, transaction, acceptChanges,  connectionString);
        }

        /// <summary>
        /// Gets data out of database using a plain text string command
        /// </summary>
        /// <param name="commandText">command text to be executed</param>
        /// <returns>DataTable with results</returns>
        /// <remarks>Without transaction.</remarks>
        public DataTable GetData(string commandText, string connectionString)
        {
            return this.GetData(commandText, false, connectionString);
        }
        public DataTable GetData(string commandText, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetData(commandText, transaction, connectionString);
                case "Npgsql":
                    return PostgreDbAccess.GetData(commandText, transaction, connectionString);
                case "MySql.Data.MySqlClient":
                   return  MySqlDbAccess.Current.GetData(commandText, transaction, connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return  FbDbAccess.Current.GetData(commandText, transaction, connectionString);
                case "oracle":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
            }
        }

        /// <summary>
        /// Executes a NonQuery
        /// </summary>
        /// <param name="cmd">NonQuery to execute</param>
        /// <remarks>Without transaction</remarks>
        public void ExecuteNonQuery(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            this.ExecuteNonQuery(cmd, true, connectionString);
        }

        public void ExecuteNonQuery(IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    MsSqlDbAccess.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "Npgsql":
                    PostgreDbAccess.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "MySql.Data.MySqlClient":
                    MySqlDbAccess.Current.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "FirebirdSql.Data.FirebirdClient":
                    FbDbAccess.Current.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "oracle":
                    //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "db2":
                    //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "other":
                    //    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }
        public int ExecuteNonQueryInt(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            return this.ExecuteNonQueryInt(cmd, true, connectionString);
        }
        public int ExecuteNonQueryInt(IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "Npgsql":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "MySql.Data.MySqlClient":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "FirebirdSql.Data.FirebirdClient":
                    throw new ApplicationException("Not implemented for the data layer.");
                case "oracle":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }

        public object ExecuteScalar(IDbCommand cmd, string connectionString)
        {
            // default to using a transaction for scaler commands
            return this.ExecuteScalar(cmd, true, connectionString);
        }
        public object ExecuteScalar(IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.ExecuteScalar(cmd, transaction, connectionString);
                case "Npgsql":
                    return PostgreDbAccess.ExecuteScalar( cmd, transaction, connectionString);
                case "MySql.Data.MySqlClient":
                  return MySqlDbAccess.Current.ExecuteScalar( cmd, transaction, connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                  return FbDbAccess.Current.ExecuteScalar(cmd, transaction, connectionString);
                case "oracle":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDbAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDbAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }

        //vzrus addons - to make casts workarounds
        /// <summary>
        /// Returns DataTable from DataReader.
        /// </summary>
        /// <param name="cmd">MySql command returning selected values</param>
        /// <param name="dca">Array of DataColumn values. The correspond to select columns names in a query or sp.</param>
        /// <returns></returns>
        public DataTable GetDataTableFromReader(IDbCommand cmd, string connectionString )
        {
            return this.GetDataTableFromReader(cmd, false, true, connectionString);
        }
        public DataTable GetDataTableFromReader(IDbCommand cmd,
              bool transaction, string connectionString)
        {
            return this.GetDataTableFromReader(cmd, transaction, true, connectionString);
        }
        public DataTable GetDataTableFromReader(IDbCommand cmd,
           bool transaction, bool acceptChanges, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                 return MsSqlDbAccess.GetDataTableFromReader(cmd,transaction, acceptChanges,connectionString);
                case "Npgsql":
                    return PostgreDbAccess.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "MySql.Data.MySqlClient":
                 return MySqlDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                  return FbDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "oracle":
                //   return PostgreDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "db2":
                //   return PostgreDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "other":
                //    return PostgreDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }


        }

        public DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, string connectionString)
        {
            return this.AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, 0, connectionString);
        }
        public DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, int currentRow, string connectionString)
        {
            int boardId = 99;
           switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                //  return MsDBAccess.Current.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow);
                case "Npgsql":
                    return PostgreDbAccess.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow, connectionString);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.Current.AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, currentRow, connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.Current.AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, currentRow, connectionString);
               case "oracle":
                //   return OracleDBAccess.Current.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow);
                case "db2":
                //   return Db2DBAccess.Current.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow);
                case "other":
                //    return OtherDBAccess.Current.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }

        #endregion

        /// <summary>
        ///   Gets the Result Filter List.
        /// </summary>
        /// <exception cref = "NotImplementedException">
        /// </exception>
        public IList<IDataTableResultFilter> ResultFilterList
        {
            get
            {
                return this._resultFilterList;
            }
        }
        private static string GetProviderNameFromConnectionString(string connectionString)
        {
            string defaultName = string.Empty;
            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                if (defaultName.IsNotSet())
                {
                    defaultName = cs.ProviderName;
                }
                if (cs.ConnectionString == connectionString )
                {
                    defaultName = cs.ProviderName;
                    break;
                }
            }

            return defaultName;
        }
        private static string GetProviderNameFromConnectionStringName(string connectionStringName)
        {
            string defaultName = string.Empty;
            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                if (defaultName.IsNotSet())
                {
                    defaultName = cs.Name;
                }
                if (cs.Name == connectionStringName)
                {
                    defaultName = cs.ProviderName;
                    break;
                }
            }

            return defaultName;
        }
    }
}
