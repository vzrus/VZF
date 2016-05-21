namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
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
    /// Summary description for approve.
    /// </summary>
    public partial class approve : ForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "approve" /> class.
        /// </summary>
        public approve()
            : base("APPROVE")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets a value indicating whether IsProtected.
        /// </summary>
        public override bool IsProtected
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The validate key_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public void ValidateKey_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            DataTable dt = CommonDb.checkemail_update(PageContext.PageModuleID, this.key.Text);
            DataRow row = dt.Rows[0];
            string dbEmail = row["Email"].ToString();

            bool keyVerified = row["ProviderUserKey"] != DBNull.Value;

            this.approved.Visible = keyVerified;
            this.error.Visible = !keyVerified;

            if (!keyVerified)
            {
                return;
            }

            // approve and update e-mail in the membership as well...
            MembershipUser user = UserMembershipHelper.GetMembershipUserByKey(row["ProviderUserKey"]);
            if (!user.IsApproved)
            {
                user.IsApproved = true;
            }

            // update the email if anything was returned...
            if (user.Email != dbEmail && dbEmail != string.Empty)
            {
                user.Email = dbEmail;
            }

            // tell the provider to update...
            this.Get<MembershipProvider>().UpdateUser(user);

            // now redirect to main site...
            this.PageContext.LoadMessage.AddSession(this.GetText("EMAIL_VERIFIED"), MessageTypes.Information);

            // default redirect -- because if may not want to redirect to login.
            YafBuildLink.Redirect(ForumPages.forum);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);

            this.ValidateKey.Text = this.GetText("validate");
            if (this.Request.QueryString["k"] != null)
            {
                this.key.Text = this.Request.QueryString["k"];
                this.ValidateKey_Click(sender, e);
            }
            else
            {
                this.approved.Visible = false;
                this.error.Visible = !this.approved.Visible;
            }
        }

        #endregion
    }
}