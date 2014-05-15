/* Yet Another Forum.NET
 * Copyright (C) 2006-2008 Jaben Cargman
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
            using (var sc = new SQLCommand(connectionStringName))
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
