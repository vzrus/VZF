
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


namespace YAF.Types.Interfaces
{
	#region Using

	using System;

	#endregion

	/// <summary>
	/// Read Tracking Interface for the Current User
	/// </summary>
	public interface IReadTrackCurrentUser
	{
		#region Public Methods

		/// <summary>
		/// Returns the last time that the forum was read or marked as Read.
		/// </summary>
		/// <param name="forumID">
		/// The forum ID of the Forum 
		/// </param>
		/// <returns>
		/// Returns the DateTime object from the Forum ID. 
		/// </returns>
		DateTime GetForumRead(int forumID, DateTime? readTimeOverride = null);

		/// <summary>
		/// Returns the last time that the Topic was read.
		/// </summary>
		/// <param name="topicID">
		/// The topicID you wish to find the DateTime object for. 
		/// </param>
		/// <returns>
		/// Returns the DateTime object from the topicID. 
		/// </returns>
		DateTime GetTopicRead(int topicID, DateTime? readTimeOverride = null);

		/// <summary>
		/// Get the Global Last Read DateTime a user Reads a topic or marks a forum as read
		/// </summary>
		/// <returns>
		/// Returns the DateTime object with the last read date. 
		/// </returns>
		DateTime LastRead { get; }

		/// <summary>
		/// Add Or Update The Forum Read DateTime
		/// </summary>
		/// <param name="forumID">
		/// The forum ID of the Forum 
		/// </param>
		void SetForumRead(int forumID);

		/// <summary>
		/// Add Or Update The topic Read DateTime
		/// </summary>
		/// <param name="topicID">
		/// The topic id to mark read.
		/// </param>
		void SetTopicRead(int topicID);

		#endregion
	}
}