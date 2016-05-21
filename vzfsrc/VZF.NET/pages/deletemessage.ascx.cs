namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Types.Objects;

    #endregion

    /// <summary>
    /// The Delete Message Page.
    /// </summary>
    public partial class deletemessage : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        ///   The _forum flags.
        /// </summary>
        protected ForumFlags _forumFlags;

        /// <summary>
        ///   The _is moderator changed.
        /// </summary>
        protected bool _isModeratorChanged;

        /// <summary>
        ///   The _message row.
        /// </summary>
        protected TypedMessageList _messageRow;

        /// <summary>
        ///   The _owner user id.
        /// </summary>
        protected int _ownerUserId;

        /// <summary>
        ///   The _topic flags.
        /// </summary>
        protected TopicFlags _topicFlags;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "deletemessage" /> class.
        /// </summary>
        public deletemessage()
            : base("DELETEMESSAGE")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets a value indicating whether CanDeletePost.
        /// </summary>
        public bool CanDeletePost
        {
            get
            {
                // Ederon : 9/9/2007 - moderators can delete in locked topics
                return ((!this.PostLocked && !this._forumFlags.IsLocked && !this._topicFlags.IsLocked
                         && (int)this._messageRow.UserID == this.PageContext.PageUserID)
                        || this.PageContext.ForumModeratorAccess) && this.PageContext.ForumDeleteAccess;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether CanUnDeletePost.
        /// </summary>
        public bool CanUnDeletePost
        {
            get
            {
                return this.PostDeleted && this.CanDeletePost;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PostDeleted.
        /// </summary>
        private bool PostDeleted
        {
            get
            {
                return this._messageRow.Flags.IsDeleted;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether PostLocked.
        /// </summary>
        private bool PostLocked
        {
            get
            {
                if (!this.PageContext.IsAdmin && this.Get<YafBoardSettings>().LockPosts > 0)
                {
                    var dateTime = this._messageRow.Edited;
                    if (dateTime != null)
                    {
                        var edited = (DateTime)dateTime;
                        if (edited.AddDays(this.Get<YafBoardSettings>().LockPosts) < DateTime.UtcNow)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The delete all posts_ checked changed 1.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void DeleteAllPosts_CheckedChanged1([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.ViewState["delAll"] = ((CheckBox)sender).Checked;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t") != null
                || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m") != null)
            {
                // reply to existing topic or editing of existing topic
                YafBuildLink.Redirect(ForumPages.posts, "t={0}", this.PageContext.PageTopicID);
            }
            else
            {
                // new topic -- cancel back to forum
                YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
            }
        }

        /// <summary>
        /// The get action text.
        /// </summary>
        /// <returns>
        /// Returns the Action Text
        /// </returns>
        protected string GetActionText()
        {
            return this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("action").ToLower() == "delete"
                       ? this.GetText("DELETE")
                       : this.GetText("UNDELETE");
        }

        /// <summary>
        /// The get reason text.
        /// </summary>
        /// <returns>
        /// Returns the reason text.
        /// </returns>
        protected string GetReasonText()
        {
            return
                this.GetText(
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("action").ToLower() == "delete"
                        ? "DELETE_REASON"
                        : "UNDELETE_REASON");
        }

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            /* get the forum editor based on the settings
      Message = yaf.editor.EditorHelper.CreateEditorFromType(PageContext.BoardSettings.ForumEditor);
      EditorLine.Controls.Add(Message); 
       */
            this.LinkedPosts.ItemDataBound += this.LinkedPosts_ItemDataBound;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this._messageRow = null;

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m") != null)
            {
                int messageId;
                if (!int.TryParse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"), out messageId))
                {
                    YafBuildLink.RedirectInfoPage(InfoMessage.Invalid);
                }

                this._messageRow = CommonDb.MessageList(PageContext.PageModuleID, messageId).FirstOrDefault();

                // first message should not be deleted
                if (this._messageRow == null || (int)this._messageRow.Position <= 0)
                {
                    YafBuildLink.RedirectInfoPage(InfoMessage.Invalid);
                }

                if (!this.PageContext.ForumModeratorAccess && this.PageContext.PageUserID != this._messageRow.UserID)
                {
                    YafBuildLink.AccessDenied();
                }
            }

            this._forumFlags = this._messageRow.ForumFlags;
            this._topicFlags = this._messageRow.TopicFlags;
            this._ownerUserId = (int)this._messageRow.UserID;
            this._isModeratorChanged = this.PageContext.PageUserID != this._ownerUserId;

            if (this.PageContext.PageForumID == 0)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t") == null
                && !this.PageContext.ForumPostAccess)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t") != null
                && !this.PageContext.ForumReplyAccess)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.IsPostBack)
            {
                return;
            }

            // setup page links
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.PageContext.PageCategoryName,
                YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
            this.PageLinks.AddForumLinks(this.PageContext.PageForumID);

            this.EraseMessage.Checked = false;
            this.ViewState["delAll"] = false;
            this.EraseRow.Visible = false;
            this.DeleteReasonRow.Visible = false;
            this.LinkedPosts.Visible = false;
            this.ReasonEditor.Attributes.Add("style", "width:100%");
            this.Cancel.Text = this.GetText("Cancel");

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m") == null)
            {
                return;
            }

            // delete message...
            this.PreviewRow.Visible = true;

            DataTable tempdb = CommonDb.message_getRepliesList(
                PageContext.PageModuleID,
                this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"));

            if (tempdb.Rows.Count > 0 && (this.PageContext.ForumModeratorAccess || this.PageContext.IsAdmin))
            {
                this.LinkedPosts.Visible = true;
                this.LinkedPosts.DataSource = tempdb;
                this.LinkedPosts.DataBind();
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("action").ToLower() == "delete")
            {
                this.Title.Text = this.GetText("EDIT"); // GetText("EDIT");
                this.Delete.Text = this.GetText("DELETE"); // "GetText("Save");

                if (this.PageContext.IsAdmin)
                {
                    this.EraseRow.Visible = true;
                }
            }
            else
            {
                this.Title.Text = this.GetText("EDIT");
                this.Delete.Text = this.GetText("UNDELETE"); // "GetText("Save");
            }

            this.Subject.Text = Convert.ToString(this._messageRow.Topic);
            this.DeleteReasonRow.Visible = true;
            this.ReasonEditor.Text = Convert.ToString(this._messageRow.DeleteReason);

            // populate the message preview with the message datarow...
            this.MessagePreview.Message = this._messageRow.Message;
            this.MessagePreview.MessageFlags = this._messageRow.Flags;
        }

        /// <summary>
        /// The toogle delete status_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void ToogleDeleteStatus_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.CanDeletePost)
            {
                return;
            }

            // Create objects for easy access
            object tmpMessageId = this._messageRow.MessageID;
            object tmpForumId = this._messageRow.ForumID;
            object tmpTopicId = this._messageRow.TopicID;

            // Toogle delete message -- if the message is currently deleted it will be undeleted.
            // If it's not deleted it will be marked deleted.
            // If it is the last message of the topic, the topic is also deleted
            CommonDb.message_delete(
                PageContext.PageModuleID,
                tmpMessageId,
                this._isModeratorChanged,
                HttpUtility.HtmlEncode(this.ReasonEditor.Text),
                this.PostDeleted ? 0 : 1,
                (bool)this.ViewState["delAll"],
                this.EraseMessage.Checked);

            // retrieve topic information.
            DataRow topic = CommonDb.topic_info(this.PageContext.PageModuleID, tmpTopicId, true, false);

            // If topic has been deleted, redirect to topic list for active forum, else show remaining posts for topic
            if (topic == null)
            {
                YafBuildLink.Redirect(ForumPages.topics, "f={0}", tmpForumId);
            }
            else
            {
                YafBuildLink.Redirect(ForumPages.posts, "t={0}", tmpTopicId);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// The linked posts_ item data bound.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LinkedPosts_ItemDataBound([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                return;
            }

            var deleteAllPosts = (CheckBox)e.Item.FindControl("DeleteAllPosts");
            deleteAllPosts.Checked =
                deleteAllPosts.Enabled = this.PageContext.ForumModeratorAccess || this.PageContext.IsAdmin;
            this.ViewState["delAll"] = deleteAllPosts.Checked;
        }

        #endregion
    }
}