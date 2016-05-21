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
 * File GlobalContainer.cs created  on 2.6.2015 in  6:29 AM.
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

    using Autofac;

    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Instance of the Global Container... yes, a God class. It's the best way to do it, though.
    /// </summary>
    public static class GlobalContainer
    {
        #region Constants and Fields

        /// <summary>
        ///   The _container.
        /// </summary>
        private static readonly IContainer _container;

        #endregion

        /// <summary>
        /// Initializes static members of the <see cref="GlobalContainer"/> class.
        /// </summary>
        static GlobalContainer()
        {
            var container = CreateContainer();
            ServiceLocatorAccess.CurrentServiceProvider = container.Resolve<IServiceLocator>();
            _container = container;
        }

        #region Properties

        /// <summary>
        ///   Gets Container.
        /// </summary>
        public static IContainer Container
        {
            get
            {
                return _container;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// The create container.
        /// </summary>
        /// <returns>
        /// The <see cref="IContainer"/>.
        /// </returns>
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            var mainModule = new YafBaseContainerModule();
            var logModule = new LoggingModule();

            builder.RegisterModule(mainModule);
            builder.RegisterModule(logModule);

            return builder.Build();
        }

        #endregion
    }
}