namespace VZF.Utilities
{
    #region Using

    using System;

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
        /// Generates Modal Dialog Script
        /// </summary>
        /// <param name="openLink">
        /// The Open Link, that opens the Modal Dialog.
        /// </param>
        /// <param name="dialogId">
        /// The Id or Css Class of the Dialog Content
        /// </param>
        /// <returns>
        /// The yaf modal dialog load js.
        /// </returns>
        public static string YafModalDialogLoadJs([NotNull] string openLink, [NotNull] string dialogId)
        {
            return
                @"{3}(document).ready(function() {{{3}('{0}').YafModalDialog({{Dialog : '{1}',ImagePath : '{2}'}}); }});"
                    .FormatWith(openLink, dialogId, YafForumInfo.GetURLToResource("images/"), Config.JQueryAlias);
        }
    }
}