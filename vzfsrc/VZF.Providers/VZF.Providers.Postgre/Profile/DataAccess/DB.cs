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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web.Security;

    using Npgsql;

    using NpgsqlTypes;

    using VZF.Data.Postgre;

    using YAF.Classes;
    using YAF.Core;

    public static class YafProfileDBConnManager 
    {
        public static string ConnectionString
        {
            get
            {
                if (YafContext.Application[PgProfileProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[PgProfileProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

    public static class Db
    {
        private static int EncodeProfileData(
            SettingsPropertyValueCollection collection,
            bool isAuthenticated,
            ref string index,
            ref string stringData,
            ref byte[] binaryData)
        {
            bool itemsToSave = false;

            // first we need to determine if there are any items that need saving
            // this is an optimization
            foreach (SettingsPropertyValue value in collection)
            {
                if (!value.IsDirty)
                {
                    continue;
                }

                if (value.Property.Attributes["AllowAnonymous"].Equals(false) && !isAuthenticated)
                {
                    continue;
                }

                itemsToSave = true;
                break;
            }

            if (!itemsToSave)
            {
                return 0;
            }

            StringBuilder indexBuilder = new StringBuilder();
            StringBuilder stringDataBuilder = new StringBuilder();
            MemoryStream binaryBuilder = new MemoryStream();
            int count = 0;

            // ok, we have some values that need to be saved so we go back through
            foreach (SettingsPropertyValue value in collection)
            {
                // if the value has not been written to and is still using the default value
                // no need to save it
                if (value.UsingDefaultValue && !value.IsDirty) continue;

                // we don't save properties that require the user to be authenticated when the
                // current user is not authenticated.
                if (value.Property.Attributes["AllowAnonymous"].Equals(false) && !isAuthenticated)
                {
                    continue;
                }

                count++;
                object propValue = value.SerializedValue;
                if ((value.Deserialized && value.PropertyValue == null) || value.SerializedValue == null)
                {
                    indexBuilder.AppendFormat("{0}//0/-1:", value.Name);
                }
                else if (propValue is string)
                {
                    indexBuilder.AppendFormat(
                        "{0}/0/{1}/{2}:", value.Name, stringDataBuilder.Length, (propValue as string).Length);
                    stringDataBuilder.Append(propValue);
                }
                else
                {
                    byte[] binaryValue = (byte[])propValue;
                    indexBuilder.AppendFormat("{0}/1/{1}/{2}:", value.Name,
                        binaryBuilder.Position, binaryValue.Length);
                    binaryBuilder.Write(binaryValue, 0, binaryValue.Length);
                }
            }
            index = indexBuilder.ToString();
            stringData = stringDataBuilder.ToString();
            binaryData = binaryBuilder.ToArray();
            return count;
        }

        public static  void DecodeProfileData(DataRow profileRow, SettingsPropertyValueCollection values)
        {
            byte[] binaryData = null;
            string indexData = null;
            string stringData = null;
            indexData = profileRow["valueindex"].ToString();
            stringData = profileRow["stringData"].ToString();
            if (profileRow["binaryData"] != DBNull.Value)
                binaryData = (byte[])profileRow["binaryData"];

            if (indexData == null) return;

            string[] indexes = indexData.Split(':');

            foreach (string index in indexes)
            {
                string[] parts = index.Split('/');
                SettingsPropertyValue value = values[parts[0]];
                if (value == null)
                {
                    continue;
                }

                int pos = int.Parse(parts[2], CultureInfo.InvariantCulture);
                int len = int.Parse(parts[3], CultureInfo.InvariantCulture);
                if (len == -1)
                {
                    value.PropertyValue = null;
                    value.IsDirty = false;
                    value.Deserialized = true;
                }
                else if (parts[1].Equals("0"))
                {
                    value.SerializedValue = stringData.Substring(pos, len);
                }
                else
                {
                    var buf = new byte[len];
                    Buffer.BlockCopy(binaryData, pos, buf, 0, len);
                    value.SerializedValue = buf;
                }
            }
        }

        public static DataTable __GetProfiles(string connectionString, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
         {
             using (NpgsqlCommand cmd = PostgreDbAccess.GetCommand("prov_profile_getprofiles"))
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_PageIndex", NpgsqlTypes.NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_PageSize", NpgsqlTypes.NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_UserNameToMatch", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = userNameToMatch;
                cmd.Parameters.Add(new NpgsqlParameter("i_InactiveSinceDate", NpgsqlTypes.NpgsqlDbType.Timestamp)).Value = inactiveSinceDate;

                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                var dt = PostgreDbAccess.GetData(cmd, connectionString);
                return dt;
            }
        }

        /// <summary>
        /// The __ get profile structure.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable __GetProfileStructure(string connectionString)
        {
            string sql = string.Format(@"SELECT DISTINCT * FROM {0} LIMIT 1", PostgreDbAccess.GetObjectName(@"prov_profile"));

            using (var cmd = new NpgsqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

       public static void __AddProfileColumn(string connectionString, string Name, NpgsqlDbType columnType, int size)
        {
            // get column type...
            string type = columnType.ToString();

            if ( size > 0 )
            {
                type += "(" + size.ToString() + ")";
            }
           
            string sql = string.Format( @"ALTER TABLE {0} ADD  {1}  {2} ", PostgreDbAccess.GetObjectName( "prov_profile" ), Name, type );

            using (var cmd = new NpgsqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString);
            }
        } 

        public static object __GetProviderUserKey(string connectionString, object appName, object username )
        {
            if (Config.IsMojoPortal)
            {
                return Membership.GetUser(username.ToString(),true).ProviderUserKey;
            }

            var row = Providers.Membership.Db.__GetUser(
                connectionString, appName.ToString(), null, username.ToString(), false);

            if (row != null)
            {
                return row["UserID"];
            }

            return null;
        }

        public static void __SetProfileProperties(
            string connectionString,
            object appName,
            object userID,
            SettingsPropertyValueCollection values,
            List<SettingsPropertyColumn> settingsColumnsList)
        {
            var sc = new SettingsContext
                         {
                             { "IsAuthenticated", true },
                             { "UserID", userID },
                             { "ApplicationName", appName }
                         };

            __SetPropertyValues(connectionString, sc, values, settingsColumnsList);
         }

         public static  int __DeleteProfiles(string connectionString,  object appName, object userNames )
        {
            using ( NpgsqlCommand cmd = PostgreDbAccess.GetCommand( "prov_profile_deleteprofiles" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_ApplicationName", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_UserNames", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = userNames;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();


                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /// <summary>
        /// The __ delete inactive profiles.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="inactiveSinceDate">
        /// The inactive since date.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static  int __DeleteInactiveProfiles(string connectionString,  object appName, object inactiveSinceDate )
        {
            using ( NpgsqlCommand cmd = PostgreDbAccess.GetCommand( "prov_profile_deleteinactive" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_ApplicationName", NpgsqlDbType.Varchar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new NpgsqlParameter("i_InactiveSinceDate", NpgsqlDbType.Timestamp));
                cmd.Parameters[1].Value = inactiveSinceDate;

                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();


                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

         public static int __GetNumberInactiveProfiles(string connectionString,  object appName, object inactiveSinceDate )
        {
            using ( NpgsqlCommand cmd = PostgreDbAccess.GetCommand( "prov_profile_getnumberinactiveprofiles" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_ApplicationName", NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_InactiveSinceDate", NpgsqlDbType.Timestamp)).Value = inactiveSinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /*
        public  void ValidateAddColumnInProfile( string columnName, NpgsqlTypes.NpgsqlDbType columnType )
        {
            NpgsqlCommand cmd = new NpgsqlCommand( sprocName );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add( "@ApplicationName", appName );
            cmd.Parameters.Add( "@Username", username );
            cmd.Parameters.Add( "@IsUserAnonymous", isAnonymous );

            return cmd;
        }
        */

        public static void __SetPropertyValues(
            string connectionString,
            SettingsContext context,
            SettingsPropertyValueCollection collection,
            List<SettingsPropertyColumn> settingsColumnsList)
        {
            bool isAuthenticated = (bool)context["IsAuthenticated"];
            string appName = (string)context["ApplicationName"];

            // sc.Add("IsAnonymous", isAnonymous);
            // sc.Add("LastActivityDate", lastActivityDate);
            if (context["UserID"] == null)
            {
                return;
            }

            var userid = (Guid)context["UserID"];
            if (collection.Count < 1)
            {
                return;
            }

            string index = string.Empty;
            string stringData = string.Empty;
            byte[] binaryData = null;
            int count = EncodeProfileData(collection, isAuthenticated, ref index, ref stringData, ref binaryData);
            if (count < 1)
            {
                return;
            }

            // save the encoded profile data to the database

             // using (TransactionScope ts = new TransactionScope())
             // {

             // either create a new user or fetch the existing user id
             Guid userId = SchemaManager.CreateOrFetchUserId(userid, isAuthenticated);
             bool profileExists = false;
             using (var cmd1 = PostgreDbAccess.GetCommand(string.Format("SELECT COUNT(1) FROM {0} WHERE userid ='{1}';", PostgreDbAccess.GetObjectName("prov_Profile"), userId), true))
             {
                 profileExists = Convert.ToBoolean(PostgreDbAccess.ExecuteScalar(cmd1,connectionString));
             }

             var mu = Membership.GetUser(userId);

             if (profileExists)
             {
                 using (
                     var cmd =
                         PostgreDbAccess.GetCommand(
                             string.Format(
                                 @"UPDATE {0} SET valueindex = :i_valueindex,stringdata= :i_stringData,binarydata= :i_binaryData,
lastupdateddate= :i_lastupdateddate,lastactivitydate= :i_lastactivitydate,username= :i_username WHERE userid = :i_userid and applicationid = :i_applicationid;",
                                 PostgreDbAccess.GetObjectName("prov_profile")),
                             true))
                 {
                     cmd.Parameters.Add(new NpgsqlParameter("i_valueindex", NpgsqlDbType.Varchar)).Value = index;
                     cmd.Parameters.Add(new NpgsqlParameter("i_stringData", NpgsqlDbType.Varchar)).Value = stringData;
                     cmd.Parameters.Add(new NpgsqlParameter("i_binaryData", NpgsqlDbType.Bytea)).Value = binaryData;
                     cmd.Parameters.Add(new NpgsqlParameter("i_lastupdateddate", NpgsqlDbType.Timestamp)).Value = DateTime.UtcNow;
                     cmd.Parameters.Add(new NpgsqlParameter("i_lastactivitydate", NpgsqlDbType.Timestamp)).Value = mu.LastActivityDate;
                     cmd.Parameters.Add(new NpgsqlParameter("i_applicationid", NpgsqlDbType.Uuid)).Value = (Guid)GetApplicationIdFromName(connectionString,appName);
                     cmd.Parameters.Add(new NpgsqlParameter("i_isanonymous", NpgsqlDbType.Boolean)).Value = false;
                     cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = mu.UserName;
                     cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Uuid)).Value = userId;

                     int res = PostgreDbAccess.ExecuteNonQueryInt(cmd,connectionString);
                     if (res == 0)
                     {
                         // Error
                     }
                 }
             }
             else
             {
                 using (NpgsqlCommand cmd = PostgreDbAccess.GetCommand(
                     string.Format(
                         @"INSERT INTO {0}(
userid,
valueindex,
stringdata,
binarydata,
lastupdateddate,
lastactivitydate,
applicationid,
isanonymous,
username)
VALUES (
:i_userid,
:i_valueindex,
:i_stringData,
:i_binaryData,
:i_lastupdateddate,
:i_lastactivitydate,
:i_applicationid,
:i_isanonymous,
:i_username) ;", PostgreDbAccess.GetObjectName("prov_profile")),
                     true))
                 {
                     cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Uuid)).Value = userId;
                     cmd.Parameters.Add(new NpgsqlParameter("i_valueindex", NpgsqlDbType.Varchar)).Value = index;
                     cmd.Parameters.Add(new NpgsqlParameter("i_stringData", NpgsqlDbType.Varchar)).Value = stringData;
                     cmd.Parameters.Add(new NpgsqlParameter("i_binaryData", NpgsqlDbType.Bytea)).Value = binaryData;
                     cmd.Parameters.Add(new NpgsqlParameter("i_lastupdateddate", NpgsqlDbType.Timestamp)).Value = DateTime.UtcNow;
                     cmd.Parameters.Add(new NpgsqlParameter("i_lastactivitydate", NpgsqlDbType.Timestamp)).Value = mu.LastActivityDate;
                     cmd.Parameters.Add(new NpgsqlParameter("i_applicationid", NpgsqlDbType.Uuid)).Value = GetApplicationIdFromName(connectionString,appName);
                     cmd.Parameters.Add(new NpgsqlParameter("i_isanonymous", NpgsqlDbType.Boolean)).Value = false;
                     cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = mu.UserName;

                     int res = PostgreDbAccess.ExecuteNonQueryInt(cmd,connectionString);
                     if (res == 0)
                     {
                         //Error
                     }
                 }
             }
         }
       
        private static object GetApplicationIdFromName(string connectionString, string appName )
        {
            using ( var cmd = PostgreDbAccess.GetCommand( "prov_createapplication" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value =  Guid.NewGuid();;
                var appId = new NpgsqlParameter("i_applicationid", NpgsqlDbType.Uuid);
                appId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(appId);

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
            
        }
    }
}
