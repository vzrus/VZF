namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;
  using System.Web;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for bannedip_edit.
  /// </summary>
  public partial class extensions_edit : AdminPage
  {
    #region Methods

    /// <summary>
    /// The is valid extension.
    /// </summary>
    /// <param name="newExtension">
    /// The new extension.
    /// </param>
    /// <returns>
    /// The is valid extension.
    /// </returns>
    protected bool IsValidExtension([NotNull] string newExtension)
    {
      if (newExtension.IsNotSet())
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EXTENSIONS_EDIT", "MSG_ENTER"));
        return false;
      }

      if (newExtension.IndexOf('.') != -1)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EXTENSIONS_EDIT", "MSG_REMOVE"));
        return false;
      }

      // TODO: maybe check for duplicate?
      return true;
    }

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      this.save.Click += this.Add_Click;
      this.cancel.Click += this.Cancel_Click;

      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      this.InitializeComponent();
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
      //string strAddEdit = (this.Request.QueryString.GetFirstOrDefault("i") == null) ? "Add" : "Edit";

        if (this.IsPostBack)
        {
            return;
        }

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_EXTENSIONS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_extensions));
        this.PageLinks.AddLink(this.GetText("ADMIN_EXTENSIONS_EDIT", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
            this.GetText("ADMIN_ADMIN", "Administration"),
            this.GetText("ADMIN_EXTENSIONS", "TITLE"),
            this.GetText("ADMIN_EXTENSIONS_EDIT", "TITLE"));

        this.save.Text = this.GetText("SAVE");
        this.cancel.Text = this.GetText("CANCEL");

        this.BindData();

        //this.extension.Attributes.Add("style", "width:250px");
    }

    /// <summary>
    /// The add_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Add_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      string ext = this.extension.Text.Trim();

      if (!this.IsValidExtension(ext))
      {
        this.BindData();
      }
      else
      {
        CommonDb.extension_save(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("i"), this.PageContext.PageBoardID, ext);
        YafBuildLink.Redirect(ForumPages.admin_extensions);
      }
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
        if (!this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i").IsSet())
        {
            return;
        }

        DataRow row =
            CommonDb.extension_edit(PageContext.PageModuleID, Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("i"))).Rows[0];
        this.extension.Text = (string)row["Extension"];
    }

    /// <summary>
    /// The cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_extensions);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    #endregion
  }
}