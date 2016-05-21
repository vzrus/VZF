
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
    public class board_stats_Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="board_stats_Result"/> class.
        /// </summary>
        /// <param name="dr">
        /// The dr.
        /// </param>
        public board_stats_Result(DataRow dr)
        {
            this.NumPosts = dr.Field<int>("NumPosts");
            this.NumTopics = dr.Field<int>("NumTopics");
            this.NumUsers = dr.Field<int>("NumUsers");
            this.BoardStart = dr.Field<DateTime>("BoardStart");
        }

        /// <summary>
        /// Gets or sets the num posts.
        /// </summary>
        public int? NumPosts { get; set; }

        /// <summary>
        /// Gets or sets the num topics.
        /// </summary>
        public int? NumTopics { get; set; }

        /// <summary>
        /// Gets or sets the num users.
        /// </summary>
        public int? NumUsers { get; set; }

        /// <summary>
        /// Gets or sets the board start.
        /// </summary>
        public DateTime? BoardStart { get; set; }
    }
}
