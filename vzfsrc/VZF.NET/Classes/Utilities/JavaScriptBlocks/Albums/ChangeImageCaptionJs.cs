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
        ///   Gets the script for changing the image caption.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public static string ChangeImageCaptionJs
        {
            get
            {
                return
                    @"function changeImageCaption(imageID, txtTitleId){{
              var imgId = imageID;var newImgTitleTxt = {1}('#' + txtTitleId).val();
              {1}.PageMethod('{0}YafAjax.asmx', 'ChangeImageCaption', changeTitleSuccess, CallFailed, 'imageID', imgId, 'newCaption', newImgTitleTxt);}}"
                        .FormatWith(YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
            }
        }
    }
}