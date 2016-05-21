namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.IO;
  using System.Linq;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;
  using VZF.Types.Data;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Flags;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

  #endregion

  /// <summary>
  /// Summary description for editgroup.
  /// </summary>
  public partial class editrank : AdminPage
  {
    #region Methods

    /// <summary>
    /// The cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      YafBuildLink.Redirect(ForumPages.admin_ranks);
    }

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
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
        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
       this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

        this.PageLinks.AddLink(this.GetText("ADMIN_RANKS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_ranks));

      // current page label (no link)
      this.PageLinks.AddLink(this.GetText("ADMIN_EDITRANK", "TITLE"), string.Empty);

      this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
         this.GetText("ADMIN_ADMIN", "Administration"),
         this.GetText("ADMIN_RANKS", "TITLE"),
         this.GetText("ADMIN_EDITRANK", "TITLE"));

      this.Save.Text = this.GetText("COMMON", "SAVE");
      this.Cancel.Text = this.GetText("COMMON", "CANCEL");

        this.BindData();

        if (this.Request.QueryString.GetFirstOrDefault("r") != null)
        {
            var dt = CommonDb.rank_list(
                PageContext.PageModuleID, this.PageContext.PageBoardID, this.Request.QueryString.GetFirstOrDefault("r")).ToList();

            var res = dt[0];
            var flags = new RankFlags(res.Flags);
            this.Name.Text = res.Name;
            this.Description.Text = res.Description;
            this.IsStart.Checked = flags.IsStart;
            this.IsLadder.Checked = flags.IsLadder;
              this.IsGuest.Checked = flags.IsGuest; 
            this.MinPosts.Text = res.MinPosts.ToString();
            this.PMLimit.Text = res.PMLimit.ToString();
            this.Style.Text = res.Style;
            this.RankPriority.Text = res.SortOrder.ToString();
            this.UsrAlbums.Text = res.UsrAlbums.ToString();
            this.UsrAlbumImages.Text = res.UsrAlbumImages.ToString();
            this.UsrSigChars.Text = res.UsrSigChars.ToString();
            this.UsrSigBBCodes.Text = res.UsrSigBBCodes;
            this.UsrSigHTMLTags.Text = res.UsrSigHTMLTags;

            ListItem item = this.RankImage.Items.FindByText(res.RankImage);
            if (item != null)
            {
              item.Selected = true;
              this.Preview.Src = "{0}{1}/{2}".FormatWith(
                YafForumInfo.ForumClientFileRoot, YafBoardFolders.Current.Ranks, res.RankImage); // path corrected
            }
            else
            {
              this.Preview.Src = "{0}images/spacer.gif".FormatWith(YafForumInfo.ForumClientFileRoot);
            }
          
        }
        else
        {
          this.Preview.Src = "{0}images/spacer.gif".FormatWith(YafForumInfo.ForumClientFileRoot);
        }
      }

      this.RankImage.Attributes["onchange"] =
        "getElementById('{2}_ctl01_Preview').src='{0}{1}/' + this.value".FormatWith(
          YafForumInfo.ForumClientFileRoot, YafBoardFolders.Current.Ranks, this.Parent.ID);
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
      if (!ValidationHelper.IsValidInt(this.PMLimit.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITGROUP", "MSG_VALID_NUMBER"));
        return;
      }

      if (!ValidationHelper.IsValidInt(this.RankPriority.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITRANK", "MSG_RANK_INTEGER"));
        return;
      }

      if (!ValidationHelper.IsValidInt(this.UsrAlbums.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITGROUP", "MSG_ALBUM_NUMBER"));
        return;
      }

      if (!ValidationHelper.IsValidInt(this.UsrSigChars.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITGROUP", "MSG_SIG_NUMBER"));
        return;
      }

      if (!ValidationHelper.IsValidInt(this.UsrAlbumImages.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITGROUP", "MSG_TOTAL_NUMBER"));
        return;
      }

      // Group
      int rankID = 0;
      if (this.Request.QueryString.GetFirstOrDefault("r") != null)
      {
        rankID = int.Parse(this.Request.QueryString.GetFirstOrDefault("r"));
      }

      object rankImage = null;
      if (this.RankImage.SelectedIndex > 0)
      {
        rankImage = this.RankImage.SelectedValue;
      }

      CommonDb.rank_save(PageContext.PageModuleID, rankID, 
        this.PageContext.PageBoardID, 
        this.Name.Text, 
        this.IsStart.Checked, 
        this.IsLadder.Checked,
        this.IsGuest.Checked,
        this.MinPosts.Text, 
        rankImage, 
        this.PMLimit.Text.Trim().ToType<int>(), 
        this.Style.Text.Trim(), 
        this.RankPriority.Text.Trim(), 
        this.Description.Text, 
        this.UsrSigChars.Text.Trim().ToType<int>(), 
        this.UsrSigBBCodes.Text.Trim(), 
        this.UsrSigHTMLTags.Text.Trim(), 
        this.UsrAlbums.Text.Trim().ToType<int>(), 
        this.UsrAlbumImages.Text.Trim().ToType<int>());

      // Clearing cache with old permisssions data...
      this.Get<IDataCache>().RemoveOf<object>(
        k => k.Key.StartsWith(Constants.Cache.ActiveUserLazyData.FormatWith(String.Empty)));

      YafBuildLink.Redirect(ForumPages.admin_ranks);
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      using (var dt = new DataTable("Files"))
      {
        dt.Columns.Add("FileID", typeof(long));
        dt.Columns.Add("FileName", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        DataRow dr = dt.NewRow();
        dr["FileID"] = 0;
        dr["FileName"] = "../spacer.gif"; // use blank.gif for Description Entry
        dr["Description"] = this.GetText("ADMIN_EDITRANK", "SELECT_IMAGE");
        dt.Rows.Add(dr);

        var dir =
          new DirectoryInfo(
            this.Request.MapPath("{0}{1}".FormatWith(YafForumInfo.ForumServerFileRoot, YafBoardFolders.Current.Ranks)));
        FileInfo[] files = dir.GetFiles("*.*");
        long nFileID = 1;

        foreach (FileInfo file in from file in files
                                  let sExt = file.Extension.ToLower()
                                  where sExt == ".png" || sExt == ".gif" || sExt == ".jpg"
                                  select file)
        {
            dr = dt.NewRow();
            dr["FileID"] = nFileID++;
            dr["FileName"] = file.Name;
            dr["Description"] = file.Name;
            dt.Rows.Add(dr);
        }

        this.RankImage.DataSource = dt;
        this.RankImage.DataValueField = "FileName";
        this.RankImage.DataTextField = "Description";
      }

      this.DataBind();
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