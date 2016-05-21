namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;
    using VZF.Utils.Helpers;
    using System.Web;

    #endregion

    /// <summary>
    /// Administrative Page for the deleting of forum properties.
    /// </summary>
    public partial class deleteforum : AdminPage
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
            int value;
            if (int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }

            if (this.Request.QueryString.GetFirstOrDefault(name) != null)
            {
                if (this.Request.QueryString.GetFirstOrDefault(name).Contains("_"))
                {

                    return TreeViewUtils.GetParcedTreeNode(this.Request.QueryString.GetFirstOrDefault(name)).ForumId;

                }
            }

            return null;
        }   

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            if (!Config.LargeForumTree)
            {
                this.MoveTopics.CheckedChanged += this.MoveTopics_CheckedChanged;
            }
            this.Delete.Click += this.Save_Click;
            this.Cancel.Click += this.Cancel_Click;

            base.OnInit(e);
        }

          /// <summary>
        /// The on pre render.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            this.PageContext.PageElements.RegisterJQuery();
            this.PageContext.PageElements.RegisterJQueryUI();

           this.PageContext.PageElements.RegisterJsResourceInclude("blockUIJs", "js/jquery.blockUI.js");

            if (Config.LargeForumTree)
            {
                this.MoveTopics.AutoPostBack = false;

                this.ForumList.Visible = false;

                this.jumpList.Visible = true;

                YafContext.Current.PageElements.RegisterJsResourceInclude("fancytree", "js/jquery.fancytree-all.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("css/fancytree/{0}/ui.fancytree.css".FormatWith(YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
                YafContext.Current.PageElements.RegisterJsResourceInclude("ftreedeljs", "js/fancytree.vzf.nodeslazy.min.js");
                var forumId = this.GetQueryStringAsInt("fa");

                string value = null;
                if (this.Request.QueryString.GetFirstOrDefault("fa") != null)
                {
                    if (this.Request.QueryString.GetFirstOrDefault("fa").Contains("_"))
                    {
                        value = this.Request.QueryString.GetFirstOrDefault("fa");
                    }
                }

                string args = "&links=0";
                if (value.IsSet())
                {
                    args +=
                        "&active={0}".FormatWith(value);
                }

                YafContext.Current.PageElements.RegisterJsBlockStartup(
                 "ftreedelfrm", "fancyTreeSelectSingleNodeLazyJs('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');"
                    .FormatWith(Config.JQueryAlias, "treedelfrm",
                    PageContext.PageUserID,
                       PageContext.PageBoardID,
                       "echoActive",
                       string.Empty,
                       args,
                       "{0}resource.ashx?tjl".FormatWith(YafForumInfo.ForumClientFileRoot),
                       "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));
                
                /*
                   JavaScriptBlocks.FancyTreeSelectSingleNodeLazyJs(
                       "treedelfrm",
                       PageContext.PageUserID,
                       PageContext.PageBoardID,
                       "echoActive",
                       string.Empty,
                       args,
                       "{0}resource.ashx?tjl".FormatWith(YafForumInfo.ForumClientFileRoot),
                       "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));*/

                /*   YafContext.Current.PageElements.RegisterJsBlock(
                 "ftreedelfrm",
                 JavaScriptBlocks.FancyTreeSelectSingleNodeLazyJsNew(
                     "treedelfrm",
                     PageContext.PageBoardID,
                     "echoActive",
                     Config.BaseScriptFile,
                     args,
                     "{0}resource.ashx?tjl".FormatWith(YafForumInfo.ForumClientFileRoot),
                     "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath())))); */            
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {        

            if (this.IsPostBack)
            {
                return;
            }

            this.LoadingImage.ImageUrl = YafForumInfo.GetURLToResource("images/loader.gif");

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            this.PageLinks.AddLink(this.GetText("TEAM", "FORUMS"), YafBuildLink.GetLink(ForumPages.admin_forums));
            this.PageLinks.AddLink(this.GetText("ADMIN_DELETEFORUM", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("TEAM", "FORUMS"),
                this.GetText("ADMIN_DELETEFORUM", "TITLE"));

            this.Delete.Text = this.GetText("ADMIN_DELETEFORUM", "DELETE_FORUM");
            this.Cancel.Text = this.GetText("CANCEL");

            this.Delete.Attributes["onclick"] =
                "return (confirm('{0}') && confirm('{1}'));".FormatWith(
                    this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE"),
                    this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_POSITIVE"));

            this.BindData();
  
            var forumId = this.GetQueryStringAsInt("fa");

            using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, forumId.Value))
            {
                DataRow row = dt.Rows[0];

                this.ForumNameTitle.Text = (string)row["Name"];

                // populate parent forums list with forums according to selected category
                this.BindParentList();
            }
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
            if (this.Get<ITaskModuleManager>().TryGetTask(ForumDeleteTask.TaskName, out task) && task.IsRunning)
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
            // Load forum's combo
            if (!Config.LargeForumTree)
            {
                this.BindParentList();
            }
        }

        /// <summary>
        /// Binds the parent list.
        /// </summary>
        private void BindParentList()
        {
            this.ForumList.DataSource = CommonDb.forum_listall(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageUserID);

            this.ForumList.DataValueField = "ForumID";
            this.ForumList.DataTextField = "Forum";
            this.ForumList.DataBind();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the MoveTopics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MoveTopics_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MoveTopics.Checked)
            {
                this.Delete.Attributes.Remove("onclick");
            }
            else
            {
                this.Delete.Attributes["onclick"] =
                    "return (confirm('{0}') && confirm('{1}'));".FormatWith(
                        this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE"),
                        this.GetText("ADMIN_FORUMS", "CONFIRM_DELETE_POSITIVE"));
            }

            if (!Config.LargeForumTree)
            {
                this.ForumList.Enabled = this.MoveTopics.Checked;
            }           
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
        }

        /// <summary>
        /// Delete The Forum
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var errorMessage = string.Empty;
          
            if (this.MoveTopics.Checked)
            {
                // Simply Delete the Forum with all of its Content
                var forumId = this.GetQueryStringAsInt("fa");
                int? selectedForum = 0;
                
                if (!Config.LargeForumTree)
                {
                    selectedForum = this.ForumList.SelectedValue.ToType<int>();
                }
                else
                {
                    string val = this.Get<IYafSession>().NntpTreeActiveNode;

                    if (val.IsSet())
                    {
                        string[] valArr = val.Split('_');

                        if (valArr.Length == 2)
                        {
                            this.PageContext.AddLoadMessage("You should select a forum, not a category");

                            // this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                            return;
                        }

                        if (valArr.Length == 3)
                        {
                            selectedForum = valArr[2].ToType<int>();
                        }
                        else
                        {
                            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                            return;
                        }                       
                    }

                }

                // schedule...
                ForumDeleteTask.Start( this.PageContext.PageModuleID,
                    this.PageContext.PageBoardID, forumId.Value, (int)selectedForum, out errorMessage);

                this.Get<IYafSession>().NntpTreeActiveNode = null;

                // enable timer...
                this.UpdateStatusTimer.Enabled = true;

                this.LocalizedLabel6.LocalizedTag = "DELETE_MOVE_TITLE";

                // show blocking ui...
                this.PageContext.PageElements.RegisterJsBlockStartup(
                    "BlockUIExecuteJs", JavaScriptBlocks.BlockUIExecuteJs("DeleteForumMessage"));
            }
            else
            {
                // Simply Delete the Forum with all of its Content
                var forumId = this.GetQueryStringAsInt("fa");

                // schedule...
                ForumDeleteTask.Start(YafContext.Current.PageModuleID, this.PageContext.PageBoardID, forumId.Value, out errorMessage);

                // enable timer...
                this.UpdateStatusTimer.Enabled = true;

                this.LocalizedLabel6.LocalizedTag = "DELETE_TITLE";

                // show blocking ui...
                this.PageContext.PageElements.RegisterJsBlockStartup(
                    "BlockUIExecuteJs", JavaScriptBlocks.BlockUIExecuteJs("DeleteForumMessage"));
            }

            // show blocking ui...
            this.PageContext.PageElements.RegisterJsBlockStartup(
                "BlockUIExecuteJs", JavaScriptBlocks.BlockUIExecuteJs("DeleteForumMessage"));

            // TODO : Handle Error Message?!
        }

        #endregion

        protected void JumpListPrerender__(object sender, EventArgs e)
        {
                     
      
        }
    }
}