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
        ///   Gets ToggleMessageJs.
        /// </summary>
        [NotNull]
        public static string ToggleMessageJs
        {
            get
            {
                return
                    @"
                      function toggleMessage(divId)
                      {{ {0}('#' + divId).toggle(); }}"
                        .FormatWith(Config.JQueryAlias);
            }
        }
    }
}