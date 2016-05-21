namespace YAF.pages
{
    using System;
    using System.Drawing;
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
    /// The personal forums.
    /// </summary>
    public partial class personalforums : ForumPage
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
            this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData);

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
                    YafBuildLink.Redirect(ForumPages.editpersonalforum, "u={0}&f={1}", PageContext.PageUserID, e.CommandArgument);
                    break;
                case "delete":
                    // schedule...
                    string errorMessage;
                    ForumDeleteTask.Start(YafContext.Current.PageModuleID, this.PageContext.PageBoardID, e.CommandArgument.ToType<int>(), out errorMessage);
                    break;
                case "moderate":
                  YafBuildLink.Redirect(ForumPages.moderating, "f={0}", e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        /// The get query string as integer.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
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
            RedirectToPage();
        }

        private void RedirectToPage()
        {
           bool toProfile = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("rp")
            == ForumPages.profile.GetStringValue();
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() > 0 && toProfile)
            {
                YafBuildLink.Redirect(ForumPages.profile, "u={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u")));
            }

            // go back to topic
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() > 0 &&  this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m").ToType<int>() > 0)
            {
                YafBuildLink.Redirect(
                    ForumPages.posts,
                    "m={0}#post{0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m")));
            }

            YafBuildLink.RedirectInfoPage(InfoMessage.Invalid);
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // add forum list
            using (
                var frmList = CommonDb.forum_listreadpersonal(
                    PageContext.PageModuleID,
                    this.PageContext.PageBoardID,
                    PageContext.PageUserID,
                    null,
                    null,
                    this.Get<YafBoardSettings>().UseStyledNicks,
                    this.Get<YafBoardSettings>().UseReadTrackingByDatabase,
                    false,
                    true,
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>()))
            {

                if (frmList == null || frmList.Rows.Count <= 0)
                {
                    // No Access simply redirect back
                    this.RedirectToPage();
                }

                this.ForumList.DataSource = frmList;
            }

            // Hide the New Forum Button if there are no Categories.
            // this.AddForumBtn.Visible = this.AddForumBtn.Visible && this.CategoryList.Items.Count < 1;
            // bind data to controls
            this.DataBind();
        }
    }
}