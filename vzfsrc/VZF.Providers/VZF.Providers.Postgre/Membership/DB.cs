#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DB.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:13 PM.
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

namespace YAF.Providers.Membership
{
    using System;
    using System.Data;
    using System.Web.Security;    

    using YAF.Classes;
    using YAF.Core;
    using VZF.Data.DAL;


    /// <summary>
    /// The db.
    /// </summary>
    public static  class Db
    {   
        public static void __ChangePassword(string connectionStringName, string appName, string userName, string newPassword, string newSalt, int passwordFormat, string newPasswordAnswer)
        {           
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_password", newPassword));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordsalt", newSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordformat", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordanswer", newPasswordAnswer));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_changepassword", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }             
        }

        public static void __ChangePasswordQuestionAndAnswer(string connectionStringName, string appName, string userName, string passwordQuestion, string passwordAnswer)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                //  sc.DataSource.ProviderName
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordquestion", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordanswer", passwordAnswer));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_changepasswordquestionandanswer", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }     
        }

        public static void __CreateUser(string connectionStringName, string appName, string userName, string password, string passwordSalt, int passwordFormat, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {          
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_password", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordsalt", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordformat", passwordFormat.ToString()));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_email", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordquestion", passwordQuestion));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordanswer", passwordAnswer));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isapproved", isApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newuserkey", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_utctimestamp", DateTime.UtcNow));

                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userkey", providerUserKey, ParameterDirection.InputOutput));               

                sc.CommandText.AppendObjectQuery("prov_createuser", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure, true);
                providerUserKey = sc.Parameters["i_userkey"].Value;
            }     
        }

        public static void __DeleteUser(string connectionStringName, string appName, string userName, bool deleteAllRelatedData)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_deleteallrelated", deleteAllRelatedData));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_deleteuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }  
        }

        public static DataTable __FindUsersByEmail(string connectionStringName, string appName, string emailToMatch, int pageIndex, int pageSize)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_emailaddress", emailToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_findusersbyemail", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }    
        }

        public static DataTable __FindUsersByName(string connectionStringName, string appName, string usernameToMatch, int pageIndex, int pageSize)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", usernameToMatch));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_findusersbyname", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }    
        }

        public static DataTable __GetAllUsers(string connectionStringName, string appName, int pageIndex, int pageSize)
        {            
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
             
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pageindex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_pagesize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_getallusers", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }    
        }

        public static int __GetNumberOfUsersOnline(string connectionStringName, string appName, int TimeWindow)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_timewindow", TimeWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_currenttimeutc", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_returnvalue", null, ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("prov_getnumberofusersonline", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);

                return Convert.ToInt32(sc.Parameters["i_returnvalue"].Value);
            }  
        }

        public static DataRow __GetUser(string connectionStringName, string appName, object providerUserKey, string userName, bool userIsOnline)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userkey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_userisonline", userIsOnline));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_utctimestamp", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
                using (var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
                {
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }

        }

        public static DataTable __GetUserPasswordInfo(string connectionStringName, string appName, string userName, bool updateUser)
        {
           
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userkey", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_userisonline", updateUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_utctimestamp", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            } 

        }

        public static DataTable __GetUserNameByEmail(string connectionStringName, string appName, string email)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_email", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_getusernamebyemail", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }      
        }


        public static void __ResetPassword(string connectionStringName, string appName, string userName, string password, string passwordSalt, int passwordFormat, int maxInvalidPasswordAttempts, int passwordAttemptWindow)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_password", password));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordsalt", passwordSalt));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_passwordformat", passwordFormat));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_maxinvalidattempts", maxInvalidPasswordAttempts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_passwordattemptwindow", passwordAttemptWindow));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_currenttimeutc", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_resetpassword", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        public static void __UnlockUser(string connectionStringName, string appName, string userName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_unlockuser", connectionStringName);

                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            } 
        }

        public static int __UpdateUser(string connectionStringName, object appName, MembershipUser user, bool requiresUniqueEmail)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {              
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_userkey", user.ProviderUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", user.UserName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_email", user.Email));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_comment", user.Comment));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isapproved", user.IsApproved));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastlogin", user.LastLoginDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_lastactivity", user.LastActivityDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_uniqueemail", requiresUniqueEmail));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_updateuser", connectionStringName);

                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }        
        }

        public static void UpgradeMembership(string connectionStringName, int previousVersion, int newVersion)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
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
