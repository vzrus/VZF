// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="FancyTree.cs">
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
//   The FancyTree core functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Kernel
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Types.Objects;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Types.Flags;
    using VZF.Utils.Helpers;
    using YAF.Core.Tasks;

    #endregion

    /// <summary>
    /// This class wraps Dynatree JQuery plug-in functionality. 
    /// </summary>
    public static class FancyTree
    {
        /*    public static string MoveForum(int userId, string nodesInfo) 
        {
            string ret = string.Empty;

            // first - moved node id, second - parent nodeid, third - prevnode
            return ret;
        } */
        /*  public static string GetAllCommonAdminTree()
        {
            using (
                DataSet ds = CommonDb.ds_forumadmin(
                    YafContext.Current.PageModuleID,
                    YafContext.Current.PageBoardID,
                    YafContext.Current.PageUserID,
                    false))
            {
                var dd = ds.Tables[SqlDbAccess.GetVzfObjectName("Category", YafContext.Current.PageModuleID)];
                if (dd != null && dd.Rows.Count > 0)
                {
                    deleteImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "DELETE_SMALL_ICON");
                    copyImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "COPY_SMALL_ICON");
                    editImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "EDIT_SMALL_ICON");
                    var levels = new ConcurrentDictionary<int, int>();
                    var sb = new StringBuilder();

                    // a loop through categories
                    for (int j = 0; j < dd.Rows.Count; j++)
                    {
                        DataRow dr = dd.Rows[j];

                        // levels.AddOrUpdate((int)dr["CategoryID"], lvlCounter, (key, oldValue) => oldValue);
                        var childRows = dr.GetChildRows("FK_Forum_Category");
                        string title = dr["Name"].ToString();

                        title = title
                                + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>"
                                      .FormatWith(
                                          YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.admin_forums,
                                              true,
                                              "dc={0}".FormatWith(dr["CategoryID"]),
                                              YafContext.Current.PageUserID),
                                          "img",
                                          string.Empty,
                                          string.Empty,
                                          deleteImage);
                        title = title
                                + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>"
                                      .FormatWith(
                                          YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.admin_editcategory,
                                              true,
                                              "c={0}".FormatWith(dr["CategoryID"]),
                                              YafContext.Current.PageUserID),
                                          "img",
                                          string.Empty,
                                          string.Empty,
                                          editImage);

                        sb.AppendFormat(
                            "{{id: \"{0}.{1}\", title: \"{2}\", isFolder: true",
                            dr["BoardID"],
                            dr["CategoryID"],
                            dr["Name"] == null ? string.Empty : title);

                        // if an empty category, simply close it.
                        if (childRows.GetLength(0) <= 0)
                        {
                            sb.AppendFormat(",unselectable: true}}");
                            continue;
                        }

                        // here only children can be added
                        sb.Append(", children: [");

                        // level counter 
                        int closeBlockTimes = 0;

                        // a loop through forums
                        for (int i = 0; i < childRows.Length; i++)
                        {
                            // first sibling or last row
                            bool nodIsParent;

                            bool hasNextSibling;

                            if (i == 0 && childRows.Length == 1)
                            {
                                // The only node 
                                nodIsParent = false;
                                hasNextSibling = false;
                            }
                            else
                            {
                                // the last row 
                                if (i == childRows.Length - 1)
                                {
                                    nodIsParent = false;
                                    hasNextSibling = false;
                                }

                                else
                                {

                                    // this node has children
                                    nodIsParent = (childRows[i]["ForumID"].ToType<int>()
                                                   == childRows[i + 1]["ParentID"].ToType<int>());
                                    // this node has next sibling
                                    hasNextSibling = (childRows[i + 1]["ParentID"].ToType<int>()
                                                      == childRows[i]["ParentID"].ToType<int>());

                                }

                            }
                            string title_f = (childRows[i]["Name"] == null)
                                                 ? string.Empty
                                                 : childRows[i]["Name"].ToString();
                            title_f = title_f
                                      + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>"
                                            .FormatWith(
                                                YafBuildLink.GetLinkNotEscaped(
                                                    ForumPages.admin_deleteforum,
                                                    true,
                                                    "f={0}".FormatWith(childRows[i]["ForumID"]),
                                                    YafContext.Current.PageUserID),
                                                "img",
                                                string.Empty,
                                                string.Empty,
                                                deleteImage);
                            title_f = title_f
                                      + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>"
                                            .FormatWith(
                                                YafBuildLink.GetLinkNotEscaped(
                                                    ForumPages.admin_editforum,
                                                    true,
                                                    "copy={0}".FormatWith(childRows[i]["ForumID"]),
                                                    YafContext.Current.PageUserID),
                                                "img",
                                                string.Empty,
                                                string.Empty,
                                                copyImage);

                            title_f = title_f
                                      + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>"
                                            .FormatWith(
                                                YafBuildLink.GetLinkNotEscaped(
                                                    ForumPages.admin_editforum,
                                                    true,
                                                    "f={0}".FormatWith(childRows[i]["ForumID"]),
                                                    YafContext.Current.PageUserID),
                                                "img",
                                                string.Empty,
                                                string.Empty,
                                                editImage);
                            // write the node beginning
                            sb.AppendFormat(
                                "{{id: \"{0}.{1}.{2}\", title: \"{3}\"{4}",
                                dr["BoardID"],
                                dr["CategoryID"],
                                childRows[i]["ForumID"],
                                title_f,
                                nodIsParent ? ", isFolder: false" : string.Empty);

                            // previous is a folder, opening childrens block for next, else simply closing the block
                            if (nodIsParent)
                            {

                                levels.TryAdd((int)childRows[i]["ForumID"], closeBlockTimes);
                                // add an entry for children 
                                sb.Append(", children: [");
                                // increase level counter for the next node
                                closeBlockTimes++;

                            }
                            else if (hasNextSibling)
                            {
                                sb.Append("},");

                            }
                            else
                            {
                                // we are getting a level of its parent  
                                int levlParent;
                                levels.TryGetValue(childRows[i]["ParentID"].ToType<int>(), out levlParent);


                                // closeBlockTimes = closeBlockTimes - levlParent;

                                // close all nodes children until a new low level
                                for (int k = 0; k < closeBlockTimes - levlParent; k++)
                                {
                                    sb.Append("}]");
                                }
                                closeBlockTimes = levlParent;
                                // close all nodes children
                                sb.Append("}");

                                if (i != childRows.Length - 1)
                                {
                                    sb.Append(",");
                                }


                            }
                        }
                        // end of forum loop

                        // close category children and category block.
                        sb.Append("]}");

                        // this is not a last category, add comma between category blocks 
                        if (j != dd.Rows.Count - 1)
                        {
                            sb.Append(",");
                        }

                    }
                    // end of category loop.
                    return sb.ToString();
                }
            }
            return null;
        } */

        /// <summary>
        /// The move forum.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <summary>
        /// The get all common admin tree.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <summary>
        /// The get all user access admin tree.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /* public static string GetAllUserAccessAdminTree(int userId)
        {
            // sort it by name
            var valuesOrdered = UserForumAccess.GetUserAccessListSortedByForumName(userId);

            var sb = new StringBuilder();

            // level counter
            // int closeBlockTimes = 0;
            int currentCategory = 0;



            int p = 0;
            foreach (var amr in valuesOrdered)
            {
                var forumId = amr.ForumId;

                if (amr.CurrentLevel == 0)
                {
                    // this is category
                    currentCategory = forumId;
                }

                string path = forumId > 0
                                  ? YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}".FormatWith(forumId))
                                  : YafBuildLink.GetLinkNotEscaped(ForumPages.forum, true, "c={0}".FormatWith(forumId));

                string accessMaskString = string.Empty;
                accessMaskString = amr.AccessMaskData.Aggregate(
                    accessMaskString,
                    (current, c) => current + "," + c.AccessMaskName);

                // write the node 
                sb.AppendFormat(
                    "{{id: \"{0}.{1}.{2}\", title: \"{3}\"{4}",
                    YafContext.Current.PageBoardID,
                    currentCategory,
                    forumId,
                    (amr.ForumName.IsNotSet())
                        ? string.Empty
                        : @"<a href='{0}' target='_top' title='{1}'>{1} -- {2}</a>".FormatWith(
                            path,
                            amr.ForumName,
                            accessMaskString.Trim(',')),
                    (amr.CurrentLevel == 0) ? ", isFolder: true" : ", isFolder: false");

                // previous is a folder, opening childrens block for next, else simply closing the block
                if (amr.NextLevel > amr.CurrentLevel)
                {
                    // add an entry for children 
                    sb.Append(", children: [");
                    // increase level counter for the next node
                    // closeBlockTimes++;

                }
                else if (amr.NextLevel == amr.CurrentLevel)
                {
                    sb.Append("},");

                }
                else
                {

                    // close all nodes children until a new low level
                    for (int k = 0; k < amr.CurrentLevel - amr.NextLevel; k++)
                    {
                        sb.Append("}]");
                    }
                    // closeBlockTimes = amr.NextLevel;
                    // close all nodes children
                    sb.Append("}");

                    if (p != valuesOrdered.Count() - 1)
                    {
                        sb.Append(",");
                    }
                    p++;
                }
            }

            // end of forum loop
            return sb.ToString();



        } */

       /* public static string GetAllUserAccessJumpTreeLazy(int userId)
        {
            using (
                var childRows =
                    YafContext.Current.Get<IDataCache>()
                        .GetOrSet(
                            Constants.Cache.ForumJump.FormatWith(
                                YafContext.Current.User != null
                                    ? YafContext.Current.PageUserID.ToString()
                                    : UserMembershipHelper.GuestUserName),
                            () =>
                            CommonDb.forum_listall_sorted(
                                YafContext.Current.PageModuleID,
                                YafContext.Current.PageBoardID,
                                YafContext.Current.PageUserID),
                            TimeSpan.FromMinutes(5)))
            {
                if (childRows != null && childRows.Rows.Count > 0)
                {
                    var sb = new StringBuilder("[");

                    // level counter
                    int closeBlockTimes = 0;
                    int currentlevel;
                    int nextlevel;
                    int currentCategory = 0;
                    bool singleFirstOrAnyLastRow;

                    // a loop through forums
                    for (int i = 0; i < childRows.Rows.Count; i++)
                    {
                        // remembering current level as previous
                        string title = childRows.Rows[i]["Title"].ToString();
                        // @"<span id=""trvmts""><p>" + (childRows.Rows[i]["Title"] == null ? string.Empty : childRows.Rows[i]["Title"].ToString()) + "< /br>&nbsp;&nbsp;" +
                        //        childRows.Rows[i]["Description"] + @"<p></span>";
                        // string title = childRows.Rows[i]["Title"].ToString();
                        var forumId = childRows.Rows[i]["ForumID"].ToType<int>();

                        currentlevel = UserForumAccess.DashCounter(title);
                        singleFirstOrAnyLastRow = (i == 0 && childRows.Rows.Count == 1) || i == childRows.Rows.Count - 1;
                        nextlevel = singleFirstOrAnyLastRow
                                        ? 0
                                        : UserForumAccess.DashCounter(childRows.Rows[i + 1]["Title"].ToString().Trim());

                        if (currentlevel == 0)
                        {
                            // this is category
                            currentCategory = forumId;
                        }

                        string path = forumId > 0
                                          ? YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.topics,
                                              "f={0}".FormatWith(forumId))
                                          : YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.forum,
                                              true,
                                              "c={0}".FormatWith(-forumId));

                        // write the node beginning
                        sb.AppendFormat(
                            "{{\"id\": \"{0}.{1}.{2}\", \"title\": \"{3}\"{4}",
                            YafContext.Current.PageBoardID,
                            currentCategory,
                            forumId,
                            title.IsNotSet()
                                ? string.Empty
                                : @"<a href='{0}' target='_top' alt='{1}'>{1}</a>".FormatWith(
                                    path.Replace("resource.ashx", "default.aspx"),
                                    title),
                            (currentlevel == 0) ? ", \"folder\": \"true\"" : ", \"folder\": \"false\"");

                        // previous is a folder, opening childrens block for next, else simply closing the block
                        if (nextlevel > currentlevel)
                        {
                            // add an entry for children 
                            sb.Append(", \"children\": [");

                            // increase level counter for the next node
                            closeBlockTimes++;

                        }
                        else if (nextlevel == currentlevel)
                        {
                            sb.Append("},");

                        }
                        else
                        {

                            // close all nodes children until a new low level
                            for (int k = 0; k < currentlevel - nextlevel; k++)
                            {
                                sb.Append("}]");
                            }
                            closeBlockTimes = nextlevel;
                            // close all nodes children
                            sb.Append("}");

                            if (i != childRows.Rows.Count - 1)
                            {
                                sb.Append(",");
                            }
                        }
                    }
                    sb.Append("]");
                    // end of forum loop
                    return sb.ToString();
                }
            }
            return null;
        }
        */


        public static string GetAllUserAccessJumpTree(int userId)
        {
            using (
                var childRows =
                    YafContext.Current.Get<IDataCache>()
                        .GetOrSet(
                            Constants.Cache.ForumJump.FormatWith(
                                YafContext.Current.User != null
                                    ? YafContext.Current.PageUserID.ToString(CultureInfo.InvariantCulture)
                                    : UserMembershipHelper.GuestUserName),
                            () =>
                            CommonDb.forum_listall_sorted_all(
                                YafContext.Current.PageModuleID,
                                YafContext.Current.PageBoardID,
                                YafContext.Current.PageUserID,
                                true),
                            TimeSpan.FromMinutes(5)))
            {
                if (childRows != null && childRows.Rows.Count > 0)
                {
                    var sb = new StringBuilder("[");
                    // level counter
                    int closeBlockTimes = 0;
                    int currentHideLevel = int.MaxValue;
                    int nextlevel;
                    int currentCategory = 0;
                    bool singleFirstOrAnyLastRow;
                    // a loop through forums
                    for (int i = 0; i < childRows.Rows.Count; i++)
                    {
                        // remembering current level as previous
                        string title = childRows.Rows[i]["Title"].ToString();
                        var forumId = childRows.Rows[i]["ForumID"].ToType<int>();
                        int currentlevel = UserForumAccess.DashCounter(title);
                        singleFirstOrAnyLastRow = (i == 0 && childRows.Rows.Count == 1) || i == childRows.Rows.Count - 1;
                        nextlevel = singleFirstOrAnyLastRow
                                        ? 0
                                        : UserForumAccess.DashCounter(childRows.Rows[i + 1]["Title"].ToString().Trim());

                        var isCurrentHidden = childRows.Rows[i]["IsHidden"].ToType<bool>();
                        var isCurrentNoAccess = !childRows.Rows[i]["ReadAccess"].ToType<bool>();
                        if (isCurrentHidden && isCurrentNoAccess && currentlevel < currentHideLevel)
                        {
                            currentHideLevel = currentlevel;
                        }


                        if (currentlevel == 0)
                        {
                            // this is category
                            currentCategory = forumId;
                        }

                        string path = forumId > 0
                                          ? YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.topics,
                                              true,
                                              "f={0}".FormatWith(forumId))
                                          : YafBuildLink.GetLinkNotEscaped(
                                              ForumPages.forum,
                                              true,
                                              "c={0}".FormatWith(-forumId));

                        // don't add a link for noaccess forum
                        if (!isCurrentNoAccess)
                        {
                            title = (title.IsNotSet())
                                        ? string.Empty
                                          : @"<a href='{0}' target='_top' alt='{1}'>{1}</a>".FormatWith(
                                            path.Replace("resource.ashx", Config.BaseScriptFile),                                      
                                            title);
                        }
                        else
                        {
                            title = (title.IsNotSet()) ? string.Empty : title;
                        }

                        // write the node beginning
                        sb.AppendFormat(
                            "{{\"key\": \"{0}_{1}_{2}\", \"title\": \"{3}\"{4}",
                            YafContext.Current.PageBoardID,
                            currentCategory,
                            forumId,
                            title,
                            (currentlevel == 0) ? ", \"folder\": \"true\"" : ", \"folder\": \"false\"");

                        // previous is a folder, opening childrens block for next, else simply closing the block
                        if (nextlevel > currentlevel)
                        {
                            // add an entry for children 
                            sb.Append(", \"children\": [");
                            // increase level counter for the next node
                            closeBlockTimes++;
                        }
                        else if (nextlevel == currentlevel)
                        {
                            sb.Append("},");
                        }
                        else
                        {
                            // close all nodes children until a new low level
                            for (int k = 0; k < currentlevel - nextlevel; k++)
                            {
                                sb.Append("}]");
                            }

                            closeBlockTimes = nextlevel;

                            // close all nodes children
                            sb.Append("}");

                            if (i != childRows.Rows.Count - 1)
                            {
                                sb.Append(",");
                            }
                        }
                    }

                    sb.Append("]");
                    // end of forum loop
                    return sb.ToString();
                }
            }
            return null;
        }

        public static bool SetTreeNodesAdminForumMove(
    string trno,
    string trn,
    string trnp,
    string trnop,
    string trna)
        {
            int? boardId = null;
            int? categoryId = null;
            int? forumId = null;
            int? parentId = null;
            int? adjacentForumId = null;
            int? adjacentCategoryId = null;
            int? adjacentPosition = null;

            switch (trna)
            {
                case "before":
                    adjacentPosition = 1;
                    break;
                case "after":
                    adjacentPosition = 2;
                    break;
                case "over":
                    adjacentPosition = 3;
                    break;
            }

            // trno - other node - the node key being moved
            // trn - node around that the other node is being moved
            // trnp,trnop respective parents
            // trna nodde move position

            var keyTrno = TreeViewUtils.GetParcedTreeNode(trno);
            var keyTrn = TreeViewUtils.GetParcedTreeNode(trn);

            // we move forum as a forum child 
            if (trn == trnop && trna == "over")
            {
                forumId = keyTrno.ForumId;
                adjacentForumId = parentId = keyTrn.ForumId;
                categoryId = keyTrn.CategoryId;
                boardId = keyTrn.BoardId;
            }

            // we move forum right into category 
            if (trnp == trnop)
            {
                forumId = keyTrno.ForumId;
                categoryId = keyTrn.CategoryId;
                boardId = keyTrn.BoardId;
                adjacentForumId = keyTrn.ForumId;
            }

            // we move a category
            if (trnp == trnop && trnp.Contains("root"))
            {
                forumId = keyTrno.ForumId;
                categoryId = keyTrno.CategoryId;
                boardId = keyTrn.BoardId;
                adjacentCategoryId = keyTrn.CategoryId;
            }          

            // a forum is being moved
            if (forumId.HasValue)
            {
                DataRow row = CommonDb.forum_list(YafContext.Current.ModuleID, boardId, forumId).Rows[0];
                string errorMessage;
                var flags = new ForumFlags(Convert.ToInt32(row["Flags"]));
                // schedule...
                ForumSaveTask.Start(YafContext.Current.ModuleID,
                    forumId,
                    categoryId,
                    parentId,
                    row["Name"],
                    row["Description"],
                    0,
                    flags.IsLocked,
                    flags.IsHidden,
                    flags.IsTest,
                    flags.IsModerated,
                    null,
                    row["RemoteURL"],
                    row["ImageURL"],
                    row["ThemeURL"],
                    row["Styles"],
                    true,
                    row["CreatedByUserID"],
                    row["IsUserForum"].ToType<bool>(),
                    row["CanHavePersForums"].ToType<bool>(),
                    adjacentForumId,
                    adjacentPosition,
                    out errorMessage);
            }
            else if (categoryId.HasValue)
            {
                DataRow crow = CommonDb.category_list(YafContext.Current.ModuleID, boardId, categoryId).Rows[0];
                // Saving a category
                string failureMessage;
                CategorySaveTask.Start(YafContext.Current.ModuleID, boardId, categoryId, crow["Name"],
                    crow["CategoryImage"], crow["SortOrder"], crow["CanHavePersForums"], adjacentCategoryId,
                    adjacentPosition, out failureMessage);
            }

            return true;
        }

        public static void SetGroupAccess(object forumId, object groupId, object accessMaskId)
        {
            if (YafContext.Current.IsAdmin || YafContext.Current.IsHostAdmin)
            {
                var parced = TreeViewUtils.GetParcedTreeNode(forumId.ToString());
                if (parced.ForumId.HasValue)
                {
                    CommonDb.forumaccess_save(YafContext.Current.PageModuleID,
                        parced.ForumId,
                        groupId,
                        accessMaskId);
                }
                else
                {
                    // TODO: implement bulk access change for a category.
                }
            }
        }

        public static List<TreeNode> GetForumsJumpTreeNodesLevel(
            string nodeIds,
            int view,
            int access,
            string active,
            bool boardFirst,
            string forumUrl,
            bool trnl,
            int? amdd)
        {
            var collection = new List<TreeNode>();

            var keySupplied = TreeViewUtils.GetParcedTreeNode(nodeIds);
           

            // var boardId = context.Request.QueryString.GetFirstOrDefault("tjls").ToType<int>();
            const bool notIncluded = true;

            const bool immediateOnly = true;

            if (!keySupplied.BoardId.HasValue)
            {
                keySupplied.BoardId = YafContext.Current.PageBoardID;
            }

            if (!keySupplied.ForumId.HasValue && !keySupplied.CategoryId.HasValue)
            {
            }

            if (boardFirst)
            {
                var bdt = CommonDb.board_list(YafContext.Current.PageModuleID, keySupplied.BoardId);
              
                if (bdt == null || bdt.Rows.Count <= 0) return null;

                string boardName = bdt.Rows[0]["Name"].ToString();
                var tn = new TreeNode
                {
                    key = keySupplied.BoardId.ToString(),
                    title = HttpUtility.HtmlEncode(boardName),
                    lazy = true,
                    folder = true,
                    expanded = false,
                    selected = false,
                    extraClasses = string.Empty,
                    tooltip =
                        YafContext.Current.Get<ILocalization>().GetText("COMMON", "VIEW_FORUM")
                };
                collection.Add(tn);
                return collection;
            }

            // Get a list of category nodes separately as we don't know access. 
            if (!keySupplied.ForumId.HasValue && !keySupplied.CategoryId.HasValue)
            {
                DataTable ctbl;
                if (YafContext.Current.IsAdmin)
                {
                    ctbl = CommonDb.category_list(YafContext.Current.PageModuleID,
                        keySupplied.BoardId, null);

                    if (ctbl == null || ctbl.Rows.Count <= 0) return collection;

                    foreach (DataRow row in ctbl.Rows)
                    {
                        collection.Add(new TreeNode
                        {
                            title = GetCategoryTitleLink(forumUrl, row["CategoryID"], row["Name"], !trnl),
                            key = GetCategoryNodeKey(keySupplied.BoardId, row),
                            lazy = true,
                            folder = true,
                            expanded = false,
                            selected = false,
                            extraClasses = string.Empty,
                            tooltip =
                                YafContext.Current.Get<ILocalization>()
                                    .GetText("COMMON", "VIEW_CATEGORY")
                        });
                    }
                }
                else
                {
                    ctbl = CommonDb.forum_cataccess_actuser(
                        YafContext.Current.PageModuleID,
                        YafContext.Current.PageBoardID,
                        YafContext.Current.PageUserID);
                    if (ctbl != null && ctbl.Rows.Count > 0)
                    {
                        collection.AddRange(from DataRow row in ctbl.Rows
                            select new TreeNode
                            {
                                key = GetCategoryNodeKey(keySupplied.BoardId, row),
                                title = GetCategoryTitleLink(forumUrl, row["CategoryID"], row["CategoryName"], !trnl),
                                lazy = true,
                                folder = true,
                                expanded = false,
                                selected = false,
                                extraClasses = string.Empty,
                                tooltip = YafContext.Current.Get<ILocalization>().GetText("COMMON", "VIEW_CATEGORY")
                            });
                    }
                }
            }
            else
            {
                DataTable ss;
                if (YafContext.Current.IsAdmin)
                {
                    if (amdd.HasValue)
                    {
                        ss = CommonDb.forum_ns_getch_accgroup(
                            YafContext.Current.PageModuleID,
                            keySupplied.BoardId,
                            keySupplied.CategoryId,
                            keySupplied.ForumId,
                            amdd,
                            notIncluded,
                            immediateOnly,
                            "-");
                    }
                    else
                    {
                        ss = CommonDb.forum_ns_getchildren(
                            YafContext.Current.PageModuleID,
                            keySupplied.BoardId,
                            keySupplied.CategoryId,
                            keySupplied.ForumId,
                            notIncluded,
                            immediateOnly,
                            "-");
                    }

                }
                else
                {
                    ss = CommonDb.forum_ns_getch_actuser(
                        YafContext.Current.PageModuleID,
                       keySupplied.BoardId,
                            keySupplied.CategoryId,
                            keySupplied.ForumId,
                        Convert.ToInt32(YafContext.Current.PageUserID),
                        notIncluded,
                        immediateOnly,
                        "-");
                }

                if (ss == null || ss.Rows.Count <= 0) return collection;

                var accessMasks =
                    CommonDb.accessmask_aforumlist(mid: YafContext.Current.ModuleID,
                        boardId: YafContext.Current.PageBoardID,
                        accessMaskId: null, excludeFlags: 0, pageUserId: null,
                        isAdminMask: true, isCommonMask: true);

                foreach (DataRow row in ss.Rows)
                {
                    var nodeKey = GetForumNodeKey(keySupplied.BoardId, row);
                    collection.Add(
                        new TreeNode
                        {
                            key = nodeKey,
                            expanded = false,
                            lazy = row["HasChildren"].ToType<bool>(),
                            folder = false,
                            tooltip = YafContext.Current.Get<ILocalization>().GetText("COMMON", "VIEW_FORUM"),
                            selected = false,
                            extraClasses = string.Empty,
                            title =
                                GetForumTitleLink(forumUrl, row["ForumID"], row["Title"],
                                    row.Table.Columns.Contains("NoAccess") ? row["NoAccess"] : null,
                                    row.Table.Columns.Contains("AccessMaskID") ? row["AccessMaskID"] : null,
                                    !trnl, view == 1, amdd.HasValue, nodeKey, accessMasks)
                        });
                }
            }

            return collection;
        }

        private static string GetCategoryNodeKey(int? boardId, DataRow row)
        {
              return "{0}_{1}".FormatWith(boardId, row["CategoryID"].ToString().IsSet() ?  row["CategoryID"]: null);
        }

        private static string GetForumNodeKey(int? boardId, DataRow row)
        {
           return "{0}_{1}".FormatWith(GetCategoryNodeKey(boardId, row) ,row["ForumID"].ToString().IsSet()
               ? row["ForumID"] : string.Empty);
        }

        private static string GetCategoryTitleLink(string forumUrl, object id, object name, bool titleOnly)
        {
            string pathStart = string.Empty;
            if (name.ToString().IsNotSet())
            {
                return string.Empty;
            }

            if (titleOnly)
            {
                return HttpUtility.HtmlEncode(name);
            }


            pathStart = Config.IsAnyPortal
                                   ? pathStart
                                   : HttpUtility.UrlDecode(forumUrl).Replace("resource.ashx", Config.BaseScriptFile);
           
            if (Config.IsMojoPortal)
            {
                pathStart = pathStart + "&";

            }
            else
            {
                if (pathStart.IndexOf('?') != pathStart.Length - 1)
                {
                    pathStart = pathStart + "?";
                }
            }

            pathStart = pathStart + "g={0}&c={1}".FormatWith(ForumPages.forum, id);
            /* if (Config.IsAnyPortal)
               {
                   realU = pathStart;
               }*/

            return @"<a href='{0}' target='_top' title='{1}'>{1}</a>".FormatWith(
                 pathStart,
                 HttpUtility.HtmlEncode(name));

        }

        private static string GetForumTitleLink(string forumUrl, 
            object id, object name, object noAccessRow, object accessMaskId, bool titleOnly, bool addAccessRow, bool addAccessDropDown, 
            string nodeKey,
            DataTable accessMasks)
        {
            if (name.ToString().IsNotSet())
            {
                return string.Empty;
            }

            string accessRow = string.Empty;
            if (addAccessRow)
            {
                accessRow = " : "
                            + UserForumAccess.AddAccessImagesAndTips(
                                YafContext.Current.PageUserID,
                                id.ToType<int>());
                YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
                YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");
            }

            if (addAccessDropDown)
            {             
               accessRow = accessRow + UserForumAccess.AddGroupAccessDdl(accessMasks, nodeKey, accessMaskId);
            }
             
            string fttl;

            if (!titleOnly)
            {
                string pathStart = HttpUtility.UrlDecode(forumUrl).Replace("resource.ashx", Config.BaseScriptFile);
               
                if (Config.IsMojoPortal)
                {
                    pathStart = pathStart + "&g={0}&f={1}".FormatWith(ForumPages.topics,id);
                }
                else
                {
                    if (pathStart.IndexOf('?') != pathStart.Length - 1)
                    {
                        pathStart = pathStart + "?";
                    }

                     pathStart = pathStart + "g={0}&f={1}".FormatWith(ForumPages.topics,id);
                }               
             
                fttl =  noAccessRow != null && noAccessRow.ToType<bool>()
                                  ? "{0}{1}".FormatWith(
                                      name,
                                      YafContext.Current.Get<ILocalization>()
                                        .GetText("DEFAULT", "NO_FORUM_ACCESS"))
                                  : @"<a href='{0}' target='_top' title='{1}'>{1}</a>{2}".FormatWith(
                                      pathStart,
                                      HttpUtility.HtmlEncode(name),
                                      accessRow);
            }
            else
            {
                fttl =  HttpUtility.HtmlEncode(name);
            }

            return fttl;
        }
    }
}
