namespace VZF.Utilities
{
    #region Using
    using YAF.Types;
    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        ///   Gets DisablePageManagerScrollJs.
        /// </summary>
        [NotNull]
        public static string DisablePageManagerScrollJs
        {
            get
            {
                return
                    @"
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_beginRequest(beginRequest);

    function beginRequest() {
        prm._scrollPosition = null;
    }
";
            }
        }
    }
}