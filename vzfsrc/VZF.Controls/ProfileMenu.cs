#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File ProfileMenu.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

namespace VZF.Controls
{
    #region Using

    using System.Text;
    using System.Web.UI;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// User Profile Menu in the User Control Panel
    /// </summary>
    public class ProfileMenu : BaseControl
    {
        #region Methods

        /// <summary>
        /// Render the Profile Menu
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render([NotNull] HtmlTextWriter writer)
        {
            var html = new StringBuilder(2000);

            html.Append(@"<table cellspacing=""0"" cellpadding=""0"" class=""content"" id=""yafprofilemenu"">");

            // Render Mailbox Items
            if (this.Get<YafBoardSettings>().AllowPrivateMessages)
            {
                html.AppendFormat(@"<tr class=""header2""><td>{0}</td></tr>", this.GetText("MESSENGER"));

                html.AppendFormat(@"<tr><td class=""post""><ul id=""yafprofilemessenger"">");

                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{3}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_pm, "v=in"),
                    this.GetText("INBOX"),
                    ForumPages.cp_pm,
                    this.GetText("TOOLBAR", "INBOX_TITLE"));
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_pm, "v=out"),
                    this.GetText("SENTITEMS"),
                    ForumPages.cp_pm);
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_pm, "v=arch"),
                    this.GetText("ARCHIVE"), 
                    ForumPages.cp_pm);
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.pmessage),
                    this.GetText("NEW_MESSAGE"),
                    ForumPages.pmessage);

                html.AppendFormat(@"</ul></td></tr>");
            }

            // Render Personal Profile Items
            html.AppendFormat(@"<tr class=""header2""><td>{0}</td></tr>", this.GetText("PERSONAL_PROFILE"));
           
            html.AppendFormat(@"<tr><td class=""post""><ul id=""yafprofilepersonal"">");
            
            html.AppendFormat(
                @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                YafBuildLink.GetLink(ForumPages.profile, "u={0}", PageContext.PageUserID),
                this.GetText("VIEW_PROFILE"),
                    ForumPages.profile);
            html.AppendFormat(
                @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                YafBuildLink.GetLink(ForumPages.cp_editprofile),
                this.GetText("EDIT_PROFILE"),
                    ForumPages.cp_editprofile);

            if (!this.PageContext.IsGuest && this.Get<YafBoardSettings>().EnableThanksMod)
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.viewthanks, "u={0}", PageContext.PageUserID),
                    this.GetText("ViewTHANKS", "TITLE"),
                    ForumPages.viewthanks);
            }

            if (!this.PageContext.IsGuest
                && this.Get<YafBoardSettings>().EnableBuddyList & this.PageContext.UserHasBuddies)
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{3}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_editbuddies),
                    this.GetText("EDIT_BUDDIES"),
                    ForumPages.cp_editbuddies,
                    this.GetText("TOOLBAR", "BUDDIES_TITLE"));
            }

            if (!this.PageContext.IsGuest
                && (this.Get<YafBoardSettings>().EnableAlbum || (this.PageContext.NumAlbums > 0)))
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.albums, "u={0}", this.PageContext.PageUserID),
                    this.GetText("EDIT_ALBUMS"),
                    ForumPages.albums);
            }

            if (this.Get<YafBoardSettings>().AvatarRemote || this.Get<YafBoardSettings>().AvatarUpload
                || this.Get<YafBoardSettings>().AvatarGallery || this.Get<YafBoardSettings>().AvatarGravatar)
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_editavatar),
                    this.GetText("EDIT_AVATAR"),
                    ForumPages.cp_editavatar);
            }

            if (this.Get<YafBoardSettings>().AllowSignatures)
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_signature),
                    this.GetText("SIGNATURE"),
                    ForumPages.cp_signature);
            }

            html.AppendFormat(
                @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                YafBuildLink.GetLink(ForumPages.cp_subscriptions),
                this.GetText("SUBSCRIPTIONS"),
                    ForumPages.cp_subscriptions);

            if (this.Get<YafBoardSettings>().AllowPasswordChange)
            {
                html.AppendFormat(
                    @"<li class=""yafprofilemenu_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.cp_changepassword),
                    this.GetText("CHANGE_PASSWORD"),
                    ForumPages.cp_changepassword);
            }

            if (PageContext.UsrPersonalForums > 0)
            {
                // Render Personal Forum Items
                html.AppendFormat(@"<tr class=""header2""><td>{0}</td></tr>", this.GetText("PERSONAL_FORUMS"));

                html.AppendFormat(@"<tr><td class=""post""><ul id=""yafforumpersonal"">");

                html.AppendFormat(
                    @"<li class=""yafforumpersonal_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.personalforum, "u={0}".FormatWith(PageContext.PageUserID)),
                    this.GetText("EDIT_PERSONALFORUMS"),
                    ForumPages.personalforum);
            }

            if (PageContext.UsrPersonalMasks > 0)
            {
                // Render Personal Masks Items
                html.AppendFormat(
                    @"<li class=""yafmaskpersonal_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.personalaccessmask, "u={0}".FormatWith(PageContext.PageUserID)),
                    this.GetText("EDIT_PERSONALACCESSMASKS"),
                    ForumPages.personalaccessmask);
            }

            if (PageContext.UsrPersonalGroups > 0)
            {
                // Render Personal Masks Items
                html.AppendFormat(
                    @"<li class=""yafgrouppersonal_{2}""><a href=""{0}"" title=""{1}"">{1}</a></li>",
                    YafBuildLink.GetLink(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID)),
                    this.GetText("EDIT_PERSONALGROUPS"),
                    ForumPages.personalgroup);
            }



            html.AppendFormat(@"</ul></td></tr>");
            html.Append(@"</table>");

            writer.Write(html.ToString());
        }

        #endregion
    }
}