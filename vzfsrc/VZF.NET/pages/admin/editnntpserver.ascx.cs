namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for editgroup.
  /// </summary>
  public partial class editnntpserver : AdminPage
  {
    #region Methods

    /// <summary>
    /// The cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_nntpservers);
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
        this.PageLinks.AddLink(this.GetText("ADMIN_NNTPSERVERS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_nntpservers));
        this.PageLinks.AddLink(this.GetText("ADMIN_EDITNNTPSERVER", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
             this.GetText("ADMIN_ADMIN", "Administration"),
             this.GetText("ADMIN_NNTPSERVERS", "TITLE"),
             this.GetText("ADMIN_EDITNNTPSERVER", "TITLE"));

        this.Save.Text = this.GetText("COMMON", "SAVE");
        this.Cancel.Text = this.GetText("COMMON", "CANCEL");

        this.BindData();
        if (this.Request.QueryString.GetFirstOrDefault("s") != null)
        {
            using (
                DataTable dt = CommonDb.nntpserver_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.Request.QueryString.GetFirstOrDefault("s")))
            {
                DataRow row = dt.Rows[0];
                this.Name.Text = row["Name"].ToString();
                this.Address.Text = row["Address"].ToString();
                this.Port.Text = row["Port"].ToString();
                this.UserName.Text = row["UserName"].ToString();
                this.UserPass.Text = row["UserPass"].ToString();
            }
        }
        else
        {
            this.Port.Text = "119";
        }
    }

    /// <summary>
    /// The save_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (this.Name.Text.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPSERVER", "MSG_SERVER_NAME"));
        return;
      }

      if (this.Address.Text.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPSERVER", "MSG_SERVER_ADR"));
        return;
      }

      object nntpServerID = null;
      if (this.Request.QueryString.GetFirstOrDefault("s") != null)
      {
        nntpServerID = this.Request.QueryString.GetFirstOrDefault("s");
      }

      CommonDb.nntpserver_save(PageContext.PageModuleID, nntpServerID, 
        this.PageContext.PageBoardID, 
        this.Name.Text, 
        this.Address.Text, 
        this.Port.Text.Length > 0 ? this.Port.Text : null, 
        this.UserName.Text.Length > 0 ? this.UserName.Text : null, 
        this.UserPass.Text.Length > 0 ? this.UserPass.Text : null);
      YafBuildLink.Redirect(ForumPages.admin_nntpservers);
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.DataBind();
    }

    #endregion
  }
}