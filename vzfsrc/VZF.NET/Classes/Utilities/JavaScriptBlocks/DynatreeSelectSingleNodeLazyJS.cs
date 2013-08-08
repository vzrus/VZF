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
        /// <param name="echoActive"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string DynatreeSelectSingleNodeLazyJS(string treeId, int userId, int boardId, string echoActive, string activeNode, string arguments, string jsonData, string forumUrl)
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
                                       url: '{1}' + 's={2}{3}{6}'  
                                     }},

                           onActivate: function (dtnode) {{
                                                            $('#{4}').text(dtnode.data.title);                                                                                                                                      
                                                            {5}.get('{1}=-100'+'&active=' + dtnode.data.key, function(data)
{{

                                                       
                                                              }});
                                                         }},
 

                           onLazyRead: function (dtnode) {{
                                                          dtnode.appendAjax({{
                           url: '{1}=' + dtnode.data.key + '{3}{6}'                    }});
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