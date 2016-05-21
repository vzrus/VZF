namespace VZF.Controls
{
  #region Using

  using System;

  using YAF.Classes;
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// DisplayAd Class
  /// </summary>
  public partial class DisplayAd : BaseUserControl
  {
    #region Properties

    /// <summary>
    ///   Gets or sets a value indicating whether IsAlt.
    /// </summary>
    public bool IsAlt { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// The get post class.
    /// </summary>
    /// <returns>
    /// Returns the post class.
    /// </returns>
    [NotNull]
    protected string GetPostClass()
    {
      return this.IsAlt ? "post_alt" : "post";
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
      this.AdMessage.Message = this.Get<YafBoardSettings>().AdPost;
      this.AdMessage.Signature = this.GetText("AD_SIGNATURE");

      this.AdMessage.MessageFlags.IsLocked = true;
      this.AdMessage.MessageFlags.NotFormatted = true;
    }

    #endregion
  }
}