// <copyright company="Vladimir Zakharov" file="Db.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
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
//   The Firebird data access layer.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.Firebird
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Security;

    using FirebirdSql.Data.FirebirdClient;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Handlers;
    using YAF.Types.Objects;

    /// <summary>
    /// All the Database functions for VZF
    /// </summary>
    [SecuritySafeCritical]
    public static class Db
    {
        // added by vzrus
        #region ConnectionStringOptions
  

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

        /// <summary>
        /// Gets the provider assembly name.
        /// </summary>
        public static string ProviderAssemblyName
        {
            get
            {
                return "FirebirdSql.Data.FirebirdClient";
            }
        }
        #endregion

        #region Basic Forum Properties

        /// <summary>
        /// Gets the database size
        /// </summary>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        /// <returns>
        /// An integer value for database size
        /// </returns>
        public static int GetDBSize([NotNull] string connectionString)
        {
            using (var cmd = FbDbAccess.GetCommand("DB_SIZE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_DBNAME", FbDbType.VarChar)).Value =
                    FbDbAccess.DBName.ToUpperInvariant();

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
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
        public static bool GetIsForumInstalled([NotNull] string connectionString)
        {
            try
            {
                using (var dt = board_list(connectionString, DBNull.Value))
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
        public static int GetDBVersion([NotNull] string connectionString)
        {
            try
            {
                using (DataTable dt = registry_list(connectionString, "version"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        // get the version...
                        return Convert.ToInt32(dt.Rows[0]["Value"]);
                    }
                }
            }
            catch
            {
                // not installed...
            }

            return -1;
        }

        #endregion

        #region Forum

        /// <summary>
        /// The pageload method returns DataRow permissions 
        /// and other current user info for access 
        /// and representation control.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="userKey">
        /// The user key.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="forumPage">
        /// The forum page name.   
        /// </param>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="platform">
        /// The platform.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="isCrawler">
        /// The isCrawler.
        /// </param>
        /// <param name="isMobileDevice">
        /// The isMobileDevice.
        /// </param>
        /// <param name="donttrack">
        /// The donttrack.
        /// </param>
        /// <returns>
        /// Common User Info cref="DataRow"
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static DataRow pageload(
            [NotNull] string connectionString,
            [NotNull] object sessionId,
            object boardId,
            object userKey,
            object ip,
            object location,
            object forumPage,
            object browser,
            object platform,
            object categoryId,
            object forumId,
            object topicId,
            object messageId,
            object isCrawler,
            object isMobileDevice,
            object donttrack)
        {
            int nTries = 0;
            while (true)
            {
                try
                {
                    using (var cmd = FbDbAccess.GetCommand("PAGELOAD"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new FbParameter("@I_SESSIONID", FbDbType.VarChar)).Value = sessionId;
                        cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                        
                        // TODO: look why guid here generated not in db
                        if (userKey != null && userKey.ToString().Length > 0)
                        {
                            cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value =
                                new Guid(userKey.ToString()).ToString();
                        }
                        else
                        {
                            cmd.Parameters.Add(new FbParameter("@I_USERKEY", FbDbType.VarChar)).Value = 
                                DBNull.Value;
                        }

                        cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip;
                        cmd.Parameters.Add(new FbParameter("@I_LOCATION", FbDbType.VarChar)).Value = location;
                        cmd.Parameters.Add(new FbParameter("@I_BROWSER", FbDbType.VarChar)).Value = browser;
                        cmd.Parameters.Add(new FbParameter("@I_PLATFORM", FbDbType.VarChar)).Value = platform;
                        cmd.Parameters.Add(new FbParameter("@I_FORUMPAGE", FbDbType.VarChar)).Value = forumPage;
                        cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId;
                        cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                        cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                        cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageId;
                        cmd.Parameters.Add(new FbParameter("@I_ISCRAWLER", FbDbType.Boolean)).Value = (bool)isCrawler;
                        cmd.Parameters.Add(new FbParameter("@I_ISMOBILEDEVICE", FbDbType.Boolean)).Value = (bool)isMobileDevice;
                        cmd.Parameters.Add(new FbParameter("@I_DONTTRACK", FbDbType.Boolean)).Value = (bool)donttrack;
                        cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                        using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                return dt.Rows[0];
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (FbException x)
                {
                    if (x.ErrorCode == 1205 && nTries < 3)
                    {
                        /// Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
                    }
                    else
                    {
                        throw new ApplicationException(
                            string.Format("Sql Exception with error number {0} (Tries={1})", x.ErrorCode, nTries), x);
                    }
                }
                ++nTries;
            }
        }


        public static DataTable GetSearchResult(
            [NotNull] string connectionString,
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
                    ForumListAll(connectionString, boardId, userId, forumIdToStartAt)
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
                DataTable dt = forum_listall_sorted(connectionString, boardId, userId, null, false, forumIdToStartAt);
                forumIDs = dt.Rows.Cast<DataRow>().Aggregate(forumIDs, (current, dr) => current + Convert.ToInt32(dr["ForumID"]).ToString() + ",");
                forumIDs = forumIDs.Substring(0, forumIDs.Length - 1);
            }

            // fix quotes for SQL insertion...
            toSearchWhat = toSearchWhat.Replace("'", "''").Trim();
            toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

            string searchSql = (maxResults == 0) ? "SELECT" : ("SELECT ");
            if (maxResults > 0)
            {
                limitString += String.Format(" FIRST {0} ", maxResults.ToString());
                searchSql += limitString;
            }

            searchSql +=
                @" a.FORUMID AS ""ForumID"", a.TOPICID AS ""TopicID"", a.TOPIC AS ""Topic"", b.USERID AS ""UserID"" , COALESCE(c.USERNAME, b.NAME) AS ""Name"", c.MESSAGEID AS ""MessageID"", c.POSTED AS ""Posted"", '' AS ""Message"", c.FLAGS AS ""Flags""";
            searchSql += " FROM " + FbDbAccess.GetObjectName("TOPIC") + " a LEFT JOIN "
                         + FbDbAccess.GetObjectName("MESSAGE") + @" c ON a.TOPICID = c.TOPICID LEFT JOIN "
                         + FbDbAccess.GetObjectName("USER") + @" b ON c.USERID = b.USERID join "
                         + FbDbAccess.GetObjectName("VACCESS") + @" x ON x.FORUMID=a.FORUMID ";
            searchSql +=
                String.Format(
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
                                searchSql += String.Format(@"(c.MESSAGE like '%{0}%' OR a.TOPIC LIKE '%{0}%' )", word);
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
                                searchSql += String.Format(@"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%' ", word);
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

            using (var cmd = FbDbAccess.GetCommand(searchSql, true))
            {
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region DataSets

        /// <summary>
        /// Gets a list of categories????
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <returns>DataSet with categories</returns>
        public static DataSet ds_forumadmin([NotNull] string connectionString, [NotNull] object boardID, object pageUserID, object isUserForum)
        {
            using (FbDbConnectionManager connMan = new FbDbConnectionManager(connectionString))
            {
                using (DataSet ds = new DataSet())
                {
                    using (
                        FbTransaction trans =
                             connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                    {
                        using (
                            var da = new FbDataAdapter(
                                  FbDbAccess.GetObjectName("category_list"), connMan.DBConnection(connectionString)))
                        {
                            da.SelectCommand.Transaction = trans;

                            da.SelectCommand.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = DBNull.Value;
                          

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.Fill(ds, FbDbAccess.GetObjectName("Category"));
                            da.SelectCommand.CommandText = FbDbAccess.GetObjectName("forum_list");
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = pageUserID;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_ISUSERFORUM", FbDbType.Boolean)).Value = isUserForum;
                            da.Fill(ds, FbDbAccess.GetObjectName("ForumUnsorted"));
                            DataTable dtForumListSorted = ds.Tables[FbDbAccess.GetObjectName("ForumUnsorted")].Clone();
                            dtForumListSorted.TableName = FbDbAccess.GetObjectName("Forum");
                            ds.Tables.Add(dtForumListSorted);
                            dtForumListSorted.Dispose();
                            forum_list_sort_basic(
                                connectionString,
                                ds.Tables[FbDbAccess.GetObjectName("ForumUnsorted")],
                                ds.Tables[FbDbAccess.GetObjectName("Forum")],
                                0,
                                0);
                            ds.Tables.Remove(FbDbAccess.GetObjectName("ForumUnsorted"));
                            ds.Relations.Add(
                                "FK_Forum_Category",
                                ds.Tables[FbDbAccess.GetObjectName("Category")].Columns["CategoryID"],
                                ds.Tables[FbDbAccess.GetObjectName("Forum")].Columns["CategoryID"]);
                            trans.Commit();

                        }

                        return ds;
                    }
                }
            }
        }

        #endregion

        #region yaf_AccessMask

        /// <summary>
        /// Gets a list of access mask properities
        /// </summary>
        /// <param name="boardID">ID of Board</param>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public static DataTable accessmask_list(
            [NotNull] string connectionString, object boardID, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            using (var cmd = FbDbAccess.GetCommand("accessmask_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add(new FbParameter("@I_EXCLUDEFLAGS", FbDbType.Integer)).Value = excludeFlags;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSEID", FbDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERMASK", FbDbType.Boolean)).Value = isUserMask;
                cmd.Parameters.Add(new FbParameter("@I_ISADMINMASK", FbDbType.Boolean)).Value = isAdminMask;
                
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The accessmask_pforumlist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        /// <param name="excludeFlags">
        /// The exclude flags.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="isUserMask">
        /// The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        /// The is admin mask.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable accessmask_pforumlist(
           [NotNull] string connectionString, object boardID, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            using (var cmd = FbDbAccess.GetCommand("accessmask_pforumlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add(new FbParameter("@I_EXCLUDEFLAGS", FbDbType.Integer)).Value = excludeFlags;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSEID", FbDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERMASK", FbDbType.Boolean)).Value = isUserMask;
                cmd.Parameters.Add(new FbParameter("@I_ISADMINMASK", FbDbType.Boolean)).Value = isAdminMask;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The accessmask_aforumlist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="accessMaskID">
        /// The access mask id.
        /// </param>
        /// <param name="excludeFlags">
        /// The exclude flags.
        /// </param>
        /// <param name="pageUserID">
        /// The page user id.
        /// </param>
        /// <param name="isUserMask">
        /// The is user mask.
        /// </param>
        /// <param name="isAdminMask">
        /// The is admin mask.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable accessmask_aforumlist(
           [NotNull] string connectionString, object boardID, object accessMaskID, object excludeFlags, object pageUserID, bool isUserMask, bool isAdminMask)
        {
            using (var cmd = FbDbAccess.GetCommand("accessmask_aforumlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add(new FbParameter("@I_EXCLUDEFLAGS", FbDbType.Integer)).Value = excludeFlags;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSEID", FbDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERMASK", FbDbType.Boolean)).Value = isUserMask;
                cmd.Parameters.Add(new FbParameter("@I_ISADMINMASK", FbDbType.Boolean)).Value = isAdminMask;

                return FbDbAccess.GetData(cmd, connectionString);

            }
        }

        /// <summary>
        /// Deletes an access mask
        /// </summary>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <returns></returns>
        public static bool accessmask_delete([NotNull] string connectionString, object accessMaskID)
        {
            using (var cmd = FbDbAccess.GetCommand("accessmask_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer));
                cmd.Parameters[0].Value = accessMaskID;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString)) != 0;
            }
        }

        /// <summary>
        /// Saves changes to a access mask 
        /// </summary>
        /// <param name="accessMaskID">ID of access mask</param>
        /// <param name="boardID">ID of board</param>
        /// <param name="name">Name of access mask</param>
        /// <param name="readAccess">Read Access?</param>
        /// <param name="postAccess">Post Access?</param>
        /// <param name="replyAccess">Reply Access?</param>
        /// <param name="priorityAccess">Priority Access?</param>
        /// <param name="pollAccess">Poll Access?</param>
        /// <param name="voteAccess">Vote Access?</param>
        /// <param name="moderatorAccess">Moderator Access?</param>
        /// <param name="editAccess">Edit Access?</param>
        /// <param name="deleteAccess">Delete Access?</param>
        /// <param name="uploadAccess">Upload Access?</param>
        /// <param name="downloadAccess">Download Access?</param>
        public static void accessmask_save(
            [NotNull] string connectionString,
            object accessMaskID,
            object boardID,
            object name,
            object readAccess,
            object postAccess,
            object replyAccess,
            object priorityAccess,
            object pollAccess,
            object voteAccess,
            object moderatorAccess,
            object editAccess,
            object deleteAccess,
            object uploadAccess,
            object downloadAccess,
            [NotNull] object userForumAccess,
            object sortOrder, 
            [CanBeNull] object userId,
            [NotNull] object isUserMask, [NotNull] object isAdminMask)
        {
            using (var cmd = FbDbAccess.GetCommand("accessmask_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_ACCESSMASKID", FbDbType.Integer).Value = accessMaskID;
                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;
                cmd.Parameters.Add("@I_NAME", FbDbType.VarChar).Value = name;
                cmd.Parameters.Add("@I_READACCESS", FbDbType.Boolean).Value = readAccess;
                cmd.Parameters.Add("@I_POSTACCESS", FbDbType.Boolean).Value = postAccess;
                cmd.Parameters.Add("@I_REPLYACCESS", FbDbType.Boolean).Value = replyAccess;
                cmd.Parameters.Add("@I_PRIORITYACCESS", FbDbType.Boolean).Value = priorityAccess;
                cmd.Parameters.Add("@I_POLLACCESS", FbDbType.Boolean).Value = pollAccess;
                cmd.Parameters.Add("@I_VOTEACCESS", FbDbType.Boolean).Value = voteAccess;
                cmd.Parameters.Add("@I_MODERATORACCESS", FbDbType.Boolean).Value = moderatorAccess;
                cmd.Parameters.Add("@I_EDITACCESS", FbDbType.Boolean).Value = editAccess;
                cmd.Parameters.Add("@I_DELETEACCESS", FbDbType.Boolean).Value = deleteAccess;
                cmd.Parameters.Add("@I_UPLOADACCESS", FbDbType.Boolean).Value = uploadAccess;
                cmd.Parameters.Add("@I_DOWNLOADACCESS", FbDbType.Boolean).Value = downloadAccess;
                cmd.Parameters.Add("@I_USERFORUMACCESS", FbDbType.Boolean).Value = userForumAccess;
                cmd.Parameters.Add("@I_SORTORDER", FbDbType.Integer).Value = sortOrder;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                cmd.Parameters.Add("@I_ISUSERMASK", FbDbType.Boolean).Value = isUserMask;
                cmd.Parameters.Add("@I_ISADMINMASK", FbDbType.Boolean).Value = isAdminMask;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Active

        /// <summary>
        /// Gets list of active users
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="Guests"></param>
        /// <returns>Returns a DataTable of active users</returns>
        public static DataTable active_list(
            [NotNull] string connectionString,
            object boardID,
            object Guests,
            object showCrawlers,
            int interval,
            object styledNicks)
        {
            if (Guests == null)
            {
                Guests = 0;
            }
            using (var cmd = FbDbAccess.GetCommand("active_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_GUESTS", FbDbType.Boolean)).Value = Guests;
                cmd.Parameters.Add(new FbParameter("@I_SHOWCRAWLERS", FbDbType.Boolean)).Value = showCrawlers;
                cmd.Parameters.Add(new FbParameter("@I_INTERVAL", FbDbType.Boolean)).Value = interval;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets list of active users for a specific user with access fixes to not display him forbidden locations. 
        /// </summary>
        /// <param name="boardID">
        /// BoardID
        /// </param>
        /// <param name="userID">
        /// the UserID
        /// </param>
        /// <param name="Guests">
        /// </param>
        /// <param name="activeTime">
        /// The active Time.
        /// </param>
        /// <param name="styledNicks">
        /// The styled Nicks.
        /// </param>
        /// <returns>
        /// Returns a DataTable of active users
        /// </returns>
        public static DataTable active_list_user(
            [NotNull] string connectionString,
            object boardID,
            object userID,
            object guests,
            object showCrawlers,
            int activeTime,
            object styledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("active_list_user"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_GUESTS", FbDbType.Boolean)).Value = guests;
                cmd.Parameters.Add(new FbParameter("@I_SHOWCRAWLERS", FbDbType.Boolean)).Value = showCrawlers;
                cmd.Parameters.Add(new FbParameter("@I_ACTIVETIME", FbDbType.Integer)).Value = activeTime;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets the list of active users within a certain forum
        /// </summary>
        /// <param name="forumID">forumID</param>
        /// <returns>DataTable of all ative users in a forum</returns>
        public static DataTable active_listforum([NotNull] string connectionString, object forumID, object styledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("active_listforum"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets the list of active users in a topic
        /// </summary>
        /// <param name="topicID">ID of topic </param>
        /// <returns>DataTable of all users that are in a topic</returns>
        public static DataTable active_listtopic([NotNull] string connectionString, object topicID, object styledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("active_listtopic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;

                return FbDbAccess.GetData(cmd, connectionString);

            }
        }

        /// <summary>
        /// Gets the activity statistics for a board
        /// </summary>
        /// <param name="boardID">boardID</param>
        /// <returns>DataRow of activity stata</returns>
        public static DataRow active_stats([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("active_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows[0];
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
        /// <param name="boardID">boardID</param>
        /// <returns>DataTable with attachement list</returns>
        public static DataTable attachment_list(
            [NotNull] string connectionString,
            [NotNull] object messageID,
            [NotNull] object attachmentID,
            [NotNull] object boardID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize)
        {
            using (var cmd = FbDbAccess.GetCommand("attachment_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_ATTACHMENTID", FbDbType.Integer)).Value = attachmentID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("attachment_save"))
            {
                byte[] fileData = null;
                if (stream != null)
                {
                    fileData = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(fileData, 0, (int)stream.Length);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_FILENAME", FbDbType.VarChar)).Value = fileName;
                cmd.Parameters.Add(new FbParameter("@I_BYTES", FbDbType.Integer)).Value = bytes;
                cmd.Parameters.Add(new FbParameter("@I_CONTENTTYPE", FbDbType.VarChar)).Value = contentType;
                cmd.Parameters.Add(new FbParameter("@I_FILEDATA", FbDbType.Binary)).Value = fileData;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete attachment
        /// </summary>
        /// <param name="attachmentID">ID of attachment to delete</param>
        public static void attachment_delete([NotNull] string connectionString, [NotNull] object attachmentID)
        {
            bool UseFileTable = GetBooleanRegistryValue(connectionString, "UseFileTable");


            //If the files are actually saved in the Hard Drive
            if (!UseFileTable)
            {
                using (var cmd = FbDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = DBNull.Value;
                    cmd.Parameters.Add(new FbParameter("@I_ATTACHMENTID", FbDbType.Integer)).Value = attachmentID;
                    cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = DBNull.Value;
                    cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = 0;
                    cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = 1;


                    DataTable tbAttachments = FbDbAccess.GetData(cmd, connectionString);
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
                            // error deleting that file... 
                        }
                    }
                }
            }
            using (var cmd = FbDbAccess.GetCommand("attachment_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ATTACHMENTID", FbDbType.Integer)).Value = attachmentID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }

        }


        /// <summary>
        /// Attachement dowload
        /// </summary>
        /// <param name="attachmentID">ID of attachemnt to download</param>
        public static void attachment_download([NotNull] string connectionString, object attachmentID)
        {
            using (var cmd = FbDbAccess.GetCommand("attachment_download"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ATTACHMENTID", FbDbType.Integer)).Value = attachmentID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_BannedIP

        /// <summary>
        /// List of Baned IP's
        /// </summary>
        /// <param name="boardID">ID of board</param>
        /// <param name="ID">ID</param>
        /// <returns>DataTable of banned IPs</returns>
        public static DataTable bannedip_list(
            [NotNull] string connectionString,
            [NotNull] object boardID,
            [CanBeNull] object ID,
            [CanBeNull] object pageIndex,
            [CanBeNull] object pageSize)
        {
            using (var cmd = FbDbAccess.GetCommand("bannedip_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = ID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves baned ip in database
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="boardID">BoardID</param>
        /// <param name="Mask">Mask</param>
        public static void bannedip_save(
            [NotNull] string connectionString, object ID, object boardID, object Mask, string reason, int userID)
        {
            using (var cmd = FbDbAccess.GetCommand("bannedip_save"))
            {
                //Regex for ip
                //  \b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = ID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_MASK", FbDbType.VarChar)).Value = Mask;
                cmd.Parameters.Add(new FbParameter("@I_REASON", FbDbType.VarChar)).Value = reason;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes Banned IP
        /// </summary>
        /// <param name="ID">ID of banned ip to delete</param>
        public static void bannedip_delete([NotNull] string connectionString, object ID)
        {
            using (var cmd = FbDbAccess.GetCommand("bannedip_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = ID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Board

        /// <summary>
        /// Gets a list of information about a board
        /// </summary>
        /// <param name="boardID">board id</param>
        /// <returns>DataTable</returns>
        public static DataTable board_list([NotNull] string connectionString, object boardID)
        {
            String _systemInfo = String.Concat(
                " OS: ",
                Platform.VersionString,
                " - Runtime: ",
                Platform.RuntimeName,
                " ",
                Platform.RuntimeString,
                " - Number of processors: ",
                Platform.Processors,
                " - Allocated memory:",
                (Platform.AllocatedMemory / 1024 / 1024).ToString(),
                " Mb.");

            using (var cmd = FbDbAccess.GetCommand("board_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer));
                cmd.Parameters[0].Value = boardID;
                cmd.Parameters.AddWithValue("@I_SYSINFO", _systemInfo);
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets posting statistics
        /// </summary>
        /// <param name="boardId">BoardID</param>
        /// <returns>DataRow of Poststats</returns>
        public static DataRow board_poststats(
            [NotNull] string connectionString, int? boardId, bool useStyledNicks, bool showNoCountPosts)
        {
            using (var cmd = FbDbAccess.GetCommand("board_poststats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_SHOWNOCOUNTPOSTS", FbDbType.Boolean)).Value = showNoCountPosts;
                cmd.Parameters.Add(new FbParameter("@I_GETDEFAULTS", FbDbType.Boolean)).Value = 0;
                cmd.Parameters[0].Value = boardId;

                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }

            // vzrus - this happens at a new install only when we don't have posts or when they are not visible to a user 
            using (var cmd = FbDbAccess.GetCommand("board_poststats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_SHOWNOCOUNTPOSTS", FbDbType.Boolean)).Value = showNoCountPosts;
                cmd.Parameters.Add(new FbParameter("@I_GETDEFAULTS", FbDbType.Boolean)).Value = 0;
                cmd.Parameters[0].Value = boardId;

                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Gets users statistics
        /// </summary>
        /// <param name="boardId">
        /// BoardID
        /// </param>
        /// <returns>
        /// DataRow of Poststats
        /// </returns>
        public static DataRow board_userstats([NotNull] string connectionString, int? boardId)
        {
            using (var cmd = FbDbAccess.GetCommand("board_userstats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows[0];
                }
            }
        }

        /// <summary>
        /// Recalculates topic and post numbers and updates last post for all forums in all boards
        /// </summary>
        public static void board_resync([NotNull] string connectionString)
        {
            board_resync(connectionString, null);
        }

        /// <summary>
        /// Recalculates topic and post numbers and updates last post for specified board
        /// </summary>
        /// <param name="boardID">BoardID of board to do re-sync for, if null, all boards are re-synced</param>
        public static void board_resync([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("board_resync"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets statistica about number of posts etc.
        /// </summary>
        /// <returns>DataRow</returns>
        public static DataRow board_stats([NotNull] string connectionString)
        {
            return board_stats(connectionString, null);
        }

        public static DataRow board_stats([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("board_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;

                return FbDbAccess.GetData(cmd, connectionString).Rows[0];
            }
        }

        /// <summary>
        /// Saves board information
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="name">Name of Board</param>
        /// <param name="allowThreaded">Boolen value, allowThreaded</param>
        public static int board_save(
            [NotNull] string connectionString,
            object boardID,
            object languageFile,
            object culture,
            object name,
            object allowThreaded)
        {
            using (var cmd = FbDbAccess.GetCommand("BOARD_SAVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;
                cmd.Parameters.Add("@I_NAME", FbDbType.VarChar).Value = name;
                cmd.Parameters.Add("@I_LANGUAGEFILE", FbDbType.VarChar).Value = languageFile;
                cmd.Parameters.Add("@I_CULTURE", FbDbType.VarChar, 10).Value = culture;
                cmd.Parameters.Add("@I_ALLOWTHREADED", FbDbType.Boolean).Value = allowThreaded;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));

            }
        }

        /// <summary>
        /// Creates a new board
        /// </summary>
        /// <param name="adminUsername">Membership Provider User Name</param>
        /// <param name="adminUserKey">Membership Provider User Key</param>
        /// <param name="boardName">Name of new board</param>
        /// <param name="boardMembershipName">Membership Provider Application Name for new board</param>
        /// <param name="boardRolesName">Roles Provider Application Name for new board</param>
        public static int board_create(
            [NotNull] string connectionString,
            [NotNull] object adminUsername,
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
            using (var cmd = FbDbAccess.GetCommand("BOARD_CREATE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDNAME", FbDbType.VarChar).Value = boardName;
                cmd.Parameters.Add("@I_CULTURE", FbDbType.VarChar, 10).Value = culture;
                cmd.Parameters.Add("@I_LANGUAGEFILE", FbDbType.VarChar).Value = languageFile;
                cmd.Parameters.Add("@I_MEMBERSHIPAPPNAME", FbDbType.VarChar).Value = boardMembershipName;
                cmd.Parameters.Add("@I_ROLESAPPNAME", FbDbType.VarChar).Value = boardRolesName;
                cmd.Parameters.Add("@I_USERNAME", FbDbType.VarChar).Value = adminUsername;
                cmd.Parameters.Add("@I_USEREMAIL", FbDbType.VarChar).Value = adminUserEmail;
                cmd.Parameters.Add("@I_USERKEY", FbDbType.VarChar).Value = adminUserKey;
                cmd.Parameters.Add("@I_ISHOSTADMIN", FbDbType.Boolean).Value = isHostUser;
                cmd.Parameters.Add("@I_ROLEPREFIX", FbDbType.VarChar).Value = rolePrefix;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));

            }
        }

        /// <summary>
        /// Deletes a board
        /// </summary>
        /// <param name="boardID">ID of board to delete</param>
        public static void board_delete([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("board_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Category

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="CategoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> value indicationg if category was deleted .
        /// </returns>
        public static bool category_delete([NotNull] string connectionString, object CategoryID)
        {
            using (var cmd = FbDbAccess.GetCommand("category_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = CategoryID;

                return (int)FbDbAccess.ExecuteScalar(cmd, connectionString) != 0;
            }
        }

        /// <summary>
        /// Gets a list of forums in a category
        /// </summary>
        /// <param name="boardID">boardID</param>
        /// <param name="categoryID">categotyID</param>
        /// <returns>DataTable with a list of forums in a category</returns>
        public static DataTable category_list([NotNull] string connectionString, object boardID, object categoryID)
        {
            using (var cmd = FbDbAccess.GetCommand("category_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Gets a list of forums in a category
        /// </summary>
        /// <param name="boardID">boardID</param>
        /// <param name="categoryID">categotyID</param>
        /// <returns>DataTable with a list of forums in a category</returns>
        public static DataTable category_pfaccesslist([NotNull] string connectionString, object boardID, object categoryID)
        {
            using (var cmd = FbDbAccess.GetCommand("category_pfaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The category_getadjacentforum.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardID">
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
        public static DataTable category_getadjacentforum([NotNull] string connectionString, [NotNull] object boardID, [CanBeNull] object categoryID, object userId, bool isAfter)
        {
            using (var cmd = FbDbAccess.GetCommand("category_getadjacentforum"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_ISAFTER", FbDbType.Boolean)).Value = isAfter;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a list of forum categories
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
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable category_listread(
            [NotNull] string connectionString, object boardId, object userId, object categoryId)
        {
            using (var cmd = FbDbAccess.GetCommand("category_listread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId;
               
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("category_simplelist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_STARTID", FbDbType.Integer)).Value = startID;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = limit;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves changes to a category
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="categoryImage">
        /// The category image.
        /// </param>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
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
            using (var cmd = FbDbAccess.GetCommand("category_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrderChecked;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYIMAGE", FbDbType.VarChar)).Value = categoryImage;
                cmd.Parameters.Add(new FbParameter("@I_CANHAVEPERSFORUMS", FbDbType.Boolean)).Value = canHavePersForums;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        public static void checkemail_save([NotNull] string connectionString, object userID, object hash, object email)
        {
            using (var cmd = FbDbAccess.GetCommand("checkemail_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_HASH", FbDbType.VarChar)).Value = hash;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;


                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Updates a hash
        /// </summary>
        /// <param name="hash">New hash</param>
        /// <returns>DataTable with user information</returns>
        public static DataTable checkemail_update([NotNull] string connectionString, object hash)
        {
            using (var cmd = FbDbAccess.GetCommand("checkemail_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_HASH", FbDbType.VarChar)).Value = hash;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets a check email entry based on email or all if no email supplied
        /// </summary>
        /// <param name="email">Associated email</param>
        /// <returns>DataTable with check email information</returns>
        public static DataTable checkemail_list([NotNull] string connectionString, object email)
        {
            using (var cmd = FbDbAccess.GetCommand("checkemail_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Choice

        /// <summary>
        /// Saves a vote in the database
        /// </summary>
        /// <param name="choiceID">Choice of the vote</param>
        public static void choice_vote([NotNull] string connectionString, object choiceID, object userID, object remoteIP)
        {
            using (var cmd = FbDbAccess.GetCommand("choice_vote"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_CHOICEID", FbDbType.Integer));
                cmd.Parameters[0].Value = choiceID;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[1].Value = userID;

                cmd.Parameters.Add(new FbParameter("@I_REMOTEIP", FbDbType.VarChar));
                cmd.Parameters[2].Value = remoteIP;


                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_EventLog

        public static void eventlog_create(
            [NotNull] string connectionString, object userID, object source, object description, object type)
        {
            try
            {
                if (userID == null) userID = DBNull.Value;
                using (var cmd = FbDbAccess.GetCommand("eventlog_create"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                    cmd.Parameters.Add(new FbParameter("@I_SOURCE", FbDbType.VarChar)).Value = source.ToString();
                    cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.Text)).Value = description.ToString();
                    cmd.Parameters.Add(new FbParameter("@I_TYPE", FbDbType.Integer)).Value = type;
                    cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }


        public static void eventlog_create([NotNull] string connectionString, object userID, object source, object description)
        {
            eventlog_create(connectionString, userID, (object)source.GetType().ToString(), description, (object)0);
        }

        /// <summary>
        /// Deletes all event log entries for given board.
        /// </summary>
        /// <param name="boardID">ID of board.</param>
        public static void eventlog_delete([NotNull] string connectionString, int boardID, int pageUserId)
        {
            eventlog_delete(connectionString, null, boardID, pageUserId);
        }

        /// <summary>
        /// Deletes event log entry of given ID.
        /// </summary>
        /// <param name="eventLogID">ID of event log entry.</param>
        public static void eventlog_delete([NotNull] string connectionString, object eventLogID, int pageUserId)
        {
            eventlog_delete(connectionString, eventLogID, null, pageUserId);
        }

        /// <summary>
        /// Calls underlying stroed procedure for deletion of event log entry(ies).
        /// </summary>
        /// <param name="eventLogID">When not null, only given event log entry is deleted.</param>
        /// <param name="boardID">Specifies board. It is ignored if eventLogID parameter is not null.</param>
        public static void eventlog_delete(
            [NotNull] string connectionString, object eventLogID, object boardID, object pageUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("eventlog_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_EVENTLOGID", FbDbType.Integer)).Value = eventLogID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("eventlog_deletebyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


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
            using (var cmd = FbDbAccess.GetCommand("eventlog_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new FbParameter("@I_MAXROWS", FbDbType.Integer)).Value = maxRows;
                cmd.Parameters.Add(new FbParameter("@I_MAXDAYS", FbDbType.Integer)).Value = maxDays;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_EVENTIDS", FbDbType.Text)).Value = eventIDs;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("eventloggroupaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_EVENTTYPEID", FbDbType.Integer)).Value = eventTypeId;
                cmd.Parameters.Add(new FbParameter("@I_EVENTTYPENAME", FbDbType.VarChar)).Value = eventTypeName;
                cmd.Parameters.Add(new FbParameter("@I_DELETEACCESS", FbDbType.Boolean)).Value = deleteAccess;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("eventloggroupaccess_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_EVENTTYPEID", FbDbType.Integer)).Value = eventTypeId;
                cmd.Parameters.Add(new FbParameter("@I_EVENTTYPENAME", FbDbType.VarChar)).Value = eventTypeName;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("eventloggroupaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_EVENTTYPEID", FbDbType.Integer)).Value = eventTypeId;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("group_eventlogaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        #endregion yaf_EventLog
        
        #region yaf_Extensions

        public static void extension_delete([NotNull] string connectionString, object extensionId)
        {
            try
            {
                using (var cmd = FbDbAccess.GetCommand("extension_delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_EXTENSIONID", FbDbType.Integer));
                    cmd.Parameters[0].Value = extensionId;

                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("extension_edit"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_EXTENSIONID", FbDbType.Integer));
                cmd.Parameters[0].Value = extensionId;

                return FbDbAccess.GetData(cmd, connectionString);
            }

        }

        // Used to validate a file before uploading
        public static DataTable extension_list([NotNull] string connectionString, object boardID, object extension)
        {
            using (var cmd = FbDbAccess.GetCommand("extension_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer));
                cmd.Parameters[0].Value = boardID;

                cmd.Parameters.Add(new FbParameter("@I_EXTENSION", FbDbType.VarChar));
                cmd.Parameters[1].Value = extension;

                return FbDbAccess.GetData(cmd, connectionString);
            }

        }

        // Returns an extension list for a given Board
        public static DataTable extension_list([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("extension_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer));
                cmd.Parameters[0].Value = boardID;

                cmd.Parameters.Add(new FbParameter("@I_EXTENSION", FbDbType.VarChar));
                cmd.Parameters[1].Value = null;


                return FbDbAccess.GetData(cmd, connectionString);

            }

        }

        // Saves / creates extension
        public static void extension_save([NotNull] string connectionString, object extensionId, object boardID, object extension)
        {
            try
            {
                using (var cmd = FbDbAccess.GetCommand("extension_save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_EXTENSIONID", FbDbType.Integer));
                    cmd.Parameters[0].Value = extensionId;

                    cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer));
                    cmd.Parameters[1].Value = boardID;

                    cmd.Parameters.Add(new FbParameter("@I_EXTENSION", FbDbType.VarChar));
                    cmd.Parameters[2].Value = extension;


                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        /// <param name="choiceID">Choice of the vote</param>
        public static DataTable pollvote_check([NotNull] string connectionString, object pollid, object userid, object remoteip)
        {
            using (var cmd = FbDbAccess.GetCommand("pollvote_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_POLLID", FbDbType.Integer)).Value = pollid;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new FbParameter("@I_REMOTEIP", FbDbType.VarChar)).Value = remoteip;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

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
            using (var cmd = FbDbAccess.GetCommand("pollgroup_votecheck"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_POLLGROUPID", FbDbType.Integer).Value = pollGroupId;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                cmd.Parameters.Add("@I_REMOTEIP", FbDbType.VarChar).Value = remoteIp;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Forum

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
            using (var cmd = FbDbAccess.GetCommand("forum_ns_getchildren_anyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("i_boardid", FbDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new FbParameter("i_categoryid", FbDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new FbParameter("i_forumid", FbDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userid;
                cmd.Parameters.Add(new FbParameter("i_notincluded", FbDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new FbParameter("i_immediateonly", FbDbType.Boolean)).Value = immediateonly;

                DataTable dt = FbDbAccess.GetData(cmd, connectionString);
                DataTable sorted = dt.Clone();
                bool forumRow = false;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = sorted.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    newRow = row;

                    int currentIndent = (int)row["Level"];
                    string sIndent = string.Empty;

                    for (int j = 0; j < currentIndent; j++)
                    {
                        sIndent += "--";
                    }
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

        /// <summary>
        /// The forum_ns_getchildren.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardid">
        /// The boardid.
        /// </param>
        /// <param name="categoryid">
        /// The categoryid.
        /// </param>
        /// <param name="forumid">
        /// The forumid.
        /// </param>
        /// <param name="notincluded">
        /// The notincluded.
        /// </param>
        /// <param name="immediateonly">
        /// The immediateonly.
        /// </param>
        /// <param name="indentchars">
        /// The indentchars.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_ns_getchildren(
            [NotNull] string connectionString,
            int? boardid,
            int? categoryid,
            int? forumid,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_ns_getchildren_activeuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("i_boardid", FbDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new FbParameter("i_categoryid", FbDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new FbParameter("i_forumid", FbDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new FbParameter("i_notincluded", FbDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new FbParameter("i_immediateonly", FbDbType.Boolean)).Value = immediateonly;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The forum_ns_getchildren_activeuser.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardid">
        /// The boardid.
        /// </param>
        /// <param name="categoryid">
        /// The categoryid.
        /// </param>
        /// <param name="forumid">
        /// The forumid.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="notincluded">
        /// The notincluded.
        /// </param>
        /// <param name="immediateonly">
        /// The immediateonly.
        /// </param>
        /// <param name="indentchars">
        /// The indentchars.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_ns_getchildren_activeuser(
            [NotNull] string connectionString,
            int? boardid,
            int? categoryid,
            int? forumid,
            int? userId,
            bool notincluded,
            bool immediateonly,
            string indentchars)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_ns_getchildren_activeuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("i_boardid", FbDbType.Integer)).Value = boardid;
                cmd.Parameters.Add(new FbParameter("i_categoryid", FbDbType.Integer)).Value = categoryid;
                cmd.Parameters.Add(new FbParameter("i_forumid", FbDbType.Integer)).Value = forumid;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("i_notincluded", FbDbType.Boolean)).Value = notincluded;
                cmd.Parameters.Add(new FbParameter("i_immediateonly", FbDbType.Boolean)).Value = immediateonly;

                DataTable dt = FbDbAccess.GetData(cmd, connectionString);
                DataTable sorted = dt.Clone();
                bool forumRow = false;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = sorted.NewRow();
                    newRow.ItemArray = row.ItemArray;
                    newRow = row;

                    int currentIndent = (int)row["Level"];
                    string sIndent = string.Empty;

                    for (int j = 0; j < currentIndent; j++)
                    {
                        sIndent += "--";
                    }
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

        /// <summary>
        /// List of categories accessible for an active user
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>A <see cref="T:System.Data.DataTable"/> of categories.</returns>
        public static DataTable forum_categoryaccess_activeuser([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_cataccess_activeuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        //ABOT NEW 16.04.04

        /// <summary>
        /// Deletes attachments out of a entire forum
        /// </summary>
        /// <param name="ForumID">ID of forum to delete all attachemnts out of</param>
        private static void forum_deleteAttachments([NotNull] string connectionString, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_listtopics"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row != null && row["TopicID"] != DBNull.Value)
                        {
                            topic_delete(connectionString, row["TopicID"], true);
                        }
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
            using (var cmd = FbDbAccess.GetCommand("forum_listSubForums"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                if (!(FbDbAccess.ExecuteScalar(cmd, connectionString) is DBNull))
                {
                    return false;
                }

                forum_deleteAttachments(connectionString, forumID);
                using (var cmdNew = FbDbAccess.GetCommand("forum_delete"))
                {
                    cmdNew.CommandType = CommandType.StoredProcedure;
                    cmdNew.CommandTimeout = 99999;
                    cmdNew.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                    FbDbAccess.ExecuteNonQuery(cmdNew, connectionString);
                }

                return true;
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
            using (var cmd = FbDbAccess.GetCommand("forum_tags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_boardid", FbDbType.Integer).Value = boardId;
                cmd.Parameters.Add("i_pageuserid", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("i_forumid", FbDbType.Integer).Value = forumId;
                cmd.Parameters.Add("i_pageindex", FbDbType.Integer).Value = pageIndex;
                cmd.Parameters.Add("i_pagesize", FbDbType.Integer).Value = pageSize;
                cmd.Parameters.Add("i_searchtext", FbDbType.VarChar).Value = searchText;
                cmd.Parameters.Add("i_beginswith", FbDbType.Boolean).Value = beginsWith;

                return FbDbAccess.GetData(cmd, true, connectionString);
            }
        }

        /// <summary>
        /// Moves a forum
        /// </summary>
        /// <param name="ForumID">forum to delete</param>
        /// <returns>bool to indicate that forum has been deleted</returns>
        public static bool forum_move([NotNull] string connectionString, [NotNull] object forumOldID, [NotNull] object forumNewID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_listSubForums"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumOldID;

                if (!(FbDbAccess.ExecuteScalar(cmd, connectionString) is DBNull))
                {
                    return false;
                }

                using (var cmd_new = FbDbAccess.GetCommand("forum_move"))
                {
                    cmd_new.CommandType = CommandType.StoredProcedure;
                    cmd_new.CommandTimeout = 99999;
                    cmd.Parameters.Add(new FbParameter("@I_FORUMOLDID", FbDbType.Integer)).Value = forumOldID;
                    cmd.Parameters.Add(new FbParameter("@I_FORUMNEWID", FbDbType.Integer)).Value = forumNewID;
                    cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                    FbDbAccess.ExecuteNonQuery(cmd_new, connectionString);
                }

                return true;
            }
        }

        /// <summary>
        /// Lists all moderated forums for a user
        /// </summary>
        /// <param name="boardID">board if of moderators</param>
        /// <param name="userID">user id</param>
        /// <returns>DataTable of moderated forums</returns>
        public static DataTable forum_listallMyModerated([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_listallmymoderated"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        //END ABOT NEW 16.04.04
        /// <summary>
        /// Gets a list of topics in a forum
        /// </summary>
        /// <param name="boardID">boardID</param>
        /// <param name="ForumID">forumID</param>
        /// <returns>DataTable with list of topics from a forum</returns>
        public static DataTable forum_list([NotNull] string connectionString, object boardID, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERFORUM", FbDbType.Boolean)).Value = false; 

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The forum_byuserlist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="isUserForum">
        /// The is user forum.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_byuserlist([NotNull] string connectionString, object boardID, object forumID, object userId, object isUserForum)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_byuserlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERFORUM", FbDbType.Boolean)).Value = isUserForum;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Listes all forums accessible to a user
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="userID">ID of user</param>
        /// <returns>DataTable of all accessible forums</returns>
        public static DataTable forum_listall([NotNull] string connectionString, object boardID, object userID)
        {
            return forum_listall(connectionString, boardID, userID, 0, false);
        }

        /// <summary>
        /// Lists all forums accessible to a user
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="userID">ID of user</param>
        /// <param name="startAt">startAt ID</param>
        /// <returns>DataTable of all accessible forums</returns>
        public static DataTable forum_listall(
            [NotNull] string connectionString, object boardID, object userID, object startAt, bool returnAll)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_listall"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_ROOT", FbDbType.Integer)).Value = startAt ?? 0;
                cmd.Parameters.Add(new FbParameter("@I_RETURNALL", FbDbType.Integer)).Value = returnAll;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The forum list all.
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
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll([NotNull] string connectionString, int boardId, int userId)
        {
            return
                forum_listall(connectionString, boardId, userId, 0, false)
                    .AsEnumerable()
                    .Select(r => new TypedForumListAll(r));
        }

        /// <summary>
        /// The bb code list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="bbcodeID">
        /// The bbcode id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [NotNull]
        public static IEnumerable<TypedBBCode> BBCodeList([NotNull] string connectionString, int boardID, int? bbcodeID)
        {
            return bbcode_list(connectionString, boardID, bbcodeID).AsEnumerable().Select(o => new TypedBBCode(o));
        }

        /// <summary>
        /// The forum list all.
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
        /// <param name="startForumId">
        /// The start forum id.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<TypedForumListAll> ForumListAll(
            [NotNull] string connectionString, int boardId, int userId, List<int> startForumId)
        {
            var allForums = ForumListAll(connectionString, boardId, userId);
            var forumIds = new List<int>();
            var tempForumIds = new List<int>();
            int addF = 0;
            if (startForumId.Any())
            {
                addF = startForumId.First(f => f > -1);
            }

            forumIds.Add(addF);
            tempForumIds.Add(addF);

            while (true)
            {
                var temp = allForums.Where(f => tempForumIds.Contains(f.ParentID ?? 0));
                if (!temp.Any())
                {
                    break;
                }

                // replace temp forum ids with these...
                tempForumIds = temp.Select(f => f.ForumID ?? 0).Distinct().ToList();

                // add them...
                forumIds.AddRange(tempForumIds);
            }

            // return filtered forums...
            return allForums.Where(f => forumIds.Contains(f.ForumID ?? 0)).Distinct();
        }

        /// <summary>
        /// Lists forums very simply (for URL rewriting)
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable forum_simplelist([NotNull] string connectionString, int StartID, int Limit)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_simplelist"))
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

                cmd.Parameters.Add(new FbParameter("@I_STARTID", FbDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = Limit;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists all forums within a given subcategory
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="CategoryID">CategoryID</param>
        /// <param name="EmptyFirstRow">EmptyFirstRow</param>
        /// <returns>DataTable with list</returns>
        public static DataTable forum_listall_fromCat(
            [NotNull] string connectionString, object boardID, object categoryID, bool emptyFirstRow, bool allowUserForumsOnly)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_listall_fromCat"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_ALLOWUSERFORUMSONLY", FbDbType.Boolean)).Value = allowUserForumsOnly;
   
                using (var dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    return forum_sort_list(connectionString, dt, 0, Convert.ToInt32(categoryID.ToString()), 0, null, emptyFirstRow);
                }
            }
        }

        /// <summary>
        /// The forum_listpath.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listpath([NotNull] string connectionString, object forumID)
        {
            if (!Config.LargeForumTree)
            {
                using (var cmd = FbDbAccess.GetCommand("forum_listpath"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                    return FbDbAccess.GetData(cmd, connectionString);
                }
            }

            using (var cmd = FbDbAccess.GetCommand("forum_ns_listpath"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                return FbDbAccess.GetData(cmd, connectionString);
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
        /// The forum Created By User Id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable forum_listread(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [NotNull] object userId,
            [NotNull] object categoryId,
            [NotNull] object parentId,
            [NotNull] object useStyledNicks,
            [NotNull] object findLastRead, 
            [NotNull] bool showCommonForums, 
            [NotNull] bool showPersonalForums, 
            [CanBeNull] int? forumCreatedByUserId)
        {
            if (!Config.LargeForumTree)
            {
                using (var cmd = FbDbAccess.GetCommand("FORUM_LISTREAD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                    cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                    cmd.Parameters.Add("@I_CATEGORYID", FbDbType.Integer).Value = categoryId ?? DBNull.Value;
                    cmd.Parameters.Add("@I_PARENTID", FbDbType.Integer).Value = parentId ?? DBNull.Value;
                    cmd.Parameters.Add("@I_STYLEDNICKS", FbDbType.Integer).Value = useStyledNicks;
                    cmd.Parameters.Add("@I_FINDLASTREAD", FbDbType.Boolean).Value = findLastRead;
                    cmd.Parameters.Add("@I_SHOWCOMMONFORUMS", FbDbType.Boolean).Value = showCommonForums;
                    cmd.Parameters.Add("@I_SHOWPERSONALFORUMS", FbDbType.Boolean).Value = showPersonalForums;
                    cmd.Parameters.Add("@I_FORUMCREATEDBYUSERID", FbDbType.Integer).Value = forumCreatedByUserId;
                    cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                    return FbDbAccess.GetData(cmd, false, connectionString);
                }
            }

            using (var cmd = FbDbAccess.GetCommand("FORUM_NS_LISTREAD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                cmd.Parameters.Add("@I_CATEGORYID", FbDbType.Integer).Value = categoryId;
                cmd.Parameters.Add("@I_PARENTID", FbDbType.Integer).Value = parentId;
                cmd.Parameters.Add("@I_STYLEDNICKS", FbDbType.Integer).Value = useStyledNicks;
                cmd.Parameters.Add("@I_FINDLASTREAD", FbDbType.Boolean).Value = findLastRead;
                cmd.Parameters.Add("@I_SHOWCOMMONFORUMS", FbDbType.Boolean).Value = showCommonForums;
                cmd.Parameters.Add("@I_SHOWPERSONALFORUMS", FbDbType.Boolean).Value = showPersonalForums;
                cmd.Parameters.Add("@I_FORUMCREATEDBYUSERID", FbDbType.Integer).Value = forumCreatedByUserId;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, false, connectionString);
            }
        }

        public static DataTable forum_listreadpersonal(
          [NotNull] string connectionString,
          [NotNull] object boardId,
          [NotNull] object userId,
          [NotNull] object categoryId,
          [NotNull] object parentId,
          [NotNull] object useStyledNicks,
          [NotNull] object findLastRead,
          [NotNull] bool showCommonForums,
          [NotNull] bool showPersonalForums,
          [CanBeNull] int? forumCreatedByUserId)
        {
            if (!Config.LargeForumTree)
            {
                using (var cmd = FbDbAccess.GetCommand("FORUM_LISTREADPERSONAL"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                    cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                    cmd.Parameters.Add("@I_CATEGORYID", FbDbType.Integer).Value = categoryId ?? DBNull.Value;
                    cmd.Parameters.Add("@I_PARENTID", FbDbType.Integer).Value = parentId ?? DBNull.Value;
                    cmd.Parameters.Add("@I_STYLEDNICKS", FbDbType.Integer).Value = useStyledNicks;
                    cmd.Parameters.Add("@I_FINDLASTREAD", FbDbType.Boolean).Value = findLastRead;
                    cmd.Parameters.Add("@I_SHOWCOMMONFORUMS", FbDbType.Boolean).Value = showCommonForums;
                    cmd.Parameters.Add("@I_SHOWPERSONALFORUMS", FbDbType.Boolean).Value = showPersonalForums;
                    cmd.Parameters.Add("@I_FORUMCREATEDBYUSERID", FbDbType.Integer).Value = forumCreatedByUserId;
                    cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                    return FbDbAccess.GetData(cmd, false, connectionString);
                }
            }

            using (var cmd = FbDbAccess.GetCommand("FORUM_NS_LISTREADPERSONAL"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                cmd.Parameters.Add("@I_CATEGORYID", FbDbType.Integer).Value = categoryId;
                cmd.Parameters.Add("@I_PARENTID", FbDbType.Integer).Value = parentId;
                cmd.Parameters.Add("@I_STYLEDNICKS", FbDbType.Integer).Value = useStyledNicks;
                cmd.Parameters.Add("@I_FINDLASTREAD", FbDbType.Boolean).Value = findLastRead;
                cmd.Parameters.Add("@I_SHOWCOMMONFORUMS", FbDbType.Boolean).Value = showCommonForums;
                cmd.Parameters.Add("@I_SHOWPERSONALFORUMS", FbDbType.Boolean).Value = showPersonalForums;
                cmd.Parameters.Add("@I_FORUMCREATEDBYUSERID", FbDbType.Integer).Value = forumCreatedByUserId;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, false, connectionString);
            }
        }

        /// <summary>
        /// Return admin view of Categories with Forums/Subforums ordered accordingly.
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="userID">UserID</param>
        /// <returns>DataSet with categories</returns>
        public static DataSet forum_moderatelist([NotNull] string connectionString, object userID, object boardID)
        {
            using (var connMan = new FbDbConnectionManager(connectionString))
            {
                using (var ds = new DataSet())
                {
                    using (
                        var da = new FbDataAdapter(
                           FbDbAccess.GetObjectName("category_list"), connMan.OpenDBConnection(connectionString)))
                    {
                        using (
                            var trans = da.SelectCommand.Connection.BeginTransaction(
                                FbDbAccess.IsolationLevel))
                        {
                            da.SelectCommand.Transaction = trans;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = DBNull.Value;

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;


                            da.Fill(ds, FbDbAccess.GetObjectName("Category"));
                            da.SelectCommand.CommandText = FbDbAccess.GetObjectName("forum_moderatelist");
                            da.SelectCommand.Parameters.Clear();
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = boardID;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = userID;
                           
                            da.Fill(ds, FbDbAccess.GetObjectName("ForumUnsorted"));

                            DataTable dtForumListSorted = ds.Tables[FbDbAccess.GetObjectName("ForumUnsorted")].Clone();
                            dtForumListSorted.TableName = FbDbAccess.GetObjectName("Forum");
                            ds.Tables.Add(dtForumListSorted);
                            dtForumListSorted.Dispose();
                            forum_list_sort_basic(
                                connectionString,
                                ds.Tables[FbDbAccess.GetObjectName("ForumUnsorted")],
                                ds.Tables[FbDbAccess.GetObjectName("Forum")],
                                0,
                                0);
                            ds.Tables.Remove(FbDbAccess.GetObjectName("ForumUnsorted"));

                            // vzrus: Remove here all forums with no reports. Would be better to do it in query...
                            // Array to write categories numbers
                            int[] categories = new int[ds.Tables[FbDbAccess.GetObjectName("Forum")].Rows.Count];
                            int cntr = 0;

                            //We should make it before too as the colection was changed
                            ds.Tables[FbDbAccess.GetObjectName("Forum")].AcceptChanges();
                            foreach (DataRow dr in ds.Tables[FbDbAccess.GetObjectName("Forum")].Rows)
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

                            ds.Tables[FbDbAccess.GetObjectName("Forum")].AcceptChanges();

                            foreach (DataRow dr in ds.Tables[FbDbAccess.GetObjectName("Category")].Rows)
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

                            ds.Tables[FbDbAccess.GetObjectName("Category")].AcceptChanges();

                            ds.Relations.Add(
                                "FK_Forum_Category",
                                ds.Tables[FbDbAccess.GetObjectName("Category")].Columns["CategoryID"],
                                ds.Tables[FbDbAccess.GetObjectName("Forum")].Columns["CategoryID"]);
                            trans.Commit();
                        }
                        return ds;
                    }
                }
            }
        }

        public static DataTable forum_moderators([NotNull] string connectionString, object styledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_moderators"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = styledNicks;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("moderators_team_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// Updates topic and post count and last topic for specified forum
        /// </summary>
        /// <param name="boardID">BoardID</param>
        /// <param name="forumID">If null, all forums in board are updated</param>
        public static void forum_resync([NotNull] string connectionString, object boardID, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_resync"))
            {
                if (forumID == null)
                {
                    forumID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer));
                cmd.Parameters[0].Value = boardID;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer));
                cmd.Parameters[1].Value = forumID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static long forum_save(
            [NotNull] string connectionString,
            [NotNull] object forumID,
            [NotNull] object categoryID,
            [NotNull] object parentID,
            [NotNull] object name,
            [NotNull] object description,
            [NotNull] object sortOrder,
            [NotNull] object locked,
            [NotNull] object hidden,
            [NotNull] object isTest,
            [NotNull] object moderated,
            [NotNull] object accessMaskID,
            [NotNull] object remoteURL,
            [NotNull] object themeURL,
            [NotNull] object imageURL,
            [NotNull] object styles,
            [NotNull] bool dummy,
            [NotNull] object userId,
            [NotNull] bool isUserForum,
            bool canhavepersforums)
        {
            using (var cmd = FbDbAccess.GetCommand("forum_save"))
            {
                if (parentID == null)
                {
                    parentID = DBNull.Value;
                }
                if (remoteURL == null)
                {
                    remoteURL = DBNull.Value;
                }
                if (themeURL == null)
                {
                    themeURL = DBNull.Value;
                }
                if (imageURL == null)
                {
                    imageURL = DBNull.Value;
                }
                if (styles == null)
                {
                    styles = DBNull.Value;
                }
                if (accessMaskID == null)
                {
                    accessMaskID = DBNull.Value;
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

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryID;
                cmd.Parameters.Add(new FbParameter("@I_PARENTID", FbDbType.Integer)).Value = parentID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrderOut;
                cmd.Parameters.Add(new FbParameter("@I_LOCKED", FbDbType.Boolean)).Value = locked;
                cmd.Parameters.Add(new FbParameter("@I_HIDDEN", FbDbType.Boolean)).Value = hidden;
                cmd.Parameters.Add(new FbParameter("@I_ISTEST", FbDbType.Boolean)).Value = isTest;
                cmd.Parameters.Add(new FbParameter("@I_MODERATED", FbDbType.Boolean)).Value = moderated;
                cmd.Parameters.Add(new FbParameter("@I_REMOTEURL", FbDbType.VarChar)).Value = remoteURL;
                cmd.Parameters.Add(new FbParameter("@I_THEMEURL", FbDbType.VarChar)).Value = themeURL;
                cmd.Parameters.Add(new FbParameter("@I_IMAGEURL", FbDbType.VarChar)).Value = imageURL;
                cmd.Parameters.Add(new FbParameter("@I_STYLES", FbDbType.VarChar)).Value = styles;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;
                cmd.Parameters.Add("@I_ISUSERFORUM", FbDbType.Boolean).Value = isUserForum;
                cmd.Parameters.Add("@I_CANHAVEPERSFORUMS", FbDbType.Boolean).Value = canhavepersforums;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                String resultop = FbDbAccess.ExecuteScalar(cmd, connectionString).ToString();
                if (String.IsNullOrEmpty(resultop))
                {
                    return 0;
                }
                else
                {
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
            using (var cmd = FbDbAccess.GetCommand("forum_save_parentschecker"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_PARENTID", FbDbType.Integer)).Value = parentID;
                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
            }

        }


        private static DataTable forum_sort_list(
            [NotNull] string connectionString,
            DataTable listSource,
            int parentID,
            int categoryID,
            int startingIndent,
            int[] forumidExclusions)
        {
            return forum_sort_list(
                connectionString, listSource, parentID, categoryID, startingIndent, forumidExclusions, true);
        }

        private static DataTable forum_sort_list(
            [NotNull] string connectionString,
            DataTable listSource,
            int parentID,
            int categoryID,
            int startingIndent,
            int[] forumidExclusions,
            bool emptyFirstRow)
        {
            DataTable listDestination = new DataTable();

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
            // filter the forum list
            DataView dv = listSource.DefaultView;

            if (forumidExclusions != null && forumidExclusions.Length > 0)
            {
                string strExclusions = string.Empty;
                bool bFirst = true;

                foreach (int forumID in forumidExclusions)
                {
                    if (bFirst) bFirst = false;
                    else strExclusions += ",";

                    strExclusions += forumID.ToString();
                }

                dv.RowFilter = string.Format("ForumID NOT IN ({0})", strExclusions);
                dv.ApplyDefaultSort = true;
            }

            forum_sort_list_recursive(
                connectionString, dv.ToTable(), listDestination, parentID, categoryID, startingIndent);

            return listDestination;
        }


        public static DataTable forum_listall_sorted(
            [NotNull] string connectionString,
            object boardID,
            object userID,
            int[] forumidExclusions,
            bool emptyFirstRow,
            List<int> startAt)
        {
            using (DataTable dataTable = forum_listall(connectionString, boardID, userID, startAt, false))
            {
                int baseForumId = 0;
                int baseCategoryId = 0;

                if (startAt.Any())
                {
                    // find the base ids...
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (Convert.ToInt32(dataRow["ForumID"]) == startAt.First(f => f > -1)
                            && dataRow["ParentID"] != DBNull.Value && dataRow["CategoryID"] != DBNull.Value)
                        {
                            baseForumId = Convert.ToInt32(dataRow["ParentID"]);
                            baseCategoryId = Convert.ToInt32(dataRow["CategoryID"]);
                            break;
                        }
                    }
                }

                return forum_sort_list(
                    connectionString, dataTable, baseForumId, baseCategoryId, 0, forumidExclusions, emptyFirstRow);
            }
        }

        private static void forum_list_sort_basic(
            [NotNull] string connectionString, DataTable listsource, DataTable list, int parentid, int currentLvl)
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
                    forum_list_sort_basic(connectionString, listsource, list, (int)row["ForumID"], currentLvl + 1);
                }
            }
        }

        private static void forum_sort_list_recursive(
            [NotNull] string connectionString,
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int categoryID,
            int currentIndent)
        {
            DataRow newRow;

            foreach (DataRow row in listSource.Rows)
            {
                // see if this is a root-forum
                if (row["ParentID"] == DBNull.Value) row["ParentID"] = 0;

                if ((int)row["ParentID"] == parentID)
                {
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

                    // recurse through the list...
                    forum_sort_list_recursive(
                        connectionString,
                        listSource,
                        listDestination,
                        (int)row["ForumID"],
                        categoryID,
                        currentIndent + 1);
                }
            }
        }



        #endregion

        #region yaf_ForumAccess

        public static DataTable forumaccess_list([NotNull] string connectionString, object forumID, object userId, bool includeUserGroups)
        {
            using (var cmd = FbDbAccess.GetCommand("forumaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_INCLUDEUSERGROUPS", FbDbType.Boolean)).Value = includeUserGroups;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void forumaccess_save(
            [NotNull] string connectionString, object forumID, object groupID, object accessMaskID)
        {
            using (var cmd = FbDbAccess.GetCommand("forumaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable forumaccess_group([NotNull] string connectionString, object groupID, object userId, bool includeUserForums)
        {
            using (var cmd = FbDbAccess.GetCommand("forumaccess_group"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_INCLUDEUSERFORUMS", FbDbType.Boolean)).Value = includeUserForums;

                return userforumaccess_sort_list(connectionString, FbDbAccess.GetData(cmd, connectionString), 0, 0, 0);
            }
        }

        public static DataTable forumaccess_personalgroup([NotNull] string connectionString, object groupID, object userId, bool includeUserForums)
        {
            using (var cmd = FbDbAccess.GetCommand("forumaccess_personalgroup"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_INCLUDEUSERFORUMS", FbDbType.Boolean)).Value = includeUserForums;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Group

        public static DataTable group_list([NotNull] string connectionString, [NotNull] object boardID, [NotNull] object groupID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable group_byuserlist([NotNull] string connectionString, [NotNull] object boardID, [NotNull] object groupID, object userId, object isUserGroup)
        {
            using (var cmd = FbDbAccess.GetCommand("group_byuserlist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_ISUSERGROUP", FbDbType.Integer)).Value = isUserGroup;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void group_delete([NotNull] string connectionString, object groupID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable group_member([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_member"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static long group_save(
            [NotNull] string connectionString,
            [NotNull] object groupID,
            [NotNull] object boardID,
            [NotNull] object name,
            [NotNull] object isAdmin,
            [NotNull] object isGuest,
            [NotNull] object isStart,
            [NotNull] object isModerator, 
            [NotNull] object isHidden,
            [NotNull] object accessMaskID,
            [NotNull] object pmLimit,
            [NotNull] object style,
            [NotNull] object sortOrder,
            [NotNull] object description,
            [NotNull] object usrSigChars,
            [NotNull] object usrSigBBCodes,
            [NotNull] object usrSigHTMLTags,
            [NotNull] object usrAlbums,
            [NotNull] object usrAlbumImages,
            [CanBeNull] object userId,
            [NotNull] object isUserGroup,
            object personalForumsNumber,
            object personalAccessMasksNumber,
            object personalGroupsNumber)
        {
            using (var cmd = FbDbAccess.GetCommand("group_save"))
            {
                if (accessMaskID == null)
                {
                    accessMaskID = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_ISADMIN", FbDbType.Boolean)).Value = isAdmin;
                cmd.Parameters.Add(new FbParameter("@I_ISGUEST", FbDbType.Boolean)).Value = isGuest;
                cmd.Parameters.Add(new FbParameter("@I_ISSTART", FbDbType.Boolean)).Value = isStart;
                cmd.Parameters.Add(new FbParameter("@I_ISMODERATOR", FbDbType.Boolean)).Value = isModerator;
                cmd.Parameters.Add(new FbParameter("@I_ISHIDDEN", FbDbType.Boolean)).Value = isHidden;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskID;
                cmd.Parameters.Add(new FbParameter("@I_PMLIMIT", FbDbType.Integer)).Value = pmLimit;
                cmd.Parameters.Add(new FbParameter("@I_STYLE", FbDbType.VarChar)).Value = style;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.Integer)).Value = sortOrder;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGCHARS", FbDbType.Integer)).Value = usrSigChars;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGBBCODES", FbDbType.VarChar)).Value = usrSigBBCodes;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGSHTMLTAGS", FbDbType.VarChar)).Value = usrSigHTMLTags;
                cmd.Parameters.Add(new FbParameter("@I_USRALBUMS", FbDbType.Integer)).Value = usrAlbums;
                cmd.Parameters.Add(new FbParameter("@I_USRALBUMIMAGES", FbDbType.Integer)).Value = usrAlbumImages;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add("@I_ISUSERGROUP", FbDbType.Boolean).Value = isUserGroup;
                cmd.Parameters.Add("@I_PERSONALACCESSMASKSNUMBER", FbDbType.Integer).Value = personalAccessMasksNumber;
                cmd.Parameters.Add("@I_PERSONALGROUPSNUMBER", FbDbType.Integer).Value = personalGroupsNumber;
                cmd.Parameters.Add("@I_PERSONALFORUMSNUMBER", FbDbType.Integer).Value = personalForumsNumber;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                return long.Parse(FbDbAccess.ExecuteScalar(cmd, connectionString).ToString());
            }
        }

        #endregion

        #region yaf_Mail

        public static void mail_delete([NotNull] string connectionString, object mailID)
        {
            using (var cmd = FbDbAccess.GetCommand("mail_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MAILID", FbDbType.Integer)).Value = mailID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("MAIL_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_PROCESSID", FbDbType.Integer)).Value = processId;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString).SelectTypedList(x => new TypedMailList(x));
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
            object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("mail_createwatch"))
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

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("@I_FROM", FbDbType.VarChar)).Value = from;
                cmd.Parameters.Add(new FbParameter("@I_FROMNAME", FbDbType.VarChar)).Value = fromName;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = subject;
                cmd.Parameters.Add(new FbParameter("@I_BODY", FbDbType.Text)).Value = body;
                cmd.Parameters.Add(new FbParameter("@I_BODYHTML", FbDbType.Text)).Value = bodyHtml;
                cmd.Parameters.Add(new FbParameter("@I_iserid", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void mail_create(
            [NotNull] string connectionString,
            [NotNull] object from,
            [NotNull] object fromName,
            [NotNull] object to,
            [NotNull] object toName,
            [NotNull] object subject,
            [NotNull] object body,
            [NotNull] object bodyHtml)
        {

            using (var cmd = FbDbAccess.GetCommand("mail_create"))
            {
                //if (fromName == null) { fromName = DBNull.Value; }
                // if (toName == null) { toName = DBNull.Value; }
                // if (bodyHtml == null) { bodyHtml = DBNull.Value; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FROM", FbDbType.VarChar)).Value = from;
                cmd.Parameters.Add(new FbParameter("@I_FROMNAME", FbDbType.VarChar)).Value = fromName;
                cmd.Parameters.Add(new FbParameter("@I_TO", FbDbType.VarChar)).Value = to;
                cmd.Parameters.Add(new FbParameter("@I_TONAME", FbDbType.VarChar)).Value = toName;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = subject;
                cmd.Parameters.Add(new FbParameter("@I_BODY", FbDbType.Text)).Value = body;
                cmd.Parameters.Add(new FbParameter("@I_BODYHTML", FbDbType.Text)).Value = bodyHtml;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Message


        public static DataTable post_list(
            [NotNull] string connectionString,
            [NotNull] object topicId,
            object currentUserID,
            [NotNull] object authorUserID,
            [NotNull] object updateViewCount,
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
            int messagePosition)
        {
            using (var cmd = FbDbAccess.GetCommand("POST_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = currentUserID;
                cmd.Parameters.Add(new FbParameter("@I_AUTHORUSERID", FbDbType.Integer)).Value = authorUserID;
                cmd.Parameters.Add(new FbParameter("@I_UPDATEVIEWCOUNT", FbDbType.SmallInt)).Value = updateViewCount
                                                                                                     ?? 0;
                cmd.Parameters.Add(new FbParameter("@I_SHOWDELETED", FbDbType.Boolean)).Value = showDeleted;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add(new FbParameter("@I_SHOWREPUTATION", FbDbType.Boolean)).Value = showReputation;
                cmd.Parameters.Add(new FbParameter("@I_SINCEPOSTEDDATE", FbDbType.TimeStamp)).Value = sincePostedDate;
                cmd.Parameters.Add(new FbParameter("@I_TOPOSTEDDATE", FbDbType.TimeStamp)).Value = toPostedDate;
                cmd.Parameters.Add(new FbParameter("@I_SINCEEDITEDDATE", FbDbType.TimeStamp)).Value = sinceEditedDate;
                cmd.Parameters.Add(new FbParameter("@I_TOEDITEDDATE", FbDbType.TimeStamp)).Value = toEditedDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_SORTPOSTED", FbDbType.Integer)).Value = sortPosted;
                cmd.Parameters.Add(new FbParameter("@I_SORTEDITED", FbDbType.Integer)).Value = sortEdited;
                cmd.Parameters.Add(new FbParameter("@I_SORTPOSITION", FbDbType.Integer)).Value = sortPosition;
                cmd.Parameters.Add(new FbParameter("@I_SHOWTHANKS", FbDbType.Boolean)).Value = showThanks;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEPOSITION", FbDbType.Integer)).Value = messagePosition;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);

            }
        }

        public static DataTable post_list_reverse10([NotNull] string connectionString, object topicID)
        {
            using (var cmd = FbDbAccess.GetCommand("post_list_reverse10"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        [Obsolete("Use post_alluser() instead.")]
        public static DataTable post_last10user(
            [NotNull] string connectionString, object boardID, object userID, object pageUserID)
        {
            // use all posts procedure to return top ten
            return post_alluser(connectionString, boardID, userID, pageUserID, 10);
        }

        public static DataTable post_alluser(
            [NotNull] string connectionString, object boardID, object userID, object pageUserID, object topCount)
        {
            using (var cmd = FbDbAccess.GetCommand("post_alluser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserID;
                cmd.Parameters.Add(new FbParameter("@I_TOPCOUNT", FbDbType.Integer)).Value = topCount;

                return FbDbAccess.GetData(cmd, connectionString);
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

            using (var cmd = FbDbAccess.GetCommand("message_reply_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                DataTable dtr = FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("message_reply_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer));
                cmd.Parameters[0].Value = messageID;


                DataTable dtr = FbDbAccess.GetData(cmd, connectionString);

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
            }

        }

        //creates new topic, using some parameters from message itself
        public static long topic_create_by_message(
            [NotNull] string connectionString, object messageId, object forumId, object newTopicSubj)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_create_by_message"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = newTopicSubj;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
                //return long.Parse(dt.Rows[0]["TopicID"].ToString());
            }
        }

        [Obsolete("Use MessageList(int messageId) instead")]
        public static DataTable message_list([NotNull] string connectionString, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("message_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(t => new TypedMessageList(t));
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
            using (var cmd = FbDbAccess.GetCommand("message_listreported"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("MESSAGE_LISTREPORTERS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        // <summary> Save reported message back to the database. </summary>
        public static void message_report(
            [NotNull] string connectionString,
            [NotNull] object messageID,
            [NotNull] object userID,
            [NotNull] object reportedDateTime,
            [NotNull] object reportText)
        {
            using (var cmd = FbDbAccess.GetCommand("message_report"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_REPORTERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_REPORTEDDATE", FbDbType.TimeStamp)).Value = reportedDateTime;
                cmd.Parameters.Add(new FbParameter("@I_REPORTTEXT", FbDbType.VarChar)).Value = reportText;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Copy current Message text over reported Message text. </summary>
        public static void message_reportcopyover([NotNull] string connectionString, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_reportcopyover"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Copy current Message text over reported Message text. </summary>
        public static void message_reportresolve(
            [NotNull] string connectionString, [NotNull] object messageFlag, [NotNull] object messageID, [NotNull] object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_reportresolve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEFLAG", FbDbType.Integer)).Value = messageFlag;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
                using (var cmd = FbDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer));
                    cmd.Parameters[0].Value = messageID;


                    DataTable tbReplies = FbDbAccess.GetData(cmd, connectionString);

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

            //If the files are actually saved in the Hard Drive
            if (!UseFileTable)
            {
                using (var cmd = FbDbAccess.GetCommand("attachment_list"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                    cmd.Parameters.Add(new FbParameter("@I_ATTACHMENTID", FbDbType.Integer)).Value = null;
                    cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = null;
                    cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = 0;
                    cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = 1000000;

                    DataTable tbAttachments = FbDbAccess.GetData(cmd, connectionString);
                    string uploadDir =
                        HostingEnvironment.MapPath(
                            String.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.Uploads));



                    foreach (DataRow row in tbAttachments.Rows)
                    {
                        try
                        {
                            string fileName = String.Format("{0}/{1}.{2}", uploadDir, messageID, row["FileName"]);
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        catch
                        {
                            // error deleting that file... 
                        }
                    }

                }
            }

            // Ederon : erase message for good
            if (eraseMessages)
            {
                using (var cmd = FbDbAccess.GetCommand("message_delete"))
                {
                    int eraseMessagesInt = 0;
                    if (eraseMessages == true)
                    {
                        eraseMessagesInt = 1;
                    }
                    else
                    {
                        eraseMessagesInt = 0;
                    }

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer));
                    cmd.Parameters[0].Value = messageID;

                    cmd.Parameters.Add(new FbParameter("@I_ERASEMESSAGE", FbDbType.Boolean));
                    cmd.Parameters[1].Value = eraseMessagesInt;


                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            else
            {
                //Delete Message
                // undelete function added
                using (var cmd = FbDbAccess.GetCommand("message_deleteundelete"))
                {
                    int isModeratorChangedInt = 0;
                    if (isModeratorChanged == true)
                    {
                        isModeratorChangedInt = 1;
                    }
                    else
                    {
                        isModeratorChangedInt = 0;
                    }


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer));
                    cmd.Parameters[0].Value = messageID;

                    cmd.Parameters.Add(new FbParameter("@I_ISMODERATORCHANGED", FbDbType.Boolean));
                    cmd.Parameters[1].Value = isModeratorChangedInt;

                    cmd.Parameters.Add(new FbParameter("@I_DELETEREASON", FbDbType.VarChar));
                    cmd.Parameters[2].Value = deleteReason;

                    cmd.Parameters.Add(new FbParameter("@I_ISDELETEACTION", FbDbType.Integer));
                    cmd.Parameters[3].Value = isDeleteAction;

                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
        }

        // <summary> Set flag on message to approved and store in DB </summary>
        public static void message_approve([NotNull] string connectionString, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_approve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("message_simplelist"))
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

                cmd.Parameters.Add(new FbParameter("@I_STARTID", FbDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = Limit;


                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        public static void message_update(
            [NotNull] string connectionString,
            [NotNull] object messageID,
            [NotNull] object priority,
            [NotNull] object message,
            [NotNull] object description,
            [NotNull] object status,
            [NotNull] object styles,
            [NotNull] object subject,
            [NotNull] object flags,
            [NotNull] object reasonOfEdit,
            [NotNull] object isModeratorChanged,
            [NotNull] object overrideApproval,
            [NotNull] object originalMessage,
            [NotNull] object editedBy,
            string tags)
        {
            using (var cmd = FbDbAccess.GetCommand("message_update"))
            {
                if (overrideApproval == null)
                {
                    overrideApproval = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_PRIORITY", FbDbType.Integer)).Value = priority;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = subject;
                cmd.Parameters.Add(new FbParameter("@I_STATUS", FbDbType.VarChar)).Value = status;
                cmd.Parameters.Add(new FbParameter("@I_STYLES", FbDbType.VarChar)).Value = styles;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_REASON", FbDbType.VarChar)).Value = reasonOfEdit;
                cmd.Parameters.Add(new FbParameter("@I_EDITEDBY", FbDbType.VarChar)).Value = editedBy;
                cmd.Parameters.Add(new FbParameter("@I_ISMODERATORCHANGED", FbDbType.Boolean)).Value =
                    isModeratorChanged;
                cmd.Parameters.Add(new FbParameter("@I_OVERRIDEAPPROVAL", FbDbType.Boolean)).Value = overrideApproval;
                cmd.Parameters.Add(new FbParameter("@I_ORIGINALMESSAGE", FbDbType.Text)).Value = originalMessage;
                cmd.Parameters.Add(new FbParameter("@I_TAGS", FbDbType.VarChar)).Value = tags;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        // <summary> Save message to DB. </summary>
        public static bool message_save(
            [NotNull] string connectionString,
            [NotNull] object topicID,
            [NotNull] object userID,
            [NotNull] object message,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object replyTo,
            [NotNull] object flags,
            ref long messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_save"))
            {
                if (userName == null)
                {
                    userName = DBNull.Value;
                }
                if (posted == null)
                {
                    posted = DBNull.Value;
                }

                object externalMesageId = null;
                object referenceMesageId = null;
                // FbParameter paramMessageID = new FbParameter("@I_MESSAGEID", messageID);
                //  paramMessageID.Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip;
                cmd.Parameters.Add(new FbParameter("@I_POSTED", FbDbType.TimeStamp)).Value = posted;
                cmd.Parameters.Add(new FbParameter("@I_REPLYTO", FbDbType.Integer)).Value = replyTo;
                cmd.Parameters.Add(new FbParameter("@I_BLOGPOSTID", FbDbType.VarChar)).Value = DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_EXTERNALMESSAGEID", FbDbType.VarChar)).Value = externalMesageId
                                                                                                      ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_REFERENCEMESSAGEID", FbDbType.VarChar)).Value = referenceMesageId
                                                                                                       ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                // cmd.Parameters.Add(paramMessageID);
                messageID = Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
                // messageID = Convert.ToInt64(paramMessageID.Value);
                return true;
            }
        }

        public static DataTable message_unapproved([NotNull] string connectionString, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_unapproved"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable message_findunread(
            [NotNull] string connectionString,
            [NotNull] object topicID,
            [NotNull] object messageId,
            [NotNull] object lastRead,
            [NotNull] object showDeleted,
            [NotNull] object authorUserID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_findunread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new FbParameter("@I_LASTREAD", FbDbType.TimeStamp)).Value = lastRead;
                cmd.Parameters.Add(new FbParameter("@I_SHOWDELETED", FbDbType.Boolean)).Value = showDeleted;
                cmd.Parameters.Add(new FbParameter("@I_AUTHORUSERID", FbDbType.Integer)).Value = authorUserID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        // message movind function
        public static void message_move(
            [NotNull] string connectionString, [NotNull] object messageID, [NotNull] object moveToTopic, bool moveAll)
        {
            using (var cmd = FbDbAccess.GetCommand("message_move"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                cmd.Parameters.Add(new FbParameter("@I_MOVETOTOPIC", FbDbType.Integer)).Value = moveToTopic;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
            //moveAll=true anyway
            // it's in charge of moving answers of moved post
            if (moveAll)
            {
                using (var cmd = FbDbAccess.GetCommand("message_getReplies"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;

                    DataTable tbReplies = FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("message_getReplies"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer));
                cmd.Parameters[0].Value = messageID;

                DataTable tbReplies = FbDbAccess.GetData(cmd, connectionString);
                foreach (DataRow row in tbReplies.Rows)
                {
                    message_moveRecursively(connectionString, row["messageID"], moveToTopic);
                }
                using (FbCommand innercmd = FbDbAccess.GetCommand("message_move"))
                {
                    innercmd.CommandType = CommandType.StoredProcedure;

                    innercmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageID;
                    innercmd.Parameters.Add(new FbParameter("@I_MOVETOTOPIC", FbDbType.Integer)).Value = moveToTopic;

                    FbDbAccess.ExecuteNonQuery(innercmd, connectionString);
                }
            }
        }

        // functions for Thanks feature
        //TODO: to delete
        // <summary> Checks if the message with the provided messageID is thanked 
        //           by the user with the provided UserID. if so, returns true,
        //           otherwise returns false. </summary>
        public static bool message_isThankedByUser([NotNull] string connectionString, object userID, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_isthankedbyuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                FbParameter paramOutput = new FbParameter();
                paramOutput.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.AddWithValue("I_USERID", userID);
                cmd.Parameters.AddWithValue("I_MESSAGEID", messageID);
                cmd.Parameters.Add(paramOutput);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return Convert.ToBoolean(paramOutput.Value);
            }
        }

        // <summary> Return the number of times the message with the provided messageID
        //           has been thanked. </summary>
        public static int message_ThanksNumber([NotNull] string connectionString, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("message_thanksnumber"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                FbParameter paramOutput = new FbParameter();
                paramOutput.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("I_MESSAGEID", FbDbType.Integer).Value = messageID;
                cmd.Parameters.Add(paramOutput);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return Convert.ToInt32(paramOutput.Value);
            }
        }

        // <summary> Returns the UserIDs and UserNames who have thanked the message
        //           with the provided messageID. </summary>
        public static DataTable message_GetThanks([NotNull] string connectionString, object messageID)
        {
            using (var cmd = FbDbAccess.GetCommand("MESSAGE_GETTHANKS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_MESSAGEID", FbDbType.Integer).Value = messageID;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retuns All the message text for the Message IDs which are in the 
        /// delimited string variable MessageIDs
        /// </summary>
        /// <param name="messageIDs">
        /// The message i ds.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable message_GetTextByIds([NotNull] string connectionString, string messageIDs)
        {
            using (var cmd = FbDbAccess.GetCommand("message_gettextbyids"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_MESSAGEIDS", FbDbType.VarChar).Value = messageIDs;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary> Returns All the Thanks for the Message IDs which are in the 
        /// delimited string variable MessageIDs 
        ///</summary>
        [Obsolete("Use MessageGetAllThanks(string messageIdsSeparatedWithColon) instead")]
        public static DataTable message_GetAllThanks([NotNull] string connectionString, object MessageIDs)
        {
            using (var cmd = FbDbAccess.GetCommand("message_getallthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_MESSAGEIDS", FbDbType.VarChar).Value = MessageIDs;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("message_getallthanks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_MESSAGEIDS", FbDbType.VarChar).Value = messageIdsSeparatedWithColon;

                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(t => new TypedAllThanks(t));
            }
        }

        public static string message_AddThanks(
            [NotNull] string connectionString, object FromUserID, object MessageID, bool useDisplayName)
        {
            using (var cmd = FbDbAccess.GetCommand("MESSAGE_ADDTHANKS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                cmd.Parameters.Add("I_MESSAGEID", FbDbType.Integer).Value = MessageID;
                cmd.Parameters.Add("I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;
                cmd.Parameters.Add("I_USEDISPLAYNAME", FbDbType.Boolean).Value = useDisplayName;
                return FbDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        public static string message_RemoveThanks(
            [NotNull] string connectionString, object FromUserID, object MessageID, bool useDisplayName)
        {
            using (var cmd = FbDbAccess.GetCommand("MESSAGE_REMOVETHANKS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new FbParameter("OUT_RESULT", FbDbType.VarChar, 128)
                                      {
                                          Direction =
                                              ParameterDirection.Output
                                      };
                cmd.Parameters.Add("I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                cmd.Parameters.Add("I_MESSAGEID", FbDbType.Integer).Value = MessageID;
                cmd.Parameters.Add("I_USEDISPLAYNAME", FbDbType.Boolean).Value = useDisplayName;
                cmd.Parameters.Add(paramOutput);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return (paramOutput.Value.ToString());
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
        /// <returns>
        /// List of all message changes. 
        /// </returns>
        public static DataTable messagehistory_list([NotNull] string connectionString, int messageID, int daysToClean)
        {
            using (var cmd = FbDbAccess.GetCommand("MESSAGEHISTORY_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("I_MESSAGEID", FbDbType.Integer).Value = messageID;
                cmd.Parameters.Add("I_DAYSTOCLEAN", FbDbType.Integer).Value = daysToClean;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns message data based on user access rights
        /// </summary>
        /// <param name="MessageID">The Message Id.</param>
        /// <param name="UserID">The UserId.</param>
        /// <returns></returns>
        public static DataTable message_secdata([NotNull] string connectionString, int messageID, object pageUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("message_secdata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_MESSAGEID", FbDbType.Integer).Value = messageID;
                cmd.Parameters.Add("@I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;

                return FbDbAccess.GetData(cmd, connectionString);

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

        /// <summary>
        /// Lists given medals.
        /// </summary>
        /// <param name="boardID">ID of board of which medals to list. Required.</param>
        /// <param name="category">Cateogry of medals to list. Can be null. In such case this parameter is ignored.</param>
        public static DataTable medal_list([NotNull] string connectionString, object boardID, object category)
        {
            return medal_list(connectionString, boardID, null, category);
        }

        /// <summary>
        /// Lists medals.
        /// </summary>
        /// <param name="boardID">ID of board of which medals to list. Can be null if medalID parameter is specified.</param>
        /// <param name="medalID">ID of medal to list. When specified, boardID and category parameters are ignored.</param>
        /// <param name="category">Cateogry of medals to list. Must be complemented with not-null boardID parameter.</param>
        public static DataTable medal_list([NotNull] string connectionString, object boardID, object medalID, object category)
        {
            using (var cmd = FbDbAccess.GetCommand("medal_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORY", FbDbType.VarChar)).Value = category ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


        /// <summary>
        /// List users who own this medal.
        /// </summary>
        /// <param name="medalID">Medal of which owners to get.</param>
        /// <returns>List of users with their user id and usernames, who own this medal.</returns>
        public static DataTable medal_listusers([NotNull] string connectionString, object medalID)
        {
            using (var cmd = FbDbAccess.GetCommand("medal_listusers"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes given medal.
        /// </summary>
        /// <param name="medalID">
        /// ID of medal to delete.
        /// </param>
        public static void medal_delete([NotNull] string connectionString, [NotNull] object medalID)
        {
            medal_delete(connectionString, null, medalID, null);
        }

        /// <summary>
        /// Deletes medals.
        /// </summary>
        /// <param name="boardID">ID of board of which medals to delete. Can be null if medalID parameter is specified.</param>
        /// <param name="medalID">ID of medal to delete. When specified, boardID and category parameters are ignored.</param>
        /// <param name="category">Cateogry of medals to delete. Must be complemented with not-null boardID parameter.</param>
        public static void medal_delete([NotNull] string connectionString, object boardID, object medalID, object category)
        {
            using (var cmd = FbDbAccess.GetCommand("medal_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORY", FbDbType.VarChar)).Value = category ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Saves new medal or updates existing one. 
        /// </summary>
        /// <param name="boardID">ID of a board.</param>
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
            [NotNull] object boardID,
            [NotNull] object medalID,
            [NotNull] object name,
            [NotNull] object description,
            [NotNull] object message,
            [NotNull] object category,
            [NotNull] object medalURL,
            [NotNull] object ribbonURL,
            [NotNull] object smallMedalURL,
            [NotNull] object smallRibbonURL,
            [NotNull] object smallMedalWidth,
            [NotNull] object smallMedalHeight,
            [NotNull] object smallRibbonWidth,
            [NotNull] object smallRibbonHeight,
            [NotNull] object sortOrder,
            [NotNull] object flags)
        {
            using (var cmd = FbDbAccess.GetCommand("medal_save"))
            {
                if (boardID == null)
                {
                    boardID = DBNull.Value;
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

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.Text)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.VarChar)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORY", FbDbType.VarChar)).Value = category;
                cmd.Parameters.Add(new FbParameter("@I_MEDALURL", FbDbType.VarChar)).Value = medalURL;
                cmd.Parameters.Add(new FbParameter("@I_RIBBONURL", FbDbType.VarChar)).Value = ribbonURL;
                cmd.Parameters.Add(new FbParameter("@I_SMALLMEDALURL", FbDbType.VarChar)).Value = smallMedalURL;
                cmd.Parameters.Add(new FbParameter("@I_SMALLRIBBONURL", FbDbType.VarChar)).Value = smallRibbonURL;
                cmd.Parameters.Add(new FbParameter("@I_SMALLMEDALWIDTH", FbDbType.SmallInt)).Value = smallMedalWidth;
                cmd.Parameters.Add(new FbParameter("@I_SMALLMEDALHEIGHT", FbDbType.SmallInt)).Value = smallMedalHeight;
                cmd.Parameters.Add(new FbParameter("@I_SMALLRIBBONWIDTH", FbDbType.SmallInt)).Value = smallRibbonWidth;
                cmd.Parameters.Add(new FbParameter("@I_SMALLRIBBONHEIGHT", FbDbType.SmallInt)).Value = smallRibbonHeight;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrderOut;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags ?? 0;

                // command succeeded if returned value is greater than zero (number of affected rows)
                // bool rres = (FbDbAccess.ExecuteScalar(cmd,connectionString) > 0);
                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString)) > 0;

            }
        }


        /// <summary>
        /// Changes medal's sort order.
        /// </summary>
        /// <param name="boardID">ID of board.</param>
        /// <param name="medalID">ID of medal to re-sort.</param>
        /// <param name="move">Change of sort.</param>
        public static void medal_resort([NotNull] string connectionString, object boardID, object medalID, int move)
        {
            using (var cmd = FbDbAccess.GetCommand("medal_resort"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new FbParameter("@I_MOVE", FbDbType.Integer)).Value = move;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Deletes medal allocation to a group.
        /// </summary>
        /// <param name="groupID">ID of group owning medal.</param>
        /// <param name="medalID">ID of medal.</param>
        public static void group_medal_delete([NotNull] string connectionString, object groupID, object medalID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_medal_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists medal(s) assigned to the group
        /// </summary>
        /// <param name="groupID">ID of group of which to list medals.</param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable group_medal_list(
            [NotNull] string connectionString, [NotNull] object groupID, [NotNull] object medalID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_medal_list"))
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

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;

                return FbDbAccess.GetData(cmd, connectionString);
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
            [NotNull] object groupID,
            [NotNull] object medalID,
            [NotNull] object message,
            [NotNull] object hide,
            [NotNull] object onlyRibbon,
            [NotNull] object sortOrder)
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

            using (var cmd = FbDbAccess.GetCommand("group_medal_save"))
            {
                if (message == null)
                {
                    message = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.VarChar)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_HIDE", FbDbType.Boolean)).Value = hide;
                cmd.Parameters.Add(new FbParameter("@I_ONLYRIBBON", FbDbType.Boolean)).Value = onlyRibbon;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrderOut;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }



        /// <summary>
        /// Deletes medal allocation to a user.
        /// </summary>
        /// <param name="userID">ID of user owning medal.</param>
        /// <param name="medalID">ID of medal.</param>
        public static void user_medal_delete([NotNull] string connectionString, object userID, object medalID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_medal_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID ?? DBNull.Value;


                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists medal(s) assigned to the group
        /// </summary>
        /// <param name="userID">ID of user who was given medal.</param>
        /// <param name="medalID">ID of medal to list.</param>
        public static DataTable user_medal_list([NotNull] string connectionString, object userID, object medalID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_medal_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);

            }
        }


        /// <summary>
        /// Saves new or update existing user-medal allocation.
        /// </summary>
        /// <param name="userID">ID of user.</param>
        /// <param name="medalID">ID of medal.</param>
        /// <param name="message">Medal message, to override medal's default one. Can be null.</param>
        /// <param name="hide">Hide medal in user box.</param>
        /// <param name="onlyRibbon">Show only ribbon bar in user box.</param>
        /// <param name="sortOrder">Sort order in user box. Overrides medal's default sort order.</param>
        /// <param name="dateAwarded">Date when medal was awarded to a user. Is ignored when existing user-medal allocation is edited.</param>
        public static void user_medal_save(
            [NotNull] string connectionString,
            object userID,
            object medalID,
            object message,
            object hide,
            object onlyRibbon,
            object sortOrder,
            object dateAwarded)
        {
            using (var cmd = FbDbAccess.GetCommand("user_medal_save"))
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

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_MEDALID", FbDbType.Integer)).Value = medalID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.VarChar)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_HIDE", FbDbType.Boolean)).Value = hide;
                cmd.Parameters.Add(new FbParameter("@I_ONLYRIBBON", FbDbType.Boolean)).Value = onlyRibbon;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrder;
                cmd.Parameters.Add(new FbParameter("@I_DATEAWARDED", FbDbType.TimeStamp)).Value = dateAwarded;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        /// <summary>
        /// Lists all medals held by user as they are to be shown in user box.
        /// </summary>
        /// <param name="userID">ID of user.</param>
        /// <returns>List of medals, ribbon bar only first.</returns>
        public static DataTable user_listmedals([NotNull] string connectionString, [NotNull] object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_listmedals"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpForum

        public static DataTable nntpforum_list(
            [NotNull] string connectionString, object boardID, object minutes, object nntpForumID, object active)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpforum_list"))
            {
                if (minutes == null)
                {
                    minutes = DBNull.Value;
                }
                if (nntpForumID == null)
                {
                    nntpForumID = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_MINUTES", FbDbType.Integer)).Value = minutes;
                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new FbParameter("@I_ACTIVE", FbDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, false, connectionString);
            }
        }

        public static IEnumerable<TypedNntpForum> NntpForumList(
            [NotNull] string connectionString, int boardID, int? minutes, int? nntpForumID, bool? active)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpforum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_MINUTES", FbDbType.Integer)).Value = minutes;
                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new FbParameter("@I_ACTIVE", FbDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedNntpForum(r));
            }
        }

        public static void nntpforum_update(
            [NotNull] string connectionString,
            [NotNull] object nntpForumID,
            [NotNull] object lastMessageNo,
            [NotNull] object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpforum_update"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new FbParameter("@I_LASTMESSAGENO", FbDbType.Integer)).Value = lastMessageNo;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpforum_save(
            [NotNull] string connectionString,
            [NotNull] object nntpForumID,
            [NotNull] object nntpServerID,
            [NotNull] object groupName,
            [NotNull] object forumID,
            [NotNull] object active,
            [NotNull] object datecutoff)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpforum_save"))
            {
                if (nntpForumID == null)
                {
                    nntpForumID = DBNull.Value;
                }


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new FbParameter("@I_NNTPSERVERID", FbDbType.Integer)).Value = nntpServerID;
                cmd.Parameters.Add(new FbParameter("@I_GROUPNAME", FbDbType.VarChar)).Value = groupName;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_ACTIVE", FbDbType.Boolean)).Value = active;
                cmd.Parameters.Add(new FbParameter("@I_DATECUTOFF", FbDbType.TimeStamp)).Value = datecutoff;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpforum_delete([NotNull] string connectionString, object nntpForumID)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer));
                cmd.Parameters[0].Value = nntpForumID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpServer

        public static DataTable nntpserver_list([NotNull] string connectionString, object boardID, object nntpServerID)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpserver_list"))
            {
                if (boardID == null)
                {
                    boardID = DBNull.Value;
                }
                if (nntpServerID == null)
                {
                    nntpServerID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NNTPSERVERID", FbDbType.Integer)).Value = nntpServerID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void nntpserver_save(
            [NotNull] string connectionString,
            [NotNull] object nntpServerID,
            [NotNull] object boardID,
            [NotNull] object name,
            [NotNull] object address,
            [NotNull] object port,
            [NotNull] object userName,
            [NotNull] object userPass)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpserver_save"))
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

                cmd.Parameters.Add(new FbParameter("@I_NNTPSERVERID", FbDbType.Integer)).Value = nntpServerID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_ADDRESS", FbDbType.VarChar)).Value = address;
                cmd.Parameters.Add(new FbParameter("@I_PORT", FbDbType.Integer)).Value = port;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_USERPASS", FbDbType.VarChar)).Value = userPass;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void nntpserver_delete([NotNull] string connectionString, object nntpServerID)
        {
            using (var cmd = FbDbAccess.GetCommand("nntpserver_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NNTPSERVERID", FbDbType.Integer)).Value = nntpServerID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_NntpTopic

        public static DataTable nntptopic_list([NotNull] string connectionString, object thread)
        {
            using (var cmd = FbDbAccess.GetCommand("nntptopic_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_THREAD", FbDbType.VarChar)).Value = thread;


                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void nntptopic_savemessage(
            [NotNull] string connectionString,
            [NotNull] object nntpForumID,
            [NotNull] object topic,
            [NotNull] object body,
            [NotNull] object userID,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object externalMessageId,
            [NotNull] object referenceMessageId)
        {
            /* String newbody = body.ToString();
            newbody = newbody.Replace(@"&amp;", "&"); 
            newbody = newbody.Replace(@"&lt;", "<");
            newbody = newbody.Replace(@"&gt;", ">");
            newbody = newbody.Replace("</unquote/>", "[quote]");
            newbody = newbody.Replace("</quote/>", "[/quote]");         
                       
            newbody = newbody.Replace("&quot;", @"""");
            newbody = newbody.Replace(@"quot;", @"'");
            newbody = newbody.Replace("[-snip-]", "(SNIP)");
            newbody = newbody.Replace(@"@I_", "[dog]");
            newbody = newbody.Replace("_.", ".");
            newbody = newbody.Replace("br", "/n");
            
             
            newbody = newbody.Replace("&", "^^^"); */
            //string newbody = body.ToString().Replace(@"&lt;br&gt;", "> ").Replace(@"&amp;lt;", "<").Replace(@"&lt;hr&gt;", "> ").Replace(@"&amp;quot;", @"""").Replace(@"&lt;", @"<").Replace(@"br&gt;", @"> ").Replace(@"&amp;gt;", @"> ").Replace(@"&gt;", "> ").Replace("&quot;", @"""").Replace("[-snip-]", "(SNIP)").Replace(@"@I_", "[dog]").Replace("_.", "");
            using (var cmd = FbDbAccess.GetCommand("nntptopic_savemessage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NNTPFORUMID", FbDbType.Integer)).Value = nntpForumID;
                cmd.Parameters.Add(new FbParameter("@I_TOPIC", FbDbType.VarChar)).Value = topic;
                cmd.Parameters.Add(new FbParameter("@I_BODY", FbDbType.Text)).Value = body;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip;
                cmd.Parameters.Add(new FbParameter("@I_POSTED", FbDbType.TimeStamp)).Value = posted;
                cmd.Parameters.Add(new FbParameter("@I_EXTERNALMESSAGEID", FbDbType.VarChar)).Value = externalMessageId;
                cmd.Parameters.Add(new FbParameter("@I_REFERENCEMESSAGEID", FbDbType.VarChar)).Value =
                    referenceMessageId;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_PMessage

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
            using (var cmd = FbDbAccess.GetCommand("PMESSAGE_LIST"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FROMUSERID", FbDbType.Integer)).Value = fromUserID
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_TOUSERID", FbDbType.Integer)).Value = toUserID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_USERPMESSAGEID", FbDbType.Integer)).Value = userPMessageID
                                                                                                   ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
            }
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
        public static DataTable pmessage_list([NotNull] string connectionString, object userPMessageID)
        {
            return pmessage_list(connectionString, null, null, userPMessageID);
        }

        /// <summary>
        /// Deletes the private message from the database as per the given parameter.  If <paramref name="fromOutbox"/> is true,
        /// the message is only removed from the user's outbox.  Otherwise, it is completely delete from the database.
        /// </summary>
        /// <param name="pMessageID"></param>
        /// <param name="fromOutbox">If true, removes the message from the outbox.  Otherwise deletes the message completely.</param>
        public static void pmessage_delete([NotNull] string connectionString, object userPMessageID, bool fromOutbox)
        {
            using (var cmd = FbDbAccess.GetCommand("pmessage_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERPMESSAGEID", FbDbType.Integer)).Value = userPMessageID;
                cmd.Parameters.Add(new FbParameter("@I_FROMOUTBOX", FbDbType.Boolean)).Value = fromOutbox;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        /// Archives the private message of the given id.  Archiving moves the message from the user's inbox to his message archive.
        /// </summary>
        /// <param name="pMessageID">The ID of the private message</param>
        public static void pmessage_archive([NotNull] string connectionString, object userPMessageID)
        {
            using (var cmd = FbDbAccess.GetCommand("pmessage_archive"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERPMESSAGEID", FbDbType.Integer)).Value = userPMessageID
                                                                                                   ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("pmessage_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FROMUSERID", FbDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new FbParameter("@I_TOUSERID", FbDbType.Integer)).Value = toUserID;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = subject;
                cmd.Parameters.Add(new FbParameter("@I_BODY", FbDbType.Text)).Value = body;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = Flags;
                cmd.Parameters.Add(new FbParameter("@I_REPLYTO", FbDbType.Integer)).Value = replyTo;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void pmessage_markread([NotNull] string connectionString, object userPMessageID)
        {
            using (var cmd = FbDbAccess.GetCommand("pmessage_markread"))
            {
                if (userPMessageID == null)
                {
                    userPMessageID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERPMESSAGEID", FbDbType.Integer)).Value = userPMessageID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable pmessage_info([NotNull] string connectionString)
        {
            using (var cmd = FbDbAccess.GetCommand("pmessage_info"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void pmessage_prune([NotNull] string connectionString, object daysRead, object daysUnread)
        {
            using (var cmd = FbDbAccess.GetCommand("pmessage_prune"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_DAYSREAD", FbDbType.Integer)).Value = daysRead;
                cmd.Parameters.Add(new FbParameter("@I_DAYSUNREAD", FbDbType.Integer)).Value = daysUnread;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("pollgroup_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_POLLGROUPID", FbDbType.Integer)).Value = pollGroupId;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("pollgroup_attach"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_POLLGROUPID", FbDbType.Integer)).Value = pollGroupId;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        public static DataTable poll_stats([NotNull] string connectionString, object pollID)
        {
            using (var cmd = FbDbAccess.GetCommand("poll_stats"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_POLLID", FbDbType.Integer)).Value = pollID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The method saves many questions and answers to them in a single transaction 
        /// </summary>
        /// <param name="pollList">List to hold all polls data</param>
        /// <returns>Last saved poll id.</returns>
        public static int? poll_save(
            [NotNull] string connectionString, [NotNull] System.Collections.Generic.List<PollSaveList> pollList)
        {

            foreach (PollSaveList question in pollList)
            {
                var sb = new System.Text.StringBuilder();
                var paramSb = new System.Text.StringBuilder("EXECUTE BLOCK (");
                sb.Append(" RETURNS(OUT_POLLGROUPID INTEGER) AS  DECLARE VARIABLE OUT_POLLID INTEGER;  BEGIN ");
                // Check if the group already exists

                if (question.CategoryId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(FbDbAccess.GetObjectName("CATEGORY"));
                    sb.Append(" WHERE CATEGORYID = :I_CATEGORYID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_CATEGORYID INTEGER = ?,");
                }
                if (question.ForumId > 0)
                {

                    sb.Append("SELECT POLLGROUPID  FROM ");
                    sb.Append(FbDbAccess.GetObjectName("FORUM"));
                    sb.Append(" WHERE FORUMID = :I_FORUMID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_FORUMID INTEGER = ?,");
                }

                if (question.TopicId > 0)
                {
                    sb.Append(" SELECT POLLID FROM ");
                    sb.Append(FbDbAccess.GetObjectName("TOPIC"));
                    sb.Append(" WHERE TOPICID = :I_TOPICID INTO :OUT_POLLGROUPID; ");
                    paramSb.Append("I_TOPICID INTEGER = ?,");
                }


                // if the poll group doesn't exists, create a new one
                sb.Append("IF (OUT_POLLGROUPID IS NULL) THEN BEGIN ");

                sb.Append("INSERT INTO ");
                sb.Append(FbDbAccess.GetObjectName("POLLGROUPCLUSTER"));
                sb.AppendFormat(
                    "(POLLGROUPID, USERID, FLAGS) VALUES((SELECT NEXT VALUE FOR SEQ_{0}PGC_POLLGROUPID FROM RDB$DATABASE), :GROUPUSERID, :POLLGROUPFLAGS) RETURNING POLLGROUPID INTO :OUT_POLLGROUPID;  END ",
                    FbDbAccess.ObjectQualifier.ToUpper());

                paramSb.Append("GROUPUSERID INTEGER = ?,");
                paramSb.Append("POLLGROUPFLAGS INTEGER = ?,");
                if (question.CategoryId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("CATEGORY"));
                    sb.Append(" SET POLLGROUPID = :OUT_POLLGROUPID WHERE CATEGORYID = :I_CATEGORYID; ");

                }
                if (question.ForumId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("FORUM"));
                    sb.Append(" SET POLLGROUPID = :OUT_POLLGROUPID WHERE FORUMID = :I_FORUMID; ");
                }

                if (question.TopicId > 0)
                {
                    sb.Append("UPDATE ");
                    sb.Append(FbDbAccess.GetObjectName("TOPIC"));
                    sb.Append(" SET POLLID = :OUT_POLLGROUPID WHERE TOPICID = :I_TOPICID; ");
                }

                // System.Text.StringBuilder paramSb = new System.Text.StringBuilder("EXECUTE BLOCK ("); 
                // INSERT in poll
                sb.Append(" INSERT INTO ");
                sb.Append(FbDbAccess.GetObjectName("POLL"));
                if (question.Closes > DateTime.MinValue)
                {

                    sb.Append("(POLLID,QUESTION, USERID, CLOSES,POLLGROUPID,FLAGS");
                }
                else
                {
                    sb.Append("(POLLID,QUESTION, USERID,POLLGROUPID,FLAGS");
                }

                if (question.QuestionObjectPath.IsSet())
                {
                    sb.Append(", OBJECTPATH");
                }
                if (question.QuestionMimeType.IsSet())
                {
                    sb.Append(", MIMETYPE");
                }
                sb.Append(") VALUES(");

                sb.AppendFormat(
                    "(SELECT NEXT VALUE FOR SEQ_{0}POLL_POLLID FROM RDB$DATABASE),",
                    FbDbAccess.ObjectQualifier.ToUpper());
                sb.Append(":QUESTION");

                paramSb.Append(" QUESTION VARCHAR(255) = ?,");

                sb.Append(",:USERID");
                paramSb.Append("USERID INTEGER = ?,");

                if (question.Closes > DateTime.MinValue)
                {
                    sb.Append(",:CLOSES");
                    paramSb.Append("CLOSES TIMESTAMP = ?,");
                }
                sb.Append(",:OUT_POLLGROUPID");

                sb.Append(",:FLAGS");
                paramSb.Append("FLAGS INTEGER = ?,");
                if (question.QuestionObjectPath.IsSet())
                {
                    sb.Append(",:QUESTIONOBJECTPATH");
                    paramSb.Append("OBJECTPATH VARCHAR(255) = ?,");
                }
                if (question.QuestionMimeType.IsSet())
                {
                    sb.Append(",:QUESTIONMIMETYPE");
                    paramSb.Append("MIMETYPE VARCHAR(50) = ?,");
                }

                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.Append(") RETURNING POLLID INTO :OUT_POLLID; ");

                // The cycle through question reply choices to create prepare statement

                // The cycle through question reply choices            
                for (uint choiceCount = 0; choiceCount < question.Choice.GetLength(0); choiceCount++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount]))
                    {

                        sb.Append("INSERT INTO ");
                        sb.Append(FbDbAccess.GetObjectName("CHOICE"));

                        sb.Append("(CHOICEID, POLLID,CHOICE,VOTES");
                        if (question.QuestionObjectPath.IsSet())
                        {
                            sb.Append(",OBJECTPATH");
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            sb.Append(",MIMETYPE");
                        }
                        sb.Append(") VALUES(");
                        sb.AppendFormat(
                            "(SELECT NEXT VALUE FOR SEQ_{0}CHOICE_CHOICEID FROM RDB$DATABASE),",
                            FbDbAccess.ObjectQualifier.ToUpper());
                        sb.AppendFormat(":OUT_POLLID,:CHOICE{0},:VOTES{0}", choiceCount);
                        if (question.QuestionObjectPath.IsSet())
                        {
                            sb.AppendFormat(",:CHOICEOBJECTPATH{0}", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            sb.AppendFormat(",:CHOICEMIMETYPE{0}", choiceCount);
                        }
                        sb.Append("); ");
                        paramSb.AppendFormat("CHOICE{0} VARCHAR(255) = ?,", choiceCount);
                        paramSb.AppendFormat("VOTES{0} INTEGER = ?,", choiceCount);

                        if (question.QuestionObjectPath.IsSet())
                        {
                            paramSb.AppendFormat("CHOICEOBJECTPATH{0} VARCHAR(255) = ?,", choiceCount);
                        }
                        if (question.QuestionMimeType.IsSet())
                        {
                            paramSb.AppendFormat("CHOICEMIMETYPE{0} VARCHAR(50) = ?,", choiceCount);
                        }

                    }

                }

                sb.Append(" SUSPEND; END;");
                var cmd = new FbCommand { CommandText = paramSb.ToString().TrimEnd(',') + ") " + sb.ToString() };
                var connMan = new FbDbConnectionManager(connectionString);
                FbConnection con = connMan.OpenDBConnection(connectionString);
                FbTransaction trans = con.BeginTransaction(FbDbAccess.IsolationLevel);

                cmd.Transaction = trans;
                // cmd.Prepare();   
                /* FbParameter ret = new FbParameter();
                ret.ParameterName = "@OUT_POLLID";
                ret.FbDbType = FbDbType.Integer;
                ret.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ret); */
                object categoryId = DBNull.Value;
                object forumId = DBNull.Value;
                object topicId = DBNull.Value;
                if (question.CategoryId > 0)
                {
                    cmd.Parameters.Add(new FbParameter("I_CATEGORYID", FbDbType.Integer)).Value = question.CategoryId
                                                                                                  ?? categoryId;
                }
                if (question.ForumId > 0)
                {
                    cmd.Parameters.Add(new FbParameter("I_FORUMID", FbDbType.Integer)).Value = question.ForumId
                                                                                               ?? forumId;
                }
                if (question.TopicId > 0)
                {
                    cmd.Parameters.Add(new FbParameter("I_TOPICID", FbDbType.Integer)).Value = question.TopicId
                                                                                               ?? topicId;
                }
                cmd.Parameters.Add(new FbParameter("GROUPUSERID", FbDbType.Integer)).Value = question.UserId;
                int pollGroupFlags = question.IsBound ? 0 | 2 : 0;
                cmd.Parameters.Add(new FbParameter("POLLGROUPFLAGS", FbDbType.Integer)).Value = pollGroupFlags;
                cmd.Parameters.Add(new FbParameter("QUESTION", FbDbType.VarChar)).Value = question.Question;
                cmd.Parameters.Add(new FbParameter("USERID", FbDbType.Integer)).Value = question.UserId;

                if (question.Closes > DateTime.MinValue)
                {
                    cmd.Parameters.Add(new FbParameter("CLOSES", FbDbType.TimeStamp)).Value = question.Closes;
                }

                // set poll  flags
                int pollFlags = 0;
                if (question.IsClosedBound)
                {
                    pollFlags = pollFlags | 4;
                }
                if (question.AllowMultipleChoices)
                {
                    pollFlags = pollFlags | 8;
                }
                if (question.ShowVoters)
                {
                    pollFlags = pollFlags | 16;
                }
                if (question.AllowSkipVote)
                {
                    pollFlags = pollFlags | 32;
                }


                cmd.Parameters.Add(new FbParameter("FLAGS", FbDbType.Integer)).Value = pollFlags;
                if (question.QuestionObjectPath.IsSet())
                {
                    cmd.Parameters.Add(new FbParameter("QUESTIONOBJECTPATH", FbDbType.VarChar)).Value =
                        question.QuestionObjectPath;
                }
                if (question.QuestionMimeType.IsSet())
                {
                    cmd.Parameters.Add(new FbParameter("QUESTIONMIMETYPE", FbDbType.VarChar)).Value =
                        question.QuestionMimeType;
                }

                for (uint choiceCount1 = 0; choiceCount1 < question.Choice.GetLength(0); choiceCount1++)
                {
                    if (!string.IsNullOrEmpty(question.Choice[0, choiceCount1]))
                    {
                        cmd.Parameters.Add(new FbParameter(String.Format("CHOICE{0}", choiceCount1), FbDbType.VarChar))
                           .Value = question.Choice[0, choiceCount1];
                        cmd.Parameters.Add(new FbParameter(String.Format("VOTES{0}", choiceCount1), FbDbType.Integer))
                           .Value = 0;
                        if (question.Choice[1, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(
                                new FbParameter(String.Format("CHOICEOBJECTPATH{0}", choiceCount1), FbDbType.VarChar))
                               .Value = question.Choice[1, choiceCount1].IsNotSet()
                                            ? String.Empty
                                            : question.Choice[1, choiceCount1];
                        }
                        if (question.Choice[2, choiceCount1].IsSet())
                        {
                            cmd.Parameters.Add(
                                new FbParameter(String.Format("CHOICEMIMETYPE{0}", choiceCount1), FbDbType.VarChar))
                               .Value = question.Choice[2, choiceCount1].IsNotSet()
                                            ? String.Empty
                                            : question.Choice[2, choiceCount1];
                        }
                    }
                }



                // cmd.Prepare();   
                int? result = Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, true, connectionString));
                trans.Commit();
                con.Close();
                return result;

            }
            return null;
        }

        public static void poll_update(
            [NotNull] string connectionString,
            [NotNull] object pollID,
            [NotNull] object question,
            [NotNull] object closes,
            [NotNull] object isBounded,
            bool isClosedBounded,
            bool allowMultipleChoices,
            bool showVoters,
            bool allowSkipVote,
            [NotNull] object questionPath,
            [NotNull] object questionMime)
        {
            using (var cmd = FbDbAccess.GetCommand("POLL_UPDATE"))
            {
                if (closes == null)
                {
                    closes = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_POLLID", FbDbType.Integer)).Value = pollID;
                cmd.Parameters.Add(new FbParameter("@I_QUESTION", FbDbType.VarChar)).Value = question;
                cmd.Parameters.Add(new FbParameter("@I_CLOSES", FbDbType.TimeStamp)).Value = closes;
                cmd.Parameters.Add(new FbParameter("@I_QUESTIONOBJECTPATH", FbDbType.VarChar)).Value = questionPath;
                cmd.Parameters.Add(new FbParameter("@I_QUESTIONMIMETYPE", FbDbType.VarChar)).Value = questionMime;
                cmd.Parameters.Add(new FbParameter("@I_ISBOUNDED", FbDbType.Boolean)).Value = isBounded;
                cmd.Parameters.Add(new FbParameter("@I_ISCLOSEDBOUNDED", FbDbType.Boolean)).Value = isClosedBounded;
                cmd.Parameters.Add(new FbParameter("@I_ALLOWMULTIPLECHOICES", FbDbType.Boolean)).Value =
                    allowMultipleChoices;
                cmd.Parameters.Add(new FbParameter("@I_SHOWVOTERS", FbDbType.Boolean)).Value = showVoters;
                cmd.Parameters.Add(new FbParameter("@I_ALLOWSKIPVOTE", FbDbType.Boolean)).Value = allowSkipVote;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("poll_remove"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_POLLGROUPID", FbDbType.Integer)).Value = pollGroupID;
                cmd.Parameters.Add(new FbParameter("@I_POLLID", FbDbType.Integer)).Value = pollID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_REMOVECOMPLETELY", FbDbType.Boolean)).Value = removeCompletely;
                cmd.Parameters.Add(new FbParameter("@I_REMOVEEVERYWHERE", FbDbType.Boolean)).Value = removeEverywhere;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("POLLGROUP_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;

                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedPollGroup(r));
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
            [NotNull] object pollGroupID,
            object topicId,
            object forumId,
            object categoryId,
            object boardId,
            bool removeCompletely,
            bool removeEverywhere)
        {
            using (var cmd = FbDbAccess.GetCommand("POLLGROUP_REMOVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_POLLGROUPID", FbDbType.Integer)).Value = pollGroupID;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_REMOVECOMPLETELY", FbDbType.Boolean)).Value = removeCompletely;
                cmd.Parameters.Add(new FbParameter("@I_REMOVEEVERYWHERE", FbDbType.Boolean)).Value = removeEverywhere;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void choice_delete([NotNull] string connectionString, [NotNull] object choiceID)
        {
            using (var cmd = FbDbAccess.GetCommand("CHOICE_DELETE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_CHOICEID", FbDbType.Integer)).Value = choiceID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void choice_update(
            [NotNull] string connectionString,
            [NotNull] object choiceID,
            [NotNull] object choice,
            [NotNull] object path,
            [NotNull] object mime)
        {
            using (var cmd = FbDbAccess.GetCommand("CHOICE_UPDATE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_CHOICEID", FbDbType.Integer)).Value = choiceID;
                cmd.Parameters.Add(new FbParameter("@I_CHOICE", FbDbType.VarChar)).Value = choice;
                cmd.Parameters.Add(new FbParameter("@I_OBJECTPATH", FbDbType.VarChar)).Value = path;
                cmd.Parameters.Add(new FbParameter("@I_MIMETYPE", FbDbType.VarChar)).Value = mime;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void choice_add(
            [NotNull] string connectionString,
            [NotNull] object pollId,
            [NotNull] object choice,
            [NotNull] object path,
            [NotNull] object mime)
        {
            using (var cmd = FbDbAccess.GetCommand("choice_add"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_POLLID", FbDbType.Integer)).Value = pollId;
                cmd.Parameters.Add(new FbParameter("@I_CHOICE", FbDbType.VarChar)).Value = choice;
                cmd.Parameters.Add(new FbParameter("@I_OBJECTPATH", FbDbType.VarChar)).Value = path;
                cmd.Parameters.Add(new FbParameter("@I_MIMETYPE", FbDbType.VarChar)).Value = mime;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Rank

        public static DataTable rank_list([NotNull] string connectionString, object boardID, object rankID)
        {
            using (var cmd = FbDbAccess.GetCommand("rank_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void rank_save(
            [NotNull] string connectionString,
            object rankID,
            object boardID,
            object name,
            object isStart,
            object isLadder,
            [NotNull] object isGuest,
            object minPosts,
            object rankImage,
            object pmlimit,
            object style,
            object sortOrder,
            object description,
            object usrSigChars,
            object usrSigBBCodes,
            object usrSigHTMLTags,
            object usrAlbums,
            object usrAlbumImages)
        {
            using (var cmd = FbDbAccess.GetCommand("rank_save"))
            {
                if (minPosts.ToString() == string.Empty)
                {
                    minPosts = 0;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_ISSTART", FbDbType.Boolean)).Value = isStart;
                cmd.Parameters.Add(new FbParameter("@I_ISLADDER", FbDbType.Boolean)).Value = isLadder;
                cmd.Parameters.Add(new FbParameter("@I_ISGUEST", FbDbType.Boolean)).Value = isGuest;
                cmd.Parameters.Add(new FbParameter("@I_MINPOSTS", FbDbType.Integer)).Value = minPosts;
                cmd.Parameters.Add(new FbParameter("@I_RANKIMAGE", FbDbType.VarChar)).Value = rankImage ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PMLIMIT", FbDbType.Integer)).Value = pmlimit;
                cmd.Parameters.Add(new FbParameter("@I_STYLE", FbDbType.VarChar)).Value = style;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.Integer)).Value = sortOrder;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGCHARS", FbDbType.Integer)).Value = usrSigChars;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGBBCODES", FbDbType.VarChar)).Value = usrSigBBCodes;
                cmd.Parameters.Add(new FbParameter("@I_USRSIGSHTMLTAGS", FbDbType.VarChar)).Value = usrSigHTMLTags;
                cmd.Parameters.Add(new FbParameter("@I_USRALBUMS", FbDbType.Integer)).Value = usrAlbums;
                cmd.Parameters.Add(new FbParameter("@I_USRALBUMIMAGES", FbDbType.Integer)).Value = usrAlbumImages;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void rank_delete([NotNull] string connectionString, [NotNull] object rankID)
        {
            using (var cmd = FbDbAccess.GetCommand("rank_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Smiley

        [NotNull]


        public static DataTable smiley_list([NotNull] string connectionString, object boardID, object smileyID)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_list"))
            {
                if (smileyID == null)
                {
                    smileyID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_SMILEYID", FbDbType.Integer)).Value = smileyID;

                return FbDbAccess.GetData(cmd, connectionString);
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
        public static IEnumerable<TypedSmileyList> SmileyList([NotNull] string connectionString, int boardID, int? smileyID)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_list"))
            {
                // if (smileyID == null) { smileyID = DBNull.Value; }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_SMILEYID", FbDbType.Integer)).Value = smileyID;

                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(r => new TypedSmileyList(r));
            }
        }

        public static DataTable smiley_listunique([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_listunique"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void smiley_delete([NotNull] string connectionString, object smileyID)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_delete"))
            {
                if (smileyID == null)
                {
                    smileyID = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_SMILEYID", FbDbType.Integer)).Value = smileyID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void smiley_save(
            [NotNull] string connectionString,
            object smileyID,
            object boardID,
            object code,
            object icon,
            object emoticon,
            object sortOrder,
            object replace)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_save"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_SMILEYID", FbDbType.Integer)).Value = smileyID ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_CODE", FbDbType.VarChar)).Value = code;
                cmd.Parameters.Add(new FbParameter("@I_ICON", FbDbType.VarChar)).Value = icon;
                cmd.Parameters.Add(new FbParameter("@I_EMOTICON", FbDbType.VarChar)).Value = emoticon;
                cmd.Parameters.Add(new FbParameter("@I_SORTORDER", FbDbType.SmallInt)).Value = sortOrder;
                cmd.Parameters.Add(new FbParameter("@I_REPLACE", FbDbType.SmallInt)).Value = replace ?? 0;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void smiley_resort([NotNull] string connectionString, object boardID, object smileyID, int move)
        {
            using (var cmd = FbDbAccess.GetCommand("smiley_resort"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_SMILEYID", FbDbType.Integer)).Value = smileyID;
                cmd.Parameters.Add(new FbParameter("@I_MOVE", FbDbType.Integer)).Value = move;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_BBCode

        public static DataTable bbcode_list(
            [NotNull] string connectionString, [NotNull] object boardID, [NotNull] object bbcodeID)
        {
            using (var cmd = FbDbAccess.GetCommand("bbcode_list"))
            {
                if (bbcodeID == null)
                {
                    bbcodeID = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_BBCODEID", FbDbType.Integer)).Value = bbcodeID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void bbcode_delete([NotNull] string connectionString, [NotNull] object bbcodeID)
        {
            using (var cmd = FbDbAccess.GetCommand("bbcode_delete"))
            {
                if (bbcodeID == null)
                {
                    bbcodeID = DBNull.Value;
                }
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BBCODEID", FbDbType.Integer)).Value = bbcodeID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void bbcode_save(
            [NotNull] string connectionString,
            [NotNull] object bbcodeID,
            [NotNull] object boardID,
            [NotNull] object name,
            [NotNull] object description,
            [NotNull] object onclickjs,
            [NotNull] object displayjs,
            [NotNull] object editjs,
            [NotNull] object displaycss,
            [NotNull] object searchregex,
            [NotNull] object replaceregex,
            [NotNull] object variables,
            [NotNull] object usemodule,
            [NotNull] object moduleclass,
            [NotNull] object execorder)
        {
            using (var cmd = FbDbAccess.GetCommand("bbcode_save"))
            {
                //My input defaults
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

                cmd.Parameters.Add(new FbParameter("@I_BBCODEID", FbDbType.Integer)).Value = bbcodeID;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_ONCLICKJS", FbDbType.VarChar)).Value = onclickjs;
                cmd.Parameters.Add(new FbParameter("@I_DISPLAYJS", FbDbType.Text)).Value = displayjs;
                cmd.Parameters.Add(new FbParameter("@I_EDITJS", FbDbType.Text)).Value = editjs;
                cmd.Parameters.Add(new FbParameter("@I_DISPLAYCSS", FbDbType.Text)).Value = displaycss;
                cmd.Parameters.Add(new FbParameter("@I_SEARCHREGEX", FbDbType.Text)).Value = searchregex;
                cmd.Parameters.Add(new FbParameter("@I_REPLACEREGEX", FbDbType.Text)).Value = replaceregex;
                cmd.Parameters.Add(new FbParameter("@I_VARIABLES", FbDbType.VarChar)).Value = variables;
                cmd.Parameters.Add(new FbParameter("@I_USEMODULE", FbDbType.Boolean)).Value =
                    Convert.ToBoolean(usemodule);
                cmd.Parameters.Add(new FbParameter("@I_MODULECLASS", FbDbType.VarChar)).Value = moduleclass;
                cmd.Parameters.Add(new FbParameter("@I_EXECORDER", FbDbType.Integer)).Value = execorder;



                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Registry

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="Name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, object name, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("registry_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Retrieves all the entries in the board settings registry
        /// </summary>
        /// <returns>DataTable filled will all registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString)
        {
            return registry_list(connectionString, null, null);
        }

        /// <summary>
        /// Retrieves entries in the board settings registry
        /// </summary>
        /// <param name="Name">Use to specify return of specific entry only. Setting this to null returns all entries.</param>
        /// <returns>DataTable filled will registry entries</returns>
        public static DataTable registry_list([NotNull] string connectionString, [NotNull] object name)
        {
            return registry_list(connectionString, name, null);
        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="Name">Unique name associated with this entry</param>
        /// <param name="Value">Value associated with this entry which can be null</param>
        public static void registry_save([NotNull] string connectionString, [NotNull] object name, [NotNull] object value)
        {
            using (var cmd = FbDbAccess.GetCommand("registry_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_VALUE", FbDbType.VarChar)).Value = value;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Saves a single registry entry pair to the database.
        /// </summary>
        /// <param name="Name">Unique name associated with this entry</param>
        /// <param name="Value">Value associated with this entry which can be null</param>
        /// <param name="BoardID">The BoardID for this entry</param>
        public static void registry_save(
            [NotNull] string connectionString, [NotNull] object name, [NotNull] object value, [NotNull] object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("registry_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_VALUE", FbDbType.VarChar)).Value = value;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_System

        /// <summary>
        /// Not in use anymore. Only required for old database versions.
        /// </summary>
        /// <returns></returns>
        public static DataTable system_list([NotNull] string connectionString)
        {
            using (var cmd = FbDbAccess.GetCommand("system_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_Topic

        public static DataTable topic_tags([NotNull] string connectionString, int boardId, int pageUserId, int topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_tags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                cmd.Parameters.Add("@I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("@I_TOPICID", FbDbType.Integer).Value = topicId;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable topic_bytags(
            [NotNull] string connectionString,
            int boardId,
            int forumId, 
            object pageUserId,
            string tags,
            object sinceDate,
            int pageIndex,
            int pageSize)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_bytags"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                cmd.Parameters.Add("@I_FORUMID", FbDbType.Integer).Value = forumId;
                cmd.Parameters.Add("@I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("@I_TAGS", FbDbType.VarChar).Value = tags;
                cmd.Parameters.Add("@I_SINCEDATE", FbDbType.TimeStamp).Value = sinceDate;
                cmd.Parameters.Add("@I_PAGEINDEX", FbDbType.Integer).Value = pageIndex;
                cmd.Parameters.Add("@I_PAGESIZE", FbDbType.Integer).Value = pageSize;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void topic_updatetopic([NotNull] string connectionString, int topicId, string topic)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_updatetopic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_TOPIC", FbDbType.VarChar)).Value = topic;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }


        public static int topic_prune([NotNull] string connectionString, object forumID, object days)
        {
            int boardID = 0;
            using (
                var cmd =
                    FbDbAccess.GetCommand(
                        String.Format(
                            @"SELECT c.BOARDID FROM {0} f INNER JOIN {1} c ON f.CATEGORYID=c.CATEGORYID  WHERE FORUMID={2};",
                            FbDbAccess.GetObjectName("Forum"),
                            FbDbAccess.GetObjectName("Category"),
                            forumID),
                        true))
            {
                boardID = Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));

            }

            return topic_prune(connectionString, boardID, forumID, days, 1);

        }

        public static int topic_prune(
            [NotNull] string connectionString, object boardId, object forumId, object days, object permDelete)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_prune"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_DAYS", FbDbType.Integer)).Value = days;
                cmd.Parameters.Add(new FbParameter("@I_PERMDELETE", FbDbType.Boolean)).Value = permDelete;

                return (int)FbDbAccess.ExecuteScalar(cmd, connectionString);
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

            using (var cmd = FbDbAccess.GetCommand("TOPIC_LIST"))
            {

                if (userId == null)
                {
                    userId = DBNull.Value;
                }
                if (sinceDate == null)
                {
                    sinceDate = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_DATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_SHOWMOVED", FbDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTUNREAD", FbDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new FbParameter("@I_GETTAGS", FbDbType.Boolean)).Value = getTags;

                return FbDbAccess.GetData(cmd, true, connectionString);
            }
        }

        public static DataTable announcements_list(
            [NotNull] string connectionString,
            [NotNull] object forumId,
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
            using (var cmd = FbDbAccess.GetCommand("announcements_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_DATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_SHOWMOVED", FbDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTUNREAD", FbDbType.Boolean)).Value = findLastRead;
                cmd.Parameters.Add(new FbParameter("@I_GETTAGS", FbDbType.Boolean)).Value = getTags;
                
                return FbDbAccess.GetData(cmd, true, connectionString);
            }
        }

        /// <summary>
        /// Lists topics very simply (for URL rewriting)
        /// </summary>
        /// <param name="startId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static DataTable topic_simplelist([NotNull] string connectionString, int startId, int limit)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_simplelist"))
            {
                if (startId <= 0)
                {
                    startId = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_STARTID", FbDbType.Integer)).Value = startId;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = limit;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void topic_move(
            [NotNull] string connectionString, object topicId, object forumId, object showMoved, object linkDays)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_move"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_SHOWMOVED", FbDbType.Boolean)).Value = showMoved;
                cmd.Parameters.Add(new FbParameter("@I_LINKDAYS", FbDbType.Integer)).Value = linkDays;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable topic_announcements(
            [NotNull] string connectionString, object boardId, object numOfPostsToRetrieve, object pageUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_announcements"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_NUMPOSTS", FbDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_latest.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="numOfPostsToRetrieve">
        /// The num of posts to retrieve.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="useStyledNicks">
        /// If true returns string for userID style.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable topic_latest(
            [NotNull] string connectionString,
            object boardID,
            object numOfPostsToRetrieve,
            object pageUserId,
            bool useStyledNicks,
            bool showNoCountPosts,
            object findLastRead)
        {
            using (var cmd = FbDbAccess.GetCommand("TOPIC_LATEST"))
            {
                int style = 0;
                if (useStyledNicks) style = 1;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NUMPOSTS", FbDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = style;
                cmd.Parameters.Add(new FbParameter("@I_SHOWNOCOUNTPOSTS", FbDbType.Boolean)).Value = showNoCountPosts;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Boolean)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
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
            object boardID,
            object numOfPostsToRetrieve,
            object userID,
            bool useStyledNicks,
            bool showNoCountPosts)
        {
            using (var cmd = FbDbAccess.GetCommand("rss_topic_latest"))
            {
                int style = 0;
                if (useStyledNicks) style = 1;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NUMPOSTS", FbDbType.Integer)).Value = numOfPostsToRetrieve;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = style;
                cmd.Parameters.Add(new FbParameter("@I_SHOWNOCOUNTPOSTS", FbDbType.Boolean)).Value = showNoCountPosts;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("topic_active"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Integer)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        private static void topic_deleteAttachments([NotNull] string connectionString, object topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_listmessages"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer));
                cmd.Parameters[0].Value = topicId;



                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        message_deleteRecursively(connectionString, row["MessageID"], true, string.Empty, 0, true, false);
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
        public static void topic_delete([NotNull] string connectionString, object topicId)
        {
            topic_delete(connectionString, topicId, false);
        }

        public static void topic_delete([NotNull] string connectionString, object topicId, object eraseTopic)
        {
            if (eraseTopic == null)
            {
                eraseTopic = false;
            }


            if (eraseTopic.ToType<bool>())
            {
                topic_deleteAttachments(connectionString, topicId);

                topic_deleteimages(connectionString, (int)topicId);
            }

            using (var cmd = FbDbAccess.GetCommand("topic_delete"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_ERASETOPIC", FbDbType.Boolean)).Value = eraseTopic ?? 0;
                cmd.Parameters.Add(new FbParameter("@I_UPDATELASTPOST", FbDbType.Boolean)).Value = true;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable topic_findprev([NotNull] string connectionString, object topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FINDPREV"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable topic_findnext([NotNull] string connectionString, object topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FINDNEXT"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void topic_lock([NotNull] string connectionString, object topicId, object locked)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_lock"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_LOCKED", FbDbType.Boolean)).Value = locked;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static long topic_save(
            [NotNull] string connectionString,
            [NotNull] object forumId,
            [NotNull] object subject,
            [NotNull] object status,
            [NotNull] object styles,
            [NotNull] object description,
            [NotNull] object message,
            [NotNull] object userID,
            [NotNull] object priority,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object blogPostID,
            [NotNull] object flags,
            ref long messageId,
            string tags)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_SUBJECT", FbDbType.VarChar)).Value = subject;
                cmd.Parameters.Add(new FbParameter("@I_STATUS", FbDbType.VarChar)).Value = status;
                cmd.Parameters.Add(new FbParameter("@I_STYLES", FbDbType.VarChar)).Value = styles;
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_PRIORITY", FbDbType.SmallInt)).Value = Convert.ToInt16(priority);
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip.ToString();
                cmd.Parameters.Add(new FbParameter("@I_POSTED", FbDbType.TimeStamp)).Value = posted;
                cmd.Parameters.Add(new FbParameter("@I_BLOGPOSTID", FbDbType.VarChar)).Value = blogPostID;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_TAGS", FbDbType.VarChar)).Value = tags;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                DataTable dt = FbDbAccess.GetData(cmd, connectionString);
                messageId = long.Parse(dt.Rows[0]["MessageID"].ToString());
                return long.Parse(dt.Rows[0]["TopicID"].ToString());
            }
        }

        public static DataRow topic_info([NotNull] string connectionString, object topicId, [NotNull] bool getTags)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_info"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_SHOWDELETED", FbDbType.Boolean)).Value = false;
                cmd.Parameters.Add(new FbParameter("@I_GETTAGS", FbDbType.Boolean)).Value = getTags;

                using (DataTable dt = FbDbAccess.GetData(cmd, connectionString))
                {
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        /// <summary>
        /// The topic_imagesave.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        public static void topic_imagesave([NotNull] string connectionString, object topicId, [NotNull] object imageUrl, Stream stream, object topicImageType)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_imagesave"))
            {
                byte[] data = null;
                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                cmd.Parameters.Add(new FbParameter("@I_IMAGEURL", FbDbType.VarChar)).Value = imageUrl ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_STREAM", FbDbType.Binary)).Value = data;
                cmd.Parameters.Add(new FbParameter("@I_TOPICIMAGETYPE", FbDbType.VarChar)).Value = topicImageType ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                int r = 1;
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
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since Date.
        /// </param>
        /// <param name="toDate">
        /// The to Date.
        /// </param> 
        /// <param name="pageIndex">
        /// The page Index.
        /// </param>
        /// <param name="pageSize">
        /// The page Size.
        /// </param>
        /// <param name="useStyledNicks">
        /// Set to true to get color nicks for last user and topic starter.
        /// </param>
        /// <param name="findLastRead">
        /// Indicates if the Table should Countain the last Access Date
        /// </param>
        /// <returns>
        /// Returns the List with the Topics Unanswered
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
            using (var cmd = FbDbAccess.GetCommand("topic_unanswered"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Integer)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("topic_unread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Integer)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Gets all topics where the pageUserid has posted
        /// </summary>
        /// <param name="boardID">
        /// The board id.
        /// </param>
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="since">
        /// The since.
        /// </param>
        /// <param name="categoryID">
        /// The category id.
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
            using (var cmd = FbDbAccess.GetCommand("topics_byuser"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Integer)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
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
                using (var cmd = FbDbAccess.GetCommand("TopicStatus_Delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("i_TopicStatusID", FbDbType.Integer).Value = topicStatusID;
                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("TopicStatus_Edit"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_TopicStatusID", FbDbType.Integer).Value = topicStatusID;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// List all Topics of the Current Board
        /// </summary>
        /// <param name="boardID">The board ID.</param>
        /// <returns></returns>
        public static DataTable TopicStatus_List([NotNull] string connectionString, [NotNull] object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("TopicStatus_List"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_BoardID", FbDbType.Integer).Value = boardID;
                return FbDbAccess.GetData(cmd, connectionString);
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
                using (var cmd = FbDbAccess.GetCommand("TopicStatus_Save"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("i_TopicStatusID", FbDbType.Integer).Value = topicStatusID;
                    cmd.Parameters.Add("i_BoardID", FbDbType.Integer).Value = boardID;
                    cmd.Parameters.Add("i_TopicStatusName", FbDbType.VarChar).Value = topicStatusName;
                    cmd.Parameters.Add("i_DefaultDescription", FbDbType.VarChar).Value = defaultDescription;

                    FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                }
            }
            catch
            {
                // Ignore any errors in this method
            }
        }

        public static int topic_findduplicate([NotNull] string connectionString, object topicName)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_findduplicate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICNAME", FbDbType.VarChar)).Value = topicName;
                return (int)FbDbAccess.ExecuteScalar(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FAVORITE_DETAILS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value = categoryId
                                                                                               ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_PAGEUSERID", FbDbType.Integer)).Value = pageUserId;
                cmd.Parameters.Add(new FbParameter("@I_SINCEDATE", FbDbType.TimeStamp)).Value = sinceDate;
                cmd.Parameters.Add(new FbParameter("@I_TODATE", FbDbType.TimeStamp)).Value = toDate;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_FINDLASTREAD", FbDbType.Integer)).Value = findLastRead;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FAVORITE_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FAVORITE_REMOVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FAVORITE_ADD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_ReplaceWords

        // rico : replace words / begin
        /// <summary>
        /// Gets a list of replace words
        /// </summary>
        /// <returns>DataTable with replace words</returns>
        public static DataTable replace_words_list(
            [NotNull] string connectionString, [NotNull] object boardId, [NotNull] object id)
        {
            using (var cmd = FbDbAccess.GetCommand("replace_words_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = id ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("replace_words_save"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = id ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@i_BADWORD", FbDbType.VarChar)).Value = badword;
                cmd.Parameters.Add(new FbParameter("@i_GOODWORD", FbDbType.VarChar)).Value = goodword;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Deletes a bad/good word
        /// </summary>
        /// <param name="ID">ID of bad/good word to delete</param>
        public static void replace_words_delete([NotNull] string connectionString, object id)
        {
            using (var cmd = FbDbAccess.GetCommand("replace_words_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ID", FbDbType.Integer)).Value = id;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region IgnoreUser

        public static void user_addignoreduser([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("USER_ADDIGNOREDUSER"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_IGNOREDUSERID", FbDbType.Integer)).Value = ignoredUserId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_removeignoreduser([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("USER_REMOVEIGNOREDUSER"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_IGNOREDUSERID", FbDbType.Integer)).Value = ignoredUserId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static bool user_isuserignored([NotNull] string connectionString, object userId, object ignoredUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("USER_ISUSERIGNORED"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_IGNOREDUSERID", FbDbType.Integer)).Value = ignoredUserId;

                return Convert.ToBoolean(FbDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        public static DataTable user_ignoredlist([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("USER_IGNOREDLIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = userId;

                return FbDbAccess.GetData(cmd, connectionString);
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
            int nTries = 0;
            while (true)
            {
                try
                {
                    using (var cmd = FbDbAccess.GetCommand("user_lazydata"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                        cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;
                        cmd.Parameters.Add("@I_SHOWPENDINGEMAILS", FbDbType.Boolean).Value = showPendingMails;
                        cmd.Parameters.Add("@I_SHOWPENDINGBUDDIES", FbDbType.Boolean).Value = showPendingBuddies;
                        cmd.Parameters.Add("@I_SHOWUNREADPMS", FbDbType.Boolean).Value = showUnreadPMs;
                        cmd.Parameters.Add("@I_SHOWUSERALBUMS", FbDbType.Boolean).Value = showUserAlbums;
                        cmd.Parameters.Add("@I_SHOWUSERSTYLE", FbDbType.Boolean).Value = styledNicks;
                        return FbDbAccess.GetData(cmd, connectionString).Rows[0];
                    }
                }
                catch (FbException x)
                {
                    if (x.ErrorCode == 1205 && nTries < 3)
                    {
                        // Transaction (Process ID XXX) was deadlocked on lock resources with another process and has been chosen as the deadlock victim. Rerun the transaction.

                    }
                    else
                    {
                        throw new ApplicationException(
                            string.Format("Sql Exception with error number {0} (Tries={1})", x.ErrorCode, nTries), x);
                    }
                }

                ++nTries;
            }
        }

        [NotNull]
        public static IEnumerable<TypedUserList> UserList(
            [NotNull] string connectionString,
            int boardID,
            int? userID,
            bool? approved,
            int? groupID,
            int? rankID,
            bool? useStyledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("user_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_APPROVED", FbDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;


                return FbDbAccess.GetData(cmd, connectionString).AsEnumerable().Select(x => new TypedUserList(x));
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
        /// <returns>
        /// </returns>
        public static DataTable user_list([NotNull] string connectionString, object boardID, object userID, object approved)
        {
            return user_list(connectionString, boardID, userID, approved, null, null, false);
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
        /// <param name="useStyledNicks">
        /// Return style info.
        /// </param> 
        /// <returns>
        /// </returns>
        public static DataTable user_list(
            [NotNull] string connectionString, object boardID, object userID, object approved, object useStyledNicks)
        {
            return user_list(connectionString, boardID, userID, approved, null, null, useStyledNicks);
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
        /// <returns>
        /// </returns>

        public static DataTable user_list(
            [NotNull] string connectionString, object boardID, object userID, object approved, object groupID, object rankID)
        {
            return user_list(connectionString, boardID, userID, approved, null, null, false);
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
            object boardID,
            object userID,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("user_list"))
            {
                if (userID == null)
                {
                    userID = DBNull.Value;
                }
                // if (approved == null) { approved = DBNull.Value; }               
                // if (approved.ToString().ToLower().Contains("true")) { approved = 1; }
                // else { approved = 0; }
                if (groupID == null)
                {
                    groupID = DBNull.Value;
                }
                if (rankID == null)
                {
                    rankID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_APPROVED", FbDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_pagedlist(
            [NotNull] string connectionString,
            object boardID,
            object userID,
            object approved,
            object groupID,
            object rankID,
            object useStyledNicks,
            object pageIndex,
            object pageSize)
        {
            using (var cmd = FbDbAccess.GetCommand("user_pagedlist"))
            {
                if (userID == null)
                {
                    userID = DBNull.Value;
                }
                // if (approved == null) { approved = DBNull.Value; }               
                // if (approved.ToString().ToLower().Contains("true")) { approved = 1; }
                // else { approved = 0; }
                if (groupID == null)
                {
                    groupID = DBNull.Value;
                }
                if (rankID == null)
                {
                    rankID = DBNull.Value;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_APPROVED", FbDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupID;
                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankID;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_PAGEINDEX", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("@I_PAGESIZE", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_listmembers(
            [NotNull] string connectionString,
            [NotNull] object boardId,
            [NotNull] object userId,
            [NotNull] object approved,
            [NotNull] object groupId,
            [NotNull] object rankId,
            [NotNull] object useStyledNicks,
            [NotNull] object lastUserId,
            [NotNull] object literals,
            [NotNull] object exclude,
            [NotNull] object beginsWith,
            [NotNull] object pageIndex,
            [NotNull] object pageSize,
            [NotNull] object sortName,
            [NotNull] object sortRank,
            [NotNull] object sortJoined,
            [NotNull] object sortPosts,
            [NotNull] object sortLastVisit,
            [NotNull] object numPosts,
            [NotNull] object numPostCompare)
        {
            using (var cmd = FbDbAccess.GetCommand("user_listmembers"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("BoardID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("UserID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("Approved", FbDbType.Boolean)).Value = approved;
                cmd.Parameters.Add(new FbParameter("GroupID", FbDbType.Integer)).Value = groupId;
                cmd.Parameters.Add(new FbParameter("RankID", FbDbType.Integer)).Value = rankId;
                cmd.Parameters.Add(new FbParameter("StyledNicks", FbDbType.Boolean)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("Literals", FbDbType.VarChar)).Value =
                    literals.ToString().Replace("\0", string.Empty);
                cmd.Parameters.Add(new FbParameter("Exclude", FbDbType.Boolean)).Value = exclude;
                cmd.Parameters.Add(new FbParameter("BeginsWith", FbDbType.VarChar)).Value = beginsWith;
                cmd.Parameters.Add(new FbParameter("PageIndex", FbDbType.Integer)).Value = pageIndex;
                cmd.Parameters.Add(new FbParameter("PageSize", FbDbType.Integer)).Value = pageSize;
                cmd.Parameters.Add(new FbParameter("SortName", FbDbType.Integer)).Value = sortName;
                cmd.Parameters.Add(new FbParameter("SortRank", FbDbType.Integer)).Value = sortRank;
                cmd.Parameters.Add(new FbParameter("SortJoined", FbDbType.Integer)).Value = sortJoined;
                cmd.Parameters.Add(new FbParameter("SortPosts", FbDbType.Integer)).Value = sortPosts;
                cmd.Parameters.Add(new FbParameter("SortLastVisit", FbDbType.Integer)).Value = sortLastVisit;
                cmd.Parameters.Add(new FbParameter("NumPosts", FbDbType.Integer)).Value = numPosts;
                cmd.Parameters.Add(new FbParameter("NumPostsCompare", FbDbType.Integer)).Value = numPostCompare;
                return FbDbAccess.GetData(cmd, connectionString);
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



        /// <summary>
        /// For URL Rewriting
        /// </summary>
        /// <param name="StartID"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        public static DataTable user_simplelist([NotNull] string connectionString, int StartID, int Limit)
        {
            using (var cmd = FbDbAccess.GetCommand("user_simplelist"))
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

                cmd.Parameters.Add(new FbParameter("@I_STARTID", FbDbType.Integer)).Value = StartID;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = Limit;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void user_delete([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setrole([NotNull] string connectionString, int boardID, object providerUserKey, object role)
        {
            // System.Guid guid = new System.Guid(providerUserKey.ToString());

            using (var cmd = FbDbAccess.GetCommand("user_setrole"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_PROVIDERUSERKEY", FbDbType.VarChar)).Value = providerUserKey;
                cmd.Parameters.Add(new FbParameter("@I_ROLE", FbDbType.VarChar)).Value = role;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setinfo([NotNull] string connectionString, int boardID, System.Web.Security.MembershipUser user)
        {
            using (
                var cmd =
                    FbDbAccess.GetCommand(
                        "update {databaseOwner}.objQual_USER set Name=@I_USERNAME,Email=@I_EMAIL where BoardID=@I_BOARDID and ProviderUserKey=@I_PROVIDERUSERKEY",
                        true))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = user.UserName;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = user.Email;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_PROVIDERUSERKEY", FbDbType.VarChar)).Value = user.ProviderUserKey;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_migrate(
            [NotNull] string connectionString, object userID, object providerUserKey, object updateProvider)
        {
            using (var cmd = FbDbAccess.GetCommand("user_migrate"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_PROVIDERUSERKEY", FbDbType.VarChar)).Value = providerUserKey
                                                                                                    ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_UPDATEPROVIDER", FbDbType.Boolean)).Value = updateProvider
                                                                                                   ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_deleteold([NotNull] string connectionString, [NotNull] object boardId, [NotNull] object days)
        {
            using (var cmd = FbDbAccess.GetCommand("user_deleteold"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_DAYS", FbDbType.Integer)).Value = days;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_approve([NotNull] string connectionString, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_approve"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_approveall([NotNull] string connectionString, object boardId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_approveall"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_suspend([NotNull] string connectionString, object userID, object suspend)
        {
            using (var cmd = FbDbAccess.GetCommand("user_suspend"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_SUSPEND", FbDbType.TimeStamp)).Value = suspend ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns data about allowed signature tags and character limits
        /// </summary>
        /// <param name="userId">The userID</param>
        /// <param name="boardId">The boardID</param>
        /// <returns>Data Table</returns>
        public static DataTable user_getsignaturedata(
            [NotNull] string connectionString, [NotNull] object userId, [NotNull] object boardId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getsignaturedata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns data about albums: allowed number of images and albums
        /// </summary>
        /// <param name="userID">The userID</param>
        /// <param name="boardID">The boardID</param>  
        public static DataTable user_getalbumsdata(
            [NotNull] string connectionString, [NotNull] object userID, [NotNull] object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getalbumsdata"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static bool user_changepassword(
            [NotNull] string connectionString, object userId, object oldPassword, object newPassword)
        {
            using (var cmd = FbDbAccess.GetCommand("user_changepassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_OLDPASSWORD", FbDbType.VarChar)).Value = oldPassword;
                cmd.Parameters.Add(new FbParameter("@I_NEWPASSWORD", FbDbType.VarChar)).Value = newPassword;

                return (bool)FbDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable user_pmcount([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_pmcount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void user_save(
            [NotNull] string connectionString,
            [NotNull] object userID,
            [NotNull] object boardID,
            [NotNull] object userName,
            [NotNull] object displayName,
            [NotNull] object email,
            [NotNull] object timeZone,
            [NotNull] object languageFile,
            [NotNull] object culture,
            [NotNull] object themeFile,
            [NotNull] object useSingleSignOn,
            [NotNull] object textEditor,
            [NotNull] object useMobileTheme,
            [NotNull] object approved,
            [NotNull] object pmNotification,
            [NotNull] object autoWatchTopics,
            [NotNull] object dSTUser,
            [NotNull] object hideUser,
            [NotNull] object notificationType,
            [NotNull] object topicsPerPage,
            [NotNull] object postsPerPage)
        {
            using (var cmd = FbDbAccess.GetCommand("user_save"))
            {

                if (useMobileTheme == null || useMobileTheme.ToString() == "false")
                {
                    useMobileTheme = 0;
                }
                if (useMobileTheme.ToString() == "true")
                {
                    useMobileTheme = 1;
                }
                if (approved == null || approved.ToString() == "false")
                {
                    approved = 0;
                }
                if (approved.ToString() == "true")
                {
                    approved = 1;
                }
                if (pmNotification == null)
                {
                    pmNotification = 1;
                }
                if (pmNotification.ToString() == "false")
                {
                    pmNotification = 0;
                }
                if (pmNotification.ToString() == "true")
                {
                    pmNotification = 1;
                }
                if (autoWatchTopics == null || autoWatchTopics.ToString() == "false")
                {
                    autoWatchTopics = 0;
                }
                if (autoWatchTopics.ToString() == "true")
                {
                    autoWatchTopics = 1;
                }
                if (dSTUser == null || dSTUser.ToString() == "false")
                {
                    dSTUser = 0;
                }
                if (dSTUser.ToString() == "true")
                {
                    dSTUser = 1;
                }
                if (hideUser == null || hideUser.ToString() == "false")
                {
                    hideUser = 0;
                }
                if (hideUser.ToString() == "true")
                {
                    hideUser = 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;
                cmd.Parameters.Add("@I_USERNAME", FbDbType.VarChar).Value = userName ?? DBNull.Value;
                cmd.Parameters.Add("@I_DISPLAYNAME", FbDbType.VarChar).Value = displayName ?? DBNull.Value;
                cmd.Parameters.Add("@I_EMAIL", FbDbType.VarChar).Value = email ?? DBNull.Value;
                cmd.Parameters.Add("@I_TIMEZONE", FbDbType.Integer).Value = timeZone;
                cmd.Parameters.Add("@I_LANGUAGEFILE", FbDbType.VarChar).Value = languageFile ?? DBNull.Value;
                cmd.Parameters.Add("@I_CULTURE", FbDbType.VarChar).Value = culture ?? DBNull.Value;
                cmd.Parameters.Add("@I_THEMEFILE", FbDbType.VarChar).Value = themeFile ?? DBNull.Value;
                cmd.Parameters.Add("@I_USESINGLESIGNON", FbDbType.Boolean).Value = useSingleSignOn;
                cmd.Parameters.Add("@I_TEXTEDITOR", FbDbType.VarChar).Value = textEditor;
                cmd.Parameters.Add("@I_OVERRIDEDEFAULTTHEME", FbDbType.Boolean).Value = useMobileTheme;
                cmd.Parameters.Add("@I_APPROVED", FbDbType.Boolean).Value = approved;
                cmd.Parameters.Add("@I_PMNOTIFICATION", FbDbType.Boolean).Value = pmNotification;
                cmd.Parameters.Add("@I_NOTIFICATIONTYPE", FbDbType.Integer).Value = notificationType;
                cmd.Parameters.Add("@I_AUTOWATCHTOPIC", FbDbType.Boolean).Value = autoWatchTopics;
                cmd.Parameters.Add("@I_PROVIDERUSERKEY", FbDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@I_DSTUSER", FbDbType.Boolean).Value = dSTUser;
                cmd.Parameters.Add("@I_HIDEUSER", FbDbType.Boolean).Value = hideUser;
                cmd.Parameters.Add("@I_TOPICSPERPAGE", FbDbType.Integer).Value = topicsPerPage;
                cmd.Parameters.Add("@I_POSTSPERPAGE", FbDbType.Integer).Value = postsPerPage;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;
               

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

                //object o = FbDbAccess.ExecuteScalar(cmd,connectionString);
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
            object userID,
            object pmNotification,
            object autoWatchTopics,
            object notificationType,
            object dailyDigest)
        {
            using (var cmd = FbDbAccess.GetCommand("user_savenotification"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_PMNOTIFICATION", FbDbType.Boolean).Value = pmNotification;
                cmd.Parameters.Add("@I_NOTIFICATIONTYPE", FbDbType.Integer).Value = notificationType;
                cmd.Parameters.Add("@I_AUTOWATCHTOPIC", FbDbType.Boolean).Value = autoWatchTopics;
                cmd.Parameters.Add("@I_DAILYDIGEST", FbDbType.Boolean).Value = dailyDigest;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            object rankId)
        {

            using (var cmd = FbDbAccess.GetCommand("user_adminsave"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_DISPLAYNAME", FbDbType.VarChar)).Value = displayName;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_RANKID", FbDbType.Integer)).Value = rankId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable user_emails([NotNull] string connectionString, object boardId, object groupId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_emails"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer)).Value = groupId ?? DBNull.Value;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_accessmasks([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_accessmasks"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return userforumaccess_sort_list(connectionString, FbDbAccess.GetData(cmd, connectionString), 0, 0, 0);
            }
        }

        public static DataTable user_accessmasksbygroup([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_accessmasksbygroup"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable user_accessmasksbyforum([NotNull] string connectionString, object boardID, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_accessmasksbyforum"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        //adds some convenience while editing group's access rights (indent forums)
        private static DataTable userforumaccess_sort_list(
            [NotNull] string connectionString, DataTable listSource, int parentID, int categoryID, int startingIndent)
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
            userforumaccess_sort_list_recursive(
                connectionString, dv.ToTable(), listDestination, parentID, categoryID, startingIndent);
            return listDestination;
        }

        private static void userforumaccess_sort_list_recursive(
            [NotNull] string connectionString,
            DataTable listSource,
            DataTable listDestination,
            int parentID,
            int categoryID,
            int currentIndent)
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

                    // recurse through the list...
                    userforumaccess_sort_list_recursive(
                        connectionString,
                        listSource,
                        listDestination,
                        (int)row["ForumID"],
                        categoryID,
                        currentIndent + 1);
                }
            }
        }

        public static DataTable recent_users(
            [NotNull] string connectionString, [NotNull] object boardID, int timeSinceLastLogin, [NotNull] object styledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("recent_users"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_TIMESINCELASTLOGIN", FbDbType.TimeStamp)).Value =
                    timeSinceLastLogin;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = styledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static object user_recoverpassword(
            [NotNull] string connectionString, object boardID, object userName, object email)
        {
            using (var cmd = FbDbAccess.GetCommand("user_recoverpassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;

                return FbDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static void user_savepassword([NotNull] string connectionString, object userID, object password)
        {
            using (var cmd = FbDbAccess.GetCommand("user_savepassword"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORD", FbDbType.VarChar)).Value =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToString(), "md5");

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static object user_login([NotNull] string connectionString, object boardID, object name, object password)
        {
            using (var cmd = FbDbAccess.GetCommand("user_login"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_NAME", FbDbType.VarChar)).Value = name;
                cmd.Parameters.Add(new FbParameter("@I_PASSWORD", FbDbType.VarChar)).Value = password;

                return FbDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static DataTable user_avatarimage([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_avatarimage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static int user_get([NotNull] string connectionString, int boardId, object providerUserKey)
        {
            using (var cmd = FbDbAccess.GetCommand("user_get"))
            {
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_PROVIDERUSERKEY", FbDbType.VarChar)).Value = providerUserKey;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString) ?? 0);
            }
        }

        public static DataTable UserFind(
            [NotNull] string connectionString,
            int boardID,
            bool filter,
            string userName,
            string email,
            string displayName,
            object notificationType,
            object dailyDigest)
        {
            using (var cmd = FbDbAccess.GetCommand("user_find"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardID;
                cmd.Parameters.Add("@I_FILTER", FbDbType.Boolean).Value = filter;
                cmd.Parameters.Add("@I_USERNAME", FbDbType.VarChar).Value = userName;
                cmd.Parameters.Add("@I_DISPLAYNAME", FbDbType.VarChar).Value = displayName;
                cmd.Parameters.Add("@I_EMAIL", FbDbType.VarChar).Value = email;
                cmd.Parameters.Add("@I_NOTIFICATIONTYPE", FbDbType.Integer).Value = notificationType;
                cmd.Parameters.Add("@I_DAILYDIGEST", FbDbType.Boolean).Value = dailyDigest;

                return FbDbAccess.GetData(cmd, connectionString);
                ;
            }
        }


        public static string user_getsignature([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getsignature"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;

                return FbDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        public static void user_savesignature([NotNull] string connectionString, object userID, object signature)
        {
            using (var cmd = FbDbAccess.GetCommand("user_savesignature"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_SIGNATURE", FbDbType.Text).Value = signature;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_saveavatar(
            [NotNull] string connectionString, object userID, object avatar, System.IO.Stream stream, object avatarImageType)
        {
            using (var cmd = FbDbAccess.GetCommand("user_saveavatar"))
            {
                byte[] data = null;

                if (stream != null)
                {
                    data = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.Read(data, 0, (int)stream.Length);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_AVATAR", FbDbType.VarChar).Value = avatar ?? DBNull.Value;
                cmd.Parameters.Add("@I_AVATARIMAGE", FbDbType.Binary).Value = data;
                cmd.Parameters.Add("@I_AVATARIMAGETYPE", FbDbType.VarChar).Value = avatarImageType ?? DBNull.Value;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_deleteavatar([NotNull] string connectionString, [NotNull] object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_deleteavatar"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
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
                using (var cmd = FbDbAccess.GetCommand("user_aspnet"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@I_BOARDID", FbDbType.Integer).Value = boardId;
                    cmd.Parameters.Add("@I_USERNAME", FbDbType.VarChar).Value = userName;
                    cmd.Parameters.Add("@I_DISPLAYNAME", FbDbType.VarChar).Value = displayName;
                    cmd.Parameters.Add("@I_EMAIL", FbDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@I_PROVIDERUSERKEY", FbDbType.VarChar).Value = providerUserKey;
                    cmd.Parameters.Add("@I_ISAPPROVED", FbDbType.Boolean).Value = isApproved;
                    cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                    return (int)FbDbAccess.ExecuteScalar(cmd, connectionString);
                }
            }
            catch (Exception x)
            {
                Db.eventlog_create(null, "user_aspnet in VZF.Classes.Data.DB.cs", x, EventLogTypes.Error);
                return 0;
            }
        }

        public static int? user_guest([NotNull] string connectionString, [NotNull] object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("user_guest"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                return FbDbAccess.ExecuteScalar(cmd, connectionString).ToType<int?>();
            }
        }

        public static bool user_ThankedMessage(
            [NotNull] string connectionString, [NotNull] object messageId, [NotNull] object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_thankedmessage"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                var thankCount = (int)FbDbAccess.ExecuteScalar(cmd, connectionString);

                return thankCount > 0;
            }
        }

        public static DataTable user_activity_rank(
            [NotNull] string connectionString, [NotNull] object boardId, object startDate, object displayNumber)
        {
            using (var cmd = FbDbAccess.GetCommand("user_activity_rank"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_STARTDATE", FbDbType.TimeStamp)).Value = startDate;
                cmd.Parameters.Add(new FbParameter("@I_DISPLAYNUMBER", FbDbType.Integer)).Value = displayNumber;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static int user_nntp(
            [NotNull] string connectionString, object boardID, object userName, object email, object timeZone)
        {
            using (var cmd = FbDbAccess.GetCommand("USER_NNTP"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_EMAIL", FbDbType.VarChar)).Value = email;
                cmd.Parameters.Add(new FbParameter("@I_TIMEZONE", FbDbType.Integer)).Value = timeZone;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                object o = FbDbAccess.ExecuteScalar(cmd, connectionString);
                //  if ( o != DBNull.Value)
                //  {
                return Convert.ToInt32(o);
                //  }
                //  else
                //     return -1;

            }
        }

        public static void user_addpoints(
            [NotNull] string connectionString, [NotNull] object userID, [CanBeNull] object fromUserID, [NotNull] object points)
        {

            using (var cmd = FbDbAccess.GetCommand("user_addpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_FROMUSERID", FbDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new FbParameter("@I_POINTS", FbDbType.Integer)).Value = points;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_removepoints(
            [NotNull] string connectionString, [NotNull] object userID, [CanBeNull] object fromUserID, [NotNull] object points)
        {
            using (var cmd = FbDbAccess.GetCommand("user_removepoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_FROMUSERID", FbDbType.Integer)).Value = fromUserID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                cmd.Parameters.Add(new FbParameter("@I_POINTS", FbDbType.Integer)).Value = points;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setnotdirty([NotNull] string connectionString, int boardId, int userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_setnotdirty"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void user_setpoints([NotNull] string connectionString, object userId, object points)
        {
            using (var cmd = FbDbAccess.GetCommand("user_setpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_POINTS", FbDbType.Integer)).Value = points;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static int user_getpoints([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getpoints"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

                return (int)FbDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        public static int user_getthanks_from([NotNull] string connectionString, object userID, object pageUserId)
        {

            using (var cmd = FbDbAccess.GetCommand("USER_GETTHANKS_FROM"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
            }
        }

        public static int[] user_getthanks_to([NotNull] string connectionString, object userID, object pageUserId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getthanks_to"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                FbParameter paramThanksToNumber = new FbParameter("@ThanksToNumber", 0);
                paramThanksToNumber.Direction = ParameterDirection.Output;
                FbParameter paramThanksToPostsNumber = new FbParameter("@ThanksToPostsNumber", 0);
                paramThanksToPostsNumber.Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("@I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add(paramThanksToNumber);
                cmd.Parameters.Add(paramThanksToPostsNumber);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

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
        /// Returns the posts which is thanked by the user + the posts which are posted by the user and 
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
            using (var cmd = FbDbAccess.GetCommand("user_viewthanksto"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_USERID", FbDbType.Integer).Value = UserID;
                cmd.Parameters.Add("I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("I_PAGEINDEX", FbDbType.Integer).Value = pageIndex;
                cmd.Parameters.Add("I_PAGESIZE", FbDbType.Integer).Value = pageSize;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns the posts which is thanked by the user + the posts which are posted by the user and 
        /// are thanked by other users.
        /// </summary>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        /// <returns>
        /// </returns>
        public static DataTable user_viewthanksfrom(
            [NotNull] string connectionString, object UserID, object pageUserId, int pageIndex, int pageSize)
        {
            using (var cmd = FbDbAccess.GetCommand("user_viewthanksfrom"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_USERID", FbDbType.Integer).Value = UserID;
                cmd.Parameters.Add("I_PAGEUSERID", FbDbType.Integer).Value = pageUserId;
                cmd.Parameters.Add("I_PAGEINDEX", FbDbType.Integer).Value = pageIndex;
                cmd.Parameters.Add("I_PAGESIZE", FbDbType.Integer).Value = pageSize;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("USER_UPDATE_SSN_STATUS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_USERID", FbDbType.Integer).Value = userID;
                cmd.Parameters.Add("I_ISFACEBOOKUSER", FbDbType.Boolean).Value = isFacebookUser;
                cmd.Parameters.Add("I_ISTWITTERUSER", FbDbType.Boolean).Value = isTwitterUser;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

            }
        }

        #endregion

        #region yaf_UserForum

        /// <summary>
        /// The userforum_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        public static void userforum_delete([NotNull] string connectionString, [NotNull] object userId, object forumId)
        {
            using (var cmd = FbDbAccess.GetCommand("userforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The userforum_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable userforum_list([NotNull] string connectionString, object userId, object forumId)
        {
            using (var cmd = FbDbAccess.GetCommand("userforum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId ?? DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId ?? DBNull.Value;
                
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The userforum_save.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="accessMaskId">
        /// The access mask id.
        /// </param>
        public static void userforum_save([NotNull] string connectionString, [NotNull] object userId, [NotNull] object forumId, [NotNull] object accessMaskId)
        {
            using (var cmd = FbDbAccess.GetCommand("userforum_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumId;
                cmd.Parameters.Add(new FbParameter("@I_ACCESSMASKID", FbDbType.Integer)).Value = accessMaskId;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_UserGroup

        public static DataTable usergroup_list([NotNull] string connectionString, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("usergroup_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void usergroup_save([NotNull] string connectionString, object userID, object groupID, object member)
        {
            using (var cmd = FbDbAccess.GetCommand("usergroup_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;

                cmd.Parameters.Add(new FbParameter("@I_GROUPID", FbDbType.Integer));
                cmd.Parameters[1].Value = groupID;

                cmd.Parameters.Add(new FbParameter("@I_MEMBER", FbDbType.Boolean));
                cmd.Parameters[2].Value = Convert.ToBoolean(member);


                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        #endregion

        #region yaf_WatchForum

        public static void watchforum_add([NotNull] string connectionString, object userID, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchforum_add"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static DataTable watchforum_list([NotNull] string connectionString, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchforum_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;



                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable watchforum_check([NotNull] string connectionString, object userID, object forumID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchforum_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer));
                cmd.Parameters[1].Value = forumID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void watchforum_delete([NotNull] string connectionString, object watchForumID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_WATCHFORUMID", FbDbType.Integer));
                cmd.Parameters[0].Value = watchForumID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

            }
        }

        #endregion

        #region yaf_WatchTopic

        public static DataTable watchtopic_list([NotNull] string connectionString, object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchtopic_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable watchtopic_check([NotNull] string connectionString, object userID, object topicID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchtopic_check"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer));
                cmd.Parameters[0].Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer));
                cmd.Parameters[1].Value = topicID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static void watchtopic_delete([NotNull] string connectionString, object watchTopicID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchtopic_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_WATCHTOPICID", FbDbType.Integer));
                cmd.Parameters[0].Value = watchTopicID;


                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void watchtopic_add([NotNull] string connectionString, object userID, object topicID)
        {
            using (var cmd = FbDbAccess.GetCommand("watchtopic_add"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("readtopic_addorupdate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("i_topicid", FbDbType.Integer)).Value = topicID;
                cmd.Parameters.Add(new FbParameter("I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete the Read Tracking
        /// </summary>
        /// <param name="trackingID">
        /// The tracking id.
        /// </param>
        /*public static void Readtopic_delete([NotNull]  object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("readtopic_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;
                FbDbAccess.ExecuteNonQuery(cmd,connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("user_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;

                var tableLastRead = FbDbAccess.ExecuteScalar(cmd, connectionString);

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
            using (var cmd = FbDbAccess.GetCommand("readtopic_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("i_topicid", FbDbType.Integer)).Value = topicID;

                var tableLastRead = FbDbAccess.ExecuteScalar(cmd, connectionString);

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
            using (var cmd = FbDbAccess.GetCommand("readforum_addorupdate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("i_forumid", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Delete the Read Tracking
        /// </summary>
        /// <param name="trackingID">
        /// The tracking id.
        /// </param>
        public static void ReadForum_delete([NotNull] string connectionString, [NotNull] object trackingID)
        {
            using (var cmd = FbDbAccess.GetCommand("readforum_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_trackingid", FbDbType.Integer)).Value = trackingID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

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
            using (var cmd = FbDbAccess.GetCommand("readforum_lastread"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("i_userid", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("i_forumid", FbDbType.Integer)).Value = forumID;

                var tableLastRead = FbDbAccess.ExecuteScalar(cmd, connectionString);

                return tableLastRead != null && tableLastRead != DBNull.Value
                           ? (DateTime)tableLastRead
                           : DateTime.MinValue.AddYears(1902);
            }
        }

        #endregion

        #region vzrus addons

        public static string btnGetStatsName
        {
            get
            {
                return "Recalculate YAF Table Index Statistics";
            }
        }

        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }

        public static string btnReindexName
        {
            get
            {
                return "Recreate YAF Tables indice";
            }
        }

        //DB Maintenance page buttons name

        public static string btnShrinkName
        {
            get
            {
                return "Shrink Database";
            }
        }

        public static string btnRecoveryModeName
        {
            get
            {
                return "Set Recovery Mode";
            }
        }

        //DB Maintenance page panels visibility
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

        #endregion

        #region reindex page controls


        public static DataTable rsstopic_list([NotNull] string connectionString, int forumID, int start, int limit)
        {
            using (var cmd = FbDbAccess.GetCommand("rsstopic_list"))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_FORUMID", FbDbType.Integer)).Value = forumID;
                cmd.Parameters.Add(new FbParameter("@I_START", FbDbType.Integer)).Value = start;
                cmd.Parameters.Add(new FbParameter("@I_LIMIT", FbDbType.Integer)).Value = limit;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        private static DataTable db_index_simplelist([NotNull] string connectionString)
        {

            using (
                var cmd =
                    FbDbAccess.GetCommand(
                        String.Format(
                            "SELECT a.RDB$INDEX_NAME FROM RDB$INDICES a WHERE a.RDB$FOREIGN_KEY IS NULL AND a.RDB$SYSTEM_FLAG=0 AND a.RDB$UNIQUE_FLAG IS NULL AND a.RDB$RELATION_NAME LIKE '%{0}%'",
                            FbDbAccess.ObjectQualifier.ToUpper()),
                        true))
            {
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        private static string getStatsMessage;

        /// <summary>
        /// The db_getstats_new.
        /// </summary>
        public static string db_getstats_new([NotNull] string connectionString)
        {
            try
            {
                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(getStats_InfoMessage);

                    DataTable indexList = Db.db_index_simplelist(connectionString);
                    foreach (DataRow indexName in indexList.Rows)
                    {
                        using (var cmd1 = new FbCommand(String.Format("SET STATISTICS INDEX {0}", indexName[0])))
                        {
                            cmd1.CommandType = CommandType.Text;
                            // up the command timeout...
                            cmd1.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                            // run it...
                            FbDbAccess.ExecuteNonQuery(cmd1, false, connectionString);
                        }
                    }

                    return getStatsMessage;
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

        /* public static void db_getstats([NotNull] string connectionString, FbDbConnectionManager conn)
        {
            DataTable indexList = Db.db_index_simplelist(connectionString);
            foreach (DataRow indexName in indexList.Rows)
            {
                using (var cmd1 = new FbCommand(String.Format("SET STATISTICS INDEX {0}", indexName[0])))
                {
                    cmd1.CommandType = CommandType.Text;
                    // up the command timeout...
                    cmd1.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                    // run it...
                    FbDbAccess.ExecuteNonQuery(cmd1, false, connectionString);
                }
            }
        } */

        public static string db_getstats_warning()
        {
            return "Recalculate index statistics is made or in progress.";
        }

        private static string reindexDbMessage;

        public static string db_reindex_new([NotNull] string connectionString)
        {
            DataTable indexList = Db.db_index_simplelist(connectionString);
            foreach (DataRow indexName in indexList.Rows)
            {

                // using (var cmd = new FbCommand(String.Format("EXECUTE BLOCK AS BEGIN EXECUTE STATEMENT 'ALTER INDEX {0} INACTIVE'; EXECUTE STATEMENT 'ALTER INDEX {0} ACTIVE';END", indexName[0])))
                try
                {
                    using (var connMan = new FbDbConnectionManager(connectionString))
                    {
                        connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(reindexDb_InfoMessage);
                        using (
                            var cmd =
                                new FbCommand(
                                    String.Format(
                                        "EXECUTE BLOCK AS BEGIN EXECUTE STATEMENT 'ALTER INDEX {0} INACTIVE'; END",
                                        indexName[0])))
                        {
                            cmd.CommandType = CommandType.Text;
                            // up the command timeout...
                            cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                            // run it...
                            FbDbAccess.ExecuteNonQuery(cmd, false, connectionString);
                        }
                    }
                }
                finally
                {

                }
            }
            string reindexDbMessageRet = reindexDbMessage;
            reindexDbMessage = string.Empty;
            return reindexDbMessageRet;
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
            return "Indexes recreating.";
        }

        private static string messageRunSql;

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
        public static string db_runsql_new([NotNull] string connectionString, string sql, bool useTransaction)
        {
            var results = new System.Text.StringBuilder();

            try
            {
                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    connMan.InfoMessage += new YafDBConnInfoMessageEventHandler(fb_runSql_InfoMessage);

                    using (var cmd = new FbCommand(sql, connMan.OpenDBConnection(connectionString)))
                    {
                        cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                        FbDataReader reader = null;

                        using (
                            FbTransaction trans =
                                connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                        {
                            try
                            {
                                cmd.Connection = connMan.DBConnection(connectionString);
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
                                        results.Append(drd["ColumnName"].ToString());
                                        gg++;

                                    }

                                    results.AppendLine();

                                    while (reader.Read())
                                    {
                                        results.AppendFormat(@"""{0}""", rowIndex++);

                                        // dump all columns...
                                        foreach (var col in columnNames)
                                        {
                                            results.AppendFormat(
                                                @",""{0}""", reader[col].ToString().Replace("\"", "\"\""));
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

                                // rollback...
                                trans.Rollback();
                                results.AppendLine();
                                results.AppendFormat("SQL ERROR: {0}", x);
                            }
                            if (reader != null)
                            {
                                reader.Close();
                            }
                            return results.ToString();
                        }
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
        private static void fb_runSql_InfoMessage([NotNull] object sender, [NotNull] YafDBConnInfoMessageEventArgs e)
        {
            messageRunSql = "\r\n" + e.Message;
        }

        public static bool forumpage_initdb([NotNull] string connectionString, [NotNull] out string errorStr, bool debugging)
        {
            errorStr = null;
            try
            {

                using (var connMan = new FbDbConnectionManager(connectionString))
                {
                    // just attempt to open the connection to test if a DB is available.           
                    FbConnection getConn = connMan.OpenDBConnection(connectionString);
                }

                return true;
            }

            catch (FbException ex)
            {
                // unable to connect to the DB...
                if (debugging)
                {
                    errorStr = "Unable to connect to the Database. Exception Message: " + ex.Message + " ("
                               + ex.ErrorCode.ToString() + ")";

                }
                else
                {
                    // re-throw since we are debugging... 

                    throw;
                }

                return false;
            }

        }

        public static string forumpage_validateversion([NotNull] string connectionString, int? mid, int appVersion)
        {
            string redirect = string.Empty;
            try
            {
                DataTable registry = Db.registry_list(connectionString, "Version");

                if ((registry.Rows.Count == 0) || (Convert.ToInt32(registry.Rows[0]["VALUE"]) < appVersion))
                {
                    // needs upgrading...
                    redirect =
                        "install/default.aspx?upgrade={0}&md={1}".FormatWith(
                            registry.Rows.Count != 0 ? Convert.ToInt32(registry.Rows[0]["Value"]) : 0, mid ?? 1);
                }
            }
            catch (FbException)
            {
                // needs to be setup...
                redirect = "install/";
            }
            return redirect;
        }

        // MS SQL Support fulltext....
        private static bool _fullTextSupported;

        public static bool FullTextSupported
        {
            get
            {
                return _fullTextSupported;
            }
            set
            {
                _fullTextSupported = value;
            }
        }

        private static string _fullTextScript = "firebird/fulltext.sql";

        public static string FullTextScript
        {
            get
            {
                return _fullTextScript;
            }
            set
            {
                _fullTextScript = value;
            }
        }


        private static readonly string[] _scriptList =
            {
                "firebird/procedures_drop.sql",
                "firebird/providers/procedures_drop.sql",
                "firebird/functions_drop.sql", "firebird/test_drop.sql",
                "firebird/views_drop.sql", "firebird/exceptions_drop.sql",
                "firebird/domains.sql", "firebird/sequences.sql",
                "firebird/tables.sql", "firebird/tablesupgrade.sql", "firebird/pkeys.sql",
                "firebird/indexes.sql", "firebird/ukeys.sql",
                "firebird/fkeys.sql", "firebird/triggers.sql",
                "firebird/views.sql", "firebird/exceptions.sql",
                "firebird/functions.sql", "firebird/providers/tables.sql",
                "firebird/providers/pkeys.sql",
                "firebird/providers/indexes.sql",
                "firebird/providers/procedures.sql", 
                // "firebird/nestedsets.sql",                                     
                "firebird/procedures.sql", "firebird/procedures1.sql",
                "firebird/procedures2.sql"
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

        public static void system_deleteinstallobjects([NotNull] string connectionString)
        {
            string tSQL = "DROP FUNCTION" + FbDbAccess.GetObjectName("system_initialize");
            using (var cmd = FbDbAccess.GetCommand(tSQL, true))
            {
                cmd.CommandType = CommandType.Text;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        public static void system_initialize_executescripts(
            [NotNull] string connectionString, string script, string scriptFile, bool useTransactions)
        {
            CreateDatabase(connectionString);
            script = FbDbAccess.GetCommandTextReplaced(script);
            // apply database owner
            if (!String.IsNullOrEmpty(FbDbAccess.SchemaName))
            {
                script = script.Replace("dbN", FbDbAccess.DBName.ToUpper());
            }
            else
            {
                script = script.Replace("dbN", "YAFNET");
            }

            // apply grantee name
            if (!String.IsNullOrEmpty(FbDbAccess.GranteeName))
            {
                script = script.Replace("grantName", FbDbAccess.GranteeName.ToUpper());
            }
            else
            {
                script = script.Replace("grantName", "PUBLIC");
            }
            // apply host name
            script = script.Replace("hostName", FbDbAccess.HostName);



            //Scripts separation regexp
            string[] statements = System.Text.RegularExpressions.Regex.Split(
                script, "(?:--GO)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Here comes add SET ARITHABORT ON for MSSQL amd Linq class
            // statements.Insert(0, "SET ARITHABORT ON");

            using (var connMan = new FbDbConnectionManager(connectionString))
            {

                // use transactions...
                if (useTransactions)
                {
                    using (
                        FbTransaction trans =
                            connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                    {
                        foreach (string sql0 in statements)
                        {
                            string sql = sql0.Trim();

                            try
                            {
                                if (sql.ToLower().IndexOf("setuser") >= 0) continue;

                                if (sql.Length > 0)
                                {
                                    using (var cmd = new FbCommand())
                                    {
                                        cmd.Transaction = trans;
                                        cmd.Connection = connMan.DBConnection(connectionString);
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = sql.Trim();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception x)
                            {
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
                    foreach (string sql0 in statements)
                    {
                        string sql = sql0.Trim();

                        try
                        {
                            if (sql.ToLower().IndexOf("setuser") >= 0) continue;

                            if (sql.Length > 0)
                            {
                                using (var cmd = new FbCommand())
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
            // USED FOR UPGRADE FROM VERY OLD VERSIONS

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
            object rolePrefix)
        {
            string gs = providerUserKey.ToString();

            using (var cmd = FbDbAccess.GetCommand("SYSTEM_INITIALIZE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // added so command won't timeout anymore...
                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);
                cmd.Parameters.Add("@I_NAME", FbDbType.VarChar).Value = forumName;
                cmd.Parameters.Add("@I_TIMEZONE", FbDbType.Integer).Value = timeZone;
                cmd.Parameters.Add("@I_CULTURE", FbDbType.VarChar, 10).Value = culture;
                cmd.Parameters.Add("@I_LANGUAGEFILE", FbDbType.VarChar).Value = languageFile;
                cmd.Parameters.Add("@I_FORUMEMAIL", FbDbType.VarChar).Value = forumEmail;
                cmd.Parameters.Add("@I_SMTPSERVER", FbDbType.VarChar).Value = smtpServer;
                cmd.Parameters.Add("@I_USER", FbDbType.VarChar).Value = userName;
                cmd.Parameters.Add("@I_USEREMAIL", FbDbType.VarChar).Value = userEmail;
                // vzrus:The input parameter should be implemented in the system initialize and board_create procedures, else there will be an error in create watch because the user email is missing
                if (gs.IsSet()) cmd.Parameters.Add("@I_USERKEY", FbDbType.VarChar).Value = gs;
                else cmd.Parameters.Add("@I_USERKEY", FbDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@I_ROLEPREFIX", FbDbType.VarChar).Value = rolePrefix;
                cmd.Parameters.Add("@I_UTCTIMESTAMP", FbDbType.TimeStamp).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

            }


        }

        public static void system_updateversion([NotNull] string connectionString, int version, string name)
        {
            using (var cmd = FbDbAccess.GetCommand("SYSTEM_UPDATEVERSION"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_VERSION", FbDbType.Integer)).Value = version;
                cmd.Parameters.Add(new FbParameter("@I_VERSIONNAME", FbDbType.VarChar)).Value = name;
              
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Returns info about all Groups and Rank styles. 
        /// Used in GroupRankStyles cache.
        /// Usage: LegendID = 1 - Select Groups, LegendID = 2 - select Ranks by Name 
        /// </summary>
        public static DataTable group_rank_style([NotNull] string connectionString, object boardID)
        {
            using (var cmd = FbDbAccess.GetCommand("group_rank_style"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        public static void CreateDatabase([NotNull] string connectionString)
        {
            CreateDatabase(connectionString, true);
        }

        public static void CreateDatabase([NotNull] string connectionString, bool embeded)
        {
            //FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
            // csb.ServerType = FbServerType.Embedded;
            // csb.Database = @I_"C:\Documents and Settings\bob\My Documents\Projects\yaffirebird\YetAnotherForum.NET\App_Data\yafnet.fdb";
            // csb.UserID = "SYSDBA";
            // csb.Password = "myfirebird";

            //if (System.IO.File.Exists(csb.Database))
            // {
            //System.IO.File.Delete(csb.Database);

            //  }
            // FbConnection.CreateDatabase(csb.ToString());  
        }

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
            using (var cmd = FbDbAccess.GetCommand("SHOUTBOX_GETMESSAGES"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_NUMBEROFMESSAGES", FbDbType.Integer)).Value = numberOfMessages;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static bool shoutbox_savemessage(
            [NotNull] string connectionString, int boardId, string message, string userName, int userID, object ip)
        {
            using (var cmd = FbDbAccess.GetCommand("SHOUTBOX_SAVEMESSAGE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.Integer)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_DATE", FbDbType.TimeStamp)).Value = DBNull.Value;
                cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);

                return true;
            }
        }

        public static Boolean shoutbox_clearmessages([NotNull] string connectionString, int boardId)
        {
            using (var cmd = FbDbAccess.GetCommand("SHOUTBOX_CLEARMESSAGES"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return true;
            }
        }

        #endregion

        #region Touradg Mods

        //Shinking Operation
        public static string db_shrink_warning()
        {
            return null;
        }

        public static string db_shrink_new([NotNull] string connectionString)
        {
            /* String ShrinkSql = "DBCC SHRINKDATABASE(N'" + DBName.DBConnection.Database + "')";
            FbConnection ShrinkConn = new FbConnection(VZF.Classes.Config.ConnectionString);
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
            return string.Empty;
        }

        //Set Recovery
        public static string db_recovery_mode_warning()
        {
            return string.Empty;
        }

        public static string db_recovery_mode_new([NotNull] string connectionString, string dbRecoveryMode)
        {
            /* String RecoveryMode = "ALTER DATABASE " + DBName.DBConnection.Database + " SET RECOVERY " + dbRecoveryMode;
             FbConnection RecoveryModeConn = new FbConnection(VZF.Classes.Config.ConnectionString);
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
        public static string[] buddy_addrequest(
            [NotNull] string connectionString, [NotNull] object FromUserID, [NotNull] object ToUserID)
        {
            using (var cmd = FbDbAccess.GetCommand("BUDDY_ADDREQUEST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_FROMUSERID", FbDbType.VarChar, 128).Value = FromUserID;
                cmd.Parameters.Add("@I_TOUSERID", FbDbType.VarChar, 128).Value = ToUserID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                DataRow drow = FbDbAccess.GetData(cmd, connectionString).Rows[0];
                return new string[] { drow["I_PARAMOUTPUT"].ToString(), drow["I_APPROVED"].ToString() };
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
            [NotNull] string connectionString, [NotNull] object FromUserID, [NotNull] object ToUserID, [NotNull] object Mutual)
        {
            using (var cmd = FbDbAccess.GetCommand("BUDDY_APPROVEREQUEST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                cmd.Parameters.Add("@I_TOUSERID", FbDbType.Integer).Value = ToUserID;
                cmd.Parameters.Add("@I_MUTUAL", FbDbType.Boolean).Value = Mutual;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                var paramOutput = new FbParameter("@I_PARMOUTPUT", FbDbType.VarChar, 128);
                paramOutput.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(paramOutput);

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("BUDDY_DENYREQUEST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new FbParameter("@I_PARAMOUTPUT", FbDbType.VarChar, 255);
                paramOutput.Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                cmd.Parameters.Add("@I_TOUSERID", FbDbType.Integer).Value = ToUserID;
                cmd.Parameters.Add(paramOutput);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("BUDDY_REMOVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var paramOutput = new FbParameter("@I_PARAMOUTPUT", FbDbType.VarChar, 128);
                paramOutput.Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                cmd.Parameters.Add("@I_TOUSERID", FbDbType.Integer).Value = ToUserID;
                cmd.Parameters.Add(paramOutput);
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
                return paramOutput.Value.ToString();
            }
        }

        /// <summary>
        /// Gets all the buddies of a certain user.
        /// </summary>
        /// <param name="FromUserID">
        /// The from user id.
        /// </param>
        /// <param name="JustApproved">
        /// Return only the approved users?
        /// </param>
        /// <returns>
        /// a Datatable containing the buddy list.
        /// </returns>
        public static DataTable buddy_list([NotNull] string connectionString, object FromUserID)
        {
            using (var cmd = FbDbAccess.GetCommand("BUDDY_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@I_FROMUSERID", FbDbType.Integer).Value = FromUserID;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        #endregion

        /// <summary>
        /// The activeaccess_reset.
        /// </summary>
        public static void activeaccess_reset([NotNull] string connectionString)
        {
            using (var cmd = FbDbAccess.GetCommand("activeaccess_reset"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        private static DataTable GetTableColumnsInfo([NotNull] string connectionString, string tableName)
        {
            string sql = @"SELECT FIRST 1 * FROM {0}".FormatWith(FbDbAccess.GetObjectName("UserProfile"));

            // using (var cmd = FbDbAccess.GetCommand("DBINFO_TABLE_COLUMNS_INFO"))
            using (var cmd = FbDbAccess.GetCommand(sql, true))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TABLENAME", FbDbType.Integer)).Value = tableName;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        public static DataTable User_ListTodaysBirthdays(
            [NotNull] string connectionString, [NotNull] object boardID, [CanBeNull] object useStyledNicks)
        {
            // Profile columns cannot yet exist when we first are gettinng data.
            try
            {
                var sqlBuilder = new StringBuilder(string.Empty);
                    // @"EXECUTE BLOCK (I_STYLEDNICKS BOOL = :I_STYLEDNICKS,I_BOARDID INTEGER = :I_BOARDID, I_UTCTIMESTAMP TIMESTAMP = :I_UTCTIMESTAMP, I_UTCTIMESTAMP2 TIMESTAMP = :I_UTCTIMESTAMP) RETURNS (""Birthday"" TIMESTAMP, ""UserName"" VARCHAR(255), ""UserDisplayName"" VARCHAR(255),""Style"" VARCHAR(255)) ");
                // objQual_DBINFO_TABLE_COLUMNS_INFO
                var dt = GetTableColumnsInfo(connectionString, FbDbAccess.GetObjectName("USERPROFILE"));
                /*  foreach (DataRow dr in dt.Rows)
                  {
                      if (dr["ActualTypeName"].ToString() == "blob")
                      {
                          string subType = dr["ActualTypeName"].ToString() == "text"
                                               ? "BLOB SUB_TYPE 1"
                                               : "BLOB SUB_TYPE 0";
                          sqlBuilder.AppendFormat("\"{0}\" {1},", dr["FieldName"], subType);
                      }
                      else
                      {
                          string subType = dr["ActualTypeName"].ToString();
                          if (dr["FieldCharacterLength"] != DBNull.Value)
                          {
                              subType = subType + "(" + dr["FieldCharacterLength"] + ")";
                          }

                          sqlBuilder.AppendFormat("\"{0}\" {1},", dr["FieldName"], subType);
                      }
                  } */

                sqlBuilder = new StringBuilder();
                sqlBuilder.Append(
                    "SELECT up.Birthday, u.USERID as \"UserID\", u.TimeZone as \"TimeZone\", u.LastVisit as \"LastVisit\", u.Name as \"UserName\",u.DisplayName as \"UserDisplayName\",(case(?) when 1 then  u.USERSTYLE ");
                sqlBuilder.Append(" else '' end) AS Style ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append(FbDbAccess.GetObjectName("UserProfile"));
                sqlBuilder.Append(" up JOIN ");
                sqlBuilder.Append(FbDbAccess.GetObjectName("USER"));
                sqlBuilder.Append(" u ON u.USERID = up.USERID ");
                sqlBuilder.Append(
                    " where u.BOARDID = ?  AND DATEADD( (? - EXTRACT(YEAR FROM up.Birthday)) year to up.Birthday) BETWEEN ? AND  ? ;"
                    + ";");

                using (var cmd = FbDbAccess.GetCommand(sqlBuilder.ToString(), true))
                {
                    cmd.Parameters.Add(new FbParameter(":I_STYLEDNICKS", FbDbType.Boolean)).Value = useStyledNicks;
                    cmd.Parameters.Add(new FbParameter(":I_BOARDID", FbDbType.Integer)).Value = boardID;
                    cmd.Parameters.Add(new FbParameter(":I_THISYEAR", FbDbType.Integer)).Value =
                        DateTime.UtcNow.Date.Year;
                    cmd.Parameters.Add(new FbParameter(":I_UTCTIMESTAMP1", FbDbType.Date)).Value =
                        DateTime.UtcNow.Date.AddDays(-1);
                    cmd.Parameters.Add(new FbParameter(":I_UTCTIMESTAMP2", FbDbType.Date)).Value =
                        DateTime.UtcNow.Date.AddDays(1);
                    return FbDbAccess.GetData(cmd, connectionString);
                }
            }
            catch (Exception e)
            {
                Db.eventlog_create(connectionString, null, e.Source, e.Message, EventLogTypes.Error);
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
                var sqlBuilder =
                    new StringBuilder("EXECUTE BLOCK (I_STYLEDNICKS BOOL = ?, I_UTCTIMESTAMP TIMESTAMP = ?) RETURNS (");
                // objQual_DBINFO_TABLE_COLUMNS_INFO
                var dt = GetTableColumnsInfo(connectionString, FbDbAccess.GetObjectName("USERPROFILE"));
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ActualTypeName"].ToString() == "blob")
                    {
                        string subType = dr["ActualTypeName"].ToString() == "text"
                                             ? "BLOB SUB_TYPE 1"
                                             : "BLOB SUB_TYPE 0";
                        sqlBuilder.AppendFormat("\"{0}\" {1},", dr["FieldName"], subType);
                    }
                    else
                    {
                        string subType = dr["ActualTypeName"].ToString();
                        if (dr["FieldCharacterLength"] != DBNull.Value)
                        {
                            subType = subType + "(" + dr["FieldCharacterLength"] + ")";
                        }

                        sqlBuilder.AppendFormat("\"{0}\" {1},", dr["FieldName"], subType);
                    }
                }

                sqlBuilder = new StringBuilder(sqlBuilder.ToString().Trim(','));
                sqlBuilder.Append(") ");
                sqlBuilder.Append(
                    " AS BEGIN SELECT up.*, u.Name as UserName,u.DisplayName as UserDisplayName, (case(?) when 1 then  u.USERSTYLE ");
                sqlBuilder.Append("  else '' end) AS Style ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append(FbDbAccess.GetObjectName("UserProfile"));
                sqlBuilder.Append(" up JOIN ");
                sqlBuilder.Append(FbDbAccess.GetObjectName("USER"));
                sqlBuilder.Append(" u ON u.USERID = up.USERID ");
                sqlBuilder.AppendFormat(" where u.BOARDID = ? AND u.UserID IN ({0})  ", stIds);
                using (var cmd = FbDbAccess.GetCommand(sqlBuilder.ToString(), true))
                {
                    cmd.Parameters.Add("@I_STYLEDNICKS", FbDbType.Boolean).Value = useStyledNicks;
                    cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardID;
                    return FbDbAccess.GetData(cmd, connectionString);
                }
            }
            catch (Exception e)
            {
                Db.eventlog_create(connectionString, null, e.Source, e.Message, EventLogTypes.Error);
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
        /// <param name="userName">
        /// The user name.
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
            if (userId <= 0 || userIdG == userId || collection.Count < 1)
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

            // load the data for the configuration
            List<SettingsPropertyColumn> spc = LoadFromPropertyValueCollection(connectionString, collection);

            if (spc != null && spc.Count > 0 && userName.IsSet())
            {
                // start saving...
                Db.SetProfileProperties(connectionString, boardId, appname, userId, userName, collection, spc, dirtyOnly);
            }
        }

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        /// <param name="dirtyOnly">
        /// The dirty only.
        /// </param>
        public static void SetProfileProperties(
            [NotNull] string connectionString,
            [NotNull] int boardId,
            [NotNull] object appName,
            [NotNull] int userId,
            [NotNull] string userName,
            [NotNull] SettingsPropertyValueCollection values,
            [NotNull] List<SettingsPropertyColumn> settingsColumnsList,
            bool dirtyOnly)
        {
            using (var conn = new FbDbConnectionManager(connectionString).OpenDBConnection(connectionString))
            {
                var cmd = new FbCommand { Connection = conn };

                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ApplicationName", appName);
                string table = FbDbAccess.GetObjectName("UserProfile");
                var sqlCommand = new StringBuilder();
                /* StringBuilder sqlCommand = new StringBuilder("EXECUTE BLOCK AS BEGIN IF (EXISTS (SELECT 1 FROM ").Append(table);
                 sqlCommand.Append(" WHERE USERID = @UserID AND ApplicationName = @ApplicationName)) THEN "); 
               
                 cmd.Parameters.AddWithValue("@UserID", userID);
                 cmd.Parameters.AddWithValue("@ApplicationName", appName); */

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
                        string valueParam = "@Value" + count;
                        valueStr.Append(valueParam);
                        cmd.Parameters.AddWithValue(valueParam, values[column.Settings.Name].PropertyValue);

                        if ((column.DataType != FbDbType.TimeStamp) || column.Settings.Name != "LastUpdatedDate"
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
                valueStr.Append(",@LastUpdatedDate");
                setStr.Append(",LastUpdatedDate=@LastUpdatedDate");
                cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow);

                // MembershipUser mu = System.Web.Security.Membership.GetUser(userID);

                columnStr.Append(",LastActivity ");
                valueStr.Append(",@LastActivity");
                setStr.Append(",LastActivity=@LastActivity");
                cmd.Parameters.AddWithValue("@LastActivity", DateTime.UtcNow);

                columnStr.Append(",ApplicationName ");
                valueStr.Append(",@ApplicationName");
                setStr.Append(",ApplicationName=@ApplicationName");
                // cmd.Parameters.AddWithValue("@ApplicationID", appId);

                columnStr.Append(",IsAnonymous ");
                valueStr.Append(",@IsAnonymous");
                setStr.Append(",IsAnonymous=@IsAnonymous");
                cmd.Parameters.AddWithValue("@IsAnonymous", 0);

                columnStr.Append(",UserName ");
                valueStr.Append(",@UserName");
                setStr.Append(",UserName=@UserName");
                cmd.Parameters.AddWithValue("@UserName", userName);
                /* if (profileExists)
                 {
                     sqlCommand.Append("UPDATE ").Append(table).Append(" SET ").Append(setStr.ToString());
                     sqlCommand.Append(" WHERE UserId = ").Append(userID.ToString()).Append(";");
                 }
                 else
                 {
                     sqlCommand.Append("INSERT INTO ").Append(table).Append(" (UserId").Append(columnStr.ToString());
                     sqlCommand.Append(") VALUES (").Append(userID.ToString()).Append("").Append(valueStr.ToString()).Append(
                       ");");
                 } */

                sqlCommand.Append("UPDATE OR INSERT INTO ")
                          .Append(table)
                          .Append(" (USERID")
                          .Append(columnStr.ToString());
                sqlCommand.Append(") VALUES (@UserID")
                          .Append(string.Empty)
                          .Append(valueStr.ToString())
                          .Append(") MATCHING (USERID);");

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
        public static DataTable GetProfileStructure([NotNull] string connectionString)
        {
            string sql = @"SELECT FIRST 1 * FROM {0}".FormatWith(FbDbAccess.GetObjectName("UserProfile"));

            using (var cmd = FbDbAccess.GetCommand(sql, true))
            {
                cmd.CommandType = CommandType.Text;
                return FbDbAccess.GetData(cmd, connectionString);
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
            [NotNull] string connectionString, [NotNull] string name, FbDbType columnType, int size)
        {
            // get column type...
            string type = columnType.ToString();

            if (size > 0)
            {
                type += "(" + size + ")";
            }

            string sql = "ALTER TABLE {0} ADD {1} {2} ".FormatWith(FbDbAccess.GetObjectName("UserProfile"), name, type);

            using (var cmd = FbDbAccess.GetCommand(sql, true))
            {
                cmd.CommandType = CommandType.Text;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
        private static bool GetDbTypeAndSizeFromString(string providerData, out FbDbType dbType, out int size)
        {
            size = -1;
            dbType = FbDbType.VarChar;

            if (providerData.IsNotSet())
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new[] { ';' });

            // first item is the column name...
            string columnName = chunk[0];
            // vzrus addon convert values from mssql types...
            if (chunk[1].IndexOf("varchar") >= 0)
            {
                chunk[1] = "VarChar";
            }
            if (chunk[1].IndexOf("int") >= 0)
            {
                chunk[1] = "Integer";
            }
            if (chunk[1].IndexOf("DateTime") >= 0)
            {
                chunk[1] = "TimeStamp";
            }

            // get the datatype and ignore case...
            dbType = (FbDbType)Enum.Parse(typeof(FbDbType), chunk[1], true);

            if (chunk.Length > 2)
            {
                // handle size...
                if (!Int32.TryParse(chunk[2], out size))
                {
                    throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
                }
            }

            return true;
        }

        public static List<SettingsPropertyColumn> LoadFromPropertyValueCollection(
            [NotNull] string connectionString, SettingsPropertyValueCollection collection)
        {
            List<SettingsPropertyColumn> settingsColumnsList = new List<SettingsPropertyColumn>();
            // clear it out just in case something is still in there...

            // validiate all the properties and populate the internal settings collection
            foreach (SettingsPropertyValue value in collection)
            {
                var tempProperty = value.Property.Attributes["CustomProviderData"];

                if (tempProperty == null)
                {
                    continue;
                }

                FbDbType dbType;
                int size;

                // parse custom provider data...
                GetDbTypeAndSizeFromString(
                    value.Property.Attributes["CustomProviderData"].ToString(), out dbType, out size);

                // default the size to 256 if no size is specified
                if (dbType == FbDbType.VarChar && size == -1)
                {
                    size = 256;
                }
                settingsColumnsList.Add(new SettingsPropertyColumn(value.Property, dbType, size));
            }

            // sync profile table structure with the FbDB...
            DataTable structure = Db.GetProfileStructure(connectionString);

            // verify all the columns are there...
            foreach (SettingsPropertyColumn column in settingsColumnsList)
            {
                // see if this column exists
                if (!structure.Columns.Contains(column.Settings.Name))
                {
                    // if not, create it...
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
        public static DataTable admin_list(
            [NotNull] string connectionString, [NotNull] object boardId, [NotNull] object useStyledNicks)
        {
            using (var cmd = FbDbAccess.GetCommand("admin_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("admin_pageaccesslist"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value = boardId;
                cmd.Parameters.Add(new FbParameter("@I_STYLEDNICKS", FbDbType.Integer)).Value = useStyledNicks;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The adminpageaccess_save.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pageName">
        /// The page name.
        /// </param>
        public static void adminpageaccess_save(
            [NotNull] string connectionString, [NotNull] object userId, [NotNull] object pageName)
        {
            using (var cmd = FbDbAccess.GetCommand("adminpageaccess_save"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_PAGENAME", FbDbType.VarChar)).Value = pageName;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The adminpageaccess_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pageName">
        /// The page name.
        /// </param>
        public static void adminpageaccess_delete(
            [NotNull] string connectionString, [NotNull] object userId, [CanBeNull] object pageName)
        {
            using (var cmd = FbDbAccess.GetCommand("adminpageaccess_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_PAGENAME", FbDbType.VarChar)).Value = pageName;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// The adminpageaccess_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pageName">
        /// The page name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable adminpageaccess_list(
            [NotNull] string connectionString, [CanBeNull] object userId, [CanBeNull] object pageName)
        {
            using (var cmd = FbDbAccess.GetCommand("adminpageaccess_list"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_PAGENAME", FbDbType.VarChar)).Value = pageName;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }


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
            [NotNull] string connectionString,
            [NotNull] object albumId,
            [NotNull] object userID,
            [NotNull] object title,
            [NotNull] object coverImageID)
        {
            using (var cmd = FbDbAccess.GetCommand("ALBUM_SAVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_ALBUMID", FbDbType.Integer)).Value = albumId;
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("@I_TITLE", FbDbType.VarChar)).Value = title;
                cmd.Parameters.Add(new FbParameter("@I_COVERIMAGEID", FbDbType.Integer)).Value = coverImageID;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return Convert.ToInt32(FbDbAccess.ExecuteScalar(cmd, connectionString));
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
            using (var cmd = FbDbAccess.GetCommand("ALBUM_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_USERID", FbDbType.Integer)).Value = UserID;
                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = AlbumID;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("ALBUM_DELETE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = AlbumID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("ALBUM_GETTITLE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = AlbumID;

                return FbDbAccess.ExecuteScalar(cmd, connectionString).ToString();
            }
        }

        /// <summary>
        /// Get the number of albums + number of current uploaded files by the user if UserID is not null,
        /// Otherwise, it gets the number of images in the album with AlbumID.
        /// </summary>
        /// <param name="userID">
        /// the User ID.
        /// </param>
        /// <param name="albumID">
        /// the album id.
        /// </param>
        /// <returns></returns>
        public static int[] album_getstats([NotNull] string connectionString, [NotNull] object userID, [NotNull] object albumID)
        {
            using (var cmd = FbDbAccess.GetCommand("ALBUM_GETSTATS"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("I_USERID", FbDbType.Integer)).Value = userID;
                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = albumID;

                DataRow dr = FbDbAccess.GetData(cmd, connectionString).Rows[0];

                return new int[] { Convert.ToInt32(dr["I_ALBUMNUMBER"]), Convert.ToInt32(dr["I_IMAGENUMBER"]) };
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
            [NotNull] object imageID,
            [NotNull] object albumID,
            [NotNull] object caption,
            [NotNull] object fileName,
            [NotNull] object bytes,
            [NotNull] object contentType)
        {
            using (var cmd = FbDbAccess.GetCommand("ALBUM_IMAGE_SAVE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_IMAGEID", FbDbType.Integer)).Value = imageID;
                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = albumID;
                cmd.Parameters.Add(new FbParameter("I_CAPTION", FbDbType.VarChar)).Value = caption;
                cmd.Parameters.Add(new FbParameter("I_FILENAME", FbDbType.VarChar)).Value = fileName;
                cmd.Parameters.Add(new FbParameter("I_BYTES", FbDbType.Integer)).Value = bytes;
                cmd.Parameters.Add(new FbParameter("I_CONTENTTYPE", FbDbType.VarChar)).Value = contentType;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Lists all the images associated with the AlbumID or
        /// the image with the ImageID.
        /// </summary>
        /// <param name="albumID">
        /// the Album id.
        /// </param>
        /// <param name="imageID">
        /// The image id.
        /// </param>
        /// <returns>
        /// a Datatable containing the image(s).
        /// </returns>
        public static DataTable album_image_list(
            [NotNull] string connectionString, [NotNull] object albumID, [NotNull] object imageID)
        {
            using (var cmd = FbDbAccess.GetCommand("ALBUM_IMAGE_LIST"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_ALBUMID", FbDbType.Integer)).Value = albumID;
                cmd.Parameters.Add(new FbParameter("I_IMAGEID", FbDbType.Integer)).Value = imageID;
                return FbDbAccess.GetData(cmd, connectionString);
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
            using (var cmd = FbDbAccess.GetCommand("ALBUM_IMAGE_DELETE"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_IMAGEID", FbDbType.Integer)).Value = ImageID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Increments the image's download times.
        /// </summary>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        /// <param name="ImageID">
        /// the image id.
        /// </param>
        public static void album_image_download([NotNull] string connectionString, object ImageID)
        {
            using (var cmd = FbDbAccess.GetCommand("ALBUM_IMAGE_DOWNLOAD"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("I_IMAGEID", FbDbType.Integer)).Value = ImageID;
                FbDbAccess.ExecuteNonQuery(cmd, connectionString);
            }
        }

        /// <summary>
        /// Album images by users the specified user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>All Albbum Images of the User</returns>
        public static DataTable album_images_by_user([NotNull] string connectionString, [NotNull] object userID)
        {
            using (var cmd = FbDbAccess.GetCommand("album_images_by_user"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_USERID", FbDbType.Integer).Value = userID;
                return FbDbAccess.GetData(cmd, connectionString);
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
        public static void unencode_all_topics_subjects(
            [NotNull] string connectionString, [NotNull] Func<string, string> decodeTopicFunc)
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
                        topic_updatetopic(connectionString, topic.TopicID.Value, decodedTopic);
                    }
                }
                catch
                {
                    // soft-fail...
                }
            }
        }

        /// <summary>
        /// Get the Thanks From Count for the user.
        /// </summary>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// Returns the Thank Count.
        /// </returns>
        public static int user_ThankFromCount([NotNull] string connectionString, [NotNull] object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_thankfromcount"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_UserID", FbDbType.Integer).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                var thankCount = (int)FbDbAccess.ExecuteScalar(cmd, connectionString);

                return thankCount;
            }
        }

        /// <summary>
        /// Checks if the User has replied to a specific topic.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
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
            using (var cmd = FbDbAccess.GetCommand("user_repliedtopic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("i_MessageID", FbDbType.Integer).Value = messageId;
                cmd.Parameters.Add("i_UserID", FbDbType.Integer).Value = userId;

                cmd.CommandTimeout = int.Parse(Config.SqlCommandTimeout);

                var messageCount = (int)FbDbAccess.ExecuteScalar(cmd, connectionString);

                return messageCount > 0;
            }
        }
    }
}