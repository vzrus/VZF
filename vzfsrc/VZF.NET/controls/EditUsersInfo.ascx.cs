namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Web.Security;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.EventProxies;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The edit users info.
    /// </summary>
    public partial class EditUsersInfo : BaseUserControl
    {
        #region Properties

        /// <summary>
        ///   Gets user ID of edited user.
        /// </summary>
        protected int CurrentUserId
        {
            get { return this.PageContext.QueryIDs["u"].ToType<int>(); }
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
            this.PageContext.QueryIDs = new QueryStringIDHelper("u", true);

            this.IsHostAdminRow.Visible = this.PageContext.IsHostAdmin;

            if (this.IsPostBack)
            {
                return;
            }

            this.Save.Text = this.Get<ILocalization>().GetText("COMMON", "SAVE");

            this.BindData();
        }

        /// <summary>
        /// The save_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // Update the Membership
            if (!this.IsGuestX.Checked)
            {
                string emailx = this.Email.Text.Trim();

                MembershipUser user = UserMembershipHelper.GetUser(this.Name.Text.Trim());

                // var usr = new CombinedUserDataHelper(user);
                string userName = this.Get<MembershipProvider>().GetUserNameByEmail(emailx);
                if ((userName != null && userName != user.UserName) 
                    || this.Get<YafBoardSettings>().ForumEmail == emailx)
                {
                    this.PageContext.AddLoadMessage(this.GetText("PROFILE", "BAD_EMAIL"));
                    return;
                }

                if (user.Email != emailx)
                {
                    // update the e-mail here too...
                    user.Email = emailx;
                }

                // Update IsApproved
                user.IsApproved = this.IsApproved.Checked;

                this.Get<MembershipProvider>().UpdateUser(user);
            }
            else
            {
                if (!this.IsApproved.Checked)
                {
                    this.PageContext.AddLoadMessage(this.Get<ILocalization>()
                        .GetText("ADMIN_EDITUSER", "MSG_GUEST_APPROVED"));
                    return;
                }
            }

            var userFlags = new UserFlags
            {
                IsHostAdmin = this.IsHostAdminX.Checked,
                IsGuest = this.IsGuestX.Checked,
                IsCaptchaExcluded = this.IsCaptchaExcluded.Checked,
                IsActiveExcluded = this.IsExcludedFromActiveUsers.Checked,
                IsApproved = this.IsApproved.Checked
            };

            CommonDb.user_adminsave(
                PageContext.PageModuleID, 
                this.PageContext.PageBoardID,
                this.CurrentUserId,
                this.Name.Text.Trim(),
                this.DisplayName.Text.Trim(),
                this.Email.Text.Trim(),
                userFlags.BitValue,
                this.RankID.SelectedValue);

            this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this.CurrentUserId));

            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.RankID.DataSource = CommonDb.rank_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null);
            this.RankID.DataValueField = "RankID";
            this.RankID.DataTextField = "Name";
            this.RankID.DataBind();

            using (
                DataTable dt = CommonDb.user_list(
                    PageContext.PageModuleID, 
                    this.PageContext.PageBoardID,
                    this.CurrentUserId, 
                    null))
            {
                DataRow row = dt.Rows[0];
                var userFlags = new UserFlags(row["Flags"]);

                this.Name.Text = (string)row["Name"];
                this.DisplayName.Text = row.Field<string>("DisplayName");
                this.Email.Text = row["Email"].ToString();
                this.IsHostAdminX.Checked = userFlags.IsHostAdmin;
                this.IsApproved.Checked = userFlags.IsApproved;
                this.IsGuestX.Checked = userFlags.IsGuest;
                this.IsCaptchaExcluded.Checked = userFlags.IsCaptchaExcluded;
                this.IsExcludedFromActiveUsers.Checked = userFlags.IsActiveExcluded;
                this.Joined.Text = row["Joined"].ToString();
                this.IsFacebookUser.Checked = row["IsFacebookUser"].ToType<bool>();
                this.IsTwitterUser.Checked = row["IsTwitterUser"].ToType<bool>();
                this.LastVisit.Text = row["LastVisit"].ToString();
                ListItem item = this.RankID.Items.FindByValue(row["RankID"].ToString());

                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        #endregion
    }
}