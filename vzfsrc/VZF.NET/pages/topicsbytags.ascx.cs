// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="topicsbytags.ascx.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The topics by a specific tag.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;

    using VZF.Data.Common;
    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The topics list page
    /// </summary>
    public partial class topicsbytags : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        /// The forum idb.
        /// </summary>
        protected int forumIdb;

        /// <summary>
        /// The topic idb.
        /// </summary>
        protected int topicIdb;

        /// <summary>
        /// The board idb.
        /// </summary>
        protected int boardIdb;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "topics" /> class. 
        ///   Overloads the topics page.
        /// </summary>
        public topicsbytags()
            : base("TOPICSBYTAGS")
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

        /// <summary>
        /// Gets or sets the ret btn args.
        /// </summary>
        public string retBtnArgs { get; set; }

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
        /// The initialization script for the topics page.
        /// </summary>
        /// <param name="e">
        /// The EventArgs object for the topics page.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
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
            int tagId;
            if (this.Request.QueryString.GetFirstOrDefault("tagid") == null && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("tagid"), out tagId))
            {
                YafBuildLink.AccessDenied();
            }

            int topicId;
            if (this.Request.QueryString.GetFirstOrDefault("t") != null && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("t"), out topicId))
            {
                YafBuildLink.AccessDenied();
            }

            int forumId;
            if (this.Request.QueryString.GetFirstOrDefault("f") != null && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("f"), out forumId))
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

                // this.PageLinks.AddLink(this.GetText("TOPICSBYTAGS","TITLE"));
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

            // get the userID to use for the deleted posts count...
            if (!this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
            {
                // only show deleted messages that belong to this user if they are not admin/mod
                if (!this.PageContext.IsAdmin && !this.PageContext.ForumModeratorAccess)
                {
                }
            }

            int forumId = 0;
            bool ok = this.Request.QueryString.GetFirstOrDefault("f") != null
            && !int.TryParse(this.Request.QueryString.GetFirstOrDefault("f"), out forumId);
           
            if (this.Request.QueryString.GetFirstOrDefault("f") != null && int.TryParse(this.Request.QueryString.GetFirstOrDefault("f"), out this.forumIdb))
            {
                this.retBtnArgs = "f={0}".FormatWith(this.forumIdb);
            }

            if (this.Request.QueryString.GetFirstOrDefault("t") != null && int.TryParse(this.Request.QueryString.GetFirstOrDefault("t"), out topicIdb))
            {
                this.retBtnArgs = "t={0}".FormatWith(this.topicIdb);
            }

            if (this.Request.QueryString.GetFirstOrDefault("b") != null && int.TryParse(this.Request.QueryString.GetFirstOrDefault("b"), out this.boardIdb))
            {
                this.retBtnArgs = "b={0}".FormatWith(this.boardIdb);
            }

            DataTable dtTopics = CommonDb.topic_bytags(
                PageContext.PageModuleID,
                this.PageContext.PageBoardID,
                forumId,
                this.PageContext.PageUserID,
                this.Request.QueryString.GetFirstOrDefault("tagid"),
                                                     DateTime.MinValue.AddYears(1902),
                                                     this.Pager.CurrentPageIndex,
                                                     this.Get<YafBoardSettings>().TopicsPerPage);
           
            if (dtTopics != null && dtTopics.Rows.Count > 0)
            {
                dtTopics = this.StyleTransformDataTable(dtTopics);
                this.TopicList.DataSource = dtTopics;
                this.TagsListLLbl.Param0 = dtTopics.Rows[0]["Tags"].ToString();
            }

            this.DataBind();
           
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
        /// The ok btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void okBtn_click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.OKButon.CommandArgument.Contains("f="))
            {
                YafBuildLink.Redirect(ForumPages.topics, "{0}".FormatWith(this.OKButon.CommandArgument));
            }

            if (this.OKButon.CommandArgument.Contains("t="))
            {
                YafBuildLink.Redirect(ForumPages.posts, "{0}".FormatWith(this.OKButon.CommandArgument));
            }

            YafBuildLink.Redirect(ForumPages.forum);
        }

        #endregion
    }
}