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
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Text;
   
    using FirebirdSql.Data.FirebirdClient;

    using VZF.Data.Firebird;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;

    public class VzfFirebirdDBConnManager : FbDbConnectionManager
    {
        public override string ConnectionString
        {
            get
            {
                if (YafContext.Application[VzfFirebirdProfileProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[VzfFirebirdProfileProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

    public class FbDB
    {
       // private FbDbAccess FbDbAccess = new FbDbAccess();

        public static FbDB Current
        {
            get
            {
                return PageSingleton<FbDB>.Instance;
            }
        }

        public FbDB()
        {
           // FbDbAccess.SetConnectionManagerAdapter<VzfFirebirdDBConnManager>();
        }

        static private int EncodeProfileData(SettingsPropertyValueCollection collection, bool isAuthenticated,
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

            var indexBuilder = new StringBuilder();
            var stringDataBuilder = new StringBuilder();
            var binaryBuilder = new MemoryStream();
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
                    var binaryValue = (byte[])propValue;
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

        public static void DecodeProfileData(DataRow profileRow, SettingsPropertyValueCollection values)
        {
            byte[] binaryData = null;
            string indexData = null;
            string stringData = null;
            indexData = profileRow["valueindex"].ToString();
            stringData = profileRow["stringData"].ToString();
            if (profileRow["binaryData"] != DBNull.Value)
                binaryData = (byte[])profileRow["binaryData"];

           // if (indexData == null) return;

            string[] indexes = indexData.Split(':');

            foreach (string index in indexes)
            {
                string[] parts = index.Split('/');
                SettingsPropertyValue value = values[parts[0]];
                if (value == null) continue;

                int pos = Int32.Parse(parts[2], CultureInfo.InvariantCulture);
                int len = Int32.Parse(parts[3], CultureInfo.InvariantCulture);
                if (len == -1)
                {
                    value.PropertyValue = null;
                    value.IsDirty = false;
                    value.Deserialized = true;
                }
                else if (parts[1].Equals("0"))
                    value.SerializedValue = stringData.Substring(pos, len);
                else
                {
                    byte[] buf = new byte[len];
                    Buffer.BlockCopy(binaryData, pos, buf, 0, len);
                    value.SerializedValue = buf;
                }
            }
        }

        public DataTable GetProfiles(string connectionString, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
        {
            using ( FbCommand cmd = FbDbAccess.GetCommand( "P_profile_getprofiles" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_PROFILEAUTHOPTIONS", FbDbType.Integer));
                cmd.Parameters[1].Value = 1;

                cmd.Parameters.Add(new FbParameter("@I_USERNAMETOMATCH", FbDbType.VarChar));
                cmd.Parameters[2].Value = userNameToMatch;

                cmd.Parameters.Add(new FbParameter("@I_INACTIVESINCEDATE", FbDbType.TimeStamp));
                cmd.Parameters[3].Value = inactiveSinceDate;

                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer));
                cmd.Parameters[4].Value = pageIndex;

                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer));
                cmd.Parameters[5].Value = pageSize;

                DataTable dt = FbDbAccess.GetData(cmd, connectionString);

                return dt;

            }
        }

         public DataTable GetProfileStructure(string connectionString)
        {
            
       
            string sql = String.Format(@"SELECT FIRST 1 * FROM {0}", FbDbAccess.GetObjectName(@"P_profile"));

            using ( FbCommand cmd = new FbCommand( sql ) )
            {
                cmd.CommandType = CommandType.Text;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

         public void AddProfileColumn(string connectionString, string Name, FbDbType columnType, int size)
        {
            // get column type...
            string type = columnType.ToString();
           // FbDbType.SmallInt
           // FbDbType.TimeStamp
          //  FbDbType.VarChar
          //  FbDbType.Integer    
            
          
            if (type.ToLower()=="timestamp")
            { type = "TimeStamp"; }

            if ( size > 0 )
            {
                type += "(" + size.ToString() + ")";
            }
            if ( type.Contains("VarChar") && FbDbAccess.DatabaseEncoding !=null)
            {
                type += " CHARACTER SET " + FbDbAccess.DatabaseEncoding;

                      if (FbDbAccess.DatabaseCollation !=null)
                  {
                      type += " COLLATE " +  FbDbAccess.DatabaseCollation;
                  }
            }
          
            string sql = String.Format(@"ALTER TABLE {0} ADD {1}  {2};", FbDbAccess.GetObjectName("P_profile"), Name, type);
             
            using ( FbCommand cmd = new FbCommand( sql ) )
            {
                cmd.CommandType = CommandType.Text;
                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

         public object GetProviderUserKey(string connectionString, object appName, object username )
        {
            DataRow row = YAF.Providers.Membership.FbDB.Current.GetUser(connectionString, appName.ToString(), null, username.ToString(), false);

            if ( row != null )
            {
                return row ["UserID"];
            }

            return null;
        }

         public void SetProfileProperties(string connectionString, object appName, object userID, System.Configuration.SettingsPropertyValueCollection values, System.Collections.Generic.List<FbSettingsPropertyColumn> settingsColumnsList )
        {      
            // EOF 'apply new profile properties'
            if (String.IsNullOrEmpty(userID.ToString())) return;
            if (values.Count <= 0) return;

            string index = String.Empty;
            string stringData = String.Empty;
            byte[] binaryData = null;
            bool isAuthenticated = true;

            int count = EncodeProfileData(values, isAuthenticated, ref index, ref stringData, ref binaryData);
            if (count < 1) return;

            bool profileExists = false;
            using (FbCommand cmd1 = FbDbAccess.GetCommand(String.Format(@"SELECT COUNT(1) FROM {0} WHERE USERID =CHAR_TO_UUID('{1}');", FbDbAccess.GetObjectName("P_PROFILE"), userID), true))
            {
                profileExists = Convert.ToBoolean(FbDbAccess.ExecuteScalar(cmd1,connectionString));
            }
            if (profileExists)
            {
                /* using (FbCommand cmd = FbDbAccess.GetCommand(
                     String.Format(@"UPDATE {0} SET valueindex ='{1}',stringdata='{2}',binarydata='{3}',LASTUPDATEDDATE='{4}' 
                            WHERE USERID =CHAR_TO_UUID('{5}');", YAF.Classes.Data.FbDbAccess.GetObjectName("P_PROFILE"), index, stringData, binaryData, DateTime.UtcNow, userID), true))
                { */


                      using (FbCommand cmd = FbDbAccess.GetCommand(
                     String.Format(@"UPDATE {0} SET valueindex = ?,stringdata=?,binarydata=?,LASTUPDATEDDATE=? 
                            WHERE USERID =CHAR_TO_UUID('{1}');", FbDbAccess.GetObjectName("P_PROFILE"), userID), true))
                {
                    cmd.Parameters.Add(new FbParameter("@I_valueindex", FbDbType.Binary)).Value = index;
                    cmd.Parameters.Add(new FbParameter("@I_stringdata", FbDbType.Binary)).Value = stringData;
                    cmd.Parameters.Add(new FbParameter("@I_binarydata", FbDbType.Binary)).Value = binaryData;
                    cmd.Parameters.Add(new FbParameter("@current_utctimestamp", FbDbType.TimeStamp)).Value = DateTime.Now;
                    // cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;

                    FbDbAccess.ExecuteNonQuery(cmd,connectionString);              
                }
            }
            else
            {
                using (FbCommand cmd = FbDbAccess.GetCommand(
                   String.Format(@"INSERT INTO {0}(USERID,valueindex,stringdata,binarydata,LASTUPDATEDDATE) 
                       VALUES(CHAR_TO_UUID(@I_USERID), @I_valueindex, @I_stringdata, @I_binarydata,@current_utctimestamp);", FbDbAccess.GetObjectName("P_PROFILE")), true))
                {
                    cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.VarChar)).Value = userID;                  
                    cmd.Parameters.Add(new FbParameter("@I_valueindex", FbDbType.Binary)).Value = index;                    
                    cmd.Parameters.Add(new FbParameter("@I_stringdata", FbDbType.Binary)).Value = stringData;                   
                    cmd.Parameters.Add(new FbParameter("@I_binarydata", FbDbType.Binary)).Value = binaryData;           
                    cmd.Parameters.Add(new FbParameter("@current_utctimestamp", FbDbType.TimeStamp)).Value = DateTime.Now;
                    
                    FbDbAccess.ExecuteNonQuery(cmd,connectionString);
                   
                }
            }
        }

         public int DeleteProfiles(string connectionString, object appName, object userNames)
        {
            int deleted = 0; 
            char [] sep= new char [1]{','};
            string [] userNamesArr = userNames.ToString().Split(sep[0]);
            for (int i =0; i <= userNamesArr.Length; i++)
            {
                using (FbCommand cmd = FbDbAccess.GetCommand("P_profile_deleteprofile"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                    cmd.Parameters[0].Value = appName;

                    cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar));
                    cmd.Parameters[1].Value = userNamesArr[i];


                    deleted += Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd,connectionString));
                }
            }
            return deleted;
        }

         public int DeleteInactiveProfiles(string connectionString, object appName, object inactiveSinceDate)
        {
            using ( FbCommand cmd = FbDbAccess.GetCommand( "P_profile_deleteinactive" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@i_InactiveSinceDate", FbDbType.TimeStamp));
                cmd.Parameters[1].Value = inactiveSinceDate;

                
                return Convert.ToInt32( FbDbAccess.ExecuteScalar(cmd,connectionString ) );
            }
        }

         public int GetNumberInactiveProfiles(string connectionString, object appName, object inactiveSinceDate)
        {
            using (FbCommand cmd = FbDbAccess.GetCommand("P_PROFILE_GETNUMINACT"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;
               
                cmd.Parameters.Add(new FbParameter("i_InactiveSinceDate", FbDbType.TimeStamp));
                cmd.Parameters[1].Value = inactiveSinceDate;

                
                return Convert.ToInt32( FbDbAccess.ExecuteScalar(cmd,connectionString ) );
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
