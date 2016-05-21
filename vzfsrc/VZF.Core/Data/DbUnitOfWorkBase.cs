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
 * File DbUnitOfWorkBase.cs created  on 2.6.2015 in  6:29 AM.
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

	using System.Data;
	using System.Data.Common;

	using YAF.Types;
	using YAF.Types.Interfaces;

	#endregion

	/// <summary>
	/// The db unit of work base.
	/// </summary>
	public class DbUnitOfWorkBase : IDbUnitOfWork
	{
		#region Constants and Fields

		/// <summary>
		/// The _connection.
		/// </summary>
		private readonly DbConnection _connection;

		/// <summary>
		/// The _transaction.
		/// </summary>
		private readonly DbTransaction _transaction;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbUnitOfWorkBase"/> class.
		/// </summary>
		/// <param name="connection">
		/// The connection.
		/// </param>
		/// <param name="isolationLevel">
		/// The isolation level.
		/// </param>
		public DbUnitOfWorkBase(
			[NotNull] DbConnection connection, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
		{
			CodeContracts.ArgumentNotNull(connection, "connection");

			this._connection = connection;
			this._transaction = this._connection.BeginTransaction(isolationLevel);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets Transaction.
		/// </summary>
		public DbTransaction Transaction
		{
			get
			{
				return this._transaction;
			}
		}

		#endregion

		#region Implemented Interfaces

		#region IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			if (this.Transaction != null)
			{
				this.Transaction.Dispose();
			}

			if (this._connection != null)
			{
				if (this._connection.State == ConnectionState.Open)
				{
					this._connection.Close();
				}

				this._connection.Dispose();
			}
		}

		#endregion

		#endregion
	}
}