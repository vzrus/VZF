namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

    #endregion

  /// <summary>
  /// Summary description for ranks.
  /// </summary>
  public partial class nntpforums : AdminPage
  {
    #region Methods

    /// <summary>
    /// The delete_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_NNTPFORUMS", "DELETE_FORUM"));
    }

    /// <summary>
    /// The new forum_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void NewForum_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_editnntpforum);
    }

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
      if (!this.IsPostBack)
      {
        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));

        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_NNTPFORUMS", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
             this.GetText("ADMIN_ADMIN", "Administration"),
             this.GetText("ADMIN_NNTPFORUMS", "TITLE"));

        this.NewForum.Text = this.GetText("ADMIN_NNTPFORUMS", "NEW_FORUM");

        this.BindData();
      }
    }

    /// <summary>
    /// The rank list_ item command.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void RankList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "edit":
          YafBuildLink.Redirect(ForumPages.admin_editnntpforum, "s={0}", e.CommandArgument);
          break;
        case "delete":
          CommonDb.nntpforum_delete(PageContext.PageModuleID, e.CommandArgument);
          this.BindData();
          break;
      }
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.RankList.DataSource = CommonDb.nntpforum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null, null, DBNull.Value);
      this.DataBind();
    }

    #endregion
  }
}