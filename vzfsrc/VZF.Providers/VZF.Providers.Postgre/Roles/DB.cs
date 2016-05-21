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

namespace YAF.Providers.Roles
{
    using System;
    using System.Data;    

    using YAF.Classes;
    using YAF.Core;
    using VZF.Data.DAL;

   

    /// <summary>
    /// The db.
    /// </summary>
    public static class Db
    {
        /// <summary>
        /// Database Action - Add User to Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public static void __AddUserToRole(string connectionStringName, object appName, object userName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_addusertorole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Database Action - Create Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public static void __CreateRole(string connectionStringName, object appName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newroleguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_createrole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Database Action - Delete Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Status as integer</returns>
        public static int __DeleteRole(string connectionStringName, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_deleteonlyifroleisempty", deleteOnlyIfRoleIsEmpty));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_returnvalue", null, ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("prov_role_deleterole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                if (sc.Parameters["i_returnvalue"].Value == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(sc.Parameters["i_ReturnValue"].Value);
                }
            }        
        }

        /// <summary>
        /// Database Action - Find Users in Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Datatable containing User Information</returns>
        public static DataTable __FindUsersInRole(string connectionStringName, object appName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_findusersinrole", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }       
        }

        /// <summary>
        /// Database Action - Get Roles
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleNames">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public static DataTable __GetRoles(string connectionStringName, object appName, object username)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", username));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_getroles", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }            
        }

        /// <summary>
        /// Database Action - Get Role Exists
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public static object __GetRoleExists(string connectionStringName, object appName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_exists", connectionStringName);
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
        public static DataTable __IsUserInRole(string connectionStringName, object appName, object userName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_isuserinrole", connectionStringName);
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
        public static void __RemoveUserFromRole(string connectionStringName, object appName, string userName, string roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_applicationname", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_username", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_rolename", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "i_newroleguid", Guid.NewGuid()));

                sc.CommandText.AppendObjectQuery("prov_role_removeuserfromrole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }
    }
}
