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
        /// Gets FancyTree complete data as parsed JSON string. .
        /// </summary>
        /// <param name="treeId">
        /// The _treeId.
        /// </param>
        /// <param name="userId"> 
        /// The iserId.
        /// </param>
        /// <param name="echofield">
        /// The echo field.
        /// </param>
        /// <param name="arguments"> 
        /// The arguments.
        /// </param>
        /// <param name="jsonData">
        /// The jsonData.
        /// </param>
        /// <param name="initializeArguments">
        /// The initializeArguments.
        /// </param>
        /// <param name="forumUrl">
        /// The forumURL.
        /// </param>
        /// <param name="boardId">
        /// The boardId.
        /// </param>
        /// <returns></returns>     
        public static string FancyTreeGetNodesJumpLazyJs(string treeId, int userId, int boardId, string echofield,
            string arguments, string jsonData, string initializeArguments, string forumUrl)
        {
            return
                String.Format(@"$(function() 
                {{
                $('#{0}').fancytree(
                        {{
                           title: 'Fancy Tree',                         
                           toggleEffect: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,
                           source: {{
                                       url: '{1}' + 's={2}{4}{3}{5}' 
                                     }},                         

                           activate: function (event, data) {{                                                    
                                                           $('#{6}').value = data.node.key;
                                                            if( node.href ){{window.open(node.href, dtnode.target);}} 
                                                         }},

                           lazyLoad: function (event, dtnode) {{

                           dtnode.result = $.ajax({{
                                             url: '{1}=' + dtnode.node.key + '{3}{5}'  ,
                                             dataType: 'json' }} );                                                      
                                                          }} 
                        }} );

              }});", treeId, jsonData, boardId, arguments, initializeArguments, forumUrl, echofield);
        }
    }
}