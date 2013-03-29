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

    using Npgsql;

    using VZF.Data.Postgre;

    using YAF.Classes;
    using YAF.Core;

    /// <summary>
    /// The yaf roles db conn manager.
    /// </summary>
    public static class YafRolesDBConnManager
    {
        public static string ConnectionString
        {
            get
            {
                if (YafContext.Application[PgRoleProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[PgRoleProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

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
        public static void __AddUserToRole(string connectionString, object appName, object userName, object roleName)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_addusertorole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Database Action - Create Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public static void __CreateRole(string connectionString, object appName, object roleName)
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_createrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_newroleguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Delete Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Status as integer</returns>
        public static int __DeleteRole(string connectionString, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_deleterole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_deleteonlyifroleisempty", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = deleteOnlyIfRoleIsEmpty;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
               
                var p = new NpgsqlParameter("i_returnvalue", NpgsqlTypes.NpgsqlDbType.Integer);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                if (p.Value == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(cmd.Parameters["i_returnvalue"].Value);
            }
        }

        /// <summary>
        /// Database Action - Find Users in Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Datatable containing User Information</returns>
        public static DataTable __FindUsersInRole(string connectionString, object appName, object roleName)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_findusersinrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Database Action - Get Roles
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleNames">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public static DataTable __GetRoles(string connectionString, object appName, object username)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_getroles")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = username;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Get Role Exists
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public static object __GetRoleExists(string connectionString, object appName, object roleName)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_exists")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        /// <summary>
        /// Database Action - Add User to Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>DataTable with user information</returns>
        public static DataTable __IsUserInRole(string connectionString, object appName, object userName, object roleName)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_isuserinrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                return PostgreDbAccess.GetData(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Remove User From Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public static void __RemoveUserFromRole(string connectionString, object appName, string userName, string roleName)
        {
            using (var cmd = new NpgsqlCommand(PostgreDbAccess.GetObjectName("prov_role_removeuserfromrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_applicationname", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = appName;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolename", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = roleName;
                cmd.Parameters.Add(new NpgsqlParameter("i_newroleguid", NpgsqlTypes.NpgsqlDbType.Uuid)).Value = Guid.NewGuid();

                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }
    }
}
