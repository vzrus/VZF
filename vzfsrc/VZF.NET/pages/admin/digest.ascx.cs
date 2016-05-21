using System.Collections.Generic;

namespace YAF.Pages.Admin
{
    #region Using

    using System;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Manage Forum Digest Sending
    /// </summary>
    public partial class digest : AdminPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="digest"/> class.
        /// </summary>
        public digest()
            : base("ADMIN_DIGEST")
        {
        }

        #region Methods

        /// <summary>
        /// Force Sending the Current Digest
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ForceSend_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<YafBoardSettings>().ForceDigestSend = true;
            ((YafLoadBoardSettings)YafContext.Current.BoardSettings).SaveRegistry(
                new Dictionary<string, object>
                {
                    {"ForceDigestSend", true}
                }, 
                PageContext.PageBoardID);


            this.PageContext.AddLoadMessage(this.GetText("ADMIN_DIGEST", "MSG_FORCE_SEND"));
        }

        /// <summary>
        /// Generate a test Digest
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void GenerateDigest_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.DigestHtmlPlaceHolder.Visible = true;
            this.DigestFrame.Attributes["src"] = this.Get<IDigest>().GetDigestUrl(
                this.PageContext.PageUserID,
                this.PageContext.PageBoardID,
                this.Get<YafBoardSettings>().WebServiceToken,
                true);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(this.GetText("ADMIN_DIGEST", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_DIGEST", "TITLE"));

            this.LastDigestSendLabel.Text = this.Get<YafBoardSettings>().LastDigestSend.IsNotSet()
                                                ? this.GetText("ADMIN_DIGEST", "DIGEST_NEVER")
                                                : this.Get<IDateTime>().FormatDateTimeTopic(this.Get<YafBoardSettings>().LastDigestSend);

            this.DigestEnabled.Text = this.Get<YafBoardSettings>().AllowDigestEmail
                                          ? this.GetText("COMMON", "YES")
                                          : this.GetText("COMMON", "NO");

            this.TestSend.Text = this.GetText("ADMIN_DIGEST", "SEND_TEST");

            this.GenerateDigest.Text = this.GetText("ADMIN_DIGEST", "GENERATE_DIGEST");
            this.Button2.Text = this.GetText("ADMIN_DIGEST", "FORCE_SEND");

            this.Button2.OnClientClick =
                "return confirm('{0}');".FormatWith(this.GetText("ADMIN_DIGEST", "CONFIRM_FORCE"));
        }

        /// <summary>
        /// Send Test Digest
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void TestSend_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.TextSendEmail.Text.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_DIGEST", "MSG_VALID_MAIL"), MessageTypes.Error);
            }

            try
            {
                // create and send a test digest to the email provided...
                var digestHtml = this.Get<IDigest>().GetDigestHtml(
                    this.PageContext.PageUserID,
                    this.PageContext.PageBoardID,
                    this.Get<YafBoardSettings>().WebServiceToken,
                    true);

                // send....
                this.Get<IDigest>().SendDigest(
                    digestHtml,
                    this.Get<YafBoardSettings>().Name,
                    this.Get<YafBoardSettings>().ForumEmail,
                    this.TextSendEmail.Text.Trim(),
                    "Digest Send Test",
                    this.SendMethod.SelectedItem.Text == "Queued");

                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_DIGEST", "MSG_SEND_SUC").FormatWith(this.SendMethod.SelectedItem.Text),
                    MessageTypes.Success);
            }
            catch (Exception ex)
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_DIGEST", "MSG_SEND_ERR").FormatWith(ex), MessageTypes.Error);
            }
        }

        #endregion
    }
}