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

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {

        /// <summary>
        /// Gets Facebook Init Js.
        /// </summary>
        public static string FacebookInitJs
        {
            get
            {
                return
                    @"window.fbAsyncInit = function() {{
                       FB.init({{
                             appId: '{0}',
                             status: true,
                             cookie: true,
                             xfbml: true
                            }}); 
                     }};
                     (function(d){{
                                   var js, id = 'facebook-jssdk'; if (d.getElementById(id)) {{return;}}
                                   js = d.createElement('script'); js.id = id; js.async = true;
                                   js.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
                                   d.getElementsByTagName('head')[0].appendChild(js);
                     }}(document));".FormatWith(Config.FacebookApiKey, YafForumInfo.ForumBaseUrl);
            }
        }
    }
}