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
        /// Gets the multi quote callback success JS.
        /// </summary>
        [NotNull]
        public static string MultiQuoteCallbackSuccessJS
        {
            get
            {
                return
                    @"function multiQuoteSuccess(res){{
                  var multiQuoteButton = {0}('#' + res.d.Id).parent('span');
                  multiQuoteButton.removeClass(multiQuoteButton.attr('class')).addClass(res.d.NewTitle);
                  {0}(document).scrollTop(multiQuoteButton.offset().top - 20);
                      }}"
                        .FormatWith(Config.JQueryAlias);
            }
        }
    }
}