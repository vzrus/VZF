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
        ///   Gets Repuatation Progress Load Js.
        /// </summary>
        [NotNull]
        public static string RepuatationProgressLoadJs
        {
            get
            {
                return
                    @"{0}(document).ready(function() {{
                    {0}('.ReputationBar').progressbar({{
                        create: function(event, ui) {{
                                ChangeReputationBarColor({0}(this).attr('data-percent'),{0}(this).attr('data-text'), this);
                                }}
                     }});}});"
                        .FormatWith(Config.JQueryAlias);
            }
        }
    }
}