/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Pages.Admin
{
    #region Using
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;
    
    using VZF.Data.Common;
    using VZF.Types.Constants;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;
    using YAF.Core.Tasks;
    
    #endregion

  /// <summary>
  /// Administrative Page for the editing of forum properties.
  /// </summary>
  public partial class editforum : AdminPage
  {
    private bool UserForum { get; set; }
    private int? UsrId { get; set; }   

    private DataTable accessMaskListId; 

    #region Public Methods

    /// <summary>
    /// The category_ change.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public void Category_Change([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.BindParentList();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The bind data_ access mask id.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BindData_AccessMaskID([NotNull] object sender, [NotNull] EventArgs e)
    {
      ((DropDownList)sender).DataSource = accessMaskListId;  
      ((DropDownList)sender).DataValueField = "AccessMaskID";
      ((DropDownList)sender).DataTextField = "Name";
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
        dr["Description"] = this.GetText("COMMON", "NONE");
        dt.Rows.Add(dr);

        var dir =
          new DirectoryInfo(
            this.Request.MapPath("{0}{1}".FormatWith(YafForumInfo.ForumServerFileRoot, YafBoardFolders.Current.Forums)));
        if (dir.Exists)
        {
          FileInfo[] files = dir.GetFiles("*.*");
          long nFileID = 1;

          foreach (FileInfo file in from file in files
                                    let sExt = file.Extension.ToLower()
                                    where sExt == ".png" || sExt == ".gif" || sExt == ".jpg" || sExt == ".jpeg"
                                    select file)
          {
            dr = dt.NewRow();
            dr["FileID"] = nFileID++;
            dr["FileName"] = file.Name;
            dr["Description"] = file.Name;
            dt.Rows.Add(dr);
          }
        }

        this.ForumImages.DataSource = dt;
        this.ForumImages.DataValueField = "FileName";
        this.ForumImages.DataTextField = "Description";
        this.ForumImages.DataBind();
      }
    }

    /// <summary>
    /// The get query string as int.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// </returns>
    protected int? GetQueryStringForumIdAsInt([NotNull] string name)
    {
        int value;

        if (this.Request.QueryString.GetFirstOrDefault(name) != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault(name).Contains("_"))
            {
                return TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault(name)).Item3;
            }

            if (int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }
        }

        return null;
    }

    protected int? GetQueryStringCategoryIdAsInt([NotNull] string name)
    {
        int value;

        if (this.Request.QueryString.GetFirstOrDefault(name) != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault(name).Contains("_"))
            {
                return TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault(name)).Item3;
            }

            if (int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
            {
                return value;
            }
        }

        return null;
    }   

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      this.CategoryList.AutoPostBack = true;
      this.Save.Click += this.Save_Click;
      this.Cancel.Click += this.Cancel_Click;
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
       var forumId = this.GetQueryStringForumIdAsInt("fa") ?? this.GetQueryStringForumIdAsInt("copy");     
     

      this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
      this.PageLinks.AddLink(
        this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

      this.PageLinks.AddLink(this.GetText("TEAM", "FORUMS"), YafBuildLink.GetLink(ForumPages.admin_forums));
      this.PageLinks.AddLink(this.GetText("ADMIN_EDITFORUM", "TITLE"), string.Empty);

      this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
        this.GetText("ADMIN_ADMIN", "Administration"), 
        this.GetText("TEAM", "FORUMS"), 
        this.GetText("ADMIN_EDITFORUM", "TITLE"));

      this.Save.Text = this.GetText("SAVE");
      this.Cancel.Text = this.GetText("CANCEL");

      // Populate Forum Images Table
      this.CreateImagesDataTable();

      this.ForumImages.Attributes["onchange"] =
        "getElementById('{1}').src='{0}{2}/' + this.value".FormatWith(
          YafForumInfo.ForumClientFileRoot, this.Preview.ClientID, YafBoardFolders.Current.Forums);
      if (Config.LargeForumTree)
      {
          rowCategoryList.Visible = false;
          rowParentList.Visible = false;
          sortOrderRow.Visible = false;
      }

      this.BindData();

    

        if (!forumId.HasValue)
        {
            this.CreatedByTr.Visible = false;

            if (!Config.LargeForumTree)
            {
                // Currently creating a New Forum, and auto fill the Forum Sort Order + 1
                using (
                DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null))
                {
                    int sortOrder = 1;

                    try
                    {
                        DataRow highestRow = dt.Rows[dt.Rows.Count - 1];

                        sortOrder = highestRow["SortOrder"].ToType<int>() + sortOrder;
                    }
                    catch
                    {
                        sortOrder = 1;
                    }

                    this.SortOrder.Text = sortOrder.ToString();
                   
                }
            }

            return;
        }

      using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, forumId.Value))
      {
        DataRow row = dt.Rows[0];
        var flags = new ForumFlags(row["Flags"]);
        this.Name.Text = (string)row["Name"];
        this.Description.Text = row["Description"].ToString();
        this.SortOrder.Text = row["SortOrder"].ToString();
        this.HideNoAccess.Checked = flags.IsHidden;
        this.Locked.Checked = flags.IsLocked;
        this.IsTest.Checked = flags.IsTest;
        this.IsUserForum.Checked = row["IsUserForum"].ToType<bool>();
        this.ForumNameTitle.Text = this.Name.Text;
        this.Moderated.Checked = flags.IsModerated;
        this.Styles.Text = row["Styles"].ToString();
        this.CanHavePersForums.Checked = row["CanHavePersForums"].ToType<bool>();

        if (!Config.LargeForumTree)
        {
            this.CategoryList.SelectedValue = row["CategoryID"].ToString();
        }

        this.UserID.Value = row["CreatedByUserID"].ToString();
        this.UserLinkCreated.UserID = row["CreatedByUserID"].ToType<int>();
        this.Preview.Src = "{0}images/spacer.gif".FormatWith(YafForumInfo.ForumClientFileRoot);

        ListItem item = this.ForumImages.Items.FindByText(row["ImageURL"].ToString());
        if (item != null)
        {
          item.Selected = true;
          this.Preview.Src = "{0}{2}/{1}".FormatWith(
            YafForumInfo.ForumClientFileRoot, row["ImageURL"], YafBoardFolders.Current.Forums); // path corrected
        }

        // populate parent forums list with forums according to selected category
        this.BindParentList();

        if (!Config.LargeForumTree)
        {
            if (!row.IsNull("ParentID"))
            {
                this.ParentList.SelectedValue = row["ParentID"].ToString();
            }
        }

        if (!row.IsNull("ThemeURL"))
        {
          this.ThemeList.SelectedValue = row["ThemeURL"].ToString();
        }

        this.remoteurl.Text = row["RemoteURL"].ToString();
      }

      this.NewGroupRow.Visible = false;
    }

    /// <summary>
    /// The set drop down index.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void SetDropDownIndex([NotNull] object sender, [NotNull] EventArgs e)
    {
      try
      {
        var list = (DropDownList)sender;
        list.Items.FindByValue(list.Attributes["value"]).Selected = true;
      }
      catch (Exception)
      {
      }
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      var forumId = this.GetQueryStringForumIdAsInt("fa") ?? this.GetQueryStringForumIdAsInt("copy");

      if (!Config.LargeForumTree)
      {
          this.CategoryList.DataSource = CommonDb.category_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null);
          this.CategoryList.DataBind();
      }

      if (forumId.HasValue)
      {         
    
          using (var ftype = CommonDb.forum_list(PageContext.PageModuleID, PageContext.PageBoardID, forumId))
          {
              UserForum = ftype.Rows[0]["IsUserForum"].ToType<bool>();
              UsrId = ftype.Rows[0]["CreatedByUserID"].ToType<int>();
          }

          bool userGroups = false;
          bool adminGroups = false;
          bool commonGroups = false;

          if (UserForum)
          {
              userGroups = true;
              adminGroups = false;
              commonGroups = !this.Get<YafBoardSettings>().AllowPersonalGroupsOnlyForPersonalForums;

              accessMaskListId = CommonDb.accessmask_pforumlist(
      mid: PageContext.PageModuleID,
      boardId: this.PageContext.PageBoardID,
      accessMaskId: null,
      excludeFlags: 0,
      pageUserId: UsrId,
      isUserMask: true,
      isCommonMask: !this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums);
              
          }
          else
          {
              userGroups = false;
              adminGroups = true;
              commonGroups = true;

              accessMaskListId = CommonDb.accessmask_aforumlist(
      mid: PageContext.PageModuleID,
      boardId: this.PageContext.PageBoardID,
      accessMaskId: null,
      excludeFlags: 0,
      pageUserId: null,
      isAdminMask: true,
      isCommonMask: true);
          }

          this.AccessList.DataSource = CommonDb.forumaccess_list(
                this.PageContext.PageModuleID,
                forumId.Value,
                UsrId,
                userGroups,
                commonGroups,
                adminGroups);       


        if (accessMaskListId.Rows.Count == 0 && this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums)
        {
            if (PageContext.PersonalAccessMasksNumber <= 0 && PageContext.UsrPersonalMasks > 0)
            {
                YafBuildLink.Redirect(ForumPages.personalaccessmask, "u={0}".FormatWith(PageContext.PageUserID));
            }

            if (PageContext.UsrPersonalMasks <= 0)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.ForumAdminShouldSetPersonalMasksOrEnableCommonMasks);
            }

            CommonDb.eventlog_create(PageContext.PageModuleID, PageContext.PageUserID, this, "Bad logic in the editpersonalforum masks.", EventLogTypes.Information);
        }

        this.AccessList.DataBind();      
      }

      // Load forum's combo
      this.BindParentList();

      // Load forum's themes
      var listheader = new ListItem { Text = this.GetText("ADMIN_EDITFORUM", "CHOOSE_THEME"), Value = string.Empty };
     // this is a new forum, we assume that this is a common forum. 
        if (accessMaskListId == null)
      { 
          accessMaskListId = CommonDb.accessmask_aforumlist(
      mid: PageContext.PageModuleID,
      boardId: this.PageContext.PageBoardID,
      accessMaskId: null,
      excludeFlags: 0,
      pageUserId: null,
      isAdminMask: true,
      isCommonMask: true);

      // we can't create a personal forum here, we can only edit it;
      this.IsUserForum.Checked = false;
      tr_isuserforum.Visible = false;

      }
      this.AccessMaskID.DataBind();

      this.ThemeList.DataSource = StaticDataHelper.Themes();
      this.ThemeList.DataTextField = "Theme";
      this.ThemeList.DataValueField = "FileName";
      this.ThemeList.DataBind();
      this.ThemeList.Items.Insert(0, listheader);
    }

    /// <summary>
    /// The bind parent list.
    /// </summary>
    private void BindParentList()
    {

        if (!Config.LargeForumTree)
        {
            this.ParentList.DataSource = CommonDb.forum_listall_fromCat(PageContext.PageModuleID, this.PageContext.PageBoardID, this.CategoryList.SelectedValue, false);
            this.ParentList.DataValueField = "ForumID";
            this.ParentList.DataTextField = "Title";
            this.ParentList.DataBind();
        }
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
      YafBuildLink.Redirect(ForumPages.admin_forums);
    }

    /// <summary>
    /// The clear caches.
    /// </summary>
    private void ClearCaches()
    {
      // clear moderatorss cache
      this.Get<IDataCache>().Remove(Constants.Cache.ForumModerators);

      // clear category cache...
      this.Get<IDataCache>().Remove(Constants.Cache.ForumCategory);
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
    private void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
    {

        if (!Config.LargeForumTree)
        {
            if (this.CategoryList.SelectedValue.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_CATEGORY"));
                return;
            }
        }

        if (this.Name.Text.Trim().Length == 0)
        {
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_NAME_FORUM"));
            return;
        }

        if (!this.Get<YafBoardSettings>().ForumDescriptionCanBeNull && this.Description.Text.Trim().Length == 0)
        {
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_DESCRIPTION"));
            return;
        }

        if (!Config.LargeForumTree)
        {
            if (this.SortOrder.Text.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_VALUE"));
                return;
            }
        }

        short sortOrder = 0;

        if (!ValidationHelper.IsValidPosShort(this.SortOrder.Text.Trim()))
        {
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_POSITIVE_VALUE"));
            return;
        }

        if (!Config.LargeForumTree && !short.TryParse(this.SortOrder.Text.Trim(), out sortOrder))
        {
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_NUMBER"));
            return;
        }

        if (this.remoteurl.Text.IsSet())
        {
            // add http:// by default
            if (!Regex.IsMatch(this.remoteurl.Text.Trim(), @"^(http|https|ftp|ftps|git|svn|news)\://.*"))
            {
                this.remoteurl.Text = "http://" + this.remoteurl.Text.Trim();
            }

            if (!ValidationHelper.IsValidURL(this.remoteurl.Text))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_INVALID_URL"));
                return;
            }
        }

        // Forum
        // vzrus: it's stored in the DB as int
        int? forumId = this.GetQueryStringForumIdAsInt("fa");
        int? forumCopyId = this.GetQueryStringForumIdAsInt("copy");

        string categoryId = null;
        object parentID = null;

        if (!Config.LargeForumTree)
        {
            if (this.ParentList.SelectedValue.Length > 0)
            {
                parentID = this.ParentList.SelectedValue;
            }
        }
        else
        {
            if (forumId.HasValue 
                &&  this.Request.QueryString.GetFirstOrDefault("fa") != null 
                &&  this.Request.QueryString.GetFirstOrDefault("fa").Contains("_"))
            {
                    var parcedNode1 = TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault("fa"));
                    categoryId = parcedNode1.Item2.ToString();
                    using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, forumId.Value))
                    {
                        parentID = dt.Rows[0]["ParentID"];                     
                    }                
            } 
        }

        // parent selection check.
        if (parentID != null && parentID.ToString() == this.Request.QueryString.GetFirstOrDefault("fa"))
        {
            this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_PARENT_SELF"));
            return;
        }

        // The picked forum cannot be a child forum as it's a parent
        // If we update a forum ForumID > 0 
        if (forumId.HasValue && parentID != null)
        {
            int dependency = CommonDb.forum_save_parentschecker(PageContext.PageModuleID, forumId.Value, parentID);
            if (dependency > 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_CHILD_PARENT"));
                return;
            }
        }

        // inital access mask
        if (!forumId.HasValue && !forumCopyId.HasValue)
        {
            if (this.AccessMaskID.SelectedValue.Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_INITAL_MASK"));
                return;
            }
        }

        // duplicate name checking...
        if (!forumId.HasValue)
        {
            var forumList = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null).AsEnumerable();

            if (forumList.Any() && !this.Get<YafBoardSettings>().AllowForumsWithSameName &&
                forumList.Any(dr => dr.Field<string>("Name") == this.Name.Text.Trim()))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_FORUMNAME_EXISTS"));
                return;
            }
        }

        object themeUrl = null;

        if (this.ThemeList.SelectedValue.Length > 0)
        {
            themeUrl = this.ThemeList.SelectedValue;
        }

        int? adjacentForumId = null;
        int? adjacentForumPosition = null;
        string message;

        Tuple<int?, int?, int?> parcedNode = null;       

        if (!Config.LargeForumTree)
        {
            categoryId = this.CategoryList.SelectedValue;
        }
        else
        {
            if (this.Request.QueryString.GetFirstOrDefault("fa") != null)
            {
                if (this.Request.QueryString.GetFirstOrDefault("fa").Contains("_"))
                {
                    parcedNode = TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault("fa"));                
                    categoryId = parcedNode.Item2.ToString();
                    if (Config.LargeForumTree)
                    {
                        adjacentForumPosition = -1;
                    }
                }
            }
        }

        if (this.Request.QueryString.GetFirstOrDefault("before") != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault("before").Contains("_"))
            {
                parcedNode = TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault("before"));
                if (parcedNode.Item3.HasValue)
                {
                    using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, parcedNode.Item3))
                    {
                        parentID = dt.Rows[0]["ParentID"];
                    }

                    adjacentForumId = parcedNode.Item3;
                    categoryId = parcedNode.Item2.ToString();
                }    
              
                adjacentForumPosition = 1;
            }
        }

        if (this.Request.QueryString.GetFirstOrDefault("after") != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault("after").Contains("_"))
            {

               
                parcedNode = TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault("after"));
               
                if (parcedNode.Item3.HasValue)
                {
                    using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, parcedNode.Item3))
                    {
                        parentID = dt.Rows[0]["ParentID"];
                    }

                    adjacentForumId = parcedNode.Item3;
                    adjacentForumPosition = 2;                   
                }

                categoryId = parcedNode.Item2.ToString();
              
            }
        }

        if (this.Request.QueryString.GetFirstOrDefault("over") != null)
        {
            if (this.Request.QueryString.GetFirstOrDefault("over").Contains("_"))
            {
                parcedNode = TreeViewUtils.GetParcedTreeNodeId(this.Request.QueryString.GetFirstOrDefault("over"));
                if (parcedNode.Item3.HasValue)
                {
                    adjacentForumId = parcedNode.Item3;
                    adjacentForumPosition = 3;
                    parentID = adjacentForumId;
                }
                else
                {
                    // this is a category                  
                   
                }
                
                categoryId = parcedNode.Item2.ToString();            
            }

        }

        var newForumId = CommonDb.forum_save(PageContext.PageModuleID, forumId,
        categoryId,
        parentID,
        this.Name.Text.Trim(),
        this.Description.Text.Trim().IsSet() ? this.Description.Text.RemoveDoubleWhiteSpaces() : null,
        sortOrder,
        this.Locked.Checked,
        this.HideNoAccess.Checked,
        this.IsTest.Checked,
        this.Moderated.Checked,
        this.AccessMaskID.SelectedValue,
        IsNull(this.remoteurl.Text),
        themeUrl,
        this.ForumImages.SelectedIndex > 0 ? this.ForumImages.SelectedValue.Trim() : null,
        this.Styles.Text.RemoveDoubleWhiteSpaces(),
        false,
        (this.UserID.Value.IsSet() ? this.UserID.Value.ToType<int>() : PageContext.PageUserID),
        this.IsUserForum.Checked,
        this.CanHavePersForums.Checked,
        adjacentForumId,
        adjacentForumPosition);

        CommonDb.activeaccess_reset(PageContext.PageModuleID);

        // Access
        if (forumId.HasValue || forumCopyId.HasValue)
        {
            foreach (var item in this.AccessList.Items.OfType<RepeaterItem>())
            {
                int groupId = int.Parse(item.FindControlAs<Label>("GroupID").Text);

                CommonDb.forumaccess_save(PageContext.PageModuleID, newForumId, groupId, item.FindControlAs<DropDownList>("AccessmaskID").SelectedValue);
            }
        }

        this.ClearCaches();

        if (forumId.HasValue)
        {
            YafBuildLink.Redirect(ForumPages.admin_forums);
        }
        else
        {
            string s = string.Empty;
            string a = string.Empty;

            if (this.Request.QueryString.GetFirstOrDefault("before") != null)
            {
                a = "before";
                if (this.Request.QueryString.GetFirstOrDefault(a).Contains("_"))
                {
                    s = "{0}_{1}_{2}".FormatWith(TreeViewUtils.GetParcedTreeNodeId(
                        this.Request.QueryString.GetFirstOrDefault("before")).Item1, 
                        categoryId, 
                        newForumId);
                }
            }

            else if (this.Request.QueryString.GetFirstOrDefault("after") != null)
            {
                if (this.Request.QueryString.GetFirstOrDefault("after").Contains("_"))
                {
                    a = "after";
                    if (this.Request.QueryString.GetFirstOrDefault(a).Contains("_"))
                    {
                        s = "{0}_{1}_{2}".FormatWith(TreeViewUtils.GetParcedTreeNodeId(
                            this.Request.QueryString.GetFirstOrDefault("after")).Item1, 
                            categoryId, 
                            newForumId);
                    }
                }
            }

            else if (this.Request.QueryString.GetFirstOrDefault("over") != null)
            {
                if (this.Request.QueryString.GetFirstOrDefault("over").Contains("_"))
                {
                    a = "over";
                    if (this.Request.QueryString.GetFirstOrDefault(a).Contains("_"))
                    {
                        s = "{0}_{1}_{2}".FormatWith(TreeViewUtils.GetParcedTreeNodeId(
                            this.Request.QueryString.GetFirstOrDefault("over")).Item1, 
                            categoryId, newForumId);
                    }
                }
            }
            else
            {
               s = "{0}_{1}_{2}".FormatWith(PageContext.PageBoardID, categoryId, newForumId);               
            }

            YafBuildLink.Redirect(ForumPages.admin_editforum, "fa={0}".FormatWith(s));
        }
    }

      /// <summary>
      /// The reset session.
      /// </summary>
      private void ResetSession()
      {       
      }

    #endregion
  }
}