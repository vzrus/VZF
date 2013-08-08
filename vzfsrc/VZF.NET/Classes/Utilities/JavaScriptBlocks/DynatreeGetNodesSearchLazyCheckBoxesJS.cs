namespace VZF.Utilities
{
    #region Using

    using System;

    using YAF.Classes;

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
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string DynatreeGetNodesSearchLazyCheckBoxesJS(string treeId, int userId, int boardId, string arguments, string jsonData, string initializeArguments, string forumUrl)
        {
            // treeId = "tree";

            return
                String.Format(@"$(function() 
                {{
                $('#{0}').dynatree(
                        {{
                           title: 'Lazy Tree',                         
                           fx: {{ height: 'toggle', duration: 200 }},
                           checkbox: true,
                           selectMode: 1,
                           autoFocus: false,
                           initAjax: {{
                                       url: '{1}' + 's={2}{5}{6}' 
                                     }},

                           onActivate: function (node) {{                                                          
                                                            if( node.data.href ){{window.open(node.data.href, node.data.target);}} 
                                                         }},
                           onSelect: function (select, node) {{ 
                                                   var selKeys = $.map(node.tree.getSelectedNodes(), function(node){{
                                                                 return node.data.key;}});  
                                                              var s = selKeys.join('!');                                                                                                                               
                                                            {4}.get('{1}=-100'+'&selected=' + s, function(data)                                                       
                                                              {{}});
                                                         }},
                           onLazyRead: function (dtnode) {{
                                                          dtnode.appendAjax({{
                           url: '{1}=' + dtnode.data.key + '{3}{6}'                    }});
                                                          }} 
                        }} );

              }});", treeId, jsonData, boardId, arguments, Config.JQueryAlias, initializeArguments, forumUrl);
        }
    }
}