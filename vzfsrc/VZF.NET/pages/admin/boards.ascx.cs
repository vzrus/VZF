using VZF.Controls;

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
  /// Summary description for members.
  /// </summary>
  public partial class boards : AdminPage
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
        ((ThemeButton)sender).Attributes["onclick"] =
               "return (confirm('{0}') && confirm('{1}'))".FormatWith(
                   this.GetText("ADMIN_BOARDS", "CONFIRM_DELETE"),
                   this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_POSITIVE"));}

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      this.List.ItemCommand += this.List_ItemCommand;
      this.New.Click += this.New_Click;

      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      base.OnInit(e);
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
      if (!this.PageContext.IsHostAdmin)
      {
        YafBuildLink.AccessDenied();
      }

        if (this.IsPostBack)
        {
            return;
        }

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_BOARDS", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
              this.GetText("ADMIN_ADMIN", "Administration"),
              this.GetText("ADMIN_BOARDS", "TITLE"));

        this.New.Text = this.GetText("ADMIN_BOARDS", "NEW_BOARD");

        this.BindData();
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.List.DataSource = CommonDb.board_list(PageContext.PageModuleID, null);
      this.DataBind();
    }

    /// <summary>
    /// The list_ item command.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void List_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "edit":
          YafBuildLink.Redirect(ForumPages.admin_editboard, "b={0}", e.CommandArgument);
          break;
        case "delete":
          CommonDb.board_delete(PageContext.PageModuleID, e.CommandArgument);
          this.BindData();
          break;
      }
    }

    /// <summary>
    /// The new_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void New_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_editboard);
    }

    #endregion
  }
}