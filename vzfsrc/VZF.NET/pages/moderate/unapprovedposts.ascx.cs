namespace YAF.Pages.moderate
{
    #region Using

    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Moderating Page for Unapproved Posts.
    /// </summary>
    public partial class unapprovedposts : ModerateForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "unapprovedposts" /> class. 
        ///   Default constructor.
        /// </summary>
        public unapprovedposts()
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
                msg = this.HtmlEncode(row["Message"].ToString());
            }
            else
            {
                // fully format message (YafBBCode, smilies)
                msg = this.Get<IFormatMessage>().FormatMessage(
                  row["Message"].ToString(), messageFlags, Convert.ToBoolean(row["IsModeratorChanged"]));
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
            // get unapproved posts for this forum
            this.List.DataSource = CommonDb.message_unapproved(PageContext.PageModuleID, this.PageContext.PageForumID);

            // bind data to controls
            this.DataBind();
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
                case "approve":

                    // approve post
                    CommonDb.message_approve(PageContext.PageModuleID, e.CommandArgument);

                    // Update statistics
                    this.Get<IDataCache>().Remove(Constants.Cache.BoardStats);

                    // re-bind data
                    this.BindData();

                    // tell user message was approved
                    this.PageContext.AddLoadMessage(this.GetText("APPROVED"), MessageTypes.Success);

                    // send notification to watching users...
                    this.Get<ISendNotification>().ToWatchingUsers(e.CommandArgument.ToType<int>());
                    break;
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
            }

            // see if there are any items left...
            DataTable dt = CommonDb.message_unapproved(PageContext.PageModuleID, this.PageContext.PageForumID);

            if (dt.Rows.Count == 0)
            {
                // nope -- redirect back to the moderate main...
                YafBuildLink.Redirect(ForumPages.moderate_index);
            }
        }

        #endregion
    }
}