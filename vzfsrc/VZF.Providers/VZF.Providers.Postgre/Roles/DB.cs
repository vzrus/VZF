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
