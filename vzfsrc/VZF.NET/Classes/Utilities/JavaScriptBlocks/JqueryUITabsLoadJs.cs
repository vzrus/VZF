/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

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