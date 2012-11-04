/* VZF by vzrus
 * Copyright (C) 2006-2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure was primarily based on MS SQL Server code,
 * created by YAF(YetAnotherForum) developers  * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
using System;
using System.Web.Security;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using YAF.Classes;
using YAF.Core;
using YAF.Classes.Data;
using YAF.Classes.Pattern;

namespace YAF.Providers.Membership
{
    public class VzfFirebirdMembershipDbConnManager : FbDbConnectionManager
    {
        public override string ConnectionString
        {
            get
            {
                if (YafContext.Application[VzfFirebirdMembershipProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[VzfFirebirdMembershipProvider.ConnStrAppKeyName] as string;
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
      
        public void UpgradeMembership(string connectionString, int I_PREVIOUSVERSION, int I_NEWVERSION)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("p_upgrade")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Nonstandard args
                cmd.Parameters.AddWithValue("@I_PREVIOUSVERSION", I_PREVIOUSVERSION);
                cmd.Parameters.AddWithValue("@I_NEWVERSION", I_NEWVERSION);

                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        //int totalRecords = 0;
        public void ChangePassword(string connectionString, string appName, string username, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
		{
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_changepassword")))
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;  
               
				// Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORD", FbDbType.VarChar)).Value = newPassword;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDSALT", FbDbType.VarChar)).Value = newSalt;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDFORMAT", FbDbType.VarChar)).Value = passwordFormat;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDANSWER", FbDbType.VarChar)).Value = newPasswordAnswer; 				

				FbDbAccess.ExecuteNonQuery(cmd,connectionString );
			}
		}

        public void ChangePasswordQuestionAndAnswer(string connectionString, string appName, string username, string passwordQuestion, string passwordAnswer)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_CHANGEPASSQUESTIONANDANSWER" ) ) )
			{
				
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;   
				
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDQUESTION", FbDbType.VarChar)).Value = passwordQuestion;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDANSWER", FbDbType.VarChar)).Value = passwordAnswer;
				
                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
			}
		}

        public void CreateUser(string connectionString, string appName, string username, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_CREATEUSER" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value=appName;			
                // Input Parameters
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORD", FbDbType.VarChar)).Value = password;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDSALT", FbDbType.VarChar)).Value = passwordSalt;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDFORMAT", FbDbType.VarChar)).Value = passwordFormat;               
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email; 
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDQUESTION", FbDbType.VarChar)).Value = passwordQuestion;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDANSWER", FbDbType.VarChar)).Value = passwordAnswer;
                cmd.Parameters.Add(new FbParameter("@I_ISAPPROVED", FbDbType.Boolean)).Value = isApproved;          
				
				// Input Output Parameters
                var paramUserKey = new FbParameter("@I_USERKEY", FbDbType.VarChar)
                    {Direction = ParameterDirection.InputOutput, Value = providerUserKey};
			    cmd.Parameters.Add( paramUserKey );
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;              		

               // FbParameter paramUserKeyOut = new FbParameter("@OUT_USERKEY", FbDbType.VarChar);
               // paramUserKey.Direction = ParameterDirection.Output;
               // paramUserKey.Value = providerUserKey;                

				//Execute
                providerUserKey = FbDbAccess.GetData(cmd,connectionString ).Rows[0][0];
				//Retrieve Output Parameters
               
				//providerUserKey = paramUserKey.Value;

			}
		}

        public void DeleteUser(string connectionString, string appName, string username, bool deleteAllRelatedData)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_deleteuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                
				// Nonstandard args

                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@i_deleteallrelated", FbDbType.Boolean)).Value = deleteAllRelatedData;
                
				FbDbAccess.ExecuteNonQuery(cmd,connectionString );
			}
		}

        public DataTable FindUsersByEmail(string connectionString, string appName, string emailToMatch, int pageIndex, int pageSize)
		{
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_FINDUSERSBYEMAIL")))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@i_emailaddress", FbDbType.VarChar)).Value = emailToMatch;
                cmd.Parameters.Add(new FbParameter("@i_pageindex", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@i_pagesize", FbDbType.Integer)).Value = pageSize;
                
                return FbDbAccess.GetData(cmd,connectionString );
			}
		}

        public DataTable FindUsersByName(string connectionString, string appName, string usernameToMatch, int pageIndex, int pageSize)
		{
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_FINDUSERSBYNAME")))
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

				// Nonstandard args

                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = usernameToMatch;
               
                //TODO:fix overflow bug
                cmd.Parameters.Add(new FbParameter("@i_pageindex", FbDbType.Integer)).Value = pageIndex;
                if (pageSize == int.MaxValue) { pageSize = 1; }
                cmd.Parameters.Add(new FbParameter("@i_pagesize", FbDbType.Integer)).Value = pageSize;
                		
				return FbDbAccess.GetData(cmd,connectionString );
			}
		}

        public DataTable GetAllUsers(string connectionString, string appName, int pageIndex, int pageSize)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_getallusers" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@i_pageindex", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@i_pagesize", FbDbType.Integer)).Value = pageSize;
                
				return FbDbAccess.GetData(cmd,connectionString );
			}
		}

        public int GetNumberOfUsersOnline(string connectionString, string appName, int TimeWindow)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_getnumberofusersonline" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_TIMEWINDOW", FbDbType.Integer)).Value = TimeWindow;
                cmd.Parameters.Add(new FbParameter("@i_currenttimeutc", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                
                var p = new FbParameter( "@i_returnvalue",DbType.Int32 ) {Direction = ParameterDirection.ReturnValue};
			    cmd.Parameters.Add( p );
				FbDbAccess.ExecuteNonQuery(cmd,connectionString );
				return Convert.ToInt32(cmd.Parameters ["@i_returnvalue"].Value );
			}
		}

        public DataRow GetUser(string connectionString, string appName, object providerUserKey, string userName, bool userIsOnline)
		{       
            
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_GETUSER" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;               
                cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value = providerUserKey;
                cmd.Parameters.Add(new FbParameter("@I_USERISONLINE", FbDbType.Boolean)).Value = Convert.ToInt32(userIsOnline);
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;              		
				
				using ( var dt = FbDbAccess.GetData(cmd,connectionString ))
				{
				    var dr = dt.Rows[0];
				    return dr["UserID"] !=DBNull.Value ? dt.Rows[0] : null;
                }
			}
		}

		public  DataTable GetUserPasswordInfo(string connectionString, string appName, string username, bool updateUser )
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_getuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;                
				// Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value = DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_USERISONLINE", FbDbType.Boolean)).Value = updateUser;                
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;              		
		
				return FbDbAccess.GetData(cmd,connectionString );
			}

		}

        public DataTable GetUserNameByEmail(string connectionString, string appName, string email)
		{
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_GETUSERNAMEBYEMAL")))
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				// Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;
				
				return FbDbAccess.GetData(cmd,connectionString );
			}
		}


        public void ResetPassword(string connectionString, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_resetpassword" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@i_password", FbDbType.VarChar)).Value = password;
                cmd.Parameters.Add(new FbParameter("@i_passwordsalt", FbDbType.VarChar)).Value = passwordSalt;
                cmd.Parameters.Add(new FbParameter("@i_passwordformat", FbDbType.VarChar)).Value = passwordFormat;
                cmd.Parameters.Add(new FbParameter("@i_maxinvalidattempts", FbDbType.Integer)).Value = maxInvalidPasswordAttempts;
                cmd.Parameters.Add(new FbParameter("@i_passwordattemptwindow", FbDbType.Integer)).Value = passwordAttemptWindow;
                cmd.Parameters.Add(new FbParameter("@i_currenttimeutc", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                
				FbDbAccess.ExecuteNonQuery(cmd,connectionString );
			}

		}

        public void UnlockUser(string connectionString, string appName, string userName)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_unlockuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				// Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
				
				FbDbAccess.ExecuteNonQuery(cmd,connectionString );
			}
		}

        public int UpdateUser(string connectionString, object appName, MembershipUser user, bool requiresUniqueEmail)
		{
			using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_updateuser" ) ) )
			{
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
				// Nonstandard args
                cmd.Parameters.Add(new FbParameter("@i_userkey", FbDbType.VarChar)).Value = user.ProviderUserKey;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = user.UserName;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = user.Email;
                cmd.Parameters.Add(new FbParameter("@I_COMMENT", FbDbType.Text)).Value = user.Comment;
                cmd.Parameters.Add(new FbParameter("@i_isapproved", FbDbType.Boolean)).Value = user.IsApproved;
                cmd.Parameters.Add(new FbParameter("@I_LASTLOGIN", FbDbType.TimeStamp)).Value = user.LastLoginDate;
                cmd.Parameters.Add(new FbParameter("@I_LASTACTIVITY", FbDbType.TimeStamp)).Value = user.LastActivityDate.ToUniversalTime();
                cmd.Parameters.Add(new FbParameter("@I_UNIQUEEMAIL", FbDbType.Boolean)).Value = requiresUniqueEmail;
                
                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd,connectionString ));
			}
		}

	}
}
