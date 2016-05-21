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
 * File DbAccessProvider.cs created  on 2.6.2015 in  6:29 AM.
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

	using Autofac.Features.Indexed;

	using YAF.Classes;
	using YAF.Types;
	using YAF.Types.Interfaces;
	using VZF.Utils;

	#endregion

	/// <summary>
	/// The db connection provider base.
	/// </summary>
	public class DbAccessProvider : IDbAccessProvider
	{
		#region Constants and Fields

		/// <summary>
		///   The _db access providers.
		/// </summary>
		private readonly IIndex<string, IDbAccessV2> _dbAccessProviders;

		/// <summary>
		///   The _last provider name.
		/// </summary>
		private readonly string _lastProviderName = string.Empty;

		/// <summary>
		///   The _service locator.
		/// </summary>
		private readonly IServiceLocator _serviceLocator;

		/// <summary>
		///   The _db access.
		/// </summary>
		private IDbAccessV2 _dbAccess;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbAccessProvider"/> class.
		/// </summary>
		/// <param name="dbAccessProviders">
		/// The db access providers. 
		/// </param>
		/// <param name="serviceLocator">
		/// The service locator. 
		/// </param>
		public DbAccessProvider(IIndex<string, IDbAccessV2> dbAccessProviders, IServiceLocator serviceLocator)
		{
			this._dbAccessProviders = dbAccessProviders;
			this._serviceLocator = serviceLocator;
			this.ProviderName = Config.ConnectionProviderName;
		}

		#endregion

		#region Public Properties

		/// <summary>
		///   The create.
		/// </summary>
		/// <returns> </returns>
		/// <exception cref="NoValidDbAccessProviderFoundException">
		///   <c>NoValidDbAccessProviderFoundException</c>
		///   .</exception>
		[CanBeNull]
		public IDbAccessV2 Instance
		{
			get
			{
				if (this._dbAccess != null && !this._lastProviderName.Equals(this.ProviderName))
				{
					this._dbAccess = null;
				}

				if (this._dbAccess == null && this.ProviderName.IsSet())
				{
					// attempt to get the provider...
					this._dbAccessProviders.TryGetValue(this.ProviderName.ToLower(), out this._dbAccess);
				}

				if (this._dbAccess == null)
				{
					throw new NoValidDbAccessProviderFoundException(
						@"No Valid Database Access Module Found for Provider Named ""{0}"".".FormatWith(this.ProviderName));
				}

				return this._dbAccess;
			}

			set
			{
				this._dbAccess = value;
				if (this._dbAccess != null)
				{
					this.ProviderName = this._dbAccess.ProviderName;
				}
			}
		}

		/// <summary>
		///   Gets or sets ProviderName.
		/// </summary>
		public string ProviderName { get; set; }

		#endregion
	}

	/// <summary>
	/// The no valid db access provider found exception.
	/// </summary>
	public class NoValidDbAccessProviderFoundException : Exception
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NoValidDbAccessProviderFoundException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public NoValidDbAccessProviderFoundException(string message)
			: base(message)
		{
		}

		#endregion
	}
}