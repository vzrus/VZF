
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



namespace VZF.Types.Objects
{
    using System;

    /// <summary>
    /// Yaf User Info
    /// </summary>
    [Serializable]
    public class YafUserInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the realname.
        /// </summary>
        /// <value>
        /// The realname.
        /// </value>
        public string realname { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        public string avatar { get; set; }

        /// <summary>
        /// Gets or sets the interests.
        /// </summary>
        /// <value>
        /// The interests.
        /// </value>
        public string interests { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public string homepage { get; set; }

        /// <summary>
        /// Gets or sets the profilelink.
        /// </summary>
        /// <value>
        /// The profilelink.
        /// </value>
        public string profilelink { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        /// <value>
        /// The posts.
        /// </value>
        public string posts { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public string points { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string rank { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the joined.
        /// </summary>
        /// <value>
        /// The joined.
        /// </value>
        public string joined { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="YafUserInfo"/> is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if online; otherwise, <c>false</c>.
        /// </value>
        public bool online { get; set; }

        /// <summary>
        /// Gets or sets the action buttons.
        /// </summary>
        /// <value>
        /// The action buttons.
        /// </value>
        public string actionButtons { get; set; }
    }
}
