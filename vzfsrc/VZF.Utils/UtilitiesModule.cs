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
 * File UtilitiesModule.cs created  on 2.6.2015 in  6:31 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
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

namespace VZF.Utils
{
  #region Using

  using System.Web;

  using Autofac;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The utilities module.
  /// </summary>
  public class UtilitiesModule : Module
  {
    #region Methods

    /// <summary>
    /// The load.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    protected override void Load([NotNull] ContainerBuilder builder)
    {
      builder.RegisterType<StyleTransform>().As<IStyleTransform>().InstancePerLifetimeScope();

      this.RegisterWebAbstractions(builder);
    }

    /// <summary>
    /// The register web abstractions.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    private void RegisterWebAbstractions([NotNull] ContainerBuilder builder)
    {
      CodeContracts.ArgumentNotNull(builder, "builder"); 

      builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerYafContext();

      builder.Register(c => c.Resolve<HttpContextBase>().Request).As<HttpRequestBase>().InstancePerYafContext();

      builder.Register(c => c.Resolve<HttpContextBase>().Response).As<HttpResponseBase>().InstancePerYafContext();

      builder.Register(c => c.Resolve<HttpContextBase>().Server).As<HttpServerUtilityBase>().InstancePerYafContext();

      builder.Register(c => c.Resolve<HttpContextBase>().Session).As<HttpSessionStateBase>().InstancePerYafContext();
    }

    #endregion
  }
}