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
        public static string FancyTreeGetNodesGroupAccessLazyJS(string treeId, int userId, int boardId, int? groupId, string arguments, string initializeArguments, string jsonData, string forumUrl)
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
                                       url: '{1}' + 'tjls={2}{4}{3}{5}' 
                                     }},

                           activate: function (event, data) {{                                                    
                                                         ////  $('#{6}').value = data.node.key;
                                                            if( node.href ){{window.open(node.href, dtnode.target);}} 
                                                         }},

                           lazyLoad: function (event, dtnode) {{

dtnode.result = $.ajax({{
          url: '{1}tjl=' + dtnode.node.key + '{3}{5}'  ,
dataType: 'json' }}); 
}}                                            
                                                          
                        }} );

 $('#{0}').delegate('select[name=selectaccessddl]', 'change', function(e){{
    var node = $.ui.fancytree.getNode(e),
         $select = $(e.target);
     e.stopPropagation();  //// prevent fancytree activate for this row
 $.ajax({{
          url: '{1}fgacc=' + $select.val() + '&fid=' + node.key + '&gid={6}',
 dataType: 'json' }}); 

   ////   if($select.is(':selected')){{
  ////      alert('selectaccessddl ' + $select.val());
   ////   }}else{{
    ////    alert('dislike ' + $select.val());
  ////    }}

    }});


              }});", treeId, jsonData, boardId, arguments, initializeArguments, forumUrl, groupId);
        }
    }
}
/*

*/