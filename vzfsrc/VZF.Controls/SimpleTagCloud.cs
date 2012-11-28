/* VZF by vzrus
 * Copyright (C) 2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    using YAF.Classes;
    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The simple tag cloud.
    /// </summary>
    public class SimpleTagCloud : BaseControl
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref = "SimpleTagCloud" /> class.
        /// </summary>
        public SimpleTagCloud()
        {
            Load += this.SimpleTagCloud_Load;
        }

        /// <summary>
        ///   Gets or sets CSS Class.
        /// </summary>
        [NotNull]
        public int TopicId
        {
            get
            {
                return this.ViewState["TopicId"] != null ? this.ViewState["TopicId"].ToType<int>() : 0;
            }

            set
            {
                this.ViewState["TopicId"] = value;
            }
        }


        #region Methods

        /// <summary>
        /// The get tag cloud html.
        /// </summary>
        /// <param name="tagNameWithFrequencies">
        /// The tag Name With Frequencies.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTagCloudHtml(DataTable tagNameWithFrequencies)
        {
            if (tagNameWithFrequencies == null && tagNameWithFrequencies.Rows.Count <= 0)
            {
                return null;
            }
            string tagLabel = null;
            if (this.Get<YafBoardSettings>().AllowTopicTags && tagNameWithFrequencies != null && tagNameWithFrequencies.Rows.Count > 0)
            {
                var tagsTopicLineIcon = this.Get<ITheme>().GetItem("ICONS", "TOPIC_TAG");

                if (tagsTopicLineIcon.IsSet())
                {
                    tagLabel = "<img src=\"{0}\" alt=\"{1}\" title=\"{1}\" style=\"border: 0;width:16px;height:16px\" />&nbsp;"
                            .FormatWith(
                                tagsTopicLineIcon, this.Get<ILocalization>().GetText("POSTS","TAGS_POSTS"));
                }
            }

            var tagCloudString = new StringBuilder();

            int tagCount = 0;
            foreach (DataRow tag in tagNameWithFrequencies.Rows)
            {
                string tagItem;
                string tagClass = "content";
                
                if (tag["TagCount"].ToType<int>() > 1)
                {
                    // GetTagClass(tagNameWithFrequesncies[tag], highestFrequency);
                    // TODO: need to set proper URL where links should redirect to
                    string targetUrl = YafBuildLink.GetLinkNotEscaped(
                        ForumPages.topicsbytags, "tagid={0}&t={1}".FormatWith(tag["TagID"], this.TopicId));
                    tagItem = "<a class=\"{0}\" href=\"{1}\">{2}({3})</a>&nbsp;&nbsp;".FormatWith(
                        tagClass, targetUrl, tag["Tag"], tag["TagCount"].ToType<int>() - 1);
                }
                else
                {
                    tagItem = "<span class=\"{0}\">{1}({2})</span>&nbsp;&nbsp;".FormatWith(
                        tagClass, tag["Tag"], tag["TagCount"].ToType<int>() - 1);
                }

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
        public string GetTagClass(int tagFrequency, int highestFrequency)
        {
            if (tagFrequency == 0 || highestFrequency == 0)
                return "tag0";

            var percentageFrequency = (tagFrequency*100)/highestFrequency;

            if (percentageFrequency >= 90)
                return "tag90";
            if (percentageFrequency >= 80)
                return "tag80";
            if (percentageFrequency >= 70)
                return "tag70";
            if (percentageFrequency >= 60)
                return "tag60";
            if (percentageFrequency >= 50)
                return "tag50";
            if (percentageFrequency >= 40)
                return "tag40";
            if (percentageFrequency >= 30)
                return "tag30";
            if (percentageFrequency >= 20)
                return "tag20";
            if (percentageFrequency >= 10)
                return "tag10";
            if (percentageFrequency >= 1)
                return "tag1";
            return null;
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void Render([NotNull] HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        /// <summary>
        /// The Simple Tag Cloud_Load_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SimpleTagCloud_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var tag = new HtmlGenericControl();
            tag.Attributes.Add("class", "iconlegend");
            Controls.Add(tag);

            DataTable dt = CommonDb.topic_tags(
                PageContext.PageModuleID, PageContext.PageBoardID, PageContext.PageUserID, this.TopicId);
            tag.InnerHtml = new SimpleTagCloud().GetTagCloudHtml(dt);
        }

        #endregion
    }
}
