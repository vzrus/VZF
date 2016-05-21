using VZF.Utils.Helpers;

namespace YAF.Pages
{
    // YAF.Pages

    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Forum Moderating Page.
    /// </summary>
    public partial class moderating : ForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "moderating" /> class.
        /// </summary>
        public moderating()
            : base("MODERATING")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add user_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void AddUser_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.mod_forumuser, "f={0}", this.PageContext.PageForumID);
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        protected void BindData()
        {
            using (
                DataTable dtAnnouncments = CommonDb.announcements_list(
                    this.PageContext.PageModuleID,
                    this.PageContext.PageForumID,
                    null,
                    null,
                    DateTime.UtcNow,
                    0,
                    10,
                    this.Get<YafBoardSettings>().UseStyledNicks,
                    true,
                    this.Get<YafBoardSettings>().ShowDeletedTopicsInTopicListForModerators
                    && this.PageContext.ForumModeratorAccess,
                    this.Get<YafBoardSettings>().UseReadTrackingByDatabase,
                    this.Get<YafBoardSettings>().AllowTopicTags))
            {
                if (dtAnnouncments != null && dtAnnouncments.Rows.Count > 0)
                {
                    this.Announcements.DataSource = dtAnnouncments;

                }
            }

            this.PagerTop.PageSize = PageContext.TopicsPerPage;
           
            DataTable dt = CommonDb.topic_list(this.PageContext.PageModuleID, this.PageContext.PageForumID,
                null,
                DateTimeHelper.SqlDbMinTime(),
                DateTime.UtcNow,
                this.PagerTop.CurrentPageIndex,
                this.PagerTop.PageSize,
                false,
                true, 
                this.PageContext.ForumModeratorAccess,
                false,
                this.Get<YafBoardSettings>().AllowTopicTags);

            this.topiclist.DataSource = dt;
            this.UserList.DataSource = CommonDb.userforum_list(PageContext.PageModuleID, null, this.PageContext.PageForumID);
            this.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                this.PagerTop.Count = dt.AsEnumerable().First().Field<int>("TotalRows");
            }
        }

        /// <summary>
        /// The delete topics_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void DeleteTopics_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var list =
                this.topiclist.Controls.OfType<RepeaterItem>().SelectMany(x => x.Controls.OfType<TopicLine>()).Where(
                    x => x.IsSelected && x.TopicRowID.HasValue).ToList();

            list.ForEach(x => CommonDb.topic_delete(PageContext.PageModuleID, x.TopicRowID, null, false));

            this.PageContext.AddLoadMessage(this.GetText("moderate", "deleted"));
            this.BindData();
        }

        protected void RestoreTopics_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var list =
                this.topiclist.Controls.OfType<RepeaterItem>().SelectMany(x => x.Controls.OfType<TopicLine>()).Where(
                    x => x.IsSelected && x.TopicRowID.HasValue).ToList();

            list.ForEach(x => CommonDb.topic_restore(PageContext.PageModuleID, x.TopicRowID,  PageContext.PageUserID));

            this.PageContext.AddLoadMessage(this.GetText("moderate", "restored"));
            this.BindData();
        }

        /// <summary>
        /// The erase topics_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void EraseTopics_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var list =
                this.topiclist.Controls.OfType<RepeaterItem>().SelectMany(x => x.Controls.OfType<TopicLine>()).Where(
                    x => x.IsSelected && x.TopicRowID.HasValue).ToList();

            list.ForEach(x => CommonDb.topic_delete(PageContext.PageModuleID, x.TopicRowID, x.TopicMovedID(), true));

            this.PageContext.AddLoadMessage(this.GetText("moderate", "deleted"));
            this.BindData();
        }


        /// <summary>
        /// The delete user_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DeleteUser_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("moderate", "confirm_delete_user"));
        }

        /// <summary>
        /// The delete_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("moderate", "confirm_delete"));
        }

        /// <summary>
        /// The erase_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Erase_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("moderate", "CONFIRM_ERASE"));
        }

        protected void Restore_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("moderate", "CONFIRM_RESTORE"));
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.RestoreTopic.Visible = this.RestoreTopic2.Visible =
            this.EraseTopic.Visible = this.EraseTopic2.Visible = this.PageContext.IsAdmin;
            if (!this.PageContext.ForumModeratorAccess)
            {
            var forumId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f");
                if (forumId != null)
                {
                    using (
                        var dt = CommonDb.forum_byuserlist(
                            PageContext.PageModuleID, PageContext.PageBoardID, forumId, PageContext.PageUserID, true))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            YafBuildLink.AccessDenied(); 
                        }
                    }
                }
                else
                {
                    YafBuildLink.AccessDenied();
                }
            }

            if (!this.IsPostBack)
            {
                if (this.PageContext.Settings.LockedForum == 0)
                {
                    this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                    this.PageLinks.AddLink(
                        this.PageContext.PageCategoryName,
                        YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
                }

                this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
                this.PageLinks.AddLink(this.GetText("MODERATE", "TITLE"), string.Empty);

                this.PagerTop.PageSize = 25;
            }

            this.BindData();
        }

        /// <summary>
        /// The pager top_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PagerTop_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            // rebind
            this.BindData();
        }

        /// <summary>
        /// The user list_ item command.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void UserList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "edit":
                    YafBuildLink.Redirect(
                        ForumPages.mod_forumuser, "f={0}&u={1}", this.PageContext.PageForumID, e.CommandArgument);
                    break;
                case "remove":
                    CommonDb.userforum_delete(PageContext.PageModuleID, e.CommandArgument, this.PageContext.PageForumID);
                    this.BindData();

                    // clear moderators cache
                    this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);
                    break;
            }
        }

        /// <summary>
        /// The topiclist_ item command.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void topiclist_ItemCommand([NotNull] object sender, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    CommonDb.topic_delete(PageContext.PageModuleID, e.CommandArgument);
                    this.PageContext.AddLoadMessage(this.GetText("deleted"));
                    this.BindData();
                    break;
            }
        }

        #endregion
    }
}