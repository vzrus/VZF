﻿using System.Collections.Generic;
using VZF.Utils.Extensions;

namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;
    using VZF.Data.DAL;

    #endregion

    /// <summary>
    /// Summary description for forums.
    /// </summary>
    public partial class forums : AdminPage
    {
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
            ((ThemeButton) sender).Attributes["onclick"] =
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
            ((ThemeButton) sender).Attributes["onclick"] =
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
                    YafBuildLink.Redirect(ForumPages.admin_editforum, "fa={0}", e.CommandArgument);
                    break;
                case "copy":
                    YafBuildLink.Redirect(ForumPages.admin_editforum, "copy={0}", e.CommandArgument);
                    break;
                case "delete":
                    YafBuildLink.Redirect(ForumPages.admin_deleteforum, "fa={0}", e.CommandArgument);
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
            /* if (!Page.IsPostBack)
            {
                this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;
            } */

            this.NewForum.Visible = this.NewCategory.Visible = !Config.LargeForumTree;

            if (Config.LargeForumTree)
            {
                tviewcontainer.Visible = true;
                this.TreeMenuTip.LocalizedPage = "ADMIN_FORUMS";
                this.TreeMenuTip.LocalizedTag = "TREEVIEW_MENUTIPS";
                //  YafContext.Current.PageElements.RegisterJsResourceInclude("yafjs", "js/vzfDynatree.js");

                YafContext.Current.PageElements.RegisterJsResourceInclude("fancytree", "js/jquery.fancytree-all.min.js");
                YafContext.Current.PageElements.RegisterJsResourceInclude("contextmenu",
                    "js/jquery.ui-contextmenu.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("css/fancytree/{0}/ui.fancytree.css".FormatWith(YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
                YafContext.Current.PageElements.RegisterJsResourceInclude("ftreedeljs",
                    "js/fancytree.vzf.nodesadmintreelazy.js");

                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "fancytreescr",
                      "fancytreeGetNodesAdminLazyJs('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');"
                        .FormatWith(
                        Config.JQueryAlias,
                        "ftree3",
                        PageContext.PageUserID,
                        PageContext.PageBoardID,
                        this.GetText("COMMON", "IMPOSSIBLE_ACTION"),
                        @"&v=2",
                        @"{0}resource.ashx".FormatWith(YafForumInfo.ForumClientFileRoot),
                        "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath())),
                        new Dictionary<string, string>
                        {
                            {"delete", this.GetText("COMMON", "DELETE")},
                            {"edit", this.GetText("COMMON", "EDIT")},
                            {"new", this.GetText("COMMON", "NEW")},
                            {"category", this.GetText("DEFAULT", "CATEGORY")},
                            {"forum", this.GetText("DEFAULT", "FORUM")},
                            {"before", this.GetText("COMMON", "BEFORE")},
                            {"after", this.GetText("COMMON", "AFTER")},
                            {"over", this.GetText("COMMON", "CHILD")}
                        }.ToJson()
                        ));
            }

            if (this.IsPostBack)
            {
                return;
            }


            this.NewCategory.Text = this.GetText("ADMIN_FORUMS", "NEW_CATEGORY");
            this.NewForum.Text = this.GetText("ADMIN_FORUMS", "NEW_FORUM");


            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("TEAM", "FORUMS"));

            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (!Config.LargeForumTree)
            {
                using (
                    DataSet ds = CommonDb.ds_forumadmin(PageContext.PageModuleID, this.PageContext.PageBoardID, null,
                        false))
                {
                    var dd = ds.Tables[SqlDbAccess.GetVzfObjectName("Category", PageContext.PageModuleID)];
                    this.CategoryList.DataSource = dd;
                }
            }

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
                    if (CommonDb.category_delete(PageContext.PageModuleID, e.CommandArgument, null))
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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTODELETENODE_MSG"));
                return;
            }

            switch (this.Get<IYafSession>().ForumTreeChangerActiveNode.Split('_').Length)
            {
                case 1:
                    if (CommonDb.category_delete(PageContext.PageModuleID,
                        this.Get<IYafSession>().ForumTreeChangerActiveNode, null))
                    {
                        this.BindData();
                        this.ClearCaches();
                    }
                    else
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
                    }

                    break;
                case 2:
                    YafBuildLink.Redirect(ForumPages.admin_deleteforum, "f={0}",
                        this.Get<IYafSession>().ForumTreeChangerActiveNode);
                    break;
                default:
                    YafBuildLink.Redirect(ForumPages.admin_editboard);
                    break;
            }
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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTOCOPYNODE_MSG"));
                return;
            }

            switch (this.Get<IYafSession>().ForumTreeChangerActiveNode.Split('_').Length)
            {
                case 2:
                    YafBuildLink.Redirect(ForumPages.admin_editforum, "copy={0}",
                        this.Get<IYafSession>().ForumTreeChangerActiveNode);
                    break;
                default:
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTOCOPYNODE_MSG"));
                    break;
            }
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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTEDITEDNODE_MSG"));
                return;
            }

            switch (this.Get<IYafSession>().ForumTreeChangerActiveNode.Split('_').Length)
            {
                case 1:
                    YafBuildLink.Redirect(ForumPages.admin_editboard);
                    break;
                case 2:
                    YafBuildLink.Redirect(ForumPages.admin_editforum);
                    break;
                case 3:
                    YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}",
                        this.Get<IYafSession>().ForumTreeChangerActiveNode);
                    break;
            }
        }

        #endregion
    }
}