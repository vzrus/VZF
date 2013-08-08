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
    using YAF.Types.Constants;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        ///   Gets the script for login callback.
        /// </summary>
        /// <returns>
        ///   the callback success js.
        /// </returns>
        [NotNull]
        public static string LoginCallSuccessJS
        {
            get
            {
                return
                    @"function loginCallSuccess(res){{
                    if (res.d != null && res.d == ""OK"") {{
                    window.location = ""{0}"";
                    }}
                    else {{
                      // Show MessageBox
                      {1}('span[id$=_YafPopupErrorMessageInner]').html(res.d);
                      {1}().YafModalDialog.Show({{Dialog : '#' + {1}('div[id$=_YafForumPageErrorPopup1]').attr('id'),ImagePath : '{2}resources/images/'}});
                    }} }}"
                        .FormatWith(YafBuildLink.GetLink(ForumPages.forum), Config.JQueryAlias, YafForumInfo.ForumClientFileRoot);
            }
        }  
    }
}