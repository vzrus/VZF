namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.IO;
    using System.Web;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The Admin Restart App Page.
    /// </summary>
    public partial class restartapp : AdminPage
    {
        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                this.PageLinks.AddLink(
                    this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
                this.PageLinks.AddLink(this.GetText("ADMIN_RESTARTAPP", "TITLE"), string.Empty);

                this.Page.Header.Title = "{0} - {1}".FormatWith(
                    this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_RESTARTAPP", "TITLE"));

                this.RestartApp.Text = this.GetText("ADMIN_RESTARTAPP", "TITLE");
            }

            this.DataBind();
        }

        /// <summary>
        /// Try to Restart the Application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void RestartApp_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (General.GetCurrentTrustLevel() >= AspNetHostingPermissionLevel.High)
            {
                HttpRuntime.UnloadAppDomain();
            }
            else
            {
                try
                {
                    File.SetLastWriteTime(this.Server.MapPath("~/web.config"), DateTime.UtcNow);
                }
                catch (Exception)
                {
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_RESTARTAPP", "MSG_TRUST"), MessageTypes.Error);
                }
            }
        }

        #endregion
    }
}