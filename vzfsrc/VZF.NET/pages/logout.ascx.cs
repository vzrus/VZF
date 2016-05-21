namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Web.Security;

  using YAF.Core;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.EventProxies;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for logout.
  /// </summary>
  public partial class logout : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "logout" /> class.
    /// </summary>
    public logout()
      : base("LOGOUT")
    {
      this.PageContext.Globals.IsSuspendCheckEnabled = false;
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
      FormsAuthentication.SignOut();

      this.Get<IRaiseEvent>().Raise(new UserLogoutEvent(this.PageContext.PageUserID));

      this.Session.Abandon();

      YafBuildLink.Redirect(ForumPages.forum);
    }

    #endregion
  }
}