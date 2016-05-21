#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DB.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:10 PM.
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

namespace YAF.Providers.Roles
{
    using System;
    using System.Data;  

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;
    using VZF.Data.DAL;

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

        public FbDB()
        {
            //  FbDbAccess.SetConnectionManagerAdapter<VzfFirebirdDBConnManager>();
        }

        /// <summary>
        /// Database Action - Add User to Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public void AddUserToRole(string connectionStringName, object appName, object userName, object roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_role_addusertorole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Database Action - Create Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public void CreateRole(string connectionStringName, object appName, object roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_role_createrole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Database Action - Delete Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Status as integer</returns>
        public int DeleteRole(string connectionStringName, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_APPLICATIONNAME", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_deleteonlyifroleisempty", deleteOnlyIfRoleIsEmpty));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_returnvalue", null, ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("P_role_deleterole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                if (sc.Parameters["@i_returnvalue"].Value == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(sc.Parameters["@i_returnvalue"].Value);
                }
            }          
        }

        /// <summary>
        /// Database Action - Find Users in Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Datatable containing User Information</returns>
        public DataTable FindUsersInRole(string connectionStringName, object appName, object roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_role_findusersinrole", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }       
        }

        /// <summary>
        /// Database Action - Get Roles
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleNames">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public DataTable GetRoles(string connectionStringName, object appName, object username)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", username));             

                sc.CommandText.AppendObjectQuery("P_role_getroles", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }            
        }

        /// <summary>
        /// Database Action - Get Role Exists
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public object GetRoleExists(string connectionStringName, object appName, object roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_role_exists", connectionStringName);
                return sc.ExecuteScalar(CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// Database Action - Add User to Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>DataTable with user information</returns>
        public DataTable IsUserInRole(string connectionStringName, object appName, object userName, object roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_role_isuserinrole", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// Database Action - Remove User From Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public void RemoveUserFromRole(string connectionStringName, object appName, string userName, string roleName)
        {
            // connectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connectionStringName);
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_USERNAME", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "I_ROLENAME", roleName));

                sc.CommandText.AppendObjectQuery("P_ROLE_REMUSERFRROLE", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }
    }
}
