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
        /// Renders the Hover card load js.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        /// <param name="loadingHtml">The loading HTML.</param>
        /// <param name="errorHtml">The error HTML.</param>
        /// <returns>Returns the js String</returns>
        [NotNull]
        public static string HoverCardLoadJs(
            [NotNull] string clientId, [NotNull] string type, [NotNull] string loadingHtml, [NotNull] string errorHtml)
        {
            return
                "{0}('{1}').hovercard({{{2}width: 350,loadingHTML: '{3}',errorHTML: '{4}'}});".FormatWith(
                    Config.JQueryAlias,
                    clientId,
                    !string.IsNullOrEmpty(type) ? "show{0}Card: true,".FormatWith(type) : string.Empty,
                    loadingHtml,
                    errorHtml);
        }
    }
}