// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="personalgroup_edituser.ascx.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2016 Vladimir Zakharov
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
//   The edit users in a personal group.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace YAF.Pages
{
    // YAF.Pages
    #region Using

    using System;
    using System.Data;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Control handling user invitations to forum (i.e. granting permissions by admin/moderator).
    /// </summary>
    public partial class personalgroup_edituser : ForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "personalgroup_edituser" /> class. 
        ///   Default constructor.
        /// </summary>
        public personalgroup_edituser()
            : base("PERSONALGROUP_EDITUSER")
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the user group.
        /// </summary>
        public int UserGroup { get; set; }

        #region Public Methods

        /// <summary>
        /// The data bind.
        /// </summary>
        public override void DataBind()
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                var forumId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr");
                if (forumId != null)
                {
                    using (
                        var dt1 = CommonDb.group_byuserlist(
                            PageContext.PageModuleID, PageContext.PageBoardID, forumId, PageContext.PageUserID, true))
                    {
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            YafBuildLink.AccessDenied();
                        }
                    }
                }
                else
                {
                    YafBuildLink.AccessDenied();
                }
            }
            base.DataBind();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles click event of cancel button.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // redirect to forum moderation page
            YafBuildLink.Redirect(ForumPages.personalgroup_users, "gr={0}", this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int>());
        }

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
           // forum index
              this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

           // profile
              this.PageLinks.AddLink(this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName, YafBuildLink.GetLink(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID))); ;
           
           // personal groups
              this.PageLinks.AddLink(this.GetText("PERSONALGROUP", "TITLE"), YafBuildLink.GetLink(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID)));
           string groupName = null;

           DataTable dtGroup = CommonDb.group_byuserlist(PageContext.PageModuleID, this.PageContext.PageBoardID, this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr"), PageContext.PageUserID, true);
           if (dtGroup != null && dtGroup.Rows.Count > 0)
           {
               groupName = dtGroup.Rows[0]["Name"].ToString();
           }

            // currect page
            this.PageLinks.AddLink(this.GetTextFormatted("TITLE", groupName), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
              this.Get<YafBoardSettings>().EnableDisplayName
                  ? this.PageContext.CurrentUserData.DisplayName
                  : this.PageContext.PageUserName,
              this.GetText("PERSONALGROUP", "TITLE"),
              this.GetText("PERSONALGROUP_EDIT", "TITLE"));
        }

        /// <summary>
        /// Handles FindUsers button click event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void FindUsers_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // we need at least two characters to search user by
            if (this.UserName.Text.Length < 2)
            {
                return;
            }

            // get found users
            var foundUsers = this.Get<IUserDisplayName>().Find(this.UserName.Text.Trim());

            // have we found anyone?
            if (foundUsers.Count > 0)
            {
                // set and enable user dropdown, disable text box
                this.ToList.DataSource = foundUsers;
                this.ToList.DataValueField = "Key";
                this.ToList.DataTextField = "Value";

                // ToList.SelectedIndex = 0;
                this.ToList.Visible = true;
                this.UserName.Visible = false;
                this.FindUsers.Visible = false;
            }

            // bind data (is this necessary?)
            base.DataBind();
        }

        /// <summary>
        /// Handles page load event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                int groupId;
                if (int.TryParse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr"), out groupId))
                {
                    using (
                        var dt1 = CommonDb.group_byuserlist(
                            PageContext.PageModuleID, PageContext.PageBoardID, groupId, PageContext.PageUserID, true))
                    {
                        if (dt1 != null && dt1.Rows.Count > 0 && (dt1.Rows[0]["CreatedByUserID"].ToType<int>() == PageContext.PageUserID || PageContext.IsAdmin))
                        {
                            // The page user has right to access it
                        }
                        else
                        {
                            YafBuildLink.AccessDenied();
                        }
                    }
                }
                else
                {
                    YafBuildLink.AccessDenied();
                }
            }

            // do not repeat on postbact
            if (this.IsPostBack)
            {
                return;
            }

            // get user roles
            // this.UserGroup = CommonDb.group_member(PageContext.PageModuleID, this.PageContext.PageBoardID, this.FindUsers.Text);
            
            // create page links
            this.CreatePageLinks();

            // bind data
            this.DataBind();

            // if there is concrete user being handled
            if (this.Request.QueryString.GetFirstOrDefault("gr") == null)
            {
                return;
            }

           /* using (
                DataTable dt = CommonDb.userforum_list(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("u"), this.PageContext.PageForumID))
            {
                foreach (DataRow row in dt.Rows)
                {
                    // set username and disable its editing
                    this.UserName.Text = PageContext.BoardSettings.EnableDisplayName ? row["DisplayName"].ToString() : row["Name"].ToString();
                    this.UserName.Enabled = false;

                    // we don't need to find users now
                    this.FindUsers.Visible = false;
                }
            } */
        }

        /// <summary>
        /// Handles click event of Update button.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Update_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // no user was specified
            if (this.UserName.Text.Length <= 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("NO_SUCH_USER"));
                return;
            }

            // if we choose user from drop down, set selected value to text box
            if (this.ToList.Visible)
            {
                this.UserName.Text = this.ToList.SelectedItem.Text;
            }

            // we need to verify user exists
            var userId = this.Get<IUserDisplayName>().GetId(this.UserName.Text.Trim());
          
            // there is no such user or reference is ambiugous
            if (!userId.HasValue)
            {
                this.PageContext.AddLoadMessage(this.GetText("NO_SUCH_USER"));
                return;
            }

            // save user in role
            CommonDb.usergroup_save(PageContext.PageModuleID, userId, this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int>(),true);

            // empty out access table
            CommonDb.activeaccess_reset(PageContext.PageModuleID);

            // update roles if this user isn't the guest
            if (!UserMembershipHelper.IsGuestUser(userId))
            {
                // get user's name
                string userName = UserMembershipHelper.GetUserNameFromID((long)userId);

                // get role name
                string roleName = string.Empty;
                using (
                    DataTable dt = CommonDb.group_list(
                        this.PageContext.PageModuleID,
                        this.PageContext.PageBoardID,
                        this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int>(), 0, 1000000))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        roleName = (string)row["Name"];
                    }
                }

                // add/remove user from roles in membership provider
                if (!RoleMembershipHelper.IsUserInRole(userName, roleName))
                {
                    RoleMembershipHelper.AddUserToRole(userName, roleName);
                }

                // Clearing cache with old permisssions data...
                this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(userId));

                // clear moderators cache
                this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);
                this.Get<IDataCache>().Remove(Constants.Cache.BoardModerators);
            }

            // redirect to forum moderation page
            YafBuildLink.Redirect(ForumPages.personalgroup_users, "gr={0}", this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int>());
        }

        #endregion
    }
}