namespace YAF.Modules
{
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Attributes;
  using VZF.Utilities;

  #endregion

  /// <summary>
  /// The time ago module.
  /// </summary>
  [YafModule("Time Ago Javascript Loading Module", "Tiny Gecko", 1)]
  public class TimeAgoForumModule : SimpleBaseForumModule
  {
    #region Public Methods

    /// <summary>
    /// The init before page.
    /// </summary>
    public override void InitAfterPage()
    {
      this.CurrentForumPage.PreRender += this.CurrentForumPage_PreRender;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The current forum page_ pre render.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void CurrentForumPage_PreRender([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (this.PageContext.BoardSettings.ShowRelativeTime && !this.PageContext.Vars.ContainsKey("RegisteredTimeago"))
      {
        YafContext.Current.PageElements.RegisterJsResourceInclude("timeagojs", "js/jquery.timeago.js");
        YafContext.Current.PageElements.RegisterJsBlockStartup("timeagoloadjs", JavaScriptBlocks.TimeagoLoadJs);
        this.PageContext.Vars["RegisteredTimeago"] = true;
      }
    }

    #endregion
  }
}