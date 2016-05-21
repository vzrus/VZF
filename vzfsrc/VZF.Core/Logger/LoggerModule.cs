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
 * File LoggerModule.cs created  on 2.6.2015 in  6:29 AM.
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

  using System.Linq;

  using Autofac;
  using Autofac.Core;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The logging module.
  /// </summary>
  public class LoggingModule : IModule, IHaveComponentRegistry
  {
    #region Properties

    /// <summary>
    ///   Gets or sets ComponentRegistry.
    /// </summary>
    public IComponentRegistry ComponentRegistry { get; set; }

    #endregion

    #region Implemented Interfaces

    #region IModule

    /// <summary>
    /// Apply the module to the component registry.
    /// </summary>
    /// <param name="componentRegistry">
    /// Component registry to apply configuration to.
    /// </param>
    public void Configure([NotNull] IComponentRegistry componentRegistry)
    {
      CodeContracts.ArgumentNotNull(componentRegistry, "componentRegistry");

      this.ComponentRegistry = componentRegistry;

      var moduleBuilder = new ContainerBuilder();
      this.Load(moduleBuilder);
      moduleBuilder.Update(componentRegistry);

      componentRegistry.Registered += (sender, e) => e.ComponentRegistration.Preparing += OnComponentPreparing;
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The load.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    protected void Load([NotNull] ContainerBuilder builder)
    {
      CodeContracts.ArgumentNotNull(builder, "builder");

      if (!this.IsRegistered<ILoggerProvider>())
      {
        builder.RegisterType<YafDbLoggerProvider>().As<ILoggerProvider>().SingleInstance();
        builder.Register(c => c.Resolve<ILoggerProvider>().Create(null)).SingleInstance();
      }
    }

    /// <summary>
    /// The on component preparing.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private static void OnComponentPreparing([NotNull] object sender, [NotNull] PreparingEventArgs e)
    {
      var t = e.Component.Activator.LimitType;
      e.Parameters =
        e.Parameters.Union(
          new[]
            {
              new ResolvedParameter(
                (p, i) => p.ParameterType == typeof(ILogger), (p, i) => i.Resolve<ILoggerProvider>().Create(t))
            });
    }

    #endregion
  }
}