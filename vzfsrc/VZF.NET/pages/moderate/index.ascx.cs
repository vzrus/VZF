namespace YAF.Pages.moderate
{
    #region Using

    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    using VZF.Data.Common;
    using VZF.Data.DAL;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Base root control for moderating, linking to other moderating controls/pages.
    /// </summary>
    public partial class index : ModerateForumPage
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "index" /> class. 
        ///   Default constructor.
        /// </summary>
        public index()
            : base("MODERATE_DEFAULT")
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates page links for this page.
        /// </summary>
        protected override void CreatePageLinks()
        {
            // forum index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // moderation index
            this.PageLinks.AddLink(this.GetText("TITLE"));
        }

        /// <summary>
        /// Handles event of item commands for each forum.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void ForumList_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            // which command are we handling
            switch (e.CommandName.ToLower())
            {
                case "viewunapprovedposts":

                    // go to unapproved posts for selected forum
                    YafBuildLink.Redirect(ForumPages.moderate_unapprovedposts, "f={0}", e.CommandArgument);
                    break;
                case "viewreportedposts":

                    // go to spam reports for selected forum
                    YafBuildLink.Redirect(ForumPages.moderate_reportedposts, "f={0}", e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        /// Handles page load event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // this needs to be done just once, not during postbacks
            if (this.IsPostBack)
            {
                return;
            }

            // create page links
            this.CreatePageLinks();

            // bind data
            this.BindData();
        }

        /// <summary>
        /// Bind data for this control.
        /// </summary>
        private void BindData()
        {
            // get list of forums and their moderating data
            using (DataSet ds = CommonDb.forum_moderatelist(PageContext.PageModuleID, this.PageContext.PageUserID, this.PageContext.PageBoardID))
            {
                this.CategoryList.DataSource = ds.Tables["Category"];
            }

            // bind data to controls
            this.DataBind();

            if (this.CategoryList.Items.Count.Equals(0))
            {
                this.InfoPlaceHolder.Visible = true;
            }
        }

        #endregion
    }
}