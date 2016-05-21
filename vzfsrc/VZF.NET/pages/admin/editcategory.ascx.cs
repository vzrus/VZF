namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;
    using YAF.Core.Tasks;

  #endregion

  /// <summary>
  /// Class for the Edit Category Page
  /// </summary>
  public partial class editcategory : AdminPage
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
      YafBuildLink.Redirect(ForumPages.admin_forums);
    }

    /// <summary>
    /// The create images data table.
    /// </summary>
    protected void CreateImagesDataTable()
    {
      using (var dt = new DataTable("Files"))
      {
        dt.Columns.Add("FileID", typeof(long));
        dt.Columns.Add("FileName", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        DataRow dr = dt.NewRow();
        dr["FileID"] = 0;
        dr["FileName"] = "../spacer.gif"; // use blank.gif for Description Entry
        dr["Description"] = "None";
        dt.Rows.Add(dr);

        var dir =
          new DirectoryInfo(
            this.Request.MapPath(
              "{0}{1}".FormatWith(YafForumInfo.ForumServerFileRoot, YafBoardFolders.Current.Categories)));
        if (dir.Exists)
        {
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
        }

        this.CategoryImages.DataSource = dt;
        this.CategoryImages.DataValueField = "FileName";
        this.CategoryImages.DataTextField = "Description";
        this.CategoryImages.DataBind();
      }
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
        this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

        this.PageLinks.AddLink(this.GetText("TEAM", "FORUMS"), YafBuildLink.GetLink(ForumPages.admin_forums));
        this.PageLinks.AddLink(this.GetText("ADMIN_EDITCATEGORY", "TITLE"), string.Empty);

        this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
             this.GetText("ADMIN_ADMIN", "Administration"),
             this.GetText("TEAM", "FORUMS"),
             this.GetText("ADMIN_EDITCATEGORY", "TITLE"));

        this.Save.Text = this.GetText("SAVE");
        this.Cancel.Text = this.GetText("CANCEL");
        
        // Populate Category Table
        this.CreateImagesDataTable();

        this.CategoryImages.Attributes["onchange"] =
            "getElementById('{1}').src='{0}{2}/' + this.value".FormatWith(
                YafForumInfo.ForumClientFileRoot, this.Preview.ClientID, YafBoardFolders.Current.Categories);
        this.rowSortOrder.Visible = !Config.LargeForumTree;

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
        int? categoryID = null;
        int? adjacentCategoryId = null;
        int? adjacentCategoryMode = null;
        if (this.Request.QueryString.GetFirstOrDefault("c") != null)
        {
            categoryID = GetQueryStringAsInt("c");
        }

        if (categoryID.HasValue && Config.LargeForumTree)
        {
            adjacentCategoryMode = -1;
        }

        if (this.Request.QueryString.GetFirstOrDefault("before") != null)
        {
            adjacentCategoryMode = 1;
            adjacentCategoryId = GetQueryStringAsInt("before");
        }
        if (this.Request.QueryString.GetFirstOrDefault("after") != null)
        {
            adjacentCategoryMode = 2;
            adjacentCategoryId = GetQueryStringAsInt("after");
        }

        short sortOrder = 0;
        string name = this.Name.Text.Trim();
        object categoryImage = null;

        if (this.CategoryImages.SelectedIndex > 0)
        {
            categoryImage = this.CategoryImages.SelectedValue;
        }

        if (!Config.LargeForumTree)
        {
            if (!ValidationHelper.IsValidPosShort(this.SortOrder.Text.Trim()))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITCATEGORY", "MSG_POSITIVE_VALUE"));
                return;
            }
        }

        if (!Config.LargeForumTree)
        {
            if (!short.TryParse(this.SortOrder.Text.Trim(), out sortOrder))
            {
                // error...
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITCATEGORY", "MSG_NUMBER"));
                return;
            }
        }

        if (string.IsNullOrEmpty(name))
        {
            // error...
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITCATEGORY", "MSG_VALUE"));
            return;
        }


        string failureMessage;
        // save category
        CategorySaveTask.Start(PageContext.PageModuleID, this.PageContext.PageBoardID, categoryID, name, categoryImage, sortOrder, this.CanHavePersForums.Checked, adjacentCategoryId, adjacentCategoryMode, out failureMessage);

        // remove category cache...
        this.Get<IDataCache>().Remove(Constants.Cache.ForumCategory);     

        // redirect
        YafBuildLink.Redirect(ForumPages.admin_forums);
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      this.Preview.Src = "{0}images/spacer.gif".FormatWith(YafForumInfo.ForumClientFileRoot);

      if (this.Request.QueryString.GetFirstOrDefault("c") == null)
      {
          if (!Config.LargeForumTree)
          {
              // Currently creating a New Category, and auto fill the Category Sort Order + 1
              using (
              DataTable dt = CommonDb.category_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null))
              {
                  int sortOrder = 1;

                  try
                  {
                      DataRow highestRow = dt.Rows[dt.Rows.Count - 1];

                      sortOrder = (short)highestRow["SortOrder"] + sortOrder;
                  }
                  catch
                  {
                      sortOrder = 1;
                  }

                  this.SortOrder.Text = sortOrder.ToString(CultureInfo.InvariantCulture);

                  return;
              }
          }
      }
      else
      {

          using (
              DataTable dt = CommonDb.category_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.GetQueryStringAsInt("c")))
          {
              DataRow row = dt.Rows[0];
              this.Name.Text = (string)row["Name"];
              if (!Config.LargeForumTree)
              {
                  this.SortOrder.Text = row["SortOrder"].ToString();
              }
              this.CategoryNameTitle.Text = this.Name.Text;
              this.CanHavePersForums.Checked = row["CanHavePersForums"].ToType<bool>();
              ListItem item = this.CategoryImages.Items.FindByText(row["CategoryImage"].ToString());

              if (item == null)
              {
                  return;
              }

              item.Selected = true;
              this.Preview.Src = "{0}{2}/{1}".FormatWith(
                  YafForumInfo.ForumClientFileRoot, row["CategoryImage"], YafBoardFolders.Current.Categories);

              // path corrected
          }
      }
    }

    /// <summary>
    /// Get query string as int.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The get query string as int.
    /// </returns>
    protected int? GetQueryStringAsInt([NotNull] string name)
    {
        int value;

        if (this.Request.QueryString.GetFirstOrDefault(name) != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault(name).Contains("_"))
            {

                return TreeViewUtils.GetParcedTreeNode(this.Request.QueryString.GetFirstOrDefault(name)).CategoryId;

            }

            if (int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }
        }

        return null;
    }   

    #endregion
  }
}