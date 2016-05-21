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
        /// script for the add Favorite Topic button
        /// </summary>
        /// <param name="untagButtonHTML">
        /// HTML code for the "Untag As Favorite" button
        /// </param>
        /// <returns>
        /// The add Favorite Topic js.
        /// </returns>
        public static string AddFavoriteTopicJs([NotNull] string untagButtonHTML)
        {
            return
                @"function addFavoriteTopic(topicID){{ var topId = topicID; {2}.PageMethod('{1}YafAjax.asmx', 'AddFavoriteTopic', addFavoriteTopicSuccess, CallFailed, 'topicId', topId);}}
          function addFavoriteTopicSuccess(res){{if (res.d != null) {{
                   {2}('#dvFavorite1').html({0});
                   {2}('#dvFavorite2').html({0});}}}}"
                    .FormatWith(untagButtonHTML, YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
        }
    }
}