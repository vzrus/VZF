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

namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utilities;
    using YAF.Utils;
    using YAF.Utils.Helpers;

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
            if (Config.LargeForumTree)
            {
                if (Page.Request.QueryString.GetFirstOrDefault("node") == null
                    && Page.Request.QueryString.GetFirstOrDefault("action") == null)
                {
                    // this.Get<IYafSession>().ForumTreeChangerActiveNode = null;
                    this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;
                }

                bool reload = true;
                if (Page.Request.QueryString.GetFirstOrDefault("action") != null)
                {
                    if (Page.Request.QueryString.GetFirstOrDefault("action") == "moveafter")
                    {
                        this.MoveForumBeforeBtn.Visible = false;
                        this.EditForumBtn.Visible = false;
                        this.CopyForumBtn.Visible = false;
                        this.DeleteForumBtn.Visible = false;
                        this.AddForumBtn.Visible = false;
                        this.AddChildrenTo.Visible = false;

                        this.ActionTipLbl.Text = this.Get<ILocalization>()
                                                     .GetText("ADMIN_FORUMS", "FORUM_SELECTAFTERNODE_MSG");
                    }

                    if (Page.Request.QueryString.GetFirstOrDefault("action") == "movebefore")
                    {
                        this.MoveForumAfterBtn.Visible = false;
                        this.EditForumBtn.Visible = false;
                        this.CopyForumBtn.Visible = false;
                        this.DeleteForumBtn.Visible = false;
                        this.AddForumBtn.Visible = false;
                        this.AddChildrenTo.Visible = false;

                        this.ActionTipLbl.Text = this.Get<ILocalization>()
                                                     .GetText("ADMIN_FORUMS", "FORUM_SELECTBEFORENODE_MSG");
                    }

                    if (Page.Request.QueryString.GetFirstOrDefault("action") == "addchildren")
                    {
                        this.MoveForumAfterBtn.Visible = false;
                        this.EditForumBtn.Visible = false;
                        this.CopyForumBtn.Visible = false;
                        this.DeleteForumBtn.Visible = false;
                        this.AddForumBtn.Visible = false;
                        this.MoveForumBeforeBtn.Visible = false;
                        this.ActionTipLbl.Text = this.Get<ILocalization>()
                                                     .GetText("ADMIN_FORUMS", "FORUM_SELECTPARENTNODE_MSG");
                    }

                    if (Page.Request.QueryString.GetFirstOrDefault("action") == "add")
                    {
                        this.MoveForumBeforeBtn.Visible = false;
                        this.EditForumBtn.Visible = false;
                        this.CopyForumBtn.Visible = false;
                        this.DeleteForumBtn.Visible = false;
                        this.AddForumBtn.Visible = false;
                        this.ActionTipLbl.Text = this.Get<ILocalization>()
                                                     .GetText("ADMIN_FORUMS", "FORUM_SELECTAFTERNODE_MSG");
                        reload = false;
                    }
                }

                int boardId = 0;
                int categoryId = 0;
                int forumId = 0;

                if (Page.Request.QueryString.GetFirstOrDefault("node") != null)
                {
                    TreeViewUtils.TreeNodeIdParser(
                        Page.Request.QueryString.GetFirstOrDefault("node"), out forumId, out categoryId, out boardId);

                    string[] nodeId = Page.Request.QueryString.GetFirstOrDefault("node").Split('_');

                    if (nodeId.Length > 1)
                    {
                        this.ActionTipLbl2.Text = this.Get<ILocalization>()
                                                      .GetText("ADMIN_FORUMS", "FORUM_SELECTEDNODE_MSG");
                        this.PageLinks1.AddLink(
                            this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                        this.PageLinks1.AddLink(
                            this.PageContext.PageCategoryName,
                            YafBuildLink.GetLink(ForumPages.forum, "c={0}", categoryId));
                        this.PageLinks1.AddForumLinks(forumId, true);
                    }
                }

                this.divactive.Visible = Config.LargeForumTree;

                if (reload)
                {
                    tviewcontainer.Visible = true;
                    YafContext.Current.PageElements.RegisterJsResourceInclude("yafjs", "js/vzfDynatree.js");
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
                            @"&v=2",
                            @"{0}resource.ashx".FormatWith(YafForumInfo.ForumClientFileRoot),
                            "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));
                }
            }

            if (this.IsPostBack)
            {
                return;
            }


            if (Config.LargeForumTree)
            {
                this.ActionTipLbl.Text = this.Get<ILocalization>().GetText("ADMIN_FORUMS", "FORUM_SELECTANODE_MSG");
            }
            else
            {
                this.NewCategory.Text = this.GetText("ADMIN_FORUMS", "NEW_CATEGORY");
                this.NewForum.Text = this.GetText("ADMIN_FORUMS", "NEW_FORUM"); 
            }

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("TEAM", "FORUMS"));

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
            this.AddForumBtn.Visible = this.AddForumBtn.Visible && this.CategoryList.Items.Count < 1;

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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTODELETENODE_MSG"));
                return;
            }

            this.SplitNodeToObjects("delete");
        }

        protected void AddForumBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
           {
               this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTARGETNODE_MSG"));
                return;
           }

            this.SplitNodeToObjects("add");
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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTOMOVENODE_MSG"));
                return;
            }

            var nodeIds = this.Get<IYafSession>().ForumTreeChangerActiveNode;
            string targetIds = this.Get<IYafSession>().ForumTreeChangerActiveTargetNode;

            // we can't add a category as a child of other category
            if (targetIds.IsSet() && targetIds.Split('_').Length == 2 && nodeIds.Split('_').Length == 3)
            {
                this.MoveForumAfterBtn.Visible = true;
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "CATEGORY_CANTADDFORUMBEFORE_MSG"));
                return;
            }

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
            var dd = Page.Request.QueryString;
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTTOMOVENODE_MSG"));
                return;
            }

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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTEDITEDNODE_MSG"));
                return;
            }

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
            if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "FORUM_SELECTPARENTNODE_MSG"));
                return;
            }

            var nodeIds = this.Get<IYafSession>().ForumTreeChangerActiveNode;
            string targetIds = this.Get<IYafSession>().ForumTreeChangerActiveTargetNode;

            // we can't add a category as a child of other category
            if (targetIds.IsSet() && targetIds.Split('_').Length == nodeIds.Split('_').Length && nodeIds.Split('_').Length == 3)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "CATEGORY_CANTADDASCHILD_MSG"));
                return;
            }

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

            if (nodeIds.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
            }

            int boardId;
            int categoryId;
            int forumId;

            int boardIdTarget;
            int categoryIdTarget;
            int forumIdTarget;

            string addnew = null;
            if (this.Get<IYafSession>().ForumAdminTreeAddForum == 1)
            {
                addnew = "&new=1";
                targetIds = nodeIds;
            }

            TreeViewUtils.TreeNodeIdParser(nodeIds, out forumId, out categoryId, out boardId);
            TreeViewUtils.TreeNodeIdParser(targetIds, out forumIdTarget, out categoryIdTarget, out boardIdTarget);
           

            switch (mode)
            {
                case "delete":
                    if (forumId > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_deleteforum, "f={0}", forumId);
                    }

                    if (categoryId > 0)
                    {
                        if (CommonDb.category_delete(PageContext.PageModuleID, categoryId))
                        {
                            this.ResetSession();
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
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_boards);
                    }

                    break;
                case "add":
                    if (forumId > 0)
                    {
                        this.Get<IYafSession>().ForumAdminTreeAddForum = 1;
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (categoryId > 0)
                    {
                        this.Get<IYafSession>().ForumAdminTreeAddForum = 1;
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (boardId > 0)
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MESSAGE_CANTADDTOBOARD"));
                    }

                    break;

                case "copy":
                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editforum, "{0}={1}", mode, forumId);
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
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}", categoryId);
                    }

                    if (boardId > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;
                case "movebefore":
                    if (forumIdTarget > 0)
                    {
                        // move forumId before a forum with path targetIds
                        this.ResetSession();
                        YafBuildLink.Redirect(
                            ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, targetIds, mode, addnew);
                    }

                    // a category was selected as a destination
                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        if (forumId > 0)
                        {
                            // we can't move forum before a category, return back
                            YafBuildLink.Redirect(ForumPages.admin_forums);
                        }
                        else
                        {
                            // a category was selected as a target and a category as a destination 
                            // we change a category sort order here
                            YafBuildLink.Redirect(
                            ForumPages.admin_editcategory, "c={0}&{1}={2}", categoryIdTarget, mode, nodeIds);
                        }
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
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
                        YafBuildLink.Redirect(
                            ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                    }

                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        if (forumId > 0)
                        {
                            YafBuildLink.Redirect(
                               ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                        }
                        else
                        {
                            // a category was selected as a target and a category as a destination 
                            // we change a category sort order here
                            YafBuildLink.Redirect(
                            ForumPages.admin_editcategory, "c={0}&{1}={2}", categoryIdTarget, mode, nodeIds); 
                        }
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
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
                        YafBuildLink.Redirect(
                            ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                    }

                    if (categoryIdTarget > 0)
                    {
                        this.ResetSession();
                        if (forumId > 0)
                        {
                            YafBuildLink.Redirect(
                               ForumPages.admin_editforum, "c={0}&{1}={2}&child=1", forumId, mode, targetIds);
                        }
                        else
                        {
                            // we can't add a category as a child of other category
                            YafBuildLink.Redirect(ForumPages.admin_forums);
                        }
                    }

                    if (boardIdTarget > 0)
                    {
                        this.ResetSession();
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    if (forumId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (categoryId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                    }

                    if (boardId > 0)
                    {
                        YafBuildLink.Redirect(ForumPages.admin_editboard);
                    }

                    break;
            }
        }

        /// <summary>
        /// The reset session.
        /// </summary>
        private void ResetSession()
        {
            this.Get<IYafSession>().ForumTreeChangerActiveNode = null;
            this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;
            this.Get<IYafSession>().ForumAdminTreeAddForum = null;
        }

        #endregion
    }
}