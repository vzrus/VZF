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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
