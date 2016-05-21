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
        /// Generates jQuery dropdown load js
        /// </summary>
        /// <param name="dropDownId">
        /// The drop Down client Id.
        /// </param>
        /// <returns>
        /// The load js.
        /// </returns>
        public static string DropDownLoadJs([NotNull] string dropDownId)
        {
            return @"Sys.Application.add_load(test);function test() {{ {0}('#{1}').msDropDown(); }} ".FormatWith(Config.JQueryAlias, dropDownId);
        }

    }
}