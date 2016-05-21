#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DB.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:12 PM.
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
    /// The my sql db.
    /// </summary>
    public class MySQLDB
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        public static MySQLDB Current
        {
            get
            {
                return PageSingleton<MySQLDB>.Instance;
            }
        }

        /// <summary>
        /// The add user to role.
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
        /// <param name="roleName">
        /// The role name.
        /// </param>
        public void AddUserToRole(string connectionStringName, object appName, object userName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_addusertorole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The create role.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        public void CreateRole(string connectionStringName, object appName, object roleName)
        {           
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_createrole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The delete role.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <param name="deleteOnlyIfRoleIsEmpty">
        /// The delete only if role is empty.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int DeleteRole(string connectionStringName, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeleteOnlyIfRoleIsEmpty", deleteOnlyIfRoleIsEmpty));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ReturnValue", null, ParameterDirection.ReturnValue));

                sc.CommandText.AppendObjectQuery("prov_role_deleterole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                if (sc.Parameters["i_ReturnValue"].Value == DBNull.Value)
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
        /// The find users in role.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable FindUsersInRole(string connectionStringName, object appName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_findusersinrole", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }       
        }

        /// <summary>
        /// The get roles.
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable GetRoles(string connectionStringName, object appName, object userName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));

                sc.CommandText.AppendObjectQuery("prov_role_getroles", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }            
        }

        /// <summary>
        /// The get role exists.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetRoleExists(string connectionStringName, object appName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_exists", connectionStringName);
                return sc.ExecuteScalar(CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// The is user in role.
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
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public DataTable IsUserInRole(string connectionStringName, object appName, object userName, object roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_isuserinrole", connectionStringName);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// The remove user from role.
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
        /// <param name="roleName">
        /// The role name.
        /// </param>
        public void RemoveUserFromRole(string connectionStringName, object appName, string userName, string roleName)
        {
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RoleName", roleName));

                sc.CommandText.AppendObjectQuery("prov_role_removeuserfromrole", connectionStringName);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }
    }
}
