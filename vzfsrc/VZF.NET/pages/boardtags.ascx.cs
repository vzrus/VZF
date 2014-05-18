// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="boardtags.ascx.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
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
//   The boardtags.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace YAF.Pages
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The board tags.
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
        protected void OkButtonClick(object sender, EventArgs e)
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
        protected void Search_Click([NotNull] object sender, [NotNull] EventArgs e)
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
        protected void Reset_Click([NotNull] object sender, [NotNull] EventArgs e)
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

        /// <summary>
        /// The get tag class.
        /// </summary>
        /// <param name="tagFrequency">
        /// The tag frequency.
        /// </param>
        /// <param name="highestFrequency">
        /// The highest frequency.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetTagClass(int tagFrequency, int highestFrequency)
        {
            if (tagFrequency == 0 || highestFrequency == 0)
            {
                return "tag0";
            }

            var percentageFrequency = (tagFrequency * 100) / highestFrequency;

            if (percentageFrequency >= 90)
            {
                return "tag{0}".FormatWith(90);
            }

            if (percentageFrequency >= 80)
            {
                return "tag80";
            }

            if (percentageFrequency >= 70)
            {
                return "tag70";
            }

            if (percentageFrequency >= 60)
            {
                return "tag60";
            }

            if (percentageFrequency >= 50)
            {
                return "tag50";
            }

            if (percentageFrequency >= 40)
            {
                return "tag40";
            }

            if (percentageFrequency >= 30)
            {
                return "tag30";
            }

            if (percentageFrequency >= 20)
            {
                return "tag20";
            }

            if (percentageFrequency >= 10)
            {
                return "tag10";
            }

            if (percentageFrequency >= 1)
            {
                return "tag1";
            }

            return null;
        }

        /// <summary>
        /// The get tag cloud html.
        /// </summary>
        /// <param name="tagNameWithFrequencies">
        /// The tag Name With Frequencies.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetTagCloudHtml(DataTable tagNameWithFrequencies)
        {
            if (tagNameWithFrequencies == null || tagNameWithFrequencies.Rows.Count <= 0)
            {
                return null;
            }

            string tagLabel = null;
            if (this.Get<YafBoardSettings>().AllowTopicTags && tagNameWithFrequencies.Rows.Count > 0)
            {
                var tagsTopicLineIcon = this.Get<ITheme>().GetItem("ICONS", "TOPIC_TAG");

                if (tagsTopicLineIcon.IsSet())
                {
                    tagLabel = "<img src=\"{0}\" alt=\"{1}\" title=\"{1}\" style=\"border: 0;width:16px;height:16px\" />&nbsp;"
                            .FormatWith(tagsTopicLineIcon, this.Get<ILocalization>().GetText("POSTS", "TAGS_POSTS"));
                }
            }

            var tagCloudString = new StringBuilder();
            int tagCount = 0;
            foreach (DataRow tag in tagNameWithFrequencies.Rows)
            {
                string tagClass = ".tagcloud " + GetTagClass(tag["TagCount"].ToType<int>(), tag["MaxTagCount"].ToType<int>());

                string numberOfTags = null;
                if (this.Get<YafBoardSettings>().ShowNumberOfTags)
                {
                    numberOfTags = "({0})".FormatWith(tag["TagCount"].ToType<int>());
                }

                string addQweryParams = "b={0}".FormatWith(this.PageContext.PageBoardID);

                string targetUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.topicsbytags, "tagid={0}&{1}".FormatWith(tag["TagID"], addQweryParams));
                string tagItem = "<a class=\"{0}\" href=\"{1}\">{2}{3}</a>&nbsp;&nbsp;".FormatWith(
                    tagClass, targetUrl, this.HtmlEncode(tag["Tag"]), numberOfTags);

                tagCloudString.Append(tagItem);
                tagCount++;
            }

            if (tagCount > 0)
            {
                return tagLabel + tagCloudString;
            }

            return null;
        }

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
            bool beginsWith = this.UserSearchName.Text.IsNotSet()
                              || !(selectedCharLetter == char.MinValue || selectedCharLetter == '#');

            string selectedLetter = this.UserSearchName.Text.IsSet() ? this.UserSearchName.Text.Trim() : (!(selectedCharLetter == char.MinValue || selectedCharLetter == '#') ? selectedCharLetter.ToString(CultureInfo.InvariantCulture) : string.Empty);

            this.PagerTop.PageSize = this.Get<YafBoardSettings>().TopicsPerPage;

            using (var dtTopics = CommonDb.forum_tags(
                    this.PageContext.PageModuleID,
                    this.PageContext.PageBoardID,
                    this.PageContext.PageUserID,
                    null,
                    this.PagerTop.CurrentPageIndex,
                    this.PagerTop.PageSize,
                    selectedLetter,
                    beginsWith))
            {
                if (dtTopics != null && dtTopics.Rows.Count > 0)
                {
                    this.PagerTop.Count = dtTopics.AsEnumerable().First().Field<int>("TotalCount");
                }

                this.TagLinks.InnerHtml = this.GetTagCloudHtml(dtTopics);
            }
        }

        #endregion
    }
}