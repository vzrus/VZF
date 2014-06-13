// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="CommonDb.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The common db.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using VZF.Data.DAL;
    using VZF.Data.Utils;
  
    using VZF.Types.Data;
    using VZF.Types.Objects;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Objects;

    /// <summary>
    /// The common db.
    /// </summary>
    public static class CommonDb
    {
        #region Access Masks

        /// <summary>
        /// Deletes the access mask.
        /// </summary>
        /// <param name="mid">
        /// The module id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id. 
        /// </param>
        /// <returns> 
        /// A <see cref="T:System.Boolean"/> with true if access mask was deleted and false if deletion failed.
        /// </returns>
        public static bool accessmask_delete(int? mid, object accessMaskID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {   
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
              
                sc.CommandText.AppendObjectQuery("accessmask_delete", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The accessmask_list.
        /// </summary>
        /// <param name="mid">
        ///     The mid.
        /// </param>
        /// <param name="boardId">
        ///     The board id.
        /// </param>
        /// <param name="accessMaskID">
        ///     The access mask id.
        /// </param>
        /// <param name="excludeFlags">
        ///     The exclude flags.
        /// </param>
        /// <param name="pageUserID">
        ///     The page user id.
        /// </param>
        /// <param name="isUserMask">
        ///     The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        ///     The is admin mask.
        /// </param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable accessmask_list(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask,
            int pageIndex,
            int pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExcludeFlags", excludeFlags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserMask", isUserMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdminMask", isAdminMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
               
                sc.CommandText.AppendObjectQuery("accessmask_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
            }
        }

        /// <summary>
        /// Gets a list of access mask properties for personal forums(blogs).
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        /// <param name="excludeFlags">
        /// The exclude flags.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="isUserMask">
        /// The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        /// The is admin mask.
        /// </param>
        /// <returns>
        ///  A <see cref="T:System.Data.DataTable"/> of Access Masks.
        /// </returns>
        public static DataTable accessmask_pforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExcludeFlags", excludeFlags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserMask", isUserMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdminMask", isAdminMask));

                sc.CommandText.AppendObjectQuery("accessmask_pforumlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The accessmask_aforumlist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        /// <param name="excludeFlags">
        /// The exclude flags.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="isUserMask">
        /// The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        /// The is admin mask.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable accessmask_aforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExcludeFlags", excludeFlags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserMask", isUserMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdminMask", isAdminMask));

                sc.CommandText.AppendObjectQuery("accessmask_aforumlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The accessmask_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="accessMaskId">
        /// The access mask id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="readAccess">
        /// The read access.
        /// </param>
        /// <param name="postAccess">
        /// The post access.
        /// </param>
        /// <param name="replyAccess">
        /// The reply access.
        /// </param>
        /// <param name="priorityAccess">
        /// The priority access.
        /// </param>
        /// <param name="pollAccess">
        /// The poll access.
        /// </param>
        /// <param name="voteAccess">
        /// The vote access.
        /// </param>
        /// <param name="moderatorAccess">
        /// The moderator access.
        /// </param>
        /// <param name="editAccess">
        /// The edit access.
        /// </param>
        /// <param name="deleteAccess">
        /// The delete access.
        /// </param>
        /// <param name="uploadAccess">
        /// The upload access.
        /// </param>
        /// <param name="downloadAccess">
        /// The download access.
        /// </param>
        /// <param name="userForumAccess">
        /// The user forum access.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserMask">
        /// The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        /// The is admin mask.
        /// </param>
        public static void accessmask_save(
            int? mid,
            object accessMaskId,
            object boardId,
            object name,
            object readAccess,
            object postAccess,
            object replyAccess,
            object priorityAccess,
            object pollAccess,
            object voteAccess,
            object moderatorAccess,
            object editAccess,
            object deleteAccess,
            object uploadAccess,
            object downloadAccess,
            object userForumAccess,
            object sortOrder,
            object userId,
            object isUserMask,
            object isAdminMask)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskId ?? DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ReadAccess", readAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_PostAccess", postAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ReplyAccess", replyAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_PriorityAccess", priorityAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_PollAccess", pollAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_VoteAccess", voteAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ModeratorAccess", moderatorAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_EditAccess", editAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeleteAccess", deleteAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UploadAccess", uploadAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DownloadAccess", downloadAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UserForumAccess", userForumAccess));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserMask", isUserMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdminMask", isAdminMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("accessmask_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        #endregion

        /// <summary>
        /// Gets list of active users
        /// </summary>
        /// <param name="mid">
        /// The module id.
        /// </param>
        /// <param name="boardId">
        /// The BoardID
        /// </param>
        /// <param name="guests">
        /// Show guests, boolean
        /// </param>
        /// <param name="showCrawlers">
        /// Show crawlers.
        /// </param>
        /// <param name="interval">
        /// The interval.
        /// </param>
        /// <param name="styledNicks">
        /// Return styled nicks info.
        /// </param>
        /// <returns>Returns  a <see cref="T:System.Data.DataTable"/>  of active users</returns>  
        public static DataTable active_list(
            int? mid,
            object boardId,
            object guests,
            object showCrawlers,
            int interval,
            object styledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Guests", guests ?? false));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowCrawlers", showCrawlers));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ActiveTime", interval));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("active_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);               
            }
        }

        /// <summary>
        /// A Data Table for active list for a specific user.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The Board ID.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="guests">
        /// Show guests, boolean
        /// </param>
        /// <param name="showCrawlers">
        /// Show crawlers in the list.
        /// </param>
        /// <param name="activeTime">
        /// The time to keep an active user in the active table.
        /// </param>
        /// <param name="styledNicks">
        /// Use styled nicks for a user.
        /// </param>
        /// <returns> A <see cref="T:System.Data.DataTable"/> for active list for a specific user.</returns>
        public static DataTable active_list_user(
            int? mid,
            object boardId,
            object userID,
            object guests,
            object showCrawlers,
            int activeTime,
            object styledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Guests", guests ?? false));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowCrawlers", showCrawlers));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ActiveTime", activeTime));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("active_list_user", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The list of active users for a forum.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="forumID">
        /// The forum ID.
        /// </param>
        /// <param name="styledNicks">
        /// Should you return styled nicks info?
        /// </param>
        /// <returns> 
        /// A <see cref="T:System.Data.DataTable"/> with list of active users for a forum.
        /// </returns>
        public static DataTable active_listforum(int? mid, object forumID, object styledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));

                sc.CommandText.AppendObjectQuery("active_listforum", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// A list of currently active users in a topic.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="topicID">
        /// The topic ID.
        /// </param>
        /// <param name="styledNicks">
        /// Return styled nicks info.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with a users which are currently active in a topic.
        /// </returns>
        public static DataTable active_listtopic(int? mid, object topicID, object styledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));

                sc.CommandText.AppendObjectQuery("active_listtopic", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// List of active users stats without details. 
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with a simple active users list.
        /// </returns>
        public static DataRow active_stats(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("active_stats", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true).Rows[0];
            }
        }

        /// <summary>
        /// Resets ActiveAccess cache table contents.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        public static void activeaccess_reset(int? mid)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendObjectQuery("activeaccess_reset", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Lists birthdays list for today.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return styled nicks string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with today's birdays list.
        /// </returns>
        public static DataTable User_ListTodaysBirthdays(int? mid, object boardId, object useStyledNicks)
        {
            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CurrentYear", DateTime.UtcNow.Year));
                    sc.Parameters.Add(
                        sc.CreateParameter(DbType.DateTime, "i_CurrentUtc", DateTime.UtcNow.Date.AddDays(-1)));
                    sc.Parameters.Add(
                        sc.CreateParameter(DbType.DateTime, "i_CurrentUtc1", DateTime.UtcNow.Date.AddDays(-1)));
                    sc.Parameters.Add(
                        sc.CreateParameter(DbType.DateTime, "i_CurrentUtc2", DateTime.UtcNow.Date.AddDays(1)));

                    sc.CommandText.AppendObjectQuery("user_listtodaysbirthdays", mid);
                    return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                }
            }
            catch (Exception e)
            {
                eventlog_create(
                    mid,
                    null,
                    e.Source,
                    "Please, change and save your profile once : " + e.Message,
                    EventLogTypes.Error);
            }

            return null;
        }

        /// <summary>
        /// A list to show admins for the board.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return styled nicks string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with list of a board admins.
        /// </returns>
        public static DataTable admin_list(int? mid, object boardId, object useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("admin_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// A list of page access for admins.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return styled nicks string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with list of pages access for admins. 
        /// </returns>
        public static DataTable admin_pageaccesslist(
            int? mid,
            [CanBeNull] object boardId,
            [NotNull] object useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("admin_pageaccesslist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The list of permissions for a page access.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        /// <param name="pageName">
        /// The page name to check for access.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with list page access for admins.
        /// </returns>
        public static DataTable adminpageaccess_list(int? mid, [CanBeNull] object userId, [CanBeNull] object pageName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                int userIdResult;
                int.TryParse(userId.ToString(), out userIdResult);
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userIdResult));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PageName", pageName));

                sc.CommandText.AppendObjectQuery("adminpageaccess_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Delete admin page access for each page.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        /// <param name="pageName">
        /// The page name to check for access.
        /// </param>
        public static void adminpageaccess_delete(int? mid, [NotNull] object userId, [CanBeNull] object pageName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                int userIdResult;
                int.TryParse(userId.ToString(), out userIdResult);
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userIdResult));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PageName", pageName));

                sc.CommandText.AppendObjectQuery("adminpageaccess_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Save admin pages access rights.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        /// <param name="pageName">
        /// The page name to check for access.
        /// </param>
        public static void adminpageaccess_save(int? mid, [NotNull] object userId, [CanBeNull] object pageName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                int userIdResult;
                int.TryParse(userId.ToString(), out userIdResult);
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userIdResult));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_PageName", pageName));

                sc.CommandText.AppendObjectQuery("adminpageaccess_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Deletes a user album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        public static void album_delete(int? mid, object AlbumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AlbumID", AlbumID));

                sc.CommandText.AppendObjectQuery("album_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Gets stats row on album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="UserID">
        /// The user ID.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Int32"/> array with stats row.
        /// </returns>
        public static int[] album_getstats(int? mid, object UserID, object AlbumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_UserID",
                        (UserID == null || UserID.ToString() == "0") ? null : UserID));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_AlbumID",
                        (AlbumID == null || AlbumID.ToString() == "0") ? null : AlbumID));

                sc.CommandText.AppendObjectQuery("album_getstats", mid);
                var strow =
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true).Rows[0];
                return new[] { Convert.ToInt32(strow["AlbumNumber"]), Convert.ToInt32(strow["ImageNumber"]) };
            }
        }

        /// <summary>
        /// Gets a title for an album with specified id. 
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="albumId">
        /// The album Id.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.String"/> with album title.
        /// </returns>
        public static string album_gettitle(int? mid, object albumId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_AlbumID",
                        (albumId == null || albumId.ToString() == "0") ? null : albumId));

                sc.CommandText.AppendObjectQuery("album_gettitle", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// Deletes an image with a specified Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="imageId">
        /// The image id.
        /// </param>
        public static void album_image_delete(int? mid, object imageId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ImageID", imageId));

                sc.CommandText.AppendObjectQuery("album_image_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Saved downloaded image.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="imageId">
        /// The image id.
        /// </param>
        public static void album_image_download(int? mid, object imageId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(DbType.Int32, "i_ImageID", imageId.ToString() == "0" ? null : imageId));

                sc.CommandText.AppendObjectQuery("album_image_download", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// All album images for a user.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with album images for a user.
        /// </returns>
        public static DataTable album_images_by_user(int? mid, [NotNull] object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", (int)userId));

                sc.CommandText.AppendObjectQuery("album_images_by_user", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Image list in an album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <param name="ImageID">
        /// The image ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with album images for an album.
        /// </returns>
        public static DataTable album_image_list(int? mid, object AlbumID, object ImageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AlbumID", AlbumID));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_ImageID",
                        (ImageID == null || ImageID.ToString() == "0") ? null : ImageID));

                sc.CommandText.AppendObjectQuery("album_image_list", mid);
                DataTable dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                if (dt.Rows.Count <= 0)
                {
                    return dt;
                }

                if (dt.Rows[0]["ImageID"] != DBNull.Value)
                {
                    return dt;
                }

                dt.Rows.RemoveAt(0);
                dt.AcceptChanges();

                return dt;
            }
        }

        /// <summary>
        /// Saves an image in an album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="imageId">
        /// The image Id.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <param name="Caption">
        /// The album caption.
        /// </param>
        /// <param name="FileName">
        /// The image file name.
        /// </param>
        /// <param name="Bytes">
        /// Image size in bytes.
        /// </param>
        /// <param name="ContentType">
        /// The content type.
        /// </param>
        public static void album_image_save(
            int? mid,
            object imageId,
            object AlbumID,
            object Caption,
            object FileName,
            object Bytes,
            object ContentType)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_ImageID",
                        (imageId == null || imageId.ToString() == "0") ? null : imageId));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_AlbumID",
                        (AlbumID == null || AlbumID.ToString() == "0") ? null : AlbumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Caption", Caption));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_FileName", FileName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Bytes", Bytes));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ContentType", ContentType));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("album_image_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Album list.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="UserID">
        /// The userId.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with an album list.
        /// </returns>
        public static DataTable album_list(int? mid, object UserID, object AlbumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_UserID",
                        (UserID == null || UserID.ToString() == "0") ? null : UserID));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_AlbumID",
                        (AlbumID == null || AlbumID.ToString() == "0") ? null : AlbumID));

                sc.CommandText.AppendObjectQuery("album_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Saves an album.
        /// </summary>
        /// <param name="mid">
        /// The module ID
        /// .</param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <param name="UserID">
        /// The user Id.
        /// </param>
        /// <param name="Title">
        /// An album title.
        /// </param>
        /// <param name="CoverImageID">
        /// The album cover image id.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Int32"/> with a saved album Id.
        /// </returns>
        public static int album_save(int? mid, object AlbumID, object UserID, object Title, object CoverImageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_AlbumID",
                        (AlbumID == null || AlbumID.ToString() == "0") ? null : AlbumID));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.Int32,
                        "i_UserID",
                        (UserID == null || UserID.ToString() == "0") ? null : UserID));
                sc.Parameters.Add(
                    sc.CreateParameter(
                        DbType.String,
                        "i_Title",
                        (Title == null || Title.ToString() == string.Empty) ? null : Title));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CoverImageID", CoverImageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("album_save", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// Deletes an attachment by its id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="attachmentID">
        /// The attachment id.
        /// </param>
        public static void attachment_delete(int? mid, object attachmentID)
        {
            bool UseFileTable = GetBooleanRegistryValue(mid, "UseFileTable");

            // If the files are actually saved in the Hard Drive
            if (!UseFileTable)
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", null));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AttachmentID", attachmentID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", null));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", 0));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", 1000));

                    sc.CommandText.AppendObjectQuery("attachment_list", mid);
                    var tbAttachments = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true);

                    var uploadDir =
                        HostingEnvironmentUtil.MapPath(
                            string.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.TopicAttachments));

                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = string.Format(
                                "{0}/{1}.{2}.yafupload",
                                uploadDir,
                                row["MessageID"],
                                row["FileName"]);
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            // error deleting that file.. 
                            eventlog_create(mid, null, "attachment_delete", ex, EventLogTypes.Error);
                        }
                    }
                }
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AttachmentID", attachmentID));

                sc.CommandText.AppendObjectQuery("attachment_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Download an attachment with an Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="attachmentId">
        /// The attachmentId
        /// </param>
        public static void attachment_download(int? mid, object attachmentId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AttachmentID", attachmentId));

                sc.CommandText.AppendObjectQuery("attachment_download", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Returnes a paged list of attachments to a message 
        /// or a list of all attachments, depending on arguments. 
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="messageID">
        /// The message ID.
        /// </param>
        /// <param name="attachmentID">
        /// The attachment ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="pageIndex">
        /// The page index. 0-based.
        /// </param>
        /// <param name="pageSize">
        /// The page size. 0-based
        /// .</param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with an attachments list.
        /// </returns>
        public static DataTable attachment_list(
            int? mid,
            object messageID,
            object attachmentID,
            object boardId,
            object pageIndex,
            object pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                // string dd = sc.DataSource.WrapObjectName("attachment_list");
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AttachmentID", attachmentID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("attachment_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Saves an attachement.
        /// </summary>
        /// <param name="mid">
        /// The module ID
        /// .</param>
        /// <param name="messageID">
        /// The message ID.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="bytes">
        /// The number of bytes.
        /// </param>
        /// <param name="contentType">
        /// The type of content.
        /// </param>
        /// <param name="stream">
        /// The bytes stream for the attachment.
        /// </param>
        public static void attachment_save(
            int? mid,
            object messageID,
            object fileName,
            object bytes,
            object contentType,
            Stream stream)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                byte[] fileData = null;
                if (stream != null)
                {
                    fileData = new byte[stream.Length];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(fileData, 0, (int)stream.Length);
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_FileName", fileName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Bytes", bytes));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ContentType", contentType));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "i_FileData", fileData));

                sc.CommandText.AppendObjectQuery("attachment_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Deletes a banned ip with an ID.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="ID">
        /// The banned IP ID
        /// </param>
        public static void bannedip_delete(int? mid, object ID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", ID));

                sc.CommandText.AppendObjectQuery("bannedip_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The list of banned Ips.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="ID">
        /// The banned IP ID.
        /// </param>
        /// <param name="pageIndex">
        /// The 0-based page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with a banned ip list.
        /// </returns>
        public static DataTable bannedip_list(
            int? mid,
            object boardId,
            object ID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", ID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("bannedip_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The bannedip_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="Mask">
        /// The mask.
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param> 
        public static void bannedip_save(int? mid, object ID, object boardId, object Mask, string reason, int userID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", ID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Mask", Mask));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Reason", reason));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("bannedip_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The bbcode_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bbcodeId">
        /// The bbcode id.
        /// </param> 
        public static void bbcode_delete(int? mid, object bbcodeId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BBCodeID", bbcodeId));

                sc.CommandText.AppendObjectQuery("bbcode_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The bbcode_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="bbcodeID">
        /// The bbcode id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/> with list of bbcodes.
        /// </returns>
        public static DataTable bbcode_list(int? mid, object boardId, object bbcodeID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BBCodeID", bbcodeID));

                sc.CommandText.AppendObjectQuery("bbcode_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The bb code list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="bbcodeID">
        /// The bbcode id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/> of BBCode list.
        /// </returns>
        public static IEnumerable<TypedBBCode> BBCodeList(int? mid, int boardId, int? bbcodeID)
        {
            return bbcode_list(mid, boardId, bbcodeID).AsEnumerable().Select(o => new TypedBBCode(o));
        }

        /// <summary>
        /// The bbcode_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bbcodeId">
        /// The bbcode id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="onclickjs">
        /// The onclickjs.
        /// </param>
        /// <param name="displayjs">
        /// The displayjs.
        /// </param>
        /// <param name="editjs">
        /// The editjs.
        /// </param>
        /// <param name="displaycss">
        /// The displaycss.
        /// </param>
        /// <param name="searchregex">
        /// The searchregex.
        /// </param>
        /// <param name="replaceregex">
        /// The replaceregex.
        /// </param>
        /// <param name="variables">
        /// The variables.
        /// </param>
        /// <param name="usemodule">
        /// The usemodule.
        /// </param>
        /// <param name="moduleclass">
        /// The moduleclass.
        /// </param>
        /// <param name="execorder">
        /// The execorder.
        /// </param>      
        public static void bbcode_save(
            int? mid,
            object bbcodeId,
            object boardId,
            object name,
            object description,
            object onclickjs,
            object displayjs,
            object editjs,
            object displaycss,
            object searchregex,
            object replaceregex,
            object variables,
            object usemodule,
            object moduleclass,
            object execorder)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BBCodeID", bbcodeId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_OnClickJS", onclickjs));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DisplayJS", displayjs));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_EditJS", editjs));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DisplayCSS", displaycss));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SearchRegEx", searchregex));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ReplaceRegEx", replaceregex));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Variables", variables));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseModule", usemodule));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ModuleClass", moduleclass));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExecOrder", execorder ?? 1));

                sc.CommandText.AppendObjectQuery("bbcode_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The board_create.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="adminUsername">
        /// The admin username.
        /// </param>
        /// <param name="adminUserEmail">
        /// The admin user email.
        /// </param>
        /// <param name="adminUserKey">
        /// The admin user key.
        /// </param>
        /// <param name="boardName">
        /// The board name.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="languageFile">
        /// The language file.
        /// </param>
        /// <param name="boardMembershipName">
        /// The board membership name.
        /// </param>
        /// <param name="boardRolesName">
        /// The board roles name.
        /// </param>
        /// <param name="rolePrefix">
        /// The role prefix.
        /// </param>
        /// <param name="isHostUser">
        /// The is host user.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> id of a new board.
        /// </returns>
        public static int board_create(
            int? mid,
            object adminUsername,
            object adminUserEmail,
            object adminUserKey,
            object boardName,
            object culture,
            object languageFile,
            object boardMembershipName,
            object boardRolesName,
            object rolePrefix,
            object isHostUser)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BoardName", boardName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Culture", culture));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_LanguageFile", languageFile));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MembershipAppName", boardMembershipName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RolesAppName", boardRolesName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", adminUsername));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserEmail", adminUserEmail));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", adminUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsHostAdmin", isHostUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RolePrefix", rolePrefix));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("board_create", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The board_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void board_delete(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("board_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The board_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/> of the board list.
        /// </returns>
        public static DataTable board_list(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("board_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The board_poststats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="showNoCountPosts">
        /// The show no count posts.
        /// </param>
        /// <returns>
        /// The <see cref="board_poststats_Result"/> with post statistics for a board.
        /// </returns>
        public static board_poststats_Result board_poststats(
            int? mid,
            int? boardId,
            bool useStyledNicks,
            bool showNoCountPosts)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowNoCountPosts", showNoCountPosts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_GetDefaults", false));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                sc.CommandText.AppendObjectQuery("board_poststats", mid);
                var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0].Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0];
                }
            }

            // TODO:  get defaults - rewrite it on sql level to not call twice
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowNoCountPosts", showNoCountPosts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_GetDefaults", true));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                sc.CommandText.AppendObjectQuery("board_poststats", mid);
                DataTable dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0].Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0];
                }

                return null;
            }
        }

        /// <summary>
        /// Recalculates topic and post numbers and updates last post for all forums in all boards
        /// </summary>
        /// <param name="mid">
        /// The module Id.
        /// </param>
        public static void board_resync(int? mid)
        {
            board_resync(mid, null);
        }

        /// <summary>
        /// The board_resync.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void board_resync(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("board_resync", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The board_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="languageFile">
        /// The language file.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="allowThreaded">
        /// The allow threaded.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> id of a saved board.
        /// </returns>
        public static int board_save(
            int? mid,
            object boardId,
            object languageFile,
            object culture,
            object name,
            object allowThreaded)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_LanguageFile", languageFile));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Culture", culture));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AllowThreaded", allowThreaded));

                sc.CommandText.AppendObjectQuery("board_save", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The board_stats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="board_stats_Result"/> class with common board statistics.
        /// </returns>
        public static board_stats_Result board_stats(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("board_stats", mid);
                DataTable dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0].Table.AsEnumerable().Select(r => new board_stats_Result(r)).ToList()[0];
                }

                return null;
            }
        }

        /// <summary>
        /// The board_userstats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/> with board user statistics.
        /// </returns>
        public static DataRow board_userstats(int? mid, int? boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", true));

                sc.CommandText.AppendObjectQuery("board_userstats", mid);
                using (
                    var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
                {
                    return dt.Rows[0];
                }
            }
        }

        /// <summary>
        /// The buddy_addrequest.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// The <see cref="{string[]}"/> of buddies.
        /// </returns>
        public static string[] buddy_addrequest(int? mid, object FromUserID, object ToUserID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", FromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", ToUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", true));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("buddy_addrequest", mid);
                using (
                    var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true))
                {
                    return new[] { dt.Rows[0]["i_paramOutput"].ToString(), dt.Rows[0]["i_approved"].ToString() };
                }
            }
        }

        /// <summary>
        /// The buddy_approve request.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <param name="Mutual">
        /// The mutual.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string buddy_approveRequest(int? mid, object FromUserID, object ToUserID, object Mutual)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", FromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", ToUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Mutual", Mutual));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", true));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("buddy_approverequest", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The buddy_deny request.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string buddy_denyRequest(int? mid, object FromUserID, object ToUserID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", FromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", ToUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", true));

                sc.CommandText.AppendObjectQuery("buddy_denyrequest", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The buddy_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable buddy_list(int? mid, object FromUserID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", FromUserID));

                sc.CommandText.AppendObjectQuery("buddy_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The buddy_remove.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string buddy_remove(int? mid, object FromUserID, object ToUserID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", FromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", ToUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", true));

                sc.CommandText.AppendObjectQuery("buddy_remove", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The category_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="CategoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The<see cref="bool"/>.
        /// </returns>
        public static bool category_delete(int? mid, object CategoryID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", CategoryID));

                sc.CommandText.AppendObjectQuery("category_delete", mid);
                var res = Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure)) != 0;
                if (Config.LargeForumTree)
                {
                    forum_ns_recreate(mid);
                }

                return res;
            }
        }

        /// <summary>
        /// The category_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable category_list(int? mid, object boardId, object categoryID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryID));
                sc.CommandText.AppendObjectQuery("category_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }

        }

        public static DataTable category_pfaccesslist(int? mid, object boardId, object categoryID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryID));
                sc.CommandText.AppendObjectQuery("category_pfaccesslist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The category_getadjacentforum.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isAfter">
        /// The is after.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable category_getadjacentforum(
            int? mid,
            object boardId,
            object categoryID,
            object userId,
            bool isAfter)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAfter", isAfter));

                sc.CommandText.AppendObjectQuery("category_getadjacentforum", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The category_listread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable category_listread(int? mid, object boardId, object userId, object categoryID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryID));

                sc.CommandText.AppendObjectQuery("category_listread", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The category_simplelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="startID">
        /// The start id.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable category_simplelist(int? mid, int startID, int limit)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (startID <= 0)
                {
                    startID = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_StartID", startID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", limit));

                sc.CommandText.AppendObjectQuery("category_simplelist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The category_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="categoryImage">
        /// The category image.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="canHavePersForums">
        /// The can Have Pers Forums.
        /// </param>        
        public static void category_save(
            int? mid,
            object boardId,
            object categoryId,
            object name,
            object categoryImage,
            object sortOrder,
            object canHavePersForums)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_CategoryImage", categoryImage));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_CanHavePersForums", canHavePersForums));

                sc.CommandText.AppendObjectQuery("category_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The checkemail_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable checkemail_list(int? mid, object email)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));

                sc.CommandText.AppendObjectQuery("checkemail_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The checkemail_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="hash">
        /// The hash.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        public static void checkemail_save(int? mid, object userId, object hash, object email)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Hash", hash));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("checkemail_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The checkemail_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="hash">
        /// The hash.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable checkemail_update(int? mid, object hash)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Hash", hash));

                sc.CommandText.AppendObjectQuery("checkemail_update", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The choice_add.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollID">
        /// The poll id.
        /// </param>
        /// <param name="choice">
        /// The choice.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="mime">
        /// The mime.
        /// </param>
        public static void choice_add(int? mid, object pollID, object choice, object path, object mime)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollID", pollID));

                sc.Parameters.Add(
                    choice != null
                        ? sc.CreateParameter(DbType.String, "i_Choice", choice)
                        : sc.CreateParameter(DbType.String, "i_Choice", "No input value supplied"));

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ObjectPath", path));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MimeType", mime));

                sc.CommandText.AppendObjectQuery("choice_add", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The choice_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="choiceID">
        /// The choice id.
        /// </param>
        public static void choice_delete(int? mid, object choiceID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ChoiceID", choiceID));

                sc.CommandText.AppendObjectQuery("choice_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The choice_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="choiceID">
        /// The choice id.
        /// </param>
        /// <param name="choice">
        /// The choice.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="mime">
        /// The mime.
        /// </param>        
        public static void choice_update(int? mid, object choiceID, object choice, object path, object mime)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ChoiceID", choiceID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Choice", choice));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ObjectPath", path));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_MimeType", mime));

                sc.CommandText.AppendObjectQuery("choice_update", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The choice_vote.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="choiceID">
        /// The choice id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="remoteIP">
        /// The remote ip.
        /// </param>
        public static void choice_vote(int? mid, object choiceID, object userId, object remoteIP)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ChoiceID", choiceID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RemoteIP", remoteIP));

                sc.CommandText.AppendObjectQuery("choice_vote", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The db_getstats_new.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_getstats_new(int? mid)
        {
            string sql;
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            string message;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_getstats(out message);
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_getstats(out message);
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_getstats(out message);
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_getstats(out message);
                    break;
                    // case SqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_getstats_new(connectionString); break;
                    // case SqlDbAccess.Db2: return VZF.Data.Db2.Db.db_getstats_new(connectionString); break;
                    // case SqlDbAccess.Other: return VZF.Data.Other.Db.db_getstats_new(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {

                    sc.CommandText.AppendQuery(sql);
                    sc.ExecuteNonQuery(CommandType.Text, false);
                    return "Reindexing complited.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "\r\n" + ex.StackTrace;
            }
        }

        /// <summary>
        /// The db_getstats_warning.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_getstats_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_getstats_warning();
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_getstats_warning();
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_getstats_warning();
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_getstats_warning();
                    // case SqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_getstats_warning(); break;
                    // case SqlDbAccess.Db2: return db2_db_getstats_warning(); break;
                    // case SqlDbAccess.Other: return VZF.Data.Other.Db.db_getstats_warning(); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_warning(int? mid)
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_recovery_mode_new.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="dbRecoveryMode">
        /// The db recovery mode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_recovery_mode_new(int? mid, string dbRecoveryMode)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string result = "Disabled";

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case SqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case SqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case SqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            return result;
        }

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_reindex_new(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;
            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_reindex_new();
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_reindex_new();
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_reindex_new();
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_reindex_new();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return "Operation is not supported";
            }

            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.CommandText.AppendQuery(sql);

                    sc.ExecuteNonQuery(CommandType.Text, false);
                    return "Database is reindexed succesfully.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "\r\n" + ex.StackTrace;
            }
        }

        /// <summary>
        /// The db_reindex_warning.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_reindex_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_reindex_warning();
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_reindex_warning();
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_reindex_warning();
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_reindex_warning();
                    // case SqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.db_reindex_warning();
                    // case SqlDbAccess.Db2:  return VZF.Data.Db2.Db.db_reindex_warning();
                    // case SqlDbAccess.Other:  return VZF.Data.Other.Db.db_reindex_warning(); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The db_runsql_new.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="useTransaction">
        /// The use transaction.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_runsql_new(int? mid, string sql, bool useTransaction)
        {
            try
            {
                sql = sql.Trim().Replace("{databaseOwner}", Config.DatabaseOwner);
                sql = sql.Trim().Replace("{databaseName}", Config.DatabaseSchemaName);
                sql = sql.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);

                using (var command = new VzfSqlCommand(mid))
                {
                    command.CommandText.AppendQuery(sql);
                    return InnerRunSqlExecuteReader(command, useTransaction);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// The db_shrink_new.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_shrink_new(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string result;
            string message = null;
            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    result = VZF.Data.MsSql.Db.db_shrink_new(out message).FormatWith("databaseNameHere");
                    break;
                case SqlDbAccess.Npgsql:
                    result = VZF.Data.Postgre.Db.db_shrink_new(out message);
                    break;
                case SqlDbAccess.MySql:
                    result = string.Empty;
                    break;
                case SqlDbAccess.Firebird:
                    result = string.Empty;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!result.IsSet())
            {
                return "The operation is not supported";
            }

            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.CommandText.AppendQuery(result);

                    sc.ExecuteNonQuery(CommandType.Text, false);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "\r\n" + ex.StackTrace;
            }
        }

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_shrink_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_shrink_warning();
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_shrink_warning();
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_shrink_warning();
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_shrink_warning();
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The ds_forumadmin.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="isUserForum">
        /// The is user forum.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public static DataSet ds_forumadmin(int? mid, object boardId, object pageUserID, object isUserForum)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                using (var ds = new DataSet())
                {
                    // var conn = sc.GetConnection(); 
                    //  sc.BeginTransaction();
                    //   using (var trans = sc.Transaction)
                    //  {                       
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", DBNull.Value));

                    sc.CommandText.AppendObjectQuery("category_list", mid);
                    var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                    dt.TableName = SqlDbAccess.GetVzfObjectName("Category", mid);
                    ds.Tables.Add(dt);

                    sc.Parameters.Clear();

                    sc.CommandText.Clear();

                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", DBNull.Value));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", pageUserID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserForum", isUserForum));

                    sc.CommandText.AppendObjectQuery("forum_list", mid);
                    dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
                    dt.TableName = SqlDbAccess.GetVzfObjectName("ForumUnsorted", mid);
                    ds.Tables.Add(dt);

                    // TODO: missing transaction
                    // if (trans != null)
                    // trans.Commit();  

                    // }

                    using (var dtForumListSorted = ds.Tables[SqlDbAccess.GetVzfObjectName("ForumUnsorted", mid)].Clone())
                    {
                        dtForumListSorted.TableName = SqlDbAccess.GetVzfObjectName("Forum", mid);
                        ds.Tables.Add(dtForumListSorted);
                    }

                    forum_list_sort_basic(
                        ds.Tables[SqlDbAccess.GetVzfObjectName("ForumUnsorted", mid)],
                        ds.Tables[SqlDbAccess.GetVzfObjectName("Forum", mid)],
                        0,
                        0);
                    ds.Tables.Remove(SqlDbAccess.GetVzfObjectName("ForumUnsorted", mid));
                    ds.Relations.Add(
                        "FK_Forum_Category",
                        ds.Tables[SqlDbAccess.GetVzfObjectName("Category", mid)].Columns["CategoryID"],
                        ds.Tables[SqlDbAccess.GetVzfObjectName("Forum", mid)].Columns["CategoryID"]);

                    return ds;
                }
            }
        }

        /// <summary>
        /// The forum_list_sort_basic.
        /// </summary>
        /// <param name="listsource">
        /// The listsource.
        /// </param>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="parentid">
        /// The parentid.
        /// </param>
        /// <param name="currentLevel">
        /// The current lvl.
        /// </param>
        public static void forum_list_sort_basic(DataTable listsource, DataTable list, int parentid, int currentLevel)
        {
            for (var i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if (row["ParentID"] == DBNull.Value)
                {
                    row["ParentID"] = 0;
                }

                if ((int)row["ParentID"] != parentid)
                {
                    continue;
                }

                string indentString = string.Empty;
                int indentInt = Convert.ToInt32(currentLevel);

                for (var j = 0; j < indentInt; j++)
                {
                    indentString += "--";
                }

                row["Name"] = string.Format(" -{0} {1}", indentString, row["Name"]);
                list.Rows.Add(row.ItemArray);
                forum_list_sort_basic(listsource, list, (int)row["ForumID"], currentLevel + 1);
            }
        }

        /// <summary>
        /// The eventlog_create.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        public static void eventlog_create(int? mid, object userId, object source, object description)
        {
            eventlog_create(mid, userId, source, description, EventLogTypes.Debug);
        }

        /// <summary>
        /// The eventlog_create.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public static void eventlog_create(
            int? mid,
            object userId,
            object source,
            object description,
            EventLogTypes type)
        {
            try
            {
                source = source.GetType().ToString();
            }
            catch
            {
            }

            if (description is Exception)
            {
                var ex = (Exception)description;
                description = ex.Message + "\r\n" + ex.Source + "/r/n/" + ex.StackTrace;
            }

            // TODO: Implement the object existance as it throws errors during install
            if (GetIsForumInstalled(mid))
            {
#if !DEBUG
                try{
#endif

                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Source", source));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Type", type.ToInt()));
                    sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                    sc.CommandText.AppendObjectQuery("eventlog_create", mid);
                    sc.ExecuteNonQuery(CommandType.StoredProcedure);
                }
