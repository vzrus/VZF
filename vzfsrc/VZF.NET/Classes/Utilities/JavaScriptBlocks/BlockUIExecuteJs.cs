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
        /// Requires {0} formatted elementId.
        /// </summary>
        /// <param name="elementId">
        /// The element Id.
        /// </param>
        /// <returns>
        /// The block ui execute js.
        /// </returns>
        public static string BlockUIExecuteJs([NotNull] string elementId)
        {
            return
                @"{1}(document).ready(function() {{ {1}.blockUI({{ message: {1}('#{0}') }}); }});".FormatWith(
                    elementId, Config.JQueryAlias);
        }
    }
}