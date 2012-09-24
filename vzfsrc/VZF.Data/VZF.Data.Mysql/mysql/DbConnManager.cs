﻿/* VZF by vzrus
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

using System;
using MySql.Data.MySqlClient;

namespace YAF.Classes.Data
{
  using System.Data;
  
  using YAF.Types;
  using YAF.Types.Handlers;
  using YAF.Types.Interfaces;
    /// <summary>
    /// Provides open/close management for DB Connections
    /// </summary>
    public class MySqlDbConnectionManager : IDisposable 
    {
        #region Constants and Fields
        /// <summary>
        /// The _connection.
        /// </summary>
        protected MySqlConnection _connection = null;
       
        #endregion
        
        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="YafDBConnManager"/> class.
        /// </summary>
        public MySqlDbConnectionManager(string connectionString)
        {
            // just initalize it (not open)
            InitConnection(connectionString);
        }

        protected MySqlDbConnectionManager()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Events

        /// <summary>
        /// The info message.
        /// </summary>
        public event YafDBConnInfoMessageEventHandler InfoMessage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets ConnectionString.
        /// </summary>
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
        ///   Gets the current DB Connection in any state.
        /// </summary>
        public MySqlConnection DBConnection(string connectionString)
        {
         
                InitConnection(connectionString);
                return this._connection;
           
        }



        /// <summary>
        /// Gets an open connection to the DB. Can be called any number of times.
        /// </summary>
        public  MySqlConnection OpenDBConnection(string connectionString)
        {

            InitConnection(connectionString);

                if (this._connection.State != ConnectionState.Open)
                {
                    // open it up...
                    this._connection.Open();
                }

                return this._connection;
           
        }


        #endregion

        #region Implemented Interfaces

        #region IDisposable

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

        #region IYafDBConnManager
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
        /// The init connection.
        /// </summary>
        public void InitConnection(string connectionString)
        {
            if (this._connection == null)
            {
                // create the connection
                this._connection = new MySqlConnection {ConnectionString = connectionString};
                this._connection.InfoMessage += new MySqlInfoMessageEventHandler(Connection_InfoMessage);
             
            }
            else if (this._connection.State != ConnectionState.Open)
            {
                // verify the connection string is in there...
                this._connection.ConnectionString = connectionString;
            }
            if (!this._connection.ConnectionString.ToLower().Contains("allow user variables"))
                this._connection.ConnectionString += ";Allow User Variables=true";
            
            // if (!this._connection.ConnectionString.ToLower().Contains("old guids"))
            // this._connection.ConnectionString += ";old guids=true";

        }

        #endregion

        #endregion

        #region Methods 
        
        /// <summary>
        /// The connection_ info message.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Connection_InfoMessage([NotNull] object sender, [NotNull] MySqlInfoMessageEventArgs e)
        {
            if (this.InfoMessage != null)
            {
                this.InfoMessage(this, new YafDBConnInfoMessageEventArgs(e.errors.ToString()));
            }
        }


    #endregion

       
    }
}