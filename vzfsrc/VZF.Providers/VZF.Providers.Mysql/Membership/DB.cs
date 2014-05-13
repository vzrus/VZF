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
    using System.Data;
    using System.Web.Security;
    
    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;
    using VZF.Data.DAL;   

    public class MySQLDB
    {
        public static MySQLDB Current
        {
            get
            {
                return PageSingleton<MySQLDB>.Instance;
            }
        }

        public void ChangePassword(string connectionStringName, string appName, string userName, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
        {
            using (var sc = new SQLCommand(connectionStringName))
            { 
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Password", newPassword));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordSalt", newSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordFormat", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordAnswer", newPasswordAnswer));

                sc.CommandText.AppendObjectQuery("prov_changepassword", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }             
        }

        public void ChangePasswordQuestionAndAnswer(string connectionStringName, string appName, string userName, string passwordQuestion, string passwordAnswer)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordQuestion", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordAnswer", passwordAnswer));

                sc.CommandText.AppendObjectQuery("prov_changepasswordquestionandanswer", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }     
        }

        public void CreateUser(string connectionStringName, string appName, string userName, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Password", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordSalt", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordFormat", passwordFormat.ToString()));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordQuestion", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordAnswer", passwordAnswer));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsApproved", isApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", providerUserKey, ParameterDirection.InputOutput));
               // sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_createuser", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure, true);
                providerUserKey = sc.Parameters["i_UserKey"].Value;
            }     
        }

        public void DeleteUser(string connectionStringName, string appName, string userName, bool deleteAllRelatedData)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeleteAllRelated", deleteAllRelatedData));

                sc.CommandText.AppendObjectQuery("prov_deleteuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }  
        }

        public DataTable FindUsersByEmail(string connectionStringName, string appName, string emailToMatch, int pageIndex, int pageSize)
        {

            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_EmailAddress", emailToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TotalRecords", 0, ParameterDirection.Output));

                sc.CommandText.AppendObjectQuery("prov_findusersbyemail", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }    
        }

        public DataTable FindUsersByName(string connectionStringName, string appName, string usernameToMatch, int pageIndex, int pageSize)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", usernameToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex)); 
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TotalRecords", 0, ParameterDirection.Output));

                sc.CommandText.AppendObjectQuery("prov_findusersbyname", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }     
        }

        public DataTable GetAllUsers(string connectionStringName, string appName, int pageIndex, int pageSize)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));            
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TotalRecords", 0, ParameterDirection.Output));

                sc.CommandText.AppendObjectQuery("prov_getallusers", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }    
        }

        public int GetNumberOfUsersOnline(string connectionStringName, string appName, int TimeWindow)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TimeWindow", TimeWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_CurrentTimeUtc", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ReturnValue", ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("prov_getnumberofusersonline", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);

                return Convert.ToInt32(sc.Parameters["i_ReturnValue"].Value);
            }  
        }

        public DataRow GetUser(string connectionStringName, string appName, object providerUserKey, string userName, bool userIsOnline)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {

                if (providerUserKey != null)
                {
                    providerUserKey = MySqlHelpers.GuidConverter(new Guid(providerUserKey.ToString()));
                }
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UserIsOnline", userIsOnline));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
                using (var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
                {                 
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        public DataTable GetUserPasswordInfo(string connectionStringName, string appName, string userName, bool updateUser)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UserIsOnline", updateUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }  
        }

        public DataTable GetUserNameByEmail(string connectionStringName, string appName, string email)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));

                sc.CommandText.AppendObjectQuery("prov_getusernamebyemail", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }      
        }

        public void ResetPassword(string connectionStringName, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Password", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordSalt", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PasswordFormat", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MaxInvalidAttempts", maxInvalidPasswordAttempts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_passwordattemptwindow", passwordAttemptWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_CurrentTimeUtc", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_resetpassword", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        public void UnlockUser(string connectionStringName, string appName, string userName)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));

                sc.CommandText.AppendObjectQuery("prov_unlockuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            } 
        }

        public int UpdateUser(string connectionStringName, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                object providerUserKey = null;
                if (user.ProviderUserKey != null)
                {
                    providerUserKey = MySqlHelpers.GuidConverter(new Guid(user.ProviderUserKey.ToString())).ToString();
                }
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", user.UserName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", user.Email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Comment", user.Comment));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsApproved", user.IsApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastLogin", user.LastLoginDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastActivity", user.LastActivityDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UniqueEmail", requiresUniqueEmail));

                sc.CommandText.AppendObjectQuery("prov_updateuser", connectionStringName);

                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }        
        }
        public void UpgradeMembership(int previousVersion, int newVersion)
        {
            UpgradeMembership(VzfMySqlMembershipProvider.ConnectionStringName, previousVersion, newVersion);
        }

        public void UpgradeMembership(string connectionStringName, int previousVersion, int newVersion)
        {
            using (var sc = new SQLCommand(connectionStringName))
            {
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PreviousVersion", previousVersion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_NewVersion", newVersion));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_upgrade", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }     

        }

    }
}
