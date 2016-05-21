namespace YAF.pages
{
    using System;
    using System.Data;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The edit access mask.
    /// </summary>
    public partial class editaccessmask : ForumPage
    {
        #region Methods

        /// <summary>
        /// Cancel Edit and Return Back To Access Mask List Page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // get back to access masks administration
            YafBuildLink.Redirect(ForumPages.personalaccessmask, "u={0}".FormatWith(PageContext.PageUserID));
        }

        /// <summary>
        /// Creates navigation page links on top of forum (breadcrumbs).
        /// </summary>
        protected override void CreatePageLinks()
        {
            // beard index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

           // user profile
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName, YafBuildLink.GetLink(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID)));

            // personalaccessmask page
            this.PageLinks.AddLink(this.GetText("PERSONALACCESSMASK", "TITLE"), YafBuildLink.GetLink(ForumPages.personalaccessmask, "u={0}".FormatWith(PageContext.PageUserID)));

            // current page label (no link)
            this.PageLinks.AddLink(this.GetText("ADMIN_EDITACCESSMASKS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.Get<YafBoardSettings>().EnableDisplayName
                    ? this.PageContext.CurrentUserData.DisplayName
                    : this.PageContext.PageUserName,
                this.GetText("PERSONALACCESSMASK", "TITLE"),
                this.GetText("ADMIN_EDITACCESSMASKS", "TITLE"));
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }
             
            // A new mask case
            if (PageContext.PersonalAccessMasksNumber >= PageContext.UsrPersonalMasks && this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i") == null)
            {
                YafBuildLink.AccessDenied();
            }

            // the calling user is not the owner
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i") != null)
            {
                if (!ValidationHelper.IsValidInt(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i")))
                {
                    YafBuildLink.AccessDenied();
                }

                DataTable dt = CommonDb.accessmask_pforumlist(
                    this.PageContext.PageModuleID,
                    this.PageContext.PageBoardID,
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i").ToType<int>(),
                    0,
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>(),
                    true,
                    false 
                   );
                if (dt != null && dt.Rows.Count > 0)
                {
                }
                else
                {
                    YafBuildLink.AccessDenied();
                }
            }

            // create page links
            this.CreatePageLinks();

            // bind data
            this.BindData();
        }

        /// <summary>
        /// Saves The Access Mask
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // retrieve access mask ID from parameter (if applicable)
            object accessMaskId = null;
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                accessMaskId = this.Request.QueryString.GetFirstOrDefault("i");
            }

            if (this.Name.Text.Trim().Length <= 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITACCESSMASKS", "MSG_MASK_NAME"));
                return;
            }

            short sortOrder;

            if (!ValidationHelper.IsValidPosShort(this.SortOrder.Text.Trim()))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITACCESSMASKS", "MSG_POSITIVE_SORT"));
                return;
            }

            if (!short.TryParse(this.SortOrder.Text.Trim(), out sortOrder))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITACCESSMASKS", "MSG_NUMBER_SORT"));
                return;
            }

            // User mask is always an adminmask because it's not available to other users but owner
            CommonDb.accessmask_save(
                this.PageContext.PageModuleID,
                accessMaskId,
                this.PageContext.PageBoardID,
                this.Name.Text,
                this.ReadAccess.Checked,
                this.PostAccess.Checked,
                this.ReplyAccess.Checked,
                this.PriorityAccess.Checked,
                this.PollAccess.Checked,
                this.VoteAccess.Checked,
                this.ModeratorAccess.Checked,
                this.EditAccess.Checked,
                this.DeleteAccess.Checked,
                this.UploadAccess.Checked,
                this.DownloadAccess.Checked,
                this.UserForumAccess.Checked,
                sortOrder,
                PageContext.PageUserID,
                true,
                false);

            // empty out access table
            CommonDb.activeaccess_reset(PageContext.PageModuleID);

            // number of current masks changed
            // Clearing cache with old permissions data...
            this.Get<IDataCache>().Remove(k => k.StartsWith(Constants.Cache.ActiveUserLazyData.FormatWith(string.Empty)));

            // clear cache
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

            // get back to access masks administration
            YafBuildLink.Redirect(ForumPages.personalaccessmask, "u={0}".FormatWith(PageContext.PageUserID));
        }

        /* Methods */

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                // load access mask
                using (
                    var dt = CommonDb.accessmask_pforumlist(mid: this.PageContext.PageModuleID, boardId: this.PageContext.PageBoardID, accessMaskId: this.Request.QueryString.GetFirstOrDefault("i"), excludeFlags: 0, pageUserId: this.PageContext.PageUserID, isUserMask: true, isCommonMask: false))
                {
                    // we need just one
                    DataRow row = dt.Rows[0];

                    // get access mask properties
                    this.Name.Text = (string)row["Name"];
                    this.SortOrder.Text = row["SortOrder"].ToString();

                    // get flags
                    var flags = new AccessFlags(row["Flags"]);
                    this.ReadAccess.Checked = flags.ReadAccess;
                    this.PostAccess.Checked = flags.PostAccess;
                    this.ReplyAccess.Checked = flags.ReplyAccess;
                    this.PriorityAccess.Checked = flags.PriorityAccess;
                    this.PollAccess.Checked = flags.PollAccess;
                    this.VoteAccess.Checked = flags.VoteAccess;
                    this.ModeratorAccess.Checked = flags.ModeratorAccess;
                    this.EditAccess.Checked = flags.EditAccess;
                    this.DeleteAccess.Checked = flags.DeleteAccess;
                    this.UploadAccess.Checked = flags.UploadAccess;
                    this.DownloadAccess.Checked = flags.DownloadAccess;
                    this.UserForumAccess.Checked = flags.UserForumAccess;
                }
            }

            this.DataBind();
        }

        #endregion
    }
}