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