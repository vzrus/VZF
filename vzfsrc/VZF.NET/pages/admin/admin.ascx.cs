﻿using System.Collections.Generic;
using VZF.Types.Data;

namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI.WebControls;

    using FarsiLibrary;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.RegisterV2;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The Admin Index Page.
    /// </summary>
    public partial class admin : AdminPage
    {
        #region Public Methods

        /// <summary>
        /// Loads the Board Stats for the Selected Board
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void BoardStatsSelect_Changed([NotNull] object sender, [NotNull] EventArgs e)
        {
            // re-bind data
            this.BindData();
        }

        /// <summary>
        /// Handles the ItemCommand event of the UserList control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        public void UserList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "edit":
                    YafBuildLink.Redirect(ForumPages.admin_edituser, "u={0}", e.CommandArgument);
                    break;
                case "delete":
                    string daysValue =
                        this.PageContext.CurrentForumPage.FindControlRecursiveAs<TextBox>("DaysOld").Text.Trim();
                    if (!ValidationHelper.IsValidInt(daysValue))
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_ADMIN", "MSG_VALID_DAYS"));
                        return;
                    }

                    if (!Config.IsAnyPortal)
                    {
                        UserMembershipHelper.DeleteUser(e.CommandArgument.ToType<int>());
                    }

                    CommonDb.user_delete(PageContext.PageModuleID, e.CommandArgument);
                    this.Get<ILogger>().UserDeleted(this.PageContext.PageUserID, "YAF.Pages.Admin.admin", "User {0} was deleted by {1}.".FormatWith(e.CommandArgument.ToType<int>(), this.PageContext.PageUserID));
                    this.BindData();
                    break;
                case "approve":
                    UserMembershipHelper.ApproveUser(e.CommandArgument.ToType<int>());
                    this.BindData();
                    break;
                case "deleteall":

                    // vzrus: Should not delete the whole providers portal data? Under investigation.
                    string daysValueAll =
                        this.PageContext.CurrentForumPage.FindControlRecursiveAs<TextBox>("DaysOld").Text.Trim();
                    if (!ValidationHelper.IsValidInt(daysValueAll))
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_ADMIN", "MSG_VALID_DAYS"));
                        return;
                    }

                    if (!Config.IsAnyPortal)
                    {
                        UserMembershipHelper.DeleteAllUnapproved(DateTime.UtcNow.AddDays(-daysValueAll.ToType<int>()));
                    }

                    CommonDb.user_deleteold(PageContext.PageModuleID, this.PageContext.PageBoardID, daysValueAll.ToType<int>());
                    this.BindData();
                    break;
                case "approveall":
                    UserMembershipHelper.ApproveAll();

                    // vzrus: Should delete users from send email list
                    CommonDb.user_approveall(PageContext.PageModuleID, this.PageContext.PageBoardID);
                    this.BindData();
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds Confirmation Dialog to the Approve All Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ApproveAll_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((Button)sender).Text = this.GetText("ADMIN_ADMIN", "APROVE_ALL");
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ADMIN", "CONFIRM_APROVE_ALL"));
        }

        /// <summary>
        /// Adds Confirmation Dialog to the Approve Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Approve_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ADMIN", "CONFIRM_APROVE"));
        }

        /// <summary>
        /// Adds Confirmation Dialog to the Delete All Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DeleteAll_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((Button)sender).Text = this.GetText("ADMIN_ADMIN", "DELETE_ALL");

            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ADMIN", "CONFIRM_DELETE_ALL"));
        }

        /// <summary>
        /// Adds Confirmation Dialog to the Delete Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ADMIN", "CONFIRM_DELETE"));
        }

        /// <summary>
        /// Formats the forum link.
        /// </summary>
        /// <param name="forumID">
        /// The forum ID.
        /// </param>
        /// <param name="forumName">
        /// Name of the forum.
        /// </param>
        /// <returns>
        /// The format forum link.
        /// </returns>
        protected string FormatForumLink([NotNull] object forumID, [NotNull] object forumName)
        {
            if (forumID.ToString() == string.Empty || forumName.ToString() == string.Empty)
            {
                return string.Empty;
            }

            return
                "<a target=\"_top\" href=\"{0}\">{1}</a>".FormatWith(
                    YafBuildLink.GetLink(ForumPages.topics, "f={0}", forumID), forumName);
        }

        /// <summary>
        /// Formats the topic link.
        /// </summary>
        /// <param name="topicID">
        /// The topic ID.
        /// </param>
        /// <param name="topicName">
        /// Name of the topic.
        /// </param>
        /// <returns>
        /// The format topic link.
        /// </returns>
        protected string FormatTopicLink([NotNull] object topicID, [NotNull] object topicName)
        {
            if (topicID.ToString() == string.Empty || topicName.ToString() == string.Empty)
            {
                return string.Empty;
            }

            return
                "<a target=\"_top\" href=\"{0}\">{1}</a>".FormatWith(
                    YafBuildLink.GetLink(ForumPages.posts, "t={0}", topicID), topicName);
        }

        /// <summary>
        /// Sets the location.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Returns the Location</returns>
        protected string SetLocation([NotNull] string userName)
        {
            string location;

            try
            {
                location = YafUserProfile.GetProfile(Eval("UserName").ToString()).Location;

                if (string.IsNullOrEmpty(location))
                {
                    location = "-";
                }
            }
            catch (Exception)
            {
                location = "-";
            }

            return this.HtmlEncode(this.Get<IBadWordReplace>().Replace(location));
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), string.Empty);

            this.Page.Header.Title = this.GetText("ADMIN_ADMIN", "Administration");

            // bind data
            this.BindBoardsList();

            this.BindData();

            var latestInfo =
                this.Get<HttpApplicationStateBase>()["YafRegistrationLatestInformation"] as LatestVersionInformation;

            if (latestInfo == null || latestInfo.Version <= YafForumInfo.AppVersionCode)
            {
                return;
            }

            // updateLink
            var updateLink = new Action<HyperLink>(
                link =>
                    {
                        link.Text = latestInfo.Message;
                        link.NavigateUrl = latestInfo.Link;
                    });

            if (latestInfo.IsWarning)
            {
                this.UpdateWarning.Visible = true;
                updateLink(this.UpdateLinkWarning);
            }
            else
            {
                this.UpdateHightlight.Visible = true;
                updateLink(this.UpdateLinkHighlight);
            }

            // UpgradeNotice.Visible = install._default.GetCurrentVersion() < Data.AppVersion;
        }

        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BindActiveUserData();
        }

        /// <summary>
        /// Binds the active user data.
        /// </summary>
        private void BindActiveUserData()
        {
            DataTable activeList = this.GetActiveUsersData(true, true);

            if (activeList == null || activeList.Rows.Count <= 0)
            {
                return;
            }

            var activeUsersView = activeList.DefaultView;
            this.Pager.PageSize = 50;

            var pds = new PagedDataSource { AllowPaging = true, PageSize = this.Pager.PageSize };
            this.Pager.Count = activeUsersView.Count;
            pds.DataSource = activeUsersView;
            pds.CurrentPageIndex = this.Pager.CurrentPageIndex;

            if (pds.CurrentPageIndex >= pds.PageCount)
            {
                pds.CurrentPageIndex = pds.PageCount - 1;
            }

            this.ActiveList.DataSource = pds;
            this.ActiveList.DataBind();
        }

        /// <summary>
        /// Bind list of boards to dropdown
        /// </summary>
        private void BindBoardsList()
        {
            // only if user is hostadmin, otherwise boards' list is hidden
            if (!this.PageContext.IsHostAdmin)
            {
                return;
            }

            DataTable dt = CommonDb.board_list(PageContext.PageModuleID, null);

            // add row for "all boards" (null value)
            DataRow r = dt.NewRow();

            r["BoardID"] = -1;
            r["Name"] = this.GetText("ADMIN_ADMIN", "ALL_BOARDS");

            dt.Rows.InsertAt(r, 0);

            // set datasource
            this.BoardStatsSelect.DataSource = dt;
            this.BoardStatsSelect.DataBind();

            // select current board as default
            this.BoardStatsSelect.SelectedIndex =
                this.BoardStatsSelect.Items.IndexOf(
                    this.BoardStatsSelect.Items.FindByValue(this.PageContext.PageBoardID.ToString()));
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            this.UserList.DataSource = CommonDb.user_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null, false);

            // this.DataBind();

            // get stats for current board, selected board or all boards (see function)
            var ss = CommonDb.board_stats(PageContext.PageModuleID, this.GetSelectedBoardID());
           
            this.NumPosts.Text = "{0:N0}".FormatWith(ss.NumPosts);
            this.NumTopics.Text = "{0:N0}".FormatWith(ss.NumTopics);
            this.NumUsers.Text = "{0:N0}".FormatWith(ss.NumUsers);

            TimeSpan span = DateTime.UtcNow - (DateTime)ss.BoardStart;
            double days = span.Days;

            this.BoardStart.Text =
                this.GetText("ADMIN_ADMIN", "DAYS_AGO").FormatWith(
                    this.Get<YafBoardSettings>().UseFarsiCalender
                        ? (object) PersianDateConverter.ToPersianDate((DateTime)ss.BoardStart)
                        : ss.BoardStart,
                    days);

            if (days < 1)
            {
                days = 1;
            }

            this.DayPosts.Text = "{0:N2}".FormatWith(ss.NumPosts.ToType<int>() / days);
            this.DayTopics.Text = "{0:N2}".FormatWith(ss.NumTopics.ToType<int>() / days);
            this.DayUsers.Text = "{0:N2}".FormatWith(ss.NumUsers.ToType<int>() / days);

            try
            {
                this.DBSize.Text = "{0} MB".FormatWith(CommonDb.GetDbSize(PageContext.PageModuleID));
            }
            catch (Exception ex)
            {
                this.DBSize.Text = this.GetText("ADMIN_ADMIN", "ERROR_DATABASESIZE");
            }

            this.BindActiveUserData();

            this.DataBind();
        }

        /// <summary>
        /// Gets active user data Table data for a page user
        /// </summary>
        /// <param name="showGuests">
        /// The show guests.
        /// </param>
        /// <param name="showCrawlers">
        /// The show crawlers.
        /// </param>
        /// <returns>
        /// A DataTable
        /// </returns>
        private DataTable GetActiveUsersData(bool showGuests, bool showCrawlers)
        {
            // vzrus: Here should not be a common cache as it's should be individual for each user because of ActiveLocationcontrol to hide unavailable places.        
            DataTable activeUsers = CommonDb.active_list_user(PageContext.PageModuleID, this.PageContext.PageBoardID,
                this.PageContext.PageUserID,
                showGuests,
                showCrawlers,
                this.Get<YafBoardSettings>().ActiveListTime,
                this.Get<YafBoardSettings>().UseStyledNicks);

            // Set colorOnly parameter to false, as we get active users style from database        
            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                this.Get<IStyleTransform>().DecodeStyleByTable(ref activeUsers, false);
            }

            return activeUsers;
        }

        /// <summary>
        /// Gets board ID for which to show statistics.
        /// </summary>
        /// <returns>
        /// Returns ID of selected board (for host admin), ID of current board (for admin), null if all boards is selected.
        /// </returns>
        private object GetSelectedBoardID()
        {
            // check dropdown only if user is hostadmin
            if (!this.PageContext.IsHostAdmin)
            {
                return this.PageContext.PageBoardID;
            }

            // -1 means all boards are selected
            return this.BoardStatsSelect.SelectedValue == "-1" ? null : this.BoardStatsSelect.SelectedValue;
        }

        #endregion
    }
}