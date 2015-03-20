using System.Web;

namespace YAF.Core.Tasks
{
    #region Using

    using System;

    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The mail sending module.
    /// </summary>
    [YafModule("Nntp retrieve module", "vzrus", 1)]
    public class SyncNntpTaskModule : BaseForumModule
    {
        #region Constants and Fields

        /// <summary>
        ///   The _key name.
        /// </summary>
        private const string _KeyName = "SyncNntpTask";

        #endregion

        #region Public Methods

        /// <summary>
        /// The init.
        /// </summary>
        public override void Init()
        {
            // hook the page init for mail sending...
            YafContext.Current.AfterInit += this.Current_AfterInit;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the AfterInit event of the Current control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Current_AfterInit([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<ITaskModuleManager>().StartTask(_KeyName, () => new SyncNntpTask());
        }

        #endregion
    }
}
