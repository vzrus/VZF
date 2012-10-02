/* VZF by vzrus
 * Copyright (C) 2006-2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure was primarily based on MS SQL Server code,
 * created by YAF(YetAnotherForum) developers  * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Classes.Data
{
	/// <summary>
	/// Provides open/close management for DB Connections
	/// </summary>
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Collections.Concurrent;
    using System.Reflection;
  
    using YAF.Types;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    using MySql.Data.MySqlClient;

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
        private static IsolationLevel _isolationLevel = System.Data.IsolationLevel.ReadUncommitted;

        /// <summary>
        ///   The _connection manager type.
        /// </summary>
        private static  Type _connectionManagerType = typeof(MySqlDbConnectionManager);

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
        ///   Gets IsolationLevel.
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get
            {
                return _isolationLevel;
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
		static public string GetObjectName( string name )
		{
			return String.Format(
							"`{0}`.`{1}{2}`",
							Config.SchemaName,
							Config.DatabaseObjectQualifier,
							name
							);
		}
        public static string GetConnectionString(
           params object[] conparams )
        {      
            return null;
        }
        public static string GetConnectionString(
            string parm1,
            string parm2,
            string parm3,
            string parm4,
            string parm5,
            string parm6,
            string parm7,
            string parm8,
            string parm9,
            string parm10,
            bool parm11,
            bool parm12,
            bool parm13,
            bool parm14,
            bool parm15,
            bool parm16,
            bool parm17,
            bool parm18,
            bool parm19,
            string userID,
            string userPassword)
        {

            var connBuilder = new MySqlConnectionStringBuilder();

          
            
            // List<MySqlParameter> conParams = new List<MySqlParameter>();
          //  conParams.Add(new MySqlParameter())
            connBuilder.OldGuids = true;
            connBuilder.AllowBatch = true;
            connBuilder.Server = parm1;
            connBuilder.Database = parm2;
            connBuilder.CharacterSet = parm3;            
            connBuilder.Port = Convert.ToUInt32(parm4);
            connBuilder.TreatTinyAsBoolean = true;
            connBuilder.AllowUserVariables = true;
            connBuilder.DefaultCommandTimeout = Convert.ToUInt32(parm5);
            
            connBuilder.CheckParameters = parm13;

            connBuilder.Pooling = parm14;
           
            connBuilder.UseCompression = parm15;
            connBuilder.UseAffectedRows = parm16;           
            connBuilder.PersistSecurityInfo = parm17;
          //  connBuilder.AllowBatch = parm18;

            connBuilder.UserID = userID;
            connBuilder.Password = userPassword;
           

            return connBuilder.ConnectionString;

        }

        public static ConcurrentDictionary<string, Type> GetConnectionParams()
        {
            var myType = (typeof (MySqlConnectionStringBuilder));
            var myPropertyInfo = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Display information for all properties. 
            var cd = new ConcurrentDictionary<string, Type>();
            foreach (var t in myPropertyInfo)
            {
                var dd = t.GetCustomAttributesData();
                if (dd == null || dd.Count <= 0) continue;
                
                foreach (var customAttributeData in dd)
                {
                    try
                    {
                        if (customAttributeData == null || customAttributeData.ConstructorArguments.Count <= 0 ||
                            (customAttributeData.ConstructorArguments[0].Value.ToString() != "Connection" &&
                             customAttributeData.ConstructorArguments[0].Value.ToString() != "Pooling" &&
                             customAttributeData.ConstructorArguments[0].Value.ToString() != "Security" &&
                             customAttributeData.ConstructorArguments[0].Value.ToString() != "Advanced" &&
                             customAttributeData.ConstructorArguments[0].Value.ToString() != "Authentication"))
                            continue;

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
        ///   Gets the Result Filter List.
        /// </summary>
        /// <exception cref = "NotImplementedException">
        /// </exception>
        public static IList<IDataTableResultFilter> ResultFilterList
        {
            get
            {
                return _resultFilterList;
            }
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
		/// Creates new SqlCommand based on command text applying all qualifiers to the name.
		/// </summary>
		/// <param name="commandText">Command text to qualify.</param>
		/// <param name="isText">Determines whether command text is text or stored procedure.</param>
		/// <returns>New SqlCommand</returns>
        static public MySqlCommand GetCommand(string commandText, bool isText)
		{

			return GetCommand(commandText, isText, null );
		}
		 /// <summary>
        /// Creates new MySqlCommand based on command text applying all qualifiers to the name.
        /// </summary>
        /// <param name="commandText">Command text to qualify.</param>
        /// <param name="isText">Determines whether command text is text or stored procedure.</param>
        /// <param name="connection">Connection to use with command.</param>
        /// <returns>New MySqlCommand</returns>

        static public MySqlCommand GetCommand(string commandText, bool isText, MySqlConnection connection)
        {
            if (isText)
            {                

                // commandText = commandText.Replace("{databaseName}", DatabaseOwner);
                commandText = commandText.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);
                commandText = commandText.Replace("{databaseName}", Config.DatabaseScheme);               

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandType = CommandType.Text;                
                cmd.CommandText = commandText;
                cmd.Connection = connection;

                return cmd;
            }
            else
            {
                return GetCommand(commandText);
            }
        }
        /// <summary>
        /// Creates new MySqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">Base of stored procedure name.</param>
        /// <returns>New MySqlCommand</returns>
        static public MySqlCommand GetCommand( string storedProcedure)
        {
            return GetCommand(storedProcedure, null);
        }
        /// <summary>
        /// Creates new MySqlCommand calling stored procedure applying all qualifiers to the name.
        /// </summary>
        /// <param name="storedProcedure">Base of stored procedure name.</param>
        /// <param name="connection">Connection to use with command.</param>
        /// <returns>New MySqlCommand</returns>
        static public MySqlCommand GetCommand(string storedProcedure, MySqlConnection connection)
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
        /// Gets data out of the database
        /// </summary>
        /// <param name="cmd">The SQL Command</param>
        /// <returns>DataTable with the results</returns>
        /// <remarks>Without transaction.</remarks>
        /// 
        public static DataTable GetData(IDbCommand cmd, string connectionString)
        {
            return GetDataTableFromReader(cmd, false, true,  connectionString);
        }
        public static DataTable GetData(IDbCommand cmd, bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true,  connectionString);
        }
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
            return GetData(commandText, false,connectionString);
        }
        public static DataTable GetData(string commandText, bool transaction, string connectionString)
        {
            QueryCounter qc = new QueryCounter(commandText);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
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
                        using (IDbTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
        public static int ExecuteNonQueryInt(IDbCommand cmd, string connectionString)
        {
            // defaults to using a transaction for non-queries
            return ExecuteNonQueryInt(cmd, true, connectionString);
        }
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
                        using (IDbTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
        public static object ExecuteScalar(IDbCommand cmd, string connectionString)
        {
            // default to using a transaction for scaler commands
            return ExecuteScalar(cmd, true,  connectionString);
        }
        public static object ExecuteScalar(IDbCommand cmd, bool transaction, string connectionString)
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
                        // get scalar using a transaction
                        using (IDbTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
        //vzrus addons - to make casts workarounds

        public static DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, string connectionString)
        {
            return AddValuesToDataTableFromReader(cmd, dt, transaction, acceptChanges, firstColumnIndex, 0,  connectionString);
        }
        public static DataTable AddValuesToDataTableFromReader(IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges, int firstColumnIndex, int currentRow, string connectionString)
        {

            var qc = new QueryCounter(cmd.CommandText);
            try
            {

                using (var connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        // get scalar using a transaction
                        using (IDbTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
                                            dt.Rows[currentRow][column] = ReaderValueConverter(column, reader[column.Ordinal - firstColumnIndex]);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            trans.Commit();
                            if (acceptChanges) dt.AcceptChanges();
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
                                        dt.Rows[currentRow][column] = ReaderValueConverter(column, reader[column.Ordinal - firstColumnIndex]);
                                    }
                                }
                            }

                        }
                        reader.Close();
                        if (acceptChanges) dt.AcceptChanges();
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
        /// Returns DataTable from DataReader.
        /// </summary>
        /// <param name="cmd">MySql command returning selected values</param>
        /// <param name="dca">Array of DataColumn values. The correspond to select columns names in a query or sp.</param>
        /// <returns></returns>

        public static DataTable GetDataTableFromReader(IDbCommand cmd, string connectionString)
        {
            return GetDataTableFromReader(cmd, false, true,connectionString);
        }
        public static DataTable GetDataTableFromReader(IDbCommand cmd,
               bool transaction, string connectionString)
        {
            return GetDataTableFromReader(cmd, transaction, true, connectionString);
        }
        public static DataTable GetDataTableFromReader(IDbCommand cmd,
            bool transaction, bool acceptChanges, string connectionString)
        {

            QueryCounter qc = new QueryCounter(cmd.CommandText);
            try
            {
                DataTable dt = new DataTable();

                using (MySqlDbConnectionManager connMan = new MySqlDbConnectionManager(connectionString))
                {
                    // get an open connection
                    cmd.Connection = connMan.OpenDBConnection(connectionString);

                    if (transaction)
                    {
                        using (IDbTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
                            if (acceptChanges) dt.AcceptChanges();
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
                        if (acceptChanges) dt.AcceptChanges();
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
        static private object ReaderValueConverter(DataColumn column, object readerValue)
        {  //It's currently implemented in the methods to make work faster
            //if (column.DataType.Name == "Guid") return GuidConverter((Guid)readerValue);
            return readerValue;
        }
        /// <summary>
        /// Here we get table structure in the most simple way - only type.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        static private DataTable GetTableColumns(IDataReader reader)
        {
            return GetTableColumns(new DataTable(), reader);
        }
        static private DataTable GetTableColumns(DataTable dt, IDataReader reader)
        {
            return GetTableColumns(dt, reader, true); 
        }
        static private DataTable GetTableColumns(DataTable dummyTable, IDataReader reader, bool convertFromUInt64ToInt32)
        {
            DataTable schemaTable = reader.GetSchemaTable();

            foreach (DataRow myField in schemaTable.Rows)
            {

                String ts = myField["DataType"].ToString();
                if ((ts == "System.UInt64" || ts == "System.Int64") && convertFromUInt64ToInt32) ts = "System.Int32";
                if (ts == "System.SByte") ts = "System.Boolean";

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
        static private DataTable TableSchemaReader(DataTable dt, IDataReader reader)
        {
            return TableSchemaReader(dt, reader, true);
        }
        static private DataTable TableSchemaReader(DataTable dt, IDataReader reader, bool convertFromUInt64ToInt32)
        {


            DataTable schemaTable = reader.GetSchemaTable();
            foreach (DataRow myField in schemaTable.Rows)
            {
                foreach (DataColumn myColumn in schemaTable.Columns)
                {
                    if (myColumn.ColumnName == "ColumnName")
                    {
                        String ts = myField["DataType"].ToString();
                        if (ts == "System.UInt64" && convertFromUInt64ToInt32) ts = "System.Int32";

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

        public static Guid GuidConverter(Guid gd)
        {
            var barrIn = gd.ToByteArray();
            var barrOut = new byte[16];

            barrOut[0] = barrIn[3];
            barrOut[1] = barrIn[2];
            barrOut[2] = barrIn[1];
            barrOut[3] = barrIn[0];
            barrOut[4] = barrIn[5];
            barrOut[5] = barrIn[4];

            barrOut[6] = barrIn[7];
            barrOut[7] = barrIn[6];

            for (var i = 8; i < 16; i++)
            {
                barrOut[i] = barrIn[i];
            }
            return new Guid(barrOut);
        }

        #region Methods

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
                    // connectionManager.Open();
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
