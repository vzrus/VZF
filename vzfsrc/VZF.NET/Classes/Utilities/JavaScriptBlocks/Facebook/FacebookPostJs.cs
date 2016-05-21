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