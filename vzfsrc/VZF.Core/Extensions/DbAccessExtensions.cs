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
 * File DbAccessExtensions.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Data.Common
{
    using System.Data.Common;

    using YAF.Classes;
    using YAF.Types;
	using YAF.Types.Interfaces;
	using YAF.Types.Interfaces.Extensions;

	/// <summary>
	/// The db access extensions.
	/// </summary>
	public static class DbAccessV2Extensions
	{
		#region Public Methods

		/// <summary>
		/// The replace command text.
		/// </summary>
		/// <param name="dbCommand">
		/// The db command.
		/// </param>
		public static DbCommand ReplaceCommandText([NotNull] this DbCommand dbCommand)
		{
			var commandText = dbCommand.CommandText;

			commandText = commandText.Replace("{databaseOwner}", Config.DatabaseOwner);
			commandText = commandText.Replace("{objectQualifier}", Config.DatabaseObjectQualifier);

			dbCommand.CommandText = commandText;

			return dbCommand;
		}

		/// <summary>
		/// Test the DB Connection.
		/// </summary>
		/// <param name="dbAccess">
		/// The db Access.
		/// </param>
		/// <param name="exceptionMessage">
		/// outbound ExceptionMessage
		/// </param>
		/// <returns>
		/// true if successfully connected
		/// </returns>
		public static bool TestConnection([NotNull] this IDbAccessV2 dbAccess, [NotNull] out string exceptionMessage)
		{
			exceptionMessage = string.Empty;
			bool success = false;

			try
			{
				using (var connection = dbAccess.CreateConnectionOpen())
				{
					// we're connected!
					var conn = connection;
				}

				// success
				success = true;
			}
			catch (DbException x)
			{
				// unable to connect...
				exceptionMessage = x.Message;
			}

			return success;
		}

		#endregion
	}
}