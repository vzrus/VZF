/* Yet Another Forum.NET
 * Copyright (C) 2006-2008 Jaben Cargman
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
    using System.Linq;
    using System.Text;

    using VZF.Data.Common;
    using VZF.Data.DAL;
    using VZF.Data.Firebird.Mappers;
    using VZF.Data.Utils;

    using YAF.Classes.Pattern;

    /// <summary>
    /// The fb db.
    /// </summary>
    public class FbDB
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        public static FbDB Current
        {
            get
            {
                return PageSingleton<FbDB>.Instance;
            }
        }

        /// <summary>
        /// The encode profile data.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="isAuthenticated">
        /// The is authenticated.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="stringData">
        /// The string data.
        /// </param>
        /// <param name="binaryData">
        /// The binary data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int EncodeProfileData(
            SettingsPropertyValueCollection collection,
            bool isAuthenticated,
            ref string index,
            ref string stringData,
            ref byte[] binaryData)
        {
            bool itemsToSave = collection.Cast<SettingsPropertyValue>().Where(value => value.IsDirty)
                .Any(value => !value.Property.Attributes["AllowAnonymous"].Equals(false) || isAuthenticated);

            // first we need to determine if there are any items that need saving
            // this is an optimization
            if (!itemsToSave)
            {
                return 0;
            }

            var indexBuilder = new StringBuilder();
            var stringDataBuilder = new StringBuilder();
            var binaryBuilder = new MemoryStream();
            int count = 0;

            // ok, we have some values that need to be saved so we go back through
            foreach (SettingsPropertyValue value in collection)
            {
                // if the value has not been written to and is still using the default value
                // no need to save it
                if (value.UsingDefaultValue && !value.IsDirty)
                {
                    continue;
                }

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
                        "{0}/0/{1}/{2}:",
                        value.Name,
                        stringDataBuilder.Length,
                        (propValue as string).Length);
                    stringDataBuilder.Append(propValue);
                }
                else
                {
                    var binaryValue = (byte[])propValue;
                    indexBuilder.AppendFormat("{0}/1/{1}/{2}:", value.Name, binaryBuilder.Position, binaryValue.Length);
                    binaryBuilder.Write(binaryValue, 0, binaryValue.Length);
                }
            }

            index = indexBuilder.ToString();
            stringData = stringDataBuilder.ToString();
            binaryData = binaryBuilder.ToArray();
            return count;
        }

        /// <summary>
        /// The decode profile data.
        /// </summary>
        /// <param name="profileRow">
        /// The profile row.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        public static void DecodeProfileData(DataRow profileRow, SettingsPropertyValueCollection values)
        {
            byte[] binaryData = null;
            string indexData = null;
            string stringData = null;
            indexData = profileRow["valueindex"].ToString();
            stringData = profileRow["stringData"].ToString();
            if (profileRow["binaryData"] != DBNull.Value)
            {
                binaryData = (byte[])profileRow["binaryData"];
            }
            
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

        /// <summary>
        /// The get profiles.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="appName">
        /// The app name.
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetProfiles(string connectionStringName, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "I_PROFILEAUTHOPTIONS", 1));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAMETOMATCH", userNameToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_INACTIVESINCEDATE", inactiveSinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "I_PAGEINDEX", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "I_PAGESIZE", pageSize));

                sc.CommandText.AppendObjectQuery("P_profile_getprofiles", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetProfileStructure(string connectionStringName)
        {
            return CommonDb.GetProfileStructure(connectionStringName, "P_profile");
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="Name">
        /// The name.
        /// </param>
        /// <param name="columnType">
        /// The column type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        public void AddProfileColumn(string connectionStringName, string Name, string columnType, int size)
        {
            string type = DataTypeMappers.typeToDbValueMap(Name, columnType, size);
            
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.CommandText.AppendQuery(@"ALTER TABLE ");
                sc.CommandText.AppendObjectQuery("P_profile", connectionStringName);
                sc.CommandText.AppendQuery(string.Format(@" ADD {0}  {1};", Name, type));

                sc.ExecuteNonQuery(CommandType.Text, false);
            }
        }

        /// <summary>
        /// The get provider user key.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetProviderUserKey(string connectionStringName, object appName, object username)
        {         
            DataRow row = YAF.Providers.Membership.FbDB.Current.GetUser(connectionStringName, appName.ToString(), null, username.ToString(), false);

            if (row != null)
            {
                return row["UserID"];
            }

            return null;
        }

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        public void SetProfileProperties(string connectionStringName, object appName, object userID, SettingsPropertyValueCollection values, List<SettingsPropertyColumn> settingsColumnsList)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            // EOF 'apply new profile properties'
            if (string.IsNullOrEmpty(userID.ToString()))
            {
                return;
            }

            if (values.Count <= 0)
            {
                return;
            }

            string index = string.Empty;
            string stringData = string.Empty;
            byte[] binaryData = null;
            bool isAuthenticated = true;

            int count = EncodeProfileData(values, isAuthenticated, ref index, ref stringData, ref binaryData);
            if (count < 1)
            {
                return;
            }

            bool profileExists = false;
            using (var sc = new VzfSqlCommand(connectionStringName))
            { 
                // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;
                sc.CommandText.AppendQuery(string.Format(@"SELECT COUNT(1) FROM {0} WHERE USERID =CHAR_TO_UUID('{1}');", SqlDbAccess.GetVzfObjectNameFromConnectionString("P_profile", connectionStringName), userID));
                profileExists = Convert.ToBoolean(sc.ExecuteScalar(CommandType.Text, false));
            }
        
            if (profileExists)
            {
                /* using (FbCommand cmd = FbDbAccess.GetCommand(
                     String.Format(@"UPDATE {0} SET valueindex ='{1}',stringdata='{2}',binarydata='{3}',LASTUPDATEDDATE='{4}' 
                            WHERE USERID =CHAR_TO_UUID('{5}');", YAF.Classes.Data.FbDbAccess.GetObjectName("P_PROFILE"), index, stringData, binaryData, DateTime.UtcNow, userID), true))
                { */

                using (var sc = new VzfSqlCommand(connectionStringName))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_valueindex", index));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_stringdata", stringData));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_binarydata", binaryData));
                    sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "current_utctimestamp", DateTime.Now));

                    // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;
                    sc.CommandText.AppendQuery(
                        string.Format(
                            @"UPDATE {0} SET valueindex = ?,stringdata=?,binarydata=?,LASTUPDATEDDATE=? 
                            WHERE USERID =CHAR_TO_UUID('{1}');",
                            SqlDbAccess.GetVzfObjectNameFromConnectionString("P_profile", connectionStringName),
                            userID));
                    sc.ExecuteNonQuery(CommandType.Text, false);
                }
            }
            else
            {
                using (var sc = new VzfSqlCommand(connectionStringName))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERID", userID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_valueindex", index));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_stringdata", stringData));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_binarydata", binaryData));
                    sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "current_utctimestamp", DateTime.Now));

                    // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;
                    sc.CommandText.AppendQuery(
                        string.Format(
                            @"INSERT INTO {0}(USERID,valueindex,stringdata,binarydata,LASTUPDATEDDATE) 
                       VALUES(CHAR_TO_UUID(@I_USERID), @I_valueindex, @I_stringdata, @I_binarydata,@current_utctimestamp);",
                            SqlDbAccess.GetVzfObjectNameFromConnectionString("P_profile", connectionStringName)));
                    sc.ExecuteNonQuery(CommandType.Text, false);
                }
            }
        }

        /// <summary>
        /// The delete profiles.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userNames">
        /// The user names.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int DeleteProfiles(string connectionStringName, object appName, object userNames)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            int deleted = 0;
            char[] sep = new[] { ',' };
            string[] userNamesArr = userNames.ToString().Split(sep[0]);
            for (int i = 0; i <= userNamesArr.Length; i++)
            {
                using (var sc = new VzfSqlCommand(connectionStringName))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName)); 
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userNamesArr[i]));

                    sc.CommandText.AppendObjectQuery("P_profile_deleteprofile", connectionStringName);
                    deleted += Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
                }
            }

            return deleted;
        }

        /// <summary>
        /// The delete inactive profiles.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
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
        public int DeleteInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_INACTIVESINCEDATE", inactiveSinceDate));

                sc.CommandText.AppendObjectQuery("P_profile_deleteinactive", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }
        }

        /// <summary>
        /// The get number inactive profiles.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
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
        public int GetNumberInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {
             // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_INACTIVESINCEDATE", inactiveSinceDate));

                sc.CommandText.AppendObjectQuery("P_PROFILE_GETNUMINACT", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }
        }

        /*
        public  void ValidateAddColumnInProfile( string columnName, NpgsqlTypes.NpgsqlDbType columnType )
        {
            FbCommand cmd = new FbCommand( sprocName );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add( "@ApplicationName", appName );
            cmd.Parameters.Add( "@Username", username );
            cmd.Parameters.Add( "@IsUserAnonymous", isAnonymous );

            return cmd;
        }
        */
    }
}
