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
        /// script for the addThanks button
        /// </summary>
        /// <param name="removeThankBoxHTML">
        /// HTML code for the "Remove Thank" button
        /// </param>
        /// <returns>
        /// The add thanks js.
        /// </returns>
        public static string AddThanksJs([NotNull] string removeThankBoxHTML)
        {
            return
                @"function addThanks(messageID){{ var messId = messageID;{2}.PageMethod('{1}YafAjax.asmx', 'AddThanks', addThanksSuccess, CallFailed, 'msgID', messId);}}
          function addThanksSuccess(res){{if (res.d != null) {{
                   {2}('#dvThanks' + res.d.MessageID).html(res.d.Thanks);
                   {2}('#dvThanksInfo' + res.d.MessageID).html(res.d.ThanksInfo);
                   {2}('#dvThankBox' + res.d.MessageID).html({0});}}}}"
                    .FormatWith(removeThankBoxHTML, YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
        }
    }
}