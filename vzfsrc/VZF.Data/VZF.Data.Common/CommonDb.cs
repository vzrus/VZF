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
    using System.IO;
    using System.Linq;
    using System.Web.Security;

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
        /// The module id
        /// .</param>
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.accessmask_delete(connectionString, accessMaskID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.accessmask_delete(connectionString, accessMaskID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.accessmask_delete(connectionString, accessMaskID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.accessmask_delete(connectionString, accessMaskID);
                    // case "oracle": return OracleLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case "db2": return Db2LegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case "other": return OtherLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID); 
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
        public static DataTable accessmask_list(int? mid, object boardId, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "oracle": orPostgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "db2": db2Postgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "other": otherPostgre.Db.accessmask_list(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
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
        public static DataTable accessmask_pforumlist(int? mid, object boardId, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "oracle": orPostgre.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "db2": db2Postgre.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "other": otherPostgre.Db.accessmask_pforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
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
        public static DataTable accessmask_aforumlist(int? mid, object boardId, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            string dataEngine;
            string connectionString;
            CommonSqlDbAccess.GetConnectionData(mid, string.Empty, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "oracle": orPostgre.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "db2": db2Postgre.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
                // case "other": otherPostgre.Db.accessmask_aforumlist(connectionString, boardId, accessMaskID, excludeFlags, pageUserID, isUserMask, isAdminMask);
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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                // case "oracle": orPostgre.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess, userForumAccess,sortOrder,userId,isUserMask,isAdminMask);break;
                // case "db2": db2Postgre.Db.accessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess,userForumAccess, sortOrder,userId,isUserMask,isAdminMask);break;
                // case "other": otherPostgre.Db.accessmask_saveaccessmask_save(connectionString,accessMaskID, boardId, name, readAccess, postAccess, replyAccess, priorityAccess, pollAccess, voteAccess, moderatorAccess, editAccess, deleteAccess, uploadAccess, downloadAccess,userForumAccess, sortOrder,userId,isUserMask,isAdminMask);break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.active_list(
                        connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case "oracle": return orPostgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case "db2": return db2Postgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
                    // case "other": return othPostgre.Db.active_list(connectionString, boardId, guests, showCrawlers, interval, styledNicks);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.active_list_user(
                        connectionString, boardId, userID, guests, showCrawlers, activeTime, styledNicks);
                    // case "oracle": return orPostgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
                    // case "db2": return db2Postgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
                    // case "other": return othPostgre.Db.active_list_user(connectionString, boardId, userID,  guests,  showCrawlers,  activeTime,styledNicks);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.active_listforum(connectionString, forumID, styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.active_listforum(connectionString, forumID, styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.active_listforum(connectionString, forumID, styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.active_listforum(connectionString, forumID, styledNicks);
                    // case "oracle": return orPostgre.Db.active_listforum(connectionString,  forumID, styledNicks);
                    // case "db2": return db2Postgre.Db.active_listforum(connectionString,  forumID, styledNicks);
                    // case "other": return othPostgre.Db.active_listforum(connectionString,  forumID, styledNicks);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.active_listtopic(connectionString, topicID, styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.active_listtopic(connectionString, topicID, styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.active_listtopic(connectionString, topicID, styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case "oracle": return orPostgre.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case "db2": return db2Postgre.Db.active_listtopic(connectionString, topicID, styledNicks);
                    // case "other": return othPostgre.Db.active_listtopic(connectionString, topicID, styledNicks);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.active_stats(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.active_stats(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.active_stats(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.active_stats(connectionString, boardId);
                    // case "oracle": return orPostgre.Db.active_stats(connectionString, boardId);
                    // case "db2": return db2Postgre.Db.active_stats(connectionString, boardId);
                    // case "other": return othPostgre.Db.active_stats(connectionString, boardId);
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.activeaccess_reset(connectionString);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.activeaccess_reset(connectionString);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.activeaccess_reset(connectionString);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.activeaccess_reset(connectionString);
                    break;
                    // case "oracle":  orPostgre.Db.activeaccess_reset(connectionString); break;
                    // case "db2": db2Postgre.Db.activeaccess_reset(connectionString); break;
                    // case "other": othPostgre.Db.activeaccess_reset(connectionString); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.User_ListProfilesByIdsList(
                        connectionString, boardID, userIdsList, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks); 
                    // case "db2":  return db2Postgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);  
                    // case "other":  return othPostgre.Db.User_ListProfilesByIdsList(connectionString,boardID, userIdsList, useStyledNicks);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
                    // case "db2":  return db2Postgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
                    // case "other":  return othPostgre.Db.User_ListTodaysBirthdays(connectionString, (int)boardId, useStyledNicks); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.admin_list(connectionString, (int)boardId, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case "db2":  return db2Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case "other":  return othPostgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.admin_pageaccesslist(connectionString, (int)boardId, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
                    // case "db2":  return db2Postgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
                    // case "other":  return othPostgre.Db.admin_pageaccesslist(connectionString, boardId, useStyledNicks); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.adminpageaccess_list(connectionString, userId, pageName);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.adminpageaccess_list(connectionString, userId, pageName);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.adminpageaccess_list(connectionString, userId, pageName);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.adminpageaccess_list(connectionString, userId, pageName);
                    // case "oracle":  return orPostgre.Db.adminpageaccess_list(connectionString, userId, pageName); 
                    // case "db2":  return db2Postgre.Db.adminpageaccess_list(connectionString, userId, pageName);
                    // case "other":  return othPostgre.Db.adminpageaccess_list(connectionString, userId, pageName); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case "Npgsql":
                    VZF.Data.Postgre.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.adminpageaccess_delete(connectionString, userId, pageName);
                    return;
                    // case "oracle":   orPostgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
                    // case "db2":   db2Postgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;
                    // case "other":   othPostgre.Db.adminpageaccess_delete(connectionString, userId,  pageName); return;

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case "Npgsql":
                    VZF.Data.Postgre.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.adminpageaccess_save(connectionString, userId, pageName);
                    return;
                    // case "oracle":   orPostgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
                    // case "db2":   db2Postgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
                    // case "other":   othPostgre.Db.adminpageaccess_save(connectionString, userId,  pageName); return;
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.album_delete(connectionString, AlbumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.album_delete(connectionString, AlbumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.album_delete(connectionString, AlbumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.album_delete(connectionString, AlbumID);
                    break;
                    // case "oracle":  orPostgre.Db.album_delete(connectionString, AlbumID); break;
                    // case "db2": db2Postgre.Db.album_delete(connectionString, AlbumID); break;
                    // case "other": othPostgre.Db.album_delete(connectionString, AlbumID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_getstats(connectionString, UserID, AlbumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_getstats(connectionString, UserID, AlbumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_getstats(connectionString, UserID, AlbumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_getstats(connectionString, UserID, AlbumID);
                    // case "oracle":  return orPostgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
                    // case "db2": return db2Postgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
                    // case "other": return othPostgre.Db.album_getstats(connectionString,  UserID,  AlbumID);
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_gettitle(connectionString, AlbumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_gettitle(connectionString, AlbumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_gettitle(connectionString, AlbumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_gettitle(connectionString, AlbumID);
                    // case "oracle":  return orPostgre.Db.album_gettitle(connectionString, AlbumID);
                    // case "db2": return db2Postgre.Db.album_gettitle(connectionString, AlbumID);
                    // case "other": return othPostgre.Db.album_gettitle(connectionString, AlbumID);
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.album_image_delete(connectionString, ImageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.album_image_delete(connectionString, ImageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.album_image_delete(connectionString, ImageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.album_image_delete(connectionString, ImageID);
                    break;
                    // case "oracle":  orPostgre.Db.album_image_delete(connectionString, ImageID); break;
                    // case "db2": db2Postgre.Db.album_image_delete(connectionString, ImageID); break;
                    // case "other": othPostgre.Db.album_image_delete(connectionString, ImageID); break;
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.album_image_download(connectionString, ImageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.album_image_download(connectionString, ImageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.album_image_download(connectionString, ImageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.album_image_download(connectionString, ImageID);
                    break;
                    // case "oracle":  orPostgre.Db.album_image_download(connectionString, ImageID); break;
                    // case "db2": db2Postgre.Db.album_image_download(connectionString, ImageID); break;
                    // case "other": othPostgre.Db.album_image_download(connectionString, ImageID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_images_by_user(connectionString, userID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_images_by_user(connectionString, userID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_images_by_user(connectionString, userID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_images_by_user(connectionString, userID);
                    // case "oracle":  return orPostgre.Db.album_images_by_user(connectionString, userID); 
                    // case "db2":  return db2Postgre.Db.album_images_by_user(connectionString, userID); 
                    // case "other":  return othPostgre.Db.album_images_by_user(connectionString, userID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_image_list(connectionString, AlbumID, ImageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_image_list(connectionString, AlbumID, ImageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_image_list(connectionString, AlbumID, ImageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_image_list(connectionString, AlbumID, ImageID);
                    // case "oracle":  return orPostgre.Db.album_image_list(connectionString, AlbumID, ImageID);
                    // case "db2":  return db2Postgre.Db.admin_list(connectionString, (int)boardId, useStyledNicks); 
                    // case "other":  return othPostgre.Db.album_image_list(connectionString, AlbumID, ImageID); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.album_image_save(
                        connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType);
                    break;
                    // case "oracle":  orPostgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
                    // case "db2": db2Postgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
                    // case "other": othPostgre.Db.album_image_save(connectionString, ImageID, AlbumID, Caption, FileName, Bytes, ContentType); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_list(connectionString, UserID, AlbumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_list(connectionString, UserID, AlbumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_list(connectionString, UserID, AlbumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_list(connectionString, UserID, AlbumID);
                    // case "oracle":  return orPostgre.Db.album_list(connectionString, UserID,  AlbumID);
                    // case "db2":  return db2Postgre.Db.album_list(connectionString, UserID,  AlbumID);
                    // case "other":  return othPostgre.Db.album_list(connectionString, UserID,  AlbumID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                    // case "oracle":  return orPostgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
                    // case "db2":  return db2Postgre.Db.album_list(connectionString, UserID,  AlbumID);
                    // case "other":  return othPostgre.Db.album_save(connectionString, AlbumID, UserID, Title, CoverImageID);
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.attachment_delete(connectionString, attachmentID);
                    break;
                    // case "oracle":  orPostgre.Db.attachment_delete(connectionString, attachmentID); break;
                    // case "db2": db2Postgre.Db.attachment_delete(connectionString, attachmentID); break;
                    // case "other": othPostgre.Db.attachment_delete(connectionString, attachmentID); break;
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.attachment_delete(connectionString, attachmentID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.attachment_delete(connectionString, attachmentID);
                    break;
                    // case "oracle":  orPostgre.Db.attachment_delete(connectionString, attachmentID); break;
                    // case "db2": db2Postgre.Db.attachment_delete(connectionString, attachmentID); break;
                    // case "other": othPostgre.Db.attachment_delete(connectionString, attachmentID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.attachment_list(
                        connectionString, messageID, attachmentID, boardId, pageIndex, pageSize);
                    // case "oracle":  return orPostgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
                    // case "db2":  return db2Postgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize);
                    // case "other":  return othPostgre.Db.attachment_list(connectionString, messageID,  attachmentID,  boardId,  pageIndex,  pageSize); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.attachment_save(connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.attachment_save(
                        connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.attachment_save(connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.attachment_save(
                        connectionString, messageID, fileName, bytes, contentType, stream);
                    break;
                    // case "oracle":  orPostgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
                    // case "db2": db2Postgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
                    // case "other": othPostgre.Db.attachment_save(connectionString, messageID,  fileName,  bytes,  contentType,stream); break;
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.bannedip_delete(connectionString, ID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.bannedip_delete(connectionString, ID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.bannedip_delete(connectionString, ID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.bannedip_delete(connectionString, ID);
                    break;
                    // case "oracle":  orPostgre.Db.bannedip_delete(connectionString, ID); break;
                    // case "db2": db2Postgre.Db.bannedip_delete(connectionString, ID); break;
                    // case "other": othPostgre.Db.bannedip_delete(connectionString, ID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.bannedip_list(connectionString, boardId, ID, pageIndex, pageSize);
                    // case "oracle":  return orPostgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
                    // case "db2":  return db2Postgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize);
                    // case "other":  return othPostgre.Db.bannedip_list(connectionString, boardId,  ID,  pageIndex,  pageSize); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.bannedip_save(connectionString, ID, boardId, Mask, reason, userID);
                    break;
                    // case "oracle":  orPostgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
                    // case "db2": db2Postgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
                    // case "other": othPostgre.Db.bannedip_save(connectionString, ID,  boardId,  Mask,  reason,  userID); break;
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.bbcode_delete(connectionString, bbcodeID);
                    break;
                    // case "oracle":  orPostgre.Db.bbcode_delete(connectionString, bbcodeID); break;
                    // case "db2": db2Postgre.Db.bbcode_delete(connectionString, bbcodeID); break;
                    // case "other": othPostgre.Db.bbcode_delete(connectionString, bbcodeID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.bbcode_list(connectionString, boardId, bbcodeID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.bbcode_list(connectionString, boardId, bbcodeID);
                    // case "oracle":  return orPostgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                    // case "db2":  return db2Postgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID);
                    // case "other":  return othPostgre.Db.bbcode_list( connectionString,  boardId,  bbcodeID); 
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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  orPostgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
                    // case "db2": db2Postgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
                    // case "other": othPostgre.Db.bbcode_save(connectionString, bbcodeID, boardId, name, description, onclickjs, displayjs,  editjs,  displaycss,  searchregex,  replaceregex, variables,  usemodule,  moduleclass,  execorder); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.BBCodeList(connectionString, boardId, bbcodeID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case "oracle":  return orPostgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case "db2":  return db2Postgre.Db.BBCodeList(connectionString, boardId, bbcodeID);
                    // case "other":  return othPostgre.Db.BBCodeList(connectionString, boardId, bbcodeID); 
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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
                    // case "db2":  return db2Postgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
                    // case "other":  return othPostgre.Db.board_create(connectionString,  adminUsername,  adminUserEmail,  adminUserKey, boardName, culture,  languageFile,  boardMembershipName,  boardRolesName, rolePrefix, isHostUser); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.board_delete(connectionString, boardId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.board_delete(connectionString, boardId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.board_delete(connectionString, boardId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.board_delete(connectionString, boardId);
                    break;
                    // case "oracle":  orPostgre.Db.board_delete(connectionString, boardId); break;
                    // case "db2": db2Postgre.Db.board_delete(connectionString, boardId); break;
                    // case "other": othPostgre.Db.board_delete(connectionString, boardId); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.board_list(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.board_list(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.board_list(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.board_list(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.board_list(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.board_list(connectionString, boardId);
                    // case "other":  return othPostgre.Db.board_list(connectionString, boardId); 
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
                case "System.Data.SqlClient":
                    return
                        VZF.Data.MsSql.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                    ;
                case "Npgsql":
                    return
                        VZF.Data.Postgre.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                case "MySql.Data.MySqlClient":
                    return
                        VZF.Data.Mysql.Db.board_poststats(connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                case "FirebirdSql.Data.FirebirdClient":
                    return
                        VZF.Data.Firebird.Db.board_poststats(
                            connectionString, boardId, useStyledNicks, showNoCountPosts)
                           .Table.AsEnumerable()
                           .Select(r => new board_poststats_Result(r))
                           .ToList()[0];
                    // case "oracle":  return orPostgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
                    // case "db2":  return db2Postgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
                    // case "other":  return othPostgre.Db.board_poststats(connectionString, boardId,  useStyledNicks, showNoCountPosts).Table.AsEnumerable().Select(r => new board_poststats_Result(r)).ToList()[0]; 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.board_resync(connectionString, boardId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.board_resync(connectionString, boardId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.board_resync(connectionString, boardId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.board_resync(connectionString, boardId);
                    break;
                    // case "oracle":  orPostgre.Db.board_resync(connectionString, boardId); break;
                    // case "db2": db2Postgre.Db.board_resync(connectionString, boardId); break;
                    // case "other": othPostgre.Db.board_resync(connectionString, boardId); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.board_save(
                        connectionString, boardId, languageFile, culture, name, allowThreaded);
                    // case "oracle":  return orPostgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
                    // case "db2":  return db2Postgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
                    // case "other":  return othPostgre.Db.board_save(connectionString,  boardId, languageFile, culture,  name,  allowThreaded); 
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
                case "System.Data.SqlClient":
                    return
                        VZF.Data.MsSql.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case "Npgsql":
                    return
                        VZF.Data.Postgre.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case "MySql.Data.MySqlClient":
                    return
                        VZF.Data.Mysql.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                case "FirebirdSql.Data.FirebirdClient":
                    return
                        VZF.Data.Firebird.Db.board_stats(connectionString, boardId)
                           .Table.AsEnumerable()
                           .Select(r => new board_stats_Result(r))
                           .ToList()[0];
                    // case "oracle":  return orPostgre.Db.board_stats(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.board_stats(connectionString, boardId);
                    // case "other":  return othPostgre.Db.board_stats(connectionString, boardId); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.board_userstats(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.board_userstats(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.board_userstats(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.board_userstats(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.board_userstats(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.board_userstats(connectionString, boardId);
                    // case "other":  return othPostgre.Db.board_userstats(connectionString, boardId); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.buddy_addrequest(connectionString, FromUserID, ToUserID);
                    // case "oracle":  return orPostgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
                    // case "db2":  return db2Postgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID);
                    // case "other":  return othPostgre.Db.buddy_addrequest(connectionString,  FromUserID, ToUserID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case "oracle":  return orPostgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case "db2":  return db2Postgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual);
                    // case "other":  return othPostgre.Db.buddy_approveRequest(connectionString, FromUserID, ToUserID, Mutual); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case "oracle":  return orPostgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case "db2":  return db2Postgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID);
                    // case "other":  return othPostgre.Db.buddy_denyRequest(connectionString, FromUserID, ToUserID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.buddy_list(connectionString, FromUserID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.buddy_list(connectionString, FromUserID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.buddy_list(connectionString, FromUserID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.buddy_list(connectionString, FromUserID);
                    // case "oracle":  return orPostgre.Db.buddy_list(connectionString, FromUserID);
                    // case "db2":  return db2Postgre.Db.buddy_list(connectionString, FromUserID);
                    // case "other":  return othPostgre.Db.buddy_list(connectionString, FromUserID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case "oracle":  return orPostgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case "db2":  return db2Postgre.Db.buddy_remove(connectionString, FromUserID, ToUserID);
                    // case "other":  return othPostgre.Db.buddy_remove(connectionString, FromUserID, ToUserID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_delete(connectionString, CategoryID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_delete(connectionString, CategoryID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_delete(connectionString, CategoryID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_delete(connectionString, CategoryID);
                    // case "oracle":  return orPostgre.Db.category_delete(connectionString, CategoryID);
                    // case "db2":  return db2Postgre.Db.category_delete(connectionString, CategoryID);
                    // case "other":  return othPostgre.Db.category_delete(connectionString, CategoryID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_list(connectionString, boardId, categoryID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_list(connectionString, boardId, categoryID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_list(connectionString, boardId, categoryID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_list(connectionString, boardId, categoryID);
                // case "oracle":  return orPostgre.Db.category_list(connectionString,  boardId, categoryID);
                // case "db2":  return db2Postgre.Db.category_list(connectionString,  boardId, categoryID);
                // case "other":  return othPostgre.Db.category_list(connectionString,  boardId, categoryID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_pfaccesslist(connectionString, boardId, categoryID);
                // case "oracle":  return orPostgre.Db.category_pfaccesslist(connectionString,  boardId, categoryID);
                // case "db2":  return db2Postgre.Db.category_pfaccesslist(connectionString,  boardId, categoryID);
                // case "other":  return othPostgre.Db.category_pfaccesslist(connectionString,  boardId, categoryID); 
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
        public static DataTable category_getadjacentforum(int? mid, object boardId, object categoryID, object userId, bool isAfter)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                // case "oracle":  return orPostgre.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                // case "db2":  return db2Postgre.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter);
                // case "other":  return othPostgre.Db.category_getadjacentforum(connectionString, boardId, categoryID, userId, isAfter); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_listread(connectionString, boardId, userId, categoryID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_listread(connectionString, boardId, userId, categoryID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_listread(connectionString, boardId, userId, categoryID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case "oracle":  return orPostgre.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case "db2":  return db2Postgre.Db.category_listread(connectionString, boardId, userId, categoryID);
                    // case "other":  return othPostgre.Db.category_listread(connectionString, boardId, userId, categoryID); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.category_simplelist(connectionString, startID, limit);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.category_simplelist(connectionString, startID, limit);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.category_simplelist(connectionString, startID, limit);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.category_simplelist(connectionString, startID, limit);
                    // case "oracle":  return orPostgre.Db.category_simplelist(connectionString, startID, limit);
                    // case "db2":  return db2Postgre.Db.category_simplelist(connectionString, startID, limit);
                    // case "other":  return othPostgre.Db.category_simplelist(connectionString, startID, limit); 
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static void category_save(
            int? mid, object boardId, object categoryId, object name, object categoryImage, object sortOrder, object canHavePersForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.category_save(
                        connectionString, boardId, categoryId, name, categoryImage, sortOrder, canHavePersForums);
                    break;
                // case "oracle":  orPostgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
                // case "db2": db2Postgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
                // case "other": othPostgre.Db.category_save(connectionString, boardId,  categoryId,  name,  categoryImage, sortOrder, canHavePersForums); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.checkemail_list(connectionString, email);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.checkemail_list(connectionString, email);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.checkemail_list(connectionString, email);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.checkemail_list(connectionString, email);
                    // case "oracle":  return orPostgre.Db.checkemail_list(connectionString, email);
                    // case "db2":  return db2Postgre.Db.checkemail_list(connectionString, email);
                    // case "other":  return othPostgre.Db.checkemail_list(connectionString, email); 
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.checkemail_save(connectionString, userId, hash, email);
                    break;
                    // case "oracle":  orPostgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
                    // case "db2": db2Postgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
                    // case "other": othPostgre.Db.checkemail_save(connectionString, userId,  hash,  email); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.checkemail_update(connectionString, hash);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.checkemail_update(connectionString, hash);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.checkemail_update(connectionString, hash);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.checkemail_update(connectionString, hash);
                    // case "oracle":  return orPostgre.Db.checkemail_update(connectionString, hash);
                    // case "db2":  return db2Postgre.Db.checkemail_update(connectionString, hash);
                    // case "other":  return othPostgre.Db.checkemail_update(connectionString, hash); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void choice_add(int? mid, object pollID, object choice, object path, object mime)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.choice_add(connectionString, pollID, choice, path, mime);
                    break;
                    // case "oracle":  orPostgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                    // case "db2": db2Postgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                    // case "other": othPostgre.Db.choice_add(connectionString, pollID, choice, path, mime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void choice_delete(int? mid, object choiceID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.choice_delete(connectionString, choiceID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.choice_delete(connectionString, choiceID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.choice_delete(connectionString, choiceID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.choice_delete(connectionString, choiceID);
                    break;
                    // case "oracle":  orPostgre.Db.choice_delete(connectionString, choiceID); break;
                    // case "db2": db2Postgre.Db.choice_delete(connectionString, choiceID); break;
                    // case "other": othPostgre.Db.choice_delete(connectionString, choiceID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void choice_update(int? mid, object choiceID, object choice, object path, object mime)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.choice_update(connectionString, choiceID, choice, path, mime);
                    break;
                    // case "oracle":  orPostgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                    // case "db2": db2Postgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                    // case "other": othPostgre.Db.choice_update(connectionString, choiceID, choice, path, mime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void choice_vote(int? mid, object choiceID, object userId, object remoteIP)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.choice_vote(connectionString, choiceID, userId, remoteIP);
                    break;
                    // case "oracle":  orPostgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                    // case "db2": db2Postgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                    // case "other": othPostgre.Db.choice_vote(connectionString, choiceID, userId, remoteIP); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_getstats_new(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_getstats_new(connectionString);
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_getstats_new(connectionString);
                    ;
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_getstats_new(connectionString);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_getstats_new(connectionString);
                    break;
                    // case "oracle": return  orPostgre.Db.db_getstats_new(connectionString); break;
                    // case "db2": return db2Postgre.Db.db_getstats_new(connectionString); break;
                    // case "other": return othPostgre.Db.db_getstats_new(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static string db_getstats_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_getstats_warning();
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_getstats_warning();
                    ;
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_getstats_warning();
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_getstats_warning();
                    break;
                    // case "oracle": return  orPostgre.Db.db_getstats_warning(); break;
                    // case "db2": return db2_db_getstats_warning(); break;
                    // case "other": return othPostgre.Db.db_getstats_warning(); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_recovery_mode_warning(int? mid, string dbRecoveryMode)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_recovery_mode_warning();
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_recovery_mode_warning();
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_recovery_mode_warning();
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_recovery_mode_warning();
                    break;
                    // case "oracle": return  orPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case "db2": return db2Postgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case "other": return othPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
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
              case "System.Data.SqlClient": return  MsSql.Db.db_getstats_warning(); break;
              case "Npgsql": return Postgre.Db.db_getstats_warning(); ; break;
              case "MySql.Data.MySqlClient": return MySqlDb.Db.db_getstats_warning(); break;
              case "FirebirdSql.Data.FirebirdClient": return FirebirdDb.Db.db_getstats_warning(); break;
              // case "oracle": return  orPostgre.Db.db_getstats_warning(); break;
              // case "db2": return db2_db_getstats_warning(); break;
              // case "other": return othPostgre.Db.db_getstats_warning(); break;
              default:
                  throw new ArgumentOutOfRangeException(dataEngine);
                  break;

          }

      } */
        //Set Recovery
        public static string db_recovery_mode_warning(int? mid)
        {
            return "";
        }

        public static string db_recovery_mode_new(int? mid, string dbRecoveryMode)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_recovery_mode_new(connectionString, dbRecoveryMode);
                    break;
                    // case "oracle": return  orPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case "db2": return db2Postgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                    // case "other": return othPostgre.Db.db_recovery_mode(connectionString, DBName, dbRecoveryMode); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_reindex_new(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_reindex_new(connectionString);
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_reindex_new(connectionString);
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_reindex_new(connectionString);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_reindex_new(connectionString);
                    break;
                    // case "oracle": return orPostgre.Db.db_reindex_new(connectionString); break;
                    // case "db2": return db2Postgre.Db.db_reindex_new(connectionString); break;
                    // case "other": return othPostgre.Db.db_reindex_new(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_reindex_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_reindex_warning();
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_reindex_warning();
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_reindex_warning();
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_reindex_warning();
                    // case "oracle":  return orPostgre.Db.db_reindex_warning();
                    // case "db2":  return db2Postgre.Db.db_reindex_warning();
                    // case "other":  return othPostgre.Db.db_reindex_warning(); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_runsql_new(int? mid, string sql, bool useTransaction)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_runsql_new(connectionString, sql, useTransaction);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_runsql_new(connectionString, sql, useTransaction);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_runsql_new(connectionString, sql, useTransaction);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_runsql_new(connectionString, sql, useTransaction);
                    // case "oracle":  return orPostgre.Db.db_runsql_new(connectionString, sql,  useTransaction);
                    // case "db2":  return db2Postgre.Db.db_runsql_new(connectionString, sql,  useTransaction);
                    // case "other":  return othPostgre.Db.db_runsql_new(connectionString, sql, useTransaction); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_shrink_new(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_shrink_new(connectionString);
                    break;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_shrink_new(connectionString);
                    break;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_shrink_new(connectionString);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_shrink_new(connectionString);
                    break;
                    // case "oracle": return orPostgre.Db.db_shrink(connectionString); break;
                    // case "db2": return db2Postgre.Db.db_shrink(connectionString); break;
                    // case "other": return othPostgre.Db.db_shrink(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string db_shrink_warning(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.db_shrink_warning();
                case "Npgsql":
                    return VZF.Data.Postgre.Db.db_shrink_warning(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.db_shrink_warning();
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.db_shrink_warning();
                    // case "oracle":  return orPostgre.Db.db_shrink_warning(connectionString);
                    // case "db2":  return db2Postgre.Db.db_shrink_warning(connectionStringe);
                    // case "other":  return othPostgre.Db.db_shrink_warning(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }


        public static DataTable forum_byuserlist(int? mid, object boardId, object forumID, object userId, object isUserForum)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_byuserlist(connectionString, boardId, forumID, userId, isUserForum);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_byuserlist(connectionString, boardId, forumID, userId, isUserForum);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_byuserlist(connectionString, boardId, forumID, userId, isUserForum);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_byuserlist(connectionString, boardId, forumID, userId, isUserForum);
                // case "oracle":  return orPostgre.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum);
                // case "db2":  return db2Postgre.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum);
                // case "other":  return othPostgre.Db.forum_list(connectionString, boardId, forumID, userId, isUserForum); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataSet ds_forumadmin(int? mid, object boardId, object pageUserID, object  isUserForum)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                // case "oracle":  return orPostgre.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                // case "db2":  return db2Postgre.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum);
                // case "other":  return othPostgre.Db.ds_forumadmin(connectionString, boardId, pageUserID, isUserForum); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void eventlog_create(int? mid, object userId, object source, object description)
        {
            eventlog_create(mid, userId, (object)source.GetType().ToString(), description, (object)0);
        }

        public static void eventlog_create(int? mid, object userId, object source, object description, object type)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.eventlog_create(connectionString, userId, source, description, type);
                    break;
                    // case "oracle":  orPostgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                    // case "db2": db2Postgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                    // case "other": othPostgre.Db.eventlog_create(connectionString,  userId, source, description,type); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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

        private static void eventlog_delete(int? mid, object eventLogID, object boardId, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.eventlog_delete(connectionString, eventLogID, boardId, pageUserId);
                    break;
                    // case "oracle":  orPostgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                    // case "db2": db2Postgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                    // case "other": othPostgre.Db.eventlog_delete(connectionString, eventLogID, boardId,pageUserId ); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

        public static void eventlog_deletebyuser(int? mid, object boardID, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.eventlog_deletebyuser(connectionString, boardID, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.eventlog_deletebyuser(connectionString, boardID, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.eventlog_deletebyuser(connectionString, boardID, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.eventlog_deletebyuser(connectionString, boardID, userId);
                    break;
                    // case "oracle":  orPostgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                    // case "db2": db2Postgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                    // case "other": othPostgre.Db.eventlog_deletebyuser(connectionString,boardID,userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable eventlog_list(
            int? mid,
            object boardId,
            [NotNull] object pageUserID,
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserID,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserID,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserID,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.eventlog_list(
                        connectionString,
                        boardId,
                        pageUserID,
                        maxRows,
                        maxDays,
                        pageIndex,
                        pageSize,
                        sinceDate,
                        toDate,
                        eventIDs);
                    // case "oracle":  return orPostgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
                    // case "db2":  return db2Postgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs);
                    // case "other":  return othPostgre.Db.eventlog_list(connectionString, boardId, pageUserID,  maxRows, maxDays,  pageIndex, pageSize,  sinceDate,  toDate,  eventIDs); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable eventloggroupaccess_list(
            int? mid, [NotNull] object groupID, [NotNull] object eventTypeId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.eventloggroupaccess_list(connectionString, groupID, eventTypeId);
                    // case "oracle":  return orPostgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
                    // case "db2":  return db2Postgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId);
                    // case "other":  return othPostgre.Db.eventloggroupaccess_list(connectionString,groupID,eventTypeId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_eventlogaccesslist(int? mid, [NotNull] object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_eventlogaccesslist(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_eventlogaccesslist(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_eventlogaccesslist(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.group_eventlogaccesslist(connectionString, boardId);
                    // case "other":  return othPostgre.Db.group_eventlogaccesslist(connectionString, boardId);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void eventloggroupaccess_save(
            int? mid,
            [NotNull] object groupID,
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.eventloggroupaccess_save(
                        connectionString, groupID, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.eventloggroupaccess_save(
                        connectionString, groupID, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.eventloggroupaccess_save(
                        connectionString, groupID, eventTypeId, eventTypeName, deleteAccess);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.eventloggroupaccess_save(
                        connectionString, groupID, eventTypeId, eventTypeName, deleteAccess);
                    break;
                    // case "oracle":  orPostgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                    // case "db2": db2Postgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                    // case "other": othPostgre.Db.eventloggroupaccess_save( connectionString, groupID,  eventTypeId,eventTypeName, deleteAccess); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void extension_delete(int? mid, object extensionId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.extension_delete(connectionString, extensionId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.extension_delete(connectionString, extensionId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.extension_delete(connectionString, extensionId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.extension_delete(connectionString, extensionId);
                    break;
                    // case "oracle":  orPostgre.Db.extension_delete(connectionString, extensionId); break;
                    // case "db2": db2Postgre.Db.extension_delete(connectionString, extensionId); break;
                    // case "other": othPostgre.Db.extension_delete(connectionString, extensionId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void eventloggroupaccess_delete(
            int? mid, [NotNull] object groupID, [NotNull] object eventTypeId, [NotNull] object eventTypeName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.eventloggroupaccess_delete(connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.eventloggroupaccess_delete(
                        connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.eventloggroupaccess_delete(connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.eventloggroupaccess_delete(
                        connectionString, groupID, eventTypeId, eventTypeName);
                    break;
                    // case "oracle":  orPostgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                    // case "db2": db2Postgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                    // case "other": othPostgre.Db.eventloggroupaccess_delete(connectionString,groupID,eventTypeId,eventTypeName); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable extension_edit(int? mid, object extensionId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.extension_edit(connectionString, extensionId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.extension_edit(connectionString, extensionId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.extension_edit(connectionString, extensionId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.extension_edit(connectionString, extensionId);
                    // case "oracle":  return orPostgre.Db.extension_edit(connectionString, extensionId);
                    // case "db2":  return db2Postgre.Db.extension_edit(connectionString, extensionId);
                    // case "other":  return othPostgre.Db.extension_edit(connectionString, extensionId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        // Returns an extension list for a given Board
        public static DataTable extension_list(int? mid, object boardId)
        {
            return extension_list(mid, boardId, string.Empty);

        }

        public static DataTable extension_list(int? mid, object boardId, object extension)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.extension_list(connectionString, boardId, extension);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.extension_list(connectionString, boardId, extension);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.extension_list(connectionString, boardId, extension);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.extension_list(connectionString, boardId, extension);
                    // case "oracle":  return orPostgre.Db.extension_list(connectionString, boardId, extension);
                    // case "db2":  return db2Postgre.Db.extension_list(connectionString, boardId, extension);
                    // case "other":  return othPostgre.Db.extension_list(connectionString, boardId, extension); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void extension_save(int? mid, object extensionId, object boardId, object extension)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.extension_save(connectionString, extensionId, boardId, extension);
                    break;
                    // case "oracle":  orPostgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                    // case "db2": db2Postgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                    // case "other": othPostgre.Db.extension_save(connectionString, extensionId, boardId, extension); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_categoryaccess_activeuser(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.forum_categoryaccess_activeuser(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool forum_delete(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_delete(connectionString, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_delete(connectionString, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_delete(connectionString, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_delete(connectionString, forumID);
                    // case "oracle":  return orPostgre.Db.forum_delete(connectionString, forumID);
                    // case "db2":  return db2Postgre.Db.forum_delete(connectionString, forumID);
                    // case "other":  return othPostgre.Db.forum_delete(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool forum_move(int? mid, [NotNull] object forumOldID, [NotNull] object forumNewID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_move(connectionString, forumOldID, forumNewID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_move(connectionString, forumOldID, forumNewID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_move(connectionString, forumOldID, forumNewID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case "oracle":  return orPostgre.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case "db2":  return db2Postgre.Db.forum_move(connectionString, forumOldID, forumNewID);
                    // case "other":  return othPostgre.Db.forum_move(connectionString, forumOldID, forumNewID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_list(int? mid, object boardId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_list(connectionString, boardId, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_list(connectionString, boardId, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_list(connectionString, boardId, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_list(connectionString, boardId, forumID);
                    // case "oracle":  return orPostgre.Db.forum_list(connectionString, boardId, forumID);
                    // case "db2":  return db2Postgre.Db.forum_list(connectionString, boardId, forumID);
                    // case "other":  return othPostgre.Db.forum_list(connectionString, boardId, forumID); 
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

        public static DataTable forum_listall(int? mid, object boardId, object userId, object startAt, bool returnAll)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case "oracle":  return orPostgre.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case "db2":  return db2Postgre.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll);
                    // case "other":  return othPostgre.Db.forum_listall(connectionString, boardId, userId, startAt, returnAll); 
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
        public static DataTable forum_listall_fromCat(int? mid, object boardId, object categoryID, bool allowUserForumsOnly)
        {
            return forum_listall_fromCat(mid, boardId, categoryID, true, allowUserForumsOnly);
        }

        public static DataTable forum_listall_fromCat(int? mid, object boardId, object categoryID, bool emptyFirstRow, bool allowUserForumsOnly)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_listall_fromCat(
                        connectionString, boardId, categoryID, emptyFirstRow, allowUserForumsOnly);
                // case "oracle":  return orPostgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly);
                // case "db2":  return db2Postgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly);
                // case "other":  return othPostgre.Db.forum_listall_fromCat(connectionString, boardId, categoryID, emptyFirstRow,allowUserForumsOnly); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
            }
        }

        public static DataTable forum_sort_list(
            int? mid,
            DataTable listSource,
            int parentID,
            int categoryID,
            int startingIndent,
            int[] forumidExclusions,
            bool emptyFirstRow,
            bool returnAll)
        {
            var listDestination = new DataTable { TableName = "forum_sort_list" };
            listDestination.Columns.Add("ForumID", typeof(String));
            listDestination.Columns.Add("ParentID", typeof(String));
            listDestination.Columns.Add("Title", typeof(String));
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

            forum_sort_list_recursive(
                mid, dv.ToTable(), listDestination, parentID, categoryID, startingIndent, returnAll);

            return listDestination;
        }

        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId, int[] forumidExclusions)
        {
            return forum_listall_sorted(mid, boardId, userId, null, false, 0, false);
        }

        public static DataTable forum_listall_sorted(int? mid, object boardId, object userId)
        {
            if (!Config.LargeForumTree)
            {

                return forum_listall_sorted(mid, boardId, userId, null, false, 0, false);
            }
            else
            {
                return forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
            }
        }

        public static DataTable forum_listall_sorted_all(int? mid, object boardId, object userId, bool returnAll)
        {
            if (!Config.LargeForumTree)
            {

                return forum_listall_sorted(mid, boardId, userId, null, false, 0, returnAll);
            }
            else
            {
                return forum_ns_getchildren_activeuser(mid, (int)boardId, 0, 0, (int)userId, false, false, "-");
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  dtTable = orPostgre.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                    // case "db2":  dtTable = db2Postgre.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                    // case "other":  dtTable = othPostgre.Db.forum_ns_getchildren_activeuser(connectionString,  boardid ?? 0,  categoryid ?? 0,  forumid ?? 0,  userid,  notincluded,  immediateonly,  indentchars);break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

            return dtTable;
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_ns_getchildren(
                        connectionString,
                        boardid ?? 0,
                        categoryid ?? 0,
                        forumid ?? 0,
                        notincluded,
                        immediateonly,
                        indentchars);
                    // case "oracle":  return orPostgre.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars);
                    // case "db2":  return db2Postgre.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars)
                    // case "other":  return othPostgre.Db.forum_ns_getchildren(connectionString, boardid ?? 0, categoryid ?? 0, forumid ?? 0, notincluded, immediateonly, indentchars)
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

        public static void forum_list_sort_basic(
            int? mid, DataTable listsource, DataTable list, int parentid, int currentLvl)
        {
            for (int i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentid)
                {
                    string sIndent = "";
                    int iIndent = Convert.ToInt32(currentLvl);
                    for (int j = 0; j < iIndent; j++) sIndent += "--";
                    row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                    list.Rows.Add(row.ItemArray);
                    forum_list_sort_basic(mid, listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
            }
        }

        public static void forum_sort_list_recursive(
            int? mid,
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int categoryID,
            int currentIndent,
            bool returnAll)
        {
            DataRow newRow;

            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentID)
                {
                    if ((int)row["CategoryID"] != categoryID)
                    {
                        categoryID = (int)row["CategoryID"];

                        newRow = listDestination.NewRow();
                        newRow["ForumID"] = -categoryID; // Ederon : 9/4/2007
                        newRow["Title"] = string.Format("{0}", row["Category"].ToString());
                        listDestination.Rows.Add(newRow);
                    }

                    string sIndent = "";

                    for (int j = 0; j < currentIndent; j++) sIndent += "--";

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
        }

        public static DataTable forum_tags(int? mid, int boardId, int pageUserId, int forumId, int pageIndex, int pageSize, string searchText, bool beginsWith)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                // case "oracle":  return orPostgre.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                // case "db2":  return db2Postgre.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                // case "other":  return othPostgre.Db.forum_tags(connectionString, boardId, pageUserId, forumId, pageIndex, pageSize, searchText, beginsWith);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_listallMyModerated(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_listallMyModerated(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.forum_listallMyModerated(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.forum_listallMyModerated(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_listpath(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_listpath(connectionString, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_listpath(connectionString, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_listpath(connectionString, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_listpath(connectionString, forumID);
                    // case "oracle":  return orPostgre.Db.forum_listpath(connectionString, forumID);
                    // case "db2":  return db2Postgre.Db.forum_listpath(connectionString, forumID);
                    // case "other":  return othPostgre.Db.forum_listpath(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_listread(
            int? mid,
            object boardID,
            object userID,
            object categoryID,
            object parentID,
            object useStyledNicks,
            bool findLastRead)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_listread(
                        connectionString, boardID, userID, categoryID, parentID, useStyledNicks, findLastRead);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_listread(
                        connectionString, boardID, userID, categoryID, parentID, useStyledNicks, findLastRead);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_listread(
                        connectionString, boardID, userID, categoryID, parentID, useStyledNicks, findLastRead);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_listread(
                        connectionString, boardID, userID, categoryID, parentID, useStyledNicks, findLastRead);
                    // case "oracle":  return orPostgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead);
                    // case "db2":  return db2Postgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead);
                    // case "other":  return othPostgre.Db.forum_listread(connectionString,boardId,userId, categoryID, parentID, useStyledNicks, findLastRead); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataSet forum_moderatelist(int? mid, object userId, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_moderatelist(connectionString, userId, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_moderatelist(connectionString, userId, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_moderatelist(connectionString, userId, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "oracle":  return orPostgre.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "db2":  return db2Postgre.Db.forum_moderatelist(connectionString, userId, boardId);
                // case "other":  return othPostgre.Db.forum_moderatelist(connectionString, userId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_moderators(int? mid, bool useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_moderators(connectionString, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_moderators(connectionString, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_moderators(connectionString, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_moderators(connectionString, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.forum_moderators(connectionString, useStyledNicks);
                    // case "db2":  return db2Postgre.Db.forum_moderators(connectionString, useStyledNicks);
                    // case "other":  return othPostgre.Db.forum_moderators(connectionString, useStyledNicks); 
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

        public static void forum_resync(int? mid, object boardId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.forum_resync(connectionString, boardId, forumID);
                    break;
                    ;
                    // case "oracle":   orPostgre.Db.forum_resync(connectionString, boardId, forumID); break;;
                    // case "db2":   db2Postgre.Db.forum_resync(connectionString, boardId, forumID); break;
                    // case "other":   othPostgre.Db.forum_resync(connectionString, boardId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                // case "oracle":  return orPostgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                // case "oracle":  return orPostgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                // case "db2":  return db2Postgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                // case "other":  return othPostgre.Db.forum_save(connectionString, forumID,categoryID,  parentID, name, description,  sortOrder,  locked, hidden,  isTest,  moderated, accessMaskID,  remoteURL, themeURL,imageURL,styles,dummy, userId,isUserForum, canhavepersforums);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int forum_save_parentschecker(int? mid, object forumID, object parentID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case "oracle":  return orPostgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case "db2":  return db2Postgre.Db.forum_save_parentschecker(connectionString, forumID, parentID);
                    // case "other":  return othPostgre.Db.forum_save_parentschecker(connectionString, forumID, parentID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forum_simplelist(int? mid, int startID, int limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forum_simplelist(connectionString, startID, limit);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forum_simplelist(connectionString, startID, limit);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forum_simplelist(connectionString, startID, limit);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forum_simplelist(connectionString, startID, limit);
                    // case "oracle":  return orPostgre.Db.forum_simplelist(connectionString, startID, limit);
                    // case "db2":  return db2Postgre.Db.forum_simplelist(connectionString, startID, limit);
                    // case "other":  return othPostgre.Db.forum_simplelist(connectionString, startID, limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable forumaccess_group(int? mid, object groupID, object userId, bool includeUserForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                // case "oracle":  return orPostgre.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                // case "db2":  return db2Postgre.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums);
                // case "other":  return othPostgre.Db.forumaccess_group(connectionString, groupID, userId, includeUserForums); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }
        public static DataTable forumaccess_personalgroup(int? mid, object groupID, object userId, bool includeUserForums)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                // case "oracle":  return orPostgre.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                // case "db2":  return db2Postgre.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums);
                // case "other":  return othPostgre.Db.forumaccess_personalgroup(connectionString, groupID, userId, includeUserForums); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }


        public static DataTable forumaccess_list(int? mid, object forumID, object userId, bool includeUserGroups)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                // case "oracle":  return orPostgre.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                // case "db2":  return db2Postgre.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups);
                // case "other":  return othPostgre.Db.forumaccess_list(connectionString, forumID, userId, includeUserGroups); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void forumaccess_save(int? mid, object forumID, object groupID, object accessMaskID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID);
                    break;
                    ;
                    // case "oracle":   orPostgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;;
                    // case "db2":   db2Postgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                    // case "other":   othPostgre.Db.forumaccess_save(connectionString, forumID, groupID, accessMaskID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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

        public static IEnumerable<TypedForumListAll> ForumListAll(
            int? mid, int boardId, int userId, List<int> startForumId, bool includeNoAccess)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case "oracle":  return orPostgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case "db2":  return db2Postgre.Db.ForumListAll(connectionString, boardId, userId, startForumId);
                    // case "other":  return othPostgre.Db.ForumListAll(connectionString, boardId, userId, startForumId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool forumpage_initdb(int? mid, out string errorStr, bool debugging)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forumpage_initdb(connectionString, out errorStr, debugging);
                    // case "oracle":  return orPostgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                    // case "db2":  return db2Postgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging);
                    // case "other":  return othPostgre.Db.forumpage_initdb(connectionString, out  errorStr,  debugging); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string forumpage_validateversion(int? mid, int appVersion)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.forumpage_validateversion(connectionString, mid, appVersion);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case "oracle":  return orPostgre.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case "db2":  return db2Postgre.Db.forumpage_validateversion(connectionString, mid, appVersion);
                    // case "other":  return othPostgre.Db.forumpage_validateversion(connectionString, mid, appVersion); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case "db2":  return db2Postgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                    // case "other":  return othPostgre.Db.GetSearchResult(connectionString, toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, categoryId, forumIDToStartAt, userId, boardId, maxResults, useFullText, searchDisplayName, includeChildren);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void group_delete(int? mid, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.group_delete(connectionString, groupID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.group_delete(connectionString, groupID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.group_delete(connectionString, groupID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.group_delete(connectionString, groupID);
                    break;
                    ;
                    // case "oracle":   orPostgre.Db.group_delete(connectionString, groupID); break;;
                    // case "db2":   db2Postgre.Db.group_delete(connectionString, groupID); break;
                    // case "other":   othPostgre.Db.group_delete(connectionString, groupID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_list(int? mid, object boardId, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_list(connectionString, boardId, groupID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_list(connectionString, boardId, groupID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_list(connectionString, boardId, groupID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_list(connectionString, boardId, groupID);
                    // case "oracle":  return orPostgre.Db.group_list(connectionString, boardId, groupID);
                    // case "db2":  return db2Postgre.Db.group_list(connectionString, boardId, groupID);
                    // case "other":  return othPostgre.Db.group_list(connectionString, boardId, groupID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_byuserlist(int? mid, object boardId, object groupID, object userId, object isUserGroup)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                // case "oracle":  return orPostgre.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                // case "db2":  return db2Postgre.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup);
                // case "other":  return othPostgre.Db.group_byuserlist(connectionString, boardId, groupID, userId, isUserGroup); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void group_medal_delete(int? mid, object groupID, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.group_medal_delete(connectionString, groupID, medalID);
                    break;
                    // case "oracle":   orPostgre.Db.group_medal_delete(connectionString, groupID, medalID);break;
                    // case "db2":   db2Postgre.Db.group_medal_delete(connectionString, groupID, medalID); break;
                    // case "other":   othPostgre.Db.group_medal_delete(connectionString, groupID, medalID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_medal_list(int? mid, object groupID, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_medal_list(connectionString, groupID, medalID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_medal_list(connectionString, groupID, medalID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_medal_list(connectionString, groupID, medalID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_medal_list(connectionString, groupID, medalID);
                    // case "oracle":  return orPostgre.Db.group_medal_list(connectionString, groupID, medalID);
                    // case "db2":  return db2Postgre.Db.group_medal_list(connectionString, groupID, medalID);
                    // case "other":  return othPostgre.Db.group_medal_list(connectionString, groupID, medalID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void group_medal_save(
            int? mid, object groupID, object medalID, object message, object hide, object onlyRibbon, object sortOrder)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.group_medal_save(
                        connectionString, groupID, medalID, message, hide, onlyRibbon, sortOrder);
                    break;
                    // case "oracle":   orPostgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder);break;
                    // case "db2":   db2Postgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                    // case "other":   othPostgre.Db.group_medal_save(connectionString, groupID, medalID, message, hide, onlyRibbon,  sortOrder); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_member(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_member(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_member(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_member(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_member(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.group_member(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.group_member(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.group_member(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable group_rank_style(int? mid, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.group_rank_style(connectionString, boardID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.group_rank_style(connectionString, boardID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.group_rank_style(connectionString, boardID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.group_rank_style(connectionString, boardID);
                    // case "oracle":  return orPostgre.Db.group_rank_style(connectionString, boardID);
                    // case "db2":  return db2Postgre.Db.group_rank_style(connectionString, boardID);
                    // case "other":  return othPostgre.Db.group_rank_style(connectionString, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
                    break;
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                // case "oracle":  return orPostgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden,accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber);
                // case "db2":  return db2Postgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden, accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber);
                // case "other":  return othPostgre.Db.group_save(connectionString, groupID, boardId, name, isAdmin, isGuest, isStart, isModerator, isHidden,accessMaskID, pmLimit, style, sortOrder,description,usrSigChars,usrSigBBCodes,usrSigHTMLTags,usrAlbums,usrAlbumImages,userId,isUserGroup,personalForumsNumber,personalAccessMasksNumber,personalGroupsNumber); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.mail_create(
                        connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.mail_create(
                        connectionString, from, fromName, to, toName, subject, body, bodyHtml);
                    break;
                    // case "oracle":   orPostgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml);break;
                    // case "db2":   db2Postgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                    // case "other":   othPostgre.Db.mail_create(connectionString, from, fromName, to, toName, subject, body, bodyHtml); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.mail_createwatch(
                        connectionString, topicID, from, fromName, subject, body, bodyHtml, userId);
                    break;
                    // case "oracle":   orPostgre.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId);break;
                    // case "db2":   db2_mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                    // case "other":   othPostgre.Db.mail_createwatch(connectionString,  topicID, from, fromName, subject, body, bodyHtml, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void mail_delete(int? mid, object mailID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.mail_delete(connectionString, mailID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.mail_delete(connectionString, mailID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.mail_delete(connectionString, mailID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.mail_delete(connectionString, mailID);
                    break;
                    // case "oracle":   orPostgre.Db.mail_delete(connectionString, mailID);break;
                    // case "db2":   db2_mail_delete(connectionString, mailID); break;
                    // case "other":   othPostgre.Db.mail_delete(connectionString, mailID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedMailList> MailList(int? mid, long processId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.MailList(connectionString, processId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.MailList(connectionString, processId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.MailList(connectionString, processId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.MailList(connectionString, processId);
                    // case "oracle":  return orPostgre.Db.MailList(connectionString, processId);
                    // case "db2":  return db2Postgre.Db.MailList(connectionString, processId);
                    // case "other":  return othPostgre.Db.MailList(connectionString, processId); 
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

        public static void medal_delete(int? mid, object boardId, object medalID, object category)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.medal_delete(connectionString, boardId, medalID, category);
                    break;
                    // case "oracle":   orPostgre.Db.medal_delete(connectionString, boardId,  medalID, category);break;
                    // case "db2":   db2Postgre.Db.medal_delete(connectionString, boardId,  medalID, category); break;
                    // case "other":   othPostgre.Db.medal_delete(connectionString, boardId,  medalID, category); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.medal_list(connectionString, null, medalID, null);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.medal_list(connectionString, null, medalID, null);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.medal_list(connectionString, null, medalID, null);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.medal_list(connectionString, null, medalID, null);
                    // case "oracle":  return orPostgre.Db.medal_list(connectionString, null, medalID, null);
                    // case "db2":  return db2Postgre.Db.medal_list(connectionString, null, medalID, null);
                    // case "other":  return othPostgre.Db.medal_list(connectionString, null, medalID, null); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }


        }

        /// <summary>
        /// Lists given medals.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">ID of board of which medals to list. Required.</param>
        /// <param name="category">Cateogry of medals to list. Can be null. In such case this parameter is ignored.</param>
        public static DataTable medal_list(int? mid, object boardId, object category)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.medal_list(connectionString, boardId, null, category);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.medal_list(connectionString, boardId, null, category);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.medal_list(connectionString, boardId, null, category);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.medal_list(connectionString, boardId, null, category);
                    // case "oracle":  return orPostgre.Db.medal_list(connectionString, boardId, null, category);
                    // case "db2":  return db2Postgre.Db.medal_list(connectionString, boardId, null, category);
                    // case "other":  return othPostgre.Db.medal_list(connectionString, boardId, null, category); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        // DataTable mail_list(object processId); 

        /// <summary>
        /// Lists given medals.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID"> </param>
        /// <param name="boardId">ID of board of which medals to list. Required.</param>
        /// <param name="category">Cateogry of medals to list. Can be null. In such case this parameter is ignored.</param>
        public static DataTable medal_listusers(int? mid, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.medal_listusers(connectionString, medalID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.medal_listusers(connectionString, medalID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.medal_listusers(connectionString, medalID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.medal_listusers(connectionString, medalID);
                    // case "oracle":  return orPostgre.Db.medal_listusers(connectionString, medalID);
                    // case "db2":  return db2Postgre.Db.medal_listusers(connectionString, medalID);
                    // case "other":  return othPostgre.Db.medal_listusers(connectionString, medalID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void medal_resort(int? mid, object boardId, object medalID, int move)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.medal_resort(connectionString, boardId, medalID, move);
                    break;
                    // case "oracle":   orPostgre.Db.medal_resort(connectionString, boardId, medalID, move);break;
                    // case "db2":   db2Postgre.Db.medal_resort(connectionString, boardId, medalID, move); break;
                    // case "other":   othPostgre.Db.medal_resort(connectionString, boardId, medalID, move); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                    // case "db2":  return db2Postgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags);
                    // case "other":  return othPostgre.Db.medal_save(connectionString, boardId, medalID, name, description, message, category, medalURL, ribbonURL, smallMedalURL, smallRibbonURL, smallMedalWidth, smallMedalHeight, smallRibbonWidth, smallRibbonHeight, sortOrder, flags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static string message_AddThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_AddThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_AddThanks(connectionString, fromUserID, messageID, useDisplayName);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_AddThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                    // case "oracle":  return or_message_AddThanks(connectionString, fromUserID, messageID,useDisplayName,useDisplayName);
                    // case "db2":  return db2Postgre.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case "other":  return othPostgre.Db.message_AddThanks(connectionString, fromUserID, messageID,useDisplayName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void message_approve(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_approve(connectionString, messageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_approve(connectionString, messageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_approve(connectionString, messageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_approve(connectionString, messageID);
                    break;
                    // case "oracle":   orPostgre.Db.message_approve(connectionString, messageID);break;
                    // case "db2":   db2Postgre.Db.message_approve(connectionString, messageID); break;
                    // case "other":   othPostgre.Db.message_approve(connectionString, messageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_delete(
                        connectionString,
                        messageID,
                        isModeratorChanged,
                        deleteReason,
                        isDeleteAction,
                        DeleteLinked,
                        eraseMessage);
                    break;
                    // case "oracle":   orPostgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage);break;
                    // case "db2":   db2Postgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                    // case "other":   othPostgre.Db.message_delete(connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction,DeleteLinked,eraseMessage); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable message_findunread(
            int? mid, object topicID, object messageId, object lastRead, object showDeleted, object authorUserID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_findunread(
                        connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case "oracle":  return orPostgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case "db2":  return db2Postgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID);
                    // case "other":  return othPostgre.Db.message_findunread(connectionString, topicID, messageId, lastRead, showDeleted, authorUserID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_getRepliesList(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_getRepliesList(connectionString, messageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_getRepliesList(connectionString, messageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_getRepliesList(connectionString, messageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_getRepliesList(connectionString, messageID);
                    // case "oracle":  return orPostgre.Db.message_getRepliesList(connectionString, messageID);
                    // case "db2":  return db2Postgre.Db.message_getRepliesList(connectionString, messageID);
                    // case "other":  return othPostgre.Db.message_getRepliesList(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_GetTextByIds(int? mid, string messageIDs)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_GetTextByIds(connectionString, messageIDs);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_GetTextByIds(connectionString, messageIDs);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_GetTextByIds(connectionString, messageIDs);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case "oracle":  return orPostgre.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case "db2":  return db2Postgre.Db.message_GetTextByIds(connectionString, messageIDs);
                    // case "other":  return othPostgre.Db.message_GetTextByIds(connectionString, messageIDs); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_GetThanks(int mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_GetThanks(connectionString, messageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_GetThanks(connectionString, messageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_GetThanks(connectionString, messageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_GetThanks(connectionString, messageID);
                    // case "oracle":  return orPostgre.Db.message_GetThanks(connectionString, messageID);
                    // case "db2":  return db2Postgre.Db.message_GetThanks(connectionString, messageID);
                    // case "other":  return othPostgre.Db.message_GetThanks(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_list(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_list(connectionString, messageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_list(connectionString, messageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_list(connectionString, messageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_list(connectionString, messageID);
                    // case "oracle":  return orPostgre.Db.message_list(connectionString, messageID);
                    // case "db2":  return db2Postgre.Db.message_list(connectionString, messageID);
                    // case "other":  return othPostgre.Db.message_list(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_listreported(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_listreported(connectionString, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_listreported(connectionString, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_listreported(connectionString, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_listreported(connectionString, forumID);
                    // case "oracle":  return orPostgre.Db.message_listreported(connectionString, forumID);
                    // case "db2":  return db2Postgre.Db.message_listreported(connectionString, forumID);
                    // case "other":  return othPostgre.Db.message_listreported(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Here we get reporters list for a reported message
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="messageID"> </param>
        /// <param name="MessageID">Should not be NULL</param>
        /// <returns>Returns reporters DataTable for a reported message.</returns>
        public static DataTable message_listreporters(int? mid, int messageID)
        {
            return message_listreporters(mid, messageID, null);
        }

        public static DataTable message_listreporters(int? mid, int messageID, object userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_listreporters(connectionString, messageID, userID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_listreporters(connectionString, messageID, userID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_listreporters(connectionString, messageID, userID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_listreporters(connectionString, messageID, userID);
                    // case "oracle":  return orPostgre.Db.message_listreporters(connectionString, messageID, userID);
                    // case "db2":  return db2Postgre.Db.message_listreporters(connectionString, messageID, userID);
                    // case "other":  return othPostgre.Db.message_listreporters(connectionString, messageID, userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void message_move(int? mid, object messageID, object moveToTopic, bool moveAll)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_move(connectionString, messageID, moveToTopic, moveAll);
                    break;
                    // case "oracle":   orPostgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll);break;
                    // case "db2":   db2Postgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                    // case "other":   othPostgre.Db.message_move(connectionString, messageID, moveToTopic, moveAll); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string message_RemoveThanks(int? mid, object fromUserID, object messageID, bool useDisplayName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_RemoveThanks(
                        connectionString, fromUserID, messageID, useDisplayName);
                    // case "oracle":  return orPostgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case "db2":  return db2Postgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName);
                    // case "other":  return othPostgre.Db.message_RemoveThanks(connectionString, fromUserID, messageID,useDisplayName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void message_report(
            int? mid, object messageID, object userId, object reportedDateTime, object reportText)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_report(
                        connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_report(
                        connectionString, messageID, userId, reportedDateTime, reportText);
                    break;
                    // case "oracle":   orPostgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText);break;
                    // case "db2":   db2Postgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                    // case "other":   othPostgre.Db.message_report(connectionString, messageID, userId, reportedDateTime, reportText); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void message_reportcopyover(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_reportcopyover(connectionString, messageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_reportcopyover(connectionString, messageID);
                    break;
                    // case "oracle":   orPostgre.Db.message_reportcopyover(connectionString, messageID);break;
                    // case "db2":   db2Postgre.Db.message_reportcopyover(connectionString, messageID); break;
                    // case "other":   othPostgre.Db.message_reportcopyover(connectionString, messageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void message_reportresolve(int? mid, object messageFlag, object messageID, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);
                    break;
                    // case "oracle":   orPostgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId);break;
                    // case "db2":   db2Postgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                    // case "other":   othPostgre.Db.message_reportresolve(connectionString, messageFlag, messageID, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_save(
                        connectionString, topicId, userId, message, userName, ip, posted, replyTo, flags, ref messageId);
                    // case "oracle":  return orPostgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                    // case "db2":  return db2Postgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId);
                    // case "other":  return othPostgre.Db.message_save(connectionString, topicId,userId,message,userName,ip,posted,replyTo,flags,ref  messageId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_secdata(int? mid, int MessageID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_secdata(connectionString, MessageID, pageUserId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_secdata(connectionString, MessageID, pageUserId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case "oracle":  return orPostgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case "db2":  return db2Postgre.Db.message_secdata(connectionString, MessageID, pageUserId);
                    // case "other":  return othPostgre.Db.message_secdata(connectionString, MessageID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_simplelist(connectionString, StartID, Limit);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_simplelist(connectionString, StartID, Limit);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_simplelist(connectionString, StartID, Limit);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_simplelist(connectionString, StartID, Limit);
                    // case "oracle":  return orPostgre.Db.message_simplelist(connectionString, StartID, Limit);
                    // case "db2":  return db2Postgre.Db.message_simplelist(connectionString, StartID, Limit);
                    // case "other":  return othPostgre.Db.message_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static int message_ThanksNumber(int? mid, object messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_ThanksNumber(connectionString, messageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_ThanksNumber(connectionString, messageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_ThanksNumber(connectionString, messageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_ThanksNumber(connectionString, messageID);
                    // case "oracle":  return orPostgre.Db.message_ThanksNumber(connectionString, messageID);
                    // case "db2":  return db2Postgre.Db.message_ThanksNumber(connectionString, messageID);
                    // case "other":  return othPostgre.Db.message_ThanksNumber(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable message_unapproved(int? mid, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.message_unapproved(connectionString, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.message_unapproved(connectionString, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.message_unapproved(connectionString, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.message_unapproved(connectionString, forumID);
                    // case "oracle":  return orPostgre.Db.message_unapproved(connectionString, forumID);
                    // case "db2":  return db2Postgre.Db.message_unapproved(connectionString, forumID);
                    // case "other":  return othPostgre.Db.message_unapproved(connectionString, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":   orPostgre.Db.message_update(connectionString, messageID, priority, message, description, status,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags);break;
                    // case "db2":   db2Postgre.Db.message_update(connectionString, messageID, priority, message, description, status,styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags); break;
                    // case "other":   othPostgre.Db.message_update(connectionString, messageID, priority, message, description, status, styles,subject,flags, reasonOfEdit,  isModeratorChanged,  overrideApproval,origMessage,  editedBy,tags); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedAllThanks> MessageGetAllThanks(int? mid, string messageIdsSeparatedWithColon)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case "oracle":  return orPostgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case "db2":  return db2Postgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon);
                    // case "other":  return othPostgre.Db.MessageGetAllThanks(connectionString, messageIdsSeparatedWithColon); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable messagehistory_list(int? mid, int messageID, int daysToClean)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.messagehistory_list(connectionString, messageID, daysToClean);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case "oracle":  return orPostgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case "db2":  return db2Postgre.Db.messagehistory_list(connectionString, messageID, daysToClean);
                    // case "other":  return othPostgre.Db.messagehistory_list(connectionString, messageID, daysToClean); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedMessageList> MessageList(int? mid, int messageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.MessageList(connectionString, messageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.MessageList(connectionString, messageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.MessageList(connectionString, messageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.MessageList(connectionString, messageID);
                    // case "oracle":  return orPostgre.Db.MessageList(connectionString, messageID);
                    // case "db2":  return db2Postgre.Db.MessageList(connectionString, messageID);
                    // case "other":  return othPostgre.Db.MessageList(connectionString, messageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable moderators_team_list(int? mid, bool useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.moderators_team_list(connectionString, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.moderators_team_list(connectionString, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.moderators_team_list(connectionString, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.moderators_team_list(connectionString, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.moderators_team_list( connectionString,  useStyledNicks);
                    // case "db2":  return db2Postgre.Db.moderators_team_list( connectionString,  useStyledNicks);
                    // case "other":  return othPostgre.Db.moderators_team_list( connectionString,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void Readtopic_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.Readtopic_AddOrUpdate(connectionString, userID, topicID);
                    break;
                    // case "oracle":   orPostgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID);break;
                    // case "db2":   db2Postgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
                    // case "other":   othPostgre.Db.Readtopic_AddOrUpdate( connectionString,  userID,   topicID); break;
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
                 case "System.Data.SqlClient": MsSql.Db.Readtopic_delete(connectionString, trackingID); break;
                 case "Npgsql": Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case "oracle":   orPostgre.Db.Readtopic_delete(connectionString, trackingID);break;
                 // case "db2":   db2Postgre.Db.Readtopic_delete(connectionString, trackingID); break;
                 // case "other":   othPostgre.Db.Readtopic_delete(connectionString, trackingID); break;
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.User_LastRead(connectionString, userID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.User_LastRead(connectionString, userID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.User_LastRead(connectionString, userID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.User_LastRead(connectionString, userID);
                    // case "oracle":  return orPostgre.Db.User_LastRead( connectionString,  userID);
                    // case "db2":  return db2Postgre.Db.User_LastRead( connectionString,  userID);
                    // case "other":  return othPostgre.Db.User_LastRead( connectionString,  userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DateTime? Readtopic_lastread(int? mid, [NotNull] object userID, [NotNull] object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Readtopic_lastread(connectionString, userID, topicID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Readtopic_lastread(connectionString, userID, topicID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case "oracle":  return orPostgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case "db2":  return db2Postgre.Db.Readtopic_lastread(connectionString, userID, topicID);
                    // case "other":  return othPostgre.Db.Readtopic_lastread(connectionString, userID, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void ReadForum_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.ReadForum_AddOrUpdate(connectionString, userID, forumID);
                    break;
                    // case "oracle":   orPostgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID);break;
                    // case "db2":   db2Postgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
                    // case "other":   othPostgre.Db.ReadForum_AddOrUpdate(connectionString,userID, forumID); break;
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
                 case "System.Data.SqlClient": MsSql.Db.ReadForum_delete(connectionString, trackingID); break;
                 case "Npgsql": Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 case "MySql.Data.MySqlClient":  MySqlDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 case "FirebirdSql.Data.FirebirdClient":  FirebirdDb.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case "oracle":   orPostgre.Db.ReadForum_delete(connectionString, trackingID);break;
                 // case "db2":   db2Postgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 // case "other":   othPostgre.Db.ReadForum_delete(connectionString, trackingID); break;
                 default:
                     throw new ArgumentOutOfRangeException(dataEngine);
                     break;
             }
         } */

        public static DateTime? ReadForum_lastread(int? mid, [NotNull] object userID, [NotNull] object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.ReadForum_lastread(connectionString, userID, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.ReadForum_lastread(connectionString, userID, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.ReadForum_lastread(connectionString, userID, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.ReadForum_lastread(connectionString, userID, forumID);
                    // case "oracle":  return orPostgre.Db.ReadForum_lastread(connectionString,userID, forumID);
                    // case "db2":  return db2Postgre.Db.ReadForum_lastread(connectionString,userID, forumID);
                    // case "other":  return othPostgre.Db.ReadForum_lastread(connectionString,userID, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

        public static void nntpforum_delete(int? mid, object nntpForumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.nntpforum_delete(connectionString, nntpForumID);
                    break;
                    // case "oracle":   orPostgre.Db.nntpforum_delete(connectionString, nntpForumID);break;
                    // case "db2":   db2Postgre.Db.nntpforum_delete(connectionString, nntpForumID); break;
                    // case "other":   othPostgre.Db.nntpforum_delete(connectionString, nntpForumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable nntpforum_list(
            int? mid, object boardId, object minutes, object nntpForumID, object active)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case "oracle":  return orPostgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case "db2":  return db2Postgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active);
                    // case "other":  return othPostgre.Db.nntpforum_list(connectionString, boardId, minutes, nntpForumID, active); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.nntpforum_save(
                        connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);
                    break;
                    // case "oracle":   orPostgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate);break;
                    // case "db2":   db2Postgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                    // case "other":   othPostgre.Db.nntpforum_save(connectionString, nntpForumID, nntpServerID, groupName, forumID, active, cutoffdate); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void nntpforum_update(int? mid, object nntpForumID, object lastMessageNo, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);
                    break;
                    // case "oracle":   orPostgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId);break;
                    // case "db2":   db2Postgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                    // case "other":   othPostgre.Db.nntpforum_update(connectionString, nntpForumID, lastMessageNo, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedNntpForum> NntpForumList(
            int? mid, int boardId, int? minutes, int? nntpForumID, bool? active)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case "oracle":  return orPostgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case "db2":  return db2Postgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active);
                    // case "other":  return othPostgre.Db.NntpForumList(connectionString, boardId, minutes, nntpForumID, active); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void nntpserver_delete(int? mid, object nntpServerID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.nntpserver_delete(connectionString, nntpServerID);
                    break;
                    // case "oracle":   orPostgre.Db.nntpserver_delete(connectionString, nntpServerID);break;
                    // case "db2":   db2Postgre.Db.nntpserver_delete(connectionString, nntpServerID); break;
                    // case "other":   othPostgre.Db.nntpserver_delete(connectionString, nntpServerID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable nntpserver_list(int? mid, object boardId, object nntpServerID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.nntpserver_list(connectionString, boardId, nntpServerID);
                    // case "oracle":  return orPostgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                    // case "db2":  return db2Postgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID);
                    // case "other":  return othPostgre.Db.nntpserver_list(connectionString,  boardId, nntpServerID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.nntpserver_save(
                        connectionString, nntpServerID, boardId, name, address, port, userName, userPass);
                    break;
                    // case "oracle":   orPostgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass);break;
                    // case "db2":   db2Postgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                    // case "other":   othPostgre.Db.nntpserver_save(connectionString, nntpServerID, boardId, name, address, port, userName, userPass); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable nntptopic_list(int? mid, object thread)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.nntptopic_list(connectionString, thread);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.nntptopic_list(connectionString, thread);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.nntptopic_list(connectionString, thread);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.nntptopic_list(connectionString, thread);
                    // case "oracle":  return orPostgre.Db.nntptopic_list(connectionString, thread);
                    // case "db2":  return db2Postgre.Db.nntptopic_list(connectionString, thread);
                    // case "other":  return othPostgre.Db.nntptopic_list(connectionString, thread); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":   orPostgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId);break;
                    // case "db2":   db2Postgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                    // case "other":   othPostgre.Db.nntptopic_savemessage(connectionString, nntpForumID,topic,body,userId, userName, ip, posted, externalMessageId,referenceMessageId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataRow pageload(
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

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pageload(
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
                        donttrack);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pageload(
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
                        donttrack);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pageload(
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
                        donttrack);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pageload(
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
                        donttrack);
                    // case "oracle":  return orPostgre.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack);
                    // case "db2":  return db2Postgre.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack);
                    // case "other":  return othPostgre.Db.pageload(connectionString, sessionId, boardId, userKey, ip, location, forumPage, browser, platform,categoryId, forumId, topicId, messageId, isCrawler, isMobileDevice, donttrack); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void pmessage_archive(int? mid, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.pmessage_archive(connectionString, userPMessageID);
                    break;
                    // case "oracle":   orPostgre.Db.pmessage_archive(connectionString, userPMessageID);break;
                    // case "db2":   db2Postgre.Db.pmessage_archive(connectionString, userPMessageID); break;
                    // case "other":   othPostgre.Db.pmessage_archive(connectionString, userPMessageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        /// <summary>
        /// Deletes the private message from the database as per the given parameter.  If fromOutbox is true,
        /// the message is only deleted from the user's outbox.  Otherwise, it is completely delete from the database.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="userPMessageID"></param>
        public static void pmessage_delete(int? mid, object userPMessageID)
        {
            pmessage_delete(mid, userPMessageID, false);
        }

        public static void pmessage_delete(int? mid, object userPMessageID, bool fromOutbox)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);
                    break;
                    // case "oracle":   orPostgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox);break;
                    // case "db2":   db2Postgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                    // case "other":   othPostgre.Db.pmessage_delete(connectionString, userPMessageID, fromOutbox); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable pmessage_info(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pmessage_info(connectionString);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pmessage_info(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pmessage_info(connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pmessage_info(connectionString);
                    // case "oracle":  return orPostgre.Db.pmessage_info(connectionString);
                    // case "db2":  return db2Postgre.Db.pmessage_info(connectionString);
                    // case "other":  return othPostgre.Db.pmessage_info(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        /// <summary>
        /// Returns a list of private messages based on the arguments specified.
        /// If pMessageID != null, returns the PM of id pMessageId.
        /// If toUserID != null, returns the list of PMs sent to the user with the given ID.
        /// If fromUserID != null, returns the list of PMs sent by the user of the given ID.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="userPMessageID"> </param>
        /// <param name="toUserID"></param>
        /// <param name="fromUserID"></param>
        /// <param name="pMessageID">The id of the private message</param>
        /// <returns></returns>
        public static DataTable pmessage_list(int? mid, object userPMessageID)
        {
            return pmessage_list(mid, null, null, userPMessageID);
        }

        public static DataTable pmessage_list(int? mid, object toUserID, object fromUserID, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case "oracle":  return orPostgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case "db2":  return db2Postgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID);
                    // case "other":  return othPostgre.Db.pmessage_list(connectionString, toUserID, fromUserID, userPMessageID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void pmessage_markread(int? mid, object userPMessageID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.pmessage_markread(connectionString, userPMessageID);
                    break;
                    // case "oracle":   orPostgre.Db.pmessage_markread(connectionString, userPMessageID);break;
                    // case "db2":   db2Postgre.Db.pmessage_markread(connectionString, userPMessageID); break;
                    // case "other":   othPostgre.Db.pmessage_markread(connectionString, userPMessageID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void pmessage_prune(int? mid, object daysRead, object daysUnread)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.pmessage_prune(connectionString, daysRead, daysUnread);
                    break;
                    // case "oracle":   orPostgre.Db.pmessage_prune(connectionString, daysRead, daysUnread);break;
                    // case "db2":   db2Postgre.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                    // case "other":   othPostgre.Db.pmessage_prune(connectionString, daysRead, daysUnread); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void pmessage_save(
            int? mid, object fromUserID, object toUserID, object subject, object body, object Flags, object replyTo)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.pmessage_save(
                        connectionString, fromUserID, toUserID, subject, body, Flags, replyTo);
                    break;
                    // case "oracle":   orPostgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo);break;
                    // case "db2":   db2Postgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                    // case "other":   othPostgre.Db.pmessage_save(connectionString, fromUserID, toUserID, subject, body, Flags,replyTo); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void poll_remove(
            int? mid, object pollGroupID, object pollID, object boardId, bool removeCompletely, bool removeEverywhere)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.poll_remove(
                        connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);
                    break;
                    // case "oracle":   orPostgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere);break;
                    // case "db2":   db2Postgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                    // case "other":   othPostgre.Db.poll_remove(connectionString, pollGroupID, pollID, boardId, removeCompletely, removeEverywhere); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int? poll_save(int? mid, List<PollSaveList> pollList)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.poll_save(connectionString, pollList);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.poll_save(connectionString, pollList);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.poll_save(connectionString, pollList);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.poll_save(connectionString, pollList);
                    // case "oracle":  return orPostgre.Db.poll_save(connectionString, pollList);
                    // case "db2":  return db2Postgre.Db.poll_save(connectionString, pollList);
                    // case "other":  return othPostgre.Db.poll_save(connectionString, pollList); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable poll_stats(int? mid, int? pollId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.poll_stats(connectionString, pollId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.poll_stats(connectionString, pollId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.poll_stats(connectionString, pollId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.poll_stats(connectionString, pollId);
                    // case "oracle":  return orPostgre.Db.poll_stats(connectionString, pollId);
                    // case "db2":  return db2Postgre.Db.poll_stats(connectionString, pollId);
                    // case "other":  return othPostgre.Db.poll_stats(connectionString, pollId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static int pollgroup_attach(
            int? mid, int? pollGroupId, int? topicId, int? forumId, int? categoryId, int? boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pollgroup_attach(
                        connectionString, pollGroupId, topicId, forumId, categoryId, boardId);
                    // case "oracle":  return orPostgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                    // case "db2":  return db2Postgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId);
                    // case "other":  return othPostgre.Db._pollgroup_attach(connectionString, pollGroupId, topicId,  forumId,  categoryId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":   orPostgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere);break;
                    // case "db2":   db2Postgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                    // case "other":   othPostgre.Db.pollgroup_remove(connectionString, pollGroupID, topicId, forumId, categoryId, boardId, removeCompletely, removeEverywhere); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable pollgroup_stats(int? mid, int? pollGroupId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pollgroup_stats(connectionString, pollGroupId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pollgroup_stats(connectionString, pollGroupId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pollgroup_stats(connectionString, pollGroupId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case "oracle":  return orPostgre.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case "db2":  return db2Postgre.Db.pollgroup_stats(connectionString, pollGroupId);
                    // case "other":  return othPostgre.Db.pollgroup_stats(connectionString, pollGroupId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":   orPostgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime);break;
                    // case "db2":   db2Postgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                    // case "other":   othPostgre.Db.poll_update(connectionString, pollID, question, closes, isBounded, isClosedBounded, allowMultipleChoices, showVoters, allowSkipVote, questionPath, questionMime); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable pollgroup_votecheck(int? mid, object pollGroupId, object userId, object remoteIp)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case "oracle":  return orPostgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case "db2":  return db2Postgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp);
                    // case "other":  return othPostgre.Db.pollgroup_votecheck(connectionString, pollGroupId, userId, remoteIp); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static IEnumerable<TypedPollGroup> PollGroupList(int? mid, int userID, int? forumId, int boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PollGroupList(connectionString, userID, forumId, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case "oracle":  return orPostgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case "db2":  return db2Postgre.Db.PollGroupList(connectionString, userID, forumId, boardId);
                    // case "other":  return othPostgre.Db.PollGroupList(connectionString, userID, forumId, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable pollvote_check(int? mid, object pollid, object userid, object remoteip)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.pollvote_check(connectionString, pollid, userid, remoteip);
                    // case "oracle":  return orPostgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                    // case "db2":  return db2Postgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip);
                    // case "other":  return othPostgre.Db.pollvote_check(connectionString, pollid,  userid,  remoteip); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable post_alluser(
            int? mid, object boardid, object userid, object pageUserID, object topCount)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.post_alluser(connectionString, boardid, userid, pageUserID, topCount);
                    // case "oracle":  return orPostgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                    // case "db2":  return db2Postgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount);
                    // case "other":  return othPostgre.Db.post_alluser(connectionString, boardid,  userid,  pageUserID,  topCount); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                    // case "db2":  return db2Postgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition);
                    // case "other":  return othPostgre.Db.post_list(connectionString, topicId, currentUserID, authoruserId, updateViewCount, showDeleted, styledNicks, showReputation, sincePostedDate, toPostedDate, sinceEditedDate, toEditedDate, pageIndex, pageSize, sortPosted, sortEdited, sortPosition, showThanks, messagePosition); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable post_list_reverse10(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.post_list_reverse10(connectionString, topicID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.post_list_reverse10(connectionString, topicID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.post_list_reverse10(connectionString, topicID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.post_list_reverse10(connectionString, topicID);
                    // case "oracle":  return orPostgre.Db.post_list_reverse10(connectionString, topicID);
                    // case "db2":  return db2Postgre.Db.post_list_reverse10(connectionString, topicID);
                    // case "other":  return othPostgre.Db.post_list_reverse10(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void rank_delete(int? mid, object rankID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.rank_delete(connectionString, rankID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.rank_delete(connectionString, rankID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.rank_delete(connectionString, rankID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.rank_delete(connectionString, rankID);
                    break;
                    // case "oracle":   orPostgre.Db.rank_delete(connectionString, rankID);break;
                    // case "db2":   db2Postgre.Db.rank_delete(connectionString, rankID); break;
                    // case "other":   othPostgre.Db.rank_delete(connectionString, rankID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable rank_list(int? mid, object boardId, object rankID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.rank_list(connectionString, boardId, rankID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.rank_list(connectionString, boardId, rankID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.rank_list(connectionString, boardId, rankID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.rank_list(connectionString, boardId, rankID);
                    // case "oracle":  return orPostgre.Db.rank_list(connectionString, boardId, rankID);
                    // case "db2":  return db2Postgre.Db.rank_list(connectionString, boardId, rankID);
                    // case "other":  return othPostgre.Db.rank_list(connectionString, boardId, rankID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void rank_save(
            int? mid,
            object rankID,
            object boardId,
            object name,
            object isStart,
            object isLadder,
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
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
                case "Npgsql":
                    VZF.Data.Postgre.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
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
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
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
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.rank_save(
                        connectionString,
                        rankID,
                        boardId,
                        name,
                        isStart,
                        isLadder,
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
                    // case "oracle":   orPostgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages);break;
                    // case "db2":   db2Postgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                    // case "other":   othPostgre.Db.rank_save(connectionString, rankID, boardId, name,isStart,  isLadder,  minPosts,  rankImage, pmLimit, style,  sortOrder, description, usrSigChars, usrSigBBCodes, usrSigHTMLTags, usrAlbums, usrAlbumImages); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

        public static DataTable recent_users(int? mid, object boardID, int timeSinceLastLogin, object styledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.recent_users(connectionString, boardID, timeSinceLastLogin, styledNicks);
                    // case "oracle":  return orPostgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                    // case "db2":  return db2Postgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks);
                    // case "other":  return othPostgre.Db.recent_users(connectionString, boardID,  timeSinceLastLogin,  styledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        /// <summary>
        /// Retrieves all the entries in the board settings registry
        /// </summary>
        /// <param name="mid"> </param>
        /// <returns>DataTable filled will all registry entries</returns>
        public static DataTable registry_list(int? mid)
        {
            return registry_list(mid, null, null);
        }

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="name"> </param>
        /// <param name="Name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list(int? mid, object name)
        {
            return registry_list(mid, name, null);
        }

        public static DataTable registry_list(int? mid, object name, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.registry_list(connectionString, name, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.registry_list(connectionString, name, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.registry_list(connectionString, name, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.registry_list(connectionString, name, boardId);
                    // case "oracle":  return orPostgre.Db.registry_list(connectionString, name,  boardId);
                    // case "db2":  return db2Postgre.Db.registry_list(connectionString, name,  boardId);
                    // case "other":  return othPostgre.Db.registry_list(connectionString, name,  boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }

        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="name"> </param>
        /// <param name="value"> </param>
        /// <param name="Name">Unique name associated with this entry</param>
        /// <param name="Value">Value associated with this entry which can be null</param>
        public static void registry_save(int? mid, object name, object value)
        {

            registry_save(mid, name, value, DBNull.Value);

        }

        public static void registry_save(int? mid, object name, object value, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.registry_save(connectionString, name, value, boardId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.registry_save(connectionString, name, value, boardId);
                    break;
                    // case "oracle":   orPostgre.Db.registry_save(connectionString, name, value, boardId);break;
                    // case "db2":   db2Postgre.Db.registry_save(connectionString, name, value, boardId); break;
                    // case "other":   othPostgre.Db.registry_save(connectionString, name, value, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void replace_words_delete(int? mid, object id)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.replace_words_delete(connectionString, id);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.replace_words_delete(connectionString, id);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.replace_words_delete(connectionString, id);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.replace_words_delete(connectionString, id);
                    break;
                    // case "oracle":   orPostgre.Db.replace_words_delete(connectionString, id);break;
                    // case "db2":   db2Postgre.Db.replace_words_delete(connectionString, id); break;
                    // case "other":   othPostgre.Db.replace_words_delete(connectionString, id); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable replace_words_list(int? mid, object boardId, object id)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.replace_words_list(connectionString, boardId, id);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.replace_words_list(connectionString, boardId, id);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.replace_words_list(connectionString, boardId, id);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.replace_words_list(connectionString, boardId, id);
                    // case "oracle":  return orPostgre.Db.replace_words_list(connectionString, boardId, id);
                    // case "db2":  return db2Postgre.Db.replace_words_list(connectionString, boardId, id);
                    // case "other":  return othPostgre.Db.replace_words_list(connectionString, boardId, id); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void replace_words_save(int? mid, object boardId, object id, object badword, object goodword)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.replace_words_save(connectionString, boardId, id, badword, goodword);
                    break;
                    // case "oracle":   orPostgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword);break;
                    // case "db2":   db2Postgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                    // case "other":   othPostgre.Db.replace_words_save(connectionString, boardId,  id,  badword,  goodword); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.rss_topic_latest(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts);
                    // case "oracle":  return orPostgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                    // case "db2":  return db2Postgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts);
                    // case "other":  return othPostgre.Db.rss_topic_latest(connectionString, boardId,  numOfPostsToRetrieve,  pageUserId,  useStyledNicks, showNoCountPosts); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable rsstopic_list(int? mid, int forumID, int topicCount)
        {
            return rsstopic_list(mid, forumID, 0, topicCount);
        }

        public static DataTable rsstopic_list(int? mid, int forumID, int topicStart, int topicCount)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case "oracle":  return orPostgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case "db2":  return db2Postgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount);
                    // case "other":  return othPostgre.Db.rsstopic_list(connectionString, forumID, topicStart, topicCount); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.SetPropertyValues(
                        connectionString, boardId, appname, userId, userName, collection, dirtyOnly);
                    break;
                    // case "oracle":   orPostgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                    // case "db2":   db2Postgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                    // case "other":   othPostgre.Db.SetPropertyValues(connectionString, boardId,  appname,  userId,  collection, dirtyOnly); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool shoutbox_clearmessages(int? mid, int boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.shoutbox_clearmessages(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.shoutbox_clearmessages(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.shoutbox_clearmessages(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.shoutbox_clearmessages(connectionString, boardId);
                    // case "other":  return othPostgre.Db.shoutbox_clearmessages(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable shoutbox_getmessages(int? mid, int boardId, int numberOfMessages, object useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.shoutbox_getmessages(
                        connectionString, boardId, numberOfMessages, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                    // case "db2":  return db2Postgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks);
                    // case "other":  return othPostgre.Db.shoutbox_getmessages(connectionString, boardId,  numberOfMessages,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static bool shoutbox_savemessage(
            int? mid, int boardId, string message, string userName, int userID, object ip)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.shoutbox_savemessage(
                        connectionString, boardId, message, userName, userID, ip);
                    // case "oracle":  return orPostgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                    // case "db2":  return db2Postgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip);
                    // case "other":  return othPostgre.Db.shoutbox_savemessage(connectionString, boardId, message, userName, userID, ip); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void smiley_delete(int? mid, object smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.smiley_delete(connectionString, smileyID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.smiley_delete(connectionString, smileyID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.smiley_delete(connectionString, smileyID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.smiley_delete(connectionString, smileyID);
                    break;
                    // case "oracle":   orPostgre.Db.smiley_delete(connectionString, smileyID);break;
                    // case "db2":   db2Postgre.Db.smiley_delete(connectionString, smileyID); break;
                    // case "other":   othPostgre.Db.smiley_delete(connectionString, smileyID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable smiley_list(int? mid, object boardId, object smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.smiley_list(connectionString, boardId, smileyID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.smiley_list(connectionString, boardId, smileyID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.smiley_list(connectionString, boardId, smileyID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.smiley_list(connectionString, boardId, smileyID);
                    // case "oracle":  return orPostgre.Db.smiley_list(connectionString, boardId, smileyID);
                    // case "db2":  return db2Postgre.Db.smiley_list(connectionString, boardId, smileyID);
                    // case "other":  return othPostgre.Db.smiley_list(connectionString, boardId, smileyID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable smiley_listunique(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.smiley_listunique(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.smiley_listunique(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.smiley_listunique(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.smiley_listunique(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.smiley_listunique(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.smiley_listunique(connectionString, boardId);
                    // case "other":  return othPostgre.Db.smiley_listunique(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void smiley_resort(int? mid, object boardId, object smileyID, int move)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.smiley_resort(connectionString, boardId, smileyID, move);
                    break;
                    // case "oracle":   orPostgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move);break;
                    // case "db2":   db2Postgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                    // case "other":   othPostgre.Db.smiley_resort(connectionString, boardId,  smileyID,  move); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.smiley_save(
                        connectionString, smileyID, boardId, code, icon, emoticon, sortOrder, replace);
                    break;
                    // case "oracle":   orPostgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace);break;
                    // case "db2":   db2Postgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                    // case "other":   othPostgre.Db.smiley_save(connectionString, smileyID, boardId, code, icon, emoticon, sortOrder,replace); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedSmileyList> SmileyList(int? mid, int boardId, int? smileyID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.SmileyList(connectionString, boardId, smileyID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.SmileyList(connectionString, boardId, smileyID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.SmileyList(connectionString, boardId, smileyID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.SmileyList(connectionString, boardId, smileyID);
                    // case "oracle":  return orPostgre.Db.SmileyList(connectionString, boardId, smileyID);
                    // case "db2":  return db2Postgre.Db.SmileyList(connectionString, boardId, smileyID);
                    // case "other":  return othPostgre.Db.SmileyList(connectionString, boardId, smileyID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void system_deleteinstallobjects(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.system_deleteinstallobjects(connectionString);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.system_deleteinstallobjects(connectionString);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.system_deleteinstallobjects(connectionString);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.system_deleteinstallobjects(connectionString);
                    break;
                    // case "oracle":   orPostgre.Db.system_deleteinstallobjects(connectionString);break;
                    // case "db2":   db2Postgre.Db.system_deleteinstallobjects(connectionString); break;
                    // case "other":   othPostgre.Db.system_deleteinstallobjects(connectionString); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":   orPostgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix);break;
                    // case "db2":   db2Postgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                    // case "other":   othPostgre.Db.system_initialize(connectionString, forumName, timeZone,  culture,  languageFile,  forumEmail, smtpServer,  userName,  userEmail,  providerUserKey, rolePrefix); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void system_initialize_executescripts(
            int? mid, string script, string scriptFile, bool useTransactions)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.system_initialize_executescripts(
                        connectionString, script, scriptFile, useTransactions);
                    break;
                    // case "oracle":   orPostgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions);break;
                    // case "db2":   db2Postgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                    // case "other":   othPostgre.Db.system_initialize_executescripts(connectionString, script, scriptFile, useTransactions); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void system_initialize_fixaccess(int? mid, bool bGrant)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.system_initialize_fixaccess(connectionString, bGrant);
                    break;
                    // case "oracle":   orPostgre.Db.system_initialize_fixaccess(connectionString, bGrant);break;
                    // case "db2":   db2Postgre.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                    // case "other":   othPostgre.Db.system_initialize_fixaccess(connectionString, bGrant); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable system_list(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.system_list(connectionString);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.system_list(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.system_list(connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.system_list(connectionString);
                    // case "oracle":  return orPostgre.Db.system_list(connectionString);
                    // case "db2":  return db2Postgre.Db.system_list(connectionString);
                    // case "other":  return othPostgre.Db.system_list(connectionString); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void system_updateversion(int? mid, int version, string name)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.system_updateversion(connectionString, version, name);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.system_updateversion(connectionString, version, name);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.system_updateversion(connectionString, version, name);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.system_updateversion(connectionString, version, name);
                    break;
                    // case "oracle":   orPostgre.Db.system_updateversion(connectionString, version, name);break;
                    // case "db2":   db2Postgre.Db.system_updateversion(connectionString, version, name); break;
                    // case "other":   othPostgre.Db.system_updateversion(connectionString, version, name); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                    // case "db2":  return db2Postgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                    // case "other":  return othPostgre.Db.topic_active(connectionString,  boardId,  categoryId,  pageUserId, sinceDate,  toDate,  pageIndex,  pageSize,  useStyledNicks,  findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_announcements(
            int? mid, object boardId, object numOfPostsToRetrieve, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_announcements(
                        connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case "oracle":  return orPostgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case "db2":  return db2Postgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId);
                    // case "other":  return othPostgre.Db.topic_announcements(connectionString, boardId, numOfPostsToRetrieve, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "db2":  return db2Postgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "other":  return othPostgre.Db.topic_unanswered(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "db2":  return db2Postgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "other":  return othPostgre.Db.topic_unread(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "db2":  return db2Postgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                    // case "other":  return othPostgre.Db.Topics_ByUser(connectionString, boardId,categoryId, pageUserId, sinceDate,toDate, pageIndex,pageSize,useStyledNicks,findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void TopicStatus_Delete(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.TopicStatus_Delete(connectionString, topicStatusID);
                    break;
                    // case "oracle":   orPostgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                    // case "db2":   db2Postgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                    // case "other":   othPostgre.Db.TopicStatus_Delete(connectionString,  topicStatusID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable TopicStatus_Edit(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.TopicStatus_Edit(connectionString, topicStatusID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.TopicStatus_Edit(connectionString, topicStatusID);
                    // case "oracle":  return orPostgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                    // case "db2":  return db2Postgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                    // case "other":  return othPostgre.Db.TopicStatus_Edit(connectionString,  topicStatusID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable TopicStatus_List(int? mid, [NotNull] object topicStatusID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.TopicStatus_List(connectionString, topicStatusID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.TopicStatus_List(connectionString, topicStatusID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.TopicStatus_List(connectionString, topicStatusID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.TopicStatus_List(connectionString, topicStatusID);
                    // case "oracle":  return orPostgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                    // case "db2":  return db2Postgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                    // case "other":  return othPostgre.Db.TopicStatus_List(connectionString,  topicStatusID);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.TopicStatus_Save(
                        connectionString, topicStatusID, boardID, topicStatusName, defaultDescription);
                    break;
                    // case "oracle":   orPostgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                    // case "db2":   db2Postgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                    // case "other":   othPostgre.Db.TopicStatus_Save(connectionString, topicStatusID, boardID, topicStatusName, defaultDescription); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static long topic_create_by_message(int? mid, object messageID, object forumId, object newTopicSubj)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_create_by_message(
                        connectionString, messageID, forumId, newTopicSubj);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_create_by_message(
                        connectionString, messageID, forumId, newTopicSubj);
                    // case "oracle":  return orPostgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                    // case "db2":  return db2Postgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj);
                    // case "other":  return othPostgre.Db.topic_create_by_message(connectionString, messageID, forumId, newTopicSubj); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void topic_delete(int? mid, object topicID)
        {
            topic_delete(mid, topicID, false);
        }

        public static void topic_delete(int? mid, object topicID, object eraseTopic)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_delete(connectionString, topicID, eraseTopic);
                    break;
                    // case "oracle":   orPostgre.Db.topic_delete(connectionString, topicID, eraseTopic);break;
                    // case "db2":   db2Postgre.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                    // case "other":   othPostgre.Db.topic_delete(connectionString, topicID, eraseTopic); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void topic_favorite_add(int? mid, object userID, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_favorite_add(connectionString, userID, topicID);
                    break;
                    // case "oracle":   orPostgre.Db.topic_favorite_add(connectionString, userID, topicID);break;
                    // case "db2":   db2Postgre.Db.topic_favorite_add(connectionString, userID, topicID); break;
                    // case "other":   othPostgre.Db.topic_favorite_add(connectionString, userID, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                    // case "db2":  return db2Postgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                    // case "other":  return othPostgre.Db.topic_favorite_details(connectionString, boardId, categoryId, pageUserId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, findLastRead);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_favorite_list(int? mid, object userID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_favorite_list(connectionString, userID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_favorite_list(connectionString, userID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_favorite_list(connectionString, userID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_favorite_list(connectionString, userID);
                    // case "oracle":  return orPostgre.Db.topic_favorite_list(connectionString, userID);
                    // case "db2":  return db2Postgre.Db.topic_favorite_list(connectionString, userID);
                    // case "other":  return othPostgre.Db.topic_favorite_list(connectionString, userID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void topic_favorite_remove(int? mid, object userID, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_favorite_remove(connectionString, userID, topicID);
                    break;
                    // case "oracle":   orPostgre.Db.topic_favorite_remove(connectionString, userID, topicID);break;
                    // case "db2":   db2Postgre.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                    // case "other":   othPostgre.Db.topic_favorite_remove(connectionString, userID, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int topic_findduplicate(int? mid, object topicName)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_findduplicate(connectionString, topicName);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_findduplicate(connectionString, topicName);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_findduplicate(connectionString, topicName);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_findduplicate(connectionString, topicName);
                    // case "oracle":  return orPostgre.Db.topic_findduplicate(connectionString, topicName);
                    // case "db2":  return db2Postgre.Db.topic_findduplicate(connectionString, topicName);
                    // case "other":  return othPostgre.Db.topic_findduplicate(connectionString, topicName); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_findnext(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_findnext(connectionString, topicID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_findnext(connectionString, topicID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_findnext(connectionString, topicID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_findnext(connectionString, topicID);
                    // case "oracle":  return orPostgre.Db.topic_findnext(connectionString, topicID);
                    // case "db2":  return db2Postgre.Db.topic_findnext(connectionString, topicID);
                    // case "other":  return othPostgre.Db.topic_findnext(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_findprev(int? mid, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_findprev(connectionString, topicID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_findprev(connectionString, topicID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_findprev(connectionString, topicID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_findprev(connectionString, topicID);
                    // case "oracle":  return orPostgre.Db.topic_findprev(connectionString, topicID);
                    // case "db2":  return db2Postgre.Db.topic_findprev(connectionString, topicID);
                    // case "other":  return othPostgre.Db.topic_findprev(connectionString, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataRow topic_info(int? mid, object topicID, bool getTags)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_info(connectionString, topicID, getTags);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_info(connectionString, topicID, getTags);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_info(connectionString, topicID, getTags);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_info(connectionString, topicID, getTags);
                // case "oracle":  return orPostgre.Db.topic_info(connectionString, topicID, getTags);
                // case "db2":  return db2Postgre.Db.topic_info(connectionString, topicID, getTags);
                // case "other":  return othPostgre.Db.topic_info(connectionString, topicID, getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_latest(
                        connectionString,
                        boardID,
                        numOfPostsToRetrieve,
                        pageUserId,
                        useStyledNicks,
                        showNoCountPosts,
                        findLastRead);
                    // case "oracle":  return orPostgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                    // case "db2":  return db2Postgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead);
                    // case "other":  return othPostgre.Db.topic_latest(connectionString, boardID, numOfPostsToRetrieve, pageUserId, useStyledNicks, showNoCountPosts, findLastRead); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                // case "oracle":  return orPostgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                // case "db2":  return db2Postgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                // case "other":  return othPostgre.Db.topic_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                // case "oracle":  return orPostgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                // case "db2":  return db2Postgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags);
                // case "other":  return othPostgre.Db.announcements_list(connectionString, forumID, userId, sinceDate, toDate, pageIndex, pageSize,useStyledNicks, showMoved, findLastRead,getTags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void topic_lock(int? mid, object topicID, object locked)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_lock(connectionString, topicID, locked);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_lock(connectionString, topicID, locked);
                    break;
                    // case "oracle":   orPostgre.Db.topic_lock(connectionString, topicID, locked);break;
                    // case "db2":   db2Postgre.Db.topic_lock(connectionString, topicID, locked); break;
                    // case "other":   othPostgre.Db.topic_lock(connectionString, topicID, locked); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void topic_move(int? mid, object topicID, object forumID, object showMoved, object linkDays)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_move(connectionString, topicID, forumID, showMoved, linkDays);
                    break;
                    // case "oracle":   orPostgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays);break;
                    // case "db2":   db2Postgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                    // case "other":   othPostgre.Db.topic_move(connectionString, topicID,  forumID,  showMoved, linkDays); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_prune(connectionString, boardID, forumID, days, permDelete);
                    // case "oracle":  return orPostgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                    // case "db2":  return db2Postgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete);
                    // case "other":  return othPostgre.Db.topic_prune(connectionString, boardID,  forumID, days, permDelete); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags);
                    // case "db2":  return db2Postgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags);
                    // case "other":  return othPostgre.Db.topic_save(connectionString, forumID,  subject, status,styles, description,  message,  userId, priority,  userName,  ip,  posted,  blogPostID,  flags,ref messageID, tags); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_simplelist(connectionString, StartID, Limit);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_simplelist(connectionString, StartID, Limit);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_simplelist(connectionString, StartID, Limit);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case "oracle":  return orPostgre.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case "db2":  return db2Postgre.Db.topic_simplelist(connectionString, StartID, Limit);
                    // case "other":  return othPostgre.Db.topic_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_tags(int? mid, int boardId, int pageUserId, int topicId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_tags(connectionString, boardId, pageUserId, topicId);
                    // case "oracle":  return orPostgre.Db.topic_tags(connectionString,  boardId, pageUserId, topicId);
                    // case "db2":  return db2Postgre.Db.topic_tags(connectionString,  boardId, pageUserId, topicId);
                    // case "other":  return othPostgre.Db.topic_tags(connectionString,  boardId, pageUserId, topicId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable topic_bytags(
            int? mid, int boardId, int forumId, object pageUserId, string tags, object date, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.topic_bytags(
                        connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                // case "oracle":  return orPostgre.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                // case "db2":  return db2Postgre.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                // case "other":  return othPostgre.Db.topic_bytags(connectionString, boardId, forumId, pageUserId, tags, date, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void topic_updatetopic(int? mid, int topicId, string topic)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.topic_updatetopic(connectionString, topicId, topic);
                    break;
                    // case "oracle":   orPostgre.Db.topic_updatetopic(connectionString, topicId, topic);break;
                    // case "db2":   db2Postgre.Db.topic_updatetopic(connectionString, topicId, topic); break;
                    // case "other":   othPostgre.Db.topic_updatetopic(connectionString, topicId, topic); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int TopicFavoriteCount(int? mid, int topicId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.TopicFavoriteCount(connectionString, topicId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.TopicFavoriteCount(connectionString, topicId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.TopicFavoriteCount(connectionString, topicId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.TopicFavoriteCount(connectionString, topicId);
                    // case "oracle":  return orPostgre.Db.TopicFavoriteCount(connectionString, topicId);
                    // case "db2":  return db2Postgre.Db.TopicFavoriteCount(connectionString, topicId);
                    // case "other":  return othPostgre.Db.TopicFavoriteCount(connectionString, topicId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void unencode_all_topics_subjects(int? mid, Func<string, string> decodeTopicFunc)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);
                    break;
                    // case "oracle":   orPostgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc);break;
                    // case "db2":   db2Postgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                    // case "other":   othPostgre.Db.unencode_all_topics_subjects(connectionString, decodeTopicFunc); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_accessmasks(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_accessmasks(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_accessmasks(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_accessmasks(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_accessmasks(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.user_accessmasks(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.user_accessmasks(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.user_accessmasks(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_accessmasksbyforum(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.user_accessmasksbyforum(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.user_accessmasksbyforum(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_accessmasksbygroup(int? mid, object boardId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case "oracle":  return orPostgre.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case "db2":  return db2Postgre.Db.user_accessmasksbygroup(connectionString, boardId, userId);
                    // case "other":  return othPostgre.Db.user_accessmasksbygroup(connectionString, boardId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_activity_rank(int? mid, object boardId, object startDate, object displayNumber)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_activity_rank(connectionString, boardId, startDate, displayNumber);
                    // case "oracle":  return orPostgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                    // case "db2":  return db2Postgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber);
                    // case "other":  return othPostgre.Db.user_activity_rank(connectionString, boardId,  startDate, displayNumber); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_addignoreduser(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_addignoreduser(connectionString, userId, ignoredUserId);
                    break;
                    // case "oracle":   orPostgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId);break;
                    // case "db2":   db2Postgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                    // case "other":   othPostgre.Db.user_addignoreduser(connectionString, userId, ignoredUserId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_addpoints(int? mid, object userId, object forumUserId, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_addpoints(connectionString, userId, forumUserId, points);
                    break;
                    // case "oracle":   orPostgre.Db.user_addpoints(connectionString, userId, forumUserId, points);break;
                    // case "db2":   db2Postgre.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                    // case "other":   othPostgre.Db.user_addpoints(connectionString, userId, forumUserId, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_adminsave(
                        connectionString, boardId, userId, name, displayName, email, flags, rankID);
                    break;
                    // case "oracle":   orPostgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID);break;
                    // case "db2":   db2Postgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                    // case "other":   othPostgre.Db.user_adminsave(connectionString, boardId,  userId,  name,  displayName,  email,  flags,  rankID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_approve(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_approve(connectionString, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_approve(connectionString, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_approve(connectionString, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_approve(connectionString, userId);
                    break;
                    // case "oracle":   orPostgre.Db.user_approve(connectionString, userId);break;
                    // case "db2":   db2Postgre.Db.user_approve(connectionString, userId); break;
                    // case "other":   othPostgre.Db.user_approve(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_approveall(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_approveall(connectionString, boardId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_approveall(connectionString, boardId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_approveall(connectionString, boardId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_approveall(connectionString, boardId);
                    break;
                    // case "oracle":   orPostgre.Db.user_approveall(connectionString, boardId);break;
                    // case "db2":   db2Postgre.Db.user_approveall(connectionString, boardId); break;
                    // case "other":   othPostgre.Db.user_approveall(connectionString, boardId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_aspnet(
                        connectionString, boardId, userName, displayName, email, providerUserKey, isApproved);
                    // case "oracle":  return orPostgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                    // case "db2":  return db2Postgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved);
                    // case "other":  return othPostgre.Db.user_aspnet(connectionString, boardId,  userName,  displayName,  email,  providerUserKey, isApproved); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_avatarimage(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_avatarimage(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_avatarimage(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_avatarimage(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_avatarimage(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_avatarimage(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_avatarimage(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_avatarimage(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static bool user_changepassword(int? mid, object userId, object oldPassword, object newPassword)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_changepassword(connectionString, userId, oldPassword, newPassword);
                    // case "oracle":  return orPostgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                    // case "db2":  return db2Postgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword);
                    // case "other":  return othPostgre.Db.user_changepassword(connectionString, userId,  oldPassword, newPassword); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_delete(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_delete(connectionString, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_delete(connectionString, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_delete(connectionString, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_delete(connectionString, userId);
                    break;
                    // case "oracle":   orPostgre.Db.user_delete(connectionString, userId);break;
                    // case "db2":   db2Postgre.Db.user_delete(connectionString, userId); break;
                    // case "other":   othPostgre.Db.user_delete(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_deleteavatar(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_deleteavatar(connectionString, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_deleteavatar(connectionString, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_deleteavatar(connectionString, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_deleteavatar(connectionString, userId);
                    break;
                    // case "oracle":   orPostgre.Db.user_deleteavatar(connectionString, userId);break;
                    // case "db2":   db2Postgre.Db.user_deleteavatar(connectionString, userId); break;
                    // case "other":   othPostgre.Db.user_deleteavatar(connectionString, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_deleteold(int? mid, object boardId, object days)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_deleteold(connectionString, boardId, days);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_deleteold(connectionString, boardId, days);
                    break;
                    // case "oracle":   orPostgre.Db.user_deleteold(connectionString, boardId, days);break;
                    // case "db2":   db2Postgre.Db.user_deleteold(connectionString, boardId, days); break;
                    // case "other":   othPostgre.Db.user_deleteold(connectionString, boardId, days); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_emails(int? mid, object boardId, object groupID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_emails(connectionString, boardId, groupID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_emails(connectionString, boardId, groupID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_emails(connectionString, boardId, groupID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_emails(connectionString, boardId, groupID);
                    // case "oracle":  return orPostgre.Db.user_emails(connectionString, boardId, groupID);
                    // case "db2":  return db2Postgre.Db.user_emails(connectionString, boardId, groupID);
                    // case "other":  return othPostgre.Db.user_emails(connectionString, boardId, groupID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static int user_get(int? mid, int boardId, object providerUserKey)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_get(connectionString, boardId, providerUserKey);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_get(connectionString, boardId, providerUserKey);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_get(connectionString, boardId, providerUserKey);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_get(connectionString, boardId, providerUserKey);
                    // case "oracle":  return orPostgre.Db.user_get(connectionString, boardId, providerUserKey);
                    // case "db2":  return db2Postgre.Db.user_get(connectionString, boardId, providerUserKey);
                    // case "other":  return othPostgre.Db.user_get(connectionString, boardId, providerUserKey); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_getalbumsdata(int? mid, object userID, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getalbumsdata(connectionString, userID, boardID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getalbumsdata(connectionString, userID, boardID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case "oracle":  return orPostgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case "db2":  return db2Postgre.Db.user_getalbumsdata(connectionString, userID, boardID);
                    // case "other":  return othPostgre.Db.user_getalbumsdata(connectionString, userID, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static int user_getpoints(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getpoints(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getpoints(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getpoints(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getpoints(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_getpoints(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_getpoints(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_getpoints(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }

        }

        public static string user_getsignature(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getsignature(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getsignature(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getsignature(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getsignature(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_getsignature(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_user_getsignature(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_user_getsignature(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_getsignaturedata(int? mid, object userID, object boardID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getsignaturedata(connectionString, userID, boardID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getsignaturedata(connectionString, userID, boardID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case "oracle":  return orPostgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case "db2":  return db2Postgre.Db.user_getsignaturedata(connectionString, userID, boardID);
                    // case "other":  return othPostgre.Db.user_getsignaturedata(connectionString, userID, boardID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }

        }

        public static int user_getthanks_from(int? mid, object userID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getthanks_from(connectionString, userID, pageUserId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case "oracle":  return orPostgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case "db2":  return db2Postgre.Db.user_getthanks_from(connectionString, userID, pageUserId);
                    // case "other":  return othPostgre.Db.user_getthanks_from(connectionString, userID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int[] user_getthanks_to(int? mid, object userID, object pageUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_getthanks_to(connectionString, userID, pageUserId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case "oracle":  return orPostgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case "db2":  return db2Postgre.Db.user_getthanks_to(connectionString, userID, pageUserId);
                    // case "other":  return othPostgre.Db.user_getthanks_to(connectionString, userID, pageUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int? user_guest(int? mid, object boardId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_guest(connectionString, boardId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_guest(connectionString, boardId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_guest(connectionString, boardId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_guest(connectionString, boardId);
                    // case "oracle":  return orPostgre.Db.user_guest(connectionString, boardId);
                    // case "db2":  return db2Postgre.Db.user_guest(connectionString, boardId);
                    // case "other":  return othPostgre.Db.user_guest(connectionString, boardId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_ignoredlist(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_ignoredlist(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_ignoredlist(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_ignoredlist(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_ignoredlist(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_ignoredlist(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_ignoredlist(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_ignoredlist(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static bool user_isuserignored(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case "oracle":  return orPostgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case "db2":  return db2Postgre.Db.user_isuserignored(connectionString, userId, ignoredUserId);
                    // case "other":  return othPostgre.Db.user_isuserignored(connectionString, userId, ignoredUserId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_lazydata(
                        connectionString,
                        userID,
                        boardID,
                        showPendingMails,
                        showPendingBuddies,
                        showUnreadPMs,
                        showUserAlbums,
                        styledNicks);
                    // case "oracle":  return orPostgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                    // case "db2":  return db2Postgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks);
                    // case "other":  return othPostgre.Db.user_lazydata(connectionString, userID, boardID, showPendingMails, showPendingBuddies, showUnreadPMs,  showUserAlbums,  styledNicks); 
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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_list(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                    // case "db2":  return db2Postgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks);
                    // case "other":  return othPostgre.Db.user_list(connectionString,  boardId,  userId,  approved,  groupID,  rankID,  useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_pagedlist(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_pagedlist(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_pagedlist(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_pagedlist(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                // case "oracle":  return orPostgre.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                // case "db2":  return db2Postgre.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize);
                // case "other":  return othPostgre.Db.user_pagedlist(connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks, pageIndex, pageSize); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_listmedals(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_listmedals(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_listmedals(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_listmedals(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_listmedals(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_listmedals(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_listmedals(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_listmedals(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                    // case "db2":  return db2Postgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare);
                    // case "other":  return othPostgre.Db.user_listmembers(connectionString, boardId, userId, approved, groupId, rankId, useStyledNicks, lastUserId, literals, exclude, beginsWith, pageIndex, pageSize, sortName, sortRank, sortJoined, sortPosts, sortLastVisit, numPosts, numPostCompare); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_medal_delete(int? mid, object userId, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_medal_delete(connectionString, userId, medalID);
                    break;
                    // case "oracle":   orPostgre.Db.user_medal_delete(connectionString, userId, medalID);break;
                    // case "db2":   db2Postgre.Db.user_medal_delete(connectionString, userId, medalID); break;
                    // case "other":   othPostgre.Db.user_medal_delete(connectionString, userId, medalID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_medal_list(int? mid, object userId, object medalID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_medal_list(connectionString, userId, medalID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_medal_list(connectionString, userId, medalID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_medal_list(connectionString, userId, medalID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_medal_list(connectionString, userId, medalID);
                    // case "oracle":  return orPostgre.Db.user_medal_list(connectionString, userId, medalID);
                    // case "db2":  return db2Postgre.Db.user_medal_list(connectionString, userId, medalID);
                    // case "other":  return othPostgre.Db.user_medal_list(connectionString, userId, medalID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_medal_save(
            int? mid,
            object userId,
            object medalID,
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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_medal_save(
                        connectionString, userId, medalID, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_medal_save(
                        connectionString, userId, medalID, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_medal_save(
                        connectionString, userId, medalID, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_medal_save(
                        connectionString, userId, medalID, message, hide, onlyRibbon, sortOrder, dateAwarded);
                    break;
                    // case "oracle":   orPostgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded);break;
                    // case "db2":   db2Postgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                    // case "other":   othPostgre.Db.user_medal_save(connectionString, userId, medalID, message,hide,  onlyRibbon, sortOrder, dateAwarded); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_migrate(int? mid, object userId, object providerUserKey, object updateProvider)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);
                    break;
                    // case "oracle":   orPostgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider);break;
                    // case "db2":   db2Postgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                    // case "other":   othPostgre.Db.user_migrate(connectionString, userId, providerUserKey, updateProvider); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static int user_nntp(int? mid, object boardId, object userName, object email, int? timeZone)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_nntp(connectionString, boardId, userName, email, timeZone);
                    // case "oracle":  return orPostgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                    // case "db2":  return db2Postgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone);
                    // case "other":  return othPostgre.Db.user_nntp(connectionString, boardId, userName,  email,timeZone); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_pmcount(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_pmcount(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_pmcount(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_pmcount(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_pmcount(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_pmcount(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.user_pmcount(connectionString, userId);
                    // case "other":  return othPostgre.Db.user_pmcount(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static object user_recoverpassword(int? mid, object boardId, object userName, object email)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_recoverpassword(connectionString, boardId, userName, email);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case "oracle":  return orPostgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case "db2":  return db2Postgre.Db.user_recoverpassword(connectionString, boardId, userName, email);
                    // case "other":  return othPostgre.Db.user_recoverpassword(connectionString, boardId, userName, email); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
                    return true;
                case "FirebirdSql.Data.FirebirdClient":
                    return true;
                    // case "oracle":  return orPostgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                    // case "db2":  return db2Postgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved);
                    // case "other":  return othPostgre.Db.user_register(connectionString, boardId,  userName,  password,  hash,  email,  location, homePage,  timeZone,  approved); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_removeignoreduser(int? mid, object userId, object ignoredUserId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);
                    break;
                    // case "oracle":   orPostgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId);break;
                    // case "db2":   db2Postgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                    // case "other":   othPostgre.Db.user_removeignoreduser(connectionString, userId, ignoredUserId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_removepoints(int? mid, object userId, [CanBeNull] object fromUserID, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_removepoints(connectionString, userId, fromUserID, points);
                    break;
                    // case "oracle":   orPostgre.Db.user_removepoints(connectionString, userId, fromUserID, points);break;
                    // case "db2":   db2Postgre.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                    // case "other":   othPostgre.Db.user_removepoints(connectionString, userId, fromUserID, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool user_RepliedTopic(int? mid, [NotNull] object messageId, [NotNull] object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_RepliedTopic(connectionString, messageId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_RepliedTopic(connectionString, messageId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case "oracle":  return orPostgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case "db2":  return db2Postgre.Db.user_RepliedTopic(connectionString, messageId, userId);
                    // case "other":  return othPostgre.Db.user_RepliedTopic(connectionString, messageId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
            object notificationType)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
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
                        notificationType);
                    break;
                case "Npgsql":
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
                        notificationType);
                    break;
                case "MySql.Data.MySqlClient":
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
                        notificationType);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
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
                        notificationType);
                    break;
                    // case "oracle":   orPostgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType);break;
                    // case "db2":   db2Postgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                    // case "other":   othPostgre.Db.user_save(connectionString, userId, boardId, userName, displayName, email, timeZone, languageFile, culture, themeFile, useSingleSignOn, textEditor, overrideDefaultThemes, approved, pmNotification, autoWatchTopics, dSTUser, isHidden, notificationType); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_saveavatar(
            int? mid, object userId, object avatar, Stream stream, object avatarImageType)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);
                    break;
                    // case "oracle":   orPostgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType);break;
                    // case "db2":   db2Postgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                    // case "other":   othPostgre.Db.user_saveavatar(connectionString, userId, avatar, stream, avatarImageType); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

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
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_savenotification(
                        connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);
                    break;
                    // case "oracle":   orPostgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest);break;
                    // case "db2":   db2Postgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                    // case "other":   othPostgre.Db.user_savenotification(connectionString, userId, pmNotification, autoWatchTopics, notificationType, dailyDigest); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_savepassword(int? mid, object userId, object password)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_savepassword(connectionString, userId, password);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_savepassword(connectionString, userId, password);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_savepassword(connectionString, userId, password);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_savepassword(connectionString, userId, password);
                    break;
                    // case "oracle":   orPostgre.Db.user_savepassword(connectionString, userId, password);break;
                    // case "db2":   db2Postgre.Db.user_savepassword(connectionString, userId, password); break;
                    // case "other":   othPostgre.Db.user_savepassword(connectionString, userId, password); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_savesignature(int? mid, object userId, object signature)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_savesignature(connectionString, userId, signature);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_savesignature(connectionString, userId, signature);
                    break;
                    // case "oracle":   orPostgre.Db.user_savesignature(connectionString, userId, signature);break;
                    // case "db2":   db2Postgre.Db.user_savesignature(connectionString, userId, signature); break;
                    // case "other":   othPostgre.Db.user_savesignature(connectionString, userId, signature); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_setinfo(int? mid, int boardId, MembershipUser user)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_setinfo(connectionString, boardId, user);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_setinfo(connectionString, boardId, user);
                    break;
                case "MySql.Data.MySqlClient":
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_setinfo(connectionString, boardId, user);
                    break;
                    // case "oracle":   orPostgre.Db.user_setinfo(connectionString, boardId, user);break;
                    // case "db2":   db2Postgre.Db.user_setinfo(connectionString, boardId, user); break;
                    // case "other":   othPostgre.Db.user_setinfo(connectionString, boardId, user); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_setnotdirty(int? mid, int boardId, int userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_setnotdirty(connectionString, boardId, userId);
                    break;
                    // case "oracle":   orPostgre.Db.user_setnotdirty(connectionString, boardId, userId);break;
                    // case "db2":   db2Postgre.Db.user_setnotdirty(connectionString, boardId, userId); break;
                    // case "other":   othPostgre.Db.user_setnotdirty(connectionString, boardId, userId); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_setpoints(int? mid, object userId, object points)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_setpoints(connectionString, userId, points);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_setpoints(connectionString, userId, points);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_setpoints(connectionString, userId, points);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_setpoints(connectionString, userId, points);
                    break;
                    // case "oracle":   orPostgre.Db.user_setpoints(connectionString, userId, points);break;
                    // case "db2":   db2Postgre.Db.user_setpoints(connectionString, userId, points); break;
                    // case "other":   othPostgre.Db.user_setpoints(connectionString, userId, points); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_setrole(int? mid, int boardId, object providerUserKey, object role)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_setrole(connectionString, boardId, providerUserKey, role);
                    break;
                    // case "oracle":   orPostgre.Db.user_setrole(connectionString, boardId, providerUserKey, role);break;
                    // case "db2":   db2Postgre.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                    // case "other":   othPostgre.Db.user_setrole(connectionString, boardId, providerUserKey, role); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_simplelist(int? mid, int StartID, int Limit)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_simplelist(connectionString, StartID, Limit);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_simplelist(connectionString, StartID, Limit);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_simplelist(connectionString, StartID, Limit);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_simplelist(connectionString, StartID, Limit);
                    // case "oracle":  return orPostgre.Db.user_simplelist(connectionString, StartID, Limit);
                    // case "db2":  return db2Postgre.Db.user_simplelist(connectionString, StartID, Limit);
                    // case "other":  return othPostgre.Db.user_simplelist(connectionString, StartID, Limit); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void user_suspend(int? mid, object userId, object suspend)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_suspend(connectionString, userId, suspend);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_suspend(connectionString, userId, suspend);
                    break;
                    // case "oracle":   orPostgre.Db.user_suspend(connectionString, userId, suspend);break;
                    // case "db2":   db2Postgre.Db.user_suspend(connectionString, userId, suspend); break;
                    // case "other":   othPostgre.Db.user_suspend(connectionString, userId, suspend); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static void user_update_single_sign_on_status(
            int? mid, [NotNull] object userID, [NotNull] object isFacebookUser, [NotNull] object isTwitterUser)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.user_update_single_sign_on_status(
                        connectionString, userID, isFacebookUser, isTwitterUser);
                    break;
                    // case "oracle":   orPostgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                    // case "db2":   db2Postgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser); break;
                    // case "other":   othPostgre.Db.user_update_single_sign_on_status(connectionString, userID, isFacebookUser, isTwitterUser);break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool user_ThankedMessage(int? mid, object messageId, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_ThankedMessage(connectionString, messageId, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_ThankedMessage(connectionString, messageId, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case "oracle":  return orPostgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case "db2":  return db2Postgre.Db.user_ThankedMessage(connectionString, messageId, userId);
                    // case "other":  return othPostgre.Db.user_ThankedMessage(connectionString, messageId, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static int user_ThankFromCount(int? mid, [NotNull] object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_ThankFromCount(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_ThankFromCount(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_ThankFromCount(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_ThankFromCount(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.user_ThankFromCount(connectionString,  userId);
                    // case "db2":  return db2Postgre.Db.user_ThankFromCount(connectionString,  userId);
                    // case "other":  return othPostgre.Db.user_ThankFromCount(connectionString,  userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable user_viewthanksfrom(
            int? mid, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_viewthanksfrom(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "oracle":  return orPostgre.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "db2":  return db2Postgre.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "other":  return othPostgre.Db.user_viewthanksfrom(connectionString, UserID, pageUserId, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static DataTable user_viewthanksto(
            int? mid, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.user_viewthanksto(
                        connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "oracle":  return orPostgre.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "db2":  return db2Postgre.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                    // case "other":  return othPostgre.Db.user_viewthanksto(connectionString, UserID, pageUserId, pageIndex, pageSize);
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

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
                case "System.Data.SqlClient":
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
                case "Npgsql":
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
                case "MySql.Data.MySqlClient":
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
                case "FirebirdSql.Data.FirebirdClient":
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
                    // case "oracle":  return orPostgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                    // case "db2":  return db2Postgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u));
                    // case "other":  return othPostgre.Db.UserFind(connectionString, boardId,  filter,  userName,  email, displayName,notificationType,dailyDigest).AsEnumerable().Select(u => new TypedUserFind(u); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void userforum_delete(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.userforum_delete(connectionString, userId, forumID);
                    break;
                    // case "oracle":   orPostgre.Db.userforum_delete(connectionString, userId, forumID);break;
                    // case "db2":   db2Postgre.Db.userforum_delete(connectionString, userId, forumID); break;
                    // case "other":   othPostgre.Db.userforum_delete(connectionString, userId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable userforum_list(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.userforum_list(connectionString, userId, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.userforum_list(connectionString, userId, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.userforum_list(connectionString, userId, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.userforum_list(connectionString, userId, forumID);
                    // case "oracle":  return orPostgre.Db.userforum_list(connectionString, userId, forumID);
                    // case "db2":  return db2Postgre.Db.userforum_list(connectionString, userId, forumID);
                    // case "other":  return othPostgre.Db.userforum_list(connectionString, userId, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void userforum_save(int? mid, object userId, object forumID, object accessMaskID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.userforum_save(connectionString, userId, forumID, accessMaskID);
                    break;
                    // case "oracle":   orPostgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID);break;
                    // case "db2":   db2Postgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                    // case "other":   othPostgre.Db.userforum_save(connectionString, userId, forumID, accessMaskID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

        public static DataTable usergroup_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.usergroup_list(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.usergroup_list(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.usergroup_list(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.usergroup_list(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.usergroup_list(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.usergroup_list(connectionString, userId);
                    // case "other":  return othPostgre.Db.usergroup_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void usergroup_save(int? mid, object userId, object groupID, object member)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.usergroup_save(connectionString, userId, groupID, member);
                    break;
                    // case "oracle":   orPostgre.Db.usergroup_save(connectionString, userId,  groupID, member);break;
                    // case "db2":   db2Postgre.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                    // case "other":   othPostgre.Db.usergroup_save(connectionString, userId,  groupID, member); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static IEnumerable<TypedUserList> UserList(
            int? mid, int boardId, int? userId, bool? approved, int? groupID, int? rankID, bool? useStyledNicks)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.UserList(
                        connectionString, boardId, userId, approved, groupID, rankID, useStyledNicks);
                    // case "oracle":  return orPostgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                    // case "db2":  return db2Postgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks);
                    // case "other":  return othPostgre.Db.UserList(connectionString, boardId,  userId,  approved,  groupID,  rankID,useStyledNicks); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void watchforum_add(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.watchforum_add(connectionString, userId, forumID);
                    break;
                    // case "oracle":   orPostgre.Db.watchforum_add(connectionString, userId, forumID);break;
                    // case "db2":   db2Postgre.Db.watchforum_add(connectionString, userId, forumID); break;
                    // case "other":   othPostgre.Db.watchforum_add(connectionString, userId, forumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable watchforum_check(int? mid, object userId, object forumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.watchforum_check(connectionString, userId, forumID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.watchforum_check(connectionString, userId, forumID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.watchforum_check(connectionString, userId, forumID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.watchforum_check(connectionString, userId, forumID);
                    // case "oracle":  return orPostgre.Db.watchforum_check(connectionString, userId, forumID);
                    // case "db2":  return db2Postgre.Db.watchforum_check(connectionString, userId, forumID);
                    // case "other":  return othPostgre.Db.watchforum_check(connectionString, userId, forumID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void watchforum_delete(int? mid, object watchForumID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.watchforum_delete(connectionString, watchForumID);
                    break;
                    // case "oracle":   orPostgre.Db.watchforum_delete(connectionString, watchForumID);break;
                    // case "db2":   db2Postgre.Db.watchforum_delete(connectionString, watchForumID); break;
                    // case "other":   othPostgre.Db.watchforum_delete(connectionString, watchForumID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable watchforum_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.watchforum_list(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.watchforum_list(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.watchforum_list(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.watchforum_list(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.watchforum_list(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.watchforum_list(connectionString, userId);
                    // case "other":  return othPostgre.Db.watchforum_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void watchtopic_add(int? mid, object userId, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.watchtopic_add(connectionString, userId, topicID);
                    break;
                    // case "oracle":   orPostgre.Db.watchtopic_add(connectionString, userId, topicID);break;
                    // case "db2":   db2Postgre.Db.watchtopic_add(connectionString, userId, topicID); break;
                    // case "other":   othPostgre.Db.watchtopic_add(connectionString, userId, topicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable watchtopic_check(int? mid, object userId, object topicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.watchtopic_check(connectionString, userId, topicID);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.watchtopic_check(connectionString, userId, topicID);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.watchtopic_check(connectionString, userId, topicID);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.watchtopic_check(connectionString, userId, topicID);
                    // case "oracle":  return orPostgre.Db.watchtopic_check(connectionString, userId, topicID);
                    // case "db2":  return db2Postgre.Db.watchtopic_check(connectionString, userId, topicID);
                    // case "other":  return othPostgre.Db.watchtopic_check(connectionString, userId, topicID); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }

        public static void watchtopic_delete(int? mid, object watchTopicID)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    VZF.Data.MsSql.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case "Npgsql":
                    VZF.Data.Postgre.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case "MySql.Data.MySqlClient":
                    VZF.Data.Mysql.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    VZF.Data.Firebird.Db.watchtopic_delete(connectionString, watchTopicID);
                    break;
                    // case "oracle":   orPostgre.Db.watchtopic_delete(connectionString, watchTopicID);break;
                    // case "db2":   db2Postgre.Db.watchtopic_delete(connectionString, watchTopicID); break;
                    // case "other":   othPostgre.Db.watchtopic_delete(connectionString, watchTopicID); break;
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static DataTable watchtopic_list(int? mid, object userId)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.watchtopic_list(connectionString, userId);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.watchtopic_list(connectionString, userId);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.watchtopic_list(connectionString, userId);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.watchtopic_list(connectionString, userId);
                    // case "oracle":  return orPostgre.Db.watchtopic_list(connectionString, userId);
                    // case "db2":  return db2Postgre.Db.watchtopic_list(connectionString, userId);
                    // case "other":  return othPostgre.Db.watchtopic_list(connectionString, userId); 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }

        }


        // Properties

        public static int GetDBSize(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.GetDBSize(connectionString);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.GetDBSize(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.GetDBSize(connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.GetDBSize(connectionString);
                    // case "oracle":  return orPostgre.Db.GetDBSize();
                    // case "db2":  return db2Postgre.Db.GetDBSize();
                    // case "other":  return othPostgre.Db.GetDBSize(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
        }

        public static bool GetIsForumInstalled(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.GetIsForumInstalled(connectionString);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.GetIsForumInstalled(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.GetIsForumInstalled(connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.GetIsForumInstalled(connectionString);
                    // case "oracle":  return orPostgre.Db.GetIsForumInstalled();
                    // case "db2":  return db2Postgre.Db.GetIsForumInstalled();
                    // case "other":  return othPostgre.Db.GetIsForumInstalled(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
        }


        public static int GetDBVersion(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.GetDBVersion(connectionString);
                case "Npgsql":
                    return VZF.Data.Postgre.Db.GetDBVersion(connectionString);
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.GetDBVersion(connectionString);
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.GetDBVersion(connectionString);
                    // case "oracle":  return orPostgre.Db.GetDBVersion();
                    // case "db2":  return db2Postgre.Db.GetDBVersion();
                    // case "other":  return othPostgre.Db.GetDBVersion(); 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
        }

        public static bool GetFullTextSupported(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.FullTextSupported;
                    ;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.FullTextSupported;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.FullTextSupported;
                    ;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.FullTextSupported;
                    ;
                    // case "oracle":  return orPostgre.Db.fullTextSupported;;
                    // case "db2":  return db2Postgre.Db.fullTextSupported;;
                    // case "other":  return othPostgre.Db.fullTextSupported;; 
                default:
                    throw new ApplicationException(string.Format("No config for Board or Object  '{0}' ", mid));
            }
        }

        public static string GetFullTextScript(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.FullTextScript;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.FullTextScript;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.FullTextScript;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.FullTextScript;
                    // case "oracle":  return orPostgre.Db.fullTextScript;
                    // case "db2":  return db2Postgre.Db.fullTextScript;
                    // case "other":  return othPostgre.Db.fullTextScript; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

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

        public static string GetProviderAssemblyName(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.ProviderAssemblyName;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.ProviderAssemblyName;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.ProviderAssemblyName;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.ProviderAssemblyName;
                    // case "oracle":  return orPostgre.Db.ProviderAssemblyName;
                    // case "db2":  return db2Postgre.Db.ProviderAssemblyName;
                    // case "other":  return othPostgre.Db.ProviderAssemblyName; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetPasswordPlaceholderVisible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PasswordPlaceholderVisible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PasswordPlaceholderVisible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PasswordPlaceholderVisible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PasswordPlaceholderVisible;
                    // case "oracle":  return orPostgre.Db.PasswordPlaceholderVisible;
                    // case "db2":  return db2Postgre.Db.PasswordPlaceholderVisible;
                    // case "other":  return othPostgre.Db.PasswordPlaceholderVisible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 1

        public static string GetParameter1_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter1_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter1Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter1_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter1Name;
                    // case "oracle":  return orPostgre.Db.Parameter1Name;
                    // case "db2":  return db2Postgre.Db.Parameter1Name;
                    // case "other":  return othPostgre.Db.Parameter1Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter1_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter1_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter1Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter1_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter1Value;
                    // case "oracle":  return orPostgre.Db.Parameter1Value;
                    // case "db2":  return db2Postgre.Db.Parameter1Value;
                    // case "other":  return othPostgre.Db.Parameter1Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter1_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter1_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter1Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter1_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter1Visible;
                    // case "oracle":  return orPostgre.Db.Parameter1Visible;
                    // case "db2":  return db2Postgre.Db.Parameter1Visible;
                    // case "other":  return othPostgre.Db.Parameter1Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 2

        public static string GetParameter2_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter2_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter2Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter2_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter2Name;
                    // case "oracle":  return orPostgre.Db.Parameter2Name;
                    // case "db2":  return db2Postgre.Db.Parameter2Name;
                    // case "other":  return othPostgre.Db.Parameter2Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter2_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter2_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter2Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter2_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter2Value;
                    // case "oracle":  return orPostgre.Db.Parameter2Value;
                    // case "db2":  return db2Postgre.Db.Parameter2Value;
                    // case "other":  return othPostgre.Db.Parameter2Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter2_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter2_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter2Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter2_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter2Visible;
                    // case "oracle":  return orPostgre.Db.Parameter2Visible;
                    // case "db2":  return db2Postgre.Db.Parameter2Visible;
                    // case "other":  return othPostgre.Db.Parameter2Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 3

        public static string GetParameter3_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter3_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter3Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter3_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter3Name;
                    // case "oracle":  return orPostgre.Db.Parameter3Name;
                    // case "db2":  return db2Postgre.Db.Parameter3Name;
                    // case "other":  return othPostgre.Db.Parameter3Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter3_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter3_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter3Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter3_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter3Value;
                    // case "oracle":  return orPostgre.Db.Parameter3Value;
                    // case "db2":  return db2Postgre.Db.Parameter3Value;
                    // case "other":  return othPostgre.Db.Parameter3Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter3_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter3_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter3Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter3_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter3Visible;
                    // case "oracle":  return orPostgre.Db.Parameter3Visible;
                    // case "db2":  return db2Postgre.Db.Parameter3Visible;
                    // case "other":  return othPostgre.Db.Parameter3Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 4

        public static string GetParameter4_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter4_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter4_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter4_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter4_Name;
                    // case "oracle":  return orPostgre.Db.Parameter4_Name;
                    // case "db2":  return db2Postgre.Db.Parameter4_Name;
                    // case "other":  return othPostgre.Db.Parameter4_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter4_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter4_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter4_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter4_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter4_Value;
                    // case "oracle":  return orPostgre.Db.Parameter4_Value;
                    // case "db2":  return db2Postgre.Db.Parameter4_Value;
                    // case "other":  return othPostgre.Db.Parameter4_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter4_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter4_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter4_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter4_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter4_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter4_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter4_Visible;
                    // case "other":  return othPostgre.Db.Parameter4_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 5

        public static string GetParameter5_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter5_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter5_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter5_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter5_Name;
                    // case "oracle":  return orPostgre.Db.Parameter5_Name;
                    // case "db2":  return db2Postgre.Db.Parameter5_Name;
                    // case "other":  return othPostgre.Db.Parameter5_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter5_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter5_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter5_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter5_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter5_Value;
                    // case "oracle":  return orPostgre.Db.Parameter5_Value;
                    // case "db2":  return db2Postgre.Db.Parameter5_Value;
                    // case "other":  return othPostgre.Db.Parameter5_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter5_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter5_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter5_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter5_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter5_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter5_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter5_Visible;
                    // case "other":  return othPostgre.Db.Parameter5_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 6

        public static string GetParameter6_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter6_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter6_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter6_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter6_Name;
                    // case "oracle":  return orPostgre.Db.Parameter6_Name;
                    // case "db2":  return db2Postgre.Db.Parameter6_Name;
                    // case "other":  return othPostgre.Db.Parameter6_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter6_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter6_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter6_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter6_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter6_Value;
                    // case "oracle":  return orPostgre.Db.Parameter6_Value;
                    // case "db2":  return db2Postgre.Db.Parameter6_Value;
                    // case "other":  return othPostgre.Db.Parameter6_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter6_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter6_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter6_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter6_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter6_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter6_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter6_Visible;
                    // case "other":  return othPostgre.Db.Parameter6_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 7

        public static string GetParameter7_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter7_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter7_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter7_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter7_Name;
                    // case "oracle":  return orPostgre.Db.Parameter7_Name;
                    // case "db2":  return db2Postgre.Db.Parameter7_Name;
                    // case "other":  return othPostgre.Db.Parameter7_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter7_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter7_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter7_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter7_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter7_Value;
                    // case "oracle":  return orPostgre.Db.Parameter7_Value;
                    // case "db2":  return db2Postgre.Db.Parameter7_Value;
                    // case "other":  return othPostgre.Db.Parameter7_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter7_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter7_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter7_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter7_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter7_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter7_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter7_Visible;
                    // case "other":  return othPostgre.Db.Parameter7_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 8

        public static string GetParameter8_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter8_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter8_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter8_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter8_Name;
                    // case "oracle":  return orPostgre.Db.Parameter8_Name;
                    // case "db2":  return db2Postgre.Db.Parameter8_Name;
                    // case "other":  return othPostgre.Db.Parameter8_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter8_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter8_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter8_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter8_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter8_Value;
                    // case "oracle":  return orPostgre.Db.Parameter8_Value;
                    // case "db2":  return db2Postgre.Db.Parameter8_Value;
                    // case "other":  return othPostgre.Db.Parameter8_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter8_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter8_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter8_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter8_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter8_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter8_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter8_Visible;
                    // case "other":  return othPostgre.Db.Parameter8_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 9

        public static string GetParameter9_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter9_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter9_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter9_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter9_Name;
                    // case "oracle":  return orPostgre.Db.Parameter9_Name;
                    // case "db2":  return db2Postgre.Db.Parameter9_Name;
                    // case "other":  return othPostgre.Db.Parameter9_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter9_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter9_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter9_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter9_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter9_Value;
                    // case "oracle":  return orPostgre.Db.Parameter9_Value;
                    // case "db2":  return db2Postgre.Db.Parameter9_Value;
                    // case "other":  return othPostgre.Db.Parameter9_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter9_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter9_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter9_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter9_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter9_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter9_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter9_Visible;
                    // case "other":  return othPostgre.Db.Parameter9_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 10

        public static string GetParameter10_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter10_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter10_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter10_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter10_Name;
                    // case "oracle":  return orPostgre.Db.Parameter10_Name;
                    // case "db2":  return db2Postgre.Db.Parameter10_Name;
                    // case "other":  return othPostgre.Db.Parameter10_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter10_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter10_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter10_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter10_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter10_Value;
                    // case "oracle":  return orPostgre.Db.Parameter10_Value;
                    // case "db2":  return db2Postgre.Db.Parameter10_Value;
                    // case "other":  return othPostgre.Db.Parameter10_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter10_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter10_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter10_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter10_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter10_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter10_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter10_Visible;
                    // case "other":  return othPostgre.Db.Parameter10_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        // Role=
        //Check boxes

        //Parameter 11 hides user password placeholder! 12 reserved for User Instance

        public static string GetParameter11_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter11_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter11_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter11_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter11_Name;
                    // case "oracle":  return orPostgre.Db.Parameter11_Name;
                    // case "db2":  return db2Postgre.Db.Parameter11_Name;
                    // case "other":  return othPostgre.Db.Parameter11_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter11_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter11_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter11_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter11_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter11_Value;
                    // case "oracle":  return orPostgre.Db.Parameter11_Value;
                    // case "db2":  return db2Postgre.Db.Parameter11_Value;
                    // case "other":  return othPostgre.Db.Parameter11_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter11_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter11_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter11_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter11_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter11_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter11_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter11_Visible;
                    // case "other":  return othPostgre.Db.Parameter11_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter12_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter12_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter12_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter12_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter12_Name;
                    // case "oracle":  return orPostgre.Db.Parameter12_Name;
                    // case "db2":  return db2Postgre.Db.Parameter12_Name;
                    // case "other":  return othPostgre.Db.Parameter12_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter12_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter12_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter12_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter12_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter12_Value;
                    // case "oracle":  return orPostgre.Db.Parameter12_Value;
                    // case "db2":  return db2Postgre.Db.Parameter12_Value;
                    // case "other":  return othPostgre.Db.Parameter12_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter12_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter12_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter12_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter12_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter12_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter12_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter12_Visible;
                    // case "other":  return othPostgre.Db.Parameter12_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static string GetParameter13_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter13_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter13_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter13_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter13_Name;
                    // case "oracle":  return orPostgre.Db.Parameter13_Name;
                    // case "db2":  return db2Postgre.Db.Parameter13_Name;
                    // case "other":  return othPostgre.Db.Parameter13_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter13_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter13_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter13_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter13_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter13_Value;
                    // case "oracle":  return orPostgre.Db.Parameter13_Value;
                    // case "db2":  return db2Postgre.Db.Parameter13_Value;
                    // case "other":  return othPostgre.Db.Parameter13_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter13_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter13_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter13_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter13_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter13_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter13_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter13_Visible;
                    // case "other":  return othPostgre.Db.Parameter13_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 14

        public static string GetParameter14_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter14_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter14_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter14_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter14_Name;
                    // case "oracle":  return orPostgre.Db.Parameter14_Name;
                    // case "db2":  return db2Postgre.Db.Parameter14_Name;
                    // case "other":  return othPostgre.Db.Parameter14_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter14_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter14_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter14_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter14_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter14_Value;
                    // case "oracle":  return orPostgre.Db.Parameter14_Value;
                    // case "db2":  return db2Postgre.Db.Parameter14_Value;
                    // case "other":  return othPostgre.Db.Parameter4_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter14_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter14_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter14_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter14_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter14_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter14_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter14_Visible;
                    // case "other":  return othPostgre.Db.Parameter14_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 15

        public static string GetParameter15_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter15_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter15_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter15_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter15_Name;
                    // case "oracle":  return orPostgre.Db.Parameter15_Name;
                    // case "db2":  return db2Postgre.Db.Parameter15_Name;
                    // case "other":  return othPostgre.Db.Parameter15_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter15_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter15_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter15_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter15_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter15_Value;
                    // case "oracle":  return orPostgre.Db.Parameter15_Value;
                    // case "db2":  return db2Postgre.Db.Parameter15_Value;
                    // case "other":  return othPostgre.Db.Parameter15_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter15_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter15_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter15_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter15_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter15_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter15_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter15_Visible;
                    // case "other":  return othPostgre.Db.Parameter15_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 16

        public static string GetParameter16_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter16_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter16_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter16_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter16_Name;
                    // case "oracle":  return orPostgre.Db.Parameter16_Name;
                    // case "db2":  return db2Postgre.Db.Parameter16_Name;
                    // case "other":  return othPostgre.Db.Parameter16_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter16_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter16_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter16_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter16_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter16_Value;
                    // case "oracle":  return orPostgre.Db.Parameter16_Value;
                    // case "db2":  return db2Postgre.Db.Parameter16_Value;
                    // case "other":  return othPostgre.Db.Parameter16_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter16_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter16_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter16_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter16_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter16_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter16_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter16_Visible;
                    // case "other":  return othPostgre.Db.Parameter16_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 17

        public static string GetParameter17_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter17_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter17_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter17_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter17_Name;
                    // case "oracle":  return orPostgre.Db.Parameter17_Name;
                    // case "db2":  return db2Postgre.Db.Parameter17_Name;
                    // case "other":  return othPostgre.Db.Parameter17_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter17_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter17_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter17_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter17_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter17_Value;
                    // case "oracle":  return orPostgre.Db.Parameter17_Value;
                    // case "db2":  return db2Postgre.Db.Parameter17_Value;
                    // case "other":  return othPostgre.Db.Parameter17_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter17_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter17_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter17_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter17_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter17_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter17_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter17_Visible;
                    // case "other":  return othPostgre.Db.Parameter17_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 18

        public static string GetParameter18_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter18_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter18_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter18_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter18_Name;
                    // case "oracle":  return orPostgre.Db.Parameter18_Name;
                    // case "db2":  return db2Postgre.Db.Parameter18_Name;
                    // case "other":  return othPostgre.Db.Parameter18_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter18_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter18_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter18_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter18_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter18_Value;
                    // case "oracle":  return orPostgre.Db.Parameter18_Value;
                    // case "db2":  return db2Postgre.Db.Parameter18_Value;
                    // case "other":  return othPostgre.Db.Parameter18_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter18_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter18_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter18_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter18_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter18_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter18_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter18_Visible;
                    // case "other":  return othPostgre.Db.Parameter18_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        //Parameter 19

        public static string GetParameter19_Name(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter19_Name;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter19_Name;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter19_Name;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter19_Name;
                    // case "oracle":  return orPostgre.Db.Parameter19_Name;
                    // case "db2":  return db2Postgre.Db.Parameter19_Name;
                    // case "other":  return othPostgre.Db.Parameter19_Name; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter19_Value(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter19_Value;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter19_Value;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter19_Value;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter19_Value;
                    // case "oracle":  return orPostgre.Db.Parameter19_Value;
                    // case "db2":  return db2Postgre.Db.Parameter19_Value;
                    // case "other":  return othPostgre.Db.Parameter19_Value; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetParameter19_Visible(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.Parameter19_Visible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.Parameter19_Visible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.Parameter19_Visible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.Parameter19_Visible;
                    // case "oracle":  return orPostgre.Db.Parameter19_Visible;
                    // case "db2":  return db2Postgre.Db.Parameter19_Visible;
                    // case "other":  return othPostgre.Db.Parameter19_Visible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }


        #endregion

        public static string[] GetScriptList(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.ScriptList;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.ScriptList;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.ScriptList;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.ScriptList;
                    // case "oracle":  return orPostgre.Db.scriptList;
                    // case "db2":  return db2Postgre.Db.scriptList;
                    // case "other":  return othPostgre.Db.scriptList; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

        public static bool GetPanelGetStats(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PanelGetStats;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PanelGetStats;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PanelGetStats;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PanelGetStats;
                    // case "oracle":  return orPostgre.Db.PanelGetStats;
                    // case "db2":  return db2Postgre.Db.PanelGetStats;
                    // case "other":  return othPostgre.Db.PanelGetStats; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetPanelRecoveryMode(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PanelRecoveryMode;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PanelRecoveryMode;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PanelRecoveryMode;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PanelRecoveryMode;
                    // case "oracle":  return orPostgre.Db.PanelRecoveryMode;
                    // case "db2":  return db2Postgre.Db.PanelRecoveryMode;
                    // case "other":  return othPostgre.Db.PanelRecoveryMode; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetPanelReindex(int? mid)
        {
            string dataEngine;
            string connectionString;
            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PanelReindex;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PanelReindex;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PanelReindex;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PanelReindex;
                    // case "oracle":  return orPostgre.Db.PanelReindex;
                    // case "db2":  return db2Postgre.Db.PanelReindex;
                    // case "other":  return othPostgre.Db.PanelReindex; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);
            }
        }

        public static bool GetPanelShrink(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.PanelShrink;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.PanelShrink;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.PanelShrink;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.PanelShrink;
                    // case "oracle":  return orPostgre.Db.PanelShrink;
                    // case "db2":  return db2Postgre.Db.PanelShrink;
                    // case "other":  return othPostgre.Db.PanelShrink; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }


        public static bool getBtnReindexVisible(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            switch (dataEngine)
            {
                case "System.Data.SqlClient":
                    return VZF.Data.MsSql.Db.btnReindexVisible;
                case "Npgsql":
                    return VZF.Data.Postgre.Db.btnReindexVisible;
                case "MySql.Data.MySqlClient":
                    return VZF.Data.Mysql.Db.btnReindexVisible;
                case "FirebirdSql.Data.FirebirdClient":
                    return VZF.Data.Firebird.Db.btnReindexVisible;
                    // case "oracle":  return orPostgre.Db.btnReindexVisible;
                    // case "db2":  return db2Postgre.Db.btnReindexVisible;
                    // case "other":  return othPostgre.Db.btnReindexVisible; 
                default:
                    throw new ArgumentOutOfRangeException(dataEngine);

            }
        }

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