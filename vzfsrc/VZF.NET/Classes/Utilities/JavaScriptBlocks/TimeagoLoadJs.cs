namespace VZF.Utilities
{
    #region Using

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Interfaces;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        ///   Gets TimeagoLoadJs.
        /// </summary>
        public static string TimeagoLoadJs
        {
            get
            {
                return
                  @"Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(loadTimeAgo);
            function loadTimeAgo() {{	
            {2}.timeago.settings.refreshMillis = {1};			      	
            {0}
              {2}('abbr.timeago').timeago();	
                  }}"
                        .FormatWith(
                            YafContext.Current.Get<ILocalization>().GetText("TIMEAGO_JS"),
                            YafContext.Current.Get<YafBoardSettings>().RelativeTimeRefreshTime,
                            Config.JQueryAlias);
            }
        }
    }
}