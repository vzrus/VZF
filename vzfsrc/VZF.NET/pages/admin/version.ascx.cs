namespace YAF.Pages.Admin
{
  #region Using

  using System;

  using YAF.Core;
  using RegisterV2;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for register.
  /// </summary>
  public partial class version : AdminPage
  {
    #region Constants and Fields

    /// <summary>
    ///   The _last version.
    /// </summary>
    private long _lastVersion;

    /// <summary>
    ///   The _last version date.
    /// </summary>
    private DateTime _lastVersionDate;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets LastVersion.
    /// </summary>
    protected string LastVersion
    {
      get
      {
        return YafForumInfo.AppVersionNameFromCode(this._lastVersion);
      }
    }

    /// <summary>
    ///   Gets LastVersionDate.
    /// </summary>
    protected string LastVersionDate
    {
      get
      {
        return this.Get<IDateTime>().FormatDateShort(this._lastVersionDate);
      }
    }

    #endregion

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
      if (!this.IsPostBack)
      {
        /*  using (var reg = new RegisterV2())
          {
              this._lastVersion = reg.LatestVersion();
              this._lastVersionDate = reg.LatestVersionDate();
          } */

          this.Upgrade.Visible = this._lastVersion > YafForumInfo.AppVersionCode;


        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
        this.PageLinks.AddLink(this.GetText("ADMIN_VERSION", "TITLE"), string.Empty);

          this.Page.Header.Title = "{0} - {1}".FormatWith(
              this.GetText("ADMIN_ADMIN", "Administration"), 
              this.GetText("ADMIN_VERSION", "TITLE"));

          this.RunningVersion.Text = this.GetTextFormatted(
              "RUNNING_VERSION",
              YafForumInfo.AppVersionName,
              this.Get<IDateTime>().FormatDateShort(YafForumInfo.AppVersionDate));

           this.LatestVersion.Text = this.GetTextFormatted(
             "LATEST_VERSION",
             this.LastVersion, 
             this.LastVersionDate);
      }

      this.DataBind();
    }

    #endregion
  }
}