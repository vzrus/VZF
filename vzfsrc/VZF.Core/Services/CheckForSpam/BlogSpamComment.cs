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
 * File BlogSpamComment.cs created  on 2.6.2015 in  6:29 AM.
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
    using CookComputing.XmlRpc;

    /// <summary>
    /// The Blog Spam Comment.
    /// </summary>
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct BlogSpamComment
    {
        #region Properties

        /// <summary>
        /// Gets or sets agent.
        /// </summary>
        public string agent { get; set; }

        /// <summary>
        /// Gets or sets comment.
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Error)]
        [XmlRpcMember]
        public string comment { get; set; }

        /// <summary>
        /// Gets or sets email.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets ip.
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Error)]
        [XmlRpcMember]
        public string ip { get; set; }

        /// <summary>
        /// Gets or sets link.
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets options.
        /// </summary>
        public string options { get; set; }

        /// <summary>
        /// Gets or sets site.
        /// </summary>
        public string site { get; set; }

        /// <summary>
        /// Gets or sets subject.
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// Gets or sets version.
        /// </summary>
        public string version { get; set; }

        #endregion
    }
}