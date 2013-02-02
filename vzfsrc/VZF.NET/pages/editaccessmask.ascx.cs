namespace YAF.pages
{
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
    using YAF.Utils;
    using YAF.Utils.Helpers;

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
            YafBuildLink.Redirect(ForumPages.editaccessmask,"u={0}".FormatWith(PageContext.PageUserID));
        }

        /// <summary>
        /// Creates navigation page links on top of forum (breadcrumbs).
        /// </summary>
        protected override void CreatePageLinks()
        {
            // beard index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // administration index
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.personalforum, "u={0}".FormatWith(PageContext.PageUserID)));

            this.PageLinks.AddLink(
                this.GetText("ADMIN_ACCESSMASKS", "TITLE"), YafBuildLink.GetLink(ForumPages.editaccessmask, "u={0}".FormatWith(PageContext.PageUserID)));

            // current page label (no link)
            this.PageLinks.AddLink(this.GetText("ADMIN_EDITACCESSMASKS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_ACCESSMASKS", "TITLE"),
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

            if (this.Get<YafBoardSettings>().PersonalAccessMasksNumber <= 0 || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
            {
                YafBuildLink.AccessDenied();
            }

            this.Save.Text = this.GetText("COMMON", "SAVE");
            this.Cancel.Text = this.GetText("COMMON", "CANCEL");

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
            object accessMaskID = null;
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                accessMaskID = this.Request.QueryString.GetFirstOrDefault("i");
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
            CommonDb.accessmask_save(this.PageContext.PageModuleID,
                accessMaskID,
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
                true);

            // empty out access table
            CommonDb.activeaccess_reset(PageContext.PageModuleID);

            // clear cache
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

            // get back to access masks administration
            YafBuildLink.Redirect(ForumPages.personalforum, "u={0}".FormatWith(PageContext.PageUserID));
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
                    var dt = CommonDb.accessmask_list(mid: PageContext.PageModuleID, boardId: this.PageContext.PageBoardID, accessMaskID: this.Request.QueryString.GetFirstOrDefault("i"), excludeFlags: 0, pageUserID: this.PageContext.PageUserID, isUserMask: true, isAdminMask: false))
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