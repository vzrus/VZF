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
        /// Gets the multi quote button js.
        /// </summary>
        [NotNull]
        public static string MultiQuoteButtonJs
        {
            get
            {
                return
                    @"function handleMultiQuoteButton(button, msgId){{
                     var messageId = msgId;var cssClass = {1}('#' + button.id).parent('span').attr('class');
                     {1}.PageMethod('{0}YafAjax.asmx', 'HandleMultiQuote', multiQuoteSuccess, CallFailed, 'buttonId', button.id, 'multiquoteButton', button.checked, 'messageId', messageId, 'buttonCssClass', cssClass);}}"
                        .FormatWith(YafForumInfo.ForumClientFileRoot, Config.JQueryAlias);
            }
        }
    }
}