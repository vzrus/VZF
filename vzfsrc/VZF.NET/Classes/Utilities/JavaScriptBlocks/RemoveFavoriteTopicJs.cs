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
        /// script for the remove Favorite Topic button
        /// </summary>
        /// <param name="tagButtonHTML">
        /// HTML code for the "Tag As a Favorite" button
        /// </param>
        /// <returns>
        /// The remove Favorite Topic js.
        /// </returns>
        public static string RemoveFavoriteTopicJs([NotNull] string tagButtonHTML)
        {
            return
                @"function removeFavoriteTopic(topicID){{ var topId = topicID;{2}.PageMethod('{1}YafAjax.asmx', 'RemoveFavoriteTopic', removeFavoriteTopicSuccess, CallFailed, 'topicId', topId);}}
          function removeFavoriteTopicSuccess(res){{if (res.d != null) {{
                   {2}('#dvFavorite1').html({0});
                   {2}('#dvFavorite2').html({0});}}}}"
                    .FormatWith(tagButtonHTML, YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
        }
    }
}