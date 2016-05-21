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
  /// Summary description for ranks.
  /// </summary>
  public partial class nntpretrieve : AdminPage
  {
    #region Methods

    /// <summary>
    /// The last message no.
    /// </summary>
    /// <param name="_o">
    /// The _o.
    /// </param>
    /// <returns>
    /// The last message no.
    /// </returns>
    protected string LastMessageNo([NotNull] object _o)
    {
      var row = (DataRowView)_o;
      return "{0:N0}".FormatWith(row["LastMessageNo"]);
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
      this.PageLinks.AddLink(
        this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
      this.PageLinks.AddLink(this.GetText("ADMIN_NNTPRETRIEVE", "TITLE"), string.Empty);

      this.Page.Header.Title = "{0} - {1}".FormatWith(
        this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_NNTPRETRIEVE", "TITLE"));

      this.BindData();
    }

    
    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.List.DataSource = CommonDb.nntpforum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, PageContext.BoardSettings.NntpTopicProtectionPeriod, null, true);
      this.DataBind();
    }

    #endregion
  }
}