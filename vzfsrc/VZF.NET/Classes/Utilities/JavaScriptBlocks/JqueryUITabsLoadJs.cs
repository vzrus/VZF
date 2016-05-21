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
        /// Gets JqueryUITabsLoadJs.
        /// </summary>
        /// <param name="tabId">
        /// The tab Id.
        /// </param>
        /// <param name="hiddenId">
        /// The hidden Id.
        /// </param>
        /// <param name="hightTransition">
        /// Height Transition
        /// </param>
        /// <returns>
        /// The jquery ui tabs load js.
        /// </returns>
        public static string JqueryUITabsLoadJs(
            [NotNull] string tabId,
            [NotNull] string hiddenId,
            [NotNull] string hiddenTabId,
            bool hightTransition)
        {
            return JqueryUITabsLoadJs(tabId, hiddenId, hiddenTabId, string.Empty, hightTransition, true);
        }


        /// <summary>
        /// Gets JqueryUITabsLoadJs.
        /// </summary>
        /// <param name="tabId">
        /// The tab Id.
        /// </param>
        /// <param name="hiddenId">
        /// The hidden Id.
        /// </param>
        /// <param name="hiddenTabId">
        /// The hidden tab id.
        /// </param>
        /// <param name="postbackJs">
        /// The postback Js.
        /// </param>
        /// <param name="hightTransition">
        /// Height Transition
        /// </param>
        /// <param name="addSelectedFunction">
        /// The add Selected Function.
        /// </param>
        /// <returns>
        /// The jquery ui tabs load js.
        /// </returns>
        public static string JqueryUITabsLoadJs(
            [NotNull] string tabId,
            [NotNull] string hiddenId,
            [CanBeNull] string hiddenTabId,
            [CanBeNull] string postbackJs,
            bool hightTransition,
            bool addSelectedFunction)
        {
            string heightTransitionJs = hightTransition ? ", fx:{height:'toggle'}" : string.Empty;

            string selectFunctionJs = addSelectedFunction
                                          ? ", beforeActivate: function(event, ui) {{ {0}('#{1}').val(ui.newTab.index());{0}('#{2}').val(ui.newPanel.selector.replace('#', ''));{3} }}"
                                                .FormatWith(Config.JQueryAlias, hiddenId, hiddenTabId, postbackJs)
                                          : string.Empty;

            return @"{3}(document).ready(function() {{
					{3}('#{0}').tabs(
                    {{
            show: function() {{
                var sel = {3}('#{0}').tabs('option', 'active');

                {3}('#{1}').val(sel);
            }},
            active: {3}('#{1}').val()
            {2}
            {4}
        }});
                    }});".FormatWith(tabId, hiddenId, heightTransitionJs, Config.JQueryAlias, selectFunctionJs);
        }
    }
}