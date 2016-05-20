#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SearchBuilder.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:18 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

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
            string categoriesIds,
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
            searchSql += " FROM " + SqlDbAccess.GetVzfObjectName("TOPIC", mid) + " a LEFT JOIN "
                         + SqlDbAccess.GetVzfObjectName("MESSAGE", mid) + @" c ON a.TOPICID = c.TOPICID LEFT JOIN "
                         + SqlDbAccess.GetVzfObjectName("USER", mid) + @" b ON c.USERID = b.USERID join "
                         + SqlDbAccess.GetVzfObjectName("VACCESS", mid) + @" x ON x.FORUMID=a.FORUMID ";
            searchSql +=
                string.Format(
                    @"WHERE x.READACCESS<>0 AND x.USERID={0} AND c.ISAPPROVED <> 0 AND a.TOPICMOVEDID IS NULL AND a.ISDELETED = 0 AND c.ISDELETED = 0 ",
                    userId);
            orderString += @" ORDER BY a.FORUMID ";


            
            bool bFirst;

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                searchSql += "AND (";

                searchSql = new SearchFromWho().Build(searchSql, searchFromWhoMethod, toSearchFromWho, useFullText, searchDisplayName);

                searchSql += ") ";
            }

            if (!string.IsNullOrEmpty(toSearchWhat))
            {
                searchSql += "AND (";

                searchSql = new SearchWhat().Build(searchSql, searchWhatMethod, toSearchWhat, useFullText); 

                searchSql += ") ";
            }

            if (categoriesIds.IsSet())
            {
                searchSql +=string.Format(" AND f.categoryid IN ({0})", categoriesIds);
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