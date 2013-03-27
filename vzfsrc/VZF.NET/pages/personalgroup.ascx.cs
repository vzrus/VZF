// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="personalgroup.ascx.cs">
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
//   The personal group list.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace YAF.pages
{
    using System;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using VZF.Controls;
    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utilities;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    /// <summary>
    /// The personalforum.
    /// </summary>
    public partial class personalgroup : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        ///   Temporary storage of un-linked provider roles.
        /// </summary>
        private readonly StringCollection _availableRoles = new StringCollection();

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

            // roles
            this.PageLinks.AddLink(this.GetText("PERSONALGROUP", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
               this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName,
               this.GetText("PERSONALGROUP", "TITLE"));
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
        protected void DeleteRole_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // number of current groups changed
            this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData);

            // add on click confirm dialog
            ((ThemeButton)sender).Attributes["onclick"] =
                  "return (confirm('{0}'))".FormatWith(
                      this.GetText("ADMIN_GROUPS", "CONFIRM_DELETE"));
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
            YafBuildLink.Redirect(ForumPages.editgroup, "u={0}".FormatWith(PageContext.PageUserID));
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
            YafBuildLink.Redirect(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID));
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
            if (PageContext.UsrPersonalGroups <= 0 || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
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

            if (PageContext.PersonalGroupsNumber < PageContext.UsrPersonalGroups)
            {
                this.NewGroup.Visible = true;
            }

            // sync roles just in case...
            RoleMembershipHelper.SyncRoles(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID);

            // bind data
            this.BindData();
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
                    YafBuildLink.Redirect(ForumPages.editgroup, "i={0}&u={1}", e.CommandArgument, PageContext.PageUserID);
                    break;
                case "delete":

                    // delete role
                    CommonDb.group_delete(PageContext.PageModuleID, e.CommandArgument);
                    
                    // delete role from provider data
                    RoleMembershipHelper.DeleteRole(e.CommandArgument.ToString(), false);
                   
                    // remove cache of forum moderators
                    this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

                    // re-bind data
                    this.BindData();
                    break;
                case "users":
                    YafBuildLink.Redirect(ForumPages.personalgroup_users, "gr={0}", e.CommandArgument);
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
            // list roles of this board
            DataTable dt = CommonDb.group_byuserlist(PageContext.PageModuleID, this.PageContext.PageBoardID, null,PageContext.PageUserID,true);

            // set repeater datasource
            this.RoleListYaf.DataSource = dt;

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
        /// The ok button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void OKButton_Click(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.forum);
        }

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
    }
}