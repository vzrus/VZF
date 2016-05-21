
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
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    using YAF.Types.Interfaces.Data;

    /// <summary>
    /// DBAccess Interface
    /// </summary>
    public interface IDbAccessV2
    {
        #region Properties

        /// <summary>
        ///   Gets or sets ConnectionString.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets DbConnectionParameters.
        /// </summary>
        IEnumerable<IDbConnectionParam> DbConnectionParameters { get; }

        /// <summary>
        ///   Gets the current db provider factory
        /// </summary>
        /// <returns>
        /// </returns>
        DbProviderFactory DbProviderFactory { get; }

        /// <summary>
        /// Gets FullTextScript.
        /// </summary>
        string FullTextScript { get; }

        /// <summary>
        ///   Gets ProviderName.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Gets Scripts.
        /// </summary>
        IEnumerable<string> Scripts { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        /// <returns>
        /// </returns>
        IDbUnitOfWork BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        /// <summary>
        /// The execute non query.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit Of Work.
        /// </param>
        void ExecuteNonQuery([NotNull] DbCommand cmd, [CanBeNull] IDbUnitOfWork unitOfWork = null);

        /// <summary>
        /// The execute scalar.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit Of Work.
        /// </param>
        /// <returns>
        /// The execute scalar.
        /// </returns>
        object ExecuteScalar([NotNull] DbCommand cmd, [CanBeNull] IDbUnitOfWork unitOfWork = null);

        /// <summary>
        /// The get command.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="isStoredProcedure">
        /// The is stored procedure.
        /// </param>
        /// <param name="parameters">
        /// Command Parameters
        /// </param>
        /// <returns>
        /// </returns>
        DbCommand GetCommand(
            [NotNull] string sql,
            bool isStoredProcedure = true,
            [CanBeNull] IEnumerable<KeyValuePair<string, object>> parameters = null);

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit Of Work.
        /// </param>
        /// <returns>
        /// </returns>
        DataTable GetData([NotNull] DbCommand cmd, [CanBeNull] IDbUnitOfWork unitOfWork = null);

        /// <summary>
        /// The get dataset.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit Of Work.
        /// </param>
        /// <returns>
        /// </returns>
        DataSet GetDataset([NotNull] DbCommand cmd, [CanBeNull] IDbUnitOfWork unitOfWork = null);

        #endregion
    }
}