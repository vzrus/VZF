// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="MySqlDbConnectionManager.cs">
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
//   The MySql Db Connection Manager. Based on YetAnotherForum.NET code.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Mysql
{
    using System;
    using System.Data;

    using MySql.Data.MySqlClient;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Handlers;

    /// <summary>
    /// Provides open/close management for DB Connections
    /// </summary>
    public class MySqlDbConnectionManager : IDisposable
    {
        #region Constants and Fields

        /// <summary>
        /// The _connection.
        /// </summary>
        private MySqlConnection _connection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDbConnectionManager"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public MySqlDbConnectionManager(string connectionString)
        {
            // just initalize it (not open)
            this.InitConnection(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDbConnectionManager"/> class.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
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
        ///   Gets the current Db Connection in any state.
        /// </summary>
        public MySqlConnection DbConnection(string connectionString)
        {
            this.InitConnection(connectionString);
            return this._connection;
        }

        /// <summary>
        /// Gets an open connection to the DB. Can be called any number of times.
        /// </summary>
        public MySqlConnection OpenDBConnection(string connectionString)
        {
            this.InitConnection(connectionString);

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
                this._connection = new MySqlConnection { ConnectionString = connectionString };
                this._connection.InfoMessage += new MySqlInfoMessageEventHandler(this.Connection_InfoMessage);
            }
            else if (this._connection.State != ConnectionState.Open)
            {
                // verify the connection string is in there...
                this._connection.ConnectionString = connectionString;
            }

            if (!this._connection.ConnectionString.ToLower().Contains("allow user variables"))
            {
                this._connection.ConnectionString += ";Allow User Variables=true";
            }

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
