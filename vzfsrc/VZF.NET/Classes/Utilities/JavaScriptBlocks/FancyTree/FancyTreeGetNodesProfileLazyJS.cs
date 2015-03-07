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
        /// <param name="treeId"></param>
        /// <param name="echoActive"></param>
        /// <param name="userId"></param>
        /// <returns></returns>     
        public static string FancyTreeGetNodesProfileLazyJS(string treeId, int userId, int boardId, string echofield, string arguments, string jsonData, string initializeArguments, string forumUrl)
        {
            // treeId = "tree";

            return
                String.Format(@"$(function() 
                {{
                $('#{0}').fancytree(
                        {{

                           title: 'Fancy Tree',                         
                           toggleEffect: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,                         
                           checkbox: false,                          

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
dataType: 'json' }}); 
}}                                             
                                                          
                        }} );

              }});", treeId, jsonData, boardId, arguments, initializeArguments, forumUrl, echofield);
        }
    }
}