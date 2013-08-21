namespace VZF.Utilities
{
    #region Using

    using System;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        // Written by vzrus(Vladimir Zakharov).
        /// <summary>
        /// Gets Dynatree complete data as parsed JSON string. .
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="echoActive"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string DynatreeGetNodesJumpLazyJS(string treeId, int userId, int boardId, string arguments, string jsonData, string initializeArguments, string forumUrl)
        {
            // treeId = "tree";

            return
                String.Format(@"$(function() 
                {{
                $('#{0}').dynatree(
                        {{
                           title: 'Lazy Tree',                         
                           fx: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,
                            initAjax: {{
                                       url: '{1}' + 's={2}{4}{5}' 
                                     }},

                           onActivate: function (dtnode) {{                                                          
                                                            if( node.data.href ){{window.open(node.data.href, dtnode.data.target);}} 
                                                         }},

                           onLazyRead: function (dtnode) {{
                                                          dtnode.appendAjax({{
                           url: '{1}=' + dtnode.data.key + '{3}{5}'                    }});
                                                          }} 
                        }} );

              }});", treeId, jsonData, boardId, arguments, initializeArguments, forumUrl);
        }
    }
}