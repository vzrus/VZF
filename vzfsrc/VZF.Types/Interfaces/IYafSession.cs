
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
    using System;
    using System.Collections;
    using System.Data;

    /// <summary>
    /// The Yaf Session Interface
    /// </summary>
    public interface IYafSession
    {
        /// <summary>
        /// Gets or sets Twitter Token.
        /// </summary>
        string TwitterToken { get; set; }

        /// <summary>
        /// Gets or sets Twitter Token Secret.
        /// </summary>
        string TwitterTokenSecret { get; set; }

        /// <summary>
        /// Gets or sets the multi quote ids.
        /// </summary>
        /// <value>
        /// The multi quote ids.
        /// </value>
        ArrayList MultiQuoteIds { get; set; }

        /// <summary>
        ///   Gets or sets Unread Topic Since.
        /// </summary>
        int? UnreadTopicSince { get; set; }

        /// <summary>
        ///   Gets or sets User Topic Since.
        /// </summary>
        int? UserTopicSince { get; set; }

        /// <summary>
        ///   Gets or sets ActiveTopicSince.
        /// </summary>
        int? ActiveTopicSince { get; set; }

         /// <summary>
        ///   Gets or sets UnansweredTopicSince.
        /// </summary>
        int? UnansweredTopicSince { get; set; }

        /// <summary>
        /// Gets or sets if the user wants to use the mobile theme.
        /// </summary>
        bool? UseMobileTheme { get; set; }

        /// <summary>
        ///   Gets or sets FavoriteTopicSince.
        /// </summary>
        int? FavoriteTopicSince { get; set; }

        /// <summary>
        /// Gets or sets the forum admin tree add forum.
        /// </summary>
        int? ForumAdminTreeAddForum { get; set; }

        /// <summary>
        ///   Gets or sets active node for an nntp tree.
        /// </summary>
        string NntpTreeActiveNode { get; set; }

        /// <summary>
        ///   Gets or sets active node for a tree.
        /// </summary>
        string ForumTreeChangerActiveNode { get; set; }

        /// <summary>
        ///   Gets or sets active target node for a tree.
        /// </summary>
        string ForumTreeChangerActiveTargetNode { get; set; }

        /// <summary>
        ///   Gets or sets selected nodes for a search forum tree.
        /// </summary>
        string[] SearchTreeSelectedNodes { get; set; }

        /// <summary>
        ///   Gets or sets ForumRead.
        /// </summary>
        Hashtable ForumRead { get; set; }

        /// <summary>
        ///   Gets or sets LastPm.
        /// </summary>
        DateTime LastPendingBuddies { get; set; }

        /// <summary>
        ///   Gets or sets LastPm.
        /// </summary>
        DateTime LastPm { get; set; }

        /// <summary>
        ///   Gets or sets LastPost.
        /// </summary>
        DateTime LastPost { get; set; }

        /// <summary>
        ///   Gets or sets LastVisit.
        /// </summary>
        DateTime? LastVisit { get; set; }

        /// <summary>
        ///   Gets PanelState.
        /// </summary>
        [NotNull]
        IPanelSessionState PanelState { get; }

        /// <summary>
        ///   Gets or sets SearchData.
        /// </summary>
        [CanBeNull]
        DataTable SearchData { get; set; }

        /// <summary>
        ///   Gets or sets ShowList.
        /// </summary>
        int ShowList { get; set; }

        /// <summary>
        ///   Gets or sets TopicRead.
        /// </summary>
        Hashtable TopicRead { get; set; }

        /// <summary>
        ///   Gets or sets UnreadTopics.
        /// </summary>
        int UnreadTopics { get; set; }

        /// <summary>
        /// Gets the last time the forum was read.
        /// </summary>
        /// <param name="forumID">
        /// This is the ID of the forum you wish to get the last read date from.
        /// </param>
        /// <returns>
        /// A DateTime object of when the forum was last read.
        /// </returns>
        DateTime GetForumRead(int forumID);

        /// <summary>
        /// Returns the last time that the topicID was read.
        /// </summary>
        /// <param name="topicID">
        /// The topicID you wish to find the DateTime object for.
        /// </param>
        /// <returns>
        /// The DateTime object from the topicID.
        /// </returns>
        DateTime GetTopicRead(int topicID);

        /// <summary>
        /// Sets the time that the forum was read.
        /// </summary>
        /// <param name="forumID">
        /// The forum ID that was read.
        /// </param>
        /// <param name="date">
        /// The DateTime you wish to set the read to.
        /// </param>
        void SetForumRead(int forumID, DateTime date);

        /// <summary>
        /// Sets the time that the <paramref name="topicID"/> was read.
        /// </summary>
        /// <param name="topicID">
        /// The topic ID that was read.
        /// </param>
        /// <param name="date">
        /// The DateTime you wish to set the read to.
        /// </param>
        void SetTopicRead(int topicID, DateTime date);
    }
}