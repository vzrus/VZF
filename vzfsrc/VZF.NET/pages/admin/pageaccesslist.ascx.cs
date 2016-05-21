namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Drawing;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

  #endregion

  /// <summary>
  /// Summary description for forums.
  /// </summary>
    public partial class pageaccesslist : AdminPage
  {
    /* Construction */
    #region Methods


    /// <summary>
    /// Creates navigation page links on top of forum (breadcrumbs).
    /// </summary>
    protected override void CreatePageLinks()
    {
      // board index
      this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));

      // administration index
      this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

      // current page label (no link)
      this.PageLinks.AddLink(this.GetText("ADMIN_PAGEACCESSLIST", "TITLE"), string.Empty);

      this.Page.Header.Title = "{0} - {1}".FormatWith(
         this.GetText("ADMIN_ADMIN", "Administration"),
         this.GetText("ADMIN_PAGEACCESSLIST", "TITLE"));
    }

    /* Event Handlers */

    /// <summary>
    /// The list_ item command.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void List_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "edit":

          // redirect to editing page
          YafBuildLink.Redirect(ForumPages.admin_pageaccessedit, "u={0}", e.CommandArgument);
          break;
      }
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
        if (this.IsPostBack)
        {
            return;
        }

        // create links
        this.CreatePageLinks();

        // bind data
        this.BindData();
    }

    /* Methods */

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      // list admins but not host admins
        this.List.DataSource = CommonDb.admin_pageaccesslist(PageContext.PageModuleID, null, true);
        this.DataBind();
    }

    #endregion
  }
}