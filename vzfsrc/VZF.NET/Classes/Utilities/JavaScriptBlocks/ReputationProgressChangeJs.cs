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
        /// The Reputation Progress Bar Change Js Code
        /// </summary>
        /// <param name="generateReputationBar">The generate reputation bar.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns the JS Code string</returns>
        [NotNull]
        public static string ReputationProgressChangeJs([NotNull] string generateReputationBar, [NotNull] string userId)
        {
            return
                @"{0}(document).ready(function() {{
                    {0}('.AddReputation_{1}').remove();
                    {0}('.RemoveReputation_{1}').remove();
                    {0}('.ReputationUser_{1}').replaceWith('{2}');
                    {0}('.ReputationBar').progressbar({{
                        create: function(event, ui) {{
                                ChangeReputationBarColor({0}(this).attr('data-percent'),{0}(this).attr('data-text'), this);
                                }}
                     }});}});"
                    .FormatWith(Config.JQueryAlias, userId, generateReputationBar);
        }
    }
}