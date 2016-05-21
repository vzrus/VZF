using System.Globalization;
using System.Web;
using VZF.Utilities;
using VZF.Utils.Helpers;

namespace YAF.Pages
{
    #region Using

    using System;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Summary description for movetopic.
    /// </summary>
    public partial class movemessage : ForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "movemessage" /> class.
        /// </summary>
        public movemessage()
            : base("MOVEMESSAGE")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create and move_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void CreateAndMove_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
                if (this.TopicSubject.Text != string.Empty)
                {
                    long nTopicId = 0;
                    if (!Config.LargeForumTree)
                    {
                     nTopicId = CommonDb.topic_create_by_message(PageContext.PageModuleID,
                        this.Request.QueryString.GetFirstOrDefault("m"), this.ForumList.SelectedValue,
                        this.TopicSubject.Text);
                    
                    }
                    else
                    {
                        string val = this.Get<IYafSession>().NntpTreeActiveNode;
                        if (val.IsSet())
                        {
                            string[] valArr = val.Split('_');

                            if (valArr.Length == 2)
                            {
                                this.PageContext.AddLoadMessage(this.GetText("CANNOT_MOVE_TO_CATEGORY"));

                                // this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                                return;
                            }

                            int? selectedForum;
                            if (valArr.Length == 3)
                            {
                                selectedForum = valArr[2].ToType<int>();
                            }
                            else
                            {
                                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                                return;
                            }

                             nTopicId = CommonDb.topic_create_by_message(PageContext.PageModuleID,
                        this.Request.QueryString.GetFirstOrDefault("m"), selectedForum,
                        this.TopicSubject.Text.Trim());
                         
                        }
                    }

                    CommonDb.message_move(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("m"),
                             nTopicId, true);
                    this.Get<IYafSession>().NntpTreeActiveNode = null;
                    YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
                }
                else
                {
                    this.PageContext.AddLoadMessage(this.GetText("Empty_Topic"));
                }
        }

        /// <summary>
        /// The forum list_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void ForumList_SelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.TopicsList.DataSource = CommonDb.topic_list(
                this.PageContext.PageModuleID, this.ForumList.SelectedValue, null, DateTimeHelper.SqlDbMinTime(), DateTime.UtcNow, 0, 32762, false, false, false, false, this.Get<YafBoardSettings>().AllowTopicTags);
            this.TopicsList.DataTextField = "Subject";
            this.TopicsList.DataValueField = "TopicID";
            this.TopicsList.DataBind();
            this.TopicsList_SelectedIndexChanged(this.ForumList, e);
            this.CreateAndMove.Enabled = this.ForumList.SelectedValue.ToType<int>() <= 0 ? false : true;
        }

        /// <summary>
        /// The move_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Move_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!Config.LargeForumTree)
            {
                if (this.TopicsList.SelectedValue.ToType<int>() != this.PageContext.PageTopicID)
                {
                    CommonDb.message_move(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("m"),
                        this.TopicsList.SelectedValue, true);
                }
            }
            else
            {
                int topicId;
                if (TopicIDTb.Text.Trim().IsNotSet() || !int.TryParse(TopicIDTb.Text.Trim(), out topicId))
                {
                    this.PageContext.AddLoadMessage(this.GetText("ENTER_VALID_TOPICID"));
                    return;
                }
                CommonDb.message_move(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("m"),
                       topicId, true);
                this.Get<IYafSession>().NntpTreeActiveNode = null;

            }

            YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
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
                this.rowTopicsDdl.Visible = false;
                this.rowTopicName.Visible = true;
                this.ForumList.AutoPostBack = false;

                this.ForumList.Visible = false;

                this.jumpList.Visible = true;

                //  YafContext.Current.PageElements.RegisterJsResourceInclude("yafjs", "js/vzfDynatree.js");              

                YafContext.Current.PageElements.RegisterJsResourceInclude("fancytree", "js/jquery.fancytree-all.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("css/fancytree/{0}/ui.fancytree.css".FormatWith(YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
                YafContext.Current.PageElements.RegisterJsResourceInclude("ftreedeljs", "js/fancytree.vzf.nodeslazy.min.js");

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
                "ftreemm", "fancyTreeSelectSingleNodeLazyJs('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');"
                   .FormatWith(Config.JQueryAlias,
                   "treemovemessage",
                   PageContext.PageUserID,
                      PageContext.PageBoardID,
                      "echoActive",
                       string.Empty,
                      args,
                      "{0}resource.ashx?tjl".FormatWith(YafForumInfo.ForumClientFileRoot),
                      "&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));
            }

            base.OnPreRender(e);
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
            if (this.Request.QueryString.GetFirstOrDefault("m") == null || !this.PageContext.ForumModeratorAccess)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            this.PageLinks.AddLink(
                this.PageContext.PageCategoryName,
                YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
            this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
            this.PageLinks.AddLink(
                this.PageContext.PageTopicName, YafBuildLink.GetLink(ForumPages.posts, "t={0}", this.PageContext.PageTopicID));

            this.PageLinks.AddLink(this.GetText("MOVE_MESSAGE"));

            this.Move.Text = this.GetText("MOVE_MESSAGE");
            this.Move.ToolTip = this.GetText("MOVE_TITLE");
            this.CreateAndMove.Text = this.GetText("CREATE_TOPIC");
            this.CreateAndMove.ToolTip = this.GetText("SPLIT_TITLE");
            if (!Config.LargeForumTree)
            {
                this.ForumList.DataSource = CommonDb.forum_listall_sorted(PageContext.PageModuleID,
                    this.PageContext.PageBoardID, this.PageContext.PageUserID);
                this.ForumList.DataTextField = "Title";
                this.ForumList.DataValueField = "ForumID";
            }
            this.DataBind();

            if (Config.LargeForumTree) return;

            this.ForumList.Items.FindByValue(this.PageContext.PageForumID.ToString(CultureInfo.InvariantCulture)).Selected = true;
            this.ForumList_SelectedIndexChanged(this.ForumList, e);
        }

        /// <summary>
        /// The topics list_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void TopicsList_SelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Move.Enabled = this.TopicsList.SelectedValue != string.Empty;
        }

        #endregion
    }
}