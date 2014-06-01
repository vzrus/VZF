// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="DataSource.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
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
//   The DataSourceInformation.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

    using VZF.Utils;

    using YAF.Classes;

    /// <summary>
    /// The data source.
    /// </summary>
    public class DataSource 
    {
        /// <summary>
        /// The _random.
        /// </summary>
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        /// <summary>
        /// The _name.
        /// </summary>
        private string _name;

        /// <summary>
        /// The _information.
        /// </summary>
        private DataSourceInformation _information;

        /// <summary>
        /// The _command builder.
        /// </summary>
        private DbCommandBuilder _commandBuilder;

        /// <summary>
        /// The _factory.
        /// </summary>
        private DbProviderFactory _factory;

        /// <summary>
        /// The _connection string builder.
        /// </summary>
        private DbConnectionStringBuilder _connectionStringBuilder;

        /// <summary>
        /// The _composite identifier separator pattern.
        /// </summary>
        private char _compositeIdentifierSeparatorPattern = ' ';

        /// <summary>
        /// The _track open connections.
        /// </summary>
        private bool _trackOpenConnections;

        /// <summary>
        /// The _provider name.
        /// </summary>
        private string _providerName;

        /// <summary>
        /// The _connectionError.
        /// </summary>
        private string _connectionError;

        /// <summary>
        /// The _separator.
        /// </summary>
        private string _separator;

        /// <summary>
        /// The _quote suffix.
        /// </summary>
        private string _quoteSuffix;

        /// <summary>
        /// The _quote prefix.
        /// </summary>
        private string _quotePrefix;

        /// <summary>
        /// The _open connections.
        /// </summary>
        private int _openConnections;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public DataSource(string name)
        {
            // this will throw if it doesn't exist
            var css = ConfigurationManager.ConnectionStrings[name];

            this.Initialize(name, css.ConnectionString, css.ProviderName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        public DataSource(string connectionString, string providerName)
        {
            string s = null; 

            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>().Where(cs => cs.ConnectionString.Equals(connectionString)))
            {
                s = cs.Name;
            }

            this.Initialize(s, connectionString, providerName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        public DataSource(string name, string connectionString, string providerName)
        {
            this.Initialize(name, connectionString, providerName);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public string ProviderName
        {
            get { return this._providerName; }
        }

        /// <summary>
        /// Gets the connection error.
        /// </summary>
        public string ConnectionError
        {
            get { return this._connectionError; }
        }

        /// <summary>
        /// Gets the information.
        /// </summary>
        public DataSourceInformation Information
        {
            get { return this._information; }
        }

        /// <summary>
        /// Gets the factory.
        /// </summary>
        public DbProviderFactory Factory
        {
            get { return this._factory; }
        }

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        public DbConnectionStringBuilder ConnectionStringBuilder
        {
            get { return this._connectionStringBuilder; }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionStringBuilder.ConnectionString; }
        }

        /// <summary>
        /// Gets the command builder.
        /// </summary>
        private DbCommandBuilder CommandBuilder
        {
            get
            {
                return this._commandBuilder ?? (this._commandBuilder = this.Factory.CreateCommandBuilder());
            }
        }

        /// <summary>
        /// Gets the composite identifier separator pattern.
        /// </summary>
        private char CompositeIdentifierSeparatorPattern
        {
            get
            {
                if (this._compositeIdentifierSeparatorPattern == ' ')
                {
                    var separator = '.';
                    var s = this._information.CompositeIdentifierSeparatorPattern;
                    if (!string.IsNullOrEmpty(s))
                    {
                        separator = s.Replace("\\", string.Empty)[0];
                    }

                    this._compositeIdentifierSeparatorPattern = separator;
                }

                return this._compositeIdentifierSeparatorPattern;
            }
        }

        /// <summary>
        /// Gets the join separator.
        /// </summary>
        private string JoinSeparator
        {
            get
            {
                if (string.IsNullOrEmpty(this._separator))
                {
                    this._separator = string.Concat(this.QuoteSuffix, this.CompositeIdentifierSeparatorPattern, this.QuotePrefix);
                }

                return this._separator;
            }
        }

        /// <summary>
        /// Gets the quote suffix.
        /// </summary>
        private string QuoteSuffix
        {
            get
            {
                if (string.IsNullOrEmpty(this._quoteSuffix))
                {
                    this._quoteSuffix = this.CommandBuilder.QuoteSuffix;
                    if (string.IsNullOrEmpty(this._quoteSuffix))
                    {
                        this._quoteSuffix = "\"";
                    }

                    this._quoteSuffix = this._quoteSuffix.Trim();
                }

                return this._quoteSuffix;
            }
        }

        /// <summary>
        /// Gets the quote prefix.
        /// </summary>
        private string QuotePrefix
        {
            get
            {
                if (!string.IsNullOrEmpty(this._quotePrefix))
                {
                    return this._quotePrefix;
                }

                this._quotePrefix = this.CommandBuilder.QuotePrefix;
                if (string.IsNullOrEmpty(this._quotePrefix))
                {
                    this._quotePrefix = "\"";
                }

                this._quotePrefix = this._quotePrefix.Trim();

                return this._quotePrefix;
            }
        }

        /// <summary>
        /// The generate new parameter name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GenerateNewParameterName()
        {
            var len = this.Information.ParameterNameMaxLength;
            return this.GenerateNewParameterName(len);
        }

        /// <summary>
        /// The generate new parameter name.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GenerateNewParameterName(int length)
        {
            if (length == 0 || length > 8)
            {
                length = 8;
            }

            var buffer = new byte[length];
            _random.GetBytes(buffer);
            var sb = new StringBuilder();
            var i = 0;
            foreach (var b in buffer)
            {
                var valid = b > 64 && b < 91; // A-Z are valid
                valid |= b > 96 && b < 123;   // a-z are also valid
                if (i > 0)
                {
                    valid |= b > 47 && b < 58;

                    // 0-9 are only valid if not the first char
                }

                // if the byte is a valid char use it,
                // otherwise, use modulo divide and addition
                // to make it an a-z value
                var c = !valid ? (char)((b % 26) + 'a') : (char)b;

                sb.Append(c);
                i++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// The wrap object name.
        /// </summary>
        /// <param name="objectName">
        /// The object name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string WrapObjectName(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
            {
                return objectName;
            }

            objectName = string.Concat(Config.DatabaseObjectQualifier, objectName);

           // if (!this._information.IdentifierCase == IdentifierCase.Sensitive)
          //  {
                // Firebird provider returns dot as a fake separator, composite object names  are not suppported at all.
                // 2 workarounds for existing data layers
                if (this._information.DataSourceProductName.Equals("Firebird"))
                {
                    objectName = objectName.ToUpperInvariant();
                }

                if (this._information.DataSourceProductName.Equals("Npgsql"))
                {
                    objectName = objectName.ToLowerInvariant();
                }
          //  }

            // this._information.IdentifierCase == IdentifierCase.Insensitive;
            if (Config.DatabaseSchemaName.IsSet() && objectName.IndexOf(this.CompositeIdentifierSeparatorPattern) <= 0)
            {
                objectName = string.Concat(
                    Config.DatabaseSchemaName,
                    this.CompositeIdentifierSeparatorPattern,
                    objectName);
            }
           
            // If the object name has come with some db specific quotes< remove them.
            var quoteSuffix = this.QuoteSuffix;
            var quotePrefix = this.QuotePrefix;
            if (objectName.Contains(quotePrefix) || 
                objectName.Contains(quoteSuffix))
            {
                objectName = this.UnwrapObjectName(objectName);
            }


            var ss = objectName.Split(this.CompositeIdentifierSeparatorPattern);
            if (ss.Length > 1)
            {
                objectName = string.Join(this.JoinSeparator, ss);
            }

            objectName = 
                string.Concat(quotePrefix, objectName, quoteSuffix);

            return objectName;
        }

        /// <summary>
        /// The unwrap object name.
        /// </summary>
        /// <param name="objectName">
        /// The object name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string UnwrapObjectName(string objectName)
        {
            if (!string.IsNullOrEmpty(objectName))
            {
                var ss = objectName.Split(this.CompositeIdentifierSeparatorPattern);
                var quotePrefix = this.QuotePrefix;
                var quoteSuffix = this.QuoteSuffix;
                if (ss.Length > 1 && quoteSuffix.Length > 0 && 
                    quotePrefix.Length > 0)
                {
                    var list = new List<string>();
                    foreach (var s in ss)
                    {
                        var tmp = s;
                        var len = tmp.Length;
                        if (len > 2)
                        {
                            if (tmp.Substring(0, 1) == quotePrefix && 
                                tmp.Substring(len - 1, 1) == quoteSuffix)
                            {
                                tmp = tmp.Substring(1, len - 2);
                            }
                        }

                        list.Add(tmp);
                    }

                    list.CopyTo(ss);
                }

                objectName = string.Join(
                      this.CompositeIdentifierSeparatorPattern.ToString(CultureInfo.InvariantCulture), ss);
            }

            return objectName;
        }

        /// <summary>
        /// The get new connection.
        /// </summary>
        /// <returns>
        /// The <see cref="DbConnection"/>.
        /// </returns>
        public DbConnection GetNewConnection()
        {
            // Create factory connection
            var conn = this.Factory.CreateConnection();

            // If creating connection fails
            if (conn == null)
            {
                return null;
            }

            // Setting a connection string from builder
            conn.ConnectionString = this._connectionStringBuilder.ConnectionString;

            if (this._trackOpenConnections)
            {
                // Add connection state change event. It depends on provider. 
                conn.StateChange += this.StateChange;
            }

            conn.Disposed += this.ConnDisposed;
            conn.Open();
            return conn;
        }

        /// <summary>
        /// The get parameter name.
        /// </summary>
        /// <param name="parameterName">
        /// The parameter name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetParameterName(string parameterName)
        {
            var s = parameterName;
            var l = this.Information.ParameterNameMaxLength;
            if (l < 1)
            {
                return this.Information.ParameterMarker;
            }

            if (l < s.Length)
            {
                s = s.Substring(0, l);
            }

            var reg = this.Information.ParameterNamePatternRegex;
            if (!reg.IsMatch(s))
            {
                s = this.GenerateNewParameterName();
            }

            return string.Concat(this.Information.ParameterMarker, s);
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        private void Initialize(string name, string connectionString, string providerName)
        {
            this._name = name;

            this._providerName = providerName;

            // get the provider and then get the Factory Singleton
            this._factory = DbProviderFactories.GetFactory(providerName);

            // if DbConnectionStringBuilder is not implemented in a provider 
            this._connectionStringBuilder = this.Factory.CreateConnectionStringBuilder() ?? 
                                       new DbConnectionStringBuilder(true);

            this._connectionStringBuilder.ConnectionString = connectionString;
          
            using (var conn = this.Factory.CreateConnection())
            {
                if (conn == null)
                {
                    return;
                }

                conn.ConnectionString = this.ConnectionString;

                // state change event for future needs to track connection state changes in future 
                conn.StateChange += this.ConnStateChange;
                conn.Open();
                
                // get data source information
                this._information = new DataSourceInformation(
                    conn.GetSchema(DbMetaDataCollectionNames.DataSourceInformation)); 
            }
        }
       

        /// <summary>
        /// The conn state change.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ConnStateChange(object sender, StateChangeEventArgs e)
        {
            // we are always tracking connection state
            this._trackOpenConnections = true;
        }

        /// <summary>
        /// The conn disposed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ConnDisposed(object sender, EventArgs e)
        {
#if DEBUG
            Debug.WriteLine("Connection was disposed");
#endif
        }

        /// <summary>
        /// The state change.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void StateChange(object sender, StateChangeEventArgs e)
        {
            var connectionState = e.CurrentState;
#if DEBUG
            Debug.WriteLine(Enum.GetName(typeof(ConnectionState), connectionState));
#endif
            switch (connectionState)
            {
                case ConnectionState.Open:
                    Interlocked.Increment(ref this._openConnections);
                    break;
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    Interlocked.Decrement(ref this._openConnections);
                    break;
            }
#if DEBUG
            Debug.WriteLine("Opened connections :" + Interlocked.Add(ref this._openConnections, 0));
#endif
        }
    }
}