/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
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