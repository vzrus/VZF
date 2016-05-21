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