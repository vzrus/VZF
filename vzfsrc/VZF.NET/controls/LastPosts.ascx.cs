/* Yet Another Forum.NET
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

namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utilities;

    #endregion

    /// <summary>
    /// The last posts.
    /// </summary>
    public partial class LastPosts : BaseUserControl
    {
        #region Properties

        /// <summary>
        ///   Gets or sets TopicID.
        /// </summary>
        public long? TopicID
        {
            get
            {
                if (this.ViewState["TopicID"] != null)
                {
                    return this.ViewState["TopicID"].ToType<int>();
                }

                return null;
            }

            set
            {
                this.ViewState["TopicID"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The last post update timer_ tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void LastPostUpdateTimer_Tick([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// The on pre render.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            YafContext.Current.PageElements.RegisterJsBlockStartup(
                this.LastPostUpdatePanel, "DisablePageManagerScrollJs", JavaScriptBlocks.DisablePageManagerScrollJs);

            base.OnPreRender(e);
        }

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
            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (this.TopicID.HasValue)
            {
                bool showDeleted = false;
                int userId = 0;

                if (this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
                {
                    showDeleted = true;
                }

                if (!showDeleted
                    && (this.Get<YafBoardSettings>().ShowDeletedMessages
                        && !this.Get<YafBoardSettings>().ShowDeletedMessagesToAll) || this.PageContext.IsAdmin
                    || this.PageContext.IsForumModerator)
                {
                    userId = this.PageContext.PageUserID;
                }

                DataTable dt = CommonDb.post_list(
                    PageContext.PageModuleID,
                    this.TopicID,
                    this.PageContext.PageUserID,
                    userId,
                    false,
                    showDeleted,
                    true,
                    false,
                    DateTimeHelper.SqlDbMinTime(),
                    DateTime.UtcNow,
                    DateTimeHelper.SqlDbMinTime(),
                    DateTime.UtcNow,
                    0,
                    10,
                    2,
                    0,
                    0,
                    false,
                    -1,
                    -1,
                    DateTimeHelper.SqlDbMinTime());

                this.repLastPosts.DataSource = dt.AsEnumerable();
            }
            else
            {
                this.repLastPosts.DataSource = null;
            }

            this.repLastPosts.DataBind();
        }

        #endregion

        /// <summary>
        /// The rep last posts_ on item data bound.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void repLastPosts_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            var dt = (DataRow)e.Item.DataItem;
            this.FindControlRecursiveAs<UserLink>("ProfileLink").ReplaceName =
                this.Get<YafBoardSettings>().EnableDisplayName
                && (!dt["IsGuest"].ToType<bool>()
                    || (dt["IsGuest"].ToType<bool>() && dt["DisplayName"].ToString() == dt["UserName"].ToString()))
                    ? dt["DisplayName"].ToString()
                    : dt["UserName"].ToString();
            this.FindControlRecursiveAs<UserLink>("ProfileLink").PostfixText = dt["IP"].ToString()
                                                                                                == "NNTP"
                                                                                                    ? this.GetText(
                                                                                                        "EXTERNALUSER")
                                                                                                    : string.Empty;
        }
    }
}