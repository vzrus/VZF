using VZF.Utils;
using YAF.Types;

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
        public static string FancyTreeSelectSingleNodeLazyJsNew(string treeId, int boardId, string defautPage, string activeNode, string arguments, string jsonData, string forumUrl)
        {
            string jsn = "{{ boardId1: '{2}', inodeId: '' , viiew1: '1', ctive1: '0' , elected1 : '' ,  boardFirst : '0', forumUrl : '{5}', showLinks1 : 'true', showAccessRow1 : 'true'}}";    
            return
                String.Format(@"$(function() 
                {{
                $('#{0}').fancytree(
                        {{
                           title: 'Fancy Tree',                         
                           toggleEffect: {{ height: 'toggle', duration: 200 }},
                           autoFocus: false,
                           source:   {{             
                                       function(mes) {{ 
                                                     //// var dataValue = '{{ boardId1: ' + '{2}' + ', inodeId: ' + '0' + ', viiew1: '+ '1' + ', ctive1: '+ '0' + ', elected1 : '+ '0' + ',  boardFirst : ' + '0' + ', forumUrl : ' + '{5}' + ', showLinks1 : ' + 'true' + ', showAccessRow1 : ' + 'true' + '}}';    
                                              {4}.ajax({{
                                                                type: 'POST',
                                                                url: '/YafAjax.asmx/FancyTreeGetNodes',
                                                                data: ""{{'boardId1': '{2}', 'inodeId': '' , 'viiew1': '1', 'ctive1': '' , 'elected1': '' ,'boardFirst' : 'false', 'forumUrl': '{5}', 'showLinks1': 'false', 'showAccessRow1': '0'}}"",
                                                                contentType: 'application/json; charset=utf-8',
                                                                dataType: 'json',
                                                                error: function (XMLHttpRequest, textStatus, errorThrown) {{
                                                                     alert('Request: ' + XMLHttpRequest.toString() + ' Error: ' + errorThrown);
                                                                      }},
                                                                success: function (result) {{                                                                      
                                                                         mes = result.d || result; 
                                                                      ////   console.log(mes);
                                                                       ////     console.log(msg.foo);  
                                                                        //// alert('We returned: ' + result.d);
                                                                         }}
                                                                      }});
return mes;
                                                                    

}}

}},

                           activate: function (event,dtnode) {{
                                                                                                                                                                                              
                                                            {4}.get('{1}=-100'+'&active=' + dtnode.node.key, function(data){{ }});
                                                         }},
 
                          lazyLoad: function (event, dtnode) {{

dtnode.result = {4}.ajax({{
          url: '{1}=' + dtnode.node.key + '{3}{5}'  ,
dataType: 'json' }} );          
                         
                                                          }} 
                        }} );

              }});", treeId, jsonData, boardId, arguments, Config.JQueryAlias, forumUrl);

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