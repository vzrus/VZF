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
        ///   Gets Pagination Load Js.
        /// </summary>
        [NotNull]
        public static string PaginationLoadJs
        {
            get
            {
                return
                    @"function pageselectCallback(page_index, jq){{
                var new_content = {0}('#SmiliesPagerHidden div.result:eq('+page_index+')').clone();
                {0}('#SmiliesPagerResult').empty().append(new_content);
                return false;
            }}
           
            {0}(document).ready(function(){{      
                var num_entries = {0}('#SmiliesPagerHidden div.result').length;
                {0}('#SmiliesPager').pagination(num_entries, {{
                    callback: pageselectCallback,
                    items_per_page:1,
                    num_display_entries: 3,
                    num_edge_entries: 1,
                    prev_class: 'smiliesPagerPrev',
                    next_class: 'smiliesPagerNext',
                    prev_text: '&laquo;',
                    next_text: '&raquo;'
                }});
            }});"
                        .FormatWith(Config.JQueryAlias);
            }
        }
    }
}