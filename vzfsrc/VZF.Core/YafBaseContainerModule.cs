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
 * File YafBaseContainerModule.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:05 PM.
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
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Core;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core.BBCode;
    using YAF.Core.Nntp;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The module for all singleton scoped items...
    /// </summary>
    public class YafBaseContainerModule : IModule, IHaveComponentRegistry
    {
        #region Properties

        /// <summary>
        ///   Gets or sets ComponentRegistry.
        /// </summary>
        public IComponentRegistry ComponentRegistry { get; set; }

        /// <summary>
        ///   Gets or sets ExtensionAssemblies.
        /// </summary>
        public IList<Assembly> ExtensionAssemblies { get; protected set; }

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

            this.ExtensionAssemblies =
                new YafModuleScanner().GetModules(Config.AllowedAssemblyMasks).OrderByDescending(x => x.GetAssemblySortOrder()).ToList();

            // handle registration...
            this.RegisterExternalModules();

            // external first...
            this.RegisterDynamicServices(this.ExtensionAssemblies.Where(a => a != Assembly.GetExecutingAssembly()));

            // internal bindings next...
            this.RegisterDynamicServices(new[] { Assembly.GetExecutingAssembly() });

            this.RegisterBasicBindings();
            this.RegisterEventBindings();
            this.RegisterMembershipProviders();
            this.RegisterServices();
            this.RegisterModules();
            this.RegisterPages();
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The register basic bindings.
        /// </summary>
        private void RegisterBasicBindings()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => this.ExtensionAssemblies)
                .Named<IList<Assembly>>("ExtensionAssemblies")
                .SingleInstance();

            builder.RegisterType<AutoFacServiceLocatorProvider>()
                .AsSelf()
                .As<IServiceLocator>()
                .As<IInjectServices>()
                .InstancePerLifetimeScope();

            // YafContext registration...
            builder.RegisterType<YafContextPageProvider>()
                .AsSelf()
                .As<IReadOnlyProvider<YafContext>>()
                .SingleInstance()
                .PreserveExistingDefaults();
            builder.Register((k) => k.Resolve<YafContextPageProvider>().Instance)
                .ExternallyOwned()
                .PreserveExistingDefaults();

            // Http Application Base
            builder.RegisterType<CurrentHttpApplicationStateBaseProvider>().SingleInstance().PreserveExistingDefaults();
            builder.Register(k => k.Resolve<CurrentHttpApplicationStateBaseProvider>().Instance)
                .ExternallyOwned()
                .PreserveExistingDefaults();

            // Task Module
            builder.RegisterType<CurrentTaskModuleProvider>().SingleInstance().PreserveExistingDefaults();
            builder.Register(k => k.Resolve<CurrentTaskModuleProvider>().Instance)
                .ExternallyOwned()
                .PreserveExistingDefaults();

            builder.RegisterType<YafNntp>().As<INewsreader>().InstancePerLifetimeScope().PreserveExistingDefaults();

            // optional defaults.
            builder.RegisterType<YafSendMail>().As<ISendMail>().SingleInstance().PreserveExistingDefaults();

            builder.RegisterType<YafSendNotification>()
                .As<ISendNotification>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            builder.RegisterType<YafDigest>().As<IDigest>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<DefaultUserDisplayName>()
                .As<IUserDisplayName>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<DefaultUrlBuilder>()
                .As<IUrlBuilder>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<YafBBCode>().As<IBBCode>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<YafFormatMessage>()
                .As<IFormatMessage>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<YafDBBroker>().As<IDBBroker>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<YafAvatars>().As<IAvatars>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<TreatCacheKeyWithBoard>()
                .As<ITreatCacheKey>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<CurrentBoardId>()
                .As<IHaveBoardId>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<CurrentModuleId>()
                .As<IHaveModuleId>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            builder.RegisterType<YafReadTrackCurrentUser>()
                .As<IReadTrackCurrentUser>()
                .InstancePerYafContext()
                .PreserveExistingDefaults();

            // cache bindings.
            builder.RegisterType<StaticLockObject>().As<IHaveLockObject>().SingleInstance().PreserveExistingDefaults();
            builder.RegisterType<HttpRuntimeCache>().As<IDataCache>().SingleInstance().PreserveExistingDefaults();

            // Shared object store -- used for objects local only
            builder.RegisterType<HttpRuntimeCache>().As<IObjectStore>().SingleInstance().PreserveExistingDefaults();

            builder.RegisterType<YafSession>().As<IYafSession>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<YafBadWordReplace>().As<IBadWordReplace>().SingleInstance().PreserveExistingDefaults();

            builder.RegisterType<YafPermissions>()
                .As<IPermissions>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<YafDateTime>().As<IDateTime>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.RegisterType<YafFavoriteTopic>()
                .As<IFavoriteTopic>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<YafUserIgnored>()
                .As<IUserIgnored>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();
            builder.RegisterType<YafBuddy>().As<IBuddy>().InstancePerLifetimeScope().PreserveExistingDefaults();

            // needs to be "instance per dependancy" so that each new request gets a new ScripBuilder.
            builder.RegisterType<JavaScriptBuilder>()
                .As<IScriptBuilder>()
                .InstancePerDependency()
                .PreserveExistingDefaults();

            // builder.RegisterType<RewriteUrlBuilder>().Named<IUrlBuilder>("rewriter").InstancePerLifetimeScope();
            builder.RegisterType<YafStopWatch>()
                .As<IStopWatch>()
                .InstancePerMatchingLifetimeScope(YafLifetimeScope.Context)
                .PreserveExistingDefaults();

            // localization registration...
            builder.RegisterType<LocalizationProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.Register(k => k.Resolve<LocalizationProvider>().Localization).PreserveExistingDefaults();

            // theme registration...
            builder.RegisterType<ThemeProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.Register(k => k.Resolve<ThemeProvider>().Theme).PreserveExistingDefaults();

            // replace rules registration...
            builder.RegisterType<ProcessReplaceRulesProvider>()
                .AsSelf()
                .As<IReadOnlyProvider<IProcessReplaceRules>>()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            builder.Register((k, p) => k.Resolve<ProcessReplaceRulesProvider>(p).Instance)
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            // module resolution bindings...
            builder.RegisterGeneric(typeof(StandardModuleManager<>))
                .As(typeof(IModuleManager<>))
                .InstancePerLifetimeScope();

            // background emailing...
            builder.RegisterType<YafSendMailThreaded>()
                .As<ISendMailThreaded>()
                .SingleInstance()
                .PreserveExistingDefaults();

            // board settings...
            builder.RegisterType<CurrentBoardSettings>()
                .AsSelf()
                .InstancePerMatchingLifetimeScope(YafLifetimeScope.Context)
                .PreserveExistingDefaults();
            builder.Register(k => k.Resolve<CurrentBoardSettings>().Instance)
                .ExternallyOwned()
                .PreserveExistingDefaults();

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// Register event bindings
        /// </summary>
        private void RegisterEventBindings()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ServiceLocatorEventRaiser>().As<IRaiseEvent>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(FireEvent<>)).As(typeof(IFireEvent<>)).InstancePerLifetimeScope();

            //// scan assemblies for events to wire up...
            // builder.RegisterAssemblyTypes(this.ExtensionAssemblies.ToArray()).AsClosedTypesOf(typeof(IHandleEvent<>)).
            // AsImplementedInterfaces().InstancePerLifetimeScope();
            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register external modules.
        /// </summary>
        private void RegisterExternalModules()
        {
            var builder = new ContainerBuilder();
        
            var modules =
                this.ExtensionAssemblies.Where(a => a != Assembly.GetExecutingAssembly()).FindModules<IModule>().Select(
                    m => Activator.CreateInstance(m) as IModule);
            foreach (var module in modules)
            {
                builder.RegisterModule(module);  
            }

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register services.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <exception cref="NotSupportedException"><c>NotSupportedException</c>.</exception>
        private void RegisterDynamicServices([NotNull] IEnumerable<Assembly> assemblies)
        {
            CodeContracts.ArgumentNotNull(assemblies, "assemblies");

            var builder = new ContainerBuilder();

            var classes = assemblies.FindClassesWithAttribute<ExportServiceAttribute>();

            var exclude = new List<Type> { typeof(IDisposable), typeof(IHaveServiceLocator), typeof(IHaveLocalization) };

            foreach (var c in classes)
            {
                var built = builder.RegisterType(c).As(c);

                var exportAttribute = c.GetAttribute<ExportServiceAttribute>();

                if (exportAttribute != null && exportAttribute.RegisterSpecifiedTypes != null
                    && exportAttribute.RegisterSpecifiedTypes.Length > 0)
                {
                    // only register types provided...
                    foreach (var regType in exportAttribute.RegisterSpecifiedTypes)
                    {
                        built.As(regType);
                    }
                }
                else
                {
                    // register all associated interfaces including inheritated interfaces!
                    foreach (var regType in c.GetInterfaces().Where(i => !exclude.Contains(i)))
                    {
                        built.As(regType);
                    }
                }

                if (exportAttribute == null || built == null)
                {
                    continue;
                }

                if (exportAttribute.Named.IsSet())
                {
                    built = built.Named(exportAttribute.Named, c.GetType());
                }

                switch (exportAttribute.ServiceLifetimeScope)
                {
                    case ServiceLifetimeScope.Singleton:
                        built.SingleInstance();
                        break;

                    case ServiceLifetimeScope.Transient:
                        built.ExternallyOwned();
                        break;

                    case ServiceLifetimeScope.OwnedByContainer:
                        built.OwnedByLifetimeScope();
                        break;

                    case ServiceLifetimeScope.InstancePerScope:
                        built.InstancePerLifetimeScope();
                        break;

                    case ServiceLifetimeScope.InstancePerDependancy:
                        built.InstancePerDependency();
                        break;

                    case ServiceLifetimeScope.InstancePerContext:
                        built.InstancePerMatchingLifetimeScope(YafLifetimeScope.Context);
                        break;
                }

                built.PreserveExistingDefaults();
            }

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// Register membership providers
        /// </summary>
        private void RegisterMembershipProviders()
        {
            var builder = new ContainerBuilder();

            // membership
            builder.RegisterType<CurrentMembershipProvider>().AsSelf().InstancePerLifetimeScope().
                PreserveExistingDefaults();
            builder.Register(x => x.Resolve<CurrentMembershipProvider>().Instance).ExternallyOwned().
                PreserveExistingDefaults();

            // roles
            builder.RegisterType<CurrentRoleProvider>().AsSelf().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.Register(x => x.Resolve<CurrentRoleProvider>().Instance).ExternallyOwned().PreserveExistingDefaults();

            // profiles
            builder.RegisterType<CurrentProfileProvider>().AsSelf().InstancePerLifetimeScope().PreserveExistingDefaults();
            builder.Register(x => x.Resolve<CurrentProfileProvider>().Instance).ExternallyOwned().
                PreserveExistingDefaults();

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register modules.
        /// </summary>
        private void RegisterModules()
        {
            var builder = new ContainerBuilder();

            // forum modules...
            builder.RegisterAssemblyTypes(this.ExtensionAssemblies.ToArray())
                .AssignableTo<IBaseForumModule>()
                .As<IBaseForumModule>()
                .InstancePerLifetimeScope();

            // editor modules...
            builder.RegisterAssemblyTypes(this.ExtensionAssemblies.ToArray())
                .AssignableTo<ForumEditor>()
                .As<ForumEditor>()
                .InstancePerLifetimeScope();

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register pages
        /// </summary>
        private void RegisterPages()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(this.ExtensionAssemblies.ToArray()).AssignableTo<ILocatablePage>().
                AsImplementedInterfaces().SingleInstance();

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register services.
        /// </summary>
        private void RegisterServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<IStartupService>()
                .As<IStartupService>()
                .InstancePerLifetimeScope();

            builder.Register(
                x =>
                x.Resolve<IEnumerable<IStartupService>>().FirstOrDefault(t => t is StartupInitializeDb) as
                StartupInitializeDb).InstancePerLifetimeScope();

            this.UpdateRegistry(builder);
        }

        #endregion
    }
}