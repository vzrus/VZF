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
        /// Toggle Event Log Item Js Scripts
        /// used to show/hide event log item
        /// </summary>
        /// <param name="showText">The show text.</param>
        /// <param name="hideText">The hide text.</param>
        /// <returns>Toggle Event Log Item Js</returns>
        [NotNull]
        public static string ToggleEventLogItemJs([NotNull] string showText, [NotNull] string hideText)
        {
            return
                @"function toggleEventLogItem(detailId) {{
                           var show = '{1}';var hide = '{2}';
                           {0}('#Show'+ detailId).text({0}('#Show'+ detailId).text() == show ? hide : show);
                           {0}('#eventDetails' + detailId).slideToggle('slow'); return false;
                  }}"
                    .FormatWith(Config.JQueryAlias, showText, hideText);
        }
    }
}