#if !DEBUG
                }
                catch(Exception e)
                {
                  throw;
                }
#endif
            }
        }

        /// <summary>
        /// Deletes event log entry of given ID.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="eventLogID">ID of event log entry.</param>
        /// <param name="pageUserId"> </param>
        public static void eventlog_delete(int? mid, object eventLogID, object pageUserId)
        {
            eventlog_delete(mid, eventLogID, null, pageUserId);
        }

        /// <summary>
        /// The eventlog_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="eventLogID">
        /// The event log id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>  
        private static void eventlog_delete(int? mid, object eventLogID, object boardId, object pageUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_EventLogID", eventLogID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));

                sc.CommandText.AppendObjectQuery("eventlog_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The eventlog_deletebyuser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>  
        public static void eventlog_deletebyuser(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", userId));

                sc.CommandText.AppendObjectQuery("eventlog_deletebyuser", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The eventlog_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="maxRows">
        /// The max rows.
        /// </param>
        /// <param name="maxDays">
        /// The max days.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="eventIDs">
        /// The event i ds.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable eventlog_list(
            int? mid,
            object boardId,
            [NotNull] object pageUserId,
            [NotNull] object maxRows,
            [NotNull] object maxDays,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object eventIDs)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MaxRows", maxRows));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MaxDays", maxDays));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_EventIDs", eventIDs));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("eventlog_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The eventloggroupaccess_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event type id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>       
        public static DataTable eventloggroupaccess_list(
            int? mid,
            [NotNull] object groupID,
            [NotNull] object eventTypeId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_EventTypeID", eventTypeId));

                sc.CommandText.AppendObjectQuery("eventloggroupaccess_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_eventlogaccesslist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>      
        public static DataTable group_eventlogaccesslist(int? mid, [NotNull] object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("group_eventlogaccesslist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The eventloggroupaccess_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event type id.
        /// </param>
        /// <param name="eventTypeName">
        /// The event type name.
        /// </param>
        /// <param name="deleteAccess">
        /// The delete access.
        /// </param>     
        public static void eventloggroupaccess_save(
            int? mid,
            [NotNull] object groupId,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName,
            [NotNull] object deleteAccess)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_EventTypeID", eventTypeId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_EventTypeName", eventTypeName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeleteAccess", deleteAccess));

                sc.CommandText.AppendObjectQuery("eventloggroupaccess_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The eventloggroupaccess_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event type id.
        /// </param>
        /// <param name="eventTypeName">
        /// The event type name.
        /// </param>
        public static void eventloggroupaccess_delete(
            int? mid,
            [NotNull] object groupId,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_EventTypeID", eventTypeId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_EventTypeName", eventTypeName));

                sc.CommandText.AppendObjectQuery("eventloggroupaccess_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The extension_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="extensionId">
        /// The extension id.
        /// </param>     
        public static void extension_delete(int? mid, object extensionId)
        {
            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExtensionID", extensionId));

                    sc.CommandText.AppendObjectQuery("extension_delete", mid);
                    sc.ExecuteNonQuery(CommandType.StoredProcedure);
                }

            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        /// <summary>
        /// The extension_edit.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="extensionId">
        /// The extension id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable extension_edit(int? mid, object extensionId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExtensionID", extensionId));

                sc.CommandText.AppendObjectQuery("extension_edit", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The extension_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable extension_list(int? mid, object boardId)
        {
            return extension_list(mid, boardId, null);
        }

        /// <summary>
        /// The extension_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable extension_list(int? mid, object boardId, object extension)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Extension", extension));

                sc.CommandText.AppendObjectQuery("extension_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The extension_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="extensionId">
        /// The extension id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        public static void extension_save(int? mid, object extensionId, object boardId, object extension)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExtensionID", extensionId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Extension", extension));

                sc.CommandText.AppendObjectQuery("extension_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The forum_byuserlist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserForum">
        /// The is user forum.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_byuserlist(
            int? mid,
            object boardId,
            object forumId,
            object userId,
            object isUserForum)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserForum", isUserForum));

                sc.CommandText.AppendObjectQuery("forum_byuserlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_categoryaccess_activeuser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable forum_categoryaccess_activeuser(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("forum_categoryaccess_activeuser", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool forum_delete(int? mid, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("forum_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                if (Config.LargeForumTree)
                {
                    forum_ns_recreate(mid);
                }

                return true;
            }
        }

        /// <summary>
        /// The forum_move.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumOldID">
        /// The forum old id.
        /// </param>
        /// <param name="forumNewID">
        /// The forum new id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>        
        public static bool forum_move(int? mid, [NotNull] object forumOldID, [NotNull] object forumNewID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumOldID));

                sc.CommandText.AppendObjectQuery("forum_listSubForums", mid);

                // if a forum already exists.
                if (!(sc.ExecuteScalar(CommandType.StoredProcedure) is DBNull))
                {
                    return false;
                }

                sc.Parameters.Clear();
                sc.CommandText.Clear();

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumOldID", forumOldID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumNewID", forumNewID));
                sc.CommandText.AppendObjectQuery("forum_move", mid);

                // TODO: command timeout should be very large here
                sc.ExecuteNonQuery(CommandType.StoredProcedure);

                if (Config.LargeForumTree)
                {
                    forum_ns_recreate(mid);
                }

                return true;
            }
        }

        /// <summary>
        /// The forum_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>      
        public static DataTable forum_list(int? mid, object boardId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", null));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserForum", false));

                sc.CommandText.AppendObjectQuery("forum_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Listes all forums accessible to a user
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">BoardID</param>
        /// <param name="userId">ID of user</param>
        /// <returns>DataTable of all accessible forums</returns>
        public static DataTable forum_listall(int? mid, object boardId, object userId)
        {
            return forum_listall(mid, boardId, userId, 0, false);
        }

        /// <summary>
        /// The forum_listall.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="startAt">
        /// The start at.
        /// </param>
        /// <param name="returnAll">
        /// The return all.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable forum_listall(int? mid, object boardId, object userId, object startAt, bool returnAll)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (startAt == null)
                {
                    startAt = 0;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Root", startAt));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ReturnAll", returnAll));

                sc.CommandText.AppendObjectQuery("forum_listall", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listall_from cat.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="allowUserForumsOnly">
        /// The allow user forums only.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_fromCat(
            int? mid,
            object boardId,
            object categoryID,
            bool allowUserForumsOnly)
        {
            return forum_listall_fromCat(mid, boardId, categoryID, true, allowUserForumsOnly);
        }

        /// <summary>
        /// The forum_listall_from cat.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="emptyFirstRow">
        /// The empty first row.
        /// </param>
        /// <param name="allowUserForumsOnly">
        /// The allow user forums only.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable forum_listall_fromCat(
            int? mid,
            object boardId,
            object categoryID,
            bool emptyFirstRow,
            bool allowUserForumsOnly)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(
                    sc.CreateParameter(DbType.Int32, "i_CategoryID", Convert.ToInt32(categoryID.ToString())));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AllowUserForumsOnly", allowUserForumsOnly));

                sc.CommandText.AppendObjectQuery("forum_listall_fromCat", mid);

                using (
                    DataTable dt = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true))
                {
                    return forum_sort_list(
                        mid,
                        dt,
                        0,
                        Convert.ToInt32(categoryID.ToString()),
                        0,
                        null,
                        emptyFirstRow,
                        true);
                }
            }
        }

        /// <summary>
        /// The forum_sort_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="listSource">
        /// The list source.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="startingIndent">
        /// The starting indent.
        /// </param>
        /// <param name="forumidExclusions">
        /// The forumid exclusions.
        /// </param>
        /// <param name="emptyFirstRow">
        /// The empty first row.
        /// </param>
        /// <param name="returnAll">
        /// The return all.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_sort_list(
            int? mid,
            DataTable listSource,
            int parentId,
            int categoryId,
            int startingIndent,
            int[] forumidExclusions,
            bool emptyFirstRow,
            bool returnAll)
        {
            var listDestination = new DataTable { TableName = "forum_sort_list" };
            listDestination.Columns.Add("ForumID", typeof(string));
            listDestination.Columns.Add("ParentID", typeof(string));
            listDestination.Columns.Add("Title", typeof(string));
            listDestination.Columns.Add("Level", typeof(int));         
            listDestination.Columns.Add("CanHavePersForums", typeof(bool));

            if (emptyFirstRow)
            {
                DataRow blankRow = listDestination.NewRow();
                blankRow["ForumID"] = string.Empty;
                blankRow["ParentID"] = string.Empty;
                blankRow["Title"] = string.Empty;
                blankRow["Level"] = -1;            
                blankRow["CanHavePersForums"] = false;

                listDestination.Rows.Add(blankRow);
            }

            // filter the forum list -- not sure if this code actually works
            DataView dv = listSource.DefaultView;

            if (forumidExclusions != null && forumidExclusions.Length > 0)
            {
                string strExclusions = string.Empty;
                bool bFirst = true;

                foreach (int forumId in forumidExclusions)
                {
                    if (bFirst)
                    {
                        bFirst = false;
                    }
                    else
                    {
                        strExclusions += ",";
                    }

                    strExclusions += forumId.ToString(CultureInfo.InvariantCulture);
                }

                dv.RowFilter = string.Format("ForumID NOT IN ({0})", strExclusions);
                dv.ApplyDefaultSort = true;
            }

            forum_sort_list_recursive(
                mid,
                dv.ToTable(),
                listDestination,
                parentId,
                categoryId,
                startingIndent,
                returnAll);

            return listDestination;
        }

        /// <summary>
        /// The forum_listall_sorted.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumidExclusions">
        /// The forumid exclusions.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId, int[] forumidExclusions)
        {
            return forum_listall_sorted(mid, boardId, userId, null, false, new [] { 0 }, false);
        }

        /// <summary>
        /// The forum_listall_sorted.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId)
        {
            return !Config.LargeForumTree
                       ? forum_listall_sorted(mid, boardId, userId, null, false, new [] { 0 }, false)
                       : forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
        }

        /// <summary>
        /// The forum_listall_sorted_all.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="returnAll">
        /// The return all.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted_all(int? mid, object boardId, object userId, bool returnAll)
        {
            return !Config.LargeForumTree
                       ? forum_listall_sorted(mid, boardId, userId, null, false, new [] { 0 }, returnAll)
                       : forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
        }

        /// <summary>
        /// The forum_ns_getchildren_activeuser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardid">
        /// The boardid.
        /// </param>
        /// <param name="categoryid">
        /// The categoryid.
        /// </param>
        /// <param name="forumid">
        /// The forumid.
        /// </param>
        /// <param name="userid">
        /// The userid.
        /// </param>
        /// <param name="notincluded">
        /// The notincluded.
        /// </param>
        /// <param name="immediateonly">
        /// The immediateonly.
        /// </param>
        /// <param name="indentchars">
        /// The indentchars.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_ns_getchildren_activeuser(
            int? mid,
            int? boardid,
            int? categoryid,
            int? forumid,
            int userid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var sc = new VzfSqlCommand(mid))
            {

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userid));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_NotIncluded", notincluded));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ImmediateOnly", immediateonly));

                sc.CommandText.AppendObjectQuery("forum_ns_getchildren_activeuser", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Recreate tree.
        /// </summary>
        public static void forum_ns_recreate([NotNull] int? mid)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendObjectQuery("forum_ns_recreate", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }

        }

        /// <summary>
        /// The forum_ns_getchildren.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardid">
        /// The boardid.
        /// </param>
        /// <param name="categoryid">
        /// The categoryid.
        /// </param>
        /// <param name="forumid">
        /// The forumid.
        /// </param>
        /// <param name="notincluded">
        /// The notincluded.
        /// </param>
        /// <param name="immediateonly">
        /// The immediateonly.
        /// </param>
        /// <param name="indentchars">
        /// The indentchars.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>      
        public static DataTable forum_ns_getchildren(
            int? mid,
            int? boardid,
            int? categoryid,
            int? forumid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumid ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_NotIncluded", notincluded));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ImmediateOnly", immediateonly));

                sc.CommandText.AppendObjectQuery("forum_ns_getchildren", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listall_sorted.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumidExclusions">
        /// The forumid exclusions.
        /// </param>
        /// <param name="emptyFirstRow">
        /// The empty first row.
        /// </param>
        /// <param name="startAt">
        /// The start at.
        /// </param>
        /// <param name="returnAll">
        /// The returnAll.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable forum_listall_sorted(
            int? mid,
            object boardId,
            object userId,
            int[] forumidExclusions,
            bool emptyFirstRow,
            int[] startAt,
            bool returnAll)
        {
            var startAtInt = 0;
            if (startAt.Any())
            {
                startAtInt = startAt.First();
            }

            using (DataTable dataTable = forum_listall(mid, boardId, userId, startAtInt, returnAll))
            {
                int baseForumId = 0;
                int baseCategoryId = 0;

                if (startAt.Any())
                {
                    // find the base ids...
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (Convert.ToInt32(dataRow["ForumID"]) == startAt.First())
                        {
                            baseForumId = Convert.ToInt32(dataRow["ParentID"]);
                            baseCategoryId = Convert.ToInt32(dataRow["CategoryID"]);
                            break;
                        }
                    }
                }

                return forum_sort_list(
                    mid,
                    dataTable,
                    baseForumId,
                    baseCategoryId,
                    0,
                    forumidExclusions,
                    emptyFirstRow,
                    returnAll);
            }
        }

        /// <summary>
        /// The forum_list_sort_basic.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="listsource">
        /// The listsource.
        /// </param>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <param name="parentid">
        /// The parentid.
        /// </param>
        /// <param name="currentLvl">
        /// The current lvl.
        /// </param>
        public static void forum_list_sort_basic(
            int? mid,
            DataTable listsource,
            DataTable list,
            int parentid,
            int currentLvl)
        {
            for (var i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value)
                {
                    row["ParentID"] = 0;
                }

                if ((int)row["ParentID"] != parentid)
                {
                    continue;
                }

                string sIndent = string.Empty;
                int iIndent = Convert.ToInt32(currentLvl);

                for (var j = 0; j < iIndent; j++)
                {
                    sIndent += "--";
                }

                row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                list.Rows.Add(row.ItemArray);
                forum_list_sort_basic(mid, listsource, list, (int)row["ForumID"], currentLvl + 1);
            }
        }

        /// <summary>
        /// The forum_sort_list_recursive.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="listSource">
        /// The list source.
        /// </param>
        /// <param name="listDestination">
        /// The list destination.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="currentIndent">
        /// The current indent.
        /// </param>
        /// <param name="returnAll">
        /// The return all.
        /// </param>
        public static void forum_sort_list_recursive(
            int? mid,
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int categoryID,
            int currentIndent,
            bool returnAll)
        {
            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value)
                {
                    row["ParentID"] = 0;
                }

                if ((int)row["ParentID"] != parentID)
                {
                    continue;
                }

                DataRow newRow;
                if ((int)row["CategoryID"] != categoryID)
                {
                    categoryID = (int)row["CategoryID"];

                    newRow = listDestination.NewRow();
                    newRow["ForumID"] = -categoryID; // Ederon : 9/4/2007
                    newRow["Title"] = string.Format("{0}", row["Category"]);                  
                    newRow["CanHavePersForums"] = row["CanHavePersForums"];
                    listDestination.Rows.Add(newRow);
                }

                string sIndent = string.Empty;

                for (int j = 0; j < currentIndent; j++)
                {
                    sIndent += "--";
                }

                // import the row into the destination
                newRow = listDestination.NewRow();

                newRow["ForumID"] = row["ForumID"];
                newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["Forum"]);              
                newRow["CanHavePersForums"] = row["CanHavePersForums"];

                listDestination.Rows.Add(newRow);

                // recurse through the list...
                forum_sort_list_recursive(
                    mid,
                    listSource,
                    listDestination,
                    (int)row["ForumID"],
                    categoryID,
                    currentIndent + 1,
                    returnAll);
            }
        }

        /// <summary>
        /// The forum_tags.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="searchText">
        /// The search text.
        /// </param>
        /// <param name="beginsWith">
        /// The begins with.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_tags(
            int? mid,
            int boardId,
            int pageUserId,
            int? forumId,
            int pageIndex,
            int pageSize,
            string searchText,
            bool beginsWith)
        {
            if (searchText.Equals(char.MinValue.ToString(CultureInfo.InvariantCulture)) || searchText.IsNotSet())
            {
                searchText = null;
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SearchText", searchText));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_BeginsWith", beginsWith));

                sc.CommandText.AppendObjectQuery("forum_tags", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listall my moderated.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listallMyModerated(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("forum_listallmymoderated", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listpath.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listpath(int? mid, object forumID)
        {
            if (!Config.LargeForumTree)
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                    sc.CommandText.AppendObjectQuery("forum_listpath", mid);
                    return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                }
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("forum_ns_listpath", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="showCommonForums">
        /// The show common forums.
        /// </param>
        /// <param name="showPersonalForums">
        /// The show personal forums.
        /// </param>
        /// <param name="forumCreatedByUserId">
        /// The forum created by user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable forum_listread(
            int? mid,
            object boardId,
            object userId,
            object categoryId,
            object parentId,
            object useStyledNicks,
            bool findLastRead,
            [NotNull] bool showCommonForums,
            [NotNull] bool showPersonalForums,
            [CanBeNull] int? forumCreatedByUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ParentID", parentId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowCommonForums", showCommonForums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowPersonalForums", showPersonalForums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumCreatedByUserId", forumCreatedByUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery(!Config.LargeForumTree ? "forum_listread" : "forum_ns_listread", mid);

                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_listreadpersonal.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="showCommonForums">
        /// The show common forums.
        /// </param>
        /// <param name="showPersonalForums">
        /// The show personal forums.
        /// </param>
        /// <param name="forumCreatedByUserId">
        /// The forum created by user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_listreadpersonal(
            int? mid,
            object boardId,
            object userId,
            object categoryId,
            object parentId,
            object useStyledNicks,
            bool findLastRead,
            [NotNull] bool showCommonForums,
            [NotNull] bool showPersonalForums,
            [CanBeNull] int? forumCreatedByUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ParentID", parentId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowCommonForums", showCommonForums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowPersonalForums", showPersonalForums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumCreatedByUserId", forumCreatedByUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery(
                    !Config.LargeForumTree ? "forum_listreadpersonal" : "forum_ns_listreadpersonal",
                    mid);

                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_moderatelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>   
        public static DataSet forum_moderatelist(int? mid, object userId, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                using (var ds = new DataSet())
                {
                    //  var conn = sc.GetConnection();
                    //  sc.BeginTransaction();
                    //  using (var trans = sc.Transaction)
                    //  {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", DBNull.Value));
                    sc.CommandText.AppendObjectQuery("category_list", mid);
                    var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                    dt.TableName = "Category";
                    ds.Tables.Add(dt);

                    sc.Parameters.Clear();
                    sc.CommandText.Clear();

                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserForum", false));

                    sc.CommandText.AppendObjectQuery("forum_moderatelist", mid);
                    dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
                    dt.TableName = "ForumUnsorted";
                    ds.Tables.Add(dt);
                    // TODO: missing transaction
                    //  if (trans != null)
                    //     trans.Commit();  
                    //  }

                    using (var dtForumListSorted = ds.Tables["ForumUnsorted"].Clone())
                    {
                        dtForumListSorted.TableName = "Forum";
                        ds.Tables.Add(dtForumListSorted);
                    }

                    forum_list_sort_basic(ds.Tables["ForumUnsorted"], ds.Tables["Forum"], 0, 0);
                    ds.Tables.Remove("ForumUnsorted");

                    // vzrus: Remove here all forums with no reports. Would be better to do it in query...
                    // Array to write categories numbers
                    var categories = new int[ds.Tables["Forum"].Rows.Count];
                    int cntr = 0;

                    //We should make it before too as the colection was changed
                    ds.Tables["Forum"].AcceptChanges();
                    foreach (DataRow dr in ds.Tables["Forum"].Rows)
                    {
                        categories[cntr] = Convert.ToInt32(dr["CategoryID"]);
                        if (Convert.ToInt32(dr["ReportedCount"]) == 0 && Convert.ToInt32(dr["MessageCount"]) == 0)
                        {
                            dr.Delete();
                            categories[cntr] = 0;
                        }

                        cntr++;
                    }

                    ds.Tables["Forum"].AcceptChanges();

                    foreach (DataRow dr in ds.Tables["Category"].Rows)
                    {
                        bool deleteMe = true;
                        foreach (int t in categories)
                        {
                            // We check here if the Category is missing in the array where 
                            // we've written categories number for each forum
                            if (t == Convert.ToInt32(dr["CategoryID"]))
                            {
                                deleteMe = false;
                            }
                        }

                        if (deleteMe)
                        {
                            dr.Delete();
                        }
                    }

                    ds.Tables["Category"].AcceptChanges();

                    ds.Relations.Add(
                        "FK_Forum_Category",
                        ds.Tables["Category"].Columns["CategoryID"],
                        ds.Tables["Forum"].Columns["CategoryID"]);

                    return ds;
                }
            }
        }

        /// <summary>
        /// The forum_moderators.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forum_moderators(int? mid, bool useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));

                sc.CommandText.AppendObjectQuery("forum_moderators", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forum_resync.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        public static void forum_resync(int? mid, object boardId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("forum_resync", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The forum_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="locked">
        /// The locked.
        /// </param>
        /// <param name="hidden">
        /// The hidden.
        /// </param>
        /// <param name="isTest">
        /// The is test.
        /// </param>
        /// <param name="moderated">
        /// The moderated.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        /// <param name="remoteURL">
        /// The remote url.
        /// </param>
        /// <param name="themeURL">
        /// The theme url.
        /// </param>
        /// <param name="imageURL">
        /// The image url.
        /// </param>
        /// <param name="styles">
        /// The styles.
        /// </param>
        /// <param name="dummy">
        /// The dummy.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserForum">
        /// The is user forum.
        /// </param>
        /// <param name="canhavepersforums">
        /// The canhavepersforums.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns> 
        public static long forum_save(
            int? mid,
            object forumID,
            object categoryID,
            object parentID,
            object name,
            object description,
            object sortOrder,
            object locked,
            object hidden,
            object isTest,
            object moderated,
            object accessMaskID,
            object remoteURL,
            object themeURL,
            object imageURL,
            object styles,
            bool dummy,
            object userId,
            bool isUserForum,
            bool canhavepersforums)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                int sortOrderOut;
                bool result = int.TryParse(sortOrder.ToString(), out sortOrderOut);
                if (result)
                {
                    if (sortOrderOut >= 255)
                    {
                        sortOrderOut = 0;
                    }
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ParentID", parentID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrderOut));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Locked", locked));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Hidden", hidden));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsTest", isTest));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Moderated", moderated));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RemoteURL", remoteURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ThemeURL", themeURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ImageURL", imageURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Styles", styles));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserForum", isUserForum));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_CanHavePersForums", canhavepersforums));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("forum_save", mid);

                object resultop = sc.ExecuteScalar(CommandType.StoredProcedure);

                if (resultop == DBNull.Value)
                {
                    return 0;
                }

                if (Config.LargeForumTree)
                {
                    forum_ns_recreate(mid);
                }
                return long.Parse(resultop.ToString());
            }
        }

        /// <summary>
        /// The forum_save_parentschecker.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>      
        public static int forum_save_parentschecker(int? mid, object forumID, object parentID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ParentID", parentID));
                sc.CommandText.AppendObjectQuery("forum_save_prntchck", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The forum_simplelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="startID">
        /// The start id.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>    
        public static DataTable forum_simplelist(int? mid, int startID, int limit)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (startID <= 0)
                {
                    startID = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_StartID", startID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", limit));

                sc.CommandText.AppendObjectQuery("forum_simplelist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forumaccess_group.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includeUserForums">
        /// The include user forums.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forumaccess_group(int? mid, object groupID, object userId, bool includeUserForums)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IncludeUserForums", includeUserForums));

                sc.CommandText.AppendObjectQuery("forumaccess_group", mid);
                return userforumaccess_sort_list(
                    mid,
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true),
                    0,
                    0,
                    0);
            }
        }

        private static  DataTable userforumaccess_sort_list(
            [NotNull] int? mid,
            DataTable listSource,
            int parentID,
            int categoryID,
            int startingIndent)
        {

            var listDestination = new DataTable();

            listDestination.Columns.Add("ForumID", typeof(String));
            listDestination.Columns.Add("ForumName", typeof(String));

            //it is used in two different procedures with different tables, 
            //so, we must add correct columns
            if (listSource.Columns.IndexOf("AccessMaskName") >= 0) listDestination.Columns.Add("AccessMaskName", typeof(String));
            else
            {
                listDestination.Columns.Add("BoardName", typeof(String));
                listDestination.Columns.Add("CategoryName", typeof(String));
                listDestination.Columns.Add("AccessMaskId", typeof(Int32));
            }

            DataView dv = listSource.DefaultView;
            userforumaccess_sort_list_recursive(dv.ToTable(),
                listDestination,
                parentID,
                startingIndent);
            return listDestination;
        }

        /// <summary>
        /// The userforumaccess_sort_list_recursive.
        /// </summary>
        /// <param name="listSource">
        /// The list source.
        /// </param>
        /// <param name="listDestination">
        /// The list destination.
        /// </param>
        /// <param name="parentID">
        /// The parent id.
        /// </param>
        /// <param name="currentIndent">
        /// The current indent.
        /// </param>
        private static void userforumaccess_sort_list_recursive(
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int currentIndent)
        {
            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value)
                {
                    row["ParentID"] = 0;
                }

                if ((int)row["ParentID"] == parentID)
                {
                    string sIndent = string.Empty;

                    for (int j = 0; j < currentIndent; j++) sIndent += "--";

                    // import the row into the destination
                    DataRow newRow = listDestination.NewRow();

                    newRow["ForumID"] = row["ForumID"];
                    newRow["ForumName"] = string.Format("{0} {1}", sIndent, row["ForumName"]);
                    if (listDestination.Columns.IndexOf("AccessMaskName") >= 0)
                    {
                        newRow["AccessMaskName"] = row["AccessMaskName"];
                    }
                    else
                    {
                        newRow["BoardName"] = row["BoardName"];
                        newRow["CategoryName"] = row["CategoryName"];
                        newRow["AccessMaskId"] = row["AccessMaskId"];
                    }

                    listDestination.Rows.Add(newRow);

                    // recurse through the list...
                    userforumaccess_sort_list_recursive(
                        listSource,
                        listDestination,
                        (int)row["ForumID"],
                        currentIndent + 1);
                }
            }
        }

        /// <summary>
        /// The forumaccess_personalgroup.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includeUserForums">
        /// The include user forums.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forumaccess_personalgroup(
            int? mid,
            object groupID,
            object userId,
            bool includeUserForums)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IncludeUserForums", includeUserForums));

                sc.CommandText.AppendObjectQuery("forumaccess_personalgroup", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forumaccess_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includeUserGroups">
        /// The include user groups.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable forumaccess_list(int? mid, object forumID, object userId, bool includeUserGroups)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IncludeUserGroups", includeUserGroups));

                sc.CommandText.AppendObjectQuery("forumaccess_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The forumaccess_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        public static void forumaccess_save(int? mid, object forumID, object groupID, object accessMaskID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));

                sc.CommandText.AppendObjectQuery("forumaccess_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The forum list all.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includeNoAccess">
        /// The include no access.
        /// </param>
        /// <returns>
        /// The <see cref="{IEnumerable}"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll(
            int? mid,
            int boardId,
            int userId,
            bool includeNoAccess)
        {
            return forum_listall(mid, boardId, userId, 0, false).AsEnumerable().Select(r => new TypedForumListAll(r));
        }

        /// <summary>
        /// The forum list all.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="startForumId">
        /// The start forum id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{TypedForumListAll}"/>.
        /// </returns>
        [NotNull]
        public static IEnumerable<TypedForumListAll> ForumListAll(int? mid, int boardId, int userId, int startForumId)
        {
            var allForums = ForumListAll(mid, boardId, userId, 0);

            var forumIds = new List<int>();
            var tempForumIds = new List<int>();

            forumIds.Add(startForumId);
            tempForumIds.Add(startForumId);

            while (true)
            {
                List<int> ids = tempForumIds;
                var temp = allForums.Where(f => ids.Contains(f.ParentID ?? 0));

                if (!temp.Any())
                {
                    break;
                }

                // replace temp forum ids with these...
                tempForumIds = temp.Select(f => f.ForumID ?? 0).Distinct().ToList();

                // add them...
                forumIds.AddRange(tempForumIds);
            }

            // return filtered forums...
            return allForums.Where(f => forumIds.Contains(f.ForumID ?? 0)).Distinct();
        }

        /// <summary>
        /// The forum list all.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="startForumId">
        /// The start forum id.
        /// </param>
        /// <param name="includeNoAccess">
        /// The include no access.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{TypedForumListAll}"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll(
            [NotNull] int? mid,
            int boardId,
            int userId,
            List<int> startForumId,
            bool includeNoAccess)
        {
            var allForums = ForumListAll(mid, boardId, userId);

            var forumIds = new List<int>();
            var tempForumIds = new List<int>();
            int ftoadd = 0;
            if (startForumId.Any())
            {
                ftoadd = startForumId.First(f => f > -1);
            }

            forumIds.Add(ftoadd);
            tempForumIds.Add(ftoadd);

            var typedForumListAlls = allForums as IList<TypedForumListAll> ?? allForums.ToList();
            while (true)
            {
                List<int> ids = tempForumIds;
                var temp = typedForumListAlls.Where(f => ids.Contains(f.ParentID ?? 0));

                var forumListAlls = temp as IList<TypedForumListAll> ?? temp.ToList();
                if (!forumListAlls.Any())
                {
                    break;
                }

                // replace temp forum ids with these...
                tempForumIds = forumListAlls.Select(f => f.ForumID ?? 0).Distinct().ToList();

                // add them...
                forumIds.AddRange(tempForumIds);
            }

            // return filtered forums...
            return typedForumListAlls.Where(f => forumIds.Contains(f.ForumID ?? 0)).Distinct();
        }

        /// <summary>
        /// The forum list all.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll([NotNull] int? mid, int boardId, int userId)
        {
            return forum_listall(mid, boardId, userId, 0, false).AsEnumerable().Select(r => new TypedForumListAll(r));
        }

        /// <summary>
        /// The forumpage_initdb.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="errorStr">
        /// The error str.
        /// </param>
        /// <param name="debugging">
        /// The debugging.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool forumpage_initdb(int? mid, out string errorStr, bool debugging)
        {
            errorStr = null;
            using (var sc = new VzfSqlCommand(mid))
            {
                if (sc.DataSource != null)
                {
                    return true;
                }

                errorStr = debugging ? "Unable to connect to the Database." : "Unable to connect to the Database.";

                return false;
            }
        }

        /// <summary>
        /// The forumpage_validateversion.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="appVersion">
        /// The app version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string forumpage_validateversion(int? mid, int appVersion)
        {
            string redirect = string.Empty;

            try
            {
                var registry = registry_list(mid, "Version");

                if ((registry.Rows.Count == 0) || (Convert.ToInt32(registry.Rows[0]["Value"]) < appVersion))
                {
                    // needs upgrading..
                    redirect =
                        "install/default.aspx?upgrade={0}&md={1}".FormatWith(
                            registry.Rows.Count != 0 ? Convert.ToInt32(registry.Rows[0]["Value"]) : 0,
                            mid ?? 1);
                }
            }
            catch (Exception)
            {
                // needs to be setup..
                redirect = "install/";
            }

            return redirect;
        }

        /// <summary>
        /// The get search result.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="toSearchWhat">
        /// The to search what.
        /// </param>
        /// <param name="toSearchFromWho">
        /// The to search from who.
        /// </param>
        /// <param name="searchFromWhoMethod">
        /// The search from who method.
        /// </param>
        /// <param name="searchWhatMethod">
        /// The search what method.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="forumIdToStartAt">
        /// The forum id to start at.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="maxResults">
        /// The max results.
        /// </param>
        /// <param name="useFullText">
        /// The use full text.
        /// </param>
        /// <param name="searchDisplayName">
        /// The search display name.
        /// </param>
        /// <param name="includeChildren">
        /// The include children.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable GetSearchResult(
            int? mid,
            string toSearchWhat,
            string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            List<int> categoryId,
            List<int> forumIdToStartAt,
            int userId,
            int boardId,
            int maxResults,
            bool useFullText,
            bool searchDisplayName,
            bool includeChildren,
            string culture)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            // New access
            /*  if (toSearchWhat == "*")
              {
                  toSearchWhat = string.Empty;
              }

              IEnumerable<int> forumIds = new List<int>();

              if (forumIdToStartAt != 0)
              {
                  forumIds = ForumListAll(boardId, userID, forumIdToStartAt).Select(f => f.ForumID ?? 0).Distinct();
              }

              string searchSql = new SearchBuilder().BuildSearchSql(toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, userID, searchDisplayName, boardId, maxResults, useFullText, forumIds);

              using (SqlCommand cmd = PostgreDbAccess.GetCommand(searchSql, true))
              {
                  return PostgreDbAccess.Current.GetData(cmd);
              } */

            if (toSearchWhat == "*")
            {
                toSearchWhat = string.Empty;
            }

            string forumIDs = string.Empty;
            string categoriesIds = string.Empty;
            DataTable dt = null;
            if ((categoryId.Any() || forumIdToStartAt.Any()) && !Config.LargeForumTree)
            {
                dt = CommonDb.forum_listall_sorted(mid, boardId, userId, forumIdToStartAt.ToArray<int>());
            }

            if (categoryId.Any())
            {
                if (Config.LargeForumTree)
                {
                    DataTable dt1 = CommonDb.forum_categoryaccess_activeuser(mid, boardId, userId);
                    foreach (DataRow c in dt1.Rows)
                    {
                        foreach (int c1 in categoryId)
                        {
                            if (Convert.ToInt32(c["CategoryID"]) == c1)
                            {
                                categoriesIds = categoriesIds + "," + c1;
                            }
                        }
                    }

                    categoriesIds = categoriesIds.Trim(',');
                }
                else
                {
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            // if not a category
                            if (Convert.ToInt32(dr["ForumID"]) == -1)
                            {
                                categoriesIds = categoriesIds + Convert.ToInt32(dr["ForumID"]) + ",";
                            }
                        }

                        categoriesIds = categoriesIds.Substring(0, forumIDs.Length - 1);

                        categoriesIds = categoriesIds.Trim(',');
                    }
                }
            }

            if (forumIdToStartAt.Any())
            {
                if (!Config.LargeForumTree)
                {
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            // if not a category
                            if (Convert.ToInt32(dr["ForumID"]) != -1)
                            {
                                forumIDs = forumIDs + Convert.ToInt32(dr["ForumID"]) + ",";
                            }
                        }

                        forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
                    }
                }
                else
                {
                    foreach (int frms in forumIdToStartAt)
                    {
                        var d1 = CommonDb.forum_ns_getchildren_activeuser(
                            mid,
                            boardId,
                            0,
                            frms,
                            userId,
                            false,
                            false,
                            "-");

                        foreach (DataRow r in d1.Rows)
                        {
                            forumIDs += "," + r["ForumID"];
                        }

                        // Parent forum only.
                        if (!includeChildren)
                        {
                            break;
                        }
                    }

                    forumIDs = forumIDs.Trim(',');
                }
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();
            string sql;
            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = new VZF.Data.MsSql.Search.SearchBuilder().BuildSearchSql(
                        mid,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        userId,
                        searchDisplayName,
                        boardId,
                        maxResults,
                        useFullText,
                        forumIDs,
                        forumIdToStartAt);
                    break;
                case SqlDbAccess.Npgsql:
                    sql = new VZF.Data.Postgre.Search.SearchBuilder().BuildSearchSql(
                        mid,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        userId,
                        searchDisplayName,
                        boardId,
                        maxResults,
                        useFullText,
                        categoriesIds,
                        forumIDs,
                        forumIdToStartAt, 
                        culture);
                    break;
                case SqlDbAccess.MySql:
                    sql = new VZF.Data.MySql.Search.SearchBuilder().BuildSearchSql(
                        mid,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        userId,
                        searchDisplayName,
                        boardId,
                        maxResults,
                        useFullText,
                        forumIDs,
                        forumIdToStartAt);
                    break;
                case SqlDbAccess.Firebird:
                    sql = new VZF.Data.Firebird.Search.SearchBuilder().BuildSearchSql(
                        mid,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        userId,
                        searchDisplayName,
                        boardId,
                        maxResults,
                        useFullText,
                        forumIDs,
                        forumIdToStartAt);
                    break;
                    // case SqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIdToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case SqlDbAccess.Db2:  return VZF.Data.Db2.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIdToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case SqlDbAccess.Other:  return VZF.Data.Other.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIdToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
            DataTable dt_result;
            using (var sc = new VzfSqlCommand(mid))
            {
                if (dataEngine == SqlDbAccess.MySql)
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_Limit", maxResults.ToString()));
                }

                sc.CommandText.AppendQuery(sql);
                dt_result = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }

            if (dataEngine == SqlDbAccess.MySql)
            {
                string old_uid = null;
                string old_fid = null;
                foreach (DataRow dr in dt_result.Rows)
                {
                    if (old_uid == dr["UserID"].ToString() || old_fid == dr["ForumID"].ToString())
                    {
                        continue;
                    }

                    using (var sc = new VzfSqlCommand(mid))
                    {
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_UserID", dr["UserID"]));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_ForumID", dr["ForumID"]));
                        sc.CommandText.AppendQuery(
                            string.Format(
                                "SELECT {0}(@i_UserID,@i_ForumID);",
                                SqlDbAccess.GetVzfObjectName("vaccess_s_readaccess_combo", mid)));

                        if (Convert.ToInt32(sc.ExecuteScalar(CommandType.Text)) == 0)
                        {
                            dr.Delete();
                        }

                    }

                    old_uid = dr["UserID"].ToString();
                    old_fid = dr["ForumID"].ToString();
                }

                dt_result.AcceptChanges();
            }

            return dt_result;
        }

        /// <summary>
        /// The group_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>    
        public static void group_delete(int? mid, object groupID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));

                sc.CommandText.AppendObjectQuery("group_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The group_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable group_list(int? mid, object boardId, object groupID, int pageIndex, int pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("group_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_byuserlist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserGroup">
        /// The is user group.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>      
        public static DataTable group_byuserlist(
            int? mid,
            object boardId,
            object groupID,
            object userId,
            object isUserGroup)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserGroup", isUserGroup));

                sc.CommandText.AppendObjectQuery("group_byuserlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_medal_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>  
        public static void group_medal_delete(int? mid, object groupID, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));

                sc.CommandText.AppendObjectQuery("group_medal_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The group_medal_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable group_medal_list(int? mid, object groupID, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));

                sc.CommandText.AppendObjectQuery("group_medal_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_medal_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="hide">
        /// The hide.
        /// </param>
        /// <param name="onlyRibbon">
        /// The only ribbon.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        public static void group_medal_save(
            int? mid,
            object groupID,
            object medalID,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Hide", hide));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_OnlyRibbon", onlyRibbon));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));

                sc.CommandText.AppendObjectQuery("group_medal_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The group_member.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable group_member(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("group_member", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_rank_style.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board Id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable group_rank_style(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("group_rank_style", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The group_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="isAdmin">
        /// The is admin.
        /// </param>
        /// <param name="isGuest">
        /// The is guest.
        /// </param>
        /// <param name="isStart">
        /// The is start.
        /// </param>
        /// <param name="isModerator">
        /// The is moderator.
        /// </param>
        /// <param name="isHidden">
        /// The is hidden.
        /// </param>
        /// <param name="accessMaskId">
        /// The access mask id.
        /// </param>
        /// <param name="pmLimit">
        /// The pm limit.
        /// </param>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="usrSigChars">
        /// The usr sig chars.
        /// </param>
        /// <param name="usrSigBBCodes">
        /// The usr sig bb codes.
        /// </param>
        /// <param name="usrSigHTMLTags">
        /// The usr sig html tags.
        /// </param>
        /// <param name="usrAlbums">
        /// The usr albums.
        /// </param>
        /// <param name="usrAlbumImages">
        /// The usr album images.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserGroup">
        /// The is user group.
        /// </param>
        /// <param name="personalForumsNumber">
        /// The personal forums number.
        /// </param>
        /// <param name="personalAccessMasksNumber">
        /// The personal access masks number.
        /// </param>
        /// <param name="personalGroupsNumber">
        /// The personal groups number.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long group_save(
            int? mid,
            object groupId,
            object boardId,
            object name,
            object isAdmin,
            object isGuest,
            object isStart,
            object isModerator,
            object isHidden,
            object accessMaskId,
            object pmLimit,
            object style,
            object sortOrder,
            object description,
            object usrSigChars,
            object usrSigBBCodes,
            object usrSigHTMLTags,
            object usrAlbums,
            object usrAlbumImages,
            object userId,
            object isUserGroup,
            object personalForumsNumber,
            object personalAccessMasksNumber,
            object personalGroupsNumber)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdmin", isAdmin));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsGuest", isGuest));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsStart", isStart));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsModerator", isModerator));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsHidden", isHidden));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PMLimit", pmLimit));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Style", style));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrSigChars", usrSigChars));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UsrSigBBCodes", usrSigBBCodes));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UsrSigHTMLTags", usrSigHTMLTags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrAlbums", usrAlbums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrAlbumImages", usrAlbumImages));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserGroup", isUserGroup));
                sc.Parameters.Add(
                    sc.CreateParameter(DbType.Int32, "i_PersonalAccessMasksNumber", personalAccessMasksNumber));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PersonalGroupsNumber", personalGroupsNumber));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PersonalForumsNumber", personalForumsNumber));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("group_save", mid);
                return Convert.ToInt64(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The mail_create.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="fromName">
        /// The from name.
        /// </param>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <param name="toName">
        /// The to name.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="bodyHtml">
        /// The body html.
        /// </param>
        public static void mail_create(
            int? mid,
            object @from,
            object fromName,
            object @to,
            object toName,
            object subject,
            object body,
            object bodyHtml)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_From", @from));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_FromName", fromName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_To", @to));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ToName", toName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", subject));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Body", body));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BodyHtml", bodyHtml));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("mail_create", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The mail_createwatch.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="fromName">
        /// The from name.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="bodyHtml">
        /// The body html.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>  
        public static void mail_createwatch(
            int? mid,
            object topicID,
            object @from,
            object fromName,
            object subject,
            object body,
            object bodyHtml,
            object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_From", @from));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_FromName", fromName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", subject));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Body", body));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BodyHtml", bodyHtml));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("mail_createwatch", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The mail_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="mailID">
        /// The mail id.
        /// </param> 
        public static void mail_delete(int? mid, object mailID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MailID", mailID));

                sc.CommandText.AppendObjectQuery("mail_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The mail list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="processId">
        /// The process id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>  
        public static IEnumerable<TypedMailList> MailList(int? mid, long processId)
        {
            //TODO: postgre only
            /* using (var sc = new SQLCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ProcessID", processId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("mail_listupdate", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }  */

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ProcessID", processId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("mail_list", mid);
                return
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                        .SelectTypedList(x => new TypedMailList(x));
            }
        }

        /// <summary>
        /// Deletes given medals.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">ID of board of which medals to delete. Required.</param>
        /// <param name="category">Cateogry of medals to delete. Can be null. In such case this parameter is ignored.</param>
        public static void medal_delete(int? mid, object boardId, object category)
        {
            medal_delete(mid, boardId, null, category);
        }

        /// <summary>
        /// Deletes given medal.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID">ID of medal to delete.</param>
        public static void medal_delete(int? mid, object medalID)
        {
            medal_delete(mid, null, medalID, null);
        }

        /// <summary>
        /// The medal_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <param name="category">
        /// The category.
        /// </param>
        public static void medal_delete(int? mid, object boardId, object medalID, object category)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Category", category));

                sc.CommandText.AppendObjectQuery("medal_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Lists given medal.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable medal_list(int? mid, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", null));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Category", null));

                sc.CommandText.AppendObjectQuery("medal_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The medal_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable medal_list(int? mid, object boardId, object category)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", null));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Category", category));

                sc.CommandText.AppendObjectQuery("medal_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The medal_listusers.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable medal_listusers(int? mid, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));

                sc.CommandText.AppendObjectQuery("medal_listusers", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The medal_resort.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <param name="move">
        /// The move.
        /// </param>   
        public static void medal_resort(int? mid, object boardId, object medalID, int move)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Move", move));

                sc.CommandText.AppendObjectQuery("medal_resort", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The medal_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="medalURL">
        /// The medal url.
        /// </param>
        /// <param name="ribbonURL">
        /// The ribbon url.
        /// </param>
        /// <param name="smallMedalURL">
        /// The small medal url.
        /// </param>
        /// <param name="smallRibbonURL">
        /// The small ribbon url.
        /// </param>
        /// <param name="smallMedalWidth">
        /// The small medal width.
        /// </param>
        /// <param name="smallMedalHeight">
        /// The small medal height.
        /// </param>
        /// <param name="smallRibbonWidth">
        /// The small ribbon width.
        /// </param>
        /// <param name="smallRibbonHeight">
        /// The small ribbon height.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool medal_save(
            int? mid,
            object boardId,
            object medalID,
            object name,
            object description,
            object message,
            object category,
            object medalURL,
            object ribbonURL,
            object smallMedalURL,
            object smallRibbonURL,
            object smallMedalWidth,
            object smallMedalHeight,
            object smallRibbonWidth,
            object smallRibbonHeight,
            object sortOrder,
            object flags)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                int sortOrderOut;
                bool result = Int32.TryParse(sortOrder.ToString(), out sortOrderOut);
                if (result)
                {
                    if (sortOrderOut >= 255)
                    {
                        sortOrderOut = 0;
                    }
                }
                else
                {
                    sortOrderOut = 0;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Category", category));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MedalURL", medalURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RibbonURL", ribbonURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SmallMedalURL", smallMedalURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SmallRibbonURL", smallRibbonURL));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmallMedalWidth", smallMedalWidth));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmallMedalHeight", smallMedalHeight));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmallRibbonWidth", smallRibbonWidth));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmallRibbonHeight", smallRibbonHeight));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrderOut));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", flags));

                sc.CommandText.AppendObjectQuery("medal_save", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The message_ add thanks.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="fromUserID">
        /// The from user id.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="useDisplayName">
        /// The use display name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string message_AddThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", fromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", useDisplayName));

                sc.CommandText.AppendObjectQuery("message_AddThanks", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The message_approve.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        public static void message_approve(int? mid, object messageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                sc.CommandText.AppendObjectQuery("message_approve", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="deleteReason">
        /// The delete reason.
        /// </param>
        /// <param name="isDeleteAction">
        /// The is delete action.
        /// </param>
        /// <param name="DeleteLinked">
        /// The delete linked.
        /// </param>
        public static void message_delete(
            int? mid,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked)
        {
            message_delete(mid, messageID, isModeratorChanged, deleteReason, isDeleteAction, DeleteLinked, false);
        }

        /// <summary>
        /// The message_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="deleteReason">
        /// The delete reason.
        /// </param>
        /// <param name="isDeleteAction">
        /// The is delete action.
        /// </param>
        /// <param name="DeleteLinked">
        /// The delete linked.
        /// </param>
        /// <param name="eraseMessage">
        /// The erase message.
        /// </param>
        public static void message_delete(
            int? mid,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool eraseMessage)
        {
            message_deleteRecursively(
                mid,
                messageID,
                isModeratorChanged,
                deleteReason,
                isDeleteAction,
                DeleteLinked,
                eraseMessage);
        }

        /// <summary>
        /// The message_delete recursively.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="deleteReason">
        /// The delete reason.
        /// </param>
        /// <param name="isDeleteAction">
        /// The is delete action.
        /// </param>
        /// <param name="deleteLinked">
        /// The delete linked.
        /// </param>
        /// <param name="eraseMessages">
        /// The erase messages.
        /// </param>
        private static void message_deleteRecursively([NotNull] int? mid, [NotNull] object messageID, bool isModeratorChanged, [NotNull] string deleteReason, int isDeleteAction, bool deleteLinked, bool eraseMessages)
        {
            bool useFileTable = GetBooleanRegistryValue(mid, "UseFileTable");

            if (deleteLinked)
            {
                // Delete replies
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                    sc.CommandText.AppendObjectQuery("message_getReplies", mid);
                    DataTable tbReplies = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true);
                    foreach (DataRow row in tbReplies.Rows)
                    {
                        message_deleteRecursively(
                            mid,
                            row["MessageID"],
                            isModeratorChanged,
                            deleteReason,
                            isDeleteAction,
                            deleteLinked,
                            eraseMessages);
                    }
                }
            }

            // If the files are actually saved in the Hard Drive
            if (!useFileTable)
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AttachmentID", null));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", null));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", 0));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", 1000000));

                    sc.CommandText.AppendObjectQuery("attachment_list", mid);
                    DataTable tbAttachments = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true);
                    string uploadDir =
                        HostingEnvironmentUtil.MapPath(
                            String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.TopicAttachments));
                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format(
                                "{0}/{1}.{2}.yafupload",
                                uploadDir,
                                messageID,
                                row["FileName"]);
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch
                        {
                            // error deleting that file... 
                        }
                    }
                }
            }

            if (eraseMessages)
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_EraseMessage", eraseMessages));

                    sc.CommandText.AppendObjectQuery("message_delete", mid);
                    sc.ExecuteNonQuery(CommandType.StoredProcedure);
                }
            }
            else
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_isModeratorChanged", isModeratorChanged));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeleteReason", deleteReason));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsDeleteAction", isDeleteAction));

                    sc.CommandText.AppendObjectQuery("message_deleteundelete", mid);
                    sc.ExecuteNonQuery(CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// The message_findunread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="lastRead">
        /// The last read.
        /// </param>
        /// <param name="showDeleted">
        /// The show deleted.
        /// </param>
        /// <param name="authorUserID">
        /// The author user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_findunread(
            int? mid,
            object topicID,
            object messageId,
            object lastRead,
            object showDeleted,
            object authorUserID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastRead", lastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowDeleted", showDeleted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AuthorUserID", authorUserID));

                sc.CommandText.AppendObjectQuery("message_findunread", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_get replies list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable message_getRepliesList(int? mid, object messageID)
        {
            DataTable list = new DataTable();
            list.Columns.Add("MessageID", typeof(int));
            list.Columns.Add("Posted", typeof(DateTime));
            list.Columns.Add("Subject", typeof(string));
            list.Columns.Add("Message", typeof(string));
            list.Columns.Add("UserID", typeof(int));
            list.Columns.Add("Flags", typeof(int));
            list.Columns.Add("UserName", typeof(string));
            list.Columns.Add("Signature", typeof(string));

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                sc.CommandText.AppendObjectQuery("message_reply_list", mid);
                DataTable dtr = sc.ExecuteDataTableFromReader(
                    CommandBehavior.Default,
                    CommandType.StoredProcedure,
                    true);

                for (int i = 0; i < dtr.Rows.Count; i++)
                {
                    DataRow newRow = list.NewRow();
                    DataRow row = dtr.Rows[i];
                    newRow["MessageID"] = row["MessageID"];
                    newRow["Posted"] = row["Posted"];
                    newRow["Subject"] = row["Subject"];
                    newRow["Message"] = row["Message"];
                    newRow["UserID"] = row["UserID"];
                    newRow["Flags"] = row["Flags"];
                    newRow["UserName"] = row["UserName"];
                    newRow["Signature"] = row["Signature"];
                    list.Rows.Add(newRow);
                    message_getRepliesList_populate(mid, list, (int)row["MessageId"]);
                }

                return list;
            }
        }

        private static void message_getRepliesList_populate([NotNull] int? mid, DataTable list, int messageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.CommandText.AppendObjectQuery("message_reply_list", mid);
                DataTable dtr = sc.ExecuteDataTableFromReader(
                    CommandBehavior.Default,
                    CommandType.StoredProcedure,
                    true);

                for (int i = 0; i < dtr.Rows.Count; i++)
                {
                    DataRow newRow = list.NewRow();
                    DataRow row = dtr.Rows[i];
                    newRow["MessageID"] = row["MessageID"];
                    newRow["Posted"] = row["Posted"];
                    newRow["Subject"] = row["Subject"];
                    newRow["Message"] = row["Message"];
                    newRow["UserID"] = row["UserID"];
                    newRow["Flags"] = row["Flags"];
                    newRow["UserName"] = row["UserName"];
                    newRow["Signature"] = row["Signature"];
                    list.Rows.Add(newRow);
                    message_getRepliesList_populate(mid, list, (int)row["MessageId"]);
                }
            }
        }

        /// <summary>
        /// The message_ get text by ids.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageIDs">
        /// The message i ds.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_GetTextByIds(int? mid, string messageIDs)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MessageIDs", messageIDs));

                sc.CommandText.AppendObjectQuery("message_gettextbyids", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_ get thanks.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_GetThanks(int mid, object messageId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));

                sc.CommandText.AppendObjectQuery("message_getthanks", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_listreported.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_listreported(int? mid, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("message_listreported", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_listreporters.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_listreporters(int? mid, int messageID)
        {
            return message_listreporters(mid, messageID, null);
        }

        /// <summary>
        /// The message_listreporters.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_listreporters(int? mid, int messageID, object userID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));

                sc.CommandText.AppendObjectQuery("message_listreporters", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_move.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="moveToTopic">
        /// The move to topic.
        /// </param>
        /// <param name="moveAll">
        /// The move all.
        /// </param>
        public static void message_move(int? mid, object messageID, object moveToTopic, bool moveAll)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MoveToTopic", moveToTopic));

                sc.CommandText.AppendObjectQuery("message_move", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message_ remove thanks.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="fromUserID">
        /// The from user id.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="useDisplayName">
        /// The use display name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string message_RemoveThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", fromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseDisplayName", useDisplayName));

                sc.CommandText.AppendObjectQuery("message_removethanks", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The message_report.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="reportedDateTime">
        /// The reported date time.
        /// </param>
        /// <param name="reportText">
        /// The report text.
        /// </param>
        public static void message_report(
            int? mid,
            object messageID,
            object userId,
            object reportedDateTime,
            object reportText)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ReporterID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ReportedDate", reportedDateTime));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ReportText", reportText));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("message_report", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message_reportcopyover.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        public static void message_reportcopyover(int? mid, object messageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                sc.CommandText.AppendObjectQuery("message_reportcopyover", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message_reportresolve.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageFlag">
        /// The message flag.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>  
        public static void message_reportresolve(int? mid, object messageFlag, object messageID, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageFlag", messageFlag));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("message_reportresolve", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="posted">
        /// The posted.
        /// </param>
        /// <param name="replyTo">
        /// The reply to.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="messageDescription">
        /// The message description.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>    
        public static bool message_save(
            int? mid,
            [NotNull] object topicId,
            [NotNull] object userId,
            [NotNull] object message,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object replyTo,
            [NotNull] object flags,
            [CanBeNull] object messageDescription,
            ref long messageId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_IP", ip));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Posted", posted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ReplyTo", replyTo));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BlogPostID", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ExternalMessageID", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ReferenceMessageID", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MessageDescription", messageDescription));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", flags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                var mesid = sc.CreateParameter(DbType.Int32, "i_MessageID", messageId, ParameterDirection.Output);
                sc.Parameters.Add(mesid);

                sc.CommandText.AppendObjectQuery("message_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                messageId = Convert.ToInt64(mesid.Value);
                return true;
            }
        }

        /// <summary>
        /// The message_secdata.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="MessageID">
        /// The message id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable message_secdata(int? mid, int messageId, object pageUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));

                sc.CommandText.AppendObjectQuery("message_secdata", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_simplelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="StartID">
        /// The start id.
        /// </param>
        /// <param name="Limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable message_simplelist(int? mid, int StartID, int Limit)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (Limit == 0)
                {
                    Limit = 1000;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_StartID", StartID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", Limit));

                sc.CommandText.AppendObjectQuery("message_simplelist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_ thanks number.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>  >
        public static int message_ThanksNumber(int? mid, object messageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                sc.CommandText.AppendObjectQuery("message_thanksnumber", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The message_unapproved.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        public static DataTable message_unapproved(int? mid, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("message_unapproved", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="styles">
        /// The styles.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="reasonOfEdit">
        /// The reason of edit.
        /// </param>
        /// <param name="isModeratorChanged">
        /// The is moderator changed.
        /// </param>
        /// <param name="overrideApproval">
        /// The override approval.
        /// </param>
        /// <param name="origMessage">
        /// The orig message.
        /// </param>
        /// <param name="editedBy">
        /// The edited by.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>   
        public static void message_update(
            int? mid,
            object messageId,
            object priority,
            object message,
            object description,
            object status,
            object styles,
            object subject,
            object flags,
            object reasonOfEdit,
            object isModeratorChanged,
            object overrideApproval,
            object origMessage,
            object editedBy,
            object messageDescription,
            string tags)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Priority", priority));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", subject));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Status", status));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Styles", styles));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", flags));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Reason", reasonOfEdit));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_EditedBy", editedBy));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsModeratorChanged", isModeratorChanged));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_OverrideApproval", overrideApproval));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_OriginalMessage", origMessage));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MessageDescription", messageDescription));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Tags", tags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("message_update", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The message get all thanks.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageIdsSeparatedWithColon">
        /// The message ids separated with colon.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>   
        public static IEnumerable<TypedAllThanks> MessageGetAllThanks(int? mid, string messageIdsSeparatedWithColon)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MessageIDs", messageIdsSeparatedWithColon));

                sc.CommandText.AppendObjectQuery("message_getallthanks", mid);
                return
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                        .AsEnumerable()
                        .Select(t => new TypedAllThanks(t));
            }
        }

        /// <summary>
        /// The messagehistory_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="daysToClean">
        /// The days to clean.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable messagehistory_list(int? mid, int messageID, int daysToClean)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_DaysToClean", daysToClean));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("messagehistory_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The message list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<TypedMessageList> MessageList(int? mid, int messageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));

                sc.CommandText.AppendObjectQuery("message_list", mid);
                return
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                        .AsEnumerable()
                        .Select(t => new TypedMessageList(t));
            }
        }

        /// <summary>
        /// The moderators_team_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable moderators_team_list(int? mid, bool useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));

                sc.CommandText.AppendObjectQuery("moderators_team_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The readtopic_ add or update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void Readtopic_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("readtopic_addorupdate", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /* public static void ReadTopic_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 case SqlDbAccess.MsSql: MsSql.Db.Readtopic_delete(connectionString, trackingID); break;
                 case SqlDbAccess.Npgsql: Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 case SqlDbAccess.MySql:  MySqlDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 case SqlDbAccess.Firebird:  FirebirdDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case SqlDbAccess.Oracle:   VZF.Data.Oracle.Db.Readtopic_delete(connectionString, trackingID);break;
                 // case SqlDbAccess.Db2:   VZF.Data.Db2.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case SqlDbAccess.Other:   VZF.Data.Other.Db.Readtopic_delete(connectionString, trackingID); break;
                 default:
                     throw new ArgumentOutOfRangeException(dataEngine);
                     break;
             }
         } */

        /// <summary>
        /// Delete the Read Tracking
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="trackingID">
        /// The tracking id.
        /// </param>
        /// <summary>
        /// Get the Global Last Read DateTime User
        /// </summary>
        /// <param name="lastVisitDate">
        /// The last Visit Date of the User
        /// </param>
        /// <returns>
        /// Returns the Global Last Read DateTime
        /// </returns>
        public static DateTime? User_LastRead(int? mid, [NotNull] object userID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));

                sc.CommandText.AppendObjectQuery("user_lastread", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToType<DateTime?>();
            }
        }

        /// <summary>
        /// The readtopic_lastread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>
        public static DateTime? Readtopic_lastread(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("readtopic_lastread", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToType<DateTime?>();
            }
        }

        /// <summary>
        /// The read forum_ add or update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>    
        public static void ReadForum_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("readforum_addorupdate", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /* public static void ReadForum_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 case SqlDbAccess.MsSql: MsSql.Db.ReadForum_delete(connectionString, trackingID); break;
                 case SqlDbAccess.Npgsql: Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 case SqlDbAccess.MySql:  MySqlDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 case SqlDbAccess.Firebird:  FirebirdDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case SqlDbAccess.Oracle:   VZF.Data.Oracle.Db.ReadForum_delete(connectionString, trackingID);break;
                 // case SqlDbAccess.Db2:   VZF.Data.Db2.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case SqlDbAccess.Other:   VZF.Data.Other.Db.ReadForum_delete(connectionString, trackingID); break;
                 default:
                     throw new ArgumentOutOfRangeException(dataEngine);
                     break;
             }
         } */

        /// <summary>
        /// The read forum_lastread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>    
        public static DateTime? ReadForum_lastread(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("readforum_lastread", mid);
                var tableLastRead = sc.ExecuteScalar(CommandType.StoredProcedure);
                return tableLastRead != null && tableLastRead != DBNull.Value
                           ? (DateTime)tableLastRead
                           : DateTimeHelper.SqlDbMinTime();
            }
        }

        /// <summary>
        /// The nntpforum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>  
        public static void nntpforum_delete(int? mid, object nntpForumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpForumID", nntpForumID));

                sc.CommandText.AppendObjectQuery("nntpforum_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The nntpforum_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="minutes">
        /// The minutes.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>
        /// <param name="active">
        /// The active.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable nntpforum_list(
            int? mid,
            object boardId,
            object minutes,
            object nntpForumID,
            object active)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Minutes", minutes));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpForumID", nntpForumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Active", active));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("nntpforum_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The nntpforum_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>
        /// <param name="nntpServerID">
        /// The nntp server id.
        /// </param>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="active">
        /// The active.
        /// </param>
        /// <param name="cutoffdate">
        /// The cutoffdate.
        /// </param>    
        public static void nntpforum_save(
            int? mid,
            object nntpForumID,
            object nntpServerID,
            object groupName,
            object forumID,
            object active,
            object cutoffdate)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpForumID", nntpForumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpServerID", nntpServerID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_GroupName", groupName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Active", active));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_DateCutoff", cutoffdate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("nntpforum_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The nntpforum_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>
        /// <param name="lastMessageNo">
        /// The last message no.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public static void nntpforum_update(int? mid, object nntpForumID, object lastMessageNo, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpForumID", nntpForumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_LastMessageNo", lastMessageNo));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("nntpforum_update", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The nntp forum list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="minutes">
        /// The minutes.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>
        /// <param name="active">
        /// The active.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<TypedNntpForum> NntpForumList(
            int? mid,
            int boardId,
            int? minutes,
            int? nntpForumID,
            bool? active)
        {
            return
                nntpforum_list(mid, boardId, minutes, nntpForumID, active)
                    .AsEnumerable()
                    .Select(r => new TypedNntpForum(r));
        }

        /// <summary>
        /// The nntpserver_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpServerID">
        /// The nntp server id.
        /// </param>  
        public static void nntpserver_delete(int? mid, object nntpServerID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpServerID", nntpServerID));

                sc.CommandText.AppendObjectQuery("nntpserver_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The nntpserver_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="nntpServerID">
        /// The nntp server id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable nntpserver_list(int? mid, object boardId, object nntpServerID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpServerID", nntpServerID));

                sc.CommandText.AppendObjectQuery("nntpserver_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The nntpserver_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpServerID">
        /// The nntp server id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userPass">
        /// The user pass.
        /// </param>     
        public static void nntpserver_save(
            int? mid,
            object nntpServerID,
            object boardId,
            object name,
            object address,
            object port,
            object userName,
            object userPass)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpServerID", nntpServerID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Address", address));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Port", port));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserPass", userPass));

                sc.CommandText.AppendObjectQuery("nntpserver_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The nntptopic_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="thread">
        /// The thread.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable nntptopic_list(int? mid, object thread)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Thread", thread));

                sc.CommandText.AppendObjectQuery("nntptopic_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The nntptopic_savemessage.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>
        /// <param name="topic">
        /// The topic.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="posted">
        /// The posted.
        /// </param>
        /// <param name="externalMessageId">
        /// The external message id.
        /// </param>
        /// <param name="referenceMessageId">
        /// The reference message id.
        /// </param>       
        public static void nntptopic_savemessage(
            int? mid,
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
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NntpForumID", nntpForumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Topic", topic));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Body", body));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_IP", ip));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Posted", posted));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ExternalMessageId", externalMessageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ReferenceMessageId", referenceMessageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("nntptopic_savemessage", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pageload.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userKey">
        /// The user key.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="forumPage">
        /// The forum page.
        /// </param>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="platform">
        /// The platform.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="isCrawler">
        /// The is crawler.
        /// </param>
        /// <param name="isMobileDevice">
        /// The is mobile device.
        /// </param>
        /// <param name="donttrack">
        /// The donttrack.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        public static pageload_Result pageload(
            int? mid,
            object sessionId,
            object boardId,
            object userKey,
            object ip,
            object location,
            object forumPage,
            object browser,
            object platform,
            object categoryId,
            object forumId,
            object topicId,
            object messageId,
            object isCrawler,
            object isMobileDevice,
            object donttrack)
        {
            int nTries = 0;
            while (true)
            {
                try
                {
                    using (var sc = new VzfSqlCommand(mid))
                    {
                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SessionID", sessionId));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                        // TODO: look why guid here generated not in db Firebird only
                        if (userKey != null && userKey.ToString().Length > 0)
                        {
                            sc.Parameters.Add(
                                sc.CreateParameter(DbType.String, "i_UserKey", new Guid(userKey.ToString()).ToString()));
                        }
                        else
                        {
                            sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", DBNull.Value));
                        }

                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_IP", ip));
                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Location", location));
                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ForumPage", forumPage));
                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Browser", browser));
                        sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Platform", platform));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsCrawler", isCrawler ?? false));
                        sc.Parameters.Add(
                            sc.CreateParameter(DbType.Boolean, "i_IsMobileDevice", isMobileDevice ?? false));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DontTrack", donttrack ?? false));
                        sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                        sc.CommandText.AppendObjectQuery("pageload", mid);
                        /*  if (dt.Rows.Count > 0)
                          {
                              return dt.Rows[0];
                          }
                          else
                          {
                              return null;
                          } */
                        return
                            sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                                .AsEnumerable()
                                .Select(r => new pageload_Result(r))
                                .ToList()[0];
                        ;
                    }
                }
                catch (ArgumentOutOfRangeException xx)
                {
                    if (nTries < 3)
                    {
                        /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                    }
                    else
                        throw new ArgumentOutOfRangeException(
                            string.Format(
                                "Number of DataTable columns from DataReader cannot be null. Trys -{0}",
                                nTries),
                            xx);
                }
                catch (Exception)
                {
                    // if (x.Number == 1213 && nTries < 3)
                    // {
                    /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                    //  }
                    //  else
                    //    throw new ApplicationException(
                    //       string.Format("Sql Exception with error number {0} (Tries={1})", x.Number, nTries), x);
                }
                ++nTries;
            }
        }

        /// <summary>
        /// The pmessage_archive.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        public static void pmessage_archive(int? mid, object userPMessageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserPMessageID", userPMessageID));

                sc.CommandText.AppendObjectQuery("pmessage_archive", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pmessage_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        public static void pmessage_delete(int? mid, object userPMessageID)
        {
            pmessage_delete(mid, userPMessageID, false);
        }

        /// <summary>
        /// The pmessage_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        /// <param name="fromOutbox">
        /// The from outbox.
        /// </param>      
        public static void pmessage_delete(int? mid, object userPMessageID, bool fromOutbox)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserPMessageID", userPMessageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FromOutbox", fromOutbox));

                sc.CommandText.AppendObjectQuery("pmessage_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pmessage_info.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>     
        public static DataTable pmessage_info(int? mid)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendObjectQuery("pmessage_info", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The pmessage_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable pmessage_list(int? mid, object userPMessageID)
        {
            return pmessage_list(mid, null, null, userPMessageID);
        }

        /// <summary>
        /// The pmessage_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="toUserID">
        /// The to user id.
        /// </param>
        /// <param name="fromUserID">
        /// The from user id.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable pmessage_list(int? mid, object toUserID, object fromUserID, object userPMessageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", fromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", toUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserPMessageID", userPMessageID));
                sc.CommandText.AppendObjectQuery("pmessage_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The pmessage_markread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        public static void pmessage_markread(int? mid, object userPMessageID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserPMessageID", userPMessageID));

                sc.CommandText.AppendObjectQuery("pmessage_markread", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pmessage_prune.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="daysRead">
        /// The days read.
        /// </param>
        /// <param name="daysUnread">
        /// The days unread.
        /// </param>     
        public static void pmessage_prune(int? mid, object daysRead, object daysUnread)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_DaysRead", daysRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_DaysUnread", daysUnread));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("pmessage_prune", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pmessage_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="fromUserID">
        /// The from user id.
        /// </param>
        /// <param name="toUserID">
        /// The to user id.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="Flags">
        /// The flags.
        /// </param>
        /// <param name="replyTo">
        /// The reply to.
        /// </param>      
        public static void pmessage_save(
            int? mid,
            object fromUserID,
            object toUserID,
            object subject,
            object body,
            object Flags,
            object replyTo)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", fromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ToUserID", toUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", subject));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Body", body));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", Flags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ReplyTo", replyTo));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("pmessage_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The poll_remove.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollGroupID">
        /// The poll group id.
        /// </param>
        /// <param name="pollID">
        /// The poll id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="removeCompletely">
        /// The remove completely.
        /// </param>
        /// <param name="removeEverywhere">
        /// The remove everywhere.
        /// </param>
        public static void poll_remove(
            int? mid,
            object pollGroupID,
            object pollID,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollID", pollID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_RemoveCompletely", removeCompletely));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_RemoveEverywhere", removeEverywhere));

                sc.CommandText.AppendObjectQuery("poll_remove", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The poll_save.
        /// </summary>
        /// <param name="pollList">
        /// The poll list.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>   
        public static int? poll_save(PollGroup pollList)
        {
            int? pollGroup = null;

            // Check if the group already exists or create it.
            using (var cmdPg = new VzfSqlCommand(pollList.mid))
            {
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_topicid", pollList.TopicId));
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_forumid", pollList.ForumId));
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_categoryid", pollList.CategoryId));
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_userid", pollList.UserId));
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.Int32, "i_flags", pollList.Flags.BitValue));
                cmdPg.Parameters.Add(cmdPg.CreateParameter(DbType.DateTime, "i_utctimestamp", DateTime.UtcNow));

                cmdPg.CommandText.AppendObjectQuery("pollgroup_save", pollList.mid);
                var dataTable = cmdPg.ExecuteDataTableFromReader(
                    CommandBehavior.Default,
                    CommandType.StoredProcedure,
                    true);

                if (dataTable.Rows[0]["PollGroupID"] != DBNull.Value)
                {
                    pollGroup = Convert.ToInt32(dataTable.Rows[0]["PollGroupID"]);
                }
                else if (dataTable.Rows[0]["NewPollGroupID"] != DBNull.Value)
                {
                    pollGroup = Convert.ToInt32(dataTable.Rows[0]["NewPollGroupID"]);
                }
            }

            pollList.Polls.ForEach(
                 poll =>
                    {
                        int? currPoll;
                        using (var cmd = new VzfSqlCommand(pollList.mid))
                        {
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_question", poll.Question));

                            if (poll.Closes > DateTimeHelper.SqlDbMinTime())
                            {
                                cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_closes", poll.Closes));
                            }
                            else
                            {
                                cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_closes", null));
                            }

                            cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_UserID", poll.UserId));
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroup));
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_ObjectPath", poll.ObjectPath));
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.String, "i_MimeType", poll.MimeType));
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.Int32, "i_Flags", poll.Flags.BitValue));
                            cmd.Parameters.Add(cmd.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                            cmd.CommandText.AppendObjectQuery("poll_save", pollList.mid);
                            currPoll = cmd.ExecuteScalar(CommandType.StoredProcedure, true).ToType<int>();
                        }

                        // The cycle through poll reply choices 
                        poll.Choices.ForEach(
                            (choice) =>
                                {
                                    if (choice.Choice.Length <= 0)
                                    {
                                        return;
                                    }

                                    using (var cmdChoice = new VzfSqlCommand(pollList.mid))
                                    {
                                        cmdChoice.Parameters.Add(
                                            cmdChoice.CreateParameter(DbType.Int32, "i_pollid", currPoll));
                                        cmdChoice.Parameters.Add(
                                            cmdChoice.CreateParameter(DbType.String, "i_choice", choice.Choice));
                                        cmdChoice.Parameters.Add(cmdChoice.CreateParameter(DbType.Int32, "i_votes", 0));
                                        cmdChoice.Parameters.Add(
                                            cmdChoice.CreateParameter(
                                                DbType.String,
                                                "i_objectpath",
                                                choice.ObjectPath.IsNotSet() ? string.Empty : choice.ObjectPath));
                                        cmdChoice.Parameters.Add(
                                            cmdChoice.CreateParameter(
                                                DbType.String,
                                                "i_mimetype",
                                                choice.MimeType.IsNotSet() ? string.Empty : choice.MimeType));
                                        cmdChoice.Parameters.Add(
                                            cmdChoice.CreateParameter(
                                                DbType.DateTime,
                                                "i_utctimestamp",
                                                DateTime.UtcNow));

                                        cmdChoice.CommandText.AppendObjectQuery("choice_save", pollList.mid);
                                        cmdChoice.ExecuteNonQuery(CommandType.StoredProcedure, true);
                                    }
                                });
                    });

            // add links to objects
            using (var cmd2 = new VzfSqlCommand(pollList.mid))
            {
                cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_topicid", pollList.TopicId));
                cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_forumid", pollList.ForumId));
                cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_categoryid", pollList.CategoryId));
                cmd2.Parameters.Add(cmd2.CreateParameter(DbType.Int32, "i_pollgroupid", pollGroup));
                cmd2.Parameters.Add(cmd2.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                cmd2.CommandText.AppendObjectQuery("pollgroup_setlinks", pollList.mid);
                cmd2.ExecuteNonQuery(CommandType.StoredProcedure, true);
            }

            return pollGroup;
        }

        /// <summary>
        /// The poll_stats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollId">
        /// The poll id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable poll_stats(int? mid, int? pollId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollID", pollId));

                sc.CommandText.AppendObjectQuery("poll_stats", mid);
                var dt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
                var dt_ret = new DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    DataColumn dc_ret;
                    if (dc.DataType == typeof(decimal))
                    {
                        dc_ret = new DataColumn(dc.ColumnName, typeof(int));
                    }
                    else
                    {
                        dc_ret = new DataColumn(dc.ColumnName, dc.DataType);
                    }

                    dt_ret.Columns.Add(dc_ret);
                }

                dt_ret.AcceptChanges();
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr_ret = dt_ret.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr_ret[dc.ColumnName] = dr[dc];

                    }

                    dt_ret.Rows.Add(dr_ret);
                }

                dt_ret.AcceptChanges();
                return dt_ret;
            }
        }

        /// <summary>
        /// The pollgroup_attach.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollGroupId">
        /// The poll group id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int pollgroup_attach(
            int? mid,
            int? pollGroupId,
            int? topicId,
            int? forumId,
            int? categoryId,
            int? boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("pollgroup_attach", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The pollgroup_remove.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollGroupID">
        /// The poll group id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="removeCompletely">
        /// The remove completely.
        /// </param>
        /// <param name="removeEverywhere">
        /// The remove everywhere.
        /// </param>
        public static void pollgroup_remove(
            int? mid,
            object pollGroupID,
            object topicId,
            object forumId,
            object categoryId,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_RemoveCompletely", removeCompletely));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_RemoveEverywhere", removeEverywhere));

                sc.CommandText.AppendObjectQuery("pollgroup_remove", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pollgroup_stats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollGroupId">
        /// The poll group id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable pollgroup_stats(int? mid, int? pollGroupId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroupId));

                sc.CommandText.AppendObjectQuery("pollgroup_stats", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The poll_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollID">
        /// The poll id.
        /// </param>
        /// <param name="question">
        /// The question.
        /// </param>
        /// <param name="closes">
        /// The closes.
        /// </param>
        /// <param name="isBounded">
        /// The is bounded.
        /// </param>
        /// <param name="isClosedBounded">
        /// The is closed bounded.
        /// </param>
        /// <param name="allowMultipleChoices">
        /// The allow multiple choices.
        /// </param>
        /// <param name="showVoters">
        /// The show voters.
        /// </param>
        /// <param name="allowSkipVote">
        /// The allow skip vote.
        /// </param>
        /// <param name="questionPath">
        /// The question path.
        /// </param>
        /// <param name="questionMime">
        /// The question mime.
        /// </param>     
        public static void poll_update(
            int? mid,
            object pollID,
            object question,
            object closes,
            object isBounded,
            bool isClosedBounded,
            bool allowMultipleChoices,
            bool showVoters,
            bool allowSkipVote,
            object questionPath,
            object questionMime)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollID", pollID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Question", question));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Closes", closes));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_QuestionObjectPath", questionPath));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_QuestionMimeType", questionMime));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsBounded", isBounded));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsClosedBounded", isClosedBounded));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AllowMultipleChoices", allowMultipleChoices));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowVoters", showVoters));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AllowSkipVote", allowSkipVote));

                sc.CommandText.AppendObjectQuery("poll_update", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The pollgroup_votecheck.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollGroupId">
        /// The poll group id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="remoteIp">
        /// The remote ip.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable pollgroup_votecheck(int? mid, object pollGroupId, object userId, object remoteIp)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PollGroupID", pollGroupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RemoteIP", remoteIp));

                sc.CommandText.AppendObjectQuery("pollgroup_votecheck", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The poll group list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>    
        public static IEnumerable<TypedPollGroup> PollGroupList(int? mid, int userID, int? forumId, int boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("pollgroup_list", mid);
                return
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                        .AsEnumerable()
                        .Select(r => new TypedPollGroup(r));
            }
        }

        /// <summary>
        /// The pollvote_check.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollid">
        /// The pollid.
        /// </param>
        /// <param name="userid">
        /// The userid.
        /// </param>
        /// <param name="remoteip">
        /// The remoteip.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        public static DataTable pollvote_check(int? mid, object pollid, object userid, object remoteip)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PolID", pollid));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userid));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RemoteIP", remoteip));

                sc.CommandText.AppendObjectQuery("pollvote_check", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The post_alluser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardid">
        /// The boardid.
        /// </param>
        /// <param name="userid">
        /// The userid.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="topCount">
        /// The top count.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable post_alluser(
            int? mid,
            object boardid,
            object userid,
            object pageUserID,
            object topCount)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardid));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userid));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumberOfMessages", topCount));

                sc.CommandText.AppendObjectQuery("post_alluser", mid);
                DataTable dt1 = sc.ExecuteDataTableFromReader(
                    CommandBehavior.Default,
                    CommandType.StoredProcedure,
                    true);
                foreach (DataRow dr in dt1.Rows)
                {
                    if (dr["ReadAccess"].ToString() == "0")
                    {
                        dr.Delete();
                    }

                }
                dt1.AcceptChanges();

                return dt1;
            }
        }

        /// <summary>
        /// The post_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="currentUserID">
        /// The current user id.
        /// </param>
        /// <param name="authoruserId">
        /// The authoruser id.
        /// </param>
        /// <param name="updateViewCount">
        /// The update view count.
        /// </param>
        /// <param name="showDeleted">
        /// The show deleted.
        /// </param>
        /// <param name="styledNicks">
        /// The styled nicks.
        /// </param>
        /// <param name="showReputation">
        /// The show reputation.
        /// </param>
        /// <param name="sincePostedDate">
        /// The since posted date.
        /// </param>
        /// <param name="toPostedDate">
        /// The to posted date.
        /// </param>
        /// <param name="sinceEditedDate">
        /// The since edited date.
        /// </param>
        /// <param name="toEditedDate">
        /// The to edited date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="sortPosted">
        /// The sort posted.
        /// </param>
        /// <param name="sortEdited">
        /// The sort edited.
        /// </param>
        /// <param name="sortPosition">
        /// The sort position.
        /// </param>
        /// <param name="showThanks">
        /// The show thanks.
        /// </param>
        /// <param name="messagePosition">
        /// The message position.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable post_list(
            int? mid,
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
            int messagePosition,
            int messageId,
            DateTime lastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (updateViewCount == null)
                {
                    updateViewCount = 1;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", currentUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AuthorUserID", authoruserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int16, "i_UpdateViewCount", updateViewCount));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowDeleted", showDeleted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowReputation", showReputation));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SincePostedDate", sincePostedDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToPostedDate", toPostedDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceEditedDate", sinceEditedDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToEditedDate", toEditedDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortPosted", sortPosted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortEdited", sortEdited));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortPosition", sortPosition));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowThanks", showThanks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessagePosition", messagePosition));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastRead", lastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("post_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The post_list_reverse 10.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable post_list_reverse10(int? mid, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                // sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("post_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The rank_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        public static void rank_delete(int? mid, object rankID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));

                sc.CommandText.AppendObjectQuery("rank_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The rank_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static IEnumerable<rank_list_Result> rank_list(int? mid, object boardId, object rankID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));

                sc.CommandText.AppendObjectQuery("rank_list", mid);
                return
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                        .AsEnumerable()
                        .Select(r => new rank_list_Result(r));
            }
        }

        public static DataTable rank_list(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", null));

                sc.CommandText.AppendObjectQuery("rank_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The rank_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="isStart">
        /// The is start.
        /// </param>
        /// <param name="isLadder">
        /// The is ladder.
        /// </param>
        /// <param name="isGuest">
        /// The is guest.
        /// </param>
        /// <param name="minPosts">
        /// The min posts.
        /// </param>
        /// <param name="rankImage">
        /// The rank image.
        /// </param>
        /// <param name="pmLimit">
        /// The pm limit.
        /// </param>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="usrSigChars">
        /// The usr sig chars.
        /// </param>
        /// <param name="usrSigBBCodes">
        /// The usr sig bb codes.
        /// </param>
        /// <param name="usrSigHTMLTags">
        /// The usr sig html tags.
        /// </param>
        /// <param name="usrAlbums">
        /// The usr albums.
        /// </param>
        /// <param name="usrAlbumImages">
        /// The usr album images.
        /// </param>
        public static void rank_save(
            int? mid,
            object rankID,
            object boardId,
            object name,
            object isStart,
            object isLadder,
            object isGuest,
            object minPosts,
            object rankImage,
            object pmLimit,
            object style,
            object sortOrder,
            object description,
            object usrSigChars,
            object usrSigBBCodes,
            object usrSigHTMLTags,
            object usrAlbums,
            object usrAlbumImages)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (minPosts.ToString() == string.Empty)
                {
                    minPosts = 0;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsStart", isStart));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsLadder", isLadder));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsGuest", isGuest));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MinPosts", minPosts));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RankImage", rankImage));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PMLimit", pmLimit));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Style", style));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrSigChars", usrSigChars));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UsrSigBBCodes", usrSigBBCodes));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UsrSigHTMLTags", usrSigHTMLTags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrAlbums", usrAlbums));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UsrAlbumImages", usrAlbumImages));

                sc.CommandText.AppendObjectQuery("rank_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The recent_users.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="timeSinceLastLogin">
        /// The time since last login.
        /// </param>
        /// <param name="styledNicks">
        /// The styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable recent_users(int? mid, object boardID, int timeSinceLastLogin, object styledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TimeSinceLastLogin", timeSinceLastLogin));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", styledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_utctimestamp", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("recent_users", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The registry_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable registry_list(int? mid)
        {
            return registry_list(mid, null, null);
        }

        /// <summary>
        /// The registry_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable registry_list(int? mid, object name)
        {
            return registry_list(mid, name, null);
        }

        /// <summary>
        /// The registry_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>    
        public static DataTable registry_list(int? mid, object name, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("registry_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="mid">
        /// The _mid.
        /// </param>
        /// <param name="name">
        /// Unique name associated with this entry.
        /// </param>
        /// <param name="value">
        /// Value associated with this entry which can be null.
        /// </param>
        public static void registry_save(int? mid, object name, object value)
        {
            registry_save(mid, name, value, DBNull.Value);
        }

        /// <summary>
        /// The registry_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void registry_save(int? mid, object name, object value, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Value", value));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("registry_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The replace_words_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public static void replace_words_delete(int? mid, object id)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", id));

                sc.CommandText.AppendObjectQuery("replace_words_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The replace_words_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        public static DataTable replace_words_list(int? mid, object boardId, object id)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", id));

                sc.CommandText.AppendObjectQuery("replace_words_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The replace_words_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="badword">
        /// The badword.
        /// </param>
        /// <param name="goodword">
        /// The goodword.
        /// </param>
        public static void replace_words_save(int? mid, object boardId, object id, object badword, object goodword)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ID", id));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BadWord", badword));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_GoodWord", goodword));

                sc.CommandText.AppendObjectQuery("replace_words_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The rss_topic_latest.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="numOfPostsToRetrieve">
        /// The num of posts to retrieve.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="showNoCountPosts">
        /// The show no count posts.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable rss_topic_latest(
            int? mid,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumPosts", numOfPostsToRetrieve));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowNoCountPosts", showNoCountPosts));

                sc.CommandText.AppendObjectQuery("rss_topic_latest", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The rsstopic_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="topicCount">
        /// The topic count.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable rsstopic_list(int? mid, int forumID, int topicCount)
        {
            return rsstopic_list(mid, forumID, 0, topicCount);
        }

        /// <summary>
        /// The rsstopic_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="topicStart">
        /// The topic start.
        /// </param>
        /// <param name="topicCount">
        /// The topic count.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>    
        public static DataTable rsstopic_list(int? mid, int forumID, int topicStart, int topicCount)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Start", topicStart));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", topicCount));

                sc.CommandText.AppendObjectQuery("rsstopic_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The set property values.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="appname">
        /// The appname.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="dirtyOnly">
        /// The dirty only.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void SetPropertyValues(
            int? mid,
            int boardId,
            string appname,
            string tableName,
            int userId,
            string userName,
            SettingsPropertyValueCollection collection,
            bool dirtyOnly = true)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            // guest should not be in profile
            int? userIdG = user_guest(mid, boardId);
            if (userId <= 0 || userIdG == userId || collection.Count < 1)
            {
                return;
            }

            bool itemsToSave = true;
            if (dirtyOnly)
            {
                itemsToSave = collection.Cast<SettingsPropertyValue>().Any(pp => pp.IsDirty);
            }

            // First make sure we have at least one item to save
            if (!itemsToSave)
            {
                return;
            }

            // load the data for the configuration
            List<SettingsPropertyColumn> spc = LoadFromPropertyValueCollection(mid, collection, tableName);

            // Save properties to database.
            if (spc == null || spc.Count <= 0 || !userName.IsSet())
            {
                return;
            }

            if (userName.IsNotSet())
            {
                return;
            }

            // check if profile exits to select insert or update
            bool profileExists;
            string profileExistsSql;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    profileExistsSql = MsProfile.ProfileExists;
                    break;
                case SqlDbAccess.Npgsql:
                    profileExistsSql = PgProfile.ProfileExists;
                    break;
                case SqlDbAccess.MySql:
                    profileExistsSql = MySqlProfile.ProfileExists;
                    break;
                case SqlDbAccess.Firebird:
                    profileExistsSql = FbProfile.ProfileExists;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appname));

                sc.CommandText.AppendQuery(profileExistsSql.FormatWith(SqlDbAccess.GetVzfObjectName(tableName, mid)));

                profileExists = Convert.ToInt32(sc.ExecuteScalar(CommandType.Text, true)) > 0;
            }

            // create data for saving profile properties
            string sql = string.Empty;

            var paramData = new List<Tuple<DbType, string, object>>();
            // Build up strings used in the query
            var columnStr = new StringBuilder();
            var valueStr = new StringBuilder();
            var setStr = new StringBuilder();

            spc.ForEach(
                (column) =>
                    {
                        if (collection[column.Settings.Name].IsDirty)
                        {

                            var nameParam = collection[column.Settings.Name].Name;
                            var valParam = collection[column.Settings.Name].PropertyValue;

                            nameParam = "@i_" + nameParam;

                            paramData.Add(new Tuple<DbType, string, object>(column.DataType, nameParam, valParam));

                            valueStr.Append(nameParam);
                            valueStr.Append(",");

                            columnStr.Append(column.Settings.Name);
                            columnStr.Append(",");

                            setStr.Append(column.Settings.Name);
                            setStr.Append("=");
                            setStr.Append(nameParam);
                            setStr.Append(",");
                        }
                    });

            // first predefined parameter without separator
            columnStr.Append("LastUpdatedDate ");
            valueStr.Append("@i_LastUpdatedDate");
            setStr.Append("LastUpdatedDate=@i_LastUpdatedDate");

            paramData.Add(new Tuple<DbType, string, object>(DbType.DateTime, "@i_LastUpdatedDate", DateTime.UtcNow));

            columnStr.Append(",LastActivity ");
            valueStr.Append(",@i_LastActivity");
            setStr.Append(",LastActivity=@i_LastActivity");

            paramData.Add(new Tuple<DbType, string, object>(DbType.DateTime, "@i_LastActivity", DateTime.UtcNow));

            columnStr.Append(",ApplicationName ");
            valueStr.Append(",@i_ApplicationName");
            setStr.Append(",ApplicationName=@i_ApplicationName");

            columnStr.Append(",IsAnonymous ");
            valueStr.Append(",@i_IsAnonymous");
            setStr.Append(",IsAnonymous=@i_IsAnonymous");

            paramData.Add(new Tuple<DbType, string, object>(DbType.Boolean, "@i_IsAnonymous", false));

            columnStr.Append(",UserName ");
            valueStr.Append(",@i_UserName");
            setStr.Append(",UserName=@i_UserName");

            paramData.Add(new Tuple<DbType, string, object>(DbType.String, "@i_UserName", userName));

            paramData.Add(new Tuple<DbType, string, object>(DbType.Int32, "@i_UserID", userId));

            paramData.Add(new Tuple<DbType, string, object>(DbType.String, "@i_ApplicationName", appname));

            // start saving...             
            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = MsProfile.SetProfileProperties(
                        setStr.ToString(),
                        columnStr.ToString(),
                        valueStr.ToString(),
                        profileExists);
                    break;
                case SqlDbAccess.Npgsql:
                    sql = PgProfile.SetProfileProperties(
                        setStr.ToString(),
                        columnStr.ToString(),
                        valueStr.ToString(),
                        profileExists);
                    break;
                case SqlDbAccess.MySql:
                    sql = MySqlProfile.SetProfileProperties(
                        setStr.ToString(),
                        columnStr.ToString(),
                        valueStr.ToString(),
                        profileExists);
                    break;
                case SqlDbAccess.Firebird:
                    sql = FbProfile.SetProfileProperties(
                        setStr.ToString(),
                        columnStr.ToString(),
                        valueStr.ToString(),
                        profileExists);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                // add parameters in a loop
                paramData.ForEach(
                    (parameter) => sc.Parameters.Add(sc.CreateParameter(parameter.Item1, parameter.Item2, parameter.Item3)));

                sc.CommandText.AppendQuery(sql.FormatWith(SqlDbAccess.GetVzfObjectName(tableName, mid)));
                sc.ExecuteNonQuery(CommandType.Text, true);
            }
        }

        /// <summary>
        /// The load from property value collection.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private static List<SettingsPropertyColumn> LoadFromPropertyValueCollection(
            [NotNull] int? mid,
            SettingsPropertyValueCollection collection,
            string tableName)
        {
            var settingsColumnsList = new List<SettingsPropertyColumn>();

            // clear it out just in case something is still in there...
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            // validiate all the properties and populate the internal settings collection           

            foreach (SettingsPropertyValue value in collection)
            {
                var tempProperty = value.Property.Attributes["CustomProviderData"];

                if (tempProperty == null)
                {
                    continue;
                }

                int size = -1;

                // split the data
                string[] chunk = tempProperty.ToString().Split(new[] { ';' });

                // parse custom provider data...
                switch (dataEngine)
                {
                    case SqlDbAccess.MsSql:
                        chunk = MsProfile.GetDbTypeAndSizeFromString(chunk);
                        break;
                    case SqlDbAccess.Npgsql:
                        chunk = PgProfile.GetDbTypeAndSizeFromString(chunk);
                        break;
                    case SqlDbAccess.MySql:
                        chunk = MySqlProfile.GetDbTypeAndSizeFromString(chunk);
                        break;
                    case SqlDbAccess.Firebird:
                        chunk = FbProfile.GetDbTypeAndSizeFromString(chunk);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(dataEngine);
                }

                // get the datatype and ignore case...
                DbType dbType = (DbType)Enum.Parse(typeof(DbType), chunk[1], true);

                if (chunk.Length > 2)
                {
                    // handle size...
                    if (!int.TryParse(chunk[2], out size))
                    {
                        throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
                    }
                }


                // default the size to 256 if no size is specified
                if (dbType == DbType.String && size == -1)
                {
                    size = 256;
                }

                settingsColumnsList.Add(new SettingsPropertyColumn(value.Property, dbType, size));
            }

            // getting current profile table colimns...
            DataTable structure = CommonDb.GetProfileStructure(SqlDbAccess.GetConnectionStringName(mid, string.Empty), Constants.SpecialObjectNames.UserProfileMirrorTable);
           
            string sqlAddColumn = string.Empty;

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in settingsColumnsList)
            {
                // see if this column exists and add it if required
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    string dataTypeName = column.DataType.ToString();

                    // if not, create it...
                    // parse custom provider data...
                    switch (dataEngine)
                    {
                        case SqlDbAccess.MsSql:
                            sqlAddColumn = MsProfile.AddProfileColumn(
                                column.Settings.Name,
                                dataTypeName,
                                column.Size,
                                SqlDbAccess.GetVzfObjectName(tableName, mid));
                            break;
                        case SqlDbAccess.Npgsql:
                            sqlAddColumn = PgProfile.AddProfileColumn(
                                column.Settings.Name,
                                dataTypeName,
                                column.Size,
                                SqlDbAccess.GetVzfObjectName(tableName, mid));
                            break;
                        case SqlDbAccess.MySql:
                            sqlAddColumn = MySqlProfile.AddProfileColumn(
                                column.Settings.Name,
                                dataTypeName,
                                column.Size,
                                SqlDbAccess.GetVzfObjectName(tableName, mid));
                            break;
                        case SqlDbAccess.Firebird:
                            sqlAddColumn = FbProfile.AddProfileColumn(
                                column.Settings.Name,
                                dataTypeName,
                                column.Size,
                                SqlDbAccess.GetVzfObjectName(tableName, mid));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(dataEngine);
                    }

                    if (sqlAddColumn.IsNotSet())
                    {
                        continue;
                    }

                    using (var sc = new VzfSqlCommand(mid))
                    {
                        sc.CommandText.AppendQuery(sqlAddColumn);
                        sc.ExecuteNonQuery(CommandType.Text, true);
                    }
                }
            }

            return settingsColumnsList;
        }

        /// <summary>
        /// The shoutbox_clearmessages.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool shoutbox_clearmessages(int? mid, int boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("shoutbox_clearmessages", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The shoutbox_getmessages.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="numberOfMessages">
        /// The number of messages.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable shoutbox_getmessages(int? mid, int boardId, int numberOfMessages, object useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumberOfMessages", numberOfMessages));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));

                sc.CommandText.AppendObjectQuery("shoutbox_getmessages", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The shoutbox_savemessage.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>   
        public static bool shoutbox_savemessage(
            int? mid,
            int boardId,
            string message,
            string userName,
            int userID,
            object ip)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Date", DBNull.Value));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_IP", ip));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("shoutbox_savemessage", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The smiley_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>    
        public static void smiley_delete(int? mid, object smileyID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmileyID", smileyID));

                sc.CommandText.AppendObjectQuery("smiley_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The smiley_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable smiley_list(int? mid, object boardId, object smileyID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmileyID", smileyID));

                sc.CommandText.AppendObjectQuery("smiley_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The smiley_listunique.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        public static DataTable smiley_listunique(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));

                sc.CommandText.AppendObjectQuery("smiley_listunique", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The smiley_resort.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>
        /// <param name="move">
        /// The move.
        /// </param>    
        public static void smiley_resort(int? mid, object boardId, object smileyID, int move)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmileyID", smileyID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Move", move));

                sc.CommandText.AppendObjectQuery("smiley_resort", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The smiley_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <param name="icon">
        /// The icon.
        /// </param>
        /// <param name="emoticon">
        /// The emoticon.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="replace">
        /// The replace.
        /// </param>
        public static void smiley_save(
            int? mid,
            object smileyID,
            object boardId,
            object code,
            object icon,
            object emoticon,
            object sortOrder,
            object replace)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (replace == null)
                {
                    replace = false;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SmileyID", smileyID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Code", code));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Icon", icon));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Emoticon", code));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Replace", replace));

                sc.CommandText.AppendObjectQuery("smiley_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The smiley list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>  
        public static IEnumerable<TypedSmileyList> SmileyList(int? mid, int boardId, int? smileyID)
        {
            return smiley_list(mid, boardId, smileyID).AsEnumerable().Select(r => new TypedSmileyList(r));
        }

        /// <summary>
        /// The system_deleteinstallobjects.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>   
        public static void system_deleteinstallobjects(int? mid)
        {
            // DROP PROCEDURE system_initialize           
        }

        /// <summary>
        /// The system_initialize.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumName">
        /// The forum name.
        /// </param>
        /// <param name="timeZone">
        /// The time zone.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="languageFile">
        /// The language file.
        /// </param>
        /// <param name="forumEmail">
        /// The forum email.
        /// </param>
        /// <param name="smtpServer">
        /// The smtp server.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userEmail">
        /// The user email.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <param name="rolePrefix">
        /// The role prefix.
        /// </param>   
        public static void system_initialize(
            int? mid,
            string forumName,
            string timeZone,
            string culture,
            string languageFile,
            string forumEmail,
            string smtpServer,
            string userName,
            string userEmail,
            object providerUserKey,
            string rolePrefix)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Name", forumName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TimeZone", timeZone));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Culture", culture));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_LanguageFile", languageFile));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ForumEmail", forumEmail));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_SmtpServer", smtpServer));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_User", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserEmail", userEmail));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserKey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_RolePrefix", rolePrefix));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("system_initialize", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The system_initialize_executescripts.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="script">
        /// The script.
        /// </param>
        /// <param name="scriptFile">
        /// The script file.
        /// </param>
        /// <param name="useTransactions">
        /// The use transactions.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void system_initialize_executescripts(
            int? mid,
            string script,
            string scriptFile,
            bool useTransactions)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
           
            string delimiter;
            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    delimiter = VZF.Data.MsSql.Db.SqlScriptsDelimiterRegexPattern;
                    break;
                case SqlDbAccess.Npgsql:
                    delimiter = VZF.Data.Postgre.Db.SqlScriptsDelimiterRegexPattern;
                    break;
                case SqlDbAccess.MySql:
                    delimiter = VZF.Data.Mysql.Db.SqlScriptsDelimiterRegexPattern;
                    break;
                case SqlDbAccess.Firebird:
                    delimiter = VZF.Data.Firebird.Db.SqlScriptsDelimiterRegexPattern;
                    break;
                    // case SqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions);break;
                    // case SqlDbAccess.Db2:   VZF.Data.Db2.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                    // case SqlDbAccess.Other:   VZF.Data.Other.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
           

            var options =
                SqlDbAccess.GetConnectionString(mid, string.Empty)
                    .Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(s => s.Split('=')[0].ToLowerInvariant(), s => s.Split('=')[1].ToLowerInvariant());
          
            if (Config.DatabaseSchemaName.IsSet())
            {
                script = Regex.Replace(script, "({databaseSchema})", Config.DatabaseSchemaName, RegexOptions.IgnoreCase);
            }
            else
            {
                script = Regex.Replace(script, "({databaseSchema})", "dbo", RegexOptions.IgnoreCase);
            }

            // used in postgre scripts
            if (Config.WithOIDs.IsSet())
            {
                script = Regex.Replace(script, "({withOIDs})", Config.WithOIDs, RegexOptions.IgnoreCase);
            }
            else
            {
                script = Regex.Replace(script, "({withOIDs})", "false", RegexOptions.IgnoreCase);
            }    

            // Detect charset used in mysql
            var chrset = options.FirstOrDefault(p => p.Key.Equals("charset") || p.Key.Equals("character set")).Value;
            if (chrset != null)
            {
                // TODO: some things here are not used
                string dbcharset = null;
                string dbcollation = null;

                // Verify if it's valid  
                dbcharset = CommonDb.db_checkvalidcharset(mid, chrset);

                if (Config.DatabaseCollation.Contains(dbcharset))
                {
                    dbcollation = Config.DatabaseCollation;
                }

                if (string.IsNullOrEmpty(dbcollation))
                {
                    // can be empty string!!!
                    dbcollation = CommonDb.db_defaultcollation(mid, dbcharset);
                }


                // No entry for encoding in connection string or app.config
                if (string.IsNullOrEmpty(dbcollation))
                {
                    dbcollation = CommonDb.db_getfirstcollation(mid);
                }

                if (string.IsNullOrEmpty(dbcharset))
                {
                    dbcharset = CommonDb.db_getfirstcharset(mid);
                }

                script = Regex.Replace(script, "({databaseCollation})", dbcollation, RegexOptions.IgnoreCase);
                script = Regex.Replace(script, "({databaseEncoding})", dbcharset, RegexOptions.IgnoreCase);
            }

            // eof charset for my sql
            script = Regex.Replace(script, "{objectQualifier}", Config.DatabaseObjectQualifier, RegexOptions.IgnoreCase);           

            // apply grantee name
            script = script.Replace(
                "grantName",
                !string.IsNullOrEmpty(Config.DatabaseGranteeName) ? Config.DatabaseGranteeName.ToUpper() : "PUBLIC");

            // apply host name
            script = script.Replace("hostName", Config.DatabaseHostName);

            var statements = Regex.Split(script, delimiter, RegexOptions.IgnoreCase).ToList();
            
            // Here comes add SET ARITHABORT ON for MSSQL amd Linq class
            // statements.Insert(0, "SET ARITHABORT ON");
            using (var cmd = new VzfSqlCommand(mid))
            {
                // use transactions...
                if (useTransactions)
                {
                    foreach (var sql in statements.Select(sql0 => sql0.Trim()))
                    {
                        try
                        {
                            if (sql.ToLower().IndexOf("setuser", StringComparison.Ordinal) >= 0)
                            {
                                continue;
                            }

                            if (sql.Length <= 0)
                            {
                                continue;
                            }

                            cmd.CommandText.Clear().AppendQuery(sql.Trim());
                            cmd.ExecuteNonQuery(CommandType.Text, true);
                        }
                        catch (Exception x)
                        {
                            throw new Exception(
                                string.Format(
                                    "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}",
                                    scriptFile,
                                    sql,
                                    x.Message));
                        }
                    }
                }
                else
                {
                    // don't use transactions
                    foreach (string sql in statements.Select(sql0 => sql0.Trim()))
                    {
                        try
                        {
                            if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                            {
                                continue;
                            }

                            if (sql.Length <= 0)
                            {
                                continue;
                            }

                            cmd.CommandText.Clear().AppendQuery(sql.Trim());
                            cmd.ExecuteNonQuery(CommandType.Text, false);
                        }
                        catch (Exception x)
                        {
                            throw new Exception(
                                string.Format(
                                    "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}",
                                    scriptFile,
                                    sql,
                                    x.Message));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The system_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        public static DataTable system_list(int? mid)
        {
            using (var sc = new VzfSqlCommand(mid))
            {

                sc.CommandText.AppendObjectQuery("system_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The system_updateversion.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>   
        public static void system_updateversion(int? mid, int version, string name)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Version", version));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_VersionName", name));

                sc.CommandText.AppendObjectQuery("system_updateversion", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The test connection.
        /// </summary>
        /// <param name="exceptionMessage">
        /// The exception message.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool TestConnection(
            [NotNull] out string exceptionMessage,
            string connectionString,
            string providerName)
        {
            exceptionMessage = null;

            using (var sc = new VzfSqlCommand(connectionString, providerName))
            {
                if (sc.DataSource != null)
                {
                    return true;
                }

                exceptionMessage = "Unable to connect to the Database.";

                return false;
            }
        }

        /// <summary>
        /// The topic_active.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_active(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastUnread", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_active", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_announcements.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="numOfPostsToRetrieve">
        /// The num of posts to retrieve.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_announcements(
            int? mid,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumPosts", numOfPostsToRetrieve));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));

                sc.CommandText.AppendObjectQuery("topic_announcements", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_unanswered.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_unanswered(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_unanswered", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_unread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_unread(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_unread", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topics_ by user.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable Topics_ByUser(
            int? mid,
            [NotNull] object boardId,
            [NotNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastUnread", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topics_byuser", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic status_ delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicStatusID">
        /// The topic status id.
        /// </param>
        public static void TopicStatus_Delete(int? mid, [NotNull] object topicStatusID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicStatusID", topicStatusID));

                sc.CommandText.AppendObjectQuery("topicstatus_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic status_ edit.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicStatusID">
        /// The topic status id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable TopicStatus_Edit(int? mid, [NotNull] object topicStatusID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicStatusID", topicStatusID));

                sc.CommandText.AppendObjectQuery("topicstatus_edit", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic status_ list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicStatusID">
        /// The topic status id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable TopicStatus_List(int? mid, [NotNull] object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));

                sc.CommandText.AppendObjectQuery("topicstatus_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic status_ save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicStatusID">
        /// The topic status id.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="topicStatusName">
        /// The topic status name.
        /// </param>
        /// <param name="defaultDescription">
        /// The default description.
        /// </param>      
        public static void TopicStatus_Save(
            int? mid,
            [NotNull] object topicStatusID,
            [NotNull] object boardID,
            [NotNull] object topicStatusName,
            [NotNull] object defaultDescription)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicStatusID", topicStatusID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_TopicStatusName", topicStatusName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DefaultDescription", defaultDescription));

                sc.CommandText.AppendObjectQuery("topicstatus_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_create_by_message.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="newTopicSubj">
        /// The new topic subj.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long topic_create_by_message(int? mid, object messageID, object forumId, object newTopicSubj)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", newTopicSubj));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_create_by_message", mid);
                return Convert.ToInt64(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The topic_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void topic_delete(int? mid, object topicID)
        {
            topic_delete(mid, topicID, null, false);
        }

        /// <summary>
        /// The topic_delete attachments.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        private static void topic_deleteAttachments([NotNull] int? mid, object topicID, bool eraseMessages )
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("topic_listmessages", mid);
                using (
                    DataTable dt = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        message_deleteRecursively(mid, row["MessageID"], true, string.Empty, 0, true, eraseMessages);
                    }
                }
            }
        }

        /// <summary>
        /// The topic_deleteimages.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        private static void topic_deleteimages([NotNull] int? mid, int topicID)
        {
            string uploadDir =
                HostingEnvironmentUtil.MapPath(
                    string.Concat(
                        BaseUrlBuilder.ServerFileRoot,
                        YafBoardFolders.Current.TopicAttachments));

            try
            {
                string topicImage = string.Empty;
                var dt = topic_info(mid, topicID, false, false);
                if (dt != null)
                {
                    topicImage = dt["TopicImage"].ToString();
                }

                string fileName = string.Format("{0}/{1}.{2}.yafupload", uploadDir, topicID, topicImage);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                string fileNameThumb = string.Format("{0}/{1}.thumb.{2}.yafupload", uploadDir, topicID, topicImage);
                if (System.IO.File.Exists(fileNameThumb))
                {
                    System.IO.File.Delete(fileNameThumb);
                }
            }
            catch
            {
                // error deleting that file... 
            }
        }


        /// <summary>
        /// The topic_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="eraseTopic">
        /// The erase topic.
        /// </param>     
        public static void topic_delete(int? mid, object topicID, object topicMovedID, bool eraseTopic)
        {
            if (eraseTopic == null)
            {
                eraseTopic = false;
            }

            if (eraseTopic.ToType<bool>())
            {
                topic_deleteAttachments(mid, topicID, eraseTopic);

                topic_deleteimages(mid, (int)topicID);
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicMovedID", topicMovedID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_EraseTopic", eraseTopic));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UpdateLastPost", true));

                sc.CommandText.AppendObjectQuery("topic_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_favorite_add.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void topic_favorite_add(int? mid, object userID, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("topic_favorite_add", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_favorite_details.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_favorite_details(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardId", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_CategoryID", categoryId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastUnread", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_favorite_details", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_favorite_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_favorite_list(int? mid, object userID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));

                sc.CommandText.AppendObjectQuery("topic_favorite_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_favorite_remove.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void topic_favorite_remove(int? mid, object userID, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("topic_favorite_remove", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_findduplicate.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicName">
        /// The topic name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int topic_findduplicate(int? mid, object topicName)
        {
            using (var sc = new VzfSqlCommand(mid))
            {

                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_TopicName", topicName));

                sc.CommandText.AppendObjectQuery("topic_findduplicate", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The topic_findnext.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_findnext(int? mid, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("topic_findnext", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_findprev.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_findprev(int? mid, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("topic_findprev", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_info.
        /// </summary>
        /// <param name="mid">
        ///     The mid.
        /// </param>
        /// <param name="topicID">
        ///     The topic id.
        /// </param>
        /// <param name="getTags">
        ///     The get tags.
        /// </param>
        /// <param name="showDeleted"></param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        public static DataRow topic_info(int? mid, object topicID, bool getTags, bool showDeleted)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowDeleted", false));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_GetTags", getTags));

                sc.CommandText.AppendObjectQuery("topic_info", mid);
                using (
                    DataTable dt = sc.ExecuteDataTableFromReader(
                        CommandBehavior.Default,
                        CommandType.StoredProcedure,
                        true))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                   
                    return null;
                    
                }
            }
        }

        /// <summary>
        /// The topic_imagesave.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="avatarImageType">
        /// The avatar image type.
        /// </param>
        public static void topic_imagesave(
            int? mid,
            object topicID,
            object imageUrl,
            Stream stream,
            object topicImageType)
        {

            using (var sc = new VzfSqlCommand(mid))
            {
                byte[] data = null;
                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ImageUrl", imageUrl));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "i_Stream", data));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_TopicImageType", topicImageType));

                sc.CommandText.AppendObjectQuery("topic_imagesave", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_latest.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="numOfPostsToRetrieve">
        /// The num of posts to retrieve.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="showNoCountPosts">
        /// The show no count posts.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_latest(
            int? mid,
            object boardID,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts,
            [CanBeNull] bool findLastRead)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumPosts", numOfPostsToRetrieve));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowNoCountPosts", showNoCountPosts));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastUnread", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_latest", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="showMoved">
        /// The show moved.
        /// </param>
        /// <param name="showDeleted">
        /// The show deleted.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable topic_list(int? mid, [NotNull] object forumID, [NotNull] object userId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [NotNull] object showMoved, bool showDeleted, [CanBeNull] bool findLastRead, [NotNull] bool getTags)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowMoved", showMoved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowDeleted", showDeleted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_GetTags", getTags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        public static DataTable announcements_list(int? mid, [NotNull] object forumID, [NotNull] object userId, [NotNull] object sinceDate, [NotNull] object toDate, [NotNull] object pageIndex, [NotNull] object pageSize, [NotNull] object useStyledNicks, [NotNull] object showMoved, bool showDeleted, [CanBeNull] bool findLastRead, [NotNull] bool getTags)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", sinceDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_ToDate", toDate));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowMoved", showMoved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowDeleted", showDeleted));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_FindLastRead", findLastRead));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_GetTags", getTags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("announcements_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_lock.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="locked">
        /// The locked.
        /// </param>
        public static void topic_lock(int? mid, object topicID, object locked)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Locked", locked));

                sc.CommandText.AppendObjectQuery("topic_lock", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_move.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="showMoved">
        /// The show moved.
        /// </param>
        /// <param name="linkDays">
        /// The link days.
        /// </param>
        public static void topic_move(int? mid, object topicID, object forumID, object showMoved, object linkDays)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowMoved", showMoved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_LinkDays", linkDays));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_move", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_prune.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="days">
        /// The days.
        /// </param>
        /// <param name="permDelete">
        /// The perm delete.
        /// </param>
        /// <param name="deletedOnly">
        /// The deleted only.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int topic_prune(
            int? mid,
            [NotNull] object boardID,
            [NotNull] object forumID,
            [NotNull] object days,
            [NotNull] object permDelete,
            bool deletedOnly)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Days", days));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DeletedOnly", deletedOnly));

                sc.CommandText.AppendObjectQuery("topic_prune", mid);
                var dataTable = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false);
                foreach (DataRow row in dataTable.Rows)
                {
                    topic_delete(mid, row[0].ToType<int?>(), null, (bool)permDelete);
                }

                return dataTable.Rows.Count;
            }
        }

        public static void topic_restore(int? mid, object topicID, int userID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID)); 

                sc.CommandText.AppendObjectQuery("topic_restore", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The topic_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="styles">
        /// The styles.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="posted">
        /// The posted.
        /// </param>
        /// <param name="blogPostID">
        /// The blog post id.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="messageDescription">
        /// The message description.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long topic_save(
            int? mid,
            object forumID,
            object subject,
            object status,
            object styles,
            object description,
            object message,
            object userId,
            object priority,
            object userName,
            object ip,
            object posted,
            object blogPostID,
            object flags,
            [CanBeNull] object messageDescription,
            ref long messageID,
            string tags)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Subject", subject));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_status", status));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Styles", styles));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Description", description));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int16, "i_Priority", priority));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_IP", ip));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Posted", posted));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_BlogPostID", blogPostID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", flags));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_MessageDescription", messageDescription));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Tags", tags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("topic_save", mid);
                var dt =
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false).Rows[0];
                messageID = Convert.ToInt32(dt["MessageID"]);
                return Convert.ToInt64(dt["TopicID"]);
            }
        }

        /// <summary>
        /// The topic_simplelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="StartID">
        /// The start id.
        /// </param>
        /// <param name="Limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        public static DataTable topic_simplelist(int? mid, int StartID, int Limit)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (StartID <= 0)
                {
                    StartID = 0;
                }
                if (Limit <= 0)
                {
                    Limit = 500;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_StartID", StartID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", Limit));

                sc.CommandText.AppendObjectQuery("topic_simplelist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_tags.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_tags(int? mid, int boardId, int pageUserId, int topicId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));

                sc.CommandText.AppendObjectQuery("topic_tags", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_bytags.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable topic_bytags(
            int? mid,
            int boardId,
            int forumId,
            object pageUserId,
            string tags,
            object date,
            int pageIndex,
            int pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Tags", tags));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_SinceDate", date));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("topic_bytags", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The topic_updatetopic.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="topic">
        /// The topic.
        /// </param>
        public static void topic_updatetopic(int? mid, int topicId, string topic)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Topic", topic));

                sc.CommandText.AppendObjectQuery("topic_updatetopic", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The unencode_all_topics_subjects.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="decodeTopicFunc">
        /// The decode topic func.
        /// </param>        
        public static void unencode_all_topics_subjects(int? mid, Func<string, string> decodeTopicFunc)
        {
            var topics =
                topic_simplelist(mid, 0, 99999999).AsEnumerable().Select(r => new TypedTopicSimpleList(r)).ToList();

            foreach (var topic in topics.Where(t => t.TopicID.HasValue && t.Topic.IsSet()))
            {
                try
                {
                    var decodedTopic = decodeTopicFunc(topic.Topic);

                    if (!decodedTopic.Equals(topic.Topic))
                    {
                        // unencode it and update.
                        topic_updatetopic(mid, topic.TopicID.Value, decodedTopic);
                    }
                }
                catch (Exception)
                {
                    // soft-fail...
                }
            }
        }

        /// <summary>
        /// The user_accessmasks.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_accessmasks(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", (int)userId));

                sc.CommandText.AppendObjectQuery("user_accessmasks", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_accessmasksbyforum.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_accessmasksbyforum(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", (int)userId));

                sc.CommandText.AppendObjectQuery("user_accessmasksbyforum", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_accessmasksbygroup.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_accessmasksbygroup(int? mid, object boardId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", (int)userId));

                sc.CommandText.AppendObjectQuery("user_accessmasksbygroup", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_activity_rank.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="displayNumber">
        /// The display number.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_activity_rank(int? mid, object boardId, object startDate, object displayNumber)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_DisplayNumber", displayNumber));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_StartDate", startDate));

                sc.CommandText.AppendObjectQuery("user_activity_rank", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_addignoreduser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="ignoredUserId">
        /// The ignored user id.
        /// </param>
        public static void user_addignoreduser(int? mid, object userId, object ignoredUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_IgnoredUserId", ignoredUserId));

                sc.CommandText.AppendObjectQuery("user_addignoreduser", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_addpoints.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumUserId">
        /// The forum user id.
        /// </param>
        /// <param name="points">
        /// The points.
        /// </param>
        public static void user_addpoints(int? mid, object userId, object forumUserId, object points)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", forumUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Points", points));

                sc.CommandText.AppendObjectQuery("user_addpoints", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_adminsave.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        public static void user_adminsave(
            int? mid,
            object boardId,
            object userId,
            object name,
            object displayName,
            object @email,
            object flags,
            object rankID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserName", name));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_DisplayName", displayName));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Email", @email));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Flags", flags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));

                sc.CommandText.AppendObjectQuery("user_adminsave", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_approve.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public static void user_approve(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_approve", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_approveall.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void user_approveall(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("user_approveall", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_aspnet.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <param name="isApproved">
        /// The is approved.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>       
        public static int user_aspnet(
            int? mid,
            int boardId,
            string userName,
            string displayName,
            string email,
            object providerUserKey,
            object isApproved)
        {
            try
            {
                using (var sc = new VzfSqlCommand(mid))
                {
                    sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DisplayName", displayName));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));
                    sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ProviderUserKey", providerUserKey));
                    sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsApproved", isApproved));
                    sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                    sc.CommandText.AppendObjectQuery("user_aspnet", mid);
                    return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
                }
            }
            catch (Exception x)
            {
                eventlog_create(mid, null, x.Source, x.StackTrace, EventLogTypes.Error);
                return 0;
            }
        }

        /// <summary>
        /// The user_avatarimage.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_avatarimage(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_avatarimage", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_changepassword.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="oldPassword">
        /// The old password.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns> 
        public static bool user_changepassword(int? mid, object userId, object oldPassword, object newPassword)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_OldPassword", oldPassword));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_NewPassword", newPassword));

                sc.CommandText.AppendObjectQuery("user_changepassword", mid);
                return Convert.ToBoolean(sc.ExecuteNonQuery(CommandType.StoredProcedure, true));
            }
        }

        /// <summary>
        /// The user_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param> 
        public static void user_delete(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_deleteavatar.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public static void user_deleteavatar(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_deleteavatar", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_deleteold.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="days">
        /// The days.
        /// </param>
        public static void user_deleteold(int? mid, object boardId, object days)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Days", days));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_deleteold", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_emails.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_emails(int? mid, object boardId, object groupID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));

                sc.CommandText.AppendObjectQuery("user_emails", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_get.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int user_get(int? mid, int boardId, object providerUserKey)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ProviderUserKey", providerUserKey));

                sc.CommandText.AppendObjectQuery("user_get", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_getalbumsdata.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_getalbumsdata(int? mid, object userID, object boardID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));

                sc.CommandText.AppendObjectQuery("user_getalbumsdata", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_getpoints.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int user_getpoints(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_getpoints", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_getsignature.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string user_getsignature(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_getsignature", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToString();
            }
        }

        /// <summary>
        /// The user_getsignaturedata.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_getsignaturedata(int? mid, object userID, object boardID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));

                sc.CommandText.AppendObjectQuery("user_getsignaturedata", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_getthanks_from.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int user_getthanks_from(int? mid, object userId, object pageUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));

                sc.CommandText.AppendObjectQuery("user_getthanks_from", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_getthanks_to.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <returns>
        /// The <see cref="int[]"/>.
        /// </returns>
        public static int[] user_getthanks_to(int? mid, object userID, object pageUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                var thton = sc.CreateParameter(DbType.Int32, "i_ThanksToNumber", null, ParameterDirection.Output);
                sc.Parameters.Add(thton);
                var thtop = sc.CreateParameter(DbType.Int32, "i_ThanksToPostsNumber", null, ParameterDirection.Output);
                sc.Parameters.Add(thtop);

                sc.CommandText.AppendObjectQuery("user_getthanks_to", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
                int thanksToPostsNumber, thanksToNumber;
                if (thton.Value == DBNull.Value)
                {
                    thanksToNumber = 0;
                    thanksToPostsNumber = 0;
                }
                else
                {
                    thanksToNumber = Convert.ToInt32(thton.Value);
                    thanksToPostsNumber = Convert.ToInt32(thtop.Value);
                }

                return new int[] { thanksToNumber, thanksToPostsNumber };
            }
        }

        /// <summary>
        /// The user_guest.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>
        public static int? user_guest(int? mid, object boardId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));

                sc.CommandText.AppendObjectQuery("user_guest", mid);
                return sc.ExecuteScalar(CommandType.StoredProcedure).ToType<int?>();
            }
        }

        /// <summary>
        /// The user_ignoredlist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_ignoredlist(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_ignoredlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_isuserignored.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="ignoredUserId">
        /// The ignored user id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool user_isuserignored(int? mid, object userId, object ignoredUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_IgnoredUserId", ignoredUserId));

                sc.CommandText.AppendObjectQuery("user_isuserignored", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_lazydata.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="showPendingMails">
        /// The show pending mails.
        /// </param>
        /// <param name="showPendingBuddies">
        /// The show pending buddies.
        /// </param>
        /// <param name="showUnreadPMs">
        /// The show unread p ms.
        /// </param>
        /// <param name="showUserAlbums">
        /// The show user albums.
        /// </param>
        /// <param name="styledNicks">
        /// The styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        public static DataRow user_lazydata(
            int? mid,
            object userID,
            object boardID,
            bool showPendingMails,
            bool showPendingBuddies,
            bool showUnreadPMs,
            bool showUserAlbums,
            bool styledNicks)
        {
            int nTries = 0;
            while (true)
            {
                try
                {
                    using (var sc = new VzfSqlCommand(mid))
                    {
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardID));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowPendingMails", showPendingMails));
                        sc.Parameters.Add(
                            sc.CreateParameter(DbType.Boolean, "i_ShowPendingBuddies", showPendingBuddies));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowUnreadPMs", showUnreadPMs));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowUserAlbums", showUserAlbums));
                        sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_ShowUserStyle", styledNicks));

                        sc.CommandText.AppendObjectQuery("user_lazydata", mid);
                        return
                            sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true)
                                .Rows[0];
                    }
                }
                catch (ArgumentOutOfRangeException xx)
                {
                    if (nTries < 3)
                    {
                        /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                    }
                    else
                        throw new ArgumentOutOfRangeException(
                            string.Format(
                                "Number of DataTable columns from DataReader cannot be null. Trys -{0}",
                                nTries),
                            xx);
                }
                /* catch (Exception x)
                 {
                    if (x.Number == 1213 && nTries < 3)
                     {
                         /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                     }
                     else
                         throw new ApplicationException(
                             string.Format("Sql Exception with error number {0} (Tries={1})", x.Number, nTries), x);
                 } */
                ++nTries;
            }
        }

        /// <summary>
        /// The user_list.
        /// </summary>
        /// <param name="mid"> </param>
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
        public static DataTable user_list(int? mid, object boardID, object userID, object approved)
        {
            return user_list(mid, boardID, userID, approved, null, null, false);
        }

        /// <summary>
        /// The user_list.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId"> </param>
        /// <param name="userId"> </param>
        /// <param name="approved">
        /// The approved.
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
        /// <param name="useStyledNicks">
        /// Return style info.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <summary>
        /// The user_list.
        /// </summary>
        /// <returns>
        /// </returns>
        public static DataTable user_list(
            int? mid,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Approved", approved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_pagedlist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
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
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns> 
        public static DataTable user_pagedlist(
            int? mid,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks,
            object pageIndex,
            object pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Approved", approved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_pagedlist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_listmedals.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_listmedals(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_listmedals", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_listmembers.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <param name="rankId">
        /// The rank id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="lastUserId">
        /// The last user id.
        /// </param>
        /// <param name="literals">
        /// The literals.
        /// </param>
        /// <param name="exclude">
        /// The exclude.
        /// </param>
        /// <param name="beginsWith">
        /// The begins with.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="sortName">
        /// The sort name.
        /// </param>
        /// <param name="sortRank">
        /// The sort rank.
        /// </param>
        /// <param name="sortJoined">
        /// The sort joined.
        /// </param>
        /// <param name="sortPosts">
        /// The sort posts.
        /// </param>
        /// <param name="sortLastVisit">
        /// The sort last visit.
        /// </param>
        /// <param name="numPosts">
        /// The num posts.
        /// </param>
        /// <param name="numPostCompare">
        /// The num post compare.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_listmembers(
            int? mid,
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
            using (var sc = new VzfSqlCommand(mid))
            {
                if (literals.Equals(char.MinValue.ToString(CultureInfo.InvariantCulture)))
                {
                    literals = null;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Approved", approved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_RankID", rankId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_StyledNicks", useStyledNicks));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Literals", literals));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Exclude", exclude));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_BeginsWith", beginsWith));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortName", sortName ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortRank", sortRank ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortJoined", sortJoined ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortPosts", sortPosts ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortLastVisit", sortLastVisit ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumPosts", numPosts ?? 0));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NumPostsCompare", numPostCompare ?? 0));

                sc.CommandText.AppendObjectQuery("user_listmembers", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_medal_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>   
        public static void user_medal_delete(int? mid, object userId, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));

                sc.CommandText.AppendObjectQuery("user_medal_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_medal_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="medalID">
        /// The medal id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_medal_list(int? mid, object userId, object medalID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_medal_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_medal_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="medalId">
        /// The medal id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="hide">
        /// The hide.
        /// </param>
        /// <param name="onlyRibbon">
        /// The only ribbon.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        /// <param name="dateAwarded">
        /// The date awarded.
        /// </param> 
        public static void user_medal_save(
            int? mid,
            object userId,
            object medalId,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder,
            object dateAwarded)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MedalID", medalId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Message", message));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Hide", hide));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_OnlyRibbon", onlyRibbon));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_SortOrder", sortOrder ?? (int)0));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_DateAwarded", dateAwarded));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_medal_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_migrate.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <param name="updateProvider">
        /// The update provider.
        /// </param>    
        public static void user_migrate(int? mid, object userId, object providerUserKey, object updateProvider)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ProviderUserKey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UpdateProvider", updateProvider));

                sc.CommandText.AppendObjectQuery("user_migrate", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_nntp.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="timeZone">
        /// The time zone.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>        
        public static int user_nntp(int? mid, object boardId, object userName, object email, int? timeZone)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", email));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TimeZone", timeZone));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_nntp", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_pmcount.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_pmcount(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_pmcount", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_removeignoreduser.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="ignoredUserId">
        /// The ignored user id.
        /// </param>      
        public static void user_removeignoreduser(int? mid, object userId, object ignoredUserId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_IgnoredUserId", ignoredUserId));

                sc.CommandText.AppendObjectQuery("user_removeignoreduser", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_removepoints.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="fromUserID">
        /// The from user id.
        /// </param>
        /// <param name="points">
        /// The points.
        /// </param>        
        public static void user_removepoints(int? mid, object userId, [CanBeNull] object fromUserID, object points)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_FromUserID", fromUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Points", points));

                sc.CommandText.AppendObjectQuery("user_removepoints", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_ replied topic.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>   
        public static bool user_RepliedTopic(int? mid, [NotNull] object messageId, [NotNull] object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_repliedtopic", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure)) > 0;
            }
        }

        /// <summary>
        /// The user_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="timeZone">
        /// The time zone.
        /// </param>
        /// <param name="languageFile">
        /// The language file.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="themeFile">
        /// The theme file.
        /// </param>
        /// <param name="useSingleSignOn">
        /// The use single sign on.
        /// </param>
        /// <param name="textEditor">
        /// The text editor.
        /// </param>
        /// <param name="overrideDefaultThemes">
        /// The override default themes.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="pmNotification">
        /// The pm notification.
        /// </param>
        /// <param name="autoWatchTopics">
        /// The auto watch topics.
        /// </param>
        /// <param name="dSTUser">
        /// The d st user.
        /// </param>
        /// <param name="isHidden">
        /// The is hidden.
        /// </param>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        /// <param name="topicsPerPage">
        /// The topics per page.
        /// </param>
        /// <param name="postsPerPage">
        /// The posts per page.
        /// </param>
        public static void user_save(
            int? mid,
            object userId,
            object boardId,
            object userName,
            object displayName,
            object @email,
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
            object notificationType,
            object topicsPerPage,
            object postsPerPage)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DisplayName", displayName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", @email));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TimeZone", timeZone));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_LanguageFile", languageFile));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Culture", culture));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ThemeFile", themeFile));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_UseSingleSignOn", useSingleSignOn));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_TextEditor", textEditor));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_OverrideDefaultTheme", overrideDefaultThemes));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Approved", approved));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_PMNotification", pmNotification));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NotificationType", notificationType));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ProviderUserKey", null));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AutoWatchTopics", autoWatchTopics));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DSTUser", dSTUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_HideUser", isHidden));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicsPerPage", topicsPerPage));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PostsPerPage", postsPerPage));

                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("user_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_saveavatar.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="avatar">
        /// The avatar.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="avatarImageType">
        /// The avatar image type.
        /// </param>   
        public static void user_saveavatar(
            int? mid,
            object userId,
            object avatar,
            Stream stream,
            object avatarImageType)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                byte[] data = null;

                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Avatar", avatar));
                sc.Parameters.Add(sc.CreateParameter(DbType.Binary, "i_AvatarImage", data));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_AvatarImageType", avatarImageType));

                sc.CommandText.AppendObjectQuery("user_saveavatar", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_savenotification.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pmNotification">
        /// The pm notification.
        /// </param>
        /// <param name="autoWatchTopics">
        /// The auto watch topics.
        /// </param>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        /// <param name="dailyDigest">
        /// The daily digest.
        /// </param>
        public static void user_savenotification(
            int? mid,
            object userId,
            object pmNotification,
            object autoWatchTopics,
            UserNotificationSetting notificationType,
            object dailyDigest)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_PMNotification", pmNotification));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_AutoWatchTopics", autoWatchTopics));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NotificationType", notificationType.ToInt()));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DailyDigest", dailyDigest));

                sc.CommandText.AppendObjectQuery("user_savenotification", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_savesignature.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="signature">
        /// The signature.
        /// </param>        
        public static void user_savesignature(int? mid, object userId, object signature)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Signature", signature));

                sc.CommandText.AppendObjectQuery("user_savesignature", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_setnotdirty.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public static void user_setnotdirty(int? mid, int boardId, int userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_setnotdirty", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_setpoints.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="points">
        /// The points.
        /// </param>
        public static void user_setpoints(int? mid, object userId, object points)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Points", points));

                sc.CommandText.AppendObjectQuery("user_setpoints", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_setrole.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="providerUserKey">
        /// The provider user key.
        /// </param>
        /// <param name="role">
        /// The role.
        /// </param>
        public static void user_setrole(int? mid, int boardId, object providerUserKey, object role)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ProviderUserKey", providerUserKey));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Role", role));

                sc.CommandText.AppendObjectQuery("user_setrole", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_simplelist.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="StartID">
        /// The start id.
        /// </param>
        /// <param name="Limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_simplelist(int? mid, int startID, int limit)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                if (startID <= 0)
                {
                    startID = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_StartID", startID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_Limit", limit));

                sc.CommandText.AppendObjectQuery("user_simplelist", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_suspend.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="suspend">
        /// The suspend.
        /// </param>      
        public static void user_suspend(int? mid, object userId, object suspend)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_Suspend", suspend));

                sc.CommandText.AppendObjectQuery("user_suspend", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_update_single_sign_on_status.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="isFacebookUser">
        /// The is facebook user.
        /// </param>
        /// <param name="isTwitterUser">
        /// The is twitter user.
        /// </param>
        public static void user_update_single_sign_on_status(
            int? mid,
            [NotNull] object userId,
            [NotNull] object isFacebookUser,
            [NotNull] object isTwitterUser)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsFacebookUser", isFacebookUser));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsTwitterUser", isTwitterUser));

                sc.CommandText.AppendObjectQuery("user_update_ssn_status", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user_ thanked message.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool user_ThankedMessage(int? mid, object messageId, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_MessageID", messageId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_thankedmessage", mid);
                return Convert.ToBoolean(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_ thank from count.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>  
        public static int user_ThankFromCount(int? mid, [NotNull] object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("user_thankfromcount", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The user_viewthanksfrom.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>        
        public static DataTable user_viewthanksfrom(
            int? mid,
            object UserID,
            object pageUserId,
            int pageIndex,
            int pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", UserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("user_viewthanksfrom", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user_viewthanksto.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable user_viewthanksto(
            int? mid,
            object UserID,
            object pageUserId,
            int pageIndex,
            int pageSize)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", UserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageIndex", pageIndex));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageSize", pageSize));

                sc.CommandText.AppendObjectQuery("user_viewthanksto", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The user find.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        /// <param name="dailyDigest">
        /// The daily digest.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>       
        public static IEnumerable<TypedUserFind> UserFind(
            int? mid,
            int boardId,
            bool filter,
            string userName,
            string @email,
            string displayName,
            object notificationType,
            object dailyDigest)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Filter", filter));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DisplayName", displayName));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_Email", @email));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_NotificationType", notificationType));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_DailyDigest", dailyDigest));

                sc.CommandText.AppendObjectQuery("user_find", mid);
                return
                        ProviderUserKeyHelper(
                        sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, false))
                        .AsEnumerable()
                        .Select(u => new TypedUserFind(u));
            }
        }

        /// <summary>
        /// The userforum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>     
        public static void userforum_delete(int? mid, object userId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("userforum_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The userforum_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>    
        public static DataTable userforum_list(int? mid, object userId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("userforum_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The userforum_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        public static void userforum_save(int? mid, object userId, object forumID, object accessMaskID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("userforum_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The usergroup_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable usergroup_list(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("usergroup_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The usergroup_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="member">
        /// The member.
        /// </param>       
        public static void usergroup_save(int? mid, object userId, object groupId, object member)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_GroupID", groupId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_Member", member));

                sc.CommandText.AppendObjectQuery("usergroup_save", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The user list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
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
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>   
        public static IEnumerable<TypedUserList> UserList(
            int? mid,
            int boardId,
            int? userId,
            bool? approved,
            int? groupID,
            int? rankID,
            bool? useStyledNicks)
        {
            return
                user_list(mid, boardId, userId, approved, groupID, rankID, useStyledNicks)
                    .AsEnumerable()
                    .Select(x => new TypedUserList(x));
        }

        /// <summary>
        /// The watchforum_add.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>        
        public static void watchforum_add(int? mid, object userId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("watchforum_add", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The watchforum_check.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable watchforum_check(int? mid, object userId, object forumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ForumID", forumID));

                sc.CommandText.AppendObjectQuery("watchforum_check", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The watchforum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="watchForumID">
        /// The watch forum id.
        /// </param>    
        public static void watchforum_delete(int? mid, object watchForumID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_WatchForumID", watchForumID));

                sc.CommandText.AppendObjectQuery("watchforum_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The watchforum_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable watchforum_list(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("watchforum_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The watchtopic_add.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void watchtopic_add(int? mid, object userId, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_UTCTIMESTAMP", DateTime.UtcNow));

                sc.CommandText.AppendObjectQuery("watchtopic_add", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The watchtopic_check.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable watchtopic_check(int? mid, object userId, object topicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_TopicID", topicID));

                sc.CommandText.AppendObjectQuery("watchtopic_check", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        /// <summary>
        /// The watchtopic_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="watchTopicID">
        /// The watch topic id.
        /// </param>
        public static void watchtopic_delete(int? mid, object watchTopicID)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_WatchTopicID", watchTopicID));

                sc.CommandText.AppendObjectQuery("watchtopic_delete", mid);
                sc.ExecuteNonQuery(CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// The watchtopic_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static DataTable watchtopic_list(int? mid, object userId)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));

                sc.CommandText.AppendObjectQuery("watchtopic_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
            }
        }

        // Properties

        /// <summary>
        /// The get db size.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetDbSize(int? mid)
        {
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DbScheme", null));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_DbName", null));

                sc.CommandText.AppendObjectQuery("db_size", mid);
                return Convert.ToInt32(sc.ExecuteScalar(CommandType.StoredProcedure));
            }
        }

        /// <summary>
        /// The get is forum installed.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetIsForumInstalled(int? mid)
        {
            try
            {
                // If boards don't exists in the table nothing was installed.
                using (var dt = board_list(mid, DBNull.Value))
                {
                    return dt.Rows.Count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The get db version.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>      
        public static int GetDBVersion(int? mid)
        {
            try
            {
                using (DataTable dt = registry_list(mid, "version", null))
                {
                    if (dt.Rows.Count > 0)
                    {
                        // get the version...
                        return dt.Rows[0]["Value"].ToType<int>();
                    }
                }
            }
            catch
            {
                // not installed...
            }

            return -1;
        }

        /// <summary>
        /// The get full text supported.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool GetFullTextSupported(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.FullTextSupported;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.FullTextSupported;
                case SqlDbAccess.MySql:
                    using (var sc = new VzfSqlCommand(mid))
                    {
                        sc.CommandText.AppendQuery("SELECT VERSION();");
                        var fullVersion = sc.ExecuteScalar(CommandType.Text).ToString();
                        var fullVersionArr = fullVersion.Trim().Split('.');
                        if (fullVersionArr[0].ToType<int>() >= 5 && fullVersionArr[1].ToType<int>() >= 6)
                        {
                            return true;
                        }
                    }

                    return false;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.FullTextSupported;
                    
                    // case SqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.fullTextSupported;;
                    // case SqlDbAccess.Db2:  return VZF.Data.Db2.Db.fullTextSupported;;
                    // case SqlDbAccess.Other:  return VZF.Data.Other.Db.fullTextSupported;; 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
        }

        /// <summary>
        /// The get full text script.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string GetFullTextScript(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.FullTextScript;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.FullTextScript;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.FullTextScript;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.FullTextScript;
                    // case SqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.fullTextScript;
                    // case SqlDbAccess.Db2:  return VZF.Data.Db2.Db.fullTextScript;
                    // case SqlDbAccess.Other:  return VZF.Data.Other.Db.fullTextScript; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        #region ConnectionStringOptions


        /// <summary>
        /// The connection parameters.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static List<ConnectionStringParameter> ConnectionParameters(int? mid)
        {          

            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ConnectionParameters;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ConnectionParameters;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ConnectionParameters;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ConnectionParameters;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        #endregion

        /// <summary>
        /// The get script list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string[] GetScriptList(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ScriptList;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ScriptList;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ScriptList;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ScriptList;
                    // case SqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.scriptList;
                    // case SqlDbAccess.Db2:  return VZF.Data.Db2.Db.scriptList;
                    // case SqlDbAccess.Other:  return VZF.Data.Other.Db.scriptList; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get panel get stats.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool GetPanelGetStats(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelGetStats;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelGetStats;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelGetStats;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelGetStats;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get panel recovery mode.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool GetPanelRecoveryMode(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelRecoveryMode;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelRecoveryMode;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelRecoveryMode;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelRecoveryMode;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get panel reindex.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool GetPanelReindex(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelReindex;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelReindex;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelReindex;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelReindex;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get panel shrink.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool GetPanelShrink(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelShrink;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelShrink;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelShrink;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelShrink;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get btn reindex visible.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool getBtnReindexVisible(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.btnReindexVisible;
                case SqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.btnReindexVisible;
                case SqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.btnReindexVisible;
                case SqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.btnReindexVisible;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get boolean registry value.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static  bool GetBooleanRegistryValue(int? mid, string name)
        {
            using (DataTable dt = registry_list(mid, name))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i;
                    return int.TryParse(dr["Value"].ToString(), out i)
                               ? Convert.ToBoolean(i)
                               : Convert.ToBoolean(dr["Value"]);
                }
            }

            return false;
        }

        /// <summary>
        /// The inner run sql execute reader.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="useTransaction">
        /// The use transaction.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string InnerRunSqlExecuteReader([NotNull] VzfSqlCommand command, bool useTransaction)
        {
            System.Data.Common.DbDataReader reader = null;
            var results = new StringBuilder();
            string messageRunSql = string.Empty;
            try
            {
                try
                {
                    /*  if (useTransaction)
                      {
                          command.BeginTransaction();
                      } */
                    reader = command.ExecuteReader();

                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            int rowIndex = 1;
                            var columnNames =
                                reader.GetSchemaTable()
                                    .Rows.Cast<DataRow>()
                                    .Select(r => r["ColumnName"].ToString())
                                    .ToList();

                            results.Append("RowNumber");

                            columnNames.ForEach(
                                n =>
                                    {
                                        results.Append(",");
                                        results.Append(n);
                                    });

                            results.AppendLine();

                            while (reader.Read())
                            {
                                results.AppendFormat(@"""{0}""", rowIndex++);

                                // dump all columns...
                                foreach (var col in columnNames)
                                {
                                    results.AppendFormat(@",""{0}""", reader[col].ToString().Replace("\"", "\"\""));
                                }

                                results.AppendLine();
                            }
                        }
                        else if (reader.RecordsAffected > 0)
                        {
                            results.AppendFormat("{0} Record(s) Affected", reader.RecordsAffected);
                            results.AppendLine();
                        }
                        else
                        {
                            if (messageRunSql.IsSet())
                            {
                                results.AppendLine(messageRunSql);
                                results.AppendLine();
                            }

                            results.AppendLine("No Results Returned.");
                        }

                        reader.Close();

                        if (command.Transaction != null)
                        {
                            command.Transaction.Commit();
                        }
                    }
                }
                finally
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
            }
            catch (Exception x)
            {
                if (reader != null)
                {
                    reader.Close();
                }

                results.AppendLine();
                results.AppendFormat("SQL ERROR: {0}", x);
            }

            return results.ToString();
        }

        /// <summary>
        /// The db_defaultcollation.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="dbcharset">
        /// The dbcharset.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_defaultcollation(int? mid, string dbcharset)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;
            string charsetColumn;
            string collationColumn;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_collations_data(out charsetColumn, out collationColumn);
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_collations_data(out charsetColumn, out collationColumn);
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_collations_data(out charsetColumn, out collationColumn);
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_collations_data(out charsetColumn, out collationColumn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return string.Empty;
            }

            string dbcollation = string.Empty;
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                DataTable dtt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);

                if (dtt.Rows.Count <= 0)
                {
                    return dbcollation;
                }

                foreach (DataRow dr in dtt.Rows)
                {
                    if (dr[charsetColumn].ToString() == dbcharset)
                    {
                        dbcollation = dr[collationColumn].ToString();
                    }
                }

                return dbcollation;
            }
        }

        /// <summary>
        /// The db_checkvalidcharset.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="dbcharset">
        /// The dbcharset.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_checkvalidcharset(int? mid, string dbcharset)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;
            string charsetColumn;
            string value;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_checkvalidcharset(out charsetColumn, out value);
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_checkvalidcharset(out charsetColumn, out value);
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_checkvalidcharset(out charsetColumn, out value);
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_checkvalidcharset(out charsetColumn, out value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return string.Empty;
            }

            string dbcollation = string.Empty;
            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                DataTable dtt = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);

                if (dtt.Rows.Count <= 0)
                {
                    return dbcollation;
                }

                foreach (DataRow dr in dtt.Rows)
                {
                    if (dr[charsetColumn].ToString() == dbcharset)
                    {
                        dbcollation = dr[value].ToString();
                    }
                }

                return dbcollation;
            }
        }

        /// <summary>
        /// The db_getfirstcharset.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_getfirstcharset(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_getfirstcharset();
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_getfirstcharset();
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_getfirstcharset();
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_getfirstcharset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return string.Empty;
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                string dbcharset =
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true).Rows[0]["Value"]
                        .ToString();

                return dbcharset;
            }
        }

        /// <summary>
        /// The db_getfirstcollation.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_getfirstcollation(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_getfirstcollation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return string.Empty;
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                string dbcollation =
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true).Rows[0]["Value"]
                        .ToString();

                return dbcollation;
            }
        }

        public static string db_allowsSchemaName(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            SqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            string sql;

            switch (dataEngine)
            {
                case SqlDbAccess.MsSql:
                    sql = VZF.Data.MsSql.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.Npgsql:
                    sql = VZF.Data.Postgre.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.MySql:
                    sql = VZF.Data.Mysql.Db.db_getfirstcollation();
                    break;
                case SqlDbAccess.Firebird:
                    sql = VZF.Data.Firebird.Db.db_getfirstcollation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            if (!sql.IsSet())
            {
                return string.Empty;
            }

            using (var sc = new VzfSqlCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                string dbcollation =
                    sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true).Rows[0]["Value"]
                        .ToString();

                return dbcollation;
            }
        }

        public static DataTable GetProfileStructure(string connectionStringName, string tableName)
        { 
            using (var sc = new VzfSqlCommand(connectionStringName))
            {
                // works with all 4 databases - used because of troubles with TableDirect for Firebird.
                // number of rows doesn't matter - we are retrieving schema only.
                string sql = "select * from {0};".FormatWith(SqlDbAccess.GetVzfObjectNameFromConnectionString(tableName, connectionStringName));
                // sql = sql.FormatWith(SqlDbAccess.GetVzfObjectNameFromConnectionString(tableName, connectionStringName));
                sc.CommandText.AppendQuery(sql);
                return sc.ExecuteDataTableFromReader(CommandBehavior.SchemaOnly, CommandType.Text, false);
              
                // sc.CommandText.AppendQuery(string.Format(SqlDbAccess.GetVzfObjectNameFromConnectionString(tableName, connectionStringName)));
               //  return sc.ExecuteDataTableFromReader(CommandBehavior.SchemaOnly, CommandType.TableDirect, false);
            }          
        }

        /// <summary>
        /// The provider user key helper.
        /// </summary>
        /// <param name="dt">
        /// The dt.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        private static DataTable ProviderUserKeyHelper(DataTable dt)
        {
            var dataTableToCopy = dt.Clone();

            foreach (DataRow dataRow in dt.Rows)
            {
                var dataRowToCopy = dataTableToCopy.NewRow();

                if (!Convert.ToBoolean(dataRow["IsGuest"]))
                {
                    ObjectExtensions.ConvertObjectToType(dataRow["ProviderUserKey"], Config.ProviderKeyType);
                    dataRow.AcceptChanges();
                }

                dataRowToCopy.ItemArray = dataRow.ItemArray;
                dataTableToCopy.Rows.Add(dataRowToCopy);
            }

            dataTableToCopy.AcceptChanges();
            return dataTableToCopy;
        }
    }
}