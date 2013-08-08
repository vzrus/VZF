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
        public static string ButtonShowForumInNNTPTree(string treeId, string btnId, string path)
        {
            return String.Format(
                @"$(function() 
                {{
                   $('#{1}').click(function()
                         {{ 
                             var tree = $('#{0}').dynatree('getTree');
                            tree.loadKeyPath('{2}', true, function(dtnode, path, isOk){{
                          if(status == 'loaded') {{   dtnode.activate();  
          dtnode.expand();
        }}else if(status == 'ok') {{       
          dtnode.activate();
        }}else if(status == 'notfound') {{
       alert(dtnode.data.key + ' is not found');

    }}

}});
                         }}
                                             );
               }});", treeId, btnId, path);


        }
    }
}