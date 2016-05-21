
#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File ActiveLocation.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion


namespace YAF.Types.Interfaces
{
  #region Using

  using System;
  using System.Collections.Generic;
  using System.Data;

  using YAF.Types.Objects;

  #endregion

  /// <summary>
  /// The idb broker.
  /// </summary>
  public interface IDBBroker
  {
    #region Public Methods

    IEnumerable<TypedBBCode> GetCustomBBCode();

    IEnumerable<DataRow> GetShoutBoxMessages(int boardId);

    /// <summary>
    /// Get the list of recently logged in users.
    /// </summary>
    /// <param name="timeSinceLastLogin">Time since last login in minutes.</param>
    /// <returns>The DataTable of the users.</returns>
    DataTable GetRecentUsers(int timeSinceLastLogin);

    /// <summary>
    /// The user lazy data.
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// </returns>
    DataRow ActiveUserLazyData([NotNull] int userId);

    /// <summary>
    /// Adds the Thanks info to a dataTable
    /// </summary>
    /// <param name="dataRows">
    /// </param>
    void AddThanksInfo([NotNull] IEnumerable<DataRow> dataRows);

    /// <summary>
    /// Returns the layout of the board
    /// </summary>
    /// <param name="boardID">
    /// BoardID
    /// </param>
    /// <param name="userID">
    /// UserID
    /// </param>
    /// <param name="categoryID">
    /// CategoryID
    /// </param>
    /// <param name="parentID">
    /// ParentID
    /// </param>
    /// <returns>
    /// Returns board layout
    /// </returns>
    DataSet BoardLayout(int boardID, int userID, int? categoryID, int? parentID);

    /// <summary>
    /// The favorite topic list.
    /// </summary>
    /// <param name="userID">
    /// The user ID.
    /// </param>
    /// <returns>
    /// </returns>
    List<int> FavoriteTopicList(int userID);

    /// <summary>
    /// The get active list.
    /// </summary>
    /// <param name="guests">
    /// The guests.
    /// </param>
    /// <param name="crawlers">
    /// The crawlers.
    /// </param>
    /// <returns>
    /// </returns>
    DataTable GetActiveList(bool guests, bool crawlers);

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
    /// </returns>
    DataTable GetActiveList(int activeTime, bool guests, bool crawlers);

    /// <summary>
    /// Get all moderators by Groups and User
    /// </summary>
    /// <returns>
    /// Returns the Moderator List
    /// </returns>
    List<SimpleModerator> GetAllModerators();

    /// <summary>
    /// The get latest topics.
    /// </summary>
    /// <param name="numberOfPosts">
    /// The number of posts.
    /// </param>
    /// <returns>
    /// </returns>
    DataTable GetLatestTopics(int numberOfPosts);

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
    /// </returns>
    DataTable GetLatestTopics(int numberOfPosts, int userId, [NotNull] params string[] styleColumnNames);

    /// <summary>
    /// The get moderators.
    /// </summary>
    /// <returns>
    /// </returns>
    DataTable GetModerators();

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
    /// </returns>
    List<SimpleForum> GetSimpleForumTopic(int boardId, int userId, DateTime timeFrame, int maxCount);

    /// <summary>
    /// The get smilies.
    /// </summary>
    /// <returns>
    /// Table with list of smiles
    /// </returns>
    IEnumerable<TypedSmileyList> GetSmilies();

    /// <summary>
    /// Loads the message text into the paged data if "Message" and "MessageID" exists.
    /// </summary>
    /// <param name="dataRows">
    /// </param>
    void LoadMessageText([NotNull] IEnumerable<DataRow> dataRows);

    /// <summary>
    /// The style transform func wrap.
    /// </summary>
    /// <param name="dt">
    /// The DateTable
    /// </param>
    /// <returns>
    /// The style transform wrap.
    /// </returns>
    DataTable StyleTransformDataTable([NotNull] DataTable dt);

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
    DataTable StyleTransformDataTable([NotNull] DataTable dt, [NotNull] params string[] styleColumns);

    /// <summary>
    /// The Buddy list for the user with the specified UserID.
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// </returns>
    DataTable UserBuddyList(int userId);

    /// <summary>
    /// The user ignored list.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// </returns>
    List<int> UserIgnoredList(int userId);

    /// <summary>
    /// The user medals.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// </returns>
    DataTable UserMedals(int userId);

    #endregion
  }
}