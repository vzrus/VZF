
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


namespace YAF.Types.Interfaces.Extensions
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YAF.Types.Interfaces.Data;

    #endregion

    /// <summary>
    /// The db specific function extensions.
    /// </summary>
    public static class DbSpecificFunctionExtensions
    {
        #region Public Methods

        /// <summary>
        /// The is operation supported.
        /// </summary>
        /// <param name="functions">
        /// The functions.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <returns>
        /// The is operation supported.
        /// </returns>
        [NotNull]
        public static IEnumerable<IDbSpecificFunction> GetForProvider(
            [NotNull] this IEnumerable<IDbSpecificFunction> functions, [NotNull] string providerName)
        {
            CodeContracts.ArgumentNotNull(functions, "functions");
            CodeContracts.ArgumentNotNull(providerName, "providerName");

            return functions
                .Where(p => string.Equals(p.ProviderName, providerName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f.SortOrder);
        }

        /// <summary>
        /// The get for provider and operation.
        /// </summary>
        /// <param name="functions">
        /// The functions.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <param name="operationName">
        /// The operation name.
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        public static IEnumerable<IDbSpecificFunction> GetForProviderAndOperation(
            [NotNull] this IEnumerable<IDbSpecificFunction> functions, 
            [NotNull] string providerName, 
            [NotNull] string operationName)
        {
            CodeContracts.ArgumentNotNull(functions, "functions");
            CodeContracts.ArgumentNotNull(providerName, "providerName");
            CodeContracts.ArgumentNotNull(operationName, "operationName");

            return functions.GetForProvider(providerName).Where(s => s.IsSupportedOperation(operationName));
        }

        /// <summary>
        /// The is operation supported.
        /// </summary>
        /// <param name="functions">
        /// The functions.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <param name="operationName">
        /// The operation name.
        /// </param>
        /// <returns>
        /// The is operation supported.
        /// </returns>
        public static bool IsOperationSupported(
            [NotNull] this IEnumerable<IDbSpecificFunction> functions, 
            [NotNull] string providerName, 
            [NotNull] string operationName)
        {
            CodeContracts.ArgumentNotNull(functions, "functions");
            CodeContracts.ArgumentNotNull(providerName, "providerName");
            CodeContracts.ArgumentNotNull(operationName, "operationName");

            return functions.GetForProvider(providerName).Any(x => x.IsSupportedOperation(operationName));
        }

        #endregion
    }
}