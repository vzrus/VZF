/* VZF by vzrus
 * Copyright (C) 2006-2014 Vladimir Zakharov
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

namespace VZF.Data.Firebird
{
    using System;
    using System.Data;
    using System.IO;
    using System.Security;

    using FirebirdSql.Data.FirebirdClient;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Handlers;

    /// <summary>
    /// Provides open/close management for DB Connections
    /// </summary>
    [SecuritySafeCritical]
    public class FbDbConnectionManager : IDisposable
    {
        /// <summary>
        /// The _connection.
        /// </summary>
        public FbConnection _connection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FbDbConnectionManager"/> class.
        /// </summary>
        public FbDbConnectionManager(string connectionString)
        {
            // just initalize it (not open)
            this.InitConnection(connectionString);
        }

        protected FbDbConnectionManager()
        {
            throw new NotImplementedException();
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
        public FbConnection DBConnection(string connectionString)
        {
            this.InitConnection(connectionString);
            return this._connection;
        }

        /// <summary>
        /// Gets an open connection to the DB. Can be called any number of times.
        /// </summary>
        public FbConnection OpenDBConnection(string connectionString)
        {
            this.InitConnection(connectionString);

            if (this._connection.State == ConnectionState.Open)
            {
                return this._connection;
            }

            string sOriginalDirectory = Directory.GetCurrentDirectory();
                
            // string sApplicationBinPath = (string)System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\bin";
            //    Directory.SetCurrentDirectory(sApplicationBinPath);
            // open it up...
            this._connection.Open();

            // if ((sOriginalDirectory != null) && (sOriginalDirectory.Length > 0))
            // {
            //      Directory.SetCurrentDirectory(sOriginalDirectory);
            // }
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
           // this._connection = null;
        }

        #endregion

        /// <summary>
        ///   The info message.
        /// </summary>
        public event YafDBConnInfoMessageEventHandler InfoMessage;

        /// <summary>
        /// The init connection.
        /// </summary>
        public void InitConnection(string connectionString)
        {
            if (this._connection == null)
            {
                // create the connection
                this._connection = new FbConnection();
                this._connection.InfoMessage += this.Connection_InfoMessage;
                this._connection.ConnectionString = connectionString;
            }
            else if (this._connection.State != ConnectionState.Open)
            {
                // verify the connection string is in there...
                this._connection.ConnectionString = connectionString;
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
        protected void Connection_InfoMessage([NotNull] object sender, [NotNull] FbInfoMessageEventArgs e)
        {
            if (this.InfoMessage != null)
            {
                this.InfoMessage(this, new YafDBConnInfoMessageEventArgs(e.Message));
            }
        }


    }
}
