﻿namespace YAF.Pages
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using VZF.Controls;
    using VZF.Data.Common;
    using VZF.Utilities;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Core.Services.Twitter;
    using YAF.Editors;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Extensions;
    using YAF.Types.Objects;

    #endregion

    /// <summary>
    /// The Posts Page.
    /// </summary>
    public partial class posts : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        ///   The _quick reply editor.
        /// </summary>
        protected ForumEditor _quickReplyEditor;

        /// <summary>
        ///   The _data bound.
        /// </summary>
        private bool _dataBound;

        /// <summary>
        ///   The _forum.
        /// </summary>
        private DataRow _forum;

        /// <summary>
        ///   The _forum flags.
        /// </summary>
        private ForumFlags _forumFlags;

        /// <summary>
        ///   The _ignore query string.
        /// </summary>
        private bool _ignoreQueryString;

        /// <summary>
        ///   The _topic.
        /// </summary>
        private DataRow _topic;

        /// <summary>
        ///   The _topic flags.
        /// </summary>
        private TopicFlags _topicFlags;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "posts" /> class.
        /// </summary>
        public posts()
            : base("POSTS")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets a value indicating whether IsThreaded.
        /// </summary>
        public bool IsThreaded
        {
            get
            {
                if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("threaded") != null)
                {
                    this.Session["IsThreaded"] =
                        bool.Parse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("threaded"));
                }
                else if (this.Session["IsThreaded"] == null)
                {
                    this.Session["IsThreaded"] = false;
                }

                return (bool)this.Session["IsThreaded"];
            }

            set
            {
                this.Session["IsThreaded"] = value;
            }
        }

        /// <summary>
        ///   Gets or sets CurrentMessage.
        /// </summary>
        protected int CurrentMessage
        {
            get
            {
                if (this.ViewState["CurrentMessage"] != null)
                {
                    return (int)this.ViewState["CurrentMessage"];
                }

                return 0;
            }

            set
            {
                this.ViewState["CurrentMessage"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The delete message_ load.
        /// </summary>
        /// <param name="sender">
        /// <param name="sender">The source of the event.</param>
        /// </param>
        /// <param name="e">
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// </param>
        protected void DeleteMessage_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("confirm_deletemessage"));
        }

        /// <summary>
        /// The delete topic_ click.
        /// </summary>
        /// <param name="sender">
        /// <param name="sender">The source of the event.</param>
        /// </param>
        /// <param name="e">
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// </param>
        protected void DeleteTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                /*"You don't have access to delete topics."*/
                YafBuildLink.AccessDenied();
            }

            CommonDb.topic_delete(PageContext.PageModuleID, this.PageContext.PageTopicID);
            YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
        }

        /// <summary>
        /// The delete topic_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DeleteTopic_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ((ThemeButton)sender).Attributes["onclick"] =
                "return confirm('{0}')".FormatWith(this.GetText("confirm_deletetopic"));
        }

        /// <summary>
        /// The email topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void EmailTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.User == null)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_EMAILLOGIN"));
                return;
            }

            YafBuildLink.Redirect(ForumPages.emailtopic, "t={0}", this.PageContext.PageTopicID);
        }

        /// <summary>
        /// The get indent image.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// Returns the indent image.
        /// </returns>
        protected string GetIndentImage([NotNull] object o)
        {
            if (!this.IsThreaded)
            {
                return string.Empty;
            }

            var currentIndex = (int)o;
            if (currentIndex > 0)
            {
                return "<img src='{1}images/spacer.gif' width='{0}' alt='' height='2'/>".FormatWith(
                    currentIndex * 32, YafForumInfo.ForumClientFileRoot);
            }

            return string.Empty;
        }

        /// <summary>
        /// The get threaded row.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// Returns the threaded row.
        /// </returns>
        [NotNull]
        protected string GetThreadedRow([NotNull] object o)
        {
            var row = (DataRow)o;
            var messageId = (int)row["MessageID"];

            if (!this.IsThreaded || this.CurrentMessage == messageId)
            {
                return string.Empty;
            }

            var html = new StringBuilder();

            // Threaded
            string brief =
                StringExtensions.RemoveMultipleWhitespace(
                    BBCodeHelper.StripBBCode(
                        BBCodeHelper.StripBBCodeQuotes(
                            HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString(row["Message"].ToString())))));

            brief = this.Get<IBadWordReplace>().Replace(brief).Truncate(100);
            brief = this.Get<IBBCode>().AddSmiles(brief);

            if (brief.IsNotSet())
            {
                brief = "...";
            }

            html.AppendFormat(@"<tr class=""post""><td colspan=""3"" style=""white-space:nowrap;"">");
            html.AppendFormat(this.GetIndentImage(row["Indent"]));

            string avatarUrl = this.Get<IAvatars>().GetAvatarUrlForUser(row.Field<int>("UserID"));

            if (avatarUrl.IsNotSet())
            {
                avatarUrl = "{0}images/noavatar.gif".FormatWith(YafForumInfo.ForumClientFileRoot);
            }

            html.Append(@"<span class=""threadedRowCollapsed"">");
            html.AppendFormat(@"<img src=""{0}"" alt="""" class=""avatarimage"" />", avatarUrl);
            html.AppendFormat(
                @"<a href=""{0}"" class=""threadUrl"">{1}</a>",
                YafBuildLink.GetLink(ForumPages.posts, "m={0}#post{0}", messageId),
                brief);

            html.Append(" (");
            html.Append(
                new UserLink
                    {
                        ID = "UserLinkForRow{0}".FormatWith(messageId), 
                        UserID = row.Field<int>("UserID")
                    }.RenderToString());

            html.AppendFormat(
                " - {0})</span>",
                new DisplayDateTime { DateTime = row["Posted"], Format = DateTimeFormat.BothTopic }.RenderToString());

            html.AppendFormat("</td></tr>");

            return html.ToString();
        }

        /// <summary>
        /// The is current message.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// Returns if it the current message.
        /// </returns>
        protected bool IsCurrentMessage([NotNull] object o)
        {
            CodeContracts.ArgumentNotNull(o, "o");

            var row = (DataRow)o;

            return !this.IsThreaded || this.CurrentMessage == (int)row["MessageID"];
        }

        /// <summary>
        /// The lock topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LockTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                // "You are not a forum moderator.
                YafBuildLink.AccessDenied();
            }

            CommonDb.topic_lock(PageContext.PageModuleID, this.PageContext.PageTopicID, true);
            this.BindData();
            this.PageContext.AddLoadMessage(this.GetText("INFO_TOPIC_LOCKED"));
            this.LockTopic1.Visible = !this.LockTopic1.Visible;
            this.UnlockTopic1.Visible = !this.UnlockTopic1.Visible;
            this.LockTopic2.Visible = this.LockTopic1.Visible;
            this.UnlockTopic2.Visible = this.UnlockTopic1.Visible;

            /*PostReplyLink1.Visible = false;
             PostReplyLink2.Visible = false;*/
        }

        /// <summary>
        /// The message list_ on item created.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void MessageList_OnItemCreated([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            var item = e.Item;

            if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            if (this.Pager.CurrentPageIndex != 0 || e.Item.ItemIndex != 0)
            {
                return;
            }

            // check if need to display the ad...
            bool showAds = true;

            if (this.User != null)
            {
                showAds = this.Get<YafBoardSettings>().ShowAdsToSignedInUsers;
            }

            if (string.IsNullOrEmpty(this.Get<YafBoardSettings>().AdPost) || !showAds)
            {
                return;
            }

            // first message... show the ad below this message
            var adControl = (DisplayAd)e.Item.FindControl("DisplayAd");
            if (adControl != null)
            {
                adControl.Visible = true;
            }
        }

        /// <summary>
        /// The move topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void MoveTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                YafBuildLink.AccessDenied(/*"You are not a forum moderator."*/);
            }
        }

        /// <summary>
        /// The new topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void NewTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this._forumFlags.IsLocked)
            {
                return;
            }

            this.PageContext.AddLoadMessage(this.GetText("WARN_FORUM_LOCKED"));
        }

        /// <summary>
        /// The next topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void NextTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            using (DataTable dt = CommonDb.topic_findnext(PageContext.PageModuleID, this.PageContext.PageTopicID))
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.PageContext.AddLoadMessage(this.GetText("INFO_NOMORETOPICS"));
                    return;
                }

                YafBuildLink.Redirect(ForumPages.posts, "t={0}", dt.Rows[0]["TopicID"]);
            }
        }

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            // get the forum editor based on the settings
            // string editorId = this.Get<YafBoardSettings>().ForumEditor;

           //  if (this.Get<YafBoardSettings>().AllowUsersTextEditor)
           // {
                // Text editor
             //   editorId = !string.IsNullOrEmpty(this.PageContext.TextEditor)
             //                  ? this.PageContext.TextEditor
             //                  : this.Get<YafBoardSettings>().ForumEditor;
          //  }

            // Check if Editor exists, if not fallback to default editorid=1
            /* this._quickReplyEditor = this.Get<IModuleManager<ForumEditor>>().GetBy(editorId, false)
                                     ?? this.Get<IModuleManager<ForumEditor>>().GetBy("1");*/
            this._quickReplyEditor = new TextEditor();

            // Override Editor when mobile device with default Yaf BBCode Editor
            if (this.PageContext.IsMobileDevice)
            {
                this._quickReplyEditor = this.Get<IModuleManager<ForumEditor>>().GetBy("1");
            }

            // Quick Reply Modification Begin
            // this._quickReplyEditor = new BasicBBCodeEditor();
            this.QuickReplyLine.Controls.Add(this._quickReplyEditor);
            this.QuickReply.Click += this.QuickReply_Click;
            this.Pager.PageChange += this.Pager_PageChange;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// The on pre render.
        /// </summary>
        /// <param name="e">
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            base.OnPreRender(e);
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PageContext.PageForumID == 0)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.Invalid);
            }

            YafContext.Current.PageElements.RegisterJsResourceInclude("yafPageMethodjs", "js/jquery.pagemethod.js");

            if (!this.PageContext.IsGuest)
            {
                // The html code for "Favorite Topic" theme buttons.
                string tagButtonHtml =
                    "'<a class=\"yafcssbigbutton rightItem\" href=\"javascript:addFavoriteTopic(' + res.d + ');\" onclick=\"jQuery(this).blur();\" title=\"{0}\"><span>{1}</span></a>'"
                        .FormatWith(this.GetText("BUTTON_TAGFAVORITE_TT"), this.GetText("BUTTON_TAGFAVORITE"));
                string untagButtonHtml =
                    "'<a class=\"yafcssbigbutton rightItem\" href=\"javascript:removeFavoriteTopic(' + res.d + ');\" onclick=\"jQuery(this).blur();\" title=\"{0}\"><span>{1}</span></a>'"
                        .FormatWith(this.GetText("BUTTON_UNTAGFAVORITE_TT"), this.GetText("BUTTON_UNTAGFAVORITE"));

                // Register the client side script for the "Favorite Topic".
                var favoriteTopicJs =
                    this.Get<IScriptBuilder>().CreateStatement().Add(
                        JavaScriptBlocks.AddFavoriteTopicJs(untagButtonHtml)).AddLine().Add(
                            JavaScriptBlocks.RemoveFavoriteTopicJs(tagButtonHtml));

                YafContext.Current.PageElements.RegisterJsBlockStartup("favoriteTopicJs", favoriteTopicJs);

                var asynchCallFailedJs =
                    this.Get<IScriptBuilder>().CreateStatement().AddFunc(
                        f => f.Name("CallFailed").WithParams("res").Func(s => s.Add("alert('Error Occurred');")));
         
                YafContext.Current.PageElements.RegisterJsBlockStartup("asynchCallFailedJs", asynchCallFailedJs);

                // Has the user already tagged this topic as favorite?
                if (this.Get<IFavoriteTopic>().IsFavoriteTopic(this.PageContext.PageTopicID))
                {
                    // Generate the "Untag" theme button with appropriate JS calls for onclick event.
                    this.TagFavorite1.NavigateUrl = "javascript:removeFavoriteTopic(" + this.PageContext.PageTopicID
                                                    + ");";
                    this.TagFavorite2.NavigateUrl = "javascript:removeFavoriteTopic(" + this.PageContext.PageTopicID
                                                    + ");";
                    this.TagFavorite1.TextLocalizedTag = "BUTTON_UNTAGFAVORITE";
                    this.TagFavorite1.TitleLocalizedTag = "BUTTON_UNTAGFAVORITE_TT";
                    this.TagFavorite2.TextLocalizedTag = "BUTTON_UNTAGFAVORITE";
                    this.TagFavorite2.TitleLocalizedTag = "BUTTON_UNTAGFAVORITE_TT";
                }
                else
                {
                    // Generate the "Tag" theme button with appropriate JS calls for onclick event.
                    this.TagFavorite1.NavigateUrl = "javascript:addFavoriteTopic(" + this.PageContext.PageTopicID + ");";
                    this.TagFavorite2.NavigateUrl = "javascript:addFavoriteTopic(" + this.PageContext.PageTopicID + ");";
                    this.TagFavorite1.TextLocalizedTag = "BUTTON_TAGFAVORITE";
                    this.TagFavorite1.TitleLocalizedTag = "BUTTON_TAGFAVORITE_TT";
                    this.TagFavorite2.TextLocalizedTag = "BUTTON_TAGFAVORITE";
                    this.TagFavorite2.TitleLocalizedTag = "BUTTON_TAGFAVORITE_TT";
                }
            }
            else
            {
                this.TagFavorite1.Visible = false;
                this.TagFavorite2.Visible = false;
            }

            this._quickReplyEditor.BaseDir = "{0}editors".FormatWith(YafForumInfo.ForumClientFileRoot);
            this._quickReplyEditor.StyleSheet = this.Get<ITheme>().BuildThemePath("theme.css");

            this._topic = CommonDb.topic_info(this.PageContext.PageModuleID, this.PageContext.PageTopicID, true, true);

            // in case topic is deleted or not existant
            if (this._topic == null)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.Invalid);
            }

            // get topic flags
            var dataRow = this._topic;
            if (dataRow == null) return;

            this._topicFlags = new TopicFlags(dataRow["Flags"]);

            using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageForumID))
            {
                this._forum = dt.Rows[0];
            }

            this._forumFlags = new ForumFlags(this._forum["Flags"]);

            if (this.PageContext.IsGuest && !this.PageContext.ForumReadAccess)
            {
                // attempt to get permission by redirecting to login...
                this.Get<IPermissions>().HandleRequest(ViewPermissions.RegisteredUsers);
            }
            else if (!this.PageContext.ForumReadAccess)
            {
                YafBuildLink.AccessDenied();
            }

            if (!this.IsPostBack)
            {
                // Clear Multiquotes
                this.Get<IYafSession>().MultiQuoteIds = null;

                if (this.PageContext.Settings.LockedForum == 0)
                {
                    this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                    this.PageLinks.AddLink(
                        this.PageContext.PageCategoryName,
                        YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
                }

                this.NewTopic2.NavigateUrl =
                    this.NewTopic1.NavigateUrl =
                        YafBuildLink.GetLinkNotEscaped(ForumPages.postmessage, "f={0}", this.PageContext.PageForumID);

                this.MoveTopic1.NavigateUrl =
                    this.MoveTopic2.NavigateUrl =
                        YafBuildLink.GetLinkNotEscaped(ForumPages.movetopic, "t={0}", this.PageContext.PageTopicID);

                this.PostReplyLink1.NavigateUrl =
                    this.PostReplyLink2.NavigateUrl =
                        YafBuildLink.GetLinkNotEscaped(
                            ForumPages.postmessage,
                            "t={0}&f={1}",
                            this.PageContext.PageTopicID,
                            this.PageContext.PageForumID);

                this.QuickReply.Text = this.GetText("POSTMESSAGE", "SAVE");
                this.DataPanel1.TitleText = this.GetText("QUICKREPLY");
                this.DataPanel1.ExpandText = this.GetText("QUICKREPLY_SHOW");
                this.DataPanel1.CollapseText = this.GetText("QUICKREPLY_HIDE");

                this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
                this.PageLinks.AddLink(
                    this.Get<IBadWordReplace>().Replace(this.Server.HtmlDecode(this.PageContext.PageTopicName)),
                    string.Empty);

                var topicSubject = this.Get<IBadWordReplace>().Replace(this.HtmlEncode(dataRow["Topic"]));

                if (dataRow["Status"].ToString().IsSet() && this.Get<YafBoardSettings>().EnableTopicStatus)
                {
                    var topicStatusIcon = this.Get<ITheme>().GetItem("TOPIC_STATUS", dataRow["Status"].ToString());

                    if (topicStatusIcon.IsSet() && !topicStatusIcon.Contains("[TOPIC_STATUS."))
                    {
                        topicSubject =
                            "<img src=\"{0}\" alt=\"{1}\" title=\"{1}\" style=\"border: 0;width:16px;height:16px\" />&nbsp;{2}"
                                .FormatWith(
                                    this.Get<ITheme>().GetItem("TOPIC_STATUS", dataRow["Status"].ToString()),
                                    this.GetText("TOPIC_STATUS", dataRow["Status"].ToString()),
                                    topicSubject);
                    }
                    else
                    {
                        topicSubject =
                            "[{0}]&nbsp;{1}".FormatWith(
                                this.GetText("TOPIC_STATUS", dataRow["Status"].ToString()), topicSubject);
                    }
                }

                if (!dataRow["Description"].IsNullOrEmptyDBField()
                    && this.Get<YafBoardSettings>().EnableTopicDescription)
                {
                    this.TopicTitle.Text = "{0} - <em>{1}</em>".FormatWith(
                        topicSubject, this.Get<IBadWordReplace>().Replace(this.HtmlEncode(dataRow["Description"])));
                }
                else
                {
                    this.TopicTitle.Text = this.Get<IBadWordReplace>().Replace(topicSubject);
                }

                this.TopicLink.ToolTip = this.Get<IBadWordReplace>().Replace(
                    this.HtmlEncode(dataRow["Description"]));
                this.TopicLink.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.posts, "t={0}", this.PageContext.PageTopicID);
                this.ViewOptions.Visible = this.Get<YafBoardSettings>().AllowThreaded;
                this.ForumJumpHolder.Visible = this.Get<YafBoardSettings>().ShowForumJump
                                               && this.PageContext.Settings.LockedForum == 0;

                this.RssTopic.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.rsstopic, "pg={0}&t={1}", YafRssFeeds.Posts.ToInt(), this.PageContext.PageTopicID);
                this.RssTopic.Visible = this.Get<YafBoardSettings>().ShowRSSLink;

                this.QuickReplyPlaceHolder.Visible = this.Get<YafBoardSettings>().ShowQuickAnswer;

                if ((this.PageContext.IsGuest && this.Get<YafBoardSettings>().EnableCaptchaForGuests)
                    || (this.Get<YafBoardSettings>().EnableCaptchaForPost && !this.PageContext.IsCaptchaExcluded))
                {
                    this.imgCaptcha.ImageUrl = "{0}resource.ashx?c=1".FormatWith(YafForumInfo.ForumClientFileRoot);
                    this.CaptchaDiv.Visible = true;
                }

                if (!this.PageContext.ForumPostAccess
                    || (this._forumFlags.IsLocked && !this.PageContext.ForumModeratorAccess))
                {
                    this.NewTopic1.Visible = false;
                    this.NewTopic2.Visible = false;
                }

                // Ederon : 9/9/2007 - moderators can reply in locked topics
                if (!this.PageContext.ForumReplyAccess
                    ||
                    ((this._topicFlags.IsLocked || this._forumFlags.IsLocked) && !this.PageContext.ForumModeratorAccess))
                {
                    this.PostReplyLink1.Visible = this.PostReplyLink2.Visible = false;
                    this.QuickReplyPlaceHolder.Visible = false;
                }

                if (this.PageContext.ForumModeratorAccess)
                {
                    this.MoveTopic1.Visible = true;
                    this.MoveTopic2.Visible = true;
                }
                else
                {
                    this.MoveTopic1.Visible = false;
                    this.MoveTopic2.Visible = false;
                }

                if (!this.PageContext.ForumModeratorAccess)
                {
                    this.LockTopic1.Visible = false;
                    this.UnlockTopic1.Visible = false;
                    this.DeleteTopic1.Visible = false;
                    this.LockTopic2.Visible = false;
                    this.UnlockTopic2.Visible = false;
                    this.DeleteTopic2.Visible = false;
                }
                else
                {
                    this.LockTopic1.Visible = !this._topicFlags.IsLocked;
                    this.UnlockTopic1.Visible = !this.LockTopic1.Visible;
                    this.LockTopic2.Visible = this.LockTopic1.Visible;
                    this.UnlockTopic2.Visible = !this.LockTopic2.Visible;
                }
            }

            this.Stc1.TopicId = this.PageContext.PageTopicID;
            #endregion

            this.BindData();
            
            if (!this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ShowShareTopicTo)
                || !Config.FacebookApiKey.IsSet())
            {
                return;
            }

            YafContext.Current.PageElements.RegisterJsBlockStartup("facebookInitJs", JavaScriptBlocks.FacebookInitJs);

            var message =
                StringExtensions.RemoveMultipleWhitespace(
                    BBCodeHelper.StripBBCode(
                        HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString((string)dataRow["Topic"]))));

            var meta = this.Page.Header.FindControlType<HtmlMeta>();
            string description = string.Empty;

            var htmlMetas = meta as IList<HtmlMeta> ?? meta.ToList();
            if (htmlMetas.Any(x => x.Name.Equals("description")))
            {
                var descriptionMeta = htmlMetas.FirstOrDefault(x => x.Name.Equals("description"));
                if (descriptionMeta != null)
                {
                    description = descriptionMeta.Content;
                }
            }

            YafContext.Current.PageElements.RegisterJsBlockStartup(
                "facebookPostJs",
                JavaScriptBlocks.FacebookPostJs(
                    this.Server.HtmlEncode(message),
                    this.Server.HtmlEncode(description),
                    this.Get<HttpRequestBase>().Url.ToString(),
                    "{0}/YAFLogo.jpg".FormatWith(Path.Combine(YafForumInfo.ForumBaseUrl, YafBoardFolders.Current.Images)),
                    "Logo"));
        }

        /// <summary>
        /// The poll group id.
        /// </summary>
        /// <returns>
        /// Returns The poll group id.
        /// </returns>
        protected int PollGroupId()
        {
            return !this._topic["PollID"].IsNullOrEmptyDBField() ? this._topic["PollID"].ToType<int>() : 0;
        }

        /// <summary>
        /// The topic id.
        /// </summary>
        /// <returns>
        /// Returns topic id.
        /// </returns>
        protected int GetTopicId()
        {
            return !this._topic["TopicID"].IsNullOrEmptyDBField() ? this._topic["TopicID"].ToType<int>() : 0;
        }

        /// <summary>
        /// The post reply link_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PostReplyLink_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // Ederon : 9/9/2007 - moderator can reply in locked posts
            if (this.PageContext.ForumModeratorAccess)
            {
                return;
            }

            if (this._topicFlags.IsLocked)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_TOPIC_LOCKED"));
                return;
            }

            if (!this._forumFlags.IsLocked)
            {
                return;
            }

            this.PageContext.AddLoadMessage(this.GetText("WARN_FORUM_LOCKED"));
        }

        /// <summary>
        /// The prev topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PrevTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            using (DataTable dt = CommonDb.topic_findprev(PageContext.PageModuleID, this.PageContext.PageTopicID))
            {
                if (dt.Rows.Count == 0)
                {
                    this.PageContext.AddLoadMessage(this.GetText("INFO_NOMORETOPICS"));
                    return;
                }

                YafBuildLink.Redirect(ForumPages.posts, "t={0}", dt.Rows[0]["TopicID"]);
            }
        }

        /// <summary>
        /// The print topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PrintTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.printtopic, "t={0}", this.PageContext.PageTopicID);
        }

        /// <summary>
        /// The show poll buttons.
        /// </summary>
        /// <returns>
        /// Returns The show poll buttons.
        /// </returns>
        protected bool ShowPollButtons()
        {
            return false;

            /* return (Convert.ToInt32(_topic["UserID"]) == PageContext.PageUserID) || PageContext.IsModerator || PageContext.IsAdmin; */
        }

        /// <summary>
        /// The track topic_ click.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        protected void TrackTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PageContext.IsGuest)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_WATCHLOGIN"));
                return;
            }

            if (this.WatchTopicID.InnerText == string.Empty)
            {
                CommonDb.watchtopic_add(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageTopicID);
                this.PageContext.AddLoadMessage(this.GetText("INFO_WATCH_TOPIC"));
            }
            else
            {
                var tmpId = this.WatchTopicID.InnerText.ToType<int>();

                CommonDb.watchtopic_delete(PageContext.PageModuleID, tmpId);

                this.PageContext.AddLoadMessage(this.GetText("INFO_UNWATCH_TOPIC"));
            }

            this.HandleWatchTopic();
        }

        /// <summary>
        /// The unlock topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void UnlockTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumModeratorAccess)
            {
                YafBuildLink.AccessDenied(/*"You are not a forum moderator."*/);
            }

            CommonDb.topic_lock(PageContext.PageModuleID, this.PageContext.PageTopicID, false);
            this.BindData();
            this.PageContext.AddLoadMessage(this.GetText("INFO_TOPIC_UNLOCKED"));
            this.LockTopic1.Visible = !this.LockTopic1.Visible;
            this.UnlockTopic1.Visible = !this.UnlockTopic1.Visible;
            this.LockTopic2.Visible = this.LockTopic1.Visible;
            this.UnlockTopic2.Visible = this.UnlockTopic1.Visible;
            this.PostReplyLink1.Visible = this.PageContext.ForumReplyAccess;
            this.PostReplyLink2.Visible = this.PageContext.ForumReplyAccess;
        }

        /// <summary>
        /// Adds meta data: description and keywords to the page header.
        /// </summary>
        /// <param name="firstMessage">
        /// first message in the topic
        /// </param>
        private void AddMetaData([NotNull] object firstMessage)
        {
            if (firstMessage.IsNullOrEmptyDBField())
            {
                return;
            }

            if (this.Page.Header == null || !this.Get<YafBoardSettings>().AddDynamicPageMetaTags)
            {
                return;
            }

            var message = this.Get<IFormatMessage>().GetCleanedTopicMessage(
                firstMessage, this.PageContext.PageTopicID);
            var meta = this.Page.Header.FindControlType<HtmlMeta>();

            var htmlMetas = meta as List<HtmlMeta> ?? meta.ToList();
            if (message.MessageTruncated.IsSet())
            {
                HtmlMeta descriptionMeta;

                // Use Topic Description if set
                var descriptionContent = !this._topic["Description"].IsNullOrEmptyDBField()
                                         ? this.Get<IBadWordReplace>().Replace(
                                             this.HtmlEncode(this._topic["Description"]))
                                         : "{0}: {1}".FormatWith(this._topic["Topic"], message.MessageTruncated);

                if (htmlMetas.Any(x => x.Name.Equals("description")))
                {
                    // use existing...
                    descriptionMeta = htmlMetas.FirstOrDefault(x => x.Name.Equals("description"));
                    if (descriptionMeta != null)
                    {
                        descriptionMeta.Content = descriptionContent;

                        this.Page.Header.Controls.Remove(descriptionMeta);

                        descriptionMeta = ControlHelper.MakeMetaDiscriptionControl(descriptionContent);

                        // add to the header...
                        this.Page.Header.Controls.Add(descriptionMeta);
                    }
                }
                else
                {
                    descriptionMeta = ControlHelper.MakeMetaDiscriptionControl(descriptionContent);

                    // add to the header...
                    this.Page.Header.Controls.Add(descriptionMeta);
                }
            }

            if (message.MessageKeywords.Count <= 0)
            {
                return;
            }

            HtmlMeta keywordMeta;

            var keywordStr = message.MessageKeywords.Where(x => x.IsSet()).ToList().ToDelimitedString(",");

            //// this.Tags.Text = "Tags: {0}".FormatWith(keywordStr);

            if (htmlMetas.Any(x => x.Name.Equals("keywords")))
            {
                // use existing...
                keywordMeta = htmlMetas.FirstOrDefault(x => x.Name.Equals("keywords"));
                keywordMeta.Content = keywordStr;

                this.Page.Header.Controls.Remove(keywordMeta);

                // add to the header...
                this.Page.Header.Controls.Add(keywordMeta);
            }
            else
            {
                keywordMeta = ControlHelper.MakeMetaKeywordsControl(keywordStr);

                // add to the header...
                this.Page.Header.Controls.Add(keywordMeta);
            }
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (this._topic == null)
            {
                YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
            }

            this._dataBound = true;

            bool showDeleted = false;
            int userId = this.PageContext.PageUserID;
          
            if ((this.PageContext.IsAdmin
                || this.PageContext.ForumModeratorAccess) || this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
            {
                showDeleted = true;
            }

            DateTime lastRead; 
            int messagePosition;
            if (!this.Get<YafBoardSettings>().ShowDeletedMessages || !this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
            {
                // normally, users can always see own deleted topics or stubs we set a false id to hide them.
                userId = -1;
            }

            int findMessageId = this.GetFindMessageId(showDeleted, userId, out messagePosition, out lastRead);
            if (findMessageId > 0)
            {
                this.CurrentMessage = findMessageId;
            }

            // Mark topic read
            this.Get<IReadTrackCurrentUser>().SetTopicRead(this.PageContext.PageTopicID);
            this.Pager.PageSize = this.PageContext.PostsPerPage;
            
            DataTable postListDataTable = CommonDb.post_list(
                PageContext.PageModuleID,
                this.PageContext.PageTopicID,
                this.PageContext.PageUserID,
                userId,
                this.IsPostBack || PageContext.IsCrawler ? 0 : 1,
                showDeleted,
                this.Get<YafBoardSettings>().UseStyledNicks,
                !this.PageContext.IsGuest && this.Get<YafBoardSettings>().DisplayPoints,
                DateTimeHelper.SqlDbMinTime(),
                DateTime.UtcNow,
                DateTimeHelper.SqlDbMinTime(),
                DateTime.UtcNow,
                this.Pager.CurrentPageIndex,
                this.Pager.PageSize,
                1,
                0,
                this.IsThreaded ? 1 : 0,
                this.Get<YafBoardSettings>().EnableThanksMod,
                messagePosition,
                findMessageId,
                lastRead);
            if (postListDataTable == null || postListDataTable.Rows.Count <= 0)
            {
#if DEBUG
                string ex = " This is a temporary diagnostic message for developers.Don't report it, simply delete it. It will be gone in future." +
                            "An access attempt to a forbidden or unexisting topic. " +
                            "PageUserID={0}, Crawler = {1}, messagePosition={2}, TopicID={3}".FormatWith(this.PageContext.PageUserID,this.PageContext.IsCrawler,  messagePosition, PageContext.PageTopicID);
                
                CommonDb.eventlog_create(PageContext.PageModuleID, (int?)YafContext.Current.PageUserID, this, ex, EventLogTypes.Information);
#endif              
                YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
                return;
            }

            if (this.Get<YafBoardSettings>().EnableThanksMod)
            {
                // Add nescessary columns for later use in displaypost.ascx (Prevent repetitive 
                // calls to database.)  
                if (!postListDataTable.Columns.Contains("ThanksInfo"))
                {
                    postListDataTable.Columns.Add("ThanksInfo", type: typeof(string));
                }

                postListDataTable.Columns.AddRange(
                    new[]
                        {
                            // General Thanks Info
                            // new DataColumn("ThanksInfo", Type.GetType("System.String")),
                            // How many times has this message been thanked.
                            new DataColumn("IsThankedByUser", typeof(string)),
                            //// How many times has the message poster thanked others?   
                            new DataColumn("MessageThanksNumber", typeof(int)),
                            //// How many times has the message poster been thanked?
                            new DataColumn("ThanksFromUserNumber", typeof(int)),
                            //// In how many posts has the message poster been thanked? 
                            new DataColumn("ThanksToUserNumber", typeof(int)),
                            //// In how many posts has the message poster been thanked? 
                            new DataColumn("ThanksToUserPostsNumber", typeof(int))
                        });

                postListDataTable.AcceptChanges();
            }

            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                // needs to be moved to the paged data below -- so it doesn't operate on unnecessary rows
                new StyleTransform(this.Get<ITheme>()).DecodeStyleByTable(ref postListDataTable, true);
            }

            // convert to linq...
            var rowList = postListDataTable.AsEnumerable();
           
            /*if (!this.IsThreaded)
            {
                            // reset position for updated sorting...
                            rowList.ForEachIndex(
                                            (row, i) =>
                                                            {
                                                                            row.BeginEdit();
                                                                            row["Position"] = (Pager.CurrentPageIndex * Pager.PageSize) + i;
                                                                            row.EndEdit();
                                                            });
            }*/
           
            this.Pager.Count = rowList.First().Field<int>(columnName: "TotalRows");

            if (findMessageId > 0)
            {
                this.Pager.CurrentPageIndex = rowList.First().Field<int>(columnName: "PageIndex");
                if (!this.PageContext.IsCrawler)
                {
                    PageContext.PageElements.RegisterJsBlockStartup(
                        this, "GotoAnchorJs", JavaScriptBlocks.LoadGotoAnchor("post{0}".FormatWith(findMessageId)));
                }
            }
            else
            {
                if (!this.PageContext.IsCrawler)
                {
                    PageContext.PageElements.RegisterJsBlockStartup(
                        this,
                        "GotoAnchorJs",
                        JavaScriptBlocks.LoadGotoAnchor("post{0}".FormatWith(rowList.First().Field<int>(columnName: "MessageID"))));
                }
            }

            var pagedData = rowList; // .Skip(this.Pager.SkipIndex).Take(this.Pager.PageSize);

            // Add thanks info and styled nicks if they are enabled
            if (this.Get<YafBoardSettings>().EnableThanksMod)
            {
                this.Get<IDBBroker>().AddThanksInfo(dataRows: pagedData);
            }

            // if current index is 0 we are on the first page and the metadata can be added.
            if (this.Pager.CurrentPageIndex == 0)
            {
                // handle add description/keywords for SEO
                this.AddMetaData(firstMessage: pagedData.First()["Message"]);
            }

            if (pagedData.Any() && this.CurrentMessage == 0)
            {
                // set it to the first...
                // this.CurrentMessage = pagedData.First().Field<int>("MessageID");
            }

            this.MessageList.DataSource = pagedData;

            /*if (this._topic["PollID"] != DBNull.Value)
            {
                            Poll.Visible = true;
                            _dtPoll = DB.poll_stats(this._topic["PollID"]);
                            Poll.DataSource = _dtPoll;
            }*/

            this.ImageLastUnreadMessageLink.Visible = this.Get<YafBoardSettings>().ShowLastUnreadPost;

            this.ImageMessageLink.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                ForumPages.posts, "t={0}&find=lastpost", this.PageContext.PageTopicID);

            this.LastPostedImage.LocalizedTitle = this.GetText("DEFAULT", "GO_LAST_POST");

            if (this.ImageLastUnreadMessageLink.Visible)
            {
                this.ImageLastUnreadMessageLink.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.posts, "t={0}&find=unread", this.PageContext.PageTopicID);

                this.LastUnreadImage.LocalizedTitle = this.GetText(page: "DEFAULT", tag: "GO_LASTUNREAD_POST");
            }

            if (this._topic["LastPosted"] != DBNull.Value)
            {
                DateTime lastPosted =
                    this.Get<IReadTrackCurrentUser>().GetForumTopicRead(
                        forumId: this.PageContext.PageForumID, topicId: this.PageContext.PageTopicID);

                this.LastUnreadImage.ThemeTag = (DateTime.Parse(this._topic["LastPosted"].ToString()) > lastPosted)
                                                    ? "ICON_NEWEST_UNREAD"
                                                    : "ICON_LATEST_UNREAD";

                this.LastPostedImage.ThemeTag = (DateTime.Parse(this._topic["LastPosted"].ToString()) > lastPosted)
                                                    ? "ICON_NEWEST"
                                                    : "ICON_LATEST";
            }

            this.DataBind();
        }

        /// <summary>
        /// Gets the message ID if "find" is in the query string
        /// </summary>
        /// <param name="showDeleted">
        /// The show Deleted.
        /// </param>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="messagePosition">
        /// The message Position.
        /// </param>
        /// <param name="lastRead">
        /// The last read.
        /// </param>
        /// <returns>
        /// The get find message id.
        /// </returns>
        private int GetFindMessageId(bool showDeleted, int userId, out int messagePosition, out DateTime lastRead)
        {
            int findMessageId = 0;
            messagePosition = -1;
            lastRead = DateTimeHelper.SqlDbMinTime();

            if (!this._ignoreQueryString && this.Get<HttpRequestBase>().QueryString != null)
            {
                // temporary find=lastpost code until all last/unread post links are find=lastpost and find=unread
                if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("find") == null)
                {
                    if (!int.TryParse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"), out findMessageId))
                    {
                        return findMessageId;
                    }
                }
                else
                {
                    // find first unread message
                    if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("find").ToLower() == "unread")
                    {
                        lastRead = !this.PageContext.IsCrawler
                                       ? this.Get<IReadTrackCurrentUser>()
                                             .GetForumTopicRead(
                                                 forumId: this.PageContext.PageForumID,
                                                 topicId: this.PageContext.PageTopicID)
                                       : DateTime.UtcNow;
                    }

                    // find last post
                    if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("find").ToLower() == "lastpost")
                    {
                        lastRead = DateTime.UtcNow;
                    }
                }

                // we have parameters to handle
                using (
                    DataTable unread = CommonDb.message_findunread(
                        PageContext.PageModuleID,
                        topicId: this.PageContext.PageTopicID,
                        messageId: findMessageId,
                        lastRead: lastRead,
                        showDeleted: showDeleted,
                        authorUserId: userId))
                {
                    var unreadFirst = unread.AsEnumerable().FirstOrDefault();
                    if (unreadFirst != null)
                    {
                        findMessageId = unreadFirst.Field<int>("MessageID");
                        messagePosition = unreadFirst.Field<int>("MessagePosition");
                    }
                }
            }

            return findMessageId;
        }

        /// <summary>
        /// The handle watch topic.
        /// </summary>
        /// <returns>
        /// Returns The handle watch topic.
        /// </returns>
        private bool HandleWatchTopic()
        {
            if (this.PageContext.IsGuest)
            {
                return false;
            }

            // check if this forum is being watched by this user
            using (DataTable dt = CommonDb.watchtopic_check(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageTopicID))
            {
                if (dt.Rows.Count > 0)
                {
                    // subscribed to this forum
                    this.TrackTopic.Text = this.GetText("UNWATCHTOPIC");

                    foreach (DataRow row in dt.Rows)
                    {
                        this.WatchTopicID.InnerText = row["WatchTopicID"].ToString();
                        return true;
                    }
                }
                else
                {
                    // not subscribed
                    this.WatchTopicID.InnerText = string.Empty;
                    this.TrackTopic.Text = this.GetText("WATCHTOPIC");
                }
            }

            return false;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Poll.ItemCommand += Poll_ItemCommand;
            this.PreRender += this.posts_PreRender;
            this.ShareMenu.ItemClick += this.ShareMenu_ItemClick;
            this.OptionsMenu.ItemClick += this.OptionsMenu_ItemClick;
            this.ViewMenu.ItemClick += this.ViewMenu_ItemClick;
        }

        /// <summary>
        /// The options menu_ item click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The Pop Event Arguments.
        /// </param>
        /// <exception cref="ApplicationException"></exception>
        private void ShareMenu_ItemClick([NotNull] object sender, [NotNull] PopEventArgs e)
        {
            var topicUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.posts, true, "t={0}", this.PageContext.PageTopicID);
            switch (e.Item.ToLower())
            {
                case "email":
                    this.EmailTopic_Click(sender, e);
                    break;
                case "tumblr":
                    {
                        // process message... clean html, strip html, remove bbcode, etc...
                        var tumblrMsg =
                            StringExtensions.RemoveMultipleWhitespace(
                                BBCodeHelper.StripBBCode(
                                    HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString((string)this._topic["Topic"]))));

                        var meta = this.Page.Header.FindControlType<HtmlMeta>();

                        string description = string.Empty;

                        if (meta.Any(x => x.Name.Equals("description")))
                        {
                            var descriptionMeta = meta.FirstOrDefault(x => x.Name.Equals("description"));
                            if (descriptionMeta != null)
                            {
                                description = "&description={0}".FormatWith(descriptionMeta.Content);
                            }
                        }

                        var tumblrUrl =
                            "http://www.tumblr.com/share/link?url={0}&name={1}{2}".FormatWith(
                                this.Server.UrlEncode(topicUrl), tumblrMsg, description);

                        this.Get<HttpResponseBase>().Redirect(tumblrUrl);
                    }

                    break;
                case "retweet":
                    {
                        var twitterName = this.Get<YafBoardSettings>().TwitterUserName.IsSet()
                                              ? "@{0} ".FormatWith(this.Get<YafBoardSettings>().TwitterUserName)
                                              : string.Empty;

                        // process message... clean html, strip html, remove bbcode, etc...
                        var twitterMsg =
                            StringExtensions.RemoveMultipleWhitespace(
                                BBCodeHelper.StripBBCode(
                                    HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString((string)this._topic["Topic"]))));

                        var tweetUrl =
                            "http://twitter.com/share?url={0}&text={1}".FormatWith(
                                this.Server.UrlEncode(topicUrl),
                                this.Server.UrlEncode("RT {1}Thread: {0}".FormatWith(twitterMsg.Truncate(100), twitterName)));

                        // Send Retweet Directlly thru the Twitter API if User is Twitter User
                        if (Config.TwitterConsumerKey.IsSet() && Config.TwitterConsumerSecret.IsSet()
                            && this.Get<IYafSession>().TwitterToken.IsSet()
                            && this.Get<IYafSession>().TwitterTokenSecret.IsSet() && this.PageContext.IsTwitterUser)
                        {
                            var oAuth = new OAuthTwitter
                                {
                                    ConsumerKey = Config.TwitterConsumerKey,
                                    ConsumerSecret = Config.TwitterConsumerSecret,
                                    Token = this.Get<IYafSession>().TwitterToken,
                                    TokenSecret = this.Get<IYafSession>().TwitterTokenSecret
                                };

                            var tweets = new TweetAPI(oAuth);

                            tweets.UpdateStatus(
                                TweetAPI.ResponseFormat.json,
                                this.Server.UrlEncode("RT {1}: {0} {2}".FormatWith(twitterMsg.Truncate(100), twitterName, topicUrl)),
                                string.Empty);
                        }
                        else
                        {
                            this.Get<HttpResponseBase>().Redirect(tweetUrl);
                        }
                    }

                    break;
                case "digg":
                    {
                        var diggUrl =
                            "http://digg.com/submit?url={0}&title={1}".FormatWith(
                                this.Server.UrlEncode(topicUrl), this.Server.UrlEncode((string)this._topic["Topic"]));

                        this.Get<HttpResponseBase>().Redirect(diggUrl);
                    }

                    break;
                case "reddit":
                    {
                        var redditUrl =
                            "http://www.reddit.com/submit?url={0}&title={1}".FormatWith(
                                this.Server.UrlEncode(topicUrl), this.Server.UrlEncode((string)this._topic["Topic"]));

                        this.Get<HttpResponseBase>().Redirect(redditUrl);
                    }

                    break;
                case "googleplus":
                    {
                        var googlePlusUrl =
                            "https://plusone.google.com/_/+1/confirm?hl=en&url={0}".FormatWith(
                                this.Server.UrlEncode(topicUrl));

                        this.Get<HttpResponseBase>().Redirect(googlePlusUrl);
                    }

                    break;
                default:
                    throw new ApplicationException(e.Item);
            }
        }

        /// <summary>
        /// The options menu_ item click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OptionsMenu_ItemClick([NotNull] object sender, [NotNull] PopEventArgs e)
        {
            switch (e.Item.ToLower())
            {
                case "print":
                    YafBuildLink.Redirect(ForumPages.printtopic, "t={0}", this.PageContext.PageTopicID);
                    break;
                case "watch":
                    this.TrackTopic_Click(sender, e);
                    break;
                case "email":
                    this.EmailTopic_Click(sender, e);
                    break;
                case "rssfeed":
                    YafBuildLink.Redirect(
                        ForumPages.rsstopic,
                        "pg={0}&t={1}&ft=0",
                        YafRssFeeds.Posts.ToInt(),
                        this.PageContext.PageTopicID);
                    break;
                case "atomfeed":
                    YafBuildLink.Redirect(
                        ForumPages.rsstopic,
                        "pg={0}&t={1}&ft=1",
                        YafRssFeeds.Posts.ToInt(),
                        this.PageContext.PageTopicID);
                    break;
                default:
                    throw new ApplicationException(e.Item);
            }
        }

        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this._ignoreQueryString = true;
            this.SmartScroller1.Reset();
            this.BindData();
        }

        /// <summary>
        /// The quick reply_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void QuickReply_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumReplyAccess
                || (this._topicFlags.IsLocked && !this.PageContext.ForumModeratorAccess))
            {
                YafBuildLink.AccessDenied();
            }

            if (this._quickReplyEditor.Text.Length <= 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("EMPTY_MESSAGE"));
                return;
            }

            // No need to check whitespace if they are actually posting something
            if (this.Get<YafBoardSettings>().MaxPostSize > 0
                && this._quickReplyEditor.Text.Length >= this.Get<YafBoardSettings>().MaxPostSize)
            {
                this.PageContext.AddLoadMessage(this.GetText("ISEXCEEDED"));
                return;
            }

            if (((this.PageContext.IsGuest && this.Get<YafBoardSettings>().EnableCaptchaForGuests)
                 || (this.Get<YafBoardSettings>().EnableCaptchaForPost && !this.PageContext.IsCaptchaExcluded))
                && !CaptchaHelper.IsValid(this.tbCaptcha.Text.Trim()))
            {
                this.PageContext.AddLoadMessage(this.GetText("BAD_CAPTCHA"));
                return;
            }

            if (!(this.PageContext.IsAdmin || this.PageContext.ForumModeratorAccess)
                && this.Get<YafBoardSettings>().PostFloodDelay > 0)
            {
                if (YafContext.Current.Get<IYafSession>().LastPost
                    > DateTime.UtcNow.AddSeconds(-this.Get<YafBoardSettings>().PostFloodDelay))
                {
                    this.PageContext.AddLoadMessage(
                        this.GetTextFormatted(
                            "wait",
                            (YafContext.Current.Get<IYafSession>().LastPost
                             - DateTime.UtcNow.AddSeconds(-this.Get<YafBoardSettings>().PostFloodDelay)).Seconds));
                    return;
                }
            }

            YafContext.Current.Get<IYafSession>().LastPost = DateTime.UtcNow;

            // post message...
            long nMessageId = 0;
            object replyTo = -1;
            string msg = this._quickReplyEditor.Text;
            long topicId = this.PageContext.PageTopicID;

            // SPAM Check

            // Check if Forum is Moderated
            DataRow forumInfo;
            bool isForumModerated = false;

            using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageForumID))
            {
                forumInfo = dt.Rows[0];
            }

            if (forumInfo != null)
            {
                isForumModerated = forumInfo["Flags"].BinaryAnd(ForumFlags.Flags.IsModerated);
            }

            bool spamApproved = true;

            // Check for SPAM
            if (!this.PageContext.IsAdmin || !this.PageContext.ForumModeratorAccess)
            {
                if (YafSpamCheck.IsPostSpam(
                    this.PageContext.IsGuest ? UserMembershipHelper.GuestUserName : this.PageContext.PageUserName,
                    this.PageContext.PageTopicName,
                    this._quickReplyEditor.Text))
                {
                    if (this.Get<YafBoardSettings>().SpamMessageHandling.Equals(1))
                    {
                        spamApproved = false;
                    }
                    else if (this.Get<YafBoardSettings>().SpamMessageHandling.Equals(2))
                    {
                        this.PageContext.AddLoadMessage(this.GetText("SPAM_MESSAGE"));
                        return;
                    }
                }
            }

            // If Forum is Moderated
            if (isForumModerated)
            {
                spamApproved = false;
            }

            // Bypass Approval if Admin or Moderator
            if (this.PageContext.IsAdmin || this.PageContext.ForumModeratorAccess)
            {
                spamApproved = true;
            }

            var tFlags = new MessageFlags
                {
                    IsHtml = this._quickReplyEditor.UsesHTML,
                    IsBBCode = this._quickReplyEditor.UsesBBCode,
                    IsApproved = spamApproved
                };

            // Bypass Approval if Admin or Moderator.
            if (!CommonDb.message_save(
                PageContext.PageModuleID,
                topicId,
                this.PageContext.PageUserID,
                msg,
                null,
                this.Get<HttpRequestBase>().GetUserRealIPAddress(),
                null,
                replyTo,
                tFlags.BitValue,
                null,
                ref nMessageId))
            {
                topicId = 0;
            }

            // Check to see if the user has enabled "auto watch topic" option in his/her profile.
            if (this.PageContext.CurrentUserData.AutoWatchTopics)
            {
                using (
                    DataTable dt = CommonDb.watchtopic_check(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageTopicID))
                {
                    if (dt.Rows.Count == 0)
                    {
                        // subscribe to this forum
                        CommonDb.watchtopic_add(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageTopicID);
                    }
                }
            }

            bool isApproved = false;
            var ml = CommonDb.MessageList(PageContext.PageModuleID, (int)nMessageId);
            var typedMessageLists = ml as IList<TypedMessageList> ?? ml.ToList();
            if (typedMessageLists.Any())
            {
                isApproved = typedMessageLists.First().Flags.IsApproved;
            }

            if (isApproved)
            {
                // send new post notification to users watching this topic/forum
                this.Get<ISendNotification>().ToWatchingUsers(nMessageId.ToType<int>());

                // redirect to newly posted message
                YafBuildLink.Redirect(ForumPages.posts, "m={0}&#post{0}", nMessageId);
            }
            else
            {
                if (this.Get<YafBoardSettings>().EmailModeratorsOnModeratedPost)
                {
                    // not approved, notifiy moderators
                    this.Get<ISendNotification>().ToModeratorsThatMessageNeedsApproval(
                        this.PageContext.PageForumID, (int)nMessageId);
                }

                string url = YafBuildLink.GetLink(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
                if (Config.IsRainbow)
                {
                    YafBuildLink.Redirect(ForumPages.info, "i=1");
                }
                else
                {
                    YafBuildLink.Redirect(ForumPages.info, "i=1&url={0}", this.Server.UrlEncode(url));
                }
            }
        }

        /// <summary>
        /// The view menu_ item click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewMenu_ItemClick([NotNull] object sender, [NotNull] PopEventArgs e)
        {
            switch (e.Item.ToLower())
            {
                case "normal":
                    this.IsThreaded = false;
                    this.BindData();
                    break;
                case "threaded":
                    this.IsThreaded = true;
                    this.BindData();
                    break;
                default:
                    throw new ApplicationException(e.Item);
            }
        }

        /// <summary>
        /// The posts_ pre render.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void posts_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            bool isWatched = this.HandleWatchTopic();

            // share menu...
            this.ShareMenu.Visible = this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ShowShareTopicTo);
            this.ShareLink.Visible = this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ShowShareTopicTo);

            this.ShareLink.ToolTip = this.GetText("SHARE_TOOLTIP");

            if (this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ShowShareTopicTo))
            {
                var topicUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.posts, true, "t={0}", this.PageContext.PageTopicID);
                if (this.Get<YafBoardSettings>().AllowEmailTopic)
                {
                    this.ShareMenu.AddPostBackItem(
                        "email", this.GetText("EMAILTOPIC"), this.Get<ITheme>().GetItem("ICONS", "EMAIL"));
                }

                this.ShareMenu.AddClientScriptItem(
                    this.GetText("LINKBACK_TOPIC"),
                    "prompt('{0}','{1}');return false;".FormatWith(this.GetText("LINKBACK_TOPIC_PROMT"), topicUrl),
                    this.Get<ITheme>().GetItem("ICONS", "LINKBACK"));
                this.ShareMenu.AddPostBackItem(
                    "retweet", this.GetText("RETWEET_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "TWITTER"));
                this.ShareMenu.AddPostBackItem(
                    "googleplus", this.GetText("GOOGLEPLUS_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "GOOGLEPLUS"));
                /* this.ShareMenu.AddPostBackItem(
                 * "facebook", this.GetText("FACEBOOK_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "FACEBOOK"));*/

                var facebookUrl =
                    "http://www.facebook.com/plugins/like.php?href={0}".FormatWith(
                        this.Server.UrlEncode(topicUrl), this.Server.UrlEncode((string)this._topic["Topic"]));

                this.ShareMenu.AddClientScriptItem(
                    this.GetText("FACEBOOK_TOPIC"),
                    @"window.open('{0}','Facebook','width=300,height=200,resizable=yes');".FormatWith(facebookUrl),
                    this.Get<ITheme>().GetItem("ICONS", "FACEBOOK"));

                if (Config.FacebookApiKey.IsSet())
                {
                    this.ShareMenu.AddClientScriptItem(
                        this.GetText("FACEBOOK_SHARE_TOPIC"),
                        "postToFacebook()",
                        this.Get<ITheme>().GetItem("ICONS", "FACEBOOK"));
                }

                this.ShareMenu.AddPostBackItem(
                    "digg", this.GetText("DIGG_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "DIGG"));
                this.ShareMenu.AddPostBackItem(
                    "reddit", this.GetText("REDDIT_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "REDDIT"));

                this.ShareMenu.AddPostBackItem(
                    "tumblr", this.GetText("TUMBLR_TOPIC"), this.Get<ITheme>().GetItem("ICONS", "TUMBLR"));
            }
            else
            {
                if (this.Get<YafBoardSettings>().AllowEmailTopic)
                {
                    this.OptionsMenu.AddPostBackItem(
                        "email", this.GetText("EMAILTOPIC"), this.Get<ITheme>().GetItem("ICONS", "EMAIL"));
                }
            }

            // options menu...
            this.OptionsLink.ToolTip = this.GetText("OPTIONS_TOOLTIP");

            this.OptionsMenu.AddPostBackItem(
                "watch",
                isWatched ? this.GetText("UNWATCHTOPIC") : this.GetText("WATCHTOPIC"),
                this.Get<ITheme>().GetItem("ICONS", "EMAIL"));

            this.OptionsMenu.AddPostBackItem(
                "print", this.GetText("PRINTTOPIC"), this.Get<ITheme>().GetItem("ICONS", "PRINT"));

            if (this.Get<YafBoardSettings>().ShowAtomLink
                && this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().PostsFeedAccess))
            {
                this.OptionsMenu.AddPostBackItem(
                    "atomfeed", this.GetText("ATOMTOPIC"), this.Get<ITheme>().GetItem("ICONS", "ATOMFEED"));
            }

            if (this.Get<YafBoardSettings>().ShowRSSLink
                && this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().PostsFeedAccess))
            {
                this.OptionsMenu.AddPostBackItem(
                    "rssfeed", this.GetText("RSSTOPIC"), this.Get<ITheme>().GetItem("ICONS", "RSSFEED"));
            }

            // view menu
            this.ViewLink.ToolTip = this.GetText("VIEW_TOOLTIP");

            if (this.IsThreaded)
            {
                this.ViewMenu.AddPostBackItem("normal", this.GetText("NORMAL"));
                this.ViewMenu.AddPostBackItem("threaded", "&#187; {0}".FormatWith(this.GetText("THREADED")));
            }
            else
            {
                this.ViewMenu.AddPostBackItem("normal", "&#187; {0}".FormatWith(this.GetText("NORMAL")));
                this.ViewMenu.AddPostBackItem("threaded", this.GetText("THREADED"));
            }

            // attach the menus to HyperLinks
            this.ShareMenu.Attach(this.ShareLink);
            this.OptionsMenu.Attach(this.OptionsLink);
            this.ViewMenu.Attach(this.ViewLink);

            if (!this._dataBound)
            {
                this.BindData();
            }
        }
    }
}