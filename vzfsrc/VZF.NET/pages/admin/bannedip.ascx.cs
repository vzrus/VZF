namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The Admin Banned Ip Page.
    /// </summary>
    public partial class bannedip : AdminPage
    {
        #region Methods

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            this.PageLinks.AddLink(this.GetText("ADMIN_BANNEDIP", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_BANNEDIP", "TITLE"));

            this.BindData();
        }

        /// <summary>
        /// Adds text to the Add Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Add_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var addButton = (Button)sender;

            addButton.Text = addButton.ToolTip = this.GetText("ADMIN_BANNEDIP", "ADD_IP");
        }

        /// <summary>
        /// Adds text to the Import Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Import_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var importButton = (Button)sender;

            importButton.Text = importButton.ToolTip = this.GetText("ADMIN_BANNEDIP", "IMPORT_IPS");
        }

        /// <summary>
        /// The list_ item command.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void List_ItemCommand([NotNull] object sender, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "import":
                    YafBuildLink.Redirect(ForumPages.admin_bannedip_import);
                    break;
                case "add":
                    YafBuildLink.Redirect(ForumPages.admin_bannedip_edit);
                    break;
                case "edit":
                    YafBuildLink.Redirect(ForumPages.admin_bannedip_edit, "i={0}", e.CommandArgument);
                    break;
                case "delete":
                    string ip = this.GetIPFromID(e.CommandArgument);
                    CommonDb.bannedip_delete(PageContext.PageModuleID, e.CommandArgument);
                    this.Get<IDataCache>().Remove(Constants.Cache.BannedIP);
                    this.BindData();
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_BANNEDIP", "MSG_REMOVEBAN"));
                    this.Get<ILogger>().IpBanLifted(
                        this.PageContext.PageUserID,
                        " YAF.Pages.Admin.bannedip",
                        "IP or mask {0} was deleted by {1}.".FormatWith(
                            ip,
                            this.Get<YafBoardSettings>().EnableDisplayName
                                ? this.PageContext.CurrentUserData.DisplayName
                                : this.PageContext.CurrentUserData.UserName));
                    break;
            }
        }

        /// <summary>
        /// The pager top_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PagerTop_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            // rebind
            this.BindData();
        }

        /// <summary>
        /// Helper to get mask from ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>
        /// Returns the IP
        /// </returns>
        private string GetIPFromID(object id)
        {
            return (from RepeaterItem ri in this.list.Items
                    let chid = ((Label)ri.FindControl("MaskBox")).Text
                    let fid = ((HiddenField)ri.FindControl("fID")).Value
                    where id.ToString() == fid
                    select chid).FirstOrDefault();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.PagerTop.PageSize = this.Get<YafBoardSettings>().MemberListPageSize;
            var dt = CommonDb.bannedip_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);
            this.list.DataSource = dt;

            this.PagerTop.Count = dt != null && dt.Rows.Count > 0
                                      ? dt.AsEnumerable().First().Field<int>("TotalRows")
                                      : 0;

            this.DataBind();
        }

        #endregion
    }
}