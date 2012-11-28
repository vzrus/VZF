/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
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

using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using YAF.Utilities;
using YAF.Utils.Helpers;

namespace YAF.Pages.Admin
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
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The Admin Edit NNTP Forum Page.
    /// </summary>
    public partial class editnntpforum : AdminPage
    {
        private int? forumId;
        private string forumPath;
        private string forumName;
        private int? categoryId;
 
        #region Methods

        /// <summary>
        /// Cancel Import and Return Back to Previous Page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_nntpforums);
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
                this.PageLinks.AddLink(
                    this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
                this.PageLinks.AddLink(
                    this.GetText("ADMIN_NNTPFORUMS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_nntpforums));
                this.PageLinks.AddLink(this.GetText("ADMIN_EDITNNTPFORUM", "TITLE"), string.Empty);

                this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                    this.GetText("ADMIN_ADMIN", "Administration"),
                    this.GetText("ADMIN_NNTPFORUMS", "TITLE"),
                    this.GetText("ADMIN_EDITNNTPFORUM", "TITLE"));

                this.Save.Text = this.GetText("COMMON", "SAVE");
                this.Cancel.Text = this.GetText("COMMON", "CANCEL");
            }

            DataTable dt = null;
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("s") != null)
            {
                dt = CommonDb.nntpforum_list(PageContext.PageModuleID, this.PageContext.PageBoardID,
                                                       null,
                                                       this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("s"),
                                                       DBNull.Value);
                if (dt.Rows.Count > 0)
                {
                    
                    forumId = dt.Rows[0]["ForumID"].ToType<int>();
                    forumName = dt.Rows[0]["ForumName"].ToString();
                    categoryId = dt.Rows[0]["CategoryID"].ToType<int>();
                    forumPath = "/{0}_{1}".FormatWith(PageContext.PageBoardID,categoryId);
                    var dtp = CommonDb.forum_listpath(PageContext.PageModuleID, forumId);
                    foreach (DataRow rowp in dtp.Rows)
                    {
                        forumPath = forumPath + "/{0}_{1}_{2}".FormatWith(PageContext.PageBoardID,categoryId,rowp["ForumID"]);
                    }
                }
            }

              
            this.BindData();

            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("s") == null || dt == null || dt.Rows.Count == 0 )
            {
                return;
            }
 
                DataRow row = dt.Rows[0];
                this.NntpServerID.Items.FindByValue(row["NntpServerID"].ToString()).Selected = true;
                this.GroupName.Text = row["GroupName"].ToString();
                this.Active.Checked = (bool)row["Active"];
                this.DateCutOff.Text = row["DateCutOff"].ToString();
                if (!Config.LargeForumTree)
                {
                    this.FindControlRecursiveAs<DropDownList>("ForumID").Items.FindByValue(row["ForumID"].ToString()).Selected = true;
                }
           
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
            if (this.GroupName.Text.Trim().IsNotSet())
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_EDITNNTPFORUM", "MSG_VALID_GROUP"), MessageTypes.Warning);
                return;
            }

            object nntpForumId = null;
            int selectedForum = 0; 
            if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("s") != null)
            {
                nntpForumId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("s");
            }
            if (!Config.LargeForumTree)
            {

                if (this.FindControlRecursiveAs<DropDownList>("ForumID").SelectedValue.ToType<int>() <= 0)
                {
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                    return;
                }
            }
            else
            {

                string val = this.Get<IYafSession>().NntpTreeActiveNode;

                if (val.IsSet())
                {
                    string[] valArr = val.Split('_');
                    if (valArr.Length == 2)
                    {
                        this.PageContext.AddLoadMessage("You should select a forum, not a category");
                       // this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                        return;  
                    }
                    else if (valArr.Length == 3)
                    {
                        selectedForum = valArr[2].ToType<int>();
                    }
                    else
                    {
                        this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                        return;
                    }
                   
                }
                if (selectedForum == 0)
                {
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_EDITNNTPFORUM", "MSG_SELECT_FORUM"));
                    return;
                }
            }

            DateTime dateCutOff;

            if (!DateTime.TryParse(this.DateCutOff.Text, out dateCutOff))
            {
                dateCutOff = DateTime.MinValue;
            }

            CommonDb.nntpforum_save(PageContext.PageModuleID, nntpForumId,
                this.NntpServerID.SelectedValue,
                this.GroupName.Text,
                !Config.LargeForumTree ? this.FindControlRecursiveAs<DropDownList>("ForumID").SelectedValue : selectedForum.ToString(),
                this.Active.Checked,
                dateCutOff == DateTime.MinValue ? null : (DateTime?)dateCutOff);
            this.Get<IYafSession>().NntpTreeActiveNode = null;
            YafBuildLink.Redirect(ForumPages.admin_nntpforums);
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            this.NntpServerID.DataSource = CommonDb.nntpserver_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null);
            this.NntpServerID.DataValueField = "NntpServerID";
            this.NntpServerID.DataTextField = "Name";
            string activeNode = string.Empty;
            if (!Config.LargeForumTree)
            {
                this.phForums.Controls.Add(new DropDownList { ID = "ForumID" });
                this.phForums.DataBind();
                this.FindControlRecursiveAs<DropDownList>("ForumID").DataSource = CommonDb.forum_listall_sorted(PageContext.PageModuleID,
                                                                        this.PageContext.PageBoardID,
                                                                        this.PageContext.PageUserID);
                this.FindControlRecursiveAs<DropDownList>("ForumID").DataValueField = "ForumID";
                this.FindControlRecursiveAs<DropDownList>("ForumID").DataTextField = "Title";
            }
            else
            {
                string args = "&v=3";
                if (forumId > 0)
                {
                    args +=
                        "&active={0}".FormatWith("{0}_{1}_{2}".FormatWith(PageContext.PageBoardID, categoryId, forumId));
                    activeNode = forumPath;
                    this.divactive.InnerHtml = forumName;
                }
                this.divactive.Visible = true;
                YafContext.Current.PageElements.RegisterJsResourceInclude("dynatree", "js/jquery.dynatree.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("js/skin/ui.dynatree.css");
               
                YafContext.Current.PageElements.RegisterJsBlock("dynatreescr",
                   JavaScriptBlocks.DynatreeSelectSingleNodeLazyJS("tree",
                   PageContext.PageUserID, PageContext.PageBoardID, "echoActive", activeNode, args, "{0}resource.ashx?tjl".FormatWith(
                   YafForumInfo.ForumClientFileRoot),"&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetBasePath()))));
                YafContext.Current.PageElements.RegisterJsBlock("btnshowtree",
                  JavaScriptBlocks.ButtonShowForumInNNTPTree("tree","btnshowtree",activeNode));
            }
            this.DataBind();
        }

        #endregion
    }
}