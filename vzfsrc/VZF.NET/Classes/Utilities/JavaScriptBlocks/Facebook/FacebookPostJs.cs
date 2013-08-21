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

    using YAF.Types;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        /// The Post to Facebook Js
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <param name="picture">
        /// The picture.
        /// </param>
        /// <param name="caption">
        /// The caption.
        /// </param>
        /// <returns>
        /// Returns the The Post to Facebook Js
        /// </returns>
        [NotNull]
        public static string FacebookPostJs(
            string message, string description, string link, string picture, string caption)
        {
            return
                @"function postToFacebook() {{

                   FB.getLoginStatus(function(response) {{
  if (response.status === 'connected') {{
    
    FB.ui(
                                {{ method: 'feed', name: '{0}', link: '{2}', picture: '{3}', caption: '{4}', description: '{1}', message: '{0}'
                                }},
                                function(response) {{
                                  if (response && response.post_id) {{
                                    alert('Post was published.');
                                  }} else {{
                                    alert('Post was not published.');
                                  }}
                                }});
  }} else {{
     FB.login(function(response) {{
                       if (response.authResponse) {{
                             FB.ui(
                                {{ method: 'feed', name: '{0}', link: '{2}', picture: '{3}', caption: '{4}', description: '{1}', message: '{0}'
                                }},
                                function(response) {{
                                  if (response && response.post_id) {{
                                    alert('Post was published.');
                                  }} else {{
                                    alert('Post was not published.');
                                  }}
                                }});
                             }}else {{
                                 alert('Not Logged in on Facebook!');
                                }}
                             }});
  }}
 }});

                         }}"
                    .FormatWith(message, description, link, picture, caption);
        }
    }
}