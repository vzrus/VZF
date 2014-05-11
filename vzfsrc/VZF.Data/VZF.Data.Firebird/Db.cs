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
    using VZF.Data.Common;

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


       

        #endregion

        #region DataSets

        /// <summary>
        /// Gets a DataSet with forums and categories list for admin.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The BoardID.</param>
        /// <param name="pageUserId">
        /// The pageUserId.
        /// </param>
        /// <param name="isUserForum">
        /// The isUserForum.
        /// </param>
        /// <returns>DataSet with categories</returns>
        public static DataSet ds_forumadmin(
            [NotNull] string connectionString, [NotNull] object boardId, object pageUserId, object isUserForum)
        {
            using (var connMan = new FbDbConnectionManager(connectionString))
            {
                using (var ds = new DataSet())
                {
                    using (
                        var trans =
                            connMan.OpenDBConnection(connectionString).BeginTransaction(FbDbAccess.IsolationLevel))
                            {
                        using (
                            var da = new FbDataAdapter(
                                FbDbAccess.GetObjectName("category_list"), connMan.DBConnection(connectionString)))
                        {
                            da.SelectCommand.Transaction = trans;

                            da.SelectCommand.Parameters.Add(new FbParameter("@I_BOARDID", FbDbType.Integer)).Value =
                                boardId;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_CATEGORYID", FbDbType.Integer)).Value =
                                DBNull.Value;

                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.Fill(ds, FbDbAccess.GetObjectName("Category"));
                            da.SelectCommand.CommandText = FbDbAccess.GetObjectName("forum_list");
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value =
                                pageUserId;
                            da.SelectCommand.Parameters.Add(new FbParameter("@I_ISUSERFORUM", FbDbType.Boolean)).Value =
                                isUserForum;
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
            int messagePosition,
            int messageId,
            DateTime lastRead)
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
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEID", FbDbType.Integer)).Value = messageId;
                cmd.Parameters.Add(new FbParameter("@I_LASTREAD", FbDbType.TimeStamp)).Value = lastRead;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The post_list_reverse 10.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicID">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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
            object messageDescription,
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
                cmd.Parameters.Add(new FbParameter("@I_DESCRIPTION", FbDbType.VarChar)).Value = description;
                cmd.Parameters.Add(new FbParameter("@I_STATUS", FbDbType.VarChar)).Value = status;
                cmd.Parameters.Add(new FbParameter("@I_STYLES", FbDbType.VarChar)).Value = styles;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_REASON", FbDbType.VarChar)).Value = reasonOfEdit;
                cmd.Parameters.Add(new FbParameter("@I_EDITEDBY", FbDbType.Integer)).Value = editedBy;
                cmd.Parameters.Add(new FbParameter("@I_ISMODERATORCHANGED", FbDbType.Boolean)).Value =
                    isModeratorChanged;
                cmd.Parameters.Add(new FbParameter("@I_OVERRIDEAPPROVAL", FbDbType.Boolean)).Value = overrideApproval;
                cmd.Parameters.Add(new FbParameter("@I_ORIGINALMESSAGE", FbDbType.Text)).Value = originalMessage;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEDESCRIPTION", FbDbType.VarChar)).Value = messageDescription;
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
            [CanBeNull] object messageDescription,
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
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEDESCRIPTION", FbDbType.VarChar)).Value = messageDescription;
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
                if (question.Closes > DateTimeHelper.SqlDbMinTime())
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

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
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

                if (question.Closes > DateTimeHelper.SqlDbMinTime())
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

        /// <summary>
        /// The topic_delete.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="eraseTopic">
        /// The erase topic.
        /// </param>
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

        /// <summary>
        /// The topic_findprev.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable topic_findprev([NotNull] string connectionString, object topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FINDPREV"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_findnext.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static DataTable topic_findnext([NotNull] string connectionString, object topicId)
        {
            using (var cmd = FbDbAccess.GetCommand("TOPIC_FINDNEXT"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICID", FbDbType.Integer)).Value = topicId;
                return FbDbAccess.GetData(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_lock.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="locked">
        /// The locked.
        /// </param>
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

        /// <summary>
        /// The topic_save.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="styles">
        /// The styles.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="posted">
        /// The posted.
        /// </param>
        /// <param name="blogPostId">
        /// The blog post id.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <param name="messageDescription">
        /// The message description.
        /// </param>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long topic_save(
            [NotNull] string connectionString,
            [NotNull] object forumId,
            [NotNull] object subject,
            [NotNull] object status,
            [NotNull] object styles,
            [NotNull] object description,
            [NotNull] object message,
            [CanBeNull] object messageDescription,
            [NotNull] object userId,
            [NotNull] object priority,
            [NotNull] object userName,
            [NotNull] object ip,
            [NotNull] object posted,
            [NotNull] object blogPostId,
            [NotNull] object flags,
            out long messageId,
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
                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGE", FbDbType.Text)).Value = message;
                cmd.Parameters.Add(new FbParameter("@I_PRIORITY", FbDbType.SmallInt)).Value = Convert.ToInt16(priority);
                cmd.Parameters.Add(new FbParameter("@I_USERNAME", FbDbType.VarChar)).Value = userName;
                cmd.Parameters.Add(new FbParameter("@I_IP", FbDbType.VarChar)).Value = ip.ToString();
                cmd.Parameters.Add(new FbParameter("@I_POSTED", FbDbType.TimeStamp)).Value = posted;
                cmd.Parameters.Add(new FbParameter("@I_BLOGPOSTID", FbDbType.VarChar)).Value = blogPostId;
                cmd.Parameters.Add(new FbParameter("@I_FLAGS", FbDbType.Integer)).Value = flags;
                cmd.Parameters.Add(new FbParameter("@I_MESSAGEDESCRIPTION", FbDbType.VarChar)).Value = messageDescription;
                cmd.Parameters.Add(new FbParameter("@I_TAGS", FbDbType.VarChar)).Value = tags;
                cmd.Parameters.Add(new FbParameter("@I_UTCTIMESTAMP", FbDbType.TimeStamp)).Value = DateTime.UtcNow;

                DataTable dt = FbDbAccess.GetData(cmd, connectionString);
                messageId = long.Parse(dt.Rows[0]["MessageID"].ToString());
                return long.Parse(dt.Rows[0]["TopicID"].ToString());
            }
        }

        /// <summary>
        /// The topic_info.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="getTags">
        /// The get tags.
        /// </param>
        /// <returns>
        /// The <see cref="DataRow"/>.
        /// </returns>
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

        /// <summary>
        /// The topic_unread.
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
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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

        /// <summary>
        /// The topic_findduplicate.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="topicName">
        /// The topic name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int topic_findduplicate([NotNull] string connectionString, object topicName)
        {
            using (var cmd = FbDbAccess.GetCommand("topic_findduplicate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new FbParameter("@I_TOPICNAME", FbDbType.VarChar)).Value = topicName;
                return (int)FbDbAccess.ExecuteScalar(cmd, connectionString);
            }
        }

        /// <summary>
        /// The topic_favorite_details.
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
        /// <param name="pageUserId">
        /// The page user id.
        /// </param>
        /// <param name="sinceDate">
        /// The since date.
        /// </param>
        /// <param name="toDate">
        /// The to date.
        /// </param>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="useStyledNicks">
        /// The use styled nicks.
        /// </param>
        /// <param name="findLastRead">
        /// The find last read.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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

        public static void user_delete([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_delete"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new FbParameter("@I_USERID", FbDbType.Integer)).Value = userId;

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
        

        public static string user_getsignature([NotNull] string connectionString, object userId)
        {
            using (var cmd = FbDbAccess.GetCommand("user_getsignature"))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@I_USERID", FbDbType.Integer).Value = userId;

                return FbDbAccess.ExecuteScalar(cmd, connectionString).ToString();
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
        
        #endregion      

        #region yaf_WatchTopic

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
                           : DateTimeHelper.SqlDbMinTime();
            }
        }

        #endregion

        #region vzrus addons

        /// <summary>
        /// Gets the btn get stats name.
        /// </summary>
        public static string btnGetStatsName
        {
            get
            {
                return "Recalculate YAF Table Index Statistics";
            }
        }

        /// <summary>
        /// Gets a value indicating whether btn reindex visible.
        /// </summary>
        public static bool btnReindexVisible
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the btn reindex name.
        /// </summary>
        public static string btnReindexName
        {
            get
            {
                return "Recreate YAF Tables indice";
            }
        }

        // DB Maintenance page buttons name

        /// <summary>
        /// Gets the btn shrink name.
        /// </summary>
        public static string btnShrinkName
        {
            get
            {
                return "Shrink Database";
            }
        }

        /// <summary>
        /// Gets the btn recovery mode name.
        /// </summary>
        public static string btnRecoveryModeName
        {
            get
            {
                return "Set Recovery Mode";
            }
        }

        // DB Maintenance page panels visibility

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

        /// <summary>
        /// Gets a value indicating whether panel recovery mode.
        /// </summary>
        public static bool PanelRecoveryMode
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel reindex.
        /// </summary>
        public static bool PanelReindex
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether panel shrink.
        /// </summary>
        public static bool PanelShrink
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region reindex page controls

        /// <summary>
        /// The rsstopic_list.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="forumID">
        /// The forum id.
        /// </param>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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

        /// <summary>
        /// The db_index_simplelist.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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
                                    string.Format(
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
      
        #endregion

        /// <summary>
        /// The create database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public static void CreateDatabase([NotNull] string connectionString)
        {
            CreateDatabase(connectionString, true);
        }

        /// <summary>
        /// The create database.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="embeded">
        /// The embeded.
        /// </param>
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

        /// <summary>
        /// The db_shrink_warning.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string db_shrink_warning()
        {
            return null;
        }

        /// <summary>
        /// The db_shrink_new.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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

        /// <summary>
        /// The get table columns info.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
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
       
        
    }
}