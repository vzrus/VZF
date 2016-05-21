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
    public partial class mod_forumuser : ForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "mod_forumuser" /> class. 
        ///   Default constructor.
        /// </summary>
        public mod_forumuser()
            : base("MOD_FORUMUSER")
        {
        }

        #endregion

        private bool isPersonalForum = false;

        #region Public Methods

        /// <summary>
        /// The data bind.
        /// </summary>
        public override void DataBind()
        {
            // load data
            DataTable dt;

            if (!this.PageContext.ForumModeratorAccess)
            {
                var forumId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f");
                if (forumId != null)
                {
                    using (
                        var dt1 = CommonDb.forum_byuserlist(
                            PageContext.PageModuleID, PageContext.PageBoardID, forumId, PageContext.PageUserID, true))
                    {
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            this.isPersonalForum = true;
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

            bool isUserMask = false;
            bool isCommonMask = false;
            bool isAdminMask = false;
            int? userId = null;
            int flags = AccessFlags.Flags.None.ToInt();
            // setup datasource for access masks dropdown
            // only admin can assign all access masks
            if (!this.PageContext.IsAdmin && !this.isPersonalForum)
            {
                // do not include access masks with this flags set
                flags = (int)AccessFlags.Flags.ModeratorAccess;
                isAdminMask = true;
                isCommonMask = true;
                this.AccessMaskID.DataSource = CommonDb.accessmask_aforumlist(
           mid: PageContext.PageModuleID,
           boardId: this.PageContext.PageBoardID,
           accessMaskId: null,
           excludeFlags: 0,
           pageUserId: userId,
           isAdminMask: isAdminMask,
           isCommonMask: isCommonMask);
            }
            else
            {
                if (this.isPersonalForum)
                {

                    userId = this.PageContext.PageUserID;
                    isUserMask = true;
                    isCommonMask = !this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums;

                    this.AccessMaskID.DataSource = CommonDb.accessmask_pforumlist(
            mid: PageContext.PageModuleID,
            boardId: this.PageContext.PageBoardID,
            accessMaskId: null,
            excludeFlags: 0,
            pageUserId: userId,
            isUserMask: isUserMask,
            isCommonMask: isCommonMask);
                }
                else
                {
                    isUserMask = false;
                    isAdminMask = PageContext.IsAdmin;
                    isCommonMask = true;

                    this.AccessMaskID.DataSource = CommonDb.accessmask_aforumlist(
           mid: PageContext.PageModuleID,
           boardId: this.PageContext.PageBoardID,
           accessMaskId: null,
           excludeFlags: 0,
           pageUserId: userId,
           isAdminMask: isAdminMask,
           isCommonMask: isCommonMask);
                }
            }



            this.AccessMaskID.DataValueField = "AccessMaskID";
            this.AccessMaskID.DataTextField = "Name";

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
            YafBuildLink.Redirect(ForumPages.moderating, "f={0}", this.PageContext.PageForumID);
        }

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
            if (this.PageContext.Settings.LockedForum == 0)
            {
                // forum index
                this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

                // category
                this.PageLinks.AddLink(
                  this.PageContext.PageCategoryName,
                  YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
            }

            // forum page
            this.PageLinks.AddForumLinks(this.PageContext.PageForumID);

            // currect page
            this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);
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
            // only moderators/admins or personal forum owners are allowed in
            if (!this.PageContext.ForumModeratorAccess)
            {
                var forumId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f");
                if (forumId != null)
                {
                    using (
                        var dt = CommonDb.forum_byuserlist(
                            PageContext.PageModuleID, PageContext.PageBoardID, forumId, PageContext.PageUserID, true))
                    {
                        if (dt != null && dt.Rows.Count > 0)
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

            // do not repeat on postbact
            if (this.IsPostBack)
            {
                return;
            }

            // create page links
            this.CreatePageLinks();

            // load localized texts for buttons
            this.FindUsers.Text = this.GetText("FIND");
            this.Update.Text = this.GetText("UPDATE");
            this.Cancel.Text = this.GetText("CANCEL");

            // bind data
            this.DataBind();

            // if there is concrete user being handled
            if (this.Request.QueryString.GetFirstOrDefault("u") == null)
            {
                return;
            }

            using (
                DataTable dt = CommonDb.userforum_list(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("u"), this.PageContext.PageForumID))
            {
                foreach (DataRow row in dt.Rows)
                {
                    // set username and disable its editing
                    this.UserName.Text = PageContext.BoardSettings.EnableDisplayName ? row["DisplayName"].ToString() : row["Name"].ToString();
                    this.UserName.Enabled = false;

                    // we don't need to find users now
                    this.FindUsers.Visible = false;

                    // get access mask for this user                
                    if (this.AccessMaskID.Items.FindByValue(row["AccessMaskID"].ToString()) != null)
                    {
                        this.AccessMaskID.Items.FindByValue(row["AccessMaskID"].ToString()).Selected = true;
                    }
                }
            }
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

            if (UserMembershipHelper.IsGuestUser(userId))
            {
                this.PageContext.AddLoadMessage(this.GetText("NOT_GUEST"));
                return;
            }

            // save permission
            CommonDb.userforum_save(PageContext.PageModuleID, userId.Value, this.PageContext.PageForumID, this.AccessMaskID.SelectedValue);
            
            // clear moderators cache
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);
            this.Get<IDataCache>().Remove(Constants.Cache.BoardModerators);

            // redirect to forum moderation page
            YafBuildLink.Redirect(ForumPages.moderating, "f={0}", this.PageContext.PageForumID);
          
        }

        #endregion
    }
}