/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
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
  #region Using

  using System;
  using System.Data;

  using YAF.Classes;
  using YAF.Classes.Pattern;
  using YAF.Core; using YAF.Types.Interfaces; using YAF.Types.Constants;
  
  using YAF.Types;
    using VZF.Data.DAL;

  #endregion 

  /// <summary>
  /// The db.
  /// </summary>
  public class DB
  {
    #region Constants and Fields

    /// <summary>
    ///   The _db access.
    /// </summary>
    // private readonly MsSqlDbAccess _msSqlDbAccess = new MsSqlDbAccess();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "DB" /> class.
    /// </summary>
    public DB()
    {
      // MsSqlDbAccess.SetConnectionManagerAdapter<MsSqlRolesDbConnectionManager>();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets Current.
    /// </summary>
    public static DB Current
    {
      get
      {
        return PageSingleton<DB>.Instance;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Database Action - Add User to Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="userName">
    /// User Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    public void AddUserToRole(string connectionStringName, object appName, object userName, object roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_addusertorole", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      }
    }

    /// <summary>
    /// Database Action - Create Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    public void CreateRole(string connectionStringName, object appName, object roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_createrole", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      }
    }

    /// <summary>
    /// Database Action - Delete Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    /// <param name="deleteOnlyIfRoleIsEmpty">
    /// The delete Only If Role Is Empty.
    /// </param>
    /// <returns>
    /// Status as integer
    /// </returns>
    public int DeleteRole(string connectionStringName, object appName, object roleName, object deleteOnlyIfRoleIsEmpty)
    {   
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@DeleteOnlyIfRoleIsEmpty", deleteOnlyIfRoleIsEmpty));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@ReturnValue", null, ParameterDirection.ReturnValue));

          sc.CommandText.AppendObjectQuery("prov_role_deleterole", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
          return Convert.ToInt32(sc.Parameters["@ReturnValue"].Value);         
      }        
    }

    /// <summary>
    /// Database Action - Find Users in Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    /// <returns>
    /// Datatable containing User Information
    /// </returns>
    public DataTable FindUsersInRole(string connectionStringName, object appName, object roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_findusersinrole", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }       
    }

    /// <summary>
    /// Database Action - Get Role Exists
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    /// <returns>
    /// Database containing Role Information
    /// </returns>
    public object GetRoleExists(string connectionStringName, object appName, object roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_exists", connectionStringName);
          return sc.ExecuteScalar(CommandType.StoredProcedure, false);
      }
    }

    /// <summary>
    /// Database Action - Get Roles
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <returns>
    /// Database containing Role Information
    /// </returns>
    public DataTable GetRoles(string connectionStringName, object appName, object userName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));

          sc.CommandText.AppendObjectQuery("prov_role_getroles", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }     
    }

    /// <summary>
    /// Database Action - Add User to Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="userName">
    /// User Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    /// <returns>
    /// DataTable with user information
    /// </returns>
    public DataTable IsUserInRole(string connectionStringName, object appName, object userName, object roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_isuserinrole", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }
    }

    /// <summary>
    /// Database Action - Remove User From Role
    /// </summary>
    /// <param name="appName">
    /// Application Name
    /// </param>
    /// <param name="userName">
    /// User Name
    /// </param>
    /// <param name="roleName">
    /// Role Name
    /// </param>
    public void RemoveUserFromRole(string connectionStringName, object appName, string userName, string roleName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@RoleName", roleName));

          sc.CommandText.AppendObjectQuery("prov_role_removeuserfromrole", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      }
    }

    #endregion
  }
}