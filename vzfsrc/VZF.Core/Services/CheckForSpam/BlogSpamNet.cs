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
 * File BlogSpamNet.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:04 PM.
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

namespace YAF.Core.Services.CheckForSpam
{
    #region

    using System;
    using CookComputing.XmlRpc;

    #endregion

    /// <summary>
    /// The blog spam.
    /// </summary>
    public class BlogSpamNet
    {
        #region Constants and Fields

        /// <summary>
        /// The _url.
        /// </summary>
        private const string _Url = "http://test.blogspam.net:8888/";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogSpamNet"/> class.
        /// </summary>
        public BlogSpamNet()
        {
            Url = new Uri(_Url);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogSpamNet"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        public BlogSpamNet(string url)
        {
            Url = new Uri(url);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        private static Uri Url { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Classify a Comment as SPAM true or false
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <param name="isSpam">
        /// The is spam.
        /// </param>
        /// <returns>
        /// The classify comment.
        /// </returns>
        public static string ClassifyComment(string comment, bool isSpam)
        {
            return GetProxy().classifyComment(new TrainComment { comment = comment, train = isSpam ? "spam" : "ok" });
        }

        /// <summary>
        /// Get stats for an Url
        /// </summary>
        /// <param name="siteUrl">
        /// The site url.
        /// </param>
        /// <returns>
        /// Returns the Stats
        /// </returns>
        public static Stats GetStats(string siteUrl)
        {
            return GetProxy().getStats(siteUrl);
        }

        /// <summary>
        /// Test a Comment for SPAM
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <param name="ignoreInternalIp">
        /// Ignore Internal Ip
        /// </param>
        /// <returns>
        /// Returns if Comment is SPAM
        /// </returns>
        public static bool CommentIsSpam(BlogSpamComment comment, bool ignoreInternalIp)
        {
            string answer = GetProxy().testComment(comment);

            // Handle interal Ips not as spam
            if (answer.Equals("SPAM:Internal Only IP") && ignoreInternalIp)
            {
                return false;
            }

            if (answer.Contains(":"))
            {
                answer = answer.Remove(answer.IndexOf(":"));
            }

            switch (answer)
            {
                case "OK":
                    return false;
                case "SPAM":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Test a Comment for SPAM
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// The test comment.
        /// </returns>
        public static string TestComment(BlogSpamComment comment)
        {
            return GetProxy().testComment(comment);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get proxy.
        /// </summary>
        /// <returns>
        /// The Proxy
        /// </returns>
        internal static IBlogSpamNet GetProxy()
        {
            IBlogSpamNet _proxy = (IBlogSpamNet)XmlRpcProxyGen.Create(typeof(IBlogSpamNet));
            XmlRpcClientProtocol _server = (XmlRpcClientProtocol)_proxy;
            _server.Url = (Url == null) ? _Url : Url.ToString();
            return _proxy;
        }

        #endregion
    }
}