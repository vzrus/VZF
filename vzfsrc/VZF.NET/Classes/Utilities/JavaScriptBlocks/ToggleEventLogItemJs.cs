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