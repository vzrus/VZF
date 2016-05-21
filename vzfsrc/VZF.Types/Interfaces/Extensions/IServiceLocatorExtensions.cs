
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

  /// <summary>
  /// The i service locator extensions.
  /// </summary>
  public static class IServiceLocatorExtensions
  {
    #region Public Methods

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IServiceLocator serviceLocator)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");

      return (TService)serviceLocator.Get(typeof(TService));
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IServiceLocator serviceLocator, [NotNull] string named)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");
      CodeContracts.ArgumentNotNull(named, "named");

      return (TService)serviceLocator.Get(typeof(TService), named);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <param name="parameters"></param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IServiceLocator serviceLocator, [NotNull] IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return (TService)serviceLocator.Get(typeof(TService), parameters);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="parameters"></param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IServiceLocator serviceLocator, [NotNull] string named, [NotNull] IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");
      CodeContracts.ArgumentNotNull(named, "named");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return (TService)serviceLocator.Get(typeof(TService), named, parameters);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IHaveServiceLocator haveLocator)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");

      return haveLocator.ServiceLocator.Get<TService>();
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IHaveServiceLocator haveLocator, [NotNull] string named)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");
      CodeContracts.ArgumentNotNull(named, "named");

      return haveLocator.ServiceLocator.Get<TService>(named);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IHaveServiceLocator haveLocator, [NotNull] IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return haveLocator.ServiceLocator.Get<TService>(parameters);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static TService Get<TService>([NotNull] this IHaveServiceLocator haveLocator, [NotNull] string named, [NotNull] IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");
      CodeContracts.ArgumentNotNull(named, "named");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return haveLocator.ServiceLocator.Get<TService>(named, parameters);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <param name="instance">
    /// The instance.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// The try get.
    /// </returns>
    public static bool TryGet<TService>([NotNull] this IServiceLocator serviceLocator, out TService instance)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");

      object tempInstance;

      instance = default(TService);

      if (serviceLocator.TryGet(typeof(TService), out tempInstance))
      {
        instance = (TService)tempInstance;

        return true;
      }

      return false;
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="instance">
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// The try get.
    /// </returns>
    public static bool TryGet<TService>(
      [NotNull] this IServiceLocator serviceLocator, [NotNull] string named, out TService instance)
    {
      CodeContracts.ArgumentNotNull(serviceLocator, "serviceLocator");
      CodeContracts.ArgumentNotNull(named, "named");

      object tempInstance;

      instance = default(TService);

      if (serviceLocator.TryGet(typeof(TService), named, out tempInstance))
      {
        instance = (TService)tempInstance;

        return true;
      }

      return false;
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <param name="instance">
    /// The instance.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// The try get.
    /// </returns>
    public static bool TryGet<TService>([NotNull] this IHaveServiceLocator haveLocator, out TService instance)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");

      return haveLocator.ServiceLocator.TryGet(out instance);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="haveLocator">
    /// The have locator.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="instance">
    /// The instance.
    /// </param>
    /// <typeparam name="TService">
    /// </typeparam>
    /// <returns>
    /// The try get.
    /// </returns>
    public static bool TryGet<TService>(
      [NotNull] this IHaveServiceLocator haveLocator, [NotNull] string named, out TService instance)
    {
      CodeContracts.ArgumentNotNull(haveLocator, "haveLocator");
      CodeContracts.ArgumentNotNull(named, "named");

      return haveLocator.ServiceLocator.TryGet(named, out instance);
    }

    #endregion
  }
}