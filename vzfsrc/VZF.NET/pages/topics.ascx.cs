﻿namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Data.DAL;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The topics list page
    /// </summary>
    public partial class topics : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        ///   The _show topic list selected.
        /// </summary>
        private int _showTopicListSelected;

        /// <summary>
        ///   The _forum.
        /// </summary>
        private DataRow _forum;

        /// <summary>
        ///   The _forum flags.
        /// </summary>
        private ForumFlags _forumFlags;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "topics" /> class. 
        ///   Overloads the topics page.
        /// </summary>
        public topics()
            : base("TOPICS")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the last post image TT.
        /// </summary>
        /// <value>
        /// The last post image TT.
        /// </value>
        public string LastPostImageTT { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The style transform func wrap.
        /// </summary>
        /// <param name="dt">
        /// The DateTable
        /// </param>
        /// <returns>
        /// The style transform wrap.
        /// </returns>
        public DataTable StyleTransformDataTable([NotNull] DataTable dt)
        {
            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                var styleTransform = this.Get<IStyleTransform>();
                styleTransform.DecodeStyleByTable(ref dt, false, "StarterStyle", "LastUserStyle");
            }

            return dt;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the sub forum title.
        /// </summary>
        /// <returns>The get sub forum title.</returns>
        protected string GetSubForumTitle()
        {
            return this.GetTextFormatted("SUBFORUMS", this.HtmlEncode(this.PageContext.PageForumName));
        }

        /// <summary>
        /// The new topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void NewTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this._forumFlags.IsLocked)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_FORUM_LOCKED"));
                return;
            }

            if (!this.PageContext.ForumPostAccess)
            {
                YafBuildLink.AccessDenied(/*"You don't have access to post new topics in this forum."*/);
            }
        }

        /// <summary>
        /// The Forum Search
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ForumSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.forumSearch.Text))
            {
                return;
            }

            YafBuildLink.Redirect(
                ForumPages.search,
                "search={0}&forum={1}",
                HttpUtility.UrlEncode(this.forumSearch.Text.TrimWordsOverMaxLengthWordsPreserved(
                    this.Get<YafBoardSettings>().SearchStringMaxLength)),
                this.PageContext.PageForumID);
        }

        /// <summary>
        /// The initialization script for the topics page.
        /// </summary>
        /// <param name="e">
        /// The EventArgs object for the topics page.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.Unload += this.Topics_Unload;

            this.ShowList.SelectedIndexChanged += this.ShowList_SelectedIndexChanged;
            this.MarkRead.Click += this.MarkRead_Click;
            this.Pager.PageChange += this.Pager_PageChange;
            this.NewTopic1.Click += this.NewTopic_Click;
            this.NewTopic2.Click += this.NewTopic_Click;
            this.WatchForum.Click += this.WatchForum_Click;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            base.OnInit(e);
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Request.QueryString.GetFirstOrDefault("f") == null)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.PageContext.IsGuest && !this.PageContext.ForumReadAccess)
            {
                // attempt to get permission by redirecting to login...
                this.Get<IPermissions>().HandleRequest(ViewPermissions.RegisteredUsers);
            }
            else if (!this.PageContext.ForumReadAccess)
            {
                YafBuildLink.AccessDenied();
            }
            this.Stc1.ForumId = PageContext.PageForumID;
            this.Get<IYafSession>().UnreadTopics = 0;
            this.AtomFeed.AdditionalParameters =
                "f={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f"));
            this.RssFeed.AdditionalParameters =
                "f={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f"));
            this.MarkRead.Text = this.GetText("MARKREAD");
            this.ForumJumpHolder.Visible = this.Get<YafBoardSettings>().ShowForumJump
                                           && this.PageContext.Settings.LockedForum == 0;

            this.LastPostImageTT = this.GetText("DEFAULT", "GO_LAST_POST");

            if (this.ForumSearchHolder.Visible)
            {
                this.forumSearch.Attributes["onkeydown"] =
                    "if(event.which || event.keyCode){{if ((event.which == 13) || (event.keyCode == 13)) {{document.getElementById('{0}').click();return false;}}}} else {{return true}}; "
                        .FormatWith(this.forumSearchOK.ClientID);
            }

            if (!this.IsPostBack)
            {
                // PageLinks.Clear();
                if (this.PageContext.Settings.LockedForum == 0)
                {
                    this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                    this.PageLinks.AddLink(
                        this.PageContext.PageCategoryName,
                        YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
                }

                this.PageLinks.AddForumLinks(this.PageContext.PageForumID, true);

                this.ShowList.DataSource = StaticDataHelper.TopicTimes();
                this.ShowList.DataTextField = "TopicText";
                this.ShowList.DataValueField = "TopicValue";
                this._showTopicListSelected = (this.Get<IYafSession>().ShowList == -1)
                                                  ? this.Get<YafBoardSettings>().ShowTopicsDefault
                                                  : this.Get<IYafSession>().ShowList;

                this.moderate1.NavigateUrl =
                    this.moderate2.NavigateUrl =
                    YafBuildLink.GetLinkNotEscaped(ForumPages.moderating, "f={0}", this.PageContext.PageForumID);

                this.NewTopic1.NavigateUrl =
                    this.NewTopic2.NavigateUrl =
                    YafBuildLink.GetLinkNotEscaped(ForumPages.postmessage, "f={0}", this.PageContext.PageForumID);

                this.HandleWatchForum();
            }

            using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageForumID))
            {
                this._forum = dt.Rows[0];
            }

            if (this._forum["RemoteURL"] != DBNull.Value)
            {
                this.Response.Clear();
                this.Response.Redirect((string)this._forum["RemoteURL"]);
            }

            this._forumFlags = new ForumFlags(this._forum["Flags"]);

            this.PageTitle.Text = this.HtmlEncode(this._forum["Name"]);

            this.BindData(); // Always because of yaf:TopicLine

            if (!this.PageContext.ForumPostAccess
                || (this._forumFlags.IsLocked && !this.PageContext.ForumModeratorAccess))
            {
                this.NewTopic1.Visible = false;
                this.NewTopic2.Visible = false;
            }

            if (this.PageContext.ForumModeratorAccess)
            {
                return;
            }

            
            this.moderate1.Visible = false;
            this.moderate2.Visible = false;
        }

        /// <summary>
        /// The watch forum_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void WatchForum_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumReadAccess)
            {
                return;
            }

            if (this.PageContext.IsGuest)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_LOGIN_FORUMWATCH"));
                return;
            }

            if (this.WatchForumID.InnerText == string.Empty)
            {
                CommonDb.watchforum_add(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageForumID);

                this.PageContext.AddLoadMessage(this.GetText("INFO_WATCH_FORUM"));
            }
            else
            {
                var tmpID = this.WatchForumID.InnerText.ToType<int>();
                CommonDb.watchforum_delete(PageContext.PageModuleID, tmpID);

                this.PageContext.AddLoadMessage(this.GetText("INFO_UNWATCH_FORUM"));
            }

            this.HandleWatchForum();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            DataSet ds = this.Get<IDBBroker>().BoardLayout(
                this.PageContext.PageBoardID,
                this.PageContext.PageUserID,
                this.PageContext.PageCategoryID,
                this.PageContext.PageForumID);
            if (ds.Tables[SqlDbAccess.GetVzfObjectName("Forum", YafContext.Current.PageModuleID)].Rows.Count > 0)
            {
                this.ForumList.DataSource = ds.Tables[SqlDbAccess.GetVzfObjectName("Forum", YafContext.Current.PageModuleID)].Rows;
                this.SubForums.Visible = true;
            }

            this.Pager.PageSize = PageContext.TopicsPerPage;

            // when userId is null it returns the count of all deleted messages
            int? userId = null;

            // get the userID to use for the deleted posts count...
            if (!this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
            {
                // only show deleted messages that belong to this user if they are not admin/mod
                if (!this.PageContext.IsAdmin && !this.PageContext.ForumModeratorAccess)
                {
                    userId = this.PageContext.PageUserID;
                }
            }

            DataTable dt = CommonDb.announcements_list(
                this.PageContext.PageModuleID,
                this.PageContext.PageForumID,
                userId,
                null,
                DateTime.UtcNow,
                0,
                10,
                this.Get<YafBoardSettings>().UseStyledNicks,
                true,
                this.Get<YafBoardSettings>().ShowDeletedTopicsInTopicListForModerators && this.PageContext.ForumModeratorAccess,
                this.Get<YafBoardSettings>().UseReadTrackingByDatabase,
                this.Get<YafBoardSettings>().AllowTopicTags);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (this.Get<YafBoardSettings>().UseStyledNicks)
                {
                    dt = this.StyleTransformDataTable(dt);
                }

                this.Announcements.DataSource = dt;
            }

            DateTime date;
            if (this._showTopicListSelected == 0)
            {
                date = DateTimeHelper.SqlDbMinTime();
            }
            else
            {
                var days = new[] { 1, 2, 7, 14, 31, 2 * 31, 6 * 31, 356 };

                date = DateTime.UtcNow.AddDays(-days[this._showTopicListSelected]);
            }

            DataTable dtTopics = CommonDb.topic_list(
                this.PageContext.PageModuleID,
                this.PageContext.PageForumID,
                userId,
                date,
                DateTime.UtcNow,
                this.Pager.CurrentPageIndex,
                this.PageContext.TopicsPerPage,
                this.Get<YafBoardSettings>().UseStyledNicks,
                true,
                this.Get<YafBoardSettings>().ShowDeletedTopicsInTopicListForModerators && this.PageContext.ForumModeratorAccess,
                this.Get<YafBoardSettings>().UseReadTrackingByDatabase,
                this.Get<YafBoardSettings>().AllowTopicTags);

            if (dtTopics != null)
            {
                dtTopics = this.StyleTransformDataTable(dtTopics);
                this.TopicList.DataSource = dtTopics;
            }

            this.DataBind();

            // setup the show topic list selection after data binding
            this.ShowList.SelectedIndex = this._showTopicListSelected;
            this.Get<IYafSession>().ShowList = this._showTopicListSelected;
            if (dtTopics != null && dtTopics.Rows.Count > 0)
            {
                this.Pager.Count = dtTopics.AsEnumerable().First().Field<int>("TotalRows");
            }
        }

        /// <summary>
        /// The handle watch forum.
        /// </summary>
        private void HandleWatchForum()
        {
            if (this.PageContext.IsGuest || !this.PageContext.ForumReadAccess)
            {
                return;
            }

            // check if this forum is being watched by this user
            using (DataTable dt = CommonDb.watchforum_check(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageForumID))
            {
                if (dt.Rows.Count > 0)
                {
                    // subscribed to this forum
                    this.WatchForum.Text = this.GetText("unwatchforum");

                    foreach (DataRow row in dt.Rows)
                    {
                        this.WatchForumID.InnerText = row["WatchForumID"].ToString();
                        break;
                    }
                }
                else
                {
                    // not subscribed
                    this.WatchForumID.InnerText = string.Empty;
                    this.WatchForum.Text = this.GetText("watchforum");
                }
            }
        }

        /// <summary>
        /// The mark read_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MarkRead_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<IReadTrackCurrentUser>().SetForumRead(this.PageContext.PageForumID);
            this.BindData();
        }

        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SmartScroller1.Reset();
            this.BindData();
        }

        /// <summary>
        /// The show list_ selected index changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowList_SelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
        {
            this._showTopicListSelected = this.ShowList.SelectedIndex;
            this.BindData();
        }

        /// <summary>
        /// The Topics unload.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Topics_Unload([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Get<IYafSession>().UnreadTopics == 0)
            {
                this.Get<IReadTrackCurrentUser>().SetForumRead(this.PageContext.PageForumID);
            }
        }

        /// <summary>
        /// The ForumID.
        /// </summary>
        /// <returns>
        /// Returns The ForumID.
        /// </returns>
        protected int GetForumId()
        {
            bool ff = !this._forum["ForumID"].IsNullOrEmptyDBField();
            int fi = !this._forum["ForumID"].IsNullOrEmptyDBField() ? this._forum["ForumID"].ToType<int>() : 0;
            return fi;
        }

        #endregion

        protected void TopicList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }
        }
    }
}