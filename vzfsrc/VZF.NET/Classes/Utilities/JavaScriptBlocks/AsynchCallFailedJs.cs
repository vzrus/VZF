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
        ///  Gets the If asynchronous callback encounters any problem, this javascript function will be called.
        /// </summary>
        [NotNull]
        public static string AsynchCallFailedJs
        {
            get
            {
                return "function CallFailed(res){{alert('Error Occurred');}}";
            }
        }

    }
}