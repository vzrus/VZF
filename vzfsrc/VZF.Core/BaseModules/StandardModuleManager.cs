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
 * File StandardModuleManager.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core
{
	#region Using

	using System.Collections.Generic;
	using System.Linq;

	using YAF.Types;
	using YAF.Types.Interfaces;

	#endregion

	/// <summary>
	/// The standard module manager.
	/// </summary>
	/// <typeparam name="TModule">
	/// The module type based on IBaseModule.
	/// </typeparam>
	public class StandardModuleManager<TModule> : IModuleManager<TModule>
		where TModule : IBaseModule
	{
		#region Constants and Fields

		/// <summary>
		/// The _modules.
		/// </summary>
		private readonly IEnumerable<TModule> _modules;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StandardModuleManager{TModule}"/> class.
		/// </summary>
		/// <param name="modules">
		/// The modules.
		/// </param>
		public StandardModuleManager([NotNull] IEnumerable<TModule> modules)
		{
			CodeContracts.ArgumentNotNull(modules, "modules");

			this._modules = modules;
		}

		#endregion

		#region Implemented Interfaces

		#region IModuleManager<TModule>

		/// <summary>
		/// Get all instances of modules available.
		/// </summary>
		/// <param name="getInactive">
		/// The get Inactive.
		/// </param>
		/// <returns>
		/// </returns>
		public IEnumerable<TModule> GetAll(bool getInactive)
		{
			return !getInactive ? this._modules.Where(m => m.Active) : this._modules;
		}

		/// <summary>
		/// Get an instance of a module (based on it's id).
		/// </summary>
		/// <param name="id">
		/// The id.
		/// </param>
		/// <param name="getInactive">
		/// The get Inactive.
		/// </param>
		/// <returns>
		/// Instance of TModule or null if not found.
		/// </returns>
		public TModule GetBy([NotNull] string id, bool getInactive)
		{
			CodeContracts.ArgumentNotNull(id, "id");

			return !getInactive
								 ? this._modules.SingleOrDefault(e => e.ModuleId.Equals(id) && e.Active)
								 : this._modules.SingleOrDefault(e => e.ModuleId.Equals(id));
		}

		/// <summary>
		/// Get an instance of a module (based on it's id).
		/// </summary>
		/// <param name="id">
		/// The id.
		/// </param>
		/// <returns>
		/// Instance of TModule or null if not found.
		/// </returns>
		public TModule GetBy([NotNull] string id)
		{
			CodeContracts.ArgumentNotNull(id, "id");

			return this._modules.SingleOrDefault(e => e.ModuleId.Equals(id));
		}

		#endregion

		#endregion
	}
}