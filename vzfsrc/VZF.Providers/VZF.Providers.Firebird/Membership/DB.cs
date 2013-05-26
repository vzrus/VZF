/* VZF by vzrus
 * Copyright (C) 2006-2013 Vladimir Zakharov
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
    using System.Data;
    using System.Web.Security;

    using FirebirdSql.Data.FirebirdClient;

    using VZF.Data.Firebird;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;

    /// <summary>
    /// The vzf firebird membership DB conn manager.
    /// </summary>
    public class VzfFirebirdMembershipDbConnManager : FbDbConnectionManager
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
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
        public void UpgradeMembership(string connectionString, int I_PREVIOUSVERSION, int I_NEWVERSION)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("p_upgrade")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Nonstandard args
                cmd.Parameters.AddWithValue("@I_PREVIOUSVERSION", I_PREVIOUSVERSION);
                cmd.Parameters.AddWithValue("@I_NEWVERSION", I_NEWVERSION);

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        public void ChangePasswordQuestionAndAnswer(string connectionString, string appName, string username, string passwordQuestion, string passwordAnswer)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_CHANGEPASSQUESTIONANDANSWER")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;   
                
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDQUESTION", FbDbType.VarChar)).Value = passwordQuestion;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORDANSWER", FbDbType.VarChar)).Value = passwordAnswer;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        public void CreateUser(string connectionString, string appName, string username, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_CREATEUSER")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;

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
                                       {
                                           Direction =
                                               ParameterDirection
                                               .InputOutput,
                                           Value = providerUserKey
                                       };
                cmd.Parameters.Add(paramUserKey);
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                providerUserKey = FbDbAccess.GetData(cmd, connectionString).Rows[0][0];
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
        public void DeleteUser(string connectionString, string appName, string username, bool deleteAllRelatedData)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_deleteuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@i_deleteallrelated", FbDbType.Boolean)).Value = deleteAllRelatedData;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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

                return FbDbAccess.GetData(cmd, connectionString);
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
        public DataTable FindUsersByName(string connectionString, string appName, string usernameToMatch, int pageIndex, int pageSize)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_FINDUSERSBYNAME")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = usernameToMatch;

                // TODO:fix overflow bug
                cmd.Parameters.Add(new FbParameter("@i_pageindex", FbDbType.Integer)).Value = pageIndex;
                if (pageSize == int.MaxValue)
                {
                    pageSize = 1;
                }

                cmd.Parameters.Add(new FbParameter("@i_pagesize", FbDbType.Integer)).Value = pageSize;

                return FbDbAccess.GetData(cmd, connectionString);
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
        public DataTable GetAllUsers(string connectionString, string appName, int pageIndex, int pageSize)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_getallusers")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@i_pageindex", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@i_pagesize", FbDbType.Integer)).Value = pageSize;

                return FbDbAccess.GetData(cmd, connectionString);
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
        public int GetNumberOfUsersOnline(string connectionString, string appName, int TimeWindow)
        {
            using ( var cmd = new FbCommand( FbDbAccess.GetObjectName( "P_getnumberofusersonline" ) ) )
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_TIMEWINDOW", FbDbType.Integer)).Value = TimeWindow;
                cmd.Parameters.Add(new FbParameter("@i_currenttimeutc", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                var p = new FbParameter("@i_returnvalue", DbType.Int32) { Direction = ParameterDirection.ReturnValue };
                cmd.Parameters.Add(p);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return Convert.ToInt32(cmd.Parameters["@i_returnvalue"].Value);
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
        public DataRow GetUser(string connectionString, string appName, object providerUserKey, string userName, bool userIsOnline)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_GETUSER")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;

                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;               
                cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value = providerUserKey;
                cmd.Parameters.Add(new FbParameter("@I_USERISONLINE", FbDbType.Boolean)).Value = Convert.ToInt32(userIsOnline);
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                using (var dt = FbDbAccess.GetData(cmd, connectionString))
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
        public DataTable GetUserPasswordInfo(string connectionString, string appName, string username, bool updateUser)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_getuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;
                
                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = username;
                cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value = DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_USERISONLINE", FbDbType.Boolean)).Value = updateUser;                
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
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
        public DataTable GetUserNameByEmail(string connectionString, string appName, string email)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_GETUSERNAMEBYEMAL")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;

                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;

                return FbDbAccess.GetData(cmd, connectionString);
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
        public void ResetPassword(string connectionString, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_resetpassword")))
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

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        public void UnlockUser(string connectionString, string appName, string userName)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_unlockuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_APPLICATIONNAME", FbDbType.VarChar)).Value = appName;

                // Nonstandard args
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        public int UpdateUser(string connectionString, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_updateuser")))
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

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }
    }
}
