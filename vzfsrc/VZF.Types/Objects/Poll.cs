
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
    using System.Collections.Generic;

    using YAF.Types.Flags;

    /// <summary>
    /// The poll.
    /// </summary>
    public class Poll
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Poll"/> class.
        /// </summary>
        public Poll()
        {
        }

        /// <summary>
        /// Gets or sets the poll id.
        /// </summary>
        public int PollID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the closes.
        /// </summary>
        public DateTime? Closes { get; set; }

        /// <summary>
        /// Gets or sets the poll group id.
        /// </summary>
        public int PollGroupID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the object path.
        /// </summary>
        public string ObjectPath { get; set; }

        /// <summary>
        /// Gets or sets the mime type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public PollFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the choices.
        /// </summary>
        public List<PollChoice> Choices { get; set; }
    }
}
