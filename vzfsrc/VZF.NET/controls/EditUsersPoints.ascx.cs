namespace VZF.Controls
{
    #region Using

    using System;

    using VZF.Data.Common;

    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The edit users points.
    /// </summary>
    public partial class EditUsersPoints : BaseUserControl
    {
        #region Properties

        /// <summary>
        ///   Gets user ID of edited user.
        /// </summary>
        protected int CurrentUserID
        {
            get
            {
                return this.PageContext.QueryIDs["u"].ToType<int>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add points_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void AddPoints_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            CommonDb.user_addpoints(PageContext.PageModuleID, this.CurrentUserID, null, this.txtAddPoints.Text);

            this.BindData();
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
            this.PageContext.QueryIDs = new QueryStringIDHelper("u", true);

            if (this.IsPostBack)
            {
                return;
            }

            this.Button1.Text = this.Get<ILocalization>().GetText("COMMON", "GO");
            this.btnAddPoints.Text = this.Get<ILocalization>().GetText("COMMON", "GO");
            this.btnUserPoints.Text = this.Get<ILocalization>().GetText("COMMON", "GO");

            this.BindData();
        }

        /// <summary>
        /// The remove points_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void RemovePoints_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            CommonDb.user_removepoints(PageContext.PageModuleID, this.CurrentUserID, null, this.txtRemovePoints.Text);
            this.BindData();
        }

        /// <summary>
        /// The set user points_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void SetUserPoints_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            CommonDb.user_setpoints(PageContext.PageModuleID, this.CurrentUserID, this.txtUserPoints.Text);
            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.ltrCurrentPoints.Text = CommonDb.user_getpoints(PageContext.PageModuleID, this.CurrentUserID).ToString();
        }

        #endregion
    }
}