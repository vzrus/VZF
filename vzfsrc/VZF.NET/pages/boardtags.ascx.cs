namespace YAF.Pages
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;

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
            if (!this.IsPostBack)
            {
                this.PageLinksTop.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                this.PageLinksTop.AddLink(this.GetText("TAGSBOARD", "TITLE"), string.Empty);
                this.SearchByUserName.Text = this.GetText("SEARCH", "BTNSEARCH");
                this.ResetUserSearch.Text = this.GetText("SEARCH", "CLEAR");
                this.OKButton.Text = this.GetText("OK");
                this.AlphaSort1.PagerPage = ForumPages.boardtags;
                this.BindData();
            }
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
          //  this.BindData();
             HttpContext.Current.Response.Charset = Encoding.UTF8.WebName;
            YafBuildLink.Redirect(ForumPages.boardtags, "tag={0}".FormatWith(HttpUtility.HtmlEncode(HttpUtility.UrlEncode(this.UserSearchName.Text.Trim()))));
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
                this.TagCloudBoard.TagsData = dtTopics;
               
                if (dtTopics != null && dtTopics.Rows.Count > 0)
                {
                    this.PagerTop.Count = dtTopics.AsEnumerable().First().Field<int>("TotalCount");
                }
            }
        }
    }
}