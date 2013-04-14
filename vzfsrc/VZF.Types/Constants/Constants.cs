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

namespace YAF.Types.Constants
{
    using System.Collections.Concurrent;
    using System.Data;

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

        public struct ForumRebuild
        {
            public static readonly string[] BlockingTaskNames = new[] { "BoardDeleteTask", "BoardCreateTask", "ForumDeleteTask", "ForumSaveTask", "CategoryDeleteTask", "CategorySaveTask" };
        }

    }
}