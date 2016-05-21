namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Web.Security;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The Admin edit user page.
    /// </summary>
    public partial class edituser : AdminPage
    {
        #region Properties

        /// <summary>
        ///   Gets user ID of edited user.
        /// </summary>
        protected int CurrentUserID
        {
            get
            {
                return this.PageContext.QueryIDs["u"].ToType<int>();
            }
        }

        /// <summary>
        ///   Gets a value indicating whether Is Guest User.
        /// </summary>
        protected bool IsGuestUser
        {
            get
            {
                return UserMembershipHelper.IsGuestUser(this.CurrentUserID);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether [is user host admin]
        /// </summary>
        /// <param name="userRow">The user row.</param>
        /// <returns>
        /// The is user host admin.
        /// </returns>
        protected bool IsUserHostAdmin([NotNull] DataRow userRow)
        {
            var userFlags = new UserFlags(userRow["Flags"]);
            return userFlags.IsHostAdmin;
        }

        /// <summary>
        /// Registers the java scripts
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            // setup jQuery and Jquery Ui Tabs.
            YafContext.Current.PageElements.RegisterJQuery();
            YafContext.Current.PageElements.RegisterJQueryUI();

            YafContext.Current.PageElements.RegisterJsBlock(
                "EditUserTabsJs",
                JavaScriptBlocks.JqueryUITabsLoadJs(
                    this.EditUserTabs.ClientID,
                    this.hidLastTab.ClientID,
                    this.hidLastTabId.ClientID,
                    false));

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // we're in the admin section...
            this.ProfileEditControl.InAdminPages = true;
            this.SignatureEditControl.InAdminPages = true;
            this.AvatarEditControl.InAdminPages = true;

            this.PageContext.QueryIDs = new QueryStringIDHelper("u", true);

            DataTable dt = CommonDb.user_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.CurrentUserID, null);

            if (dt.Rows.Count != 1)
            {
                return;
            }

            DataRow userRow = dt.Rows[0];

            // do admin permission check...
            if (!this.PageContext.IsHostAdmin && this.IsUserHostAdmin(userRow))
            {
                // user is not host admin and is attempted to edit host admin account...
                YafBuildLink.AccessDenied();
            }

            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            this.PageLinks.AddLink(this.GetText("ADMIN_USERS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_users));

            // current page label (no link)
            this.PageLinks.AddLink(
                this.GetText("ADMIN_EDITUSER", "TITLE").FormatWith(
                    this.Get<YafBoardSettings>().EnableDisplayName
                        ? userRow["DisplayName"].ToString()
                        : userRow["Name"].ToString()),
                string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_USERS", "TITLE"),
                this.GetText("ADMIN_EDITUSER", "TITLE"));

            // do a quick user membership sync...
            MembershipUser user = UserMembershipHelper.GetMembershipUserById(this.CurrentUserID);

            // update if the user is not Guest
            if (!this.IsGuestUser)
            {
                RoleMembershipHelper.UpdateForumUser(user, this.PageContext.PageBoardID);
            }

            this.EditUserTabs.DataBind();
        }

        #endregion
    }
}