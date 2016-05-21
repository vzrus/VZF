namespace YAF.pages
{
    using System;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Linq;
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
    using YAF.Types.Interfaces;

    /// <summary>
    /// The personalforum.
    /// </summary>
    public partial class personalaccessmask : ForumPage
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
            this.PageLinks.AddLink(
                this.Get<YafBoardSettings>().EnableDisplayName
                    ? this.PageContext.CurrentUserData.DisplayName
                    : this.PageContext.PageUserName,
                YafBuildLink.GetLink(ForumPages.cp_profile));

            // title
            this.PageLinks.AddLink(this.GetText("PERSONALACCESSMASK", "TITLE"), string.Empty);

            this.Page.Header.Title =
                "{0} - {1}".FormatWith(
                    this.Get<YafBoardSettings>().EnableDisplayName
                        ? this.PageContext.CurrentUserData.DisplayName
                        : this.PageContext.PageUserName,
                    this.GetText("PERSONALACCESSMASK", "TITLE"));
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
            return currentRow["Flags"].BinaryAnd(2)
                       ? this.GetText("ADMIN_GROUPS", "UNLINKABLE")
                       : this.GetText("ADMIN_GROUPS", "LINKED");
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
            if (PageContext.UsrPersonalMasks <= 0
                || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null
                || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>()
                != PageContext.PageUserID)
            {
                YafBuildLink.AccessDenied();
            }

            // this needs to be done just once, not during postbacks
            if (this.IsPostBack)
            {
                return;
            }

            if (PageContext.PersonalAccessMasksNumber < PageContext.UsrPersonalMasks)
            {
                this.NewAccessMaskBtn.Visible = true;
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
                    YafBuildLink.Redirect(
                        ForumPages.editpersonalforum, "u={0}&f={0}", PageContext.PageUserID, e.CommandArgument);
                    break;
                case "delete":
                    // schedule...
                    string errorMessage;
                    ForumDeleteTask.Start(
                        YafContext.Current.PageModuleID, this.PageContext.PageBoardID, (int)e.CommandArgument, out errorMessage);
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

        #endregion

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.Cancel.Click += this.Cancel_Click;
            base.OnInit(e);
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
            // number of current access masks changed
            this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData);

            // add on click confirm dialog
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("ADMIN_ACCESSMASKS", "CONFIRM_DELETE"));
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
                    YafBuildLink.Redirect(
                        ForumPages.editaccessmask, "i={0}&u={1}", e.CommandArgument, PageContext.PageUserID);
                    break;
                case "delete":

                    // attmempt to delete access masks
                    if (CommonDb.accessmask_delete(mid: this.PageContext.PageModuleID, accessMaskId: e.CommandArgument))
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
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // Hide the NewAccessMaskBtn Forum Button if there are no Categories.
            // this.AddForumBtn.Visible = this.AddForumBtn.Visible && this.CategoryList.Items.Count < 1;
            // bind data to controls

            // list all access masks for this boeard
            this.List.DataSource = CommonDb.accessmask_pforumlist(
              mid: PageContext.PageModuleID,
              boardId: this.PageContext.PageBoardID,
              accessMaskId: null,
              excludeFlags: 0,
              pageUserId: this.PageContext.PageUserID,
              isUserMask: true,            
              isCommonMask: !this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums); 

            this.DataBind();
        }
    }
}