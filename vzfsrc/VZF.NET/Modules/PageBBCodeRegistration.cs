namespace YAF.Modules
{
  #region Using

  using YAF.Core.BBCode;
  using YAF.Types.Attributes;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Summary description for PageTitleModule
  /// </summary>
  [YafModule("Page BBCode Registration Module", "Tiny Gecko", 1)]
  public class PageBBCodeRegistration : SimpleBaseForumModule
  {
    #region Public Methods

    /// <summary>
    /// The init after page.
    /// </summary>
    public override void InitAfterPage()
    {
      switch (this.PageContext.ForumPageType)
      {
        case ForumPages.cp_message:
        case ForumPages.search:
        case ForumPages.lastposts:
        case ForumPages.posts:
        case ForumPages.profile:
          this.Get<IBBCode>().RegisterCustomBBCodePageElements(
            this.PageContext.CurrentForumPage.Page, this.PageContext.CurrentForumPage.GetType());
          break;
      }
    }

    #endregion
  }
}