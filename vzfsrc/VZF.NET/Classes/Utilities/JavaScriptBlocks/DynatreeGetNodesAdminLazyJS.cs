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
        public static string DynatreeGetNodesAdminLazyJS(string treeId, int userId, int boardId, string echoActive, string arguments, string jsonData, string forumPath)
        {
            // treeId = "tree";

            /*  return String.Format(
                    @"$(function() 
                    {{
                      DynatreeGetNodesAdminLazyJS('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');}});", 
                                                                                          treeId, 
                                                                                          jsonData, 
                                                                                          boardId, 
                                                                                          arguments, 
                                                                                          echoActive, 
                                                                                          forumPath, 
                                                                                          Config.JQueryAlias); */

            return String.Format(@"$(function() 
                {{
                $('#{0}').dynatree(
                        {{
                           title: 'Lazy Tree',                         
                           fx: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,
                           initAjax: {{
                                       url: '{1}' + '?tjls={2}{5}' 
                                     }},

                           onActivate: function (node) {{
                                                            $('#{4}').text(node.data.key + ':' + node.data.title);  
                                                           {6}.get('{1}' + '?tnm=' + node.data.key);
                                                         }},

                           onLazyRead: function (node) {{
                                                          node.appendAjax({{
                           url: '{1}' + '?tjl=' + node.data.key + '{3}{5}'                    }});
                                                          }}
 
                        }} );

              }});", treeId, jsonData, boardId, arguments, echoActive, forumPath, Config.JQueryAlias);
        }
    }
}