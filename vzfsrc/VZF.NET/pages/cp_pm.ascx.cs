using YAF.Classes;

namespace YAF.Pages
{
  #region Using

  using System;

  using VZF.Controls;
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utilities;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The cp_pm.
  /// </summary>
  public partial class cp_pm : ForumPageRegistered
  {
    #region Constants and Fields

    /// <summary>
    ///   The _view.
    /// </summary>
    private PMView _view;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "cp_pm" /> class.
    /// </summary>
    public cp_pm()
      : base("CP_PM")
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets View.
    /// </summary>
    protected PMView View
    {
      get
      {
        return this._view;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The On PreRender event.
    /// </summary>
    /// <param name="e">
    /// the Event Arguments
    /// </param>
    protected override void OnPreRender([NotNull] EventArgs e)
    {
      // setup jQuery and Jquery Ui Tabs.
      YafContext.Current.PageElements.RegisterJQuery();
      YafContext.Current.PageElements.RegisterJQueryUI();

      YafContext.Current.PageElements.RegisterJsBlock(
                "yafPmTabsJs",
                JavaScriptBlocks.JqueryUITabsLoadJs(
                    this.PmTabs.ClientID,
                    this.hidLastTab.ClientID,
                    this.hidLastTabId.ClientID,
                    false));


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
      // check if this feature is disabled
      if (!this.PageContext.BoardSettings.AllowPrivateMessages)
      {
        YafBuildLink.RedirectInfoPage(InfoMessage.Disabled);
      }

      if (!this.IsPostBack)
      {
        if (this.Request.QueryString.GetFirstOrDefault("v").IsSet())
        {
          this._view = PMViewConverter.FromQueryString(this.Request.QueryString.GetFirstOrDefault("v"));

          this.hidLastTab.Value = ((int)this._view).ToString();
        }

        // if (_view == PMView.Inbox)
        // this.PMTabs.ActiveTab = this.InboxTab;
        // else if (_view == PMView.Outbox)
        // this.PMTabs.ActiveTab = this.OutboxTab;
        // else if (_view == PMView.Archive)
        // this.PMTabs.ActiveTab = this.ArchiveTab;
        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
         this.Get<YafBoardSettings>().EnableDisplayName
             ? this.PageContext.CurrentUserData.DisplayName
             : this.PageContext.PageUserName,
         YafBuildLink.GetLink(ForumPages.cp_profile));

        this.PageLinks.AddLink(this.GetText("TITLE"));

        // InboxTab.HeaderText = GetText("INBOX");
        // OutboxTab.HeaderText = GetText("SENTITEMS");
        // ArchiveTab.HeaderText = GetText("ARCHIVE");
        this.NewPM.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.pmessage);
        this.NewPM2.NavigateUrl = this.NewPM.NavigateUrl;

        // inbox tab
        // ScriptManager.RegisterClientScriptBlock(InboxTabUpdatePanel, typeof(UpdatePanel), "InboxTabRefresh", String.Format("function InboxTabRefresh() {1}\n__doPostBack('{0}', '');\n{2}", InboxTabUpdatePanel.ClientID, '{', '}'), true);
        // sent tab
        // ScriptManager.RegisterClientScriptBlock(SentTabUpdatePanel, typeof(UpdatePanel), "SentTabRefresh", String.Format("function SentTabRefresh() {1}\n__doPostBack('{0}', '');\n{2}", SentTabUpdatePanel.ClientID, '{', '}'), true);
        // archive tab
        // ScriptManager.RegisterClientScriptBlock(ArchiveTabUpdatePanel, typeof(UpdatePanel), "ArchiveTabRefresh", String.Format("function ArchiveTabRefresh() {1}\n__doPostBack('{0}', '');\n{2}", ArchiveTabUpdatePanel.ClientID, '{', '}'), true);
      }
    }

    #endregion
  }
}