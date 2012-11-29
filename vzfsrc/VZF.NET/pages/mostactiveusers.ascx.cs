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

namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Globalization;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Forum Moderating Page.
    /// </summary>
    public partial class mostactiveusers : ForumPage
    {
        /// <summary>
        /// The records to show.
        /// </summary>
        private const int recordsToShow = 10;

        /// <summary>
        /// The days to show.
        /// </summary>
        private readonly int daysToShow;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="mostactiveusers"/> class.
        /// </summary>
        public mostactiveusers()
            : base("MOSACTIVEUSERS")
        {
            this.daysToShow = this.Get<YafBoardSettings>().MostActiveUserDays;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The bind data.
        /// </summary>
        protected void BindData()
        {
            DataTable table = this.Get<IDataCache>()
                                  .GetOrSet(
                                      Constants.Cache.MostActiveUsers,
                                      () =>
                                      CommonDb.user_activity_rank(
                                          PageContext.PageModuleID,
                                          this.PageContext.PageBoardID,
                                          DateTime.UtcNow.AddHours(-this.daysToShow * 24),
                                          recordsToShow),
                                      TimeSpan.FromMinutes(30));
            if (table.Rows.Count <= 0)
            {
                return;
            }

            DataTable dt = table.Clone();
            foreach (DataRow dataRow in table.Rows)
            {
                if (dataRow["IsHidden"].ToType<bool>() && !this.PageContext.IsAdmin)
                {
                    continue;
                }

                DataRow drr = dt.NewRow();
                drr.ItemArray = dataRow.ItemArray;
                dt.Rows.Add(drr);
            }

            this.UserList.DataSource = dt;
            this.DataBind();
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // the feature is disabled.
            if (this.daysToShow <= 0)
            {
                this.RedirectNoAccess();
            }

            if (!this.IsPostBack)
            {
                this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                this.PageLinks.AddLink(this.GetText("MOSTACTIVEUSERS", "TITLE"));
                this.HeaderLbl.Text = this.GetText("MOSTACTIVEUSERS", "TITLE") + "-"
                                      + (this.daysToShow == 1
                                             ? this.GetTextFormatted("MOSTACTIVEUSERS_FORTODAY_LINK", this.daysToShow)
                                             : this.GetTextFormatted("MOSTACTIVEUSERS_FOR_LINK", this.daysToShow));
            }

            this.BindData();
        }

        /// <summary>
        /// The UserList item command.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void UserList_OnItemDataBound([NotNull] object source, [NotNull] RepeaterItemEventArgs e)
        {
            var item = e.Item;

            if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            var drowv = (DataRowView)e.Item.DataItem;
            item.FindControlRecursiveAs<Label>("PercentsOf").Text = this.GetPMessageText(
                drowv["NumOfPosts"], drowv["NumOfAllIntervalPosts"]);
           var ul = item.FindControlRecursiveAs<UserLink>("NameLink");
           ul.ReplaceName = (string)(this.Get<YafBoardSettings>().EnableDisplayName ? drowv["DisplayName"] : drowv["Name"]);
           ul.UserID = drowv["ID"].ToType<int>();
           ul.Style = this.Get<YafBoardSettings>().UseStyledNicks
                                          ? this.Get<IStyleTransform>().DecodeStyleByString(
                                            drowv["UserStyle"].ToString(), false)
                                          : null;
        }

        /// <summary>
        /// Gets the message text.
        /// </summary>
        /// <param name="_total">The _total.</param>
        /// <param name="_limit">The _limit.</param>
        /// <returns>Returns the Message Text</returns>
        protected string GetPMessageText([NotNull] object _total, [NotNull] object _limit)
        {
            if (_limit.ToType<int>() != 0)
            {
                return decimal.Round((_total.ToType<decimal>() / _limit.ToType<decimal>()) * 100, 2).ToString();
            }

            return 0.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The Event args
        /// </param>
        protected void btnReturn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.forum);
        }

        #endregion
    }
}