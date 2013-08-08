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
        /// Gets Dynatree complete data as parsed JSON string. .
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="echoActive"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string DynatreeGetNodesAllJS(string treeId, string echoActive, int userId, string jsonData)
        {
            // treeId = "tree";

            return
                String.Format(@"$(function() 
                {{
                $('#{0}').dynatree(
                        {{
                           title: 'Lazy Tree',                         
                           fx: {{ height: 'toggle', duration: 200 }},
                           children:[
                                       {2}
                                     ],
                           minExpandLevel: 2,

                           onActivate: function (dtnode) {{
                                                            $('#{1}').text(dtnode.data.title);
                                                           
                                                         }} 
                        }} );

              }});", treeId, echoActive, jsonData);
        }

    }
}