namespace YAF.Pages.Admin
{
  #region Using

  using System;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Handlers;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The runsql.
  /// </summary>
  public partial class runsql : AdminPage
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

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));

       this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_RUNSQL", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1}".FormatWith(
            this.GetText("ADMIN_ADMIN", "Administration"),
            this.GetText("ADMIN_RUNSQL", "TITLE"));

        this.chkRunInTransaction.Text = this.GetText("ADMIN_RUNSQL", "RUN_TRANSCATION");
        this.btnRunQuery.Text = this.GetText("ADMIN_RUNSQL", "RUN_QUERY");

        this.BindData();
    }

    /// <summary>
    /// The btn run query_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnRunQuery_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.txtResult.Text = string.Empty;
      this.ResultHolder.Visible = true;

      /*  using (var connMan = new MsSqlDbConnectionManager())
        {
          connMan.InfoMessage += this.connMan_InfoMessage;
          string sql = this.txtQuery.Text.Trim();

          // connMan.DBConnection.FireInfoMessageEventOnUserErrors = true;
          sql = sql.Replace("{databaseOwner}", Config.DatabaseOwner);
          sql = sql.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);

          this.txtResult.Text = CommonDb.db_runsql(YafContext.Current.PageModuleID, sql, connMan, this.chkRunInTransaction.Checked);
        } */
      this.txtResult.Text = CommonDb.db_runsql_new(PageContext.PageModuleID, this.txtQuery.Text.Trim(), this.chkRunInTransaction.Checked);
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.DataBind();
    }

    /// <summary>
    /// The conn man_ info message.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void connMan_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
    {
      this.txtResult.Text = "\r\n" + e.Message;
    }

    #endregion
  }
}