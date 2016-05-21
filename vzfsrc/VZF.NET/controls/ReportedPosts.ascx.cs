namespace VZF.Controls
{
  #region Using

  using System;

  using VZF.Data.Common;

  
  using YAF.Types;

  #endregion

  /// <summary>
  /// The reported posts.
  /// </summary>
  public partial class ReportedPosts : BaseReportedPosts
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

        this.ReportedPostsRepeater.DataSource = CommonDb.message_listreporters((int?) PageContext.PageModuleID, this.MessageID);
        this.ReportedPostsRepeater.DataBind();
    }

    #endregion
  }
}