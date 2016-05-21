namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Core.Tasks;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

    #endregion

  /// <summary>
  /// Summary description for prune.
  /// </summary>
  public partial class prune : AdminPage
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
      this.commit.Click += this.commit_Click;

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
      if (!this.IsPostBack)
      {
          this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
       this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
       this.PageLinks.AddLink(this.GetText("ADMIN_PRUNE", "TITLE"), string.Empty);

       this.Page.Header.Title = "{0} - {1}".FormatWith(
             this.GetText("ADMIN_ADMIN", "Administration"),
             this.GetText("ADMIN_PRUNE", "TITLE"));

          this.commit.Text = this.GetText("ADMIN_PRUNE", "PRUNE_START");

        this.days.Text = "60";
        this.BindData();
      }

      this.lblPruneInfo.Text = string.Empty;

        if (!this.Get<ITaskModuleManager>().IsTaskRunning(PruneTopicTask.TaskName))
        {
            return;
        }

        this.lblPruneInfo.Text = this.GetText("ADMIN_PRUNE", "PRUNE_INFO");

        this.commit.Enabled = false;
    }

    /// <summary>
    /// The prune button_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void PruneButton_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_PRUNE", "CONFIRM_PRUNE"));
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
        this.forumlist.DataSource = CommonDb.forum_listread(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageUserID, null, null, false, false, true,false, null);

        this.forumlist.DataValueField = "ForumID";
        this.forumlist.DataTextField = "Forum";

        this.DataBind();

        this.forumlist.Items.Insert(0, new ListItem(this.GetText("ADMIN_PRUNE", "ALL_FORUMS"), "0"));
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    /// <summary>
    /// The commit_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void commit_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      PruneTopicTask.Start(
        YafContext.Current.PageModuleID,
        this.PageContext.PageBoardID, 
        this.forumlist.SelectedValue.ToType<int>(), 
        this.days.Text.ToType<int>(), 
        this.permDeleteChkBox.Checked,
        this.deletedOnlyChkBox.Checked);

      this.PageContext.AddLoadMessage(this.GetText("ADMIN_PRUNE", "MSG_TASK"));
    }

    #endregion
  }
}