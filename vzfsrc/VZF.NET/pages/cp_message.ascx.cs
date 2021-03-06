namespace YAF.Pages
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
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The cp_message.
    /// </summary>
    public partial class cp_message : ForumPageRegistered
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "cp_message" /> class.
        /// </summary>
        public cp_message()
            : base("CP_MESSAGE")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets a value indicating whether IsArchived.
        /// </summary>
        protected bool IsArchived
        {
            get
            {
                return this.ViewState["IsArchived"] != null && (bool)this.ViewState["IsArchived"];
            }

            set
            {
                this.ViewState["IsArchived"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether IsOutbox.
        /// </summary>
        protected bool IsOutbox
        {
            get
            {
                return this.ViewState["IsOutbox"] != null && (bool)this.ViewState["IsOutbox"];
            }

            set
            {
                this.ViewState["IsOutbox"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The On PreRender event.
        /// </summary>
        /// <param name="e">
        /// the Event Arguments
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            // Setup Syntax Highlight JS
            YafContext.Current.PageElements.RegisterJsResourceInclude(
                "syntaxhighlighter", "js/jquery.syntaxhighligher.js");
            YafContext.Current.PageElements.RegisterCssIncludeResource("css/jquery.syntaxhighligher.css");
            YafContext.Current.PageElements.RegisterJsBlockStartup(
                "syntaxhighlighterjs", JavaScriptBlocks.SyntaxHighlightLoadJs);

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handles the ItemCommand event of the Inbox control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterCommandEventArgs" /> instance containing the event data.</param>
        protected void Inbox_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    if (this.IsOutbox)
                    {
                        CommonDb.pmessage_delete(PageContext.PageModuleID, e.CommandArgument, true);
                    }
                    else
                    {
                        CommonDb.pmessage_delete(PageContext.PageModuleID, e.CommandArgument);
                    }

                    this.BindData();
                    this.PageContext.AddLoadMessage(this.GetText("msg_deleted"), MessageTypes.Success);
                    YafBuildLink.Redirect(ForumPages.cp_pm);
                    break;
                case "reply":
                    YafBuildLink.Redirect(ForumPages.pmessage, "p={0}&q=0", e.CommandArgument);
                    break;
                case "quote":
                    YafBuildLink.Redirect(ForumPages.pmessage, "p={0}&q=1", e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // check if this feature is disabled
            if (!this.Get<YafBoardSettings>().AllowPrivateMessages)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.Disabled);
            }

            if (this.IsPostBack)
            {
                return;
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("pm").IsNotSet())
            {
                YafBuildLink.AccessDenied();
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.Get<YafBoardSettings>().EnableDisplayName
                    ? this.PageContext.CurrentUserData.DisplayName
                    : this.PageContext.PageUserName,
                YafBuildLink.GetLink(ForumPages.cp_profile));

            // handle custom YafBBCode javascript or CSS...
            this.Get<IBBCode>().RegisterCustomBBCodePageElements(this.Page, this.GetType());

            this.BindData();
        }

        /// <summary>
        /// The theme button delete_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ThemeButtonDelete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var themeButton = (ThemeButton)sender;
            themeButton.Attributes["onclick"] = "return confirm('{0}')".FormatWith(
                this.GetText("confirm_deletemessage"));
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            using (
                DataTable dt =
                    CommonDb.pmessage_list(PageContext.PageModuleID, Security.StringToLongOrRedirect(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("pm"))))
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // if the pm isn't from or two the current user--then it's access denied
                    if ((int)row["ToUserID"] != this.PageContext.PageUserID
                        && (int)row["FromUserID"] != this.PageContext.PageUserID)
                    {
                        YafBuildLink.AccessDenied();
                    }

                    this.SetMessageView(
                        row["FromUserID"],
                        row["ToUserID"],
                        Convert.ToBoolean(row["IsInOutbox"]),
                        Convert.ToBoolean(row["IsArchived"]));

                    // get the return link to the pm listing
                    if (this.IsOutbox)
                    {
                        this.PageLinks.AddLink(
                            this.GetText("SENTITEMS"), YafBuildLink.GetLink(ForumPages.cp_pm, "v=out"));
                    }
                    else if (this.IsArchived)
                    {
                        this.PageLinks.AddLink(
                            this.GetText("ARCHIVE"), YafBuildLink.GetLink(ForumPages.cp_pm, "v=arch"));
                    }
                    else
                    {
                        this.PageLinks.AddLink(this.GetText("INBOX"), YafBuildLink.GetLink(ForumPages.cp_pm));
                    }

                    this.PageLinks.AddLink(row["Subject"].ToString());

                    this.Inbox.DataSource = dt;
                }
                else
                {
                    YafBuildLink.Redirect(ForumPages.cp_pm);
                }
            }

            this.DataBind();

            if (this.IsOutbox)
            {
                return;
            }

            var userPmessageId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("pm").ToType<int>();

            CommonDb.pmessage_markread(PageContext.PageModuleID, userPmessageId);
            this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(this.PageContext.PageUserID));
            this.Get<IRaiseEvent>().Raise(
                new UpdateUserPrivateMessageEvent(this.PageContext.PageUserID, userPmessageId));
        }

        /// <summary>
        /// Sets the IsOutbox property as appropriate for this private message.
        /// </summary>
        /// <remarks>
        /// User id parameters are downcast to object to allow for potential future use of non-integer user id's
        /// </remarks>
        /// <param name="fromUserID">
        /// The from User ID.
        /// </param>
        /// <param name="toUserID">
        /// The to User ID.
        /// </param>
        /// <param name="messageIsInOutbox">
        /// Bool indicating whether the message is in the sender's outbox
        /// </param>
        /// <param name="messageIsArchived">
        /// The message Is Archived.
        /// </param>
        private void SetMessageView(
            [NotNull] object fromUserID, [NotNull] object toUserID, bool messageIsInOutbox, bool messageIsArchived)
        {
            bool isCurrentUserFrom = fromUserID.Equals(this.PageContext.PageUserID);
            bool isCurrentUserTo = toUserID.Equals(this.PageContext.PageUserID);

            // check if it's the same user...
            if (isCurrentUserFrom && isCurrentUserTo)
            {
                // it is... handle the view based on the query string passed
                this.IsOutbox = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("v") == "out";
                this.IsArchived = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("v") == "arch";

                // see if the message got deleted, if so, redirect to their outbox/archive
                if (this.IsOutbox && !messageIsInOutbox)
                {
                    YafBuildLink.Redirect(ForumPages.cp_pm, "v=out");
                }
                else if (this.IsArchived && !messageIsArchived)
                {
                    YafBuildLink.Redirect(ForumPages.cp_pm, "v=arch");
                }
            }
            else if (isCurrentUserFrom)
            {
                // see if it's been deleted by the from user...
                if (!messageIsInOutbox)
                {
                    // deleted for this user, redirect...
                    YafBuildLink.Redirect(ForumPages.cp_pm, "v=out");
                }
                else
                {
                    // nope
                    this.IsOutbox = true;
                }
            }
            else if (isCurrentUserTo)
            {
                // get the status for the receiver
                this.IsArchived = messageIsArchived;
                this.IsOutbox = false;
            }
        }

        #endregion
    }
}
