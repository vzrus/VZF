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