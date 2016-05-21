namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using YAF.Types.Constants;

    #endregion

    /// <summary>
    /// The last posts.
    /// </summary>
    public partial class LastPosts : BaseUserControl
    {
        #region Properties

        /// <summary>
        ///   Gets or sets TopicID.
        /// </summary>
        public long? TopicID
        {
            get
            {
                if (this.ViewState["TopicID"] != null)
                {
                    return this.ViewState["TopicID"].ToType<int>();
                }

                return null;
            }

            set
            {
                this.ViewState["TopicID"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The last post update timer_ tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void LastPostUpdateTimer_Tick([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// The on pre render.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            this.PageContext.PageElements.RegisterJsBlockStartup(
                this.LastPostUpdatePanel,
                "DisablePageManagerScrollJs",
                JavaScriptBlocks.DisablePageManagerScrollJs);

            if (this.PageContext.ForumPageType == ForumPages.postmessage)
            {
                var editorId = this.Get<YafBoardSettings>().AllowUsersTextEditor && this.PageContext.TextEditor.IsSet()
                                   ? this.PageContext.TextEditor
                                   : this.Get<YafBoardSettings>().ForumEditor;

                // Check if Editor exists, if not fallback to default editorid=1
                var forumEditor = this.Get<IModuleManager<ForumEditor>>().GetBy(editorId, false)
                                  ?? this.Get<IModuleManager<ForumEditor>>().GetBy("1");

                if (forumEditor.Description.Contains("CKEditor") || forumEditor.Description.Contains("DotNetNuke"))
                {
                    this.LastPostUpdateTimer.Enabled = false;
                }
            }

            base.OnPreRender(e);
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
            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (this.TopicID.HasValue)
            {
                bool showDeleted = false;
                int userId = 0;

                if (this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
                {
                    showDeleted = true;
                }

                if (!showDeleted
                    && (this.Get<YafBoardSettings>().ShowDeletedMessages
                        && !this.Get<YafBoardSettings>().ShowDeletedMessagesToAll) || this.PageContext.IsAdmin
                    || this.PageContext.IsForumModerator)
                {
                    userId = this.PageContext.PageUserID;
                }

                DataTable dt = CommonDb.post_list(
                    PageContext.PageModuleID,
                    this.TopicID,
                    this.PageContext.PageUserID,
                    userId,
                    false,
                    showDeleted,
                    true,
                    false,
                    DateTimeHelper.SqlDbMinTime(),
                    DateTime.UtcNow,
                    DateTimeHelper.SqlDbMinTime(),
                    DateTime.UtcNow,
                    0,
                    10,
                    2,
                    0,
                    0,
                    false,
                    -1,
                    -1,
                    DateTimeHelper.SqlDbMinTime());

                this.repLastPosts.DataSource = dt.AsEnumerable();
            }
            else
            {
                this.repLastPosts.DataSource = null;
            }

            this.repLastPosts.DataBind();
        }

        #endregion

        /// <summary>
        /// The rep last posts_ on item data bound.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void repLastPosts_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            var dt = (DataRow)e.Item.DataItem;
            this.FindControlRecursiveAs<UserLink>("ProfileLink").ReplaceName =
                this.Get<YafBoardSettings>().EnableDisplayName
                && (!dt["IsGuest"].ToType<bool>()
                    || (dt["IsGuest"].ToType<bool>() && dt["DisplayName"].ToString() == dt["UserName"].ToString()))
                    ? dt["DisplayName"].ToString()
                    : dt["UserName"].ToString();
            this.FindControlRecursiveAs<UserLink>("ProfileLink").PostfixText = dt["IP"].ToString()
                                                                                                == "NNTP"
                                                                                                    ? this.GetText(
                                                                                                        "EXTERNALUSER")
                                                                                                    : string.Empty;
        }
    }
}