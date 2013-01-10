/* Copyright (C) 2009 vzrus
 * http://sourceforge.net/yaf-datalayers 
 * PostgreSQL data layers for Yet Another Forum.NET
 * The code structure is based on code for MS SQL Server database for 1.9.3 
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

namespace YAF.Providers.Profile
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web.Profile;

    using NpgsqlTypes;

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
        private static string _connStrAppKeyName = "PgProfileConnectionString";

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
                NpgsqlDbType dbType;
                int size;

                // parse custom provider data...
                this.GetDbTypeAndSizeFromString(property.Attributes["CustomProviderData"].ToString(), out dbType, out size);

                // default the size to 256 if no size is specified
                // default the size to 256 if no size is specified
                if (dbType == NpgsqlDbType.Varchar && size == -1)
                {
                    size = 256;
                }

                this._settingsColumnsList.Add(new SettingsPropertyColumn(property, dbType, size));
            }

            // sync profile table structure with the db...
            DataTable structure = Db.__GetProfileStructure(ConnectionString);

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in this._settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it...
                    Db.__AddProfileColumn(ConnectionString, column.Settings.Name, column.DataType, column.Size);
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
                NpgsqlDbType dbType;
                int size;

                // parse custom provider data...
                this.GetDbTypeAndSizeFromString(value.Property.Attributes ["CustomProviderData"].ToString(), out dbType, out size );

                if (dbType == NpgsqlDbType.Varchar && size == -1)
                {
                    size = 256;
                }

                this._settingsColumnsList.Add( new SettingsPropertyColumn(value.Property, dbType, size));
            }

            // sync profile table structure with the db...
            DataTable structure = Db.__GetProfileStructure(ConnectionString);

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in this._settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it...
                    Db.__AddProfileColumn( ConnectionString, column.Settings.Name, column.DataType, column.Size);
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
        private bool GetDbTypeAndSizeFromString( string providerData, out NpgsqlDbType dbType, out int size )
        {
            size = -1;
            dbType = NpgsqlDbType.Varchar;

            if (string.IsNullOrEmpty(providerData))
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new[] { ';' });
        
            // first item is the column name...
            string columnName = chunk[0];

            // vzrus addon convert values from mssql types...
            if (chunk[1].IndexOf("varchar", StringComparison.Ordinal) >= 0)
            {
                chunk[1] = "Varchar";
            }

            if (chunk[1].IndexOf("int", StringComparison.Ordinal) >= 0)
            {
                chunk[1] = "Integer";
            }

            if (chunk[1].IndexOf("DateTime", StringComparison.Ordinal) >= 0)
            {
                chunk[1] = "Timestamp";
            }

            // get the datatype and ignore case...
            dbType = (NpgsqlDbType)Enum.Parse(typeof(NpgsqlDbType), chunk[1], true);

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
            DataTable allProfilesDt = Db.__GetProfiles(ConnectionString, this.ApplicationName, pageIndex, pageSize, userNameToMatch, inactiveSinceDate);
            
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


