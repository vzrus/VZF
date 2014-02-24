// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="DynatreeGetNodesAdminLazyJS.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The DynatreeGetNodesAdminLazyJS.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
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