namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Handlers;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The reindex.
  /// </summary>
  public partial class reindex : AdminPage
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

        // Check and see if it should make panels enable or not
        this.PanelReindex.Visible = CommonDb.GetPanelReindex(YafContext.Current.PageModuleID);
        this.PanelShrink.Visible = CommonDb.GetPanelShrink(YafContext.Current.PageModuleID);
        this.PanelRecoveryMode.Visible = CommonDb.GetPanelRecoveryMode(YafContext.Current.PageModuleID);
        this.PanelGetStats.Visible = CommonDb.GetPanelGetStats(YafContext.Current.PageModuleID);

        // Get the name of buttons
        this.btnReindex.Text = this.GetText("ADMIN_REINDEX", "REINDEXTBL_BTN");
        this.btnGetStats.Text = this.GetText("ADMIN_REINDEX", "TBLINDEXSTATS_BTN");
        this.btnShrink.Text = this.GetText("ADMIN_REINDEX", "SHRINK_BTN");
        this.btnRecoveryMode.Text = this.GetText("ADMIN_REINDEX", "SETRECOVERY_BTN");

        this.btnShrink.OnClientClick =
            "return confirm('{0}');".FormatWith(this.GetText("ADMIN_REINDEX", "CONFIRM_SHRINK"));
        
        this.btnReindex.OnClientClick =
            "return confirm('{0}');".FormatWith(this.GetText("ADMIN_REINDEX", "CONFIRM_REINDEX"));
        
        this.btnRecoveryMode.OnClientClick =
            "return confirm('{0}');".FormatWith(this.GetText("ADMIN_REINDEX", "CONFIRM_RECOVERY"));

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_REINDEX", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
              this.GetText("ADMIN_ADMIN", "Administration"),
              this.GetText("ADMIN_REINDEX", "TITLE"));

        this.RadioButtonList1.Items.Add(new ListItem(this.GetText("ADMIN_REINDEX", "RECOVERY1")));
        this.RadioButtonList1.Items.Add(new ListItem(this.GetText("ADMIN_REINDEX", "RECOVERY2")));
        this.RadioButtonList1.Items.Add(new ListItem(this.GetText("ADMIN_REINDEX", "RECOVERY3")));

        this.RadioButtonList1.SelectedIndex = 0;

        this.BindData();
    }

    /// <summary>
    /// The btn get stats_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnGetStats_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
        this.txtIndexStatistics.Text = CommonDb.db_getstats_warning(PageContext.PageModuleID) + "\r\n{0}".FormatWith(CommonDb.db_getstats_new(PageContext.PageModuleID));
    }

    /// <summary>
    /// The btn recovery mode_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnRecoveryMode_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
        string dbRecoveryMode = string.Empty;
        switch (this.RadioButtonList1.SelectedIndex)
        {
            case 0: dbRecoveryMode = "FULL"; break;
            case 1: dbRecoveryMode = "SIMPLE"; break;
            case 2: dbRecoveryMode = "BULK_LOGGED"; break;
        }
        string error = CommonDb.db_recovery_mode_new(PageContext.PageModuleID, dbRecoveryMode);
        if (error.IsSet())
        {
            this.txtIndexStatistics.Text = CommonDb.db_recovery_mode_warning(PageContext.PageModuleID) + this.GetText("ADMIN_REINDEX", "INDEX_STATS_FAIL").FormatWith(error);
        }
        else
        {
            this.txtIndexStatistics.Text = this.GetText("ADMIN_REINDEX", "INDEX_STATS").FormatWith(dbRecoveryMode);
            this.txtIndexStatistics.Text = CommonDb.db_recovery_mode_warning(PageContext.PageModuleID) + "\r\n{0}".FormatWith(CommonDb.db_recovery_mode_new(PageContext.PageModuleID, dbRecoveryMode));
        }
    }

    /// <summary>
    /// Reindexing Database
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnReindex_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
        /* using (var connMan = new MsSqlDbConnectionManager())
         {
           connMan.InfoMessage += this.connMan_InfoMessage;
           this.txtIndexStatistics.Text = CommonDb.db_reindex_warning(YafContext.Current.PageModuleID);
           CommonDb.db_reindex(connMan);
         } */

        this.txtIndexStatistics.Text = CommonDb.db_reindex_warning(PageContext.PageModuleID) + CommonDb.db_reindex_new(PageContext.PageModuleID);
    }

    /// <summary>
    /// Mod By Touradg (herman_herman) 2009/10/19
    /// Shrinking Database
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnShrink_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
        try
        {
            this.txtIndexStatistics.Text = CommonDb.db_shrink_warning(PageContext.PageModuleID) + @"\r\n\{0}\r\n\".FormatWith(CommonDb.db_shrink_new(PageContext.PageModuleID));
            this.txtIndexStatistics.Text = this.GetText("ADMIN_REINDEX", "INDEX_SHRINK").FormatWith(CommonDb.GetDbSize(PageContext.PageModuleID));
        }
        catch (Exception error)
        {
            this.txtIndexStatistics.Text += this.GetText("ADMIN_REINDEX", "INDEX_STATS_FAIL").FormatWith(error.Message);
        }
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