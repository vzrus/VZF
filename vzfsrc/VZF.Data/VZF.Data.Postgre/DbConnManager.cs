/* VZF by vzrus
 * Copyright (C) 2006-2013 Vladimir Zakharov
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

namespace VZF.Data.Postgre
{
    using System;
    using System.Data;
    using System.Globalization;

    using Npgsql;

    using YAF.Classes;
    using YAF.Types.Handlers;

    /// <summary>
  /// Provides open/close management for DB Connections
  /// </summary>
    public class PostgreDbConnectionManager : IDisposable
  {
    /// <summary>
    /// The _connection.
    /// </summary>
    protected NpgsqlConnection _connection = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="YafDBConnManager"/> class.
    /// </summary>
    public PostgreDbConnectionManager()
    {
      // just initalize it (not open)
      this.InitConnection();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="YafDBConnManager"/> class.
    /// </summary>
    public PostgreDbConnectionManager(string connectionString)
    {
        // just initalize it (not open)
        this.InitConnection(connectionString);
    }



    /// <summary>
    /// Gets ConnectionString.
    /// </summary>
    public virtual string ConnectionString
    {
      get
      {
        return Config.ConnectionString;
      }
    }

    /// <summary>
    /// Gets the current DB Connection in any state.
    /// </summary>
    public NpgsqlConnection DBConnection
    {
      get
      {
        this.InitConnection();
        return this._connection;
      }
    }

    /// <summary>
    /// Gets an open connection to the DB. Can be called any number of times.
    /// </summary>
    public NpgsqlConnection OpenDBConnection(string connectionString)
    {

        this.InitConnection(connectionString);

        if (this._connection.State != ConnectionState.Open)
        {
          // open it up...
          this._connection.Open();
        }

        return this._connection;
      
    }

    #region IDisposable Members

    /// <summary>
    /// The dispose.
    /// </summary>
    public virtual void Dispose()
    {
      // close and delete connection
      this.CloseConnection();
      this._connection = null;
    }

    #endregion

    /// <summary>
    /// The info message.
    /// </summary>
    public event YafDBConnInfoMessageEventHandler InfoMessage;

    /// <summary>
    /// The init connection.
    /// </summary>
    public void InitConnection()
    {
      if (this._connection == null)
      {
        // create the connection
        this._connection = new NpgsqlConnection();
        this._connection.Notification += new NotificationEventHandler(this.Connection_InfoMessage);
        this._connection.ConnectionString = this.ConnectionString;
      }
      else if (this._connection.State != ConnectionState.Open)
      {
        // verify the connection string is in there...
        this._connection.ConnectionString = this.ConnectionString;
      }
    }

    /// <summary>
    /// The init connection.
    /// </summary>
    public void InitConnection(string connectionString)
    {
        if (this._connection == null)
        {
            // create the connection
            this._connection = new NpgsqlConnection(connectionString);
            this._connection.Notification += new NotificationEventHandler(this.Connection_InfoMessage);
            this._connection.ConnectionString = this.ConnectionString;
        }
        else if (this._connection.State != ConnectionState.Open)
        {
            // verify the connection string is in there...
            this._connection.ConnectionString = this.ConnectionString;
        }
    }


    /// <summary>
    /// The close connection.
    /// </summary>
    public void CloseConnection()
    {
      if (this._connection != null && this._connection.State != ConnectionState.Closed)
      {
        this._connection.Close();
      }
    }




    /// <summary>
    /// The connection_ info message.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Connection_InfoMessage(object sender, NpgsqlNotificationEventArgs e)
    {
        if (this.InfoMessage != null)
        {
            this.InfoMessage(this, new YafDBConnInfoMessageEventArgs(e.PID.ToString(CultureInfo.InvariantCulture) + ":::" + e.Condition));
        }
    }

  }
}
