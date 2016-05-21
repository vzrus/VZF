namespace YAF.Modules
{
    #region Using

    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Page Logo Handler Module
    /// </summary>
    [YafModule("Page Logo Handler Module", "Tiny Gecko", 1)]
    public class PageLogoHandlerForumModule : SimpleBaseForumModule
    {
        #region Public Methods

        /// <summary>
        /// The init after page.
        /// </summary>
        public override void InitAfterPage()
        {
            this.CurrentForumPage.PreRender += this.ForumPage_PreRender;
        }

        /// <summary>
        /// The init before page.
        /// </summary>
        public override void InitBeforePage()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The forum page_ pre render.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ForumPage_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            var htmlImgBanner = this.CurrentForumPage.FindControlRecursiveBothAs<HtmlImage>("imgBanner");
            var imgBanner = this.CurrentForumPage.FindControlRecursiveBothAs<Image>("imgBanner");
            var bannerLink = this.CurrentForumPage.FindControlRecursiveBothAs<HyperLink>("BannerLink");

            if (bannerLink != null)
            {
                bannerLink.NavigateUrl = YafBuildLink.GetLink(ForumPages.forum);
                bannerLink.ToolTip = this.GetText("TOOLBAR", "FORUM_TITLE");
            }

            if (!this.CurrentForumPage.ShowToolBar)
            {
                if (htmlImgBanner != null)
                {
                    htmlImgBanner.Visible = false;
                }
                else if (imgBanner != null)
                {
                    imgBanner.Visible = false;
                }
            }

            if (!this.Get<YafBoardSettings>().AllowThemedLogo || Config.IsAnyPortal)
            {
                return;
            }

            string graphicSrc = this.Get<ITheme>().GetItem("FORUM", "BANNER", null);

            if (!graphicSrc.IsSet())
            {
                return;
            }

            if (htmlImgBanner != null)
            {
                htmlImgBanner.Src = graphicSrc;
            }
            else if (imgBanner != null)
            {
                imgBanner.ImageUrl = graphicSrc;
            }
        }

        #endregion
    }
}