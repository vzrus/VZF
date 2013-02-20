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
    using YAF.Utils;
    using YAF.Utils.Helpers;
    
    #endregion

  /// <summary>
  /// Administrative Page for the editing of forum properties.
  /// </summary>
  public partial class editforum : AdminPage
  {
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
      ((DropDownList)sender).DataSource = CommonDb.accessmask_aforumlist(mid: PageContext.PageModuleID, boardId: this.PageContext.PageBoardID, accessMaskID: null, excludeFlags: 0, pageUserID: this.PageContext.PageUserID, isUserMask: false, isAdminMask: true);
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
    protected int? GetQueryStringAsInt([NotNull] string name)
    {
      int value;

      if (this.Request.QueryString.GetFirstOrDefault(name) != null &&
          int.TryParse(this.Request.QueryString.GetFirstOrDefault(name), out value))
      {
        return value;
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

      this.BindData();

      var forumId = this.GetQueryStringAsInt("f") ?? this.GetQueryStringAsInt("copy");

 



        if (!forumId.HasValue)
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

              return;
          }
      }

      using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, forumId.Value))
      {
        DataRow row = dt.Rows[0];
        var flags = new ForumFlags(row["Flags"]);
        this.Name.Text = (string)row["Name"];
        this.Description.Text = (string)row["Description"];
        this.SortOrder.Text = row["SortOrder"].ToString();
        this.HideNoAccess.Checked = flags.IsHidden;
        this.Locked.Checked = flags.IsLocked;
        this.IsTest.Checked = flags.IsTest;
        this.ForumNameTitle.Text = this.Name.Text;
        this.Moderated.Checked = flags.IsModerated;
        this.Styles.Text = row["Styles"].ToString();
        this.CanHavePersForums.Checked = row["CanHavePersForums"].ToType<bool>();
        this.CategoryList.SelectedValue = row["CategoryID"].ToString();

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

        if (!row.IsNull("ParentID"))
        {
          this.ParentList.SelectedValue = row["ParentID"].ToString();
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
      var forumId = this.GetQueryStringAsInt("f") ?? this.GetQueryStringAsInt("copy");

      this.CategoryList.DataSource = CommonDb.category_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null);
      this.CategoryList.DataBind();

      if (forumId.HasValue)
      {
        this.AccessList.DataSource = CommonDb.forumaccess_list(this.PageContext.PageModuleID, forumId.Value, null, false);
        this.AccessList.DataBind();
      }

      // Load forum's combo
      this.BindParentList();

      // Load forum's themes
      var listheader = new ListItem { Text = this.GetText("ADMIN_EDITFORUM", "CHOOSE_THEME"), Value = string.Empty };

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
      this.ParentList.DataSource = CommonDb.forum_listall_fromCat(PageContext.PageModuleID, this.PageContext.PageBoardID, this.CategoryList.SelectedValue, false);
      this.ParentList.DataValueField = "ForumID";
      this.ParentList.DataTextField = "Title";
      this.ParentList.DataBind();
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
      if (this.CategoryList.SelectedValue.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_CATEGORY"));
        return;
      }

      if (this.Name.Text.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_NAME_FORUM"));
        return;
      }

      if (this.Description.Text.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_DESCRIPTION"));
        return;
      }

      if (this.SortOrder.Text.Trim().Length == 0)
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_VALUE"));
        return;
      }

      short sortOrder;

      if (!ValidationHelper.IsValidPosShort(this.SortOrder.Text.Trim()))
      {
        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_POSITIVE_VALUE"));
        return;
      }

      if (!short.TryParse(this.SortOrder.Text.Trim(), out sortOrder))
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
      int? forumId = this.GetQueryStringAsInt("f");
      int? forumCopyId = this.GetQueryStringAsInt("copy");

      object parentID = null;

      if (this.ParentList.SelectedValue.Length > 0)
      {
        parentID = this.ParentList.SelectedValue;
      }

      // parent selection check.
      if (parentID != null && parentID.ToString() == this.Request.QueryString.GetFirstOrDefault("f"))
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

      var newForumId = CommonDb.forum_save(PageContext.PageModuleID, forumId, 
        this.CategoryList.SelectedValue, 
        parentID, 
        this.Name.Text.Trim(), 
        this.Description.Text.Trim(), 
        sortOrder, 
        this.Locked.Checked, 
        this.HideNoAccess.Checked, 
        this.IsTest.Checked, 
        this.Moderated.Checked, 
        this.AccessMaskID.SelectedValue, 
        IsNull(this.remoteurl.Text), 
        themeUrl, 
        this.ForumImages.SelectedIndex > 0 ? this.ForumImages.SelectedValue.Trim() : null, 
        this.Styles.Text, 
        false,
        PageContext.PageUserID,
        false,
        this.CanHavePersForums.Checked);

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
        YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}", newForumId);
      }
    }

      private string GetAncorNodeData(out int boardId, out int categoryId, out object parentId, out int sortOrder, out bool isNew)
      {
          boardId = PageContext.PageBoardID;
          categoryId = 0;
          sortOrder = 0;
          parentId = null;
          isNew = false;
          if (this.Request.QueryString.GetFirstOrDefault("after") != null)
          {
              int afterBoardId;
              int afterCategoryId;
              int afterForumId;
              TreeViewUtils.TreeNodeIdParser(
                  this.Request.QueryString.GetFirstOrDefault("after"),
                  out afterForumId,
                  out afterCategoryId,
                  out afterBoardId);

              // Insert after a forum
              if (afterForumId > 0)
              {
                  using (
                      DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, afterForumId))
                  {
                      sortOrder = 1;

                      if (dt.Rows.Count > 0)
                      {
                          sortOrder = dt.Rows[0]["SortOrder"].ToType<int>();
                          sortOrder = sortOrder + 1;
                      }
                      else
                      {
                          return "Incorrect parameters!"; 
                      }

                      this.SortOrder.Text = sortOrder.ToString(CultureInfo.InvariantCulture);
                      this.SortOrder.Enabled = false;
                      parentId = afterForumId;
                      return string.Empty;
                  }
              }

              // Insert after a category
              if (categoryId > 0)
              {
                  using (
                      DataTable dt = CommonDb.category_getadjacentforum(PageContext.PageModuleID, this.PageContext.PageBoardID,categoryId, PageContext.PageUserID,true))
                  {
                      sortOrder = 1;

                      if (dt.Rows.Count > 0)
                      {
                          sortOrder = dt.Rows[0]["SortOrder"].ToType<int>();
                          sortOrder = sortOrder + 1;
                      }

                      this.SortOrder.Text = sortOrder.ToString(CultureInfo.InvariantCulture);
                      this.SortOrder.Enabled = false;
                      return string.Empty;
                  }
              }
          }

          if (this.Request.QueryString.GetFirstOrDefault("before") != null)
          {
              int beforeBoardId;
              int beforeCategoryId;
              int beforeForumId;
              TreeViewUtils.TreeNodeIdParser(
                  this.Request.QueryString.GetFirstOrDefault("before"),
                  out beforeForumId,
                  out beforeCategoryId,
                  out beforeBoardId);

              // Insert after a forum
              if (beforeForumId > 0)
              {
                  using (
                      var dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, beforeForumId))
                  {
                      sortOrder = 1;

                      if (dt.Rows.Count > 0)
                      {
                          sortOrder = dt.Rows[0]["SortOrder"].ToType<int>();
                          if (sortOrder > 0)
                          {
                              sortOrder = sortOrder + 1;
                          }
                          else
                          {
                              sortOrder = 0;
                          }
                      }

                      this.SortOrder.Text = sortOrder.ToString(CultureInfo.InvariantCulture);
                      this.SortOrder.Enabled = false;
                      return string.Empty;  
                  }
              }

              // Insert after a category
              if (beforeCategoryId > 0)
              {
                  using (
                      DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, beforeCategoryId))
                  {
                      sortOrder = 1;

                      if (dt.Rows.Count > 0)
                      {
                          sortOrder = dt.Rows[0]["SortOrder"].ToType<int>();
                          sortOrder = sortOrder + 1;
                      }

                      this.SortOrder.Text = sortOrder.ToString(CultureInfo.InvariantCulture);
                      this.SortOrder.Enabled = false;
                      return string.Empty ;
                  }
              }
          }

          if (this.Request.QueryString.GetFirstOrDefault("addchild") != null)
          {
              int addchildBoardId;
              int addchildCategoryId;
              int addchildForumId;
              TreeViewUtils.TreeNodeIdParser(
                  this.Request.QueryString.GetFirstOrDefault("addchild"),
                  out addchildForumId,
                  out addchildCategoryId,
                  out addchildBoardId);
          }

          if (this.Request.QueryString.GetFirstOrDefault("new") != null)
          {
              isNew = this.Request.QueryString.GetFirstOrDefault("new") == "1";
          }

          return null;
      }

      /// <summary>
      /// The split node to objects.
      /// </summary>
      /// <param name="mode">
      /// The mode.
      /// </param>
      private void SplitNodeToObjects(string mode)
      {
          var nodeIds = this.Get<IYafSession>().ForumTreeChangerActiveNode;
          string targetIds = this.Get<IYafSession>().ForumTreeChangerActiveTargetNode;

          if (nodeIds.IsNotSet())
          {
              this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
          }

          int boardId;
          int categoryId;
          int forumId;

          int boardIdTarget;
          int categoryIdTarget;
          int forumIdTarget;

          string addnew = null;
          if (this.Get<IYafSession>().ForumAdminTreeAddForum == 1)
          {
              addnew = "&new=1";
              targetIds = nodeIds;
          }

          TreeViewUtils.TreeNodeIdParser(nodeIds, out forumId, out categoryId, out boardId);
          TreeViewUtils.TreeNodeIdParser(targetIds, out forumIdTarget, out categoryIdTarget, out boardIdTarget);


          switch (mode)
          {
              case "delete":
                  if (forumId > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_deleteforum, "f={0}", forumId);
                  }

                  if (categoryId > 0)
                  {
                      if (CommonDb.category_delete(PageContext.PageModuleID, categoryId))
                      {
                          this.ResetSession();
                          this.BindData();
                          this.ClearCaches();
                      }
                      else
                      {
                          this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MSG_NOT_DELETE"));
                      }
                      return;
                  }

                  if (boardId > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_boards);
                  }

                  break;
              case "add":
                  if (forumId > 0)
                  {
                      this.Get<IYafSession>().ForumAdminTreeAddForum = 1;
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (categoryId > 0)
                  {
                      this.Get<IYafSession>().ForumAdminTreeAddForum = 1;
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (boardId > 0)
                  {
                      this.PageContext.AddLoadMessage(this.GetText("ADMIN_FORUMS", "MESSAGE_CANTADDTOBOARD"));
                  }

                  break;

              case "copy":
                  if (forumId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_editforum, "{0}={1}", mode, forumId);
                  }

                  if (categoryId > 0 || boardId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums);
                  }

                  break;
              case "edit":
                  if (forumId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_editforum, "f={0}", forumId);
                  }

                  if (categoryId > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_editcategory, "c={0}", categoryId);
                  }

                  if (boardId > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  break;
              case "movebefore":
                  if (forumIdTarget > 0)
                  {
                      // move forumId before a forum with path targetIds
                      this.ResetSession();
                      YafBuildLink.Redirect(
                          ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, targetIds, mode, addnew);
                  }

                  // a category was selected as a destination
                  if (categoryIdTarget > 0)
                  {
                      this.ResetSession();
                      if (forumId > 0)
                      {
                          // we can't move forum before a category, return back
                          YafBuildLink.Redirect(ForumPages.admin_forums);
                      }
                      else
                      {
                          // a category was selected as a target and a category as a destination 
                          // we change a category sort order here
                          YafBuildLink.Redirect(
                          ForumPages.admin_editcategory, "c={0}&{1}={2}", categoryIdTarget, mode, nodeIds);
                      }
                  }

                  if (boardIdTarget > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  if (forumId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (categoryId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (boardId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  break;
              case "moveafter":
                  if (forumIdTarget > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(
                          ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                  }

                  if (categoryIdTarget > 0)
                  {
                      this.ResetSession();
                      if (forumId > 0)
                      {
                          YafBuildLink.Redirect(
                             ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                      }
                      else
                      {
                          // a category was selected as a target and a category as a destination 
                          // we change a category sort order here
                          YafBuildLink.Redirect(
                          ForumPages.admin_editcategory, "c={0}&{1}={2}", categoryIdTarget, mode, nodeIds);
                      }
                  }

                  if (boardIdTarget > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  if (forumId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (categoryId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (boardId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  break;

              case "addchildren":
                  if (forumIdTarget > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(
                          ForumPages.admin_editforum, "f={0}&{1}={2}{3}", forumId, mode, targetIds, addnew);
                  }

                  if (categoryIdTarget > 0)
                  {
                      this.ResetSession();
                      if (forumId > 0)
                      {
                          YafBuildLink.Redirect(
                             ForumPages.admin_editforum, "c={0}&{1}={2}&child=1", forumId, mode, targetIds);
                      }
                      else
                      {
                          // we can't add a category as a child of other category
                          YafBuildLink.Redirect(ForumPages.admin_forums);
                      }
                  }

                  if (boardIdTarget > 0)
                  {
                      this.ResetSession();
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  if (forumId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (categoryId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_forums, "node={0}&action={1}", nodeIds, mode);
                  }

                  if (boardId > 0)
                  {
                      YafBuildLink.Redirect(ForumPages.admin_editboard);
                  }

                  break;
          }

      }

      /// <summary>
      /// The reset session.
      /// </summary>
      private void ResetSession()
      {
          this.Get<IYafSession>().ForumTreeChangerActiveNode = null;
          this.Get<IYafSession>().ForumTreeChangerActiveTargetNode = null;
          this.Get<IYafSession>().ForumAdminTreeAddForum = null;
      }

    #endregion

      protected void MoveForumAfterBtn_Click(object sender, EventArgs e)
      {
          throw new NotImplementedException();
      }

      protected void AddChildrenTo_Click(object sender, EventArgs e)
      {
          throw new NotImplementedException();
      }
  }
}