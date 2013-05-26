using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using YAF.Providers.Utils;

namespace YAF.Providers.Profile
{
    public partial class PgProfileProvider
    {
        /// <summary>
        /// The _app name.
        /// </summary>
        private string _appName;

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

        public override SettingsPropertyValueCollection GetPropertyValues(
           SettingsContext context, SettingsPropertyCollection collection)
        {
            var settingPropertyCollection = new SettingsPropertyValueCollection();

            if (collection.Count < 1)
            {
                return settingPropertyCollection;
            }

            string username = context["UserName"].ToString();

            if (string.IsNullOrWhiteSpace(username))
            {
                return settingPropertyCollection;
            }

            // this provider doesn't support anonymous users
            if (!Convert.ToBoolean(context["IsAuthenticated"]))
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            // load the property collection (sync profile class)
            this.LoadFromPropertyCollection(collection);

            // see if it's cached...
            if (this.UserProfileCache.ContainsKey(username.ToLower()))
            {
                // just use the cached version...
                return this.UserProfileCache[username.ToLower()];
            }
            else
            {

                foreach (SettingsProperty property in collection)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                        property.SerializeAs = SettingsSerializeAs.String;
                    else
                        property.SerializeAs = SettingsSerializeAs.Xml;
                    settingPropertyCollection.Add(new SettingsPropertyValue(property));
                }

                // retrieve encoded profile data from the database

                DataTable dt = Db.__GetProfiles(ConnectionString,ApplicationName, 0, 1, username, null);

                if (dt.Rows.Count > 0)
                {
                    YAF.Providers.Profile.Db.DecodeProfileData(dt.Rows[0], settingPropertyCollection);
                    /* foreach (SettingsPropertyValue prop in settingPropertyCollection)
                     {
                         object val = dt.Rows[0][prop.Name];

                         // Only initialize a SettingsPropertyValue for non-null values
                         if ((val is DBNull || val == null)) continue;
                         prop.PropertyValue = val;
                         prop.IsDirty = false;
                         prop.Deserialized = true;
                     } */
                }


                // save this collection to the cache it should be deleted as useless or in other place?

                if (!UserProfileCache.ContainsKey(username.ToLower()))
                {
                    // save this collection to the cache
                    this.UserProfileCache.AddOrUpdate(username.ToLower(), (k) => settingPropertyCollection, (k, v) => settingPropertyCollection);
                }

                return settingPropertyCollection;
            }

        }

        public override void SetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyValueCollection collection)
        {
            string username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;

            // this provider doesn't support anonymous users
            if (!Convert.ToBoolean(context["IsAuthenticated"]))
            {
                ExceptionReporter.ThrowArgument("PROFILE", "NOANONYMOUS");
            }

            bool itemsToSave = false;

            // First make sure we have at least one item to save
            foreach (SettingsPropertyValue pp in collection)
            {
                if (pp.IsDirty)
                {
                    itemsToSave = true;
                    break;
                }
            }

            if (!itemsToSave)
                return;

            // load the data for the configuration
            LoadFromPropertyValueCollection(collection);

            object userID = Db.__GetProviderUserKey(ConnectionString, this.ApplicationName, username);
            if (userID != null)
            {
                // start saving...
                Db.__SetProfileProperties(ConnectionString, this.ApplicationName, userID, collection, _settingsColumnsList);
                // erase from the cache
                DeleteFromProfileCacheIfExists(username.ToLower());
            }
        }

    }
}
