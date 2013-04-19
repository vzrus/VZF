/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2011 Jaben Cargman
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

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;

    using VZF.Data.Utils;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

  /// <summary>
  /// The yaf db access for SQL Server.
  /// </summary>
  public static class MsSqlDbAccess
  {
    #region Constants and Fields

    /// <summary>
    ///   Result filter list
    /// </summary>
    private static readonly IList<IDataTableResultFilter> _resultFilterList = new List<IDataTableResultFilter>();

    /// <summary>
    ///   The _isolation level.
    /// </summary>
    private static IsolationLevel _isolationLevel = IsolationLevel.ReadUncommitted;

    /// <summary>
    ///   The _connection manager type.
    /// </summary>
    private static Type _connectionManagerType = typeof(MsSqlDbConnectionManager);

    #endregion

    #region Properties

      /// <summary>
      /// Gets a value indicating whether large forum tree.
      /// </summary>
      public static bool LargeForumTree 
    {
        get
        {
            return Config.LargeForumTree;
        }
    }

      /// <summary>
      /// Gets the connection parameters.
      /// </summary>
      public static List<ConnectionStringParameter> ConnectionParameters
      {
          get
          {
              var cstr = new SqlConnectionStringBuilder();
              var connectionParametersBuilder = new List<ConnectionStringParameter>();
              bool locals = false;

              if (locals)
              {
                  connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", cstr.DataSource.GetType(), @".\SQLExpress", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("IntegratedSecurity", cstr.IntegratedSecurity.GetType(), "true", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("UserInstance", cstr.UserInstance.GetType(), "True", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("AttachDBFilename", cstr.AttachDBFilename.GetType(), @"|DataDirectory|Database1.mdf", false));
              }
              else
              {
                  connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", cstr.DataSource.GetType(), "(local)", false));
                 
                  // connectionParametersBuilder.Add(new ConnectionStringParameter("DataSource", cstr.DataSource.GetType(), "190.190.200.100,1433", false));

                  //connectionParametersBuilder.Add(new ConnectionStringParameter("NetworkLibrary", cstr.NetworkLibrary.GetType(), "DBMSSOCN", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("InitialCatalog", cstr.InitialCatalog.GetType(), "yafnet", false));
                  //connectionParametersBuilder.Add(new ConnectionStringParameter("IntegratedSecurity", cstr.IntegratedSecurity.GetType(), "True", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("UserID", cstr.UserID.GetType(), "admin", false));
                  connectionParametersBuilder.Add(new ConnectionStringParameter("Password", cstr.Password.GetType(), "password", false));
              }

              connectionParametersBuilder.Add(new ConnectionStringParameter("Pooling", cstr.Pooling.GetType(), "True", false));
              connectionParametersBuilder.Add(new ConnectionStringParameter("ApplicationName", cstr.DataSource.GetType(), string.Empty, false));
             // connectionParametersBuilder.Add(new ConnectionStringParameter("MultipleActiveResultSets", cstr.MultipleActiveResultSets.GetType(), "false", false));
             // connectionParametersBuilder.Add(new ConnectionStringParameter("TrustServerCertificate", cstr.TrustServerCertificate.GetType(), "true", false));

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

      #endregion

    #region Public Methods

    /// <summary>
    /// A method to handle custom scripts execution tags
    /// </summary>
    /// <param name="scriptChunk">
    /// Input string
    /// </param>
    /// <param name="versionSQL">
    /// SQL server version as ushort
    /// </param>
    /// <returns>
    /// Returns an empty string if condition was not and cleanedfrom tags string if was.
    /// </returns>
    [NotNull]
    public static string CleanForSQLServerVersion([NotNull] string scriptChunk, ushort versionSQL)
    {
      if (!scriptChunk.Contains("#IFSRVVER"))
      {
        return scriptChunk;
      }
      else
      {
        int indSign = scriptChunk.IndexOf("#IFSRVVER") + 9;
        string temp = scriptChunk.Substring(indSign);
        int indEnd = temp.IndexOf("#");
        int indEqual = temp.IndexOf("=");
        int indMore = temp.IndexOf(">");

        if (indEqual >= 0 && indEqual < indEnd)
        {
          ushort indVerEnd = Convert.ToUInt16(temp.Substring(indEqual + 1, indEnd - indEqual - 1).Trim());
          if (versionSQL == indVerEnd)
          {
            return scriptChunk.Substring(indEnd + indSign + 1);
          }
        }

        if (indMore >= 0 && indMore < indEnd)
        {
          ushort indVerEnd = Convert.ToUInt16(temp.Substring(indMore + 1, indEnd - indMore - 1).Trim());
          if (versionSQL > indVerEnd)
          {
            return scriptChunk.Substring(indEnd + indSign + 1);
          }
        }

        return String.Empty;
      }
    }

    /// <summary>
    /// Creates new SqlCommand based on command text applying all qualifiers to the name.
    /// </summary>
    /// <param name="commandText">
    /// Command text to qualify.
    /// </param>
    /// <param name="isText">
    /// Determines whether command text is text or stored procedure.
    /// </param>
    /// <returns>
    /// New SqlCommand
    /// </returns>
    public static SqlCommand GetCommand([NotNull] string commandText, bool isText)
    {
      return GetCommand(commandText, isText, null);
    }

    /// <summary>
    /// Creates new SqlCommand based on command text applying all qualifiers to the name.
    /// </summary>
    /// <param name="commandText">
    /// Command text to qualify.
    /// </param>
    /// <param name="isText">
    /// Determines whether command text is text or stored procedure.
    /// </param>
    /// <param name="connection">
    /// Connection to use with command.
    /// </param>
    /// <returns>
    /// New SqlCommand
    /// </returns>
    public static SqlCommand GetCommand([NotNull] string commandText, bool isText, [NotNull] SqlConnection connection)
    {
      return isText
               ? new SqlCommand
                 {
                   CommandType = CommandType.Text, 
                   CommandText = GetCommandTextReplaced(commandText), 
                   Connection = connection
                 }
               : GetCommand(commandText);
    }

    /// <summary>
    /// Creates new SqlCommand calling stored procedure applying all qualifiers to the name.
    /// </summary>
    /// <param name="storedProcedure">
    /// Base of stored procedure name.
    /// </param>
    /// <returns>
    /// New SqlCommand
    /// </returns>
    [NotNull]
    public static SqlCommand GetCommand([NotNull] string storedProcedure)
    {
      return GetCommand(storedProcedure, null);
    }

    /// <summary>
    /// Creates new SqlCommand calling stored procedure applying all qualifiers to the name.
    /// </summary>
    /// <param name="storedProcedure">
    /// Base of stored procedure name.
    /// </param>
    /// <param name="connection">
    /// Connection to use with command.
    /// </param>
    /// <returns>
    /// New SqlCommand
    /// </returns>
    [NotNull]
    public static SqlCommand GetCommand([NotNull] string storedProcedure, [NotNull] SqlConnection connection)
    {
      var cmd = new SqlCommand
        {
          CommandType = CommandType.StoredProcedure, 
          CommandText = GetObjectName(storedProcedure), 
          Connection = connection, 
          CommandTimeout = Int32.Parse(Config.SqlCommandTimeout)
        };

      return cmd;
    }

    /// <summary>
    /// Gets command text replaced with {databaseOwner} and {objectQualifier}.
    /// </summary>
    /// <param name="commandText">
    /// Test to transform.
    /// </param>
    /// <returns>
    /// The get command text replaced.
    /// </returns>
    [NotNull]
    public static string GetCommandTextReplaced([NotNull] string commandText)
    {
      commandText = commandText.Replace("{databaseOwner}", Config.DatabaseOwner);
      commandText = commandText.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);

      return commandText;
    }
   
      /// <summary>
      /// The get connection string.
      /// </summary>
      /// <returns>
      /// The <see cref="string"/>.
      /// </returns>
      public static string GetConnectionString()
    {
        var connBuilder = new SqlConnectionStringBuilder();

        foreach (var parameter in ConnectionParameters)
        {
            connBuilder.Add(parameter.Name, parameter.Value);
        }

        return connBuilder.ConnectionString;
    }

    /// <summary>
    /// Gets qualified object name
    /// </summary>
    /// <param name="name">
    /// Base name of an object
    /// </param>
    /// <returns>
    /// Returns qualified object name of format {databaseOwner}.{objectQualifier}name
    /// </returns>
    public static string GetObjectName([NotNull] string name)
    {
      return "[{0}].[{1}{2}]".FormatWith(Config.DatabaseOwner, Config.DatabaseObjectQualifier, name);
    }



      /* public IDbConnectionManager GetConnectionManager(int boardOrObject)
      {
          throw new NotImplementedException();
      }

      public IDbConnectionManager GetConnectionManager(string connectionString)
      {
          throw new NotImplementedException();
      } */

      /// <summary>
    /// Change the Connection Manager used in all DB operations.
    /// </summary>
    /// <typeparam name="TManager">
    /// </typeparam>
  /*  public void SetConnectionManagerAdapter<TManager>()
      where TManager : IDbConnectionManager
    {
      Type newConnectionManager = typeof(TManager);

      if (typeof(IDbConnectionManager).IsAssignableFrom(newConnectionManager))
      {
        this._connectionManagerType = newConnectionManager;
      }
    } */

    #endregion

    #region Implemented Interfaces

    #region IDbAccess

    public static void ExecuteNonQuery([NotNull] IDbCommand cmd,  string connectionString)
    {
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
    public static void ExecuteNonQuery([NotNull] IDbCommand cmd, bool transaction, string connectionString)
    {
      using (var qc = new QueryCounter(cmd.CommandText))
      {
        using (var connectionManager = new MsSqlDbConnectionManager(connectionString))
        {
          // get an open connection
          cmd.Connection = connectionManager.OpenDBConnection(connectionString);

          Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

          if (transaction)
          {
            // execute using a transaction
            using (var trans = connectionManager.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
    }
    public static object ExecuteScalar([NotNull] IDbCommand cmd, string connectionString)
    {
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
    /// <returns>
    /// The execute scalar.
    /// </returns>
    public static object ExecuteScalar([NotNull] IDbCommand cmd, bool transaction, string connectionString)
    {
      using (var qc = new QueryCounter(cmd.CommandText))
      {
          using (var connectionManager = new MsSqlDbConnectionManager(connectionString))
        {
          // get an open connection
            cmd.Connection = connectionManager.OpenDBConnection(connectionString);

          Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

          if (transaction)
          {
            // get scalar using a transaction
              using (var trans = connectionManager.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
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
    }

    /// <summary>
    /// The get connection manager.
    /// </summary>
    /// <returns>
    /// </returns>
   /* [CanBeNull]
    public static IDbConnectionManager GetConnectionManager()
    {
      return Activator.CreateInstance(this._connectionManagerType).ToClass<IDbConnectionManager>();
    } */

    // vzrus: The methods should not be implemented and used for other data layers compatability.
    public static DataTable AddValuesToDataTableFromReader([NotNull] IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges,
                                             int firstColumnIndex, string connectionString)
    {
        throw new Exception("Not in use for the data layer.");
    }

    public static DataTable AddValuesToDataTableFromReader([NotNull] IDbCommand cmd, DataTable dt, bool transaction, bool acceptChanges,
                                             int firstColumnIndex, int currentRow, string connectionString)
    {
        throw new Exception("Not in use for the data layer.");
    }

    public static DataTable GetDataTableFromReader([NotNull] IDbCommand cmd, bool transaction, bool acceptChanges, string connectionString)
    {
        throw new Exception("Not in use for the data layer.");
    }

    public static DataTable GetData([NotNull] IDbCommand cmd, string connectionString)
    {
        return GetData(cmd, true, connectionString);
    }

    public static DataTable GetList([NotNull] IDbCommand cmd, string connectionString)
    {
        return GetList(cmd, true, connectionString);
    }

    public static DataTable GetList([NotNull] IDbCommand cmd, bool transaction, string connectionString)
    {
        return GetList(cmd, true, connectionString);
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
    /// <returns>
    /// </returns>
    public static DataTable GetData([NotNull] IDbCommand cmd, bool transaction, string connectionString)
    {
      using (var qc = new QueryCounter(cmd.CommandText))
      {
        return ProcessUsingResultFilters(GetDatasetBasic(cmd, transaction, connectionString).Tables[0], cmd.CommandText);
      }
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
    /// <returns>
    /// </returns>
    public static DataTable GetData([NotNull] string commandText, bool transaction, string connectionString)
    {
      using (var qc = new QueryCounter(commandText))
      {
        using (var cmd = new SqlCommand())
        {
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = commandText;
          return ProcessUsingResultFilters(GetDatasetBasic(cmd, transaction, connectionString).Tables[0], commandText);
        }
      }
    }
    [NotNull]
    public static DataSet GetDataset([NotNull] IDbCommand cmd, string connectionString)
    {
        return GetDataset(cmd,  true,  connectionString);
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
        return GetDatasetBasic(cmd, transaction,connectionString);
      }
    }

    #endregion

    #endregion

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
        using (var connectionManager = new MsSqlDbConnectionManager(connectionString))
      {
        // see if an existing connection is present
        if (cmd.Connection == null)
        {
          cmd.Connection = connectionManager.OpenDBConnection(connectionString);
        }
        else if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
        {
          cmd.Connection.Open();
        }

        // create the adapters
        using (var ds = new DataSet())
        {
          using (var da = new SqlDataAdapter())
          {
            da.SelectCommand = (SqlCommand)cmd;
            da.SelectCommand.Connection = (SqlConnection)cmd.Connection;
            Trace.WriteLine(cmd.ToDebugString(), "DbAccess");

            // use a transaction
            if (transaction)
            {
              using (var trans = connectionManager.OpenDBConnection(connectionString).BeginTransaction(_isolationLevel))
              {
                try
                {
                  da.SelectCommand.Transaction = (SqlTransaction)trans;
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
    /// Process a <see cref="DataTable"/> using Result Filters.
    /// </summary>
    /// <param name="dataTable">
    /// data table to process
    /// </param>
    /// <param name="sqlCommand">
    /// </param>
    /// <returns>
    /// </returns>
    private static DataTable ProcessUsingResultFilters([NotNull] DataTable dataTable, [NotNull] string sqlCommand)
    {
      string commandCleaned =
        sqlCommand.Replace("[{0}].[{1}".FormatWith(Config.DatabaseOwner, Config.DatabaseObjectQualifier), String.Empty);

      if (commandCleaned.EndsWith("]"))
      {
        // remove last character
        commandCleaned = commandCleaned.Substring(0, commandCleaned.Length - 1);
      }

      // sort filters and process each one...
      ResultFilterList.OrderBy(x => x.Rank).ToList().ForEach(i => i.Process(ref dataTable, commandCleaned));

      // return possibility modified dataTable
      return dataTable;
    }

    #endregion
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
        exceptionMessage = String.Empty;
        bool success = false;

        try
        {

            using (var connection = new SqlConnection(connectionString))
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
  }
}