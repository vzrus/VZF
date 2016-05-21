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
        /// script for the removeThanks button
        /// </summary>
        /// <param name="addThankBoxHTML">
        /// The Add Thank Box HTML.
        /// </param>
        /// <returns>
        /// The remove thanks js.
        /// </returns>
        public static string RemoveThanksJs([NotNull] string addThankBoxHTML)
        {
            return
                @"function removeThanks(messageID){{ var messId = messageID;{2}.PageMethod('{1}YafAjax.asmx', 'RemoveThanks', removeThanksSuccess, CallFailed, 'msgID', messId);}}
          function removeThanksSuccess(res){{if (res.d != null) {{
                   {2}('#dvThanks' + res.d.MessageID).html(res.d.Thanks);
                   {2}('#dvThanksInfo' + res.d.MessageID).html(res.d.ThanksInfo);
                   {2}('#dvThankBox' + res.d.MessageID).html({0});}}}}"
                    .FormatWith(addThankBoxHTML, YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
        }
    }
}