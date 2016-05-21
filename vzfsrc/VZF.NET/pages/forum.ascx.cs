namespace YAF.Pages
{
  #region Using

    using System;
    using System.Web;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;

    #endregion

  /// <summary>
  /// The Main Forum Page.
  /// </summary>
  public partial class forum : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "forum" /> class.
    /// </summary>
    public forum()
      : base("DEFAULT")
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.PollList.Visible = this.Get<YafBoardSettings>().BoardPollID > 0;
      this.PollList.PollGroupId = this.Get<YafBoardSettings>().BoardPollID;
      this.PollList.BoardId = this.PageContext.Settings.BoardID;

      // Since these controls have EnabledViewState=false, set their visibility on every page load so that this value is not lost on postback.
      // This is important for another reason: these are board settings; values in the view state should have no impact on whether these controls are shown or not.
      this.ShoutBox1.Visible = this.Get<YafBoardSettings>().ShowShoutbox;
      this.ForumStats.Visible = this.Get<YafBoardSettings>().ShowForumStatistics;
      this.ActiveDiscussions.Visible = this.Get<YafBoardSettings>().ShowActiveDiscussions;

        if (this.IsPostBack)
        {
            return;
        }
        
        /*
        // vzrus: needs testing, potentially can cause problems 

        // Jaben: I can't access MY OWN FORUM with this code. Commented out. Either it's an optional feature or will be completely removed.
        // As far as I can see this is the worst kind of "feature": that no one asked for and solves a problem that no one had.

        //string ua = HttpContext.Current.Request.UserAgent;

        //if (!UserAgentHelper.IsSearchEngineSpider(ua) && (!UserAgentHelper.IsNotCheckedForCookiesAndJs(ua)))
        //{
        //  if (!HttpContext.Current.Request.Browser.Cookies)
        //  {
        //    YafBuildLink.RedirectInfoPage(InfoMessage.RequiresCookies);
        //  }

        //  Version ecmaVersion = HttpContext.Current.Request.Browser.EcmaScriptVersion;

        //  if (ecmaVersion != null)
        //  {
        //    try
        //    {
        //      string[] arrJsVer = Config.BrowserJSVersion.Split('.');

        //      if (!(ecmaVersion.Major >= arrJsVer[0].ToType<int>()) && !(ecmaVersion.Minor >= arrJsVer[1].ToType<int>()))
        //      {
        //        YafBuildLink.RedirectInfoPage(InfoMessage.EcmaScriptVersionUnsupported);
        //      }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //  }
        //  else
        //  {
        //    YafBuildLink.RedirectInfoPage(InfoMessage.RequiresEcmaScript);
        //  }
        //}*/

        if (this.PageContext.Settings.LockedForum == 0)
        {
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            if (this.PageContext.PageCategoryID != 0)
            {
                this.PageLinks.AddLink(
                    this.PageContext.PageCategoryName, 
                    YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
                this.Welcome.Visible = false;
            }
        }

        int rl = this.Get<YafBoardSettings>().RestartApplicationLimit;
        if (rl <= 0 || General.GetCurrentTrustLevel() <= AspNetHostingPermissionLevel.Medium)
        {
            return;
        }

        if (Platform.AllocatedMemory <= rl * 1000000)
        {
            return;
        }
    
        HttpRuntime.UnloadAppDomain();
        this.Logger.Info(
            "Application allocated memory is {0}. This exceeds limit set and application is restarting. Please, find the reason or reset limit. ",
            Platform.AllocatedMemory);
    }

    #endregion
  }
}