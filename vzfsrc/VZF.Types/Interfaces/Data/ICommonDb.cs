// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="ICommonDb.cs">
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
//   The common db interface.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace YAF.Types.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
 
    using VZF.Types.Data;
    using VZF.Types.Objects;

    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Objects;

    /// <summary>
    /// The CommonDb interface.
    /// </summary>
    public interface ICommonDb
    {
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
        bool accessmask_delete(int? mid, object accessMaskID);

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
        DataTable accessmask_list(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask,
            int pageIndex,
            int pageSize);

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
        DataTable accessmask_pforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask);

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
        DataTable accessmask_aforumlist(
            int? mid,
            object boardId,
            object accessMaskID,
            object excludeFlags,
            object pageUserID,
            bool isUserMask,
            bool isAdminMask);

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
        void accessmask_save(
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
            object isAdminMask);

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
        DataTable active_list(
            int? mid,
            object boardId,
            object guests,
            object showCrawlers,
            int interval,
            object styledNicks);

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
        DataTable active_list_user(
            int? mid,
            object boardId,
            object userID,
            object guests,
            object showCrawlers,
            int activeTime,
            object styledNicks);

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
        DataTable active_listforum(int? mid, object forumID, object styledNicks);

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
        DataTable active_listtopic(int? mid, object topicID, object styledNicks);

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
        DataRow active_stats(int? mid, object boardId);

        /// <summary>
        /// Resets ActiveAccess cache table contents.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        void activeaccess_reset(int? mid);

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
        DataTable User_ListTodaysBirthdays(int? mid, object boardId, object useStyledNicks);

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
        DataTable admin_list(int? mid, object boardId, object useStyledNicks);

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
        DataTable admin_pageaccesslist(
            int? mid,
            [CanBeNull] object boardId,
            [NotNull] object useStyledNicks);

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
        DataTable adminpageaccess_list(int? mid, [CanBeNull] object userId, [CanBeNull] object pageName);

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
        void adminpageaccess_delete(int? mid, [NotNull] object userId, [CanBeNull] object pageName);

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
        void adminpageaccess_save(int? mid, [NotNull] object userId, [CanBeNull] object pageName);

        /// <summary>
        /// Deletes a user album.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="AlbumID">
        /// The album Id.
        /// </param>
        void album_delete(int? mid, object AlbumID);

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
        int[] album_getstats(int? mid, object UserID, object AlbumID);

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
        string album_gettitle(int? mid, object albumId);

        /// <summary>
        /// Deletes an image with a specified Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="imageId">
        /// The image id.
        /// </param>
        void album_image_delete(int? mid, object imageId);

        /// <summary>
        /// Saved downloaded image.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="imageId">
        /// The image id.
        /// </param>
        void album_image_download(int? mid, object imageId);

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
        DataTable album_images_by_user(int? mid, [NotNull] object userId);

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
        DataTable album_image_list(int? mid, object AlbumID, object ImageID);

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
        void album_image_save(
            int? mid,
            object imageId,
            object AlbumID,
            object Caption,
            object FileName,
            object Bytes,
            object ContentType);

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
        DataTable album_list(int? mid, object UserID, object AlbumID);

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
        int album_save(int? mid, object AlbumID, object UserID, object Title, object CoverImageID);

        /// <summary>
        /// Deletes an attachment by its id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="attachmentID">
        /// The attachment id.
        /// </param>
        void attachment_delete(int? mid, object attachmentID);

        /// <summary>
        /// Download an attachment with an Id.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="attachmentId">
        /// The attachmentId
        /// </param>
        void attachment_download(int? mid, object attachmentId);

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
        DataTable attachment_list(
            int? mid,
            object messageID,
            object attachmentID,
            object boardId,
            object pageIndex,
            object pageSize);

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
        void attachment_save(
            int? mid,
            object messageID,
            object fileName,
            object bytes,
            object contentType,
            Stream stream);

        /// <summary>
        /// Deletes a banned ip with an ID.
        /// </summary>
        /// <param name="mid">
        /// The module ID.
        /// </param>
        /// <param name="ID">
        /// The banned IP ID
        /// </param>
        void bannedip_delete(int? mid, object ID);

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
        DataTable bannedip_list(
            int? mid,
            object boardId,
            object ID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize);

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
        void bannedip_save(int? mid, object ID, object boardId, object Mask, string reason, int userID);

        /// <summary>
        /// The bbcode_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="bbcodeId">
        /// The bbcode id.
        /// </param> 
        void bbcode_delete(int? mid, object bbcodeId);

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
        DataTable bbcode_list(int? mid, object boardId, object bbcodeID);

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
        IEnumerable<TypedBBCode> BBCodeList(int? mid, int boardId, int? bbcodeID);

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
        void bbcode_save(
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
            object execorder);

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
        int board_create(
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
            object isHostUser);

        /// <summary>
        /// The board_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        void board_delete(int? mid, object boardId);

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
        DataTable board_list(int? mid, object boardId);

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
        board_poststats_Result board_poststats(
            int? mid,
            int? boardId,
            bool useStyledNicks,
            bool showNoCountPosts);

        /// <summary>
        /// Recalculates topic and post numbers and updates last post for all forums in all boards
        /// </summary>
        /// <param name="mid">
        /// The module Id.
        /// </param>
        void board_resync(int? mid);

        /// <summary>
        /// The board_resync.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        void board_resync(int? mid, object boardId);

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
        int board_save(
            int? mid,
            object boardId,
            object languageFile,
            object culture,
            object name,
            object allowThreaded);

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
        board_stats_Result board_stats(int? mid, object boardId);

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
        DataRow board_userstats(int? mid, int? boardId);

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
        string[] buddy_addrequest(int? mid, object FromUserID, object ToUserID);

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
        string buddy_approveRequest(int? mid, object FromUserID, object ToUserID, object Mutual);

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
        string buddy_denyRequest(int? mid, object FromUserID, object ToUserID);

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
        DataTable buddy_list(int? mid, object FromUserID);

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
        string buddy_remove(int? mid, object FromUserID, object ToUserID);

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
        bool category_delete(int? mid, object CategoryID);

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
        DataTable category_list(int? mid, object boardId, object categoryID);

        DataTable category_pfaccesslist(int? mid, object boardId, object categoryID);

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
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataTable category_getadjacentforum(
            int? mid,
            object boardId,
            object categoryID,
            object userId,
            bool isAfter);

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
        DataTable category_listread(int? mid, object boardId, object userId, object categoryID);

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
        DataTable category_simplelist(int? mid, int startID, int limit);

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
        void category_save(
            int? mid,
            object boardId,
            object categoryId,
            object name,
            object categoryImage,
            object sortOrder,
            object canHavePersForums);

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
        DataTable checkemail_list(int? mid, object email);

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
        void checkemail_save(int? mid, object userId, object hash, object email);

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
        DataTable checkemail_update(int? mid, object hash);

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
        void choice_add(int? mid, object pollID, object choice, object path, object mime);

        /// <summary>
        /// The choice_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="choiceID">
        /// The choice id.
        /// </param>
        void choice_delete(int? mid, object choiceID);

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
        void choice_update(int? mid, object choiceID, object choice, object path, object mime);

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
        void choice_vote(int? mid, object choiceID, object userId, object remoteIP);

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
        string db_getstats_new(int? mid);

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
        string db_getstats_warning(int? mid);

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string db_recovery_mode_warning(int? mid);

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
        string db_recovery_mode_new(int? mid, string dbRecoveryMode);

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
        string db_reindex_new(int? mid);

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
        string db_reindex_warning(int? mid);

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
        string db_runsql_new(int? mid, string sql, bool useTransaction);

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
        string db_shrink_new(int? mid);

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
        string db_shrink_warning(int? mid);

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
        DataSet ds_forumadmin(int? mid, object boardId, object pageUserID, object isUserForum);

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
        void forum_list_sort_basic(DataTable listsource, DataTable list, int parentid, int currentLevel);

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
        void eventlog_create(int? mid, object userId, object source, object description);

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
        void eventlog_create(
            int? mid,
            object userId,
            object source,
            object description,
            EventLogTypes type);

        /// <summary>
        /// Deletes event log entry of given ID.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="eventLogID">ID of event log entry.</param>
        /// <param name="pageUserId"> </param>
        void eventlog_delete(int? mid, object eventLogID, object pageUserId);

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
        void eventlog_deletebyuser(int? mid, object boardId, object userId);

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
        DataTable eventlog_list(
            int? mid,
            object boardId,
            [NotNull] object pageUserId,
            [NotNull] object maxRows,
            [NotNull] object maxDays,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object eventIDs);

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
        DataTable eventloggroupaccess_list(
            int? mid,
            [NotNull] object groupID,
            [NotNull] object eventTypeId);

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
        DataTable group_eventlogaccesslist(int? mid, [NotNull] object boardId);

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
        void eventloggroupaccess_save(
            int? mid,
            [NotNull] object groupId,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName,
            [NotNull] object deleteAccess);

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
        void eventloggroupaccess_delete(
            int? mid,
            [NotNull] object groupId,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName);

        /// <summary>
        /// The extension_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="extensionId">
        /// The extension id.
        /// </param>     
        void extension_delete(int? mid, object extensionId);

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
        DataTable extension_edit(int? mid, object extensionId);

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
        DataTable extension_list(int? mid, object boardId);

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
        DataTable extension_list(int? mid, object boardId, object extension);

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
        void extension_save(int? mid, object extensionId, object boardId, object extension);

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
        DataTable forum_byuserlist(
            int? mid,
            object boardId,
            object forumId,
            object userId,
            object isUserForum);

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
        DataTable forum_categoryaccess_activeuser(int? mid, object boardId, object userId);

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
        bool forum_delete(int? mid, object forumID);

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
        bool forum_move(int? mid, [NotNull] object forumOldID, [NotNull] object forumNewID);

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
        DataTable forum_list(int? mid, object boardId, object forumID);

        /// <summary>
        /// Listes all forums accessible to a user
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">BoardID</param>
        /// <param name="userId">ID of user</param>
        /// <returns>DataTable of all accessible forums</returns>
        DataTable forum_listall(int? mid, object boardId, object userId);

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
        DataTable forum_listall(int? mid, object boardId, object userId, object startAt, bool returnAll);

        /// <summary>
        /// Lists all forums within a given subcategory
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">BoardID</param>
        /// <param name="categoryID">The category ID. </param>
        /// <returns>DataTable with list</returns>
        DataTable forum_listall_fromCat(
            int? mid,
            object boardId,
            object categoryID,
            bool allowUserForumsOnly);

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
        DataTable forum_listall_fromCat(
            int? mid,
            object boardId,
            object categoryID,
            bool emptyFirstRow,
            bool allowUserForumsOnly);

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
        DataTable forum_sort_list(
            int? mid,
            DataTable listSource,
            int parentId,
            int categoryId,
            int startingIndent,
            int[] forumidExclusions,
            bool emptyFirstRow,
            bool returnAll);

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
        DataTable forum_listall_sorted(int? mid, object boardId, object userId, int[] forumidExclusions);

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
        DataTable forum_listall_sorted(int? mid, object boardId, object userId);

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
        DataTable forum_listall_sorted_all(int? mid, object boardId, object userId, bool returnAll);

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
        DataTable forum_ns_getchildren_activeuser(
            int? mid,
            int? boardid,
            int? categoryid,
            int? forumid,
            int userid,
            bool notincluded,
            bool immediateonly,
            string indentchars);

        /// <summary>
        /// Recreate tree.
        /// </summary>
        void forum_ns_recreate([NotNull] int? mid);

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
        DataTable forum_ns_getchildren(
            int? mid,
            int? boardid,
            int? categoryid,
            int? forumid,
            bool notincluded,
            bool immediateonly,
            string indentchars);

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
        DataTable forum_listall_sorted(
            int? mid,
            object boardId,
            object userId,
            int[] forumidExclusions,
            bool emptyFirstRow,
            int[] startAt,
            bool returnAll);

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
        void forum_list_sort_basic(
            int? mid,
            DataTable listsource,
            DataTable list,
            int parentid,
            int currentLvl);

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
        void forum_sort_list_recursive(
            int? mid,
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int categoryID,
            int currentIndent,
            bool returnAll);

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
        DataTable forum_tags(
            int? mid,
            int boardId,
            int pageUserId,
            int? forumId,
            int pageIndex,
            int pageSize,
            string searchText,
            bool beginsWith);

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
        DataTable forum_listallMyModerated(int? mid, object boardId, object userId);

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
        DataTable forum_listpath(int? mid, object forumID);

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
        DataTable forum_listread(
            int? mid,
            object boardId,
            object userId,
            object categoryId,
            object parentId,
            object useStyledNicks,
            bool findLastRead,
            [NotNull] bool showCommonForums,
            [NotNull] bool showPersonalForums,
            [CanBeNull] int? forumCreatedByUserId);

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
        DataTable forum_listreadpersonal(
            int? mid,
            object boardId,
            object userId,
            object categoryId,
            object parentId,
            object useStyledNicks,
            bool findLastRead,
            [NotNull] bool showCommonForums,
            [NotNull] bool showPersonalForums,
            [CanBeNull] int? forumCreatedByUserId);

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
        DataSet forum_moderatelist(int? mid, object userId, object boardId);

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
        DataTable forum_moderators(int? mid, bool useStyledNicks);

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
        void forum_resync(int? mid, object boardId, object forumID);

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
        long forum_save(
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
            bool canhavepersforums);

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
        int forum_save_parentschecker(int? mid, object forumID, object parentID);

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
        DataTable forum_simplelist(int? mid, int startID, int limit);

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
        DataTable forumaccess_group(int? mid, object groupID, object userId, bool includeUserForums);

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
        DataTable forumaccess_personalgroup(
            int? mid,
            object groupID,
            object userId,
            bool includeUserForums);

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
        DataTable forumaccess_list(int? mid, object forumID, object userId, bool includeUserGroups);

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
        void forumaccess_save(int? mid, object forumID, object groupID, object accessMaskID);

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
        IEnumerable<TypedForumListAll> ForumListAll(
            int? mid,
            int boardId,
            int userId,
            bool includeNoAccess);

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
        IEnumerable<TypedForumListAll> ForumListAll(int? mid, int boardId, int userId, int startForumId);

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
        /// The <see cref="IEnumerable{T}<TypedForumListAll>"/>.
        /// </returns>
        IEnumerable<TypedForumListAll> ForumListAll(
            [NotNull] int? mid,
            int boardId,
            int userId,
            List<int> startForumId,
            bool includeNoAccess);

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
        IEnumerable<TypedForumListAll> ForumListAll([NotNull] int? mid, int boardId, int userId);

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
        bool forumpage_initdb(int? mid, out string errorStr, bool debugging);

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
        string forumpage_validateversion(int? mid, int appVersion);

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
        DataTable GetSearchResult(
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
            bool includeChildren);

        /// <summary>
        /// The group_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>    
        void group_delete(int? mid, object groupID);

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
        DataTable group_list(int? mid, object boardId, object groupID, int pageIndex, int pageSize);

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
        DataTable group_byuserlist(
            int? mid,
            object boardId,
            object groupID,
            object userId,
            object isUserGroup);

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
        void group_medal_delete(int? mid, object groupID, object medalID);

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
        DataTable group_medal_list(int? mid, object groupID, object medalID);

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
        void group_medal_save(
            int? mid,
            object groupID,
            object medalID,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder);

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
        DataTable group_member(int? mid, object boardId, object userId);

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
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>  
        DataTable group_rank_style(int? mid, object boardId);

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
        long group_save(
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
            object personalGroupsNumber);

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
        void mail_create(
            int? mid,
            object @from,
            object fromName,
            object @to,
            object toName,
            object subject,
            object body,
            object bodyHtml);

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
        void mail_createwatch(
            int? mid,
            object topicID,
            object @from,
            object fromName,
            object subject,
            object body,
            object bodyHtml,
            object userId);

        /// <summary>
        /// The mail_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="mailID">
        /// The mail id.
        /// </param> 
        void mail_delete(int? mid, object mailID);

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
        IEnumerable<TypedMailList> MailList(int? mid, long processId);

        /// <summary>
        /// Deletes given medals.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="boardId">ID of board of which medals to delete. Required.</param>
        /// <param name="category">Cateogry of medals to delete. Can be null. In such case this parameter is ignored.</param>
        void medal_delete(int? mid, object boardId, object category);

        /// <summary>
        /// Deletes given medal.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID">ID of medal to delete.</param>
        void medal_delete(int? mid, object medalID);

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
        void medal_delete(int? mid, object boardId, object medalID, object category);

        /// <summary>
        /// Lists given medal.
        /// </summary>
        /// <param name="mid"> </param>
        /// <param name="medalID">ID of medal to list.</param>
        DataTable medal_list(int? mid, object medalID);

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
        DataTable medal_list(int? mid, object boardId, object category);

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
        DataTable medal_listusers(int? mid, object medalID);

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
        void medal_resort(int? mid, object boardId, object medalID, int move);

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
        bool medal_save(
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
            object flags);

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
        string message_AddThanks(int? mid, object fromUserID, object messageID, bool useDisplayName);

        /// <summary>
        /// The message_approve.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        void message_approve(int? mid, object messageID);

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
        void message_delete(
            int? mid,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked);

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
        void message_delete(
            int? mid,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool eraseMessage);

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
        DataTable message_findunread(
            int? mid,
            object topicID,
            object messageId,
            object lastRead,
            object showDeleted,
            object authorUserID);

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
        DataTable message_getRepliesList(int? mid, object messageID);

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
        DataTable message_GetTextByIds(int? mid, string messageIDs);

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
        DataTable message_GetThanks(int mid, object messageId);

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
        DataTable message_listreported(int? mid, object forumID);

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
        DataTable message_listreporters(int? mid, int messageID);

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
        DataTable message_listreporters(int? mid, int messageID, object userID);

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
        void message_move(int? mid, object messageID, object moveToTopic, bool moveAll);

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
        string message_RemoveThanks(int? mid, object fromUserID, object messageID, bool useDisplayName);

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
        void message_report(
            int? mid,
            object messageID,
            object userId,
            object reportedDateTime,
            object reportText);

        /// <summary>
        /// The message_reportcopyover.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        void message_reportcopyover(int? mid, object messageID);

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
        void message_reportresolve(int? mid, object messageFlag, object messageID, object userId);

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
        bool message_save(
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
            ref long messageId);

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
        DataTable message_secdata(int? mid, int messageId, object pageUserId);

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
        DataTable message_simplelist(int? mid, int StartID, int Limit);

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
        int message_ThanksNumber(int? mid, object messageID);

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
        DataTable message_unapproved(int? mid, object forumID);

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
        void message_update(
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
            string tags);

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
        IEnumerable<TypedAllThanks> MessageGetAllThanks(int? mid, string messageIdsSeparatedWithColon);

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
        DataTable messagehistory_list(int? mid, int messageID, int daysToClean);

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
        IEnumerable<TypedMessageList> MessageList(int? mid, int messageID);

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
        DataTable moderators_team_list(int? mid, bool useStyledNicks);

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
        void Readtopic_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object topicID);

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
        DateTime? User_LastRead(int? mid, [NotNull] object userID);

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
        DateTime? Readtopic_lastread(int? mid, [NotNull] object userID, [NotNull] object topicID);

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
        void ReadForum_AddOrUpdate(int? mid, [NotNull] object userID, [NotNull] object forumID);

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
        DateTime? ReadForum_lastread(int? mid, [NotNull] object userID, [NotNull] object forumID);

        /// <summary>
        /// The nntpforum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpForumID">
        /// The nntp forum id.
        /// </param>  
        void nntpforum_delete(int? mid, object nntpForumID);

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
        DataTable nntpforum_list(
            int? mid,
            object boardId,
            object minutes,
            object nntpForumID,
            object active);

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
        void nntpforum_save(
            int? mid,
            object nntpForumID,
            object nntpServerID,
            object groupName,
            object forumID,
            object active,
            object cutoffdate);

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
        void nntpforum_update(int? mid, object nntpForumID, object lastMessageNo, object userId);

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
        IEnumerable<TypedNntpForum> NntpForumList(
            int? mid,
            int boardId,
            int? minutes,
            int? nntpForumID,
            bool? active);

        /// <summary>
        /// The nntpserver_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="nntpServerID">
        /// The nntp server id.
        /// </param>  
        void nntpserver_delete(int? mid, object nntpServerID);

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
        DataTable nntpserver_list(int? mid, object boardId, object nntpServerID);

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
        void nntpserver_save(
            int? mid,
            object nntpServerID,
            object boardId,
            object name,
            object address,
            object port,
            object userName,
            object userPass);

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
        DataTable nntptopic_list(int? mid, object thread);

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
        void nntptopic_savemessage(
            int? mid,
            object nntpForumID,
            object topic,
            object body,
            object userId,
            object userName,
            object ip,
            object posted,
            object externalMessageId,
            object referenceMessageId);

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
        pageload_Result pageload(
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
            object donttrack);

        /// <summary>
        /// The pmessage_archive.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        void pmessage_archive(int? mid, object userPMessageID);

        /// <summary>
        /// The pmessage_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        void pmessage_delete(int? mid, object userPMessageID);

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
        void pmessage_delete(int? mid, object userPMessageID, bool fromOutbox);

        /// <summary>
        /// The pmessage_info.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>     
        DataTable pmessage_info(int? mid);

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
        DataTable pmessage_list(int? mid, object userPMessageID);

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
        DataTable pmessage_list(int? mid, object toUserID, object fromUserID, object userPMessageID);

        /// <summary>
        /// The pmessage_markread.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        void pmessage_markread(int? mid, object userPMessageID);

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
        void pmessage_prune(int? mid, object daysRead, object daysUnread);

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
        void pmessage_save(
            int? mid,
            object fromUserID,
            object toUserID,
            object subject,
            object body,
            object Flags,
            object replyTo);

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
        void poll_remove(
            int? mid,
            object pollGroupID,
            object pollID,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere);

        /// <summary>
        /// The poll_save.
        /// </summary>
        /// <param name="pollList">
        /// The poll list.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>   
        int? poll_save(PollGroup pollList);

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
        DataTable poll_stats(int? mid, int? pollId);

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
        int pollgroup_attach(
            int? mid,
            int? pollGroupId,
            int? topicId,
            int? forumId,
            int? categoryId,
            int? boardId);

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
        void pollgroup_remove(
            int? mid,
            object pollGroupID,
            object topicId,
            object forumId,
            object categoryId,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere);

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
        DataTable pollgroup_stats(int? mid, int? pollGroupId);

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
        void poll_update(
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
            object questionMime);

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
        DataTable pollgroup_votecheck(int? mid, object pollGroupId, object userId, object remoteIp);

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
        IEnumerable<TypedPollGroup> PollGroupList(int? mid, int userID, int? forumId, int boardId);

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
        DataTable pollvote_check(int? mid, object pollid, object userid, object remoteip);

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
        DataTable post_alluser(
            int? mid,
            object boardid,
            object userid,
            object pageUserID,
            object topCount);

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
        DataTable post_list(
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
            DateTime lastRead);

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
        DataTable post_list_reverse10(int? mid, object topicID);

        /// <summary>
        /// The rank_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        void rank_delete(int? mid, object rankID);

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
        IEnumerable<rank_list_Result> rank_list(int? mid, object boardId, object rankID);

        DataTable rank_list(int? mid, object boardId);

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
        void rank_save(
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
            object usrAlbumImages);

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
        DataTable recent_users(int? mid, object boardID, int timeSinceLastLogin, object styledNicks);

        /// <summary>
        /// The registry_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataTable registry_list(int? mid);

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
        DataTable registry_list(int? mid, object name);

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
        DataTable registry_list(int? mid, object name, object boardId);

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
        void registry_save(int? mid, object name, object value);

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
        void registry_save(int? mid, object name, object value, object boardId);

        /// <summary>
        /// The replace_words_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        void replace_words_delete(int? mid, object id);

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
        DataTable replace_words_list(int? mid, object boardId, object id);

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
        void replace_words_save(int? mid, object boardId, object id, object badword, object goodword);

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
        DataTable rss_topic_latest(
            int? mid,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts);

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
        DataTable rsstopic_list(int? mid, int forumID, int topicCount);

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
        DataTable rsstopic_list(int? mid, int forumID, int topicStart, int topicCount);

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
        void SetPropertyValues(
            int? mid,
            int boardId,
            string appname,
            string tableName,
            int userId,
            string userName,
            SettingsPropertyValueCollection collection,
            bool dirtyOnly = true);

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
        bool shoutbox_clearmessages(int? mid, int boardId);

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
        DataTable shoutbox_getmessages(int? mid, int boardId, int numberOfMessages, object useStyledNicks);

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
        bool shoutbox_savemessage(
            int? mid,
            int boardId,
            string message,
            string userName,
            int userID,
            object ip);

        /// <summary>
        /// The smiley_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>    
        void smiley_delete(int? mid, object smileyID);

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
        DataTable smiley_list(int? mid, object boardId, object smileyID);

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
        DataTable smiley_listunique(int? mid, object boardId);

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
        void smiley_resort(int? mid, object boardId, object smileyID, int move);

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
        void smiley_save(
            int? mid,
            object smileyID,
            object boardId,
            object code,
            object icon,
            object emoticon,
            object sortOrder,
            object replace);

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
        IEnumerable<TypedSmileyList> SmileyList(int? mid, int boardId, int? smileyID);

        /// <summary>
        /// The system_deleteinstallobjects.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>   
        void system_deleteinstallobjects(int? mid);

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
        void system_initialize(
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
            string rolePrefix);

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
        void system_initialize_executescripts(
            int? mid,
            string script,
            string scriptFile,
            bool useTransactions);

        /// <summary>
        /// The system_list.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>   
        DataTable system_list(int? mid);

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
        void system_updateversion(int? mid, int version, string name);

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
        bool TestConnection(
            [NotNull] out string exceptionMessage,
            string connectionString,
            string providerName);

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
        DataTable topic_active(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead);

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
        DataTable topic_announcements(
            int? mid,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId);

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
        DataTable topic_unanswered(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead);

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
        DataTable topic_unread(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead);

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
        DataTable Topics_ByUser(
            int? mid,
            [NotNull] object boardId,
            [NotNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead);

        /// <summary>
        /// The topic status_ delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicStatusID">
        /// The topic status id.
        /// </param>
        void TopicStatus_Delete(int? mid, [NotNull] object topicStatusID);

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
        DataTable TopicStatus_Edit(int? mid, [NotNull] object topicStatusID);

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
        DataTable TopicStatus_List(int? mid, [NotNull] object boardId);

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
        void TopicStatus_Save(
            int? mid,
            [NotNull] object topicStatusID,
            [NotNull] object boardID,
            [NotNull] object topicStatusName,
            [NotNull] object defaultDescription);

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
        long topic_create_by_message(int? mid, object messageID, object forumId, object newTopicSubj);

        /// <summary>
        /// The topic_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        void topic_delete(int? mid, object topicID);

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
        void topic_delete(int? mid, object topicID, object eraseTopic);

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
        void topic_favorite_add(int? mid, object userID, object topicID);

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
        DataTable topic_favorite_details(
            int? mid,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead);

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
        DataTable topic_favorite_list(int? mid, object userID);

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
        void topic_favorite_remove(int? mid, object userID, object topicID);

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
        int topic_findduplicate(int? mid, object topicName);

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
        DataTable topic_findnext(int? mid, object topicID);

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
        DataTable topic_findprev(int? mid, object topicID);

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
        DataRow topic_info(int? mid, object topicID, bool getTags);

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
        void topic_imagesave(
            int? mid,
            object topicID,
            object imageUrl,
            Stream stream,
            object topicImageType);

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
        DataTable topic_latest(
            int? mid,
            object boardID,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts,
            [CanBeNull] bool findLastRead);

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
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataTable topic_list(
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
            [NotNull] bool getTags);

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
        /// The <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataTable announcements_list(
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
            [NotNull] bool getTags);

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
        void topic_lock(int? mid, object topicID, object locked);

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
        void topic_move(int? mid, object topicID, object forumID, object showMoved, object linkDays);

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
        int topic_prune(
            int? mid,
            [NotNull] object boardID,
            [NotNull] object forumID,
            [NotNull] object days,
            [NotNull] object permDelete);

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
        long topic_save(
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
            string tags);

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
        DataTable topic_simplelist(int? mid, int StartID, int Limit);

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
        DataTable topic_tags(int? mid, int boardId, int pageUserId, int topicId);

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
        DataTable topic_bytags(
            int? mid,
            int boardId,
            int forumId,
            object pageUserId,
            string tags,
            object date,
            int pageIndex,
            int pageSize);

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
        void topic_updatetopic(int? mid, int topicId, string topic);

        /// <summary>
        /// The unencode_all_topics_subjects.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="decodeTopicFunc">
        /// The decode topic func.
        /// </param>        
        void unencode_all_topics_subjects(int? mid, Func<string, string> decodeTopicFunc);

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
        DataTable user_accessmasks(int? mid, object boardId, object userId);

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
        DataTable user_accessmasksbyforum(int? mid, object boardId, object userId);

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
        DataTable user_accessmasksbygroup(int? mid, object boardId, object userId);

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
        DataTable user_activity_rank(int? mid, object boardId, object startDate, object displayNumber);

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
        void user_addignoreduser(int? mid, object userId, object ignoredUserId);

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
        void user_addpoints(int? mid, object userId, object forumUserId, object points);

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
        void user_adminsave(
            int? mid,
            object boardId,
            object userId,
            object name,
            object displayName,
            object @email,
            object flags,
            object rankID);

        /// <summary>
        /// The user_approve.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        void user_approve(int? mid, object userId);

        /// <summary>
        /// The user_approveall.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        void user_approveall(int? mid, object boardId);

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
        int user_aspnet(
            int? mid,
            int boardId,
            string userName,
            string displayName,
            string email,
            object providerUserKey,
            object isApproved);

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
        DataTable user_avatarimage(int? mid, object userId);

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
        bool user_changepassword(int? mid, object userId, object oldPassword, object newPassword);

        /// <summary>
        /// The user_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param> 
        void user_delete(int? mid, object userId);

        /// <summary>
        /// The user_deleteavatar.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        void user_deleteavatar(int? mid, object userId);

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
        void user_deleteold(int? mid, object boardId, object days);

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
        DataTable user_emails(int? mid, object boardId, object groupID);

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
        int user_get(int? mid, int boardId, object providerUserKey);

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
        DataTable user_getalbumsdata(int? mid, object userID, object boardID);

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
        int user_getpoints(int? mid, object userId);

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
        string user_getsignature(int? mid, object userId);

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
        DataTable user_getsignaturedata(int? mid, object userID, object boardID);

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
        int user_getthanks_from(int? mid, object userId, object pageUserId);

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
        int[] user_getthanks_to(int? mid, object userID, object pageUserId);

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
        int? user_guest(int? mid, object boardId);

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
        DataTable user_ignoredlist(int? mid, object userId);

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
        bool user_isuserignored(int? mid, object userId, object ignoredUserId);

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
        DataRow user_lazydata(
            int? mid,
            object userID,
            object boardID,
            bool showPendingMails,
            bool showPendingBuddies,
            bool showUnreadPMs,
            bool showUserAlbums,
            bool styledNicks);

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
        DataTable user_list(int? mid, object boardID, object userID, object approved);

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
        DataTable user_list(
            int? mid,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks);

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
        DataTable user_pagedlist(
            int? mid,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks,
            object pageIndex,
            object pageSize);

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
        DataTable user_listmedals(int? mid, object userId);

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
        DataTable user_listmembers(
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
            object numPostCompare);

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
        void user_medal_delete(int? mid, object userId, object medalID);

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
        DataTable user_medal_list(int? mid, object userId, object medalID);

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
        void user_medal_save(
            int? mid,
            object userId,
            object medalId,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder,
            object dateAwarded);

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
        void user_migrate(int? mid, object userId, object providerUserKey, object updateProvider);

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
        int user_nntp(int? mid, object boardId, object userName, object email, int? timeZone);

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
        DataTable user_pmcount(int? mid, object userId);

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
        void user_removeignoreduser(int? mid, object userId, object ignoredUserId);

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
        void user_removepoints(int? mid, object userId, [CanBeNull] object fromUserID, object points);

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
        bool user_RepliedTopic(int? mid, [NotNull] object messageId, [NotNull] object userId);

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
        void user_save(
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
            object postsPerPage);

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
        void user_saveavatar(
            int? mid,
            object userId,
            object avatar,
            Stream stream,
            object avatarImageType);

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
        void user_savenotification(
            int? mid,
            object userId,
            object pmNotification,
            object autoWatchTopics,
            UserNotificationSetting notificationType,
            object dailyDigest);

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
        void user_savesignature(int? mid, object userId, object signature);

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
        void user_setnotdirty(int? mid, int boardId, int userId);

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
        void user_setpoints(int? mid, object userId, object points);

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
        void user_setrole(int? mid, int boardId, object providerUserKey, object role);

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
        DataTable user_simplelist(int? mid, int startID, int limit);

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
        void user_suspend(int? mid, object userId, object suspend);

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
        void user_update_single_sign_on_status(
            int? mid,
            [NotNull] object userId,
            [NotNull] object isFacebookUser,
            [NotNull] object isTwitterUser);

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
        bool user_ThankedMessage(int? mid, object messageId, object userId);

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
        int user_ThankFromCount(int? mid, [NotNull] object userId);

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
        DataTable user_viewthanksfrom(
            int? mid,
            object UserID,
            object pageUserId,
            int pageIndex,
            int pageSize);

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
        DataTable user_viewthanksto(
            int? mid,
            object UserID,
            object pageUserId,
            int pageIndex,
            int pageSize);

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
        IEnumerable<TypedUserFind> UserFind(
            int? mid,
            int boardId,
            bool filter,
            string userName,
            string @email,
            string displayName,
            object notificationType,
            object dailyDigest);

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
        void userforum_delete(int? mid, object userId, object forumID);

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
        DataTable userforum_list(int? mid, object userId, object forumID);

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
        void userforum_save(int? mid, object userId, object forumID, object accessMaskID);

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
        DataTable usergroup_list(int? mid, object userId);

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
        void usergroup_save(int? mid, object userId, object groupId, object member);

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
        IEnumerable<TypedUserList> UserList(
            int? mid,
            int boardId,
            int? userId,
            bool? approved,
            int? groupID,
            int? rankID,
            bool? useStyledNicks);

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
        void watchforum_add(int? mid, object userId, object forumID);

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
        DataTable watchforum_check(int? mid, object userId, object forumID);

        /// <summary>
        /// The watchforum_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="watchForumID">
        /// The watch forum id.
        /// </param>    
        void watchforum_delete(int? mid, object watchForumID);

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
        DataTable watchforum_list(int? mid, object userId);

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
        void watchtopic_add(int? mid, object userId, object topicID);

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
        DataTable watchtopic_check(int? mid, object userId, object topicID);

        /// <summary>
        /// The watchtopic_delete.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="watchTopicID">
        /// The watch topic id.
        /// </param>
        void watchtopic_delete(int? mid, object watchTopicID);

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
        DataTable watchtopic_list(int? mid, object userId);

        /// <summary>
        /// The get db size.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int GetDbSize(int? mid);

        /// <summary>
        /// The get is forum installed.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool GetIsForumInstalled(int? mid);

        /// <summary>
        /// The get db version.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>      
        int GetDBVersion(int? mid);

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
        bool GetFullTextSupported(int? mid);

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
        string GetFullTextScript(int? mid);

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
        List<ConnectionStringParameter> ConnectionParameters(int? mid);

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
        string[] GetScriptList(int? mid);

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
        bool GetPanelGetStats(int? mid);

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
        bool GetPanelRecoveryMode(int? mid);

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
        bool GetPanelReindex(int? mid);

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
        bool GetPanelShrink(int? mid);

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
        bool getBtnReindexVisible(int? mid);
       
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
        string db_defaultcollation(int? mid, string dbcharset);

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
        string db_checkvalidcharset(int? mid, string dbcharset);

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
        string db_getfirstcharset(int? mid);

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
        string db_getfirstcollation(int? mid);

        string db_allowsSchemaName(int? mid);

        DataTable GetProfileStructure(string connectionStringName, string tableName);
    }
}