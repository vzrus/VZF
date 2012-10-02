using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using YAF.Types;
using YAF.Types.Interfaces;
using YAF.Utils;

namespace YAF.Classes.Data
{
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
                     //   return MsDBAccess.IsolationLevel;
                    case "Npgsql":
                        return PostgreDBAccess.IsolationLevel;
                    case "MySql.Data.MySqlClient":
                       // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                      //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                     //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                     //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }
            }
        }


        // TODO: Move it to Config
        static public string DatabaseOwner
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.DatabaseOwner;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }
            }
        }

        static public string ObjectQualifier
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.ObjectQualifier;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }
            }
        }

        static public string SchemaName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.SchemaName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        static public string DatabaseEncoding
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.DatabaseEncoding;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        static public string GranteeName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.GranteeName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        static public string DBName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.DBName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        static public string HostName
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    //    return MsDBAccess.DatabaseOwner;
                    case "Npgsql":
                        return PostgreDBAccess.HostName;
                    case "MySql.Data.MySqlClient":
                    // return PostgreDBAccess.IsolationLevel;
                    case "FirebirdSql.Data.FirebirdClient":
                    //  return PostgreDBAccess.IsolationLevel;
                    case "oracle":
                    //   return PostgreDBAccess.IsolationLevel;
                    case "db2":
                    //    return PostgreDBAccess.IsolationLevel;
                    case "other":
                    //   return PostgreDBAccess.IsolationLevel;
                    default:
                        throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                        break;

                }

            }
        }
        static public string WithOIDs
        {
            get
            {
                int boardId = 99;
                switch (GetProviderNameFromConnectionString(null))
                {
                    case "System.Data.SqlClient":
                    return string.Empty;
                    case "Npgsql":
                        return PostgreDBAccess.HostName;
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
                     return PostgreDBAccess.GetConnectionManager();
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
                    return PostgreDBAccess.GetConnectionManager();
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
            GetConnectionData(boardOrObject, namePattern, out dataEngine, out connectionString);
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
        static public string GetObjectName(int boardOrObject, string name)
        {

            int boardId = 99;
            string connectionString;
            string dataEngine;
            string namePattern = string.Empty;
            GetConnectionData(boardOrObject, namePattern, out dataEngine, out connectionString);

            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetObjectName(name);
                case "Npgsql":
                    return PostgreDBAccess.GetObjectName(name);
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
                    break;

            }
        }
        /// <summary>
        /// Gets qualified object name
        /// </summary>
        /// <param name="name">Base name of an object</param>
        /// <returns>Returns qualified object name of format {databaseOwner}.{objectQualifier}name</returns>
        static public string GetObjectName(string name)
        {
            return GetObjectName(0, name);
        }
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
          [NotNull] string userPassword,
        string provName)
        {
            int boardId = 99;
            switch (provName)
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetConnectionString(parm1,
                        parm2,
                        parm3,
                        parm4,
                        parm5,
                        parm6,
                        parm7,
                        parm8,
                        parm9,
                        parm10,
                        parm11,
                        parm12,
                        parm13,
                        parm14,
                        parm15,
                        parm16,
                        parm17,
                        parm18,
                        parm19,
                        userId,
                        userPassword);
                case "Npgsql":
                    return PostgreDBAccess.GetConnectionString(parm1,
                        parm2,
                        parm3,
                        parm4,
                        parm5,
                        parm6,
                        parm7,
                        parm8,
                        parm9,
                        parm10,
                        parm11,
                        parm12,
                        parm13,
                        parm14,
                        parm15,
                        parm16,
                        parm17,
                        parm18,
                        parm19,
                        userId,
                        userPassword);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.GetConnectionString(parm1,
                        parm2,
                        parm3,
                        parm4,
                        parm5,
                        parm6,
                        parm7,
                        parm8,
                        parm9,
                        parm10,
                        parm11,
                        parm12,
                        parm13,
                        parm14,
                        parm15,
                        parm16,
                        parm17,
                        parm18,
                        parm19,
                        userId,
                        userPassword);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.GetConnectionString(parm1,
                        parm2,
                        parm3,
                        parm4,
                        parm5,
                        parm6,
                        parm7,
                        parm8,
                        parm9,
                        parm10,
                        parm11,
                        parm12,
                        parm13,
                        parm14,
                        parm15,
                        parm16,
                        parm17,
                        parm18,
                        parm19,
                        userId,
                        userPassword);
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
            return GetConnectionString(parm1, parm2, parm3, parm4, parm5, parm6, parm7, parm8, parm9, parm10, parm11,
                                          parm12, parm13, parm14, parm15, parm16, parm17, parm18, parm19, userId,
                                          userPassword,string.Empty);

            

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
        public static bool TestConnection([NotNull] out string exceptionMessage)
        {
            return TestConnection(out exceptionMessage, null);
        }
    
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
                    return PostgreDBAccess.TestConnection(connectionString, out exceptionMessage);
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
                    break;

            }
        }

        /// <summary>
        /// Creates new NpgsqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <returns>New NpgsqlCommand</returns>
        static public IDbCommand GetCommand(string commandText, bool isText, string connectionString)
        {

            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetCommand(commandText, isText);
                case "Npgsql":
                    return PostgreDBAccess.GetCommand(commandText,isText);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.GetCommand(commandText, isText);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.GetCommand(commandText, isText);
                case "oracle":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;

            }

          //  throw new ApplicationException("The method is not implemented!!! ");
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
                    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "MySql.Data.MySqlClient":
                    return MySqlDbAccess.GetCommandTextReplaced(commandText);
                case "FirebirdSql.Data.FirebirdClient":
                    return FbDbAccess.GetCommandTextReplaced(commandText);
                case "oracle":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;

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
                //  return PostgreDBAccess.GetCommandTextReplaced(commandText)
                case "FirebirdSql.Data.FirebirdClient":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);;
                case "oracle":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
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
            return GetDataTableFromReader(cmd, false, true, connectionString);
        }
        public DataTable GetData(IDbCommand cmd, bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true,  connectionString);
        }
        public DataTable GetData(IDbCommand cmd, bool transaction, bool acceptChanges, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, acceptChanges,  connectionString);
        }

        /// <summary>
        /// Gets data out of database using a plain text string command
        /// </summary>
        /// <param name="commandText">command text to be executed</param>
        /// <returns>DataTable with results</returns>
        /// <remarks>Without transaction.</remarks>
        public DataTable GetData(string commandText, string connectionString)
        {
            return GetData(commandText, false, connectionString);
        }
        public DataTable GetData(string commandText, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.GetData(commandText, transaction, connectionString);
                case "Npgsql":
                    return PostgreDBAccess.GetData(commandText, transaction, connectionString);
                case "MySql.Data.MySqlClient":
                   return  MySqlDbAccess.Current.GetData(commandText, transaction, connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return  FbDbAccess.Current.GetData(commandText, transaction, connectionString);
                case "oracle":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;

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
            ExecuteNonQuery(cmd, true, connectionString);
        }

        public void ExecuteNonQuery(IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    MsSqlDbAccess.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "Npgsql":
                    PostgreDBAccess.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "MySql.Data.MySqlClient":
                    MySqlDbAccess.Current.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "FirebirdSql.Data.FirebirdClient":
                    FbDbAccess.Current.ExecuteNonQuery(cmd, transaction, connectionString); break;
                case "oracle":
                    //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                    //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                    //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }
        public int ExecuteNonQueryInt(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            return ExecuteNonQueryInt(cmd, true, connectionString);
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
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }
        }

        public object ExecuteScalar(IDbCommand cmd, string connectionString)
        {
            // default to using a transaction for scaler commands
            return ExecuteScalar(cmd, true, connectionString);
        }
        public object ExecuteScalar(IDbCommand cmd, bool transaction, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                    return MsSqlDbAccess.ExecuteScalar(cmd, transaction, connectionString);
                case "Npgsql":
                    return PostgreDBAccess.ExecuteScalar( cmd, transaction, connectionString);
                case "MySql.Data.MySqlClient":
                  return MySqlDbAccess.Current.ExecuteScalar( cmd, transaction, connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                  return FbDbAccess.Current.ExecuteScalar(cmd, transaction, connectionString);
                case "oracle":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "db2":
                //   return PostgreDBAccess.GetCommandTextReplaced(commandText);
                case "other":
                //    return PostgreDBAccess.GetCommandTextReplaced(commandText);
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
            return GetDataTableFromReader(cmd, false, true, connectionString);
        }
        public DataTable GetDataTableFromReader(IDbCommand cmd,
              bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true, connectionString);
        }
        public DataTable GetDataTableFromReader(IDbCommand cmd,
           bool transaction, bool acceptChanges, string connectionString)
        {
            int boardId = 99;
            switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                 return MsSqlDbAccess.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "Npgsql":
                    return PostgreDBAccess.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "MySql.Data.MySqlClient":
                 return MySqlDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                  return FbDbAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "oracle":
                //   return PostgreDBAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "db2":
                //   return PostgreDBAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                case "other":
                //    return PostgreDBAccess.Current.GetDataTableFromReader( cmd,transaction, acceptChanges,connectionString);
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", boardId));
                    break;
            }


        }

        public DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, string connectionString)
        {
            return AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, 0, connectionString);
        }
        public DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, int currentRow, string connectionString)
        {
            int boardId = 99;
           switch (GetProviderNameFromConnectionString(connectionString))
            {
                case "System.Data.SqlClient":
                //  return MsDBAccess.Current.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow);
                case "Npgsql":
                    return PostgreDBAccess.AddValuesToDataTableFromReader( cmd,  dt,  transaction,  acceptChanges,  firstColumnIndex,  currentRow, connectionString);
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
