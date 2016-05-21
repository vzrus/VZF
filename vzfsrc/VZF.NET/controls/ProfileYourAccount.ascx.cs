namespace VZF.Controls
{
  #region Using

  using System;
  using System.Data;

  using VZF.Data.Common;

  using YAF.Classes;
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The profile your account.
  /// </summary>
  public partial class ProfileYourAccount : BaseUserControl
  {
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
      if (!this.IsPostBack)
      {
        this.BindData();
      }
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      DataTable dt = CommonDb.usergroup_list(PageContext.PageModuleID, this.PageContext.PageUserID);

      if (YafContext.Current.BoardSettings.UseStyledNicks)
      {
        this.Get<IStyleTransform>().DecodeStyleByTable(ref dt, false);
      }

      this.Groups.DataSource = dt;

      // Bind			
      this.DataBind();

      // TitleUserName.Text = HtmlEncode( userData.Membership.UserName );
      this.AccountEmail.Text = this.PageContext.CurrentUserData.Membership.Email;
      this.Name.Text = this.HtmlEncode(this.PageContext.CurrentUserData.Membership.UserName);
      this.Joined.Text = this.Get<IDateTime>().FormatDateTime(this.PageContext.CurrentUserData.Joined);
      this.NumPosts.Text = "{0:N0}".FormatWith(this.PageContext.CurrentUserData.NumPosts);

      this.DisplayNameHolder.Visible = this.PageContext.BoardSettings.EnableDisplayName;

      if (this.PageContext.BoardSettings.EnableDisplayName)
      {
        this.DisplayName.Text =
          this.HtmlEncode(this.Get<IUserDisplayName>().GetName(this.PageContext.PageUserID));
      }

      string avatarImg = this.Get<IAvatars>().GetAvatarUrlForCurrentUser();

      if (avatarImg.IsSet())
      {
        this.AvatarImage.ImageUrl = avatarImg;
      }
      else
      {
        this.AvatarImage.Visible = false;
      }
    }

      /// <summary>
      /// The style transform data row.
      /// </summary>
      /// <param name="style">
      /// The style.
      /// </param>
      /// <returns>
      /// The <see cref="string"/>.
      /// </returns>
      public string StyleTransformDataRow([NotNull] object style)
    {
        if (this.Get<YafBoardSettings>().UseStyledNicks)
        {
            return this.Get<IStyleTransform>().DecodeStyleByString(style.ToString(), false);
        }

        return string.Empty;
    }

    #endregion
  }
}