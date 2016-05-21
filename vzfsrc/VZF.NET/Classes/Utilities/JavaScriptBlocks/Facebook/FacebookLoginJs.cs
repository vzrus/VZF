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