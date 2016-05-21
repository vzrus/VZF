using YAF.Classes;

namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for cp_signature.
  /// </summary>
  public partial class cp_signature : ForumPageRegistered
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="cp_signature"/> class.
    /// </summary>
    public cp_signature()
      : base("CP_SIGNATURE")
    {
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
        if (!this.Get<YafBoardSettings>().AllowSignatures
            && !(this.PageContext.IsAdmin || this.PageContext.IsForumModerator))
        {
            // Not accessbile...
            YafBuildLink.AccessDenied();
        }

        if (this.IsPostBack)
        {
            return;
        }

        this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
            this.Get<YafBoardSettings>().EnableDisplayName
                ? this.PageContext.CurrentUserData.DisplayName
                : this.PageContext.PageUserName,
            YafBuildLink.GetLink(ForumPages.cp_profile));
        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);
    }


    #endregion
  }
}