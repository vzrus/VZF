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
 * File IModuleManagerExtensions.cs created  on 2.6.2015 in  6:29 AM.
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

  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The i module manager extensions.
  /// </summary>
  public static class IModuleManagerExtensions
  {
    #region Public Methods

    /// <summary>
    /// The get editors table.
    /// </summary>
    /// <typeparam name="TModule">
    /// </typeparam>
    /// <param name="moduleManager">
    /// The module Manager.
    /// </param>
    /// <param name="tableName">
    /// The table Name.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static DataTable ActiveAsDataTable<TModule>(
      [NotNull] this IModuleManager<TModule> moduleManager, [NotNull] string tableName) where TModule : IBaseModule
    {
      CodeContracts.ArgumentNotNull(moduleManager, "moduleManager");
      CodeContracts.ArgumentNotNull(tableName, "tableName");

      using (var dataTable = new DataTable(tableName))
      {
        dataTable.Columns.Add("Value", Type.GetType("System.Int32"));
        dataTable.Columns.Add("Name", Type.GetType("System.String"));

        foreach (var module in moduleManager.GetAll())
        {
          dataTable.Rows.Add(new object[] { module.ModuleId, module.Description });
        }

        return dataTable;
      }
    }

    /// <summary>
    /// Get a dictionary list discribing the active modules.
    /// </summary>
    /// <typeparam name="TModule">
    /// </typeparam>
    /// <param name="moduleManager">
    /// The module Manager.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static IDictionary<string, string> ActiveAsDictionary<TModule>(
      [NotNull] this IModuleManager<TModule> moduleManager) where TModule : IBaseModule
    {
      CodeContracts.ArgumentNotNull(moduleManager, "moduleManager");

      return moduleManager.GetAll().ToDictionary((mk) => mk.ModuleId, (mv) => mv.Description);
    }

    /// <summary>
    /// Get all active modules.
    /// </summary>
    /// <typeparam name="TModule">
    /// </typeparam>
    /// <param name="moduleManager">
    /// The module Manager.
    /// </param>
    /// <returns>
    /// </returns>
    public static IEnumerable<TModule> GetAll<TModule>([NotNull] this IModuleManager<TModule> moduleManager)
      where TModule : IBaseModule
    {
      CodeContracts.ArgumentNotNull(moduleManager, "moduleManager");

      return moduleManager.GetAll(false);
    }

    #endregion
  }
}