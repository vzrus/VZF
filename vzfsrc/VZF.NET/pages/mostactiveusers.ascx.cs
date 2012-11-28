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

using System.Collections;
using YAF.Utils.Helpers;

namespace YAF.Pages
{
    // YAF.Pages

    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// Forum Moderating Page.
    /// </summary>
    public partial class mostactiveusers : ForumPage
    {
        int recordsToShow = 10;
        int daysToShow = 7; 
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "moderating" /> class.
        /// </summary>
        public mostactiveusers()
            : base("MOSACTIVEUSERS")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The bind data.
        /// </summary>
        protected void BindData()
        {
            this.Stats_Renew();
            DataBind();
        }
    
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
          if (!this.IsPostBack)
            {

               //  this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
                this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                this.PageLinks.AddLink(this.GetText("MOSTACTIVEUSERS", "TITLE"));
                this.HeaderLbl.Text = this.GetText("MOSTACTIVEUSERS", "TITLE") + "-" +
                                      this.GetTextFormatted("MOSTACTIVEUSERS_FOR_LINK",daysToShow);
                // this.PagerTop.PageSize = 25;
            }

            this.BindData();
        }


        /// <summary>
        /// The stats_ renew.
        /// </summary>
        protected void Stats_Renew()
        {
          
            // Renew PM Statistics
            DataTable rankDt = this.Get<IDataCache>().GetOrSet(
              Constants.Cache.MostActiveUsers,
              () =>
              CommonDb.user_activity_rank(PageContext.PageModuleID, this.PageContext.PageBoardID, DateTime.UtcNow.AddDays(-daysToShow), recordsToShow),
              TimeSpan.FromMinutes(5));
            if (rankDt.Rows.Count > 0)
            {
                DataTable dt = rankDt.Clone(); 
                foreach (DataRow dataRow in rankDt.Rows)
                {
                    if (!dataRow["IsHidden"].ToType<bool>() || PageContext.IsAdmin)
                    {
                        DataRow drr = dt.NewRow();
                        drr.ItemArray = dataRow.ItemArray;
                        dt.Rows.Add(drr);
                    }
               
                }
               // dt.AcceptChanges();
                this.UserList.DataSource = dt;
            }
        }

        /// <summary>
        /// The UserList item command.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void UserList_OnItemDataBound([NotNull] object source, [NotNull] RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                var drowv = (DataRowView) e.Item.DataItem;
                item.FindControlRecursiveAs<Label>("PercentsOf").Text = this.GetPMessageText(
                    drowv["NumOfPosts"],
                    drowv["NumOfAllIntervalPosts"]);
            }
        }


        /// <summary>
                    /// Gets the message text.
                    /// </summary>
                    /// <param name="_total">The _total.</param>
                    /// <param name="_limit">The _limit.</param>
                    /// <returns>Returns the Message Text</returns>
                protected
                string GetPMessageText(
            [NotNull] object _total,
            [NotNull] object _limit)
        {
           
            if (_limit.ToType<int>() != 0)
            {
                return decimal.Round((_total.ToType<decimal>()/_limit.ToType<decimal>())*100, 2).ToString();
            }
           
             return  0.ToString();
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