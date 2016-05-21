namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;
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
  /// Summary description for prune.
  /// </summary>
  public partial class pm : AdminPage
  {
    #region Methods

    /// <summary>
    /// The delete button_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DeleteButton_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_PM", "CONFIRM_DELETE"));
    }

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      this.commit.Click += this.commit_Click;
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
        if (this.IsPostBack)
        {
            return;
        }

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_PM", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
              this.GetText("ADMIN_ADMIN", "Administration"),
              this.GetText("ADMIN_PM", "TITLE"));

        this.commit.Text = this.GetText("COMMON", "DELETE");

        this.Days1.Text = "60";
        this.Days2.Text = "180";
        this.BindData();
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      using (DataTable dt = CommonDb.pmessage_info(PageContext.PageModuleID))
      {
        this.Count.Text = dt.Rows[0]["NumTotal"].ToString();
      }
    }

    /// <summary>
    /// The commit_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void commit_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      CommonDb.pmessage_prune(PageContext.PageModuleID, this.Days1.Text, this.Days2.Text);
      this.BindData();
    }

    #endregion
  }
}