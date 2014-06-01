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
namespace YAF.Providers.Membership
{
  #region Using

  using System;
  using System.Data;
  using System.Web.Security;

  using YAF.Classes;
  using YAF.Classes.Pattern;
  using YAF.Core; 
  using YAF.Types.Interfaces; 
  using YAF.Types.Constants;
  
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
      // MsSqlDbAccess.SetConnectionManagerAdapter<MsSqlMembershipDbConnectionManager>();
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
    /// The change password.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="newPassword">
    /// The new password.
    /// </param>
    /// <param name="newSalt">
    /// The new salt.
    /// </param>
    /// <param name="passwordFormat">
    /// The password format.
    /// </param>
    /// <param name="newPasswordAnswer">
    /// The new password answer.
    /// </param>
    public void ChangePassword(string connectionStringName, [NotNull] string appName, [NotNull] string userName, [NotNull] string newPassword, [NotNull] string newSalt, int passwordFormat, [NotNull] string newPasswordAnswer)
    {
        using (var sc = new VzfSqlCommand(connectionStringName))
        {
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
            //  sc.DataSource.ProviderName
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Password", newPassword));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordSalt", newSalt));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordFormat", passwordFormat));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordAnswer", newPasswordAnswer));

            sc.CommandText.AppendObjectQuery("prov_changepassword", connectionStringName);
            sc.ExecuteNonQuery(CommandType.StoredProcedure);
        }
    }

    /// <summary>
    /// The change password question and answer.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="passwordQuestion">
    /// The password question.
    /// </param>
    /// <param name="passwordAnswer">
    /// The password answer.
    /// </param>
    public void ChangePasswordQuestionAndAnswer(string connectionStringName, [NotNull] string appName, [NotNull] string userName, [NotNull] string passwordQuestion, [NotNull] string passwordAnswer)
    {
        using (var sc = new VzfSqlCommand(connectionStringName))
        {
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
            //  sc.DataSource.ProviderName
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Username", userName));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordQuestion", passwordQuestion));
            sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordAnswer", passwordAnswer));

            sc.CommandText.AppendObjectQuery("prov_changepasswordquestionandanswer", connectionStringName);
            sc.ExecuteNonQuery(CommandType.StoredProcedure);
        }
    }

    /// <summary>
    /// The create user.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <param name="passwordSalt">
    /// The password salt.
    /// </param>
    /// <param name="passwordFormat">
    /// The password format.
    /// </param>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <param name="passwordQuestion">
    /// The password question.
    /// </param>
    /// <param name="passwordAnswer">
    /// The password answer.
    /// </param>
    /// <param name="isApproved">
    /// The is approved.
    /// </param>
    /// <param name="providerUserKey">
    /// The provider user key.
    /// </param>
    public void CreateUser(string connectionStringName, [NotNull] string appName, [NotNull] string userName, [NotNull] string password, [NotNull] string passwordSalt, 
      int passwordFormat, [NotNull] string email, [NotNull] string passwordQuestion, [NotNull] string passwordAnswer, 
      bool isApproved, [NotNull] object providerUserKey)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));

          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Username", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Password", password));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordSalt", passwordSalt));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordFormat", passwordFormat.ToString()));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Email", email));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordQuestion", passwordQuestion));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordAnswer", passwordAnswer));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@IsApproved", isApproved));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@UTCTIMESTAMP", DateTime.UtcNow));

          var paramUserKey = sc.CreateParameter(DbType.Guid, "@UserKey", providerUserKey, ParameterDirection.InputOutput);
          sc.Parameters.Add(paramUserKey);

          sc.CommandText.AppendObjectQuery("prov_createuser", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure, true);
          providerUserKey = paramUserKey.Value;
      }     
    }

    /// <summary>
    /// The delete user.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="deleteAllRelatedData">
    /// The delete all related data.
    /// </param>
    public void DeleteUser(string connectionStringName, [NotNull] string appName, [NotNull] string userName, bool deleteAllRelatedData)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@DeleteAllRelated", deleteAllRelatedData));

          sc.CommandText.AppendObjectQuery("prov_deleteuser", connectionStringName);

          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      } 
    }

    /// <summary>
    /// The find users by email.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="emailToMatch">
    /// The email to match.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <returns>
    /// </returns>
    public DataTable FindUsersByEmail(string connectionStringName, [NotNull] string appName, [NotNull] string emailToMatch, int pageIndex, int pageSize)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));

          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@EmailAddress", emailToMatch));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageIndex", pageIndex));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageSize", pageSize));        

          sc.CommandText.AppendObjectQuery("prov_findusersbyemail", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }    
    }

    /// <summary>
    /// The find users by name.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="usernameToMatch">
    /// The username to match.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <returns>
    /// </returns>
    public DataTable FindUsersByName(string connectionStringName, [NotNull] string appName, [NotNull] string usernameToMatch, int pageIndex, int pageSize)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));

          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", usernameToMatch));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageIndex", pageIndex));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageSize", pageSize));         

          sc.CommandText.AppendObjectQuery("prov_findusersbyname", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }     
    }

    /// <summary>
    /// The get all users.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <returns>
    /// </returns>
    public DataTable GetAllUsers(string connectionStringName, [NotNull] string appName, int pageIndex, int pageSize)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageIndex", pageIndex));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PageSize", pageSize));          

          sc.CommandText.AppendObjectQuery("prov_getallusers", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }    
    }

    /// <summary>
    /// The get number of users online.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="timeWindow">
    /// The time window.
    /// </param>
    /// <returns>
    /// The get number of users online.
    /// </returns>
    public int GetNumberOfUsersOnline(string connectionStringName, [NotNull] string appName, int timeWindow)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@TimeWindow", timeWindow));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@CurrentTimeUtc", DateTime.UtcNow));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@ReturnValue", null, ParameterDirection.ReturnValue));

          sc.CommandText.AppendObjectQuery("prov_getnumberofusersonline", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);

          return Convert.ToInt32(sc.Parameters["@ReturnValue"].Value);
      }  
    }

    /// <summary>
    /// The get user.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="providerUserKey">
    /// The provider user key.
    /// </param>
    /// <param name="userName">
    /// The user name.
    /// </param>
    /// <param name="userIsOnline">
    /// The user is online.
    /// </param>
    /// <returns>
    /// </returns>
    public DataRow GetUser(string connectionStringName, [NotNull] string appName, [NotNull] object providerUserKey, [NotNull] string userName, bool userIsOnline)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          var providerUserKeyNew = providerUserKey != null ? providerUserKey.ToString() : null;

          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserKey", providerUserKeyNew));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@UserIsOnline", userIsOnline));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@UTCTIMESTAMP", DateTime.UtcNow));

          sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
          using (var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
          {
              return dt.Rows.Count > 0 ? dt.Rows[0] : null;
          }
      }
    }

    /// <summary>
    /// The get user name by email.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <returns>
    /// </returns>
    public DataTable GetUserNameByEmail(string connectionStringName, [NotNull] string appName, [NotNull] string email)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Email", email));

          sc.CommandText.AppendObjectQuery("prov_getusernamebyemail", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }      
    }

    /// <summary>
    /// The get user password info.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="updateUser">
    /// The update user.
    /// </param>
    /// <returns>
    /// </returns>
    public DataTable GetUserPasswordInfo(string connectionStringName, [NotNull] string appName, [NotNull] string userName, bool updateUser)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName)); 
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@UserIsOnline", updateUser));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@UTCTIMESTAMP", DateTime.UtcNow));

          sc.CommandText.AppendObjectQuery("prov_getuser", connectionStringName);
          return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
      }  
    }

    /// <summary>
    /// The reset password.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="userName">
    /// The user name.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <param name="passwordSalt">
    /// The password salt.
    /// </param>
    /// <param name="passwordFormat">
    /// The password format.
    /// </param>
    /// <param name="maxInvalidPasswordAttempts">
    /// The max invalid password attempts.
    /// </param>
    /// <param name="passwordAttemptWindow">
    /// The password attempt window.
    /// </param>
    public void ResetPassword(string connectionStringName, [NotNull] string appName, [NotNull] string userName, [NotNull] string password, [NotNull] string passwordSalt, 
      int passwordFormat, 
      int maxInvalidPasswordAttempts, 
      int passwordAttemptWindow)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));

          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Password", password));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordSalt", passwordSalt));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PasswordFormat", passwordFormat));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@MaxInvalidAttempts", maxInvalidPasswordAttempts));
          sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@PasswordAttemptWindow", passwordAttemptWindow));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@CurrentTimeUtc", DateTime.UtcNow));

          sc.CommandText.AppendObjectQuery("prov_resetpassword", connectionStringName);

          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      }
    }

    /// <summary>
    /// The unlock user.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="userName">
    /// The user name.
    /// </param>
    public void UnlockUser(string connectionStringName, [NotNull] string appName, [NotNull] string userName)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", userName));

          sc.CommandText.AppendObjectQuery("prov_unlockuser", connectionStringName);

          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      } 
    }

    /// <summary>
    /// The update user.
    /// </summary>
    /// <param name="appName">
    /// The app name.
    /// </param>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <param name="requiresUniqueEmail">
    /// The requires unique email.
    /// </param>
    /// <returns>
    /// The update user.
    /// </returns>
    public int UpdateUser(string connectionStringName, [NotNull] object appName, [NotNull] MembershipUser user, bool requiresUniqueEmail)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@ApplicationName", appName));
          sc.Parameters.Add(sc.CreateParameter(DbType.Guid, "@UserKey", user.ProviderUserKey));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@UserName", user.UserName));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Email", user.Email));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@Comment", user.Comment));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@IsApproved", user.IsApproved));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@LastLogin", user.LastLoginDate));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@LastActivity", user.LastActivityDate));
          sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "@UniqueEmail", requiresUniqueEmail));
          var p = sc.CreateParameter(DbType.Boolean, "@ReturnValue", null, ParameterDirection.ReturnValue);       
       
          sc.Parameters.Add(p);

          sc.CommandText.AppendObjectQuery("prov_updateuser", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
          return Convert.ToInt32(p.Value);
      }        
    }

    /// <summary>
    /// The upgrade membership.
    /// </summary>
    /// <param name="previousVersion">
    /// The previous version.
    /// </param>
    /// <param name="newVersion">
    /// The new version.
    /// </param>
    public void UpgradeMembership(string connectionStringName, int previousVersion, int newVersion)
    {
      using (var sc = new VzfSqlCommand(connectionStringName))
      {
          //  sc.DataSource.ProviderName
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@PreviousVersion", previousVersion));
          sc.Parameters.Add(sc.CreateParameter(DbType.String, "@NewVersion", newVersion));
          sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "@UTCTIMESTAMP", DateTime.UtcNow));

          sc.CommandText.AppendObjectQuery("prov_upgrade", connectionStringName);
          sc.ExecuteNonQuery(CommandType.StoredProcedure);
      }     
    }

    #endregion
  }
}