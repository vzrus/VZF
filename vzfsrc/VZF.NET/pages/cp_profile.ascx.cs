using YAF.Classes;
using YAF.Types.Interfaces;

namespace YAF.Pages
{
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for editprofile.
  /// </summary>
  public partial class cp_profile : ForumPageRegistered
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="cp_profile"/> class.
    /// </summary>
    public cp_profile()
      : base("CP_PROFILE")
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
    }


    #endregion
  }
}