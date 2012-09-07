/* VZF by vzrus
 * Copyright (C) 2006-2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Objects;

namespace YAF.Classes.Data
{
    public static class LegacyDb
    {
        #region Access Masks
        /// <summary>
        /// Deletes the access mask.
        /// </summary>
        /// <param name="mid">The module id.</param>
        /// <param name="accessMaskID">The access mask id</param>
        /// <returns>Boolean value</returns>
        public static bool accessmask_delete(int? mid, object accessMaskID)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.accessmask_delete(connectionString, accessMaskID);
                case "Npgsql": return Postgre.Db.accessmask_delete(connectionString, accessMaskID);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.accessmask_delete(connectionString, accessMaskID);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.accessmask_delete(connectionString, accessMaskID);
                // case "oracle": return OracleLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                // case "db2": return Db2LegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                // case "other": return OtherLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID); 
                default:
                    throw new ApplicationException("No return type");
            }
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="mid">The module id.</param>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public static DataTable accessmask_list(int? mid, object boardId, object accessMaskID)
        {
            return accessmask_list(mid, boardId, accessMaskID, 0);
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="mid">The module id.</param>
        /// <param name="boardId">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public static DataTable accessmask_list(int? mid, object boardId, object accessMaskID, object excludeFlags)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.accessmask_list(connectionString,boardId, accessMaskID); 
                case "Npgsql": return Postgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags);
                default:
                    throw new ApplicationException("No return type");
            }
        }

        /// <summary>
        /// Saves changes to access mask.
        /// </summary>
        /// <param name="mid">The module id.</param>
        /// <param name="accessMaskID">The access mask ID.</param>
        /// <param name="boardId">The board ID.</param>
        /// <param name="name">The access mask name.</param>
        /// <param name="readAccess">The post read access.</param>
        /// <param name="postAccess">The post write access.</param>
        /// <param name="replyAccess">The reply topic access.</param>
        /// <param name="priorityAccess">The priority access.</param>
        /// <param name="pollAccess">The poll create access.</param>
        /// <param name="voteAccess">The vote access.</param>
        /// <param name="moderatorAccess">The moderator access.</param>
        /// <param name="editAccess">The topic edit access.</param>
        /// <param name="deleteAccess">The topic delete access.</param>
        /// <param name="uploadAccess">The attachments upload access.</param>
        /// <param name="downloadAccess">The attachments download access.</param>
        /// <param name="sortOrder"> The access mask sort order. </param>
        public static void accessmask_save(int? mid, object accessMaskID, object boardId, object name, object readAccess, object postAccess, object replyAccess, object priorityAccess, object pollAccess, object voteAccess, object moderatorAccess, object editAccess, object deleteAccess, object uploadAccess, object downloadAccess, object sortOrder)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                // case "System.Data.SqlClient": MsSql.LegacyDb.accessmask_save(connectionString,accessMaskID, boardId,  name,  readAccess,  postAccess,  replyAccess,  priorityAccess,  pollAccess, voteAccess,  moderatorAccess,editAccess, deleteAccess, uploadAccess,downloadAccess,sortOrder);break;
                case "Npgsql": Postgre.Db.accessmask_save(connectionString, accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder); break;
                case "MySql.Data.MySqlClient": MySqlDb.Db.accessmask_save(connectionString, accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder); break;
                case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.accessmask_save(connectionString, accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder); break;
                // case "oracle": orPostgre.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder);break;
                // case "db2": db2Postgre.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder);break;
                // case "other": otherPostgre.Db.accessmask_saveaccessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, sortOrder);break;
                default:
                    throw new ApplicationException("No return type");
            }
        } 
        #endregion

        /// <summary>
        /// Gets list of active users
        /// </summary>
        /// <param name="mid">The module id.</param>
        /// <param name="boardId">The BoardID</param>
        /// <param name="guests">Show guests, boolean</param>
        /// <returns>Returns a DataTable of active users</returns>  
        public static DataTable active_list(int? mid, object boardId, object guests, object showCrawlers, int interval, object styledNicks)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "Npgsql": return Postgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                // case "oracle": return orPostgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                // case "db2": return db2Postgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                // case "other": return othPostgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                default:
                    throw new ApplicationException("No return type");
            }
        }
        
        /// <summary>
        /// A data Table for active list for a specific user.
        /// </summary>
        /// <param name="mid"> The module ID.</param>
        /// <param name="boardId"> The Board ID. </param>
        /// <param name="userID"> The user ID.</param>
        /// <param name="guests">Show guests, boolean</param>
        /// <param name="showCrawlers">Show crawlers in the list.</param>
        /// <param name="activeTime">The time to keep an active user in the active table.</param>
        /// <param name="styledNicks">Use styled nicks for a user.</param>
        /// <returns>A data Table for active list for a specific user.</returns>
        public static DataTable active_list_user(int? mid, object boardId, object userID, object guests, object showCrawlers, int activeTime, object styledNicks)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              case "Npgsql": return Postgre.Db.active_list_user(connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              // case "oracle": return orPostgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              // case "db2": return db2Postgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              // case "other": return othPostgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
              default:
                  throw new ApplicationException("No return type");
            }
        }

      public static DataTable active_listforum(int? mid, object forumID, object styledNicks)
      {
          string dataEngine;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.active_listforum(connectionString,  forumID, styledNicks);
              case "Npgsql": return Postgre.Db.active_listforum(connectionString, forumID, styledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.active_listforum(connectionString,  forumID, styledNicks);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.active_listforum(connectionString,  forumID, styledNicks);
              // case "oracle": return orPostgre.Db.active_listforum(connectionString,  forumID, styledNicks);
              // case "db2": return db2Postgre.Db.active_listforum(connectionString,  forumID, styledNicks);
              // case "other": return othPostgre.Db.active_listforum(connectionString,  forumID, styledNicks);
              default:
                  throw new ApplicationException("No return type");
          }
      }

      public static DataTable active_listtopic(int? mid, object topicID, object styledNicks)
      {
          string dataEngine;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.active_listtopic(connectionString, topicID, styledNicks);
              case "Npgsql": return Postgre.Db.active_listtopic(connectionString, topicID, styledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.active_listtopic(connectionString, topicID, styledNicks);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.active_listtopic(connectionString, topicID, styledNicks);
              // case "oracle": return orPostgre.Db.active_listtopic(connectionString, topicID, styledNicks);
              // case "db2": return db2Postgre.Db.active_listtopic(connectionString, topicID, styledNicks);
              // case "other": return othPostgre.Db.active_listtopic(connectionString, topicID, styledNicks);
              default:
                  throw new ApplicationException("No return type");
          }
      }

      public static  DataRow active_stats(int? mid, object boardId)
      {
          string dataEngine;
          string connectionString;
          string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.active_stats(connectionString, boardId);
              case "Npgsql": return Postgre.Db.active_stats(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.active_stats(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.active_stats(connectionString, boardId);
              // case "oracle": return orPostgre.Db.active_stats(connectionString, boardId);
              // case "db2": return db2Postgre.Db.active_stats(connectionString, boardId);
              // case "other": return othPostgre.Db.active_stats(connectionString, boardId);
              default:
                  throw new ApplicationException("No return type");
          }
      }

      public static void activeaccess_reset(int? mid)
      {
          string dataEngine;
          string connectionString;
          string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": MsSql.LegacyDb.activeaccess_reset(connectionString); break;
              case "Npgsql": Postgre.Db.activeaccess_reset(connectionString); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.activeaccess_reset(connectionString); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.activeaccess_reset(connectionString); break;
              // case "oracle":  orPostgre.Db.activeaccess_reset(connectionString); break;
              // case "db2": db2Postgre.Db.activeaccess_reset(connectionString); break;
              // case "other": othPostgre.Db.activeaccess_reset(connectionString); break;
              default:
                  throw new ApplicationException("No return type");
          }
      }

      public static DataTable User_ListProfilesByIdsList(int? mid, int boardID, [NotNull] int[] userIdsList, [CanBeNull] object useStyledNicks)
      {
          string dataEngine;
          string connectionString;
          string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
              case "Npgsql": return Postgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
              // case "oracle":  return orPostgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks); 
              // case "db2":  return db2Postgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);  
              // case "other":  return othPostgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks); 
              default:
                  throw new ApplicationException("No return type");
          }
      }

      public static DataTable User_ListTodaysBirthdays(int? mid, object boardId, object useStyledNicks)
      {
          string dataEngine;
          string connectionString;
          string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
              case "Npgsql": return Postgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
              // case "oracle":  return orPostgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
              // case "db2":  return db2Postgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
              // case "other":  return othPostgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable admin_list(int? mid, object boardId, object useStyledNicks)
      {
          string dataEngine;
          string connectionString;
          string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.admin_list(connectionString, (int)boardId, useStyledNicks); 
              case "Npgsql": return Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              // case "oracle":  return orPostgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              // case "db2":  return db2Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              // case "other":  return othPostgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable admin_pageaccesslist([CanBeNull] object boardId, [NotNull] object useStyledNicks)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.admin_pageaccesslist(connectionString, (int)boardId, useStyledNicks); 
              case "Npgsql": return Postgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
              // case "oracle":  return orPostgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
              // case "db2":  return db2Postgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
              // case "other":  return othPostgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static DataTable adminpageaccess_list([CanBeNull] object userId, [CanBeNull] object pageName)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.adminpageaccess_list(connectionString, userId, pageName); 
              case "Npgsql": return Postgre.Db.adminpageaccess_list(connectionString, userId, pageName);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.adminpageaccess_list(connectionString, userId, pageName); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.adminpageaccess_list(connectionString, userId, pageName); 
              // case "oracle":  return orPostgre.Db.adminpageaccess_list(connectionString, userId, pageName); 
              // case "db2":  return db2Postgre.Db.adminpageaccess_list(connectionString, userId, pageName); 
              // case "other":  return othPostgre.Db.adminpageaccess_list(connectionString, userId, pageName); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }


      public static void adminpageaccess_delete( [NotNull] object userId, [CanBeNull] object pageName)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.adminpageaccess_delete(connectionString, userId,  pageName); return;
              case "Npgsql": Postgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              case "MySql.Data.MySqlClient":  MySqlDb.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              // case "oracle":   orPostgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              // case "db2":   db2Postgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              // case "other":   othPostgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void adminpageaccess_save([NotNull] object userId, [CanBeNull] object pageName)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.adminpageaccess_save(connectionString, userId,  pageName); return;
              case "Npgsql": Postgre.Db.adminpageaccess_save(connectionString, userId, pageName); return;
              case "MySql.Data.MySqlClient":  MySqlDb.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
              case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
              // case "oracle":   orPostgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
              // case "db2":   db2Postgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
              // case "other":   othPostgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void album_delete(object AlbumID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": MsSql.LegacyDb.album_delete(connectionString, AlbumID); break;
              case "Npgsql": Postgre.Db.album_delete(connectionString, AlbumID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.album_delete(connectionString, AlbumID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.album_delete(connectionString, AlbumID); break;
              // case "oracle":  orPostgre.Db.album_delete(connectionString, AlbumID); break;
              // case "db2": db2Postgre.Db.album_delete(connectionString, AlbumID); break;
              // case "other": othPostgre.Db.album_delete(connectionString, AlbumID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static int[] album_getstats(object UserID, object AlbumID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_getstats(connectionString,  UserID,  AlbumID);
              case "Npgsql": return Postgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_getstats(connectionString,  UserID,  AlbumID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_getstats(connectionString,  UserID,  AlbumID);
              // case "oracle":  return orPostgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
              // case "db2": return db2Postgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
              // case "other": return othPostgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static string album_gettitle(object AlbumID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_gettitle(connectionString, AlbumID);
              case "Npgsql": return Postgre.Db.album_gettitle(connectionString, AlbumID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_gettitle(connectionString, AlbumID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_gettitle(connectionString, AlbumID);
              // case "oracle":  return orPostgre.Db.album_gettitle(connectionString, AlbumID);
              // case "db2": return db2Postgre.Db.album_gettitle(connectionString, AlbumID);
              // case "other": return othPostgre.Db.album_gettitle(connectionString, AlbumID);
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void album_image_delete(object ImageID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": MsSql.LegacyDb.album_image_delete(connectionString, ImageID); break;
              case "Npgsql": Postgre.Db.album_image_delete(connectionString, ImageID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.album_image_delete(connectionString, ImageID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.album_image_delete(connectionString, ImageID); break;
              // case "oracle":  orPostgre.Db.album_image_delete(connectionString, ImageID); break;
              // case "db2": db2Postgre.Db.album_image_delete(connectionString, ImageID); break;
              // case "other": othPostgre.Db.album_image_delete(connectionString, ImageID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void album_image_download(object ImageID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": MsSql.LegacyDb.album_image_download(connectionString, ImageID); break;
              case "Npgsql": Postgre.Db.album_image_download(connectionString, ImageID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.album_image_download(connectionString, ImageID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.album_image_download(connectionString, ImageID); break;
              // case "oracle":  orPostgre.Db.album_image_download(connectionString, ImageID); break;
              // case "db2": db2Postgre.Db.album_image_download(connectionString, ImageID); break;
              // case "other": othPostgre.Db.album_image_download(connectionString, ImageID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable album_images_by_user([NotNull] object userID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_images_by_user(connectionString, userID);
              case "Npgsql": return Postgre.Db.album_images_by_user(connectionString, userID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_images_by_user(connectionString, userID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_images_by_user(connectionString, userID);
              // case "oracle":  return orPostgre.Db.album_images_by_user(connectionString, userID); 
              // case "db2":  return db2Postgre.Db.album_images_by_user(connectionString, userID); 
              // case "other":  return othPostgre.Db.album_images_by_user(connectionString, userID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static DataTable album_image_list(object AlbumID, object ImageID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_image_list(connectionString, AlbumID, ImageID); 
              case "Npgsql": return Postgre.Db.album_image_list(connectionString, AlbumID, ImageID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_image_list(connectionString, AlbumID, ImageID); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_image_list(connectionString, AlbumID, ImageID); 
              // case "oracle":  return orPostgre.Db.album_image_list(connectionString, AlbumID, ImageID); 
              // case "db2":  return db2Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
              // case "other":  return othPostgre.Db.album_image_list(connectionString, AlbumID, ImageID); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void album_image_save(object ImageID, object AlbumID, object Caption, object FileName, object Bytes,
                              object ContentType)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              case "Npgsql": Postgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              // case "oracle":  orPostgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              // case "db2": db2Postgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              // case "other": othPostgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable album_list(object UserID, object AlbumID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_list(connectionString, UserID,  AlbumID);
              case "Npgsql": return Postgre.Db.album_list(connectionString, UserID,  AlbumID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_list(connectionString, UserID,  AlbumID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_list(connectionString, UserID,  AlbumID);
              // case "oracle":  return orPostgre.Db.album_list(connectionString, UserID,  AlbumID);
              // case "db2":  return db2Postgre.Db.album_list(connectionString, UserID,  AlbumID);
              // case "other":  return othPostgre.Db.album_list(connectionString, UserID,  AlbumID); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static int album_save(object AlbumID, object UserID, object Title, object CoverImageID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              case "Npgsql": return Postgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              // case "oracle":  return orPostgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              // case "db2":  return db2Postgre.Db.album_list(connectionString, UserID,  AlbumID);
              // case "other":  return othPostgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void attachment_delete(object attachmentID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.attachment_delete(connectionString, attachmentID); break;
              case "Npgsql": Postgre.Db.attachment_delete(connectionString, attachmentID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.attachment_delete(connectionString, attachmentID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.attachment_delete(connectionString, attachmentID); break;
              // case "oracle":  orPostgre.Db.attachment_delete(connectionString, attachmentID); break;
              // case "db2": db2Postgre.Db.attachment_delete(connectionString, attachmentID); break;
              // case "other": othPostgre.Db.attachment_delete(connectionString, attachmentID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static void attachment_download(object attachmentID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.attachment_delete(connectionString, attachmentID); break;
              case "Npgsql": Postgre.Db.attachment_delete(connectionString, attachmentID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.attachment_delete(connectionString, attachmentID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.attachment_delete(connectionString, attachmentID); break;
              // case "oracle":  orPostgre.Db.attachment_delete(connectionString, attachmentID); break;
              // case "db2": db2Postgre.Db.attachment_delete(connectionString, attachmentID); break;
              // case "other": othPostgre.Db.attachment_delete(connectionString, attachmentID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static DataTable attachment_list(object messageID, object attachmentID, object boardId, object pageIndex, object pageSize)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
              case "Npgsql": return Postgre.Db.attachment_list(connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
              // case "oracle":  return orPostgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
              // case "db2":  return db2Postgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
              // case "other":  return othPostgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static void attachment_save(object messageID, object fileName, object bytes, object contentType,
                             System.IO.Stream stream)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              case "Npgsql": Postgre.Db.attachment_save(connectionString, messageID, fileName, bytes, contentType, stream); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              // case "oracle":  orPostgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              // case "db2": db2Postgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              // case "other": othPostgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void bannedip_delete(object ID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.bannedip_delete(connectionString, ID); break;
              case "Npgsql": Postgre.Db.bannedip_delete(connectionString, ID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.bannedip_delete(connectionString, ID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.bannedip_delete(connectionString, ID); break;
              // case "oracle":  orPostgre.Db.bannedip_delete(connectionString, ID); break;
              // case "db2": db2Postgre.Db.bannedip_delete(connectionString, ID); break;
              // case "other": othPostgre.Db.bannedip_delete(connectionString, ID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable bannedip_list(object boardId, object ID, [CanBeNull] object pageIndex, [CanBeNull] object pageSize)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              case "Npgsql": return Postgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              // case "oracle":  return orPostgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              // case "db2":  return db2Postgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
              // case "other":  return othPostgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static void bannedip_save(object ID, object boardId, object Mask, string reason, int userID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              case "Npgsql": Postgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              // case "oracle":  orPostgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              // case "db2": db2Postgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              // case "other": othPostgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void bbcode_delete(object bbcodeID)
      {
          string dataEngine;
          int? mid = 0;  string namePattern = string.Empty;
          string connectionString;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.bbcode_delete(connectionString, bbcodeID); break;
              case "Npgsql": Postgre.Db.bbcode_delete(connectionString, bbcodeID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.bbcode_delete(connectionString, bbcodeID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.bbcode_delete(connectionString, bbcodeID); break;
              // case "oracle":  orPostgre.Db.bbcode_delete(connectionString, bbcodeID); break;
              // case "db2": db2Postgre.Db.bbcode_delete(connectionString, bbcodeID); break;
              // case "other": othPostgre.Db.bbcode_delete(connectionString, bbcodeID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void bbcode_save(object bbcodeID, object boardId, object name, object description, object onclickjs,
                         object displayjs, object editjs, object displaycss, object searchregex, object replaceregex,
                         object variables, object usemodule, object moduleclass, object execorder)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              case "Npgsql": Postgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              // case "oracle":  orPostgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              // case "db2": db2Postgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              // case "other": othPostgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static IEnumerable<TypedBBCode> BBCodeList(int boardId, int? bbcodeID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.BBCodeList(connectionString, boardId, bbcodeID);
              case "Npgsql": return Postgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.BBCodeList(connectionString, boardId, bbcodeID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.BBCodeList(connectionString, boardId, bbcodeID);
              // case "oracle":  return orPostgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
              // case "db2":  return db2Postgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
              // case "other":  return othPostgre.Db.BBCodeList(connectionString, boardId, bbcodeID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static int board_create(object adminUsername, object adminUserEmail, object adminUserKey, object boardName,
                         object culture, object languageFile, object boardMembershipName, object boardRolesName,
                object rolePrefix, object isHostUser)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              case "Npgsql": return Postgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              // case "oracle":  return orPostgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              // case "db2":  return db2Postgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              // case "other":  return othPostgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static void board_delete(object boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.board_delete(connectionString, boardId); break;
              case "Npgsql": Postgre.Db.board_delete(connectionString, boardId); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.board_delete(connectionString, boardId); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.board_delete(connectionString, boardId); break;
              // case "oracle":  orPostgre.Db.board_delete(connectionString, boardId); break;
              // case "db2": db2Postgre.Db.board_delete(connectionString, boardId); break;
              // case "other": othPostgre.Db.board_delete(connectionString, boardId); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable board_list(object boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_list(connectionString, boardId);
              case "Npgsql": return Postgre.Db.board_list(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_list(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_list(connectionString, boardId);
              // case "oracle":  return orPostgre.Db.board_list(connectionString, boardId);
              // case "db2":  return db2Postgre.Db.board_list(connectionString, boardId);
              // case "other":  return othPostgre.Db.board_list(connectionString, boardId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static DataRow board_poststats(int? boardId, bool useStyledNicks, bool showNoCountPosts)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              case "Npgsql": return Postgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              // case "oracle":  return orPostgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              // case "db2":  return db2Postgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts);
              // case "other":  return othPostgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      /// <summary>
      /// Recalculates topic and post numbers and updates last post for all forums in all boards
      /// </summary>
      static public void board_resync()
      {
          board_resync(null);
      }
      /// <summary>
      /// Recalculates topic and post numbers and updates last post for all forums in all boards
      /// </summary>
      static public void board_resync(object boardId)
      {
         string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.board_resync(connectionString, boardId); break;
              case "Npgsql": Postgre.Db.board_resync(connectionString, boardId); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.board_resync(connectionString, boardId); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.board_resync(connectionString, boardId); break;
              // case "oracle":  orPostgre.Db.board_resync(connectionString, boardId); break;
              // case "db2": db2Postgre.Db.board_resync(connectionString, boardId); break;
              // case "other": othPostgre.Db.board_resync(connectionString, boardId); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static int board_save(object boardId, object languageFile, object culture, object name, object allowThreaded)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              case "Npgsql": return Postgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              // case "oracle":  return orPostgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              // case "db2":  return db2Postgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              // case "other":  return othPostgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      static public DataRow board_stats()
      {
          return board_stats(null);
      }
      public static DataRow board_stats(object boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_stats(connectionString, boardId);
              case "Npgsql": return Postgre.Db.board_stats(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_stats(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_stats(connectionString, boardId);
              // case "oracle":  return orPostgre.Db.board_stats(connectionString, boardId);
              // case "db2":  return db2Postgre.Db.board_stats(connectionString, boardId);
              // case "other":  return othPostgre.Db.board_stats(connectionString, boardId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataRow board_userstats(int? boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.board_userstats(connectionString, boardId);
              case "Npgsql": return Postgre.Db.board_userstats(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.board_userstats(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.board_userstats(connectionString, boardId);
              // case "oracle":  return orPostgre.Db.board_userstats(connectionString, boardId);
              // case "db2":  return db2Postgre.Db.board_userstats(connectionString, boardId);
              // case "other":  return othPostgre.Db.board_userstats(connectionString, boardId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static string[] buddy_addrequest(object FromUserID, object ToUserID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              case "Npgsql": return Postgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              // case "oracle":  return orPostgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              // case "db2":  return db2Postgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
              // case "other":  return othPostgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static string buddy_approveRequest(object FromUserID, object ToUserID, object Mutual)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              case "Npgsql": return Postgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              // case "oracle":  return orPostgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              // case "db2":  return db2Postgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
              // case "other":  return othPostgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static string buddy_denyRequest(object FromUserID, object ToUserID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              case "Npgsql": return Postgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              // case "oracle":  return orPostgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              // case "db2":  return db2Postgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
              // case "other":  return othPostgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static DataTable buddy_list(object FromUserID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.buddy_list(connectionString, FromUserID);
              case "Npgsql": return Postgre.Db.buddy_list(connectionString, FromUserID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.buddy_list(connectionString, FromUserID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.buddy_list(connectionString, FromUserID);
              // case "oracle":  return orPostgre.Db.buddy_list(connectionString, FromUserID);
              // case "db2":  return db2Postgre.Db.buddy_list(connectionString, FromUserID);
              // case "other":  return othPostgre.Db.buddy_list(connectionString, FromUserID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static string buddy_remove(object FromUserID, object ToUserID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.buddy_remove(connectionString, FromUserID, ToUserID);
              case "Npgsql": return Postgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.buddy_remove(connectionString, FromUserID, ToUserID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.buddy_remove(connectionString, FromUserID, ToUserID);
              // case "oracle":  return orPostgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
              // case "db2":  return db2Postgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
              // case "other":  return othPostgre.Db.buddy_remove(connectionString, FromUserID, ToUserID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static bool category_delete(object CategoryID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.category_delete(connectionString, CategoryID);
              case "Npgsql": return Postgre.Db.category_delete(connectionString, CategoryID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.category_delete(connectionString, CategoryID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.category_delete(connectionString, CategoryID);
              // case "oracle":  return orPostgre.Db.category_delete(connectionString, CategoryID);
              // case "db2":  return db2Postgre.Db.category_delete(connectionString, CategoryID);
              // case "other":  return othPostgre.Db.category_delete(connectionString, CategoryID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static DataTable category_list(object boardId, object categoryID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.category_list(connectionString,  boardId, categoryID);
              case "Npgsql": return Postgre.Db.category_list(connectionString,  boardId, categoryID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.category_list(connectionString,  boardId, categoryID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.category_list(connectionString,  boardId, categoryID);
              // case "oracle":  return orPostgre.Db.category_list(connectionString,  boardId, categoryID);
              // case "db2":  return db2Postgre.Db.category_list(connectionString,  boardId, categoryID);
              // case "other":  return othPostgre.Db.category_list(connectionString,  boardId, categoryID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataTable category_listread(object boardId, object userId, object categoryID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.category_listread(connectionString, boardId, userId, categoryID);
              case "Npgsql": return Postgre.Db.category_listread(connectionString, boardId, userId, categoryID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.category_listread(connectionString, boardId, userId, categoryID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.category_listread(connectionString, boardId, userId, categoryID);
              // case "oracle":  return orPostgre.Db.category_listread(connectionString, boardId, userId, categoryID);
              // case "db2":  return db2Postgre.Db.category_listread(connectionString, boardId, userId, categoryID);
              // case "other":  return othPostgre.Db.category_listread(connectionString, boardId, userId, categoryID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      public static DataTable category_simplelist(int startID, int limit)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.category_simplelist(connectionString, startID, limit);
              case "Npgsql": return Postgre.Db.category_simplelist(connectionString, startID, limit);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.category_simplelist(connectionString, startID, limit);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.category_simplelist(connectionString, startID, limit);
              // case "oracle":  return orPostgre.Db.category_simplelist(connectionString, startID, limit);
              // case "db2":  return db2Postgre.Db.category_simplelist(connectionString, startID, limit);
              // case "other":  return othPostgre.Db.category_simplelist(connectionString, startID, limit); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      static public void category_save(object boardId, object categoryId, object name, object categoryImage, object sortOrder)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              case "Npgsql": Postgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              // case "oracle":  orPostgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              // case "db2": db2Postgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              // case "other": othPostgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static DataTable checkemail_list(object email)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.checkemail_list(connectionString, email);
              case "Npgsql": return Postgre.Db.checkemail_list(connectionString, email);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.checkemail_list(connectionString, email);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.checkemail_list(connectionString, email);
              // case "oracle":  return orPostgre.Db.checkemail_list(connectionString, email);
              // case "db2":  return db2Postgre.Db.checkemail_list(connectionString, email);
              // case "other":  return othPostgre.Db.checkemail_list(connectionString, email); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      static public void checkemail_save(object userId, object hash, object email)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.checkemail_save(connectionString, userId,  hash,  email); break;
              case "Npgsql": Postgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              // case "oracle":  orPostgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              // case "db2": db2Postgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              // case "other": othPostgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable checkemail_update(object hash)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.checkemail_update(connectionString, hash);
              case "Npgsql": return Postgre.Db.checkemail_update(connectionString, hash);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.checkemail_update(connectionString, hash);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.checkemail_update(connectionString, hash);
              // case "oracle":  return orPostgre.Db.checkemail_update(connectionString, hash);
              // case "db2":  return db2Postgre.Db.checkemail_update(connectionString, hash);
              // case "other":  return othPostgre.Db.checkemail_update(connectionString, hash); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      static public void choice_add(object pollID, object choice, object path, object mime)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.choice_add(connectionString, pollID, choice, path, mime); break;
              case "Npgsql": Postgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              // case "oracle":  orPostgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              // case "db2": db2Postgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              // case "other": othPostgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      static public void choice_delete(object choiceID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.choice_delete(connectionString, choiceID); break;
              case "Npgsql": Postgre.Db.choice_delete(connectionString, choiceID); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.choice_delete(connectionString, choiceID); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.choice_delete(connectionString, choiceID); break;
              // case "oracle":  orPostgre.Db.choice_delete(connectionString, choiceID); break;
              // case "db2": db2Postgre.Db.choice_delete(connectionString, choiceID); break;
              // case "other": othPostgre.Db.choice_delete(connectionString, choiceID); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      static public void choice_update(object choiceID, object choice, object path, object mime)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.choice_update(connectionString, choiceID, choice, path, mime); break;
              case "Npgsql": Postgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              // case "oracle":  orPostgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              // case "db2": db2Postgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              // case "other": othPostgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      static public void choice_vote(object choiceID, object userId, object remoteIP)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              case "Npgsql": Postgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              // case "oracle":  orPostgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              // case "db2": db2Postgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              // case "other": othPostgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      static public string db_getstats_new()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return  MsSql.LegacyDb.db_getstats_new(connectionString); break;
              case "Npgsql": return Postgre.Db.db_getstats_new(connectionString); ; break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_getstats_new(connectionString); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_getstats_new(connectionString); break;
              // case "oracle": return  orPostgre.Db.db_getstats_new(connectionString); break;
              // case "db2": return db2Postgre.Db.db_getstats_new(connectionString); break;
              // case "other": return othPostgre.Db.db_getstats_new(connectionString); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
        
      }
   
      public static string db_getstats_warning()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return  MsSql.LegacyDb.db_getstats_warning(); break;
              case "Npgsql": return Postgre.Db.db_getstats_warning(); ; break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_getstats_warning(); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_getstats_warning(); break;
              // case "oracle": return  orPostgre.Db.db_getstats_warning(); break;
              // case "db2": return db2_db_getstats_warning(); break;
              // case "other": return othPostgre.Db.db_getstats_warning(); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
         
      }

      

      static public string db_recovery_mode_warning(string dbRecoveryMode)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.db_recovery_mode_warning(); break;
              case "Npgsql": return Postgre.Db.db_recovery_mode_warning(); break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_recovery_mode_warning(); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_recovery_mode_warning(); break;
              // case "oracle": return  orPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              // case "db2": return db2Postgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              // case "other": return othPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
    /*  public static string db_getstats_warning()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return  MsSql.LegacyDb.db_getstats_warning(); break;
              case "Npgsql": return Postgre.Db.db_getstats_warning(); ; break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_getstats_warning(); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_getstats_warning(); break;
              // case "oracle": return  orPostgre.Db.db_getstats_warning(); break;
              // case "db2": return db2_db_getstats_warning(); break;
              // case "other": return othPostgre.Db.db_getstats_warning(); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }

      } */
      //Set Recovery
      static public string db_recovery_mode_warning()
      {
          return "";
      }

      static public string db_recovery_mode_new(string dbRecoveryMode)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return  MsSql.LegacyDb.db_recovery_mode_new(connectionString, dbRecoveryMode); break;
              case "Npgsql": return Postgre.Db.db_recovery_mode_new(connectionString,dbRecoveryMode); break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_recovery_mode_new(connectionString,dbRecoveryMode); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_recovery_mode_new(connectionString,dbRecoveryMode); break;
              // case "oracle": return  orPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              // case "db2": return db2Postgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              // case "other": return othPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      static public string db_reindex_new()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return  MsSql.LegacyDb.db_reindex_new(connectionString); break;
              case "Npgsql": return Postgre.Db.db_reindex_new(connectionString); break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_reindex_new(connectionString); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_reindex_new(connectionString); break;
              // case "oracle": return orPostgre.Db.db_reindex_new(connectionString); break;
              // case "db2": return db2Postgre.Db.db_reindex_new(connectionString); break;
              // case "other": return othPostgre.Db.db_reindex_new(connectionString); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static string db_reindex_warning()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.db_reindex_warning();
              case "Npgsql": return Postgre.Db.db_reindex_warning();
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_reindex_warning();
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_reindex_warning();
              // case "oracle":  return orPostgre.Db.db_reindex_warning();
              // case "db2":  return db2Postgre.Db.db_reindex_warning();
              // case "other":  return othPostgre.Db.db_reindex_warning(); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static string db_runsql_new(string sql, bool useTransaction)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.db_runsql_new(connectionString, sql,   useTransaction);
              case "Npgsql": return Postgre.Db.db_runsql_new(connectionString, sql, useTransaction);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_runsql_new(connectionString, sql,  useTransaction);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_runsql_new(connectionString, sql,   useTransaction);
              // case "oracle":  return orPostgre.Db.db_runsql_new(connectionString, sql,  useTransaction);
              // case "db2":  return db2Postgre.Db.db_runsql_new(connectionString, sql,  useTransaction);
              // case "other":  return othPostgre.Db.db_runsql_new(connectionString, sql, useTransaction); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      static public string  db_shrink_new()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.db_shrink_new(connectionString); break;
              case "Npgsql": return Postgre.Db.db_shrink_new(connectionString); break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_shrink_new(connectionString); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_shrink_new(connectionString); break;
              // case "oracle": return orPostgre.Db.db_shrink(connectionString); break;
              // case "db2": return db2Postgre.Db.db_shrink(connectionString); break;
              // case "other": return othPostgre.Db.db_shrink(connectionString); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static string db_shrink_warning()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.db_shrink_warning();
              case "Npgsql": return Postgre.Db.db_shrink_warning(connectionString);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_shrink_warning();
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_shrink_warning();
              // case "oracle":  return orPostgre.Db.db_shrink_warning(connectionString);
              // case "db2":  return db2Postgre.Db.db_shrink_warning(connectionStringe);
              // case "other":  return othPostgre.Db.db_shrink_warning(connectionString); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataSet ds_forumadmin(object boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.ds_forumadmin(connectionString, boardId);
              case "Npgsql": return Postgre.Db.ds_forumadmin(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.ds_forumadmin(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.ds_forumadmin(connectionString, boardId);
              // case "oracle":  return orPostgre.Db.ds_forumadmin(connectionString, boardId);
              // case "db2":  return db2Postgre.Db.ds_forumadmin(connectionString, boardId);
              // case "other":  return othPostgre.Db.ds_forumadmin(connectionString, boardId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      static public void eventlog_create( object userId, object source, object description)
      {
         eventlog_create(userId, (object)source.GetType().ToString(), description, (object)0);
      }
     
      static public void eventlog_create(object userId, object source, object description, object type)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.eventlog_create(connectionString,  userId, source, description,type); break;
              case "Npgsql": Postgre.Db.eventlog_create(connectionString, userId, source, description, type); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.eventlog_create(connectionString,  userId, source, description,type); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.eventlog_create(connectionString,  userId, source, description,type); break;
              // case "oracle":  orPostgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
              // case "db2": db2Postgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
              // case "other": othPostgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      static public void eventlog_delete(int boardId)
      {
          eventlog_delete(null, boardId);
      }
      /// <summary>
      /// Deletes event log entry of given ID.
      /// </summary>
      /// <param name="eventLogID">ID of event log entry.</param>
      static public void eventlog_delete(object eventLogID, object pageUserId)
      {
          eventlog_delete(eventLogID, null, pageUserId);
      }
      static private void eventlog_delete(object eventLogID, object boardId, object pageUserId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              case "Npgsql": Postgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              // case "oracle":  orPostgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              // case "db2": db2Postgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              // case "other": othPostgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      static public void eventlog_deletebyuser(object boardID, object userId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.eventlog_deletebyuser(connectionString,boardID,userId); break;
              case "Npgsql": Postgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              // case "oracle":  orPostgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              // case "db2": db2Postgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              // case "other": othPostgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static DataTable eventlog_list(object boardId, [NotNull] object pageUserID, [NotNull] object maxRows, [NotNull] object maxDays, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object eventIDs)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              case "Npgsql": return Postgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              // case "oracle":  return orPostgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              // case "db2":  return db2Postgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
              // case "other":  return othPostgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataTable eventloggroupaccess_list([NotNull] object groupID, [NotNull] object eventTypeId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              case "Npgsql": return Postgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              // case "oracle":  return orPostgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              // case "db2":  return db2Postgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
              // case "other":  return othPostgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataTable group_eventlogaccesslist([NotNull] object boardId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.group_eventlogaccesslist(connectionString, boardId);
              case "Npgsql": return Postgre.Db.group_eventlogaccesslist(connectionString, boardId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.group_eventlogaccesslist(connectionString, boardId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_eventlogaccesslist(connectionString, boardId);
              // case "oracle":  return orPostgre.Db.group_eventlogaccesslist(connectionString, boardId);
              // case "db2":  return db2Postgre.Db.group_eventlogaccesslist(connectionString, boardId);
              // case "other":  return othPostgre.Db.group_eventlogaccesslist(connectionString, boardId);
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      static public void eventloggroupaccess_save([NotNull] object groupID, [NotNull] object eventTypeId, [NotNull] object eventTypeName, [NotNull] object deleteAccess)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              case "Npgsql": Postgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              // case "oracle":  orPostgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              // case "db2": db2Postgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              // case "other": othPostgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      static public void extension_delete(object extensionId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.extension_delete(connectionString, extensionId); break;
              case "Npgsql": Postgre.Db.extension_delete(connectionString, extensionId); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.extension_delete(connectionString, extensionId); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.extension_delete(connectionString, extensionId); break;
              // case "oracle":  orPostgre.Db.extension_delete(connectionString, extensionId); break;
              // case "db2": db2Postgre.Db.extension_delete(connectionString, extensionId); break;
              // case "other": othPostgre.Db.extension_delete(connectionString, extensionId); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      static public void eventloggroupaccess_delete([NotNull] object groupID, [NotNull] object eventTypeId, [NotNull] object eventTypeName)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              case "Npgsql": Postgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              // case "oracle":  orPostgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              // case "db2": db2Postgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              // case "other": othPostgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }

      public static DataTable extension_edit(object extensionId)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.extension_edit(connectionString, extensionId);
              case "Npgsql": return Postgre.Db.extension_edit(connectionString, extensionId);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.extension_edit(connectionString, extensionId);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.extension_edit(connectionString, extensionId);
              // case "oracle":  return orPostgre.Db.extension_edit(connectionString, extensionId);
              // case "db2":  return db2Postgre.Db.extension_edit(connectionString, extensionId);
              // case "other":  return othPostgre.Db.extension_edit(connectionString, extensionId); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      // Returns an extension list for a given Board
      static public DataTable extension_list(object boardId)
      {
          return extension_list(boardId, null);

      }

      public static DataTable extension_list(object boardId, object extension)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.extension_list(connectionString, boardId, extension);
              case "Npgsql": return Postgre.Db.extension_list(connectionString, boardId, extension);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.extension_list(connectionString, boardId, extension);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.extension_list(connectionString, boardId, extension);
              // case "oracle":  return orPostgre.Db.extension_list(connectionString, boardId, extension);
              // case "db2":  return db2Postgre.Db.extension_list(connectionString, boardId, extension);
              // case "other":  return othPostgre.Db.extension_list(connectionString, boardId, extension); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

      static public void extension_save(object extensionId, object boardId, object extension)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient":  MsSql.LegacyDb.extension_save(connectionString, extensionId, boardId, extension); break;
              case "Npgsql": Postgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              case "MySql.Data.MySqlClient": MySqlDb.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              // case "oracle":  orPostgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              // case "db2": db2Postgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              // case "other": othPostgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
              default:
                  throw new ApplicationException("No return type");
                  break;

          }
      }
      public static bool forum_delete(object forumID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_delete(connectionString, forumID);
              case "Npgsql": return Postgre.Db.forum_delete(connectionString, forumID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_delete(connectionString, forumID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_delete(connectionString, forumID);
              // case "oracle":  return orPostgre.Db.forum_delete(connectionString, forumID);
              // case "db2":  return db2Postgre.Db.forum_delete(connectionString, forumID);
              // case "other":  return othPostgre.Db.forum_delete(connectionString, forumID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static bool forum_move([NotNull] object forumOldID, [NotNull] object forumNewID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_move(connectionString, forumOldID, forumNewID);
              case "Npgsql": return Postgre.Db.forum_move(connectionString, forumOldID, forumNewID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_move(connectionString, forumOldID, forumNewID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_move(connectionString, forumOldID, forumNewID);
              // case "oracle":  return orPostgre.Db.forum_move(connectionString, forumOldID, forumNewID);
              // case "db2":  return db2Postgre.Db.forum_move(connectionString, forumOldID, forumNewID);
              // case "other":  return othPostgre.Db.forum_move(connectionString, forumOldID, forumNewID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
      public static DataTable forum_list(object boardId, object forumID)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_list(connectionString, boardId, forumID);
              case "Npgsql": return Postgre.Db.forum_list(connectionString, boardId, forumID);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_list(connectionString, boardId, forumID);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_list(connectionString, boardId, forumID);
              // case "oracle":  return orPostgre.Db.forum_list(connectionString, boardId, forumID);
              // case "db2":  return db2Postgre.Db.forum_list(connectionString, boardId, forumID);
              // case "other":  return othPostgre.Db.forum_list(connectionString, boardId, forumID); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }

        		/// <summary>
		/// Listes all forums accessible to a user
		/// </summary>
		/// <param name="boardId">BoardID</param>
		/// <param name="userId">ID of user</param>
		/// <returns>DataTable of all accessible forums</returns>
		static public DataTable forum_listall(object boardId, object userId)
		{
			return forum_listall(boardId, userId, 0);
		}

        public static DataTable  forum_listall(object boardId, object userId, object startAt)
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
          
          switch (dataEngine)
          {
              // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_listall(connectionString, boardId, userId, startAt);
              case "Npgsql": return Postgre.Db.forum_listall(connectionString, boardId, userId, startAt);
              case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_listall(connectionString, boardId, userId, startAt);
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_listall(connectionString, boardId, userId, startAt);
              // case "oracle":  return orPostgre.Db.forum_listall(connectionString, boardId, userId, startAt);
              // case "db2":  return db2Postgre.Db.forum_listall(connectionString, boardId, userId, startAt);
              // case "other":  return othPostgre.Db.forum_listall(connectionString, boardId, userId, startAt); 
              default:
                  throw new ApplicationException("No return type");
                  break;
          }
      }
        /// <summary>
        /// Lists all forums within a given subcategory
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="CategoryID">CategoryID</param>
        /// <returns>DataTable with list</returns>
        static public DataTable forum_listall_fromCat(object boardId, object categoryID)
        {
            return forum_listall_fromCat(boardId, categoryID, true);
        }

        public static DataTable forum_listall_fromCat(object boardId, object categoryID, bool emptyFirstRow)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                case "Npgsql": return Postgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                // case "oracle":  return orPostgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                // case "db2":  return db2Postgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow);
                // case "other":  return othPostgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

        static private DataTable forum_sort_list(DataTable listSource, int parentID, int categoryID, int startingIndent, int[] forumidExclusions)
        {
            return forum_sort_list(listSource, parentID, categoryID, startingIndent, forumidExclusions, true);
        }

        static public DataTable forum_sort_list(DataTable listSource, int parentID, int categoryID, int startingIndent, int[] forumidExclusions, bool emptyFirstRow)
        {
            DataTable listDestination = new DataTable();
            listDestination.TableName = "forum_sort_list";
            listDestination.Columns.Add("ForumID", typeof(String));
            listDestination.Columns.Add("Title", typeof(String));

            if (emptyFirstRow)
            {
                DataRow blankRow = listDestination.NewRow();
                blankRow["ForumID"] = string.Empty;
                blankRow["Title"] = string.Empty;
                listDestination.Rows.Add(blankRow);
            }
            // filter the forum list -- not sure if this code actually works
            DataView dv = listSource.DefaultView;

            if (forumidExclusions != null && forumidExclusions.Length > 0)
            {
                string strExclusions = "";
                bool bFirst = true;

                foreach (int forumID in forumidExclusions)
                {
                    if (bFirst) bFirst = false;
                    else strExclusions += ",";

                    strExclusions += forumID.ToString();
                }

                dv.RowFilter = string.Format("ForumID NOT IN ({0})", strExclusions);
                dv.ApplyDefaultSort = true;
            }

            forum_sort_list_recursive(dv.ToTable(), listDestination, parentID, categoryID, startingIndent);

            return listDestination;
        }

        static public DataTable forum_listall_sorted(object boardId, object userId)
        {
            return forum_listall_sorted(boardId, userId, null, false, 0);
        }

        static public DataTable forum_listall_sorted(object boardId, object userId, int[] forumidExclusions)
        {
            return forum_listall_sorted(boardId, userId, null, false, 0);
        }



        //Here
        static public DataTable forum_listall_sorted(object boardId, object userId, int[] forumidExclusions, bool emptyFirstRow, int startAt)
        {
            using (DataTable dataTable = forum_listall(boardId, userId, startAt))
            {
                int baseForumId = 0;
                int baseCategoryId = 0;

                if (startAt != 0)
                {
                    // find the base ids...
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (Convert.ToInt32(dataRow["ForumID"]) == startAt)
                        {
                            baseForumId = Convert.ToInt32(dataRow["ParentID"]);
                            baseCategoryId = Convert.ToInt32(dataRow["CategoryID"]);
                            break;
                        }
                    }
                }

                return forum_sort_list(dataTable, baseForumId, baseCategoryId, 0, forumidExclusions, emptyFirstRow);
            }
        }

        static public void forum_list_sort_basic(DataTable listsource, DataTable list, int parentid, int currentLvl)
        {
            for (int i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value)
                    row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentid)
                {
                    string sIndent = "";
                    int iIndent = Convert.ToInt32(currentLvl);
                    for (int j = 0; j < iIndent; j++) sIndent += "--";
                    row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                    list.Rows.Add(row.ItemArray);
                    forum_list_sort_basic(listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
            }
        }

        static public void forum_sort_list_recursive(DataTable listSource, DataTable listDestination, int parentID, int categoryID, int currentIndent)
        {
            DataRow newRow;

            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value)
                    row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentID)
                {
                    if ((int)row["CategoryID"] != categoryID)
                    {
                        categoryID = (int)row["CategoryID"];

                        newRow = listDestination.NewRow();
                        newRow["ForumID"] = -categoryID;		// Ederon : 9/4/2007
                        newRow["Title"] = string.Format("{0}", row["Category"].ToString());
                        listDestination.Rows.Add(newRow);
                    }

                    string sIndent = "";

                    for (int j = 0; j < currentIndent; j++)
                        sIndent += "--";

                    // import the row into the destination
                    newRow = listDestination.NewRow();

                    newRow["ForumID"] = row["ForumID"];
                    newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["Forum"]);

                    listDestination.Rows.Add(newRow);

                    // recurse through the list...
                    forum_sort_list_recursive(listSource, listDestination, (int)row["ForumID"], categoryID, currentIndent + 1);
                }
            }
        }
        public static DataTable forum_listallMyModerated(object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_listallMyModerated(connectionString, boardId, userId);
                case "Npgsql": return Postgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_listallMyModerated(connectionString, boardId, userId);
                // case "oracle":  return orPostgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                // case "db2":  return db2Postgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                // case "other":  return othPostgre.Db.forum_listallMyModerated(connectionString, boardId, userId); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }
        public static DataTable forum_listpath(object forumID)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_listpath(connectionString, forumID);
                case "Npgsql": return Postgre.Db.forum_listpath(connectionString, forumID);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_listpath(connectionString, forumID);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_listpath(connectionString, forumID);
                // case "oracle":  return orPostgre.Db.forum_listpath(connectionString, forumID);
                // case "db2":  return db2Postgre.Db.forum_listpath(connectionString, forumID);
                // case "other":  return othPostgre.Db.forum_listpath(connectionString, forumID); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }
        public static DataTable forum_listread(object boardID,
                       object userID, object categoryID, object parentID, object useStyledNicks, bool findLastRead)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead);
                case "Npgsql": return Postgre.Db.forum_listread(connectionString, boardID, userID, categoryID, parentID, useStyledNicks, findLastRead);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_listread(connectionString,boardID,userID, categoryID, parentID, useStyledNicks, findLastRead);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_listread(connectionString,boardID,userID, categoryID, parentID, useStyledNicks, findLastRead);
                // case "oracle":  return orPostgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead);
                // case "db2":  return db2Postgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead);
                // case "other":  return othPostgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

        public static DataSet forum_moderatelist(object userId, object boardId)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_moderatelist(connectionString, userId, boardId);
                case "Npgsql": return Postgre.Db.forum_moderatelist(connectionString, userId, boardId);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_moderatelist(connectionString, userId, boardId);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "oracle":  return orPostgre.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "db2":  return db2Postgre.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "other":  return othPostgre.Db.forum_moderatelist(connectionString, userId, boardId); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

        public static DataTable forum_moderators(bool useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_moderators(connectionString, useStyledNicks);
                case "Npgsql": return Postgre.Db.forum_moderators(connectionString, useStyledNicks);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_moderators(connectionString, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_moderators(connectionString, useStyledNicks);
                // case "oracle":  return orPostgre.Db.forum_moderators(connectionString, useStyledNicks);
                // case "db2":  return db2Postgre.Db.forum_moderators(connectionString, useStyledNicks);
                // case "other":  return othPostgre.Db.forum_moderators(connectionString, useStyledNicks); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

        /// <summary>
        /// Updates topic and post count and last topic for all forums in specified board
        /// </summary>
        /// <param name="boardId">BoardID</param>
        static public void forum_resync(object boardId)
        {
            forum_resync(boardId, null);
        }

        public static void forum_resync(object boardId, object forumID)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": MsSql.LegacyDb.forum_resync(connectionString, boardId, forumID); break;
                case "Npgsql": Postgre.Db.forum_resync(connectionString, boardId, forumID); break;
                case "MySql.Data.MySqlClient":  MySqlDb.Db.forum_resync(connectionString, boardId, forumID); break;
                case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.forum_resync(connectionString, boardId, forumID); break;;
                // case "oracle":   orPostgre.Db.forum_resync(connectionString, boardId, forumID); break;;
                // case "db2":   db2Postgre.Db.forum_resync(connectionString, boardId, forumID); break;
                // case "other":   othPostgre.Db.forum_resync(connectionString, boardId, forumID); break;
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

        public static long forum_save(object forumID,
                        object categoryID, object parentID, object name,
                        object description, object sortOrder, object locked,
                        object hidden, object isTest, object moderated,
                        object accessMaskID, object remoteURL,
                        object themeURL,
                        object imageURL,
                        object styles,
                        bool dummy)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                case "Npgsql": return Postgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                // case "oracle":  return orPostgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                // case "db2":  return db2Postgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy);
                // case "other":  return othPostgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

         public static int forum_save_parentschecker(object forumID, object parentID)
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_save_parentschecker(connectionString, forumID, parentID);
                case "Npgsql": return Postgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                // case "oracle":  return orPostgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                // case "db2":  return db2Postgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                // case "other":  return othPostgre.Db.forum_save_parentschecker(connectionString, forumID, parentID); 
                default:
                    throw new ApplicationException("No return type");
                    break;
            }
        }

         public static DataTable forum_simplelist(int startID, int limit)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.forum_simplelist(connectionString, startID, limit);
                 case "Npgsql": return Postgre.Db.forum_simplelist(connectionString, startID, limit);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.forum_simplelist(connectionString, startID, limit);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forum_simplelist(connectionString, startID, limit);
                 // case "oracle":  return orPostgre.Db.forum_simplelist(connectionString, startID, limit);
                 // case "db2":  return db2Postgre.Db.forum_simplelist(connectionString, startID, limit);
                 // case "other":  return othPostgre.Db.forum_simplelist(connectionString, startID, limit); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable forumaccess_group(object groupID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.forumaccess_group(connectionString, groupID);
                 case "Npgsql": return Postgre.Db.forumaccess_group(connectionString, groupID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.forumaccess_group(connectionString, groupID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forumaccess_group(connectionString, groupID);
                 // case "oracle":  return orPostgre.Db.forumaccess_group(connectionString, groupID);
                 // case "db2":  return db2Postgre.Db.forumaccess_group(connectionString, groupID);
                 // case "other":  return othPostgre.Db.forumaccess_group(connectionString, groupID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable forumaccess_list(object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.forumaccess_list(connectionString, forumID);
                 case "Npgsql": return Postgre.Db.forumaccess_list(connectionString, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.forumaccess_list(connectionString, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forumaccess_list(connectionString, forumID);
                 // case "oracle":  return orPostgre.Db.forumaccess_list(connectionString, forumID);
                 // case "db2":  return db2Postgre.Db.forumaccess_list(connectionString, forumID);
                 // case "other":  return othPostgre.Db.forumaccess_list(connectionString, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void forumaccess_save(object forumID, object groupID, object accessMaskID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                 case "Npgsql": Postgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;;
                 // case "oracle":   orPostgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;;
                 // case "db2":   db2Postgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                 // case "other":   othPostgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static IEnumerable<TypedForumListAll> ForumListAll(int boardId, int userId)
         {
             return forum_listall(boardId, userId, 0).AsEnumerable().Select(r => new TypedForumListAll(r));
         }
         public static IEnumerable<TypedForumListAll> ForumListAll(int boardId, int userId, int startForumId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.ForumListAll(connectionString, boardId, userId, startForumId);
                 case "Npgsql": return Postgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                 // case "oracle":  return orPostgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                 // case "db2":  return db2Postgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                 // case "other":  return othPostgre.Db.ForumListAll(connectionString, boardId, userId, startForumId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static bool forumpage_initdb(out string errorStr, bool debugging)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 case "Npgsql": return Postgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 // case "oracle":  return orPostgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 // case "db2":  return db2Postgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                 // case "other":  return othPostgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static string forumpage_validateversion(int appVersion)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.forumpage_validateversion(connectionString, appVersion);
                 case "Npgsql": return Postgre.Db.forumpage_validateversion(connectionString, appVersion);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.forumpage_validateversion(connectionString, appVersion);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.forumpage_validateversion(connectionString, appVersion);
                 // case "oracle":  return orPostgre.Db.forumpage_validateversion(connectionString, appVersion);
                 // case "db2":  return db2Postgre.Db.forumpage_validateversion(connectionString, appVersion);
                 // case "other":  return othPostgre.Db.forumpage_validateversion(connectionString, appVersion); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static DataTable GetSearchResult(string toSearchWhat, string toSearchFromWho, SearchWhatFlags searchFromWhoMethod,
                                  SearchWhatFlags searchWhatMethod, int forumIDToStartAt, int userId, int boardId,
                                  int maxResults, bool useFullText, bool searchDisplayName)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 case "Npgsql": return Postgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 // case "oracle":  return orPostgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 // case "db2":  return db2Postgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName);
                 // case "other":  return othPostgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void group_delete(object groupID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.group_delete(connectionString, groupID); break;
                 case "Npgsql": Postgre.Db.group_delete(connectionString, groupID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.group_delete(connectionString, groupID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.group_delete(connectionString, groupID); break;;
                 // case "oracle":   orPostgre.Db.group_delete(connectionString, groupID); break;;
                 // case "db2":   db2Postgre.Db.group_delete(connectionString, groupID); break;
                 // case "other":   othPostgre.Db.group_delete(connectionString, groupID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable group_list(object boardId, object groupID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.group_list(connectionString, boardId, groupID);
                 case "Npgsql": return Postgre.Db.group_list(connectionString, boardId, groupID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.group_list(connectionString, boardId, groupID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_list(connectionString, boardId, groupID);
                 // case "oracle":  return orPostgre.Db.group_list(connectionString, boardId, groupID);
                 // case "db2":  return db2Postgre.Db.group_list(connectionString, boardId, groupID);
                 // case "other":  return othPostgre.Db.group_list(connectionString, boardId, groupID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void group_medal_delete(object groupID, object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.group_medal_delete(connectionString, groupID, medalID); break;
                 case "Npgsql": Postgre.Db.group_medal_delete(connectionString, groupID, medalID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.group_medal_delete(connectionString, groupID, medalID); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.group_medal_delete(connectionString, groupID, medalID); break;
                 // case "oracle":   orPostgre.Db.group_medal_delete(connectionString, groupID, medalID);break;
                 // case "db2":   db2Postgre.Db.group_medal_delete(connectionString, groupID, medalID); break;
                 // case "other":   othPostgre.Db.group_medal_delete(connectionString, groupID, medalID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable group_medal_list(object groupID, object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.group_medal_list(connectionString, groupID, medalID);
                 case "Npgsql": return Postgre.Db.group_medal_list(connectionString, groupID, medalID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.group_medal_list(connectionString, groupID, medalID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_medal_list(connectionString, groupID, medalID);
                 // case "oracle":  return orPostgre.Db.group_medal_list(connectionString, groupID, medalID);
                 // case "db2":  return db2Postgre.Db.group_medal_list(connectionString, groupID, medalID);
                 // case "other":  return othPostgre.Db.group_medal_list(connectionString, groupID, medalID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void group_medal_save(
            object groupID, object medalID, object message,
            object hide, object onlyRibbon, object sortOrder)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                 case "Npgsql": Postgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder); break;
                 // case "oracle":   orPostgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder);break;
                 // case "db2":   db2Postgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                 // case "other":   othPostgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable group_member(object boardId, object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.group_member(connectionString, boardId, userId);
                 case "Npgsql": return Postgre.Db.group_member(connectionString, boardId, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.group_member(connectionString, boardId, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_member(connectionString, boardId, userId);
                 // case "oracle":  return orPostgre.Db.group_member(connectionString, boardId, userId);
                 // case "db2":  return db2Postgre.Db.group_member(connectionString, boardId, userId);
                 // case "other":  return othPostgre.Db.group_member(connectionString, boardId, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable group_rank_style(object boardID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.group_rank_style(connectionString, boardID);
                 case "Npgsql": return Postgre.Db.group_rank_style(connectionString, boardID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.group_rank_style(connectionString, boardID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_rank_style(connectionString, boardID);
                 // case "oracle":  return orPostgre.Db.group_rank_style(connectionString, boardID);
                 // case "db2":  return db2Postgre.Db.group_rank_style(connectionString, boardID);
                 // case "other":  return othPostgre.Db.group_rank_style(connectionString, boardID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static long group_save(object groupID, object boardId, object name,
                        object isAdmin, object isGuest, object isStart, object isModerator,
                        object accessMaskID, object pmLimit, object style, object sortOrder,
                        object description,
                        object usrSigChars,
                        object usrSigBBCodes,
                        object usrSigHTMLTags,
                        object usrAlbums,
                        object usrAlbumImages)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 case "Npgsql": return Postgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 // case "oracle":  return orPostgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 // case "db2":  return db2Postgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages);
                 // case "other":  return othPostgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void mail_create(object from, object fromName, object to, object toName, object subject, object body,
                         object bodyHtml)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 case "Npgsql": Postgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 // case "oracle":   orPostgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);break;
                 // case "db2":   db2Postgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 // case "other":   othPostgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void mail_createwatch(object topicID, object from, object fromName, object subject, object body, object bodyHtml,
                              object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 case "Npgsql": Postgre.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.mail_createwatch(connectionString, topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 // case "oracle":   orPostgre.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId);break;
                 // case "db2":   db2_mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 // case "other":   othPostgre.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void mail_delete(object mailID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.mail_delete(connectionString, mailID); break;
                 case "Npgsql": Postgre.Db.mail_delete(connectionString, mailID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.mail_delete(connectionString, mailID); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.mail_delete(connectionString, mailID); break;
                 // case "oracle":   orPostgre.Db.mail_delete(connectionString, mailID);break;
                 // case "db2":   db2_mail_delete(connectionString, mailID); break;
                 // case "other":   othPostgre.Db.mail_delete(connectionString, mailID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static IEnumerable<TypedMailList> MailList(long processId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.MailList(connectionString, processId);
                 case "Npgsql": return Postgre.Db.MailList(connectionString, processId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.MailList(connectionString, processId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.MailList(connectionString, processId);
                 // case "oracle":  return orPostgre.Db.MailList(connectionString, processId);
                 // case "db2":  return db2Postgre.Db.MailList(connectionString, processId);
                 // case "other":  return othPostgre.Db.MailList(connectionString, processId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         /// <summary>
         /// Deletes given medals.
         /// </summary>
         /// <param name="boardId">ID of board of which medals to delete. Required.</param>
         /// <param name="category">Cateogry of medals to delete. Can be null. In such case this parameter is ignored.</param>
         static public void medal_delete(object boardId, object category)
         {
             medal_delete(boardId, null, category);
         }

         /// <summary>
         /// Deletes given medal.
         /// </summary>
         /// <param name="medalID">ID of medal to delete.</param>
         static public void medal_delete(object medalID)
         {
             medal_delete(null, medalID, null);
         }

         public static void medal_delete(object boardId, object medalID, object category)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.medal_delete(connectionString, boardId,  medalID, category); break;
                 case "Npgsql": Postgre.Db.medal_delete(connectionString, boardId,  medalID, category); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.medal_delete(connectionString, boardId,  medalID, category); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.medal_delete(connectionString, boardId, medalID, category); break;
                 // case "oracle":   orPostgre.Db.medal_delete(connectionString, boardId,  medalID, category);break;
                 // case "db2":   db2Postgre.Db.medal_delete(connectionString, boardId,  medalID, category); break;
                 // case "other":   othPostgre.Db.medal_delete(connectionString, boardId,  medalID, category); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         /// <summary>
         /// Lists given medal.
         /// </summary>
         /// <param name="medalID">ID of medal to list.</param>
         static public DataTable medal_list(object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.medal_list(connectionString, null, medalID, null);
                 case "Npgsql": return Postgre.Db.medal_list(connectionString, null, medalID, null);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.medal_list(connectionString, null, medalID, null);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.medal_list(connectionString, null, medalID, null);
                 // case "oracle":  return orPostgre.Db.medal_list(connectionString, null, medalID, null);
                 // case "db2":  return db2Postgre.Db.medal_list(connectionString, null, medalID, null);
                 // case "other":  return othPostgre.Db.medal_list(connectionString, null, medalID, null); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

             
         }
         /// <summary>
         /// Lists given medals.
         /// </summary>
         /// <param name="boardId">ID of board of which medals to list. Required.</param>
         /// <param name="category">Cateogry of medals to list. Can be null. In such case this parameter is ignored.</param>
         static public DataTable medal_list(object boardId, object category)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.medal_list(connectionString, boardId, null, category);
                 case "Npgsql": return Postgre.Db.medal_list(connectionString, boardId, null, category);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.medal_list(connectionString, boardId, null, category);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.medal_list(connectionString, boardId, null, category);
                 // case "oracle":  return orPostgre.Db.medal_list(connectionString, boardId, null, category);
                 // case "db2":  return db2Postgre.Db.medal_list(connectionString, boardId, null, category);
                 // case "other":  return othPostgre.Db.medal_list(connectionString, boardId, null, category); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
      
        // DataTable mail_list(object processId); 

         /// <summary>
         /// Lists given medals.
         /// </summary>
         /// <param name="boardId">ID of board of which medals to list. Required.</param>
         /// <param name="category">Cateogry of medals to list. Can be null. In such case this parameter is ignored.</param>
         static public DataTable medal_listusers(object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.medal_listusers(connectionString, medalID);
                 case "Npgsql": return Postgre.Db.medal_listusers(connectionString, medalID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.medal_listusers(connectionString, medalID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.medal_listusers(connectionString, medalID);
                 // case "oracle":  return orPostgre.Db.medal_listusers(connectionString, medalID);
                 // case "db2":  return db2Postgre.Db.medal_listusers(connectionString, medalID);
                 // case "other":  return othPostgre.Db.medal_listusers(connectionString, medalID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void medal_resort(object boardId, object medalID, int move)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.medal_resort(connectionString, boardId, medalID, move); break;
                 case "Npgsql": Postgre.Db.medal_resort(connectionString, boardId, medalID, move); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.medal_resort(connectionString, boardId, medalID, move); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.medal_resort(connectionString, boardId, medalID, move); break;
                 // case "oracle":   orPostgre.Db.medal_resort(connectionString, boardId, medalID, move);break;
                 // case "db2":   db2Postgre.Db.medal_resort(connectionString, boardId, medalID, move); break;
                 // case "other":   othPostgre.Db.medal_resort(connectionString, boardId, medalID, move); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public bool medal_save(
            object boardId, object medalID, object name, object description, object message, object category,
            object medalURL, object ribbonURL, object smallMedalURL, object smallRibbonURL, object smallMedalWidth,
            object smallMedalHeight, object smallRibbonWidth, object smallRibbonHeight, object sortOrder, object flags)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 case "Npgsql": return Postgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 // case "oracle":  return orPostgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 // case "db2":  return db2Postgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                 // case "other":  return othPostgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public string message_AddThanks(object fromUserID, object messageID, bool useDisplayName)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName);
                 case "Npgsql": return Postgre.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                 // case "oracle":  return or_message_AddThanks(connectionString, fromUserID, messageID,useDisplayName,useDisplayName);
                 // case "db2":  return db2Postgre.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName);
                 // case "other":  return othPostgre.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void message_approve(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_approve(connectionString, messageID); break;
                 case "Npgsql": Postgre.Db.message_approve(connectionString, messageID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_approve(connectionString, messageID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_approve(connectionString, messageID); break;
                 // case "oracle":   orPostgre.Db.message_approve(connectionString, messageID);break;
                 // case "db2":   db2Postgre.Db.message_approve(connectionString, messageID); break;
                 // case "other":   othPostgre.Db.message_approve(connectionString, messageID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public void message_delete(object messageID, bool isModeratorChanged, string deleteReason, int isDeleteAction, bool DeleteLinked)
         {
             message_delete(messageID, isModeratorChanged, deleteReason, isDeleteAction, DeleteLinked, false);
         }

         public static void message_delete(object messageID, bool isModeratorChanged, string deleteReason, int isDeleteAction,
                            bool DeleteLinked, bool eraseMessage)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 case "Npgsql": Postgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 // case "oracle":   orPostgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage);break;
                 // case "db2":   db2Postgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 // case "other":   othPostgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable message_findunread(object topicID, object messageId, object lastRead, object showDeleted,
                                     object authorUserID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 case "Npgsql": return Postgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 // case "oracle":  return orPostgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 // case "db2":  return db2Postgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                 // case "other":  return othPostgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_getRepliesList(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_getRepliesList(connectionString, messageID);
                 case "Npgsql": return Postgre.Db.message_getRepliesList(connectionString, messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_getRepliesList(connectionString, messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_getRepliesList(connectionString, messageID);
                 // case "oracle":  return orPostgre.Db.message_getRepliesList(connectionString, messageID);
                 // case "db2":  return db2Postgre.Db.message_getRepliesList(connectionString, messageID);
                 // case "other":  return othPostgre.Db.message_getRepliesList(connectionString, messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_GetTextByIds(string messageIDs)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_GetTextByIds(connectionString, messageIDs);
                 case "Npgsql": return Postgre.Db.message_GetTextByIds(connectionString, messageIDs);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_GetTextByIds(connectionString, messageIDs);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_GetTextByIds(connectionString, messageIDs);
                 // case "oracle":  return orPostgre.Db.message_GetTextByIds(connectionString, messageIDs);
                 // case "db2":  return db2Postgre.Db.message_GetTextByIds(connectionString, messageIDs);
                 // case "other":  return othPostgre.Db.message_GetTextByIds(connectionString, messageIDs); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_GetThanks(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_GetThanks(connectionString, messageID);
                 case "Npgsql": return Postgre.Db.message_GetThanks(connectionString, messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_GetThanks(connectionString, messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_GetThanks(connectionString, messageID);
                 // case "oracle":  return orPostgre.Db.message_GetThanks(connectionString, messageID);
                 // case "db2":  return db2Postgre.Db.message_GetThanks(connectionString, messageID);
                 // case "other":  return othPostgre.Db.message_GetThanks(connectionString, messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_list(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_list(connectionString, messageID);
                 case "Npgsql": return Postgre.Db.message_list(connectionString, messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_list(connectionString, messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_list(connectionString, messageID);
                 // case "oracle":  return orPostgre.Db.message_list(connectionString, messageID);
                 // case "db2":  return db2Postgre.Db.message_list(connectionString, messageID);
                 // case "other":  return othPostgre.Db.message_list(connectionString, messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_listreported(object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_listreported(connectionString, forumID);
                 case "Npgsql": return Postgre.Db.message_listreported(connectionString, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_listreported(connectionString, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_listreported(connectionString, forumID);
                 // case "oracle":  return orPostgre.Db.message_listreported(connectionString, forumID);
                 // case "db2":  return db2Postgre.Db.message_listreported(connectionString, forumID);
                 // case "other":  return othPostgre.Db.message_listreported(connectionString, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }


         /// <summary>
         /// Here we get reporters list for a reported message
         /// </summary>       
         /// <param name="MessageID">Should not be NULL</param>
         /// <returns>Returns reporters DataTable for a reported message.</returns>
         static public DataTable message_listreporters(int messageID)
         {

             return message_listreporters(messageID, null);

         }
         static public DataTable message_listreporters(int messageID, object userID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_listreporters(connectionString, messageID, userID);
                 case "Npgsql": return Postgre.Db.message_listreporters(connectionString, messageID, userID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_listreporters(connectionString, messageID, userID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_listreporters(connectionString, messageID, userID);
                 // case "oracle":  return orPostgre.Db.message_listreporters(connectionString, messageID, userID);
                 // case "db2":  return db2Postgre.Db.message_listreporters(connectionString, messageID, userID);
                 // case "other":  return othPostgre.Db.message_listreporters(connectionString, messageID, userID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void message_move(object messageID, object moveToTopic, bool moveAll)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 case "Npgsql": Postgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 // case "oracle":   orPostgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll);break;
                 // case "db2":   db2Postgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 // case "other":   othPostgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public string message_RemoveThanks(object fromUserID, object messageID, bool useDisplayName)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                 case "Npgsql": return Postgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID, useDisplayName);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_RemoveThanks(connectionString, fromUserID, messageID, useDisplayName);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                 // case "oracle":  return orPostgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                 // case "db2":  return db2Postgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                 // case "other":  return othPostgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void message_report(object messageID, object userId, object reportedDateTime, object reportText)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 case "Npgsql": Postgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 // case "oracle":   orPostgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);break;
                 // case "db2":   db2Postgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 // case "other":   othPostgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void message_reportcopyover(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_reportcopyover(connectionString, messageID); break;
                 case "Npgsql": Postgre.Db.message_reportcopyover(connectionString, messageID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_reportcopyover(connectionString, messageID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_reportcopyover(connectionString, messageID); break;
                 // case "oracle":   orPostgre.Db.message_reportcopyover(connectionString, messageID);break;
                 // case "db2":   db2Postgre.Db.message_reportcopyover(connectionString, messageID); break;
                 // case "other":   othPostgre.Db.message_reportcopyover(connectionString, messageID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void message_reportresolve(object messageFlag, object messageID, object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 case "Npgsql": Postgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 // case "oracle":   orPostgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);break;
                 // case "db2":   db2Postgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 // case "other":   othPostgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public bool message_save(
            [NotNull] object topicId,
            [NotNull] object userId,
            [NotNull] object message,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object replyTo,
            [NotNull] object flags,
            ref long messageId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 case "Npgsql": return Postgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 // case "oracle":  return orPostgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 // case "db2":  return db2Postgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                 // case "other":  return othPostgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_secdata(int MessageID, object pageUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_secdata(connectionString, MessageID, pageUserId);
                 case "Npgsql": return Postgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_secdata(connectionString, MessageID, pageUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_secdata(connectionString, MessageID, pageUserId);
                 // case "oracle":  return orPostgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                 // case "db2":  return db2Postgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                 // case "other":  return othPostgre.Db.message_secdata(connectionString, MessageID, pageUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_simplelist(int StartID, int Limit)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_simplelist(connectionString, StartID, Limit);
                 case "Npgsql": return Postgre.Db.message_simplelist(connectionString, StartID, Limit);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_simplelist(connectionString, StartID, Limit);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_simplelist(connectionString, StartID, Limit);
                 // case "oracle":  return orPostgre.Db.message_simplelist(connectionString, StartID, Limit);
                 // case "db2":  return db2Postgre.Db.message_simplelist(connectionString, StartID, Limit);
                 // case "other":  return othPostgre.Db.message_simplelist(connectionString, StartID, Limit); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public int message_ThanksNumber(object messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_ThanksNumber(connectionString, messageID);
                 case "Npgsql": return Postgre.Db.message_ThanksNumber(connectionString, messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_ThanksNumber(connectionString, messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_ThanksNumber(connectionString, messageID);
                 // case "oracle":  return orPostgre.Db.message_ThanksNumber(connectionString, messageID);
                 // case "db2":  return db2Postgre.Db.message_ThanksNumber(connectionString, messageID);
                 // case "other":  return othPostgre.Db.message_ThanksNumber(connectionString, messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable message_unapproved(object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.message_unapproved(connectionString, forumID);
                 case "Npgsql": return Postgre.Db.message_unapproved(connectionString, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.message_unapproved(connectionString, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.message_unapproved(connectionString, forumID);
                 // case "oracle":  return orPostgre.Db.message_unapproved(connectionString, forumID);
                 // case "db2":  return db2Postgre.Db.message_unapproved(connectionString, forumID);
                 // case "other":  return othPostgre.Db.message_unapproved(connectionString, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void message_update(object messageID, object priority, object message,object description, object status, object styles, object subject,
                            object flags, object reasonOfEdit, object isModeratorChanged, object overrideApproval,
                            object origMessage, object editedBy)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.message_update(connectionString, messageID, priority, message, description,status,styles, subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy); break;
                 case "Npgsql": Postgre.Db.message_update(connectionString, messageID, priority, message, description, status, styles, subject, flags, reasonOfEdit, isModeratorChanged, overrideApproval, origMessage, editedBy); break;
                 case "MySql.Data.MySqlClient": MySqlDb.Db.message_update(connectionString, messageID, priority, message, description, status, styles, subject, flags, reasonOfEdit, isModeratorChanged, overrideApproval, origMessage, editedBy); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.message_update(connectionString, messageID, priority, message, description, status, styles, subject, flags, reasonOfEdit, isModeratorChanged, overrideApproval, origMessage, editedBy); break;
                 // case "oracle":   orPostgre.Db.message_update(connectionString, messageID, priority, message, description, status,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy);break;
                 // case "db2":   db2Postgre.Db.message_update(connectionString, messageID, priority, message, description, status,styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy); break;
                 // case "other":   othPostgre.Db.message_update(connectionString, messageID, priority, message, description, status, styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public IEnumerable<TypedAllThanks> MessageGetAllThanks(string messageIdsSeparatedWithColon)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 case "Npgsql": return Postgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 // case "oracle":  return orPostgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 // case "db2":  return db2Postgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                 // case "other":  return othPostgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable messagehistory_list(int messageID, int daysToClean)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.messagehistory_list(connectionString, messageID, daysToClean);
                 case "Npgsql": return Postgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.messagehistory_list(connectionString, messageID, daysToClean);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.messagehistory_list(connectionString, messageID, daysToClean);
                 // case "oracle":  return orPostgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                 // case "db2":  return db2Postgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                 // case "other":  return othPostgre.Db.messagehistory_list(connectionString, messageID, daysToClean); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         static public IEnumerable<TypedMessageList> MessageList(int messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.MessageList(connectionString, messageID);
                 case "Npgsql": return Postgre.Db.MessageList(connectionString, messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.MessageList(connectionString, messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.MessageList(connectionString, messageID);
                 // case "oracle":  return orPostgre.Db.MessageList(connectionString, messageID);
                 // case "db2":  return db2Postgre.Db.MessageList(connectionString, messageID);
                 // case "other":  return othPostgre.Db.MessageList(connectionString, messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static DataTable moderators_team_list(bool useStyledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.moderators_team_list( connectionString,  useStyledNicks);
                 case "Npgsql": return Postgre.Db.moderators_team_list( connectionString,  useStyledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.moderators_team_list( connectionString,  useStyledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.moderators_team_list( connectionString,  useStyledNicks);
                 // case "oracle":  return orPostgre.Db.moderators_team_list( connectionString,  useStyledNicks);
                 // case "db2":  return db2Postgre.Db.moderators_team_list( connectionString,  useStyledNicks);
                 // case "other":  return othPostgre.Db.moderators_team_list( connectionString,  useStyledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void Readtopic_AddOrUpdate([NotNull] object userID, [NotNull] object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 case "Npgsql": Postgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 // case "oracle":   orPostgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID);break;
                 // case "db2":   db2Postgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 // case "other":   othPostgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         /// <summary>
         /// Delete the Read Tracking
         /// </summary>
         /// <param name="trackingID">
         /// The tracking id.
         /// </param>
        /* public static void ReadTopic_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.Readtopic_delete(connectionString, trackingID); break;
                 case "Npgsql": Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case "oracle":   orPostgre.Db.Readtopic_delete(connectionString, trackingID);break;
                 // case "db2":   db2Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case "other":   othPostgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         } */
         /// <summary>
         /// Get the Global Last Read DateTime User
         /// </summary>
         /// <param name="userID">
         /// The user ID.
         /// </param>
         /// <param name="lastVisitDate">
         /// The last Visit Date of the User
         /// </param>
         /// <returns>
         /// Returns the Global Last Read DateTime
         /// </returns>
         public static DateTime? User_LastRead([NotNull] object userID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.User_LastRead( connectionString,  userID);
                 case "Npgsql": return Postgre.Db.User_LastRead(connectionString, userID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.User_LastRead(connectionString, userID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.User_LastRead(connectionString, userID);
                 // case "oracle":  return orPostgre.Db.User_LastRead( connectionString,  userID);
                 // case "db2":  return db2Postgre.Db.User_LastRead( connectionString,  userID);
                 // case "other":  return othPostgre.Db.User_LastRead( connectionString,  userID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DateTime? Readtopic_lastread([NotNull] object userID, [NotNull] object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.Readtopic_lastread(connectionString, userID, topicID);
                 case "Npgsql": return Postgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.Readtopic_lastread(connectionString, userID, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Readtopic_lastread(connectionString, userID, topicID);
                 // case "oracle":  return orPostgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                 // case "db2":  return db2Postgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                 // case "other":  return othPostgre.Db.Readtopic_lastread(connectionString, userID, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void ReadForum_AddOrUpdate([NotNull] object userID, [NotNull] object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 case "Npgsql": Postgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 // case "oracle":   orPostgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID);break;
                 // case "db2":   db2Postgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 // case "other":   othPostgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         /* public static void ReadForum_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.ReadForum_delete(connectionString, trackingID); break;
                 case "Npgsql": Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case "oracle":   orPostgre.Db.ReadForum_delete(connectionString, trackingID);break;
                 // case "db2":   db2Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case "other":   othPostgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         } */

         public static DateTime ReadForum_lastread([NotNull] object userID, [NotNull] object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.ReadForum_lastread(connectionString,userID, forumID);
                 case "Npgsql": return Postgre.Db.ReadForum_lastread(connectionString,userID, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.ReadForum_lastread(connectionString,userID, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.ReadForum_lastread(connectionString,userID, forumID);
                 // case "oracle":  return orPostgre.Db.ReadForum_lastread(connectionString,userID, forumID);
                 // case "db2":  return db2Postgre.Db.ReadForum_lastread(connectionString,userID, forumID);
                 // case "other":  return othPostgre.Db.ReadForum_lastread(connectionString,userID, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void  nntpforum_delete(object nntpForumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntpforum_delete(connectionString, nntpForumID); break;
                 case "Npgsql": Postgre.Db.nntpforum_delete(connectionString, nntpForumID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntpforum_delete(connectionString, nntpForumID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntpforum_delete(connectionString, nntpForumID); break;
                 // case "oracle":   orPostgre.Db.nntpforum_delete(connectionString, nntpForumID);break;
                 // case "db2":   db2Postgre.Db.nntpforum_delete(connectionString, nntpForumID); break;
                 // case "other":   othPostgre.Db.nntpforum_delete(connectionString, nntpForumID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public DataTable nntpforum_list(object boardId, object minutes, object nntpForumID, object active)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 case "Npgsql": return Postgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 // case "oracle":  return orPostgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 // case "db2":  return db2Postgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                 // case "other":  return othPostgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void nntpforum_save(object nntpForumID, object nntpServerID, object groupName, object forumID, object active,
                            object cutoffdate)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 case "Npgsql": Postgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 // case "oracle":   orPostgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);break;
                 // case "db2":   db2Postgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 // case "other":   othPostgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void nntpforum_update(object nntpForumID, object lastMessageNo, object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 case "Npgsql": Postgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 // case "oracle":   orPostgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);break;
                 // case "db2":   db2Postgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 // case "other":   othPostgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public IEnumerable<TypedNntpForum> NntpForumList(int boardId, int? minutes, int? nntpForumID, bool? active)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 case "Npgsql": return Postgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 // case "oracle":  return orPostgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 // case "db2":  return db2Postgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                 // case "other":  return othPostgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void nntpserver_delete(object nntpServerID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntpserver_delete(connectionString, nntpServerID); break;
                 case "Npgsql": Postgre.Db.nntpserver_delete(connectionString, nntpServerID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntpserver_delete(connectionString, nntpServerID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntpserver_delete(connectionString, nntpServerID); break;
                 // case "oracle":   orPostgre.Db.nntpserver_delete(connectionString, nntpServerID);break;
                 // case "db2":   db2Postgre.Db.nntpserver_delete(connectionString, nntpServerID); break;
                 // case "other":   othPostgre.Db.nntpserver_delete(connectionString, nntpServerID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public DataTable nntpserver_list(object boardId, object nntpServerID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.nntpserver_list(connectionString,  boardId, nntpServerID);
                 case "Npgsql": return Postgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                 // case "oracle":  return orPostgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                 // case "db2":  return db2Postgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                 // case "other":  return othPostgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void nntpserver_save(object nntpServerID, object boardId, object name, object address, object port,
                             object userName, object userPass)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 case "Npgsql": Postgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 // case "oracle":   orPostgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass);break;
                 // case "db2":   db2Postgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 // case "other":   othPostgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public DataTable nntptopic_list(object thread)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.nntptopic_list(connectionString, thread);
                 case "Npgsql": return Postgre.Db.nntptopic_list(connectionString, thread);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.nntptopic_list(connectionString, thread);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.nntptopic_list(connectionString, thread);
                 // case "oracle":  return orPostgre.Db.nntptopic_list(connectionString, thread);
                 // case "db2":  return db2Postgre.Db.nntptopic_list(connectionString, thread);
                 // case "other":  return othPostgre.Db.nntptopic_list(connectionString, thread); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void nntptopic_savemessage(
            object nntpForumID,
            object topic,
            object body,
            object userId,
            object userName,
            object ip,
            object posted,
            object externalMessageId,
            object referenceMessageId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 case "Npgsql": Postgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 // case "oracle":   orPostgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId);break;
                 // case "db2":   db2Postgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 // case "other":   othPostgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataRow pageload(
            object sessionID,
            object boardId,
            object userKey,
            object ip,
            object location,
            object forumPage,
            object browser,
            object platform,
            object categoryID,
            object forumID,
            object topicID,
            object messageID,
            object isCrawler,
            object isMobileDevice,
            object donttrack)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 case "Npgsql": return Postgre.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 // case "oracle":  return orPostgre.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 // case "db2":  return db2Postgre.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack);
                 // case "other":  return othPostgre.Db.pageload(connectionString, sessionID, boardId, userKey, ip, location, forumPage, browser, platform,categoryID, forumID, topicID, messageID, isCrawler, isMobileDevice, donttrack); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void pmessage_archive(object userPMessageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pmessage_archive(connectionString, userPMessageID); break;
                 case "Npgsql": Postgre.Db.pmessage_archive(connectionString, userPMessageID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pmessage_archive(connectionString, userPMessageID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pmessage_archive(connectionString, userPMessageID); break;
                 // case "oracle":   orPostgre.Db.pmessage_archive(connectionString, userPMessageID);break;
                 // case "db2":   db2Postgre.Db.pmessage_archive(connectionString, userPMessageID); break;
                 // case "other":   othPostgre.Db.pmessage_archive(connectionString, userPMessageID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         /// <summary>
         /// Deletes the private message from the database as per the given parameter.  If fromOutbox is true,
         /// the message is only deleted from the user's outbox.  Otherwise, it is completely delete from the database.
         /// </summary>
         /// <param name="userPMessageID"></param>
         static public void pmessage_delete(object userPMessageID)
         {
             pmessage_delete(userPMessageID, false);
         }
         public static void pmessage_delete(object userPMessageID, bool fromOutbox)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 case "Npgsql": Postgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 // case "oracle":   orPostgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);break;
                 // case "db2":   db2Postgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 // case "other":   othPostgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable pmessage_info()
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pmessage_info(connectionString);
                 case "Npgsql": return Postgre.Db.pmessage_info(connectionString);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pmessage_info(connectionString);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pmessage_info(connectionString);
                 // case "oracle":  return orPostgre.Db.pmessage_info(connectionString);
                 // case "db2":  return db2Postgre.Db.pmessage_info(connectionString);
                 // case "other":  return othPostgre.Db.pmessage_info(connectionString); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         /// <summary>
         /// Returns a list of private messages based on the arguments specified.
         /// If pMessageID != null, returns the PM of id pMessageId.
         /// If toUserID != null, returns the list of PMs sent to the user with the given ID.
         /// If fromUserID != null, returns the list of PMs sent by the user of the given ID.
         /// </summary>
         /// <param name="toUserID"></param>
         /// <param name="fromUserID"></param>
         /// <param name="pMessageID">The id of the private message</param>
         /// <returns></returns>
         static public DataTable pmessage_list(object userPMessageID)
         {
             return pmessage_list(null, null, userPMessageID);
         }
         static public DataTable pmessage_list(object toUserID, object fromUserID, object userPMessageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 case "Npgsql": return Postgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 // case "oracle":  return orPostgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 // case "db2":  return db2Postgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                 // case "other":  return othPostgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void pmessage_markread(object userPMessageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pmessage_markread(connectionString, userPMessageID); break;
                 case "Npgsql": Postgre.Db.pmessage_markread(connectionString, userPMessageID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pmessage_markread(connectionString, userPMessageID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pmessage_markread(connectionString, userPMessageID); break;
                 // case "oracle":   orPostgre.Db.pmessage_markread(connectionString, userPMessageID);break;
                 // case "db2":   db2Postgre.Db.pmessage_markread(connectionString, userPMessageID); break;
                 // case "other":   othPostgre.Db.pmessage_markread(connectionString, userPMessageID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void pmessage_prune(object daysRead, object daysUnread)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 case "Npgsql": Postgre.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 // case "oracle":   orPostgre.Db.pmessage_prune(connectionString, daysRead, daysUnread);break;
                 // case "db2":   db2Postgre.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 // case "other":   othPostgre.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void pmessage_save(object fromUserID, object toUserID, object subject, object body, object Flags, object replyTo)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 case "Npgsql": Postgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 // case "oracle":   orPostgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo);break;
                 // case "db2":   db2Postgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 // case "other":   othPostgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void poll_remove(object pollGroupID, object pollID, object boardId, bool removeCompletely, bool removeEverywhere)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 case "Npgsql": Postgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 // case "oracle":   orPostgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);break;
                 // case "db2":   db2Postgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 // case "other":   othPostgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int? poll_save(List<PollSaveList> pollList)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.poll_save(connectionString, pollList);
                 case "Npgsql": return Postgre.Db.poll_save(connectionString, pollList);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.poll_save(connectionString, pollList);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.poll_save(connectionString, pollList);
                 // case "oracle":  return orPostgre.Db.poll_save(connectionString, pollList);
                 // case "db2":  return db2Postgre.Db.poll_save(connectionString, pollList);
                 // case "other":  return othPostgre.Db.poll_save(connectionString, pollList); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable poll_stats(int? pollId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.poll_stats(connectionString, pollId);
                 case "Npgsql": return Postgre.Db.poll_stats(connectionString, pollId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.poll_stats(connectionString, pollId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.poll_stats(connectionString, pollId);
                 // case "oracle":  return orPostgre.Db.poll_stats(connectionString, pollId);
                 // case "db2":  return db2Postgre.Db.poll_stats(connectionString, pollId);
                 // case "other":  return othPostgre.Db.poll_stats(connectionString, pollId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public int pollgroup_attach(int? pollGroupId, int? topicId, int? forumId, int? categoryId, int? boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 case "Npgsql": return Postgre.Db.pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 // case "oracle":  return orPostgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 // case "db2":  return db2Postgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                 // case "other":  return othPostgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void pollgroup_remove(object pollGroupID, object topicId, object forumId, object categoryId, object boardId,
                              bool removeCompletely, bool removeEverywhere)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 case "Npgsql": Postgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 // case "oracle":   orPostgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere);break;
                 // case "db2":   db2Postgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 // case "other":   othPostgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable pollgroup_stats(int? pollGroupId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pollgroup_stats(connectionString, pollGroupId);
                 case "Npgsql": return Postgre.Db.pollgroup_stats(connectionString, pollGroupId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pollgroup_stats(connectionString, pollGroupId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pollgroup_stats(connectionString, pollGroupId);
                 // case "oracle":  return orPostgre.Db.pollgroup_stats(connectionString, pollGroupId);
                 // case "db2":  return db2Postgre.Db.pollgroup_stats(connectionString, pollGroupId);
                 // case "other":  return othPostgre.Db.pollgroup_stats(connectionString, pollGroupId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void poll_update(object pollID, object question, object closes, object isBounded, bool isClosedBounded, bool allowMultipleChoices, bool showVoters, bool allowSkipVote, object questionPath, object questionMime)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 case "Npgsql": Postgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 // case "oracle":   orPostgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime);break;
                 // case "db2":   db2Postgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 // case "other":   othPostgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable pollgroup_votecheck(object pollGroupId, object userId, object remoteIp)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 case "Npgsql": return Postgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 // case "oracle":  return orPostgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 // case "db2":  return db2Postgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                 // case "other":  return othPostgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public IEnumerable<TypedPollGroup> PollGroupList(int userID, int? forumId, int boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.PollGroupList(connectionString, userID, forumId, boardId);
                 case "Npgsql": return Postgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.PollGroupList(connectionString, userID, forumId, boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PollGroupList(connectionString, userID, forumId, boardId);
                 // case "oracle":  return orPostgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                 // case "db2":  return db2Postgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                 // case "other":  return othPostgre.Db.PollGroupList(connectionString, userID, forumId, boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable pollvote_check(object pollid, object userid, object remoteip)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 case "Npgsql": return Postgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 // case "oracle":  return orPostgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 // case "db2":  return db2Postgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                 // case "other":  return othPostgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable post_alluser(object boardid, object userid, object pageUserID, object topCount)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 case "Npgsql": return Postgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 // case "oracle":  return orPostgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 // case "db2":  return db2Postgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                 // case "other":  return othPostgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         static public DataTable post_list(
            object topicId,
            object currentUserID,
            object authoruserId,
            object updateViewCount,
            bool showDeleted,
            bool styledNicks,
            bool showReputation,
            DateTime sincePostedDate,
            DateTime toPostedDate,
            DateTime sinceEditedDate,
            DateTime toEditedDate,
            int pageIndex,
            int pageSize,
            int sortPosted,
            int sortEdited,
            int sortPosition,
            bool showThanks,
            int messagePosition)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 case "Npgsql": return Postgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 // case "oracle":  return orPostgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 // case "db2":  return db2Postgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                 // case "other":  return othPostgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable post_list_reverse10(object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.post_list_reverse10(connectionString, topicID);
                 case "Npgsql": return Postgre.Db.post_list_reverse10(connectionString, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.post_list_reverse10(connectionString, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.post_list_reverse10(connectionString, topicID);
                 // case "oracle":  return orPostgre.Db.post_list_reverse10(connectionString, topicID);
                 // case "db2":  return db2Postgre.Db.post_list_reverse10(connectionString, topicID);
                 // case "other":  return othPostgre.Db.post_list_reverse10(connectionString, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void rank_delete(object rankID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.rank_delete(connectionString, rankID); break;
                 case "Npgsql": Postgre.Db.rank_delete(connectionString, rankID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.rank_delete(connectionString, rankID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.rank_delete(connectionString, rankID); break;
                 // case "oracle":   orPostgre.Db.rank_delete(connectionString, rankID);break;
                 // case "db2":   db2Postgre.Db.rank_delete(connectionString, rankID); break;
                 // case "other":   othPostgre.Db.rank_delete(connectionString, rankID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable rank_list(object boardId, object rankID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.rank_list(connectionString, boardId, rankID);
                 case "Npgsql": return Postgre.Db.rank_list(connectionString, boardId, rankID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.rank_list(connectionString, boardId, rankID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.rank_list(connectionString, boardId, rankID);
                 // case "oracle":  return orPostgre.Db.rank_list(connectionString, boardId, rankID);
                 // case "db2":  return db2Postgre.Db.rank_list(connectionString, boardId, rankID);
                 // case "other":  return othPostgre.Db.rank_list(connectionString, boardId, rankID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void rank_save(object rankID, object boardId, object name,
                       object isStart, object isLadder, object minPosts, object rankImage,
                       object pmLimit, object style, object sortOrder,
                       object description,
                       object usrSigChars,
                       object usrSigBBCodes,
                       object usrSigHTMLTags,
                       object usrAlbums,
                       object usrAlbumImages)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 case "Npgsql": Postgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 // case "oracle":   orPostgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages);break;
                 // case "db2":   db2Postgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 // case "other":   othPostgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable recent_users(object boardID, int timeSinceLastLogin, object styledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 case "Npgsql": return Postgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 // case "oracle":  return orPostgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 // case "db2":  return db2Postgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                 // case "other":  return othPostgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         /// <summary>
         /// Retrieves all the entries in the board settings registry
         /// </summary>
         /// <returns>DataTable filled will all registry entries</returns>
         static public DataTable registry_list()
         {
             return registry_list(null, null);
         }
         /// <summary>
         /// Retrieves entries in the board settings registry
         /// </summary>
         /// <param name="Name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
         /// <returns>DataTable filled will registry entries</returns>
         static public DataTable registry_list(object name)
         {
             return registry_list(name, null);
         }
         static public DataTable registry_list(object name, object boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.registry_list(connectionString, name,  boardId);
                 case "Npgsql": return Postgre.Db.registry_list(connectionString, name,  boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.registry_list(connectionString, name,  boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.registry_list(connectionString, name,  boardId);
                 // case "oracle":  return orPostgre.Db.registry_list(connectionString, name,  boardId);
                 // case "db2":  return db2Postgre.Db.registry_list(connectionString, name,  boardId);
                 // case "other":  return othPostgre.Db.registry_list(connectionString, name,  boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         /// <summary>
         /// Saves a single registry entry pair to the database.
         /// </summary>
         /// <param name="Name">Unique name associated with this entry</param>
         /// <param name="Value">Value associated with this entry which can be null</param>
         static public void registry_save(object name, object value)
         {

             registry_save(name, value, DBNull.Value);

         }
         public static void registry_save(object name, object value, object boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.registry_save(connectionString, name, value, boardId); break;
                 case "Npgsql": Postgre.Db.registry_save(connectionString, name, value, boardId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.registry_save(connectionString, name, value, boardId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.registry_save(connectionString, name, value, boardId); break;
                 // case "oracle":   orPostgre.Db.registry_save(connectionString, name, value, boardId);break;
                 // case "db2":   db2Postgre.Db.registry_save(connectionString, name, value, boardId); break;
                 // case "other":   othPostgre.Db.registry_save(connectionString, name, value, boardId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void replace_words_delete(object id)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.replace_words_delete(connectionString, id); break;
                 case "Npgsql": Postgre.Db.replace_words_delete(connectionString, id); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.replace_words_delete(connectionString, id); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.replace_words_delete(connectionString, id); break;
                 // case "oracle":   orPostgre.Db.replace_words_delete(connectionString, id);break;
                 // case "db2":   db2Postgre.Db.replace_words_delete(connectionString, id); break;
                 // case "other":   othPostgre.Db.replace_words_delete(connectionString, id); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable replace_words_list(object boardId, object id)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.replace_words_list(connectionString, boardId, id);
                 case "Npgsql": return Postgre.Db.replace_words_list(connectionString, boardId, id);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.replace_words_list(connectionString, boardId, id);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.replace_words_list(connectionString, boardId, id);
                 // case "oracle":  return orPostgre.Db.replace_words_list(connectionString, boardId, id);
                 // case "db2":  return db2Postgre.Db.replace_words_list(connectionString, boardId, id);
                 // case "other":  return othPostgre.Db.replace_words_list(connectionString, boardId, id); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void replace_words_save(object boardId, object id, object badword, object goodword)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 case "Npgsql": Postgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 // case "oracle":   orPostgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword);break;
                 // case "db2":   db2Postgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 // case "other":   othPostgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable rss_topic_latest(object boardId, object numOfPostsToRetrieve, object pageUserId, bool useStyledNicks,
                                   bool showNoCountPosts)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 case "Npgsql": return Postgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 // case "oracle":  return orPostgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 // case "db2":  return db2Postgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                 // case "other":  return othPostgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable rsstopic_list(int forumID, int topicCount)
         {
             return rsstopic_list(forumID, 0, topicCount);
         }
         static public DataTable rsstopic_list(int forumID, int topicStart, int topicCount)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 case "Npgsql": return Postgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 // case "oracle":  return orPostgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 // case "db2":  return db2Postgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                 // case "other":  return othPostgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void SetPropertyValues(int boardId, string appname, int userId, SettingsPropertyValueCollection collection, bool dirtyOnly = true)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                 case "Npgsql": Postgre.Db.SetPropertyValues(connectionString, boardId, appname, userId, collection, dirtyOnly); break;
                 case "MySql.Data.MySqlClient": MySqlDb.Db.SetPropertyValues(connectionString, boardId, appname, userId, collection, dirtyOnly); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.SetPropertyValues(connectionString, boardId, appname, userId, collection, dirtyOnly); break;
                 // case "oracle":   orPostgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                 // case "db2":   db2Postgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                 // case "other":   othPostgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
       
         static public Boolean shoutbox_clearmessages(int boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.shoutbox_clearmessages(connectionString, boardId);
                 case "Npgsql": return Postgre.Db.shoutbox_clearmessages(connectionString, boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.shoutbox_clearmessages(connectionString, boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.shoutbox_clearmessages(connectionString, boardId);
                 // case "oracle":  return orPostgre.Db.shoutbox_clearmessages(connectionString, boardId);
                 // case "db2":  return db2Postgre.Db.shoutbox_clearmessages(connectionString, boardId);
                 // case "other":  return othPostgre.Db.shoutbox_clearmessages(connectionString, boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable shoutbox_getmessages(int boardId, int numberOfMessages, object useStyledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 case "Npgsql": return Postgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 // case "oracle":  return orPostgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 // case "db2":  return db2Postgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                 // case "other":  return othPostgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public Boolean shoutbox_savemessage(int boardId, string message, string userName, int userID, object ip)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 case "Npgsql": return Postgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 // case "oracle":  return orPostgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 // case "db2":  return db2Postgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                 // case "other":  return othPostgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void smiley_delete(object smileyID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.smiley_delete(connectionString, smileyID); break;
                 case "Npgsql": Postgre.Db.smiley_delete(connectionString, smileyID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.smiley_delete(connectionString, smileyID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.smiley_delete(connectionString, smileyID); break;
                 // case "oracle":   orPostgre.Db.smiley_delete(connectionString, smileyID);break;
                 // case "db2":   db2Postgre.Db.smiley_delete(connectionString, smileyID); break;
                 // case "other":   othPostgre.Db.smiley_delete(connectionString, smileyID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable smiley_list(object boardId, object smileyID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.smiley_list(connectionString, boardId, smileyID);
                 case "Npgsql": return Postgre.Db.smiley_list(connectionString, boardId, smileyID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.smiley_list(connectionString, boardId, smileyID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.smiley_list(connectionString, boardId, smileyID);
                 // case "oracle":  return orPostgre.Db.smiley_list(connectionString, boardId, smileyID);
                 // case "db2":  return db2Postgre.Db.smiley_list(connectionString, boardId, smileyID);
                 // case "other":  return othPostgre.Db.smiley_list(connectionString, boardId, smileyID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable smiley_listunique(object boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.smiley_listunique(connectionString, boardId);
                 case "Npgsql": return Postgre.Db.smiley_listunique(connectionString, boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.smiley_listunique(connectionString, boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.smiley_listunique(connectionString, boardId);
                 // case "oracle":  return orPostgre.Db.smiley_listunique(connectionString, boardId);
                 // case "db2":  return db2Postgre.Db.smiley_listunique(connectionString, boardId);
                 // case "other":  return othPostgre.Db.smiley_listunique(connectionString, boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void smiley_resort(object boardId, object smileyID, int move)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 case "Npgsql": Postgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 // case "oracle":   orPostgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move);break;
                 // case "db2":   db2Postgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 // case "other":   othPostgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void smiley_save(object smileyID, object boardId, object code, object icon, object emoticon, object sortOrder,
                         object replace)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 case "Npgsql": Postgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 // case "oracle":   orPostgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace);break;
                 // case "db2":   db2Postgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 // case "other":   othPostgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public IEnumerable<TypedSmileyList> SmileyList(int boardId, int? smileyID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.SmileyList(connectionString, boardId, smileyID);
                 case "Npgsql": return Postgre.Db.SmileyList(connectionString, boardId, smileyID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.SmileyList(connectionString, boardId, smileyID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.SmileyList(connectionString, boardId, smileyID);
                 // case "oracle":  return orPostgre.Db.SmileyList(connectionString, boardId, smileyID);
                 // case "db2":  return db2Postgre.Db.SmileyList(connectionString, boardId, smileyID);
                 // case "other":  return othPostgre.Db.SmileyList(connectionString, boardId, smileyID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void  system_deleteinstallobjects()
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.system_deleteinstallobjects(connectionString); break;
                 case "Npgsql": Postgre.Db.system_deleteinstallobjects(connectionString); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.system_deleteinstallobjects(connectionString); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.system_deleteinstallobjects(connectionString); break;
                 // case "oracle":   orPostgre.Db.system_deleteinstallobjects(connectionString);break;
                 // case "db2":   db2Postgre.Db.system_deleteinstallobjects(connectionString); break;
                 // case "other":   othPostgre.Db.system_deleteinstallobjects(connectionString); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void system_initialize(string forumName, string timeZone, string culture, string languageFile, string forumEmail,
                               string smtpServer, string userName, string userEmail, object providerUserKey,
                               string rolePrefix)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 case "Npgsql": Postgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 // case "oracle":   orPostgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix);break;
                 // case "db2":   db2Postgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 // case "other":   othPostgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void system_initialize_executescripts(string script, string scriptFile, bool useTransactions)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 case "Npgsql": Postgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 // case "oracle":   orPostgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions);break;
                 // case "db2":   db2Postgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 // case "other":   othPostgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void system_initialize_fixaccess(bool bGrant)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.system_initialize_fixaccess(connectionString, bGrant); break;
                 case "Npgsql": Postgre.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                 // case "oracle":   orPostgre.Db.system_initialize_fixaccess(connectionString, bGrant);break;
                 // case "db2":   db2Postgre.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                 // case "other":   othPostgre.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable system_list()
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.system_list(connectionString);
                 case "Npgsql": return Postgre.Db.system_list(connectionString);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.system_list(connectionString);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.system_list(connectionString);
                 // case "oracle":  return orPostgre.Db.system_list(connectionString);
                 // case "db2":  return db2Postgre.Db.system_list(connectionString);
                 // case "other":  return othPostgre.Db.system_list(connectionString); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void system_updateversion(int version, string name)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.system_updateversion(connectionString, version, name); break;
                 case "Npgsql": Postgre.Db.system_updateversion(connectionString, version, name); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.system_updateversion(connectionString, version, name); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.system_updateversion(connectionString, version, name); break;
                 // case "oracle":   orPostgre.Db.system_updateversion(connectionString, version, name);break;
                 // case "db2":   db2Postgre.Db.system_updateversion(connectionString, version, name); break;
                 // case "other":   othPostgre.Db.system_updateversion(connectionString, version, name); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static DataTable topic_active([NotNull] object boardId, [CanBeNull] object categoryId, [NotNull] object pageUserId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [CanBeNull]bool findLastRead)
        {
          
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 case "Npgsql": return Postgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 // case "other":  return othPostgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable bbcode_list(object boardId, object bbcodeID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.bbcode_list( connectionString,  boardId,  bbcodeID);
                 case "Npgsql": return Postgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                 // case "oracle":  return orPostgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                 // case "db2":  return db2Postgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                 // case "other":  return othPostgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_announcements(object boardId, object numOfPostsToRetrieve, object pageUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 case "Npgsql": return Postgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 // case "oracle":  return orPostgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 // case "db2":  return db2Postgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                 // case "other":  return othPostgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static DataTable topic_unanswered(
            [NotNull] object boardId, 
            [CanBeNull] object categoryId, 
            [NotNull] object pageUserId, 
            [NotNull] object sinceDate, 
            [NotNull] object toDate, 
            [NotNull] object pageIndex, 
            [NotNull] object pageSize, 
            [NotNull] object useStyledNicks, 
            [CanBeNull]bool findLastRead)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_unanswered(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "Npgsql": return Postgre.Db.topic_unanswered(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_unanswered(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "other":  return othPostgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static DataTable topic_unread(
             [NotNull] object boardId,
             [CanBeNull] object categoryId,
             [NotNull] object pageUserId,
             [NotNull] object sinceDate,
             [NotNull] object toDate,
             [NotNull] object pageIndex,
             [NotNull] object pageSize,
             [NotNull] object useStyledNicks,
             [CanBeNull]bool findLastRead)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_unread(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "Npgsql": return Postgre.Db.topic_unread(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_unread(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_unread(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "other":  return othPostgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static DataTable Topics_ByUser([NotNull] object boardId, [NotNull] object categoryId, [NotNull] object pageUserId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [CanBeNull]bool findLastRead)
         {

             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_unread(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "Npgsql": return Postgre.Db.Topics_ByUser(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.Topics_ByUser(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Topics_ByUser(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, findLastRead);
                 // case "oracle":  return orPostgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "db2":  return db2Postgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 // case "other":  return othPostgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void TopicStatus_Delete([NotNull] object topicStatusID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.TopicStatus_Delete(connectionString,  topicStatusID); break;
                 case "Npgsql": Postgre.Db.TopicStatus_Delete(connectionString, topicStatusID); break;
                 case "MySql.Data.MySqlClient": MySqlDb.Db.TopicStatus_Delete(connectionString, topicStatusID); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.TopicStatus_Delete(connectionString, topicStatusID); break;
                 // case "oracle":   orPostgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                 // case "db2":   db2Postgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                 // case "other":   othPostgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public DataTable TopicStatus_Edit([NotNull] object topicStatusID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.TopicStatus_Edit(connectionString,  topicStatusID);
                 case "Npgsql": return Postgre.Db.TopicStatus_Edit(connectionString, topicStatusID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.TopicStatus_Edit(connectionString, topicStatusID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.TopicStatus_Edit(connectionString, topicStatusID);
                 // case "oracle":  return orPostgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                 // case "db2":  return db2Postgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                 // case "other":  return othPostgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable TopicStatus_List([NotNull] object topicStatusID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.TopicStatus_List(connectionString, topicStatusID);
                 case "Npgsql": return Postgre.Db.TopicStatus_List(connectionString, topicStatusID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.TopicStatus_List(connectionString, topicStatusID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.TopicStatus_List(connectionString, topicStatusID);
                 // case "oracle":  return orPostgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                 // case "db2":  return db2Postgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                 // case "other":  return othPostgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void TopicStatus_Save([NotNull] object topicStatusID, [NotNull] object boardID, [NotNull] object topicStatusName, [NotNull] object defaultDescription)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 case "Npgsql": Postgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 case "MySql.Data.MySqlClient": MySqlDb.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 // case "oracle":   orPostgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 // case "db2":   db2Postgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 // case "other":   othPostgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public long topic_create_by_message(object messageID, object forumId, object newTopicSubj)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 case "Npgsql": return Postgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 // case "oracle":  return orPostgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 // case "db2":  return db2Postgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                 // case "other":  return othPostgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         static public void topic_delete(object topicID)
         {
             topic_delete(topicID, false);
         }
         public static void topic_delete(object topicID, object eraseTopic)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_delete(connectionString, topicID, eraseTopic); break;
                 case "Npgsql": Postgre.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                 // case "oracle":   orPostgre.Db.topic_delete(connectionString, topicID, eraseTopic);break;
                 // case "db2":   db2Postgre.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                 // case "other":   othPostgre.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void topic_favorite_add(object userID, object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_favorite_add(connectionString, userID, topicID); break;
                 case "Npgsql": Postgre.Db.topic_favorite_add(connectionString, userID, topicID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_favorite_add(connectionString, userID, topicID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_favorite_add(connectionString, userID, topicID); break;
                 // case "oracle":   orPostgre.Db.topic_favorite_add(connectionString, userID, topicID);break;
                 // case "db2":   db2Postgre.Db.topic_favorite_add(connectionString, userID, topicID); break;
                 // case "other":   othPostgre.Db.topic_favorite_add(connectionString, userID, topicID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

        public static DataTable topic_favorite_details( [NotNull] object boardId, [CanBeNull] object categoryId, [NotNull] object pageUserId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [CanBeNull]bool findLastRead)
        {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 case "Npgsql": return Postgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 // case "other":  return othPostgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_favorite_list(object userID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_favorite_list(connectionString, userID);
                 case "Npgsql": return Postgre.Db.topic_favorite_list(connectionString, userID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_favorite_list(connectionString, userID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_favorite_list(connectionString, userID);
                 // case "oracle":  return orPostgre.Db.topic_favorite_list(connectionString, userID);
                 // case "db2":  return db2Postgre.Db.topic_favorite_list(connectionString, userID);
                 // case "other":  return othPostgre.Db.topic_favorite_list(connectionString, userID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void topic_favorite_remove(object userID, object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_favorite_remove(connectionString, userID, topicID); break;
                 case "Npgsql": Postgre.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                 // case "oracle":   orPostgre.Db.topic_favorite_remove(connectionString, userID, topicID);break;
                 // case "db2":   db2Postgre.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                 // case "other":   othPostgre.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public int topic_findduplicate(object topicName)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_findduplicate(connectionString, topicName);
                 case "Npgsql": return Postgre.Db.topic_findduplicate(connectionString, topicName);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_findduplicate(connectionString, topicName);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_findduplicate(connectionString, topicName);
                 // case "oracle":  return orPostgre.Db.topic_findduplicate(connectionString, topicName);
                 // case "db2":  return db2Postgre.Db.topic_findduplicate(connectionString, topicName);
                 // case "other":  return othPostgre.Db.topic_findduplicate(connectionString, topicName); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_findnext(object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_findnext(connectionString, topicID);
                 case "Npgsql": return Postgre.Db.topic_findnext(connectionString, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_findnext(connectionString, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_findnext(connectionString, topicID);
                 // case "oracle":  return orPostgre.Db.topic_findnext(connectionString, topicID);
                 // case "db2":  return db2Postgre.Db.topic_findnext(connectionString, topicID);
                 // case "other":  return othPostgre.Db.topic_findnext(connectionString, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_findprev(object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_findprev(connectionString, topicID);
                 case "Npgsql": return Postgre.Db.topic_findprev(connectionString, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_findprev(connectionString, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_findprev(connectionString, topicID);
                 // case "oracle":  return orPostgre.Db.topic_findprev(connectionString, topicID);
                 // case "db2":  return db2Postgre.Db.topic_findprev(connectionString, topicID);
                 // case "other":  return othPostgre.Db.topic_findprev(connectionString, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataRow topic_info(object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_info(connectionString, topicID);
                 case "Npgsql": return Postgre.Db.topic_info(connectionString, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_info(connectionString, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_info(connectionString, topicID);
                 // case "oracle":  return orPostgre.Db.topic_info(connectionString, topicID);
                 // case "db2":  return db2Postgre.Db.topic_info(connectionString, topicID);
                 // case "other":  return othPostgre.Db.topic_info(connectionString, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_latest(object boardID, object numOfPostsToRetrieve, object pageUserId, bool useStyledNicks,
                               bool showNoCountPosts, [CanBeNull]bool findLastRead)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 case "Npgsql": return Postgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                 // case "other":  return othPostgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_list([NotNull] object forumID, [NotNull] object userId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [NotNull] object showMoved, [CanBeNull]bool findLastRead)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead);
                 case "Npgsql": return Postgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 // case "oracle":  return orPostgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead);
                 // case "db2":  return db2Postgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead);
                 // case "other":  return othPostgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static DataTable announcements_list([NotNull] object forumID, [NotNull] object userId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [NotNull] object showMoved, [CanBeNull]bool findLastRead)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 case "Npgsql": return Postgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize, useStyledNicks, showMoved, findLastRead);
                 // case "oracle":  return orPostgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead);
                 // case "db2":  return db2Postgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead);
                 // case "other":  return othPostgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void topic_lock(object topicID, object locked)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_lock(connectionString, topicID, locked); break;
                 case "Npgsql": Postgre.Db.topic_lock(connectionString, topicID, locked); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_lock(connectionString, topicID, locked); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_lock(connectionString, topicID, locked); break;
                 // case "oracle":   orPostgre.Db.topic_lock(connectionString, topicID, locked);break;
                 // case "db2":   db2Postgre.Db.topic_lock(connectionString, topicID, locked); break;
                 // case "other":   othPostgre.Db.topic_lock(connectionString, topicID, locked); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void topic_move(object topicID, object forumID, object showMoved, object linkDays)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 case "Npgsql": Postgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 // case "oracle":   orPostgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays);break;
                 // case "db2":   db2Postgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 // case "other":   othPostgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int topic_prune([NotNull] object boardID, [NotNull] object forumID, [NotNull] object days,
                       [NotNull] object permDelete)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 case "Npgsql": return Postgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 // case "oracle":  return orPostgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 // case "db2":  return db2Postgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                 // case "other":  return othPostgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public long topic_save(object forumID, object subject, object status, object styles, object description, object message, object userId,
                        object priority, object userName, object ip, object posted, object blogPostID, object flags,
                        ref long messageID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_save(connectionString, forumID, subject, status, styles, description, message, userId, priority, userName, ip, posted, blogPostID, flags, ref messageID);
                 case "Npgsql": return Postgre.Db.topic_save(connectionString, forumID, subject, status, styles, description, message, userId, priority, userName, ip, posted, blogPostID, flags, ref messageID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_save(connectionString, forumID, subject, status, styles, description, message, userId, priority, userName, ip, posted, blogPostID, flags, ref messageID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_save(connectionString, forumID, subject, status,styles, description, message, userId, priority, userName, ip, posted, blogPostID, flags, ref messageID);
                 // case "oracle":  return orPostgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID);
                 // case "db2":  return db2Postgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID);
                 // case "other":  return othPostgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable topic_simplelist(int StartID, int Limit)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.topic_simplelist(connectionString, StartID, Limit);
                 case "Npgsql": return Postgre.Db.topic_simplelist(connectionString, StartID, Limit);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.topic_simplelist(connectionString, StartID, Limit);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.topic_simplelist(connectionString, StartID, Limit);
                 // case "oracle":  return orPostgre.Db.topic_simplelist(connectionString, StartID, Limit);
                 // case "db2":  return db2Postgre.Db.topic_simplelist(connectionString, StartID, Limit);
                 // case "other":  return othPostgre.Db.topic_simplelist(connectionString, StartID, Limit); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void topic_updatetopic(int topicId, string topic)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.topic_updatetopic(connectionString, topicId, topic); break;
                 case "Npgsql": Postgre.Db.topic_updatetopic(connectionString, topicId, topic); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.topic_updatetopic(connectionString, topicId, topic); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.topic_updatetopic(connectionString, topicId, topic); break;
                 // case "oracle":   orPostgre.Db.topic_updatetopic(connectionString, topicId, topic);break;
                 // case "db2":   db2Postgre.Db.topic_updatetopic(connectionString, topicId, topic); break;
                 // case "other":   othPostgre.Db.topic_updatetopic(connectionString, topicId, topic); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int  TopicFavoriteCount(int topicId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.TopicFavoriteCount(connectionString, topicId);
                 case "Npgsql": return Postgre.Db.TopicFavoriteCount(connectionString, topicId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.TopicFavoriteCount(connectionString, topicId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.TopicFavoriteCount(connectionString, topicId);
                 // case "oracle":  return orPostgre.Db.TopicFavoriteCount(connectionString, topicId);
                 // case "db2":  return db2Postgre.Db.TopicFavoriteCount(connectionString, topicId);
                 // case "other":  return othPostgre.Db.TopicFavoriteCount(connectionString, topicId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void unencode_all_topics_subjects(Func<string, string> decodeTopicFunc)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 case "Npgsql": Postgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 // case "oracle":   orPostgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);break;
                 // case "db2":   db2Postgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 // case "other":   othPostgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable user_accessmasks(object boardId, object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_accessmasks(connectionString, boardId, userId);
                 case "Npgsql": return Postgre.Db.user_accessmasks(connectionString, boardId, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_accessmasks(connectionString, boardId, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_accessmasks(connectionString, boardId, userId);
                 // case "oracle":  return orPostgre.Db.user_accessmasks(connectionString, boardId, userId);
                 // case "db2":  return db2Postgre.Db.user_accessmasks(connectionString, boardId, userId);
                 // case "other":  return othPostgre.Db.user_accessmasks(connectionString, boardId, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_activity_rank(object boardId, object startDate, object displayNumber)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 case "Npgsql": return Postgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 // case "oracle":  return orPostgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 // case "db2":  return db2Postgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                 // case "other":  return othPostgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void user_addignoreduser(object userId, object ignoredUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 case "Npgsql": Postgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 // case "oracle":   orPostgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId);break;
                 // case "db2":   db2Postgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 // case "other":   othPostgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_addpoints(object userId, object forumUserId, object points)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_addpoints(connectionString, userId, forumUserId, points); break;
                 case "Npgsql": Postgre.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                 // case "oracle":   orPostgre.Db.user_addpoints(connectionString, userId, forumUserId, points);break;
                 // case "db2":   db2Postgre.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                 // case "other":   othPostgre.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_adminsave
            (object boardId, object userId, object name, object displayName, object email, object flags, object rankID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 case "Npgsql": Postgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 // case "oracle":   orPostgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID);break;
                 // case "db2":   db2Postgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 // case "other":   othPostgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_approve(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_approve(connectionString, userId); break;
                 case "Npgsql": Postgre.Db.user_approve(connectionString, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_approve(connectionString, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_approve(connectionString, userId); break;
                 // case "oracle":   orPostgre.Db.user_approve(connectionString, userId);break;
                 // case "db2":   db2Postgre.Db.user_approve(connectionString, userId); break;
                 // case "other":   othPostgre.Db.user_approve(connectionString, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void user_approveall(object boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_approveall(connectionString, boardId); break;
                 case "Npgsql": Postgre.Db.user_approveall(connectionString, boardId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_approveall(connectionString, boardId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_approveall(connectionString, boardId); break;
                 // case "oracle":   orPostgre.Db.user_approveall(connectionString, boardId);break;
                 // case "db2":   db2Postgre.Db.user_approveall(connectionString, boardId); break;
                 // case "other":   othPostgre.Db.user_approveall(connectionString, boardId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int user_aspnet(int boardId, string userName, string displayName, string email, object providerUserKey,
                        object isApproved)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 case "Npgsql": return Postgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 // case "oracle":  return orPostgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 // case "db2":  return db2Postgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                 // case "other":  return othPostgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_avatarimage(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_avatarimage(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_avatarimage(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_avatarimage(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_avatarimage(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_avatarimage(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_avatarimage(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_avatarimage(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public bool user_changepassword(object userId, object oldPassword, object newPassword)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 case "Npgsql": return Postgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 // case "oracle":  return orPostgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 // case "db2":  return db2Postgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                 // case "other":  return othPostgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void user_delete(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_delete(connectionString, userId); break;
                 case "Npgsql": Postgre.Db.user_delete(connectionString, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_delete(connectionString, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_delete(connectionString, userId); break;
                 // case "oracle":   orPostgre.Db.user_delete(connectionString, userId);break;
                 // case "db2":   db2Postgre.Db.user_delete(connectionString, userId); break;
                 // case "other":   othPostgre.Db.user_delete(connectionString, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_deleteavatar(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_deleteavatar(connectionString, userId); break;
                 case "Npgsql": Postgre.Db.user_deleteavatar(connectionString, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_deleteavatar(connectionString, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_deleteavatar(connectionString, userId); break;
                 // case "oracle":   orPostgre.Db.user_deleteavatar(connectionString, userId);break;
                 // case "db2":   db2Postgre.Db.user_deleteavatar(connectionString, userId); break;
                 // case "other":   othPostgre.Db.user_deleteavatar(connectionString, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_deleteold(object boardId, object days)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_deleteold(connectionString, boardId, days); break;
                 case "Npgsql": Postgre.Db.user_deleteold(connectionString, boardId, days); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_deleteold(connectionString, boardId, days); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_deleteold(connectionString, boardId, days); break;
                 // case "oracle":   orPostgre.Db.user_deleteold(connectionString, boardId, days);break;
                 // case "db2":   db2Postgre.Db.user_deleteold(connectionString, boardId, days); break;
                 // case "other":   othPostgre.Db.user_deleteold(connectionString, boardId, days); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable user_emails(object boardId, object groupID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_emails(connectionString, boardId, groupID);
                 case "Npgsql": return Postgre.Db.user_emails(connectionString, boardId, groupID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_emails(connectionString, boardId, groupID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_emails(connectionString, boardId, groupID);
                 // case "oracle":  return orPostgre.Db.user_emails(connectionString, boardId, groupID);
                 // case "db2":  return db2Postgre.Db.user_emails(connectionString, boardId, groupID);
                 // case "other":  return othPostgre.Db.user_emails(connectionString, boardId, groupID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         static public int user_get(int boardId, object providerUserKey)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_get(connectionString, boardId, providerUserKey);
                 case "Npgsql": return Postgre.Db.user_get(connectionString, boardId, providerUserKey);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_get(connectionString, boardId, providerUserKey);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_get(connectionString, boardId, providerUserKey);
                 // case "oracle":  return orPostgre.Db.user_get(connectionString, boardId, providerUserKey);
                 // case "db2":  return db2Postgre.Db.user_get(connectionString, boardId, providerUserKey);
                 // case "other":  return othPostgre.Db.user_get(connectionString, boardId, providerUserKey); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_getalbumsdata(object userID, object boardID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getalbumsdata(connectionString, userID, boardID);
                 case "Npgsql": return Postgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getalbumsdata(connectionString, userID, boardID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getalbumsdata(connectionString, userID, boardID);
                 // case "oracle":  return orPostgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                 // case "db2":  return db2Postgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                 // case "other":  return othPostgre.Db.user_getalbumsdata(connectionString, userID, boardID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public int user_getpoints(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getpoints(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_getpoints(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getpoints(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getpoints(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_getpoints(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_getpoints(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_getpoints(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public string user_getsignature(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getsignature(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_getsignature(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getsignature(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getsignature(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_getsignature(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_user_getsignature(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_user_getsignature(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_getsignaturedata(object userID, object boardID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getsignaturedata(connectionString, userID, boardID);
                 case "Npgsql": return Postgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getsignaturedata(connectionString, userID, boardID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getsignaturedata(connectionString, userID, boardID);
                 // case "oracle":  return orPostgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                 // case "db2":  return db2Postgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                 // case "other":  return othPostgre.Db.user_getsignaturedata(connectionString, userID, boardID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public int user_getthanks_from(object userID, object pageUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getthanks_from(connectionString, userID, pageUserId);
                 case "Npgsql": return Postgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getthanks_from(connectionString, userID, pageUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getthanks_from(connectionString, userID, pageUserId);
                 // case "oracle":  return orPostgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                 // case "db2":  return db2Postgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                 // case "other":  return othPostgre.Db.user_getthanks_from(connectionString, userID, pageUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int[] user_getthanks_to(object userID, object pageUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_getthanks_to(connectionString, userID, pageUserId);
                 case "Npgsql": return Postgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_getthanks_to(connectionString, userID, pageUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_getthanks_to(connectionString, userID, pageUserId);
                 // case "oracle":  return orPostgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                 // case "db2":  return db2Postgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                 // case "other":  return othPostgre.Db.user_getthanks_to(connectionString, userID, pageUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int? user_guest(object boardId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_guest(connectionString, boardId);
                 case "Npgsql": return Postgre.Db.user_guest(connectionString, boardId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_guest(connectionString, boardId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_guest(connectionString, boardId);
                 // case "oracle":  return orPostgre.Db.user_guest(connectionString, boardId);
                 // case "db2":  return db2Postgre.Db.user_guest(connectionString, boardId);
                 // case "other":  return othPostgre.Db.user_guest(connectionString, boardId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable user_ignoredlist(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_ignoredlist(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_ignoredlist(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_ignoredlist(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_ignoredlist(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_ignoredlist(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_ignoredlist(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_ignoredlist(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public bool user_isuserignored(object userId, object ignoredUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_isuserignored(connectionString, userId, ignoredUserId);
                 case "Npgsql": return Postgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                 // case "oracle":  return orPostgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                 // case "db2":  return db2Postgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                 // case "other":  return othPostgre.Db.user_isuserignored(connectionString, userId, ignoredUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataRow user_lazydata(object userID, object boardID, bool showPendingMails, bool showPendingBuddies,
                              bool showUnreadPMs, bool showUserAlbums, bool styledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 case "Npgsql": return Postgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 // case "oracle":  return orPostgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 // case "db2":  return db2Postgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                 // case "other":  return othPostgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         /// <summary>
         /// The user_list.
         /// </summary>
         /// <param name="boardID">
         /// The board id.
         /// </param>
         /// <param name="userID">
         /// The user id.
         /// </param>
         /// <param name="approved">
         /// The approved.
         /// </param>    
         /// <returns>
         /// </returns>
         public static DataTable user_list(object boardID, object userID, object approved)
         {
             return user_list(boardID, userID, approved, null, null, false);
         }
         /// <summary>
         /// The user_list.
         /// </summary>
         /// <param name="boardID">
         /// The board id.
         /// </param>
         /// <param name="userID">
         /// The user id.
         /// </param>
         /// <param name="approved">
         /// The approved.
         /// </param>
         /// <param name="useStyledNicks">
         /// Return style info.
         /// </param> 
         /// <returns>
         /// </returns>
         public static DataTable user_list( object boardID, object userID, object approved, object useStyledNicks)
         {
             return user_list(boardID, userID, approved, null, null, useStyledNicks);
         }

         /// <summary>
         /// The user_list.
         /// </summary>
         /// <param name="boardID">
         /// The board id.
         /// </param>
         /// <param name="userID">
         /// The user id.
         /// </param>
         /// <param name="approved">
         /// The approved.
         /// </param>
         /// <param name="groupID">
         /// The group id.
         /// </param>
         /// <param name="rankID">
         /// The rank id.
         /// </param>
         /// <returns>
         /// </returns>
         public static DataTable user_list(object boardID, object userID, object approved, object groupID, object rankID)
         {
             return user_list(boardID, userID, approved, null, null, false);
         }
         static public DataTable user_list(object boardId, object userId, object approved, object groupID, object rankID, object useStyledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 case "Npgsql": return Postgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 // case "oracle":  return orPostgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 // case "db2":  return db2Postgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                 // case "other":  return othPostgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_listmedals(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_listmedals(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_listmedals(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_listmedals(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_listmedals(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_listmedals(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_listmedals(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_listmedals(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_listmembers(
            object boardId,
            object userId,
            object approved,
            object groupId,
            object rankId,
            object useStyledNicks,
            object lastUserId,
            object literals,
            object exclude,
            object beginsWith,
            object pageIndex,
            object pageSize,
            object sortName,
            object sortRank,
            object sortJoined,
            object sortPosts,
            object sortLastVisit,
            object numPosts,
            object numPostCompare)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 case "Npgsql": return Postgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 // case "oracle":  return orPostgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 // case "db2":  return db2Postgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                 // case "other":  return othPostgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
 
         public static void user_medal_delete(object userId, object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_medal_delete(connectionString, userId, medalID); break;
                 case "Npgsql": Postgre.Db.user_medal_delete(connectionString, userId, medalID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_medal_delete(connectionString, userId, medalID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_medal_delete(connectionString, userId, medalID); break;
                 // case "oracle":   orPostgre.Db.user_medal_delete(connectionString, userId, medalID);break;
                 // case "db2":   db2Postgre.Db.user_medal_delete(connectionString, userId, medalID); break;
                 // case "other":   othPostgre.Db.user_medal_delete(connectionString, userId, medalID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable user_medal_list(object userId, object medalID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_medal_list(connectionString, userId, medalID);
                 case "Npgsql": return Postgre.Db.user_medal_list(connectionString, userId, medalID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_medal_list(connectionString, userId, medalID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_medal_list(connectionString, userId, medalID);
                 // case "oracle":  return orPostgre.Db.user_medal_list(connectionString, userId, medalID);
                 // case "db2":  return db2Postgre.Db.user_medal_list(connectionString, userId, medalID);
                 // case "other":  return othPostgre.Db.user_medal_list(connectionString, userId, medalID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void user_medal_save(
            object userId, object medalID, object message,
            object hide, object onlyRibbon, object sortOrder, object dateAwarded)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 case "Npgsql": Postgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 // case "oracle":   orPostgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded);break;
                 // case "db2":   db2Postgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 // case "other":   othPostgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_migrate(object userId, object providerUserKey, object updateProvider)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 case "Npgsql": Postgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 // case "oracle":   orPostgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);break;
                 // case "db2":   db2Postgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 // case "other":   othPostgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public int user_nntp(object boardId, object userName, object email, int? timeZone)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 case "Npgsql": return Postgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 // case "oracle":  return orPostgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 // case "db2":  return db2Postgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                 // case "other":  return othPostgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable user_pmcount(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_pmcount(connectionString, userId);
                 case "Npgsql": return Postgre.Db.user_pmcount(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_pmcount(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_pmcount(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.user_pmcount(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.user_pmcount(connectionString, userId);
                 // case "other":  return othPostgre.Db.user_pmcount(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public object user_recoverpassword(object boardId, object userName, object email)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_recoverpassword(connectionString, boardId, userName, email);
                 case "Npgsql": return Postgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_recoverpassword(connectionString, boardId, userName, email);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_recoverpassword(connectionString, boardId, userName, email);
                 // case "oracle":  return orPostgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                 // case "db2":  return db2Postgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                 // case "other":  return othPostgre.Db.user_recoverpassword(connectionString, boardId, userName, email); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public bool user_register(object boardId, object userName, object password, object hash, object email, object location,
                           object homePage, object timeZone, bool approved)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                 case "Npgsql": return Postgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                 case "MySql.Data.MySqlClient": return true;
                 case "FirebirdSql.Data.FirebirdClient": return true;
                 // case "oracle":  return orPostgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                 // case "db2":  return db2Postgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                 // case "other":  return othPostgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void user_removeignoreduser(object userId, object ignoredUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 case "Npgsql": Postgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 // case "oracle":   orPostgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);break;
                 // case "db2":   db2Postgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 // case "other":   othPostgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void user_removepoints(object userId, [CanBeNull] object fromUserID, object points)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_removepoints(connectionString, userId, fromUserID, points); break;
                 case "Npgsql": Postgre.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                 // case "oracle":   orPostgre.Db.user_removepoints(connectionString, userId, fromUserID, points);break;
                 // case "db2":   db2Postgre.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                 // case "other":   othPostgre.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_removepointsByTopicID(object topicID, object points)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_removepointsByTopicID(connectionString, topicID, points); break;
                 case "Npgsql": Postgre.Db.user_removepointsByTopicID(connectionString, topicID, points); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_removepointsByTopicID(connectionString, topicID, points); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_removepointsByTopicID(connectionString, topicID, points); break;
                 // case "oracle":   orPostgre.Db.user_removepointsByTopicID(connectionString, topicID, points);break;
                 // case "db2":   db2Postgre.Db.user_removepointsByTopicID(connectionString, topicID, points); break;
                 // case "other":   othPostgre.Db.user_removepointsByTopicID(connectionString, topicID, points); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public bool  user_RepliedTopic([NotNull] object messageId, [NotNull] object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_RepliedTopic(connectionString, messageId, userId);
                 case "Npgsql": return Postgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_RepliedTopic(connectionString, messageId, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_RepliedTopic(connectionString, messageId, userId);
                 // case "oracle":  return orPostgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                 // case "db2":  return db2Postgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                 // case "other":  return othPostgre.Db.user_RepliedTopic(connectionString, messageId, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void user_save(
            object userId,
            object boardId,
            object userName,
            object displayName,
            object email,
            object timeZone,
            object languageFile,
            object culture,
            object themeFile,
            object useSingleSignOn,
            object textEditor,
            object overrideDefaultThemes,
            object approved,
            object pmNotification,
            object autoWatchTopics,
            object dSTUser,
            object isHidden,
            object notificationType)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 case "Npgsql": Postgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 // case "oracle":   orPostgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType);break;
                 // case "db2":   db2Postgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 // case "other":   othPostgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_saveavatar(object userId, object avatar, System.IO.Stream stream, object avatarImageType)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 case "Npgsql": Postgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 // case "oracle":   orPostgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);break;
                 // case "db2":   db2Postgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 // case "other":   othPostgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_savenotification(
            object userId,
            object pmNotification,
            object autoWatchTopics,
            object notificationType,
            object dailyDigest)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 case "Npgsql": Postgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 // case "oracle":   orPostgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);break;
                 // case "db2":   db2Postgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 // case "other":   othPostgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_savepassword(object userId, object password)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_savepassword(connectionString, userId, password); break;
                 case "Npgsql": Postgre.Db.user_savepassword(connectionString, userId, password); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_savepassword(connectionString, userId, password); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_savepassword(connectionString, userId, password); break;
                 // case "oracle":   orPostgre.Db.user_savepassword(connectionString, userId, password);break;
                 // case "db2":   db2Postgre.Db.user_savepassword(connectionString, userId, password); break;
                 // case "other":   othPostgre.Db.user_savepassword(connectionString, userId, password); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_savesignature(object userId, object signature)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_savesignature(connectionString, userId, signature); break;
                 case "Npgsql": Postgre.Db.user_savesignature(connectionString, userId, signature); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_savesignature(connectionString, userId, signature); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_savesignature(connectionString, userId, signature); break;
                 // case "oracle":   orPostgre.Db.user_savesignature(connectionString, userId, signature);break;
                 // case "db2":   db2Postgre.Db.user_savesignature(connectionString, userId, signature); break;
                 // case "other":   othPostgre.Db.user_savesignature(connectionString, userId, signature); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_setinfo(int boardId, System.Web.Security.MembershipUser user)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_setinfo(connectionString, boardId, user); break;
                 case "Npgsql": Postgre.Db.user_setinfo(connectionString, boardId, user); break;
                 case "MySql.Data.MySqlClient":  break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_setinfo(connectionString, boardId, user); break;
                 // case "oracle":   orPostgre.Db.user_setinfo(connectionString, boardId, user);break;
                 // case "db2":   db2Postgre.Db.user_setinfo(connectionString, boardId, user); break;
                 // case "other":   othPostgre.Db.user_setinfo(connectionString, boardId, user); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_setnotdirty(int boardId, int userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_setnotdirty(connectionString, boardId, userId); break;
                 case "Npgsql": Postgre.Db.user_setnotdirty(connectionString, boardId, userId); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_setnotdirty(connectionString, boardId, userId); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_setnotdirty(connectionString, boardId, userId); break;
                 // case "oracle":   orPostgre.Db.user_setnotdirty(connectionString, boardId, userId);break;
                 // case "db2":   db2Postgre.Db.user_setnotdirty(connectionString, boardId, userId); break;
                 // case "other":   othPostgre.Db.user_setnotdirty(connectionString, boardId, userId); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_setpoints(object userId, object points)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_setpoints(connectionString, userId, points); break;
                 case "Npgsql": Postgre.Db.user_setpoints(connectionString, userId, points); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_setpoints(connectionString, userId, points); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_setpoints(connectionString, userId, points); break;
                 // case "oracle":   orPostgre.Db.user_setpoints(connectionString, userId, points);break;
                 // case "db2":   db2Postgre.Db.user_setpoints(connectionString, userId, points); break;
                 // case "other":   othPostgre.Db.user_setpoints(connectionString, userId, points); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         public static void user_setrole(int boardId, object providerUserKey, object role)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 case "Npgsql": Postgre.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 // case "oracle":   orPostgre.Db.user_setrole(connectionString, boardId, providerUserKey, role);break;
                 // case "db2":   db2Postgre.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 // case "other":   othPostgre.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public DataTable user_simplelist(int StartID, int Limit)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_simplelist(connectionString, StartID, Limit);
                 case "Npgsql": return Postgre.Db.user_simplelist(connectionString, StartID, Limit);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_simplelist(connectionString, StartID, Limit);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_simplelist(connectionString, StartID, Limit);
                 // case "oracle":  return orPostgre.Db.user_simplelist(connectionString, StartID, Limit);
                 // case "db2":  return db2Postgre.Db.user_simplelist(connectionString, StartID, Limit);
                 // case "other":  return othPostgre.Db.user_simplelist(connectionString, StartID, Limit); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void user_suspend(object userId, object suspend)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_suspend(connectionString, userId, suspend); break;
                 case "Npgsql": Postgre.Db.user_suspend(connectionString, userId, suspend); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.user_suspend(connectionString, userId, suspend); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.user_suspend(connectionString, userId, suspend); break;
                 // case "oracle":   orPostgre.Db.user_suspend(connectionString, userId, suspend);break;
                 // case "db2":   db2Postgre.Db.user_suspend(connectionString, userId, suspend); break;
                 // case "other":   othPostgre.Db.user_suspend(connectionString, userId, suspend); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         public static void user_update_single_sign_on_status([NotNull] object userID, [NotNull] object isFacebookUser, [NotNull] object isTwitterUser)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);

             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 case "Npgsql": Postgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 case "MySql.Data.MySqlClient": MySqlDb.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 // case "oracle":   orPostgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 // case "db2":   db2Postgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                 // case "other":   othPostgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser);break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }

         static public bool user_ThankedMessage(object messageId, object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_ThankedMessage(connectionString, messageId, userId);
                 case "Npgsql": return Postgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_ThankedMessage(connectionString, messageId, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_ThankedMessage(connectionString, messageId, userId);
                 // case "oracle":  return orPostgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                 // case "db2":  return db2Postgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                 // case "other":  return othPostgre.Db.user_ThankedMessage(connectionString, messageId, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public int user_ThankFromCount([NotNull] object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_ThankFromCount(connectionString,  userId);
                 case "Npgsql": return Postgre.Db.user_ThankFromCount(connectionString,  userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_ThankFromCount(connectionString,  userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_ThankFromCount(connectionString,  userId);
                 // case "oracle":  return orPostgre.Db.user_ThankFromCount(connectionString,  userId);
                 // case "db2":  return db2Postgre.Db.user_ThankFromCount(connectionString,  userId);
                 // case "other":  return othPostgre.Db.user_ThankFromCount(connectionString,  userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public DataTable user_viewallthanks(object UserID, object pageUserId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.user_viewallthanks(connectionString, UserID, pageUserId);
                 case "Npgsql": return Postgre.Db.user_viewallthanks(connectionString, UserID, pageUserId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.user_viewallthanks(connectionString, UserID, pageUserId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.user_viewallthanks(connectionString, UserID, pageUserId);
                 // case "oracle":  return orPostgre.Db.user_viewallthanks(connectionString, UserID, pageUserId);
                 // case "db2":  return db2Postgre.Db.user_viewallthanks(connectionString, UserID, pageUserId);
                 // case "other":  return othPostgre.Db.user_viewallthanks(connectionString, UserID, pageUserId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         static public IEnumerable<TypedUserFind> UserFind(int boardId, bool filter, string userName, string email, string displayName,
                                            object notificationType, object dailyDigest)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                 case "Npgsql":   DataTable dt = Postgre.Db.UserFind(connectionString, boardId, filter, userName, email, displayName, notificationType, dailyDigest);
                     return dt.AsEnumerable().Select(u => new TypedUserFind(u));
                 case "MySql.Data.MySqlClient":
                     return MySqlDb.Db.UserFind(connectionString, boardId, filter, userName, email,
                                                          displayName, notificationType, dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                 case "FirebirdSql.Data.FirebirdClient":
                     return FirebirdDb.Db.UserFind(connectionString, boardId, filter, userName, email, displayName, notificationType, dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                 // case "oracle":  return orPostgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                 // case "db2":  return db2Postgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                 // case "other":  return othPostgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void userforum_delete(object userId, object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.userforum_delete(connectionString, userId, forumID); break;
                 case "Npgsql": Postgre.Db.userforum_delete(connectionString, userId, forumID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.userforum_delete(connectionString, userId, forumID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.userforum_delete(connectionString, userId, forumID); break;
                 // case "oracle":   orPostgre.Db.userforum_delete(connectionString, userId, forumID);break;
                 // case "db2":   db2Postgre.Db.userforum_delete(connectionString, userId, forumID); break;
                 // case "other":   othPostgre.Db.userforum_delete(connectionString, userId, forumID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable userforum_list(object userId, object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.userforum_list(connectionString, userId, forumID);
                 case "Npgsql": return Postgre.Db.userforum_list(connectionString, userId, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.userforum_list(connectionString, userId, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.userforum_list(connectionString, userId, forumID);
                 // case "oracle":  return orPostgre.Db.userforum_list(connectionString, userId, forumID);
                 // case "db2":  return db2Postgre.Db.userforum_list(connectionString, userId, forumID);
                 // case "other":  return othPostgre.Db.userforum_list(connectionString, userId, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void userforum_save(object userId, object forumID, object accessMaskID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 case "Npgsql": Postgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 // case "oracle":   orPostgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID);break;
                 // case "db2":   db2Postgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 // case "other":   othPostgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable usergroup_list(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.usergroup_list(connectionString, userId);
                 case "Npgsql": return Postgre.Db.usergroup_list(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.usergroup_list(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.usergroup_list(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.usergroup_list(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.usergroup_list(connectionString, userId);
                 // case "other":  return othPostgre.Db.usergroup_list(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void usergroup_save(object userId, object groupID, object member)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.usergroup_save(connectionString, userId,  groupID, member); break;
                 case "Npgsql": Postgre.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                 // case "oracle":   orPostgre.Db.usergroup_save(connectionString, userId,  groupID, member);break;
                 // case "db2":   db2Postgre.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                 // case "other":   othPostgre.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public IEnumerable<TypedUserList> UserList(int boardId, int? userId, bool? approved, int? groupID, int? rankID,
                                            bool? useStyledNicks)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 case "Npgsql": return Postgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 // case "oracle":  return orPostgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 // case "db2":  return db2Postgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                 // case "other":  return othPostgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void watchforum_add(object userId, object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.watchforum_add(connectionString, userId, forumID); break;
                 case "Npgsql": Postgre.Db.watchforum_add(connectionString, userId, forumID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.watchforum_add(connectionString, userId, forumID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.watchforum_add(connectionString, userId, forumID); break;
                 // case "oracle":   orPostgre.Db.watchforum_add(connectionString, userId, forumID);break;
                 // case "db2":   db2Postgre.Db.watchforum_add(connectionString, userId, forumID); break;
                 // case "other":   othPostgre.Db.watchforum_add(connectionString, userId, forumID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable watchforum_check(object userId, object forumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.watchforum_check(connectionString, userId, forumID);
                 case "Npgsql": return Postgre.Db.watchforum_check(connectionString, userId, forumID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.watchforum_check(connectionString, userId, forumID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.watchforum_check(connectionString, userId, forumID);
                 // case "oracle":  return orPostgre.Db.watchforum_check(connectionString, userId, forumID);
                 // case "db2":  return db2Postgre.Db.watchforum_check(connectionString, userId, forumID);
                 // case "other":  return othPostgre.Db.watchforum_check(connectionString, userId, forumID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }

         public static void watchforum_delete(object watchForumID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.watchforum_delete(connectionString, watchForumID); break;
                 case "Npgsql": Postgre.Db.watchforum_delete(connectionString, watchForumID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.watchforum_delete(connectionString, watchForumID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.watchforum_delete(connectionString, watchForumID); break;
                 // case "oracle":   orPostgre.Db.watchforum_delete(connectionString, watchForumID);break;
                 // case "db2":   db2Postgre.Db.watchforum_delete(connectionString, watchForumID); break;
                 // case "other":   othPostgre.Db.watchforum_delete(connectionString, watchForumID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable watchforum_list(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.watchforum_list(connectionString, userId);
                 case "Npgsql": return Postgre.Db.watchforum_list(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.watchforum_list(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.watchforum_list(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.watchforum_list(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.watchforum_list(connectionString, userId);
                 // case "other":  return othPostgre.Db.watchforum_list(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void watchtopic_add(object userId, object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.watchtopic_add(connectionString, userId, topicID); break;
                 case "Npgsql": Postgre.Db.watchtopic_add(connectionString, userId, topicID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.watchtopic_add(connectionString, userId, topicID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.watchtopic_add(connectionString, userId, topicID); break;
                 // case "oracle":   orPostgre.Db.watchtopic_add(connectionString, userId, topicID);break;
                 // case "db2":   db2Postgre.Db.watchtopic_add(connectionString, userId, topicID); break;
                 // case "other":   othPostgre.Db.watchtopic_add(connectionString, userId, topicID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable watchtopic_check(object userId, object topicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.watchtopic_check(connectionString, userId, topicID);
                 case "Npgsql": return Postgre.Db.watchtopic_check(connectionString, userId, topicID);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.watchtopic_check(connectionString, userId, topicID);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.watchtopic_check(connectionString, userId, topicID);
                 // case "oracle":  return orPostgre.Db.watchtopic_check(connectionString, userId, topicID);
                 // case "db2":  return db2Postgre.Db.watchtopic_check(connectionString, userId, topicID);
                 // case "other":  return othPostgre.Db.watchtopic_check(connectionString, userId, topicID); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }
         public static void watchtopic_delete(object watchTopicID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": MsSql.LegacyDb.watchtopic_delete(connectionString, watchTopicID); break;
                 case "Npgsql": Postgre.Db.watchtopic_delete(connectionString, watchTopicID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.watchtopic_delete(connectionString, watchTopicID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.watchtopic_delete(connectionString, watchTopicID); break;
                 // case "oracle":   orPostgre.Db.watchtopic_delete(connectionString, watchTopicID);break;
                 // case "db2":   db2Postgre.Db.watchtopic_delete(connectionString, watchTopicID); break;
                 // case "other":   othPostgre.Db.watchtopic_delete(connectionString, watchTopicID); break;
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }
         }
         static public DataTable watchtopic_list(object userId)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 // case "System.Data.SqlClient": return MsSql.LegacyDb.watchtopic_list(connectionString, userId);
                 case "Npgsql": return Postgre.Db.watchtopic_list(connectionString, userId);
                 case "MySql.Data.MySqlClient": return MySqlDb.Db.watchtopic_list(connectionString, userId);
                 case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.watchtopic_list(connectionString, userId);
                 // case "oracle":  return orPostgre.Db.watchtopic_list(connectionString, userId);
                 // case "db2":  return db2Postgre.Db.watchtopic_list(connectionString, userId);
                 // case "other":  return othPostgre.Db.watchtopic_list(connectionString, userId); 
                 default:
                     throw new ApplicationException("No return type");
                     break;
             }

         }


         // Properties

        public static int GetDBSize()
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.GetDBSize(connectionString);
                case "Npgsql": return Postgre.Db.GetDBSize(connectionString);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.GetDBSize(connectionString);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.GetDBSize(connectionString);
                // case "oracle":  return orPostgre.Db.GetDBSize();
                // case "db2":  return db2Postgre.Db.GetDBSize();
                // case "other":  return othPostgre.Db.GetDBSize(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ",
                                                                 mid));
                    break;
            }
        }

        public static bool GetIsForumInstalled()
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.GetIsForumInstalled(connectionString);
                case "Npgsql": return Postgre.Db.GetIsForumInstalled(connectionString);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.GetIsForumInstalled(connectionString);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.GetIsForumInstalled(connectionString);
                // case "oracle":  return orPostgre.Db.GetIsForumInstalled();
                // case "db2":  return db2Postgre.Db.GetIsForumInstalled();
                // case "other":  return othPostgre.Db.GetIsForumInstalled(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ",
                                                                 mid));
                    break;
            }
        }
       

        public static int GetDBVersion()
        {
            string dataEngine;
            string connectionString;
            int? mid = 0;  string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
            
            switch (dataEngine)
            {
                // case "System.Data.SqlClient": return MsSql.LegacyDb.GetDBVersion(connectionString);
                case "Npgsql": return Postgre.Db.GetDBVersion(connectionString);
                case "MySql.Data.MySqlClient": return MySqlDb.Db.GetDBVersion(connectionString);
                case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.GetDBVersion(connectionString);
                    // case "oracle":  return orPostgre.Db.GetDBVersion();
                    // case "db2":  return db2Postgre.Db.GetDBVersion();
                    // case "other":  return othPostgre.Db.GetDBVersion(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ",
                                                                 mid));
                    break;
            }
        }

        public static bool FullTextSupported
         {
             get
             { 
                 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.FullTextSupported;;
                     case "Npgsql": return Postgre.Db.FullTextSupported;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.FullTextSupported;;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.FullTextSupported;;
                     // case "oracle":  return orPostgre.Db.fullTextSupported;;
                     // case "db2":  return db2Postgre.Db.fullTextSupported;;
                     // case "other":  return othPostgre.Db.fullTextSupported;; 
                     default:
                         throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ",
                                                                      mid));
                         break;
                 }

             }
             set
             {
                 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient":  MsSql.LegacyDb.FullTextSupported = value; break;
                     case "Npgsql": Postgre.Db.FullTextSupported = value; break;
                     case "MySql.Data.MySqlClient":  MySqlDb.Db.FullTextSupported = value; break;
                     case "FirebirdSql.Data.FirebirdClient": FirebirdDb.Db.FullTextSupported = value; break;
                     // case "oracle":   orPostgre.Db.fullTextSupported = value; break;
                     // case "db2":   db2Postgre.Db.fullTextSupported = value; break;
                     // case "other":   othPostgre.Db.fullTextSupported = value; break; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }

                 
             }
         }

       
         public static string FullTextScript
         {
             get
             {
                 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.FullTextScript;
                     case "Npgsql": return Postgre.Db.FullTextScript;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.FullTextScript;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.FullTextScript;
                     // case "oracle":  return orPostgre.Db.fullTextScript;
                     // case "db2":  return db2Postgre.Db.fullTextScript;
                     // case "other":  return othPostgre.Db.fullTextScript; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
                 
             }
             set
             {
                 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient":  MsSql.LegacyDb.FullTextScript = value; break;
                     case "Npgsql": Postgre.Db.FullTextScript = value; break;
                     case "MySql.Data.MySqlClient":  MySqlDb.Db.FullTextScript = value; break;
                     case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.FullTextScript = value; break;
                     // case "oracle":   orPostgre.Db.fullTextScript = value; break;
                     // case "db2":   db2Postgre.Db.fullTextScript = value; break;
                     // case "other":   othPostgre.Db.fullTextScript = value; break; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
                 
             }

         }

       /*  private static List<ConnectionStringOptions> _connectionOptions;

         public static  List<ConnectionStringOptions> ConnectionOptions
         {
             get { return _connectionOptions; }
             set { _connectionOptions = value; }
         } */

        //added vzrus
		#region ConnectionStringOptions

		public static string ProviderAssemblyName
		{
			get
			{
                 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.ProviderAssemblyName;
                     case "Npgsql": return Postgre.Db.ProviderAssemblyName;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.ProviderAssemblyName;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.ProviderAssemblyName;
                     // case "oracle":  return orPostgre.Db.ProviderAssemblyName;
                     // case "db2":  return db2Postgre.Db.ProviderAssemblyName;
                     // case "other":  return othPostgre.Db.ProviderAssemblyName; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool PasswordPlaceholderVisible
		{
			get
			{
				 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.PasswordPlaceholderVisible;
                     case "Npgsql": return Postgre.Db.PasswordPlaceholderVisible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.PasswordPlaceholderVisible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PasswordPlaceholderVisible;
                     // case "oracle":  return orPostgre.Db.PasswordPlaceholderVisible;
                     // case "db2":  return db2Postgre.Db.PasswordPlaceholderVisible;
                     // case "other":  return othPostgre.Db.PasswordPlaceholderVisible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
	  
		//Parameter 1
		public static string Parameter1_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter1_Name;
                     case "Npgsql": return Postgre.Db.Parameter1_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter1_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter1_Name;
                     // case "oracle":  return orPostgre.Db.Parameter1_Name;
                     // case "db2":  return db2Postgre.Db.Parameter1_Name;
                     // case "other":  return othPostgre.Db.Parameter1_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter1_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter1_Value;
                     case "Npgsql": return Postgre.Db.Parameter1_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter1_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter1_Value;
                     // case "oracle":  return orPostgre.Db.Parameter1_Value;
                     // case "db2":  return db2Postgre.Db.Parameter1_Value;
                     // case "other":  return othPostgre.Db.Parameter1_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter1_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter1_Visible;
                     case "Npgsql": return Postgre.Db.Parameter1_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter1_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter1_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter1_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter1_Visible;
                     // case "other":  return othPostgre.Db.Parameter1_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 2
		public static string Parameter2_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter2_Name;
                     case "Npgsql": return Postgre.Db.Parameter2_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter2_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter2_Name;
                     // case "oracle":  return orPostgre.Db.Parameter2_Name;
                     // case "db2":  return db2Postgre.Db.Parameter2_Name;
                     // case "other":  return othPostgre.Db.Parameter2_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter2_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter2_Value;
                     case "Npgsql": return Postgre.Db.Parameter2_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter2_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter2_Value;
                     // case "oracle":  return orPostgre.Db.Parameter2_Value;
                     // case "db2":  return db2Postgre.Db.Parameter2_Value;
                     // case "other":  return othPostgre.Db.Parameter2_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter2_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter2_Visible;
                     case "Npgsql": return Postgre.Db.Parameter2_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter2_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter2_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter2_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter2_Visible;
                     // case "other":  return othPostgre.Db.Parameter2_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 3
		public static string Parameter3_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter3_Name;
                     case "Npgsql": return Postgre.Db.Parameter3_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter3_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter3_Name;
                     // case "oracle":  return orPostgre.Db.Parameter3_Name;
                     // case "db2":  return db2Postgre.Db.Parameter3_Name;
                     // case "other":  return othPostgre.Db.Parameter3_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
        public static string Parameter3_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter3_Value;
                     case "Npgsql": return Postgre.Db.Parameter3_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter3_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter3_Value;
                     // case "oracle":  return orPostgre.Db.Parameter3_Value;
                     // case "db2":  return db2Postgre.Db.Parameter3_Value;
                     // case "other":  return othPostgre.Db.Parameter3_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter3_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter3_Visible;
                     case "Npgsql": return Postgre.Db.Parameter3_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter3_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter3_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter3_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter3_Visible;
                     // case "other":  return othPostgre.Db.Parameter3_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 4
		public static string Parameter4_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter4_Name;
                     case "Npgsql": return Postgre.Db.Parameter4_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter4_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter4_Name;
                     // case "oracle":  return orPostgre.Db.Parameter4_Name;
                     // case "db2":  return db2Postgre.Db.Parameter4_Name;
                     // case "other":  return othPostgre.Db.Parameter4_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter4_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter4_Value;
                     case "Npgsql": return Postgre.Db.Parameter4_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter4_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter4_Value;
                     // case "oracle":  return orPostgre.Db.Parameter4_Value;
                     // case "db2":  return db2Postgre.Db.Parameter4_Value;
                     // case "other":  return othPostgre.Db.Parameter4_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter4_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter4_Visible;
                     case "Npgsql": return Postgre.Db.Parameter4_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter4_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter4_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter4_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter4_Visible;
                     // case "other":  return othPostgre.Db.Parameter4_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 5
		public static string Parameter5_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter5_Name;
                     case "Npgsql": return Postgre.Db.Parameter5_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter5_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter5_Name;
                     // case "oracle":  return orPostgre.Db.Parameter5_Name;
                     // case "db2":  return db2Postgre.Db.Parameter5_Name;
                     // case "other":  return othPostgre.Db.Parameter5_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter5_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter5_Value;
                     case "Npgsql": return Postgre.Db.Parameter5_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter5_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter5_Value;
                     // case "oracle":  return orPostgre.Db.Parameter5_Value;
                     // case "db2":  return db2Postgre.Db.Parameter5_Value;
                     // case "other":  return othPostgre.Db.Parameter5_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter5_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter5_Visible;
                     case "Npgsql": return Postgre.Db.Parameter5_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter5_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter5_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter5_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter5_Visible;
                     // case "other":  return othPostgre.Db.Parameter5_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 6
		public static string Parameter6_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter6_Name;
                     case "Npgsql": return Postgre.Db.Parameter6_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter6_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter6_Name;
                     // case "oracle":  return orPostgre.Db.Parameter6_Name;
                     // case "db2":  return db2Postgre.Db.Parameter6_Name;
                     // case "other":  return othPostgre.Db.Parameter6_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter6_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter6_Value;
                     case "Npgsql": return Postgre.Db.Parameter6_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter6_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter6_Value;
                     // case "oracle":  return orPostgre.Db.Parameter6_Value;
                     // case "db2":  return db2Postgre.Db.Parameter6_Value;
                     // case "other":  return othPostgre.Db.Parameter6_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter6_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter6_Visible;
                     case "Npgsql": return Postgre.Db.Parameter6_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter6_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter6_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter6_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter6_Visible;
                     // case "other":  return othPostgre.Db.Parameter6_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 7
		public static string Parameter7_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter7_Name;
                     case "Npgsql": return Postgre.Db.Parameter7_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter7_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter7_Name;
                     // case "oracle":  return orPostgre.Db.Parameter7_Name;
                     // case "db2":  return db2Postgre.Db.Parameter7_Name;
                     // case "other":  return othPostgre.Db.Parameter7_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static string Parameter7_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter7_Value;
                     case "Npgsql": return Postgre.Db.Parameter7_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter7_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter7_Value;
                     // case "oracle":  return orPostgre.Db.Parameter7_Value;
                     // case "db2":  return db2Postgre.Db.Parameter7_Value;
                     // case "other":  return othPostgre.Db.Parameter7_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter7_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter7_Visible;
                     case "Npgsql": return Postgre.Db.Parameter7_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter7_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter7_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter7_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter7_Visible;
                     // case "other":  return othPostgre.Db.Parameter7_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 8
		public static string Parameter8_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter8_Name;
                     case "Npgsql": return Postgre.Db.Parameter8_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter8_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter8_Name;
                     // case "oracle":  return orPostgre.Db.Parameter8_Name;
                     // case "db2":  return db2Postgre.Db.Parameter8_Name;
                     // case "other":  return othPostgre.Db.Parameter8_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static string Parameter8_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter8_Value;
                     case "Npgsql": return Postgre.Db.Parameter8_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter8_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter8_Value;
                     // case "oracle":  return orPostgre.Db.Parameter8_Value;
                     // case "db2":  return db2Postgre.Db.Parameter8_Value;
                     // case "other":  return othPostgre.Db.Parameter8_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter8_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter8_Visible;
                     case "Npgsql": return Postgre.Db.Parameter8_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter8_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter8_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter8_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter8_Visible;
                     // case "other":  return othPostgre.Db.Parameter8_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 9
		public static string Parameter9_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter9_Name;
                     case "Npgsql": return Postgre.Db.Parameter9_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter9_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter9_Name;
                     // case "oracle":  return orPostgre.Db.Parameter9_Name;
                     // case "db2":  return db2Postgre.Db.Parameter9_Name;
                     // case "other":  return othPostgre.Db.Parameter9_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static string Parameter9_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter9_Value;
                     case "Npgsql": return Postgre.Db.Parameter9_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter9_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter9_Value;
                     // case "oracle":  return orPostgre.Db.Parameter9_Value;
                     // case "db2":  return db2Postgre.Db.Parameter9_Value;
                     // case "other":  return othPostgre.Db.Parameter9_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter9_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter9_Visible;
                     case "Npgsql": return Postgre.Db.Parameter9_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter9_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter9_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter9_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter9_Visible;
                     // case "other":  return othPostgre.Db.Parameter9_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 10
		public static string Parameter10_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter10_Name;
                     case "Npgsql": return Postgre.Db.Parameter10_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter10_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter10_Name;
                     // case "oracle":  return orPostgre.Db.Parameter10_Name;
                     // case "db2":  return db2Postgre.Db.Parameter10_Name;
                     // case "other":  return othPostgre.Db.Parameter10_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static string Parameter10_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter10_Value;
                     case "Npgsql": return Postgre.Db.Parameter10_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter10_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter10_Value;
                     // case "oracle":  return orPostgre.Db.Parameter10_Value;
                     // case "db2":  return db2Postgre.Db.Parameter10_Value;
                     // case "other":  return othPostgre.Db.Parameter10_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter10_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter10_Visible;
                     case "Npgsql": return Postgre.Db.Parameter10_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter10_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter10_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter10_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter10_Visible;
                     // case "other":  return othPostgre.Db.Parameter10_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		// Role=
		//Check boxes

		//Parameter 11 hides user password placeholder! 12 reserved for User Instance

		public static string Parameter11_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter11_Name;
                     case "Npgsql": return Postgre.Db.Parameter11_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter11_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter11_Name;
                     // case "oracle":  return orPostgre.Db.Parameter11_Name;
                     // case "db2":  return db2Postgre.Db.Parameter11_Name;
                     // case "other":  return othPostgre.Db.Parameter11_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter11_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter11_Value;
                     case "Npgsql": return Postgre.Db.Parameter11_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter11_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter11_Value;
                     // case "oracle":  return orPostgre.Db.Parameter11_Value;
                     // case "db2":  return db2Postgre.Db.Parameter11_Value;
                     // case "other":  return othPostgre.Db.Parameter11_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter11_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter11_Visible;
                     case "Npgsql": return Postgre.Db.Parameter11_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter11_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter11_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter11_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter11_Visible;
                     // case "other":  return othPostgre.Db.Parameter11_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static string Parameter12_Name
		{
			get
			{

				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter12_Name;
                     case "Npgsql": return Postgre.Db.Parameter12_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter12_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter12_Name;
                     // case "oracle":  return orPostgre.Db.Parameter12_Name;
                     // case "db2":  return db2Postgre.Db.Parameter12_Name;
                     // case "other":  return othPostgre.Db.Parameter12_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter12_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter12_Value;
                     case "Npgsql": return Postgre.Db.Parameter12_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter12_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter12_Value;
                     // case "oracle":  return orPostgre.Db.Parameter12_Value;
                     // case "db2":  return db2Postgre.Db.Parameter12_Value;
                     // case "other":  return othPostgre.Db.Parameter12_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter12_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter12_Visible;
                     case "Npgsql": return Postgre.Db.Parameter12_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter12_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter12_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter12_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter12_Visible;
                     // case "other":  return othPostgre.Db.Parameter12_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static string Parameter13_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter13_Name;
                     case "Npgsql": return Postgre.Db.Parameter13_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter13_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter13_Name;
                     // case "oracle":  return orPostgre.Db.Parameter13_Name;
                     // case "db2":  return db2Postgre.Db.Parameter13_Name;
                     // case "other":  return othPostgre.Db.Parameter13_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter13_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter13_Value;
                     case "Npgsql": return Postgre.Db.Parameter13_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter13_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter13_Value;
                     // case "oracle":  return orPostgre.Db.Parameter13_Value;
                     // case "db2":  return db2Postgre.Db.Parameter13_Value;
                     // case "other":  return othPostgre.Db.Parameter13_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter13_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter13_Visible;
                     case "Npgsql": return Postgre.Db.Parameter13_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter13_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter13_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter13_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter13_Visible;
                     // case "other":  return othPostgre.Db.Parameter13_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 14
		public static string Parameter14_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter14_Name;
                     case "Npgsql": return Postgre.Db.Parameter14_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter14_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter14_Name;
                     // case "oracle":  return orPostgre.Db.Parameter14_Name;
                     // case "db2":  return db2Postgre.Db.Parameter14_Name;
                     // case "other":  return othPostgre.Db.Parameter14_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter14_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter14_Value;
                     case "Npgsql": return Postgre.Db.Parameter14_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter14_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter14_Value;
                     // case "oracle":  return orPostgre.Db.Parameter14_Value;
                     // case "db2":  return db2Postgre.Db.Parameter14_Value;
                     // case "other":  return othPostgre.Db.Parameter4_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter14_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter14_Visible;
                     case "Npgsql": return Postgre.Db.Parameter14_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter14_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter14_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter14_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter14_Visible;
                     // case "other":  return othPostgre.Db.Parameter14_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 15
		public static string Parameter15_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter15_Name;
                     case "Npgsql": return Postgre.Db.Parameter15_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter15_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter15_Name;
                     // case "oracle":  return orPostgre.Db.Parameter15_Name;
                     // case "db2":  return db2Postgre.Db.Parameter15_Name;
                     // case "other":  return othPostgre.Db.Parameter15_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter15_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter15_Value;
                     case "Npgsql": return Postgre.Db.Parameter15_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter15_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter15_Value;
                     // case "oracle":  return orPostgre.Db.Parameter15_Value;
                     // case "db2":  return db2Postgre.Db.Parameter15_Value;
                     // case "other":  return othPostgre.Db.Parameter15_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter15_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter15_Visible;
                     case "Npgsql": return Postgre.Db.Parameter15_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter15_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter15_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter15_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter15_Visible;
                     // case "other":  return othPostgre.Db.Parameter15_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 16
		public static string Parameter16_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter16_Name;
                     case "Npgsql": return Postgre.Db.Parameter16_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter16_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter16_Name;
                     // case "oracle":  return orPostgre.Db.Parameter16_Name;
                     // case "db2":  return db2Postgre.Db.Parameter16_Name;
                     // case "other":  return othPostgre.Db.Parameter16_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter16_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter16_Value;
                     case "Npgsql": return Postgre.Db.Parameter16_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter16_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter16_Value;
                     // case "oracle":  return orPostgre.Db.Parameter16_Value;
                     // case "db2":  return db2Postgre.Db.Parameter16_Value;
                     // case "other":  return othPostgre.Db.Parameter16_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter16_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter16_Visible;
                     case "Npgsql": return Postgre.Db.Parameter16_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter16_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter16_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter16_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter16_Visible;
                     // case "other":  return othPostgre.Db.Parameter16_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 17
		public static string Parameter17_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter17_Name;
                     case "Npgsql": return Postgre.Db.Parameter17_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter17_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter17_Name;
                     // case "oracle":  return orPostgre.Db.Parameter17_Name;
                     // case "db2":  return db2Postgre.Db.Parameter17_Name;
                     // case "other":  return othPostgre.Db.Parameter17_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter17_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter17_Value;
                     case "Npgsql": return Postgre.Db.Parameter17_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter17_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter17_Value;
                     // case "oracle":  return orPostgre.Db.Parameter17_Value;
                     // case "db2":  return db2Postgre.Db.Parameter17_Value;
                     // case "other":  return othPostgre.Db.Parameter17_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter17_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter17_Visible;
                     case "Npgsql": return Postgre.Db.Parameter17_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter17_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter17_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter17_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter17_Visible;
                     // case "other":  return othPostgre.Db.Parameter17_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		//Parameter 18
		public static string Parameter18_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter18_Name;
                     case "Npgsql": return Postgre.Db.Parameter18_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter18_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter18_Name;
                     // case "oracle":  return orPostgre.Db.Parameter18_Name;
                     // case "db2":  return db2Postgre.Db.Parameter18_Name;
                     // case "other":  return othPostgre.Db.Parameter18_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter18_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter18_Value;
                     case "Npgsql": return Postgre.Db.Parameter18_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter18_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter18_Value;
                     // case "oracle":  return orPostgre.Db.Parameter18_Value;
                     // case "db2":  return db2Postgre.Db.Parameter18_Value;
                     // case "other":  return othPostgre.Db.Parameter18_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter18_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter18_Visible;
                     case "Npgsql": return Postgre.Db.Parameter18_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter18_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter18_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter18_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter18_Visible;
                     // case "other":  return othPostgre.Db.Parameter18_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		//Parameter 19
		public static string Parameter19_Name
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter19_Name;
                     case "Npgsql": return Postgre.Db.Parameter19_Name;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter19_Name;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter19_Name;
                     // case "oracle":  return orPostgre.Db.Parameter19_Name;
                     // case "db2":  return db2Postgre.Db.Parameter19_Name;
                     // case "other":  return othPostgre.Db.Parameter19_Name; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter19_Value
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter19_Value;
                     case "Npgsql": return Postgre.Db.Parameter19_Value;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter19_Value;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter19_Value;
                     // case "oracle":  return orPostgre.Db.Parameter19_Value;
                     // case "db2":  return db2Postgre.Db.Parameter19_Value;
                     // case "other":  return othPostgre.Db.Parameter19_Value; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		public static bool Parameter19_Visible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.Parameter19_Visible;
                     case "Npgsql": return Postgre.Db.Parameter19_Visible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.Parameter19_Visible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.Parameter19_Visible;
                     // case "oracle":  return orPostgre.Db.Parameter19_Visible;
                     // case "db2":  return db2Postgre.Db.Parameter19_Visible;
                     // case "other":  return othPostgre.Db.Parameter19_Visible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		#endregion


         public static string [] ScriptList
		{
			get
			{
                string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.ScriptList;
                     case "Npgsql": return Postgre.Db.ScriptList;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.ScriptList;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.ScriptList;
                     // case "oracle":  return orPostgre.Db.scriptList;
                     // case "db2":  return db2Postgre.Db.scriptList;
                     // case "other":  return othPostgre.Db.scriptList; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
				
			}
		}

        		public static bool PanelGetStats
		{
			get
			{
				 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.PanelGetStats;
                     case "Npgsql": return Postgre.Db.PanelGetStats;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.PanelGetStats;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PanelGetStats;
                     // case "oracle":  return orPostgre.Db.PanelGetStats;
                     // case "db2":  return db2Postgre.Db.PanelGetStats;
                     // case "other":  return othPostgre.Db.PanelGetStats; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static bool PanelRecoveryMode
		{
			get
			{
				 string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.PanelRecoveryMode;
                     case "Npgsql": return Postgre.Db.PanelRecoveryMode;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.PanelRecoveryMode;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PanelRecoveryMode;
                     // case "oracle":  return orPostgre.Db.PanelRecoveryMode;
                     // case "db2":  return db2Postgre.Db.PanelRecoveryMode;
                     // case "other":  return othPostgre.Db.PanelRecoveryMode; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static bool PanelReindex
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.PanelReindex;
                     case "Npgsql": return Postgre.Db.PanelReindex;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.PanelReindex;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PanelReindex;
                     // case "oracle":  return orPostgre.Db.PanelReindex;
                     // case "db2":  return db2Postgre.Db.PanelReindex;
                     // case "other":  return othPostgre.Db.PanelReindex; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}
		public static bool PanelShrink
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.PanelShrink;
                     case "Npgsql": return Postgre.Db.PanelShrink;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.PanelShrink;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.PanelShrink;
                     // case "oracle":  return orPostgre.Db.PanelShrink;
                     // case "db2":  return db2Postgre.Db.PanelShrink;
                     // case "other":  return othPostgre.Db.PanelShrink; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		}

		
		public static bool btnReindexVisible
		{
			get
			{
				string dataEngine;
                 string connectionString;
                 int? mid = 0;  string namePattern = string.Empty;
                 CommonSqlDbAccess.GetConnectionData((int)mid, namePattern, out dataEngine, out connectionString);
                 
                 switch (dataEngine)
                 {
                     // case "System.Data.SqlClient": return MsSql.LegacyDb.btnReindexVisible;
                     case "Npgsql": return Postgre.Db.btnReindexVisible;
                     case "MySql.Data.MySqlClient": return MySqlDb.Db.btnReindexVisible;
                     case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.btnReindexVisible;
                     // case "oracle":  return orPostgre.Db.btnReindexVisible;
                     // case "db2":  return db2Postgre.Db.btnReindexVisible;
                     // case "other":  return othPostgre.Db.btnReindexVisible; 
                     default:
                         throw new ApplicationException("No return type");
                         break;
                 }
			}
		} 
         


    }

    

}
