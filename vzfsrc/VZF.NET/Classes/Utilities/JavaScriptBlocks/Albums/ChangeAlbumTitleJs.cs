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
        ///   Gets the script for changing the album title.
        /// </summary>
        /// <returns>
        ///   the change album title js.
        /// </returns>
        [NotNull]
        public static string ChangeAlbumTitleJs
        {
            get
            {
                return
                    @"function changeAlbumTitle(albumId, txtTitleId){{
                     var albId = albumId;var newTitleTxt = {1}('#' + txtTitleId).val();
                     {1}.PageMethod('{0}YafAjax.asmx', 'ChangeAlbumTitle', changeTitleSuccess, CallFailed, 'albumID', albId, 'newTitle', newTitleTxt);}}"
                        .FormatWith(YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
            }
        }

    }
}