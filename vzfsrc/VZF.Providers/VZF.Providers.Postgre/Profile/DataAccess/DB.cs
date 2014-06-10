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
    using VZF.Data.Common;
    using VZF.Data.DAL;
    using VZF.Data.Postgre.Mappers;
    using VZF.Data.Utils;

    using YAF.Classes;
  

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
            string indexData = profileRow["valueindex"].ToString();
            string stringData = profileRow["stringData"].ToString();
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

        public static DataTable __GetProfiles(string connectionStringName, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
         {
            using (var sc = new VzfSqlCommand(connectionStringName))
             {
                 sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                 sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                 sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                 sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserNameToMatch", userNameToMatch));
                 sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));
                 sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                 sc.CommandText.AppendObjectQuery("prov_profile_getprofiles", connectionStringName);

                 return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);                
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
        public static DataTable __GetProfileStructure(string connectionStringName)
        {
            return CommonDb.GetProfileStructure(connectionStringName, "prov_profile");
        }

        public static void __AddProfileColumn(string connectionStringName, string name, string type, int size)
        {
            // get column type...
            type = DataTypeMappers.typeToDbValueMap(name, type, size);
          

            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                string sql = String.Format("ALTER TABLE {0} ADD  {1}  {2} ", SqlDbAccess.GetVzfObjectNameFromConnectionString("prov_profile", connectionStringName), name, type);
                sc.CommandText.AppendQuery(sql);
                sc.ExecuteNonQuery(CommandType.Text, false);
            }
        }

        public static object __GetProviderUserKey(string connectionStringName, object appName, object username)
        {
            if (Config.IsMojoPortal)
            {
                var mm = Membership.GetUser(username.ToString());
               
                return Membership.GetUser(username.ToString(),true).ProviderUserKey;
            }

            var row = Providers.Membership.Db.__GetUser(
                connectionStringName, appName.ToString(), null, username.ToString(), false);

            if (row != null)
            {
                return row["UserID"];
            }

            return null;
        }

        public static void __SetProfileProperties(
            string connectionStringName,
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

            __SetPropertyValues(connectionStringName, sc, values, settingsColumnsList);
         }

        public static int __DeleteProfiles(string connectionStringName, object appName, object userNames)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName)); ;
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserNames", userNames));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_profile_deleteprofiles", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
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
        public static int __DeleteInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {           
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_profile_deleteinactive", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }
        }

        public static int __GetNumberInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {            
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_profile_getnumberinactiveprofiles", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
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
            string connectionStringName,
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
    
             string table =
                              SqlDbAccess.GetVzfObjectNameFromConnectionString("prov_profile", connectionStringName);
             using (var sc1 = new VzfSqlCommand(connectionStringName))
             {

                 // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;
                 sc1.CommandText.AppendQuery(String.Format("SELECT COUNT(1) FROM {0} WHERE userid ='{1}';", table, new Guid(userId.ToString())));

                 profileExists = Convert.ToBoolean(sc1.ExecuteScalar(CommandType.Text, false));
                 sc1.CommandText.Clear();
             }

             var mu = Membership.GetUser(userId);

             if (profileExists)
             {
                 using (var sc = new VzfSqlCommand(connectionStringName))
                 {


                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_valueindex", index));
                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_stringdata", stringData));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_binarydata", binaryData));
                     sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastupdateddate", DateTime.UtcNow));
                     sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastactivitydate", mu.LastActivityDate));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_applicationid", (Guid)GetApplicationIdFromName(connectionStringName,appName)));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isanonymous", false));
                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", mu.UserName));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userId", userId));
                     sc.CommandText.AppendQuery(string.Format(
                                 @"UPDATE {0} SET valueindex = :i_valueindex,stringdata= :i_stringData,binarydata= :i_binaryData,
                                                  lastupdateddate= :i_lastupdateddate,lastactivitydate= :i_lastactivitydate,
                                                 username= :i_username WHERE userid = :i_userid and applicationid = :i_applicationid;",
                                 table));
                    int res =  sc.ExecuteNonQuery(CommandType.Text, false);
                      if (res == 0)
                     {
                         // Error
                     }
                 }
             }
             else
             {
                   using (var sc = new VzfSqlCommand(connectionStringName))
                 {

                     sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userId", userId));
                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_valueindex", index));
                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_stringdata", stringData));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_binarydata", binaryData));
                     sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastupdateddate", DateTime.UtcNow));
                     sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastactivitydate", mu.LastActivityDate));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_applicationid", (Guid)GetApplicationIdFromName(connectionStringName,appName)));
                     sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isanonymous", false));
                     sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", mu.UserName));                    
                     sc.CommandText.AppendQuery(string.Format(@"INSERT INTO {0} 
                                                                (userid,valueindex,stringdata,binarydata,lastupdateddate,lastactivitydate,
                                                                applicationid,isanonymous,username)
                                                                VALUES (:i_userid,:i_valueindex,:i_stringData,:i_binaryData,:i_lastupdateddate,
                                                                        :i_lastactivitydate,:i_applicationid,:i_isanonymous,:i_username) ;",
                                                                                                                                           table));
                    int res =  sc.ExecuteNonQuery(CommandType.Text, false);
                      if (res == 0)
                     {
                         // Error
                     }
                 }                
             }
         }

        private static object GetApplicationIdFromName(string connectionStringName, string appName)
        {          
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_applicationid", null, ParameterDirection.Output));
                

                // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;

                sc.CommandText.AppendObjectQuery("prov_createapplication", connectionStringName);
                return sc.ExecuteScalar(CommandType.StoredProcedure, false);
            }            
        }
    }
}
