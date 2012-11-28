/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj?rnar Henden
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

using YAF.Classes;

using YAF.Utils;

namespace YAF.Controls
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The thanks list mode.
    /// </summary>
    public enum ThanksListMode
    {
        /// <summary>
        ///   The from user.
        /// </summary>
        FromUser,

        /// <summary>
        ///   The to user.
        /// </summary>
        ToUser
    }

    /// <summary>
    /// Summary description for buddies.
    /// </summary>
    public partial class ViewThanksList : BaseUserControl
    {
        /* Data Fields */
        /// <summary>
        ///   Gets or sets a value indicating whether Auto Databind.
        /// </summary>
        public bool AutoDatabind { get; set; }
        /* Properties */
        #region Constants and Fields

        /// <summary>
        ///   The _count.
        /// </summary>
        private int _count;

        private int userID;

        #endregion

        #region Properties

        /// <summary>
        ///   Determines what is th current mode of the control.
        /// </summary>
        public ThanksListMode CurrentMode { get; set; }

        /// <summary>
        ///   The Thanks Info.
        /// </summary>
        public DataTable ThanksInfo { get; set; }

        /// <summary>
        ///   The User ID.
        /// </summary>
        public int UserID { get; set; }

        #endregion

        // keeps count

        /* Event Handlers */

        /* Methods */
        #region Public Methods

        /// <summary>
        /// The bind data.
        /// </summary>
        public void BindData()
        {
            // we'll hold topics in this table
            DataTable topicList = null;

            // set the page size here
            this.PagerTop.PageSize = this.Get<YafBoardSettings>().MyTopicsListPageSize;

            // now depending on mode fill the table
            switch (this.CurrentMode)
            {
                case ThanksListMode.FromUser:
                    topicList = CommonDb.user_viewthanksfrom(PageContext.PageModuleID, userID, this.PageContext.PageUserID, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);
                    break;
                case ThanksListMode.ToUser:
                    topicList = CommonDb.user_viewthanksto(PageContext.PageModuleID, userID, this.PageContext.PageUserID, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);
                    break;
            }

            if (topicList == null)
            {
                this.PagerTop.Count = 0;
                return;
            }

            if (topicList.Rows.Count <= 0)
            {
                this.PagerTop.Count = 0;
                this.ThanksRes.DataSource = null;
                this.ThanksRes.DataBind();
                return;
            }

            // let's page the results
            this.PagerTop.Count = topicList.Rows.Count > 0
                                      ? topicList.AsEnumerable().First().Field<int>("TotalRows")
                                      : 0;

            this.ThanksRes.DataSource = topicList.AsEnumerable();

            // data bind controls
            this.DataBind();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns <see langword="true"/> if the count is odd
        /// </summary>
        /// <returns>
        /// The is odd.
        /// </returns>
        protected bool IsOdd()
        {
            return (this._count++ % 2) == 0;
        }

        /* Methods */

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            userID = (int)Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("u"));
            if (this.AutoDatabind)
            {
                this.BindData();
            }
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
        protected void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// Handles the ItemCreated event of the ThanksRes control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> 
        ///   instance containing the event data.
        /// </param>
        protected void ThanksRes_ItemCreated([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            switch (this.CurrentMode)
            {
               case ThanksListMode.FromUser:
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        var thanksNumberCell = (HtmlTableCell)e.Item.FindControl("ThanksNumberCell");
                        thanksNumberCell.Visible = false;
                    }

                    break;
                case ThanksListMode.ToUser:
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        var nameCell = (HtmlTableCell)e.Item.FindControl("NameCell");
                        nameCell.Visible = false;
                    }

                    break;
            } 
        }

        #endregion
    }
}