namespace VZF.Utilities
{
    #region Using

    using YAF.Classes;
    using YAF.Types;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        /// Renders the Hover card load js.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        /// <param name="loadingHtml">The loading HTML.</param>
        /// <param name="errorHtml">The error HTML.</param>
        /// <returns>Returns the js String</returns>
        [NotNull]
        public static string HoverCardLoadJs(
            [NotNull] string clientId, [NotNull] string type, [NotNull] string loadingHtml, [NotNull] string errorHtml)
        {
            return
                "{0}('{1}').hovercard({{{2}width: 350,loadingHTML: '{3}',errorHTML: '{4}'}});".FormatWith(
                    Config.JQueryAlias,
                    clientId,
                    !string.IsNullOrEmpty(type) ? "show{0}Card: true,".FormatWith(type) : string.Empty,
                    loadingHtml,
                    errorHtml);
        }
    }
}