/* VZF by vzrus
 * Copyright (C) 2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using YAF.Classes;

namespace VZF.Kernel
{
    #region Using

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Web;
    using VZF.Types;
    using VZF.Types.Objects;

    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utils; 

    #endregion

    /// <summary>
    /// This class wraps Dynatree JQuery plug-in functionality. 
    /// </summary>
    public class Dynatree
    {
        private static string yesImage;
        private static string noImage;
        private static string deleteImage;
        private static string copyImage;
        private static string editImage;
        public static string MoveForum(int userId, string nodesInfo )
        {
            string ret = string.Empty;           
            // first - moved node id, second - parent nodeid, third - prevnode


            return ret;
        }
        public static string GetAllCommonAdminTree()
        {
            using (DataSet ds = CommonDb.ds_forumadmin(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID))
            {
                var dd = ds.Tables[CommonSqlDbAccess.GetObjectName("Category")];
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

                        title = title + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>".FormatWith(
                            YafBuildLink.GetLinkNotEscaped(ForumPages.admin_forums, true, "dc={0}".FormatWith(dr["CategoryID"]), YafContext.Current.PageUserID),
                            "img",
                             "",
                             "",
                             deleteImage);
                        title = title + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>".FormatWith(
                             YafBuildLink.GetLinkNotEscaped(ForumPages.admin_editcategory, true, "c={0}".FormatWith(dr["CategoryID"]), YafContext.Current.PageUserID),
                          "img",
                             "",
                           "",
                           editImage);

                        sb.AppendFormat("{{id: \"{0}.{1}\", title: \"{2}\", isFolder: true", dr["BoardID"],
                                        dr["CategoryID"],
                                        dr["Name"] == null ? "" : title);

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
                                    nodIsParent = (childRows[i]["ForumID"].ToType<int>() ==
                                                   childRows[i + 1]["ParentID"].ToType<int>());
                                    // this node has next sibling
                                    hasNextSibling = (childRows[i + 1]["ParentID"].ToType<int>() ==
                                                      childRows[i]["ParentID"].ToType<int>());

                                }

                            }
                            string title_f = (childRows[i]["Name"] == null)
                                                 ? ""
                                                 : childRows[i]["Name"].ToString();
                            title_f = title_f + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>".FormatWith(
                          YafBuildLink.GetLinkNotEscaped(ForumPages.admin_deleteforum, true, "f={0}".FormatWith(childRows[i]["ForumID"]), YafContext.Current.PageUserID),
                          "img",
                           "",
                           "",
                           deleteImage);
                            title_f = title_f + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>".FormatWith(
                       YafBuildLink.GetLinkNotEscaped(ForumPages.admin_editforum, true, "copy={0}".FormatWith(childRows[i]["ForumID"]), YafContext.Current.PageUserID),
                           "img",   
                       "",
                               "",
                               copyImage);

                            title_f = title_f + @"<a id='ancim' href='{0}'><img id='{1}' alt='{2}'  title='{3}' src='{4}' /></a>".FormatWith(
                        YafBuildLink.GetLinkNotEscaped(ForumPages.admin_editforum, true, "f={0}".FormatWith(childRows[i]["ForumID"]), YafContext.Current.PageUserID),
                             "img", 
                        "",
                               "",
                               editImage);
                            // write the node beginning
                            sb.AppendFormat("{{id: \"{0}.{1}.{2}\", title: \"{3}\"{4}", dr["BoardID"],
                                            dr["CategoryID"], childRows[i]["ForumID"],
                                           title_f,
                                            nodIsParent ? ", isFolder: false" : string.Empty);

                            // previous is a folder, opening childrens block for next, else simply closing the block
                            if (nodIsParent)
                            {

                                levels.TryAdd((int) childRows[i]["ForumID"], closeBlockTimes);
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
        }

        public static string GetAllUserAccessAdminTree(int userId)
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
                       
                        string path = forumId > 0 ? YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}".FormatWith(forumId)) : YafBuildLink.GetLinkNotEscaped(ForumPages.forum, true, "c={0}".FormatWith(forumId));

                        string accessMaskString = string.Empty;
                        accessMaskString = amr.AccessMaskData.Aggregate(accessMaskString, (current, c) => current + "," + c.AccessMaskName);
                        
                        // write the node 
                        sb.AppendFormat("{{id: \"{0}.{1}.{2}\", title: \"{3}\"{4}", YafContext.Current.PageBoardID,
                                        currentCategory, forumId,
                                        (amr.ForumName.IsNotSet())
                                            ? ""
                                            : @"<a href='{0}' target='_top' title='{1}'>{1} -- {2}</a>".FormatWith(path, amr.ForumName, accessMaskString.Trim(',')),
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
               
           
          
        }
        public static string GetAllUserAccessJumpTree(int userId)
        {
            using (var childRows = YafContext.Current.Get<IDataCache>().GetOrSet(
                Constants.Cache.ForumJump.FormatWith(
                YafContext.Current.User != null ? YafContext.Current.PageUserID.ToString() : UserMembershipHelper.GuestUserName),
                () => CommonDb.forum_listall_sorted_all(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, YafContext.Current.PageUserID, true),
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
                        nextlevel = singleFirstOrAnyLastRow ? 0 : UserForumAccess.DashCounter(childRows.Rows[i + 1]["Title"].ToString().Trim());

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
                        
                        string path = forumId > 0 ? YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}".FormatWith(forumId)) : YafBuildLink.GetLinkNotEscaped(ForumPages.forum, true, "c={0}".FormatWith(-forumId));

                        // don't add a link for noaccess forum
                        if (!isCurrentNoAccess)
                        {
                            title = (title.IsNotSet())
                                        ? ""
                                        : @"<a href='{0}' target='_top' alt='{1}'>{1}</a>".FormatWith(
                                            path.Replace("resource.ashx", "default.aspx"), title);
                        }
                        else
                        {
                            title = (title.IsNotSet())
                                      ? ""
                                      : title;
                        }

                   // write the node beginning
                        sb.AppendFormat("{{\"id\": \"{0}.{1}.{2}\", \"title\": \"{3}\"{4}", YafContext.Current.PageBoardID,
                                            currentCategory, forumId,
                                            title,
                                            (currentlevel == 0) ? ", \"isFolder\": \"true\"" : ", \"isFolder\": \"false\"");

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
        public static string GetAllUserAccessJumpTreeLazy(int userId)
        {
            using (var childRows = YafContext.Current.Get<IDataCache>().GetOrSet(
                Constants.Cache.ForumJump.FormatWith(
                YafContext.Current.User != null ? YafContext.Current.PageUserID.ToString() : UserMembershipHelper.GuestUserName),
                () => CommonDb.forum_listall_sorted(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, YafContext.Current.PageUserID),
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
                        nextlevel = singleFirstOrAnyLastRow ? 0 : UserForumAccess.DashCounter(childRows.Rows[i + 1]["Title"].ToString().Trim());

                        if (currentlevel == 0)
                        {
                            // this is category
                            currentCategory = forumId;
                        }

                        string path = forumId > 0 ? YafBuildLink.GetLinkNotEscaped(ForumPages.topics,  "f={0}".FormatWith(forumId)) : YafBuildLink.GetLinkNotEscaped(ForumPages.forum, true, "c={0}".FormatWith(-forumId));

                        // write the node beginning
                        sb.AppendFormat("{{\"id\": \"{0}.{1}.{2}\", \"title\": \"{3}\"{4}", YafContext.Current.PageBoardID,
                                        currentCategory, forumId,
                                        (title.IsNotSet())
                                            ? ""
                                            : @"<a href='{0}' target='_top' alt='{1}'>{1}</a>".FormatWith(path.Replace("resource.ashx", "default.aspx"), title),
                                        (currentlevel == 0) ? ", \"isFolder\": \"true\"" : ", \"isFolder\": \"false\"");

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
        public static List<TreeNode> GetForumsJumpTreeNodesLevel(string nodeIdString, int view, int access, string initializeArguments, bool boardFirst, string forumUrl)
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
                        categoryId = nodeId[1].ToType<int>();
                        break;
                    case 3:
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
                            key = YafContext.Current.PageBoardID.ToString(CultureInfo.InvariantCulture),
                            title = HttpUtility.HtmlEncode(boardName),
                            isLazy = true,
                            isFolder = true,
                            addClass = "",
                            tooltip = YafContext.Current.Get<ILocalization>().GetText(
                                                                    "COMMON", "VIEW_FORUM")
                        };
                    collection.Add(tn);
                    return collection;
                }
                return null;
            }
           

            // Get a list of category nodes separately as we don't know access. 
            if (forumId == 0 && categoryId == 0)
            {
                var tc = CommonDb.forum_categoryaccess_activeuser(YafContext.Current.PageModuleID,
                                                                  YafContext.Current.PageBoardID,
                                                                  YafContext.Current.PageUserID);


                if (tc != null && tc.Rows.Count > 0)
                {
                    yesImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
                    noImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");

                    string pathStart;
                    if (Config.IsMojoPortal)
                    {
                        pathStart = HttpUtility.UrlDecode(forumUrl) + "&g={0}&c=***".FormatWith(ForumPages.forum);
                    }
                    else
                    {
                        pathStart = forumUrl.Replace(".aspx", ".aspx?g={0}&c=***".FormatWith(ForumPages.forum));
                    }
                    string dd = HttpUtility.UrlDecode(forumUrl);
                    
                string realU = Config.IsAnyPortal ? pathStart : YafBuildLink.GetLinkNotEscaped(ForumPages.forum, "c=***").Replace("resource.ashx", "default.aspx");
                    foreach (DataRow row in tc.Rows)
                    {
                        var tn = new TreeNode
                            {
                                key = YafContext.Current.PageBoardID +
                                      "_" + row["CategoryID"],
                                title = (row["CategoryName"].ToString().IsNotSet())
                                            ? ""
                                            : @"<a href='{0}' target='_top' title='{1}'>{1}</a>".FormatWith(
                                                realU.Replace("***", row["CategoryID"].ToString()),
                                                HttpUtility.HtmlEncode(row["CategoryName"])),
                                isLazy = true,
                                isFolder = true,
                                addClass = "",
                                tooltip = YafContext.Current.Get<ILocalization>().GetText(
                                                                    "COMMON", "VIEW_CATEGORY")
                            };

                        collection.Add(tn);
                    }
                }
            }
            else
            {
                var ss = CommonDb.forum_ns_getchildren_activeuser(YafContext.Current.PageModuleID,
                                                                       boardId, categoryId, forumId, Convert.ToInt32(YafContext.Current.PageUserID), notIncluded, immediateOnly, "-");

                if (ss != null && ss.Rows.Count > 0)
                {
                    yesImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
                    noImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");
                    string pathStart;
                    if (Config.IsMojoPortal)
                    {
                        pathStart = HttpUtility.UrlDecode(forumUrl) + "&g={0}&f=***".FormatWith(ForumPages.topics);
                    }
                    else
                    {
                        pathStart = HttpUtility.UrlDecode(forumUrl).Replace(".aspx", ".aspx?g={0}&f=***".FormatWith(ForumPages.topics));
                    }


                    string realU = Config.IsAnyPortal ? pathStart : YafBuildLink.GetLinkNotEscaped(ForumPages.topics, "f=***").Replace("resource.ashx", "default.aspx");
                    foreach (DataRow row in ss.Rows)
                    {
                        var tn = new TreeNode
                            {
                                key = YafContext.Current.PageBoardID +
                                      (row["CategoryID"].ToString().IsSet() ? ("_" + row["CategoryID"]) : null) +
                                      (row["ForumID"].ToString().IsSet() ? ("_" + row["ForumID"]) : null)
                            };

                        string accessRow = string.Empty;
                        if (view == 1)
                        {
                            accessRow =" : " + UserForumAccess.AddAccessImagesAndTips(YafContext.Current.PageUserID,
                                                                               row["ForumID"].ToType<int>());
                        }
                        if (view == 3)
                        {
                            tn.title = HttpUtility.HtmlEncode(row["Title"]);
                        }
                        else
                        {
                            tn.title = row["NoAccess"].ToType<bool>()
                                          ? "{0}{1}".FormatWith(row["Title"],
                                                                YafContext.Current.Get<ILocalization>().GetText(
                                                                    "DEFAULT", "NO_FORUM_ACCESS"))
                                          : @"<a href='{0}' target='_top' title='{1}'>{1}</a>{2}".FormatWith(
                                               realU.Replace("***", row["ForumID"].ToString()), HttpUtility.HtmlEncode(row["Title"]),
                                              accessRow);
                        }
                      
                        tn.isLazy = row["HasChildren"].ToType<bool>();
                        tn.isFolder = false;
                        tn.addClass = "";
                        tn.tooltip = YafContext.Current.Get<ILocalization>().GetText(
                            "COMMON", "VIEW_FORUM");
                        
                        collection.Add(tn);
                    }
                }
            }
            return collection;
        }
    }
}
