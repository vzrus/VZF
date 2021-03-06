#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File DynamicDbFunction.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Data
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Dynamic;
    using System.Linq;

    using YAF.Types;
    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Data;
    using YAF.Types.Interfaces.Extensions;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The dynamic db function.
    /// </summary>
    public class DynamicDbFunction : IDbFunction
    {
        #region Constants and Fields

        /// <summary>
        /// The _db access provider.
        /// </summary>
        private readonly IDbAccessProvider _dbAccessProvider;

        /// <summary>
        /// The _db specific functions.
        /// </summary>
        private readonly IEnumerable<IDbSpecificFunction> _dbSpecificFunctions;

        /// <summary>
        /// The _get data proxy.
        /// </summary>
        private readonly TryInvokeMemberProxy _getDataProxy;

        /// <summary>
        /// The _get data set proxy.
        /// </summary>
        private readonly TryInvokeMemberProxy _getDataSetProxy;

        /// <summary>
        /// The _query proxy.
        /// </summary>
        private readonly TryInvokeMemberProxy _queryProxy;

        /// <summary>
        /// The _scalar proxy.
        /// </summary>
        private readonly TryInvokeMemberProxy _scalarProxy;

        /// <summary>
        /// The _unit of work.
        /// </summary>
        private WeakReference _unitOfWork = new WeakReference(null);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicDbFunction"/> class.
        /// </summary>
        /// <param name="dbAccessProvider">
        /// The db Access Provider.
        /// </param>
        /// <param name="dbSpecificFunctions">
        /// The db Specific Functions. 
        /// </param>
        public DynamicDbFunction([NotNull] IDbAccessProvider dbAccessProvider, IEnumerable<IDbSpecificFunction> dbSpecificFunctions)
        {
            this._dbAccessProvider = dbAccessProvider;
            this._dbSpecificFunctions = dbSpecificFunctions;
            this._getDataProxy = new TryInvokeMemberProxy(this.InvokeGetData);
            this._getDataSetProxy = new TryInvokeMemberProxy(this.InvokeGetDataSet);
            this._queryProxy = new TryInvokeMemberProxy(this.InvokeQuery);
            this._scalarProxy = new TryInvokeMemberProxy(this.InvokeScalar);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets GetData.
        /// </summary>
        public dynamic GetData
        {
            get
            {
                return this._getDataProxy.ToDynamic();
            }
        }

        /// <summary>
        /// Gets GetDataSet.
        /// </summary>
        public dynamic GetDataSet
        {
            get
            {
                return this._getDataSetProxy.ToDynamic();
            }
        }

        /// <summary>
        /// Gets Query.
        /// </summary>
        public dynamic Query
        {
            get
            {
                return this._queryProxy.ToDynamic();
            }
        }

        /// <summary>
        /// Gets Scalar.
        /// </summary>
        public dynamic Scalar
        {
            get
            {
                return this._scalarProxy.ToDynamic();
            }
        }

        /// <summary>
        /// Gets or sets UnitOfWork.
        /// </summary>
        public virtual IDbUnitOfWork UnitOfWork
        {
            get
            {
                if (this._unitOfWork != null && this._unitOfWork.IsAlive)
                {
                    return this._unitOfWork.Target as IDbUnitOfWork;
                }

                return null;
            }

            set
            {
                this._unitOfWork = new WeakReference(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The db function execute.
        /// </summary>
        /// <param name="functionType">
        /// The function Type. 
        /// </param>
        /// <param name="binder">
        /// The binder. 
        /// </param>
        /// <param name="parameters">
        /// The parameters. 
        /// </param>
        /// <param name="executeDb">
        /// The execute db. 
        /// </param>
        /// <param name="result">
        /// The result. 
        /// </param>
        /// <returns>
        /// The db function execute. 
        /// </returns>
        protected bool DbFunctionExecute(
            DbFunctionType functionType, 
            [NotNull] InvokeMemberBinder binder, 
            [NotNull] IList<KeyValuePair<string, object>> parameters, 
            [NotNull] Func<DbCommand, object> executeDb, 
            [CanBeNull] out object result)
        {
            CodeContracts.ArgumentNotNull(binder, "binder");
            CodeContracts.ArgumentNotNull(parameters, "parameters");
            CodeContracts.ArgumentNotNull(executeDb, "executeDb");

            var operationName = binder.Name;

            // see if there's a specific function override for the current provider...
            var specificFunction = this._dbSpecificFunctions
                .GetForProviderAndOperation(this._dbAccessProvider.ProviderName, operationName)
                .FirstOrDefault();

            if (specificFunction == null)
            {
                throw new NotSupportedException(
                    string.Format("Operation: [{0}] Is not Supported for Provider: [{1}]", operationName, this._dbAccessProvider.ProviderName));
            }

            if (specificFunction.Execute(functionType, operationName, parameters, out result))
            {
                return true;
            }

            using (var cmd = this._dbAccessProvider.Instance.GetCommand(operationName.ToLower(), true, parameters))
            {
                result = executeDb(cmd);
            }

            return true;
        }

        /// <summary>
        /// The invoke get data.
        /// </summary>
        /// <param name="binder">
        /// The binder. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <param name="result">
        /// The result. 
        /// </param>
        /// <returns>
        /// The invoke get data. 
        /// </returns>
        protected bool InvokeGetData(
            [NotNull] InvokeMemberBinder binder, [NotNull] object[] args, [NotNull] out object result)
        {
            return this.DbFunctionExecute(
                DbFunctionType.DataTable, 
                binder, 
                this.MapParameters(binder.CallInfo, args), 
                (cmd) => this._dbAccessProvider.Instance.GetData(cmd, this.UnitOfWork), 
                out result);
        }

        /// <summary>
        /// The invoke get data set.
        /// </summary>
        /// <param name="binder">
        /// The binder. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <param name="result">
        /// The result. 
        /// </param>
        /// <returns>
        /// The invoke get data set. 
        /// </returns>
        protected bool InvokeGetDataSet(
            [NotNull] InvokeMemberBinder binder, [NotNull] object[] args, [NotNull] out object result)
        {
            return this.DbFunctionExecute(
                DbFunctionType.DataSet, 
                binder, 
                this.MapParameters(binder.CallInfo, args), 
                (cmd) => this._dbAccessProvider.Instance.GetDataset(cmd, this.UnitOfWork), 
                out result);
        }

        /// <summary>
        /// The invoke query.
        /// </summary>
        /// <param name="binder">
        /// The binder. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <param name="result">
        /// The result. 
        /// </param>
        /// <returns>
        /// The invoke query. 
        /// </returns>
        protected bool InvokeQuery([NotNull] InvokeMemberBinder binder, [NotNull] object[] args, [NotNull] out object result)
        {
            return this.DbFunctionExecute(
                DbFunctionType.Query, 
                binder, 
                this.MapParameters(binder.CallInfo, args), 
                (cmd) =>
                    {
                        this._dbAccessProvider.Instance.ExecuteNonQuery(cmd, this.UnitOfWork);
                        return null;
                    }, 
                out result);
        }

        /// <summary>
        /// The invoke scalar.
        /// </summary>
        /// <param name="binder">
        /// The binder. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <param name="result">
        /// The result. 
        /// </param>
        /// <returns>
        /// The invoke scalar. 
        /// </returns>
        protected bool InvokeScalar([NotNull] InvokeMemberBinder binder, [NotNull] object[] args, [NotNull] out object result)
        {
            return this.DbFunctionExecute(
                DbFunctionType.Scalar, 
                binder, 
                this.MapParameters(binder.CallInfo, args), 
                (cmd) => this._dbAccessProvider.Instance.ExecuteScalar(cmd, this.UnitOfWork), 
                out result);
        }

        /// <summary>
        /// The map parameters.
        /// </summary>
        /// <param name="callInfo">
        /// The call info. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <returns>
        /// </returns>
        [NotNull]
        protected IList<KeyValuePair<string, object>> MapParameters([NotNull] CallInfo callInfo, [NotNull] object[] args)
        {
            CodeContracts.ArgumentNotNull(callInfo, "callInfo");
            CodeContracts.ArgumentNotNull(args, "args");

            return args
                .Reverse()
                .Zip(callInfo.ArgumentNames.Reverse().Infinite(), (a, name) => new KeyValuePair<string, object>(name, a))
                .Reverse()
                .ToList();
        }

        #endregion
    }
}