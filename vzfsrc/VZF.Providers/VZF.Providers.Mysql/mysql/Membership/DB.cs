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


namespace YAF.Providers.Membership
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Text.RegularExpressions;
    using System.Security.Cryptography;
    using System.Data;
    using MySql.Data.MySqlClient;
    using MySql.Data;
    using YAF.Classes;
    using YAF.Core;
    using YAF.Classes.Data;
    using YAF.Classes.Pattern;

    public class VzfMySqlMembershipDBConnManager : MySqlDbConnectionManager
    {
    
        public override string ConnectionString
        {
            get
            {
                if (YafContext.Application[VzfMySqlMembershipProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[VzfMySqlMembershipProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

	public class MySQLDB
	{
        public static MySQLDB Current
        {
            get
            {
                return PageSingleton<MySQLDB>.Instance;
            }
        }



        public void ChangePassword(string connectionString, string appName, string userName, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_changepassword" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;               
				cmd.Parameters.Add( "i_Password", MySqlDbType.VarChar ).Value = newPassword;
                cmd.Parameters.Add( "i_PasswordSalt", MySqlDbType.VarChar ).Value = newSalt;
                cmd.Parameters.Add( "i_PasswordFormat", MySqlDbType.VarChar ).Value = passwordFormat.ToString();
                cmd.Parameters.Add( "i_PasswordAnswer", MySqlDbType.VarChar ).Value = newPasswordAnswer;

                MySqlDbAccess.ExecuteNonQuery(cmd, connectionString);
			}
		}

        public void ChangePasswordQuestionAndAnswer(string connectionString, string appName, string userName, string passwordQuestion, string passwordAnswer)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_changepasswordquestionandanswer" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;
                cmd.Parameters.Add( "i_PasswordQuestion", MySqlDbType.VarChar ).Value = passwordQuestion;
                cmd.Parameters.Add( "i_PasswordAnswer", MySqlDbType.VarChar ).Value = passwordAnswer;
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
			}
		}

        public void CreateUser(string connectionString, string appName, string userName, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_createuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Input Parameters
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;
                cmd.Parameters.Add( "i_Password", MySqlDbType.VarChar ).Value = password;
                cmd.Parameters.Add( "i_PasswordSalt", MySqlDbType.VarChar ).Value = passwordSalt;
                cmd.Parameters.Add( "i_PasswordFormat", MySqlDbType.VarChar ).Value = passwordFormat.ToString();
                cmd.Parameters.Add( "i_Email", MySqlDbType.VarChar ).Value = email;
				cmd.Parameters.Add( "i_PasswordQuestion", MySqlDbType.VarChar ).Value = passwordQuestion;
                cmd.Parameters.Add( "i_PasswordAnswer", MySqlDbType.VarChar ).Value = passwordAnswer;
                cmd.Parameters.Add( "i_IsApproved", MySqlDbType.Byte ).Value = isApproved;
				// Input Output Parameters
				var paramUserKey = new MySqlParameter( "i_UserKey", MySqlDbType.VarChar)
				    {Direction = ParameterDirection.InputOutput, Value = providerUserKey};
			    cmd.Parameters.Add( paramUserKey );

				//Execute
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
				//Retrieve Output Parameters
				providerUserKey = paramUserKey.Value;

			}
		}

        public void DeleteUser(string connectionString, string appName, string userName, bool deleteAllRelatedData)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_deleteuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;
                cmd.Parameters.Add( "i_DeleteAllRelated", MySqlDbType.Byte ).Value = deleteAllRelatedData;
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
			}
		}

        public DataTable FindUsersByEmail(string connectionString, string appName, string emailToMatch, int pageIndex, int pageSize)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_findusersbyemail" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value = appName;
				// Nonstandard args
                cmd.Parameters.Add( "i_EmailAddress", MySqlDbType.VarChar ).Value = emailToMatch;
				cmd.Parameters.Add( "i_PageIndex", MySqlDbType.Int32 ).Value =  pageIndex;
                cmd.Parameters.Add( "i_PageSize", MySqlDbType.Int32 ).Value = pageSize;
                var trecords = new MySqlParameter( "i_TotalRecords", MySqlDbType.Int32 )
                    {Direction = ParameterDirection.Output, Value = 0};
			    cmd.Parameters.Add( trecords );
                return MySqlDbAccess.GetData( cmd, connectionString) ;
			}
		}

        public DataTable FindUsersByName(string connectionString, string appName, string usernameToMatch, int pageIndex, int pageSize)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_findusersbyname" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value = appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = usernameToMatch;
				cmd.Parameters.Add( "i_PageIndex", MySqlDbType.Int32 ).Value = pageIndex;
                cmd.Parameters.Add( "i_PageSize", MySqlDbType.Int32 ).Value = pageSize;

                var trecords = new MySqlParameter( "i_TotalRecords", MySqlDbType.Int32 );
                trecords.Direction = ParameterDirection.Output;
                trecords.Value = 0;
                cmd.Parameters.Add( trecords );
                return MySqlDbAccess.GetData( cmd, connectionString) ;
			}
		}

        public DataTable GetAllUsers(string connectionString, string appName, int pageIndex, int pageSize)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_getallusers" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
                cmd.Parameters.Add( "i_PageIndex", MySqlDbType.Int32 ).Value = pageIndex;
                cmd.Parameters.Add( "i_PageSize", MySqlDbType.Int32 ).Value = pageSize;
                var trecords = new MySqlParameter( "i_TotalRecords", MySqlDbType.Int32 );
                trecords.Direction = ParameterDirection.Output;
                cmd.Parameters.Add( trecords );
                return MySqlDbAccess.GetData( cmd, connectionString) ;
			}
		}

		public  int GetNumberOfUsersOnline(string connectionString, string appName, int TimeWindow )
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_getnumberofusersonline" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value = appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_TimeWindow", MySqlDbType.Timestamp ).Value =  TimeWindow;
				cmd.Parameters.Add( "i_CurrentTimeUtc", MySqlDbType.Timestamp ).Value = DateTime.UtcNow;
				var p = new MySqlParameter( "i_ReturnValue", MySqlDbType.Int32 ) {Direction = ParameterDirection.ReturnValue};
			    cmd.Parameters.Add( p );
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
				return (int) cmd.Parameters ["i_ReturnValue"].Value ;
			}
		}

        public DataRow GetUser(string connectionString, string appName, object providerUserKey, string userName, bool userIsOnline)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_getuser" ) ) )
			{
                
               
                if (providerUserKey != null)          
                {                   
                    providerUserKey = MySqlDbAccess.GuidConverter( new Guid(providerUserKey.ToString() ) );
                }

				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;               
                cmd.Parameters.Add( "i_UserKey", MySqlDbType.String ).Value = providerUserKey;
				cmd.Parameters.Add( "i_UserIsOnline", MySqlDbType.Byte ).Value = userIsOnline;
                cmd.Parameters.Add("i_UTCTIMESTAMP", MySqlDbType.DateTime).Value = DateTime.UtcNow;
				using ( var dt = MySqlDbAccess.GetData( cmd, connectionString)  )
				{
					return dt.Rows.Count > 0 ? dt.Rows [0] : null;
				}
                
			}

		}

        public DataTable GetUserPasswordInfo(string connectionString, string appName, string userName, bool updateUser)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_getuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;
                cmd.Parameters.Add( "i_UserKey", MySqlDbType.String ).Value = DBNull.Value;
                cmd.Parameters.Add( "i_UserIsOnline", MySqlDbType.Byte ).Value = updateUser;
				return MySqlDbAccess.GetData( cmd, connectionString) ;
			}

		}

        public DataTable GetUserNameByEmail(string connectionString, string appName, string email)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_getusernamebyemail" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value = appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_Email", MySqlDbType.VarChar ).Value = email ;
				return MySqlDbAccess.GetData( cmd, connectionString) ;
			}
		}

        public void ResetPassword(string connectionString, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_resetpassword" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName;
				cmd.Parameters.Add( "i_Password", MySqlDbType.VarChar ).Value = password ;
                cmd.Parameters.Add( "i_PasswordSalt", MySqlDbType.VarChar ).Value = passwordSalt;
                cmd.Parameters.Add( "i_PasswordFormat", MySqlDbType.VarChar ).Value = passwordFormat.ToString();
                cmd.Parameters.Add( "i_MaxInvalidAttempts", MySqlDbType.Int32 ).Value = maxInvalidPasswordAttempts;
                cmd.Parameters.Add( "i_PasswordAttemptWindow", MySqlDbType.Int32 ).Value = passwordAttemptWindow;
                cmd.Parameters.Add( "i_CurrentTimeUtc", MySqlDbType.Timestamp ).Value = DateTime.UtcNow;

				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
			}

		}

        public void UnlockUser(string connectionString, string appName, string userName)
		{
			using ( var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_unlockuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add( "i_ApplicationName",  MySqlDbType.VarChar ).Value= appName ;
				// Nonstandard args
				cmd.Parameters.Add( "i_UserName", MySqlDbType.VarChar ).Value = userName ;
				MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
			}
		}

        public int UpdateUser(string connectionString, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            using (var cmd = new MySqlCommand(MySqlDbAccess.GetObjectName("prov_updateuser")))
            {
                object providerUserKey = null;
                if (user.ProviderUserKey != null)
                {
                    providerUserKey = MySqlDbAccess.GuidConverter(new Guid(user.ProviderUserKey.ToString())).ToString();
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_ApplicationName", MySqlDbType.VarChar).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add("i_UserKey", MySqlDbType.String).Value = providerUserKey;
                cmd.Parameters.Add("i_UserName", MySqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("i_Email", MySqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("i_Comment", MySqlDbType.VarChar).Value = user.Comment;
                cmd.Parameters.Add("i_IsApproved", MySqlDbType.Byte).Value = user.IsApproved;
                cmd.Parameters.Add("i_LastLogin", MySqlDbType.Timestamp).Value = user.LastLoginDate;
                cmd.Parameters.Add("i_LastActivity", MySqlDbType.Timestamp).Value = user.LastActivityDate.ToUniversalTime();
                cmd.Parameters.Add("i_UniqueEmail", MySqlDbType.Byte).Value = requiresUniqueEmail;

                return (int)MySqlDbAccess.ExecuteScalar(cmd, connectionString); // Execute Scalar

            }
        }
        public void UpgradeMembership(int previousVersion, int newVersion)
        {
            UpgradeMembership(VzfMySqlMembershipProvider.ConnStrAppKeyName, previousVersion, newVersion);
        }

	    public void UpgradeMembership(string connectionString, int previousVersion, int newVersion)
        {
            using (var cmd = new MySqlCommand( MySqlDbAccess.GetObjectName( "prov_upgrade" ) ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Nonstandard args
                cmd.Parameters.Add( "i_PreviousVersion", MySqlDbType.Int32 ).Value = previousVersion;
                cmd.Parameters.Add( "i_NewVersion", MySqlDbType.Int32 ).Value = newVersion;
                cmd.Parameters.Add("i_UTCTIMESTAMP", MySqlDbType.DateTime).Value = DateTime.UtcNow;

                MySqlDbAccess.ExecuteNonQuery( cmd, connectionString) ;
            }

        }

	}
}
