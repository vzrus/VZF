namespace YAF
{
  #region Using

  using System;
  using System.Web;
  using System.Web.UI;

  using VZF.Data.Common;

  using YAF.Core;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Optional forum page base providing some helper functions.
  /// </summary>
  public class ForumPageBase : Page, IHaveServiceLocator, IRequireStartupServices
  {
    #region Properties

    /// <summary>
    ///   Gets ServiceLocator.
    /// </summary>
    public IServiceLocator ServiceLocator
    {
      get
      {
        return YafContext.Current.ServiceLocator;
      }
    }

    public YafContext PageContext
    {
      get
      {
        return YafContext.Current;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The page_ error.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public void Page_Error([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (!this.Get<StartupInitializeDb>().Initialized)
      {
        return;
      }

      var error = this.Get<HttpServerUtilityBase>().GetLastError();
      CommonDb.eventlog_create(PageContext.PageModuleID, (int?)YafContext.Current.PageUserID, this, error);
    }

    #endregion
  }
}