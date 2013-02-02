﻿
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
    using YAF.Controls;
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
    public partial class personalaccessmask : ForumPage
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
            this.PageLinks.AddLink(this.GetText("CP_PROFILE", "VIEW_PROFILE"), YafBuildLink.GetLink(ForumPages.cp_profile, "u={0}".FormatWith(PageContext.PageUserID)));

            // title
            this.PageLinks.AddLink(this.GetText("ADMIN_ACCESSMASKS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
               this.Get<YafBoardSettings>().Name,
               this.GetText("CP_PROFILE", "VIEW_PROFILE"),
               this.GetText("ADMIN_ACCESSMASKS", "TITLE"));
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
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_GROUPS", "CONFIRM_DELETE"));
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
            YafBuildLink.Redirect(ForumPages.editforum);
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
            YafBuildLink.Redirect(ForumPages.editgroup);
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
            if (this.Get<YafBoardSettings>().PersonalAccessMasksNumber <= 0 || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
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

            // bind data
            this.BindData();
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
                    YafBuildLink.Redirect(ForumPages.editforum, "f={0}", e.CommandArgument);
                    break;
                case "delete":
                    var errorMessage = string.Empty;

                    // Simply Delete the Forum with all of its Content
                    var forumId = this.GetQueryStringAsInt("f");

                    // schedule...
                    ForumDeleteTask.Start(YafContext.Current.PageModuleID, this.PageContext.PageBoardID, forumId.Value, out errorMessage);
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
            // Hide the New Forum Button if there are no Categories.
            // this.AddForumBtn.Visible = this.AddForumBtn.Visible && this.CategoryList.Items.Count < 1;
            // bind data to controls

            // list all access masks for this boeard
            this.List.DataSource = CommonDb.accessmask_pforumlist(mid: PageContext.PageModuleID, boardId: this.PageContext.PageBoardID, accessMaskID: null, excludeFlags: 0, pageUserID: this.PageContext.PageUserID, isUserMask: true, isAdminMask: false);
           
            this.New.Text = this.GetText("ADMIN_ACCESSMASKS", "NEW_MASK");

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
        /// The delete_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void DeleteAccessMask_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // add on click confirm dialog
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ACCESSMASKS", "CONFIRM_DELETE"));
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

        /// <summary>
        /// The list_ item command.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void AccessMaskList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "edit":

                    // redirect to editing page
                    YafBuildLink.Redirect(ForumPages.editaccessmask, "i={0}&u={1}", e.CommandArgument, PageContext.PageUserID);
                    break;
                case "delete":

                    // attmempt to delete access masks
                    if (CommonDb.accessmask_delete(mid: this.PageContext.PageModuleID, accessMaskID: e.CommandArgument))
                    {
                        // remove cache of forum moderators
                        this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);
                        this.BindData();
                    }
                    else
                    {
                        // used masks cannot be deleted
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_ACCESSMASKS", "MSG_NOT_DELETE"));
                    }

                    // quit switch
                    break;
            }
        }

        /// <summary>
        /// The new_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void NewAccessMask_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // redirect to page for access mask creation
            YafBuildLink.Redirect(ForumPages.editaccessmask, "u={0}".FormatWith(PageContext.PageUserID));
        }
    }
}