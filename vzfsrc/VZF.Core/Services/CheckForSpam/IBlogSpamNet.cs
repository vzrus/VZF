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
 * File IBlogSpamNet.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Services.CheckForSpam
{
    using CookComputing.XmlRpc;

    /// <summary>
    /// The BlogSpamNet Interface
    /// </summary>
    public interface IBlogSpamNet
    {
        #region Public Methods

        /// <summary>
        /// The classify comment.
        /// </summary>
        /// <param name="commentToTrain">
        /// The comment to train.
        /// </param>
        /// <returns>
        /// The classify comment.
        /// </returns>
        [XmlRpcMethod("classifyComment")]
        string classifyComment(TrainComment commentToTrain);

        /// <summary>
        /// The get stats.
        /// </summary>
        /// <param name="siteUrl">
        /// The site url.
        /// </param>
        /// <returns>
        /// Returns the Stats
        /// </returns>
        [XmlRpcMethod("getStats")]
        Stats getStats(string siteUrl);

        /// <summary>
        /// The test comment.
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// The test comment.
        /// </returns>
        [XmlRpcMethod("testComment")]
        string testComment(BlogSpamComment comment);

        #endregion
    }
}