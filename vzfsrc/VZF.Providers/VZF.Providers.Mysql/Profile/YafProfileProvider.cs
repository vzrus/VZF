#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File YafProfileProvider.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:12 PM.
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

using System.Web;

namespace YAF.Providers.Profile
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Text;
    using System.Web.Profile;

    using VZF.Data.Common;
    using VZF.Data.DAL;
    using VZF.Data.MySql.Mappers;
    using VZF.Data.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Providers.Utils;
    using YAF.Types.Interfaces;

    /// <summary>
    /// YAF Custom Profile Provider
    /// </summary>
    public class VzfMySqlProfileProvider : ProfileProvider
    {
        #region Constants and Fields

        /// <summary>
        /// The _conn str app key name.
        /// </summary>
        private static string _connStrAppKeyName = "YafProfileConnectionString";

        /// <summary>
        /// The _app name.
        /// </summary>
        private string _appName;

        /// <summary>
        /// The _conn str name.
        /// </summary>
        private string _connStrName;

        /// <summary>
        /// The _properties setup.
        /// </summary>
        private bool _propertiesSetup;

        /// <summary>
        /// The _property lock.
        /// </summary>
        private object _propertyLock = new object();

        /// <summary>
        /// The _settings columns list.
        /// </summary>
        private System.Collections.Generic.List<SettingsPropertyColumn> _settingsColumnsList =
            new System.Collections.Generic.List<SettingsPropertyColumn>();

        #endregion

        #region Properties

        /// <summary>
        /// The _connection string.
        /// </summary>
        private static string _connectionString;

        #region ProviderBase Members(Override Public Properties)

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
                // string ss = ConfigurationManager.ConnectionStrings[ConnStrAppKeyName].ConnectionString;

                //  ProfileSection profileConfig = (ProfileSection)
                //  WebConfigurationManager.GetSection("system.web/profile");
                //  _connectionString = profileConfig.Providers["VzfMySqlProfileProvider"].
                //     ElementInformation.Properties["connectionStringName"].Value.ToString();
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Gets or sets the connection string name.
        /// </summary>
        public static string ConnectionStringName { get; set; }

        /// <summary>
        /// The application name that is stored with each profile. 
        /// The profile provider uses the application name to store 
        /// profile information separately for each application. 
        /// This enables multiple ASP.NET applications to use the same 
        /// data source without a conflict if the same user = name is created 
        /// in different applications. Alternatively, multiple ASP.NET 
        /// applications can share a profile data source by specifying 
        /// the same application name.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return _appName;
            }

            set
            {
                _appName = value;
            }
        }

        /// <summary>
        /// The _user profile cache.
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

                return this._userProfileCache
                       ?? (this._userProfileCache =
                           YafContext.Current.Get<IObjectStore>()
                               .GetOrSet(key, () => new ConcurrentDictionary<string, SettingsPropertyValueCollection>()));
            }
        }

        #endregion

        #endregion

        #region SettingsProvider Members(Overriden Public Methods)

        /// <summary>
        /// Takes as input the name of the provider instance and 
        /// a  NameValueCollection of configuration settings. 
        /// Used to set options and property values for the provider instance, 
        /// including implementation-specific values and options specified 
        /// in the machine configuration or Web.config file.
        /// </summary>
        /// <param name="name">Type: System..::.String
        /// The friendly name of the provider. </param>
        /// <param name="config">Type: System.Collections.Specialized..::.NameValueCollection
        /// A collection of the name/value pairs representing 
        /// the provider-specific attributes specified in the configuration 
        /// for this provider. </param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // verify that the configuration section was properly passed
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Application Name
            this._appName = config["applicationName"].ToStringDBNull(Config.ApplicationName);

            // Connection String Name
            this._connStrName = config["connectionStringName"].ToStringDBNull(Config.ConnectionStringName);

            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "YetAnotherForum";
            }

            // is the connection string set?
            if (!string.IsNullOrEmpty(this._connStrName))
            {
                string connStr = ConfigurationManager.ConnectionStrings[this._connStrName].ConnectionString;

                ConnectionString = connStr;
                ConnectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connStr);

                // set the app variable...
                if (YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] == null)
                {
                    YafContext.Current.Get<HttpApplicationStateBase>().Add(ConnStrAppKeyName, connStr);
                }
                else
                {
                    YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] = connStr;
                }
            }

            base.Initialize(name, config);
        }

        /// <summary>
        /// Takes as input a  SettingsContext and a  SettingsPropertyCollection 
        /// object. The SettingsContext provides information about the user. 
        /// You can use the information as a primary key to retrieve profile 
        /// property information for the user. 
        /// Use the SettingsContext object to get the user name 
        /// and whether the user is authenticated or anonymous.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="collection"></param>
        /// <returns>Return Value Type: System.Configuration..::.SettingsPropertyValueCollection
        /// A SettingsPropertyValueCollection containing the values for 
        /// the specified settings property group. </returns>
        public override SettingsPropertyValueCollection GetPropertyValues(
            SettingsContext context,
            SettingsPropertyCollection collection)
        {
            var settingPropertyCollection = new SettingsPropertyValueCollection();
            if (collection.Count < 1) return settingPropertyCollection;

            // unboxing fits?
            string username = context["UserName"].ToString();

            if (String.IsNullOrEmpty(username))
            {
                return settingPropertyCollection;
            }

            // (if) this provider doesn't support anonymous users
            if (Convert.ToBoolean(context["IsAuthenticated"]) == GeneralProviderSettings.AllowAnonymous)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            // Migration code

            // see if it's cached...
            if (UserProfileCache.ContainsKey(username.ToLower()))
            {
                // just use the cached version...
                return UserProfileCache[username.ToLower()];
            }

            if (Config.GetConfigValueAsBool("YAF.OldProfileProvider", true))
            {
                // load the property collection (sync profile class)
                this.LoadFromPropertyCollection(collection);

                // transfer properties regardless...
                foreach (SettingsProperty prop in collection)
                {
                    settingPropertyCollection.Add(new SettingsPropertyValue(prop));
                }

                // get this profile from the MySQLDB				
                DataTable profileDT = MySQLDB.Current.GetProfiles(
                    ConnectionStringName,
                    this.ApplicationName,
                    0,
                    1,
                    username,
                    null);

                if (profileDT.Rows.Count > 0)
                {
                    DataRow row = profileDT.Rows[0];

                    // load the data into the collection...
                    foreach (SettingsPropertyValue prop in settingPropertyCollection)
                    {
                        object val = row[prop.Name];

                        // Only initialize a SettingsPropertyValue for non-null values
                        if (!(val is DBNull || val == null))
                        {
                            prop.PropertyValue = val;
                            prop.IsDirty = false;
                            prop.Deserialized = true;
                        }
                    }
                }
            }
                //End of old
            else
            {
                foreach (SettingsProperty property in collection)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                    {
                        property.SerializeAs = SettingsSerializeAs.String;
                    }
                    else
                    {
                        property.SerializeAs = SettingsSerializeAs.Xml;
                    }

                    settingPropertyCollection.Add(new SettingsPropertyValue(property));
                }

                // retrieve encoded profile data from the database
                DataTable dt = MySQLDB.Current.GetProfiles(
                    ConnectionStringName,
                    this.ApplicationName,
                    0,
                    1,
                    username,
                    null);

                if (dt.Rows.Count > 0)
                {
                    YAF.Providers.Profile.MySQLDB.UnpackProfileData(dt.Rows[0], settingPropertyCollection);
                }
            }

            // save this collection to the cache
            this.UserProfileCache.AddOrUpdate(
                username.ToLower(),
                (k) => settingPropertyCollection,
                (k, v) => settingPropertyCollection);

            return settingPropertyCollection;
        }

        /// <summary>
        /// Takes as input a  SettingsContext and a  
        /// SettingsPropertyValueCollection object.The SettingsContext 
        /// provides information about the user. You can use the information 
        /// as a primary key to retrieve profile property information for 
        /// the user. Use the SettingsContext object to get the user name 
        /// and whether the user is authenticated or anonymous. 
        /// The SettingsPropertyValueCollection contains a collection 
        /// of SettingsPropertyValue objects. Each SettingsPropertyValue 
        /// object provides the name, type, and value of the property as 
        /// well as additional information such as the default value for
        /// the property and whether the property is read-only. 
        /// The SetPropertyValues method updates the profile property 
        /// values in the data source for the specified user.
        /// Calling the method also updates the LastActivityDate and 
        /// LastUpdatedDate values for the specified user profile to the 
        /// current date and time.
        /// </summary>
        /// <param name="context">Type: System.Configuration..::.SettingsContext
        /// A SettingsContext describing the current application usage. </param>
        /// <param name="collection">Type: System.Configuration..::.SettingsPropertyValueCollection
        /// A SettingsPropertyValueCollection representing the group 
        /// of property settings to set. </param>
        /// <remarks>  ApplicationSettingsBase contains the  Save method, 
        /// which is called to persist the values of all of its settings 
        /// properties. This method enumerates through all the settings 
        /// providers associated with its settings properties, and calls 
        /// the SetPropertyValues method for each  SettingsProvider to 
        /// perform the actual serialization operation.
        /// The SetPropertyValues method should be implemented with 
        /// security in mind:
        /// *   Only fully trusted code should be allowed to update 
        /// application settings.
        /// Partially trusted code should be allowed to update 
        /// only user application settings. 
        /// Untrusted code is not typically allowed to update 
        /// application settings.
        /// *  Usage quotas should be considered to guard against 
        /// resource attacks by partially trusted applications. </remarks>
        public override void SetPropertyValues(
            System.Configuration.SettingsContext context,
            System.Configuration.SettingsPropertyValueCollection collection)
        {
            string username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1) return;

            // this provider doesn't support anonymous users
            if (!Convert.ToBoolean(context["IsAuthenticated"]))
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            bool itemsToSave = false;

            // First make sure we have at least one item to save
            foreach (SettingsPropertyValue pp in collection)
            {
                if (!pp.IsDirty)
                {
                    continue;
                }

                itemsToSave = true;
                break;
            }

            if (!itemsToSave)
            {
                return;
            }

            // load the data for the configuration
            LoadFromPropertyValueCollection(collection);

            object userID = MySQLDB.Current.GetProviderUserKey(ConnectionStringName, this.ApplicationName, username);
            if (userID != null)
            {
                // start saving...
                MySQLDB.Current.SetProfileProperties(
                    ConnectionStringName,
                    this.ApplicationName,
                    userID,
                    collection,
                    _settingsColumnsList);
                // erase from the cache
                DeleteFromProfileCacheIfExists(username.ToLower());
            }
        }

        #endregion


        #region ProfileProvider Members(Overriden Public Methods)

        /// <summary>
        /// The delete profiles.
        /// </summary>
        /// <param name="usernames">
        /// The usernames.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int DeleteProfiles(string[] usernames)
        {
            if (usernames == null || usernames.Length < 1)
            {
                return 0;
            }

            // make single string of usernames...
            StringBuilder userNameBuilder = new StringBuilder();
            bool bFirst = true;

            for (int i = 0; i < usernames.Length; i++)
            {
                string username = usernames[i].Trim();

                if (username.Length > 0)
                {
                    if (!bFirst) userNameBuilder.Append(",");
                    else bFirst = false;
                    userNameBuilder.Append(username);

                    // delete this user from the cache if they are in there...
                    DeleteFromProfileCacheIfExists(username.ToLower());
                }
            }

            // call the MySQLDB...
            return MySQLDB.Current.DeleteProfiles(
                ConnectionStringName,
                this.ApplicationName,
                userNameBuilder.ToString());
        }

        /// <summary>
        /// The delete profiles.
        /// </summary>
        /// <param name="profiles">
        /// The profiles.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            if (profiles == null)
            {
                ExceptionReporter.ThrowArgumentNull("PROFILE", "PROFILESNULL");
            }

            if (profiles.Count < 1)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "PROFILESEMPTY");
            }

            string[] usernames = new string[profiles.Count];

            int index = 0;
            foreach (ProfileInfo profile in profiles)
            {
                usernames[index++] = profile.UserName;
            }

            return DeleteProfiles(usernames);
        }

        /// <summary>
        /// The delete inactive profiles.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="userInactiveSinceDate">
        /// The user inactive since date.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int DeleteInactiveProfiles(
            ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate)
        {
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            // just clear the whole thing...
            ClearUserProfileCache();

            return MySQLDB.Current.DeleteInactiveProfiles(
                ConnectionStringName,
                this.ApplicationName,
                userInactiveSinceDate);
        }

        /// <summary>
        /// The get all profiles.
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
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileInfoCollection"/>.
        /// </returns>
        public override ProfileInfoCollection GetAllProfiles(
            ProfileAuthenticationOption authenticationOption,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            return GetProfileAsCollection(authenticationOption, pageIndex, pageSize, null, null, out totalRecords);
        }

        /// <summary>
        /// The get all inactive profiles.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="userInactiveSinceDate">
        /// The user inactive since date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileInfoCollection"/>.
        /// </returns>
        public override ProfileInfoCollection GetAllInactiveProfiles(
            ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            return GetProfileAsCollection(
                authenticationOption,
                pageIndex,
                pageSize,
                null,
                userInactiveSinceDate,
                out totalRecords);
        }

        /// <summary>
        /// The find profiles by user name.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="usernameToMatch">
        /// The username to match.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileInfoCollection"/>.
        /// </returns>
        public override ProfileInfoCollection FindProfilesByUserName(
            ProfileAuthenticationOption authenticationOption,
            string usernameToMatch,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            return GetProfileAsCollection(
                authenticationOption,
                pageIndex,
                pageSize,
                usernameToMatch,
                null,
                out totalRecords);
        }

        /// <summary>
        /// The find inactive profiles by user name.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="usernameToMatch">
        /// The username to match.
        /// </param>
        /// <param name="userInactiveSinceDate">
        /// The user inactive since date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileInfoCollection"/>.
        /// </returns>
        public override ProfileInfoCollection FindInactiveProfilesByUserName(
            ProfileAuthenticationOption authenticationOption,
            string usernameToMatch,
            DateTime userInactiveSinceDate,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            return GetProfileAsCollection(
                authenticationOption,
                pageIndex,
                pageSize,
                usernameToMatch,
                userInactiveSinceDate,
                out totalRecords);
        }

        /// <summary>
        /// The get number of inactive profiles.
        /// </summary>
        /// <param name="authenticationOption">
        /// The authentication option.
        /// </param>
        /// <param name="userInactiveSinceDate">
        /// The user inactive since date.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetNumberOfInactiveProfiles(
            ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate)
        {
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            return MySQLDB.Current.GetNumberInactiveProfiles(
                ConnectionStringName,
                this.ApplicationName,
                userInactiveSinceDate);
        }

        #endregion


        #region Profile Cache     

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
            return String.Format("VzfMySqlProfileProvider-{0}-{1}", name, this.ApplicationName);
        }

        #endregion

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
        private ProfileInfoCollection GetProfileAsCollection(
            ProfileAuthenticationOption authenticationOption,
            int pageIndex,
            int pageSize,
            object userNameToMatch,
            object inactiveSinceDate,
            out int totalRecords)
        {
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }
            if (pageIndex < 0)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "PAGEINDEXTOOSMALL");
            }
            if (pageSize < 1)
            {
                ExceptionReporter.ThrowArgument("PROFILE", "PAGESIZETOOSMALL");
            }

            // get all the profiles...
            // DataSet allProfilesDS = MySQLDB.GetProfiles(this.ApplicationName, pageIndex, pageSize, userNameToMatch, inactiveSinceDate);

            // create an instance for the profiles...
            ProfileInfoCollection profiles = new ProfileInfoCollection();

            DataTable allProfilesDT = MySQLDB.Current.GetProfiles(
                ConnectionStringName,
                this.ApplicationName,
                pageIndex,
                pageSize,
                userNameToMatch,
                inactiveSinceDate);
            //DataTable profilesCountDT = allProfilesDS.Tables [1];

            foreach (DataRow profileRow in allProfilesDT.Rows)
            {
                string username = profileRow["Username"].ToString();
                DateTime lastActivity = DateTime.SpecifyKind(
                    Convert.ToDateTime(profileRow["LastActivity"]),
                    DateTimeKind.Utc);
                DateTime lastUpdated = DateTime.SpecifyKind(
                    Convert.ToDateTime(profileRow["LastUpdated"]),
                    DateTimeKind.Utc);

                profiles.Add(new ProfileInfo(username, false, lastActivity, lastUpdated, 0));
            }

            // get the first record which is the count...
            //totalRecords = Convert.ToInt32( profilesCountDT.Rows [0] [0] );
            // We get rid of the dataset in the future and added TotalRecords toallProfilesDT as first column
            if (allProfilesDT.Rows.Count > 0) totalRecords = Convert.ToInt32(allProfilesDT.Rows[0][0]);
            else totalRecords = 0;
            return profiles;
        }

        /// <summary>
        /// The load from property collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        protected void LoadFromPropertyCollection(SettingsPropertyCollection collection)
        {
            if (!_propertiesSetup)
            {
                lock (_propertyLock) // clear it out just in case something is still in there...
                    _settingsColumnsList.Clear();

                // validiate all the properties and populate the internal settings collection
                foreach (SettingsProperty property in collection)
                {
                    DbType dbType;
                    int size;

                    // parse custom provider data...
                    this.GetDbTypeAndSizeFromString(
                        property.Attributes["CustomProviderData"].ToString(),
                        out dbType,
                        out size);

                    // default the size to 256 if no size is specified
                    if (dbType == DbType.String && size == -1)
                    {
                        size = 256;
                    }

                    _settingsColumnsList.Add(new SettingsPropertyColumn(property, dbType, size));
                }

                // sync profile table structure with the MySQLDB...
                DataTable structure = MySQLDB.Current.GetProfileStructure(ConnectionStringName);

                // verify all the columns are there...
                foreach (SettingsPropertyColumn column in _settingsColumnsList)
                {
                    // see if this column exists
                    if (!structure.Columns.Contains(column.Settings.Name))
                    {
                        // if not, create it...
                        MySQLDB.Current.AddProfileColumn(
                            ConnectionStringName,
                            column.Settings.Name,
                            column.DataType.ToString(),
                            column.Size);
                    }
                }

                // it's setup now...
                _propertiesSetup = true;
            }
        }

        /// <summary>
        /// The load from property value collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        protected void LoadFromPropertyValueCollection(SettingsPropertyValueCollection collection)
        {
            if (!_propertiesSetup)
            {
                // clear it out just in case something is still in there...
                _settingsColumnsList.Clear();

                // validiate all the properties and populate the internal settings collection
                foreach (SettingsPropertyValue value in collection)
                {
                    DbType dbType;
                    int size;

                    // parse custom provider data...
                    this.GetDbTypeAndSizeFromString(
                        value.Property.Attributes["CustomProviderData"].ToString(),
                        out dbType,
                        out size);

                    // default the size to 256 if no size is specified
                    if (dbType == DbType.String && size == -1)
                    {
                        size = 256;
                    }

                    this._settingsColumnsList.Add(new SettingsPropertyColumn(value.Property, dbType, size));
                }

                // sync profile table structure with the MySQLDB...
                DataTable structure = MySQLDB.Current.GetProfileStructure(ConnectionStringName);

                // verify all the columns are there...
                foreach (SettingsPropertyColumn column in this._settingsColumnsList)
                {
                    // see if this column exists
                    if (!structure.Columns.Contains(column.Settings.Name))
                    {
                        // if not, create it...
                        MySQLDB.Current.AddProfileColumn(
                            ConnectionStringName,
                            column.Settings.Name,
                            column.DataType.ToString(),
                            column.Size);
                    }
                }

                // it's setup now...
                this._propertiesSetup = true;
            }
        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="providerData">
        ///     The provider data.
        /// </param>
        /// <param name="dbType">
        ///     The db type.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void GetDbTypeAndSizeFromString(string providerData, out DbType dbType, out int size)
        {
            size = -1;
            dbType = DbType.String;

            if (string.IsNullOrEmpty(providerData))
            {
                return;
            }

            // split the data
            string[] chunk = providerData.Split(new char[] { ';' });

            // first item is the column name...
            string columnName = chunk[0];
            string paramName = DataTypeMappers.FromDbValueMap(chunk[1]);



            // get the datatype and ignore case...
            dbType = (DbType)Enum.Parse(typeof(DbType), paramName, true);

            if (chunk.Length > 2)
            {
                // handle size...
                if (!Int32.TryParse(chunk[2], out size))
                {
                    throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
                }
            }

            return;
        }
    }
}


