namespace YAF.Modules
{
  #region Using

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;

  #endregion

  /// <summary>
  /// Summary description for SimpleBaseModule
  /// </summary>
  public class SimpleBaseForumModule : BaseForumModule
  {
    #region Constants and Fields

    /// <summary>
    ///   The _forum page type.
    /// </summary>
    protected ForumPages _forumPageType;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets CurrentForumPage.
    /// </summary>
    public ForumPage CurrentForumPage
    {
      get
      {
        return this.PageContext.CurrentForumPage;
      }
    }

    /// <summary>
    ///   Gets ForumControl.
    /// </summary>
    public Forum ForumControl
    {
      get
      {
        return (Forum)this.ForumControlObj;
      }
    }

    /// <summary>
    ///   Gets ForumPageType.
    /// </summary>
    public ForumPages ForumPageType
    {
      get
      {
        return this.PageContext.ForumPageType;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The init.
    /// </summary>
    public override void Init()
    {
      this.ForumControl.BeforeForumPageLoad += this.ForumControl_BeforeForumPageLoad;
      this.ForumControl.AfterForumPageLoad += this.ForumControl_AfterForumPageLoad;
      this.InitForum();
    }

    /// <summary>
    /// The init after page.
    /// </summary>
    public virtual void InitAfterPage()
    {
    }

    /// <summary>
    /// The init before page.
    /// </summary>
    public virtual void InitBeforePage()
    {
    }

    /// <summary>
    /// The init forum.
    /// </summary>
    public virtual void InitForum()
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The forum control_ after forum page load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ForumControl_AfterForumPageLoad([NotNull] object sender, [NotNull] YafAfterForumPageLoad e)
    {
      this.InitAfterPage();
    }

    /// <summary>
    /// The forum control_ before forum page load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ForumControl_BeforeForumPageLoad([NotNull] object sender, [NotNull] YafBeforeForumPageLoad e)
    {
      this.InitBeforePage();
    }

    #endregion
  }
}