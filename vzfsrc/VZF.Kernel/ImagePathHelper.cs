#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File ImagePathHelper.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Web;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Interfaces;

    public static class ImagePathHelper
    {
        private static string GetTopicImageUploadPath(int topicId, string thumbClause, string topicImageName)
        {
            string uploadFileExtension = "yafupload";

            return "{0}/{1}{2}.{3}.{4}".FormatWith(
                TopicImageUploadDir, topicId, thumbClause, topicImageName, uploadFileExtension);
        }

        public static string GetTopicImageUploadFullPath(int topicId, string topicImageName)
        {
            return GetTopicImageUploadPath(topicId, string.Empty, topicImageName);
        }

        public static string GetTopicImageUploadThumbPath(int topicId, string topicImageName)
        {
            string thumbClause = ".thumb";
            return GetTopicImageUploadPath(topicId, thumbClause, topicImageName);
        }

        public static string TopicImageUploadDir
        {
            get
            {
                return YafContext.Current.Get<HttpRequestBase>()
                    .MapPath(
                        string.Concat(
                            BaseUrlBuilder.ServerFileRoot,
                            YafBoardFolders.Current.TopicImages));
            }
        }

    }
}
