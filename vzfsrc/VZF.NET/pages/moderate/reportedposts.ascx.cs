﻿namespace YAF.Pages.moderate
{
    #region Using

    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using VZF.Controls;
    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Core.Services.CheckForSpam;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    ///  Moderating Page for Reported Posts.
    /// </summary>
    public partial class reportedposts : ModerateForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "reportedposts" /> class. 
        ///   Default constructor.
        /// </summary>
        public reportedposts()
            : base("MODERATE_FORUM")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
            // forum index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // moderation index
            this.PageLinks.AddLink(this.GetText("MODERATE_DEFAULT", "TITLE"), YafBuildLink.GetLink(ForumPages.moderate_index));

            // current page
            this.PageLinks.AddLink(this.PageContext.PageForumName);
        }

        /// <summary>
        /// Handles load event for delete button, adds confirmation dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var button = sender as ThemeButton;
            if (button != null)
            {
                button.Attributes["onclick"] = "return confirm('{0}');".FormatWith(this.GetText("ASK_DELETE"));
            }
        }

        /// <summary>
        /// Format message.
        /// </summary>
        /// <param name="row">
        /// Message data row.
        /// </param>
        /// <returns>
        /// Formatted string with escaped HTML markup and formatted.
        /// </returns>
        protected string FormatMessage([NotNull] DataRowView row)
        {
            // get message flags
            var messageFlags = new MessageFlags(row["Flags"]);

            // message
            string msg;

            // format message?
            if (messageFlags.NotFormatted)
            {
                // just encode it for HTML output
                msg = this.HtmlEncode(row["OriginalMessage"].ToString());
            }
            else
            {
                // fully format message (YafBBCode, smilies)
                msg = this.Get<IFormatMessage>().FormatMessage(
                  row["OriginalMessage"].ToString(), messageFlags, Convert.ToBoolean(row["IsModeratorChanged"]));
            }

            // return formatted message
            return msg;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.List.ItemCommand += this.List_ItemCommand;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Handles page load event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // do this just on page load, not postbacks
            if (this.IsPostBack)
            {
                return;
            }

            // check if the user has access to the page.
            using (DataSet ds = CommonDb.forum_moderatelist(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageBoardID))
            {
                if (ds.Tables["Category"] == null || ds.Tables["Category"].Rows.Count <= 0)
                {
                    YafBuildLink.RedirectInfoPage(InfoMessage.AccessDenied);
                }

            }

            // create page links
            this.CreatePageLinks();

            // bind data
            this.BindData();
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // get reported posts for this forum
            this.List.DataSource = CommonDb.message_listreported(PageContext.PageModuleID, this.PageContext.PageForumID);

            // bind data to controls
            this.DataBind();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Handles post moderation events/buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        private void List_ItemCommand([NotNull] object sender, [NotNull] RepeaterCommandEventArgs e)
        {
            // which command are we handling
            switch (e.CommandName.ToLower())
            {
                case "delete":

                    // delete message
                    CommonDb.message_delete(PageContext.PageModuleID, e.CommandArgument, true, string.Empty, 1, true);

                    // Update statistics
                    this.Get<IDataCache>().Remove(Constants.Cache.BoardStats);

                    // re-bind data
                    this.BindData();

                    // tell user message was deleted
                    this.PageContext.AddLoadMessage(this.GetText("DELETED"));
                    break;
                case "view":

                    // go to the message
                    YafBuildLink.Redirect(ForumPages.posts, "m={0}#post{0}", e.CommandArgument);
                    break;
                case "copyover":

                    // re-bind data
                    this.BindData();

                    // update message text
                    CommonDb.message_reportcopyover(PageContext.PageModuleID, e.CommandArgument);
                    break;
                case "viewhistory":

                    // go to history page
                    string[] ff = e.CommandArgument.ToString().Split(',');
                    YafContext.Current.Get<HttpResponseBase>().Redirect(
                      YafBuildLink.GetLinkNotEscaped(ForumPages.messagehistory, "f={0}&m={1}", ff[0], ff[1]));
                    break;
                case "resolved":

                    // mark message as resolved
                    CommonDb.message_reportresolve(PageContext.PageModuleID, 7, e.CommandArgument, this.PageContext.PageUserID);

                    // re-bind data
                    this.BindData();

                    // tell user message was flagged as resolved
                    this.PageContext.AddLoadMessage(this.GetText("RESOLVEDFEEDBACK"), MessageTypes.Success);
                    break;
                case "spam":

                    this.ReportSpam((string)e.CommandArgument);

                    break;
            }

            // see if there are any items left...
            DataTable dt = CommonDb.message_listreported(PageContext.PageModuleID, this.PageContext.PageForumID);

            if (dt.Rows.Count == 0)
            {
                // nope -- redirect back to the moderate main...
                YafBuildLink.Redirect(ForumPages.moderate_index);
            }
        }

        /// <summary>
        /// Report Message as Spam
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        private void ReportSpam(string comment)
        {
            if (this.Get<YafBoardSettings>().SpamServiceType.Equals(1))
            {
                string message = BlogSpamNet.ClassifyComment(comment, true);

                this.PageContext.AddLoadMessage(message);
            }

            try
            {
                if (!this.Get<YafBoardSettings>().SpamServiceType.Equals(2)
                    || string.IsNullOrEmpty(this.Get<YafBoardSettings>().AkismetApiKey))
                {
                    return;
                }

                var service = new AkismetSpamClient(this.Get<YafBoardSettings>().AkismetApiKey, new Uri(BaseUrlBuilder.BaseUrl));

                service.SubmitSpam(new Comment(null, string.Empty) { Content = comment });

                this.PageContext.AddLoadMessage(this.GetText("MODERATE_DEFAULT", "SPAM_REPORTED"));
            }
            catch (Exception)
            {
                this.PageContext.AddLoadMessage(this.GetText("MODERATE_DEFAULT", "SPAM_REPORTED_FAILED"));
            }
        }

        #endregion
    }
}