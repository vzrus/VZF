﻿namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The Topic Status Edit Page
    /// </summary>
    public partial class topicstatus_edit : AdminPage
    {
        #region Methods

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.save.Click += this.Add_Click;
            this.cancel.Click += this.Cancel_Click;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
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
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_TOPICSTATUS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_topicstatus));
            this.PageLinks.AddLink(this.GetText("ADMIN_TOPICSTATUS_EDIT", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_TOPICSTATUS", "TITLE"),
                this.GetText("ADMIN_TOPICSTATUS_EDIT", "TITLE"));

            this.save.Text = this.GetText("SAVE");
            this.cancel.Text = this.GetText("CANCEL");

            this.BindData();
        }

        /// <summary>
        /// The add_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Add_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (TopicStatusName.Text.Trim().IsNotSet() || DefaultDescription.Text.Trim().IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_TOPICSTATUS_EDIT", "MSG_ENTER"));

                this.BindData();
            }
            else
            {
                CommonDb.TopicStatus_Save(PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("i"),
                    this.PageContext.PageBoardID,
                    TopicStatusName.Text.Trim(),
                    DefaultDescription.Text.Trim());

                YafBuildLink.Redirect(ForumPages.admin_topicstatus);
            }
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (!this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("i").IsSet())
            {
                return;
            }

            DataRow row =
                CommonDb.TopicStatus_Edit(PageContext.PageModuleID, Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("i"))).Rows[0];

            this.TopicStatusName.Text = (string)row["TopicStatusName"];
            this.DefaultDescription.Text = (string)row["DefaultDescription"];
        }

        /// <summary>
        /// The cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_topicstatus);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}