
#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
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


//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VZF.Types.Data
{
    using System;
    
    public partial class user_list_Result
    {
        public int UserID { get; set; }
        public int BoardID { get; set; }
        public string ProviderUserKey { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public System.DateTime Joined { get; set; }
        public System.DateTime LastVisit { get; set; }
        public string IP { get; set; }
        public int NumPosts { get; set; }
        public int TimeZone { get; set; }
        public string Avatar { get; set; }
        public string Signature { get; set; }
        public byte[] AvatarImage { get; set; }
        public string AvatarImageType { get; set; }
        public int RankID { get; set; }
        public Nullable<System.DateTime> Suspended { get; set; }
        public string LanguageFile { get; set; }
        public string ThemeFile { get; set; }
        public bool UseSingleSignOn { get; set; }
        public string TextEditor { get; set; }
        public bool OverrideDefaultThemes { get; set; }
        public bool PMNotification { get; set; }
        public bool AutoWatchTopics { get; set; }
        public bool DailyDigest { get; set; }
        public Nullable<int> NotificationType { get; set; }
        public int Flags { get; set; }
        public int Points { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsGuest { get; set; }
        public Nullable<bool> IsCaptchaExcluded { get; set; }
        public Nullable<bool> IsActiveExcluded { get; set; }
        public Nullable<bool> IsDST { get; set; }
        public Nullable<bool> IsDirty { get; set; }
        public bool IsFacebookUser { get; set; }
        public bool IsTwitterUser { get; set; }
        public string Culture { get; set; }
        public string CultureUser { get; set; }
        public string RankName { get; set; }
        public string Style { get; set; }
        public Nullable<int> NumDays { get; set; }
        public Nullable<int> NumPostsForum { get; set; }
        public Nullable<int> HasAvatarImage { get; set; }
        public int IsAdmin { get; set; }
        public int IsGuest1 { get; set; }
        public int IsHostAdmin { get; set; }
        public int IsForumModerator { get; set; }
        public int IsModerator { get; set; }
    }
}
