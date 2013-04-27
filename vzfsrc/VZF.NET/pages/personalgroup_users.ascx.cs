// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="personalgroup_users.ascx.cs">
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
//   The user list in a personal group.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace YAF.Pages
{
    // YAF.Pages

    #region Using

    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Controls;
    using VZF.Data.Common;
    using VZF.Types.Data;

    using YAF.Classes;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Control handling user invitations to forum (i.e. granting permissions by admin/moderator).
    /// </summary>
    public partial class personalgroup_users : ForumPage
    {
        #region Fields

        /// <summary>
        /// The _userListDataTable.
        /// </summary>
        private DataTable _userListDataTable;

        /*
        /// <summary>
        /// The _sortName.
        /// </summary>
        private bool _sortName = true;

        /// <summary>
        /// The _sortRank.
        /// </summary>
        private bool _sortRank;

        /// <summary>
        /// The _sortPosts.
        /// </summary>
        private bool _sortPosts;

        /// <summary>
        /// The _sortJoined.
        /// </summary>
        private bool _sortJoined; 
        */

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "members" /> class.
        /// </summary>
        public personalgroup_users()
            : base("PERSONALGROUP_USERS")
        {
        }

        #endregion

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
            YafBuildLink.Redirect(
                ForumPages.personalgroup_users,
                "gr={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr")));
        }

        /// <summary>
        /// The delete_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // add on click confirm dialog
          ((ThemeButton)sender).Attributes["onclick"] =
                  "return (confirm('{0}'))".FormatWith(
                      this.GetText("MODERATE", "CONFIRM_DELETE_USER")); 
        }

        /// <summary>
        /// The member list_ item command.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void MemberList_ItemCommand([NotNull] object sender, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    // get role ID from it
                    int roleThisId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int>();

                    // get role name
                    string roleName = string.Empty;

                    using (
                        DataTable dt = CommonDb.group_list(
                            PageContext.PageModuleID, this.PageContext.PageBoardID, roleThisId))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            roleName = (string)row["Name"];
                        }
                    }

                    // save user in role
                    CommonDb.usergroup_save(
                        PageContext.PageModuleID, e.CommandArgument.ToType<long>(), roleThisId, false);

                    // empty out access table
                    CommonDb.activeaccess_reset(PageContext.PageModuleID);

                    // update roles if this user isn't the guest
                    if (!UserMembershipHelper.IsGuestUser(e.CommandArgument.ToType<int>()))
                    {
                        // get user's name
                        string userName = UserMembershipHelper.GetUserNameFromID(e.CommandArgument.ToType<long>());
                        RoleMembershipHelper.RemoveUserFromRole(userName, roleName);
                    }

                    // Clearing cache with old permisssions data...
                    this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(e.CommandArgument));


                    // update forum moderators cache just in case something was changed...
                    this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

                    // clear the cache for this user...
                    this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(e.CommandArgument.ToType<int>()));

                    this.BindData();
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the avatar Url for the user
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="avatarString">The avatar string.</param>
        /// <param name="hasAvatarImage">if set to <c>true</c> [has avatar image].</param>
        /// <param name="email">The email.</param>
        /// <returns>Returns the File Url</returns>
        protected string GetAvatarUrlFileName(int userId, string avatarString, bool hasAvatarImage, string email)
        {
            string avatarUrl = this.Get<IAvatars>().GetAvatarUrlForUser(userId, avatarString, hasAvatarImage, email);

            return avatarUrl.IsNotSet()
                       ? "{0}images/noavatar.gif".FormatWith(YafForumInfo.ForumClientFileRoot)
                       : avatarUrl;
        }

        /// <summary>
        /// protects from script in "location" field
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The get string safely.
        /// </returns>
        protected string GetStringSafely(object value)
        {
            return value == null ? string.Empty : this.HtmlEncode(value.ToString());
        }

        /// <summary>
        /// Get all users from user_list for this board.
        /// </summary>
        /// <param name="literals">
        /// The literals.
        /// </param>
        /// <param name="lastUserId">
        /// The last User Id.
        /// </param>
        /// <param name="specialSymbol">
        /// The special Symbol.
        /// </param>
        /// <param name="totalCount">
        /// The total Count.
        /// </param>
        /// <returns>
        /// The Members List
        /// </returns>
        protected DataTable GetUserList(string literals, int lastUserId, bool specialSymbol, out int totalCount)
        {
            var groupId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr");

            this._userListDataTable = CommonDb.user_listmembers(
                PageContext.PageModuleID,
                PageContext.PageBoardID,
                null,
                true,
                groupId,
                null,
                this.Get<YafBoardSettings>().UseStyledNicks,
                lastUserId,
                literals,
                specialSymbol,
                specialSymbol,
                this.Pager.CurrentPageIndex,
                this.Pager.PageSize,
                (int?)ViewState["SortNameField"],
                (int?)ViewState["SortRankNameField"],
                (int?)ViewState["SortJoinedField"],
                (int?)ViewState["SortNumPostsField"],
                (int?)ViewState["SortLastVisitField"],
                this.NumPostsTB.Text.Trim().IsSet() ? this.NumPostsTB.Text.Trim().ToType<int>() : 0,
                this.NumPostDDL.SelectedIndex < 0
                    ? 3
                    : (this.NumPostsTB.Text.Trim().IsSet() ? this.NumPostDDL.SelectedValue.ToType<int>() : 0));

            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                new StyleTransform(this.Get<ITheme>()).DecodeStyleByTable(ref this._userListDataTable, false);
            }

            if (this._userListDataTable.Rows.Count > 0)
            {
                // commits the deletes to the table
                totalCount = (int)this._userListDataTable.Rows[0]["TotalCount"];
            }
            else
            {
                totalCount = 0;
            }

            return this._userListDataTable;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // this.Page.Form.DefaultButton = this.SearchByUserName.UniqueID;

            this.SearchByUserName.Focus();
            var gr = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr");
            if (gr == null)
            {
                return;
            }

            string groupName = null;

            // check if the user accessed his personal group.
            DataTable dtGroup = CommonDb.group_byuserlist(
                PageContext.PageModuleID,
                this.PageContext.PageBoardID,
                this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr"),
                PageContext.PageUserID,
                true);
            if (dtGroup != null && dtGroup.Rows.Count > 0)
            {
                groupName = dtGroup.Rows[0]["Name"].ToString();
            }
            else
            {
                YafBuildLink.Redirect(ForumPages.forum);
            }

            if (this.IsPostBack)
            {
                return;
            }


            this.ViewState["SortNameField"] = 1;
            this.ViewState["SortRankNameField"] = 0;
            this.ViewState["SortJoinedField"] = 0;
            this.ViewState["SortNumPostsField"] = 0;
            this.ViewState["SortLastVisitField"] = 0;

            // board
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // profile
            this.PageLinks.AddLink(
                 this.Get<YafBoardSettings>().EnableDisplayName
                        ? this.PageContext.CurrentUserData.DisplayName
                        : this.PageContext.PageUserName,
                YafBuildLink.GetLink(ForumPages.profile, "u={0}", this.PageContext.PageUserID));
          
            // personal groups
            this.PageLinks.AddLink(
                this.GetText("PERSONALGROUP", "TITLE"),
                YafBuildLink.GetLink(ForumPages.personalgroup, "u={0}", this.PageContext.PageUserID));

            this.PageLinks.AddLink(this.GetTextFormatted("TITLE", groupName), string.Empty);

            this.Page.Header.Title =
                "{0} - {1} - {2}".FormatWith(
                    this.Get<YafBoardSettings>().EnableDisplayName
                        ? this.PageContext.CurrentUserData.DisplayName
                        : this.PageContext.PageUserName,
                    this.GetText("PERSONALGROUP", "TITLE"), 
                    this.GetTextFormatted("TITLE", groupName));

            //// this.SetSort("Name", true);

            this.UserName.Text = this.GetText("username");
            this.Rank.Text = this.GetText("rank");
            this.Joined.Text = this.GetText("joined");
            this.Posts.Text = this.GetText("posts");

            using (DataTable dt = CommonDb.group_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null))
            {
                // add empty item for no filtering
                DataRow newRow = dt.NewRow();
                newRow["Name"] = this.GetText("ALL");
                newRow["GroupID"] = DBNull.Value;
                dt.Rows.InsertAt(newRow, 0);

                // commits to the table
                dt.AcceptChanges();
            }

            this.NumPostDDL.Items.Add(new ListItem(this.GetText("MEMBERS", "NUMPOSTSEQUAL"), "1"));
            this.NumPostDDL.Items.Add(new ListItem(this.GetText("MEMBERS", "NUMPOSTSLESSOREQUAL"), "2"));
            this.NumPostDDL.Items.Add(new ListItem(this.GetText("MEMBERS", "NUMPOSTSMOREOREQUAL"), "3"));

            this.NumPostDDL.DataBind();

            // get list of user ranks for filtering
            var rankList = CommonDb.rank_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null).ToList();

            // add empty for for no filtering
            rankList.Insert(0, new rank_list_Result(0, this.GetText("ALL")));

            for (int index = 0; index < rankList.Count; index++)
            {
                var drow = rankList[index];
                if ((drow.Flags & RankFlags.Flags.IsHidden.ToInt()) == RankFlags.Flags.IsHidden.ToInt())
                {
                    rankList.Remove(drow);
                }
            }

            this.Ranks.DataSource = rankList;
            this.Ranks.DataTextField = "Name";
            this.Ranks.DataValueField = "RankID";
            this.Ranks.DataBind();

            this.BindData();
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
        /// The joined_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Joined_Click(object sender, EventArgs e)
        {
            this.SetSort("Joined");

            this.ViewState["SortNameField"] = 0;
            this.ViewState["SortRankNameField"] = 0;
            this.ViewState["SortNumPostsField"] = 0;
            this.ViewState["SortLastVisitField"] = 0;

            this.BindData();
        }

        /// <summary>
        /// The LastVisitLB Click event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void LastVisitLB_Click(object sender, EventArgs e)
        {
            this.SetSort("LastVisit");

            this.ViewState["SortNameField"] = 0;
            this.ViewState["SortRankNameField"] = 0;
            this.ViewState["SortJoinedField"] = 0;
            this.ViewState["SortNumPostsField"] = 0;

            this.BindData();
        }

        /// <summary>
        /// The posts_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Posts_Click(object sender, EventArgs e)
        {
            this.SetSort("NumPosts");

            this.ViewState["SortNameField"] = 0;
            this.ViewState["SortRankNameField"] = 0;
            this.ViewState["SortJoinedField"] = 0;
            this.ViewState["SortLastVisitField"] = 0;

            this.BindData();
        }

        /// <summary>
        /// The rank_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Rank_Click(object sender, EventArgs e)
        {
            this.SetSort("RankName");

            this.ViewState["SortNameField"] = 0;
            this.ViewState["SortJoinedField"] = 0;
            this.ViewState["SortNumPostsField"] = 0;
            this.ViewState["SortLastVisitField"] = 0;

            this.BindData();
        }

        /// <summary>
        /// The user name_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void UserName_Click(object sender, EventArgs e)
        {
            this.SetSort("Name");

            this.ViewState["SortRankNameField"] = 0;
            this.ViewState["SortJoinedField"] = 0;
            this.ViewState["SortNumPostsField"] = 0;
            this.ViewState["SortLastVisitField"] = 0;

            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.AlphaSort1.PagerPage = ForumPages.personalgroup_users;
            this.AlphaSort1.GroupID = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr").ToType<int?>();
            this.Pager.PageSize = this.Get<YafBoardSettings>().MemberListPageSize;
            char selectedCharLetter = this.AlphaSort1.CurrentLetter;

            // get the user list...
            int totalCount;

            string selectedLetter = this.UserSearchName.Text.IsSet()
                                        ? this.UserSearchName.Text.Trim()
                                        : selectedCharLetter.ToString(CultureInfo.InvariantCulture);

            int numpostsTb;

            if (this.NumPostsTB.Text.Trim().IsSet()
                && (!int.TryParse(this.NumPostsTB.Text.Trim(), out numpostsTb) || numpostsTb < 0
                    || numpostsTb > int.MaxValue))
            {
                PageContext.AddLoadMessage(this.GetText("MEMBERS", "INVALIDPOSTSVALUE"));
                return;
            }

            if (this.NumPostsTB.Text.Trim().IsNotSet())
            {
                this.NumPostsTB.Text = "0";
                this.NumPostDDL.SelectedValue = "3";
            }

            // get the user list...
            this._userListDataTable = this.GetUserList(
                selectedLetter,
                0,
                this.UserSearchName.Text.IsNotSet()
                || (selectedCharLetter == char.MinValue || selectedCharLetter == '#'),
                out totalCount);

            this.Pager.Count = totalCount;
            this.MemberList.DataSource = this._userListDataTable;
            this.DataBind();

            // handle the sort fields at the top
            // TODO: make these "sorts" into controls
            // this.SortAscendingName = (string)this.ViewState["SortField"] == nameField;
            // this.SortAscendingName.Value = "Name";
            switch ((int?)ViewState["SortNameField"])
            {
                case 1:
                    this.SortUserName.Src = this.GetThemeContents("SORT", "ASCENDING");
                    this.SortUserName.Visible = true;
                    break;
                case 2:
                    this.SortUserName.Src = this.GetThemeContents("SORT", "DESCENDING");
                    this.SortUserName.Visible = true;
                    break;
                default:
                    this.ViewState["SortNameField"] = 0;
                    this.SortUserName.Visible = false;
                    break;
            }

            switch ((int?)this.ViewState["SortRankNameField"])
            {
                case 1:
                    this.SortRank.Src = this.GetThemeContents("SORT", "ASCENDING");
                    this.SortRank.Visible = true;
                    break;
                case 2:
                    this.SortRank.Src = this.GetThemeContents("SORT", "DESCENDING");
                    this.SortRank.Visible = true;
                    break;
                default:
                    this.ViewState["SortRankNameField"] = 0;
                    this.SortRank.Visible = false;
                    break;
            }

            switch ((int?)ViewState["SortJoinedField"])
            {
                case 1:
                    this.SortJoined.Src = this.GetThemeContents("SORT", "ASCENDING");
                    this.SortJoined.Visible = true;
                    break;
                case 2:
                    this.SortJoined.Src = this.GetThemeContents("SORT", "DESCENDING");
                    this.SortJoined.Visible = true;
                    break;
                default:
                    this.ViewState["SortJoinedField"] = 0;
                    this.SortJoined.Visible = false;
                    break;
            }

            switch ((int?)ViewState["SortNumPostsField"])
            {
                case 1:
                    this.SortPosts.Src = this.GetThemeContents("SORT", "ASCENDING");
                    this.SortPosts.Visible = true;
                    break;
                case 2:
                    this.SortPosts.Src = this.GetThemeContents("SORT", "DESCENDING");
                    this.SortPosts.Visible = true;
                    break;
                default:
                    this.ViewState["SortNumPostsField"] = 0;
                    this.SortPosts.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Get Theme Contents
        /// </summary>
        /// <param name="page">
        /// The Localization Page.
        /// </param>
        /// <param name="tag">
        /// The Localisation Page Tag.
        /// </param>
        /// <returns>
        /// Returns Theme Content.
        /// </returns>
        protected string GetThemeContents([NotNull] string page, [NotNull] string tag)
        {
            return this.Get<ITheme>().GetItem(page, tag);
        }

        /// <summary>
        /// Helper function for setting up the current sort on the memberlist view
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        private void SetSort(string field)
        {
            switch (field)
            {
                case "Name":
                    this.ViewState["SortNameField"] = this.ViewState["SortNameField"] == null
                                                          ? 0
                                                          : (this.ViewState["SortNameField"].ToType<int>() == 1 ? 2 : 1);
                    break;
                case "RankName":
                    this.ViewState["SortRankNameField"] = this.ViewState["SortRankNameField"] == null
                                                              ? 0
                                                              : (this.ViewState["SortRankNameField"].ToType<int>() == 1
                                                                     ? 2
                                                                     : 1);
                    break;
                case "Joined":
                    this.ViewState["SortJoinedField"] = this.ViewState["SortJoinedField"] == null
                                                            ? 0
                                                            : (this.ViewState["SortJoinedField"].ToType<int>() == 1
                                                                   ? 2
                                                                   : 1);
                    break;
                case "NumPosts":
                    this.ViewState["SortNumPostsField"] = this.ViewState["SortNumPostsField"] == null
                                                              ? 0
                                                              : (ViewState["SortNumPostsField"].ToType<int>() == 1
                                                                     ? 2
                                                                     : 1);
                    break;
                case "LastVisit":
                    this.ViewState["SortLastVisitField"] = this.ViewState["SortLastVisitField"] == null
                                                               ? 0
                                                               : (this.ViewState["SortLastVisitField"].ToType<int>()
                                                                  == 1
                                                                      ? 2
                                                                      : 1);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// The add user to group btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void AddUserToGroupBtn_Click(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(
                ForumPages.personalgroup_edituser,
                "gr={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("gr")));
        }

        /// <summary>
        /// Handles click on cancel button.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // go back to personal group selection
            YafBuildLink.Redirect(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID));
        }
    }
}