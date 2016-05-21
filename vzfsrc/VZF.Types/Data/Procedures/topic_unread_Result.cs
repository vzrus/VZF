
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
    
    public partial class topic_unread_Result
    {
        public int ForumID { get; set; }
        public int TopicID { get; set; }
        public Nullable<int> TopicMovedID { get; set; }
        public System.DateTime Posted { get; set; }
        public int LinkTopicID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Styles { get; set; }
        public int UserID { get; set; }
        public string Starter { get; set; }
        public string StarterDisplay { get; set; }
        public Nullable<int> NumPostsDeleted { get; set; }
        public Nullable<int> Replies { get; set; }
        public int Views { get; set; }
        public Nullable<System.DateTime> LastPosted { get; set; }
        public Nullable<int> LastUserID { get; set; }
        public string LastUserName { get; set; }
        public string LastUserDisplayName { get; set; }
        public Nullable<int> LastMessageID { get; set; }
        public Nullable<int> LastMessageFlags { get; set; }
        public int LastTopicID { get; set; }
        public int TopicFlags { get; set; }
        public Nullable<int> FavoriteCount { get; set; }
        public short Priority { get; set; }
        public Nullable<int> PollID { get; set; }
        public string ForumName { get; set; }
        public Nullable<int> TopicMovedID1 { get; set; }
        public int ForumFlags { get; set; }
        public string FirstMessage { get; set; }
        public string StarterStyle { get; set; }
        public string LastUserStyle { get; set; }
        public Nullable<System.DateTime> LastForumAccess { get; set; }
        public Nullable<System.DateTime> LastTopicAccess { get; set; }
        public Nullable<int> TotalRows { get; set; }
        public Nullable<int> PageIndex { get; set; }
    }
}
