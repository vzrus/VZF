namespace YAF.Modules
{
    #region Using

    using System;

    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Summary description for SuspendCheckModule
    /// </summary>
    [YafModule("Suspend Check Module", "Tiny Gecko", 1)]
    public class SuspendCheckForumModule : SimpleBaseForumModule
    {
        #region Constants and Fields

        /// <summary>
        /// The _pre load page.
        /// </summary>
        private readonly IFireEvent<ForumPagePreLoadEvent> _preLoadPage;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SuspendCheckForumModule"/> class.
        /// </summary>
        /// <param name="preLoadPage">
        /// The pre load page.
        /// </param>
        public SuspendCheckForumModule([NotNull] IFireEvent<ForumPagePreLoadEvent> preLoadPage)
        {
            this._preLoadPage = preLoadPage;
            this._preLoadPage.HandleEvent += this._preLoadPage_HandleEvent;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The _pre load page_ handle event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _preLoadPage_HandleEvent(
            [NotNull] object sender, [NotNull] EventConverterArgs<ForumPagePreLoadEvent> e)
        {
            // check for suspension if enabled...
            if (!this.PageContext.Globals.IsSuspendCheckEnabled)
            {
                return;
            }

            if (!this.PageContext.IsSuspended)
            {
                return;
            }

            if (this.PageContext.SuspendedUntil <= DateTime.UtcNow)
            {
                CommonDb.user_suspend(this.PageContext.PageModuleID, this.PageContext.PageUserID, null);
            }
            else
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.Suspended);
            }
        }

        #endregion
    }
}