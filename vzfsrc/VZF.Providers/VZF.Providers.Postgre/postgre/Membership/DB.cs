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

namespace YAF.Providers.Membership
{
    using System;
    using System.Data;
    using System.Web.Security;

    using Npgsql;

    using NpgsqlTypes;

    using VZF.Data.Postgre;

    using YAF.Classes;
    using YAF.Core;

    /// <summary>
    /// The pg membership db conn manager.
    /// </summary>
    public static class PgMembershipDBConnManager 
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if  (YafContext.Application[PgMembershipProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[PgMembershipProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

    public static  class Db
    {
        public static void UpgradeMembership(int previousVersion, int newVersion)
        {
            UpgradeMembership(string.Empty, previousVersion, newVersion);
        }

        public static void UpgradeMembership(string connectionString, int previousVersion, int newVersion)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_upgrade")))
            {
                /*
                cmd.CommandType = CommandType.StoredProcedure;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_previousversion", NpgsqlDbType.Integer)).Value = previousVersion;
                cmd.Parameters.Add(new NpgsqlParameter("i_newversion", NpgsqlDbType.Integer)).Value = newVersion;

                PostgreDBAccess.ExecuteNonQuery(cmd,connectionString ); */
            }

        }

        public static void __ChangePassword(string connectionString, string appName, string username, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_changepassword")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;

                // Nonstandard args

                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_password", NpgsqlDbType.Varchar)).Value = newPassword;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordsalt", NpgsqlDbType.Varchar)).Value = newSalt;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordformat", NpgsqlDbType.Varchar)).Value = passwordFormat;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordanswer", NpgsqlDbType.Varchar)).Value = newPasswordAnswer;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        public static void __ChangePasswordQuestionAndAnswer(string connectionString, string appName, string username, string passwordQuestion, string passwordAnswer)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_changepasswordquestionandanswer")))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordquestion", NpgsqlDbType.Varchar)).Value = passwordQuestion;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordanswer", NpgsqlDbType.Varchar)).Value = passwordAnswer;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        public static void __CreateUser(string connectionString, string appName, string username, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_createuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Input Parameters
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_password", NpgsqlDbType.Varchar)).Value = password;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordsalt", NpgsqlDbType.Varchar)).Value = passwordSalt;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordformat", NpgsqlDbType.Varchar)).Value = passwordFormat;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordquestion", NpgsqlDbType.Varchar)).Value = passwordQuestion;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordanswer", NpgsqlDbType.Varchar)).Value = passwordAnswer;
                cmd.Parameters.Add(new NpgsqlParameter("i_isapproved", NpgsqlDbType.Boolean)).Value = isApproved;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_newuserkey", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.TimestampTZ)).Value = DateTime.UtcNow;
                // Input Output Parameters
                var paramUserKey = new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)
                    {Direction = ParameterDirection.InputOutput, Value = providerUserKey};
                cmd.Parameters.Add(paramUserKey);
                

                //Execute
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
                //Retrieve Output Parameters
                providerUserKey = paramUserKey.Value;

            }
        }

        public static void __DeleteUser(string connectionString, string appName, string username, bool deleteAllRelatedData)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_deleteuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args

                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_deleteallrelated", NpgsqlDbType.Boolean)).Value = deleteAllRelatedData;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        public static DataTable __FindUsersByEmail(string connectionString, string appName, string emailToMatch, int pageIndex, int pageSize)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_findusersbyemail")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_emailaddress", NpgsqlDbType.Varchar)).Value = emailToMatch;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }

        public static DataTable __FindUsersByName(string connectionString, string appName, string usernameToMatch, int pageIndex, int pageSize)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_findusersbyname")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = usernameToMatch;
                //TODO:fix overflow bug                
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                if (pageSize == int.MaxValue) { pageSize = 1; }
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;

                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }

        public static DataTable __GetAllUsers(string connectionString, string appName, int pageIndex, int pageSize)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_getallusers")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }

        public static int __GetNumberOfUsersOnline(string connectionString, string appName, int TimeWindow)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_getnumberofusersonline")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args

                cmd.Parameters.Add(new NpgsqlParameter("i_timewindow", NpgsqlDbType.Integer)).Value = TimeWindow;
                cmd.Parameters.Add(new NpgsqlParameter("i_currenttimeutc", NpgsqlDbType.Timestamp)).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                NpgsqlParameter p = new NpgsqlParameter("i_returnvalue", DbType.Int32);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
                return Convert.ToInt32(cmd.Parameters["i_returnvalue"].Value);
            }
        }

        public static DataRow __GetUser(string connectionString, string appName, object providerUserKey, string userName, bool userIsOnline)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_getuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)).Value = providerUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_userisonline", NpgsqlDbType.Boolean)).Value = userIsOnline;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.TimestampTZ)).Value = DateTime.UtcNow;

                using (DataTable dt = PostgreDbAccess.GetData(cmd,connectionString ))
                {
                    if (dt.Rows.Count > 0)
                    {

                        return dt.Rows[0];
                    }
                    else
                        return null;
                }
            }

        }

        public static DataTable __GetUserPasswordInfo(string connectionString, string appName, string username, bool updateUser)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_getuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)).Value = DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_userisonline", NpgsqlDbType.Boolean)).Value = updateUser;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.TimestampTZ)).Value = DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd,connectionString );
            }

        }

        public static DataTable __GetUserNameByEmail(string connectionString, string appName, string email)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_getusernamebyemail")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_newroleguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }


        public static void __ResetPassword(string connectionString, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_resetpassword")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_password", NpgsqlDbType.Varchar)).Value = password;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordsalt", NpgsqlDbType.Varchar)).Value = passwordSalt;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordformat", NpgsqlDbType.Varchar)).Value = passwordFormat;
                cmd.Parameters.Add(new NpgsqlParameter("i_maxinvalidattempts", NpgsqlDbType.Integer)).Value = maxInvalidPasswordAttempts;
                cmd.Parameters.Add(new NpgsqlParameter("i_passwordattemptwindow", NpgsqlDbType.Integer)).Value = passwordAttemptWindow;
                cmd.Parameters.Add(new NpgsqlParameter("i_currenttimeutc", NpgsqlDbType.Timestamp)).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new NpgsqlParameter("i_newroleguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }

        }

        public static void __UnlockUser(string connectionString, string appName, string userName)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_unlockuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        public static int __UpdateUser(string connectionString, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_updateuser")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlDbType.Varchar)).Value = appName;
                // Nonstandard args
                cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)).Value = user.ProviderUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = user.UserName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = user.Email;
                cmd.Parameters.Add(new NpgsqlParameter("i_comment", NpgsqlDbType.Text)).Value = user.Comment;
                cmd.Parameters.Add(new NpgsqlParameter("i_isapproved", NpgsqlDbType.Boolean)).Value = user.IsApproved;
                cmd.Parameters.Add(new NpgsqlParameter("i_lastlogin", NpgsqlDbType.Timestamp)).Value = user.LastLoginDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_lastactivity", NpgsqlDbType.Timestamp)).Value = user.LastActivityDate.ToUniversalTime();
                cmd.Parameters.Add(new NpgsqlParameter("i_uniqueemail", NpgsqlDbType.Boolean)).Value = requiresUniqueEmail;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }


    }
}
