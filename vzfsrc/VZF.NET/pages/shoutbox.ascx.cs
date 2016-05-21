namespace YAF.Pages
{
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The shoutbox.
  /// </summary>
  public partial class shoutbox : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "shoutbox" /> class.
    /// </summary>
    public shoutbox()
      : base("SHOUTBOX")
    {
      this.AllowAsPopup = true;
    }

    #endregion

    #region Methods

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
      this.ShoutBox1.Visible = this.PageContext.BoardSettings.ShowShoutbox && !this.PageContext.IsGuest;
      this.MustBeLoggedIn.Visible = this.PageContext.IsGuest;
    }

    #endregion
  }
}