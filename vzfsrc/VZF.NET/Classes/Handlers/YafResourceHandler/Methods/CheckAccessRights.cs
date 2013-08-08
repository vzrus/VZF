/* YetAnotherForum.NET
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

namespace YAF
{
    #region Using

    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        /// <summary>
        /// Checks the access rights.
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="messageId">The message id.</param>
        /// <returns>
        /// The check access rights.
        /// </returns>
        private bool CheckAccessRights([NotNull] object boardId, [NotNull] object messageId)
        {
            // Find user name
            MembershipUser user = Membership.GetUser();

            string browser = "{0} {1}".FormatWith(
                HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.Version);
            string platform = HttpContext.Current.Request.Browser.Platform;
            bool isMobileDevice = HttpContext.Current.Request.Browser.IsMobileDevice;
            bool isSearchEngine;
            bool dontTrack;
            string userAgent = HttpContext.Current.Request.UserAgent;

            // try and get more verbose platform name by ref and other parameters             
            UserAgentHelper.Platform(
                userAgent,
                HttpContext.Current.Request.Browser.Crawler,
                ref platform,
                ref browser,
                out isSearchEngine,
                out dontTrack);

            this.Get<StartupInitializeDb>().Run();

            object userKey = DBNull.Value;

            if (user != null)
            {
                userKey = user.ProviderUserKey;
            }

            string forumPage = this.Get<HttpRequestBase>().QueryString.ToString();
            string location = this.Get<HttpRequestBase>().FilePath;
            if (location.Contains("resource.ashx"))
            {
                forumPage = string.Empty;
                location = string.Empty;
            }

            var pageRow = CommonDb.pageload(
                mid: YafContext.Current.PageModuleID,
                sessionId: HttpContext.Current.Session.SessionID,
                boardId: boardId,
                userKey: userKey,
                ip: this.Get<HttpRequestBase>().GetUserRealIPAddress(),
                location: location,
                forumPage: forumPage,
                browser: browser,
                platform: platform,
                categoryId: null,
                forumId: null,
                topicId: null,
                messageId: messageId,
                //// don't track if this is a search engine
                isCrawler: isSearchEngine,
                isMobileDevice: isMobileDevice,
                donttrack: dontTrack);

            return pageRow.DownloadAccess || pageRow.ModeratorAccess;
        }
    }
}