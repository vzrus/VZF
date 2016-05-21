
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


namespace YAF.Types.Constants
{

    /// <summary>
    /// For globally or multiple times used constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Cache key constants
        /// </summary>
        public struct Cache
        {
            #region Constants and Fields

            /// <summary>
            /// The Album count for the User.
            /// </summary>
            public const string AlbumCountUser = "AlbumCountUser{0}";

            /// <summary>
            ///   The active discussions.
            /// </summary>
            public const string ActiveDiscussions = "ActiveDiscussions";

            /// <summary>
            ///   The user data which is not refreshed too often.
            /// </summary>
            public const string ActiveUserLazyData = "ActiveUserLazyData{0}";

            /// <summary>
            ///   The banned ip.
            /// </summary>
            public const string BannedIP = "BannedIP";

            /// <summary>
            ///  The board moderators cache.
            /// </summary>
            public const string BoardModerators = "BoardModerators";

            /// <summary>
            ///   The board admins cache.
            /// </summary>
            public const string BoardAdmins = "BoardAdmins";

            /// <summary>
            ///   The board settings.
            /// </summary>
            public const string BoardSettings = "BoardSettings";

            /// <summary>
            ///   The board stats.
            /// </summary>
            public const string BoardStats = "BoardStats";

            /// <summary>
            ///   The board user stats.
            /// </summary>
            public const string BoardUserStats = "BoardUserStats";

            /// <summary>
            ///   The custom bb code.
            /// </summary>
            public const string CustomBBCode = "CustomBBCode";

            /// <summary>
            ///   The favorite topic list.
            /// </summary>
            public const string FavoriteTopicCount = "FavoriteTopicId{0}";

            /// <summary>
            ///   The favorite topic list.
            /// </summary>
            public const string FavoriteTopicList = "FavoriteTopicList{0}";

            /// <summary>
            ///   The first post cleaned.
            /// </summary>
            public const string FirstPostCleaned = "FirstPostCleaned{0}{1}";

            /// <summary>
            ///   The forum active discussions.
            /// </summary>
            public const string ForumActiveDiscussions = "ForumActiveDiscussions";

            /// <summary>
            ///   The forum category.
            /// </summary>
            public const string ForumCategory = "ForumCategory";

            /// <summary>
            ///   The forum jump.
            /// </summary>
            public const string ForumJump = "ForumJump{0}";

            /// <summary>
            ///   The forum moderators.
            /// </summary>
            public const string ForumModerators = "ForumModerators";

            /// <summary>
            ///   The user access table.
            /// </summary>
            public const string UserForumAccessData = "UserForumAccessData{0}_{1}_{2}";

            /// <summary>
            ///   The guest groups cache.
            /// </summary>
            public const string GuestGroupsCache = "GuestGroupsCache";

            /// <summary>
            ///   The group rank styles.
            /// </summary>
            public const string GroupRankStyles = "GroupRankStyles";

            /// <summary>
            ///   The guest user id.
            /// </summary>
            public const string GuestUserID = "GuestUserID";

            /// <summary>
            ///   The most active users.
            /// </summary>
            public const string MostActiveUsers = "MostActiveUsers";

            /// <summary>
            ///   The replace rules.
            /// </summary>
            public const string ReplaceRules = "ReplaceRules{0}";

            /// <summary>
            ///   The replace words.
            /// </summary>
            public const string ReplaceWords = "ReplaceWords";

            /// <summary>
            ///   The replace words.
            /// </summary>
            public const string ReplaceWordsCache = "ReplaceWordsCache";

            /// <summary>
            ///   The shoutbox.
            /// </summary>
            public const string Shoutbox = "Shoutbox";

            /// <summary>
            ///   The smilies.
            /// </summary>
            public const string Smilies = "Smilies";

            /// <summary>
            /// The task module.
            /// </summary>
            public const string TaskModule = "YafTaskModule";

            /// <summary>
            ///   The user boxes.
            /// </summary>
            public const string UserBoxes = "UserBoxes";

            /// <summary>
            ///   The user buddies.
            /// </summary>
            public const string UserBuddies = "UserBuddies{0}";

