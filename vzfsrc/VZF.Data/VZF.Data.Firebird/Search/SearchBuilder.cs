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
namespace VZF.Data.Firebird.Search
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;

    using VZF.Data.DAL;
    using VZF.Utils;

    using YAF.Types;
    using YAF.Types.Constants;

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
        /// <param name="userId">
        /// The user Id.
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
            int userId,
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
            IEnumerable<int> forumIds = null;
            if (!string.IsNullOrEmpty(ids))
            {
                forumIds = new[] { ids.ToType<int>() };
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            string searchSql = "SELECT ";
            if (maxResults > 0)
            {
                limitString += string.Format(" FIRST {0} ", maxResults);
                searchSql += limitString;
            }

            searchSql +=
                @" a.FORUMID AS ""ForumID"", a.TOPICID AS ""TopicID"", a.TOPIC AS ""Topic"", b.USERID AS ""UserID"" , COALESCE(c.USERNAME, b.NAME) AS ""Name"", c.MESSAGEID AS ""MessageID"", c.POSTED AS ""Posted"", '' AS ""Message"", c.FLAGS AS ""Flags""";
            searchSql += " FROM " + ObjectName.GetVzfObjectName("TOPIC", mid) + " a LEFT JOIN "
                         + ObjectName.GetVzfObjectName("MESSAGE", mid) + @" c ON a.TOPICID = c.TOPICID LEFT JOIN "
                         + ObjectName.GetVzfObjectName("USER", mid) + @" b ON c.USERID = b.USERID join "
                         + ObjectName.GetVzfObjectName("VACCESS", mid) + @" x ON x.FORUMID=a.FORUMID ";
            searchSql +=
                string.Format(
                    @"WHERE x.READACCESS<>0 AND x.USERID={0} AND c.ISAPPROVED <> 0 AND a.TOPICMOVEDID IS NULL AND a.ISDELETED = 0 AND c.ISDELETED = 0 ",
                    userId);
            orderString += @" ORDER BY a.FORUMID ";


            string[] words;
            bool bFirst;

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                searchSql += "AND (";
                bFirst = true;

                // generate user search sql...
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

                            searchSql +=
                                string.Format(
                                    @" ((c.USERNAME IS NULL AND b.NAME LIKE '%{0}%') OR (c.USERNAME LIKE '%{0}%'))",
                                    word);
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
                                            " ((c.Username IS NULL AND b.DisplayName LIKE '%{0}%') OR (c.Username LIKE '%{0}%'))",
                                            word);
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.Username IS NULL AND b.Name LIKE '%{0}%') OR (c.Username LIKE '%{0}%'))",
                                            word);
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
                                if (searchDisplayName)
                                {
                                    searchSql +=
                                        string.Format(
                                            " ((c.USERNAME IS NULL AND b.DISPLAYNAME = '{0}') OR (c.Username = '{0}')",
                                            toSearchFromWho);
                                }
                                else
                                {
                                    searchSql +=
                                        string.Format(
                                            @" ((c.USERNAME IS NULL AND b.NAME LIKE '%{0}%') OR (c.USERNAME LIKE '%{0}%'))",
                                            word);
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
                            searchSql +=
                                string.Format(
                                    @" ((c.USERNAME IS NULL AND b.NAME = '{0}' ) OR (c.USERNAME = '{0}' ))",
                                    toSearchFromWho);
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
                                    @"( CONTAINS (c.MESSAGE, ' {0} ') OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                    ftInner);
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql += " AND ";
                                else bFirst = false;
                                searchSql += string.Format(@"(c.MESSAGE like '%{0}%' OR a.TOPIC LIKE '%{0}%' )", word);
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
                                    @"( CONTAINS (c.MESSAGE, ' {0} ' ) OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                    ftInner);
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql += " OR ";
                                else bFirst = false;
                                searchSql += string.Format(@"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%' ", word);
                            }
                        }

                        break;
                    case SearchWhatFlags.ExactMatch:
                        if (useFullText)
                        {
                            // searchSql += string.Format(@"( CONTAINS (c.MESSAGE, ' \"{0}\" ' ) OR CONTAINS (a.Topic, ' \"{0}\" '  )", toSearchWhat);
                        }
                        else
                        {
                            searchSql += string.Format(
                                @"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%'  ",
                                toSearchWhat);
                        }

                        break;
                }

                searchSql += ") ";
            }

            // Ederon : 6/16/2007 - forum IDs start above 0, if forum id is 0, there is no forum filtering
            if (forumIdToStartAt.Any())
            {
                searchSql += string.Format(@"AND a.FORUMID IN ({0})", forumIds);
            }

            if (orderString != string.Empty)
            {
                orderString += ", ";
            }

            if (!orderString.Contains("ORDER BY"))
            {
                searchSql += " ORDER BY ";
            }

            searchSql += orderString + @"c.POSTED DESC ";
            return searchSql;
        }

        #endregion
    }
}