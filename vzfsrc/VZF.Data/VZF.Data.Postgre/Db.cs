// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="Db.cs">
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
//   The common db.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Postgre
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Security;

    using Npgsql;

    using NpgsqlTypes;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;

    /// <summary>
    /// All the Database functions for YAF
    /// </summary>
    public static class Db
    {
        // added vzrus
        #region ConnectionStringOptions

        /// <summary>
        /// Gets the provider assembly name.
        /// </summary>
        public static string ProviderAssemblyName
        {
            get
            {
                return "Npgsql";
            }
        }

        /// <summary>
        /// Gets a value indicating whether password placeholder visible.
        /// </summary>
        public static bool PasswordPlaceholderVisible
        {
            get
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// Gets the database size
        /// </summary>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>. value for database size.
        /// </returns>
        public static int GetDBSize([NotNull] string connectionString )
        {
            using (
                var cmd =
                    new NpgsqlCommand(
                        String.Format("select pg_database_size('{0}')/1024/1024;", PostgreDbAccess.DBName)))
            {
                cmd.CommandType = CommandType.Text;
                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /// <summary>
        /// The get is forum installed.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetIsForumInstalled([NotNull] string connectionString )
        {
            try
            {
                using (DataTable dt = board_list(connectionString, DBNull.Value))
                {
                    return dt.Rows.Count > 0;
                }
            }
            catch
            {
            }

            return false;
        }

        /// <summary>
        /// The get db version.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetDBVersion([NotNull] string connectionString )
        {
            try
            {
                using (DataTable dt = Db.registry_list(connectionString, "version"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        // get the version..
                        return Convert.ToInt32(dt.Rows[0]["Value"]);
                    }
                }
            }
            catch
            {
                // not installed..
            }

            return -1;
        }

        public static bool FullTextSupported = false;

        public static string FullTextScript = "postgre/fulltext.sql";


        #region Forum

        public static DataTable forum_ns_getchildren_anyuser(
            [NotNull] string connectionString,
            int boardid,
            int categoryid,
            int forumid,
            int userid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_getchildren_anyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new NpgsqlParameter("i_notincluded", NpgsqlDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new NpgsqlParameter("i_immediateonly", NpgsqlDbType.Boolean)).Value = immediateonly;

                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                DataTable sorted = dt.Clone();
                bool forumRow = false;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = sorted.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    newRow = row;

                    int currentIndent = (int)row["Level"];
                    string sIndent = string.Empty;

                    for (int j = 0; j < currentIndent; j++) sIndent += "--";
                    if (currentIndent == 1 && !forumRow)
                    {
                        newRow["ForumID"] = currentIndent;
                        newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["CategoryName"]);
                        forumRow = true;
                    }
                    else
                    {
                        newRow["ForumID"] = currentIndent;
                        newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["Title"]);
                        forumRow = false;
                    }


                    // import the row into the destination




                    sorted.Rows.Add(newRow);
                }

                return sorted;
            }
        }

        public static DataTable forum_ns_getchildren(
            [NotNull] string connectionString,
            int? boardid,
            int? categoryid,
            int? forumid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_getchildren"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new NpgsqlParameter("i_notincluded", NpgsqlDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new NpgsqlParameter("i_immediateonly", NpgsqlDbType.Boolean)).Value = immediateonly;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable forum_ns_getchildren_activeuser(
            [NotNull] string connectionString,
            int? boardid,
            int? categoryid,
            int? forumid,
            int userid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_getchildren_activeuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new NpgsqlParameter("i_notincluded", NpgsqlDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new NpgsqlParameter("i_immediateonly", NpgsqlDbType.Boolean)).Value = immediateonly;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        public static DataTable forum_listall_sorted(
            [NotNull] string connectionString, object boardId, object userId, int[] forumidExclusions)
        {
            var d = new List<int>();
            d.Add(0);
            return forum_listall_sorted(connectionString, boardId, userId, null, false, d);
        }

        //Here
        public static DataTable forum_listall_sorted(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            int[] forumidExclusions,
            bool emptyFirstRow,
            List<int> startAt)
        {
            using (DataTable dataTable = forum_listall(connectionString, boardId, userId, startAt, false))
            {
                int baseForumId = 0;
                int baseCategoryId = 0;

                if (startAt.Any())
                {
                    // find the base ids..
                    foreach (DataRow dataRow in
                        dataTable.Rows.Cast<DataRow>()
                                 .Where(dataRow => Convert.ToInt32(dataRow["ForumID"]) == startAt.First(f => f > -1)))
                    {
                        baseForumId = dataRow["ParentID"] != DBNull.Value ? Convert.ToInt32(dataRow["ParentID"]) : 0;
                        baseCategoryId = Convert.ToInt32(dataRow["CategoryID"]);
                        break;
                    }
                }

                return forum_sort_list(dataTable, baseForumId, baseCategoryId, 0, forumidExclusions, emptyFirstRow);
            }
        }

        public static void activeaccess_reset([NotNull] string connectionString )
        {
            using (var cmd = PostgreDbAccess.GetCommand("activeaccess_reset"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataRow pageload(
            [NotNull] string connectionString,
            object sessionID,
            object boardId,
            object userKey,
            object ip,
            object location,
            object forumPage,
            object browser,
            object platform,
            object categoryID,
            object forumID,
            object topicID,
            object messageID,
            object isCrawler,
            object isMobileDevice,
            object donttrack)
        {
            int nTries = 0;
            while (true)
            {
                try
                {
                    // var dd = PostgreDbAccess.GetConnectionParams();
                    DataTable dt1 = null;
                    using (var cmd = PostgreDbAccess.GetCommand("pageload"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new NpgsqlParameter("i_sessionid", NpgsqlDbType.Varchar)).Value = sessionID;
                        cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                        cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Varchar)).Value = userKey
                                                                                                           ?? DBNull
                                                                                                                  .Value;
                        cmd.Parameters.Add(new NpgsqlParameter("i_ip", NpgsqlDbType.Varchar)).Value = ip;
                        cmd.Parameters.Add(new NpgsqlParameter("i_location", NpgsqlDbType.Varchar)).Value = location;
                        cmd.Parameters.Add(new NpgsqlParameter("i_forumpage", NpgsqlDbType.Varchar)).Value = forumPage;
                        cmd.Parameters.Add(new NpgsqlParameter("i_browser", NpgsqlDbType.Varchar)).Value = browser;
                        cmd.Parameters.Add(new NpgsqlParameter("i_platform", NpgsqlDbType.Varchar)).Value = platform;
                        cmd.Parameters.Add(new NpgsqlParameter("ii_categoryid", NpgsqlDbType.Integer)).Value =
                            categoryID;
                        cmd.Parameters.Add(new NpgsqlParameter("ii_forumid", NpgsqlDbType.Integer)).Value = forumID;
                        cmd.Parameters.Add(new NpgsqlParameter("ii_topicid", NpgsqlDbType.Integer)).Value = topicID;
                        cmd.Parameters.Add(new NpgsqlParameter("ii_messageid", NpgsqlDbType.Integer)).Value = messageID;
                        cmd.Parameters.Add(new NpgsqlParameter("i_iscrawler", NpgsqlDbType.Boolean)).Value = isCrawler;
                        cmd.Parameters.Add(new NpgsqlParameter("i_ismobiledevice", NpgsqlDbType.Boolean)).Value =
                            isMobileDevice;
                        cmd.Parameters.Add(new NpgsqlParameter("i_donttrack", NpgsqlDbType.Boolean)).Value = donttrack;
                        cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                            DateTime.UtcNow;

                        dt1 = PostgreDbAccess.GetData(cmd, false, connectionString);
                        return dt1.Rows[0];

                        /*   if (dt1.Columns.Count == 0) throw new ArgumentOutOfRangeException();
                        using (var cmd1 = PostgreDbAccess.GetCommand("vaccess_combo"))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = dt1.Rows[0]["UserID"];
                            cmd1.Parameters.Add("i_forumid", NpgsqlDbType.Integer).Value = dt1.Rows[0]["ForumID"];
                            //We  trigger AcceptChanges() right now as we don't need to return more rows
                            return PostgreDbAccess.Current.AddValuesToDataTableFromReader(cmd1, dt1, false, true, dt1.Columns.Count).Rows[0];
                        } */


                    }
                }
                catch (NpgsqlException x)
                {
                    if (x.ErrorCode == 1205 && nTries < 3)
                    {
                        /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                    }
                    else
                        throw new ApplicationException(
                            string.Format("Sql Exception with error number {0} (Tries={1})", x.Code, nTries), x);
                }
                ++nTries;
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
        public static DataTable GetSearchResult(
            [NotNull] string connectionString,
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
                    DataTable dt = Db.forum_categoryaccess_activeuser(connectionString, boardId, userId);
                    foreach (DataRow c in dt.Rows)
                    {
                        foreach (int c1 in categoryId)
                        {
                            if (c["CategoryID"].ToType<int>() == c1)
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
                    DataTable dt = forum_listall_sorted(
                        connectionString, boardId, userId, null, false, forumIDToStartAt);
                    foreach (DataRow dr in dt.Rows) forumIDs = forumIDs + Convert.ToInt32(dr["ForumID"]).ToString() + ",";
                    forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
                }
                else
                {
                    foreach (int frms in forumIDToStartAt)
                    {
                        var d1 = Db.forum_ns_getchildren_activeuser(
                            connectionString, boardId, 0, frms, userId, false, false, "-");
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
                limitString += String.Format(" LIMIT {0} ", maxResults.ToString());
            }

            searchSql.Append(
                " a.forumid, a.topicid, a.topic, b.userid, COALESCE(c.username, b.name) as Name, c.messageid as \"MessageID\", c.posted, c.message as \"Message\", c.flags FROM ");
            searchSql.Append(PostgreDbAccess.GetObjectName("topic"));
            searchSql.Append(" a JOIN ");
            searchSql.Append(PostgreDbAccess.GetObjectName("forum"));
            searchSql.Append(" f ON f.forumid = a.forumid LEFT JOIN ");
            searchSql.Append(PostgreDbAccess.GetObjectName("message"));
            searchSql.Append(" c ON a.topicid = c.topicid LEFT JOIN ");
            searchSql.Append(PostgreDbAccess.GetObjectName("user"));
            searchSql.Append(" b ON c.userid = b.userid join ");
            searchSql.Append(PostgreDbAccess.GetObjectName("vaccess"));
            searchSql.Append(" x ON x.forumid=a.forumid ");
            searchSql.Append(
                "WHERE x.readaccess<>0 AND x.userid={0} AND c.isapproved IS TRUE AND a.topicmovedid IS NULL AND a.isdeleted IS FALSE AND c.isdeleted IS FALSE "
                    .FormatWith(userId));


            orderString += "ORDER BY a.forumid ";

            string[] words;
            bool bFirst;

            if (!String.IsNullOrEmpty(toSearchFromWho))
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


            if (!String.IsNullOrEmpty(toSearchWhat))
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
                                ftInner += String.Format(@"""{0}""", word);

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


            using (var cmd = PostgreDbAccess.GetCommand(searchSql.ToString(), true))
            {
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region DataSets

        public static void forum_list_sort_basic(DataTable listsource, DataTable list, int parentid, int currentLvl)
        {
            for (int i = 0; i < listsource.Rows.Count; i++)
            {
                DataRow row = listsource.Rows[i];
                if ((row["ParentID"]) == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentid)
                {
                    string sIndent = string.Empty;
                    int iIndent = Convert.ToInt32(currentLvl);
                    for (int j = 0; j < iIndent; j++) sIndent += "--";
                    row["Name"] = string.Format(" -{0} {1}", sIndent, row["Name"]);
                    list.Rows.Add(row.ItemArray);
                    forum_list_sort_basic(listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
            }
        }

        /// <summary>
        /// Gets a list of categories????
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <returns>DataSet with categories</returns>
        public static DataSet ds_forumadmin([NotNull] string connectionString, object boardId, object pageUserID, object isUserForum)
        {
            using (var connMan = new PostgreDbConnectionManager(connectionString))
            {
                using (var ds = new DataSet())
                {
                    using (var trans = connMan.OpenDBConnection(connectionString).BeginTransaction(PostgreDbAccess.IsolationLevel))
                    {
                        using (var da = new NpgsqlDataAdapter(PostgreDbAccess.GetObjectName("category_list"), connMan.DBConnection))
                        {
                            da.SelectCommand.Transaction = trans;

                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer))
                              .Value = boardId;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer))
                              .Value = DBNull.Value;

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.Fill(ds, PostgreDbAccess.GetObjectName("Category"));
                            da.SelectCommand.Parameters.Clear();
                            da.SelectCommand.CommandText = PostgreDbAccess.GetObjectName("forum_list");
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer))
                            .Value = boardId;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = DBNull.Value;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = pageUserID;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_isuserforum", NpgsqlDbType.Boolean)).Value = isUserForum;
                            da.Fill(ds, PostgreDbAccess.GetObjectName("ForumUnsorted"));
                            DataTable dtForumListSorted =
                                ds.Tables[PostgreDbAccess.GetObjectName("ForumUnsorted")].Clone();
                            dtForumListSorted.TableName = PostgreDbAccess.GetObjectName("Forum");
                            ds.Tables.Add(dtForumListSorted);
                            dtForumListSorted.Dispose();
                            Db.forum_list_sort_basic(
                                ds.Tables[PostgreDbAccess.GetObjectName("ForumUnsorted")],
                                ds.Tables[PostgreDbAccess.GetObjectName("Forum")],
                                0,
                                0);
                            ds.Tables.Remove(PostgreDbAccess.GetObjectName("ForumUnsorted"));
                            ds.Relations.Add(
                                "FK_Forum_Category",
                                ds.Tables[PostgreDbAccess.GetObjectName("Category")].Columns["CategoryID"],
                                ds.Tables[PostgreDbAccess.GetObjectName("Forum")].Columns["CategoryID"]);
                            trans.Commit();
                        }

                        return ds;
                    }
                }
            }
        }

        #endregion

        #region yaf_Attachment

        /// <summary>
        /// Gets a list of attachments
        /// </summary>
        /// <param name="messageID">messageID</param>
        /// <param name="attachmentID">attachementID</param>
        /// <param name="boardId">boardId</param>
        /// <returns>DataTable with attachement list</returns>
        public static DataTable attachment_list(
            [NotNull] string connectionString,
            [NotNull] object messageID,
            [NotNull] object attachmentID,
            [NotNull] object boardID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("attachment_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_attachmentid", NpgsqlDbType.Integer)).Value = attachmentID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageisize", NpgsqlDbType.Integer)).Value = pageSize;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// saves attachment
        /// </summary>
        /// <param name="messageID">messageID</param>
        /// <param name="fileName">File Name</param>
        /// <param name="bytes">number of bytes</param>
        /// <param name="contentType">type of attchment</param>
        /// <param name="stream">stream of bytes</param>
        public static void attachment_save(
            [NotNull] string connectionString,
            object messageID,
            object fileName,
            object bytes,
            object contentType,
            System.IO.Stream stream)
        {
            using (var cmd = PostgreDbAccess.GetCommand("attachment_save"))
            {
                byte[] fileData = null;
                if (stream != null)
                {
                    fileData = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(fileData, 0, (int)stream.Length);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_filename", NpgsqlDbType.Varchar)).Value = fileName;
                cmd.Parameters.Add(new NpgsqlParameter("i_bytes", NpgsqlDbType.Integer)).Value = bytes;
                cmd.Parameters.Add(new NpgsqlParameter("i_contenttype", NpgsqlDbType.Varchar)).Value = contentType;
                cmd.Parameters.Add(new NpgsqlParameter("i_filedata", NpgsqlDbType.Bytea)).Value = fileData;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        //ABOT CHANGE 16.04.04
        /// <summary>
        /// Delete attachment
        /// </summary>
        /// <param name="attachmentID">ID of attachment to delete</param>
        public static void attachment_delete([NotNull] string connectionString, object attachmentID)
        {
            bool UseFileTable = GetBooleanRegistryValue(connectionString, "UseFileTable");

            //If the files are actually saved in the Hard Drive
            if (!UseFileTable)
            {
                using (var cmd = PostgreDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = DBNull.Value;
                    cmd.Parameters.Add(new NpgsqlParameter("i_attachmentid", NpgsqlDbType.Integer)).Value = attachmentID;
                    cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = DBNull.Value;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = 0;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pageisize", NpgsqlDbType.Integer)).Value = 1000;

                    DataTable tbAttachments = PostgreDbAccess.GetData(cmd, connectionString);
                    string uploadDir =
                        HostingEnvironment.MapPath(
                            String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads));


                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format(
                                "{0}/{1}.{2}.yafupload", uploadDir, row["MessageID"], row["FileName"]);
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch
                        {
                            // error deleting that file.. 
                        }
                    }
                }
            }
            using (var cmd = PostgreDbAccess.GetCommand("attachment_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_attachmentid", NpgsqlDbType.Integer)).Value = attachmentID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
            //End ABOT CHANGE 16.04.04
        }


        /// <summary>
        /// Attachement dowload
        /// </summary>
        /// <param name="attachmentID">ID of attachemnt to download</param>
        public static void attachment_download([NotNull] string connectionString, object attachmentID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("attachment_download"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_attachmentid", NpgsqlDbType.Integer)).Value = attachmentID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_BannedIP

        /// <summary>
        /// List of Baned IP's
        /// </summary>
        /// <param name="boardId">ID of board</param>
        /// <param name="ID">ID</param>
        /// <returns>DataTable of banned IPs</returns>
        public static DataTable bannedip_list(
            [NotNull] string connectionString,
            [NotNull] object boardID,
            [CanBeNull] object ID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bannedip_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = ID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves baned ip in database
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="boardId">BoardID</param>
        /// <param name="Mask">Mask</param>
        public static void bannedip_save(
            [NotNull] string connectionString, object ID, object boardId, object Mask, string reason, int userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bannedip_save"))
            {
                //Regex for ip
                //  \b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = ID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_mask", NpgsqlDbType.Varchar)).Value = Mask;
                cmd.Parameters.Add(new NpgsqlParameter("i_reason", NpgsqlDbType.Varchar)).Value = reason;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes Banned IP
        /// </summary>
        /// <param name="ID">ID of banned ip to delete</param>
        public static void bannedip_delete([NotNull] string connectionString, object ID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bannedip_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = ID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Board

        /// <summary>
        /// Gets a list of information about a board
        /// </summary>
        /// <param name="boardId">board id</param>
        /// <returns>DataTable</returns>
        public static DataTable board_list([NotNull] string connectionString, object boardId)
        {
            
            using (var cmd = PostgreDbAccess.GetCommand("board_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets posting statistics
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="useStyledNicks">useStyledNicks</param>
        /// <returns>DataRow of Poststats</returns>
        public static DataRow board_poststats(
            [NotNull] string connectionString, int? boardId, bool useStyledNicks, bool showNoCountPosts)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_poststats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_usestylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_shownocountposts", NpgsqlDbType.Boolean)).Value =
                    showNoCountPosts;
                cmd.Parameters.Add(new NpgsqlParameter("i_getdefaults", NpgsqlDbType.Boolean)).Value = false;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }

            using (var cmd = PostgreDbAccess.GetCommand("board_poststats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_usestylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_shownocountposts", NpgsqlDbType.Boolean)).Value =
                    showNoCountPosts;
                cmd.Parameters.Add(new NpgsqlParameter("i_getdefaults", NpgsqlDbType.Boolean)).Value = true;

                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets users statistics
        /// </summary>
        /// <param name="boardID">
        /// BoardID
        /// </param>
        /// <returns>
        /// DataRow of Poststats
        /// </returns>
        public static DataRow board_userstats([NotNull] string connectionString, int? boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_userstats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows[0];
                }
            }
        }

        /// <summary>
        /// Recalculates topic and post numbers and updates last post for specified board
        /// </summary>
        /// <param name="boardId">BoardID of board to do re-sync for, if null, all boards are re-synced</param>
        public static void board_resync([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_resync"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets statistica about number of posts etc.
        /// </summary>
        /// <returns>DataRow</returns>

        public static DataRow board_stats([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows[0];
                }
            }
        }

        /// <summary>
        /// Saves board information
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="name">Name of Board</param>
        /// <param name="allowThreaded">Boolen value, allowThreaded</param>
        public static int board_save(
            [NotNull] string connectionString,
            object boardId,
            object languageFile,
            object culture,
            object name,
            object allowThreaded)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_languagefile", NpgsqlDbType.Varchar)).Value = languageFile;
                cmd.Parameters.Add(new NpgsqlParameter("i_culture", NpgsqlDbType.Varchar)).Value = culture;
                cmd.Parameters.Add(new NpgsqlParameter("i_allowthreaded", NpgsqlDbType.Boolean)).Value = allowThreaded;

                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /// <summary>
        /// Creates a new board
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="adminUserName">
        /// The admin username.
        /// </param>
        /// <param name="adminUserEmail">
        /// The admin user email.
        /// </param>
        /// <param name="adminUserKey">
        /// The admin user key.
        /// </param>
        /// <param name="boardName">
        /// The board name.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="languageFile">
        /// The language file.
        /// </param>
        /// <param name="boardMembershipName">
        /// The board membership name.
        /// </param>
        /// <param name="boardRolesName">
        /// The board roles name.
        /// </param>
        /// <param name="rolePrefix">
        /// The role prefix.
        /// </param>
        /// <param name="isHostUser">
        /// The is host user.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> key for a created board.
        /// </returns>
        public static int board_create(
            [NotNull] string connectionString,
            [NotNull] object adminUserName,
            [NotNull] object adminUserEmail,
            [NotNull] object adminUserKey,
            [NotNull] object boardName,
            [NotNull] object culture,
            [NotNull] object languageFile,
            [NotNull] object boardMembershipName,
            [NotNull] object boardRolesName,
            [NotNull] object rolePrefix,
            [NotNull] object isHostUser)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_create"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardname", NpgsqlDbType.Varchar)).Value = boardName;
                cmd.Parameters.Add(new NpgsqlParameter("i_languagefile", NpgsqlDbType.Varchar)).Value = languageFile;
                cmd.Parameters.Add(new NpgsqlParameter("i_culture", NpgsqlDbType.Varchar)).Value = culture;
                cmd.Parameters.Add(new NpgsqlParameter("i_membershipappname", NpgsqlDbType.Varchar)).Value =
                    boardMembershipName;
                cmd.Parameters.Add(new NpgsqlParameter("i_rolesappname", NpgsqlDbType.Varchar)).Value = boardRolesName;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = adminUserName;
                cmd.Parameters.Add(new NpgsqlParameter("i_useremail", NpgsqlDbType.Varchar)).Value = adminUserEmail;
                cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)).Value = adminUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_ishostadmin", NpgsqlDbType.Boolean)).Value = isHostUser;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_roleprefix", NpgsqlDbType.Varchar)).Value = rolePrefix;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                int res = Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
                forum_ns_recreate(connectionString);
                return res;
            }
        }

        /// <summary>
        /// Deletes a board
        /// </summary>
        /// <param name="boardId">ID of board to delete</param>
        public static void board_delete([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("board_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Category

        /// <summary>
        /// Deletes a category
        /// </summary>
        /// <param name="CategoryID">ID of category to delete</param>
        /// <returns>Bool value indicationg if category was deleted</returns>
        public static bool category_delete([NotNull] string connectionString, object CategoryID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = CategoryID;
                bool res = (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString) != 0;
                forum_ns_recreate(connectionString);
                return res;
            }
        }

        /// <summary>
        /// Gets a list of forums in a category
        /// </summary>
        /// <param name="boardId">boardId</param>
        /// <param name="categoryID">categotyID</param>
        /// <returns>DataTable with a list of forums in a category</returns>
        public static DataTable category_list([NotNull] string connectionString, object boardId, object categoryID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;
                
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a list of forums in a category
        /// </summary>
        /// <param name="boardId">boardId</param>
        /// <param name="categoryID">categotyID</param>
        /// <returns>DataTable with a list of forums in a category</returns>
        public static DataTable category_pfaccesslist([NotNull] string connectionString, object boardId, object categoryID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_pfaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// The category_getadjacentforum.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <param name="isAfter">
        /// The is after.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable category_getadjacentforum([NotNull] string connectionString, [NotNull] object boardId, [CanBeNull] object categoryID, object userId, bool isAfter)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_getadjacentforum"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_isafter", NpgsqlDbType.Boolean)).Value = isAfter;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a list of forum categories.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable category_listread(
            [NotNull] string connectionString, object boardId, object userId, object categoryID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_listread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists categories very simply (for URL rewriting)
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable category_simplelist([NotNull] string connectionString, int startID, int limit)
        {
            using (var cmd = PostgreDbAccess.GetCommand("category_simplelist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_startid", NpgsqlDbType.Integer)).Value = startID;
                cmd.Parameters.Add(new NpgsqlParameter("i_limit", NpgsqlDbType.Integer)).Value = limit;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves changes to a category
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="CategoryID">CategoryID so save changes to</param>
        /// <param name="Name">Name of the category</param>
        /// <param name="SortOrder">Sort Order</param>
        public static void category_save(
            [NotNull] string connectionString,
            object boardId,
            object categoryId,
            object name,
            object categoryImage,
            object sortOrder, 
            object canHavePersForums)
        {

            int sortOrderChecked = 0;
            bool result = Int32.TryParse(sortOrder.ToString(), out sortOrderChecked);
            if (result)
            {
                if (sortOrderChecked >= 255)
                {
                    sortOrderChecked = 0;
                }
            }
            using (var cmd = PostgreDbAccess.GetCommand("category_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Integer)).Value = sortOrderChecked;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryimage", NpgsqlDbType.Varchar)).Value = categoryImage;
                cmd.Parameters.Add(new NpgsqlParameter("i_canhavepersforums", NpgsqlDbType.Boolean)).Value = canHavePersForums;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                forum_ns_recreate(connectionString);
            }
        }

        #endregion

        #region yaf_CheckEmail

        /// <summary>
        /// Saves a new email into the table for verification
        /// </summary>
        /// <param name="UserID">ID of user to verify</param>
        /// <param name="Hash">Hash of user</param>
        /// <param name="Email">email of user</param>
        public static void checkemail_save([NotNull] string connectionString, object userId, object hash, object email)
        {
            using (var cmd = PostgreDbAccess.GetCommand("checkemail_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_iserid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_hash", NpgsqlDbType.Varchar)).Value = hash;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Updates a hash
        /// </summary>
        /// <param name="hash">New hash</param>
        /// <returns>DataTable with user information</returns>
        public static DataTable checkemail_update([NotNull] string connectionString, object hash)
        {
            using (var cmd = PostgreDbAccess.GetCommand("checkemail_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_hash", NpgsqlDbType.Varchar)).Value = hash;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a check email entry based on email or all if no email supplied
        /// </summary>
        /// <param name="email">Associated email</param>
        /// <returns>DataTable with check email information</returns>
        public static DataTable checkemail_list([NotNull] string connectionString, object email)
        {
            using (var cmd = PostgreDbAccess.GetCommand("checkemail_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Choice

        /// <summary>
        /// Saves a vote in the database
        /// </summary>
        /// <param name="choiceID">Choice of the vote</param>
        public static void choice_vote([NotNull] string connectionString, object choiceID, object userId, object remoteIP)
        {
            using (var cmd = PostgreDbAccess.GetCommand("choice_vote"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_choiceid", NpgsqlDbType.Integer)).Value = choiceID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_remoteip", NpgsqlDbType.Varchar)).Value = remoteIP;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_EventLog

        public static void eventlog_create([NotNull] string connectionString, object userId, object source, object description)
        {
            eventlog_create(connectionString, userId, (object)source.GetType().ToString(), description, (object)0);
        }

        public static void eventlog_create(
            [NotNull] string connectionString, object userId, object source, object description, object type)
        {
            try
            {
                if (userId == null) userId = DBNull.Value;
                using (var cmd = PostgreDbAccess.GetCommand("eventlog_create"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_source", NpgsqlDbType.Varchar)).Value = source.ToString();
                    cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Text)).Value =
                        description.ToString();
                    cmd.Parameters.Add(new NpgsqlParameter("i_type", NpgsqlDbType.Integer)).Value = type;
                    cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                        DateTime.UtcNow;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }


        public static void eventlog_delete(int boardId, object pageuserId)
        {
            eventlog_delete(null, boardId, pageuserId);
        }

        /// <summary>
        /// Deletes event log entry of given ID.
        /// </summary>
        /// <param name="eventLogID">ID of event log entry.</param>
        public static void eventlog_delete([NotNull] string connectionString, object eventLogID, object pageuserId)
        {
            eventlog_delete(connectionString, eventLogID, null, pageuserId);
        }

        /// <summary>
        /// Calls underlying stroed procedure for deletion of event log entry(ies).
        /// </summary>
        /// <param name="eventLogID">When not null, only given event log entry is deleted.</param>
        /// <param name="boardId">Specifies board. It is ignored if eventLogID parameter is not null.</param>
        public static void eventlog_delete(
            [NotNull] string connectionString, object eventLogID, object boardId, object pageUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventlog_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_eventlogid", NpgsqlDbType.Integer)).Value = eventLogID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes events of a type.
        /// </summary>
        /// <param name="boardId">
        /// The board Id.
        /// </param>
        /// <param name="pageUserId">
        /// The page User Id.
        /// </param>
        public static void eventlog_deletebyuser(
            [NotNull] string connectionString, [NotNull] object boardId, [NotNull] object pageUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventlog_deletebyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The eventlog_list.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <returns>
        /// A list of events for the pageUserID access level. 
        /// </returns>
        public static DataTable eventlog_list(
            [NotNull] string connectionString,
            [NotNull] object boardID,
            [NotNull] object pageUserID,
            [NotNull] object maxRows,
            [NotNull] object maxDays,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object eventIDs)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventlog_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_maxrows", NpgsqlDbType.Integer)).Value = maxRows;
                cmd.Parameters.Add(new NpgsqlParameter("i_maxdays", NpgsqlDbType.Integer)).Value = maxDays;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventids", NpgsqlDbType.Varchar)).Value = eventIDs;

                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves access entry for a log type for a user.
        /// </summary>
        /// <param name="groupID">
        /// The group Id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event Type Id.
        /// </param>
        /// <param name="eventTypeName">
        /// The event Type Name.
        /// </param>
        public static void eventloggroupaccess_save(
            [NotNull] string connectionString,
            [NotNull] object groupID,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName,
            [NotNull] object deleteAccess)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventloggroupaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventtypeid", NpgsqlDbType.Integer)).Value = eventTypeId;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventtypename", NpgsqlDbType.Varchar)).Value = eventTypeName;
                cmd.Parameters.Add(new NpgsqlParameter("i_deleteaccess", NpgsqlDbType.Boolean)).Value = deleteAccess;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes event log access entries from table.
        /// </summary>
        /// <param name="groupID">
        /// The group Id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event Type Id.
        /// </param>
        /// <param name="eventTypeName">
        /// The event Type Name.
        /// </param>
        public static void eventloggroupaccess_delete(
            [NotNull] string connectionString,
            [NotNull] object groupID,
            [NotNull] object eventTypeId,
            [NotNull] object eventTypeName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventloggroupaccess_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventtypeid", NpgsqlDbType.Integer)).Value = eventTypeId;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventtypename", NpgsqlDbType.Varchar)).Value = eventTypeName;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns a list of access entries for a group.
        /// </summary>
        /// <param name="groupID">
        /// The group Id.
        /// </param>
        /// <param name="eventTypeId">
        /// The event Type Id.
        /// </param>
        /// <returns>Returns a list of access entries for a group.</returns>
        public static DataTable eventloggroupaccess_list(
            [NotNull] string connectionString, [NotNull] object groupID, [NotNull] object eventTypeId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("eventloggroupaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_eventtypeid", NpgsqlDbType.Integer)).Value = eventTypeId;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists group for the board Id handy to display on the calling admin page. 
        /// </summary>
        /// <param name="boardId">
        /// The board Id.
        /// </param>
        /// <returns>Lists group for the board Id handy to display on the calling admin page.
        /// </returns>
        public static DataTable group_eventlogaccesslist([NotNull] string connectionString, [CanBeNull] object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_eventlogaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        #endregion yaf_EventLog

        // Admin control of file extensions - MJ Hufford
        #region yaf_Extensions

        /// <summary>
        /// The extension_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="extensionId">
        /// The extension id.
        /// </param>
        public static void extension_delete([NotNull] string connectionString, object extensionId)
        {
            try
            {
                using (var cmd = PostgreDbAccess.GetCommand("extension_delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_extensionid", NpgsqlDbType.Integer)).Value = extensionId;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        // Get Extension record by extensionId
        public static DataTable extension_edit([NotNull] string connectionString, object extensionId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("extension_edit"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_extensionid", NpgsqlDbType.Integer)).Value = extensionId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }

        }

        // Returns an extension list for a given Board
        public static DataTable extension_list([NotNull] string connectionString, object boardId)
        {
            return extension_list(connectionString, boardId, null);

        }

        // Used to validate a file before uploading
        public static DataTable extension_list([NotNull] string connectionString, object boardId, object extension)
        {
            using (var cmd = PostgreDbAccess.GetCommand("extension_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_extension", NpgsqlDbType.Varchar)).Value = extension;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }

        }

        // Saves / creates extension
        public static void extension_save([NotNull] string connectionString, object extensionId, object boardId, object extension)
        {
            try
            {
                using (var cmd = PostgreDbAccess.GetCommand("extension_save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_extensionid", NpgsqlDbType.Integer)).Value = extensionId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_extension", NpgsqlDbType.Varchar)).Value = extension;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        #endregion yaf_EventLog

        #region yaf_PollVote

        /// <summary>
        /// Checks for a vote in the database 
        /// </summary>
        /// <param name="pollGroupId">
        /// The pollGroupid.
        /// </param>
        /// <param name="userId">
        /// The userid.
        /// </param>
        /// <param name="remoteIp">
        /// The remoteip.
        /// </param>
        public static DataTable pollgroup_votecheck(
            [NotNull] string connectionString, object pollGroupId, object userId, object remoteIp)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollgroup_votecheck"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("i_pollgroupid", pollGroupId);
                cmd.Parameters.AddWithValue("i_userid", userId);
                cmd.Parameters.AddWithValue("i_remoteip", remoteIp);
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Checks for a vote in the database
        /// </summary>
        /// <param name="choiceID">Choice of the vote</param>
        public static DataTable pollvote_check([NotNull] string connectionString, object pollid, object userid, object remoteip)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollvote_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollid;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new NpgsqlParameter("i_remoteip", NpgsqlDbType.Varchar)).Value = remoteip;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Forum

        /// <summary>
        /// Get the list of recently logged in users.
        /// </summary>
        /// <param name="boardID">
        /// The board ID.
        /// </param>
        /// <param name="timeSinceLastLogin">
        /// The time since last login in minutes.
        /// </param>
        /// <param name="styledNicks">
        /// The styled Nicks.
        /// </param>
        /// <returns>
        /// The list of users in Datatable format.
        /// </returns>
        public static DataTable recent_users(
            [NotNull] string connectionString, object boardID, int timeSinceLastLogin, object styledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("recent_users"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_timesincelastlogin", NpgsqlDbType.Integer)).Value =
                    timeSinceLastLogin;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// List of categories accessible for an active user
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>A <see cref="T:System.Data.DataTable"/> of categories.</returns>
        public static DataTable forum_categoryaccess_activeuser([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_categoryaccess_activeuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        //ABOT NEW 16.04.04
        /// <summary>
        /// Deletes attachments out of a entire forum
        /// </summary>
        /// <param name="ForumID">ID of forum to delete all attachemnts out of</param>
        private static void forum_deleteAttachments([NotNull] string connectionString, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listtopics"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                using (var dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    foreach (DataRow row in
                        dt.Rows.Cast<DataRow>().Where(row => row != null && row["TopicID"] != DBNull.Value))
                    {
                        topic_delete(connectionString, row["TopicID"], true);
                    }
                }
            }
        }

        //END ABOT NEW 16.04.04
        //ABOT CHANGE 16.04.04
        /// <summary>
        /// Deletes a forum
        /// </summary>
        /// <param name="ForumID">forum to delete</param>
        /// <returns>bool to indicate that forum has been deleted</returns>
        public static bool forum_delete([NotNull] string connectionString, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listSubForums"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                if (!(PostgreDbAccess.ExecuteScalar(cmd, connectionString) is DBNull)) return false;
                else
                {
                    forum_deleteAttachments(connectionString, forumID);
                    using (var cmd_new = PostgreDbAccess.GetCommand("forum_delete"))
                    {
                        cmd_new.CommandType = CommandType.StoredProcedure;
                        cmd_new.CommandTimeout = 99999;
                        cmd_new.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                        PostgreDbAccess.ExecuteNonQuery(cmd_new, connectionString);
                    }

                    forum_ns_recreate(connectionString);
                    return true;
                }
            }
        }

        /// <summary>
        /// The forum_tags.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_tags([NotNull] string connectionString, int boardId, int pageUserId, int forumId, int pageIndex, int pageSize, string searchText, bool beginsWith)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_tags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardId;
                cmd.Parameters.Add("i_pageuserid", NpgsqlDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("i_forumid", NpgsqlDbType.Integer).Value = forumId;
                cmd.Parameters.Add("i_pageindex", NpgsqlDbType.Integer).Value = pageIndex;
                cmd.Parameters.Add("i_pagesize", NpgsqlDbType.Integer).Value = pageSize;
                cmd.Parameters.Add("i_searchtext", NpgsqlDbType.Varchar).Value = searchText;
                cmd.Parameters.Add("i_beginswith", NpgsqlDbType.Boolean).Value = beginsWith;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The forum_move.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="forumOldID">
        /// The forum old id.
        /// </param>
        /// <param name="forumNewID">
        /// The forum new id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool forum_move([NotNull] string connectionString, [NotNull] object forumOldID, [NotNull] object forumNewID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listSubForums"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumOldID;

                if (!(PostgreDbAccess.ExecuteScalar(cmd, connectionString) is DBNull))
                {
                    return false;
                }

                using (var cmd_new = PostgreDbAccess.GetCommand("forum_move"))
                {
                    cmd_new.CommandType = CommandType.StoredProcedure;
                    cmd_new.CommandTimeout = 99999;
                    cmd.Parameters.Add(new NpgsqlParameter("i_forumoldid", NpgsqlDbType.Integer)).Value = forumOldID;
                    cmd.Parameters.Add(new NpgsqlParameter("i_forumnewid", NpgsqlDbType.Integer)).Value = forumNewID;

                    PostgreDbAccess.ExecuteNonQuery(cmd_new, connectionString);
                }

                return true;
            }
        }

        /// <summary>
        /// Lists all moderated forums for a user
        /// </summary>
        /// <param name="boardId">board if of moderators</param>
        /// <param name="userId">user id</param>
        /// <returns>DataTable of moderated forums</returns>
        public static DataTable forum_listallMyModerated([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listallmymoderated"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        //END ABOT NEW 16.04.04
        /// <summary>
        /// Gets a list of topics in a forum
        /// </summary>
        /// <param name="boardId">boardId</param>
        /// <param name="ForumID">forumID</param>
        /// <returns>DataTable with list of topics from a forum</returns>
        public static DataTable forum_list([NotNull] string connectionString, object boardId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_list"))
            {
                if (forumID == null)
                {
                    forumID = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_isuserforum", NpgsqlDbType.Boolean)).Value = false;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable forum_byflags(string connectionString, object forumId, object flags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_byflags"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumId
                                                                                                   ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static
            DataTable forum_byuserlist([NotNull] string connectionString, object boardId, object forumID, object userId, object isUserForum)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_byuserlist"))
            {
                if (forumID == null)
                {
                    forumID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_isuserforum", NpgsqlDbType.Boolean)).Value = isUserForum;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a max id of forums.
        /// </summary>
        /// <param name="boardID">
        /// boardID
        /// </param>
        /// <returns>
        /// Max forum id for a board
        /// </returns>
        public static int forum_maxid([NotNull] string connectionString, [NotNull] object boardID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_maxid"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /// <summary>
        /// Lists all forums accessible to a user
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="userId">ID of user</param>
        /// <param name="startAt">startAt ID</param>
        /// <returns>DataTable of all accessible forums</returns>
        public static DataTable forum_listall(
            [NotNull] string connectionString, object boardId, object userId, object startAt, bool returnAll)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listall"))
            {
                if (startAt == null)
                {
                    startAt = 0;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_root", NpgsqlDbType.Integer)).Value = startAt;
                cmd.Parameters.Add(new NpgsqlParameter("i_returnall", NpgsqlDbType.Boolean)).Value = returnAll;
                var dd = PostgreDbAccess.GetData(cmd, connectionString);
                return dd;
            }
        }


        public static IEnumerable<TypedForumListAll> ForumListAll([NotNull] string connectionString, int boardId, int userId)
        {
            return
                forum_listall(connectionString, boardId, userId, 0, false)
                    .AsEnumerable()
                    .Select(r => new TypedForumListAll(r));
        }

        public static IEnumerable<TypedForumListAll> ForumListAll(
            [NotNull] string connectionString, int boardId, int userId, List<int> startForumId)
        {
            var allForums = ForumListAll(connectionString, boardId, userId);

            var forumIds = new List<int>();
            var tempForumIds = new List<int>();

            int ff = 0;
            if (startForumId.Any())
            {
                ff = startForumId.First(s => s > -1);
            }
            forumIds.Add(ff);
            tempForumIds.Add(ff);

            IEnumerable<TypedForumListAll> typedForumListAlls = allForums as List<TypedForumListAll>
                                                                ?? allForums.ToList();
            while (true)
            {
                var ids = tempForumIds;
                var temp = typedForumListAlls.Where(f => ids.Contains(f.ParentID ?? 0));

                var forumListAlls = temp as List<TypedForumListAll> ?? temp.ToList();
                if (!forumListAlls.Any())
                {
                    break;
                }

                // replace temp forum ids with these..
                tempForumIds = forumListAlls.Select(f => f.ForumID ?? 0).Distinct().ToList();

                // add them..
                forumIds.AddRange(tempForumIds);
            }

            // return filtered forums..
            return typedForumListAlls.Where(f => forumIds.Contains(f.ForumID ?? 0)).Distinct();
        }


        /// <summary>
        /// Lists forums very simply (for URL rewriting)
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable forum_simplelist([NotNull] string connectionString, int startID, int limit)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_simplelist"))
            {
                if (startID <= 0)
                {
                    startID = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_startid", NpgsqlDbType.Integer)).Value = startID;
                cmd.Parameters.Add(new NpgsqlParameter("i_limit", NpgsqlDbType.Integer)).Value = limit;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void forum_sort_list_recursive(
            DataTable listSource, DataTable listDestination, int parentID, int categoryID, int currentIndent)
        {
            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] != parentID) continue;

                DataRow newRow;
                if ((int)row["CategoryID"] != categoryID)
                {
                    categoryID = (int)row["CategoryID"];

                    newRow = listDestination.NewRow();
                    newRow["ForumID"] = -categoryID; // Ederon : 9/4/2007
                    newRow["Title"] = string.Format("{0}", row["Category"]);
                    newRow["CanHavePersForums"] = row["CanHavePersForums"].ToType<bool>();
                    listDestination.Rows.Add(newRow);
                }

                string sIndent = string.Empty;

                for (int j = 0; j < currentIndent; j++) sIndent += "--";

                // import the row into the destination
                newRow = listDestination.NewRow();

                newRow["ForumID"] = row["ForumID"];
                newRow["Title"] = string.Format(" -{0} {1}", sIndent, row["Forum"]);
                newRow["CanHavePersForums"] = row["CanHavePersForums"].ToType<bool>();

                listDestination.Rows.Add(newRow);

                // recurse through the list..
                forum_sort_list_recursive(
                    listSource, listDestination, (int)row["ForumID"], categoryID, currentIndent + 1);
            }
        }

        public static DataTable forum_sort_list(
            DataTable listSource,
            int parentID,
            int categoryID,
            int startingIndent,
            int[] forumidExclusions,
            bool emptyFirstRow)
        {
            var listDestination = new DataTable { TableName = "forum_sort_list" };
            listDestination.Columns.Add("ForumID", typeof(String));
            listDestination.Columns.Add("Title", typeof(String));
            listDestination.Columns.Add("CanHavePersForums", typeof(bool));

            if (emptyFirstRow)
            {
                DataRow blankRow = listDestination.NewRow();
                blankRow["ForumID"] = string.Empty;
                blankRow["Title"] = string.Empty;
                blankRow["CanHavePersForums"] = false;
                listDestination.Rows.Add(blankRow);
            }
            // filter the forum list -- not sure if this code actually works
            DataView dv = listSource.DefaultView;

            if (forumidExclusions != null && forumidExclusions.Length > 0)
            {
                string strExclusions = string.Empty;
                bool bFirst = true;

                foreach (int forumId in forumidExclusions)
                {
                    if (bFirst) bFirst = false;
                    else strExclusions += ",";

                    strExclusions += forumId.ToString();
                }

                dv.RowFilter = string.Format("ForumID NOT IN ({0})", strExclusions);
                dv.ApplyDefaultSort = true;
            }

            forum_sort_list_recursive(dv.ToTable(), listDestination, parentID, categoryID, startingIndent);

            return listDestination;
        }

        /// <summary>
        /// Lists all forums within a given subcategory
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="CategoryID">CategoryID</param>
        /// <param name="EmptyFirstRow">EmptyFirstRow</param>
        /// <returns>DataTable with list</returns>
        public static DataTable forum_listall_fromCat(
            [NotNull] string connectionString, object boardId, object categoryID, bool emptyFirstRow, bool allowUserForumsOnly)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_listall_fromCat"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;
                cmd.Parameters.Add(new NpgsqlParameter("i_allowuseforumsonly", NpgsqlDbType.Boolean)).Value = allowUserForumsOnly;
                int intCategoryId = Convert.ToInt32(categoryID.ToString());

                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    return Db.forum_sort_list(dt, 0, intCategoryId, 0, null, emptyFirstRow);
                }
            }
        }

        /// <summary>
        /// Sorry no idea what this does
        /// </summary>
        /// <param name="forumID"></param>
        /// <returns></returns>
        public static DataTable forum_listpath([NotNull] string connectionString, object forumID)
        {
            if (!Config.LargeForumTree)
            {

                using (var cmd = PostgreDbAccess.GetCommand("forum_listpath"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                    return PostgreDbAccess.GetData(cmd, connectionString);
                }
            }
            else
            {
                using (var cmd = PostgreDbAccess.GetCommand("forum_ns_listpath"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                    return PostgreDbAccess.GetData(cmd, connectionString);
                }
            }
        }

        /// <summary>
        /// The forum_listread.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="showCommonForums">
        /// The show common forums.
        /// </param>
        /// <param name="showPersonalForums">
        /// The show personal forums.
        /// </param>
        /// <param name="forumCreatedByUserId">
        /// The forum created by user id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listread(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            object categoryId,
            object parentId,
            object useStyledNicks,
            [CanBeNull] bool findLastRead, 
            [NotNull] bool showCommonForums, 
            [NotNull]bool showPersonalForums, 
            [CanBeNull] int? forumCreatedByUserId)
        {
            if (!Config.LargeForumTree)
            {
                using (var cmd = PostgreDbAccess.GetCommand("forum_listread"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardId;
                    cmd.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = userId;
                    cmd.Parameters.Add("i_categoryid", NpgsqlDbType.Integer).Value = categoryId ?? DBNull.Value;
                    cmd.Parameters.Add("i_parentid", NpgsqlDbType.Integer).Value = parentId ?? DBNull.Value;
                    cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                    cmd.Parameters.Add("i_findlastunread", NpgsqlDbType.Boolean).Value = findLastRead;
                    cmd.Parameters.Add("i_showcommonforums", NpgsqlDbType.Boolean).Value = showCommonForums;
                    cmd.Parameters.Add("i_showpersonalforums", NpgsqlDbType.Boolean).Value = showPersonalForums;
                    cmd.Parameters.Add("i_forumcreatedbyuserid", NpgsqlDbType.Integer).Value = forumCreatedByUserId;
                    cmd.Parameters.Add("i_UTCTIMESTAMP", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;
                    
                    return PostgreDbAccess.GetData(cmd, false, connectionString);
                }
            }
            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_listread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardId;
                cmd.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = userId;
                cmd.Parameters.Add("i_categoryid", NpgsqlDbType.Integer).Value = categoryId;
                cmd.Parameters.Add("i_parentid", NpgsqlDbType.Integer).Value = parentId;
                cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                cmd.Parameters.Add("i_findlastunread", NpgsqlDbType.Boolean).Value = findLastRead;
                cmd.Parameters.Add("i_ShowCommonForums", NpgsqlDbType.Boolean).Value = showCommonForums;
                cmd.Parameters.Add("i_ShowPersonalForums", NpgsqlDbType.Boolean).Value = showPersonalForums;
                cmd.Parameters.Add("i_ForumCreatedByUserId", NpgsqlDbType.Integer).Value = forumCreatedByUserId;
                cmd.Parameters.Add("i_UTCTIMESTAMP", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;
                  
                return PostgreDbAccess.GetData(cmd, false, connectionString);
            }
        }

        /// <summary>
        /// The forum_listreadpersonal.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <param name="showCommonForums">
        /// The show common forums.
        /// </param>
        /// <param name="showPersonalForums">
        /// The show personal forums.
        /// </param>
        /// <param name="forumCreatedByUserId">
        /// The forum created by user id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listreadpersonal(
           [NotNull] string connectionString,
           object boardId,
           object userId,
           object categoryId,
           object parentId,
           object useStyledNicks,
           [CanBeNull] bool findLastRead,
           [NotNull] bool showCommonForums,
           [NotNull]bool showPersonalForums,
           [CanBeNull] int? forumCreatedByUserId)
        {
            if (!Config.LargeForumTree)
            {
                using (var cmd = PostgreDbAccess.GetCommand("forum_listreadpersonal"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardId;
                    cmd.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = userId;
                    cmd.Parameters.Add("i_categoryid", NpgsqlDbType.Integer).Value = categoryId ?? DBNull.Value;
                    cmd.Parameters.Add("i_parentid", NpgsqlDbType.Integer).Value = parentId ?? DBNull.Value;
                    cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                    cmd.Parameters.Add("i_findlastunread", NpgsqlDbType.Boolean).Value = findLastRead;
                    cmd.Parameters.Add("i_showcommonforums", NpgsqlDbType.Boolean).Value = showCommonForums;
                    cmd.Parameters.Add("i_showpersonalforums", NpgsqlDbType.Boolean).Value = showPersonalForums;
                    cmd.Parameters.Add("i_forumcreatedbyuserid", NpgsqlDbType.Integer).Value = forumCreatedByUserId;
                    cmd.Parameters.Add("i_UTCTIMESTAMP", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;

                    return PostgreDbAccess.GetData(cmd, false, connectionString);
                }
            }

            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_listreadpersonal"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardId;
                cmd.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = userId;
                cmd.Parameters.Add("i_categoryid", NpgsqlDbType.Integer).Value = categoryId;
                cmd.Parameters.Add("i_parentid", NpgsqlDbType.Integer).Value = parentId;
                cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                cmd.Parameters.Add("i_findlastunread", NpgsqlDbType.Boolean).Value = findLastRead;
                cmd.Parameters.Add("i_ShowCommonForums", NpgsqlDbType.Boolean).Value = showCommonForums;
                cmd.Parameters.Add("i_ShowPersonalForums", NpgsqlDbType.Boolean).Value = showPersonalForums;
                cmd.Parameters.Add("i_ForumCreatedByUserId", NpgsqlDbType.Integer).Value = forumCreatedByUserId;
                cmd.Parameters.Add("i_UTCTIMESTAMP", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, false, connectionString);
            }
        }

        /// <summary>
        /// Return admin view of Categories with Forums/Subforums ordered accordingly.
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="userId">UserID</param>
        /// <returns>DataSet with categories</returns>
        public static DataSet forum_moderatelist([NotNull] string connectionString, object userId, object boardId)
        {
            using (var connMan = new PostgreDbConnectionManager(connectionString))
            {
                using (var ds = new DataSet())
                {
                    using (var da = new NpgsqlDataAdapter(PostgreDbAccess.GetObjectName("category_list"), connMan.OpenDBConnection(connectionString)))
                    {
                        using (var trans = da.SelectCommand.Connection.BeginTransaction(PostgreDbAccess.IsolationLevel))
                        {
                            da.SelectCommand.Transaction = trans;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer))
                              .Value = boardId;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer))
                              .Value = DBNull.Value;

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;


                            da.Fill(ds, PostgreDbAccess.GetObjectName("Category"));
                            da.SelectCommand.CommandText = PostgreDbAccess.GetObjectName("forum_moderatelist");
                            da.SelectCommand.Parameters.Clear();
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer))
                              .Value = boardId;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value
                                = userId;
                            da.SelectCommand.Parameters.Add(new NpgsqlParameter("i_isuserforum", NpgsqlDbType.Boolean)).Value
                               = false;
                            da.Fill(ds, PostgreDbAccess.GetObjectName("ForumUnsorted"));
                            DataTable dtForumListSorted =
                                ds.Tables[PostgreDbAccess.GetObjectName("ForumUnsorted")].Clone();
                            dtForumListSorted.TableName = PostgreDbAccess.GetObjectName("Forum");
                            ds.Tables.Add(dtForumListSorted);
                            dtForumListSorted.Dispose();
                            Db.forum_list_sort_basic(
                                ds.Tables[PostgreDbAccess.GetObjectName("ForumUnsorted")],
                                ds.Tables[PostgreDbAccess.GetObjectName("Forum")],
                                0,
                                0);
                            ds.Tables.Remove(PostgreDbAccess.GetObjectName("ForumUnsorted"));

                            // vzrus: Remove here all forums with no reports. Would be better to do it in query..
                            // Array to write categories numbers
                            var categories = new int[ds.Tables[PostgreDbAccess.GetObjectName("Forum")].Rows.Count];
                            int cntr = 0;

                            // We should make it before too as the colection was changed
                            ds.Tables[PostgreDbAccess.GetObjectName("Forum")].AcceptChanges();
                            foreach (DataRow dr in ds.Tables[PostgreDbAccess.GetObjectName("Forum")].Rows)
                            {
                                categories[cntr] = Convert.ToInt32(dr["CategoryID"]);
                                if (Convert.ToInt32(dr["ReportedCount"]) == 0
                                    && Convert.ToInt32(dr["MessageCount"]) == 0)
                                {
                                    dr.Delete();
                                    categories[cntr] = 0;
                                }

                                cntr++;
                            }

                            ds.Tables[PostgreDbAccess.GetObjectName("Forum")].AcceptChanges();

                            foreach (DataRow dr in ds.Tables[PostgreDbAccess.GetObjectName("Category")].Rows)
                            {
                                bool deleteMe = true;
                                for (int i = 0; i < categories.Length; i++)
                                {
                                    // We check here if the Category is missing in the array where 
                                    // we've written categories number for each forum
                                    if (categories[i] == Convert.ToInt32(dr["CategoryID"]))
                                    {
                                        deleteMe = false;
                                    }
                                }

                                if (deleteMe)
                                {
                                    dr.Delete();
                                }
                            }

                            ds.Tables[PostgreDbAccess.GetObjectName("Category")].AcceptChanges();

                            ds.Relations.Add(
                                "FK_Forum_Category",
                                ds.Tables[PostgreDbAccess.GetObjectName("Category")].Columns["CategoryID"],
                                ds.Tables[PostgreDbAccess.GetObjectName("Forum")].Columns["CategoryID"]);
                            trans.Commit();
                        }

                        return ds;
                    }
                }
            }
        }

        public static DataTable forum_moderators([NotNull] string connectionString, bool useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_moderators"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The moderators_team_list
        /// </summary>
        /// <param name="useStyledNicks">
        /// The use Styled Nicks.
        /// </param>
        /// <returns>
        ///  Returns Data Table with all Mods
        /// </returns>
        public static DataTable moderators_team_list([NotNull] string connectionString, bool useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("moderators_team_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Updates topic and post count and last topic for specified forum
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <param name="forumID">If null, all forums in board are updated</param>
        public static void forum_resync([NotNull] string connectionString, object boardId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_resync"))
            {
                if (forumID == null)
                {
                    forumID = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static long forum_save(
            [NotNull] string connectionString,
            object forumID,
            object categoryID,
            object parentID,
            object name,
            object description,
            object sortOrder,
            object locked,
            object hidden,
            object isTest,
            object moderated,
            object accessMaskID,
            object remoteURL,
            object themeURL,
            object imageURL,
            object styles,
            bool dummy, 
            object userId,
            bool isUserForum,
            bool canhavepersforums)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_save"))
            {
                int sortOrderOut = 0;
                bool result = int.TryParse(sortOrder.ToString(), out sortOrderOut);
                if (result)
                {
                    if (sortOrderOut >= 255)
                    {
                        sortOrderOut = 0;
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryID;
                cmd.Parameters.Add(new NpgsqlParameter("i_parentid", NpgsqlDbType.Integer)).Value = parentID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrderOut;
                cmd.Parameters.Add(new NpgsqlParameter("i_locked", NpgsqlDbType.Boolean)).Value = locked;
                cmd.Parameters.Add(new NpgsqlParameter("i_hidden", NpgsqlDbType.Boolean)).Value = hidden;
                cmd.Parameters.Add(new NpgsqlParameter("i_istest", NpgsqlDbType.Boolean)).Value = isTest;
                cmd.Parameters.Add(new NpgsqlParameter("i_moderated", NpgsqlDbType.Boolean)).Value = moderated;
                cmd.Parameters.Add(new NpgsqlParameter("i_remoteurl", NpgsqlDbType.Varchar)).Value = remoteURL ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_themeurl", NpgsqlDbType.Varchar)).Value = themeURL ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageurl", NpgsqlDbType.Varchar)).Value = imageURL ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_styles", NpgsqlDbType.Varchar)).Value = styles;
                cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlDbType.Integer)).Value = accessMaskID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_isuserforum", NpgsqlDbType.Boolean)).Value = isUserForum;
                cmd.Parameters.Add(new NpgsqlParameter("i_canhavepersforums", NpgsqlDbType.Boolean)).Value = canhavepersforums;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                   DateTime.UtcNow;
                String resultop = PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString();
                if (String.IsNullOrEmpty(resultop))
                {
                    return 0;
                }
                else
                {
                    forum_ns_recreate(connectionString);
                    return long.Parse(resultop);
                }
            }
        }

        /// <summary>
        /// The method returns an integer value for a  found parent forum 
        /// if a forum is a parent of an existing child to avoid circular dependency
        /// while creating a new forum
        /// </summary>
        /// <param name="forumID"></param>
        /// <param name="parentID"></param>
        /// <returns>Integer value for a found dependency</returns>
        public static int forum_save_parentschecker([NotNull] string connectionString, object forumID, object parentID)
        {
            using (
                var cmd =
                    PostgreDbAccess.GetCommand(
                        "SELECT " + PostgreDbAccess.GetObjectName("forum_save_parentschecker") + "(@ForumID,@ParentID)",
                        true))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new NpgsqlParameter("@ForumID", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("@ParentID", NpgsqlDbType.Integer)).Value = parentID;
                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }

        }

        /// <summary>
        /// Recreate tree.
        /// </summary>
        public static void forum_ns_recreate([NotNull] string connectionString )
        {
            using (var cmd = PostgreDbAccess.GetCommand("forum_ns_recreate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        #endregion

        #region yaf_ForumAccess

        public static DataTable forumaccess_list([NotNull] string connectionString, object forumID, object userId, bool includeUserGroups)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forumaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_ForumID", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_UserID", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_IncludeUserGroups", NpgsqlDbType.Boolean)).Value = includeUserGroups;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void forumaccess_save(
            [NotNull] string connectionString, object forumID, object groupID, object accessMaskID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("forumaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlDbType.Integer)).Value = accessMaskID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable forumaccess_group([NotNull] string connectionString, object groupID, object userId, bool includeUserForums)
        {
            // this needs to be rewritten as a separate sp
            using (var cmd = PostgreDbAccess.GetCommand("forumaccess_group"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_UserID", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_IncludeUserForums", NpgsqlDbType.Boolean)).Value = includeUserForums;
                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                if (includeUserForums) return dt;
                return userforumaccess_sort_list(dt, 0, 0, 0);
            }
        }
        public static DataTable forumaccess_personalgroup([NotNull] string connectionString, object groupID, object userId, bool includeUserForums)
        {
            // this needs to be rewritten as a separate sp
            using (var cmd = PostgreDbAccess.GetCommand("forumaccess_personalgroup"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_UserID", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_IncludeUserForums", NpgsqlDbType.Boolean)).Value = includeUserForums;
               
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Group

        public static DataTable group_list([NotNull] string connectionString, object boardId, object groupID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable group_byuserlist([NotNull] string connectionString, object boardId, object groupID, object userId, object isUserGroup)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_byuserlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_isusergroup", NpgsqlDbType.Boolean)).Value = isUserGroup;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void group_delete([NotNull] string connectionString, object groupID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable group_member([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_member"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;


                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static long group_save(
            [NotNull] string connectionString,
            object groupId,
            object boardId,
            object name,
            object isAdmin,
            object isGuest,
            object isStart,
            object isModerator,
            [NotNull] object isHidden,
            object accessMaskId,
            object pmLimit,
            object style,
            object sortOrder,
            object description,
            object usrSigChars,
            object usrSigBBCodes,
            object usrSigHTMLTags,
            object usrAlbums,
            object usrAlbumImages,
            [CanBeNull] object userId,
            [NotNull] object isUserGroup,
            object personalForumsNumber,
            object personalAccessMasksNumber,
            object personalGroupsNumber)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_save"))
            {
                if (accessMaskId == null)
                {
                    accessMaskId = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupId;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_isadmin", NpgsqlDbType.Boolean)).Value = isAdmin;
                cmd.Parameters.Add(new NpgsqlParameter("i_isguest", NpgsqlDbType.Boolean)).Value = isGuest;
                cmd.Parameters.Add(new NpgsqlParameter("i_isstart", NpgsqlDbType.Boolean)).Value = isStart;
                cmd.Parameters.Add(new NpgsqlParameter("i_ismoderator", NpgsqlDbType.Boolean)).Value = isModerator;
                cmd.Parameters.Add(new NpgsqlParameter("i_ishidden", NpgsqlDbType.Boolean)).Value = isHidden;
                cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlDbType.Integer)).Value = accessMaskId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pmlimit", NpgsqlDbType.Integer)).Value = pmLimit;
                cmd.Parameters.Add(new NpgsqlParameter("i_style", NpgsqlDbType.Varchar)).Value = style;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrder;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsigchars", NpgsqlDbType.Integer)).Value = usrSigChars;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsigbbcodes", NpgsqlDbType.Varchar)).Value = usrSigBBCodes;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsightmltags", NpgsqlDbType.Varchar)).Value = usrSigHTMLTags;
                cmd.Parameters.Add(new NpgsqlParameter("i_usralbums", NpgsqlDbType.Integer)).Value = usrAlbums;
                cmd.Parameters.Add(new NpgsqlParameter("i_usralbumimages", NpgsqlDbType.Integer)).Value = usrAlbumImages;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_isusergroup", NpgsqlDbType.Boolean)).Value = isUserGroup;
                cmd.Parameters.Add(new NpgsqlParameter("i_personalaccessmasksnumber", NpgsqlDbType.Integer)).Value = personalAccessMasksNumber;
                cmd.Parameters.Add(new NpgsqlParameter("i_personalgroupsnumber", NpgsqlDbType.Integer)).Value = personalGroupsNumber;
                cmd.Parameters.Add(new NpgsqlParameter("i_personalforumsnumber", NpgsqlDbType.Integer)).Value = personalForumsNumber;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                  DateTime.UtcNow;
                return long.Parse(PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString());
            }
        }

        #endregion

        #region yaf_Mail

        public static void mail_delete([NotNull] string connectionString, object mailID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("mail_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_mailid", NpgsqlDbType.Integer)).Value = mailID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The mail_list.
        /// </summary>
        /// <param name="processId">
        /// The process id.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<TypedMailList> MailList([NotNull] string connectionString, long processId)
        {
            DateTime dtm = DateTime.UtcNow;
            using (var cmd = PostgreDbAccess.GetCommand("mail_listupdate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_processid", NpgsqlDbType.Integer)).Value = processId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value = dtm;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
            using (var cmd1 = PostgreDbAccess.GetCommand("mail_list"))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new NpgsqlParameter("i_processid", NpgsqlDbType.Integer)).Value = processId;
                cmd1.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value = dtm;
                return PostgreDbAccess.GetData(cmd1, connectionString).SelectTypedList(x => new TypedMailList(x));
            }
        }

        public static void mail_createwatch(
            [NotNull] string connectionString,
            object topicID,
            object from,
            object fromName,
            object subject,
            object body,
            object bodyHtml,
            object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("mail_createwatch"))
            {
                if (fromName == null)
                {
                    fromName = DBNull.Value;
                }
                if (bodyHtml == null)
                {
                    bodyHtml = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_from", NpgsqlDbType.Varchar)).Value = from;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromname", NpgsqlDbType.Varchar)).Value = fromName;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = subject;
                cmd.Parameters.Add(new NpgsqlParameter("i_body", NpgsqlDbType.Text)).Value = body;
                cmd.Parameters.Add(new NpgsqlParameter("i_bodyhtml", NpgsqlDbType.Text)).Value = bodyHtml;
                cmd.Parameters.Add(new NpgsqlParameter("i_iserid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void mail_create(
            [NotNull] string connectionString,
            object from,
            object fromName,
            object to,
            object toName,
            object subject,
            object body,
            object bodyHtml)
        {


            using (var cmd = PostgreDbAccess.GetCommand("mail_create"))
            {
                //if (fromName == null) { fromName = DBNull.Value; }
                // if (toName == null) { toName = DBNull.Value; }
                // if (bodyHtml == null) { bodyHtml = DBNull.Value; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_from", NpgsqlDbType.Varchar)).Value = from;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromname", NpgsqlDbType.Varchar)).Value = fromName;
                cmd.Parameters.Add(new NpgsqlParameter("i_to", NpgsqlDbType.Varchar)).Value = to;
                cmd.Parameters.Add(new NpgsqlParameter("i_toname", NpgsqlDbType.Varchar)).Value = toName;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = subject;
                cmd.Parameters.Add(new NpgsqlParameter("i_body", NpgsqlDbType.Text)).Value = body;
                cmd.Parameters.Add(new NpgsqlParameter("i_bodyhtml", NpgsqlDbType.Text)).Value = bodyHtml;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Message

        public static DataTable post_list(
            [NotNull] string connectionString,
            object topicId,
            object currentUserID,
            object authorUserID,
            object updateViewCount,
            bool showDeleted,
            bool styledNicks,
            bool showReputation,
            DateTime sincePostedDate,
            DateTime toPostedDate,
            DateTime sinceEditedDate,
            DateTime toEditedDate,
            int pageIndex,
            int pageSize,
            int sortPosted,
            int sortEdited,
            int sortPosition,
            bool showThanks,
            int messagePosition,
            int messageId, 
            DateTime lastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("post_list"))
            {
                if (updateViewCount == null)
                {
                    updateViewCount = 1;
                }
                //if (showDeleted != false || showDeleted != true) { showDeleted = true; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = currentUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_authoruserid", NpgsqlDbType.Integer)).Value = authorUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_updateviewcount", NpgsqlDbType.Smallint)).Value =
                    updateViewCount;
                cmd.Parameters.Add(new NpgsqlParameter("i_showdeleted", NpgsqlDbType.Boolean)).Value = showDeleted;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_showreputation", NpgsqlDbType.Boolean)).Value = showReputation;
                cmd.Parameters.Add(new NpgsqlParameter("i_sinceposteddate", NpgsqlDbType.Timestamp)).Value =
                    sincePostedDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_toposteddate", NpgsqlDbType.Timestamp)).Value = toPostedDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_sinceediteddate", NpgsqlDbType.Timestamp)).Value =
                    sinceEditedDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_toediteddate", NpgsqlDbType.Timestamp)).Value = toEditedDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortposted", NpgsqlDbType.Integer)).Value = sortPosted;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortedited", NpgsqlDbType.Integer)).Value = sortEdited;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortposition", NpgsqlDbType.Integer)).Value = sortPosition;
                cmd.Parameters.Add(new NpgsqlParameter("i_showthanks", NpgsqlDbType.Boolean)).Value = showThanks;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageposition", NpgsqlDbType.Integer)).Value =
                    messagePosition;
                cmd.Parameters.Add("i_messsageid", NpgsqlDbType.Integer).Value = messageId;
                cmd.Parameters.Add("i_lastread", NpgsqlDbType.Timestamp).Value = lastRead;
                cmd.Parameters.Add("i_utctimestamp", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);

            }
        }

        public static DataTable post_list_reverse10([NotNull] string connectionString, object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("post_list_reverse10"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Gets all the post by a user.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="topCount">
        /// Top count to return. Null is all.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable post_alluser(
            [NotNull] string connectionString, object boardid, object userid, object pageUserID, object topCount)
        {
            using (var cmd = PostgreDbAccess.GetCommand("post_alluser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topcount", NpgsqlDbType.Integer)).Value = topCount;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        // gets list of replies to message
        public static DataTable message_getRepliesList([NotNull] string connectionString, object messageID)
        {
            DataTable list = new DataTable();

            list.Columns.Add("MessageID", typeof(int));
            list.Columns.Add("Posted", typeof(DateTime));
            list.Columns.Add("Subject", typeof(string));
            list.Columns.Add("Message", typeof(string));
            list.Columns.Add("UserID", typeof(int));
            list.Columns.Add("Flags", typeof(int));
            list.Columns.Add("UserName", typeof(string));
            list.Columns.Add("Signature", typeof(string));

            using (var cmd = PostgreDbAccess.GetCommand("message_reply_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                DataTable dtr = PostgreDbAccess.GetData(cmd, connectionString);
                for (int i = 0; i < dtr.Rows.Count; i++)
                {
                    DataRow newRow = list.NewRow();
                    DataRow row = dtr.Rows[i];
                    newRow["MessageID"] = row["MessageID"];
                    newRow["Posted"] = row["Posted"];
                    newRow["Subject"] = row["Subject"];
                    newRow["Message"] = row["Message"];
                    newRow["UserID"] = row["UserID"];
                    newRow["Flags"] = row["Flags"];
                    newRow["UserName"] = row["UserName"];
                    newRow["Signature"] = row["Signature"];
                    list.Rows.Add(newRow);
                    message_getRepliesList_populate(connectionString, dtr, list, (int)row["MessageId"]);
                }
                return list;
            }
        }

        // gets list of nested replies to message
        private static void message_getRepliesList_populate(
            [NotNull] string connectionString, DataTable listsource, DataTable list, int messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_reply_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer));
                cmd.Parameters[0].Value = messageID;


                DataTable dtr = PostgreDbAccess.GetData(cmd, connectionString);

                for (int i = 0; i < dtr.Rows.Count; i++)
                {
                    DataRow row = dtr.Rows[i];
                    DataRow newRow = list.NewRow();
                    newRow["MessageID"] = row["MessageID"];
                    newRow["Posted"] = row["Posted"];
                    newRow["Subject"] = row["Subject"];
                    newRow["Message"] = row["Message"];
                    newRow["UserID"] = row["UserID"];
                    newRow["Flags"] = row["Flags"];
                    newRow["UserName"] = row["UserName"];
                    newRow["Signature"] = row["Signature"];
                    list.Rows.Add(newRow);
                    message_getRepliesList_populate(connectionString, dtr, list, (int)row["MessageId"]);
                }
            }

        }

        //creates new topic, using some parameters from message itself
        public static long topic_create_by_message(
            [NotNull] string connectionString, object messageID, object forumId, object newTopicSubj)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_create_by_message"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = newTopicSubj;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
                //return long.Parse(dt.Rows[0]["TopicID"].ToString());
            }
        }

        /// <summary>
        /// The message_list.
        /// </summary>
        /// <param name="messageID">
        /// The message id.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<TypedMessageList> MessageList([NotNull] string connectionString, int messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                var dd = PostgreDbAccess.GetData(cmd, connectionString)
                                      .AsEnumerable()
                                      .Select(t => new TypedMessageList(t));
                return dd;

            }
        }

        public static void message_delete(
            [NotNull] string connectionString,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked)
        {
            message_delete(
                connectionString, messageID, isModeratorChanged, deleteReason, isDeleteAction, DeleteLinked, false);
        }

        public static void message_delete(
            [NotNull] string connectionString,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool eraseMessage)
        {
            message_deleteRecursively(
                connectionString,
                messageID,
                isModeratorChanged,
                deleteReason,
                isDeleteAction,
                DeleteLinked,
                false,
                eraseMessage);
        }

        // <summary> Retrieve all reported messages with the correct forumID argument. </summary>
        public static DataTable message_listreported([NotNull] string connectionString, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_listreported"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Here we get reporters list for a reported message
        /// </summary>       
        /// <param name="MessageID">Should not be NULL</param>
        /// <returns>Returns reporters DataTable for a reported message.</returns>
        public static DataTable message_listreporters([NotNull] string connectionString, int messageID)
        {

            return message_listreporters(connectionString, messageID, null);

        }

        public static DataTable message_listreporters([NotNull] string connectionString, int messageID, object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_listreporters"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        // <summary> Save reported message back to the database. </summary>
        public static void message_report(
            [NotNull] string connectionString, object messageID, object userId, object reportedDateTime, object reportText)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_report"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_reporterid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_reporteddate", NpgsqlDbType.Timestamp)).Value =
                    reportedDateTime;
                cmd.Parameters.Add(new NpgsqlParameter("i_reporttext", NpgsqlDbType.Varchar)).Value = reportText;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Copy current Message text over reported Message text. </summary>
        public static void message_reportcopyover([NotNull] string connectionString, object messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_reportcopyover"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Copy current Message text over reported Message text. </summary>
        public static void message_reportresolve(
            [NotNull] string connectionString, object messageFlag, object messageID, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_reportresolve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageflag", NpgsqlDbType.Integer)).Value = messageFlag;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        //BAI ADDED 30.01.2004
        // <summary> Delete message and all subsequent releated messages to that ID </summary>
        private static void message_deleteRecursively(
            [NotNull] string connectionString,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool isLinked)
        {
            message_deleteRecursively(
                connectionString,
                messageID,
                isModeratorChanged,
                deleteReason,
                isDeleteAction,
                DeleteLinked,
                isLinked,
                false);
        }

        private static void message_deleteRecursively(
            [NotNull] string connectionString,
            object messageID,
            bool isModeratorChanged,
            string deleteReason,
            int isDeleteAction,
            bool DeleteLinked,
            bool isLinked,
            bool eraseMessages)
        {
            bool UseFileTable = GetBooleanRegistryValue(connectionString, "UseFileTable");


            if (DeleteLinked)
            {
                //Delete replies
                using (var cmd = PostgreDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                    DataTable tbReplies = PostgreDbAccess.GetData(cmd, connectionString);

                    foreach (DataRow row in tbReplies.Rows)
                        message_deleteRecursively(
                            connectionString,
                            row["MessageID"],
                            isModeratorChanged,
                            deleteReason,
                            isDeleteAction,
                            DeleteLinked,
                            true,
                            eraseMessages);
                }
            }

            // If the files are actually saved in the Hard Drive.
            if (!UseFileTable)
            {
                using (var cmd = PostgreDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                    cmd.Parameters.Add(new NpgsqlParameter("i_attachmentid", NpgsqlDbType.Integer)).Value = null;
                    cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = null;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = 0;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pageisize", NpgsqlDbType.Integer)).Value = 1000000;
                    DataTable tbAttachments = PostgreDbAccess.GetData(cmd, connectionString);
                    string uploadDir =
                        HostingEnvironment.MapPath(
                            String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads));


                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format(
                                "{0}/{1}.{2}.yafupload", uploadDir, messageID, row["FileName"]);

                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch
                        {
                            // error deleting that file.. 
                        }
                    }
                }
            }

            // Ederon : erase message for good
            if (eraseMessages)
            {
                using (var cmd = PostgreDbAccess.GetCommand("message_delete"))
                {
                    //if (eraseMessages == null) { eraseMessages = false; }
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                    cmd.Parameters.Add(new NpgsqlParameter("i_erasemessage", NpgsqlDbType.Boolean)).Value =
                        eraseMessages;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            else
            {
                //Delete Message
                // undelete function added
                using (var cmd = PostgreDbAccess.GetCommand("message_deleteundelete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                    cmd.Parameters.Add(new NpgsqlParameter("i_ismoderatorchanged", NpgsqlDbType.Boolean)).Value =
                        isModeratorChanged;
                    cmd.Parameters.Add(new NpgsqlParameter("i_deletereason", NpgsqlDbType.Varchar)).Value = deleteReason;
                    cmd.Parameters.Add(new NpgsqlParameter("i_isdeleteaction", NpgsqlDbType.Integer)).Value =
                        isDeleteAction;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
        }

        // <summary> Set flag on message to approved and store in DB </summary>
        public static void message_approve([NotNull] string connectionString, object messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_approve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Get message topic IDs (for URL rewriting)
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable message_simplelist([NotNull] string connectionString, int StartID, int Limit)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_simplelist"))
            {
                if (StartID <= 0)
                {
                    StartID = 0;
                }
                if (Limit <= 0)
                {
                    Limit = 1000;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_startid", NpgsqlDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new NpgsqlParameter("i_limit", NpgsqlDbType.Integer)).Value = Limit;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        public static void message_update(
            [NotNull] string connectionString,
            object messageID,
            object priority,
            object message,
            object description,
            object status,
            object styles,
            object subject,
            object flags,
            object reasonOfEdit,
            object isModeratorChanged,
            object overrideApproval,
            object origMessage,
            object editedBy,
            object messageDescription,
            string tags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_priority", NpgsqlDbType.Integer)).Value = priority;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = subject;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_status", NpgsqlDbType.Varchar)).Value = status;
                cmd.Parameters.Add(new NpgsqlParameter("i_styles", NpgsqlDbType.Varchar)).Value = styles;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Text)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_reason", NpgsqlDbType.Varchar)).Value = reasonOfEdit;
                cmd.Parameters.Add(new NpgsqlParameter("i_editedby", NpgsqlDbType.Integer)).Value = editedBy;
                cmd.Parameters.Add(new NpgsqlParameter("i_ismoderatorchanged", NpgsqlDbType.Boolean)).Value =
                    isModeratorChanged;
                cmd.Parameters.Add(new NpgsqlParameter("i_overrideapproval", NpgsqlDbType.Boolean)).Value =
                    overrideApproval ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_originalmessage", NpgsqlDbType.Text)).Value = origMessage;
                cmd.Parameters.Add(new NpgsqlParameter("i_newguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_messagedescription", NpgsqlDbType.Varchar)).Value = messageDescription;
                cmd.Parameters.Add(new NpgsqlParameter("i_tags", NpgsqlDbType.Text)).Value = tags;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Save message to Db. </summary>
        public static bool message_save(
            [NotNull] string connectionString,
            [NotNull] object topicId,
            [NotNull] object userId,
            [NotNull] object message,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object replyTo,
            [NotNull] object flags,
            [CanBeNull] object messageDescription,
            ref long messageId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_save"))
            {
                if (userName == null)
                {
                    userName = DBNull.Value;
                }
                if (posted == null)
                {
                    posted = DBNull.Value;
                }


                NpgsqlParameter paramMessageID = new NpgsqlParameter("i_messageid", messageId);
                paramMessageID.Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Text)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_ip", NpgsqlDbType.Varchar)).Value = ip;
                cmd.Parameters.Add(new NpgsqlParameter("i_posted", NpgsqlDbType.Timestamp)).Value = posted;
                cmd.Parameters.Add(new NpgsqlParameter("i_replyto", NpgsqlDbType.Integer)).Value = replyTo;
                cmd.Parameters.Add(new NpgsqlParameter("i_blogpostid", NpgsqlDbType.Varchar)).Value = DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_externalmessageid", NpgsqlDbType.Varchar)).Value =
                    DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_referencemessageid", NpgsqlDbType.Varchar)).Value =
                    DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new NpgsqlParameter("i_messagedescription", NpgsqlDbType.Varchar)).Value = messageDescription;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                cmd.Parameters.Add(paramMessageID);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                messageId = Convert.ToInt64(paramMessageID.Value);
                return true;
            }
        }

        public static DataTable message_unapproved([NotNull] string connectionString, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_unapproved"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumID", NpgsqlDbType.Integer)).Value = forumID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable message_findunread(
            [NotNull] string connectionString,
            object topicID,
            object messageId,
            object lastRead,
            object showDeleted,
            object authorUserID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_findunread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new NpgsqlParameter("i_lastread", NpgsqlDbType.Timestamp)).Value = lastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_showdeleted", NpgsqlDbType.Boolean)).Value = showDeleted;
                cmd.Parameters.Add(new NpgsqlParameter("i_authoruserid", NpgsqlDbType.Integer)).Value = authorUserID;
                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                return dt;
            }
        }

        // message movind function
        public static void message_move([NotNull] string connectionString, object messageID, object moveToTopic, bool moveAll)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_move"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_movetotopic", NpgsqlDbType.Integer)).Value = moveToTopic;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
            //moveAll=true anyway
            // it's in charge of moving answers of moved post
            if (moveAll)
            {
                using (var cmd = PostgreDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                    DataTable tbReplies = PostgreDbAccess.GetData(cmd, connectionString);
                    foreach (DataRow row in tbReplies.Rows)
                    {
                        message_moveRecursively(connectionString, row["MessageID"], moveToTopic);
                    }

                }
            }
        }

        //moves answers of moved post
        private static void message_moveRecursively([NotNull] string connectionString, object messageID, object moveToTopic)
        {
            bool UseFileTable = GetBooleanRegistryValue(connectionString, "UseFileTable");

            //Delete replies
            using (var cmd = PostgreDbAccess.GetCommand("message_getReplies"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                DataTable tbReplies = PostgreDbAccess.GetData(cmd, connectionString);
                foreach (DataRow row in tbReplies.Rows)
                {
                    message_moveRecursively(connectionString, row["messageID"], moveToTopic);
                }
                using (NpgsqlCommand innercmd = PostgreDbAccess.GetCommand("message_move"))
                {
                    innercmd.CommandType = CommandType.StoredProcedure;

                    innercmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                    innercmd.Parameters.Add(new NpgsqlParameter("i_movetotopic", NpgsqlDbType.Integer)).Value =
                        moveToTopic;

                    PostgreDbAccess.ExecuteNonQuery(innercmd, connectionString);
                }
            }
        }



        // functions for Thanks feature

        // <summary> Checks if the message with the provided messageID is thanked 
        //           by the user with the provided UserID. if so, returns true,
        //           otherwise returns false. </summary>
        public static bool message_isThankedByUser([NotNull] string connectionString, object userID, object messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_isthankedbyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                return Convert.ToBoolean(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        /// <summary>
        /// Is User Thanked the current Message
        /// </summary>
        /// <param name="messageId">
        /// The message Id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// If the User Thanked the the Current Message
        /// </returns>
        public static bool user_ThankedMessage([NotNull] string connectionString, object messageId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_thankedmessage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                int thankCount = (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return thankCount > 0;
            }
        }

        public static int user_ThankFromCount([NotNull] string connectionString, [NotNull] object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_thankfromcount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                var thankCount = (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return thankCount;
            }
        }

        // <summary> Return the number of times the message with the provided messageID
        //           has been thanked. </summary>
        public static int message_ThanksNumber([NotNull] string connectionString, object messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_thanksnumber"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;

                return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        // <summary> Returns the UserIDs and UserNames who have thanked the message
        //           with the provided messageID. </summary>
        public static DataTable message_GetThanks([NotNull] string connectionString, object messageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_getthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        // <summary> Retuns All the Thanks for the Message IDs which are in the 
        //           delimited string variable MessageIDs </summary>
        public static DataTable message_GetAllThanks([NotNull] string connectionString, object messageIDs)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_getallthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageids", NpgsqlDbType.Text)).Value = messageIDs;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retuns All the message text for the Message IDs which are in the 
        /// delimited string variable MessageIDs
        /// </summary>
        /// <param name="messageIDs">
        /// The message ids.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable message_GetTextByIds([NotNull] string connectionString, string messageIDs)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_gettextbyids"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageids", NpgsqlDbType.Text)).Value = messageIDs;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retuns All the Thanks for the Message IDs which are in the 
        /// delimited string variable MessageIDs
        /// </summary>
        /// <param name="messageIdsSeparatedWithColon">
        /// The message i ds.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<TypedAllThanks> MessageGetAllThanks(
            [NotNull] string connectionString, string messageIdsSeparatedWithColon)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_getallthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageids", NpgsqlDbType.Text)).Value =
                    messageIdsSeparatedWithColon;

                return PostgreDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(t => new TypedAllThanks(t));
            }
        }

        public static string message_AddThanks(
            [NotNull] string connectionString, object fromUserID, object messageID, bool useDisplayName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_addthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // var paramOutput = new NpgsqlParameter("paramOutput", NpgsqlDbType.Varchar);
                // paramOutput.Direction = ParameterDirection.Output;                  
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                cmd.Parameters.Add(new NpgsqlParameter("i_usedisplayname", NpgsqlDbType.Boolean)).Value = useDisplayName;

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString();
                //return paramOutput.Value.ToString();                
            }
        }

        public static string message_RemoveThanks(
            [NotNull] string connectionString, object fromUserID, object messageID, bool useDisplayName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_removethanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_usedisplayname", NpgsqlDbType.Boolean)).Value = useDisplayName;

                PostgreDbAccess.ExecuteScalar(cmd, connectionString);
                return PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        /// <summary>
        /// The messagehistory_list.
        /// </summary>
        /// <param name="messageID">
        /// The Message ID.
        /// </param>
        /// <param name="daysToClean">
        /// Days to clean.
        /// </param>
        /// <param name="showAll">
        /// The Show All.
        /// </param>
        /// <returns>
        /// List of all message changes. 
        /// </returns>
        public static DataTable messagehistory_list([NotNull] string connectionString, int messageID, int daysToClean)
        {
            using (var cmd = PostgreDbAccess.GetCommand("messagehistory_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_daystoclean", NpgsqlDbType.Integer)).Value = daysToClean;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns message data based on user access rights
        /// </summary>
        /// <param name="MessageID">The Message Id.</param>
        /// <param name="UserID">The UserId.</param>
        /// <returns></returns>
        public static DataTable message_secdata([NotNull] string connectionString, int MessageID, object pageUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("message_secdata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = MessageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;

                return PostgreDbAccess.GetData(cmd, connectionString);

            }
        }

        #endregion

        #region yaf_Medal

        /// <summary>
        /// Lists given medal.
        /// </summary>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable medal_list([NotNull] string connectionString, object medalID)
        {
            return medal_list(connectionString, null, medalID, null);
        }

        public static DataTable medal_list([NotNull] string connectionString, object boardId, object category)
        {
            return medal_list(connectionString, boardId, category, null);
        }

        /// <summary>
        /// Lists medals.
        /// </summary>
        /// <param name="boardId">ID of board of which medals to list. Can be null if medalID parameter is specified.</param>
        /// <param name="medalID">ID of medal to list. When specified, boardId and category parameters are ignored.</param>
        /// <param name="category">Cateogry of medals to list. Must be complemented with not-null boardId parameter.</param>
        public static DataTable medal_list([NotNull] string connectionString, object boardId, object medalID, object category)
        {
            using (var cmd = PostgreDbAccess.GetCommand("medal_list"))
            {
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }
                if (medalID == null)
                {
                    medalID = DBNull.Value;
                }
                if (category == null)
                {
                    category = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_category", NpgsqlDbType.Varchar)).Value = category;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// List users who own this medal.
        /// </summary>
        /// <param name="medalID">Medal of which owners to get.</param>
        /// <returns>List of users with their user id and usernames, who own this medal.</returns>
        public static DataTable medal_listusers([NotNull] string connectionString, object medalID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("medal_listusers"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }




        /// <summary>
        /// Deletes given medals.
        /// </summary>
        /// <param name="boardId">ID of board of which medals to delete. Required.</param>
        /// <param name="category">Cateogry of medals to delete. Can be null. In such case this parameter is ignored.</param>
        public static void medal_delete([NotNull] string connectionString, object boardId, object category)
        {
            medal_delete(connectionString, boardId, null, category);
        }

        /// <summary>
        /// Deletes given medal.
        /// </summary>
        /// <param name="medalID">ID of medal to delete.</param>
        public static void medal_delete([NotNull] string connectionString, object medalID)
        {
            medal_delete(connectionString, null, medalID, null);
        }

        /// <summary>
        /// Deletes medals.
        /// </summary>
        /// <param name="boardId">ID of board of which medals to delete. Can be null if medalID parameter is specified.</param>
        /// <param name="medalID">ID of medal to delete. When specified, boardId and category parameters are ignored.</param>
        /// <param name="category">Cateogry of medals to delete. Must be complemented with not-null boardId parameter.</param>
        public static void medal_delete([NotNull] string connectionString, object boardId, object medalID, object category)
        {
            using (var cmd = PostgreDbAccess.GetCommand("medal_delete"))
            {
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }
                if (medalID == null)
                {
                    medalID = DBNull.Value;
                }
                if (category == null)
                {
                    category = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_category", NpgsqlDbType.Varchar)).Value = category;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Saves new medal or updates existing one. 
        /// </summary>
        /// <param name="boardId">ID of a board.</param>
        /// <param name="medalID">ID of medal to update. Null if new medal is being created.</param>
        /// <param name="name">Name of medal.</param>
        /// <param name="description">Description of medal.</param>
        /// <param name="message">Defaukt message to display. Should briefly describe why was medal awarded to user.</param>
        /// <param name="category">Category of medal.</param>
        /// <param name="medalURL">URL of medal's image.</param>
        /// <param name="ribbonURL">URL of medal's ribbon bar. Can be null.</param>
        /// <param name="smallMedalURL">URL of medal's small image. This one is displayed in user box.</param>
        /// <param name="smallRibbonURL">URL of medal's small ribbon bar. This one is eventually displayed in user box. Can be null.</param>
        /// <param name="smallMedalWidth">Width of small medal's image, in pixels.</param>
        /// <param name="smallMedalHeight">Height of small medal's image, in pixels.</param>
        /// <param name="smallRibbonWidth">Width of small medal's ribbon bar image, in pixels.</param>
        /// <param name="smallRibbonHeight">Width of small medal's ribbon bar image, in pixels.</param>
        /// <param name="sortOrder">Default order of medal as it will be displayed in user box.</paramHeight
        /// <param name="flags">Medal's flags.</param>
        /// <returns>True if medal was successfully created or updated. False otherwise.</returns>
        public static bool medal_save(
            [NotNull] string connectionString,
            object boardId,
            object medalID,
            object name,
            object description,
            object message,
            object category,
            object medalURL,
            object ribbonURL,
            object smallMedalURL,
            object smallRibbonURL,
            object smallMedalWidth,
            object smallMedalHeight,
            object smallRibbonWidth,
            object smallRibbonHeight,
            object sortOrder,
            object flags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("medal_save"))
            {
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }
                if (medalID == null)
                {
                    medalID = DBNull.Value;
                }
                if (category == null)
                {
                    category = DBNull.Value;
                }
                if (ribbonURL == null)
                {
                    ribbonURL = DBNull.Value;
                }
                if (smallRibbonURL == null)
                {
                    smallRibbonURL = DBNull.Value;
                }
                if (smallRibbonWidth == null)
                {
                    smallRibbonWidth = DBNull.Value;
                }
                if (smallRibbonHeight == null)
                {
                    smallRibbonHeight = DBNull.Value;
                }

                int sortOrderOut = 0;
                bool result = Int32.TryParse(sortOrder.ToString(), out sortOrderOut);
                if (result)
                {
                    if (sortOrderOut >= 255)
                    {
                        sortOrderOut = 0;
                    }
                }

                if (flags == null)
                {
                    flags = 0;
                }



                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Text)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Varchar)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_category", NpgsqlDbType.Varchar)).Value = category;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalurl", NpgsqlDbType.Varchar)).Value = medalURL;
                cmd.Parameters.Add(new NpgsqlParameter("i_ribbonurl", NpgsqlDbType.Varchar)).Value = ribbonURL;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallmedalurl", NpgsqlDbType.Varchar)).Value = smallMedalURL;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallribbonurl", NpgsqlDbType.Varchar)).Value = smallRibbonURL;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallmedalwidth", NpgsqlDbType.Smallint)).Value =
                    smallMedalWidth;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallmedalheight", NpgsqlDbType.Smallint)).Value =
                    smallMedalHeight;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallribbonwidth", NpgsqlDbType.Smallint)).Value =
                    smallRibbonWidth;
                cmd.Parameters.Add(new NpgsqlParameter("i_smallribbonheight", NpgsqlDbType.Smallint)).Value =
                    smallRibbonHeight;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrderOut;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;


                // command succeeded if returned value is greater than zero (number of affected rows)
                // bool rres = (PostgreDbAccess.ExecuteScalar(cmd,connectionString) > 0);
                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString)) > 0;

            }
        }


        /// <summary>
        /// Changes medal's sort order.
        /// </summary>
        /// <param name="boardId">ID of board.</param>
        /// <param name="medalID">ID of medal to re-sort.</param>
        /// <param name="move">Change of sort.</param>
        public static void medal_resort([NotNull] string connectionString, object boardId, object medalID, int move)
        {
            using (var cmd = PostgreDbAccess.GetCommand("medal_resort"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_move", NpgsqlDbType.Integer)).Value = move;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Deletes medal allocation to a group.
        /// </summary>
        /// <param name="groupID">ID of group owning medal.</param>
        /// <param name="medalID">ID of medal.</param>
        public static void group_medal_delete([NotNull] string connectionString, object groupID, object medalID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_medal_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists medal(s) assigned to the group
        /// </summary>
        /// <param name="groupID">ID of group of which to list medals.</param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable group_medal_list([NotNull] string connectionString, object groupID, object medalID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_medal_list"))
            {
                if (groupID == null)
                {
                    groupID = DBNull.Value;
                }
                if (medalID == null)
                {
                    medalID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Saves new or update existing group-medal allocation.
        /// </summary>
        /// <param name="groupID">ID of user group.</param>
        /// <param name="medalID">ID of medal.</param>
        /// <param name="message">Medal message, to override medal's default one. Can be null.</param>
        /// <param name="hide">Hide medal in user box.</param>
        /// <param name="onlyRibbon">Show only ribbon bar in user box.</param>
        /// <param name="sortOrder">Sort order in user box. Overrides medal's default sort order.</param>
        public static void group_medal_save(
            [NotNull] string connectionString,
            object groupID,
            object medalID,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder)
        {
            int sortOrderOut = 0;
            bool result = Int32.TryParse(sortOrder.ToString(), out sortOrderOut);
            if (result)
            {
                if (sortOrderOut >= 255)
                {
                    sortOrderOut = 0;
                }
            }

            using (var cmd = PostgreDbAccess.GetCommand("group_medal_save"))
            {
                if (message == null)
                {
                    message = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Varchar)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_hide", NpgsqlDbType.Boolean)).Value = hide;
                cmd.Parameters.Add(new NpgsqlParameter("i_onlyribbon", NpgsqlDbType.Boolean)).Value = onlyRibbon;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrderOut;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }



        /// <summary>
        /// Deletes medal allocation to a user.
        /// </summary>
        /// <param name="userId">ID of user owning medal.</param>
        /// <param name="medalID">ID of medal.</param>
        public static void user_medal_delete([NotNull] string connectionString, object userId, object medalID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_medal_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists medal(s) assigned to the group
        /// </summary>
        /// <param name="userId">ID of user who was given medal.</param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable user_medal_list([NotNull] string connectionString, object userId, object medalID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_medal_list"))
            {
                if (userId == null)
                {
                    userId = DBNull.Value;
                }
                if (medalID == null)
                {
                    medalID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Saves new or update existing user-medal allocation.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <param name="medalID">ID of medal.</param>
        /// <param name="message">Medal message, to override medal's default one. Can be null.</param>
        /// <param name="hide">Hide medal in user box.</param>
        /// <param name="onlyRibbon">Show only ribbon bar in user box.</param>
        /// <param name="sortOrder">Sort order in user box. Overrides medal's default sort order.</param>
        /// <param name="dateAwarded">Date when medal was awarded to a user. Is ignored when existing user-medal allocation is edited.</param>
        public static void user_medal_save(
            [NotNull] string connectionString,
            object userId,
            object medalID,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder,
            object dateAwarded)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_medal_save"))
            {
                if (message == null)
                {
                    message = DBNull.Value;
                }
                if (dateAwarded == null)
                {
                    dateAwarded = DBNull.Value;
                }
                if (sortOrder == null)
                {
                    sortOrder = 0;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_medalid", NpgsqlDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Varchar)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_hide", NpgsqlDbType.Boolean)).Value = hide;
                cmd.Parameters.Add(new NpgsqlParameter("i_onlyribbon", NpgsqlDbType.Boolean)).Value = onlyRibbon;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrder;
                cmd.Parameters.Add(new NpgsqlParameter("i_dateawarded", NpgsqlDbType.Timestamp)).Value = dateAwarded;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists all medals held by user as they are to be shown in user box.
        /// </summary>
        /// <param name="userId">ID of user.</param>
        /// <returns>List of medals, ribbon bar only first.</returns>
        public static DataTable user_listmedals([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_listmedals"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpForum

        public static IEnumerable<TypedNntpForum> NntpForumList(
            [NotNull] string connectionString, int boardId, int? minutes, int? nntpForumID, bool? active)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpforum_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_minutes", NpgsqlDbType.Integer)).Value = minutes;
                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_active", NpgsqlDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedNntpForum(r));
            }
        }

        public static DataTable nntpforum_list(
            [NotNull] string connectionString, object boardId, object minutes, object nntpForumID, object active)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpforum_list"))
            {
                if (minutes == null)
                {
                    minutes = DBNull.Value;
                }
                if (nntpForumID == null)
                {
                    nntpForumID = DBNull.Value;
                }
                if (active == null)
                {
                    active = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_minutes", NpgsqlDbType.Integer)).Value = minutes;
                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_active", NpgsqlDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void nntpforum_update(
            [NotNull] string connectionString, object nntpForumID, object lastMessageNo, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpforum_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_lastmessageno", NpgsqlDbType.Integer)).Value = lastMessageNo;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpforum_save(
            [NotNull] string connectionString,
            object nntpForumID,
            object nntpServerID,
            object groupName,
            object forumID,
            object active,
            object cutoffdate)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpforum_save"))
            {
                if (nntpForumID == null)
                {
                    nntpForumID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_nntpserverid", NpgsqlDbType.Integer)).Value = nntpServerID;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupname", NpgsqlDbType.Varchar)).Value = groupName;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_active", NpgsqlDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new NpgsqlParameter("i_datecutoff", NpgsqlDbType.Timestamp)).Value = cutoffdate;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpforum_delete([NotNull] string connectionString, object nntpForumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpServer

        public static DataTable nntpserver_list([NotNull] string connectionString, object boardId, object nntpServerID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpserver_list"))
            {
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }
                if (nntpServerID == null)
                {
                    nntpServerID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_nntpserverid", NpgsqlDbType.Integer)).Value = nntpServerID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void nntpserver_save(
            [NotNull] string connectionString,
            object nntpServerID,
            object boardId,
            object name,
            object address,
            object port,
            object userName,
            object userPass)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpserver_save"))
            {
                if (nntpServerID == null)
                {
                    nntpServerID = DBNull.Value;
                }
                if (userName == null)
                {
                    userName = DBNull.Value;
                }
                if (userPass == null)
                {
                    userPass = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpserverid", NpgsqlDbType.Integer)).Value = nntpServerID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_address", NpgsqlDbType.Varchar)).Value = address;
                cmd.Parameters.Add(new NpgsqlParameter("i_port", NpgsqlDbType.Integer)).Value = port;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_userpass", NpgsqlDbType.Varchar)).Value = userPass;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpserver_delete([NotNull] string connectionString, object nntpServerID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntpserver_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpserverid", NpgsqlDbType.Integer)).Value = nntpServerID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpTopic

        public static DataTable nntptopic_list([NotNull] string connectionString, object thread)
        {
            using (var cmd = PostgreDbAccess.GetCommand("nntptopic_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_thread", NpgsqlDbType.Varchar)).Value = thread;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void nntptopic_savemessage(
            [NotNull] string connectionString,
            object nntpForumID,
            object topic,
            object body,
            object userId,
            object userName,
            object ip,
            object posted,
            object externalMessageId,
            object referenceMessageId)
        {
            // string newbody = body.ToString().Replace(@"&lt;br&gt;", "> ").Replace(@"&amp;lt;", "<").Replace(@"&lt;hr&gt;", "> ").Replace(@"&amp;quot;", @"""").Replace(@"&lt;", @"<").Replace(@"br&gt;", @"> ").Replace(@"&amp;gt;", @"> ").Replace(@"&gt;", "> ").Replace("&quot;", @"""").Replace("[-snip-]","(SNIP)").Replace(@"@","[dog]").Replace("_.","");
            using (var cmd = PostgreDbAccess.GetCommand("nntptopic_savemessage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_nntpforumid", NpgsqlDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topic", NpgsqlDbType.Varchar)).Value = topic;
                cmd.Parameters.Add(new NpgsqlParameter("i_body", NpgsqlDbType.Text)).Value = body;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_ip", NpgsqlDbType.Varchar)).Value = ip;
                cmd.Parameters.Add(new NpgsqlParameter("i_posted", NpgsqlDbType.Timestamp)).Value = posted;
                cmd.Parameters.Add(new NpgsqlParameter("i_externalmessageid", NpgsqlDbType.Varchar)).Value =
                    externalMessageId;
                cmd.Parameters.Add(new NpgsqlParameter("i_referencemessageid", NpgsqlDbType.Varchar)).Value =
                    referenceMessageId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_PMessage

        /// <summary>
        /// The pmessage_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userPMessageID">
        /// The user p message id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable pmessage_list([NotNull] string connectionString, object userPMessageID)
        {
            return pmessage_list(connectionString, null, null, userPMessageID);
        }

        /// <summary>
        /// Returns a list of private messages based on the arguments specified.
        /// If pMessageID != null, returns the PM of id pMessageId.
        /// If toUserID != null, returns the list of PMs sent to the user with the given ID.
        /// If fromUserID != null, returns the list of PMs sent by the user of the given ID.
        /// </summary>
        /// <param name="toUserID"></param>
        /// <param name="fromUserID"></param>
        /// <param name="pMessageID">The id of the private message</param>
        /// <returns></returns>
        public static DataTable pmessage_list(
            [NotNull] string connectionString, object toUserID, object fromUserID, object userPMessageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = toUserID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = fromUserID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_userpmessageid", NpgsqlDbType.Integer)).Value = userPMessageID ?? DBNull.Value;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes the private message from the database as per the given parameter.  If fromOutbox is true,
        /// the message is only deleted from the user's outbox.  Otherwise, it is completely delete from the database.
        /// </summary>
        /// <param name="userPMessageID"></param>
        public static void pmessage_delete([NotNull] string connectionString, object userPMessageID)
        {
            pmessage_delete(connectionString, userPMessageID, false);
        }

        /// <summary>
        /// Deletes the private message from the database as per the given parameter.  If <paramref name="fromOutbox"/> is true,
        /// the message is only removed from the user's outbox.  Otherwise, it is completely delete from the database.
        /// </summary>
        /// <param name="pMessageID"></param>
        /// <param name="fromOutbox">If true, removes the message from the outbox.  Otherwise deletes the message completely.</param>
        public static void pmessage_delete([NotNull] string connectionString, object userPMessageID, bool fromOutbox)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_delete"))
            {
                // if (fromOutbox != false || fromOutbox != true) { fromOutbox = false; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userpmessageid", NpgsqlDbType.Integer)).Value = userPMessageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromoutbox", NpgsqlDbType.Boolean)).Value = fromOutbox;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Archives the private message of the given id.  Archiving moves the message from the user's inbox to his message archive.
        /// </summary>
        /// <param name="pMessageID">The ID of the private message</param>
        public static void pmessage_archive([NotNull] string connectionString, object userPMessageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_archive"))
            {
                if (userPMessageID == null)
                {
                    userPMessageID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userpmessageid", NpgsqlDbType.Integer)).Value = userPMessageID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void pmessage_save(
            [NotNull] string connectionString,
            object fromUserID,
            object toUserID,
            object subject,
            object body,
            object Flags,
            object replyTo)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = toUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = subject;
                cmd.Parameters.Add(new NpgsqlParameter("i_body", NpgsqlDbType.Text)).Value = body;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = Flags;
                cmd.Parameters.Add(new NpgsqlParameter("i_replyto", NpgsqlDbType.Integer)).Value = replyTo;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void pmessage_markread([NotNull] string connectionString, object userPMessageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_markread"))
            {
                if (userPMessageID == null)
                {
                    userPMessageID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userpmessageid", NpgsqlDbType.Integer)).Value = userPMessageID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable pmessage_info([NotNull] string connectionString )
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_info"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void pmessage_prune([NotNull] string connectionString, object daysRead, object daysUnread)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pmessage_prune"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_daysread", NpgsqlDbType.Integer)).Value = daysRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_daysunread", NpgsqlDbType.Integer)).Value = daysUnread;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Poll

        /// <summary>
        /// The pollgroup_stats.
        /// </summary>
        /// <param name="pollGroupId">
        /// The poll group id.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable pollgroup_stats([NotNull] string connectionString, int? pollGroupId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollgroup_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("i_pollgroupid", pollGroupId);
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The pollgroup_attach.
        /// </summary>
        /// <param name="pollGroupId">
        /// The poll group id.
        /// </param>
        /// <returns>
        /// </returns>
        public static int pollgroup_attach(
            [NotNull] string connectionString, int? pollGroupId, int? topicId, int? forumId, int? categoryId, int? boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollgroup_attach"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("i_pollgroupid", pollGroupId);
                cmd.Parameters.AddWithValue("i_topicid", topicId);
                cmd.Parameters.AddWithValue("i_forumid", forumId);
                cmd.Parameters.AddWithValue("i_categoryid", categoryId);
                cmd.Parameters.AddWithValue("i_boardid", boardId);
                return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable poll_stats([NotNull] string connectionString, int? pollId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("poll_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save([NotNull] string connectionString, List<PollSaveList> pollList)
        {
            int? currPoll;
            int? pollGroup = null;
            foreach (PollSaveList question in pollList)
            {
                StringBuilder pgStr = new StringBuilder();
                // Check if the group already exists
                if (question.TopicId > 0)
                {
                    pgStr.Append("select pollid  from ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("topic"));
                    pgStr.Append(" WHERE topicid = :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    pgStr.Append("select  pollgroupid  from ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("forum"));
                    pgStr.Append(" WHERE forumid =:i_forumid");
                }
                else if (question.CategoryId > 0)
                {
                    pgStr.Append("select pollgroupid  from ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("category"));
                    pgStr.Append(" WHERE categoryid = :i_categoryid");
                }

                using (var cmdPg = PostgreDbAccess.GetCommand(pgStr.ToString(), true))
                {
                    // Add parameters
                    if (question.TopicId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value =
                            question.TopicId;
                    }
                    else if (question.ForumId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value =
                            question.ForumId;
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmdPg.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value =
                            question.CategoryId;
                    }

                    object pgexist = PostgreDbAccess.ExecuteScalar(cmdPg, true, connectionString);
                    if (pgexist != DBNull.Value)
                    {
                        pollGroup = Convert.ToInt32(pgexist);
                    }

                }
                // buck stops here
                // the group doesn't exists, create a new one
                if (pollGroup == null)
                {
                    pgStr = new StringBuilder();
                    pgStr.Append("INSERT INTO ");
                    pgStr.Append(PostgreDbAccess.GetObjectName("pollgroupcluster"));
                    pgStr.Append("(userid,flags ) VALUES(:i_userid, :i_flags) RETURNING pollgroupid; ");
                    using (var cmdPgIns = PostgreDbAccess.GetCommand(pgStr.ToString(), true))
                    {
                        // set poll group flags
                        int pollGroupFlags = 0;
                        if (question.IsBound)
                        {
                            pollGroupFlags = pollGroupFlags | 2;
                        }

                        // Add parameters
                        cmdPgIns.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value =
                            question.UserId;
                        cmdPgIns.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value =
                            pollGroupFlags;
                        pollGroup = (int?)PostgreDbAccess.ExecuteScalar(cmdPgIns, true, connectionString);
                    }
                }




                using (var cmd = PostgreDbAccess.GetCommand("poll_save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("i_question", NpgsqlDbType.Varchar)).Value =
                        question.Question;

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("i_closes", NpgsqlDbType.Timestamp)).Value =
                            question.Closes;
                    }
                    else
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("i_closes", NpgsqlDbType.Timestamp)).Value =
                            DBNull.Value;
                    }
                    int pollFlags = question.IsClosedBound ? 0 | 4 : 0;
                    pollFlags = question.AllowMultipleChoices ? pollFlags | 8 : pollFlags;
                    pollFlags = question.ShowVoters ? pollFlags | 16 : pollFlags;
                    pollFlags = question.AllowSkipVote ? pollFlags | 32 : pollFlags;
                    cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = question.UserId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                    cmd.Parameters.Add(new NpgsqlParameter("i_objectpath", NpgsqlDbType.Varchar)).Value =
                        question.QuestionObjectPath;
                    cmd.Parameters.Add(new NpgsqlParameter("i_mimetype", NpgsqlDbType.Varchar)).Value =
                        question.QuestionMimeType;
                    cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = pollFlags;


                    currPoll = (int?)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
                }


                // The cycle through question reply choices  
                int chl = question.Choice.GetUpperBound(1) + 1;
                for (uint choiceCount = 0; choiceCount < chl; choiceCount++)
                {
                    if (question.Choice[0, choiceCount].Trim().Length > 0)
                    {
                        StringBuilder sbChoice = new StringBuilder();
                        sbChoice.Append("INSERT INTO ");
                        sbChoice.Append(PostgreDbAccess.GetObjectName("choice"));
                        sbChoice.AppendFormat(
                            "(pollid,choice,votes,objectpath,mimetype) VALUES (:pollid{0}, :choice{0}, :votes{0}, :objectpath{0}, :mimetype{0}); ",
                            choiceCount);
                        using (var cmdChoice = PostgreDbAccess.GetCommand(sbChoice.ToString(), true))
                        {
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("pollid{0}", choiceCount), NpgsqlDbType.Integer))
                                     .Value = currPoll;
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("choice{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[0, choiceCount];
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("votes{0}", choiceCount), NpgsqlDbType.Integer)).Value
                                = 0;
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("objectpath{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[1, choiceCount].IsNotSet()
                                                  ? String.Empty
                                                  : question.Choice[1, choiceCount];
                            cmdChoice.Parameters.Add(
                                new NpgsqlParameter(String.Format("mimetype{0}", choiceCount), NpgsqlDbType.Varchar))
                                     .Value = question.Choice[2, choiceCount].IsNotSet()
                                                  ? String.Empty
                                                  : question.Choice[2, choiceCount];
                            PostgreDbAccess.ExecuteNonQuery(cmdChoice, true, connectionString);
                        }

                    }

                }
                //   var cmd = new NpgsqlCommand();
                //  cmd.CommandText = paramSb.ToString() + ")" + sb.ToString();
                //   NpgsqlConnection con = PostgreDbAccess.Current.GetConnectionManager().DBConnection;
                // con.Open();
                //  cmd.Connection = con;
                //   IDbTransaction trans = cmd.Connection.BeginTransaction();
                //    cmd.Transaction = trans;
                //    cmd.CommandText = sb.ToString();


                /* using (var cmd1 = PostgreDbAccess.GetCommand(sb.ToString(), true))
                {


                    // Add parameters
                     cmd1.Parameters.Add(new NpgsqlParameter("question", NpgsqlDbType.Varchar));

                    if (question.Closes > DateTimeHelper.SqlDbMinTime())
                    {
                        cmd1.Parameters.Add(new NpgsqlParameter("closes", NpgsqlDbType.Timestamp));
                    } 
                    for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetLength(0); choiceCount1++)
                    {
                        if (question.Choice[0, choiceCount1].Trim().Length > 0)
                        {
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("choice{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[0, choiceCount1];
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("votes{0}", choiceCount1),
                                                                    NpgsqlDbType.Integer)).Value = 0;
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("objectpath{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[1, choiceCount1].IsNotSet() ? String.Empty : question.Choice[1, choiceCount1];
                            cmd1.Parameters.Add(new NpgsqlParameter(String.Format("mimetype{0}", choiceCount1),
                                                                    NpgsqlDbType.Varchar)).Value =
                                question.Choice[2, choiceCount1].IsNotSet() ? String.Empty : question.Choice[2, choiceCount1];
                        }
                    }
                     int? result = (int?)PostgreDbAccess.ExecuteNonQueryInt(cmd1, true);
                }
            */

                // Add pollgroup id to an object
                StringBuilder Usb = new StringBuilder();
                //cmd2.Parameters.Add(new NpgsqlParameter(":i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                if (question.TopicId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(PostgreDbAccess.GetObjectName("topic"));
                    Usb.Append(" SET pollid = :i_pollid WHERE topicid= :i_topicid; ");
                }
                else if (question.ForumId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(PostgreDbAccess.GetObjectName("forum"));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where forumid = :i_forumid; ");

                }
                else if (question.CategoryId > 0)
                {
                    Usb.Append("UPDATE ");
                    Usb.Append(PostgreDbAccess.GetObjectName("category"));
                    Usb.Append(" SET pollgroupid = :i_pollgroupid where categoryid = :i_categoryid; ");
                }


                using (var cmd2 = PostgreDbAccess.GetCommand(Usb.ToString(), true))
                {
                    cmd2.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollGroup;
                    //cmd2.Parameters.Add(new NpgsqlParameter(":i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroup;
                    if (question.TopicId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value =
                            question.TopicId;
                    }
                    else if (question.ForumId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value =
                            question.ForumId;
                    }
                    else if (question.CategoryId > 0)
                    {
                        cmd2.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value =
                            question.CategoryId;
                    }
                    PostgreDbAccess.ExecuteNonQuery(cmd2, connectionString);
                }


                /* if (ret.Value != DBNull.Value)
                     {
                         return (int?)ret.Value;
                     }*/

                //  }
                //   trans.Commit();
                //   con.Close();

            }

            return pollGroup;
        }


        public static void poll_update(
            [NotNull] string connectionString,
            object pollID,
            object question,
            object closes,
            object isBounded,
            bool isClosedBounded,
            bool allowMultipleChoices,
            bool showVoters,
            bool allowSkipVote,
            object questionPath,
            object questionMime)
        {
            using (var cmd = PostgreDbAccess.GetCommand("poll_update"))
            {
                if (closes == null)
                {
                    closes = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollID;
                cmd.Parameters.Add(new NpgsqlParameter("i_question", NpgsqlDbType.Varchar)).Value = question;
                cmd.Parameters.Add(new NpgsqlParameter("i_closes", NpgsqlDbType.Timestamp)).Value = closes;
                cmd.Parameters.Add(new NpgsqlParameter("i_questionobjectpath", NpgsqlDbType.Varchar)).Value =
                    questionPath;
                cmd.Parameters.Add(new NpgsqlParameter("i_questionmimetype", NpgsqlDbType.Varchar)).Value = questionMime;
                cmd.Parameters.Add(new NpgsqlParameter("i_isbounded", NpgsqlDbType.Boolean)).Value = isBounded;
                cmd.Parameters.Add(new NpgsqlParameter("i_isclosedbounded", NpgsqlDbType.Boolean)).Value =
                    isClosedBounded;
                cmd.Parameters.Add(new NpgsqlParameter("i_allowmultiplechoices", NpgsqlDbType.Boolean)).Value =
                    allowMultipleChoices;
                cmd.Parameters.Add(new NpgsqlParameter("i_showvoters", NpgsqlDbType.Boolean)).Value = showVoters;
                cmd.Parameters.Add(new NpgsqlParameter("i_allowskipvote", NpgsqlDbType.Boolean)).Value = allowSkipVote;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void poll_remove(
            [NotNull] string connectionString,
            object pollGroupID,
            object pollID,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere)
        {
            using (var cmd = PostgreDbAccess.GetCommand("poll_remove"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_removecompletely", NpgsqlDbType.Boolean)).Value =
                    removeCompletely;
                cmd.Parameters.Add(new NpgsqlParameter("i_removeeverywhere", NpgsqlDbType.Boolean)).Value =
                    removeEverywhere;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a typed poll group list.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<TypedPollGroup> PollGroupList(
            [NotNull] string connectionString, int userID, int? forumId, int boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollgroup_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return PostgreDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedPollGroup(r));
            }
        }

        /// <summary>
        /// The poll_remove.
        /// </summary>
        /// <param name="pollGroupID">
        /// The poll group id. The parameter should always be present. 
        /// </param>
        /// <param name="topicId">
        /// The poll id. If null all polls in a group a deleted. 
        /// </param>
        /// <param name="boardId">
        /// The BoardID id. 
        /// </param>
        /// <param name="removeCompletely">
        /// The RemoveCompletely. If true and pollID is null , all polls in a group are deleted completely, 
        /// else only one poll is deleted completely. 
        /// </param>
        /// <param name="forumId"></param>
        /// <param name="removeEverywhere"></param>
        public static void pollgroup_remove(
            [NotNull] string connectionString,
            object pollGroupID,
            object topicId,
            object forumId,
            object categoryId,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere)
        {
            using (var cmd = PostgreDbAccess.GetCommand("pollgroup_remove"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_pollgroupid", NpgsqlDbType.Integer)).Value = pollGroupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_removecompletely", NpgsqlDbType.Boolean)).Value =
                    removeCompletely;
                cmd.Parameters.Add(new NpgsqlParameter("i_removeeverywhere", NpgsqlDbType.Boolean)).Value =
                    removeEverywhere;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        public static void choice_delete([NotNull] string connectionString, object choiceID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("choice_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_choiceid", NpgsqlDbType.Integer)).Value = choiceID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void choice_update(
            [NotNull] string connectionString, object choiceID, object choice, object path, object mime)
        {
            using (var cmd = PostgreDbAccess.GetCommand("choice_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_choiceid", NpgsqlDbType.Integer)).Value = choiceID;
                cmd.Parameters.Add(new NpgsqlParameter("i_choice", NpgsqlDbType.Varchar)).Value = choice;
                cmd.Parameters.Add(new NpgsqlParameter("i_objectpath", NpgsqlDbType.Varchar)).Value = path;
                cmd.Parameters.Add(new NpgsqlParameter("i_mimetype", NpgsqlDbType.Varchar)).Value = mime;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void choice_add([NotNull] string connectionString, object pollID, object choice, object path, object mime)
        {
            using (var cmd = PostgreDbAccess.GetCommand("choice_add"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_pollid", NpgsqlDbType.Integer)).Value = pollID;

                if (choice != null)
                {
                    cmd.Parameters.Add(new NpgsqlParameter("i_choice", NpgsqlDbType.Varchar)).Value = choice;
                }
                else
                {
                    cmd.Parameters.Add(new NpgsqlParameter("i_choice", NpgsqlDbType.Varchar));
                    cmd.Parameters[1].Value = "No input value supplied";
                }

                cmd.Parameters.Add(new NpgsqlParameter("i_objectpath", NpgsqlDbType.Varchar)).Value = path;
                cmd.Parameters.Add(new NpgsqlParameter("i_mimetype", NpgsqlDbType.Varchar)).Value = mime;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Rank

        public static DataTable rank_list([NotNull] string connectionString, object boardId, object rankID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("rank_list"))
            {
                if (rankID == null)
                {
                    rankID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void rank_save(
            [NotNull] string connectionString,
            object rankID,
            object boardId,
            object name,
            object isStart,
            object isLadder,
            [NotNull] object isGuest,
            object minPosts,
            object rankImage,
            object pmLimit,
            object style,
            object sortOrder,
            object description,
            object usrSigChars,
            object usrSigBBCodes,
            object usrSigHTMLTags,
            object usrAlbums,
            object usrAlbumImages)
        {
            using (var cmd = PostgreDbAccess.GetCommand("rank_save"))
            {
                if (rankImage == null)
                {
                    rankImage = DBNull.Value;
                }
                if (minPosts.ToString() == string.Empty)
                {
                    minPosts = 0;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_isstart", NpgsqlDbType.Boolean)).Value = isStart;
                cmd.Parameters.Add(new NpgsqlParameter("i_isladder", NpgsqlDbType.Boolean)).Value = isLadder;
                cmd.Parameters.Add(new NpgsqlParameter("i_isguest", NpgsqlDbType.Boolean)).Value = isGuest;
                cmd.Parameters.Add(new NpgsqlParameter("i_minposts", NpgsqlDbType.Integer)).Value = minPosts;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankimage", NpgsqlDbType.Varchar)).Value = rankImage;
                cmd.Parameters.Add(new NpgsqlParameter("i_pmlimit", NpgsqlDbType.Integer)).Value = pmLimit;
                cmd.Parameters.Add(new NpgsqlParameter("i_style", NpgsqlDbType.Varchar)).Value = style;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrder;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsigchars", NpgsqlDbType.Integer)).Value = usrSigChars;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsigbbcodes", NpgsqlDbType.Varchar)).Value = usrSigBBCodes;
                cmd.Parameters.Add(new NpgsqlParameter("i_usrsightmltags", NpgsqlDbType.Varchar)).Value = usrSigHTMLTags;
                cmd.Parameters.Add(new NpgsqlParameter("i_usralbums", NpgsqlDbType.Integer)).Value = usrAlbums;
                cmd.Parameters.Add(new NpgsqlParameter("i_usralbumimages", NpgsqlDbType.Integer)).Value = usrAlbumImages;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void rank_delete([NotNull] string connectionString, object rankID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("rank_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Smiley

        public static DataTable smiley_list([NotNull] string connectionString, object boardId, object smileyID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_list"))
            {
                if (smileyID == null)
                {
                    smileyID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_smileyid", NpgsqlDbType.Integer)).Value = smileyID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The smiley_list.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="smileyID">
        /// The smiley id.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<TypedSmileyList> SmileyList([NotNull] string connectionString, int boardId, int? smileyID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_smileyid", NpgsqlDbType.Integer)).Value = smileyID;

                return PostgreDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedSmileyList(r));
            }
        }

        public static DataTable smiley_listunique([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_listunique"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void smiley_delete([NotNull] string connectionString, object smileyID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_delete"))
            {
                if (smileyID == null)
                {
                    smileyID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_smileyid", NpgsqlDbType.Integer)).Value = smileyID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void smiley_save(
            [NotNull] string connectionString,
            object smileyID,
            object boardId,
            object code,
            object icon,
            object emoticon,
            object sortOrder,
            object replace)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_save"))
            {
                if (smileyID == null)
                {
                    smileyID = DBNull.Value;
                }

                if (replace == null)
                {
                    replace = 0;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_smileyid", NpgsqlDbType.Integer)).Value = smileyID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_code", NpgsqlDbType.Varchar)).Value = code;
                cmd.Parameters.Add(new NpgsqlParameter("i_icon", NpgsqlDbType.Varchar)).Value = icon;
                cmd.Parameters.Add(new NpgsqlParameter("i_emoticon", NpgsqlDbType.Varchar)).Value = emoticon;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortorder", NpgsqlDbType.Smallint)).Value = sortOrder;
                cmd.Parameters.Add(new NpgsqlParameter("i_replace", NpgsqlDbType.Smallint)).Value = replace;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void smiley_resort([NotNull] string connectionString, object boardId, object smileyID, int move)
        {
            using (var cmd = PostgreDbAccess.GetCommand("smiley_resort"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_smileyid", NpgsqlDbType.Integer)).Value = smileyID;
                cmd.Parameters.Add(new NpgsqlParameter("i_move", NpgsqlDbType.Integer)).Value = move;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_BBCode

        /// <summary>
        /// The bbcode_list.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="bbcodeID">
        /// The bbcode id.
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        public static IEnumerable<TypedBBCode> BBCodeList([NotNull] string connectionString, int boardID, int? bbcodeID)
        {
            return bbcode_list(connectionString, boardID, bbcodeID).AsEnumerable().Select(o => new TypedBBCode(o));
        }

        public static DataTable bbcode_list([NotNull] string connectionString, object boardId, object bbcodeID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bbcode_list"))
            {
                if (bbcodeID == null)
                {
                    bbcodeID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_bbcodeid", NpgsqlDbType.Integer)).Value = bbcodeID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void bbcode_delete([NotNull] string connectionString, object bbcodeID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bbcode_delete"))
            {
                if (bbcodeID == null)
                {
                    bbcodeID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_bbcodeid", NpgsqlDbType.Integer)).Value = bbcodeID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void bbcode_save(
            [NotNull] string connectionString,
            object bbcodeID,
            object boardId,
            object name,
            object description,
            object onclickjs,
            object displayjs,
            object editjs,
            object displaycss,
            object searchregex,
            object replaceregex,
            object variables,
            object usemodule,
            object moduleclass,
            object execorder)
        {
            using (var cmd = PostgreDbAccess.GetCommand("bbcode_save"))
            {
                // My input defaults
                if (bbcodeID == null)
                {
                    bbcodeID = DBNull.Value;
                }

                if (description == null)
                {
                    description = DBNull.Value;
                }

                if (onclickjs == null)
                {
                    onclickjs = DBNull.Value;
                }

                if (displayjs == null)
                {
                    displayjs = DBNull.Value;
                }
                if (editjs == null)
                {
                    editjs = DBNull.Value;
                }

                if (displaycss == null)
                {
                    displaycss = DBNull.Value;
                }

                if (variables == null)
                {
                    variables = DBNull.Value;
                }

                if (usemodule == null || usemodule.ToString().Contains("false"))
                {
                    usemodule = 0;
                }

                if (usemodule.ToString().Contains("true"))
                {
                    usemodule = 1;
                }

                if (moduleclass == null)
                {
                    moduleclass = DBNull.Value;
                }

                if (execorder == null)
                {
                    execorder = 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_bbcodeid", NpgsqlDbType.Integer)).Value = bbcodeID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_onclickjs", NpgsqlDbType.Varchar)).Value = onclickjs;
                cmd.Parameters.Add(new NpgsqlParameter("i_displayjs", NpgsqlDbType.Text)).Value = displayjs;
                cmd.Parameters.Add(new NpgsqlParameter("i_editjs", NpgsqlDbType.Text)).Value = editjs;
                cmd.Parameters.Add(new NpgsqlParameter("i_displaycss", NpgsqlDbType.Text)).Value = displaycss;
                cmd.Parameters.Add(new NpgsqlParameter("i_searchregex", NpgsqlDbType.Text)).Value = searchregex;
                cmd.Parameters.Add(new NpgsqlParameter("i_replaceregex", NpgsqlDbType.Text)).Value = replaceregex;
                cmd.Parameters.Add(new NpgsqlParameter("i_variables", NpgsqlDbType.Varchar)).Value = variables;
                cmd.Parameters.Add(new NpgsqlParameter("i_usemodule", NpgsqlDbType.Boolean)).Value =
                    Convert.ToBoolean(usemodule);
                cmd.Parameters.Add(new NpgsqlParameter("i_moduleclass", NpgsqlDbType.Varchar)).Value = moduleclass;
                cmd.Parameters.Add(new NpgsqlParameter("i_execorder", NpgsqlDbType.Integer)).Value = execorder;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Registry

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="name">Use to specify return of specific entry only. Setting this to null returns all entries.
        /// </param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, object name, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("registry_list"))
            {
                if (name == null)
                {
                    name = DBNull.Value;
                }

                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, [NotNull] object name)
        {
            return registry_list(connectionString, name, null);
        }

        /// <summary>
        /// Retrieves all the entries in the board settings registry
        /// </summary>
        /// <returns>DataTable filled will all registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString )
        {
            return registry_list(connectionString, null, null);
        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="Name">Unique name associated with this entry</param>
        /// <param name="Value">Value associated with this entry which can be null</param>
        public static void registry_save([NotNull] string connectionString, object name, object value)
        {

            registry_save(connectionString, name, value, DBNull.Value);

        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="Name">Unique name associated with this entry</param>
        /// <param name="Value">Value associated with this entry which can be null</param>
        /// <param name="BoardID">The BoardID for this entry</param>
        public static void registry_save([NotNull] string connectionString, object name, object value, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("registry_save"))
            {
                if (value == null)
                {
                    value = DBNull.Value;
                }
                if (boardId == null)
                {
                    boardId = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_value", NpgsqlDbType.Text)).Value = value;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_System

        /// <summary>
        /// Not in use anymore. Only required for old database versions.
        /// </summary>
        /// <returns></returns>
        public static DataTable system_list([NotNull] string connectionString )
        {
            using (var cmd = PostgreDbAccess.GetCommand("system_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Topic

        public static DataTable topic_tags([NotNull] string connectionString, object boardId, object pageUserId, object topicId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_tags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }

        }

        public static DataTable topic_bytags(
            [NotNull] string connectionString,
            object boardId,
            int forumId,
            object pageUserId,
            object tags,
            object sinceDate,
            int pageIndex,
            int pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_bytags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_tags", NpgsqlDbType.Varchar)).Value = tags;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }

        }


        public static void topic_updatetopic([NotNull] string connectionString, int topicId, string topic)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_updatetopic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new NpgsqlParameter("i_topic", NpgsqlDbType.Varchar)).Value = topic;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static int topic_prune(
            [NotNull] string connectionString, object boardId, object forumID, object days, object permDelete)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_prune"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_days", NpgsqlDbType.Integer)).Value = days;
                cmd.Parameters.Add(new NpgsqlParameter("i_permdelete", NpgsqlDbType.Boolean)).Value =
                    Convert.ToBoolean(permDelete);

                return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable topic_list(
            [NotNull] string connectionString,
            object forumID,
            [NotNull] object userId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [NotNull] object showMoved,
            [CanBeNull] bool findLastRead, 
            [NotNull] bool getTags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_showmoved", NpgsqlDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_gettags", NpgsqlDbType.Boolean)).Value = getTags;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable announcements_list(
            [NotNull] string connectionString,
            [NotNull] object forumID,
            [NotNull] object userId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [NotNull] object showMoved,
            [CanBeNull] bool findLastRead, 
            [NotNull]bool getTags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("announcements_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_showmoved", NpgsqlDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_gettags", NpgsqlDbType.Boolean)).Value = getTags;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists topics very simply (for URL rewriting)
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable topic_simplelist([NotNull] string connectionString, int StartID, int Limit)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_simplelist"))
            {
                if (StartID <= 0)
                {
                    StartID = 0;
                }
                if (Limit <= 0)
                {
                    Limit = 500;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_startid", NpgsqlDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new NpgsqlParameter("i_limit", NpgsqlDbType.Integer)).Value = Limit;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void topic_move(
            [NotNull] string connectionString, object topicID, object forumID, object showMoved, object linkDays)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_move"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_showmoved", NpgsqlDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new NpgsqlParameter("i_linkdays", NpgsqlDbType.Integer)).Value = linkDays;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable topic_announcements(
            [NotNull] string connectionString, object boardId, object numOfPostsToRetrieve, object pageUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_announcements"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_numposts", NpgsqlDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable topic_latest(
            [NotNull] string connectionString,
            object boardID,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts,
            bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_latest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_numposts", NpgsqlDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_shownocountposts", NpgsqlDbType.Boolean)).Value =
                    showNoCountPosts;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The rss_topic_latest.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="numOfPostsToRetrieve">
        /// The num of posts to retrieve.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="useStyledNicks">
        /// If true returns string for userID style.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable rss_topic_latest(
            [NotNull] string connectionString,
            object boardId,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts)
        {
            using (var cmd = PostgreDbAccess.GetCommand("rss_topic_latest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_numposts", NpgsqlDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_shownocountposts", NpgsqlDbType.Boolean)).Value =
                    showNoCountPosts;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable topic_active(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_active"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        private static void topic_deleteAttachments([NotNull] string connectionString, object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_listmessages"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;

                using (DataTable dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        message_deleteRecursively(
                            connectionString, row["MessageID"], true, string.Empty, 0, true, false);
                    }
                }
            }
        }


        private static void topic_deleteimages([NotNull] string connectionString, int topicID)
        {

            string uploadDir = HostingEnvironment.MapPath(String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads, "/", YafBoardFolders.Current.Topics));

            try
            {
                string topicImage = string.Empty;
                var dt = topic_info(
                 connectionString, topicID, false);
                if (dt != null)
                {
                    topicImage = dt["TopicImage"].ToString();
                }

                string fileName = string.Format("{0}/{1}.{2}.yafupload", uploadDir, topicID, topicImage);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                string fileNameThumb = string.Format("{0}/{1}.thumb.{2}.yafupload", uploadDir, topicID, topicImage);
                if (System.IO.File.Exists(fileNameThumb))
                {
                    System.IO.File.Delete(fileNameThumb);
                }
            }
            catch
            {
                // error deleting that file... 
            }
        }
        public static void topic_delete([NotNull] string connectionString, object topicID)
        {
            topic_delete(connectionString, topicID, false);
        }

        public static void topic_delete([NotNull] string connectionString, object topicID, object eraseTopic)
        {
            if (eraseTopic == null)
            {
                eraseTopic = false;
            }
           

            if (eraseTopic.ToType<bool>())
            {
                topic_deleteAttachments(connectionString, topicID);

                topic_deleteimages(connectionString, (int)topicID);
            }

            using (var cmd = PostgreDbAccess.GetCommand("topic_delete"))
            {
               
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_updatelastpost", NpgsqlDbType.Boolean)).Value = true;
                cmd.Parameters.Add(new NpgsqlParameter("i_erasetopic", NpgsqlDbType.Boolean)).Value = eraseTopic.ToType<bool>();

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable topic_findprev([NotNull] string connectionString, object topicID)
        {
            DataTable dt;
            DataRow dr;
            using (var cmd = PostgreDbAccess.GetCommand("topic_findprev"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;

                dt = PostgreDbAccess.GetData(cmd, connectionString);
                dr = dt.Rows[0];
                if (dt.Rows[0][0] == DBNull.Value)
                {
                    return new DataTable();
                }

                return dt;
            }
        }

        public static DataTable topic_findnext([NotNull] string connectionString, object topicID)
        {
            DataTable dt;
            DataRow dr;
            using (var cmd = PostgreDbAccess.GetCommand("topic_findnext"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;

                dt = PostgreDbAccess.GetData(cmd, connectionString);
                dr = dt.Rows[0];
                if (dt.Rows[0][0] == DBNull.Value)
                {

                    return new DataTable();
                }

                return dt;
            }
        }

        public static void topic_lock([NotNull] string connectionString, object topicID, object locked)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_lock"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_locked", NpgsqlDbType.Boolean)).Value = locked;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static long topic_save(
            [NotNull] string connectionString,
            object forumID,
            object subject,
            object status,
            object styles,
            object description,
            object message,
             [CanBeNull] object messageDescription,
            object userId,
            object priority,
            object userName,
            object ip,
            object posted,
            object blogPostID,
            object flags,
            out long messageID,
            string tags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_subject", NpgsqlDbType.Varchar)).Value = subject;
                cmd.Parameters.Add(new NpgsqlParameter("i_status", NpgsqlDbType.Varchar)).Value = status;
                cmd.Parameters.Add(new NpgsqlParameter("i_styles", NpgsqlDbType.Varchar)).Value = styles;
                cmd.Parameters.Add(new NpgsqlParameter("i_description", NpgsqlDbType.Varchar)).Value = description;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Text)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_priority", NpgsqlDbType.Smallint)).Value = priority;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_ip", NpgsqlDbType.Varchar)).Value = ip;
                cmd.Parameters.Add(new NpgsqlParameter("i_posted", NpgsqlDbType.Timestamp)).Value = posted;
                cmd.Parameters.Add(new NpgsqlParameter("i_blogpostid", NpgsqlDbType.Varchar)).Value = blogPostID;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new NpgsqlParameter("i_messagedescription", NpgsqlDbType.Varchar)).Value = messageDescription;
                cmd.Parameters.Add(new NpgsqlParameter("i_tags", NpgsqlDbType.Text)).Value = tags;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                messageID = long.Parse(dt.Rows[0]["MessageID"].ToString());
                return long.Parse(dt.Rows[0]["TopicID"].ToString());
            }
        }

        public static DataRow topic_info([NotNull] string connectionString, object topicID, [NotNull] bool getTags)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_info"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_showdeleted", NpgsqlDbType.Boolean)).Value = false;
                cmd.Parameters.Add(new NpgsqlParameter("i_gettags", NpgsqlDbType.Boolean)).Value = getTags;

                using (var dt = PostgreDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        public static void topic_imagesave([NotNull] string connectionString, object topicID, [NotNull] object imageUrl, Stream stream, object avatarImageType)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_imagesave"))
            {
                byte[] data = null;
                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageurl", NpgsqlDbType.Varchar)).Value = imageUrl;
                cmd.Parameters.Add(new NpgsqlParameter("i_stream", NpgsqlDbType.Bytea)).Value = data;
                cmd.Parameters.Add(new NpgsqlParameter("i_avatarimagetype", NpgsqlDbType.Varchar)).Value = avatarImageType;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }
      
        public static int topic_findduplicate([NotNull] string connectionString, object topicName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_findduplicate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicname", NpgsqlDbType.Varchar)).Value = topicName;
                return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable topic_favorite_details(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_favorite_details"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_favorite_list.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable topic_favorite_list([NotNull] string connectionString, object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_favorite_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_favorite_remove.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void topic_favorite_remove([NotNull] string connectionString, object userID, object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_favorite_remove"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_favorite_add.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void topic_favorite_add([NotNull] string connectionString, object userID, object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_favorite_add"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_unanswered
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        ///  </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="useStyledNicks">
        /// Set to true to get color nicks for last user and topic starter.
        /// </param>
        /// <param name="findLastRead">
        /// Indicates if the Table should Countain the last Access Date
        /// </param>
        /// <returns>
        /// Returns the List with the Active Topics
        /// </returns>
        public static DataTable topic_unanswered(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_unanswered"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable topic_unread(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topic_unread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets all topics where the pageUserid has posted
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        ///  </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param> 
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since.
        /// </param>
        /// <param name="useStyledNicks">
        /// Set to true to get color nicks for last user and topic starter.
        /// </param>
        /// <param name="findLastRead">
        /// Indicates if the Table should Countain the last Access Date
        /// </param>
        /// <returns>
        /// Returns the List with the User Topics
        /// </returns>
        public static DataTable Topics_ByUser(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [CanBeNull] object categoryId,
            [NotNull] object pageUserId,
            [NotNull] object sinceDate,
            [NotNull] object toDate,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object useStyledNicks,
            [CanBeNull] bool findLastRead)
        {
            using (var cmd = PostgreDbAccess.GetCommand("topics_byuser"))
            {
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_categoryid", NpgsqlDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_sincedate", NpgsqlDbType.Timestamp)).Value = sinceDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_todate", NpgsqlDbType.Timestamp)).Value = toDate;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_findlastread", NpgsqlDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete a topic status.
        /// </summary>
        /// <param name="topicStatusID">The topic status ID.</param>
        public static void TopicStatus_Delete([NotNull] string connectionString, [NotNull] object topicStatusID)
        {
            try
            {
                using (var cmd = PostgreDbAccess.GetCommand("TopicStatus_Delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("i_TopicStatusID", NpgsqlDbType.Integer).Value = topicStatusID;
                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        /// <summary>
        /// Get a Topic Status by topicStatusID
        /// </summary>
        /// <param name="topicStatusID">The topic status ID.</param>
        /// <returns></returns>
        public static DataTable TopicStatus_Edit([NotNull] string connectionString, [NotNull] object topicStatusID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("TopicStatus_Edit"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_TopicStatusID", NpgsqlDbType.Integer).Value = topicStatusID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// List all Topics of the Current Board
        /// </summary>
        /// <param name="boardID">The board ID.</param>
        /// <returns></returns>
        public static DataTable TopicStatus_List([NotNull] string connectionString, [NotNull] object boardID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("TopicStatus_List"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_BoardID", NpgsqlDbType.Integer).Value = boardID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves a topic status
        /// </summary>
        /// <param name="topicStatusID">The topic status ID.</param>
        /// <param name="boardID">The board ID.</param>
        /// <param name="topicStatusName">Name of the topic status.</param>
        /// <param name="defaultDescription">The default description.</param>
        public static void TopicStatus_Save(
            [NotNull] string connectionString,
            [NotNull] object topicStatusID,
            [NotNull] object boardID,
            [NotNull] object topicStatusName,
            [NotNull] object defaultDescription)
        {
            try
            {
                using (var cmd = PostgreDbAccess.GetCommand("TopicStatus_Save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_TopicStatusID", NpgsqlDbType.Integer).Value = topicStatusID;
                    cmd.Parameters.Add("i_BoardID", NpgsqlDbType.Integer).Value = boardID;
                    cmd.Parameters.Add("i_TopicStatusName", NpgsqlDbType.Varchar).Value = topicStatusName;
                    cmd.Parameters.Add("i_DefaultDescription", NpgsqlDbType.Varchar).Value = defaultDescription;

                    PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        #endregion

        #region yaf_ReplaceWords

        // rico : replace words / begin
        /// <summary>
        /// Gets a list of replace words
        /// </summary>
        /// <returns>DataTable with replace words</returns>
        public static DataTable replace_words_list([NotNull] string connectionString, object boardId, object id)
        {
            using (var cmd = PostgreDbAccess.GetCommand("replace_words_list"))
            {
                if (id == null)
                {
                    id = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = id;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves changs to a words
        /// </summary>
        /// <param name="id">ID of bad/good word</param>
        /// <param name="badword">bad word</param>
        /// <param name="goodword">good word</param>
        public static void replace_words_save(
            [NotNull] string connectionString, object boardId, object id, object badword, object goodword)
        {
            using (var cmd = PostgreDbAccess.GetCommand("replace_words_save"))
            {
                if (id == null)
                {
                    id = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = id;
                cmd.Parameters.Add(new NpgsqlParameter("i_badword", NpgsqlDbType.Varchar)).Value = badword;
                cmd.Parameters.Add(new NpgsqlParameter("i_goodword", NpgsqlDbType.Varchar)).Value = goodword;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes a bad/good word
        /// </summary>
        /// <param name="ID">ID of bad/good word to delete</param>
        public static void replace_words_delete([NotNull] string connectionString, object id)
        {
            using (var cmd = PostgreDbAccess.GetCommand("replace_words_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_id", NpgsqlDbType.Integer)).Value = id;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_IgnoreUser

        public static void user_addignoreduser([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_addignoreduser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_ignoreduserid", NpgsqlDbType.Integer)).Value = ignoredUserId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_removeignoreduser([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_removeignoreduser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_ignoreduserid", NpgsqlDbType.Integer)).Value = ignoredUserId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static bool user_isuserignored([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_isuserignored"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_ignoreduserid", NpgsqlDbType.Integer)).Value = ignoredUserId;
                cmd.Parameters.Add("result", NpgsqlDbType.Boolean);
                cmd.Parameters["result"].Direction = ParameterDirection.ReturnValue;

                return Convert.ToBoolean(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        public static DataTable user_ignoredlist([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_ignoredlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_User

        /// <summary>
        /// To return a rather rarely updated active user data
        /// </summary>
        /// <param name="userID">The UserID. It is always should have a positive > 0 value.</param>
        /// <param name="styledNicks">If styles should be returned.</param>
        /// <returns>A DataRow, it should never return a null value.</returns>
        public static DataRow user_lazydata(
            [NotNull] string connectionString,
            object userID,
            object boardID,
            bool showPendingMails,
            bool showPendingBuddies,
            bool showUnreadPMs,
            bool showUserAlbums,
            bool styledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_lazydata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_showpendingmails", NpgsqlDbType.Boolean)).Value =
                    showPendingMails;
                cmd.Parameters.Add(new NpgsqlParameter("i_showpendingbuddies", NpgsqlDbType.Boolean)).Value =
                    showPendingBuddies;
                cmd.Parameters.Add(new NpgsqlParameter("i_showunreadpms", NpgsqlDbType.Boolean)).Value = showUnreadPMs;
                cmd.Parameters.Add(new NpgsqlParameter("i_showuseralbums", NpgsqlDbType.Boolean)).Value = showUserAlbums;
                cmd.Parameters.Add(new NpgsqlParameter("i_showuserstyle", NpgsqlDbType.Boolean)).Value = styledNicks;
                return PostgreDbAccess.GetData(cmd, connectionString).Rows[0];
            }
        }

        /// <summary>
        /// The user_list.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return style info.
        /// </param> 
        /// <returns>
        /// </returns>
        public static DataTable user_list(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId
                                                                                                   ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved
                                                                                                    ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID
                                                                                                   ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks
                                                                                                       ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The user_pagedlist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable user_pagedlist(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks,
            object pageIndex,
            object pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_pagedlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId
                                                                                                   ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved
                                                                                                    ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID
                                                                                                   ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize; 
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Get the user list as a typed list.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="groupID">
        /// The group id.
        /// </param>
        /// <param name="rankID">
        /// The rank id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        public static IEnumerable<TypedUserList> UserList(
            [NotNull] string connectionString,
            int boardId,
            int? userId,
            bool? approved,
            int? groupID,
            int? rankID,
            bool? useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_list"))
            {
                // if (userId == null) { userId = DBNull.Value; }
                // if (approved == null) { approved = DBNull.Value; }
                // if (groupID == null) { groupID = DBNull.Value; }
                // if (rankID == null) { rankID = DBNull.Value; }
                //  if (useStyledNicks == null) { useStyledNicks = DBNull.Value; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(x => new TypedUserList(x));
            }
        }

        /// <summary>
        /// The user_ list with todays birthdays.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return style info.
        /// </param>
        /// <returns>
        /// The user_ list with todays birthdays.
        /// </returns>
        public static DataTable User_ListTodaysBirthdays(
            [NotNull] string connectionString, [NotNull] int boardID, [CanBeNull] object useStyledNicks)
        {
            // Profile columns cannot yet exist when we first are gettinng data.
            try
            {
                var sqlBuilder =
                    new StringBuilder(
                        "SELECT up.Birthday, up.UserID, u.TimeZone, u.name as UserName,u.DisplayName AS UserDisplayName, (case(:i_stylednicks) when TRUE then  u.userstyle ");
                sqlBuilder.Append(" else '' end) AS Style ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append(PostgreDbAccess.GetObjectName("userprofile"));
                sqlBuilder.Append(" up JOIN ");
                sqlBuilder.Append(PostgreDbAccess.GetObjectName("user"));
                sqlBuilder.Append(" u ON u.userid = up.userid ");
                sqlBuilder.Append(
                    " where u.boardid = :i_boardid AND (up.birthday + (:i_currentyear - extract(year  from up.birthday))*interval '1 year' between (:i_currentutc - interval '24 hours') and (:i_currentutc + interval '24 hours'));");
                using (var cmd = PostgreDbAccess.GetCommand(sqlBuilder.ToString(), true))
                {
                    cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                    cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardID;
                    cmd.Parameters.Add("i_currentyear", NpgsqlDbType.Integer).Value = DateTime.UtcNow.Year;
                    cmd.Parameters.Add("i_currentutc", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;
                    return PostgreDbAccess.GetData(cmd, connectionString);
                }
            }
            catch (Exception e)
            {
                Db.eventlog_create(null, e.Source, e.Message, EventLogTypes.Error);
            }

            return null;
        }

        /// <summary>
        /// The user_ list with todays birthdays.
        /// </summary>
        /// <param name="userIdsList">
        /// The Int array of userIds.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return or not style info.
        /// </param>
        /// <returns>
        /// The user_ list profiles.
        /// </returns>
        public static DataTable User_ListProfilesByIdsList(
            [NotNull] string connectionString,
            [NotNull] int boardID,
            [NotNull] int[] userIdsList,
            [CanBeNull] object useStyledNicks)
        {
            string stIds = userIdsList.Aggregate(string.Empty, (current, userId) => current + (',' + userId)).Trim(',');
            // Profile columns cannot yet exist when we first are gettinng data.
            try
            {
                var sqlBuilder = new StringBuilder(" ");
                sqlBuilder.Append(
                    "SELECT up.*, u.Name as UserName,u.DisplayName as UserDisplayName,(case(:i_stylednicks) when 1 then  u.UserStyle ");
                sqlBuilder.Append(" else '' end) AS Style");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append(PostgreDbAccess.GetObjectName("UserProfile"));
                sqlBuilder.Append(" up JOIN ");
                sqlBuilder.Append(PostgreDbAccess.GetObjectName("User"));
                sqlBuilder.Append(" u ON u.UserID = up.UserID ");
                sqlBuilder.AppendFormat(" where u.boardid = :i_boardid AND UserID IN ({0})  ", stIds);
                using (var cmd = PostgreDbAccess.GetCommand(sqlBuilder.ToString(), true))
                {
                    cmd.Parameters.Add("i_stylednicks", NpgsqlDbType.Boolean).Value = useStyledNicks;
                    cmd.Parameters.Add("i_boardid", NpgsqlDbType.Integer).Value = boardID;
                    return PostgreDbAccess.GetData(cmd, connectionString);
                }
            }
            catch (Exception e)
            {
                Db.eventlog_create(null, e.Source, e.Message, EventLogTypes.Error);
            }

            return null;
        }

        #region ProfileMirror

        /// <summary>
        /// The set property values.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="appname">
        /// The appname.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="dirtyOnly">
        /// The dirty only.
        /// </param>
        public static void SetPropertyValues(
            [NotNull] string connectionString,
            int boardId,
            string appname,
            int userId, 
            string userName,
            SettingsPropertyValueCollection collection,
            bool dirtyOnly = true)
        {
            // guest should not be in profile
            int? userIdG = Db.user_guest(connectionString, boardId);
            if (userId <=0 || userIdG == userId || collection.Count < 1)
            {
                return;
            }

            bool itemsToSave = true;
            if (dirtyOnly)
            {
                itemsToSave = collection.Cast<SettingsPropertyValue>().Any(pp => pp.IsDirty);
            }

            // First make sure we have at least one item to save
            if (!itemsToSave)
            {
                return;
            }

            // load the data for the configuration or add new columns
            List<SettingsPropertyColumn> spc = LoadFromPropertyValueCollection(connectionString, collection);

            if (spc != null && spc.Count > 0 && userName.IsSet())
            {
                // start saving..
                Db.SetProfileProperties(connectionString, boardId, appname, userId, userName, collection, spc, dirtyOnly);
            }
        }

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        public static void SetProfileProperties(
            [NotNull] string connectionString,
            [NotNull] int boardId,
            [NotNull] object appName,
            [NotNull] int userID,
            [NotNull] string userName,
            [NotNull] SettingsPropertyValueCollection values,
            [NotNull] List<SettingsPropertyColumn> settingsColumnsList,
            bool dirtyOnly)
        {
            if (userName.IsNotSet())
            {
                return;
            }

            using (var conn = new PostgreDbConnectionManager(connectionString).OpenDBConnection(connectionString))
            {
                var cmd = new NpgsqlCommand { Connection = conn };

                string table = PostgreDbAccess.GetObjectName("UserProfile");
                StringBuilder sqlCommand = new StringBuilder("SELECT 1 FROM ").Append(table);
                sqlCommand.Append(" WHERE UserID = :UserID AND ApplicationName = :ApplicationName");
                cmd.Parameters.Add("UserID", NpgsqlDbType.Integer).Value = userID;
                cmd.Parameters.Add("ApplicationName", NpgsqlDbType.Varchar).Value = appName;
                cmd.CommandText = sqlCommand.ToString();
                cmd.CommandType = CommandType.Text;

                object o = cmd.ExecuteScalar();
                int dd;

                // Build up strings used in the query
                var columnStr = new StringBuilder();
                var valueStr = new StringBuilder();
                var setStr = new StringBuilder();
                int count = 0;

                foreach (SettingsPropertyColumn column in settingsColumnsList)
                {
                    // only write if it's dirty
                    if (!dirtyOnly || values[column.Settings.Name].IsDirty)
                    {
                        columnStr.Append(", ");
                        valueStr.Append(", ");
                        columnStr.Append(column.Settings.Name);
                        string valueParam = ":Value" + count;
                        valueStr.Append(valueParam);
                        cmd.Parameters.Add(valueParam, column.DataType).Value =
                            values[column.Settings.Name].PropertyValue;
                        if ((column.DataType != NpgsqlDbType.Timestamp) || column.Settings.Name != "LastUpdatedDate"
                            || column.Settings.Name != "LastActivity")
                        {
                            if (count > 0)
                            {
                                setStr.Append(",");
                            }

                            setStr.Append(column.Settings.Name);
                            setStr.Append("=");
                            setStr.Append(valueParam);
                        }

                        count++;
                    }
                }

                columnStr.Append(",LastUpdatedDate ");
                valueStr.Append(",:LastUpdatedDate");
                setStr.Append(",LastUpdatedDate=:LastUpdatedDate");
                cmd.Parameters.Add("LastUpdatedDate", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;

                // MembershipUser mu = System.Web.Security.Membership.GetUser(userID);
                columnStr.Append(",LastActivity ");
                valueStr.Append(",:LastActivity");
                setStr.Append(",LastActivity=:LastActivity");
                cmd.Parameters.Add("LastActivity", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;

                columnStr.Append(",ApplicationName ");
                valueStr.Append(",:ApplicationName");
                setStr.Append(",ApplicationName=:ApplicationName");

                columnStr.Append(",IsAnonymous ");
                valueStr.Append(",:IsAnonymous");
                setStr.Append(",IsAnonymous=:IsAnonymous");
                cmd.Parameters.AddWithValue("IsAnonymous", 0);
                cmd.Parameters.Add("IsAnonymous", NpgsqlDbType.Boolean).Value = false;

                columnStr.Append(",UserName ");
                valueStr.Append(",:UserName");
                setStr.Append(",UserName=:UserName");
                cmd.Parameters.Add("UserName", NpgsqlDbType.Varchar).Value = userName;

                // the user  exists. 
                sqlCommand.Clear();
                if (o != null && int.TryParse(o.ToString(), out dd))
                {
                    sqlCommand.Append("UPDATE ").Append(table).Append(" SET ").Append(setStr.ToString());
                    sqlCommand.Append(" WHERE UserID = ").Append(userID.ToString()).Append(string.Empty);
                }
                else
                {
                    sqlCommand.Append("INSERT INTO ").Append(table).Append(" (UserID").Append(columnStr.ToString());
                    sqlCommand.Append(") VALUES (")
                              .Append(userID.ToString())
                              .Append(string.Empty)
                              .Append(valueStr.ToString())
                              .Append(")");
                }


                cmd.CommandText = sqlCommand.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <returns>
        /// </returns>
        public static DataTable GetProfileStructure([NotNull] string connectionString )
        {
            string sql = @"SELECT * FROM {0} LIMIT 1".FormatWith(PostgreDbAccess.GetObjectName("UserProfile"));

            using (var cmd = PostgreDbAccess.GetCommand(sql, true))
            {
                cmd.CommandType = CommandType.Text;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="columnType">
        /// The column type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        public static void AddProfileColumn(
            [NotNull] string connectionString, [NotNull] string name, NpgsqlDbType columnType, int size)
        {
            // get column type..
            string type = columnType.ToString();

            if (size > 0)
            {
                type += "(" + size + ")";
            }

            string sql = "ALTER TABLE {0} ADD {1} {2}".FormatWith(
                PostgreDbAccess.GetObjectName("userprofile"), name, type);

            using (var cmd = PostgreDbAccess.GetCommand(sql, true))
            {
                cmd.CommandType = CommandType.Text;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="providerData">
        /// The provider data.
        /// </param>
        /// <param name="dbType">
        /// The db type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The get db type and size from string.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static bool GetDbTypeAndSizeFromString(string providerData, out NpgsqlDbType dbType, out int size)
        {
            size = -1;
            dbType = NpgsqlDbType.Varchar;

            if (string.IsNullOrEmpty(providerData))
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new char[] { ';' });

            // first item is the column name..
            string columnName = chunk[0];

            // vzrus addon convert values from mssql types..
            if (chunk[1].IndexOf("varchar", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Varchar";
            }

            if (chunk[1].IndexOf("int", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Integer";
            }

            if (chunk[1].IndexOf("DateTime", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Timestamp";
            }


            // get the datatype and ignore case..
            dbType = (NpgsqlDbType)Enum.Parse(typeof(NpgsqlDbType), chunk[1], true);

            if (chunk.Length <= 2)
            {
                return true;
            }

            // handle size..
            if (!int.TryParse(chunk[2], out size))
            {
                throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
            }

            return true;
        }

        /// <summary>
        /// The load from property value collection.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private static List<SettingsPropertyColumn> LoadFromPropertyValueCollection(
            [NotNull] string connectionString, SettingsPropertyValueCollection collection)
        {
            var settingsColumnsList = new List<SettingsPropertyColumn>();


            // validiate all the properties and populate the internal settings collection
            foreach (SettingsPropertyValue value in collection)
            {
                var tempProperty = value.Property.Attributes["CustomProviderData"];

                if (tempProperty == null)
                {
                    continue;
                }

                NpgsqlDbType dbType;
                int size;

                // parse custom provider data..
                GetDbTypeAndSizeFromString(tempProperty.ToString(), out dbType, out size);


                // default the size to 256 if no size is specified
                if (dbType == NpgsqlDbType.Varchar && size == -1)
                {
                    size = 256;
                }

                settingsColumnsList.Add(new SettingsPropertyColumn(value.Property, dbType, size));
            }

            // sync profile table structure with the db..
            DataTable structure = Db.GetProfileStructure(connectionString);

            // verify all the columns are there..
            foreach (SettingsPropertyColumn column in settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it..
                    Db.AddProfileColumn(connectionString, column.Settings.Name, column.DataType, column.Size);
                }
            }

            return settingsColumnsList;
        }

        #endregion

        /// <summary>
        /// The admin_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable admin_list([NotNull] string connectionString, int? boardId, object useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("admin_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The admin_pageaccesslist.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable admin_pageaccesslist(
            [NotNull] string connectionString, [CanBeNull] object boardId, [NotNull] object useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("admin_pageaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void adminpageaccess_save(
            [NotNull] string connectionString, [NotNull] object userId, [NotNull] object pageName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("adminpageaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagename", NpgsqlDbType.Varchar)).Value = pageName;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void adminpageaccess_delete(
            [NotNull] string connectionString, [NotNull] object userId, [CanBeNull] object pageName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("adminpageaccess_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagename", NpgsqlDbType.Varchar)).Value = pageName;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable adminpageaccess_list(
            [NotNull] string connectionString, [CanBeNull] object userId, [CanBeNull] object pageName)
        {
            using (var cmd = PostgreDbAccess.GetCommand("adminpageaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagename", NpgsqlDbType.Varchar)).Value = pageName;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The user_list20members.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="approved">
        /// The approved.
        /// </param>
        /// <param name="groupId">
        /// The group id.
        /// </param>
        /// <param name="rankId">
        /// The rank id.
        /// </param>
        /// <param name="useStyledNicks">
        /// Return style info.
        /// </param>
        /// <param name="lastUserId">
        /// The last user Id.
        /// </param>
        /// <param name="literals">
        /// The literals.
        /// </param>
        /// <param name="exclude">
        /// The exclude.
        /// </param>
        /// <param name="beginsWith">
        /// The begins with.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable user_listmembers(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            object approved,
            object groupId,
            object rankId,
            object useStyledNicks,
            object lastUserId,
            object literals,
            object exclude,
            object beginsWith,
            object pageIndex,
            object pageSize,
            object sortName,
            object sortRank,
            object sortJoined,
            object sortPosts,
            object sortLastVisit,
            object numPosts,
            object numPostCompare)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_listmembers"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupId;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankId;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new NpgsqlParameter("i_literals", NpgsqlDbType.Varchar)).Value = literals.ToString()
                                                                                                    != "\0"
                                                                                                    && literals.ToString
                                                                                                           ().IsSet()
                                                                                                        ? literals
                                                                                                        : string.Empty;
                cmd.Parameters.Add(new NpgsqlParameter("i_exclude", NpgsqlDbType.Boolean)).Value = exclude;
                cmd.Parameters.Add(new NpgsqlParameter("i_beginswith", NpgsqlDbType.Boolean)).Value = beginsWith;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new NpgsqlParameter("i_sortname", NpgsqlDbType.Integer)).Value =
                    sortName.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_sortrank", NpgsqlDbType.Integer)).Value =
                    sortRank.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_sortjoined", NpgsqlDbType.Integer)).Value =
                    sortJoined.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_sortposts", NpgsqlDbType.Integer)).Value =
                    sortPosts.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_sortlastvisit", NpgsqlDbType.Integer)).Value =
                    sortLastVisit.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_numposts", NpgsqlDbType.Integer)).Value =
                    numPosts.ToType<int>();
                cmd.Parameters.Add(new NpgsqlParameter("i_numpostcopmate", NpgsqlDbType.Integer)).Value =
                    numPostCompare.ToType<int>();

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// For URL Rewriting
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable user_simplelist([NotNull] string connectionString, int StartID, int Limit)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_simplelist"))
            {
                if (StartID <= 0)
                {
                    StartID = 0;
                }
                if (Limit <= 0)
                {
                    Limit = 500;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_startid", NpgsqlDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new NpgsqlParameter("i_limit", NpgsqlDbType.Integer)).Value = Limit;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void user_delete([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setrole([NotNull] string connectionString, int boardId, object providerUserKey, object role)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_setrole"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value =
                    providerUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_role", NpgsqlDbType.Varchar)).Value = role;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // TODO: is not used anywhere? 
        public static void user_setinfo([NotNull] string connectionString, int boardId, System.Web.Security.MembershipUser user)
        {
            using (
                var cmd =
                    PostgreDbAccess.GetCommand(
                        "update {databaseOwner}.{objectQualifier}User set Name=i_UserName,Email=i_Email where BoardID=i_BoardID and ProviderUserKey=i_ProviderUserKey",
                        true))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new NpgsqlParameter("i_UserName", NpgsqlDbType.Varchar)).Value = user.UserName;
                cmd.Parameters.Add(new NpgsqlParameter("i_Email", NpgsqlDbType.Varchar)).Value = user.Email;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_ProviderUserKey", NpgsqlDbType.Varchar)).Value =
                    user.ProviderUserKey;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The user_setnotdirty.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userId">
        /// The userId key.
        /// </param>
        public static void user_setnotdirty([NotNull] string connectionString, int boardId, int userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_setnotdirty"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_migrate(
            [NotNull] string connectionString, object userId, object providerUserKey, object updateProvider)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_migrate"))
            {
                if (providerUserKey == null)
                {
                    providerUserKey = DBNull.Value;
                }
                if (updateProvider == null)
                {
                    updateProvider = DBNull.Value;
                }
                //if (date == null) { date = DBNull.Value; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value =
                    providerUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_updateprovider", NpgsqlDbType.Boolean)).Value = updateProvider;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_deleteold([NotNull] string connectionString, object boardId, object days)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_deleteold"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_days", NpgsqlDbType.Integer)).Value = days;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_approve([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_approve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_approveall([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_approveall"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_suspend([NotNull] string connectionString, object userId, object suspend)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_suspend"))
            {
                if (suspend == null)
                {
                    suspend = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_suspend", NpgsqlDbType.Timestamp)).Value = suspend;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns data about allowed signature tags and character limits
        /// </summary>
        /// <param name="userID">The userID</param>
        /// <param name="boardID">The boardID</param>
        /// <returns>Data Table</returns>
        public static DataTable user_getsignaturedata([NotNull] string connectionString, object userID, object boardID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_getsignaturedata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns data about albums: allowed number of images and albums
        /// </summary>
        /// <param name="userID">The userID</param>
        /// <param name="boardID">The boardID</param>  
        public static DataTable user_getalbumsdata([NotNull] string connectionString, object userID, object boardID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_getalbumsdata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                return dt;
            }
        }

        public static bool user_changepassword(
            [NotNull] string connectionString, object userId, object oldPassword, object newPassword)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_changepassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_oldpassword", NpgsqlDbType.Varchar)).Value = oldPassword;
                cmd.Parameters.Add(new NpgsqlParameter("i_newpassword", NpgsqlDbType.Varchar)).Value = newPassword;

                return (bool)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable user_pmcount([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_pmcount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Checks if the User has replied tho the specifc topic.
        /// </summary>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// Returns if true or not
        /// </returns>
        public static bool user_RepliedTopic(
            [NotNull] string connectionString, [NotNull] object messageId, [NotNull] object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_repliedtopic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_messageid", NpgsqlDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                var messageCount = (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return messageCount > 0;
            }
        }

        public static void user_save(
            [NotNull] string connectionString,
            object userId,
            object boardId,
            object userName,
            object displayName,
            object email,
            object timeZone,
            object languageFile,
            object culture,
            object themeFile,
            object useSingleSignOn,
            object textEditor,
            object overrideDefaultThemes,
            object approved,
            object pmNotification,
            object autoWatchTopics,
            object dSTUser,
            object isHidden,
            object notificationType, 
            object topicsPerPage, 
            object postsPerPage)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_save"))
            {
                if (email == null)
                {
                    email = DBNull.Value;
                }
                if (languageFile == null)
                {
                    languageFile = DBNull.Value;
                }
                if (themeFile == null)
                {
                    themeFile = DBNull.Value;
                }
                if (overrideDefaultThemes == null)
                {
                    overrideDefaultThemes = DBNull.Value;
                }
                if (approved == null)
                {
                    approved = DBNull.Value;
                }
                if (pmNotification == null)
                {
                    pmNotification = DBNull.Value;
                }
                if (culture == null)
                {
                    culture = DBNull.Value;
                }
                if (dSTUser == null)
                {
                    dSTUser = DBNull.Value;
                }
                if (isHidden == null)
                {
                    isHidden = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName ?? DBNull.Value; 
                cmd.Parameters.Add(new NpgsqlParameter("i_displayname", NpgsqlDbType.Varchar)).Value = displayName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_timezone", NpgsqlDbType.Integer)).Value = timeZone;
                cmd.Parameters.Add(new NpgsqlParameter("i_languagefile", NpgsqlDbType.Varchar)).Value = languageFile;
                cmd.Parameters.Add(new NpgsqlParameter("i_culture", NpgsqlDbType.Varchar)).Value = culture;
                cmd.Parameters.Add(new NpgsqlParameter("i_themefile", NpgsqlDbType.Varchar)).Value = themeFile;
                cmd.Parameters.Add(new NpgsqlParameter("i_usesinglesignon", NpgsqlDbType.Boolean)).Value =
                    useSingleSignOn;
                cmd.Parameters.Add(new NpgsqlParameter("i_texteditor", NpgsqlDbType.Varchar)).Value = textEditor;
                cmd.Parameters.Add(new NpgsqlParameter("i_overridedefaulttheme", NpgsqlDbType.Boolean)).Value =
                    overrideDefaultThemes;
                cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new NpgsqlParameter("i_pmnotification", NpgsqlDbType.Boolean)).Value = pmNotification;
                cmd.Parameters.Add(new NpgsqlParameter("i_notificationtype", NpgsqlDbType.Integer)).Value =
                    notificationType;
                cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value = DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_autowatchtopics", NpgsqlDbType.Boolean)).Value =
                    autoWatchTopics;
                cmd.Parameters.Add(new NpgsqlParameter("i_dstuser", NpgsqlDbType.Boolean)).Value = dSTUser;
                cmd.Parameters.Add(new NpgsqlParameter("i_hideuser", NpgsqlDbType.Boolean)).Value = isHidden;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicsperpage", NpgsqlDbType.Integer)).Value = topicsPerPage;
                cmd.Parameters.Add(new NpgsqlParameter("i_postsperpage", NpgsqlDbType.Integer)).Value = postsPerPage;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value = DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);

            }
        }

        /// <summary>
        /// Saves the notification type for a user
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="notificationType">
        /// The notification type.
        /// </param>
        public static void user_savenotification(
            [NotNull] string connectionString,
            object userId,
            object pmNotification,
            object autoWatchTopics,
            object notificationType,
            object dailyDigest)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_savenotification"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pmnotification", NpgsqlDbType.Boolean)).Value = pmNotification;
                cmd.Parameters.Add(new NpgsqlParameter("i_autowatchtopics", NpgsqlDbType.Boolean)).Value =
                    autoWatchTopics;
                cmd.Parameters.Add(new NpgsqlParameter("i_notificationtype", NpgsqlDbType.Integer)).Value =
                    notificationType;
                cmd.Parameters.Add(new NpgsqlParameter("i_dailydigest", NpgsqlDbType.Boolean)).Value = dailyDigest;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_adminsave(
            [NotNull] string connectionString,
            object boardId,
            object userId,
            object name,
            object displayName,
            object email,
            object flags,
            object rankID)
        {

            using (var cmd = PostgreDbAccess.GetCommand("user_adminsave"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_displayname", NpgsqlDbType.Varchar)).Value = displayName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_flags", NpgsqlDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new NpgsqlParameter("i_rankid", NpgsqlDbType.Integer)).Value = rankID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable user_emails([NotNull] string connectionString, object boardId, object groupID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_emails"))
            {

                if (groupID == null)
                {
                    groupID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_accessmasks([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_accessmasks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return userforumaccess_sort_list(PostgreDbAccess.GetData(cmd, connectionString), 0, 0, 0);
            }
        }

        public static DataTable user_accessmasksbyforum([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_accessmasksbyforum"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_accessmasksbygroup([NotNull] string connectionString, object boardId, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_accessmasksbygroup"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        //adds some convenience while editing group's access rights (indent forums)
        private static DataTable userforumaccess_sort_list(
            DataTable listSource, int parentID, int categoryID, int startingIndent)
        {

            DataTable listDestination = new DataTable();

            listDestination.Columns.Add("ForumID", typeof(String));
            listDestination.Columns.Add("ForumName", typeof(String));
            //it is uset in two different procedures with different tables, 
            //so, we must add correct columns
            if (listSource.Columns.IndexOf("AccessMaskName") >= 0) listDestination.Columns.Add("AccessMaskName", typeof(String));
            else
            {
                listDestination.Columns.Add("BoardName", typeof(String));
                listDestination.Columns.Add("CategoryName", typeof(String));
                listDestination.Columns.Add("AccessMaskId", typeof(Int32));
            }
            DataView dv = listSource.DefaultView;
            userforumaccess_sort_list_recursive(dv.ToTable(), listDestination, parentID, categoryID, startingIndent);
            return listDestination;
        }

        private static void userforumaccess_sort_list_recursive(
            DataTable listSource, DataTable listDestination, int parentID, int categoryID, int currentIndent)
        {
            DataRow newRow;

            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentID)
                {
                    string sIndent = string.Empty;

                    for (int j = 0; j < currentIndent; j++) sIndent += "--";

                    // import the row into the destination
                    newRow = listDestination.NewRow();

                    newRow["ForumID"] = row["ForumID"];
                    newRow["ForumName"] = string.Format("{0} {1}", sIndent, row["ForumName"]);
                    if (listDestination.Columns.IndexOf("AccessMaskName") >= 0) newRow["AccessMaskName"] = row["AccessMaskName"];
                    else
                    {
                        newRow["BoardName"] = row["BoardName"];
                        newRow["CategoryName"] = row["CategoryName"];
                        newRow["AccessMaskId"] = row["AccessMaskId"];
                    }


                    listDestination.Rows.Add(newRow);

                    // recurse through the list..
                    userforumaccess_sort_list_recursive(
                        listSource, listDestination, (int)row["ForumID"], categoryID, currentIndent + 1);
                }
            }
        }

        public static object user_recoverpassword(
            [NotNull] string connectionString, object boardId, object userName, object email)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_recoverpassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static void user_savepassword([NotNull] string connectionString, object userId, object password)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_savepassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_password", NpgsqlDbType.Varchar)).Value =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToString(), "md5");

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static object user_login([NotNull] string connectionString, object boardId, object name, object password)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_login"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = name;
                cmd.Parameters.Add(new NpgsqlParameter("i_password", NpgsqlDbType.Varchar)).Value = password;

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable user_avatarimage([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_avatarimage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static int user_get([NotNull] string connectionString, int boardId, object providerUserKey)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_get"))
            {
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value =
                    providerUserKey;

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToType<int>();
                //return PostgreDbAccess.GetData(cmd,connectionString);
            }
        }

        /// <summary>
        /// The UserFind.
        /// </summary>
        /// <param name="boardID">
        ///   The board id.
        /// </param>
        /// <param name="filter">
        ///   The filter.
        /// </param>
        /// <param name="userName">
        ///   The user name.
        /// </param>
        /// <param name="email">
        ///   The email.
        /// </param>
        /// <param name="displayName"></param>
        /// <param name="notificationType"></param>
        /// <param name="dailyDigest"></param>
        /// <returns>
        /// </returns>
        public static DataTable UserFind(
            [NotNull] string connectionString,
            int boardId,
            bool filter,
            string userName,
            string email,
            string displayName,
            object notificationType,
            object dailyDigest)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_find"))
            {
                // if (userName == null) { userName = DBNull.Value; }
                // if (email == null) { email = DBNull.Value; }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_filter", NpgsqlDbType.Boolean)).Value = filter;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_displayname", NpgsqlDbType.Varchar)).Value = displayName;
                cmd.Parameters.Add(new NpgsqlParameter("i_notificationtype", NpgsqlDbType.Integer)).Value =
                    notificationType;
                cmd.Parameters.Add(new NpgsqlParameter("i_dailydigest", NpgsqlDbType.Boolean)).Value = dailyDigest;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static string user_getsignature([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_getsignature"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        public static void user_savesignature([NotNull] string connectionString, object userId, object signature)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_savesignature"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_signature", NpgsqlDbType.Text)).Value = signature;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_saveavatar(
            [NotNull] string connectionString, object userId, object avatar, System.IO.Stream stream, object avatarImageType)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_saveavatar"))
            {
                byte[] data = null;

                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_avatar", NpgsqlDbType.Varchar)).Value = avatar ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_avatarimage", NpgsqlDbType.Bytea)).Value = data;
                cmd.Parameters.Add(new NpgsqlParameter("i_avatarimagetype", NpgsqlDbType.Varchar)).Value =
                    avatarImageType ?? DBNull.Value;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_deleteavatar([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_deleteavatar"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static bool user_register(
            [NotNull] string connectionString,
            object boardId,
            object userName,
            object password,
            object hash,
            object email,
            object location,
            object homePage,
            object timeZone,
            bool approved)
        {

            using (var connMan = new PostgreDbConnectionManager(connectionString))
            {
                using (NpgsqlTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(PostgreDbAccess.IsolationLevel))
                {
                    try
                    {
                        using (NpgsqlCommand cmd = PostgreDbAccess.GetCommand("user_save", connMan.OpenDBConnection(connectionString)))
                        {
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value =
                                DBNull.Value;
                            cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                            cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                            cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                            cmd.Parameters.Add(new NpgsqlParameter("i_timezone", NpgsqlDbType.Integer)).Value = timeZone;
                            cmd.Parameters.Add(new NpgsqlParameter("i_languagefile", NpgsqlDbType.Varchar)).Value =
                                DBNull.Value;
                            cmd.Parameters.Add(new NpgsqlParameter("i_themefile", NpgsqlDbType.Varchar)).Value =
                                DBNull.Value;
                            cmd.Parameters.Add(new NpgsqlParameter("i_overridedefaulttheme", NpgsqlDbType.Boolean))
                               .Value = false;
                            cmd.Parameters.Add(new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean)).Value = approved;
                            cmd.Parameters.Add(new NpgsqlParameter("i_pmnotification", NpgsqlDbType.Boolean)).Value =
                                false;
                            cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value =
                                DBNull.Value;
                            cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                                DateTime.UtcNow;

                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception x)
                    {
                        trans.Rollback();
                        eventlog_create(null, "user_register in VZF.Classes.Data.Db.cs", x, EventLogTypes.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        public static int user_aspnet(
            [NotNull] string connectionString,
            int boardId,
            string userName,
            string displayName,
            string email,
            object providerUserKey,
            object isApproved)
        {
            try
            {
                using (var cmd = PostgreDbAccess.GetCommand("user_aspnet"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                    cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                    cmd.Parameters.Add(new NpgsqlParameter("i_displayname", NpgsqlDbType.Varchar)).Value = displayName;
                    // ?? userName;                    
                    cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                    cmd.Parameters.Add(new NpgsqlParameter("i_provideruserkey", NpgsqlDbType.Varchar)).Value =
                        providerUserKey;
                    cmd.Parameters.Add(new NpgsqlParameter("i_isapproved", NpgsqlDbType.Boolean)).Value = isApproved;
                    cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                        DateTime.UtcNow;

                    return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
                }
            }
            catch (Exception x)
            {
                Db.eventlog_create(null, "user_aspnet in VZF.Classes.Data.Db.cs", x, EventLogTypes.Error);
                return 0;
            }
        }

        /// <summary>
        /// The user_guest.
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <returns>
        /// The user_guest.
        /// </returns>
        public static int? user_guest([NotNull] string connectionString, object boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_guest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;

                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        public static DataTable user_activity_rank(
            [NotNull] string connectionString, object boardId, object startDate, object displayNumber)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_activity_rank"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_displaynumber", NpgsqlDbType.Integer)).Value = displayNumber;
                cmd.Parameters.Add(new NpgsqlParameter("i_startdate", NpgsqlDbType.Timestamp)).Value = startDate;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static int user_nntp(
            [NotNull] string connectionString, object boardId, object userName, object email, int? timeZone)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_nntp"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_email", NpgsqlDbType.Varchar)).Value = email;
                cmd.Parameters.Add(new NpgsqlParameter("i_timezone", NpgsqlDbType.Integer)).Value = timeZone;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                object o = PostgreDbAccess.ExecuteScalar(cmd, connectionString);
                //  if ( o != DBNull.Value)
                //  {
                return Convert.ToInt32(o);
                //  }
                //  else
                //     return -1;

            }
        }

        /// <summary>
        /// Add Reputation Points to the specified user id.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="fromUserID">From user ID.</param>
        /// <param name="points">The points.</param>
        public static void user_addpoints(
            [NotNull] string connectionString, [NotNull] object userID, [CanBeNull] object fromUserID, [NotNull] object points)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_addpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add("i_fromuserid", NpgsqlDbType.Integer).Value = fromUserID;
                cmd.Parameters.Add("i_utctimestamp", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new NpgsqlParameter("i_points", NpgsqlDbType.Integer)).Value = points;


                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Remove Repuatation Points from the specified user id.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="fromUserID">From user ID.</param>
        /// <param name="points">The points.</param>
        public static void user_removepoints(
            [NotNull] string connectionString, [NotNull] object userID, [CanBeNull] object fromUserID, [NotNull] object points)
        {

            using (var cmd = PostgreDbAccess.GetCommand("user_removepoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add("i_fromuserid", NpgsqlDbType.Integer).Value = fromUserID;
                cmd.Parameters.Add("i_utctimestamp", NpgsqlDbType.Timestamp).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new NpgsqlParameter("i_points", NpgsqlDbType.Integer)).Value = points;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setpoints([NotNull] string connectionString, object userId, object points)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_setpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_points", NpgsqlDbType.Integer)).Value = points;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static int user_getpoints([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_getpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return (int)PostgreDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }


        public static int user_getthanks_from([NotNull] string connectionString, object userID, object pageUserId)
        {

            using (var cmd = PostgreDbAccess.GetCommand("user_getthanks_from"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                return Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        //<summary> Returns the number of times and posts that other users have thanked the 
        // user with the provided userID.
        public static int[] user_getthanks_to([NotNull] string connectionString, object userID, object pageUserId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_getthanks_to"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                NpgsqlParameter paramThanksToNumber = new NpgsqlParameter("i_thankstonumber", NpgsqlDbType.Integer);
                paramThanksToNumber.Direction = ParameterDirection.Output;
                NpgsqlParameter paramThanksToPostsNumber = new NpgsqlParameter(
                    "i_thankstopostsnumber", NpgsqlDbType.Integer);
                paramThanksToPostsNumber.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(paramThanksToNumber);
                cmd.Parameters.Add(paramThanksToPostsNumber);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);

                int ThanksToPostsNumber, ThanksToNumber;
                if (paramThanksToNumber.Value == DBNull.Value)
                {
                    ThanksToNumber = 0;
                    ThanksToPostsNumber = 0;
                }
                else
                {
                    ThanksToPostsNumber = Convert.ToInt32(paramThanksToPostsNumber.Value);
                    ThanksToNumber = Convert.ToInt32(paramThanksToNumber.Value);
                }
                return new int[] { ThanksToNumber, ThanksToPostsNumber };
            }
        }



        /// <summary>
        /// Returns the posts which is thanked by the user.
        /// </summary>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable user_viewthanksfrom(
            [NotNull] string connectionString, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_viewthanksfrom"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns the posts which are posted by the user and 
        /// are thanked by other users.
        /// </summary>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable user_viewthanksto(
            [NotNull] string connectionString, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_viewthanksto"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageuserid", NpgsqlDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new NpgsqlParameter("i_pageindex", NpgsqlDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new NpgsqlParameter("i_pagesize", NpgsqlDbType.Integer)).Value = pageSize;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Update the single Sign on Status
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="isFacebookUser">
        /// The is Facebook User
        /// </param>
        /// <param name="isTwitterUser">
        /// The is Twitter User.
        /// </param>
        public static void user_update_single_sign_on_status(
            [NotNull] string connectionString,
            [NotNull] object userID,
            [NotNull] object isFacebookUser,
            [NotNull] object isTwitterUser)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_update_single_sign_on_status"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_userid", NpgsqlDbType.Integer).Value = userID;
                cmd.Parameters.Add("i_isfacebookuser", NpgsqlDbType.Boolean).Value = isFacebookUser;
                cmd.Parameters.Add("i_istwitteruser", NpgsqlDbType.Boolean).Value = isTwitterUser;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        #endregion

        #region yaf_UserForum

        /// <summary>
        /// The userforum_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable userforum_list([NotNull] string connectionString, object userId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("userforum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID ?? DBNull.Value;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The userforum_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        public static void userforum_delete([NotNull] string connectionString, object userId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("userforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void userforum_save([NotNull] string connectionString, object userId, object forumID, object accessMaskID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("userforum_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_accessmaskid", NpgsqlDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_UserGroup

        public static DataTable usergroup_list([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("usergroup_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void usergroup_save([NotNull] string connectionString, object userId, object groupID, object member)
        {
            using (var cmd = PostgreDbAccess.GetCommand("usergroup_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_groupid", NpgsqlDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new NpgsqlParameter("i_member", NpgsqlDbType.Boolean)).Value =
                    Convert.ToBoolean(member);

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_WatchForum

        public static void watchforum_add([NotNull] string connectionString, object userId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchforum_add"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable watchforum_list([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchforum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable watchforum_check([NotNull] string connectionString, object userId, object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchforum_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void watchforum_delete([NotNull] string connectionString, object watchForumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_watchforumid", NpgsqlDbType.Integer)).Value = watchForumID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);

            }
        }

        #endregion

        #region yaf_WatchTopic

        /// <summary>
        /// The watchtopic_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable watchtopic_list([NotNull] string connectionString, object userId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchtopic_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The watchtopic_check.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable watchtopic_check([NotNull] string connectionString, object userId, object topicId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchtopic_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicId;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The watchtopic_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="watchTopicID">
        /// The watch topic id.
        /// </param>
        public static void watchtopic_delete([NotNull] string connectionString, object watchTopicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchtopic_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_watchtopicid", NpgsqlDbType.Integer)).Value = watchTopicID;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The watchtopic_add.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void watchtopic_add([NotNull] string connectionString, object userId, object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("watchtopic_add"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Add Or Update Read Tracking for the Current User and Topic
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        public static void Readtopic_AddOrUpdate(
            [NotNull] string connectionString, [NotNull] object userID, [NotNull] object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readtopic_addorupdate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete the Read Tracking
        /// </summary>
        /// <param name="trackingID">
        /// The tracking id.
        /// </param>
        /* public static void Readtopic_delete([NotNull] object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readtopic_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString);
            }
        } */

        /// <summary>
        /// Get the Global Last Read DateTime User
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="lastVisitDate">
        /// The last Visit Date of the User
        /// </param>
        /// <returns>
        /// Returns the Global Last Read DateTime
        /// </returns>
        public static DateTime? User_LastRead([NotNull] string connectionString, [NotNull] object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("user_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;

                var tableLastRead = PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return tableLastRead.ToType<DateTime?>();
            }
        }

        /// <summary>
        /// Get the Last Read DateTime for the Current Topic and User
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="topicID">
        /// The topic ID.
        /// </param>
        /// <returns>
        /// Returns the Last Read DateTime
        /// </returns>
        public static DateTime? Readtopic_lastread(
            [NotNull] string connectionString, [NotNull] object userID, [NotNull] object topicID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readtopic_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_topicid", NpgsqlDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                var tableLastRead = PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return tableLastRead.ToType<DateTime?>();
            }
        }

        /// <summary>
        /// Add Or Update Read Tracking for the forum and Topic
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        public static void ReadForum_AddOrUpdate(
            [NotNull] string connectionString, [NotNull] object userID, [NotNull] object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readforum_addorupdate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete the Read Tracking
        /// </summary>
        /// <param name="trackingID">
        /// The tracking id.
        /// </param>
        /* public static void ReadForum_delete([NotNull] object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                PostgreDbAccess.ExecuteNonQuery(cmd,connectionString);
            }
        } */

        /// <summary>
        /// Get the Last Read DateTime for the Forum and User
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="forumID">
        /// The forum ID.
        /// </param>
        /// <returns>
        /// Returns the Last Read DateTime
        /// </returns>
        public static DateTime? ReadForum_lastread(
            [NotNull] string connectionString, [NotNull] object userID, [NotNull] object forumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("readforum_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;

                var tableLastRead = PostgreDbAccess.ExecuteScalar(cmd, connectionString);

                return tableLastRead != null && tableLastRead != DBNull.Value
                           ? (DateTime)tableLastRead
                           : DateTimeHelper.SqlDbMinTime();
            }
        }

        #endregion

        #region Miscelaneous vzrus addons

        #region reindex page controls

        // DB Maintenance page buttons name    

        /// <summary>
        /// Gets a value indicating whether panel get stats.
        /// </summary>
        public static bool PanelGetStats
        {
            get
            {
                return true;
            }
        }

        public static bool PanelRecoveryMode
        {
            get
            {
                return true;
            }
        }

        public static bool PanelReindex
        {
            get
            {
                return true;
            }
        }

        public static bool PanelShrink
        {
            get
            {
                return true;
            }
        }


        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }

        #endregion

        /// <summary>
        /// The rsstopic_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="topicStart">
        /// The topic start.
        /// </param>
        /// <param name="topicCount">
        /// The topic count.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable rsstopic_list([NotNull] string connectionString, int forumID, int topicStart, int topicCount)
        {
            using (var cmd = PostgreDbAccess.GetCommand("rsstopic_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumid", NpgsqlDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_start", NpgsqlDbType.Integer)).Value = topicStart;
                cmd.Parameters.Add(new NpgsqlParameter("i_count", NpgsqlDbType.Integer)).Value = topicCount;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The get stats message.
        /// </summary>
        private static string getStatsMessage;

        /// <summary>
        /// The db_getstats_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_getstats_new([NotNull] string connectionString )
        {
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);
                    using (var cmd = new NpgsqlCommand("VACUUM ANALYZE VERBOSE;", connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandType = CommandType.Text;

                        // up the command timeout..
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it..
                        cmd.ExecuteNonQuery();
                        return getStatsMessage;
                    }
                }
            }
            finally
            {
                getStatsMessage = string.Empty;
            }
        }

        /// <summary>
        /// The reindexDb_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void getStats_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            getStatsMessage += "\r\n{0}".FormatWith(e.Message);
        }

        /// <summary>
        /// The reindex db message.
        /// </summary>
        private static string reindexDbMessage;

        /// <summary>
        /// The db_reindex_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_reindex_new([NotNull] string connectionString )
        {
            // VACUUM ANALYZE VERBOSE;VACUUM cannot be implemeneted within function or multiline command line string 
            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(reindexDb_InfoMessage);

                    using (
                        var cmd =
                            new NpgsqlCommand(
                                String.Format(
                                    @"REINDEX DATABASE {0};",
                                    Config.DatabaseSchemaName.IsSet() ? Config.DatabaseSchemaName : "public"),
                                connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandType = CommandType.Text;
                        
                        // up the command timeout..
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        // run it..                   
                        cmd.ExecuteNonQuery();
                        return reindexDbMessage;
                    }
                }
            }
            finally
            {
                reindexDbMessage = string.Empty;
            }
        }

        /// <summary>
        /// The reindexDb_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void reindexDb_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            reindexDbMessage += "\r\n{0}".FormatWith(e.Message);
        }


        public static string db_reindex_warning()
        {
            return "Operation completed. Database tables reindexing can take a lot of time.";
        }

        public static string db_getstats_warning()
        {
            return
                "Operation completed. Vacuum operation blocks all database objects! If there is a lot of indexes, the database can be blocked for a long time. Choose a right timing!";
        }

        /// <summary>
        /// The db_runsql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="connMan">
        /// The conn man.
        /// </param>
        /// <returns>
        /// The db_runsql.
        /// </returns>
        /*  public static string db_runsql(string sql, IDbConnectionManager connMan, bool useTransaction)
        {
            using (var command = new SqlCommand(sql, connMan.OpenDBConnection))
            {
                command.CommandTimeout = 9999;
                command.Connection = connMan.OpenDBConnection;

                return InnerRunSqlExecuteReader(command, useTransaction);
            }
        } */

        /*   public static string db_runsql( string sql, bool useTransaction)
        {
            string txtResult = "";
            var results = new System.Text.StringBuilder();

            using (var cmd = new NpgsqlCommand(sql, connMan.OpenDBConnection))
            {
                cmd.CommandTimeout = 9999;
                NpgsqlDataReader reader = null;

                using (NpgsqlTransaction trans = connMan.OpenDBConnection.BeginTransaction(PostgreDbAccess.IsolationLevel))
                {
                    try
                    {
                        cmd.Connection = connMan.DBConnection;
                        cmd.Transaction = trans;
                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            int rowIndex = 1;

                            results.Append("RowNumber");
                            int gg = 0;
                            var columnNames = new string[reader.GetSchemaTable().Rows.Count-1];
                            foreach (DataRow drd in reader.GetSchemaTable().Rows)
                            {
                                  columnNames[gg] = drd["ColumnName"].ToString();
                                  results.Append(",");
                                  results.Append(drd["ColumnName"].ToString());
                                  gg++;
                                
                            }
                         //   var columnNames = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();

                            
                           

                            results.AppendLine();

                            while (reader.Read())
                            {
                                results.AppendFormat(@"""{0}""", rowIndex++);

                                // dump all columns..
                                foreach (var col in columnNames)
                                {
                                    results.AppendFormat(@",""{0}""", reader[col].ToString().Replace("\"", "\"\""));
                                }

                                results.AppendLine();
                            }
                        }
                        else if (reader.RecordsAffected > 0)
                        {
                            results.AppendFormat("{0} Record(s) Affected", reader.RecordsAffected);
                            results.AppendLine();
                        }
                        else
                        {
                            results.AppendLine("No Results Returned.");
                        }


                        reader.Close();
                        trans.Commit();
                    }
                    catch (Exception x)
          {
            if (reader != null)
            {
              reader.Close();
            }

            // rollback..
            trans.Rollback();
            results.AppendLine();
            results.AppendFormat("SQL ERROR: {0}", x);
          }

          return results.ToString();
        }
               
            }

        } 
       */

        private static string messageRunSql;

        /// <summary>
        /// The db_runsql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="connectionManager">
        /// The conn man.
        /// </param>
        /// <param name="useTransaction">
        /// The use Transaction.
        /// </param>
        /// <returns>
        /// The db_runsql.
        /// </returns>
        public static string db_runsql_new([NotNull] string connectionString, [NotNull] string sql, bool useTransaction)
        {

            try
            {
                using (var connMan = new PostgreDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(runSql_InfoMessage);
                    sql = PostgreDbAccess.GetCommandTextReplaced(sql.Trim());

                    var results = new System.Text.StringBuilder();

                    using (var cmd = new NpgsqlCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandTimeout = 9999;
                        NpgsqlDataReader reader = null;
                        var trans = useTransaction ? cmd.Connection.BeginTransaction(PostgreDbAccess.IsolationLevel)
                                                                   : null;
                        try
                        {
                            cmd.Connection = connMan.DBConnection;
                            cmd.Transaction = trans;
                            reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                int rowIndex = 1;

                                results.Append("RowNumber");
                                int gg = 0;
                                var columnNames = new string[reader.GetSchemaTable().Rows.Count];
                                foreach (DataRow drd in reader.GetSchemaTable().Rows)
                                {
                                    columnNames[gg] = drd["ColumnName"].ToString();
                                    results.Append(",");
                                    results.Append(drd["ColumnName"]);
                                    gg++;
                                }
                                
                                //   var columnNames = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(r => r["ColumnName"].ToString()).ToList();

                                results.AppendLine();

                                while (reader.Read())
                                {
                                    results.AppendFormat(@"""{0}""", rowIndex++);

                                    // dump all columns..
                                    foreach (var col in columnNames)
                                    {
                                        results.AppendFormat(@",""{0}""", reader[col].ToString().Replace("\"", "\"\""));
                                    }

                                    results.AppendLine();
                                }
                            }
                            else if (reader.RecordsAffected > 0)
                            {
                                results.AppendFormat("{0} Record(s) Affected", reader.RecordsAffected);
                                results.AppendLine();
                            }
                            else
                            {
                                if (messageRunSql.IsSet())
                                {
                                    results.AppendLine(messageRunSql);
                                    results.AppendLine();
                                }
                                results.AppendLine("No Results Returned.");
                            }


                            reader.Close();
                            trans.Commit();
                        }
                        catch (Exception x)
                        {
                            if (reader != null)
                            {
                                reader.Close();
                            }

                            // rollback..
                            trans.Rollback();
                            results.AppendLine();
                            results.AppendFormat("SQL ERROR: {0}", x);
                        }

                        return results.ToString();


                    }
                }
            }
            finally
            {
                messageRunSql = string.Empty;
            }


        }

        /// <summary>
        /// The runSql_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void runSql_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            messageRunSql = "\r\n" + e.Message;
        }

       
        public static string forumpage_validateversion([NotNull] string connectionString, int? mid, int appVersion)
        {
            string redirect = string.Empty;

            try
            {
                var registry = Db.registry_list(connectionString, "Version");

                if ((registry.Rows.Count == 0) || (Convert.ToInt32(registry.Rows[0]["Value"]) < appVersion))
                {
                    // needs upgrading..
                    redirect =
                        "install/default.aspx?upgrade={0}&md={1}".FormatWith(
                            registry.Rows.Count != 0 ? Convert.ToInt32(registry.Rows[0]["Value"]) : 0, mid ?? 1);
                }
            }
            catch (Npgsql.NpgsqlException)
            {
                // needs to be setup..
                redirect = "install/";
            }
            return redirect;
        }

        public static readonly string[] _scriptList =
            {
                "pgsql/preinstall.sql", "pgsql/domains.sql", "pgsql/tables.sql",
                "pgsql/sequences.sql", "pgsql/pkeys.sql", "pgsql/indexes.sql",
                "pgsql/fkeys.sql", "pgsql/triggers.sql", "pgsql/rules.sql",
                "pgsql/views.sql", "pgsql/types.sql", "pgsql/procedures.sql",
                "pgsql/procedures1.sql", "pgsql/functions.sql",
                "pgsql/providers/tables.sql", "pgsql/providers/pkeys.sql",
                "pgsql/providers/indexes.sql", "pgsql/providers/types.sql",
                "pgsql/providers/procedures.sql", "pgsql/postinstall.sql",
                "pgsql/nestedsets.sql", "pgsql/nestedsets_sp.sql",
                "pgsql/fulltext_ru.sql"
            };

        public static string[] ScriptList
        {
            get
            {
                return _scriptList;
            }
        }

        private static bool GetBooleanRegistryValue([NotNull] string connectionString, string name)
        {
            using (DataTable dt = Db.registry_list(connectionString, name))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int i;
                    return int.TryParse(dr["Value"].ToString(), out i)
                               ? Convert.ToBoolean(i)
                               : Convert.ToBoolean(dr["Value"]);
                }
            }
            return false;
        }

        public static void system_deleteinstallobjects([NotNull] string connectionString )
        {
            string tSQL = "DROP FUNCTION" + PostgreDbAccess.GetObjectName("system_initialize");
            using (var cmd = PostgreDbAccess.GetCommand(tSQL, true))
            {
                cmd.CommandType = CommandType.Text;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void system_initialize_executescripts(
            [NotNull] string connectionString, string script, string scriptFile, bool useTransactions)
        {
            script = PostgreDbAccess.GetCommandTextReplaced(script);


            //Scripts separation regexp
            var statements = System.Text.RegularExpressions.Regex.Split(script, "(?:--GO)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            using (var connMan = new PostgreDbConnectionManager(connectionString))
            {              
                // use transactions..
                if (useTransactions)
                {
                    using (NpgsqlTransaction trans = connMan.OpenDBConnection(connectionString).BeginTransaction(PostgreDbAccess.IsolationLevel))
                    {
                        foreach (var sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                                {
                                    continue;
                                }

                                if (sql.Length > 0)
                                {
                                    using (var cmd = new NpgsqlCommand())
                                    {
                                        cmd.Transaction = trans;
                                        cmd.Connection = connMan.DBConnection;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = sql.Trim();

                                        // added so command won't timeout anymore..
                                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception x)
                            {
                                sql = sql0;
                                trans.Rollback();
                                throw new Exception(
                                    String.Format(
                                        "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                            }
                        }

                        trans.Commit();
                    }
                }
                else
                {
                    // don't use transactions
                    foreach (string sql in statements.Select(sql0 => sql0.Trim()))
                    {
                        try
                        {
                            if (sql.ToLower().IndexOf("setuser", System.StringComparison.Ordinal) >= 0)
                            {
                                continue;
                            }

                            if (sql.Length > 0)
                            {
                                using (var cmd = new NpgsqlCommand())
                                {
                                    cmd.Connection = connMan.OpenDBConnection(connectionString);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = sql.Trim();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception x)
                        {
                            throw new Exception(
                                String.Format(
                                    "FILE:\n{0}\n\nERROR:\n{2}\n\nSTATEMENT:\n{1}", scriptFile, sql, x.Message));
                        }
                    }
                }
            }


        }

        public static void system_initialize_fixaccess([NotNull] string connectionString, bool bGrant)
        {
            /*   using (VZF.Classes.Data.IDbConnectionManager connMan = new IDbConnectionManager())
            {
                using (IDbTransaction trans = connMan.OpenDBConnection.BeginTransaction(VZF.Classes.Data.PostgreDbAccess.IsolationLevel))
                {
                    // select * from  pg_tables where schemaname tableowner
                  //  select * from  pg_views where schemaname viewowner
                    //  select * from  pg_proc where proname like {objectQualifier} prowner(oid)
                   // select * from pg_user usesysoid <-oid
                    // REVIEW : Ederon - would "{databaseOwner}.{objectQualifier}" work, might need only "{objectQualifier}"
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select Name, OBJECTPROPERTY(id, N'IsUserTable') AS IsUserTable,IsScalarFunction = OBJECTPROPERTY(id, N'IsScalarFunction'),IsProcedure = OBJECTPROPERTY(id, N'IsProcedure'),IsView = OBJECTPROPERTY(id, N'IsView') from dbo.sysobjects where Name like '{databaseOwner}.{objectQualifier}%'", connMan.OpenDBConnection))
                    {
                        da.SelectCommand.Transaction = trans;
                        using (DataTable dt = new DataTable("sysobjects"))
                        {
                            da.Fill(dt);
                            using (var cmd = connMan.DBConnection.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "select current_user";
                                string userName = (string)cmd.ExecuteScalar();

                                if (bGrant)
                                {
                                    cmd.CommandType = CommandType.Text;
                                    foreach (DataRow row in dt.Select("IsProcedure=1 or IsScalarFunction=1"))
                                    {
                                        cmd.CommandText = string.Format("grant execute on \"{0}\" to \"{1}\"", row["Name"], userName);
                                        cmd.ExecuteNonQuery();
                                    }
                                    foreach (DataRow row in dt.Select("IsUserTable=1 or IsView=1"))
                                    {
                                        cmd.CommandText = string.Format("grant select,update on \"{0}\" to \"{1}\"", row["Name"], userName);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    cmd.CommandText = "sp_changeobjectowner";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    foreach (DataRow row in dt.Select("IsUserTable=1"))
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add(new NpgsqlParameter("i_objname", NpgsqlDbType.Varchar));
                                        cmd.Parameters[0].Value = row["Name"];
                                        cmd.Parameters.Add(new NpgsqlParameter("i_newowner", NpgsqlDbType.Varchar));
                                        cmd.Parameters[1].Value = PostgreDbAccess.SchemaName;                                        
                                        
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (NpgsqlException)
                                        {
                                        }
                                    }
                                    foreach (DataRow row in dt.Select("IsView=1"))
                                    {
                                        cmd.Parameters.Clear();
                                      
                                        cmd.Parameters.Add(new NpgsqlParameter("i_objname", NpgsqlDbType.Varchar));
                                        cmd.Parameters[0].Value = row["Name"];
                                        cmd.Parameters.Add(new NpgsqlParameter("i_newowner", NpgsqlDbType.Varchar));
                                        cmd.Parameters[1].Value = PostgreDbAccess.SchemaName;  
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (NpgsqlException)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                    trans.Commit();
                }
            }*/

        }

        public static void system_initialize(
            [NotNull] string connectionString,
            string forumName,
            string timeZone,
            string culture,
            string languageFile,
            string forumEmail,
            string smtpServer,
            string userName,
            string userEmail,
            object providerUserKey,
            string rolePrefix)
        {
            using (var cmd = PostgreDbAccess.GetCommand("system_initialize"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_name", NpgsqlDbType.Varchar)).Value = forumName;
                cmd.Parameters.Add(new NpgsqlParameter("i_timezone", NpgsqlDbType.Integer)).Value = timeZone;
                cmd.Parameters.Add(new NpgsqlParameter("i_languagefile", NpgsqlDbType.Varchar)).Value = languageFile;
                cmd.Parameters.Add(new NpgsqlParameter("i_culture", NpgsqlDbType.Varchar)).Value = culture;
                cmd.Parameters.Add(new NpgsqlParameter("i_forumemail", NpgsqlDbType.Varchar)).Value = forumEmail;
                cmd.Parameters.Add(new NpgsqlParameter("i_smtpserver", NpgsqlDbType.Varchar)).Value = smtpServer;
                cmd.Parameters.Add(new NpgsqlParameter("i_user", NpgsqlDbType.Varchar)).Value = userName;
                // vzrus:The input parameter should be implemented in the system initialize and board_create procedures, else there will be an error in create watch because the user email is missing
                cmd.Parameters.Add(new NpgsqlParameter("i_useremail", NpgsqlDbType.Varchar)).Value = userEmail;
                cmd.Parameters.Add(new NpgsqlParameter("i_userkey", NpgsqlDbType.Uuid)).Value = providerUserKey;
                cmd.Parameters.Add(new NpgsqlParameter("i_newboardguid", NpgsqlDbType.Uuid)).Value = Guid.NewGuid();
                cmd.Parameters.Add(new NpgsqlParameter("i_roleprefix", NpgsqlDbType.Varchar)).Value = rolePrefix;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);

            }

        }

        public static void system_updateversion([NotNull] string connectionString, int version, string name)
        {
            using (var cmd = PostgreDbAccess.GetCommand("system_updateversion"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_version", NpgsqlDbType.Integer)).Value = version;
                cmd.Parameters.Add(new NpgsqlParameter("i_versionname", NpgsqlDbType.Varchar)).Value = name;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns info about all Groups and Rank styles. 
        /// Used in GroupRankStyles cache.
        /// Usage: LegendID = 1 - Select Groups, LegendID = 2 - select Ranks by Name 
        /// </summary>
        public static DataTable group_rank_style([NotNull] string connectionString, object boardID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("group_rank_style"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region DLESKTECH_ShoutBox

        /// <summary>
        /// The shoutbox_getmessages.
        /// </summary>
        /// <param name="numberOfMessages">
        /// The number of messages.
        /// </param>
        /// <param name="useStyledNicks">
        /// Use style for user nicks in ShoutBox.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable shoutbox_getmessages(
            [NotNull] string connectionString, int boardId, int numberOfMessages, object useStyledNicks)
        {
            using (var cmd = PostgreDbAccess.GetCommand("shoutbox_getmessages"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_numberofmessages", NpgsqlDbType.Integer)).Value =
                    numberOfMessages;
                cmd.Parameters.Add(new NpgsqlParameter("i_stylednicks", NpgsqlDbType.Boolean)).Value = useStyledNicks;

                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The shoutbox_savemessage.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool shoutbox_savemessage(
            [NotNull] string connectionString, int boardId, string message, string userName, int userID, object ip)
        {
            using (var cmd = PostgreDbAccess.GetCommand("shoutbox_savemessage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new NpgsqlParameter("i_username", NpgsqlDbType.Varchar)).Value = userName;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_message", NpgsqlDbType.Text)).Value = message;
                cmd.Parameters.Add(new NpgsqlParameter("i_date", NpgsqlDbType.Timestamp)).Value = DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_ip", NpgsqlDbType.Varchar)).Value = ip;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);

                return true;
            }
        }

        public static Boolean shoutbox_clearmessages([NotNull] string connectionString, int boardId)
        {
            using (var cmd = PostgreDbAccess.GetCommand("shoutbox_clearmessages"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_boardid", NpgsqlDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                return true;
            }
        }

        #endregion

        #region Touradg Mods

        // Shinking Operation

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_warning([NotNull] string connectionString )
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_shrink.
        /// </summary>
        public static void db_shrink()
        {
            /*  String ShrinkSql = "DBCC SHRINKDATABASE(N'" + DBName.DBConnection.Database + "')";
            SqlConnection ShrinkConn = new SqlConnection(VZF.Classes.Config.ConnectionString);
            SqlCommand ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
            ShrinkConn.Open();
            ShrinkCmd.ExecuteNonQuery();
            ShrinkConn.Close();
            using (SqlCommand cmd = new SqlCommand(ShrinkSql.ToString(), DBName.OpenDBConnection))
            {
                cmd.Connection = DBName.DBConnection;
                cmd.CommandTimeout = 9999;
                cmd.ExecuteNonQuery();
            }*/
        }

        /// <summary>
        /// The db shink message.
        /// </summary>
        private static string dbShinkMessage;

        /// <summary>
        /// The db_shrink.
        /// </summary>
        /// <param name="DBName">
        /// The db name.
        /// </param>
        public static string db_shrink_new([NotNull] string connectionString )
        {
            /* try
             {
                 using (var conn = new PostgreDbConnectionManager(connectionString))
                 {
                     conn.InfoMessage += new YafDBConnInfoMessageEventHandler(dbShink_InfoMessage);
                   
                     string ShrinkSql = "DBCC SHRINKDATABASE(N'" + conn.DBConnection.Database + "')";
                     var ShrinkConn = new SqlConnection(Config.ConnectionString);
                     var ShrinkCmd = new SqlCommand(ShrinkSql, ShrinkConn);
                     ShrinkConn.Open();
                     ShrinkCmd.ExecuteNonQuery();
                     ShrinkConn.Close();
                     using (var cmd = new SqlCommand(ShrinkSql, conn.OpenDBConnection(connectionString)))
                     {
                         cmd.Connection = conn.DBConnection;
                         cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                         cmd.ExecuteNonQuery();
                     }
                 }
                 return dbShinkMessage;
             }
             finally
             {
                 dbShinkMessage = string.Empty;
             } */
            return string.Empty;
        }

        /// <summary>
        /// The runSql_InfoMessage.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void dbShink_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            dbShinkMessage = "\r\n" + e.Message;
        }

        /// <summary>
        /// The db_recovery_mode_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_warning()
        {
            return string.Empty;
        }

        /// <summary>
        /// The db_recovery_mode_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="dbRecoveryMode">
        /// The db recovery mode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_recovery_mode_new([NotNull] string connectionString, string dbRecoveryMode)
        {
            /* String RecoveryMode = "ALTER DATABASE " + DBName.DBConnection.Database + " SET RECOVERY " + dbRecoveryMode;
            SqlConnection RecoveryModeConn = new SqlConnection(VZF.Classes.Config.ConnectionString);
            SqlCommand RecoveryModeCmd = new SqlCommand(RecoveryMode, RecoveryModeConn);
            RecoveryModeConn.Open();
            RecoveryModeCmd.ExecuteNonQuery();
            RecoveryModeConn.Close();
            using (SqlCommand cmd = new SqlCommand(RecoveryMode.ToString(), DBName.OpenDBConnection))
            {
                cmd.Connection = DBName.DBConnection;
                cmd.CommandTimeout = 9999;
                cmd.ExecuteNonQuery();
            }*/
            return string.Empty;
        }

        #endregion

        #region Buddy

        /// <summary>
        /// Adds a buddy request. (Should be approved later by "ToUserID")
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// The name of the second user + Whether this request is approved or not.
        /// </returns>
        public static string[] buddy_addrequest([NotNull] string connectionString, object FromUserID, object ToUserID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("buddy_addrequest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new NpgsqlParameter("i_paramoutput", NpgsqlDbType.Varchar, 128);
                var approved = new NpgsqlParameter("i_approved", NpgsqlDbType.Boolean);
                paramOutput.Direction = ParameterDirection.Output;
                approved.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = FromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = ToUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                cmd.Parameters.Add(paramOutput);
                cmd.Parameters.Add(approved);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                return new string[] { paramOutput.Value.ToString(), approved.Value.ToString() };
            }
        }

        /// <summary>
        /// Approves a buddy request.
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <param name="Mutual">
        /// Should the requesting user (ToUserID) be added to FromUserID's buddy list too?
        /// </param>
        /// <returns>
        /// the name of the second user.
        /// </returns>
        public static string buddy_approveRequest(
            [NotNull] string connectionString, object FromUserID, object ToUserID, object Mutual)
        {
            using (var cmd = PostgreDbAccess.GetCommand("buddy_approverequest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new NpgsqlParameter("paramOutput", NpgsqlDbType.Varchar, 128)
                                      {
                                          Direction =
                                              ParameterDirection
                                              .Output
                                      };
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = FromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = ToUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_mutual", NpgsqlDbType.Boolean)).Value = Mutual;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;
                cmd.Parameters.Add(paramOutput);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                return paramOutput.Value.ToString();
            }
        }

        /// <summary>
        /// Denies a buddy request.
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// the name of the second user.
        /// </returns>
        public static string buddy_denyRequest([NotNull] string connectionString, object FromUserID, object ToUserID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("buddy_denyrequest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new NpgsqlParameter("paramOutput", NpgsqlDbType.Varchar, 128)
                                      {
                                          Direction =
                                              ParameterDirection
                                              .Output
                                      };
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = FromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = ToUserID;
                cmd.Parameters.Add(paramOutput);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                return paramOutput.Value.ToString();
            }
        }

        /// <summary>
        /// Removes the "ToUserID" from "FromUserID"'s buddy list.
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="ToUserID">
        /// The to user id.
        /// </param>
        /// <returns>
        /// The name of the second user.
        /// </returns>
        public static string buddy_remove([NotNull] string connectionString, object FromUserID, object ToUserID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("buddy_remove"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new NpgsqlParameter("i_paramoutput", NpgsqlDbType.Varchar, 128)
                                      {
                                          Direction =
                                              ParameterDirection
                                              .Output
                                      };
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = FromUserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_touserid", NpgsqlDbType.Integer)).Value = ToUserID;
                cmd.Parameters.Add(paramOutput);
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
                return paramOutput.Value.ToString();
            }
        }

        /// <summary>
        /// Gets all the buddies of a certain user.
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <returns>
        /// a Datatable containing the buddy list.
        /// </returns>
        public static DataTable buddy_list([NotNull] string connectionString, object FromUserID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("buddy_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_fromuserid", NpgsqlDbType.Integer)).Value = FromUserID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region Album

        /// <summary>
        /// Inserts/Saves a user album.
        /// </summary>
        /// <param name="AlbumID">
        /// AlbumID of an existing Album.
        /// </param>
        /// <param name="UserID">
        /// UserID of the user who wants to create a new album.
        /// </param>
        /// <param name="Title">
        /// New Album title.
        /// </param>
        /// <param name="CoverImageID">
        /// New Cover image id.
        /// </param>
        public static int album_save(
            [NotNull] string connectionString, object AlbumID, object UserID, object Title, object CoverImageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("album_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (AlbumID == null || AlbumID.ToString() == "0")
                {
                    AlbumID = DBNull.Value;
                }

                if (UserID == null || UserID.ToString() == "0")
                {
                    UserID = DBNull.Value;
                }

                if (Title == null || string.IsNullOrEmpty(Title.ToString()))
                {
                    Title = DBNull.Value;
                }

                if (CoverImageID == null)
                {
                    CoverImageID = DBNull.Value;
                }

                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_title", NpgsqlDbType.Varchar)).Value = Title;
                cmd.Parameters.Add(new NpgsqlParameter("i_coverimageid", NpgsqlDbType.Integer)).Value = CoverImageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                int uu = Convert.ToInt32(PostgreDbAccess.ExecuteScalar(cmd, connectionString));
                return uu;
            }
        }

        /// <summary>
        /// Lists all the albums associated with the UserID or gets all the
        /// specifications for the specified album id.
        /// </summary>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <param name="AlbumID">
        /// the album id.
        /// </param>
        /// <returns>
        /// a Datatable containing the albums.
        /// </returns>
        public static DataTable album_list([NotNull] string connectionString, object UserID, object AlbumID)
        {
            if (AlbumID == null || AlbumID.ToString() == "0")
            {
                AlbumID = DBNull.Value;
            }
            if (UserID == null || UserID.ToString() == "0")
            {
                UserID = DBNull.Value;
            }
            using (var cmd = PostgreDbAccess.GetCommand("album_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;
                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                return dt;
            }
        }

        /// <summary>
        /// Deletes an album and all Images in that album.
        /// </summary>
        /// <param name="AlbumID">
        /// the album id.
        /// </param>
        public static void album_delete([NotNull] string connectionString, object AlbumID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("album_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes an album and all Images in that album.
        /// </summary>
        /// <param name="AlbumID">
        /// the album id.
        /// </param>
        public static string album_gettitle([NotNull] string connectionString, object AlbumID)
        {

            if (AlbumID == null || AlbumID.ToString() == "0")
            {
                AlbumID = DBNull.Value;
            }

            using (var cmd = PostgreDbAccess.GetCommand("album_gettitle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;
                // object o = PostgreDbAccess.ExecuteScalar(cmd,connectionString);
                return PostgreDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        /// <summary>
        /// Get the number of albums + number of current uploaded files by the user if UserID is not null,
        /// Otherwise, it gets the number of images in the album with AlbumID.
        /// </summary>
        /// <param name="UserID">
        /// the User ID.
        /// </param>
        /// <param name="AlbumID">
        /// the album id.
        /// </param>
        /// <returns></returns>
        public static int[] album_getstats([NotNull] string connectionString, object UserID, object AlbumID)
        {
            if (AlbumID == null || AlbumID.ToString() == "0")
            {
                AlbumID = DBNull.Value;
            }

            if (UserID == null || UserID.ToString() == "0")
            {
                UserID = DBNull.Value;
            }

            using (var cmd = PostgreDbAccess.GetCommand("album_getstats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new NpgsqlParameter("i_userid", NpgsqlDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;

                DataRow dr = PostgreDbAccess.GetData(cmd, connectionString).Rows[0];

                return new[] { Convert.ToInt32(dr["albumnumber"]), Convert.ToInt32(dr["imagenumber"]) };
            }
        }

        /// <summary>
        /// Inserts/Saves a user image.
        /// </summary>
        /// <param name="ImageID">
        /// the image id of an existing image.
        /// </param>
        /// <param name="AlbumID">
        /// the album id for adding a new image.
        /// </param>
        /// <param name="Caption">
        /// the caption of the existing/new image.
        /// </param>
        /// <param name="FileName">
        /// the file name of the new image.
        /// </param>
        /// <param name="Bytes">
        /// the size of the new image.
        /// </param>
        /// <param name="ContentType">
        /// the content type.
        /// </param>
        public static void album_image_save(
            [NotNull] string connectionString,
            object ImageID,
            object AlbumID,
            object Caption,
            object FileName,
            object Bytes,
            object ContentType)
        {
            if (AlbumID == null || AlbumID.ToString() == "0")
            {
                AlbumID = DBNull.Value;
            }

            if (ImageID == null || ImageID.ToString() == "0")
            {
                ImageID = DBNull.Value;
            }

            if (Caption == null || Caption.ToString() == "0")
            {
                Caption = DBNull.Value;
            }

            using (var cmd = PostgreDbAccess.GetCommand("album_image_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageid", NpgsqlDbType.Integer)).Value = ImageID;
                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID;
                cmd.Parameters.Add(new NpgsqlParameter("i_caption", NpgsqlDbType.Varchar)).Value = Caption;
                cmd.Parameters.Add(new NpgsqlParameter("i_filename", NpgsqlDbType.Varchar)).Value = FileName;
                cmd.Parameters.Add(new NpgsqlParameter("i_bytes", NpgsqlDbType.Integer)).Value = Bytes;
                cmd.Parameters.Add(new NpgsqlParameter("i_contenttype", NpgsqlDbType.Varchar)).Value = ContentType;
                cmd.Parameters.Add(new NpgsqlParameter("i_utctimestamp", NpgsqlDbType.Timestamp)).Value =
                    DateTime.UtcNow;

                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists all the images associated with the AlbumID or
        /// the image with the ImageID.
        /// </summary>
        /// <param name="AlbumID">
        /// the Album id.
        /// </param>
        /// <param name="ImageID">
        /// The image id.
        /// </param>
        /// <returns>
        /// a Datatable containing the image(s).
        /// </returns>
        public static DataTable album_image_list([NotNull] string connectionString, object AlbumID, object ImageID)
        {
            if (ImageID == null || ImageID.ToString() == "0")
            {
                ImageID = DBNull.Value;
            }
            using (var cmd = PostgreDbAccess.GetCommand("album_image_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_albumid", NpgsqlDbType.Integer)).Value = AlbumID ?? DBNull.Value;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageid", NpgsqlDbType.Integer)).Value = ImageID;
                DataTable dt = PostgreDbAccess.GetData(cmd, connectionString);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ImageID"] == DBNull.Value)
                    {
                        dt.Rows.RemoveAt(0);
                        dt.AcceptChanges();
                    }
                }

                return dt;
            }
        }

        /// <summary>
        /// Deletes the image which has the specified image id.
        /// </summary>
        /// <param name="ImageID">
        /// the image id.
        /// </param>
        public static void album_image_delete([NotNull] string connectionString, object ImageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("album_image_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageid", NpgsqlDbType.Integer)).Value = ImageID;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Increments the image's download times.
        /// </summary>
        /// <param name="ImageID">
        /// the image id.
        /// </param>
        public static void album_image_download([NotNull] string connectionString, object ImageID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("album_image_download"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new NpgsqlParameter("i_imageid", NpgsqlDbType.Integer)).Value = ImageID;
                PostgreDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Album images by users the specified user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>All Albbum Images of the User</returns>
        public static DataTable album_images_by_user([NotNull] string connectionString, [NotNull] object userID)
        {
            using (var cmd = PostgreDbAccess.GetCommand("album_images_by_user"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_UserID", NpgsqlDbType.Integer).Value = userID;
                return PostgreDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        /// <summary>
        /// The unencode_all_topics_subjects.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="decodeTopicFunc">
        /// The decode topic func.
        /// </param>
        public static void unencode_all_topics_subjects([NotNull] string connectionString, Func<string, string> decodeTopicFunc)
        {
            var topics =
                Db.topic_simplelist(connectionString, 0, 99999999)
                  .SelectTypedList(r => new TypedTopicSimpleList(r))
                  .ToList();

            foreach (var topic in topics.Where(t => t.TopicID.HasValue && t.Topic.IsSet()))
            {
                try
                {
                    var decodedTopic = decodeTopicFunc(topic.Topic);

                    if (!decodedTopic.Equals(topic.Topic))
                    {
                        // unencode it and update.
                        Db.topic_updatetopic(connectionString, topic.TopicID.Value, decodedTopic);
                    }
                }
                catch
                {
                    // soft-fail..
                }
            }
        }
    }
}
