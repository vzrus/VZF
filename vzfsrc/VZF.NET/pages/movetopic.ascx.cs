using System.Web;
using VZF.Utilities;

namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Web.UI.WebControls;

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
  public partial class movetopic : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "movetopic" /> class.
    /// </summary>
    public movetopic()
      : base("MOVETOPIC")
    {
    }

    #endregion

    #region Methods

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

      int? linkDays = null;
      int ld = -2;
      if (this.LeavePointer.Checked && this.LinkDays.Text.IsSet() && !int.TryParse(this.LinkDays.Text, out ld))
      {
          this.PageContext.AddLoadMessage(this.GetText("POINTER_DAYS_INVALID"));
          return;
      }
      int? selectedForum = null;
      if (!Config.LargeForumTree)
      {
          selectedForum = this.ForumList.SelectedValue.ToType<int>();
          if (selectedForum <= 0)
          {
              this.PageContext.AddLoadMessage(this.GetText("CANNOT_MOVE_TO_CATEGORY"));
              return;
          }
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

      // only move if it's a destination is a different forum.
      if (selectedForum != this.PageContext.PageForumID)
      {
          if (ld >= -2)
          {
              linkDays = ld;
          }
          // Ederon : 7/14/2007
          CommonDb.topic_move(PageContext.PageModuleID, this.PageContext.PageTopicID, selectedForum, this.LeavePointer.Checked, linkDays);
      }
      this.Get<IYafSession>().NntpTreeActiveNode = null;
      YafBuildLink.Redirect(ForumPages.topics, "f={0}", this.PageContext.PageForumID);
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
      InitializeComponent();
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
                  "treemovetopic",
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
          if (this.Request.QueryString.GetFirstOrDefault("t") == null || !this.PageContext.ForumModeratorAccess)
          {
              YafBuildLink.AccessDenied();
          }

          if (this.IsPostBack)
          {
              return;
          }

          if (this.PageContext.Settings.LockedForum == 0)
          {
              this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
              this.PageLinks.AddLink(
                  this.PageContext.PageCategoryName,
                  YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
          }

          this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
          this.PageLinks.AddLink(
              this.PageContext.PageTopicName,
              YafBuildLink.GetLink(ForumPages.posts, "t={0}", this.PageContext.PageTopicID));

          this.Move.Text = this.GetText("MOVE");
          this.Move.ToolTip = "{0}: {1}".FormatWith(this.GetText("MOVE"), this.PageContext.PageTopicName);

          bool showMoved = this.Get<YafBoardSettings>().ShowMoved;
          // Ederon : 7/14/2007 - by default, leave pointer is set on value defined on host level
          this.LeavePointer.Checked = this.Get<YafBoardSettings>().ShowMoved;

          trLeaveLink.Visible = showMoved;
          trLeaveLinkDays.Visible = showMoved;
          if (showMoved)
          {
              LinkDays.Text = "1";
          }

          if (!Config.LargeForumTree)
          {
              this.ForumList.DataSource = CommonDb.forum_listall_sorted(PageContext.PageModuleID,
                  this.PageContext.PageBoardID, this.PageContext.PageUserID);
          }
          this.DataBind();
          if (!Config.LargeForumTree)
          {
              ListItem pageItem = this.ForumList.Items.FindByValue(this.PageContext.PageForumID.ToString());
              if (pageItem != null)
              {
                  pageItem.Selected = true;
              }
          }
      }

      /// <summary>
    /// Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private static void InitializeComponent()
    {
    }

    #endregion
  }
}