﻿namespace YAF.pages
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Controls;
    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The personalforum.
    /// </summary>
    public partial class personalforum : ForumPage
    {
        #region Constants and Fields
        #endregion
        #region Methods


        /// <summary>
        /// Format string color.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// Set values are are rendered green if true, and if not red
        /// </returns>
        protected Color GetItemColorString(string item)
        {
            // show enabled flag red
            return item.IsSet() ? Color.Green : Color.Red;
        }

        /// <summary>
        /// Get a user friendly item name.
        /// </summary>
        /// <param name="enabled">
        /// The enabled.
        /// </param>
        /// <returns>
        /// Item Name.
        /// </returns>
        protected string GetItemName(bool enabled)
        {
            return enabled ? this.GetText("DEFAULT", "YES") : this.GetText("DEFAULT", "NO");
        }

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
            // forum index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // user profile
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName, YafBuildLink.GetLink(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID)));

            // title
            this.PageLinks.AddLink(this.GetText("PERSONALFORUM", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
               this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName,
               this.GetText("ADMINMENU", "FORUMS"));
        }

        /// <summary>
        /// The delete forum_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void DeleteForum_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // number of current forums changed
            // Clearing cache with old Active User Lazy Data ...
            YafContext.Current.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(PageContext.PageUserID));

            ((ThemeButton)sender).Attributes["onclick"] =
                "return (confirm('{0}') && confirm('{1}'))".FormatWith(
                    this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE"),
                    this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_POSITIVE"));
        }


        /// <summary>
        /// The new forum_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void NewForum_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.editpersonalforum, "u={0}".FormatWith(PageContext.PageUserID));
        }

        /// <summary>ag
        /// Handles page load event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (PageContext.UsrPersonalForums <= 0 || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
            {
                YafBuildLink.AccessDenied();
            }

            // this needs to be done just once, not during postbacks
            if (this.IsPostBack)
            {
                return;
            }

            // create page links
            this.CreatePageLinks();

            // sync roles just in case...
            RoleMembershipHelper.SyncRoles(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID);

            if ((this.Get<YafBoardSettings>().AllowPersonalForumsAsSubForums || this.Get<YafBoardSettings>().AllowPersonalForumsInCategories) && PageContext.PersonalForumsNumber < PageContext.UsrPersonalForums)
            {
                this.NewForum.Visible = true;
            }

            // bind data
            this.BindData();

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("del").ToType<int>() == 1)
            {
                this.PageContext.AddLoadMessage(this.GetText("PERSONALFORUM", "DELETINGUNDERWAY"));
            }
        }

        /// <summary>
        /// The forum list_ item command.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void ForumList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "edit":
                    YafBuildLink.Redirect(ForumPages.editpersonalforum, "u={0}&fa={1}", PageContext.PageUserID, e.CommandArgument);
                    break;
                case "delete":
                    var errorMessage = string.Empty;

                    // schedule...
                    ForumDeleteTask.Start(YafContext.Current.PageModuleID, this.PageContext.PageBoardID, e.CommandArgument.ToType<int>(), out errorMessage);

                    // Clearing cache with old Active User Lazy Data as it contains number of forums info...
                    YafContext.Current.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(this.PageContext.PageUserID));

                    // If we have no access masks only we redirect it to profile page
                    var dd = CommonDb.accessmask_pforumlist(
              mid: PageContext.PageModuleID,
              boardId: this.PageContext.PageBoardID,
              accessMaskId: null,
              excludeFlags: AccessFlags.Flags.None.ToInt(),
              pageUserId: null,
              isUserMask: false,           
              isCommonMask: !this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums
             );

                    if (dd != null && dd.Rows.Count > 0)
                    {
                        YafBuildLink.Redirect(
                           ForumPages.personalforum,
                           "u={0}&del=1",
                           PageContext.PageUserID,
                           e.CommandArgument);
                    }
                    else
                    {
                        YafBuildLink.Redirect(
                             ForumPages.profile,
                             "u={0}",
                             PageContext.PageUserID,
                             e.CommandArgument);
                    }
                    break;
                case "moderate":
                    YafBuildLink.Redirect(ForumPages.moderating, "f={0}", e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        /// Get query string as int.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The get query string as int.
        /// </returns>
        protected int? GetQueryStringAsInt([NotNull] string name)
        {
            int value;

            if (this.Request.QueryString.GetFirstOrDefault(name) != null
                && int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // add forum list
            using (var frmList = CommonDb.forum_byuserlist(PageContext.PageModuleID, this.PageContext.PageBoardID, null, PageContext.PageUserID, true))
            {
                this.ForumList.DataSource = frmList;
            }

            // Hide the New Forum Button if there are no Categories.
            // this.AddForumBtn.Visible = this.AddForumBtn.Visible && this.CategoryList.Items.Count < 1;
            // bind data to controls



            this.DataBind();
        }

        /// <summary>
        /// The bit set.
        /// </summary>
        /// <param name="_o">
        /// The _o.
        /// </param>
        /// <param name="bitmask">
        /// The bitmask.
        /// </param>
        /// <returns>
        /// The bit set.
        /// </returns>
        protected bool BitSet([NotNull] object _o, int bitmask)
        {
            var i = (int)_o;
            return (i & bitmask) != 0;
        }

        #endregion

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
            YafBuildLink.Redirect(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID));
        }
    }
}