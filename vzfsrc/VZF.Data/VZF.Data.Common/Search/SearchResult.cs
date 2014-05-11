using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZF.Data.DAL;
using VZF.Data.Common.Search;
using YAF.Classes;
using YAF.Types;
using YAF.Types.Constants;

namespace VZF.Data.Common
{
    using VZF.Utils;

    public static class SearchResult
    {
        /// <summary>
        /// Returns Search results
        /// </summary>
        /// <param name="ToSearch"></param>
        /// <param name="sf">Field to search</param>
        /// <param name="sw">Search what</param>
        /// <param name="fid"></param>
        /// <param name="UserID">ID of user</param>
        /// <returns>Results</returns>
        public static DataTable GetMySearchResult(
            [NotNull] int? mid,
            string toSearchWhat,
            string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            List<int> categoryId,
            List<int> forumIDToStartAt,
            int userID,
            int boardId,
            int maxResults,
            bool useFullText,
            bool searchDisplayName,
            bool includeChildren)
        {
            bool bFirst = true;
            string forumIds = string.Empty;

            DataTable dt_result = null;
            // if ( ToSearch.Length == 0 )
            //	return new DataTable();

            if (toSearchWhat == "*") toSearchWhat = string.Empty;
            string forumIDs = string.Empty;
            string limitString = string.Empty;
            string orderString = string.Empty;

            //Search not in all forums
            if (forumIDToStartAt.Any())
            {
                var dt = CommonDb.forum_listall_sorted(mid, boardId, userID, null, false, forumIDToStartAt.ToArray<int>(),false);

                forumIDs = dt.Rows.Cast<DataRow>()
                             .Aggregate(
                                 forumIDs, (current, dr) => current + Convert.ToInt32(dr["ForumID"]).ToString() + ",");

                forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            string searchSql = (maxResults == 0) ? "SELECT" : ("SELECT DISTINCTROW ");

            searchSql +=
                " a.ForumID, a.TopicID, a.Topic, b.UserID, IFNULL(c.Username, b.Name) as Name, c.MessageID, c.Posted, '' AS Message, c.Flags";
            searchSql += " from " + ObjectName.GetVzfObjectName("Topic", mid) + " a left join "
                         + ObjectName.GetVzfObjectName("Message", mid) + " c on a.TopicID = c.TopicID left join "
                         + ObjectName.GetVzfObjectName("User", mid) + " b on c.UserID = b.UserID where ";
            searchSql +=
                " IFNULL(CAST(SIGN(c.Flags & 16) AS SIGNED),0) = 1 AND a.TopicMovedID IS NULL AND IFNULL(CAST(SIGN(a.Flags & 8) AS SIGNED),0) = 0 AND IFNULL(CAST(SIGN(c.Flags & 8) AS SIGNED),0) = 0 ";
            orderString += " ORDER BY a.ForumID ";
            limitString += " LIMIT @i_Limit ";
            string[] words;

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                searchSql += "AND (";
                bFirst = true;
                int userId;
                // generate user search sql...
                switch (searchFromWhoMethod)
                {
                    case SearchWhatFlags.AllWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst) searchSql += " AND ";
                            else bFirst = false;

                            if (int.TryParse(word, out userId))
                            {
                                searchSql +=
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
                            if (!bFirst) searchSql += " OR ";
                            else bFirst = false;

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
                                if (!bFirst) ftInner += " AND ";
                                else bFirst = false;
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
                                if (!bFirst) ftInner += " OR ";
                                else bFirst = false;
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
            if (forumIDToStartAt.Any())
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

            using (var sc = new SQLCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_Limit", maxResults.ToString()));              

                sc.CommandText.AppendQuery(searchSql);
                dt_result = sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }

           
            string old_uid = null;
            string old_fid = null;
            foreach (DataRow dr in dt_result.Rows)
            {
                if (old_uid == dr["UserID"].ToString() || old_fid == dr["ForumID"].ToString()) continue;

                        using (var sc = new SQLCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_UserID", dr["UserID"])); 
                            sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_ForumID", dr["ForumID"]));   
                sc.CommandText.AppendQuery(string.Format(
                                "SELECT {0}(@i_UserID,@i_ForumID);",
                              ObjectName.GetVzfObjectName("vaccess_s_readaccess_combo", mid)));             

                    if (Convert.ToInt32(sc.ExecuteScalar(CommandType.Text)) == 0)
                    {
                        dr.Delete();
                    }

                }
                old_uid = dr["UserID"].ToString();
                old_fid = dr["ForumID"].ToString();
            }

            dt_result.AcceptChanges();
            return dt_result;

        }
        public static DataTable GetFbSearchResult(
           [NotNull] int? mid,
           string toSearchWhat,
           string toSearchFromWho,
           SearchWhatFlags searchFromWhoMethod,
           SearchWhatFlags searchWhatMethod,
           List<int> categoryId,
           List<int> forumIdToStartAt,
           int userId,
           int boardId,
           int maxResults,
           bool useFullText,
           bool searchDisplayName,
           bool includeChildren)
        {
            /*     if (toSearchWhat == "*")
                   {
                       toSearchWhat = string.Empty;
                   }

                   IEnumerable<int> forumIds = new List<int>();

                   if (forumIDToStartAt != 0)
                   {
                       forumIds = ForumListAll(boardId, userID, forumIDToStartAt).Select(f => f.ForumID ?? 0).Distinct();
                   }
             string searchSql = new SearchBuilder().BuildSearchSql(toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, userID, searchDisplayName, boardId, maxResults, useFullText, forumIds);
    
                   using (var cmd = FbDbAccess.GetCommand(searchSql, true))
                   {
                       return FbDbAccess.GetData(cmd,connectionString);
                   } 

            */
            // TODO: to move it to a class
            if (toSearchWhat == "*")
            {
                toSearchWhat = string.Empty;
            }

            IEnumerable<int> forumIds = new List<int>();

            if (forumIdToStartAt.Any())
            {
                forumIds =
                    CommonDb.ForumListAll(mid, boardId, userId, forumIdToStartAt, false)
                        .Select(f => f.ForumID ?? 0)
                        .Distinct();
            }

            /*  string searchSql = new SearchBuilder().BuildSearchSql(toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, userID, searchDisplayName, boardId, maxResults, useFullText, forumIds);

              using (var cmd = FbDbAccess.GetCommand(searchSql, true))
              {
                  return FbDbAccess.GetData(cmd,connectionString);
              } */

            // if ( ToSearch.Length == 0 )
            //	return new DataTable();

            if (toSearchWhat == "*")
            {
                toSearchWhat = string.Empty;
            }

            string forumIDs = string.Empty;
            string limitString = string.Empty;
            string orderString = string.Empty;

            if (forumIdToStartAt.Any())
            {
                DataTable dt = CommonDb.forum_listall_sorted(mid, boardId, userId, forumIdToStartAt.ToArray<int>());
                forumIDs = dt.Rows.Cast<DataRow>().Aggregate(forumIDs, (current, dr) => current + Convert.ToInt32(dr["ForumID"]).ToString() + ",");
                forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            string searchSql = (maxResults == 0) ? "SELECT" : ("SELECT ");
            if (maxResults > 0)
            {
                limitString += string.Format(" FIRST {0} ", maxResults.ToString());
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
                                    @"( CONTAINS (c.MESSAGE, ' {0} ') OR CONTAINS (a.TOPIC, ' {0} ' ) )", ftInner);
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
                                    @"( CONTAINS (c.MESSAGE, ' {0} ' ) OR CONTAINS (a.TOPIC, ' {0} ' ) )", ftInner);
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
                                @"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%'  ", toSearchWhat);
                        }
                        break;
                }
                searchSql += ") ";
            }

