namespace VZF.Controls
{
  #region Using

  using System;
  using System.Data;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  
  using YAF.Core;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.EventProxies;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

  #endregion

  /// <summary>
  /// The edit users groups.
  /// </summary>
  public partial class EditUsersGroups : BaseUserControl
  {
    #region Properties

    /// <summary>
    ///   Gets user ID of edited user.
    /// </summary>
    protected int CurrentUserID
    {
      get
      {
        return (int)this.PageContext.QueryIDs["u"];
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles click on cancel button.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      // redurect to user admin page.
      YafBuildLink.Redirect(ForumPages.admin_users);
    }

    /// <summary>
    /// Checks if user is memeber of role or not depending on value of parameter.
    /// </summary>
    /// <param name="o">
    /// Parameter if 0, user is not member of a role.
    /// </param>
    /// <returns>
    /// True if user is member of role (o &gt; 0), false otherwise.
    /// </returns>
    protected bool IsMember([NotNull] object o)
    {
      return long.Parse(o.ToString()) > 0;
    }

    /// <summary>
    /// Handles page load event.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        this.PageContext.QueryIDs = new QueryStringIDHelper("u", true);

        // this needs to be done just once, not during postbacks
        if (this.IsPostBack)
        {
            return;
        }

        this.Save.Text = this.Get<ILocalization>().GetText("COMMON", "SAVE");

        // bind data
        this.BindData();
    }

      /// <summary>
    /// Handles click on save button.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      // go through all roles displayed on page
      for (int i = 0; i < this.UserGroups.Items.Count; i++)
      {
        // get current item
        RepeaterItem item = this.UserGroups.Items[i];

        // get role ID from it
        int roleID = int.Parse(((Label)item.FindControl("GroupID")).Text);

        // get role name
        string roleName = string.Empty;
        using (DataTable dt = CommonDb.group_list(this.PageContext.PageModuleID, this.PageContext.PageBoardID, roleID, 0, 1000000))
        {
          foreach (DataRow row in dt.Rows)
          {
            roleName = (string)row["Name"];
          }
        }

        // is user supposed to be in that role?
        bool isChecked = ((CheckBox)item.FindControl("GroupMember")).Checked;

        // save user in role
        CommonDb.usergroup_save(PageContext.PageModuleID, this.CurrentUserID, roleID, isChecked);

        // empty out access table
        CommonDb.activeaccess_reset(PageContext.PageModuleID);

        // update roles if this user isn't the guest
          if (UserMembershipHelper.IsGuestUser(this.CurrentUserID))
          {
              continue;
          }

          // get user's name
          string userName = UserMembershipHelper.GetUserNameFromID(this.CurrentUserID);

          // add/remove user from roles in membership provider
          if (isChecked && !RoleMembershipHelper.IsUserInRole(userName, roleName))
          {
              RoleMembershipHelper.AddUserToRole(userName, roleName);
          }
          else if (!isChecked && RoleMembershipHelper.IsUserInRole(userName, roleName))
          {
              RoleMembershipHelper.RemoveUserFromRole(userName, roleName);
          }

          // Clearing cache with old permisssions data...
        this.Get<IDataCache>().Remove(Constants.Cache.ActiveUserLazyData.FormatWith(this.CurrentUserID));
      }

      // update forum moderators cache just in case something was changed...
      this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

      // clear the cache for this user...
      this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this.CurrentUserID));

      this.BindData();
    }

    /// <summary>
    /// Bind data for this control.
    /// </summary>
    private void BindData()
    {
      // get user roles
      this.UserGroups.DataSource = CommonDb.group_member(PageContext.PageModuleID, this.PageContext.PageBoardID, this.CurrentUserID);

      // bind data to controls
      this.DataBind();
    }

    #endregion
  }
}