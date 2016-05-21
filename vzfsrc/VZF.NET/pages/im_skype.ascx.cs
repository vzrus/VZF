namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Web.Security;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The im_skype.
  /// </summary>
  public partial class im_skype : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "im_skype" /> class.
    /// </summary>
    public im_skype()
      : base("IM_SKYPE")
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets UserID.
    /// </summary>
    public int UserID
    {
      get
      {
        return (int)Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("u"));
      }
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
      if (this.User == null)
      {
        YafBuildLink.AccessDenied();
      }

      if (!this.IsPostBack)
      {
        // get user data...
        MembershipUser user = UserMembershipHelper.GetMembershipUserById(this.UserID);

        if (user == null)
        {
          YafBuildLink.AccessDenied( /*No such user exists*/);
        }

        string displayName = UserMembershipHelper.GetDisplayNameFromID(this.UserID);

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
          this.PageContext.BoardSettings.EnableDisplayName
             ? displayName : user.UserName, 
          YafBuildLink.GetLink(ForumPages.profile, "u={0}", this.UserID));
        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);

        // get full user data...
        var userData = new CombinedUserDataHelper(user, this.UserID);

        this.Msg.NavigateUrl = "skype:{0}?call".FormatWith(userData.Profile.Skype);
        this.Msg.Attributes.Add("onclick", "return skypeCheck();");
        this.Img.Src = "http://mystatus.skype.com/bigclassic/{0}".FormatWith(userData.Profile.Skype);
      }
    }

    #endregion
  }
}