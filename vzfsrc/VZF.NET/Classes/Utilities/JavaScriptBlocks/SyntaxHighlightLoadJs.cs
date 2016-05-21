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
        ///   Gets SyntaxHighlightLoadJs.
        /// </summary>
        [NotNull]
        public static string SyntaxHighlightLoadJs
        {
            get
            {
                return
                    @"{0}(document).ready(function() {{
                    SyntaxHighlighter.all()}});".FormatWith(
                        Config.JQueryAlias);
            }
        }
    }
}