// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="editpersonalforum.ascx.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2016 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The edit forum.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace YAF.pages
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI.WebControls;
    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;
    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    public partial class editpersonalforum : ForumPage
    {
        private bool UserForum { get; set; }
        private int? UsrId { get; set; }

        private DataTable accessMaskListId;
 
        #region Public Methods

        /// <summary>
        /// Gets or sets the ftitle.
        /// </summary>
        protected string ftitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can have forums as subforums.
        /// </summary>
        protected bool canHaveForumsAsSubforums { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can have forums in categories.
        /// </summary>
        protected bool canHaveForumsInCategories { get; set; } 

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

                /* this.ForumImages.DataSource = dt;
                 this.ForumImages.DataValueField = "FileName";
                 this.ForumImages.DataTextField = "Description";
                 this.ForumImages.DataBind(); */
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
            this.canHaveForumsAsSubforums = this.Get<YafBoardSettings>().AllowPersonalForumsAsSubForums;
            this.canHaveForumsInCategories = this.Get<YafBoardSettings>().AllowPersonalForumsInCategories;  

            if (this.IsPostBack)
            {
                return;
            }
            

            // A new forum case
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("fa") == null)
            {
                if (PageContext.PersonalForumsNumber >= PageContext.UsrPersonalForums)
                {
                    YafBuildLink.AccessDenied();
                }

                if (!this.canHaveForumsAsSubforums && !this.canHaveForumsInCategories)
                {
                    YafBuildLink.RedirectInfoPage(InfoMessage.HostAdminShouldSetAllowedPersonalForums);
                }
            }

            // the calling user is not the owner
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u") == null || !ValidationHelper.IsValidInt(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u")) || this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>() != PageContext.PageUserID)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("fa") != null)
            {
                if (!ValidationHelper.IsValidInt(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("fa")))
                {
                    YafBuildLink.AccessDenied();
                }

                // checking if the personal forum of the page user exists.
                DataTable dt = CommonDb.forum_byuserlist(
                    PageContext.PageModuleID,
                    PageContext.PageBoardID,
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("fa").ToType<int>(),
                    this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("u").ToType<int>(),
                    true);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ftitle = dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    YafBuildLink.AccessDenied();
                }
            }
           
            accessMaskListId = CommonDb.accessmask_pforumlist(
              mid: PageContext.PageModuleID,
              boardId: this.PageContext.PageBoardID,
              accessMaskId: null,
              excludeFlags: 0,
              pageUserId: this.PageContext.PageUserID,
              isUserMask: true,             
              isCommonMask: !this.Get<YafBoardSettings>().AllowPersonalMasksOnlyForPersonalForums);
           
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

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // user profile
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName, YafBuildLink.GetLink(ForumPages.cp_profile));

            // personalforum page
            this.PageLinks.AddLink(this.GetText("PERSONALFORUM", "TITLE"), YafBuildLink.GetLink(ForumPages.personalforum, "u={0}".FormatWith(PageContext.PageUserID)));

            // this page link
            this.PageLinks.AddLink(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("fa") == null ? this.GetText("ADMIN_FORUMS", "NEW_FORUM") : this.HtmlEncode(ftitle), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.Get<YafBoardSettings>().EnableDisplayName ? this.PageContext.CurrentUserData.DisplayName : this.PageContext.PageUserName,
                ftitle.IsSet() ? this.HtmlEncode(ftitle) : this.GetText("PERSONALFORUM", "TITLE"));

            // Populate Forum Images Table
            this.CreateImagesDataTable();

            /*  this.ForumImages.Attributes["onchange"] =
                "getElementById('{1}').src='{0}{2}/' + this.value".FormatWith(
                  YafForumInfo.ForumClientFileRoot, this.Preview.ClientID, YafBoardFolders.Current.Forums); */

            this.BindData();

            var forumId = this.GetQueryStringAsInt("fa") ?? this.GetQueryStringAsInt("copy");


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
            using (
                   DataTable dt = CommonDb.forum_list(
                       PageContext.PageModuleID,
                       this.PageContext.PageBoardID,
                       forumId.Value))
            {        
                    DataRow row = dt.Rows[0];
                    var flags = new ForumFlags(row["Flags"]);
                    this.Name.Text = (string)row["Name"];
                    this.Description.Text = row["Description"].ToString();
                    this.SortOrder.Text = row["SortOrder"].ToString();
                    this.HideNoAccess.Checked = flags.IsHidden;
                    this.Locked.Checked = flags.IsLocked;
                    this.IsTest.Checked = flags.IsTest;
                    this.ForumNameTitle.Text = this.Name.Text;
                    this.Moderated.Checked = flags.IsModerated;
                    this.Styles.Text = row["Styles"].ToString();
                    this.Preview.Src = "{0}images/spacer.gif".FormatWith(YafForumInfo.ForumClientFileRoot);
                    this.CanHavePersForums.Checked = row["CanHavePersForums"].ToType<bool>();
                    this.remoteurl.Text = row["RemoteURL"].ToString();

                    if (this.canHaveForumsInCategories && this.CategoryList.DataSource != null)
                    {
                        this.CategoryAllowed.SelectedValue = row["CategoryID"].ToString();
                    }

                    if (this.canHaveForumsAsSubforums)
                    {
                        this.CategoryList.SelectedValue = row["CategoryID"].ToString();
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
                    }          

                    /* ListItem item = this.ForumImages.Items.FindByText(row["ImageURL"].ToString());
                if (item != null)
                {
                    item.Selected = true;
                    this.Preview.Src = "{0}{2}/{1}".FormatWith(
                      YafForumInfo.ForumClientFileRoot, row["ImageURL"], YafBoardFolders.Current.Forums); // path corrected
                } */       
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
            var forumId = this.GetQueryStringAsInt("fa") ?? this.GetQueryStringAsInt("copy");

            DataTable dataCat = CommonDb.category_pfaccesslist(PageContext.PageModuleID, this.PageContext.PageBoardID, null);
            DataTable dataCatNew = dataCat.Clone();
            DataTable dataForumNew = dataCat.Clone();
            foreach (DataRow row in dataCat.Rows)
            {
                if (row["CanHavePersForums"].ToType<bool>())
                {
                    DataRow newRow = dataCatNew.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    dataCatNew.Rows.Add(newRow);
                }
                if (row["HasForumsForPersForums"].ToType<bool>())
                {
                    DataRow newRow = dataForumNew.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    dataForumNew.Rows.Add(newRow);
                }               
            }

            dataCatNew.AcceptChanges();
            dataForumNew.AcceptChanges();

            if (dataCatNew.Rows.Count <= 0)
            {
                YafBuildLink.RedirectInfoPage(InfoMessage.HostAdminShouldSetAllowedPersonalForums);
            }

            if (this.canHaveForumsInCategories && dataCatNew.Rows.Count > 0)
            {
                this.tr_categoriesallowed.Visible = true;

                this.CategoryAllowed.DataSource = dataCatNew;             
                this.CategoryAllowed.DataValueField = "CategoryID";
                this.CategoryAllowed.DataTextField = "Name";
                this.CategoryAllowed.DataBind();
            }

            if (this.canHaveForumsAsSubforums && dataForumNew.Rows.Count > 0)
            {
                this.tr_categoriesforforumsallowed.Visible = true;
                this.tr_forumsallowed.Visible = true;

                this.CategoryList.DataSource = dataForumNew;
                this.CategoryList.DataValueField = "CategoryID";
                this.CategoryList.DataTextField = "Name";              
                this.CategoryList.DataBind();

                // Load forum's combo
                this.BindParentList();
            }

            // forum already exists
            if (forumId.HasValue)
            {
                this.AccessList.DataSource = CommonDb.forumaccess_list(
                        this.PageContext.PageModuleID,
                        forumId.Value,
                        PageContext.PageUserID,
                        true,
                        !this.Get<YafBoardSettings>().AllowPersonalGroupsOnlyForPersonalForums,
                        false);
             
                this.AccessList.DataBind();
            }           

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
            if (this.canHaveForumsAsSubforums && this.CategoryList.SelectedValue.IsSet())
            {
                DataTable dataCat = CommonDb.forum_listall_fromCat(
                    PageContext.PageModuleID,
                    this.PageContext.PageBoardID,
                    this.CategoryList.SelectedValue,
                    false);
                DataTable dataCatNew = dataCat.Clone();
                foreach (DataRow row in dataCat.Rows)
                {
                    if (row["CanHavePersForums"].ToType<bool>() || row["ForumID"].ToString() == string.Empty)
                    {
                        DataRow newRow = dataCatNew.NewRow();
                        newRow.ItemArray = row.ItemArray;
                        dataCatNew.Rows.Add(newRow);
                    }
                }

                dataCatNew.AcceptChanges();
                if (dataCatNew.Rows.Count <= 0)
                {
                    YafBuildLink.RedirectInfoPage(InfoMessage.HostAdminShouldSetAllowedPersonalForums);
                }

                this.ParentList.DataSource = dataCatNew;
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
            YafBuildLink.Redirect(ForumPages.personalforum, "u={0}".FormatWith(PageContext.PageUserID));
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
            if ((this.canHaveForumsAsSubforums && this.CategoryList.SelectedValue.Trim().Length == 0) && (this.canHaveForumsInCategories && this.CategoryAllowed.SelectedValue.Trim().Length == 0))
            {
                this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_CATEGORY"));
                return;
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
            int? forumId = this.GetQueryStringAsInt("fa");
            int? forumCopyId = this.GetQueryStringAsInt("copy");

            object categoryId = null;
            object parentID = null;
            if (this.canHaveForumsAsSubforums && this.CategoryList.SelectedValue.Trim().Length != 0)
            {
                categoryId = this.CategoryList.SelectedValue.Trim();
                if (this.ParentList.SelectedValue.Length > 0)
                {
                    parentID = this.ParentList.SelectedValue;
                }

                // parent selection check.
                if (parentID != null && parentID.ToString() == this.Request.QueryString.GetFirstOrDefault("fa"))
                {
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITFORUM", "MSG_PARENT_SELF"));
                    return;
                }
            }

            if (this.canHaveForumsInCategories && this.CategoryAllowed.SelectedValue.Trim().Length != 0)
            {
                categoryId = this.CategoryAllowed.SelectedValue.Trim();
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

            string errorMessage;
            var newForumId = ForumSaveTask.Start(
                PageContext.PageModuleID,
                forumId,
                categoryId,
                parentID,
              this.Name.Text.Trim(),
              this.Description.Text.Trim().IsSet() ? this.Description.Text.Trim() : null,
              sortOrder,
              this.Locked.Checked,
              this.HideNoAccess.Checked,
              this.IsTest.Checked,
              this.Moderated.Checked,
              this.AccessMaskID.SelectedValue,
              IsNull(this.remoteurl.Text),
              themeUrl,
                // this.ForumImages.SelectedIndex > 0 ? this.ForumImages.SelectedValue.Trim() : 
              null,
              this.Styles.Text,
              false,
              PageContext.PageUserID,
              true,
              this.CanHavePersForums.Checked,
              null,
              null,
              out errorMessage);

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

            // Clearing cache with old permissions data...
            this.ClearCaches();

            this.Get<IDataCache>().Remove(k => k.StartsWith(Constants.Cache.ActiveUserLazyData.FormatWith(string.Empty)));
            if (forumId.HasValue)
            {
                YafBuildLink.Redirect(ForumPages.personalforum, "fa={0}&u={1}".FormatWith(forumId, this.PageContext.PageUserID));
            }
            else
            {
                YafBuildLink.Redirect(ForumPages.editpersonalforum, "fa={0}&u={1}", newForumId, this.PageContext.PageUserID);
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

                var key = TreeViewUtils.GetParcedTreeNode(
                    this.Request.QueryString.GetFirstOrDefault("after"));

                // Insert after a forum
                if (key.ForumId > 0)
                {
                    using (
                        DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, key.ForumId))
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
                        parentId = key.ForumId;
                        return string.Empty;
                    }
                }

                // Insert after a category
                if (categoryId > 0)
                {
                    using (
                        DataTable dt = CommonDb.category_getadjacentforum(PageContext.PageModuleID, this.PageContext.PageBoardID, categoryId, PageContext.PageUserID, true))
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

                var key = TreeViewUtils.GetParcedTreeNode(
                    this.Request.QueryString.GetFirstOrDefault("before"));

                // Insert after a forum
                if (key.ForumId > 0)
                {
                    using (
                        var dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, key.ForumId))
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
                if (key.CategoryId > 0)
                {
                    using (
                        DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, key.CategoryId))
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

            if (this.Request.QueryString.GetFirstOrDefault("addchild") != null)
            {
               var d = TreeViewUtils.GetParcedTreeNode(
                    this.Request.QueryString.GetFirstOrDefault("addchild"));
            }

            if (this.Request.QueryString.GetFirstOrDefault("new") != null)
            {
                isNew = this.Request.QueryString.GetFirstOrDefault("new") == "1";
            }

            return null;
        }
        #endregion
    }
}