namespace VZF.Utilities
{
    #region Using

    using System;

    using YAF.Classes;
    using YAF.Types;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        // Written by vzrus(Vladimir Zakharov).
        /// <summary>
        /// Opens and closes dividers or things like this.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ToggleDivider(string id)
        {
            return @"function toggle_visibility(id) {
                              var e = document.getElementById(id);
                              if(e.style.display == 'block')
                              e.style.display = 'none';
                              else
                               e.style.display = 'block';};";
        } 
    }
}