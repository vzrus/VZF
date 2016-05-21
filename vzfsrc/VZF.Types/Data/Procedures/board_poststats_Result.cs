
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


namespace VZF.Types.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// Returnes board_stats_Result row representation.
    /// </summary>
    public class board_poststats_Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="board_poststats_Result"/> class.
        /// </summary>
        /// <param name="dr">
        /// The dr.
        /// </param>
        public board_poststats_Result(DataRow dr)
        {
            this.Posts = dr.Field<int?>("Posts");
            this.Topics = dr.Field<int?>("Topics");
            this.Forums = dr.Field<int?>("Forums");
            this.LastPostInfoID = dr.Field<int?>("LastPostInfoID");
            this.LastPost = dr.Field<DateTime?>("LastPost");
            this.LastUserID = dr.Field<int?>("LastUserID");
            this.LastUser = dr.Field<string>("LastUser");
            this.LastUserDisplayName = dr.Field<string>("LastUserDisplayName");
            this.LastUserStyle = dr.Field<string>("LastUserStyle");
        }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public int? Posts { get; set; }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        public int? Topics { get; set; }

        /// <summary>
        /// Gets or sets the forums.
        /// </summary>
        public int? Forums { get; set; }

        /// <summary>
        /// Gets or sets the last post info id.
        /// </summary>
        public int? LastPostInfoID { get; set; }

        /// <summary>
        /// Gets or sets the last post.
        /// </summary>
        public DateTime? LastPost { get; set; }

        /// <summary>
        /// Gets or sets the last user id.
        /// </summary>
        public int? LastUserID { get; set; }

        /// <summary>
        /// Gets or sets the last user.
        /// </summary>
        public string LastUser { get; set; }

        /// <summary>
        /// Gets or sets the last user display name.
        /// </summary>
        public string LastUserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the last user style.
        /// </summary>
        public string LastUserStyle { get; set; }
    }
}
