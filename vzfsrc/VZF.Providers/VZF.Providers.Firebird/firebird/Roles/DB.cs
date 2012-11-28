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

    using FirebirdSql.Data.FirebirdClient;

    using VZF.Data.Firebird;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core;

    public class VzfFirebirdDBConnManager : FbDbConnectionManager
    {
        public override string ConnectionString
        {
            get
            {
                if (YafContext.Application[VzfFirebirdRoleProvider.ConnStrAppKeyName] != null)
                {
                    return YafContext.Application[VzfFirebirdRoleProvider.ConnStrAppKeyName] as string;
                }

                return Config.ConnectionString;
            }
        }
    }

    public class FbDB
    {
       // private FbDbAccess FbDbAccess = new FbDbAccess();

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
        public void AddUserToRole(string connectionString, object appName, object userName, object roleName)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_addusertorole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar)).Value = appName;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar)).Value = roleName; 
               
                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Create Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public void CreateRole(string connectionString, object appName, object roleName)
        {
            using (var cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_createrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = roleName;

               
                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
                int i = 1;
            }
        }

        /// <summary>
        /// Database Action - Delete Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Status as integer</returns>
        public int DeleteRole(string connectionString, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
        {
            using (FbCommand cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_deleterole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = roleName;

                cmd.Parameters.Add(new FbParameter("@i_deleteonlyifroleisempty", FbDbType.Boolean));
                cmd.Parameters[2].Value = deleteOnlyIfRoleIsEmpty;

                FbParameter p = new FbParameter("@i_returnvalue", FbDbType.Integer);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);

                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
                if (p.Value == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(cmd.Parameters["@i_returnvalue"].Value);
                }
                }
        }

        /// <summary>
        /// Database Action - Find Users in Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>Datatable containing User Information</returns>
        public DataTable FindUsersInRole(string connectionString, object appName, object roleName)
        {
            using (FbCommand cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_findusersinrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = roleName;
                
                return FbDbAccess.GetData(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Get Roles
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="roleNames">Role Name</param>
        /// <returns>Database containing Role Information</returns>
        public DataTable GetRoles(string connectionString, object appName, object username)
        {
            using (FbCommand cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_getroles")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = username;

                
                return FbDbAccess.GetData(cmd,connectionString );
            }
        }

				/// <summary>
				/// Database Action - Get Role Exists
				/// </summary>
				/// <param name="appName">Application Name</param>
				/// <param name="roleName">Role Name</param>
				/// <returns>Database containing Role Information</returns>
        public object GetRoleExists(string connectionString, object appName, object roleName)
				{
					using ( FbCommand cmd = new FbCommand( FbDbAccess.GetObjectName( "P_role_exists" ) ) )
					{
						cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                        cmd.Parameters[0].Value = appName;
                       
                        cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                        cmd.Parameters[1].Value = roleName;
						
                        return FbDbAccess.ExecuteScalar(cmd,connectionString );
					}
				}

        /// <summary>
        /// Database Action - Add User to Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns>DataTable with user information</returns>
        public  DataTable IsUserInRole(string connectionString,object appName, object userName, object roleName)
        {
            using (FbCommand cmd = new FbCommand(FbDbAccess.GetObjectName("P_role_isuserinrole")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;

                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = userName;

                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                cmd.Parameters[2].Value = roleName;
               
                return FbDbAccess.GetData(cmd,connectionString );
            }
        }

        /// <summary>
        /// Database Action - Remove User From Role
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        /// <returns></returns>
        public void RemoveUserFromRole(string connectionString, object appName, string userName, string roleName)
        {
            using (FbCommand cmd = new FbCommand(FbDbAccess.GetObjectName("P_ROLE_REMUSERFRROLE")))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                               
                cmd.Parameters.Add(new FbParameter("@i_applicationname", FbDbType.VarChar));
                cmd.Parameters[0].Value = appName;
                
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar));
                cmd.Parameters[1].Value = userName;

                cmd.Parameters.Add(new FbParameter("@I_ROLENAME", FbDbType.VarChar));
                cmd.Parameters[2].Value = roleName;
                
                FbDbAccess.ExecuteNonQuery(cmd,connectionString );
            }

        }
    }
}
