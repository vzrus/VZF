
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
        /// Generated the load Script for the Spinner Widget
        /// </summary>
        /// <returns>
        /// Returns the Java Script that loads table Sorter
        /// </returns>
        public static string LoadSpinnerWidget()
        {
            return @"{0}(document).ready(function() {{
                        if (typeof (jQuery.fn.spinner) !== 'undefined') {{
                            {0}('.Numeric').spinner();
                        }}
                    }});".FormatWith(Config.JQueryAlias);
        }
    }
}