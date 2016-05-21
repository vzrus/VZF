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
 * File NameValueCollectionExtensions.cs created  on 2.6.2015 in  6:31 AM.
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

namespace VZF.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    using YAF.Types;

    /// <summary>
    /// The name value collection extensions.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get first or default.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="paramName">
        /// The param name.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFirstOrDefault(
            [NotNull] this NameValueCollection collection,
            [NotNull] string paramName,
            IEqualityComparer<string> comparer = null)
        {
            CodeContracts.ArgumentNotNull(collection, "collection");
            CodeContracts.ArgumentNotNull(paramName, "paramName");

            return collection.ToLookup(comparer)[paramName].FirstOrDefault();
        }

        /// <summary>
        /// The get first or default as.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="paramName">
        /// The param name.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetFirstOrDefaultAs<T>(
            [NotNull] this NameValueCollection collection,
            [NotNull] string paramName,
            IEqualityComparer<string> comparer = null)
        {
            CodeContracts.ArgumentNotNull(collection, "collection");
            CodeContracts.ArgumentNotNull(paramName, "paramName");

            return collection.GetFirstOrDefault(paramName, comparer).ToType<T>();
        }

        /// <summary>
        /// The get value list.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="paramName">
        /// The param name.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<string> GetValueList(
            [NotNull] this NameValueCollection collection, [NotNull] string paramName)
        {
            CodeContracts.ArgumentNotNull(collection, "collection");
            CodeContracts.ArgumentNotNull(paramName, "paramName");

            return collection[paramName] == null
                       ? Enumerable.Empty<string>()
                       : collection[paramName].Split(',').AsEnumerable();
        }

        /// <summary>
        /// The to lookup.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <returns>
        /// The <see cref="IDictionary{TKey,TValue}"/>.
        /// </returns>
        [NotNull]
        public static ILookup<string, string> ToLookup(
            [NotNull] this NameValueCollection collection, IEqualityComparer<string> comparer = null)
        {
            CodeContracts.ArgumentNotNull(collection, "collection");

            return collection.Cast<string>()
                             .ToLookup(key => key, key => collection[key], comparer ?? StringComparer.OrdinalIgnoreCase);
        }

        #endregion
    }
}