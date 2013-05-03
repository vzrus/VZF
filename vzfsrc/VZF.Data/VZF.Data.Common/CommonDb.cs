// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="CommonDb.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
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
    using System.Web.Security;

    using VZF.Data.Utils;
    using VZF.Types.Data;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Objects;

    /// <summary>
    /// The common DB.
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.accessmask_delete(connectionString, accessMaskID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.accessmask_delete(connectionString, accessMaskID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.accessmask_delete(connectionString, accessMaskID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.accessmask_delete(connectionString, accessMaskID);
                    // case CommonSqlDbAccess.Oracle: return OracleLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case CommonSqlDbAccess.Db2: return Db2LegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case CommonSqlDbAccess.Other: return OtherLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="mid">
        /// The module id.
        /// </param>
        /// <param name="boardId">
        /// ID of Board
        /// </param>
        /// <param name="accessMaskID">
        /// ID of access mask
        /// </param>
        /// <returns> A <see cref="T:System.Data.DataTable"/> of Access Masks. </returns>
        public static DataTable accessmask_list(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.accessmask_list(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.accessmask_list(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.accessmask_list(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.accessmask_list(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Oracle: VZF.Data.Oracle.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Other: otherPostgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="mid">
        /// The module id.
        /// </param>
        /// <param name="boardId">
        /// ID of Board
        /// </param>
        /// <param name="accessMaskID">
        /// ID of access mask
        /// </param>
        /// <returns> A <see cref="T:System.Data.DataTable"/> of Access Masks. </returns>
        public static DataTable accessmask_pforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.accessmask_pforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.accessmask_pforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.accessmask_pforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.accessmask_pforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Oracle: VZF.Data.Oracle.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Other: otherPostgre.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable accessmask_aforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.accessmask_aforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.accessmask_aforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.accessmask_aforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.accessmask_aforumlist(
                        connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Oracle: VZF.Data.Oracle.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                    // case CommonSqlDbAccess.Other: otherPostgre.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Saves changes to access mask.
        /// </summary>
        /// <param name="mid">
        /// The module id.
        /// </param>
        /// <param name="accessMaskId">
        /// The access mask ID.
        /// </param>
        /// <param name="boardId">
        /// The board ID.
        /// </param>
        /// <param name="name">
        /// The access mask name.
        /// </param>
        /// <param name="readAccess">
        /// The post read access.
        /// </param>
        /// <param name="postAccess">
        /// The post write access.
        /// </param>
        /// <param name="replyAccess">
        /// The reply topic access.
        /// </param>
        /// <param name="priorityAccess">
        /// The priority access.
        /// </param>
        /// <param name="pollAccess">
        /// The poll create access.
        /// </param>
        /// <param name="voteAccess">
        /// The vote access.
        /// </param>
        /// <param name="moderatorAccess">
        /// The moderator access.
        /// </param>
        /// <param name="editAccess">
        /// The topic edit access.
        /// </param>
        /// <param name="deleteAccess">
        /// The topic delete access.
        /// </param>
        /// <param name="uploadAccess">
        /// The attachments upload access.
        /// </param>
        /// <param name="downloadAccess">
        /// The attachments download access.
        /// </param>
        /// <param name="sortOrder">
        /// The access mask sort order.
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.accessmask_save(
                        connectionString,
                        accessMaskId,
                        boardId,
                        name,
                        readAccess,
                        postAccess,
                        replyAccess,
                        priorityAccess,
                        pollAccess,
                        voteAccess,
                        moderatorAccess,
                        editAccess,
                        deleteAccess,
                        uploadAccess,
                        downloadAccess,
                        userForumAccess,
                        sortOrder,
                        userId,
                        isUserMask,
                        isAdminMask);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.accessmask_save(
                        connectionString,
                        accessMaskId,
                        boardId,
                        name,
                        readAccess,
                        postAccess,
                        replyAccess,
                        priorityAccess,
                        pollAccess,
                        voteAccess,
                        moderatorAccess,
                        editAccess,
                        deleteAccess,
                        uploadAccess,
                        downloadAccess,
                        userForumAccess,
                        sortOrder,
                        userId,
                        isUserMask,
                        isAdminMask);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.accessmask_save(
                        connectionString,
                        accessMaskId,
                        boardId,
                        name,
                        readAccess,
                        postAccess,
                        replyAccess,
                        priorityAccess,
                        pollAccess,
                        voteAccess,
                        moderatorAccess,
                        editAccess,
                        deleteAccess,
                        uploadAccess,
                        downloadAccess,
                        userForumAccess,
                        sortOrder,
                        userId,
                        isUserMask,
                        isAdminMask);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.accessmask_save(
                        connectionString,
                        accessMaskId,
                        boardId,
                        name,
                        readAccess,
                        postAccess,
                        replyAccess,
                        priorityAccess,
                        pollAccess,
                        voteAccess,
                        moderatorAccess,
                        editAccess,
                        deleteAccess,
                        uploadAccess,
                        downloadAccess,
                        userForumAccess,
                        sortOrder,
                        userId,
                        isUserMask,
                        isAdminMask);
                    break;
                    // case CommonSqlDbAccess.Oracle: VZF.Data.Oracle.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, userForumAccess,sortOrder,userId,isUserMask,isAdminMask);break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess,userForumAccess, sortOrder,userId,isUserMask,isAdminMask);break;
                    // case CommonSqlDbAccess.Other: otherPostgre.Db.accessmask_saveaccessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess,userForumAccess, sortOrder,userId,isUserMask,isAdminMask);break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, object boardId, object guests, object showCrawlers, int interval, object styledNicks)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.active_listforum(connectionString, forumID, styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.active_listforum(connectionString, forumID, styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.active_listforum(connectionString, forumID, styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.active_listforum(connectionString, forumID, styledNicks);
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.active_listforum(connectionString,  forumID, styledNicks);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.active_listforum(connectionString,  forumID, styledNicks);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.active_listforum(connectionString,  forumID, styledNicks);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.active_listtopic(connectionString, topicID, styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.active_listtopic(connectionString, topicID, styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.active_listtopic(connectionString, topicID, styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.active_listtopic(connectionString, topicID, styledNicks);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.active_stats(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.active_stats(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.active_stats(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.active_stats(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.active_stats(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.active_stats(connectionString, boardId);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.active_stats(connectionString, boardId);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.activeaccess_reset(connectionString);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.activeaccess_reset(connectionString);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.activeaccess_reset(connectionString);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.activeaccess_reset(connectionString);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.activeaccess_reset(connectionString); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.activeaccess_reset(connectionString); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.activeaccess_reset(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// List user profile by a comma-delimited ID list input.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="boardID">
        /// The board ID.
        /// </param>
        /// <param name="userIdsList">
        /// A comma-delimited id list.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return styled nicks string.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with profiles info. 
        /// </returns>
        public static DataTable User_ListProfilesByIdsList(
            int? mid, int boardID, [NotNull] int[] userIdsList, [CanBeNull] object useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);  
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);
            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, [CanBeNull] object boardId, [NotNull] object useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.admin_pageaccesslist(connectionString, (int)boardId, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.adminpageaccess_list(connectionString, userId, pageName);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.adminpageaccess_list(connectionString, userId, pageName);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.adminpageaccess_list(connectionString, userId, pageName);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.adminpageaccess_list(connectionString, userId, pageName);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.adminpageaccess_list(connectionString, userId, pageName); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.adminpageaccess_list(connectionString, userId, pageName);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.adminpageaccess_list(connectionString, userId, pageName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;

                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.album_delete(connectionString, AlbumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.album_delete(connectionString, AlbumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.album_delete(connectionString, AlbumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.album_delete(connectionString, AlbumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.album_delete(connectionString, AlbumID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.album_delete(connectionString, AlbumID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.album_delete(connectionString, AlbumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_getstats(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_getstats(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_getstats(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_getstats(connectionString, UserID, AlbumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_getstats(connectionString,  UserID,  AlbumID);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.album_getstats(connectionString,  UserID,  AlbumID);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.album_getstats(connectionString,  UserID,  AlbumID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Gets a title for an album with specified id. 
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.String"/> with album title.
        /// </returns>
        public static string album_gettitle(int? mid, object AlbumID)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_gettitle(connectionString, AlbumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_gettitle(connectionString, AlbumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_gettitle(connectionString, AlbumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_gettitle(connectionString, AlbumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_gettitle(connectionString, AlbumID);
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.album_gettitle(connectionString, AlbumID);
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.album_gettitle(connectionString, AlbumID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Deletes an image with a specified Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="ImageID">
        /// The image id.
        /// </param>
        public static void album_image_delete(int? mid, object ImageID)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.album_image_delete(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.album_image_delete(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.album_image_delete(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.album_image_delete(connectionString, ImageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.album_image_delete(connectionString, ImageID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.album_image_delete(connectionString, ImageID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.album_image_delete(connectionString, ImageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Saved downloaded image.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="ImageID">
        /// The image id.
        /// </param>
        public static void album_image_download(int? mid, object ImageID)
        {
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.album_image_download(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.album_image_download(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.album_image_download(connectionString, ImageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.album_image_download(connectionString, ImageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.album_image_download(connectionString, ImageID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.album_image_download(connectionString, ImageID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.album_image_download(connectionString, ImageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// All album images for a user.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// Returns a <see cref="T:System.Data.DataTable"/> with album images for a user.
        /// </returns>
        public static DataTable album_images_by_user(int? mid, [NotNull] object userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_images_by_user(connectionString, userID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_images_by_user(connectionString, userID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_images_by_user(connectionString, userID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_images_by_user(connectionString, userID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_images_by_user(connectionString, userID); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.album_images_by_user(connectionString, userID); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.album_images_by_user(connectionString, userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_image_list(connectionString, AlbumID, ImageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_image_list(connectionString, AlbumID, ImageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_image_list(connectionString, AlbumID, ImageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_image_list(connectionString, AlbumID, ImageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_image_list(connectionString, AlbumID, ImageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.album_image_list(connectionString, AlbumID, ImageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
            }
        }

        /// <summary>
        /// Saves an image in an album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="ImageID">
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
            int? mid, object ImageID, object AlbumID, object Caption, object FileName, object Bytes, object ContentType)
        {
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_list(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_list(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_list(connectionString, UserID, AlbumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_list(connectionString, UserID, AlbumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_list(connectionString, UserID,  AlbumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.album_list(connectionString, UserID,  AlbumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.album_list(connectionString, UserID,  AlbumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.album_list(connectionString, UserID,  AlbumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.attachment_delete(connectionString, attachmentID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.attachment_delete(connectionString, attachmentID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.attachment_delete(connectionString, attachmentID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.attachment_delete(connectionString, attachmentID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Download an attachment with an Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="attachmentID">
        /// The attachmentId
        /// </param>
        public static void attachment_download(int? mid, object attachmentID)
        {
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.attachment_delete(connectionString, attachmentID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.attachment_delete(connectionString, attachmentID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.attachment_delete(connectionString, attachmentID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.attachment_delete(connectionString, attachmentID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, object messageID, object attachmentID, object boardId, object pageIndex, object pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, object messageID, object fileName, object bytes, object contentType, Stream stream)
        {
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.attachment_save(connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.attachment_save(
                        connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.attachment_save(connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.attachment_save(
                        connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.bannedip_delete(connectionString, ID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.bannedip_delete(connectionString, ID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.bannedip_delete(connectionString, ID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.bannedip_delete(connectionString, ID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.bannedip_delete(connectionString, ID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.bannedip_delete(connectionString, ID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.bannedip_delete(connectionString, ID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, object boardId, object ID, [CanBeNull] object pageIndex, [CanBeNull] object pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static void bannedip_save(int? mid, object ID, object boardId, object Mask, string reason, int userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The bbcode_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bbcodeID">
        /// The bbcode id.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static void bbcode_delete(int? mid, object bbcodeID)
        {
            string dataEngine;
            string namePattern = string.Empty;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.bbcode_delete(connectionString, bbcodeID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.bbcode_delete(connectionString, bbcodeID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.bbcode_delete(connectionString, bbcodeID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/> with list of bbcodes.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// /// The ArgumentOutOfRangeException.
        /// </exception>
        public static DataTable bbcode_list(int? mid, object boardId, object bbcodeID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.bbcode_list(connectionString, boardId, bbcodeID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.bbcode_list( connectionString,  boardId,  bbcodeID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The bbcode_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bbcodeID">
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static void bbcode_save(
            int? mid,
            object bbcodeID,
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.bbcode_save(
                        connectionString,
                        bbcodeID,
                        boardId,
                        name,
                        description,
                        onclickjs,
                        displayjs,
                        editjs,
                        displaycss,
                        searchregex,
                        replaceregex,
                        variables,
                        usemodule,
                        moduleclass,
                        execorder);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.bbcode_save(
                        connectionString,
                        bbcodeID,
                        boardId,
                        name,
                        description,
                        onclickjs,
                        displayjs,
                        editjs,
                        displaycss,
                        searchregex,
                        replaceregex,
                        variables,
                        usemodule,
                        moduleclass,
                        execorder);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.bbcode_save(
                        connectionString,
                        bbcodeID,
                        boardId,
                        name,
                        description,
                        onclickjs,
                        displayjs,
                        editjs,
                        displaycss,
                        searchregex,
                        replaceregex,
                        variables,
                        usemodule,
                        moduleclass,
                        execorder);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.bbcode_save(
                        connectionString,
                        bbcodeID,
                        boardId,
                        name,
                        description,
                        onclickjs,
                        displayjs,
                        editjs,
                        displaycss,
                        searchregex,
                        replaceregex,
                        variables,
                        usemodule,
                        moduleclass,
                        execorder);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
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
        /// The <see cref="IEnumerable"/> of BBCode list.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static IEnumerable<TypedBBCode> BBCodeList(int? mid, int boardId, int? bbcodeID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.BBCodeList(connectionString, boardId, bbcodeID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.board_create(
                        connectionString,
                        adminUsername,
                        adminUserEmail,
                        adminUserKey,
                        boardName,
                        culture,
                        languageFile,
                        boardMembershipName,
                        boardRolesName,
                        rolePrefix,
                        isHostUser);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.board_create(
                        connectionString,
                        adminUsername,
                        adminUserEmail,
                        adminUserKey,
                        boardName,
                        culture,
                        languageFile,
                        boardMembershipName,
                        boardRolesName,
                        rolePrefix,
                        isHostUser);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.board_create(
                        connectionString,
                        adminUsername,
                        adminUserEmail,
                        adminUserKey,
                        boardName,
                        culture,
                        languageFile,
                        boardMembershipName,
                        boardRolesName,
                        rolePrefix,
                        isHostUser);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.board_create(
                        connectionString,
                        adminUsername,
                        adminUserEmail,
                        adminUserKey,
                        boardName,
                        culture,
                        languageFile,
                        boardMembershipName,
                        boardRolesName,
                        rolePrefix,
                        isHostUser);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static void board_delete(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.board_delete(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.board_delete(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.board_delete(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.board_delete(connectionString, boardId);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.board_delete(connectionString, boardId); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.board_delete(connectionString, boardId); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.board_delete(connectionString, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/> of the board list.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static DataTable board_list(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.board_list(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.board_list(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.board_list(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.board_list(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_list(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_list(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_list(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static board_poststats_Result board_poststats(
            int? mid, int? boardId, bool useStyledNicks, bool showNoCountPosts)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return
                        VZF.Data.MsSql.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                    ;
                case CommonSqlDbAccess.Npgsql:
                    return
                        VZF.Data.Postgre.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                case CommonSqlDbAccess.MySql:
                    return
                        VZF.Data.Mysql.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                case CommonSqlDbAccess.Firebird:
                    return
                        VZF.Data.Firebird.Db.board_poststats(
                            connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException. 
        /// </exception>
        public static void board_resync(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.board_resync(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.board_resync(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.board_resync(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.board_resync(connectionString, boardId);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.board_resync(connectionString, boardId); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.board_resync(connectionString, boardId); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.board_resync(connectionString, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static int board_save(
            int? mid, object boardId, object languageFile, object culture, object name, object allowThreaded)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static board_stats_Result board_stats(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return
                        VZF.Data.MsSql.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case CommonSqlDbAccess.Npgsql:
                    return
                        VZF.Data.Postgre.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case CommonSqlDbAccess.MySql:
                    return
                        VZF.Data.Mysql.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case CommonSqlDbAccess.Firebird:
                    return
                        VZF.Data.Firebird.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_stats(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_stats(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_stats(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static DataRow board_userstats(int? mid, int? boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.board_userstats(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.board_userstats(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.board_userstats(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.board_userstats(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.board_userstats(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.board_userstats(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.board_userstats(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="string[]"/> of buddies.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static string[] buddy_addrequest(int? mid, object FromUserID, object ToUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static string buddy_approveRequest(int? mid, object FromUserID, object ToUserID, object Mutual)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static string buddy_denyRequest(int? mid, object FromUserID, object ToUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static DataTable buddy_list(int? mid, object FromUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.buddy_list(connectionString, FromUserID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.buddy_list(connectionString, FromUserID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.buddy_list(connectionString, FromUserID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.buddy_list(connectionString, FromUserID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.buddy_list(connectionString, FromUserID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.buddy_list(connectionString, FromUserID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.buddy_list(connectionString, FromUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static string buddy_remove(int? mid, object FromUserID, object ToUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.buddy_remove(connectionString, FromUserID, ToUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// The ArgumentOutOfRangeException.
        /// </exception>
        public static bool category_delete(int? mid, object CategoryID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_delete(connectionString, CategoryID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_delete(connectionString, CategoryID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_delete(connectionString, CategoryID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_delete(connectionString, CategoryID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_delete(connectionString, CategoryID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_delete(connectionString, CategoryID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_delete(connectionString, CategoryID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable category_list(int? mid, object boardId, object categoryID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_list(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_list(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_list(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_list(connectionString, boardId, categoryID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_list(connectionString,  boardId, categoryID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_list(connectionString,  boardId, categoryID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_list(connectionString,  boardId, categoryID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable category_pfaccesslist(int? mid, object boardId, object categoryID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_pfaccesslist(connectionString,  boardId, categoryID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_pfaccesslist(connectionString,  boardId, categoryID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_pfaccesslist(connectionString,  boardId, categoryID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <param name="isAfter">
        /// The is after.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable category_getadjacentforum(
            int? mid, object boardId, object categoryID, object userId, bool isAfter)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_getadjacentforum(
                        connectionString, boardId, categoryID, userId, isAfter);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_getadjacentforum(
                        connectionString, boardId, categoryID, userId, isAfter);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_getadjacentforum(
                        connectionString, boardId, categoryID, userId, isAfter);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_getadjacentforum(
                        connectionString, boardId, categoryID, userId, isAfter);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable category_listread(int? mid, object boardId, object userId, object categoryID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_listread(connectionString, boardId, userId, categoryID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_listread(connectionString, boardId, userId, categoryID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_listread(connectionString, boardId, userId, categoryID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_listread(connectionString, boardId, userId, categoryID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable category_simplelist(int? mid, int startID, int limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.category_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.category_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.category_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.category_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.category_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.category_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.category_simplelist(connectionString, startID, limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void category_save(
            int? mid,
            object boardId,
            object categoryId,
            object name,
            object categoryImage,
            object sortOrder,
            object canHavePersForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable checkemail_list(int? mid, object email)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.checkemail_list(connectionString, email);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.checkemail_list(connectionString, email);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.checkemail_list(connectionString, email);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.checkemail_list(connectionString, email);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.checkemail_list(connectionString, email);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.checkemail_list(connectionString, email);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.checkemail_list(connectionString, email); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void checkemail_save(int? mid, object userId, object hash, object email)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.checkemail_save(connectionString, userId,  hash,  email); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.checkemail_save(connectionString, userId,  hash,  email); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.checkemail_save(connectionString, userId,  hash,  email); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable checkemail_update(int? mid, object hash)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.checkemail_update(connectionString, hash);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.checkemail_update(connectionString, hash);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.checkemail_update(connectionString, hash);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.checkemail_update(connectionString, hash);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.checkemail_update(connectionString, hash);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.checkemail_update(connectionString, hash);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.checkemail_update(connectionString, hash); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void choice_add(int? mid, object pollID, object choice, object path, object mime)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void choice_delete(int? mid, object choiceID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.choice_delete(connectionString, choiceID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.choice_delete(connectionString, choiceID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.choice_delete(connectionString, choiceID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.choice_delete(connectionString, choiceID);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.choice_delete(connectionString, choiceID); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.choice_delete(connectionString, choiceID); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.choice_delete(connectionString, choiceID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void choice_update(int? mid, object choiceID, object choice, object path, object mime)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void choice_vote(int? mid, object choiceID, object userId, object remoteIP)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_getstats_new(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_getstats_new(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_getstats_new(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_getstats_new(connectionString);
                    // case CommonSqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_getstats_new(connectionString); break;
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.db_getstats_new(connectionString); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_getstats_new(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_getstats_warning();
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_getstats_warning();
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_getstats_warning();
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_getstats_warning();
                    // case CommonSqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_getstats_warning(); break;
                    // case CommonSqlDbAccess.Db2: return db2_db_getstats_warning(); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_getstats_warning(); break;
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
        /// <param name="dbRecoveryMode">
        /// The db recovery mode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string db_recovery_mode_warning(int? mid, string dbRecoveryMode)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_recovery_mode_warning();
                    break;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_recovery_mode_warning();
                    break;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_recovery_mode_warning();
                    break;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_recovery_mode_warning();
                    break;
                    // case CommonSqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /*  public static string db_getstats_warning()
      {
          string dataEngine;
          string connectionString;
          int? mid = 0;  string namePattern = string.Empty;
          CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

          switch (dataEngine)
          {
              case CommonSqlDbAccess.MsSql: return  MsSql.Db.db_getstats_warning(); break;
              case CommonSqlDbAccess.Npgsql: return Postgre.Db.db_getstats_warning(); ; break;
              case CommonSqlDbAccess.MySql: return MySqlDb.Db.db_getstats_warning(); break;
              case CommonSqlDbAccess.Firebird: return FirebirdDb.Db.db_getstats_warning(); break;
              // case CommonSqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_getstats_warning(); break;
              // case CommonSqlDbAccess.Db2: return db2_db_getstats_warning(); break;
              // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_getstats_warning(); break;
              default:
                  throw new ArgumentOutOfRangeException(dataEngine);
                  break;

          }

      } */

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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                    // case CommonSqlDbAccess.Oracle: return  VZF.Data.Oracle.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_reindex_new(connectionString);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_reindex_new(connectionString);
                    break;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_reindex_new(connectionString);
                    break;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_reindex_new(connectionString);
                    break;
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.db_reindex_new(connectionString); break;
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.db_reindex_new(connectionString); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_reindex_new(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_reindex_warning();
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_reindex_warning();
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_reindex_warning();
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_reindex_warning();
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.db_reindex_warning();
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.db_reindex_warning();
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.db_reindex_warning(); 
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_runsql_new(connectionString, sql, useTransaction);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_runsql_new(connectionString, sql, useTransaction);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_runsql_new(connectionString, sql, useTransaction);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_runsql_new(connectionString, sql, useTransaction);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.db_runsql_new(connectionString, sql,  useTransaction);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.db_runsql_new(connectionString, sql,  useTransaction);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.db_runsql_new(connectionString, sql, useTransaction); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_shrink_new(connectionString);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_shrink_new(connectionString);
                    break;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_shrink_new(connectionString);
                    break;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_shrink_new(connectionString);
                    break;
                    // case CommonSqlDbAccess.Oracle: return VZF.Data.Oracle.Db.db_shrink(connectionString); break;
                    // case CommonSqlDbAccess.Db2: return VZF.Data.Db2.Db.db_shrink(connectionString); break;
                    // case CommonSqlDbAccess.Other: return VZF.Data.Other.Db.db_shrink(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.db_shrink_warning();
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.db_shrink_warning(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.db_shrink_warning();
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.db_shrink_warning();
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.db_shrink_warning(connectionString);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.db_shrink_warning(connectionStringe);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.db_shrink_warning(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_byuserlist(
            int? mid, object boardId, object forumId, object userId, object isUserForum)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_byuserlist(connectionString, boardId, forumId, userId, isUserForum);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_byuserlist(connectionString, boardId, forumId, userId, isUserForum);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_byuserlist(connectionString, boardId, forumId, userId, isUserForum);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_byuserlist(
                        connectionString, boardId, forumId, userId, isUserForum);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum); 
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataSet ds_forumadmin(int? mid, object boardId, object pageUserID, object isUserForum)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            eventlog_create(mid, userId, (object)source.GetType().ToString(), description, (object)0);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void eventlog_create(int? mid, object userId, object source, object description, object type)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The eventlog_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void eventlog_delete(int? mid, int boardId)
        {
            eventlog_delete(mid, null, boardId);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private static void eventlog_delete(int? mid, object eventLogID, object boardId, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void eventlog_deletebyuser(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.eventlog_deletebyuser(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.eventlog_deletebyuser(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.eventlog_deletebyuser(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.eventlog_deletebyuser(connectionString, boardId, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserId,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserId,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserId,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserId,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable eventloggroupaccess_list(
            int? mid, [NotNull] object groupID, [NotNull] object eventTypeId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_eventlogaccesslist(int? mid, [NotNull] object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_eventlogaccesslist(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_eventlogaccesslist(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_eventlogaccesslist(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_eventlogaccesslist(connectionString, boardId);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void eventloggroupaccess_save(
            int? mid,
            [NotNull] object groupId,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName,
            [NotNull] object deleteAccess)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.eventloggroupaccess_save(
                        connectionString, groupId, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.eventloggroupaccess_save(
                        connectionString, groupId, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.eventloggroupaccess_save(
                        connectionString, groupId, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.eventloggroupaccess_save(
                        connectionString, groupId, eventTypeId, eventTypeName, deleteAccess);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void extension_delete(int? mid, object extensionId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.extension_delete(connectionString, extensionId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.extension_delete(connectionString, extensionId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.extension_delete(connectionString, extensionId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.extension_delete(connectionString, extensionId);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.extension_delete(connectionString, extensionId); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.extension_delete(connectionString, extensionId); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.extension_delete(connectionString, extensionId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The eventloggroupaccess_delete.
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
        /// <param name="eventTypeName">
        /// The event type name.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void eventloggroupaccess_delete(
            int? mid, [NotNull] object groupID, [NotNull] object eventTypeId, [NotNull] object eventTypeName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.eventloggroupaccess_delete(connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.eventloggroupaccess_delete(
                        connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.eventloggroupaccess_delete(connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.eventloggroupaccess_delete(
                        connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable extension_edit(int? mid, object extensionId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.extension_edit(connectionString, extensionId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.extension_edit(connectionString, extensionId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.extension_edit(connectionString, extensionId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.extension_edit(connectionString, extensionId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.extension_edit(connectionString, extensionId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.extension_edit(connectionString, extensionId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.extension_edit(connectionString, extensionId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable extension_list(int? mid, object boardId)
        {
            return extension_list(mid, boardId, string.Empty);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable extension_list(int? mid, object boardId, object extension)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.extension_list(connectionString, boardId, extension);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.extension_list(connectionString, boardId, extension);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.extension_list(connectionString, boardId, extension);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.extension_list(connectionString, boardId, extension);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.extension_list(connectionString, boardId, extension);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.extension_list(connectionString, boardId, extension);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.extension_list(connectionString, boardId, extension); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void extension_save(int? mid, object extensionId, object boardId, object extension)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                    // case CommonSqlDbAccess.Oracle:  VZF.Data.Oracle.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                    // case CommonSqlDbAccess.Db2: VZF.Data.Db2.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                    // case CommonSqlDbAccess.Other: VZF.Data.Other.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_categoryaccess_activeuser(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool forum_delete(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_delete(connectionString, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_delete(connectionString, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_delete(connectionString, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_delete(connectionString, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_delete(connectionString, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_delete(connectionString, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_delete(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool forum_move(int? mid, [NotNull] object forumOldID, [NotNull] object forumNewID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_move(connectionString, forumOldID, forumNewID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_move(connectionString, forumOldID, forumNewID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_move(connectionString, forumOldID, forumNewID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_move(connectionString, forumOldID, forumNewID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_list(int? mid, object boardId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_list(connectionString, boardId, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_list(connectionString, boardId, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_list(connectionString, boardId, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_list(connectionString, boardId, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_list(connectionString, boardId, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_list(connectionString, boardId, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_list(connectionString, boardId, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_listall(int? mid, object boardId, object userId, object startAt, bool returnAll)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Lists all forums within a given subcategory
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">BoardID</param>
        /// <param name="categoryID">The category ID. </param>
        /// <returns>DataTable with list</returns>
        public static DataTable forum_listall_fromCat(
            int? mid, object boardId, object categoryID, bool allowUserForumsOnly)
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_listall_fromCat(
            int? mid, object boardId, object categoryID, bool emptyFirstRow, bool allowUserForumsOnly)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
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
            listDestination.Columns.Add("IsHidden", typeof(bool));
            listDestination.Columns.Add("ReadAccess", typeof(bool));

            if (emptyFirstRow)
            {
                DataRow blankRow = listDestination.NewRow();
                blankRow["ForumID"] = string.Empty;
                blankRow["ParentID"] = string.Empty;
                blankRow["Title"] = string.Empty;
                blankRow["Level"] = string.Empty;
                blankRow["IsHidden"] = string.Empty;
                blankRow["ReadAccess"] = string.Empty;
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
                mid, dv.ToTable(), listDestination, parentId, categoryId, startingIndent, returnAll);

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
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId, int[] forumidExclusions)
        {
            return forum_listall_sorted(mid, boardId, userId, null, false, 0, false);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId)
        {
            return !Config.LargeForumTree ? forum_listall_sorted(mid, boardId, userId, null, false, 0, false) : forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listall_sorted_all(int? mid, object boardId, object userId, bool returnAll)
        {
            return !Config.LargeForumTree ? forum_listall_sorted(mid, boardId, userId, null, false, 0, returnAll) : forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
            DataTable dtTable;
            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    dtTable = VZF.Data.MsSql.Db.forum_ns_getchildren_activeuser(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        userid,
                        notincluded,
                        immediateonly,
                        indentchars);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    dtTable = VZF.Data.Postgre.Db.forum_ns_getchildren_activeuser(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        userid,
                        notincluded,
                        immediateonly,
                        indentchars);
                    break;
                case CommonSqlDbAccess.MySql:
                    dtTable = VZF.Data.Mysql.Db.forum_ns_getchildren_activeuser(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        userid,
                        notincluded,
                        immediateonly,
                        indentchars);
                    break;
                case CommonSqlDbAccess.Firebird:
                    dtTable = VZF.Data.Firebird.Db.forum_ns_getchildren_activeuser(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        userid,
                        notincluded,
                        immediateonly,
                        indentchars);
                    break;
                    // case CommonSqlDbAccess.Oracle:  dtTable = VZF.Data.Oracle.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                    // case CommonSqlDbAccess.Db2:  dtTable = VZF.Data.Db2.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                    // case CommonSqlDbAccess.Other:  dtTable = VZF.Data.Other.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            return dtTable;
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_ns_getchildren(
            int? mid,
            int? boardid,
            int? categoryid,
            int? forumid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars)
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars)
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The forum_listall_sorted.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
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
        /// <returns>
        /// </returns>
        public static DataTable forum_listall_sorted(
            int? mid,
            object boardId,
            object userId,
            int[] forumidExclusions,
            bool emptyFirstRow,
            int startAt,
            bool returnAll)
        {
            using (DataTable dataTable = forum_listall(mid, boardId, userId, startAt, returnAll))
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

                return forum_sort_list(
                    mid, dataTable, baseForumId, baseCategoryId, 0, forumidExclusions, emptyFirstRow, returnAll);
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
            int? mid, DataTable listsource, DataTable list, int parentid, int currentLvl)
        {
            for (int i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value)
                {
                    row["ParentID"] = 0;
                }

                if ((int)row["ParentID"] == parentid)
                {
                    string sIndent = string.Empty;
                    int iIndent = Convert.ToInt32(currentLvl);
                    for (int j = 0; j < iIndent; j++)
                    {
                        sIndent += "--";
                    }
                    row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                    list.Rows.Add(row.ItemArray);
                    forum_list_sort_basic(mid, listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
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

                listDestination.Rows.Add(newRow);

                // recurse through the list...
                forum_sort_list_recursive(
                    mid, listSource, listDestination, (int)row["ForumID"], categoryID, currentIndent + 1, returnAll);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_tags(
            int? mid,
            int boardId,
            int pageUserId,
            int forumId,
            int pageIndex,
            int pageSize,
            string searchText,
            bool beginsWith)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_tags(
                        connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_tags(
                        connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_tags(
                        connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_tags(
                        connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_listallMyModerated(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_listallMyModerated(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_listpath(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_listpath(connectionString, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_listpath(connectionString, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_listpath(connectionString, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_listpath(connectionString, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_listpath(connectionString, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_listpath(connectionString, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_listpath(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The forum_listread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="parentID">
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_listread(
            int? mid,
            object boardID,
            object userID,
            object categoryID,
            object parentID,
            object useStyledNicks,
            bool findLastRead,
            [NotNull] bool showCommonForums,
            [NotNull] bool showPersonalForums,
            [CanBeNull] int? forumCreatedByUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_listread(
                        connectionString,
                        boardID,
                        userID,
                        categoryID,
                        parentID,
                        useStyledNicks,
                        findLastRead,
                        showCommonForums,
                        showPersonalForums,
                        forumCreatedByUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_listread(
                        connectionString,
                        boardID,
                        userID,
                        categoryID,
                        parentID,
                        useStyledNicks,
                        findLastRead,
                        showCommonForums,
                        showPersonalForums,
                        forumCreatedByUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_listread(
                        connectionString,
                        boardID,
                        userID,
                        categoryID,
                        parentID,
                        useStyledNicks,
                        findLastRead,
                        showCommonForums,
                        showPersonalForums,
                        forumCreatedByUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_listread(
                        connectionString,
                        boardID,
                        userID,
                        categoryID,
                        parentID,
                        useStyledNicks,
                        findLastRead,
                        showCommonForums,
                        showPersonalForums,
                        forumCreatedByUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead, showCommonForums, showPersonalForums, forumCreatedByUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead, showCommonForums, showPersonalForums, forumCreatedByUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead, showCommonForums, showPersonalForums, forumCreatedByUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataSet forum_moderatelist(int? mid, object userId, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_moderatelist(connectionString, userId, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_moderatelist(connectionString, userId, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_moderatelist(connectionString, userId, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_moderatelist(connectionString, userId, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_moderatelist(connectionString, userId, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_moderatelist(connectionString, userId, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_moderatelist(connectionString, userId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_moderators(int? mid, bool useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_moderators(connectionString, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_moderators(connectionString, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_moderators(connectionString, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_moderators(connectionString, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_moderators(connectionString, useStyledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_moderators(connectionString, useStyledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_moderators(connectionString, useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Updates topic and post count and last topic for all forums in specified board
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">BoardID</param>
        public static void forum_resync(int? mid, object boardId)
        {
            forum_resync(mid, boardId, null);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void forum_resync(int? mid, object boardId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                    ;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.forum_resync(connectionString, boardId, forumID); break;;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.forum_resync(connectionString, boardId, forumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.forum_resync(connectionString, boardId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_save(
                        connectionString,
                        forumID,
                        categoryID,
                        parentID,
                        name,
                        description,
                        sortOrder,
                        locked,
                        hidden,
                        isTest,
                        moderated,
                        accessMaskID,
                        remoteURL,
                        themeURL,
                        imageURL,
                        styles,
                        dummy,
                        userId,
                        isUserForum,
                        canhavepersforums);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_save(
                        connectionString,
                        forumID,
                        categoryID,
                        parentID,
                        name,
                        description,
                        sortOrder,
                        locked,
                        hidden,
                        isTest,
                        moderated,
                        accessMaskID,
                        remoteURL,
                        themeURL,
                        imageURL,
                        styles,
                        dummy,
                        userId,
                        isUserForum,
                        canhavepersforums);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_save(
                        connectionString,
                        forumID,
                        categoryID,
                        parentID,
                        name,
                        description,
                        sortOrder,
                        locked,
                        hidden,
                        isTest,
                        moderated,
                        accessMaskID,
                        remoteURL,
                        themeURL,
                        imageURL,
                        styles,
                        dummy,
                        userId,
                        isUserForum,
                        canhavepersforums);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_save(
                        connectionString,
                        forumID,
                        categoryID,
                        parentID,
                        name,
                        description,
                        sortOrder,
                        locked,
                        hidden,
                        isTest,
                        moderated,
                        accessMaskID,
                        remoteURL,
                        themeURL,
                        imageURL,
                        styles,
                        dummy,
                        userId,
                        isUserForum,
                        canhavepersforums);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int forum_save_parentschecker(int? mid, object forumID, object parentID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_save_parentschecker(connectionString, forumID, parentID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forum_simplelist(int? mid, int startID, int limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forum_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forum_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forum_simplelist(connectionString, startID, limit);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forum_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forum_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forum_simplelist(connectionString, startID, limit);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forum_simplelist(connectionString, startID, limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forumaccess_group(int? mid, object groupID, object userId, bool includeUserForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forumaccess_personalgroup(
            int? mid, object groupID, object userId, bool includeUserForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forumaccess_personalgroup(
                        connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forumaccess_personalgroup(
                        connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forumaccess_personalgroup(
                        connectionString, groupID, userId, includeUserForums);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forumaccess_personalgroup(
                        connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable forumaccess_list(int? mid, object forumID, object userId, bool includeUserGroups)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void forumaccess_save(int? mid, object forumID, object groupID, object accessMaskID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll(
            int? mid, int boardId, int userId, bool includeNoAccess)
        {
            return forum_listall(mid, boardId, userId, 0, false).AsEnumerable().Select(r => new TypedForumListAll(r));
        }

        /// <summary>
        /// The forum list all.
        /// </summary>
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
        /// The forum list all.
        /// </returns>
        [NotNull]
        public static IEnumerable<TypedForumListAll> ForumListAll(
            string connectionString, int boardId, int userId, int startForumId)
        {
            var allForums = ForumListAll(connectionString, boardId, userId, 0);

            var forumIds = new List<int>();
            var tempForumIds = new List<int>();

            forumIds.Add(startForumId);
            tempForumIds.Add(startForumId);

            while (true)
            {
                var temp = allForums.Where(f => tempForumIds.Contains(f.ParentID ?? 0));

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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedForumListAll> ForumListAll(
            int? mid, int boardId, int userId, List<int> startForumId, bool includeNoAccess)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.ForumListAll(connectionString, boardId, userId, startForumId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool forumpage_initdb(int? mid, out string errorStr, bool debugging)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forumpage_initdb(connectionString, out  errorStr,  debugging); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string forumpage_validateversion(int? mid, int appVersion)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.forumpage_validateversion(connectionString, mid, appVersion); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <param name="forumIDToStartAt">
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
        /// The <see cref="DataTable"/>.
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
            List<int> forumIDToStartAt,
            int userId,
            int boardId,
            int maxResults,
            bool useFullText,
            bool searchDisplayName,
            bool includeChildren)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.GetSearchResult(
                        connectionString,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        categoryId,
                        forumIDToStartAt,
                        userId,
                        boardId,
                        maxResults,
                        useFullText,
                        searchDisplayName,
                        includeChildren);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.GetSearchResult(
                        connectionString,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        categoryId,
                        forumIDToStartAt,
                        userId,
                        boardId,
                        maxResults,
                        useFullText,
                        searchDisplayName,
                        includeChildren);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.GetSearchResult(
                        connectionString,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        categoryId,
                        forumIDToStartAt,
                        userId,
                        boardId,
                        maxResults,
                        useFullText,
                        searchDisplayName,
                        includeChildren);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.GetSearchResult(
                        connectionString,
                        toSearchWhat,
                        toSearchFromWho,
                        searchFromWhoMethod,
                        searchWhatMethod,
                        categoryId,
                        forumIDToStartAt,
                        userId,
                        boardId,
                        maxResults,
                        useFullText,
                        searchDisplayName,
                        includeChildren);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void group_delete(int? mid, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.group_delete(connectionString, groupID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.group_delete(connectionString, groupID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.group_delete(connectionString, groupID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.group_delete(connectionString, groupID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.group_delete(connectionString, groupID); break;;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.group_delete(connectionString, groupID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.group_delete(connectionString, groupID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_list(int? mid, object boardId, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_list(connectionString, boardId, groupID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_list(connectionString, boardId, groupID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_list(connectionString, boardId, groupID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_list(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_list(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_list(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_list(connectionString, boardId, groupID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_byuserlist(
            int? mid, object boardId, object groupID, object userId, object isUserGroup)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_byuserlist(
                        connectionString, boardId, groupID, userId, isUserGroup);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void group_medal_delete(int? mid, object groupID, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.group_medal_delete(connectionString, groupID, medalId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.group_medal_delete(connectionString, groupID, medalId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.group_medal_delete(connectionString, groupID, medalId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_medal_list(int? mid, object groupID, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_medal_list(connectionString, groupID, medalID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_medal_list(connectionString, groupID, medalID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_medal_list(connectionString, groupID, medalID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_medal_list(connectionString, groupID, medalID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_medal_list(connectionString, groupID, medalId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_medal_list(connectionString, groupID, medalId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_medal_list(connectionString, groupID, medalId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void group_medal_save(
            int? mid, object groupID, object medalID, object message, object hide, object onlyRibbon, object sortOrder)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.group_medal_save(connectionString, groupID, medalId, message, hide, onlyRibbon,  sortOrder);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.group_medal_save(connectionString, groupID, medalId, message, hide, onlyRibbon,  sortOrder); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.group_medal_save(connectionString, groupID, medalId, message, hide, onlyRibbon,  sortOrder); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_member(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_member(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_member(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_member(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_member(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_member(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_member(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_member(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The group_rank_style.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable group_rank_style(int? mid, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_rank_style(connectionString, boardID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_rank_style(connectionString, boardID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_rank_style(connectionString, boardID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_rank_style(connectionString, boardID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_rank_style(connectionString, boardID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_rank_style(connectionString, boardID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_rank_style(connectionString, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.group_save(
                        connectionString,
                        groupId,
                        boardId,
                        name,
                        isAdmin,
                        isGuest,
                        isStart,
                        isModerator,
                        isHidden,
                        accessMaskId,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages,
                        userId,
                        isUserGroup,
                        personalForumsNumber,
                        personalAccessMasksNumber,
                        personalGroupsNumber);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.group_save(
                        connectionString,
                        groupId,
                        boardId,
                        name,
                        isAdmin,
                        isGuest,
                        isStart,
                        isModerator,
                        isHidden,
                        accessMaskId,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages,
                        userId,
                        isUserGroup,
                        personalForumsNumber,
                        personalAccessMasksNumber,
                        personalGroupsNumber);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.group_save(
                        connectionString,
                        groupId,
                        boardId,
                        name,
                        isAdmin,
                        isGuest,
                        isStart,
                        isModerator,
                        isHidden,
                        accessMaskId,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages,
                        userId,
                        isUserGroup,
                        personalForumsNumber,
                        personalAccessMasksNumber,
                        personalGroupsNumber);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.group_save(
                        connectionString,
                        groupId,
                        boardId,
                        name,
                        isAdmin,
                        isGuest,
                        isStart,
                        isModerator,
                        isHidden,
                        accessMaskId,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages,
                        userId,
                        isUserGroup,
                        personalForumsNumber,
                        personalAccessMasksNumber,
                        personalGroupsNumber);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden,accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden,accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void mail_create(
            int? mid,
            object @from,
            object fromName,
            object to,
            object toName,
            object subject,
            object body,
            object bodyHtml)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.mail_create(
                        connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.mail_create(
                        connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId);break;
                    // case CommonSqlDbAccess.Db2:   db2_mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void mail_delete(int? mid, object mailID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.mail_delete(connectionString, mailID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.mail_delete(connectionString, mailID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.mail_delete(connectionString, mailID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.mail_delete(connectionString, mailID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.mail_delete(connectionString, mailID);break;
                    // case CommonSqlDbAccess.Db2:   db2_mail_delete(connectionString, mailID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.mail_delete(connectionString, mailID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedMailList> MailList(int? mid, long processId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.MailList(connectionString, processId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.MailList(connectionString, processId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.MailList(connectionString, processId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.MailList(connectionString, processId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.MailList(connectionString, processId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.MailList(connectionString, processId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.MailList(connectionString, processId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void medal_delete(int? mid, object boardId, object medalID, object category)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.medal_delete(connectionString, boardId,  medalId, category);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.medal_delete(connectionString, boardId,  medalId, category); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.medal_delete(connectionString, boardId,  medalId, category); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Lists given medal.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable medal_list(int? mid, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.medal_list(connectionString, null, medalID, null);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.medal_list(connectionString, null, medalID, null);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.medal_list(connectionString, null, medalID, null);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.medal_list(connectionString, null, medalID, null);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.medal_list(connectionString, null, medalId, null);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.medal_list(connectionString, null, medalId, null);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.medal_list(connectionString, null, medalId, null); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable medal_list(int? mid, object boardId, object category)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.medal_list(connectionString, boardId, null, category);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.medal_list(connectionString, boardId, null, category);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.medal_list(connectionString, boardId, null, category);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.medal_list(connectionString, boardId, null, category);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.medal_list(connectionString, boardId, null, category);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.medal_list(connectionString, boardId, null, category);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.medal_list(connectionString, boardId, null, category); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable medal_listusers(int? mid, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.medal_listusers(connectionString, medalID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.medal_listusers(connectionString, medalID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.medal_listusers(connectionString, medalID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.medal_listusers(connectionString, medalID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.medal_listusers(connectionString, medalId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.medal_listusers(connectionString, medalId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.medal_listusers(connectionString, medalId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void medal_resort(int? mid, object boardId, object medalID, int move)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.medal_resort(connectionString, boardId, medalId, move);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.medal_resort(connectionString, boardId, medalId, move); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.medal_resort(connectionString, boardId, medalId, move); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.medal_save(
                        connectionString,
                        boardId,
                        medalID,
                        name,
                        description,
                        message,
                        category,
                        medalURL,
                        ribbonURL,
                        smallMedalURL,
                        smallRibbonURL,
                        smallMedalWidth,
                        smallMedalHeight,
                        smallRibbonWidth,
                        smallRibbonHeight,
                        sortOrder,
                        flags);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.medal_save(
                        connectionString,
                        boardId,
                        medalID,
                        name,
                        description,
                        message,
                        category,
                        medalURL,
                        ribbonURL,
                        smallMedalURL,
                        smallRibbonURL,
                        smallMedalWidth,
                        smallMedalHeight,
                        smallRibbonWidth,
                        smallRibbonHeight,
                        sortOrder,
                        flags);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.medal_save(
                        connectionString,
                        boardId,
                        medalID,
                        name,
                        description,
                        message,
                        category,
                        medalURL,
                        ribbonURL,
                        smallMedalURL,
                        smallRibbonURL,
                        smallMedalWidth,
                        smallMedalHeight,
                        smallRibbonWidth,
                        smallRibbonHeight,
                        sortOrder,
                        flags);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.medal_save(
                        connectionString,
                        boardId,
                        medalID,
                        name,
                        description,
                        message,
                        category,
                        medalURL,
                        ribbonURL,
                        smallMedalURL,
                        smallRibbonURL,
                        smallMedalWidth,
                        smallMedalHeight,
                        smallRibbonWidth,
                        smallRibbonHeight,
                        sortOrder,
                        flags);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.medal_save(connectionString, boardId, medalId, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.medal_save(connectionString, boardId, medalId, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.medal_save(connectionString, boardId, medalId, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string message_AddThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_AddThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_AddThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                    // case CommonSqlDbAccess.Oracle:  return or_message_AddThanks(connectionString, fromUserID, messageID,useDisplayName,useDisplayName);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_approve(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_approve(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_approve(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_approve(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_approve(connectionString, messageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_approve(connectionString, messageID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_approve(connectionString, messageID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_approve(connectionString, messageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_delete(
            int? mid,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool eraseMessage)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_findunread(
            int? mid, object topicID, object messageId, object lastRead, object showDeleted, object authorUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_getRepliesList(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_getRepliesList(connectionString, messageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_getRepliesList(connectionString, messageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_getRepliesList(connectionString, messageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_getRepliesList(connectionString, messageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_getRepliesList(connectionString, messageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_getRepliesList(connectionString, messageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_getRepliesList(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_GetTextByIds(int? mid, string messageIDs)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_GetTextByIds(connectionString, messageIDs);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_GetTextByIds(connectionString, messageIDs);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_GetTextByIds(connectionString, messageIDs);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_GetTextByIds(connectionString, messageIDs); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_GetThanks(int mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_GetThanks(connectionString, messageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_GetThanks(connectionString, messageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_GetThanks(connectionString, messageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_GetThanks(connectionString, messageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_GetThanks(connectionString, messageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_GetThanks(connectionString, messageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_GetThanks(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The message_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_list(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_list(connectionString, messageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_list(connectionString, messageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_list(connectionString, messageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_list(connectionString, messageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_list(connectionString, messageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_list(connectionString, messageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_list(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_listreported(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_listreported(connectionString, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_listreported(connectionString, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_listreported(connectionString, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_listreported(connectionString, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_listreported(connectionString, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_listreported(connectionString, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_listreported(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_listreporters(int? mid, int messageID, object userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_listreporters(connectionString, messageID, userID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_listreporters(connectionString, messageID, userID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_listreporters(connectionString, messageID, userID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_listreporters(connectionString, messageID, userID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_listreporters(connectionString, messageID, userID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_listreporters(connectionString, messageID, userID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_listreporters(connectionString, messageID, userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_move(int? mid, object messageID, object moveToTopic, bool moveAll)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_move(connectionString, messageID, moveToTopic, moveAll);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string message_RemoveThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_report(
            int? mid, object messageID, object userId, object reportedDateTime, object reportText)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_report(
                        connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_report(
                        connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_reportcopyover(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_reportcopyover(connectionString, messageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_reportcopyover(connectionString, messageID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_reportcopyover(connectionString, messageID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_reportcopyover(connectionString, messageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_reportresolve(int? mid, object messageFlag, object messageID, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            ref long messageId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_secdata(int? mid, int MessageID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_secdata(connectionString, MessageID, pageUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_secdata(connectionString, MessageID, pageUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_secdata(connectionString, MessageID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int message_ThanksNumber(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_ThanksNumber(connectionString, messageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_ThanksNumber(connectionString, messageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_ThanksNumber(connectionString, messageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_ThanksNumber(connectionString, messageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_ThanksNumber(connectionString, messageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_ThanksNumber(connectionString, messageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_ThanksNumber(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable message_unapproved(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.message_unapproved(connectionString, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.message_unapproved(connectionString, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.message_unapproved(connectionString, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.message_unapproved(connectionString, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.message_unapproved(connectionString, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.message_unapproved(connectionString, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.message_unapproved(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The message_update.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void message_update(
            int? mid,
            object messageID,
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
            string tags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.message_update(
                        connectionString,
                        messageID,
                        priority,
                        message,
                        description,
                        status,
                        styles,
                        subject,
                        flags,
                        reasonOfEdit,
                        isModeratorChanged,
                        overrideApproval,
                        origMessage,
                        editedBy,
                        tags);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.message_update(
                        connectionString,
                        messageID,
                        priority,
                        message,
                        description,
                        status,
                        styles,
                        subject,
                        flags,
                        reasonOfEdit,
                        isModeratorChanged,
                        overrideApproval,
                        origMessage,
                        editedBy,
                        tags);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.message_update(
                        connectionString,
                        messageID,
                        priority,
                        message,
                        description,
                        status,
                        styles,
                        subject,
                        flags,
                        reasonOfEdit,
                        isModeratorChanged,
                        overrideApproval,
                        origMessage,
                        editedBy,
                        tags);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.message_update(
                        connectionString,
                        messageID,
                        priority,
                        message,
                        description,
                        status,
                        styles,
                        subject,
                        flags,
                        reasonOfEdit,
                        isModeratorChanged,
                        overrideApproval,
                        origMessage,
                        editedBy,
                        tags);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.message_update(connectionString, messageID, priority, message, description, status,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.message_update(connectionString, messageID, priority, message, description, status,styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.message_update(connectionString, messageID, priority, message, description, status, styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedAllThanks> MessageGetAllThanks(int? mid, string messageIdsSeparatedWithColon)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable messagehistory_list(int? mid, int messageID, int daysToClean)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.messagehistory_list(connectionString, messageID, daysToClean); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedMessageList> MessageList(int? mid, int messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.MessageList(connectionString, messageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.MessageList(connectionString, messageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.MessageList(connectionString, messageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.MessageList(connectionString, messageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.MessageList(connectionString, messageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.MessageList(connectionString, messageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.MessageList(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable moderators_team_list(int? mid, bool useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.moderators_team_list(connectionString, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.moderators_team_list(connectionString, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.moderators_team_list(connectionString, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.moderators_team_list(connectionString, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.moderators_team_list( connectionString,  useStyledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.moderators_team_list( connectionString,  useStyledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.moderators_team_list( connectionString,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void Readtopic_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /* public static void ReadTopic_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 case CommonSqlDbAccess.MsSql: MsSql.Db.Readtopic_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.Npgsql: Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.MySql:  MySqlDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.Firebird:  FirebirdDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.Readtopic_delete(connectionString, trackingID);break;
                 // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.Readtopic_delete(connectionString, trackingID); break;
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.User_LastRead(connectionString, userID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.User_LastRead(connectionString, userID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.User_LastRead(connectionString, userID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.User_LastRead(connectionString, userID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.User_LastRead( connectionString,  userID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.User_LastRead( connectionString,  userID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.User_LastRead( connectionString,  userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime? Readtopic_lastread(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.Readtopic_lastread(connectionString, userID, topicID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.Readtopic_lastread(connectionString, userID, topicID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.Readtopic_lastread(connectionString, userID, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void ReadForum_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /* public static void ReadForum_delete([NotNull] object trackingID)
         {
             string dataEngine;
             string connectionString;
             int? mid = 0;  string namePattern = string.Empty;
             CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);
             
             switch (dataEngine)
             {
                 case CommonSqlDbAccess.MsSql: MsSql.Db.ReadForum_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.Npgsql: Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.MySql:  MySqlDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 case CommonSqlDbAccess.Firebird:  FirebirdDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.ReadForum_delete(connectionString, trackingID);break;
                 // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.ReadForum_delete(connectionString, trackingID); break;
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime? ReadForum_lastread(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ReadForum_lastread(connectionString, userID, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ReadForum_lastread(connectionString, userID, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ReadForum_lastread(connectionString, userID, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ReadForum_lastread(connectionString, userID, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.ReadForum_lastread(connectionString,userID, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.ReadForum_lastread(connectionString,userID, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.ReadForum_lastread(connectionString,userID, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void nntpforum_delete(int? mid, object nntpForumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntpforum_delete(connectionString, nntpForumID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntpforum_delete(connectionString, nntpForumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntpforum_delete(connectionString, nntpForumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable nntpforum_list(
            int? mid, object boardId, object minutes, object nntpForumID, object active)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void nntpforum_save(
            int? mid,
            object nntpForumID,
            object nntpServerID,
            object groupName,
            object forumID,
            object active,
            object cutoffdate)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void nntpforum_update(int? mid, object nntpForumID, object lastMessageNo, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedNntpForum> NntpForumList(
            int? mid, int boardId, int? minutes, int? nntpForumID, bool? active)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void nntpserver_delete(int? mid, object nntpServerID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntpserver_delete(connectionString, nntpServerID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntpserver_delete(connectionString, nntpServerID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntpserver_delete(connectionString, nntpServerID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable nntpserver_list(int? mid, object boardId, object nntpServerID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.nntpserver_list(connectionString,  boardId, nntpServerID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable nntptopic_list(int? mid, object thread)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.nntptopic_list(connectionString, thread);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.nntptopic_list(connectionString, thread);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.nntptopic_list(connectionString, thread);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.nntptopic_list(connectionString, thread);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.nntptopic_list(connectionString, thread);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.nntptopic_list(connectionString, thread);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.nntptopic_list(connectionString, thread); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.nntptopic_savemessage(
                        connectionString,
                        nntpForumID,
                        topic,
                        body,
                        userId,
                        userName,
                        ip,
                        posted,
                        externalMessageId,
                        referenceMessageId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.nntptopic_savemessage(
                        connectionString,
                        nntpForumID,
                        topic,
                        body,
                        userId,
                        userName,
                        ip,
                        posted,
                        externalMessageId,
                        referenceMessageId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.nntptopic_savemessage(
                        connectionString,
                        nntpForumID,
                        topic,
                        body,
                        userId,
                        userName,
                        ip,
                        posted,
                        externalMessageId,
                        referenceMessageId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.nntptopic_savemessage(
                        connectionString,
                        nntpForumID,
                        topic,
                        body,
                        userId,
                        userName,
                        ip,
                        posted,
                        externalMessageId,
                        referenceMessageId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            pageload_Result pload;
            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    pload = VZF.Data.MsSql.Db.pageload(
                        connectionString,
                        sessionId,
                        boardId,
                        userKey,
                        ip,
                        location,
                        forumPage,
                        browser,
                        platform,
                        categoryId,
                        forumId,
                        topicId,
                        messageId,
                        isCrawler,
                        isMobileDevice,
                        donttrack).Table.AsEnumerable()
                               .Select(r => new pageload_Result(r))
                               .ToList()[0];
                    pload.ModuleID = mid;
                    return pload;
                case CommonSqlDbAccess.Npgsql:
                    var dt =
    VZF.Data.Postgre.Db.pageload(
        connectionString,
        sessionId,
        boardId,
        userKey,
        ip,
        location,
        forumPage,
        browser,
        platform,
        categoryId,
        forumId,
        topicId,
        messageId,
        isCrawler,
        isMobileDevice,
        donttrack).Table;
                    pload = dt.AsEnumerable()
                               .Select(r => new pageload_Result(r))
                               .ToList()[0];
                     pload.ModuleID = mid;
                    return pload;
                case CommonSqlDbAccess.MySql:
                    pload = VZF.Data.Mysql.Db.pageload(
                        connectionString,
                        sessionId,
                        boardId,
                        userKey,
                        ip,
                        location,
                        forumPage,
                        browser,
                        platform,
                        categoryId,
                        forumId,
                        topicId,
                        messageId,
                        isCrawler,
                        isMobileDevice,
                        donttrack).Table.AsEnumerable()
                               .Select(r => new pageload_Result(r))
                               .ToList()[0];
                     pload.ModuleID = mid;
                    return pload;
                case CommonSqlDbAccess.Firebird:;
                    pload = VZF.Data.Firebird.Db.pageload(
                            connectionString,
                            sessionId,
                            boardId,
                            userKey,
                            ip,
                            location,
                            forumPage,
                            browser,
                            platform,
                            categoryId,
                            forumId,
                            topicId,
                            messageId,
                            isCrawler,
                            isMobileDevice,
                            donttrack).Table.AsEnumerable()
                               .Select(r => new pageload_Result(r))
                               .ToList()[0];
                     pload.ModuleID = mid;
                    return pload;
                // case CommonSqlDbAccess.Oracle:  var pload = VZF.Data.Oracle.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack).Table.AsEnumerable().Select(r => new pageload_Result()).ToList()[0]; pload.ModuleID = mid;return pload;
                // case CommonSqlDbAccess.Db2:  var pload = VZF.Data.Db2.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack).Table.AsEnumerable().Select(r => new pageload_Result()).ToList()[0]; pload.ModuleID = mid;return pload;
                // case CommonSqlDbAccess.Other:  var pload = VZF.Data.Other.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack).Table.AsEnumerable().Select(r => new pageload_Result()).ToList()[0]; pload.ModuleID = mid;return pload;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void pmessage_archive(int? mid, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pmessage_archive(connectionString, userPMessageID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pmessage_archive(connectionString, userPMessageID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pmessage_archive(connectionString, userPMessageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void pmessage_delete(int? mid, object userPMessageID, bool fromOutbox)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The pmessage_info.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable pmessage_info(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pmessage_info(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pmessage_info(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pmessage_info(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pmessage_info(connectionString);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.pmessage_info(connectionString);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.pmessage_info(connectionString);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.pmessage_info(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable pmessage_list(int? mid, object toUserID, object fromUserID, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void pmessage_markread(int? mid, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pmessage_markread(connectionString, userPMessageID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pmessage_markread(connectionString, userPMessageID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pmessage_markread(connectionString, userPMessageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void pmessage_prune(int? mid, object daysRead, object daysUnread)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pmessage_prune(connectionString, daysRead, daysUnread);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void pmessage_save(
            int? mid, object fromUserID, object toUserID, object subject, object body, object Flags, object replyTo)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void poll_remove(
            int? mid, object pollGroupID, object pollID, object boardId, bool removeCompletely, bool removeEverywhere)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The poll_save.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="pollList">
        /// The poll list.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int? poll_save(int? mid, List<PollSaveList> pollList)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.poll_save(connectionString, pollList);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.poll_save(connectionString, pollList);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.poll_save(connectionString, pollList);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.poll_save(connectionString, pollList);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.poll_save(connectionString, pollList);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.poll_save(connectionString, pollList);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.poll_save(connectionString, pollList); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable poll_stats(int? mid, int? pollId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.poll_stats(connectionString, pollId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.poll_stats(connectionString, pollId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.poll_stats(connectionString, pollId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.poll_stats(connectionString, pollId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.poll_stats(connectionString, pollId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.poll_stats(connectionString, pollId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.poll_stats(connectionString, pollId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int pollgroup_attach(
            int? mid, int? pollGroupId, int? topicId, int? forumId, int? categoryId, int? boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.pollgroup_remove(
                        connectionString,
                        pollGroupID,
                        topicId,
                        forumId,
                        categoryId,
                        boardId,
                        removeCompletely,
                        removeEverywhere);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.pollgroup_remove(
                        connectionString,
                        pollGroupID,
                        topicId,
                        forumId,
                        categoryId,
                        boardId,
                        removeCompletely,
                        removeEverywhere);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.pollgroup_remove(
                        connectionString,
                        pollGroupID,
                        topicId,
                        forumId,
                        categoryId,
                        boardId,
                        removeCompletely,
                        removeEverywhere);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.pollgroup_remove(
                        connectionString,
                        pollGroupID,
                        topicId,
                        forumId,
                        categoryId,
                        boardId,
                        removeCompletely,
                        removeEverywhere);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable pollgroup_stats(int? mid, int? pollGroupId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pollgroup_stats(connectionString, pollGroupId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pollgroup_stats(connectionString, pollGroupId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pollgroup_stats(connectionString, pollGroupId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.pollgroup_stats(connectionString, pollGroupId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.poll_update(
                        connectionString,
                        pollID,
                        question,
                        closes,
                        isBounded,
                        isClosedBounded,
                        allowMultipleChoices,
                        showVoters,
                        allowSkipVote,
                        questionPath,
                        questionMime);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.poll_update(
                        connectionString,
                        pollID,
                        question,
                        closes,
                        isBounded,
                        isClosedBounded,
                        allowMultipleChoices,
                        showVoters,
                        allowSkipVote,
                        questionPath,
                        questionMime);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.poll_update(
                        connectionString,
                        pollID,
                        question,
                        closes,
                        isBounded,
                        isClosedBounded,
                        allowMultipleChoices,
                        showVoters,
                        allowSkipVote,
                        questionPath,
                        questionMime);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.poll_update(
                        connectionString,
                        pollID,
                        question,
                        closes,
                        isBounded,
                        isClosedBounded,
                        allowMultipleChoices,
                        showVoters,
                        allowSkipVote,
                        questionPath,
                        questionMime);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable pollgroup_votecheck(int? mid, object pollGroupId, object userId, object remoteIp)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedPollGroup> PollGroupList(int? mid, int userID, int? forumId, int boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PollGroupList(connectionString, userID, forumId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable pollvote_check(int? mid, object pollid, object userid, object remoteip)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.pollvote_check(connectionString, pollid,  userid,  remoteip); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable post_alluser(
            int? mid, object boardid, object userid, object pageUserID, object topCount)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            int messagePosition)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.post_list(
                        connectionString,
                        topicId,
                        currentUserID,
                        authoruserId,
                        updateViewCount,
                        showDeleted,
                        styledNicks,
                        showReputation,
                        sincePostedDate,
                        toPostedDate,
                        sinceEditedDate,
                        toEditedDate,
                        pageIndex,
                        pageSize,
                        sortPosted,
                        sortEdited,
                        sortPosition,
                        showThanks,
                        messagePosition);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.post_list(
                        connectionString,
                        topicId,
                        currentUserID,
                        authoruserId,
                        updateViewCount,
                        showDeleted,
                        styledNicks,
                        showReputation,
                        sincePostedDate,
                        toPostedDate,
                        sinceEditedDate,
                        toEditedDate,
                        pageIndex,
                        pageSize,
                        sortPosted,
                        sortEdited,
                        sortPosition,
                        showThanks,
                        messagePosition);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.post_list(
                        connectionString,
                        topicId,
                        currentUserID,
                        authoruserId,
                        updateViewCount,
                        showDeleted,
                        styledNicks,
                        showReputation,
                        sincePostedDate,
                        toPostedDate,
                        sinceEditedDate,
                        toEditedDate,
                        pageIndex,
                        pageSize,
                        sortPosted,
                        sortEdited,
                        sortPosition,
                        showThanks,
                        messagePosition);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.post_list(
                        connectionString,
                        topicId,
                        currentUserID,
                        authoruserId,
                        updateViewCount,
                        showDeleted,
                        styledNicks,
                        showReputation,
                        sincePostedDate,
                        toPostedDate,
                        sinceEditedDate,
                        toEditedDate,
                        pageIndex,
                        pageSize,
                        sortPosted,
                        sortEdited,
                        sortPosition,
                        showThanks,
                        messagePosition);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable post_list_reverse10(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.post_list_reverse10(connectionString, topicID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.post_list_reverse10(connectionString, topicID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.post_list_reverse10(connectionString, topicID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.post_list_reverse10(connectionString, topicID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.post_list_reverse10(connectionString, topicID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.post_list_reverse10(connectionString, topicID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.post_list_reverse10(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void rank_delete(int? mid, object rankID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.rank_delete(connectionString, rankID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.rank_delete(connectionString, rankID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.rank_delete(connectionString, rankID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.rank_delete(connectionString, rankID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.rank_delete(connectionString, rankID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.rank_delete(connectionString, rankID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.rank_delete(connectionString, rankID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<rank_list_Result> rank_list(int? mid, object boardId, object rankID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r));
                // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.rank_list(connectionString, boardId, rankID).AsEnumerable().Select(r => new rank_list_Result(r)); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
                        isGuest,
                        minPosts,
                        rankImage,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
                        isGuest,
                        minPosts,
                        rankImage,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
                        isGuest,
                        minPosts,
                        rankImage,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
                        isGuest,
                        minPosts,
                        rankImage,
                        pmLimit,
                        style,
                        sortOrder,
                        description,
                        usrSigChars,
                        usrSigBBCodes,
                        usrSigHTMLTags,
                        usrAlbums,
                        usrAlbumImages);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable recent_users(int? mid, object boardID, int timeSinceLastLogin, object styledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The registry_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
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
        /// The <see cref="DataTable"/>.
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable registry_list(int? mid, object name, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.registry_list(connectionString, name, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.registry_list(connectionString, name, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.registry_list(connectionString, name, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.registry_list(connectionString, name, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.registry_list(connectionString, name,  boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.registry_list(connectionString, name,  boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.registry_list(connectionString, name,  boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void registry_save(int? mid, object name, object value, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.registry_save(connectionString, name, value, boardId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.registry_save(connectionString, name, value, boardId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.registry_save(connectionString, name, value, boardId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.registry_save(connectionString, name, value, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void replace_words_delete(int? mid, object id)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.replace_words_delete(connectionString, id);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.replace_words_delete(connectionString, id);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.replace_words_delete(connectionString, id);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.replace_words_delete(connectionString, id);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.replace_words_delete(connectionString, id);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.replace_words_delete(connectionString, id); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.replace_words_delete(connectionString, id); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable replace_words_list(int? mid, object boardId, object id)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.replace_words_list(connectionString, boardId, id);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.replace_words_list(connectionString, boardId, id);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.replace_words_list(connectionString, boardId, id);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.replace_words_list(connectionString, boardId, id);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.replace_words_list(connectionString, boardId, id);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.replace_words_list(connectionString, boardId, id);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.replace_words_list(connectionString, boardId, id); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void replace_words_save(int? mid, object boardId, object id, object badword, object goodword)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable rss_topic_latest(
            int? mid,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable rsstopic_list(int? mid, int forumID, int topicStart, int topicCount)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int userId,
            string userName,
            SettingsPropertyValueCollection collection,
            bool dirtyOnly = true)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool shoutbox_clearmessages(int? mid, int boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.shoutbox_clearmessages(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.shoutbox_clearmessages(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.shoutbox_clearmessages(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.shoutbox_clearmessages(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable shoutbox_getmessages(int? mid, int boardId, int numberOfMessages, object useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool shoutbox_savemessage(
            int? mid, int boardId, string message, string userName, int userID, object ip)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void smiley_delete(int? mid, object smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.smiley_delete(connectionString, smileyID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.smiley_delete(connectionString, smileyID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.smiley_delete(connectionString, smileyID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.smiley_delete(connectionString, smileyID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.smiley_delete(connectionString, smileyID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.smiley_delete(connectionString, smileyID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.smiley_delete(connectionString, smileyID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable smiley_list(int? mid, object boardId, object smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.smiley_list(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.smiley_list(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.smiley_list(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.smiley_list(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.smiley_list(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.smiley_list(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.smiley_list(connectionString, boardId, smileyID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable smiley_listunique(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.smiley_listunique(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.smiley_listunique(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.smiley_listunique(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.smiley_listunique(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.smiley_listunique(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.smiley_listunique(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.smiley_listunique(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void smiley_resort(int? mid, object boardId, object smileyID, int move)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.smiley_resort(connectionString, boardId,  smileyID,  move);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedSmileyList> SmileyList(int? mid, int boardId, int? smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.SmileyList(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.SmileyList(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.SmileyList(connectionString, boardId, smileyID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.SmileyList(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.SmileyList(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.SmileyList(connectionString, boardId, smileyID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.SmileyList(connectionString, boardId, smileyID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The system_deleteinstallobjects.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void system_deleteinstallobjects(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.system_deleteinstallobjects(connectionString);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.system_deleteinstallobjects(connectionString);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.system_deleteinstallobjects(connectionString);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.system_deleteinstallobjects(connectionString);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_deleteinstallobjects(connectionString);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.system_deleteinstallobjects(connectionString); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.system_deleteinstallobjects(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.system_initialize(
                        connectionString,
                        forumName,
                        timeZone,
                        culture,
                        languageFile,
                        forumEmail,
                        smtpServer,
                        userName,
                        userEmail,
                        providerUserKey,
                        rolePrefix);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.system_initialize(
                        connectionString,
                        forumName,
                        timeZone,
                        culture,
                        languageFile,
                        forumEmail,
                        smtpServer,
                        userName,
                        userEmail,
                        providerUserKey,
                        rolePrefix);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.system_initialize(
                        connectionString,
                        forumName,
                        timeZone,
                        culture,
                        languageFile,
                        forumEmail,
                        smtpServer,
                        userName,
                        userEmail,
                        providerUserKey,
                        rolePrefix);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.system_initialize(
                        connectionString,
                        forumName,
                        timeZone,
                        culture,
                        languageFile,
                        forumEmail,
                        smtpServer,
                        userName,
                        userEmail,
                        providerUserKey,
                        rolePrefix);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            int? mid, string script, string scriptFile, bool useTransactions)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The system_initialize_fixaccess.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bGrant">
        /// The b grant.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void system_initialize_fixaccess(int? mid, bool bGrant)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_initialize_fixaccess(connectionString, bGrant);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The system_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable system_list(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.system_list(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.system_list(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.system_list(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.system_list(connectionString);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.system_list(connectionString);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.system_list(connectionString);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.system_list(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void system_updateversion(int? mid, int version, string name)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.system_updateversion(connectionString, version, name);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.system_updateversion(connectionString, version, name);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.system_updateversion(connectionString, version, name);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.system_updateversion(connectionString, version, name);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.system_updateversion(connectionString, version, name);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.system_updateversion(connectionString, version, name); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.system_updateversion(connectionString, version, name); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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

            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_active(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_active(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_active(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_active(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_announcements(
            int? mid, object boardId, object numOfPostsToRetrieve, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_unanswered(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_unanswered(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_unanswered(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_unanswered(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_unread(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_unread(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_unread(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_unread(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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

            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_unread(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.Topics_ByUser(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.Topics_ByUser(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.Topics_ByUser(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void TopicStatus_Delete(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable TopicStatus_Edit(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.TopicStatus_Edit(connectionString, topicStatusID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable TopicStatus_List(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.TopicStatus_List(connectionString, topicStatusID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.TopicStatus_List(connectionString, topicStatusID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.TopicStatus_List(connectionString, topicStatusID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.TopicStatus_List(connectionString, topicStatusID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.TopicStatus_List(connectionString,  topicStatusID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.TopicStatus_List(connectionString,  topicStatusID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.TopicStatus_List(connectionString,  topicStatusID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void TopicStatus_Save(
            int? mid,
            [NotNull] object topicStatusID,
            [NotNull] object boardID,
            [NotNull] object topicStatusName,
            [NotNull] object defaultDescription)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static long topic_create_by_message(int? mid, object messageID, object forumId, object newTopicSubj)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_create_by_message(
                        connectionString, messageID, forumId, newTopicSubj);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_create_by_message(
                        connectionString, messageID, forumId, newTopicSubj);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            topic_delete(mid, topicID, false);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_delete(int? mid, object topicID, object eraseTopic)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_delete(connectionString, topicID, eraseTopic);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_favorite_add(int? mid, object userID, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_favorite_add(connectionString, userID, topicID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_favorite_add(connectionString, userID, topicID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_favorite_add(connectionString, userID, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_favorite_details(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_favorite_details(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_favorite_details(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_favorite_details(
                        connectionString,
                        boardId,
                        categoryId,
                        pageUserId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_favorite_list(int? mid, object userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_favorite_list(connectionString, userID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_favorite_list(connectionString, userID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_favorite_list(connectionString, userID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_favorite_list(connectionString, userID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_favorite_list(connectionString, userID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_favorite_list(connectionString, userID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_favorite_list(connectionString, userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_favorite_remove(int? mid, object userID, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_favorite_remove(connectionString, userID, topicID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int topic_findduplicate(int? mid, object topicName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_findduplicate(connectionString, topicName);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_findduplicate(connectionString, topicName);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_findduplicate(connectionString, topicName);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_findduplicate(connectionString, topicName);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_findduplicate(connectionString, topicName);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_findduplicate(connectionString, topicName);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_findduplicate(connectionString, topicName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_findnext(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_findnext(connectionString, topicID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_findnext(connectionString, topicID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_findnext(connectionString, topicID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_findnext(connectionString, topicID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_findnext(connectionString, topicID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_findnext(connectionString, topicID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_findnext(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_findprev(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_findprev(connectionString, topicID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_findprev(connectionString, topicID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_findprev(connectionString, topicID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_findprev(connectionString, topicID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_findprev(connectionString, topicID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_findprev(connectionString, topicID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_findprev(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The topic_info.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataRow topic_info(int? mid, object topicID, bool getTags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_info(connectionString, topicID, getTags);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_info(connectionString, topicID, getTags);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_info(connectionString, topicID, getTags);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_info(connectionString, topicID, getTags);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_info(connectionString, topicID, getTags);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_info(connectionString, topicID, getTags);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_info(connectionString, topicID, getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_imagesave(
            int? mid, object topicID, object imageUrl, Stream stream, object avatarImageType)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType);
                    break;
                    // case CommonSqlDbAccess.Oracle: VZF.Data.Oracle.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType); break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_imagesave(connectionString, topicID, imageUrl, stream, avatarImageType); break; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_latest(
            int? mid,
            object boardID,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts,
            [CanBeNull] bool findLastRead)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_list(
            int? mid,
            [NotNull] object forumID,
            [NotNull] object userId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [NotNull] object showMoved,
            [CanBeNull] bool findLastRead,
            [NotNull] bool getTags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The announcements_list.
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
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable announcements_list(
            int? mid,
            [NotNull] object forumID,
            [NotNull] object userId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [NotNull] object showMoved,
            [CanBeNull] bool findLastRead,
            [NotNull] bool getTags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.announcements_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.announcements_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.announcements_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.announcements_list(
                        connectionString,
                        forumID,
                        userId,
                        sinceDate,
                        toDate,
                        pageIndex,
                        pageSize,
                        useStyledNicks,
                        showMoved,
                        findLastRead,
                        getTags);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_lock(int? mid, object topicID, object locked)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_lock(connectionString, topicID, locked);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_lock(connectionString, topicID, locked);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_lock(connectionString, topicID, locked); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_lock(connectionString, topicID, locked); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_move(int? mid, object topicID, object forumID, object showMoved, object linkDays)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int topic_prune(
            int? mid,
            [NotNull] object boardID,
            [NotNull] object forumID,
            [NotNull] object days,
            [NotNull] object permDelete)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            ref long messageID,
            string tags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_save(
                        connectionString,
                        forumID,
                        subject,
                        status,
                        styles,
                        description,
                        message,
                        userId,
                        priority,
                        userName,
                        ip,
                        posted,
                        blogPostID,
                        flags,
                        ref messageID,
                        tags);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_save(
                        connectionString,
                        forumID,
                        subject,
                        status,
                        styles,
                        description,
                        message,
                        userId,
                        priority,
                        userName,
                        ip,
                        posted,
                        blogPostID,
                        flags,
                        ref messageID,
                        tags);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_save(
                        connectionString,
                        forumID,
                        subject,
                        status,
                        styles,
                        description,
                        message,
                        userId,
                        priority,
                        userName,
                        ip,
                        posted,
                        blogPostID,
                        flags,
                        ref messageID,
                        tags);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_save(
                        connectionString,
                        forumID,
                        subject,
                        status,
                        styles,
                        description,
                        message,
                        userId,
                        priority,
                        userName,
                        ip,
                        posted,
                        blogPostID,
                        flags,
                        ref messageID,
                        tags);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_tags(int? mid, int boardId, int pageUserId, int topicId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_tags(connectionString,  boardId, pageUserId, topicId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_tags(connectionString,  boardId, pageUserId, topicId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_tags(connectionString,  boardId, pageUserId, topicId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable topic_bytags(
            int? mid, int boardId, int forumId, object pageUserId, string tags, object date, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void topic_updatetopic(int? mid, int topicId, string topic)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.topic_updatetopic(connectionString, topicId, topic);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.topic_updatetopic(connectionString, topicId, topic); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.topic_updatetopic(connectionString, topicId, topic); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void unencode_all_topics_subjects(int? mid, Func<string, string> decodeTopicFunc)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_accessmasks(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_accessmasks(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_accessmasks(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_accessmasks(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_accessmasks(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_accessmasks(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_accessmasks(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_accessmasks(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_accessmasksbyforum(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_accessmasksbyforum(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_accessmasksbygroup(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_accessmasksbygroup(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_activity_rank(int? mid, object boardId, object startDate, object displayNumber)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_addignoreduser(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_addignoreduser(connectionString, userId, ignoredUserId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_addpoints(int? mid, object userId, object forumUserId, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_addpoints(connectionString, userId, forumUserId, points);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_adminsave(
            int? mid,
            object boardId,
            object userId,
            object name,
            object displayName,
            object email,
            object flags,
            object rankID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_approve(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_approve(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_approve(connectionString, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_approve(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_approve(connectionString, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_approve(connectionString, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_approve(connectionString, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_approve(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_approveall(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_approveall(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_approveall(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_approveall(connectionString, boardId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_approveall(connectionString, boardId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_approveall(connectionString, boardId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_approveall(connectionString, boardId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_approveall(connectionString, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_aspnet(
            int? mid,
            int boardId,
            string userName,
            string displayName,
            string email,
            object providerUserKey,
            object isApproved)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_avatarimage(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_avatarimage(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_avatarimage(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_avatarimage(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_avatarimage(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_avatarimage(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_avatarimage(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_avatarimage(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool user_changepassword(int? mid, object userId, object oldPassword, object newPassword)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_delete(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_delete(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_delete(connectionString, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_delete(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_delete(connectionString, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_delete(connectionString, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_delete(connectionString, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_delete(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_deleteavatar(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_deleteavatar(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_deleteavatar(connectionString, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_deleteavatar(connectionString, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_deleteavatar(connectionString, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_deleteavatar(connectionString, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_deleteavatar(connectionString, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_deleteavatar(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_deleteold(int? mid, object boardId, object days)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_deleteold(connectionString, boardId, days);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_deleteold(connectionString, boardId, days);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_deleteold(connectionString, boardId, days); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_deleteold(connectionString, boardId, days); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_emails(int? mid, object boardId, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_emails(connectionString, boardId, groupID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_emails(connectionString, boardId, groupID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_emails(connectionString, boardId, groupID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_emails(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_emails(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_emails(connectionString, boardId, groupID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_emails(connectionString, boardId, groupID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_get(int? mid, int boardId, object providerUserKey)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_get(connectionString, boardId, providerUserKey);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_get(connectionString, boardId, providerUserKey);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_get(connectionString, boardId, providerUserKey);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_get(connectionString, boardId, providerUserKey);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_get(connectionString, boardId, providerUserKey);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_get(connectionString, boardId, providerUserKey);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_get(connectionString, boardId, providerUserKey); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_getalbumsdata(int? mid, object userID, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getalbumsdata(connectionString, userID, boardID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getalbumsdata(connectionString, userID, boardID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_getalbumsdata(connectionString, userID, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_getpoints(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getpoints(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getpoints(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getpoints(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getpoints(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getpoints(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_getpoints(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_getpoints(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string user_getsignature(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getsignature(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getsignature(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getsignature(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getsignature(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getsignature(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_user_getsignature(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_user_getsignature(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_getsignaturedata(int? mid, object userID, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getsignaturedata(connectionString, userID, boardID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getsignaturedata(connectionString, userID, boardID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_getsignaturedata(connectionString, userID, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_getthanks_from(int? mid, object userID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_getthanks_from(connectionString, userID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int[] user_getthanks_to(int? mid, object userID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_getthanks_to(connectionString, userID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int? user_guest(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_guest(connectionString, boardId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_guest(connectionString, boardId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_guest(connectionString, boardId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_guest(connectionString, boardId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_guest(connectionString, boardId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_guest(connectionString, boardId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_guest(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_ignoredlist(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_ignoredlist(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_ignoredlist(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_ignoredlist(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_ignoredlist(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_ignoredlist(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_ignoredlist(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_ignoredlist(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool user_isuserignored(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_isuserignored(connectionString, userId, ignoredUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_pagedlist(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupID,
                        rankID,
                        useStyledNicks,
                        pageIndex,
                        pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_pagedlist(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupID,
                        rankID,
                        useStyledNicks,
                        pageIndex,
                        pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_pagedlist(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupID,
                        rankID,
                        useStyledNicks,
                        pageIndex,
                        pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_pagedlist(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupID,
                        rankID,
                        useStyledNicks,
                        pageIndex,
                        pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_listmedals(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_listmedals(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_listmedals(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_listmedals(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_listmedals(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_listmedals(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_listmedals(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_listmedals(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_listmembers(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupId,
                        rankId,
                        useStyledNicks,
                        lastUserId,
                        literals,
                        exclude,
                        beginsWith,
                        pageIndex,
                        pageSize,
                        sortName,
                        sortRank,
                        sortJoined,
                        sortPosts,
                        sortLastVisit,
                        numPosts,
                        numPostCompare);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_listmembers(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupId,
                        rankId,
                        useStyledNicks,
                        lastUserId,
                        literals,
                        exclude,
                        beginsWith,
                        pageIndex,
                        pageSize,
                        sortName,
                        sortRank,
                        sortJoined,
                        sortPosts,
                        sortLastVisit,
                        numPosts,
                        numPostCompare);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_listmembers(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupId,
                        rankId,
                        useStyledNicks,
                        lastUserId,
                        literals,
                        exclude,
                        beginsWith,
                        pageIndex,
                        pageSize,
                        sortName,
                        sortRank,
                        sortJoined,
                        sortPosts,
                        sortLastVisit,
                        numPosts,
                        numPostCompare);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_listmembers(
                        connectionString,
                        boardId,
                        userId,
                        approved,
                        groupId,
                        rankId,
                        useStyledNicks,
                        lastUserId,
                        literals,
                        exclude,
                        beginsWith,
                        pageIndex,
                        pageSize,
                        sortName,
                        sortRank,
                        sortJoined,
                        sortPosts,
                        sortLastVisit,
                        numPosts,
                        numPostCompare);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_medal_delete(int? mid, object userId, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_medal_delete(connectionString, userId, medalId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_medal_delete(connectionString, userId, medalId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_medal_delete(connectionString, userId, medalId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_medal_list(int? mid, object userId, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_medal_list(connectionString, userId, medalID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_medal_list(connectionString, userId, medalID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_medal_list(connectionString, userId, medalID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_medal_list(connectionString, userId, medalID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_medal_list(connectionString, userId, medalId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_medal_list(connectionString, userId, medalId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_medal_list(connectionString, userId, medalId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_medal_save(
                        connectionString, userId, medalId, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_medal_save(
                        connectionString, userId, medalId, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_medal_save(
                        connectionString, userId, medalId, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_medal_save(
                        connectionString, userId, medalId, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_medal_save(connectionString, userId, medalId, message,hide,  onlyRibbon, sortOrder, dateAwarded);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_medal_save(connectionString, userId, medalId, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_medal_save(connectionString, userId, medalId, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_migrate(int? mid, object userId, object providerUserKey, object updateProvider)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_nntp(int? mid, object boardId, object userName, object email, int? timeZone)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_nntp(connectionString, boardId, userName,  email,timeZone); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_pmcount(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_pmcount(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_pmcount(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_pmcount(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_pmcount(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_pmcount(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_pmcount(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_pmcount(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The user_recoverpassword.
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
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static object user_recoverpassword(int? mid, object boardId, object userName, object email)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_recoverpassword(connectionString, boardId, userName, email); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The user_register.
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
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="hash">
        /// The hash.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="homePage">
        /// The home page.
        /// </param>
        /// <param name="timeZone">
        /// The time zone.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool user_register(
            int? mid,
            object boardId,
            object userName,
            object password,
            object hash,
            object email,
            object location,
            object homePage,
            object timeZone,
            bool approved)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_register(
                        connectionString,
                        boardId,
                        userName,
                        password,
                        hash,
                        email,
                        location,
                        homePage,
                        timeZone,
                        approved);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_register(
                        connectionString,
                        boardId,
                        userName,
                        password,
                        hash,
                        email,
                        location,
                        homePage,
                        timeZone,
                        approved);
                case CommonSqlDbAccess.MySql:
                    return true;
                case CommonSqlDbAccess.Firebird:
                    return true;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_removeignoreduser(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_removepoints(int? mid, object userId, [CanBeNull] object fromUserID, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_removepoints(connectionString, userId, fromUserID, points);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool user_RepliedTopic(int? mid, [NotNull] object messageId, [NotNull] object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_RepliedTopic(connectionString, messageId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_RepliedTopic(connectionString, messageId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_RepliedTopic(connectionString, messageId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_save(
            int? mid,
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
            object notificationType,
            object topicsPerPage,
            object postsPerPage)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_save(
                        connectionString,
                        userId,
                        boardId,
                        userName,
                        displayName,
                        email,
                        timeZone,
                        languageFile,
                        culture,
                        themeFile,
                        useSingleSignOn,
                        textEditor,
                        overrideDefaultThemes,
                        approved,
                        pmNotification,
                        autoWatchTopics,
                        dSTUser,
                        isHidden,
                        notificationType,
                        topicsPerPage,
                        postsPerPage);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_save(
                        connectionString,
                        userId,
                        boardId,
                        userName,
                        displayName,
                        email,
                        timeZone,
                        languageFile,
                        culture,
                        themeFile,
                        useSingleSignOn,
                        textEditor,
                        overrideDefaultThemes,
                        approved,
                        pmNotification,
                        autoWatchTopics,
                        dSTUser,
                        isHidden,
                        notificationType,
                        topicsPerPage,
                        postsPerPage);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_save(
                        connectionString,
                        userId,
                        boardId,
                        userName,
                        displayName,
                        email,
                        timeZone,
                        languageFile,
                        culture,
                        themeFile,
                        useSingleSignOn,
                        textEditor,
                        overrideDefaultThemes,
                        approved,
                        pmNotification,
                        autoWatchTopics,
                        dSTUser,
                        isHidden,
                        notificationType,
                        topicsPerPage,
                        postsPerPage);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_save(
                        connectionString,
                        userId,
                        boardId,
                        userName,
                        displayName,
                        email,
                        timeZone,
                        languageFile,
                        culture,
                        themeFile,
                        useSingleSignOn,
                        textEditor,
                        overrideDefaultThemes,
                        approved,
                        pmNotification,
                        autoWatchTopics,
                        dSTUser,
                        isHidden,
                        notificationType,
                        topicsPerPage,
                        postsPerPage);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_saveavatar(
            int? mid, object userId, object avatar, Stream stream, object avatarImageType)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_savenotification(
            int? mid,
            object userId,
            object pmNotification,
            object autoWatchTopics,
            object notificationType,
            object dailyDigest)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The user_savepassword.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_savepassword(int? mid, object userId, object password)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_savepassword(connectionString, userId, password);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_savepassword(connectionString, userId, password);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_savepassword(connectionString, userId, password);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_savepassword(connectionString, userId, password);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_savepassword(connectionString, userId, password);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_savepassword(connectionString, userId, password); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_savepassword(connectionString, userId, password); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_savesignature(int? mid, object userId, object signature)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_savesignature(connectionString, userId, signature);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_savesignature(connectionString, userId, signature);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_savesignature(connectionString, userId, signature); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_savesignature(connectionString, userId, signature); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The user_setinfo.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_setinfo(int? mid, int boardId, MembershipUser user)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_setinfo(connectionString, boardId, user);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_setinfo(connectionString, boardId, user);
                    break;
                case CommonSqlDbAccess.MySql:
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_setinfo(connectionString, boardId, user);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_setinfo(connectionString, boardId, user);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_setinfo(connectionString, boardId, user); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_setinfo(connectionString, boardId, user); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_setnotdirty(int? mid, int boardId, int userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_setnotdirty(connectionString, boardId, userId);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_setnotdirty(connectionString, boardId, userId); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_setnotdirty(connectionString, boardId, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_setpoints(int? mid, object userId, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_setpoints(connectionString, userId, points);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_setpoints(connectionString, userId, points);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_setpoints(connectionString, userId, points);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_setpoints(connectionString, userId, points);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_setpoints(connectionString, userId, points);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_setpoints(connectionString, userId, points); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_setpoints(connectionString, userId, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_setrole(int? mid, int boardId, object providerUserKey, object role)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_setrole(connectionString, boardId, providerUserKey, role);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_simplelist(connectionString, StartID, Limit);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_simplelist(connectionString, StartID, Limit);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_suspend(int? mid, object userId, object suspend)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_suspend(connectionString, userId, suspend);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_suspend(connectionString, userId, suspend);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_suspend(connectionString, userId, suspend); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_suspend(connectionString, userId, suspend); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void user_update_single_sign_on_status(
            int? mid, [NotNull] object userID, [NotNull] object isFacebookUser, [NotNull] object isTwitterUser)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser);break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool user_ThankedMessage(int? mid, object messageId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_ThankedMessage(connectionString, messageId, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_ThankedMessage(connectionString, messageId, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_ThankedMessage(connectionString, messageId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static int user_ThankFromCount(int? mid, [NotNull] object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_ThankFromCount(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_ThankFromCount(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_ThankFromCount(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_ThankFromCount(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_ThankFromCount(connectionString,  userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_ThankFromCount(connectionString,  userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_ThankFromCount(connectionString,  userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_viewthanksfrom(
            int? mid, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable user_viewthanksto(
            int? mid, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedUserFind> UserFind(
            int? mid,
            int boardId,
            bool filter,
            string userName,
            string email,
            string displayName,
            object notificationType,
            object dailyDigest)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return
                        VZF.Data.MsSql.Db.UserFind(
                            connectionString,
                            boardId,
                            filter,
                            userName,
                            email,
                            displayName,
                            notificationType,
                            dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                case CommonSqlDbAccess.Npgsql:
                    return
                        VZF.Data.Postgre.Db.UserFind(
                            connectionString,
                            boardId,
                            filter,
                            userName,
                            email,
                            displayName,
                            notificationType,
                            dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                case CommonSqlDbAccess.MySql:
                    return
                        VZF.Data.Mysql.Db.UserFind(
                            connectionString,
                            boardId,
                            filter,
                            userName,
                            email,
                            displayName,
                            notificationType,
                            dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                case CommonSqlDbAccess.Firebird:
                    return
                        VZF.Data.Firebird.Db.UserFind(
                            connectionString,
                            boardId,
                            filter,
                            userName,
                            email,
                            displayName,
                            notificationType,
                            dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void userforum_delete(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.userforum_delete(connectionString, userId, forumID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.userforum_delete(connectionString, userId, forumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.userforum_delete(connectionString, userId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable userforum_list(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.userforum_list(connectionString, userId, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.userforum_list(connectionString, userId, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.userforum_list(connectionString, userId, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.userforum_list(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.userforum_list(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.userforum_list(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.userforum_list(connectionString, userId, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void userforum_save(int? mid, object userId, object forumID, object accessMaskID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.userforum_save(connectionString, userId, forumID, accessMaskID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable usergroup_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.usergroup_list(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.usergroup_list(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.usergroup_list(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.usergroup_list(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.usergroup_list(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.usergroup_list(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.usergroup_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void usergroup_save(int? mid, object userId, object groupID, object member)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.usergroup_save(connectionString, userId,  groupID, member);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static IEnumerable<TypedUserList> UserList(
            int? mid, int boardId, int? userId, bool? approved, int? groupID, int? rankID, bool? useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void watchforum_add(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.watchforum_add(connectionString, userId, forumID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.watchforum_add(connectionString, userId, forumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.watchforum_add(connectionString, userId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable watchforum_check(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.watchforum_check(connectionString, userId, forumID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.watchforum_check(connectionString, userId, forumID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.watchforum_check(connectionString, userId, forumID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.watchforum_check(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.watchforum_check(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.watchforum_check(connectionString, userId, forumID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.watchforum_check(connectionString, userId, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void watchforum_delete(int? mid, object watchForumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.watchforum_delete(connectionString, watchForumID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.watchforum_delete(connectionString, watchForumID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.watchforum_delete(connectionString, watchForumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable watchforum_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.watchforum_list(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.watchforum_list(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.watchforum_list(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.watchforum_list(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.watchforum_list(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.watchforum_list(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.watchforum_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void watchtopic_add(int? mid, object userId, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.watchtopic_add(connectionString, userId, topicID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.watchtopic_add(connectionString, userId, topicID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.watchtopic_add(connectionString, userId, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable watchtopic_check(int? mid, object userId, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.watchtopic_check(connectionString, userId, topicID);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.watchtopic_check(connectionString, userId, topicID);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.watchtopic_check(connectionString, userId, topicID);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.watchtopic_check(connectionString, userId, topicID);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.watchtopic_check(connectionString, userId, topicID);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.watchtopic_check(connectionString, userId, topicID);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.watchtopic_check(connectionString, userId, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void watchtopic_delete(int? mid, object watchTopicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    VZF.Data.MsSql.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case CommonSqlDbAccess.Npgsql:
                    VZF.Data.Postgre.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case CommonSqlDbAccess.MySql:
                    VZF.Data.Mysql.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case CommonSqlDbAccess.Firebird:
                    VZF.Data.Firebird.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                    // case CommonSqlDbAccess.Oracle:   VZF.Data.Oracle.Db.watchtopic_delete(connectionString, watchTopicID);break;
                    // case CommonSqlDbAccess.Db2:   VZF.Data.Db2.Db.watchtopic_delete(connectionString, watchTopicID); break;
                    // case CommonSqlDbAccess.Other:   VZF.Data.Other.Db.watchtopic_delete(connectionString, watchTopicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DataTable watchtopic_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.watchtopic_list(connectionString, userId);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.watchtopic_list(connectionString, userId);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.watchtopic_list(connectionString, userId);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.watchtopic_list(connectionString, userId);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.watchtopic_list(connectionString, userId);
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.watchtopic_list(connectionString, userId);
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.watchtopic_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
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
        /// <exception cref="ApplicationException">
        /// </exception>
        public static int GetDbSize(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.GetDBSize(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.GetDBSize(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.GetDbSize(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.GetDBSize(connectionString);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.GetDBSize();
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.GetDBSize();
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.GetDBSize(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
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
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool GetIsForumInstalled(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.GetIsForumInstalled(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.GetIsForumInstalled(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.GetIsForumInstalled(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.GetIsForumInstalled(connectionString);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.GetIsForumInstalled();
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.GetIsForumInstalled();
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.GetIsForumInstalled(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
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
        /// <exception cref="ApplicationException">
        /// </exception>
        public static int GetDBVersion(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.GetDBVersion(connectionString);
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.GetDBVersion(connectionString);
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.GetDbVersion(connectionString);
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.GetDBVersion(connectionString);
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.GetDBVersion();
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.GetDBVersion();
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.GetDBVersion(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.FullTextSupported;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.FullTextSupported;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.FullTextSupported;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.FullTextSupported;
                    ;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.fullTextSupported;;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.fullTextSupported;;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.fullTextSupported;; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.FullTextScript;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.FullTextScript;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.FullTextScript;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.FullTextScript;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.fullTextScript;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.fullTextScript;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.fullTextScript; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        #region ConnectionStringOptions

        /// <summary>
        /// The get provider assembly name.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string GetProviderAssemblyName(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ProviderAssemblyName;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ProviderAssemblyName;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ProviderAssemblyName;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ProviderAssemblyName;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.ProviderAssemblyName;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.ProviderAssemblyName;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.ProviderAssemblyName; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The get password placeholder visible.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static bool GetPasswordPlaceholderVisible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PasswordPlaceholderVisible;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PasswordPlaceholderVisible;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PasswordPlaceholderVisible;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PasswordPlaceholderVisible;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PasswordPlaceholderVisible;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PasswordPlaceholderVisible;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PasswordPlaceholderVisible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The connection parameters.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static List<ConnectionStringParameter> ConnectionParameters(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.MsSqlDbAccess.ConnectionParameters;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.PostgreDbAccess.ConnectionParameters;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.MySqlDbAccess.ConnectionParameters;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.FbDbAccess.ConnectionParameters;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.Parameter19_Value;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.Parameter19_Value;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.Parameter19_Value; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.ScriptList;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.ScriptList;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.ScriptList;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.ScriptList;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.scriptList;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.scriptList;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.scriptList; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelGetStats;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelGetStats;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelGetStats;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelGetStats;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PanelGetStats;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PanelGetStats;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PanelGetStats; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelRecoveryMode;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelRecoveryMode;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelRecoveryMode;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelRecoveryMode;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PanelRecoveryMode;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PanelRecoveryMode;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PanelRecoveryMode; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelReindex;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelReindex;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelReindex;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelReindex;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PanelReindex;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PanelReindex;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PanelReindex; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.PanelShrink;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.PanelShrink;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.PanelShrink;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.PanelShrink;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.PanelShrink;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.PanelShrink;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.PanelShrink; 
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
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case CommonSqlDbAccess.MsSql:
                    return VZF.Data.MsSql.Db.btnReindexVisible;
                case CommonSqlDbAccess.Npgsql:
                    return VZF.Data.Postgre.Db.btnReindexVisible;
                case CommonSqlDbAccess.MySql:
                    return VZF.Data.Mysql.Db.btnReindexVisible;
                case CommonSqlDbAccess.Firebird:
                    return VZF.Data.Firebird.Db.btnReindexVisible;
                    // case CommonSqlDbAccess.Oracle:  return VZF.Data.Oracle.Db.btnReindexVisible;
                    // case CommonSqlDbAccess.Db2:  return VZF.Data.Db2.Db.btnReindexVisible;
                    // case CommonSqlDbAccess.Other:  return VZF.Data.Other.Db.btnReindexVisible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// The data engine name.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DataEngineName(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            return dataEngine;
        }
    }
}