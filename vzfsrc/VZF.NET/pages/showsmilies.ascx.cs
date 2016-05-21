namespace YAF.Pages
{
  #region Using

  using System;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The showsmilies.
  /// </summary>
  public partial class showsmilies : ForumPage
  {
    // constructor
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "showsmilies" /> class.
    /// </summary>
    public showsmilies()
      : base("SHOWSMILIES")
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get smiley script.
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <param name="icon">
    /// The icon.
    /// </param>
    /// <returns>
    /// The get smiley script.
    /// </returns>
    protected string GetSmileyScript([NotNull] string code, [NotNull] string icon)
    {
      code = code.ToLower();
      code = code.Replace("&", "&amp;");
      code = code.Replace("\"", "&quot;");
      code = code.Replace("'", "\\'");

      return "javascript:{0}('{1} ','{3}{4}/{2}');".FormatWith(
        "insertsmiley", code, icon, YafForumInfo.ForumClientFileRoot, YafBoardFolders.Current.Emoticons);
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
      this.ShowToolBar = false;
      this.ShowFooter = false;

      this.BindData();
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.List.DataSource = CommonDb.smiley_listunique(PageContext.PageModuleID, this.PageContext.PageBoardID);
      this.DataBind();
    }

    #endregion
  }
}