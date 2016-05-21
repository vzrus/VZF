#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File profileprovider.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:13 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

namespace YAF.Providers.Profile
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.Profile;
    using VZF.Data.Common;
    using VZF.Data.Postgre.Mappers;
    using VZF.Data.Utils;

    using YAF.Core;
    using YAF.Providers.Utils;
    using YAF.Types.Interfaces;

    /// <summary>
    ///  YAF Custom Profile Provider
    /// </summary>
    public partial class PgProfileProvider : ProfileProvider
    {
       #region Constants and Fields

        /// <summary>
        /// The conn str app key name.
        /// </summary>
        private static string _connStrAppKeyName = "YafProfileConnectionString";

        /// <summary>
        /// The _connection string.
        /// </summary>
        private static string _connectionString;

        /// <summary>
        /// The _conn str name.
        /// </summary>
        private string _connStrName;

        /// <summary>
        /// The _properties setup.
        /// </summary>
        private bool _propertiesSetup = false;

        /// <summary>
        /// The _property lock.
        /// </summary>
        private object _propertyLock = new object();

        /// <summary>
        /// The _settings columns list.
        /// </summary>
        private List<SettingsPropertyColumn> _settingsColumnsList = new List<SettingsPropertyColumn>();

        #endregion

        #region Override Public Properties
        /// <summary>
        /// Gets the Connection String App Key Name.
        /// </summary>
        public static string ConnStrAppKeyName
        {
            get
            {
                return _connStrAppKeyName;
            }
        }

        /// <summary>
        /// Gets the Connection String App Key Name.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }
      
        /// <summary>
        /// Gets the Connection String App Key Name.
        /// </summary>
        public static string ConnectionStringName
        {
            get;
            set;
        }
       
        #endregion
    
        #region Common Properties

        /// <summary>
        ///  Gets UserProfileCache.
        /// </summary>  
        private ConcurrentDictionary<string, SettingsPropertyValueCollection> _userProfileCache = null;

        /// <summary>
        /// Gets UserProfileCache.
        /// </summary>
        private ConcurrentDictionary<string, SettingsPropertyValueCollection> UserProfileCache
        {
            get
            {
                string key = this.GenerateCacheKey("UserProfileDictionary");

                return this._userProfileCache ??
                       (this._userProfileCache =
                        YafContext.Current.Get<IObjectStore>().GetOrSet(
                          key, () => new ConcurrentDictionary<string, SettingsPropertyValueCollection>()));
            }
        }
    
        #endregion   
 
        /// <summary>
        /// The delete from profile cache if exists.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        private void DeleteFromProfileCacheIfExists(string key)
        {
            SettingsPropertyValueCollection collection;

            this.UserProfileCache.TryRemove(key, out collection);
        }

        /// <summary>
        /// The clear user profile cache.
        /// </summary>
        private void ClearUserProfileCache()
        {
            YafContext.Current.Get<IObjectStore>().Remove(this.GenerateCacheKey("UserProfileDictionary"));
        }

        /// <summary>
        /// The generate cache key.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateCacheKey(string name)
        {
            return string.Format("PgProfileProvider-{0}-{1}", name, this.ApplicationName);
        }

        #region Overriden Public Methods

        /// <summary>
        /// The load from property collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        protected void LoadFromPropertyCollection(SettingsPropertyCollection collection)
        {
            if (this._propertiesSetup)
            {
                return;
            }

            lock (this._propertyLock)
            {
                // clear it out just in case something is still in there...
                this._settingsColumnsList.Clear();
            }

            // validiate all the properties and populate the internal settings collection
            foreach (SettingsProperty property in collection)
            {
                DbType dbType;
                int size;

                // parse custom provider data...
                this.GetDbTypeAndSizeFromString(property.Attributes["CustomProviderData"].ToString(), out dbType, out size);

                // default the size to 256 if no size is specified
                // default the size to 256 if no size is specified
                if (dbType == DbType.String && size == -1)
                {
                    size = 256;
                }

                this._settingsColumnsList.Add(new SettingsPropertyColumn(property, dbType, size));
            }

            // sync profile table structure with the db...
            DataTable structure = Db.__GetProfileStructure(ConnectionStringName);

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in this._settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it...
                    Db.__AddProfileColumn(ConnectionStringName, column.Settings.Name, column.DataType.ToString(), column.Size);
                }
            }

            // it's setup now...
            this._propertiesSetup = true;
        }

        /// <summary>
        /// The load from property value collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        protected void LoadFromPropertyValueCollection(SettingsPropertyValueCollection collection)
        {
            if (_propertiesSetup)
            {
                return;
            }

            // clear it out just in case something is still in there...
            this._settingsColumnsList.Clear();

            // validiate all the properties and populate the internal settings collection
            foreach (SettingsPropertyValue value in collection)
            {
                DbType dbType;
                int size;

                // parse custom provider data...

                this.GetDbTypeAndSizeFromString(value.Property.Attributes ["CustomProviderData"].ToString(), out dbType, out size );

                if (dbType == DbType.String && size == -1)
                {
                    size = 256;
                }

                this._settingsColumnsList.Add( new SettingsPropertyColumn(value.Property, dbType, size));
            }

            // sync profile table structure with the db...
            DataTable structure = Db.__GetProfileStructure(ConnectionStringName);

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in this._settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it...
                    Db.__AddProfileColumn( ConnectionStringName, column.Settings.Name, column.DataType.ToString(), column.Size);
                }
            }

            // it's setup now...
            this._propertiesSetup = true;
        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="providerData">
        /// The provider data.
        /// </param>
        /// <param name="dbType">
        /// The db type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private bool GetDbTypeAndSizeFromString( string providerData, out DbType dbType, out int size )
        {
            size = -1;
            dbType = DbType.String;

            if (string.IsNullOrEmpty(providerData))
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new[] { ';' });
        
            // first item is the column name...
            string paramName = DataTypeMappers.FromDbValueMap(chunk[1]);
           
            // get the datatype and ignore case...
            dbType = (DbType)Enum.Parse(typeof(DbType), paramName, true);

            if (chunk.Length > 2)
            {
                // handle size...
                if (!int.TryParse(chunk[2], out size))
                {
                    throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
                }
            }

            return true;
        }

        /// <summary>
        /// The get profile as collection.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="userNameToMatch">
        /// The user name to match.
        /// </param>
        /// <param name="inactiveSinceDate">
        /// The inactive since date.
        /// </param>
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileInfoCollection"/>.
        /// </returns>
        private ProfileInfoCollection GetProfileAsCollection( ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, object userNameToMatch, object inactiveSinceDate, out int totalRecords )
        {
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            if (pageIndex < 0)
            {
                ExceptionReporter.ThrowArgument( "PROFILE", "PAGEINDEXTOOSMALL");
            }

            if (pageSize < 1)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "PAGESIZETOOSMALL");
            }

            // get all the profiles...
            // DataSet allProfilesDS = Db.GetProfiles( this.ApplicationName, pageIndex, pageSize, userNameToMatch, inactiveSinceDate );

            // create an instance for the profiles...
            var profiles = new ProfileInfoCollection();
            DataTable allProfilesDt = Db.__GetProfiles(ConnectionStringName, this.ApplicationName, pageIndex, pageSize, userNameToMatch, inactiveSinceDate);
            
            // DataTable allProfilesDT = allProfilesDS.Tables [0];
            // DataTable profilesCountDT = allProfilesDS.Tables [1];
            foreach (DataRow profileRow in allProfilesDt.Rows)
            {
                string username = profileRow["Username"].ToString();
                var lastActivity = DateTime.SpecifyKind(Convert.ToDateTime(profileRow["LastActivity"]), DateTimeKind.Utc);
                var lastUpdated = DateTime.SpecifyKind(Convert.ToDateTime(profileRow["LastUpdated"]), DateTimeKind.Utc);

                profiles.Add(new ProfileInfo(username, false, lastActivity, lastUpdated, 0));
            }

            // get the first record which is the count...
            totalRecords = Convert.ToInt32(allProfilesDt.Rows[0]["TotalCount"]);

            return profiles;
        }

        #endregion
    }
}


