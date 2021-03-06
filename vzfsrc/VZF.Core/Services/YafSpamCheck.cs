﻿#region copyright
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
 * File YafSpamCheck.cs created  on 2.6.2015 in  6:29 AM.
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
    using System;
    using System.Globalization;
    using System.Net;
    using System.Web;

    using YAF.Classes;
    using YAF.Core.Services.CheckForSpam;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    /// <summary>
    /// Yaf Spam Checking
    /// </summary>
    public class YafSpamCheck
    {
        /// <summary>
        /// Check a Post for SPAM against the BlogSpam.NET API or Akismet Service
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="postSubject">The post subject.</param>
        /// <param name="postMessage">The post message.</param>
        /// <returns>
        /// Returns if Post is SPAM or not
        /// </returns>
        public static bool IsPostSpam([NotNull]string userName, [NotNull]string postSubject, [NotNull]string postMessage)
        {
            if (YafContext.Current.Get<YafBoardSettings>().SpamServiceType.Equals(0))
            {
                return false;
            }

            string ipAdress = YafContext.Current.Get<HttpRequestBase>().GetUserRealIPAddress();

            if (ipAdress.Equals("::1"))
            {
                ipAdress = "127.0.0.1";
            }

            string whiteList = string.Empty;

            if (ipAdress.Equals("127.0.0.1"))
            {
                whiteList = "whitelist=127.0.0.1";
            }

            // Use BlogSpam.NET API
            if (YafContext.Current.Get<YafBoardSettings>().SpamServiceType.Equals(1))
            {
                try
                {
                    return
                        BlogSpamNet.CommentIsSpam(
                            new BlogSpamComment
                            {
                                comment = postMessage,
                                ip = ipAdress,
                                agent = YafContext.Current.Get<HttpRequestBase>().UserAgent,
                                name = userName,
                                options = whiteList,
                            },
                            true);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            // Use Akismet API
            if (YafContext.Current.Get<YafBoardSettings>().SpamServiceType.Equals(2) && !string.IsNullOrEmpty(YafContext.Current.Get<YafBoardSettings>().AkismetApiKey))
            {
                try
                {
                    var service = new AkismetSpamClient(YafContext.Current.Get<YafBoardSettings>().AkismetApiKey, new Uri(BaseUrlBuilder.BaseUrl));

                    return
                        service.CheckCommentForSpam(
                            new Comment(IPAddress.Parse(ipAdress), YafContext.Current.Get<HttpRequestBase>().UserAgent)
                            {
                                Content = postMessage,
                                Author = userName,
                            });
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
