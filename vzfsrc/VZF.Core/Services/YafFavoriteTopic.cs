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
 * File YafFavoriteTopic.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:05 PM.
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

namespace YAF.Core.Services
{
  #region Using

  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Web;

  using VZF.Data.Common;

  
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

  #endregion

  /// <summary>
  /// Favorite Topic Service for the current user.
  /// </summary>
  public class YafFavoriteTopic : IFavoriteTopic, IHaveServiceLocator
  {
    public HttpSessionStateBase SessionState { get; set; }

    public IServiceLocator ServiceLocator { get; set; }

    public ITreatCacheKey TreatCacheKey { get; set; }

    public YafFavoriteTopic(HttpSessionStateBase sessionState, IServiceLocator serviceLocator, ITreatCacheKey treatCacheKey)
    {
      SessionState = sessionState;
      ServiceLocator = serviceLocator;
      TreatCacheKey = treatCacheKey;
    }

    #region Constants and Fields

    /// <summary>
    ///   The _favorite Topic list.
    /// </summary>
    private List<int> _favoriteTopicList;

    #endregion

    #region Implemented Interfaces

    #region IFavoriteTopic

    /// <summary>
    /// The add favorite topic.
    /// </summary>
    /// <param name="topicId">
    /// The topic ID.
    /// </param>
    /// <returns>
    /// The add favorite topic.
    /// </returns>
    public int AddFavoriteTopic(int topicId)
    {
        CommonDb.topic_favorite_add(YafContext.Current.PageModuleID, YafContext.Current.PageUserID, topicId);
      this.ClearFavoriteTopicCache();

      if (YafContext.Current.CurrentUserData.NotificationSetting == UserNotificationSetting.TopicsIPostToOrSubscribeTo)
      {
        // add to watches...
        this.WatchTopic(YafContext.Current.PageUserID, topicId);
      }

      return topicId;
    }

    /// <summary>
    /// The clear favorite topic cache.
    /// </summary>
    public void ClearFavoriteTopicCache()
    {
      // clear for the session
      this.SessionState.Remove(
        this.TreatCacheKey.Treat(Constants.Cache.FavoriteTopicList.FormatWith(YafContext.Current.PageUserID)));
    }

    /// <summary>
    /// the favorite topic details.
    /// </summary>
    /// <param name="sinceDate">
    /// the since date.
    /// </param>
    /// <returns>
    /// a Data table containing all the current user's favorite topics in details.
    /// </returns>
    public DataTable FavoriteTopicDetails(DateTime sinceDate)
    {
        return CommonDb.topic_favorite_details(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID,
                         (YafContext.Current.Settings.CategoryID == 0) ? null : (object)YafContext.Current.Settings.CategoryID,
                        YafContext.Current.PageUserID,
                        sinceDate,
                        DateTime.UtcNow,
                        // page index in db is 1 based!
                        0,
                        // set the page size here
                        1000,
                        YafContext.Current.BoardSettings.UseStyledNicks,
                        false);
    }

    /// <summary>
    /// The is favorite topic.
    /// </summary>
    /// <param name="topicID">
    /// The topic id.
    /// </param>
    /// <returns>
    /// The is favorite topic.
    /// </returns>
    public bool IsFavoriteTopic(int topicID)
    {
      this.InitializeFavoriteTopicList();

      if (this._favoriteTopicList.Count > 0)
      {
        return this._favoriteTopicList.Contains(topicID);
      }

      return false;
    }

    /// <summary>
    /// The remove favorite topic.
    /// </summary>
    /// <param name="topicId">
    /// The favorite topic id.
    /// </param>
    /// <returns>
    /// The remove favorite topic.
    /// </returns>
    public int RemoveFavoriteTopic(int topicId)
    {
        CommonDb.topic_favorite_remove(YafContext.Current.PageModuleID, YafContext.Current.PageUserID, topicId);
      this.ClearFavoriteTopicCache();

      if (YafContext.Current.CurrentUserData.NotificationSetting == UserNotificationSetting.TopicsIPostToOrSubscribeTo)
      {
        // no longer watching this topic...
        this.UnwatchTopic(YafContext.Current.PageUserID, topicId);
      }

      return topicId;
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The initialize favorite topic list.
    /// </summary>
    private void InitializeFavoriteTopicList()
    {
      if (this._favoriteTopicList == null)
      {
        this._favoriteTopicList = YafContext.Current.Get<IDBBroker>().FavoriteTopicList(YafContext.Current.PageUserID);
      }
    }

    /// <summary>
    /// Returns true if the topic is set to watch for userId
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="topicId">
    /// The topic Id.
    /// </param>
    /// <returns>
    /// </returns>
    private int? TopicWatchedId(int userId, int topicId)
    {
        return CommonDb.watchtopic_check(YafContext.Current.PageModuleID, userId, topicId).GetFirstRowColumnAsValue<int?>("WatchTopicID", null);
    }

    /// <summary>
    /// Checks if this topic is watched, if not, adds it.
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="topicId">
    /// The topic Id.
    /// </param>
    private void UnwatchTopic(int userId, int topicId)
    {
      var watchedId = this.TopicWatchedId(userId, topicId);

      if (watchedId.HasValue)
      {
        // subscribe to this forum
          CommonDb.watchtopic_delete(YafContext.Current.PageModuleID, watchedId);
      }
    }

    /// <summary>
    /// Checks if this topic is watched, if not, adds it.
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="topicId">
    /// The topic Id.
    /// </param>
    private void WatchTopic(int userId, int topicId)
    {
      if (!this.TopicWatchedId(userId, topicId).HasValue)
      {
        // subscribe to this forum
          CommonDb.watchtopic_add(YafContext.Current.PageModuleID, userId, topicId);
      }
    }

    #endregion
  }
}