namespace VZF.Utilities
{
    #region Using

    using YAF.Types;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        ///   Gets the script for album/image title/image callback.
        /// </summary>
        /// <returns>
        ///   the callback success js.
        /// </returns>
        [NotNull]
        public static string AlbumCallbackSuccessJS
        {
            get
            {
                return
                    @"function changeTitleSuccess(res){{
                  spnTitleVar = document.getElementById('spnTitle' + res.d.Id);
                  txtTitleVar =  document.getElementById('txtTitle' + res.d.Id);
                  spnTitleVar.firstChild.nodeValue = res.d.NewTitle;
                  txtTitleVar.disabled = false;
                  spnTitleVar.style.display = 'inline';
                  txtTitleVar.style.display='none';}}";
            }
        }
    }
}