
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
    /// The db function type.
    /// </summary>
    public enum DbFunctionType
    {
        /// <summary>
        ///   The query.
        /// </summary>
        Query,

        /// <summary>
        ///   The data table.
        /// </summary>
        DataTable,

        /// <summary>
        ///   The data set.
        /// </summary>
        DataSet,

        /// <summary>
        ///   The scalar.
        /// </summary>
        Scalar,

        /// <summary>
        ///   The reader.
        /// </summary>
        Reader
    }

    /// <summary>
    /// The db function cancelled exception.
    /// </summary>
    [Serializable]
    public class DbFunctionCancelledException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbFunctionCancelledException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DbFunctionCancelledException([NotNull] string message)
            : base(message)
        {
        }

        #endregion
    }

    /// <summary>
    /// The i db function.
    /// </summary>
    public interface IDbFunction
    {
        #region Properties

        /// <summary>
        ///   Gets GetData.
        /// </summary>
        dynamic GetData { get; }

        /// <summary>
        ///   Gets GetDataSet.
        /// </summary>
        dynamic GetDataSet { get; }

        /// <summary>
        ///   Gets Query.
        /// </summary>
        dynamic Query { get; }

        /// <summary>
        ///   Gets Scalar.
        /// </summary>
        dynamic Scalar { get; }

        /// <summary>
        /// Gets or sets the current Unit of Work used.
        /// </summary>
        IDbUnitOfWork UnitOfWork { get; set; }

        #endregion
    }
}