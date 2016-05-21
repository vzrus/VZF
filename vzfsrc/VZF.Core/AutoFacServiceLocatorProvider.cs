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
 * File AutoFacServiceLocatorProvider.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  using Autofac;
  using Autofac.Core;

  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  using NamedParameter = YAF.Types.NamedParameter;
  using TypedParameter = YAF.Types.TypedParameter;

  #endregion

  /// <summary>
  /// The auto fac service locator provider.
  /// </summary>
  public class AutoFacServiceLocatorProvider : IServiceLocator, IInjectServices
  {
    #region Constants and Fields

    /// <summary>
    ///   The default flags.
    /// </summary>
    private const BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance;

    /// <summary>
    /// The _injection cache.
    /// </summary>
    private static readonly ConcurrentDictionary<KeyValuePair<Type, Type>, IList<PropertyInfo>> _injectionCache =
      new ConcurrentDictionary<KeyValuePair<Type, Type>, IList<PropertyInfo>>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoFacServiceLocatorProvider"/> class.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    public AutoFacServiceLocatorProvider([NotNull] ILifetimeScope container)
    {
      CodeContracts.ArgumentNotNull(container, "container");

      this.Container = container;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets Container.
    /// </summary>
    public ILifetimeScope Container { get; set; }

    #endregion

    #region Implemented Interfaces

    #region IInjectServices

    /// <summary>
    /// Inject an object with services.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// TAttribute is the attribute that marks properties to inject to.
    /// </typeparam>
    /// <param name="instance">
    /// the object to inject
    /// </param>
    public void InjectMarked<TAttribute>(object instance) where TAttribute : Attribute
    {
      CodeContracts.ArgumentNotNull(instance, "instance");

      // Container.InjectUnsetProperties(instance);
      var type = instance.GetType();
      var attributeType = typeof(TAttribute);

      var keyPair = new KeyValuePair<Type, Type>(type, attributeType);

      IList<PropertyInfo> properties;

      if (!_injectionCache.TryGetValue(keyPair, out properties))
      {
        // find them...
        properties =
          type.GetProperties(DefaultFlags).Where(
            p =>
            p.GetSetMethod(false) != null && !p.GetIndexParameters().Any() && p.IsDefined(attributeType, true)).ToList();

        _injectionCache.AddOrUpdate(keyPair, k => properties, (k, v) => properties);
      }

      foreach (var injectProp in properties)
      {
          object serviceInstance = injectProp.PropertyType == typeof(ILogger)
                                       ? this.Container.Resolve<ILoggerProvider>().Create(injectProp.DeclaringType)
                                       : this.Container.Resolve(injectProp.PropertyType);

          // set value is super slow... best not to use it very much.
        injectProp.SetValue(instance, serviceInstance, null);
      }
    }

    #endregion

    #region IServiceLocator

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceType">The service type.</param>
    /// <returns>
    /// The get.
    /// </returns>
    public object Get(Type serviceType)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");

      return this.Container.Resolve(serviceType);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceType">
    /// The service type.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The get.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <c>NotSupportedException</c>.
    /// </exception>
    public object Get(Type serviceType, IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return this.Container.Resolve(serviceType, ConvertToAutofacParameters(parameters));
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceType">
    /// The service type.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <returns>
    /// The get.
    /// </returns>
    public object Get(Type serviceType, string named)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");
      CodeContracts.ArgumentNotNull(named, "named");

      return this.Container.ResolveNamed(named, serviceType);
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="serviceType">
    /// The service type.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The get.
    /// </returns>
    public object Get(Type serviceType, string named, IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");
      CodeContracts.ArgumentNotNull(named, "named");
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      return this.Container.ResolveNamed(named, serviceType, ConvertToAutofacParameters(parameters));
    }

    /// <summary>
    /// The try get.
    /// </summary>
    /// <param name="serviceType">
    /// The service type.
    /// </param>
    /// <param name="instance">
    /// The instance.
    /// </param>
    /// <returns>
    /// The try get.
    /// </returns>
    public bool TryGet(Type serviceType, [NotNull] out object instance)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");

      return this.Container.TryResolve(out instance);
    }

    /// <summary>
    /// The try get.
    /// </summary>
    /// <param name="serviceType">
    /// The service type.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="instance">
    /// The instance.
    /// </param>
    /// <returns>
    /// The try get.
    /// </returns>
    public bool TryGet(Type serviceType, string named, [NotNull] out object instance)
    {
      CodeContracts.ArgumentNotNull(serviceType, "serviceType");
      CodeContracts.ArgumentNotNull(named, "named");

      return this.Container.TryResolve(out instance);
    }

    #endregion

    #region IServiceProvider

    /// <summary>
    /// Gets the service object of the specified type.
    /// </summary>
    /// <returns>
    /// A service object of type <paramref name="serviceType"/>.
    ///   -or- 
    ///   null if there is no service object of type <paramref name="serviceType"/>.
    /// </returns>
    /// <param name="serviceType">
    /// An object that specifies the type of service object to get. 
    /// </param>
    /// <filterpriority>2</filterpriority>
    [CanBeNull]
    public object GetService([NotNull] Type serviceType)
    {
      object instance;

      return this.TryGet(serviceType, out instance) ? instance : null;
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The convert to autofac parameters.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <exception cref="NotSupportedException">
    /// <c>NotSupportedException</c>.
    /// </exception>
    /// <exception cref="NotSupportedException">Parameter Type of is not supported.</exception>
    [NotNull]
    private static IEnumerable<Parameter> ConvertToAutofacParameters(
      [NotNull] IEnumerable<IServiceLocationParameter> parameters)
    {
      CodeContracts.ArgumentNotNull(parameters, "parameters");

      var autoParams = new List<Parameter>();

      foreach (var parameter in parameters)
      {
        if (parameter is NamedParameter)
        {
          var param = parameter as NamedParameter;
          autoParams.Add(new Autofac.NamedParameter(param.Name, param.Value));
        }
        else if (parameter is TypedParameter)
        {
          var param = parameter as TypedParameter;
          autoParams.Add(new Autofac.TypedParameter(param.Type, param.Value));
        }
        else
        {
          throw new NotSupportedException("Parameter Type of {0} is not supported.".FormatWith(parameter.GetType()));
        }
      }

      return autoParams;
    }

    #endregion
  }
}