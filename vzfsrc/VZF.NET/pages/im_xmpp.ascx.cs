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
  /// Class to communicate in XMPP.
  /// </summary>
  public partial class im_xmpp : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "im_xmpp" /> class.
    /// </summary>
    public im_xmpp()
      : base("IM_XMPP")
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
        MembershipUser userHe = UserMembershipHelper.GetMembershipUserById(this.UserID);

        string displayNameHe = UserMembershipHelper.GetDisplayNameFromID(this.UserID);

        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
          this.PageContext.BoardSettings.EnableDisplayName ? displayNameHe : userHe.UserName, 
          YafBuildLink.GetLink(ForumPages.profile, "u={0}", this.UserID));
        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);

        if (this.UserID == this.PageContext.PageUserID)
        {
          this.NotifyLabel.Text = this.GetText("SERVERYOU");
        }
        else
        {
          if (userHe == null)
          {
            YafBuildLink.AccessDenied( /*No such user exists*/);
          }

          // Data for current page user
          MembershipUser userMe = UserMembershipHelper.GetMembershipUserById(this.PageContext.PageUserID);

          // get full user data...
          var userDataHe = new CombinedUserDataHelper(userHe, this.UserID);
          var userDataMe = new CombinedUserDataHelper(userMe, this.PageContext.PageUserID);

          string serverHe = userDataHe.Profile.XMPP.Substring(userDataHe.Profile.XMPP.IndexOf("@") + 1).Trim();
          string serverMe = userDataMe.Profile.XMPP.Substring(userDataMe.Profile.XMPP.IndexOf("@") + 1).Trim();
          if (serverMe == serverHe)
          {
            this.NotifyLabel.Text = this.GetTextFormatted("SERVERSAME", userDataHe.Profile.XMPP);
          }
          else
          {
            this.NotifyLabel.Text = this.GetTextFormatted("SERVEROTHER", "http://" + serverHe);
          }
        }
      }
    }

    #endregion
  }
}