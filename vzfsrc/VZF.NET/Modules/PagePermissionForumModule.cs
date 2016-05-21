namespace YAF.Modules
{
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Attributes;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Module that handles page permission feature
  /// </summary>
  [YafModule("Page Permission Module", "Tiny Gecko", 1)]
  public class PagePermissionForumModule : SimpleBaseForumModule
  {
    #region Constants and Fields

    /// <summary>
    /// The _permissions.
    /// </summary>
    private readonly IPermissions _permissions;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PagePermissionForumModule"/> class.
    /// </summary>
    /// <param name="permissions">
    /// The permissions.
    /// </param>
    public PagePermissionForumModule([NotNull] IPermissions permissions)
    {
      this._permissions = permissions;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The init after page.
    /// </summary>
    public override void InitAfterPage()
    {
      this.CurrentForumPage.Load += this.CurrentPage_Load;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The current page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void CurrentPage_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      // check access permissions for specific pages...
      switch (this.ForumPageType)
      {
        case ForumPages.activeusers:
          this._permissions.HandleRequest(this.PageContext.BoardSettings.ActiveUsersViewPermissions);
          break;
        case ForumPages.members:
          this._permissions.HandleRequest(this.PageContext.BoardSettings.MembersListViewPermissions);
          break;
        case ForumPages.profile:
        case ForumPages.albums:
        case ForumPages.album:
          this._permissions.HandleRequest(this.PageContext.BoardSettings.ProfileViewPermissions);
          break;
        case ForumPages.search:
          this._permissions.HandleRequest(
            this._permissions.Check(this.PageContext.BoardSettings.SearchPermissions)
              ? this.PageContext.BoardSettings.SearchPermissions
              : this.PageContext.BoardSettings.ExternalSearchPermissions);
          break;
        default:
          break;
      }
    }

    #endregion
  }
}