            // Ederon : 6/16/2007 - forum IDs start above 0, if forum id is 0, there is no forum filtering
            if (forumIdToStartAt.Any())
            {
                searchSql += string.Format(@"AND a.FORUMID IN ({0})", forumIDs);
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
            using (var sc = new SQLCommand(mid))
            {
              //  sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_Limit", maxResults.ToString()));

                sc.CommandText.AppendQuery(searchSql);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }           
        }

        /// <summary>
        /// Returns Search results
        /// </summary>
        /// <param name="ToSearch"></param>
        /// <param name="sf">Field to search</param>
        /// <param name="sw">Search what</param>
        /// <param name="fid"></param>
        /// <param name="UserID">ID of user</param>
        /// <returns>Results</returns>
        public static DataTable GetPgSearchResult(
            [NotNull] int? mid,
            string toSearchWhat,
            string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            List<int> categoryId,
            List<int> forumIDToStartAt,
            int userId,
            int boardId,
            int maxResults,
            bool useFullText,
            bool searchDisplayName,
            bool includeChildren)
        {
            // New access
            /*  if (toSearchWhat == "*")
              {
                  toSearchWhat = string.Empty;
              }

              IEnumerable<int> forumIds = new List<int>();

              if (forumIDToStartAt != 0)
              {
                  forumIds = ForumListAll(boardId, userID, forumIDToStartAt).Select(f => f.ForumID ?? 0).Distinct();
              }

              string searchSql = new SearchBuilder().BuildSearchSql(toSearchWhat, toSearchFromWho, searchFromWhoMethod, searchWhatMethod, userID, searchDisplayName, boardId, maxResults, useFullText, forumIds);

              using (SqlCommand cmd = PostgreDbAccess.GetCommand(searchSql, true))
              {
                  return PostgreDbAccess.Current.GetData(cmd);
              } */


            if (toSearchWhat == "*") toSearchWhat = string.Empty;
            string forumIDs = string.Empty;
            string limitString = string.Empty;
            string orderString = string.Empty;
            string categoriesIds = string.Empty;

            StringBuilder searchSql = new StringBuilder();

            if (categoryId.Any())
            {
                if (Config.LargeForumTree)
                {
                    DataTable dt = CommonDb.forum_categoryaccess_activeuser(mid, boardId, userId);
                    foreach (DataRow c in dt.Rows)
                    {
                        foreach (int c1 in categoryId)
                        {
                            if (Convert.ToInt32(c["CategoryID"])== c1)
                            {
                                categoriesIds = categoriesIds + "," + c1.ToString();
                            }
                        }
                    }
                    categoriesIds = categoriesIds.Trim(',');
                }
            }

            if (forumIDToStartAt.Any())
            {
                if (!Config.LargeForumTree)
                {
                    DataTable dt = CommonDb.forum_listall_sorted(
                        mid, boardId, userId, forumIDToStartAt.ToArray<int>());
                    foreach (DataRow dr in dt.Rows) forumIDs = forumIDs + Convert.ToInt32(dr["ForumID"]).ToString() + ",";
                    forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
                }
                else
                {
                    foreach (int frms in forumIDToStartAt)
                    {
                        var d1 = CommonDb.forum_ns_getchildren_activeuser(
                            mid, boardId, 0, frms, userId, false, false, "-");
                        foreach (DataRow r in d1.Rows)
                        {
                            forumIDs += "," + r["ForumID"];
                        }
                        // Parent forum only.
                        if (!includeChildren)
                        {
                            break;
                        }
                    }
                    forumIDs = forumIDs.Trim(',');
                }
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();
            searchSql.Append(maxResults == 0 ? "SELECT" : ("SELECT DISTINCT "));
            if (maxResults > 0)
            {
                limitString += string.Format(" LIMIT {0} ", maxResults.ToString());
            }

            searchSql.Append(
                " a.forumid, a.topicid, a.topic, b.userid, COALESCE(c.username, b.name) as Name, c.messageid as \"MessageID\", c.posted, c.message as \"Message\", c.flags FROM ");
            searchSql.Append(ObjectName.GetVzfObjectName("topic", mid));
            searchSql.Append(" a JOIN ");
            searchSql.Append(ObjectName.GetVzfObjectName("forum", mid));
            searchSql.Append(" f ON f.forumid = a.forumid LEFT JOIN ");
            searchSql.Append(ObjectName.GetVzfObjectName("message", mid));
            searchSql.Append(" c ON a.topicid = c.topicid LEFT JOIN ");
            searchSql.Append(ObjectName.GetVzfObjectName("user", mid));
            searchSql.Append(" b ON c.userid = b.userid join ");
            searchSql.Append(ObjectName.GetVzfObjectName("vaccess", mid));
            searchSql.Append(" x ON x.forumid=a.forumid ");
            searchSql.Append(
                "WHERE x.readaccess<>0 AND x.userid={0} AND c.isapproved IS TRUE AND a.topicmovedid IS NULL AND a.isdeleted IS FALSE AND c.isdeleted IS FALSE "
                    .FormatWith(userId));
         

            orderString += "ORDER BY a.forumid ";

            string[] words;
            bool bFirst;

            if (!string.IsNullOrEmpty(toSearchFromWho))
            {
                searchSql.Append("AND (");
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
                                searchSql.Append(" AND ");
                            }
                            else
                            {
                                bFirst = false;
                            }
                            // searchSql += string.Format(" ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))", word);

                            if (!int.TryParse(word, out userId))
                            {
                                if (searchDisplayName)
                                {
                                    searchSql.Append(
                                        string.Format(
                                            " ((c.userdisplayname IS NULL AND b.displayname  ~* '.*{0}.*') OR (c.userdisplayname ~* '.*{0}.*'))",
                                            word));
                                }
                                else
                                {
                                    searchSql.Append(
                                        string.Format(
                                            " ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))",
                                            word));
                                }
                            }
                            else
                            {
                                searchSql.Append(string.Format(" (c.userid IN ({0}))", userId));
                            }
                        }
                        break;
                    case SearchWhatFlags.AnyWords:
                        words = toSearchFromWho.Split(' ');
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                searchSql.Append(" OR ");
                            }
                            else
                            {
                                bFirst = false;
                            }
                            searchSql.Append(
                                string.Format(
                                    " ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))", word));
                        }
                        break;
                    case SearchWhatFlags.ExactMatch:
                        // searchSql += string.Format(" ((c.username IS NULL AND b.name = '{0}' ) OR (c.username = '{0}' ))", toSearchFromWho);

                        if (!int.TryParse(toSearchFromWho, out userId))
                        {
                            if (searchDisplayName)
                            {
                                searchSql.Append(
                                    string.Format(
                                        " ((c.userdisplayname IS NULL AND b.displayname = '{0}') OR (c.userdisplayname = '{0}'))",
                                        toSearchFromWho));
                            }
                            else
                            {
                                searchSql.Append(
                                    string.Format(
                                        " ((c.username IS NULL AND b.name = '{0}') OR (c.username = '{0}'))",
                                        toSearchFromWho));
                            }

                        }
                        else
                        {
                            searchSql.Append(string.Format(" (c.userid IN ({0})) ", userId));

                        }
                        break;
                }
                searchSql.Append(") ");
            }


            if (!string.IsNullOrEmpty(toSearchWhat))
            {
                searchSql.Append("AND (");
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
                                if (!bFirst) ftInner += " AND ";
                                else bFirst = false;
                                ftInner += string.Format(@"""{0}""", word);
                            }
                            // make final string...
                            searchSql.Append(
                                string.Format(
                                    "( CONTAINS (c.message, ' {0} ') OR CONTAINS (a.topic, ' {0} ' ) )", ftInner));
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst)
                                {
                                    searchSql.Append(" AND ");
                                }
                                else
                                {
                                    bFirst = false;
                                }
                                searchSql.Append(
                                    string.Format("(c.message ~* '.*{0}.*' OR a.topic ~* '.*{0}.*' )", word));
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
                                if (!bFirst) ftInner += " OR ";
                                else bFirst = false;
                                ftInner += string.Format(@"""{0}""", word);

                                if (int.TryParse(word, out userId))
                                {
                                    searchSql.Append(string.Format(" (c.userid IN ({0}))", userId));
                                }
                                else
                                {
                                    searchSql.Append(
                                        string.Format(
                                            " ((c.username IS NULL AND b.name ~* '.*{0}.*') OR (c.username ~* '.*{0}.*'))",
                                            word));
                                }
                            }
                            // make final string...
                            searchSql.Append(
                                string.Format(
                                    "( CONTAINS (c.message, ' {0} ' ) OR CONTAINS (a.topic, ' {0} ' ) )", ftInner));
                        }
                        else
                        {
                            foreach (string word in words)
                            {
                                if (!bFirst) searchSql.Append(" OR ");
                                else bFirst = false;
                                searchSql.Append(
                                    string.Format("c.message ~* '.*{0}.*'  OR a.topic ~* '.*{0}.*' ", word));
                            }
                        }
                        break;
                    case SearchWhatFlags.ExactMatch:
                        if (useFullText)
                        {
                            searchSql.Append(
                                string.Format(
                                    "( CONTAINS (c.message, ' \"{0}\" ' ) OR CONTAINS (a.topic, ' \"{0}\" '  )",
                                    toSearchWhat));
                        }
                        else
                        {
                            searchSql.Append(
                                string.Format("c.message ~* '.*{0}.*'  OR a.topic ~* '.*{0}.*'  ", toSearchWhat));
                        }
                        break;
                }
                searchSql.Append(") ");
            }

            // vzrus
            if (categoriesIds.IsSet())
            {
                searchSql.Append(string.Format("AND f.categoryid IN ({0})", categoriesIds));
            }

            // Ederon : 6/16/2007 - forum IDs start above 0, if forum id is 0, there is no forum filtering

            if (forumIDs.IsSet())
            {
                searchSql.Append(string.Format("AND a.forumid IN ({0})", forumIDs));
            }


            if (orderString != string.Empty)
            {
                orderString += ", ";
            }
            if (!orderString.Contains("ORDER BY"))
            {
                searchSql.Append(" ORDER BY ");
            }

            searchSql.Append(orderString + "c.posted DESC ");

            if (!orderString.Contains("LIMIT"))
            {
                searchSql.Append(limitString);
            }

            using (var sc = new SQLCommand(mid))
            {
                //  sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_Limit", maxResults.ToString()));

                sc.CommandText.AppendQuery(searchSql.ToString());
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }             
        }
        /// <summary>
        /// Returns Search results
        /// </summary>
        /// <param name="toSearchWhat">
        /// The to Search What.
        /// </param>
        /// <param name="toSearchFromWho">
        /// The to Search From Who.
        /// </param>
        /// <param name="searchFromWhoMethod">
        /// The search From Who Method.
        /// </param>
        /// <param name="searchWhatMethod">
        /// The search What Method.
        /// </param>
        /// <param name="forumIDToStartAt">
        /// The forum ID To Start At.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="boardId">
        /// The board Id.
        /// </param>
        /// <param name="maxResults">
        /// The max Results.
        /// </param>
        /// <param name="useFullText">
        /// The use Full Text.
        /// </param>
        /// <param name="searchDisplayName">
        /// The search Display Name.
        /// </param>
        /// <returns>
        /// Results
        /// </returns>
        public static DataTable GetMsSearchResult([NotNull] int? mid,
            [NotNull] string toSearchWhat,
            [NotNull] string toSearchFromWho,
            SearchWhatFlags searchFromWhoMethod,
            SearchWhatFlags searchWhatMethod,
            List<int> categoryId,
          List<int> forumIDToStartAt,
          int userId,
          int boardId,
          int maxResults,
          bool useFullText,
          bool searchDisplayName, bool includeChildren)
        {
            string forumIDs = string.Empty;
            if (toSearchWhat == "*")
            {
                toSearchWhat = string.Empty;
            }

            if (forumIDToStartAt.Any())
            {
                if (!Config.LargeForumTree)
                {
                    DataTable dt = CommonDb.forum_listall_sorted(
                        mid, boardId, userId, forumIDToStartAt.ToArray<int>());
                    foreach (DataRow dr in dt.Rows) forumIDs = forumIDs + Convert.ToInt32(dr["ForumID"]).ToString() + ",";
                    forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
                }
                else
                {
                    foreach (int frms in forumIDToStartAt)
                    {
                        var d1 = CommonDb.forum_ns_getchildren_activeuser(
                            mid, boardId, 0, frms, userId, false, false, "-");
                        foreach (DataRow r in d1.Rows)
                        {
                            forumIDs += "," + r["ForumID"];
                        }
                        // Parent forum only.
                        if (!includeChildren)
                        {
                            break;
                        }
                    }
                    forumIDs = forumIDs.Trim(',');
                }
            }

            string searchSql = new SearchBuilder().BuildSearchSql(
              toSearchWhat,
              toSearchFromWho,
              searchFromWhoMethod,
              searchWhatMethod,
              userId,
              searchDisplayName,
              boardId,
              maxResults,
              useFullText,
              new[] { forumIDs.ToType<int>() });

            using (var sc = new SQLCommand(mid))
            {
                //  sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "@i_Limit", maxResults.ToString()));

                sc.CommandText.AppendQuery(searchSql.ToString());
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }            
   
        }
    }
}
