namespace YAF.Pages
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    /// <summary>
    /// The boardtags.
    /// </summary>
    public partial class boardtags : ForumPage
    {
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinksTop.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinksTop.AddLink(this.GetText("TAGSBOARD", "TITLE"), string.Empty);
            this.SearchByUserName.Text = this.GetText("SEARCH", "BTNSEARCH");
            this.ResetUserSearch.Text = this.GetText("SEARCH", "CLEAR");
            this.OKButton.Text = this.GetText("OK");

           // this.AlphaSort1.PagerPage = ForumPages.boardtags;

            this.BindData();
        }

        /// <summary>
        /// The ok button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void OKButton_Click(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.forum);
        }

        protected void TagList_OnItemDataBound(object sender, RepeaterItemEventArgs repeaterItemEventArgs)
        {
            // populate the controls here...
            if (repeaterItemEventArgs.Item.ItemType != ListItemType.Item && repeaterItemEventArgs.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            var currentRow = (DataRowView)repeaterItemEventArgs.Item.DataItem;

            // get the controls

            var tagLink = repeaterItemEventArgs.Item.FindControlRecursiveAs<HtmlAnchor>("TagLink");
            tagLink.HRef = YafBuildLink.GetLinkNotEscaped(
                ForumPages.topicsbytags,
                "tagid={0}&{1}".FormatWith(currentRow["TagID"], "b={0}".FormatWith(PageContext.PageBoardID)));
            tagLink.Attributes["alt"] = this.HtmlEncode(currentRow["Tag"]);
            tagLink.Title = this.HtmlEncode(currentRow["Tag"]);
            tagLink.Attributes["class"] = "tag90";
        }
        
        #region Public Methods

        /// <summary>
        /// The search_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void Search_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // re-bind data
            this.BindData();
        }

        /// <summary>
        /// The search_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void Reset_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // re-direct to self.
            YafBuildLink.Redirect(ForumPages.boardtags);
        }


        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Pager_PageChange(object sender, EventArgs e)
        {
            this.BindData();
        }

        #endregion

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {   
          
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("tag") != null)
            {
                this.UserSearchName.Text = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("tag")));
            }
           
            char selectedCharLetter = this.AlphaSort1.CurrentLetter;
            string selectedLetter;

            bool beginsWith = this.UserSearchName.Text.IsNotSet()
                              || !(selectedCharLetter == char.MinValue || selectedCharLetter == '#');
     

            selectedLetter = this.UserSearchName.Text.IsSet() ? this.UserSearchName.Text.Trim() : (!(selectedCharLetter == char.MinValue || selectedCharLetter == '#') ? selectedCharLetter.ToString(CultureInfo.InvariantCulture) : string.Empty);

            this.PagerTop.PageSize = this.Get<YafBoardSettings>().TopicsPerPage;

            using (var dtTopics = CommonDb.forum_tags(
                    this.PageContext.PageModuleID,
                    this.PageContext.PageBoardID,
                    this.PageContext.PageUserID,
                    0,
                    this.PagerTop.CurrentPageIndex,
                    this.PagerTop.PageSize,
                    selectedLetter,
                    beginsWith))
            {
               this.TagList.DataSource = dtTopics; 
               this.TagList.DataBind();
              // this.TagCloudBoard.TagsData = dtTopics;
              // this.TagCloudBoard.DataBind();
                if (dtTopics != null && dtTopics.Rows.Count > 0)
                {
                    this.PagerTop.Count = dtTopics.AsEnumerable().First().Field<int>("TotalCount");
                }
                
            }
        }


    }
}