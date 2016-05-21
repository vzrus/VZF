namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Collections.Generic;

    using System.Data;

    using VZF.Data.Common;
    using VZF.Types.Objects;
    using VZF.Utilities;
    using VZF.Utils.Extensions;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Administrative Page for the deleting of forum properties.
    /// </summary>
    public partial class deletecategory : AdminPage
    {
        #region Methods

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
            if (this.Request.QueryString.GetFirstOrDefault(name) == null)
            {
                return null;
            }

            if (this.Request.QueryString.GetFirstOrDefault(name).Contains("_"))
            {
                return TreeViewUtils.GetParcedTreeNode(this.Request.QueryString.GetFirstOrDefault(name)).CategoryId;
            }

            int value;
            if (int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.Delete.Click += this.Save_Click;
            this.Cancel.Click += this.Cancel_Click;

            base.OnInit(e);
        }

        protected override void OnPreRender([NotNull] EventArgs e)
        {
            this.PageContext.PageElements.RegisterJQueryUI();
            this.PageContext.PageElements.RegisterJsResourceInclude("blockUIJs", "js/jquery.blockUI.js");

            DataTable ctbl = CommonDb.category_list(
                YafContext.Current.PageModuleID,
                PageContext.PageBoardID, null);

            var collection = new List<TreeNode>();

            foreach (DataRow row in ctbl.Rows)
            {
                // Don't add the category itself.
                if (this.GetQueryStringAsInt("fa") == (int)row["CategoryID"])
                {
                    continue;
                }

                collection.Add(new TreeNode
                {
                    title = row["Name"].ToString(),
                    key = PageContext.PageBoardID + "_" + row["CategoryID"],
                    lazy = true,
                    folder = true,
                    expanded = false,
                    selected = false,
                    extraClasses = string.Empty,
                    tooltip = string.Empty
                });
            }

            YafContext.Current.PageElements.RegisterJsResourceInclude("fancytree", "js/jquery.fancytree-all.min.js");
            YafContext.Current.PageElements.RegisterCssIncludeResource(
                "css/fancytree/{0}/ui.fancytree.css".FormatWith(
                    YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
            YafContext.Current.PageElements.RegisterJsResourceInclude("ftreedeljs",
                "js/fancytree.vzf.nodesadmincategorylist.js");
            YafContext.Current.PageElements.RegisterJsBlockStartup(
                "ftreedelcat",
                "fancyTreeGetCategoriesListJs('{0}','{1}','{2}','{3}','{4}');"
                .FormatWith(
                Config.JQueryAlias,
                "treedelcat",
                "You selected category ",
                "{0}resource.ashx".FormatWith(YafForumInfo.ForumClientFileRoot),
                collection.ToJson()));

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Request.QueryString.GetFirstOrDefault("fa") == null)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.AccessDenied);
            }

            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            this.PageLinks.AddLink(this.GetText("TEAM", "FORUMS"), YafBuildLink.GetLink(ForumPages.admin_forums));
            this.PageLinks.AddLink(this.GetText("ADMIN_DELETECATEGORY", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("TEAM", "FORUMS"),
                this.GetText("ADMIN_DELETECATEGORY", "TITLE"));

            this.Delete.Text = this.GetText("ADMIN_DELETECATEGORY", "DELETE_CATEGORY");
            this.Cancel.Text = this.GetText("CANCEL");

            this.Delete.Attributes["onclick"] =
                "return (confirm('{0}') && confirm('{1}'));".FormatWith(
                    this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_CAT"),
                    this.GetText("ADMIN_DELETECATEGORY", "CONFIRM_DELETE_POSITIVE"));

            this.CategoryNameTitle.Text = CommonDb.category_list(
                PageContext.PageModuleID,
                PageContext.PageBoardID, 
                this.GetQueryStringAsInt("fa")).Rows[0]["Name"].ToString();

            this.LoadingImage.ImageUrl = YafForumInfo.GetURLToResource("images/loader.gif");
            this.BindData();
        }

        /// <summary>
        /// The update status timer_ tick.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void UpdateStatusTimer_Tick([NotNull] object sender, [NotNull] EventArgs e)
        {
            IBackgroundTask task;

            // see if the migration is done....
            if (this.Get<ITaskModuleManager>().TryGetTask(CategoryDeleteTask.TaskName, out task) && task.IsRunning)
            {
                // continue...
                return;
            }

            this.UpdateStatusTimer.Enabled = false;

            // rebind...
            this.BindData();

            // clear caches...
            this.ClearCaches();

            YafBuildLink.Redirect(ForumPages.admin_forums);
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
        }

        /// <summary>
        /// Cancel Deleting and Redirecting back to The Admin Forums Page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_forums);
        }

        /// <summary>
        /// Clears the caches.
        /// </summary>
        private void ClearCaches()
        {
            // clear moderatorss cache
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

            // clear category cache...
            this.Get<IDataCache>().Remove(Constants.Cache.ForumCategory);

            // clear active discussions cache..
            this.Get<IDataCache>().Remove(Constants.Cache.ForumActiveDiscussions);

            this.Get<IYafSession>().NntpTreeActiveNode = null;
        }

        /// <summary>
        /// Delete The Forum
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var errorMessage = string.Empty;

            int? categoryToMoveForums = null;

            string val = this.Get<IYafSession>().NntpTreeActiveNode;

            if (val.IsSet())
            {
                string[] valArr = val.Split('_');

                if (valArr.Length == 3)
                {
                    return;
                }

                if (valArr.Length == 2)
                {
                    categoryToMoveForums = valArr[1].ToType<int>();
                }
                else
                {
                    return;
                }
            }

            // Simply Delete the Category if it's empty.
            var categoryId = this.GetQueryStringAsInt("fa");

            if (CategoryDeleteTask.Start(
                YafContext.Current.PageModuleID, 
                categoryId.Value, 
                categoryToMoveForums,
                out errorMessage))
            {
                this.ClearCaches();
            }
            else
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
            }

            // schedule...
            // enable timer...
            this.UpdateStatusTimer.Enabled = true;

            this.LocalizedLabel6.LocalizedTag = "DELETE_TITLE";

            // show blocking ui...
            this.PageContext.PageElements.RegisterJsBlockStartup(
                "BlockUIExecuteJs", JavaScriptBlocks.BlockUIExecuteJs("DeleteCategoryMessage"));
        }

        #endregion
    }
}