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

namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Provides Active Users location info
    /// </summary>
    public class ActiveLocation : BaseControl
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the ForumID of the current location
        /// </summary>
        public int ForumID
        {
            get
            {
                if (this.ViewState["ForumID"] != null)
                {
                    return Convert.ToInt32(this.ViewState["ForumID"]);
                }

                return -1;
            }

            set
            {
                this.ViewState["ForumID"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the forumname of the current location
        /// </summary>
        [NotNull]
        public string ForumName
        {
            get
            {
                if (this.ViewState["ForumName"] != null)
                {
                    return this.ViewState["ForumName"].ToString();
                }

                return string.Empty;
            }

            set
            {
                this.ViewState["ForumName"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the localization tag for the current location.
        ///   It should be  equal to page name
        /// </summary>
        [NotNull]
        public string ForumPage
        {
            get
            {
                if (this.ViewState["ForumPage"] != null || this.ViewState["ForumPage"] != DBNull.Value)
                {
                    // string localizedPage = ViewState["ForumPage"].ToString().Substring(ViewState["ForumPage"].ToString().IndexOf("default.aspx?") - 14, ViewState["ForumPage"].ToString().IndexOf("&"));
                    return this.ViewState["ForumPage"].ToString();
                }

                return "MAINPAGE";
            }

            set
            {
                this.ViewState["ForumPage"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether to show only topic link.
        /// </summary>
        public bool HasForumAccess
        {
            get
            {
                if (this.ViewState["HasForumAccess"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["HasForumAccess"]);
                }

                return true;
            }

            set
            {
                this.ViewState["HasForumAccess"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether to show only topic link.
        /// </summary>
        public bool LastLinkOnly
        {
            get
            {
                if (this.ViewState["LastLinkOnly"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["LastLinkOnly"]);
                }

                return false;
            }

            set
            {
                this.ViewState["LastLinkOnly"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the topicid of the current location
        /// </summary>
        public int TopicID
        {
            get
            {
                if (this.ViewState["TopicID"] != null)
                {
                    return Convert.ToInt32(this.ViewState["TopicID"]);
                }

                return -1;
            }

            set
            {
                this.ViewState["TopicID"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the topicname of the current location
        /// </summary>
        [NotNull]
        public string TopicName
        {
            get
            {
                if (this.ViewState["TopicName"] != null)
                {
                    return this.ViewState["TopicName"].ToString();
                }

                return string.Empty;
            }

            set
            {
                this.ViewState["TopicName"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the userid of the current user
        /// </summary>
        public int UserID
        {
            get
            {
                if (this.ViewState["UserID"] != null)
                {
                    return Convert.ToInt32(this.ViewState["UserID"]);
                }

                return -1;
            }

            set
            {
                this.ViewState["UserID"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets the UserName of the current user
        /// </summary>
        [NotNull]
        public string UserName
        {
            get
            {
                if (this.ViewState["UserName"] != null)
                {
                    return this.ViewState["UserName"].ToString();
                }

                return string.Empty;
            }

            set
            {
                this.ViewState["UserName"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        protected override void Render([NotNull] HtmlTextWriter output)
        {

            var forumPageName = this.ForumPage;
            string forumPageAttributes = null;
            var outText = new StringBuilder();

            // Find a user page name. If it's missing we are very probably on the start page 
            if (string.IsNullOrEmpty(forumPageName))
            {
                forumPageName = "MAINPAGE";
                outText.Append(this.GetText("ACTIVELOCATION", "MAINPAGE"));
            }
            else
            {
                // We find here a page name start position
                if (forumPageName.Contains("g="))
                {
                    forumPageName =
                        forumPageName.Substring(forumPageName.IndexOf("g=", StringComparison.OrdinalIgnoreCase) + 2);

                    // We find here a page name end position
                    if (forumPageName.Contains("&"))
                    {
                        forumPageAttributes =
                            forumPageName.Substring(forumPageName.IndexOf("&", StringComparison.OrdinalIgnoreCase) + 1);
                        forumPageName = forumPageName.Substring(
                            0,
                            forumPageName.IndexOf("&", StringComparison.OrdinalIgnoreCase));
                    }
                }
                else
                {
                    if (Config.IsDotNetNuke)
                    {
                        int idxfrst = forumPageName.IndexOf("&", StringComparison.OrdinalIgnoreCase);
                        forumPageName = forumPageName.Substring(idxfrst + 1);
                    }

                    int idx = forumPageName.IndexOf("=", StringComparison.OrdinalIgnoreCase);
                    if (idx > 0)
                    {
                        forumPageAttributes = forumPageName.Substring(
                            0,
                            forumPageName.IndexOf("&", StringComparison.OrdinalIgnoreCase) > 0
                                ? forumPageName.IndexOf("&", StringComparison.OrdinalIgnoreCase)
                                : forumPageName.Length - 1);
                        forumPageName = forumPageName.Substring(0, idx);
                    }
                }
            }

            

            output.BeginRender();

            // All pages should be processed in call frequency order 
            if (Enum.IsDefined(typeof(ForumPages), forumPageName))
            {
                var pageParced = (ForumPages)Enum.Parse(typeof(ForumPages), forumPageName);
                
                // We are in messages
                if (this.TopicID > 0 && this.ForumID > 0)
                {
                    switch (pageParced)
                    {
                        case ForumPages.topics:
                            outText.Append(this.GetText("ACTIVELOCATION", "TOPICS"));
                            break;

                        case ForumPages.posts:
                            outText.Append(this.GetText("ACTIVELOCATION", "POSTS"));
                            break;

                        case ForumPages.postmessage:
                            outText.Append(this.GetText("ACTIVELOCATION", "POSTMESSAGE_FULL"));
                            break;

                        case ForumPages.reportpost:
                            outText.Append(this.GetText("ACTIVELOCATION", "REPORTPOST"));
                            outText.Append(". ");
                            outText.Append(this.GetText("ACTIVELOCATION", "TOPICS"));
                            break;

                        case ForumPages.messagehistory:
                            outText.Append(this.GetText("ACTIVELOCATION", "MESSAGEHISTORY"));
                            outText.Append(". ");
                            outText.Append(this.GetText("ACTIVELOCATION", "TOPICS"));
                            break;

                        default:
                            outText.Append(this.GetText("ACTIVELOCATION", "POSTS"));
                            break;
                    }

                    if (this.HasForumAccess)
                    {
                        outText.Append(
                            @"<a href=""{0}"" id=""topicid_{1}""  title=""{2}"" runat=""server""> {3} </a>".FormatWith(
                                YafBuildLink.GetLink(ForumPages.posts, "t={0}", this.TopicID),
                                this.UserID,
                                this.GetText("COMMON", "VIEW_TOPIC"),
                                HttpUtility.HtmlEncode(this.TopicName)));
                        if (!this.LastLinkOnly)
                        {
                            outText.Append(this.GetText("ACTIVELOCATION", "TOPICINFORUM"));
                            outText.Append(
                                @"<a href=""{0}"" id=""forumidtopic_{1}"" title=""{2}"" runat=""server""> {3} </a>"
                                    .FormatWith(
                                        YafBuildLink.GetLink(ForumPages.topics, "f={0}", this.ForumID),
                                        this.UserID,
                                        this.GetText("COMMON", "VIEW_FORUM"),
                                        HttpUtility.HtmlEncode(this.ForumName)));
                        }
                    }
                }
                else if (this.ForumID > 0 && this.TopicID <= 0)
                {
                    // User views a forum
                    switch (pageParced)
                    {
                        case ForumPages.topics:
                            outText.Append(this.GetText("ACTIVELOCATION", "FORUM"));

                            if (this.HasForumAccess)
                            {
                                outText.Append(
                                    @"<a href=""{0}"" id=""forumid_{1}"" title=""{2}"" runat=""server""> {3} </a>"
                                        .FormatWith(
                                            YafBuildLink.GetLink(ForumPages.topics, "f={0}", this.ForumID),
                                            this.UserID,
                                            this.GetText("COMMON", "VIEW_FORUM"),
                                            HttpUtility.HtmlEncode(this.ForumName)));
                            }

                            break;
                        case ForumPages.topicsbytags:
                            outText.Append(this.GetText("ACTIVELOCATION", "TOPICBYTAGS_FORUM"));

                            if (this.HasForumAccess)
                            {
                                outText.Append(
                                    @"<a href=""{0}"" id=""topicbytagsforumid_{1}"" title=""{2}"" runat=""server""> {3} </a>"
                                        .FormatWith(
                                            YafBuildLink.GetLink(ForumPages.topics, "f={0}", this.ForumID),
                                            this.UserID,
                                            this.GetText("ACTIVELOCATION", "TOPICBYTAGS_FORUM"),
                                            HttpUtility.HtmlEncode(this.ForumName)));
                            }
                            break;
                    }
                }
                else if (this.ForumID <= 0 && this.TopicID > 0)
                {
                    if (pageParced == ForumPages.topicsbytags)
                    {
                        outText.Append(this.GetText("ACTIVELOCATION", "TOPICBYTAGS_TOPIC"));

                        outText.Append(
                            @"<a href=""{0}"" id=""topicbytagsforumid_{1}"" title=""{2}"" runat=""server""> {3} </a>"
                                .FormatWith(
                                    string.Empty,
                            // YafBuildLink.GetLink(ForumPages.posts, "t={0}", this.TopicID),
                                    this.UserID,
                                    this.GetText("ACTIVELOCATION", "TOPICBYTAGS_TOPIC"),
                                    HttpUtility.HtmlEncode(this.TopicName)));
                    }
                }
                else
                {
                    // First specially treated pages where we can render
                    // an info about user name, etc. 
                    switch (pageParced)
                    {
                        case ForumPages.boardtags:
                            outText.Append(this.GetText("ACTIVELOCATION", "TAGS_BOARD"));
                            break;
                        case ForumPages.profile:
                            outText.Append(this.Profile(forumPageAttributes));
                            break;
                        case ForumPages.albums:
                            outText.Append(this.Albums(forumPageAttributes));
                            break;
                        case ForumPages.album:
                            outText.Append(this.Album(forumPageAttributes));
                            break;
                        case ForumPages.mostactiveusers:
                            outText.Append(this.GetText("ACTIVELOCATION", "MOSTACTIVEUSERS"));
                            break;
                        case ForumPages.help_index:
                            outText.Append(this.GetText("ACTIVELOCATION", "HELPINDEX"));
                            break;
                        case ForumPages.forum:
                            if (this.TopicID <= 0 && this.ForumID <= 0)
                            {
                                outText.Append(
                                    this.ForumPage.Contains("c=")
                                        ? this.GetText("ACTIVELOCATION", "FORUMFROMCATEGORY")
                                        : this.GetText("ACTIVELOCATION", "MAINPAGE"));
                            }
                            break;

                        default:
                            if (!YafContext.Current.IsAdmin && pageParced.ToString().ToUpper().Contains("MODERATE_"))
                            {
                                // We shouldn't show moderators activity to all users but admins
                                outText.Append(this.GetText("ACTIVELOCATION", "MODERATE"));
                            }
                            else if (!YafContext.Current.IsHostAdmin && pageParced.ToString().ToUpper().Contains("ADMIN_"))
                            {
                                // We shouldn't show admin activity to all users 
                                outText.Append(this.GetText("ACTIVELOCATION", "ADMINTASK"));
                            }
                            else
                            {
                                // Generic action name based on page name
                                outText.Append(this.GetText("ACTIVELOCATION", pageParced.ToString().ToUpper()));
                            }

                            break;
                    }
                } 
            }

            var outputText = outText.ToString();

            if (outputText.Contains("ACTIVELOCATION") || string.IsNullOrEmpty(outputText.Trim()))
            {
                    if (this.Get<YafBoardSettings>().EnableActiveLocationErrorsLog)
                    {
                        CommonDb.eventlog_create(
                            PageContext.PageModuleID,
                            this.UserID,
                            this,
                            "Incorrect active location string: ForumID = {0}; ForumName= {1}; ForumPage={2}; TopicID={3}; TopicName={4}; UserID={5}; UserName={6}; Attributes={7}; ForumPageName={8}"
                                .FormatWith(
                                    this.ForumID,
                                    this.ForumName,
                                    this.ForumPage,
                                    this.TopicID,
                                    this.TopicName,
                                    this.UserID,
                                    this.UserName,
                                    forumPageAttributes,
                                    forumPageName),
                            EventLogTypes.Error);
                    }

                    outText.AppendFormat("{0}.", this.GetText("ACTIVELOCATION", "NODATA"));
            }

            output.Write(outputText);

            output.EndRender();
        }



        /// <summary>
        /// A method to get album path string.
        /// </summary>
        /// <param name="forumPageAttributes">
        /// A page query string cleared from page name.
        /// </param>
        /// <returns>
        /// The string
        /// </returns>
        private string Album([NotNull] string forumPageAttributes)
        {
            string outstring = string.Empty;
            string userId =
                forumPageAttributes.Substring(forumPageAttributes.IndexOf("u=", System.StringComparison.Ordinal) + 2)
                    .Trim();

            if (userId.Contains("&"))
            {
                userId = userId.Substring(0, userId.IndexOf("&", System.StringComparison.Ordinal)).Trim();
            }

            string albumId =
                forumPageAttributes.Substring(forumPageAttributes.IndexOf("a=", System.StringComparison.Ordinal) + 2);

            if (albumId.Contains("&"))
            {
                albumId = albumId.Substring(0, albumId.IndexOf("&", System.StringComparison.Ordinal)).Trim();
            }
            else
            {
                albumId = albumId.Substring(0).Trim();
            }

            if (ValidationHelper.IsValidInt(userId) && ValidationHelper.IsValidInt(albumId))
            {
                string albumName;

                // The DataRow should not be missing in the case
                DataRow dr =
                    CommonDb.album_list(PageContext.PageModuleID, null, Convert.ToInt32(albumId.Trim())).Rows[0];

                // If album doesn't have a Title, use his ID.
                if (!string.IsNullOrEmpty(dr["Title"].ToString()))
                {
                    albumName = dr["Title"].ToString();
                }
                else
                {
                    albumName = dr["AlbumID"].ToString();
                }

                // Render
                if (Convert.ToInt32(userId) != this.UserID)
                {
                    outstring += this.GetText("ACTIVELOCATION", "ALBUM").FormatWith();
                    outstring +=
                        @"<a href=""{0}"" id=""uiseralbumid_{1}"" runat=""server""> {2} </a>".FormatWith(
                            YafBuildLink.GetLink(ForumPages.album, "a={0}", albumId),
                            userId + this.PageContext.PageUserID,
                            HttpUtility.HtmlEncode(albumName));
                    outstring += this.GetText("ACTIVELOCATION", "ALBUM_OFUSER").FormatWith();
                    outstring +=
                        @"<a href=""{0}"" id=""albumuserid_{1}"" runat=""server""> {2} </a>".FormatWith(
                            YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId),
                            userId,
                            HttpUtility.HtmlEncode(UserMembershipHelper.GetUserNameFromID(Convert.ToInt64(userId))));
                }
                else
                {
                    outstring += this.GetText("ACTIVELOCATION", "ALBUM_OWN").FormatWith();
                    outstring +=
                        @"<a href=""{0}"" id=""uiseralbumid_{1}"" runat=""server""> {2} </a>".FormatWith(
                            YafBuildLink.GetLink(ForumPages.album, "a={0}", albumId),
                            userId + this.PageContext.PageUserID,
                            HttpUtility.HtmlEncode(albumName));
                }
            }
            else
            {
                outstring += this.GetText("ACTIVELOCATION", "ALBUM").FormatWith();
            }

            return outstring;
        }

        /// <summary>
        /// A method to get albums path string.
        /// </summary>
        /// <param name="forumPageAttributes">
        /// A page query string cleared from page name.
        /// </param>
        /// <returns>
        /// The string
        /// </returns>
        private string Albums([NotNull] string forumPageAttributes)
        {
            string outstring = string.Empty;

            string userId =
                forumPageAttributes.Substring(forumPageAttributes.IndexOf("u=", StringComparison.OrdinalIgnoreCase) + 2)
                    .Substring(0)
                    .Trim();

            if (ValidationHelper.IsValidInt(userId))
            {
                if (userId.ToType<int>() == this.UserID)
                {
                    outstring += this.GetText("ACTIVELOCATION", "ALBUMS_OWN").FormatWith();
                }
                else
                {
                    outstring += this.GetText("ACTIVELOCATION", "ALBUMS_OFUSER").FormatWith();
                    outstring +=
                        @"<a href=""{0}"" id=""albumsuserid_{1}"" runat=""server""> {2} </a>".FormatWith(
                            YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId),
                            userId + this.PageContext.PageUserID,
                            HttpUtility.HtmlEncode(UserMembershipHelper.GetUserNameFromID(Convert.ToInt64(userId))));
                }
            }
            else
            {
                outstring += this.GetTextFormatted("ACTIVELOCATION", "ALBUMS").FormatWith();
            }

            return outstring;
        }

        /// <summary>
        /// The profile.
        /// </summary>
        /// <param name="forumPageAttributes">
        /// The forum page attributes.
        /// </param>
        /// <returns>
        /// The profile.
        /// </returns>
        private string Profile([NotNull] string forumPageAttributes)
        {
            string outstring = string.Empty;
            string userId =
                forumPageAttributes.Substring(forumPageAttributes.IndexOf("u=", StringComparison.OrdinalIgnoreCase) + 2);

            userId = userId.Contains("&") ? 
                userId.Substring(0, userId.IndexOf("&", StringComparison.OrdinalIgnoreCase)).Trim() : 
                userId.Substring(0).Trim();

            if (ValidationHelper.IsValidInt(userId.Trim()))
            {
                if (userId.ToType<int>() != this.UserID)
                {
                    string dname =
                        HttpUtility.HtmlEncode(UserMembershipHelper.GetDisplayNameFromID(Convert.ToInt64(userId)));
                    if (dname.IsNotSet())
                    {
                        dname = HttpUtility.HtmlEncode(UserMembershipHelper.GetUserNameFromID(Convert.ToInt64(userId)));
                    }
                    outstring += this.GetText("ACTIVELOCATION", "PROFILE_OFUSER").FormatWith();
                    outstring +=
                        @"<a href=""{0}""  id=""profileuserid_{1}"" title=""{2}"" alt=""{2}"" runat=""server""> {3} </a>"
                            .FormatWith(
                                YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId),
                                userId + this.PageContext.PageUserID,
                                this.GetText("COMMON", "VIEW_USRPROFILE"),
                                HttpUtility.HtmlEncode(dname));
                }
                else
                {
                    outstring += this.GetText("ACTIVELOCATION", "PROFILE_OWN").FormatWith();
                }
            }
            else
            {
                outstring += this.GetText("ACTIVELOCATION", "PROFILE").FormatWith();
            }

            return outstring;
        }

        #endregion
    }
}