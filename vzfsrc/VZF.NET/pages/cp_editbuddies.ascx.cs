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
  /// The cp_editbuddies.
  /// </summary>
  public partial class cp_editbuddies : ForumPageRegistered
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the cp_editbuddies class.
    /// </summary>
    public cp_editbuddies()
      : base("CP_EDITBUDDIES")
    {
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
                "yafBuddiesTabsJs",
                JavaScriptBlocks.JqueryUITabsLoadJs(
                    this.BuddiesTabs.ClientID,
                    this.hidLastTab.ClientID,
                    this.hidLastTabId.ClientID,
                    false));

      base.OnPreRender(e);
    }

    /// <summary>
    /// Called when the page loads
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.Get<YafBoardSettings>().EnableDisplayName
                    ? this.PageContext.CurrentUserData.DisplayName
                    : this.PageContext.PageUserName,
                YafBuildLink.GetLink(ForumPages.cp_profile));
            this.PageLinks.AddLink(this.GetText("BUDDYLIST_TT"), string.Empty);
        }

        this.BindData();
    }


    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.InitializeBuddyList(this.BuddyList1, 2);
      this.InitializeBuddyList(this.PendingBuddyList, 3);
      this.InitializeBuddyList(this.BuddyRequested, 4);
    }

    /// <summary>
    /// Initializes the values of BuddyList control's properties.
    /// </summary>
    /// <param name="customBuddyList">
    /// The custom BuddyList control.
    /// </param>
    /// <param name="mode">
    /// The mode of this BuddyList.
    /// </param>
    private void InitializeBuddyList([NotNull] BuddyList customBuddyList, int mode)
    {
      customBuddyList.CurrentUserID = this.PageContext.PageUserID;
      customBuddyList.Mode = mode;
      customBuddyList.Container = this;
    }

    #endregion
  }
}