/* Yet Another Forum.NET
 * Copyright (C) 2006-2011 Jaben Cargman
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
namespace VZF.Data.MySql.Search
{
    using VZF.Data.DAL;

    #region Using

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using YAF.Types;
    using YAF.Utils;
    using YAF.Types.Constants;

    using VZF.Utils;

    using YAF.Classes;

    #endregion

    #region Enums

    #endregion

    /// <summary>
    /// The search builder.
    /// </summary>
    public class SearchBuilder
    {
        #region Public Methods

        /// <summary>
        /// The build search sql.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="toSearchWhat">
        /// The to search what.
        /// </param>
        /// <param name="toSearchFromWho">
        /// The to search from who.
        /// </param>
        /// <param name="searchFromWhoMethod">
        /// The search from who method.
        /// </param>
        /// <param name="searchWhatMethod">
        /// The search what method.
        /// </param>
        /// <param name="pageUserId">
        /// The page User Id.
        /// </param>
        /// <param name="searchDisplayName">
        /// The search display name.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="maxResults">
        /// The max results.
        /// </param>
        /// <param name="useFullText">
        /// The use full text.
        /// </param>
        /// <param name="ids">
        /// The ids.
        /// </param>
        /// <param name="forumIdToStartAt">
        /// The forum Id To Start At.
        /// </param>
        /// <returns>
        /// The build search sql.
        /// </returns>
        public string BuildSearchSql(
            int? mid,
            [NotNull] string toSearchWhat,
            [NotNull] string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            int pageUserId,
            bool searchDisplayName,
            int boardId,
            int maxResults,
            bool useFullText,
            string ids,
            [NotNull] IEnumerable<int> forumIdToStartAt)
        {
            CodeContracts.ArgumentNotNull(toSearchWhat, "toSearchWhat");
            CodeContracts.ArgumentNotNull(toSearchFromWho, "toSearchFromWho");

            string limitString = string.Empty;
            string orderString = string.Empty;

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            bool bFirst = true;

            // if ( ToSearch.Length == 0 )
            //	return new DataTable();
            if (toSearchWhat == "*")
            {
                toSearchWhat = string.Empty;
            }
            string forumIDs = ids;

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            string searchSql = (maxResults == 0) ? "SELECT" : "SELECT DISTINCTROW ";

            searchSql +=
                " a.ForumID, a.TopicID, a.Topic, b.UserID, IFNULL(c.Username, b.Name) as Name, c.MessageID, c.Posted, '' AS Message, c.Flags";
            searchSql += " from " + SqlDbAccess.GetVzfObjectName("Topic", mid) + " a left join "
                         + SqlDbAccess.GetVzfObjectName("Message", mid) + " c on a.TopicID = c.TopicID left join "
                         + SqlDbAccess.GetVzfObjectName("User", mid) + " b on c.UserID = b.UserID join "
                         + SqlDbAccess.GetVzfObjectName("vaccess", mid) + " x ON x.ForumID=a.ForumID ";
            searchSql += string.Format(@"WHERE x.ReadAccess<>0 AND x.UserID={0} ", pageUserId);

            searchSql +=
                " and IFNULL(CAST(SIGN(c.Flags & 16) AS SIGNED),0) = 1 AND a.TopicMovedID IS NULL AND IFNULL(CAST(SIGN(a.Flags & 8) AS SIGNED),0) = 0 AND IFNULL(CAST(SIGN(c.Flags & 8) AS SIGNED),0) = 0 ";
            orderString += " ORDER BY a.ForumID ";
            limitString += " LIMIT @i_Limit ";
            string[] words;

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                searchSql += "AND (";

                // generate user search sql...
                int userId;
                switch (searchFromWhoMethod)
                {
                    case SearchWhatFlags.AllWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql += " AND ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            if (int.TryParse(word, out userId))
                            {
                                searchSql +=
                                    string.Format(
                                        " ((c.Username IS NULL AND b.Name LIKE CONVERT('{0}' USING {1})) OR (c.Username LIKE CONVERT('{0}' USING {1})))",
                                        word,
                                        !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                            ? Config.DatabaseEncoding
                                            : "utf8");
                            }
                            else
                            {
                                if (searchDisplayName)
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.DisplayName LIKE CONVERT('{0}' USING {1})) OR (c.Username LIKE CONVERT('{0}' USING {1})))",
                                            word,
                                            !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                                ? Config.DatabaseEncoding
                                                : "utf8");
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.Name LIKE CONVERT('{0}' USING {1})) OR (c.Username LIKE CONVERT('{0}' USING {1})))",
                                            word,
                                            !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                                ? Config.DatabaseEncoding
                                                : "utf8");
                                }
                            }
                        }
                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql += " OR ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            if (int.TryParse(word, out userId))
                            {
                                searchSql += string.Format(" (c.UserID IN ({0}))", userId);
                            }
                            else
                            {
                                if (searchDisplayName)
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.DisplayName LIKE CONVERT('{0}' USING {1})) OR (c.Username LIKE CONVERT('{0}' USING {1})))",
                                            word,
                                            !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                                ? Config.DatabaseEncoding
                                                : "utf8");
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.Name LIKE CONVERT('{0}' USING {1})) OR (c.Username LIKE CONVERT('{0}' USING {1})))",
                                            word,
                                            !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                                ? Config.DatabaseEncoding
                                                : "utf8");
                                }
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:
                        if (int.TryParse(toSearchFromWho, out userId))
                        {
                            searchSql += string.Format(" (c.UserID IN ({0}))", userId);
                        }
                        else
                        {
                            if (searchDisplayName)
                            {
                                searchSql +=
                                    string.Format(
                                        " ((c.Username IS NULL AND b.DisplayName = CONVERT('{0}' USING {1})) OR (c.Username = CONVERT('{0}' USING {1})))",
                                        toSearchFromWho,
                                        !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                            ? Config.DatabaseEncoding
                                            : "utf8");
                            }
                            else
                            {
                                searchSql +=
                                    string.Format(
                                        " ((c.Username IS NULL AND b.Name = CONVERT('{0}' USING {1})) OR (c.Username = CONVERT('{0}' USING {1})))",
                                        toSearchFromWho,
                                        !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                            ? Config.DatabaseEncoding
                                            : "utf8");
                            }
                        }

                        break;
                }

                searchSql += ") ";
            }


            if (!string.IsNullOrEmpty(toSearchWhat))
            {
                searchSql += "AND (";
                bFirst = true;

                // generate message and topic search sql...
                switch (searchWhatMethod)
                {
                    case SearchWhatFlags.AllWords:
                        words = toSearchWhat.Split(' ');
                        if (useFullText)
                        {
                            string ftInner = string.Empty;

                            // make the inner FULLTEXT search
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    ftInner += " AND ";
                                }
                                else
                                {
                                    bFirst = false;
                                }

                                ftInner += string.Format(@"""{0}""", word);
                            }

                            // make final string...
                            searchSql +=
                                string.Format(
                                    "( CONTAINS (c.Message, CONVERT(' {0} ' USING {1}) OR CONTAINS (a.Topic, CONVERT(' {0} ' USING {1})) )",
                                    ftInner,
                                    !string.IsNullOrEmpty(Config.DatabaseEncoding) ? Config.DatabaseEncoding : "utf8");
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql += " AND ";
                                else bFirst = false;
                                searchSql +=
                                    string.Format(
                                        "(c.Message like CONVERT('%{0}%' USING {1}) OR a.Topic LIKE CONVERT('%{0}%' USING {1}))",
                                        word,
                                        !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                            ? Config.DatabaseEncoding
                                            : "utf8");
                            }
                        }

                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchWhat.Split(' ');

                        if (useFullText)
                        {
                            string ftInner = string.Empty;

                            // make the inner FULLTEXT search
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    ftInner += " OR ";
                                }
                                else
                                {
                                    bFirst = false;
                                }

                                ftInner += string.Format(@"""{0}""", word);
                            }

                            // make final string...
                            searchSql +=
                                string.Format(
                                    "( CONTAINS (c.Message, CONVERT(' {0} ' USING {1})) OR CONTAINS (a.Topic, CONVERT(' {0} ' USING {1})) )",
                                    ftInner,
                                    !string.IsNullOrEmpty(Config.DatabaseEncoding) ? Config.DatabaseEncoding : "utf8");
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql += " OR ";
                                else bFirst = false;
                                searchSql +=
                                    string.Format(
                                        "c.Message LIKE CONVERT('%{0}%' USING {1}) OR a.Topic LIKE CONVERT('%{0}%' USING {1})",
                                        word,
                                        !string.IsNullOrEmpty(Config.DatabaseEncoding)
                                            ? Config.DatabaseEncoding
                                            : "utf8");
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:
                        if (useFullText)
                        {
                            searchSql +=
                                string.Format(
                                    "( CONTAINS (c.Message, CONVERT(' \"{0}\" ' USING {1})) OR CONTAINS (a.Topic, CONVERT(' \"{0}\" ' USING {1}) )",
                                    toSearchWhat,
                                    !string.IsNullOrEmpty(Config.DatabaseEncoding) ? Config.DatabaseEncoding : "utf8");
                        }
                        else
                        {
                            searchSql +=
                                string.Format(
                                    "c.Message LIKE CONVERT('%{0}%' USING {1}) OR a.Topic LIKE CONVERT('%{0}%' USING {1}) ",
                                    toSearchWhat,
                                    !string.IsNullOrEmpty(Config.DatabaseEncoding) ? Config.DatabaseEncoding : "utf8");
                        }

                        break;
                }

                searchSql += ") ";
            }

            // Ederon : 6/16/2007 - forum IDs start above 0, if forum id is 0, there is no forum filtering
            if (forumIdToStartAt.Any())
            {
                searchSql += string.Format("AND a.ForumID IN ( SELECT {0})", forumIDs);
            }

            if (orderString != string.Empty)
            {
                orderString += ", ";
            }

            if (!orderString.Contains("ORDER BY"))
            {
                searchSql += " ORDER BY ";
            }

            searchSql += orderString + "c.Posted DESC ";

            if (!orderString.Contains("LIMIT"))
            {
                searchSql += limitString;
            }

            return searchSql;
        }

        #endregion
    }
}