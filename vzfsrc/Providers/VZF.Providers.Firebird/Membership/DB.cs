/* VZF by vzrus
 * Copyright (C) 2006-2014 Vladimir Zakharov
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

namespace YAF.Providers.Membership
{
    using System;
    using System.Text;
    using System.Data;
    using System.Web.Security;   

    using VZF.Data.DAL;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;

    /// <summary>
    /// The fb db.
    /// </summary>
    public class FbDB
    {
        // private FbDbAccess FbDbAccess = new FbDbAccess();

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
        /// The upgrade membership.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="I_PREVIOUSVERSION">
        /// The previous version.
        /// </param>
        /// <param name="I_NEWVERSION">
        /// The new version.
        /// </param>
        public void UpgradeMembership(string connectionStringName, int I_PREVIOUSVERSION, int I_NEWVERSION)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PREVIOUSVERSION", I_PREVIOUSVERSION));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_NEWVERSION", I_NEWVERSION));

                sc.CommandText.AppendObjectQuery("p_upgrade", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }            
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <param name="newSalt">
        /// The new salt.
        /// </param>
        /// <param name="passwordFormat">
        /// The password format.
        /// </param>
        /// <param name="newPasswordAnswer">
        /// The new password answer.
        /// </param>
        public void ChangePassword(string connectionStringName, string appName, string username, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORD", newPassword));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDSALT", newSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDFORMAT", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDANSWER", newPasswordAnswer));
             
                sc.CommandText.AppendObjectQuery("P_changepassword", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }          
        }

        /// <summary>
        /// The change password question and answer.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="passwordQuestion">
        /// The password question.
        /// </param>
        /// <param name="passwordAnswer">
        /// The password answer.
        /// </param>
        public void ChangePasswordQuestionAndAnswer(string connectionStringName, string appName, string username, string passwordQuestion, string passwordAnswer)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDQUESTION", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDANSWER", passwordAnswer));
              
                sc.CommandText.AppendObjectQuery("P_CHANGEPASSQUESTIONANDANSWER", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }          
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="passwordSalt">
        /// The password salt.
        /// </param>
        /// <param name="passwordFormat">
        /// The password format.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="passwordQuestion">
        /// The password question.
        /// </param>
        /// <param name="passwordAnswer">
        /// The password answer.
        /// </param>
        /// <param name="isApproved">
        /// The is approved.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        public void CreateUser(string connectionStringName, string appName, string username, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
           
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORD", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDSALT", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDFORMAT", passwordFormat.ToString()));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_EMAIL", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDQUESTION", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_PASSWORDANSWER", passwordAnswer));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "I_ISAPPROVED", isApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERKEY", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("P_CREATEUSER", connectionStringName);
                providerUserKey = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false).Rows[0][0];                
            }      
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="deleteAllRelatedData">
        /// The delete all related data.
        /// </param>
        public void DeleteUser(string connectionStringName, string appName, string username, bool deleteAllRelatedData)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
             
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_deleteallrelated", deleteAllRelatedData));               

                sc.CommandText.AppendObjectQuery("P_deleteuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }            
        }

        /// <summary>
        /// The find users by email.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="emailToMatch">
        /// The email to match.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable FindUsersByEmail(string connectionStringName, string appName, string emailToMatch, int pageIndex, int pageSize)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_emailaddress", emailToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize));
               

                sc.CommandText.AppendObjectQuery("P_FINDUSERSBYEMAIL", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }          
        }

        /// <summary>
        /// The find users by name.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="usernameToMatch">
        /// The username to match.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable FindUsersByName(string connectionStringName, string appName, string usernameToMatch, int pageIndex, int pageSize)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", usernameToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));

                // TODO:fix overflow bug
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize == int.MaxValue ? 1 : pageSize));

                sc.CommandText.AppendObjectQuery("P_FINDUSERSBYNAME", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }      
        }

        /// <summary>
        /// The get all users.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetAllUsers(string connectionStringName, string appName, int pageIndex, int pageSize)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));            
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));
                // TODO:fix overflow bug
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize == int.MaxValue ? 1 : pageSize));

                sc.CommandText.AppendObjectQuery("P_getallusers", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }  
        }

        /// <summary>
        /// The get number of users online.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="TimeWindow">
        /// The time window.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetNumberOfUsersOnline(string connectionStringName, string appName, int TimeWindow)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "I_TIMEWINDOW", TimeWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_currenttimeutc", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_returnvalue", null, ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("P_getnumberofusersonline", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);

                return Convert.ToInt32(sc.Parameters["@i_returnvalue"].Value);
            }
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userIsOnline">
        /// The user is online.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        public DataRow GetUser(string connectionStringName, string appName, object providerUserKey, string userName, bool userIsOnline)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {

              //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERKEY", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "I_USERISONLINE", Convert.ToInt32(userIsOnline)));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));
               
                sc.CommandText.AppendObjectQuery("P_GETUSER", connectionStringName);
                using (var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
                {
                    var dr = dt.Rows[0];
                    return dr["UserID"] != DBNull.Value ? dt.Rows[0] : null;
                }
            }           
        }

        /// <summary>
        /// The get user password info.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="updateUser">
        /// The update user.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetUserPasswordInfo(string connectionStringName, string appName, string username, bool updateUser)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERKEY", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "I_USERISONLINE", updateUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_UTCTIMESTAMP", DateTime.UtcNow));              

                sc.CommandText.AppendObjectQuery("P_getuser", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }             
        }

        /// <summary>
        /// The get user name by email.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetUserNameByEmail(string connectionStringName, string appName, string email)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_EMAIL", email));             

                sc.CommandText.AppendObjectQuery("P_GETUSERNAMEBYEMAL", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }  
        }

        /// <summary>
        /// The reset password.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="passwordSalt">
        /// The password salt.
        /// </param>
        /// <param name="passwordFormat">
        /// The password format.
        /// </param>
        /// <param name="maxInvalidPasswordAttempts">
        /// The max invalid password attempts.
        /// </param>
        /// <param name="passwordAttemptWindow">
        /// The password attempt window.
        /// </param>
        public void ResetPassword(string connectionStringName, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_password", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordsalt", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordformat", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_maxinvalidattempts", maxInvalidPasswordAttempts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_passwordattemptwindow", passwordAttemptWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_currenttimeutc", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("P_resetpassword", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            } 
        }

        /// <summary>
        /// The unlock user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        public void UnlockUser(string connectionStringName, string appName, string userName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));                

                sc.CommandText.AppendObjectQuery("P_unlockuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            } 
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="requiresUniqueEmail">
        /// The requires unique email.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UpdateUser(string connectionStringName, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_userkey", user.ProviderUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", user.UserName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_EMAIL", user.Email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_COMMENT", user.Comment));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isapproved", user.IsApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_LASTLOGIN", user.LastLoginDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "I_LASTACTIVITY", user.LastActivityDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "I_UNIQUEEMAIL", requiresUniqueEmail));

                sc.CommandText.AppendObjectQuery("P_updateuser", connectionStringName);

                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }             
        }
    }
}
