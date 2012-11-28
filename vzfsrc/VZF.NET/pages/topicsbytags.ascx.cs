/* Yet Another Forum.net
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The topics list page
    /// </summary>
    public partial class topicsbytags : ForumPage
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
        public topicsbytags()
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
        /// The initialization script for the topics page.
        /// </summary>
        /// <param name="e">
        /// The EventArgs object for the topics page.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.Unload += this.Topics_Unload;
            this.ShowList.SelectedIndexChanged += this.ShowList_SelectedIndexChanged;
            this.Pager.PageChange += this.Pager_PageChange;
           

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
            int tagId = 0;
            if (this.Request.QueryString.GetFirstOrDefault("tagid") == null && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("tagid"), out tagId))
            {
                YafBuildLink.AccessDenied();
            }
            int topicId = 0;
            if (this.Request.QueryString.GetFirstOrDefault("t") != null && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("t"), out topicId))
            {
                YafBuildLink.AccessDenied();
            }

            this.Get<IYafSession>().UnreadTopics = 0;
            this.ForumJumpHolder.Visible = this.Get<YafBoardSettings>().ShowForumJump;

            this.LastPostImageTT = this.GetText("DEFAULT", "GO_LAST_POST");

           if (!this.IsPostBack)
            {
                // PageLinks.Clear();
                    this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                  //  this.PageLinks.AddLink(this.GetText("TOPICSBYTAGS","TITLE"));
                
                this.ShowList.DataSource = StaticDataHelper.TopicTimes();
                this.ShowList.DataTextField = "TopicText";
                this.ShowList.DataValueField = "TopicValue";
                this._showTopicListSelected = (this.Get<IYafSession>().ShowList == -1)
                                                  ? this.Get<YafBoardSettings>().ShowTopicsDefault
                                                  : this.Get<IYafSession>().ShowList;
                this.TagsListLLbl.LocalizedPage = "TOPICSBYTAGS";
                this.TagsListLLbl.LocalizedTag = "TAGS_LIST";
            }


            this.PageTitle.Text = this.GetText("TOPICSBYTAGS", "TITLE");

            this.BindData(); // Always because of yaf:TopicLine

           
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.Pager.PageSize = this.Get<YafBoardSettings>().TopicsPerPage;

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

            DateTime date;
            if (this._showTopicListSelected == 0)
            {
                date = DateTimeHelper.SqlDbMinTime();
            }
            else
            {
                int[] days = new[] { 1, 2, 7, 14, 31, 2 * 31, 6 * 31, 356 };

                date = DateTime.UtcNow.AddDays(-days[this._showTopicListSelected]);
            }
            
            DataTable dtTopics = CommonDb.topic_bytags(PageContext.PageModuleID, this.PageContext.PageBoardID,
                                                     this.PageContext.PageUserID,
                                                     this.Request.QueryString.GetFirstOrDefault("tagid"),
                                                     date,
                                                     this.Pager.CurrentPageIndex,
                                                     this.Get<YafBoardSettings>().TopicsPerPage);
            if (dtTopics != null && dtTopics.Rows.Count > 0)
            {
                dtTopics = this.StyleTransformDataTable(dtTopics);
                this.TopicList.DataSource = dtTopics;
                this.TagsListLLbl.Param0 = dtTopics.Rows[0]["Tags"].ToString();
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

        #endregion
    }
}