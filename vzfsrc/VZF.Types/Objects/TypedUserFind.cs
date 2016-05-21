
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


namespace YAF.Types.Objects
{
    #region Using

    using System;
    using System.Data;

    #endregion

    /// <summary>
    /// The typed user find.
    /// </summary>
    public class TypedUserFind
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedUserFind"/> class.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        public TypedUserFind(DataRow row)
        {
            this.UserID = row.Field<int?>("UserID");
            this.BoardID = row.Field<int?>("BoardID");
            this.Name = row.Field<string>("Name");
            this.Password = row.Field<string>("Password");
            this.Email = row.Field<string>("Email");
            this.Joined = row.Field<DateTime?>("Joined");
            this.LastVisit = row.Field<DateTime?>("LastVisit");
            this.IP = row.Field<string>("IP");
            this.NumPosts = row.Field<int?>("NumPosts");
            this.TimeZone = row.Field<int?>("TimeZone");
            this.Avatar = row.Field<string>("Avatar");
            this.Signature = row.Field<string>("Signature");
            this.AvatarImage = row.Field<byte[]>("AvatarImage");
            this.RankID = row.Field<int?>("RankID");
            this.Suspended = row.Field<DateTime?>("Suspended");
            this.LanguageFile = row.Field<string>("LanguageFile");
            this.ThemeFile = row.Field<string>("ThemeFile");
            this.Flags = row.Field<int?>("Flags");
            this.PMNotification = row.Field<bool?>("PMNotification");
            this.Points = row.Field<int?>("Points");
            this.IsApproved = row.Field<bool?>("IsApproved");
            this.IsActiveExcluded = row.Field<bool?>("IsActiveExcluded");
            this.UseMobileTheme = row.Field<bool?>("OverrideDefaultThemes");
            this.AvatarImageType = row.Field<string>("AvatarImageType");
            this.AutoWatchTopics = row.Field<bool?>("AutoWatchTopics");
            this.UseSingleSignOn = row.Field<bool>("UseSingleSignOn");
            this.TextEditor = row.Field<string>("TextEditor");
            this.DisplayName = row.Field<string>("DisplayName");
            this.Culture = row.Field<string>("Culture");
            this.NotificationType = row.Field<int?>("NotificationType");
            this.DailyDigest = row.Field<bool?>("DailyDigest");
            this.IsGuest = row.Field<bool>("IsGuest");
            this.ProviderUserKey = row.Field<string>("ProviderUserKey");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets AutoWatchTopics.
        /// </summary>
        public bool? AutoWatchTopics { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseSingleSignOn.
        /// </summary>
        public bool UseSingleSignOn { get; set; }

        /// <summary>
        /// Gets or sets TextEditor.
        /// </summary>
        public string TextEditor { get; set; }

        /// <summary>
        /// Gets or sets Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets or sets AvatarImage.
        /// </summary>
        public byte[] AvatarImage { get; set; }

        /// <summary>
        /// Gets or sets AvatarImageType.
        /// </summary>
        public string AvatarImageType { get; set; }

        /// <summary>
        /// Gets or sets BoardID.
        /// </summary>
        public int? BoardID { get; set; }

        /// <summary>
        /// Gets or sets Culture.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets DailyDigest.
        /// </summary>
        public bool? DailyDigest { get; set; }

        /// <summary>
        /// Gets or sets DisplayName.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Flags.
        /// </summary>
        public int? Flags { get; set; }

        /// <summary>
        /// Gets or sets IP.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets IsActiveExcluded.
        /// </summary>
        public bool? IsActiveExcluded { get; set; }

        /// <summary>
        /// Gets or sets IsApproved.
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        /// Gets or sets IsGuest.
        /// </summary>
        public bool IsGuest { get; set; }

        /// <summary>
        /// Gets or sets Joined.
        /// </summary>
        public DateTime? Joined { get; set; }

        /// <summary>
        /// Gets or sets LanguageFile.
        /// </summary>
        public string LanguageFile { get; set; }

        /// <summary>
        /// Gets or sets LastVisit.
        /// </summary>
        public DateTime? LastVisit { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets NotificationType.
        /// </summary>
        public int? NotificationType { get; set; }

        /// <summary>
        /// Gets or sets NumPosts.
        /// </summary>
        public int? NumPosts { get; set; }

        /// <summary>
        /// Gets or sets UseMobileTheme.
        /// </summary>
        public bool? UseMobileTheme { get; set; }

        /// <summary>
        /// Gets or sets PMNotification.
        /// </summary>
        public bool? PMNotification { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets Points.
        /// </summary>
        public int? Points { get; set; }

        /// <summary>
        /// Gets or sets ProviderUserKey.
        /// </summary>
        public object ProviderUserKey { get; set; }

        /// <summary>
        /// Gets or sets RankID.
        /// </summary>
        public int? RankID { get; set; }

        /// <summary>
        /// Gets or sets Signature.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets Suspended.
        /// </summary>
        public DateTime? Suspended { get; set; }

        /// <summary>
        /// Gets or sets ThemeFile.
        /// </summary>
        public string ThemeFile { get; set; }

        /// <summary>
        /// Gets or sets TimeZone.
        /// </summary>
        public int? TimeZone { get; set; }

        /// <summary>
        /// Gets or sets UserID.
        /// </summary>
        public int? UserID { get; set; }

        #endregion
    }
}