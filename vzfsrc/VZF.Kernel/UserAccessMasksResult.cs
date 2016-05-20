#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File UserAccessMasksResult.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20,.2016 in 2:56 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

namespace VZF.Kernel
{
    #region Using

    using System.Collections.Generic;

    using YAF.Types.Flags; 

    #endregion

    public class UserAccessMasksResult
    {
        public UserAccessMasksResult()
        {
            AccessMaskData = new List<AccessMask>();
        }

        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public int CurrentLevel { get; set; }
        public int NextLevel { get; set; }
       
        public List<AccessMask> AccessMaskData  { get; set; }

        public class AccessMask
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public int AccessMaskId { get; set; }
            public string AccessMaskName { get; set; }
            public bool IsUserMask { get; set; }
            public int AccessMaskFlags { get; set; }

            public bool ReadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ReadAccess; }
            }

            public bool PostAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PostAccess; }
            }

            public bool ReplyAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ReplyAccess; }
            }

            public bool PriorityAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PriorityAccess; }
            }

            public bool PollAccess
            {
                get { return new AccessFlags(AccessMaskFlags).PollAccess; }
            }

            public bool VoteAccess
            {
                get { return new AccessFlags(AccessMaskFlags).VoteAccess; }
            }

            public bool ModeratorAccess
            {
                get { return new AccessFlags(AccessMaskFlags).ModeratorAccess; }
            }

            public bool EditAccess
            {
                get { return new AccessFlags(AccessMaskFlags).EditAccess; }
            }

            public bool DeleteAccess
            {
                get { return new AccessFlags(AccessMaskFlags).DeleteAccess; }
            }

            public bool UploadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).UploadAccess; }
            }

            public bool DownloadAccess
            {
                get { return new AccessFlags(AccessMaskFlags).DownloadAccess; }
            }
        }
    }
}
