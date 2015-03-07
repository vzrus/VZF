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
        /// Gets FancyTree complete data as parsed JSON string. .
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="impossibleLabel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>      
        public static string FancytreeGetNodesAdminLazyJS(string treeId, int userId, int boardId, string impossibleLabel, string arguments, string jsonData, string forumPath)
        {
            return String.Format(@"$(function() 
                {{
                $('#{0}').fancytree(
                        {{
                           extensions: ['dnd'],
                           title: 'Fancy Tree',                         
                           toggleEffect: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,
                           source: {{
                                       url: '{1}' + '?tjls={2}{5}' 
                                   }},

                           dnd: {{
                                  autoExpandMS: 400,
                                  focusOnClick: true,
                                  preventVoidMoves: true,
                                  preventRecursiveMoves: true,
                               dragStart: function(node, data) {{    
                                                                    return true;
                                                               }},
                               dragEnter: function(node, data) {{ 
                                          if(node.folder == true && data.otherNode.folder) {{
                                                                                            return ['before','after'];
                                                                                            }};
                                          if((node.folder == true && data.otherNode.folder == false)) 
                                                                                            {{
                                                                                               return ['over'];
                                                                                            }}; 
                                           if(node.folder == false && data.otherNode.folder == true) {{
                                                                                                       return false;
                                                                                                       }};  
                                           return true;
                                                               }},
                              dragDrop: function(node, data) {{ 
                                        var saveready;
                                        var movepath;    
                                        data.otherNode.moveTo(node, data.hitMode);
                                        //// console.log(' hit mode = ' + data.hitMode + ';'  );
                                        //// console.log(' other node title =' + data.otherNode.title + '; key=' +  data.otherNode.key );
                                        //// console.log(' node title =' + node.title + '; key=' +  data.otherNode.key );
                                        //// console.log(' node parent title =' + data.node.parent.title + '; key' +  data.node.parent.key );
                                        //// console.log(' othernode parent title =' + data.otherNode.parent.title + '; key' +  data.otherNode.parent.key );
                                        //// moving as a forum child
                                        movepath = '{1}' + '?trno=' + data.otherNode.key
                                                          + '&trn=' + data.node.key 
                                                          + '&trnp=' + data.node.parent.key 
                                                          + '&trnop=' + data.otherNode.parent.key 
                                                          + '&trna=' + data.hitMode +  '{3}{5}' ;


                                        saveready = $.ajax({{
                                                             url: movepath ,
                                                             dataType: 'text' 
                                                           }} );    
                                                    console.log(' ready = ' + saveready + ';'  );
                       }}
            }},
                           activate: function (event,data) {{                     
                                                        ////   {6}.get('{1}' + '?tnm=' + data.node.key);
    
                                                         }},
                           lazyLoad: function (event, dtnode) {{
                                                              dtnode.result = $.ajax({{
                                                                               url: '{1}' + '?tjl=' + dtnode.node.key + '{3}{5}'  ,
                                                                               dataType: 'json' 
                                                                                     }});                    
                                                               }}
}} );

 $('#{0}').contextmenu({{
      delegate: 'span.fancytree-title',
////      menu: '#options',
      menu: [         
          {{title: 'Delete  ', cmd: 'delete', uiIcon: 'ui-icon-trash', disabled: false }},
          {{title: '----'}},
          {{title: 'Edit', cmd: 'edit', uiIcon: 'ui-icon-pencil', disabled: false }},         
          {{title: 'New', data:'new', uiIcon: 'ui-icon-plus', children: [
              {{title: 'Category', data: 'category', uiIcon: 'ui-icon-folder-collapsed', children: [
                   {{title: 'After', cmd: 'catafter', uiIcon: 'ui-icon-arrow-1-s',  disabled: false }},
                   {{title: 'Before', cmd: 'catbefore', uiIcon: 'ui-icon-arrow-1-n',  disabled: false}}                                                            ]
              }},
              {{title: 'Forum', data: 'forum', uiIcon: 'ui-icon-note', children: [
                   {{title: 'After', cmd: 'frmafter', uiIcon: 'ui-icon-arrow-1-s', disabled: false }},
                   {{title: 'Before', cmd: 'frmbefore', uiIcon: 'ui-icon-arrow-1-n',  disabled: false}},
                   {{title: 'Child', cmd: 'frmover', uiIcon: 'ui-icon-arrowreturn-1-e', disabled: false}}                                                         ]
              }}   
                                                ]
          }}                                        
],         
      beforeOpen: function(event, ui) {{
                  var node = $.ui.fancytree.getNode(ui.target);
  console.log(' node.folder = ' + node.folder + ';'  );
                  if (!node.folder) {{
                                     $('#{0}').contextmenu('setEntry', 'new', 
             
              {{title: 'New', children: [
            {{title: 'Forum', data: 'forum', children: [
            {{title: 'After', cmd: 'frmafter', uiIcon: 'ui-icon-trash', disabled: false }},
            {{title: 'Before', cmd: 'frmbefore', uiIcon: 'ui-icon-trash', disabled: false}},
            {{title: 'Child', cmd: 'frmover', uiIcon: 'ui-icon-trash', disabled: false}}
                                                       ]
             }}                         ]
          }}
                                                     );     
                                   }}
                  else {{
                        $('#{0}').contextmenu('setEntry', 'newm', 
              {{title: 'New', children: [
            {{title: 'Category', data: 'category', children: [
            {{title: 'After', cmd: 'catafter', uiIcon: 'ui-icon-trash', disabled: false }},
            {{title: 'Before', cmd: 'catbefore', uiIcon: 'ui-icon-trash', disabled: false}}      
                                                              ]
             }}                     ]

                }});
                        //// $('#{0}').contextmenu('showEntry', 'category', false);
                        }};
                  ////  node.setFocus();
                  node.setActive();
                                      }},
      select: function(event, ui) {{
                                    var node = $.ui.fancytree.getNode(ui.target);
                                    var url = '';
                                
                                   //// alert('select ' + ui.cmd + ' on ' + node);
                                    
                                    switch (ui.cmd) {{
                                                 case 'edit':
                                                     if (node.folder){{
                                                            url = url + 'g=admin_editcategory&c=' + node.key;
                                                                     }}
                                                       else {{
                                                            url = url + 'g=admin_editforum&fa=' + node.key;
                                                            }}; 
                                                            break;
                                                 case 'delete':
                                                     if (node.folder){{
                                                           url = url + 'g=admin_deletecategory&fa=' + node.key;
                                                                     }}
                                                           else      {{
                                                           url = url + 'g=admin_deleteforum&fa=' + node.key;
                                                                     }};
                                                       break;

                                                 case 'catbefore':
                                                     if (node.folder){{
                                                                    url = url + 'g=admin_editcategory&before=' + node.key; 
                                                                     }}
                                                           else      {{
                                                           alert('{4}');
                                                                     }};
                                                       break;
                                                case 'catafter':
                                                     if (node.folder){{
                                                                     url = url + 'g=admin_editcategory&after=' + node.key;   
                                                                     }}
                                                           else      {{
                                                           alert('{4}');
                                                                     }};
                                                       break;
                                                case 'frmbefore':
                                                     if (!node.folder){{
                                                                    url = url + 'g=admin_editforum&before=' + node.key; 
                                                                     }}
                                                           else      {{
                                                           alert('{4}');
                                                                     }};
                                                       break;
                                                case 'frmafter':
                                                     if (!node.folder){{
                                                                    url = url + 'g=admin_editforum&after=' + node.key; 
                                                                     }}
                                                           else      {{
                                                           alert('{4}');
                                                                     }};
                                                       break;
                                                case 'frmover':                                                     
                                                                    url = url + 'g=admin_editforum&over=' + node.key; 
                                                                    
                                                       break;
                                                     }};                   

                                  
                                    if (url.length > 0) {{
                                         url = '/default.aspx?' + url;
                                           if (navigator.userAgent.match(/MSIE\s(?!9.0)/)) {{
                                              var referLink = document.createElement('a');
                                              referLink.href = url;
                                              document.body.appendChild(referLink);
                                              referLink.click();
                                                                                           }}
                                  else {{      
                                          window.location.replace(url);
                                       }};
                                                         }};
                                                         }}
                  }});

              }});", treeId, jsonData, boardId, arguments, impossibleLabel, forumPath, Config.JQueryAlias);

        }
    }
}