﻿namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Primary administrator interface for groups/roles editing.
    /// </summary>
    public partial class groups : AdminPage
    {
        #region Constants and Fields

        /// <summary>
        ///   Temporary storage of un-linked provider roles.
        /// </summary>
        private readonly StringCollection _availableRoles = new StringCollection();

        #endregion

        #region Methods
        /// <summary>
        /// Format access mask setting color formatting.
        /// </summary>
        /// <param name="enabled">
        /// The enabled.
        /// </param>
        /// <returns>
        /// Set access mask flags are rendered green if true, and if not red
        /// </returns>
        protected Color GetItemColor(bool enabled)
        {
            // show enabled flag red
            return enabled ? Color.Green : Color.Red;
        }

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

            // admin index
            this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            // roles
            this.PageLinks.AddLink(this.GetText("ADMIN_GROUPS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
               this.GetText("ADMIN_ADMIN", "Administration"),
               this.GetText("ADMIN_GROUPS", "TITLE"));
        }

        /// <summary>
        /// Handles load event for delete button, adds confirmation dialog.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_GROUPS", "CONFIRM_DELETE"));
        }

        /// <summary>
        /// Get status of provider role vs YAF roles.
        /// </summary>
        /// <param name="currentRow">
        /// Data row which contains data about role.
        /// </param>
        /// <returns>
        /// String "Linked" when role is linked to YAF roles, "Unlinkable" otherwise.
        /// </returns>
        [NotNull]
        protected string GetLinkedStatus([NotNull] DataRowView currentRow)
        {
            // check whether role is Guests role, which can't be linked
            return currentRow["Flags"].BinaryAnd(2) ? this.GetText("ADMIN_GROUPS", "UNLINKABLE") : this.GetText("ADMIN_GROUPS", "LINKED");
        }

        /// <summary>
        /// Handles click on new role button
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void NewGroup_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // redirect to new role page
            YafBuildLink.Redirect(ForumPages.admin_editgroup);
        }

        /// <summary>
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
            // this needs to be done just once, not during postbacks
            if (this.IsPostBack)
            {
                return;
            }

            // create page links
            this.CreatePageLinks();

            this.NewGroup.Text = this.GetText("ADMIN_GROUPS", "NEW_ROLE");

            // sync roles just in case...
            RoleMembershipHelper.SyncRoles(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID);

            // bind data
            this.BindData();
        }

        /// <summary>
        /// Handles provider roles additing/deletetion.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void RoleListNet_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            // detect which command are we handling
            switch (e.CommandName)
            {
                case "add":

                    // save role and get its ID
                    const int _initialPMessages = 0;

                    long groupID = CommonDb.group_save(PageContext.PageModuleID, DBNull.Value,
                      this.PageContext.PageBoardID,
                      e.CommandArgument.ToString(),
                      false,
                      false,
                      false,
                      false,
                      false,
                      1,
                      _initialPMessages,
                      null,
                      100,
                      null,
                      0,
                      null,
                      null,
                      0,
                      0,
                      PageContext.PageUserID,
                      false,
                      0,
                      0,
                      0);

                    // redirect to newly created role
                    YafBuildLink.Redirect(ForumPages.admin_editgroup, "i={0}", groupID);
                    break;
                case "delete":

                    // delete role from provider data
                    RoleMembershipHelper.DeleteRole(e.CommandArgument.ToString(), false);

                    // re-bind data
                    this.BindData();
                    break;
            }
        }

        /// <summary>
        /// Handles role editing/deletion buttons.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void RoleListYaf_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            // detect which command are we handling
            switch (e.CommandName)
            {
                case "edit":

                    // go to role editing page
                    YafBuildLink.Redirect(ForumPages.admin_editgroup, "i={0}", e.CommandArgument);
                    break;
                case "delete":

                    // delete role
                    CommonDb.group_delete(PageContext.PageModuleID, e.CommandArgument);

                    // remove cache of forum moderators
                    this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

                    // re-bind data
                    this.BindData();
                    break;
            }
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            this.PagerTop.PageSize = this.Get<YafBoardSettings>().MemberListPageSize;
            
            // list groups of this board
            var dt = CommonDb.group_list(this.PageContext.PageModuleID, this.PageContext.PageBoardID, null, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);

            // set repeater data source
            this.RoleListYaf.DataSource = dt;

            this.PagerTop.Count = dt != null && dt.Rows.Count > 0
                                     ? Convert.ToInt32(dt.Rows[0]["TotalRows"])
                                     : 0;

            // clear cached list of roles
            this._availableRoles.Clear();

            // get all provider roles
            foreach (string role in from role in RoleMembershipHelper.GetAllRoles()
                                    let filter = "Name='{0}'".FormatWith(role.Replace("'", "''"))
                                    let rows = dt.Select(filter)
                                    where rows.Length == 0
                                    select role)
            {
                // doesn't exist in the Yaf Groups
                this._availableRoles.Add(role);
            }

            // check if there are any roles for syncing
            if (this._availableRoles.Count > 0 && !Config.IsDotNetNuke)
            {
                // make it datasource
                this.RoleListNet.DataSource = this._availableRoles;
            }
            else
            {
                // no datasource for provider roles
                this.RoleListNet.DataSource = null;
            }

            // bind data to controls
            this.DataBind();
        }

        /// <summary>
        /// The pager top_ page change.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void PagerTop_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            // rebind
            this.BindData();
        }

        #endregion
    }
}