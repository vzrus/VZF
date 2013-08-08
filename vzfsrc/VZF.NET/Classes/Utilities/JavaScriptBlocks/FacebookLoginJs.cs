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
        /// Gets the script for logging in thru facebook.
        /// </summary>
        /// <param name="remberMeId">
        /// The rember Me Checkbox Client Id.
        /// </param>
        /// <returns>
        /// The facebook login js.
        /// </returns>
        [NotNull]
        public static string FacebookLoginJs(string remberMeId)
        {
            return
                @"function LoginUser() {{

                    var Remember = {1}('#{2}').is(':checked');

                    FB.api('/me', function (response) {{
                    
                    {1}.PageMethod('{0}YafAjax.asmx', 'LoginFacebookUser', loginCallSuccess, LoginCallFailed,
                              'id', response.id,
                              'name', response.name,
                              'first_name', response.first_name,
                              'last_name', response.last_name,
                              'link', response.link,
                              'username', response.username === undefined ? response.name : response.username,
                              'birthday', response.birthday,
                              'hometown', response.hometown === undefined ? '' : response.hometown.name,
                              'gender', response.gender === undefined ? '0' : response.gender,
                              'email', response.email,
                              'timezone', response.timezone === undefined ? '' : response.timezone,
                              'locale', '',
                              'remember', Remember);
                     }});}}"
                    .FormatWith(YafForumInfo.ForumClientFileRoot, Config.JQueryAlias, remberMeId);
        }
    }
}