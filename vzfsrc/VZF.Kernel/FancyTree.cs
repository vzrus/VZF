// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="FancyTree.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Data.DAL;

    using VZF.Types.Objects;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Data.DAL;
    using YAF.Types.Flags;
    using VZF.Types.Constants;
    using VZF.Utils.Helpers;
    using YAF.Core.Tasks;

    #endregion

    /// <summary>
    /// This class wraps Dynatree JQuery plug-in functionality. 
    /// </summary>
    public static class FancyTree
    {
        /// <summary>
        /// The yes image.
        /// </summary>
        private static string yesImage;

        /// <summary>
        /// The no image.
        /// </summary>
        private static string noImage;

        /// <summary>
        /// The delete image.
        /// </summary>
        private static string deleteImage;

        /// <summary>
        /// The copy image.
        /// </summary>
        private static string copyImage;

        /// <summary>
        /// The edit image.
        /// </summary>
        private static string editImage;

        /// <summary>
        /// The move forum.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="nodesInfo">
        /// The nodes info.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
    /*    public static string MoveForum(int userId, string nodesInfo) 
        {
            string ret = string.Empty;

            // first - moved node id, second - parent nodeid, third - prevnode
            return ret;
        } */

        /// <summary>
        /// The get all common admin tree.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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
                                    ? YafContext.Current.PageUserID.ToString()
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
                    int currentlevel;
                    int nextlevel;
                    int currentCategory = 0;
                    bool singleFirstOrAnyLastRow;
                    // a loop through forums
                    for (int i = 0; i < childRows.Rows.Count; i++)
                    {
                        // remembering current level as previous
                        string title = childRows.Rows[i]["Title"].ToString();
                        var forumId = childRows.Rows[i]["ForumID"].ToType<int>();
                        currentlevel = UserForumAccess.DashCounter(title);
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
            // return true;
            int? boardId = null;
            int? categoryId = null;
            int? forumId = null;
            int? parentId = null;
            int sortOrder = 0;
            int? adjacentForumId = null;
            int? adjacentCategoryId = null;
            int? adjacentPosition = null;

            bool withBoards = false;

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
          

            // we move forum as a forum child 
            if (trn == trnop && trna == "over")
            {
                forumId = TreeViewUtils.GetParcedTreeNodeId(trno).Item3;
                adjacentForumId = parentId = TreeViewUtils.GetParcedTreeNodeId(trn).Item3;
                categoryId = TreeViewUtils.GetParcedTreeNodeId(trn).Item2;
                boardId = TreeViewUtils.GetParcedTreeNodeId(trn).Item1;
            }

            // we move forum right into category 
            if (trnp == trnop)
            {
                forumId = TreeViewUtils.GetParcedTreeNodeId(trno).Item3;
                categoryId = TreeViewUtils.GetParcedTreeNodeId(trn).Item2;
                boardId = TreeViewUtils.GetParcedTreeNodeId(trn).Item1;
                adjacentForumId = TreeViewUtils.GetParcedTreeNodeId(trn).Item3;
            }

            // we move a category
            if (trnp == trnop && trnp.Contains("root"))
            {
                forumId = TreeViewUtils.GetParcedTreeNodeId(trno).Item3;
                categoryId = TreeViewUtils.GetParcedTreeNodeId(trno).Item2;
                boardId = TreeViewUtils.GetParcedTreeNodeId(trn).Item1;
                adjacentCategoryId = TreeViewUtils.GetParcedTreeNodeId(trn).Item2;
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
                    sortOrder,
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
            else if (categoryId.HasValue && !forumId.HasValue)
            {
                DataRow crow = CommonDb.category_list(YafContext.Current.ModuleID, boardId,categoryId).Rows[0];  
               // Saving a category
               string failureMessage;
               CategorySaveTask.Start(YafContext.Current.ModuleID, boardId, categoryId, crow["Name"], crow["CategoryImage"], crow["SortOrder"], crow["CanHavePersForums"], adjacentCategoryId, adjacentPosition, out failureMessage);
            }

            return true;
        }

        public static void SetGroupAccess(object forumId, object groupId, object accessMaskId)
        {
            if (YafContext.Current.IsAdmin || YafContext.Current.IsHostAdmin)
            {
                var parced = TreeViewUtils.GetParcedTreeNodeId(forumId.ToString());
                if (parced.Item3.HasValue)
                {
                    CommonDb.forumaccess_save(YafContext.Current.PageModuleID,
                        parced.Item3,
                        groupId,
                        accessMaskId);
                }
                else
                {
                    /// TODO: implement bulk access change for a category.
                }
            }
        }
        public static List<TreeNode> GetForumsJumpTreeNodesLevel(
            string nodeIdString,
            int view,
            int access,
            string active,
            bool boardFirst,
            string forumUrl,
            bool trnl,
            int? amdd)
        {
            var collection = new List<TreeNode>();

            int boardId = 0;
            int categoryId = 0;
            int forumId = 0;
            var nodeIds = nodeIdString;
            if (nodeIds.IsSet())
            {
                string[] nodeId = nodeIds.Split('_');
                switch (nodeId.Count())
                {
                    case 1:
                        boardId = nodeId[0].ToType<int>();
                        break;
                    case 2:
                        boardId = nodeId[0].ToType<int>();
                        categoryId = nodeId[1].ToType<int>();
                        break;
                    case 3:
                        boardId = nodeId[0].ToType<int>();
                        categoryId = nodeId[1].ToType<int>();
                        forumId = nodeId[2].ToType<int>();
                        break;
                }
            }

            // var boardId = context.Request.QueryString.GetFirstOrDefault("tjls").ToType<int>();
            const bool notIncluded = true;

            const bool immediateOnly = true;

            if (boardId <= 0)
            {
                boardId = YafContext.Current.PageBoardID;
            }

            if (forumId == 0 && categoryId == 0)
            {
            }

            if (boardFirst)
            {
                var bdt = CommonDb.board_list(YafContext.Current.PageModuleID, boardId);
                if (bdt != null && bdt.Rows.Count > 0)
                {
                    string boardName = bdt.Rows[0]["Name"].ToString();
                    var tn = new TreeNode
                                 {
                                     key = boardId.ToString(CultureInfo.InvariantCulture),
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

                return null;
            }


            // Get a list of category nodes separately as we don't know access. 
            if (forumId == 0 && categoryId == 0)
            {
                DataTable ctbl;
                if (YafContext.Current.IsAdmin)
                {
                    ctbl = CommonDb.category_list(YafContext.Current.PageModuleID,
                        YafContext.Current.PageBoardID, null);
                    if (ctbl != null && ctbl.Rows.Count > 0)
                    {
                        foreach (DataRow row in ctbl.Rows)
                        {
                            collection.Add(new TreeNode
                            {
                                title = GetCategoryTitleLink(forumUrl, row["CategoryID"], row["Name"], !trnl),
                                key = GetCategoryNodeKey(boardId, row),
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
                }
                else
                {
                    ctbl = CommonDb.forum_cataccess_actuser(
                        YafContext.Current.PageModuleID,
                        YafContext.Current.PageBoardID,
                        YafContext.Current.PageUserID);
                    if (ctbl != null && ctbl.Rows.Count > 0)
                    {
                        foreach (DataRow row in ctbl.Rows)
                        {

                            collection.Add(new TreeNode
                            {
                                key = GetCategoryNodeKey(boardId, row),
                                title = GetCategoryTitleLink(forumUrl, row["CategoryID"], row["CategoryName"], !trnl),
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
                      boardId,
                      categoryId,
                      forumId,
                      amdd,
                      notIncluded,
                      immediateOnly,
                      "-");
                    }
                    else
                    {
                        ss = CommonDb.forum_ns_getchildren(
                      YafContext.Current.PageModuleID,
                      boardId,
                      categoryId,
                      forumId,
                      notIncluded,
                      immediateOnly,
                      "-");
                    }

                }
                else
                {
                    ss = CommonDb.forum_ns_getch_actuser(
                        YafContext.Current.PageModuleID,
                        boardId,
                        categoryId,
                        forumId,
                        Convert.ToInt32(YafContext.Current.PageUserID),
                        notIncluded,
                        immediateOnly,
                        "-");
                }

                if (ss != null && ss.Rows.Count > 0)
                {
                    DataTable accessMasks = CommonDb.accessmask_aforumlist(mid: YafContext.Current.ModuleID, boardId: YafContext.Current.PageBoardID,
              accessMaskId: null, excludeFlags: 0, pageUserId: null,
              isAdminMask: true, isCommonMask: true);
                    foreach (DataRow row in ss.Rows)
                    {
                        var nodeKey = GetForumNodeKey(boardId, row);
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
                                         title = GetForumTitleLink(forumUrl, row["ForumID"], row["Title"], row.Table.Columns.Contains("NoAccess") ? row["NoAccess"] : null, row.Table.Columns.Contains("AccessMaskID") ? row["AccessMaskID"] : null,
                                             !trnl, view == 1, amdd.HasValue, nodeKey, accessMasks)
                                     });
                    }
                }
            }

            return collection;
        }

        private static string GetCategoryNodeKey(int boardId, DataRow row)
        {
              return "{0}_{1}".FormatWith(boardId, row["CategoryID"].ToString().IsSet() ?  row["CategoryID"]: null);
        }

        private static string GetForumNodeKey(int boardId, DataRow row)
        {
           return "{0}_{1}".FormatWith(GetCategoryNodeKey(boardId, row) ,row["ForumID"].ToString().IsSet()
               ? row["ForumID"] : string.Empty);
        }    
 
        private static string GetCategoryTitleLink(string forumUrl, object id, object name, bool titleOnly)
        {
            string pathStart;
            string realU;
            if (!titleOnly)
            {
                if (Config.IsMojoPortal)
                {
                    pathStart = HttpUtility.UrlDecode(forumUrl) + "&g={0}&c=***".FormatWith(ForumPages.forum);
                }
                else
                {
                    pathStart = forumUrl.Replace(".aspx", ".aspx?g={0}&c=***".FormatWith(ForumPages.forum));
                }
            }
            else
            {
                return name.ToString().IsNotSet()
                                                ? string.Empty
                                                : HttpUtility.HtmlEncode(name);
            }

            if (name.ToString().IsSet())
            {
                if (Config.IsAnyPortal)
                {
                    realU = pathStart;
                }
                else
                {
                    realU = YafBuildLink.GetLinkNotEscaped(ForumPages.forum, "c=***")
                          .Replace("resource.ashx", Config.BaseScriptFile);
                }
              

                return @"<a href='{0}' target='_top' title='{1}'>{1}</a>".FormatWith(
                   realU.Replace("***", id.ToString()),                     
                     HttpUtility.HtmlEncode(name));
            }
          
            return string.Empty;
        }

        private static string GetForumTitleLink(string forumUrl, 
            object id, object name, object noAccessRow, object accessMaskID, bool titleOnly, bool addAccessRow, bool addAccessDropDown, 
            string nodeKey,
            DataTable accessMasks)
        {
            string accessRow = string.Empty;
            if (addAccessRow)
            {
                accessRow = " : "
                            + UserForumAccess.AddAccessImagesAndTips(
                                YafContext.Current.PageUserID,
                                id.ToType<int>());
                yesImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
                noImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");
            }

            if (addAccessDropDown)
            {             
               accessRow = accessRow + UserForumAccess.AddGroupAccessDdl(accessMasks, nodeKey, accessMaskID);
            }
          
            string fttl;
            if (!titleOnly)
            {
                string pathStart;
                if (Config.IsMojoPortal)
                {
                    pathStart = HttpUtility.UrlDecode(forumUrl) + "&g={0}&f=***".FormatWith(ForumPages.topics);
                }
                else
                {
                    pathStart = HttpUtility.UrlDecode(forumUrl)
                        .Replace(".aspx", ".aspx?g={0}&f=***".FormatWith(ForumPages.topics));
                }


                string realU = Config.IsAnyPortal
                                   ? pathStart
                                   : YafBuildLink.GetLinkNotEscaped(ForumPages.topics, "f=***")
                                         .Replace("resource.ashx", Config.BaseScriptFile);
             
                fttl =  noAccessRow != null && noAccessRow.ToType<bool>()
                                  ? "{0}{1}".FormatWith(
                                      name,
                                      YafContext.Current.Get<ILocalization>()
                                        .GetText("DEFAULT", "NO_FORUM_ACCESS"))
                                  : @"<a href='{0}' target='_top' title='{1}'>{1}</a>{2}".FormatWith(
                                      realU.Replace("***", id.ToString()),
                                      HttpUtility.HtmlEncode(name),
                                      accessRow);
            }
            else
            {
                fttl = name.ToString().IsNotSet()
                                        ? string.Empty
                                        : HttpUtility.HtmlEncode(name);
            }
            return fttl;
        }

      
    }
}
