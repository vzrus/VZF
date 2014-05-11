/* Yet Another Forum.net
 * Copyright (C) 2006-2012 Jaben Cargman
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

namespace YAF.Core.Services
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Data.DAL;

    using YAF.Classes;
    
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Types.Objects;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Class used for multi-step DB operations so they can be cached, etc.
    /// </summary>
    public class YafDBBroker : IDBBroker, IHaveServiceLocator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YafDBBroker"/> class.
        /// </summary>
        /// <param name="serviceLocator">
        /// The service locator.
        /// </param>
        /// <param name="httpSessionState">
        /// The http session state.
        /// </param>
        /// <param name="dataCache">
        /// The data cache.
        /// </param>
        public YafDBBroker(IServiceLocator serviceLocator, HttpSessionStateBase httpSessionState, IDataCache dataCache)
        {
            this.ServiceLocator = serviceLocator;
            this.HttpSessionState = httpSessionState;
            this.DataCache = dataCache;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets DataCache.
        /// </summary>
        public IDataCache DataCache { get; set; }

        /// <summary>
        ///   Gets or sets HttpSessionState.
        /// </summary>
        public HttpSessionStateBase HttpSessionState { get; set; }

        /// <summary>
        ///   Gets or sets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator { get; set; }

        #endregion

        #region Implemented Interfaces

        #region IDBBroker

        /// <summary>
        /// The user lazy data.
        /// </summary>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        /// <returns>
        /// Returns the Active User
        /// </returns>
        public DataRow ActiveUserLazyData(int userId)
        {
            // get a row with user lazy data...
            return
                this.DataCache.GetOrSet(
                    Constants.Cache.ActiveUserLazyData.FormatWith(userId),
                    () =>
                    CommonDb.user_lazydata(
                        YafContext.Current.PageModuleID,
                        userId,
                        YafContext.Current.PageBoardID,
                        this.Get<YafBoardSettings>().AllowEmailSending, 
                        this.Get<YafBoardSettings>().EnableBuddyList, 
                        this.Get<YafBoardSettings>().AllowPrivateMessages, 
                        this.Get<YafBoardSettings>().EnableAlbum, 
                        this.Get<YafBoardSettings>().UseStyledNicks).Table, 
                    TimeSpan.FromMinutes(this.Get<YafBoardSettings>().ActiveUserLazyDataCacheTimeout)).Rows[0];
        }

        /// <summary>
        /// Adds the Thanks info to a dataTable
        /// </summary>
        /// <param name="dataRows">
        /// The data Rows.
        /// </param>
        public void AddThanksInfo(IEnumerable<DataRow> dataRows)
        {
            var postRows = dataRows as List<DataRow> ?? dataRows.ToList();
            var messageIds = postRows.Select(x => x.Field<int>("MessageID"));

            // Initialize the "IsthankedByUser" column.
            postRows.ForEach(x => x["IsThankedByUser"] = false);

            // Initialize the "Thank Info" column.
            postRows.ForEach(x => x["ThanksInfo"] = string.Empty);

            // Iterate through all the thanks relating to this topic and make appropriate
            // changes in columns.
            var allThanks = CommonDb.MessageGetAllThanks(YafContext.Current.PageModuleID, messageIds.ToDelimitedString(","));

            var typedAllThankses = allThanks as List<TypedAllThanks> ?? allThanks.ToList();
            foreach (var f in
                typedAllThankses.Where(t => t.FromUserID != null && t.FromUserID == YafContext.Current.PageUserID).SelectMany(
                    thanks => postRows.Where(x => x.Field<int>("MessageID") == thanks.MessageID)))
            {
                f["IsThankedByUser"] = "true";
                f.AcceptChanges();
            }

            var thanksFieldNames = new[] { "ThanksFromUserNumber", "ThanksToUserNumber", "ThanksToUserPostsNumber" };

            foreach (DataRow postRow in postRows)
            {
                var messageId = postRow.Field<int>("MessageID");

                postRow["MessageThanksNumber"] =
                    typedAllThankses.Count(t => t.FromUserID != null && t.MessageID == messageId);

                var thanksFiltered = typedAllThankses.Where(t => t.MessageID == messageId);

                var allThankses = thanksFiltered as List<TypedAllThanks> ?? thanksFiltered.ToList();
                if (allThankses.Any())
                {
                    var thanksItem = allThankses.First();

                    postRow["ThanksFromUserNumber"] = thanksItem.ThanksFromUserNumber ?? 0;
                    postRow["ThanksToUserNumber"] = thanksItem.ThanksToUserNumber ?? 0;
                    postRow["ThanksToUserPostsNumber"] = thanksItem.ThanksToUserPostsNumber ?? 0;
                }
                else
                {
                    DataRow row = postRow;
                    thanksFieldNames.ForEach(f => row[f] = 0);
                }

                // load all all thanks info into a special column...
                postRow["ThanksInfo"] =
                    allThankses.Where(t => t.FromUserID != null).Select(
                        x => "{0}|{1}".FormatWith(x.FromUserID.Value, x.ThanksDate)).ToDelimitedString(",");

                postRow.AcceptChanges();
            }
        }

        /// <summary>
        /// Returns the layout of the board
        /// </summary>
        /// <param name="boardID">
        /// The board ID.
        /// </param>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <param name="categoryID">
        /// The category ID.
        /// </param>
        /// <param name="parentID">
        /// The parent ID.
        /// </param>
        /// <returns>
        /// The board layout.
        /// </returns>
        public DataSet BoardLayout(int boardID, int userID, int? categoryID, int? parentID)
        {
            if (categoryID.HasValue && categoryID == 0)
            {
                categoryID = null;
            }

            using (var ds = new DataSet())
            {
                DataTable moderator;
                if (this.Get<YafBoardSettings>().ShowModeratorList)
                {
                    moderator = this.GetModerators();
                }
                else
                {
                    // add dummy table.
                    moderator = new DataTable(CommonSqlDbAccess.GetObjectName("Moderator"));
                    moderator.Columns.AddRange(
                        new[]
                            {
                                new DataColumn("ForumID", typeof(int)), 
                                new DataColumn("ForumName", typeof(string)),
                                new DataColumn("ModeratorName", typeof(string)),
                                new DataColumn("ModeratorDisplayName", typeof(string)),
                                new DataColumn("ModeratorEmail", typeof(string)),
                                new DataColumn("ModeratorAvatar", typeof(string)),
                                new DataColumn("ModeratorAvatarImage", typeof(bool)),
                                new DataColumn("Style", typeof(string)), 
                                new DataColumn("IsGroup", typeof(bool))
                            });
                   // moderator.Constraints.Add("PK_Mod_ForumID", moderator.Columns["ForumID"], true);
                }

                // insert it into this DataSet
                ds.Tables.Add(moderator.Copy());

                // get the Category Table
                DataTable category = this.DataCache.GetOrSet(
                    Constants.Cache.ForumCategory,
                    () =>
                        {
                            var catDt = CommonDb.category_list(YafContext.Current.PageModuleID, boardID, null);
                            catDt.TableName = CommonSqlDbAccess.GetObjectName("Category");
                            return catDt;
                        },
                    TimeSpan.FromMinutes(this.Get<YafBoardSettings>().BoardCategoriesCacheTimeout));

                // add it to this dataset
                ds.Tables.Add(category.Copy());

                DataTable categoryTable = ds.Tables[CommonSqlDbAccess.GetObjectName("Category")];

                if (categoryID.HasValue)
                {
                    // make sure this only has the category desired in the dataset
                    foreach (DataRow row in
                        categoryTable.AsEnumerable().Where(row => row.Field<int>("CategoryID") != categoryID))
                    {
                        // delete it...
                        row.Delete();
                    }

                    categoryTable.AcceptChanges();
                }

                DataTable forum = CommonDb.forum_listread(
                    YafContext.Current.PageModuleID,
                    boardID,
                    userID,
                    categoryID,
                    parentID,
                    this.Get<YafBoardSettings>().UseStyledNicks,
                    this.Get<YafBoardSettings>().UseReadTrackingByDatabase,
                    true,
                    false,
                    null);
                forum.TableName = CommonSqlDbAccess.GetObjectName("Forum");
                ds.Tables.Add(forum.Copy());

                ds.Relations.Add(
                    "FK_Forum_Category",
                    categoryTable.Columns["CategoryID"],
                    ds.Tables[CommonSqlDbAccess.GetObjectName("Forum")].Columns["CategoryID"],
                    false);
                ds.Relations.Add(
                    "FK_Moderator_Forum",
                    ds.Tables[CommonSqlDbAccess.GetObjectName("Forum")].Columns["ForumID"],
                    ds.Tables[CommonSqlDbAccess.GetObjectName("Moderator")].Columns["ForumID"],
                    false);

                bool deletedCategory = false;

                // remove empty categories...
                foreach (DataRow row in
                    categoryTable.SelectTypedList(row => new { row, childRows = row.GetChildRows("FK_Forum_Category") })
                                 .Where(@t => !@t.childRows.Any())
                                 .Select(@t => @t.row))
                {
                    // remove this category...
                    row.Delete();
                    deletedCategory = true;
                }

                if (deletedCategory)
                {
                    categoryTable.AcceptChanges();
                }

                return ds;
            }
        }

        /// <summary>
        /// The favorite topic list.
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// Returns The favorite topic list.
        /// </returns>
        public List<int> FavoriteTopicList(int userID)
        {
            string key = this.Get<ITreatCacheKey>().Treat(Constants.Cache.FavoriteTopicList.FormatWith(userID));

            // stored in the user session...
            var favoriteTopicList = this.HttpSessionState[key] as List<int>;

            // was it in the cache?
            if (favoriteTopicList == null)
            {
                // get fresh values
                DataTable favoriteTopicListDt = CommonDb.topic_favorite_list(YafContext.Current.PageModuleID, userID);

                // convert to list...
                favoriteTopicList = favoriteTopicListDt.GetColumnAsList<int>("TopicID");

                // store it in the user session...
                this.HttpSessionState.Add(key, favoriteTopicList);
            }

            return favoriteTopicList;
        }

        /// <summary>
        /// The get active list.
        /// </summary>
        /// <param name="guests">
        /// The guests.
        /// </param>
        /// <param name="crawlers">
        /// The bots.
        /// </param>
        /// <returns>
        /// Returns the active list.
        /// </returns>
        public DataTable GetActiveList(bool guests, bool crawlers)
        {
            return this.GetActiveList(this.Get<YafBoardSettings>().ActiveListTime, guests, crawlers);
        }

        /// <summary>
        /// The get active list.
        /// </summary>
        /// <param name="activeTime">
        /// The active time.
        /// </param>
        /// <param name="guests">
        /// The guests.
        /// </param>
        /// <param name="crawlers">
        /// The crawlers.
        /// </param>
        /// <returns>
        /// Returns the active list.
        /// </returns>
        public DataTable GetActiveList(int activeTime, bool guests, bool crawlers)
        {
            return
                this.StyleTransformDataTable(
                    CommonDb.active_list(
                        YafContext.Current.PageModuleID,
                        YafContext.Current.PageBoardID,
                        guests,
                        crawlers,
                        activeTime,
                        this.Get<YafBoardSettings>().UseStyledNicks));
        }

        /// <summary>
        /// The get all moderators.
        /// </summary>
        /// <returns>
        /// Returns List with all moderators
        /// </returns>
        public List<SimpleModerator> GetAllModerators()
        {
            // get the cached version of forum moderators if it's valid
            var moderator = this.GetModerators();

            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                this.Get<IStyleTransform>().DecodeStyleByTable(ref moderator, false);
            }

            return
                moderator.SelectTypedList(
                    row =>
                    new SimpleModerator(
                        row.Field<int>("ForumID"), 
                        row.Field<string>("ForumName"),
                        row.Field<int>("ModeratorID"), 
                        row.Field<string>("ModeratorName"),
                        row.Field<string>("ModeratorEmail"),
                        row.Field<string>("ModeratorAvatar"),
                        row.Field<bool>("ModeratorAvatarImage"),
                        row.Field<string>("ModeratorDisplayName"), 
                        row.Field<string>("Style"),
                        row["IsGroup"].ToType<bool>())).ToList();
        }
        
        /// <summary>
        /// The get custom bb code.
        /// </summary>
        /// <returns>
        /// Returns List with Custom BBCodes
        /// </returns>
        public IEnumerable<TypedBBCode> GetCustomBBCode()
        {
            return this.DataCache.GetOrSet(
                Constants.Cache.CustomBBCode, () => CommonDb.BBCodeList(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, null).ToList());
        }

        /// <summary>
        /// The get latest topics.
        /// </summary>
        /// <param name="numberOfPosts">
        /// The number of posts.
        /// </param>
        /// <returns>
        /// Returns List with Latest Topics.
        /// </returns>
        public DataTable GetLatestTopics(int numberOfPosts)
        {
            return this.GetLatestTopics(numberOfPosts, YafContext.Current.PageUserID);
        }

        /// <summary>
        /// The get latest topics.
        /// </summary>
        /// <param name="numberOfPosts">
        /// The number of posts.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="styleColumnNames">
        /// The style Column Names.
        /// </param>
        /// <returns>
        /// Returns List with Latest Topics.
        /// </returns>
        public DataTable GetLatestTopics(int numberOfPosts, int userId, params string[] styleColumnNames)
        {
            return
                this.StyleTransformDataTable(
                    CommonDb.topic_latest(
                        YafContext.Current.PageModuleID,
                        YafContext.Current.PageBoardID,
                        numberOfPosts,
                        userId, 
                        this.Get<YafBoardSettings>().UseStyledNicks, 
                        this.Get<YafBoardSettings>().NoCountForumsInActiveDiscussions,
                        this.Get<YafBoardSettings>().UseReadTrackingByDatabase), 
                    styleColumnNames);
        }

        /// <summary>
        /// Get all moderators by Groups and User
        /// </summary>
        /// <returns>
        /// Returns the Moderator List
        /// </returns>
        public DataTable GetModerators()
        {
            return this.DataCache.GetOrSet(
                Constants.Cache.ForumModerators,
                () =>
                    {
                        DataTable moderator = CommonDb.forum_moderators(
                            YafContext.Current.PageModuleID, this.Get<YafBoardSettings>().UseStyledNicks);
                        moderator.TableName = CommonSqlDbAccess.GetObjectName("Moderator");
                        return moderator;
                    },
                TimeSpan.FromMinutes(this.Get<YafBoardSettings>().BoardModeratorsCacheTimeout));
        }

        /// <summary>
        /// Get the list of recently logged in users.
        /// </summary>
        /// <param name="timeSinceLastLogin">
        /// The time since last login in minutes.
        /// </param>
        /// <returns>
        /// The list of users in DataTable format.
        /// </returns>
        public DataTable GetRecentUsers(int timeSinceLastLogin)
        {
            return
                this.StyleTransformDataTable(
                    CommonDb.recent_users(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, timeSinceLastLogin, this.Get<YafBoardSettings>().UseStyledNicks));
        }

        /// <summary>
        /// The get shout box messages.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <returns>
        /// Returns the shout box messages.
        /// </returns>
        public IEnumerable<DataRow> GetShoutBoxMessages(int boardId)
        {
            return this.DataCache.GetOrSet(
                Constants.Cache.Shoutbox, 
                () =>
                    {
                        var messages = CommonDb.shoutbox_getmessages(
                            YafContext.Current.PageModuleID,
                            boardId,
                            this.Get<YafBoardSettings>().ShoutboxShowMessageCount,
                            this.Get<YafBoardSettings>().UseStyledNicks);

                        // Set colorOnly parameter to false, as we get all color info from data base
                        if (this.Get<YafBoardSettings>().UseStyledNicks)
                        {
                            this.Get<IStyleTransform>().DecodeStyleByTable(ref messages, false);
                        }

                        var flags = new MessageFlags { IsBBCode = true, IsHtml = false };

                        foreach (var row in messages.AsEnumerable())
                        {
                            string formattedMessage =
                                this.Get<IFormatMessage>().FormatMessage(row.Field<string>("Message"), flags);

                            // Extra Formating not needed already done tru this.Get<IFormatMessage>().FormatMessage
                            // formattedMessage = FormatHyperLink(formattedMessage);
                            row["Message"] = formattedMessage;
                        }

                        return messages;
                    }, 
                TimeSpan.FromMilliseconds(30000)).AsEnumerable();
        }

        /// <summary>
        /// Get a simple forum/topic listing.
        /// </summary>
        /// <param name="boardId">
        /// The board Id.
        /// </param>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="timeFrame">
        /// The time Frame.
        /// </param>
        /// <param name="maxCount">
        /// The max Count.
        /// </param>
        /// <returns>
        /// The get simple forum topic.
        /// </returns>
        public List<SimpleForum> GetSimpleForumTopic(int boardId, int userId, DateTime timeFrame, int maxCount)
        {
            var forumData =
                CommonDb.forum_listall(YafContext.Current.PageModuleID, boardId, userId).SelectTypedList(
                    x => new SimpleForum { ForumID = x.Field<int>("ForumID"), Name = x.Field<string>("Forum") }).ToList();

            // get topics for all forums...
            foreach (var forum in forumData)
            {
                SimpleForum forum1 = forum;

                // add topics
                var topics =
                    CommonDb.topic_list(
                        YafContext.Current.PageModuleID,
                        forum.ForumID,
                        userId,
                        timeFrame,
                        DateTime.UtcNow,
                        0,
                        maxCount,
                        false,
                        false,
                        false,
                        this.Get<YafBoardSettings>().AllowTopicTags).AsEnumerable();

                // filter first...   
                forum.Topics = topics.Where(x => x.Field<DateTime>("LastPosted") >= timeFrame)   
                                      .Select(x => this.LoadSimpleTopic(x, forum1))   
                                        .ToList(); 
            }

            return forumData;
        }

        /// <summary>
        /// The get smilies.
        /// </summary>
        /// <returns>
        /// Table with list of smilies
        /// </returns>
        public IEnumerable<TypedSmileyList> GetSmilies()
        {
            return this.DataCache.GetOrSet(
                Constants.Cache.Smilies,
                () => CommonDb.SmileyList(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, null).ToList(), 
                TimeSpan.FromMinutes(60));
        }

        /// <summary>
        /// Loads the message text into the paged data if "Message" and "MessageID" exists.
        /// </summary>
        /// <param name="dataRows">
        /// The data Rows.
        /// </param>
        public void LoadMessageText(IEnumerable<DataRow> dataRows)
        {
            var enumerable = dataRows as IList<DataRow> ?? dataRows.ToList();
            var messageIds =
                enumerable.AsEnumerable().Where(x => x.Field<string>("Message").IsNotSet()).Select(
                    x => x.Field<int>("MessageID"));

            var messageTextTable = CommonDb.message_GetTextByIds(YafContext.Current.PageModuleID, messageIds.ToDelimitedString(","));

            if (messageTextTable == null)
            {
                return;
            }

            // load them into the page data...
            foreach (var dataRow in enumerable)
            {
                // find the message id in the results...
                DataRow row = dataRow;

                var message =
                    messageTextTable.AsEnumerable().FirstOrDefault(x => x.Field<int>("MessageID") == row.Field<int>("MessageID"));

                if (message == null)
                {
                    continue;
                }

                dataRow.BeginEdit();
                dataRow["Message"] = message.Field<string>("Message");
                dataRow.EndEdit();
            }
        }

        /// <summary>
        /// The style transform func wrap.
        /// </summary>
        /// <param name="dt">
        /// The DateTable
        /// </param>
        /// <returns>
        /// The style transform wrap.
        /// </returns>
        public DataTable StyleTransformDataTable(DataTable dt)
        {
            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                var styleTransform = this.Get<IStyleTransform>();
                styleTransform.DecodeStyleByTable(ref dt, true);
            }

            return dt;
        }

        /// <summary>
        /// The style transform func wrap.
        /// </summary>
        /// <param name="dt">
        /// The DateTable
        /// </param>
        /// <param name="styleColumns">
        /// Style columns names
        /// </param>
        /// <returns>
        /// The style transform wrap.
        /// </returns>
        public DataTable StyleTransformDataTable(DataTable dt, params string[] styleColumns)
        {
            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                var styleTransform = this.Get<IStyleTransform>();
                styleTransform.DecodeStyleByTable(ref dt, true, styleColumns);
            }

            return dt;
        }

        /// <summary>
        /// The Buddy list for the user with the specified UserID.
        /// </summary>
        /// <param name="userId">
        /// The User ID.
        /// </param>
        /// <returns>
        /// The user buddy list.
        /// </returns>
        public DataTable UserBuddyList(int userId)
        {
            return this.DataCache.GetOrSet(
                Constants.Cache.UserBuddies.FormatWith(userId), 
                () => CommonDb.buddy_list(YafContext.Current.PageModuleID, userId), 
                TimeSpan.FromMinutes(10));
        }

        /// <summary>
        /// The user ignored list.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// Returns the user ignored list.
        /// </returns>
        public List<int> UserIgnoredList(int userId)
        {
            string key = Constants.Cache.UserIgnoreList.FormatWith(userId);

            // stored in the user session...
            var userList = this.HttpSessionState[key] as List<int>;

            // was it in the cache?
            if (userList == null)
            {
                // get fresh values
                DataTable userListDt = CommonDb.user_ignoredlist(YafContext.Current.PageModuleID, userId);

                // convert to list...
                userList = userListDt.GetColumnAsList<int>("IgnoredUserID");

                // store it in the user session...
                this.HttpSessionState.Add(key, userList);
            }

            return userList;
        }

        /// <summary>
        /// The user medals.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// Returns the User Medals
        /// </returns>
        public DataTable UserMedals(int userId)
        {
            string key = Constants.Cache.UserMedals.FormatWith(userId);

            // get the medals cached...
            DataTable dt = this.DataCache.GetOrSet(
                key, () => CommonDb.user_listmedals(YafContext.Current.PageModuleID, userId), TimeSpan.FromMinutes(10));

            return dt;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The load simple topic.
        /// </summary>
        /// Creates a Simple Topic item.
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="forum">
        /// The forum.
        /// </param>
        /// <returns>
        /// Returns the simple topic.
        /// </returns>
        [NotNull]
        private SimpleTopic LoadSimpleTopic([NotNull] DataRow row, [NotNull] SimpleForum forum)
        {
            CodeContracts.ArgumentNotNull(row, "row");
            CodeContracts.ArgumentNotNull(forum, "forum");

            return new SimpleTopic
                {
                    TopicID = row.Field<int>("TopicID"), 
                    CreatedDate = row.Field<DateTime>("Posted"), 
                    Subject = row.Field<string>("Subject"), 
                    StartedUserID = row.Field<int>("UserID"), 
                    StartedUserName = UserMembershipHelper.GetDisplayNameFromID(row.Field<int>("UserID")), 
                    Replies = row.Field<int>("Replies"), 
                    LastPostDate = row.Field<DateTime>("LastPosted"), 
                    LastUserID = row.Field<int>("LastUserID"), 
                    LastUserName = UserMembershipHelper.GetDisplayNameFromID(row.Field<int>("LastUserID")), 
                    LastMessageID = row.Field<int>("LastMessageID"), 
                    FirstMessage = row.Field<string>("FirstMessage"),
                    LastMessage = CommonDb.MessageList(YafContext.Current.PageModuleID, row.Field<int>("LastMessageID")).First().Message, 
                    Forum = forum
                };
        }

        #endregion
    }
}