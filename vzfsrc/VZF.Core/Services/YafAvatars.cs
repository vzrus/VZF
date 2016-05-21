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
 * File YafAvatars.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Web;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The yaf avatars.
    /// </summary>
    public class YafAvatars : IAvatars
    {
        #region Constants and Fields

        /// <summary>
        /// The _yaf board settings.
        /// </summary>
        private readonly YafBoardSettings _yafBoardSettings;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YafAvatars"/> class.
        /// </summary>
        /// <param name="boardSettings">
        /// The board settings.
        /// </param>
        public YafAvatars(YafBoardSettings boardSettings)
        {
            this._yafBoardSettings = boardSettings;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get avatar url for current user.
        /// </summary>
        /// <returns>
        /// Returns the Avatar Url 
        /// </returns>
        public string GetAvatarUrlForCurrentUser()
        {
            return this.GetAvatarUrlForUser(YafContext.Current.CurrentUserData);
        }

        /// <summary>
        /// The get avatar url for user.
        /// </summary>
        /// <param name="userId">
        /// The user id. 
        /// </param>
        /// <returns>
        /// Returns the Avatar Url 
        /// </returns>
        public string GetAvatarUrlForUser(int userId)
        {
            var userData = new CombinedUserDataHelper(userId);

            return this.GetAvatarUrlForUser(userData);
        }

        /// <summary>
        /// The get avatar url for user.
        /// </summary>
        /// <param name="userData">
        /// The user data. 
        /// </param>
        /// <returns>
        /// Returns the Avatar Url 
        /// </returns>
        public string GetAvatarUrlForUser([NotNull] IUserData userData)
        {
            CodeContracts.ArgumentNotNull(userData, "userData");
            var usrEmail = new Func<string>(
                () =>
                    {
                        string userEmail;
                        try
                        {
                            userEmail = userData.Email;
                        }
                        catch (Exception)
                        {
                            userEmail = string.Empty;
                        }
                        return userEmail;
                    });
            return this.GetAvatarUrlForUser(
                userData.UserID,
                userData.Avatar,
                userData.HasAvatarImage,
                this._yafBoardSettings.AvatarGravatar ? usrEmail() : string.Empty);
        }

        /// <summary>
        /// The get avatar url for user.
        /// </summary>
        /// <param name="userId">
        /// The user Id. 
        /// </param>
        /// <param name="avatarString">
        /// The avatarString. 
        /// </param>
        /// <param name="hasAvatarImage">
        /// The hasAvatarImage. 
        /// </param>
        /// <param name="email">
        /// The email. 
        /// </param>
        /// <returns>
        /// Returns the Avatar Url 
        /// </returns>
        public string GetAvatarUrlForUser(int userId, string avatarString, bool hasAvatarImage, string email)
        {
            string avatarUrl = string.Empty;

            if (this._yafBoardSettings.AvatarUpload && hasAvatarImage)
            {
                avatarUrl = "{0}resource.ashx?u={1}".FormatWith(YafForumInfo.ForumClientFileRoot, userId);
            }
            else if (avatarString.IsSet())
            {
                // Took out PageContext.BoardSettings.AvatarRemote
                avatarUrl =
                    "{3}resource.ashx?url={0}&width={1}&height={2}".FormatWith(
                        HttpUtility.UrlEncode(avatarString),
                        this._yafBoardSettings.AvatarWidth,
                        this._yafBoardSettings.AvatarHeight,
                        YafForumInfo.ForumClientFileRoot);
            }
            else if (this._yafBoardSettings.AvatarGravatar && email.IsSet())
            {
                // JoeOuts added 8/17/09 for Gravatar use

                // string noAvatarGraphicUrl = HttpContext.Current.Server.UrlEncode( string.Format( "{0}/images/avatars/{1}", YafForumInfo.ForumBaseUrl, "NoAvatar.gif" ) );
                string gravatarUrl =
                    @"http://www.gravatar.com/avatar/{0}.jpg?r={1}".FormatWith(
                        StringExtensions.StringToHexBytes(email), this._yafBoardSettings.GravatarRating);

                avatarUrl =
                    @"{3}resource.ashx?url={0}&width={1}&height={2}".FormatWith(
                        HttpUtility.UrlEncode(gravatarUrl),
                        this._yafBoardSettings.AvatarWidth,
                        this._yafBoardSettings.AvatarHeight,
                        YafForumInfo.ForumClientFileRoot);
            }

            // Return NoAvatar Image is no Avatar available for that user.
            if (avatarUrl.IsNotSet())
            {
                avatarUrl = "{0}images/noavatar.gif".FormatWith(YafForumInfo.ForumClientFileRoot);
            }

            return avatarUrl;
        }

        #endregion
    }
}