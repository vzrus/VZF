/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
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

namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web.Security;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Interface for creating or editing user roles/groups.
    /// </summary>
    public partial class editgroup : ForumPage
    {
        #region Methods

        public DataTable AccessMasksList { get; set; }
        /// <summary>
        /// Handles databinding event of initial access maks dropdown control.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void BindData_AccessMaskID([NotNull] object sender, [NotNull] EventArgs e)
        {
            // We don't change access masks if it's a guest
        
                // get sender object as dropdown list
                var c = (DropDownList) sender;

                // list all access masks as data source
                c.DataSource = this.AccessMasksList;
                // set value and text field names
                c.DataValueField = "AccessMaskID";
                c.DataTextField = "Name";
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
            // go back to roles administration
            YafBuildLink.Redirect(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID));
        }

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
            // forum index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // user profile
            this.PageLinks.AddLink(this.GetText("CP_PROFILE", "TITLE"), YafBuildLink.GetLink(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID)));

            this.PageLinks.AddLink(this.GetText("ADMIN_GROUPS", "TITLE"), YafBuildLink.GetLink(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID)));

            // current page label (no link)
            this.PageLinks.AddLink(this.GetText("ADMIN_EDITGROUP", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
               this.Get<YafBoardSettings>().Name,
               this.GetText("CP_PROFILE", "TITLE"),
               this.GetText("ADMIN_EDITGROUP", "TITLE"));
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

            this.Save.Text = this.GetText("COMMON", "SAVE");
            this.Cancel.Text = this.GetText("COMMON", "CANCEL");


            // bind data
            this.BindData();
            // is this editing of existing role or creation of new one?
            if (this.Request.QueryString.GetFirstOrDefault("i") == null)
            {
                return;
            }

            // we are not creating new role
            this.NewGroupRow.Visible = false;

            // get data about edited role
            using (
                DataTable dt = CommonDb.group_byuserlist(PageContext.PageModuleID, this.PageContext.PageBoardID, this.Request.QueryString.GetFirstOrDefault("i"),PageContext.PageUserID, true))
            {
                // get it as row
                DataRow row = dt.Rows[0];

                // get role flags
                var flags = new GroupFlags(row["Flags"]);

                // set controls to role values
                this.Name.Text = (string)row["Name"];
                this.StyleTextBox.Text = row["Style"].ToString();
                this.Priority.Text = row["SortOrder"].ToString();
                this.Description.Text = row["Description"].ToString();
            }
        }

        /// <summary>
        /// Handles click on save button.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
         

            if (!ValidationHelper.IsValidInt(this.Priority.Text.Trim()))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITGROUP", "MSG_INTEGER"));
                return;
            }

            // Role
            long roleID = 0;

            // get role ID from page's parameter
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                roleID = long.Parse(this.Request.QueryString.GetFirstOrDefault("i"));
            }

            // get new and old name
            string roleName = this.Name.Text.Trim();
            string oldRoleName = string.Empty;

            // if we are editing exising role, get it's original name
            if (roleID != 0)
            {
                // get the current role name in the DB
                using (DataTable dt = CommonDb.group_byuserlist(PageContext.PageModuleID, this.PageContext.PageBoardID, this.Request.QueryString.GetFirstOrDefault("i"), PageContext.PageUserID, true))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        oldRoleName = row["Name"].ToString();
                    }
                }
            }

            // save role and get its ID if it's new (if it's old role, we get it anyway)
            roleID = CommonDb.group_save(PageContext.PageModuleID, roleID,
              this.PageContext.PageBoardID,
              roleName,
              false,
              false,
              false,
              false,
              AccessMaskID.SelectedValue,
              0,
              this.StyleTextBox.Text.Trim(),
              this.Priority.Text.Trim(),
              this.Description.Text,
              0,
              null,
              null,
              0,
              0,
              PageContext.PageUserID,
              true);

            // empty out access table
            CommonDb.activeaccess_reset(PageContext.PageModuleID);

            // see if need to rename an existing role...
            if (oldRoleName.IsSet() && roleName != oldRoleName && RoleMembershipHelper.RoleExists(oldRoleName) && !RoleMembershipHelper.RoleExists(roleName))
            {
                // transfer users in addition to changing the name of the role...
                var users = this.Get<RoleProvider>().GetUsersInRole(oldRoleName);

                // delete the old role...
                RoleMembershipHelper.DeleteRole(oldRoleName, false);

                // create new role...
                RoleMembershipHelper.CreateRole(roleName);

                if (users.Any())
                {
                    // put users into new role...
                    this.Get<RoleProvider>().AddUsersToRoles(users, new[] { roleName });
                }
            }
            else if (!RoleMembershipHelper.RoleExists(roleName))
            {
                // if role doesn't exist in provider's data source, create it

                // simply create it
                RoleMembershipHelper.CreateRole(roleName);
            }

            // Access masks for a newly created or an existing role
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                    // go trhough all forums
                    for (int i = 0; i < this.AccessList.Items.Count; i++)
                    {
                        // get current repeater item
                        RepeaterItem item = this.AccessList.Items[i];

                        // get forum ID
                        int forumID = int.Parse(((Label)item.FindControl("ForumID")).Text);

                        // save forum access maks for this role
                        CommonDb.forumaccess_save(PageContext.PageModuleID, forumID, roleID,
                                                  ((DropDownList)item.FindControl("AccessmaskID")).SelectedValue);
                    }

                    YafBuildLink.Redirect(ForumPages.personalgroup, "u={0}".FormatWith(PageContext.PageUserID));
            }

            // remove caching in case something got updated...
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

            // Clearing cache with old permissions data...
            this.Get<IDataCache>().Remove(k => k.StartsWith(Constants.Cache.ActiveUserLazyData.FormatWith(String.Empty)));

            // Done, redirect to role editing page
            YafBuildLink.Redirect(ForumPages.editgroup, "i={0}", roleID);
        }

        /// <summary>
        /// Handles pre-render event of each forum's access mask dropdown.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void SetDropDownIndex([NotNull] object sender, [NotNull] EventArgs e)
        {
            // get dropdown which raised this event
            var list = (DropDownList)sender;

            // select value from the list
            ListItem item = list.Items.FindByValue(list.Attributes["value"]);

            // verify something was found...
            if (item != null)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // set datasource of access list (list of forums and role's access masks) if we are editing existing mask
            if (this.Request.QueryString.GetFirstOrDefault("i") != null)
            {
                this.AccessList.DataSource = CommonDb.forumaccess_group(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("i"),PageContext.PageUserID,true);
            }

            this.AccessMasksList = CommonDb.accessmask_pforumlist(mid: PageContext.PageModuleID, boardId: this.PageContext.PageBoardID, accessMaskID: null, excludeFlags: 0, pageUserID: this.PageContext.PageUserID, isUserMask: true, isAdminMask: false);
           
            // bind data to controls
            this.DataBind();
        }

        #endregion
    }
}