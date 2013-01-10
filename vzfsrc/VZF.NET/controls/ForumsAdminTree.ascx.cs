namespace YAF.Controls
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utilities;
    using YAF.Utils;

    /// <summary>
    /// The forums admin tree.
    /// </summary>
    public partial class ForumsAdminTree : BaseUserControl
    {
        private bool _adminMode;

        /// <summary>
        ///   Gets or sets Alt.
        /// </summary>
     /*   [NotNull]
        public bool AdminMode
        {
            get
            {
                return _adminMode.ToType<bool>();
            }

            set
            {
                _adminMode = value;
            }
        } */

        #region Methods

        /// <summary>
        /// The delete category_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void DeleteCategory_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_CAT"));
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
                    YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}", e.CommandArgument);
                    break;
                case "copy":
                    YafBuildLink.Redirect(ForumPages.admin_editforum, "copy={0}", e.CommandArgument);
                    break;
                case "delete":
                    YafBuildLink.Redirect(ForumPages.admin_deleteforum, "f={0}", e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        /// The new category_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void NewCategory_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_editcategory);
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
            YafBuildLink.Redirect(ForumPages.admin_editforum);
        }

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            // this.Get<IYafSession>().ForumTreeChangerActiveNode = null;
            // this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;

            if (Config.LargeForumTree)
            {
                YafContext.Current.PageElements.RegisterJsResourceInclude("dynatree", "js/jquery.dynatree.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("js/skin/ui.dynatree.css");
                this.divactive.Visible = true;
                YafContext.Current.PageElements.RegisterJsBlock(
                    "dynatreescr",
                    JavaScriptBlocks.DynatreeGetNodesAdminLazyJS(
                        "tree",
                        PageContext.PageUserID,
                        PageContext.PageBoardID,
                        "echoActive",
                        "&v=2",
                        "{0}resource.ashx?".FormatWith(YafForumInfo.ForumClientFileRoot),
                        "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));
            }

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("TEAM", "FORUMS"));

            this.NewCategory.Text = this.GetText("ADMIN_FORUMS", "NEW_CATEGORY");
            this.NewForum.Text = this.GetText("ADMIN_FORUMS", "NEW_FORUM");

            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            using (DataSet ds = CommonDb.ds_forumadmin(PageContext.PageModuleID, this.PageContext.PageBoardID))
            {
                var dd = ds.Tables[CommonSqlDbAccess.GetObjectName("Category")];
                this.CategoryList.DataSource = dd;
            }

            // Hide the New Forum Button if there are no Categories.
            this.NewForum.Visible = this.CategoryList.Items.Count < 1;

            this.DataBind();
        }

        /// <summary>
        /// The category list_ item command.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CategoryList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "edit":
                    YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}", e.CommandArgument);
                    break;
                case "delete":
                    if (CommonDb.category_delete(PageContext.PageModuleID, e.CommandArgument))
                    {
                        this.BindData();
                        this.ClearCaches();
                    }
                    else
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
                    }

                    break;
            }
        }

        /// <summary>
        /// The clear caches.
        /// </summary>
        private void ClearCaches()
        {
            // clear moderatorss cache
            this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

            // clear category cache...
            this.Get<IDataCache>().Remove(Constants.Cache.ForumCategory);

            // clear active discussions cache..
            this.Get<IDataCache>().Remove(Constants.Cache.ForumActiveDiscussions);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           this.CategoryList.ItemCommand += this.CategoryList_ItemCommand;
        }

        /// <summary>
        /// The delete forum btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void DeleteForumBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("delete");
        }

        /// <summary>
        /// The copy forum btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void CopyForumBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("copy");
        }

        /// <summary>
        /// The move forum before btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void MoveForumBeforeBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("movebefore");
        }

        /// <summary>
        /// The move forum after btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void MoveForumAfterBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("moveafter");
        }

        /// <summary>
        /// The edit forum btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void EditForumBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("edit");
        }

        /// <summary>
        /// The add children to btn_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void AddChildrenToBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SplitNodeToObjects("addchildren");
        }

        /// <summary>
        /// The split node to objects.
        /// </summary>
        /// <param name="mode">
        /// The mode.
        /// </param>
        private void SplitNodeToObjects(string mode)
        {
            var nodeIds = this.Get<IYafSession>().ForumTreeChangerActiveNode;
            string targetIds = this.Get<IYafSession>().ForumTreeChangerActiveTargetNode;
            int boardId = 0;
            int categoryId = 0;
            int forumId = 0;

            int boardIdTarget = 0;
            int categoryIdTarget = 0;
            int forumIdTarget = 0;

            if (nodeIds.IsSet())
            {
                string[] nodeId = nodeIds.Split('_');
                switch (nodeId.Length)
                {
                    case 1:
                        boardId = nodeId[0].ToType<int>();
                        break;
                    case 2:
                        categoryId = nodeId[1].ToType<int>();
                        break;
                    case 3:
                        forumId = nodeId[2].ToType<int>();
                        break;
                }
            }

            if (targetIds.IsSet())
            {
                string[] nodeId = targetIds.Split('_');
                switch (nodeId.Length)
                {
                    case 1:
                        boardIdTarget = nodeId[0].ToType<int>();
                        break;
                    case 2:
                        categoryIdTarget = nodeId[1].ToType<int>();
                        break;
                    case 3:
                        forumIdTarget = nodeId[2].ToType<int>();
                        break;
                }
            }


            switch (mode)
            {
                case "delete":
                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_deleteforum, "f={0}", forumId);
                    }

                    if (categoryId > 0)
                    {
                        if (CommonDb.category_delete(PageContext.PageModuleID, categoryId))
                        {
                            this.BindData();
                            this.ClearCaches();
                        }
                        else
                        {
                            this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
                        }

                        return;
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_boards);
                    }

                    break;
                case "copy":
                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "copy={0}", forumId);
                    }

                    if (categoryId > 0 || boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums);
                    }

                    break;
                case "edit":
                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}", forumId);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}", categoryId);
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;
                case "movebefore":
                    if (forumIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}&before={1}", forumIdTarget, nodeIds);
                    }

                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}&before={1}", categoryIdTarget, nodeIds);
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", categoryId);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", forumId);
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;
                case "moveafter":
                    if (forumIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}&before={1}", forumIdTarget, nodeIds);
                    }

                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}&before={1}", categoryIdTarget, nodeIds);
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", categoryId);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", forumId);
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;

                case "addchildren":
                    if (forumIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}&before={1}&child=1", forumIdTarget, nodeIds);
                    }

                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}&before={1}&child=1", categoryIdTarget, nodeIds);
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", categoryId);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "before={0}", forumId);
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;
            }
        }

        private void ResetSession()
        {
            this.Get<IYafSession>().ForumTreeChangerActiveNode = null;
            this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;
        }

        #endregion
    }
}