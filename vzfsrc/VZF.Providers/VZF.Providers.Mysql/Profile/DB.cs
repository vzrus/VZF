#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DB.cs created  on 2.6.2015 in  6:31 AM.
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

namespace YAF.Providers.Profile
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using VZF.Data.Common;
    using VZF.Data.DAL;
    using VZF.Data.MySql.Mappers;
    using VZF.Data.Utils;

    using YAF.Classes;
    using YAF.Classes.Pattern;

    public class MySQLDB
    {
        public static MySQLDB Current
        {
            get
            {
                return PageSingleton<MySQLDB>.Instance;
            }
        }

        #region Utilities
        private static int PackProfileData(SettingsPropertyValueCollection collection, bool isAuthenticated,
ref string index, ref string stringData, ref byte[] binaryData)
        {
            bool itemsToSave = false;

            // first we need to determine if there are any items that need saving
            // this is an optimization
            foreach (SettingsPropertyValue value in collection)
            {
                if (!value.IsDirty) continue;
                if (value.Property.Attributes["AllowAnonymous"].Equals(false) &&
                    !isAuthenticated) continue;
                itemsToSave = true;
                break;
            }
            if (!itemsToSave) return 0;

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
                if (value.Property.Attributes["AllowAnonymous"].Equals(false) &&
                    !isAuthenticated) continue;

                count++;
                object propValue = value.SerializedValue;
                if ((value.Deserialized && value.PropertyValue == null) ||
                    value.SerializedValue == null)
                    indexBuilder.AppendFormat("{0}//0/-1:", value.Name);
                else if (propValue is string)
                {
                    indexBuilder.AppendFormat("{0}/0/{1}/{2}:", value.Name,
                        stringDataBuilder.Length, (propValue as string).Length);
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

        public static void UnpackProfileData(DataRow profileRow, SettingsPropertyValueCollection values)
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
                    byte[] buf = new byte[len];
                    Buffer.BlockCopy(binaryData, pos, buf, 0, len);
                    value.SerializedValue = buf;
                }
            }
        }
        #endregion

        public DataTable GetProfiles(string connectionStringName, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
        {
            int TotalCountNew = 0;

            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserNameToMatch", userNameToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TotalCount", TotalCountNew, ParameterDirection.Output));

                sc.CommandText.AppendObjectQuery("prov_profile_getprofiles", connectionStringName);
               
                var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
                TotalCountNew = (int)sc.Parameters["i_TotalCount"].Value;
                if (dt.Rows.Count > 0) dt.Rows[0]["TotalCount"] = TotalCountNew;
                return dt;
            }
        }

        public DataTable GetProfileStructure(string connectionStringName)
        {
            return CommonDb.GetProfileStructure(connectionStringName, "prov_Profile");
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        public void AddProfileColumn(string connectionStringName, string name, string type, int size)
        {
           type = DataTypeMappers.typeToDbValueMap(name, type, size);

            if (type.ToLowerInvariant().Contains("varchar") && Config.DatabaseEncoding != null)
            {
                type += " CHARACTER SET " + Config.DatabaseEncoding;

                if (Config.DatabaseCollation != null)
                {
                    type += " COLLATE " + Config.DatabaseEncoding + "_" + Config.DatabaseCollation;
                }
            }

            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                string sql = string.Format("ALTER TABLE {0} ADD `{1}` {2};", SqlDbAccess.GetVzfObjectNameFromConnectionString("prov_Profile", connectionStringName), name, type);
                sc.CommandText.AppendQuery(sql);
                sc.ExecuteNonQuery(CommandType.Text, false);
            }
        }

        public object GetProviderUserKey(string ConnectionStringName, object appName, object username)
        {
            DataRow row = YAF.Providers.Membership.MySQLDB.Current.GetUser(ConnectionStringName, appName.ToString(), null, username.ToString(), false);

            if (row != null)
            {
                return row["UserID"];
            }

            return null;
        }

        public void SetProfilePropertiesOld(string connectionStringName, object appName, object userID, SettingsPropertyValueCollection values, List<SettingsPropertyColumn> settingsColumnsList)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                bool profileExists = false;
                string table =
                             SqlDbAccess.GetVzfObjectNameFromConnectionString("prov_Profile", connectionStringName);    
                using (var sc1 = new VzfSqlCommand(connectionStringName))
                {                    

                    // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;
                    sc1.CommandText.AppendQuery(string.Format("SELECT COUNT(1) FROM {0} WHERE UserID =UNHEX(REPLACE('{1}','-',''));", table, MySqlHelpers.GuidConverter(new Guid(userID.ToString())).ToString()));

                    profileExists = Convert.ToBoolean(sc1.ExecuteScalar(CommandType.Text, false));
                }

                StringBuilder MySqlCommandTextMain =
                    new StringBuilder("");
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "?i_UserID", MySqlHelpers.GuidConverter(new Guid(userID.ToString())).ToString()));

                // Build up strings used in the query
                StringBuilder columnStr = new StringBuilder();
                StringBuilder valueStr = new StringBuilder();
                StringBuilder setStr = new StringBuilder();
                int count = 0;

                foreach (SettingsPropertyColumn column in settingsColumnsList)
                {
                    // only write if it's dirty
                    if (values[column.Settings.Name].IsDirty)
                    {
                        columnStr.Append(", ");
                        valueStr.Append(", ");
                        columnStr.Append(column.Settings.Name);
                        string valueParam = "?Value" + count;
                        valueStr.Append(valueParam);                       
                        sc.Parameters.Add(sc.CreateParameter(column.DataType, valueParam, values[column.Settings.Name].PropertyValue));
                        if (column.DataType != DbType.DateTime)
                        {
                            if (count > 0)
                            {
                                setStr.Append(",");
                            }
                            setStr.Append(column.Settings.Name);
                            setStr.Append("=");
                            setStr.Append(valueParam);
                        }
                        count++;
                    }
                }

                columnStr.Append(",LastUpdatedDate ");
                valueStr.Append(",?LastUpdatedDate");
                setStr.Append(",LastUpdatedDate=?LastUpdatedDate");
                  sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "?LastUpdatedDate", DateTime.UtcNow));               
                if (profileExists)
                {
                    MySqlCommandTextMain.Append(" UPDATE ").Append(table).Append(" SET ").Append(setStr.ToString());
                    MySqlCommandTextMain.Append(" WHERE UserID =UNHEX(REPLACE(@i_UserID,'-',''))");
                    MySqlCommandTextMain.Append(";");
                }
                else
                {
                    MySqlCommandTextMain.Append("INSERT INTO ").Append(table).Append(" (UserID").Append(columnStr.ToString());
                    MySqlCommandTextMain.Append(") VALUES (UNHEX(REPLACE(@i_UserID,'-',''))").Append(valueStr.ToString()).Append(");");
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "@i_UserID", MySqlHelpers.GuidConverter(new Guid(userID.ToString())).ToString()));

                sc.CommandText.AppendQuery(MySqlCommandTextMain.ToString());
                sc.ExecuteNonQuery(CommandType.Text, false);
            }
        }

        public void SetProfileProperties(string connectionStringName, object appName, object userID, SettingsPropertyValueCollection values, List<SettingsPropertyColumn> settingsColumnsList)
        {
            if (YAF.Classes.Config.GetConfigValueAsBool("YAF.OldProfileProvider", true))
                SetProfilePropertiesOld(connectionStringName, appName, userID, values, settingsColumnsList);
            // Apply here new profile properties
            SettingsContext sctxt = new SettingsContext();
            sctxt.Add("IsAuthenticated", true);
            sctxt.Add("UserID", userID);
            sctxt.Add("ApplicationName", appName);

            bool isAuthenticated = true;

            if (string.IsNullOrEmpty(userID.ToString())) return;
            if (values.Count < 1) return;

            string index = string.Empty;
            string stringData = string.Empty;
            byte[] binaryData = null;

            if (PackProfileData(values, isAuthenticated, ref index, ref stringData, ref binaryData) < 1) return;

            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_userId", MySqlHelpers.GuidConverter(new Guid(userID.ToString())).ToString()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_index", index));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_stringdata", stringData));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "I_binarydata", binaryData));


                // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;

                sc.CommandText.AppendObjectQuery("prov_setprofileproperties", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure, false);
            }

            // EOF 'apply new profile properties'
        }

        public int DeleteProfiles(string connectionStringName, object appName, object userNames)
        {

            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName)); ;
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserNames", userNames));

                sc.CommandText.AppendObjectQuery("prov_profile_deleteprofiles", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }

        }

        public int DeleteInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));

                sc.CommandText.AppendObjectQuery("prov_profile_deleteinactive", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }
        }

        public int GetNumberInactiveProfiles(string connectionStringName, object appName, object inactiveSinceDate)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_InactiveSinceDate", inactiveSinceDate));

                sc.CommandText.AppendObjectQuery("prov_profile_getnumberinactiveprofiles", connectionStringName);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure, false));
            }
        }
    }
}
