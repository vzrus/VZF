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
        /// Gets FancyTree complete data as parsed JSON string. .
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="echoActive"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string FancyTreeSelectSingleNodeLazyJS(string treeId, int userId, int boardId, string echoActive, string activeNode, string arguments, string jsonData, string forumUrl)
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
                           source: {{
                                       url: '{1}' + 's={2}{3}{6}'  
                                     }},

                           activate: function (event,dtnode) {{
                                                            $('#{4}').text(dtnode.node.title);                                                                                                                                      
                                                            {5}.get('{1}=-100'+'&active=' + dtnode.node.key, function(data){{ }});
                                                         }},
 
                            lazyLoad: function (event, dtnode) {{

dtnode.result = $.ajax({{
          url: '{1}=' + dtnode.node.key + '{3}{6}'  ,
dataType: 'json' }} );          
                         
                                                          }} 
                        }} );

              }});", treeId, jsonData, boardId, arguments, echoActive, Config.JQueryAlias, forumUrl);

            /*                                   onPostInit:
         function (dtnode) {{ $('#{0}').loadKeyPath('{5}', function(dtnode, status){{
                if(status == 'loaded') {{         
                  dtnode.expand();
                }}else if(status == 'ok') {{          
                  dtnode.activate();
                }}
              }}); }}
        , */
        }
    }
}