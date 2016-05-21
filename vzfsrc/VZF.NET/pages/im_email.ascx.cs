namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Net.Mail;
  using System.Web;
  using System.Web.Security;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for active.
  /// </summary>
  public partial class im_email : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "im_email" /> class.
    /// </summary>
    public im_email()
      : base("IM_EMAIL")
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
        return (int)Security.StringToLongOrRedirect(this.Get<HttpRequestBase>().QueryString["u"]);
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
      if (this.User == null || !this.Get<YafBoardSettings>().AllowEmailSending)
      {
        YafBuildLink.AccessDenied();
      }

        if (this.IsPostBack)
        {
            return;
        }

        // get user data...
        MembershipUser user = UserMembershipHelper.GetMembershipUserById(this.UserID);

        if (user == null)
        {
            YafBuildLink.AccessDenied(/*No such user exists*/);
        }

        string displayName = UserMembershipHelper.GetDisplayNameFromID(this.UserID);

        this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
            this.PageContext.BoardSettings.EnableDisplayName ? displayName : user.UserName, 
            YafBuildLink.GetLink(ForumPages.profile, "u={0}", this.UserID));
        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);

        this.Send.Text = this.GetText("SEND");
    }

    /// <summary>
    /// The send_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Send_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      try
      {
        // get "to" user...
        MembershipUser toUser = UserMembershipHelper.GetMembershipUserById(this.UserID);

        // send it...
        this.Get<ISendMail>().Send(
          new MailAddress(this.PageContext.User.Email, this.PageContext.User.UserName), 
          new MailAddress(toUser.Email.Trim(), toUser.UserName.Trim()), 
          this.Subject.Text.Trim(), 
          this.Body.Text.Trim());

        // redirect to profile page...
        YafBuildLink.Redirect(ForumPages.profile, false, "u={0}", this.UserID);
      }
      catch (Exception x)
      {
          CommonDb.eventlog_create(PageContext.PageModuleID, this.PageContext.PageUserID, this, x);

          this.PageContext.AddLoadMessage(this.PageContext.IsAdmin ? x.Message : this.GetText("ERROR"));
      }
    }

    #endregion
  }
}