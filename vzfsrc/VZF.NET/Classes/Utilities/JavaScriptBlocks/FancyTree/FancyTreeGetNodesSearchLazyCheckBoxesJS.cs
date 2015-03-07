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
        public static string FancyTreeGetNodesSearchLazyCheckBoxesJS(string treeId, int userId, int boardId, string arguments, string jsonData, string initializeArguments, string forumUrl)
        {
            // treeId = "tree";

            return
                String.Format(@"$(function() 
                {{
                $('#{0}').fancytree(
                        {{
                           title: 'Fancy Tree',                         
                           toggleEffect: {{ height: 'toggle', duration: 200 }},
                           checkbox: true,
                           selectMode: 1,
                           autoFocus: false,
                           source: {{
                                       url: '{1}' + 's={2}{5}{6}' 
                                     }},

                           activate: function (event,dtnode) {{                                                          
                                                            if( dtnode.node.href ){{window.open(dtnode.href, dtnode.node.target);}} 
                                                         }},
                           select: function (event, dtnode) {{ 
                                                   var selKeys = $.map(dtnode.tree.getSelectedNodes(), function(data){{
                                                                 return dtnode.node.key;}});  
                                                              var s = selKeys.join('!');                                                                                                                               
                                                            {4}.get('{1}=-100'+'&selected=' + s, function(data)                                                       
                                                              {{}});
                                                         }},
                           lazyLoad: function (event,dtnode) {{                                                       
                                     dtnode.result = $.ajax({{
                                     url: '{1}=' + dtnode.node.key + '{3}{6}'  ,
                                     dataType: 'json' }} );   
                                                          }} 
                        }} );


              }});", treeId, jsonData, boardId, arguments, Config.JQueryAlias, initializeArguments, forumUrl);
        }
    }
}