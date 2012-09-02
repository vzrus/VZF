/* Yet Another Forum.NET MySQL data layer by vzrus
 * Copyright (C) 2009-2010 vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * General class structure is based on MS SQL Server code,
 * created by YAF developers
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2009 Jaben Cargman
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
 *
 * 
 */


namespace YAF.Providers.Profile
{
    using System;
    using System.Configuration;
    using System.Text;
    using System.Web;
    using System.IO;
    using System.Globalization;
    using System.Data;

    using MySql.Data;
    using MySql.Data.MySqlClient;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Classes.Data;
    using YAF.Classes.Pattern;

    public class VzfMySqlProfileDBConnManager : MySqlDbConnectionManager
    {

        public override string ConnectionString
        {
            get
            {
                if (YafContext.Application[VzfMySqlProfileProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[VzfMySqlProfileProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

	public  class MySQLDB
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

        static public void UnpackProfileData(DataRow profileRow, SettingsPropertyValueCollection values)
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
        #endregion

        public DataTable GetProfiles(string connectionString, object appName, object pageIndex, object pageSize, object userNameToMatch, object inactiveSinceDate)
		{
            
           
            //int TotalCount = 0;
            int TotalCountNew = 0;
			/*using ( MySqlCommand cmd = MySqlDbAccess.GetCommand( "prov_profile_getprofiles_count" ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar).Value= appName;			
				cmd.Parameters.AddWithValue( "i_UserNameToMatch", userNameToMatch );
				cmd.Parameters.AddWithValue( "i_InactiveSinceDate", inactiveSinceDate );
     
                TotalCount = Convert.ToInt32(MySqlDbAccess.ExecuteScalar( cmd, connectionString) );
                
			}*/
            using ( MySqlCommand cmd = MySqlDbAccess.GetCommand( "prov_profile_getprofiles" ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add( "i_ApplicationName", MySqlDbType.VarChar ).Value = appName;
                cmd.Parameters.Add( "i_PageIndex", MySqlDbType.Int32 ).Value = pageIndex;
                cmd.Parameters.Add( "i_PageSize", MySqlDbType.Int32 ).Value = pageSize;
                cmd.Parameters.Add( "i_UserNameToMatch", MySqlDbType.VarChar ).Value = userNameToMatch;
                cmd.Parameters.Add( "i_InactiveSinceDate", MySqlDbType.Timestamp ).Value = inactiveSinceDate;
                MySqlParameter outputtc = new MySqlParameter("i_TotalCount", MySqlDbType.Int32);
                            outputtc.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add( outputtc );
                            MySqlDbAccess.ExecuteNonQuery( cmd, false, connectionString) ;
                            TotalCountNew = ( int ) outputtc.Value;
              
                DataTable dt = MySqlDbAccess.GetData( cmd, connectionString) ;
                if ( dt.Rows.Count > 0 ) dt.Rows[0]["TotalCount"] = TotalCountNew; //TotalCount;
                
                return dt;
            }

		}

		public DataTable GetProfileStructure(string connectionString)
		{			

			using ( MySqlCommand cmd = MySqlDbAccess.GetCommand(String.Format( @"SELECT  * FROM {0} LIMIT 1", MySqlDbAccess.GetObjectName( "prov_Profile" ) ),true ))
			{				
				return MySqlDbAccess.GetData( cmd, connectionString) ;
			}
		}

        public void AddProfileColumn(string connectionString, string Name, MySqlDbType columnType, int size)
		{
			// get column type...
			string type = columnType.ToString();

            if ( type.Contains("DateTime".ToLower() ) )
            { type = "TIMESTAMP"; }
            if ( type.Contains("VarChar") )
            { type = "VARCHAR"; }
            if ( type.Contains("Int32") )
            { type = "INT"; }
       

			if ( size > 0 )
			{
				type += "(" + size.ToString() + ")";
			}

			string sql = String.Format( "ALTER TABLE {0} ADD `{1}` {2};", MySqlDbAccess.GetObjectName( "prov_Profile" ), Name, type );

			using ( MySqlCommand cmd = new MySqlCommand( sql ) )
			{
				cmd.CommandType = CommandType.Text;
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
			}
		}

        public object GetProviderUserKey(string connectionString, object appName, object username)
		{
			DataRow row = YAF.Providers.Membership.MySQLDB.Current.GetUser(connectionString, appName.ToString(), null, username.ToString(), false );

			if ( row != null )
			{
				return row ["UserID"];
			}

			return null;
		}

        public void SetProfilePropertiesOld(string connectionString, object appName, object userID, System.Configuration.SettingsPropertyValueCollection values, System.Collections.Generic.List<MySqlSettingsPropertyColumn> settingsColumnsList)
        {
             
            
            using (MySqlCommand cmd = new MySqlCommand())
            {
                bool profileExists = false;  
                   string table =
                                YAF.Classes.Data.MySqlDbAccess.GetObjectName("prov_Profile");
                        using (MySqlCommand cmd1 = MySqlDbAccess.GetCommand(String.Format("SELECT COUNT(1) FROM {0} WHERE UserID =UNHEX(REPLACE('{1}','-',''));", table, MySqlDbAccess.GuidConverter(new Guid(userID.ToString())).ToString()), true))
                        {
                            profileExists = Convert.ToBoolean( MySqlDbAccess.ExecuteScalar(cmd1,connectionString) );
                        }
                StringBuilder MySqlCommandTextMain =
                    new StringBuilder("");
                cmd.Parameters.Add("?UserID", MySqlDbType.VarChar).Value = MySqlDbAccess.GuidConverter(new Guid(userID.ToString()));

                // Build up strings used in the query
                StringBuilder columnStr = new StringBuilder();
                StringBuilder valueStr = new StringBuilder();
                StringBuilder setStr = new StringBuilder();
                int count = 0;

                foreach (MySqlSettingsPropertyColumn column in settingsColumnsList)
                {
                    // only write if it's dirty
                    if (values[column.Settings.Name].IsDirty)
                    {
                        columnStr.Append(", ");
                        valueStr.Append(", ");
                        columnStr.Append(column.Settings.Name);
                        string valueParam = "?Value" + count;
                        valueStr.Append(valueParam);
                        cmd.Parameters.Add(valueParam, column.DataType).Value = values[column.Settings.Name].PropertyValue;

                        if ( column.DataType != MySqlDbType.Timestamp )
                        {
                            if ( count > 0 )
                            {
                                setStr.Append(",");
                            }
                            setStr.Append( column.Settings.Name );
                            setStr.Append( "=" );
                            setStr.Append( valueParam );
                        }
                        count++;
                    }
                }

                columnStr.Append( ",LastUpdatedDate " );
                valueStr.Append( ",?LastUpdatedDate" );
                setStr.Append( ",LastUpdatedDate=?LastUpdatedDate" );
                cmd.Parameters.Add( "?LastUpdatedDate", MySqlDbType.Timestamp ).Value = DateTime.UtcNow;
                if (profileExists)
                {
                    MySqlCommandTextMain.Append( " UPDATE " ).Append(table).Append( " SET " ).Append(setStr.ToString());
                    MySqlCommandTextMain.Append(" WHERE UserID =UNHEX(REPLACE(@i_userID,'-',''))");
                    MySqlCommandTextMain.Append( ";" );
                }
                else
                {
                    MySqlCommandTextMain.Append( "INSERT INTO " ).Append(table).Append( " (UserID" ).Append(columnStr.ToString());
                    MySqlCommandTextMain.Append(") VALUES (UNHEX(REPLACE(@i_userID,'-',''))").Append(valueStr.ToString()).Append(");");
                }

                cmd.CommandText = MySqlCommandTextMain.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@i_userID", MySqlDbType.VarChar).Value = MySqlDbAccess.GuidConverter(new Guid(userID.ToString()));
                MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
            }
        }
        public void SetProfileProperties(string connectionString, object appName, object userID, System.Configuration.SettingsPropertyValueCollection values, System.Collections.Generic.List<MySqlSettingsPropertyColumn> settingsColumnsList)
		{
            if (YAF.Classes.Config.GetConfigValueAsBool("YAF.OldProfileProvider", true))
                SetProfilePropertiesOld(connectionString,appName, userID, values, settingsColumnsList);
            // Apply here new profile properties
            SettingsContext sc=new SettingsContext();
            sc.Add( "IsAuthenticated", true );
            sc.Add( "UserID", userID );
            sc.Add( "ApplicationName", appName );

            bool isAuthenticated = true;

            if ( String.IsNullOrEmpty( userID.ToString() ) ) return;
            if ( values.Count < 1 ) return;

            string index = String.Empty;
            string stringData = String.Empty;           
            byte[] binaryData = null;            
           
            if (PackProfileData(values, isAuthenticated, ref index, ref stringData, ref binaryData) < 1 ) return;
                         
            
                     using (MySqlCommand cmd = MySqlDbAccess.GetCommand("prov_setprofileproperties"))
           
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@i_userId",MySqlDbType.VarChar).Value=MySqlDbAccess.GuidConverter(new Guid(userID.ToString())).ToString();
                            cmd.Parameters.Add("@i_index",MySqlDbType.LongText).Value=index;
                            cmd.Parameters.Add("@i_stringData",MySqlDbType.LongText).Value=stringData;
                            cmd.Parameters.Add("@i_binaryData",MySqlDbType.LongBlob).Value=binaryData;
                            
                            MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;                          
                        }

                       
            
            // EOF 'apply new profile properties'
                 
            
		}

         public int DeleteProfiles(string connectionString, object appName, object userNames)
		{
			using ( MySqlCommand cmd = MySqlDbAccess.GetCommand( "prov_profile_deleteprofiles" ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add( "i_ApplicationName", MySqlDbType.VarChar ).Value = appName;
				cmd.Parameters.Add( "i_UserNames", MySqlDbType.VarChar ).Value=userNames;
				return Convert.ToInt32( MySqlDbAccess.ExecuteScalar( cmd, connectionString)  );
			}
		}

         public int DeleteInactiveProfiles(string connectionString, object appName, object inactiveSinceDate)
		{
			using ( MySqlCommand cmd = MySqlDbAccess.GetCommand( "prov_profile_deleteinactive" ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add( "i_ApplicationName", MySqlDbType.VarChar ).Value = appName;
                cmd.Parameters.Add( "i_InactiveSinceDate", MySqlDbType.Timestamp ).Value = inactiveSinceDate;
				return Convert.ToInt32( MySqlDbAccess.ExecuteScalar( cmd, connectionString)  );
			}
		}

         public int GetNumberInactiveProfiles(string connectionString, object appName, object inactiveSinceDate)
		{
			using ( MySqlCommand cmd = MySqlDbAccess.GetCommand( "prov_profile_getnumberinactiveprofiles" ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add( "i_ApplicationName", MySqlDbType.VarChar ).Value = appName;
                cmd.Parameters.Add( "i_InactiveSinceDate", MySqlDbType.Timestamp ).Value = inactiveSinceDate;
				return Convert.ToInt32( MySqlDbAccess.ExecuteScalar( cmd, connectionString)  );
			}
		}

		


	}
}
