namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Core.BBCode;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

    #endregion

  /// <summary>
  /// Summary description for smilies.
  /// </summary>
  public partial class smilies : AdminPage
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
        ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_SMILIES", "MSG_DELETE"));
    }

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      this.Pager.PageChange += this.Pager_PageChange;
      this.List.ItemCommand += this.List_ItemCommand;

      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      this.InitializeComponent();
      base.OnInit(e);
    }

    /// <summary>
    /// Add Localized Text to Button
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void addLoad(object sender, EventArgs e)
    {
        var add = (Button)sender;
        add.Text = this.GetText("ADMIN_SMILIES", "ADD");
    }

    /// <summary>
    /// Add Localized Text to Button
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void importLoad(object sender, EventArgs e)
    {
        var import = (Button)sender;
        import.Text = this.GetText("ADMIN_SMILIES", "IMPORT");
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

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_SMILIES", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
            this.GetText("ADMIN_ADMIN", "Administration"),
            this.GetText("ADMIN_SMILIES", "TITLE"));

        this.BindData();
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.Pager.PageSize = 25;
      DataView dv = CommonDb.smiley_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null).DefaultView;
      this.Pager.Count = dv.Count;

      var pds = new PagedDataSource
          {
              DataSource = dv,
              AllowPaging = true,
              CurrentPageIndex = this.Pager.CurrentPageIndex,
              PageSize = this.Pager.PageSize
          };

        this.List.DataSource = pds;
      this.DataBind();
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
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
        case "add":
          YafBuildLink.Redirect(ForumPages.admin_smilies_edit);
          break;
        case "edit":
          YafBuildLink.Redirect(ForumPages.admin_smilies_edit, "s={0}", e.CommandArgument);
          break;
        case "moveup":
          CommonDb.smiley_resort(PageContext.PageModuleID, this.PageContext.PageBoardID, e.CommandArgument, -1);

          // invalidate the cache...
          this.Get<IDataCache>().Remove(Constants.Cache.Smilies);
          this.BindData();
          this.Get<IObjectStore>().RemoveOf<IProcessReplaceRules>();
          break;
        case "movedown":
          CommonDb.smiley_resort(PageContext.PageModuleID, this.PageContext.PageBoardID, e.CommandArgument, 1);

          // invalidate the cache...
          this.Get<IDataCache>().Remove(Constants.Cache.Smilies);
          this.BindData();
          this.Get<IObjectStore>().RemoveOf<IProcessReplaceRules>();
          break;
        case "delete":
          CommonDb.smiley_delete(PageContext.PageModuleID, e.CommandArgument);

          // invalidate the cache...
          this.Get<IDataCache>().Remove(Constants.Cache.Smilies);
          this.BindData();
          this.Get<IObjectStore>().RemoveOf<IProcessReplaceRules>();
          break;
        case "import":
          YafBuildLink.Redirect(ForumPages.admin_smilies_import);
          break;
      }
    }

    /// <summary>
    /// The pager_ page change.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.BindData();
    }

    #endregion
  }
}