            /// <summary>
            ///   The user ignore list.
            /// </summary>
            public const string UserIgnoreList = "UserIgnoreList{0}";

            /// <summary>
            ///   The user list for id.
            /// </summary>
            public const string UserListForID = "UserListForID{0}";

            /// <summary>
            ///   The user medals.
            /// </summary>
            public const string UserMedals = "UserMedals{0}";

            /// <summary>
            ///   The user signature cache.
            /// </summary>
            public const string UserSignatureCache = "UserSignatureCache";

            /// <summary>
            ///   The users display name collection.
            /// </summary>
            public const string UsersDisplayNameCollection = "UsersDisplayNameCollection";

            /// <summary>
            ///   The users online status.
            /// </summary>
            public const string UsersOnlineStatus = "UsersOnlineStatus";

            /// <summary>
            ///  The Todays Birthdays
            /// </summary>
            public const string TodaysBirthdays = "TodaysBirthdays";

            /// <summary>
            /// The Visitors In The Last 30 Days
            /// </summary>
            public const string VisitorsInTheLast30Days = "VisitorsInTheLast30Days";

            #endregion
        }

        /// <summary>
        /// Constants for UserBox templating
        /// </summary>
        public struct UserBox
        {
            #region Constants and Fields

            /// <summary>
            ///   The avatar.
            /// </summary>
            public const string Avatar = "<vzf:avatar\\s*/>";

            /// <summary>
            ///   The display template default.
            /// </summary>
            public const string DisplayTemplateDefault =
              "<vzf:avatar /><div class=\"section\"><vzf:rankimage /><vzf:rank /></div><br /><vzf:reputation /><vzf:medals /><div class=\"section\"><vzf:groups /><vzf:joindate /><vzf:posts /><vzf:gender /><vzf:countryimage /><vzf:location /></div><br/ ><div class=\"section\"><vzf:thanksfrom /><vzf:thanksto /></div>";

            /// <summary>
            ///   The gender.
            /// </summary>
            public const string Gender = "<vzf:gender\\s*/>";

            /// <summary>
            ///   The groups.
            /// </summary>
            public const string Groups = "<vzf:groups\\s*/>";

            /// <summary>
            ///   The join date.
            /// </summary>
            public const string JoinDate = "<vzf:joindate\\s*/>";

            /// <summary>
            ///   The location.
            /// </summary>
            public const string Location = "<vzf:location\\s*/>";

            /// <summary>
            ///   The rank image.
            /// </summary>
            public const string CountryImage = "<vzf:countryimage\\s*/>";

            /// <summary>
            ///   The medals.
            /// </summary>
            public const string Medals = "<vzf:medals\\s*/>";

            /// <summary>
            ///   The Reputation (points).
            /// </summary>
            public const string Reputation = "<vzf:reputation\\s*/>";

            /// <summary>
            ///   The posts.
            /// </summary>
            public const string Posts = "<vzf:posts\\s*/>";

            /// <summary>
            ///   The rank.
            /// </summary>
            public const string Rank = "<vzf:rank\\s*/>";

            /// <summary>
            ///   The rank image.
            /// </summary>
            public const string RankImage = "<vzf:rankimage\\s*/>";

            /// <summary>
            ///   The thanks from.
            /// </summary>
            public const string ThanksFrom = "<vzf:thanksfrom\\s*/>";

            /// <summary>
            ///   The thanks to.
            /// </summary>
            public const string ThanksTo = "<vzf:thanksto\\s*/>";

            #endregion
        }

        /// <summary>
        /// The forum rebuild.
        /// </summary>
        public struct ForumRebuild
        {
            /// <summary>
            /// The blocking task names.
            /// </summary>
            public static readonly string[] BlockingTaskNames = new[] { "BoardDeleteTask", "BoardCreateTask", "ForumDeleteTask", "ForumSaveTask", "CategoryDeleteTask", "CategorySaveTask" };
        }

        /// <summary>
        /// The special object names.
        /// </summary>
        public struct SpecialObjectNames
        {
            /// <summary>
            /// The user profile table.
            /// </summary>
            public const string UserProfileMirrorTable = "UserProfile";
        }
    }
}