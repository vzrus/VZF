namespace VZF.Utilities
{
    #region Using

    using YAF.Classes;
    using YAF.Types;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        ///   Gets CeeBox Load Js.
        /// </summary>
        [NotNull]
        public static string CeeBoxLoadJs
        {
            get
            {
                return
                    @"{0}(document).ready(function() {{ 
                    {0}('.ceebox').ceebox({{titles:true}});}});".FormatWith(
                        Config.JQueryAlias);
            }
        }
    }
}