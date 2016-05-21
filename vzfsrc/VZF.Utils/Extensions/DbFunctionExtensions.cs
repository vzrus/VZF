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
 * File DbFunctionExtensions.cs created  on 2.6.2015 in  6:31 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
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

namespace VZF.Utils.Extensions
{
	#region Using

	using System;
	using System.Collections.Generic;

	using YAF.Types;
	using YAF.Types.Interfaces;

	#endregion

	/// <summary>
	/// The db function extensions.
	/// </summary>
	public static class DbFunctionExtensions
	{
		#region Public Methods

		/// <summary>
		/// The get data typed.
		/// </summary>
		/// <param name="dbFunction">
		/// The db function.
		/// </param>
		/// <param name="function">
		/// The function.
		/// </param>
		/// <param name="comparer">
		/// The comparer.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		[CanBeNull]
		public static IEnumerable<T> GetDataTyped<T>(
			[NotNull] this IDbFunction dbFunction, 
			[NotNull] Func<object, object> function, 
			[CanBeNull] IEqualityComparer<string> comparer = null) where T : IDataLoadable, new()
		{
			CodeContracts.ArgumentNotNull(dbFunction, "dbFunction");
			CodeContracts.ArgumentNotNull(function, "function");

			return dbFunction.GetData(function).Typed<T>(comparer);
		}

		/// <summary>
		/// The get scalar as.
		/// </summary>
		/// <param name="dbFunction">
		/// The db function.
		/// </param>
		/// <param name="function">
		/// The function.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		[CanBeNull]
		public static T GetScalar<T>([NotNull] this IDbFunction dbFunction, [NotNull] Func<object, object> function)
		{
			CodeContracts.ArgumentNotNull(dbFunction, "dbFunction");
			CodeContracts.ArgumentNotNull(function, "function");

			return ((object)function(dbFunction.Scalar)).ToType<T>();
		}

		#endregion
	}
}