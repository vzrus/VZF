#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File PgRoleProvider.cs created  on 2.6.2015 in  6:31 AM.
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

using System.Web;

namespace YAF.Providers.Roles
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Web.Security;

    using VZF.Data.DAL;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Providers.Utils;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The yaf role provider.
    /// </summary>
    public class PgRoleProvider : RoleProvider
    {
        #region Constants and Fields

        /// <summary>
        /// The conn str app key name.
        /// </summary>
        private static string _connStrAppKeyName = "YafRolesConnectionString";

        /// <summary>
        /// The _connection string.
        /// </summary>
        private static string _connectionString;

        /// <summary>
        /// The _app name.
        /// </summary>
        private string _appName;

        /// <summary>
        /// The _conn str name.
        /// </summary>
        private string _connStrName;

        /// <summary>
        /// The _user role cache.
        /// </summary>
        private ConcurrentDictionary<string, StringCollection> _userRoleCache;

        #endregion

        #region Properties

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
        public static string ConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets ApplicationName.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return this._appName;
            }

            set
            {
                if (value != this._appName)
                {
                    this._appName = value;

                    // clear the cache for obvious reasons...
                    this.ClearUserRoleCache();
                }
            }
        }

        /// <summary>
        /// Gets UserRoleCache.
        /// </summary>
        private ConcurrentDictionary<string, StringCollection> UserRoleCache
        {
            get
            {
                string key = this.GenerateCacheKey("UserRoleDictionary");

                return this._userRoleCache
                       ?? (this._userRoleCache =
                           YafContext.Current.Get<IObjectStore>()
                               .GetOrSet(key, () => new ConcurrentDictionary<string, StringCollection>()));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a list of users to a list of groups
        /// </summary>
        /// <param name="usernames">
        /// List of Usernames
        /// </param>
        /// <param name="roleNames">
        /// List of Rolenames
        /// </param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (usernames == null || usernames.Length == 0)
            {
                throw new ArgumentException("usernames is null or empty.", "usernames");
            }

            if (roleNames == null || roleNames.Length == 0)
            {
                throw new ArgumentException("roleNames is null or empty.", "roleNames");
            }

            // Loop through username
            foreach (string username in usernames)
            {
                var allRoles = this.GetAllRoles().ToList();

                // Loop through roles
                foreach (string roleName in roleNames)
                {
                    // only add user if this role actually exists...
                    if (roleName.IsSet() && allRoles.Contains(roleName))
                    {
                        Db.__AddUserToRole(ConnectionStringName, this.ApplicationName, username, roleName);
                    }
                }

                // invalidate the cache for this user...
                this.DeleteFromRoleCacheIfExists(username.ToLower());
            }
        }

        /// <summary>
        /// The create role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        public override void CreateRole(string roleName)
        {
            if (roleName.IsNotSet())
            {
                ExceptionReporter.ThrowArgument("ROLES", "ROLENAMEBLANK");
            }

            Db.__CreateRole(ConnectionStringName, this.ApplicationName, roleName);
        }

        /// <summary>
        /// The delete role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <param name="throwOnPopulatedRole">
        /// The throw on populated role.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            int returnValue = Db.__DeleteRole(
                ConnectionStringName,
                this.ApplicationName,
                roleName,
                throwOnPopulatedRole);

            this.ClearUserRoleCache();

            // zero means there were no complications...
            return returnValue == 0;
        }

        /// <summary>
        /// The find users in role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <param name="usernameToMatch">
        /// The username to match.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (roleName.IsNotSet())
            {
                ExceptionReporter.ThrowArgument("ROLES", "ROLENAMEBLANK");
            }

            // Roles
            DataTable users = Db.__FindUsersInRole(ConnectionStringName, this.ApplicationName, roleName);
            var usernames = new StringCollection();
            foreach (DataRow user in users.Rows)
            {
                usernames.Add(user["Username"].ToStringDBNull());
            }

            return usernames.ToStringArray();
        }

        /// <summary>
        /// The get all roles.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public override string[] GetAllRoles()
        {
            // get all roles...
            DataTable roles = Db.__GetRoles(ConnectionStringName, this.ApplicationName, null);

            // make a string collection to store the role list...
            var roleNames = new StringCollection();

            foreach (DataRow row in roles.Rows)
            {
                roleNames.Add(row["RoleName"].ToStringDBNull());
            }

            return roleNames.ToStringArray(); // return as a string array
        }

        /// <summary>
        /// The get roles for user.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public override string[] GetRolesForUser(string username)
        {
            if (username.IsNotSet())
            {
                ExceptionReporter.ThrowArgument("ROLES", "USERNAMEBLANK");
            }

            StringCollection roleNames;

            // get the users's collection from the dictionary
            if (!this.UserRoleCache.ContainsKey(username.ToLower()))
            {
                roleNames = new StringCollection();

                DataTable roles = Db.__GetRoles(ConnectionStringName, this.ApplicationName, username);

                foreach (DataRow dr in roles.Rows)
                {
                    roleNames.Add(dr["Rolename"].ToStringDBNull()); // add rolename to collection
                }

                // add it to the dictionary cache...
                this.UserRoleCache.AddOrUpdate(username.ToLower(), k => roleNames, (s, v) => roleNames);
            }
            else
            {
                roleNames = this.UserRoleCache[username.ToLower()];
            }

            return roleNames.ToStringArray(); // return as a string array
        }

        /// <summary>
        /// Gets a list of usernames in a a particular role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            if (roleName.IsNotSet())
            {
                ExceptionReporter.ThrowArgument("ROLES", "ROLENAMEBLANK");
            }

            DataTable users = Db.__FindUsersInRole(ConnectionStringName, this.ApplicationName, roleName);

            var userNames = new StringCollection();

            foreach (DataRow dr in users.Rows)
            {
                userNames.Add(dr["Username"].ToStringDBNull());
            }

            return userNames.ToStringArray();
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // verify that the configuration section was properly passed
            if (!config.HasKeys())
            {
                ExceptionReporter.ThrowArgument("ROLES", "CONFIGNOTFOUND");
            }

            // Application Name
            this._appName = config["applicationName"].ToStringDBNull(Config.ApplicationName);

            // Connection String Name
            this._connStrName = config["connectionStringName"].ToStringDBNull(Config.ConnectionStringName);

            // is the connection string set?
            if (this._connStrName.IsSet())
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

            // application name
            this._appName = config["applicationName"];

            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "YetAnotherForum";
            }
        }

        /// <summary>
        /// Check to see if user belongs to a role.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            DataTable roles = Db.__IsUserInRole(ConnectionStringName, this.ApplicationName, username, roleName);

            return roles.Rows.Count > 0;
        }

        /// <summary>
        /// The remove users from roles.
        /// </summary>
        /// <param name="usernames">
        /// The usernames.
        /// </param>
        /// <param name="roleNames">
        /// The role names.
        /// </param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            // Loop through username
            foreach (string username in usernames)
            {
                // Loop through roles
                foreach (string roleName in roleNames)
                {
                    Db.__RemoveUserFromRole(ConnectionStringName, this.ApplicationName, username, roleName);

                    // invalidate cache for this user...
                    this.DeleteFromRoleCacheIfExists(username.ToLower());
                }
            }
        }

        /// <summary>
        /// The role exists.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            // get this role...
            object exists = Db.__GetRoleExists(ConnectionStringName, this.ApplicationName, roleName);

            // if there are any rows then this role exists...
            if (Convert.ToInt32(exists) > 0)
            {
                return true;
            }

            // doesn't exist
            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The clear user role cache.
        /// </summary>
        private void ClearUserRoleCache()
        {
            string key = this.GenerateCacheKey("UserRoleDictionary");
            YafContext.Current.Get<IObjectStore>().Remove(key);
        }

        /// <summary>
        /// The delete from role cache if exists.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        private void DeleteFromRoleCacheIfExists(string key)
        {
            StringCollection collection;
            this.UserRoleCache.TryRemove(key, out collection);
        }

        /// <summary>
        /// The generate cache key.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// Returns a generated cache key.
        /// </returns>
        private string GenerateCacheKey(string name)
        {
            return "PgRoleProvider-{0}-{1}".FormatWith(name, this.ApplicationName);
        }

        #endregion
    }
}