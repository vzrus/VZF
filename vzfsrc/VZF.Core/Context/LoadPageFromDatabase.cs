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
 * File LoadPageFromDatabase.cs created  on 2.6.2015 in  6:29 AM.
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

using VZF.Utils.Helpers;

namespace YAF.Core
{
    using System;
    using System.Web;
    using System.Web.Security;

    using VZF.Data.Common;
    using VZF.Types.Data;
    using VZF.Utils;
    using VZF.Utils.Extensions;


    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Extensions;

    /// <summary>
    /// The load page from database.
    /// </summary>
    [ExportService(ServiceLifetimeScope.InstancePerContext, null, typeof(IHandleEvent<InitPageLoadEvent>))]
    public class LoadPageFromDatabase : IHandleEvent<InitPageLoadEvent>, IHaveServiceLocator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPageFromDatabase"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="dataCache">The data cache.</param>
        public LoadPageFromDatabase(
            [NotNull] IServiceLocator serviceLocator, ILogger logger, [NotNull] IDataCache dataCache)
        {
            this.ServiceLocator = serviceLocator;
            this.Logger = logger;
            this.DataCache = dataCache;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets DataCache.
        /// </summary>
        public IDataCache DataCache { get; set; }

        /// <summary>
        ///   Gets Order.
        /// </summary>
        public int Order
        {
            get
            {
                return 1000;
            }
        }

        /// <summary>
        ///   Gets or sets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator { get; set; }

        #endregion

        #region Implemented Interfaces

        #region IHandleEvent<InitPageLoadEvent>

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        /// <exception cref="ApplicationException">Failed to find guest user.</exception>
        /// <exception cref="ApplicationException">Failed to create new user.</exception>
        /// <exception cref="ApplicationException">Unable to find the Guest User!</exception>
        public void Handle([NotNull] InitPageLoadEvent @event)
        {
            try
            {
                object userKey = null;

                if (YafContext.Current.User != null)
                {
                    userKey = YafContext.Current.User.ProviderUserKey;
                }

                int tries = 0;
                pageload_Result pageRow;

                // resources are not handled by ActiveLocation control so far.
                string forumPage = this.Get<HttpRequestBase>().QueryString.ToString();
                string location = this.Get<HttpRequestBase>().FilePath;
                if (location.Contains("resource.ashx") 
                    || location.IndexOf("digest.aspx", System.StringComparison.Ordinal) > 0)
                {
                    forumPage = string.Empty;
                    location = string.Empty;
                }

                do
                {
                   /* if (YafContext.Current.IsGuest)
                    {
                        object key = userKey;
                        pageRow = this.DataCache.GetOrSet(
                            Constants.Cache.UserForumAccessData.FormatWith(0, (string)@event.Data.CategoryID, (string)@event.Data.ForumID),
                            () =>
                                {
                                    DataRow pRow = CommonDb.pageload(
                                        mid: YafContext.Current.PageModuleID,
                                        sessionId: this.Get<HttpSessionStateBase>().SessionID,
                                        boardId: YafContext.Current.PageBoardID,
                                        userKey: key,
                                        ip: this.Get<HttpRequestBase>().GetUserRealIPAddress(),
                                        location: location,
                                        forumPage: forumPage,
                                        browser: @event.Data.Browser,
                                        platform: @event.Data.Platform,
                                        categoryId: (string)@event.Data.CategoryID,
                                        forumId: @event.Data.ForumID,
                                        topicId: @event.Data.TopicID,
                                        messageId: @event.Data.MessageID,
                                        // don't track if this is a search engine
                                        isCrawler: @event.Data.IsSearchEngine,
                                        isMobileDevice: @event.Data.IsMobileDevice,
                                        donttrack: @event.Data.DontTrack);
                                    return pRow;
                                },
                            TimeSpan.FromMinutes(this.Get<YafBoardSettings>().ActiveUserLazyDataCacheTimeout));
                    }
                    else
                    { */
                        pageRow = CommonDb.pageload(
                            mid: YafContext.Current.PageModuleID,
                            sessionId: this.Get<HttpSessionStateBase>().SessionID,
                            boardId: YafContext.Current.PageBoardID,
                            userKey: userKey,
                            ip: this.Get<HttpRequestBase>().GetUserRealIPAddress(),
                            location: location,
                            forumPage: forumPage,
                            browser: @event.Data.Browser,
                            platform: @event.Data.Platform,
                            categoryId: @event.Data.CategoryID,
                            forumId: @event.Data.ForumID,
                            topicId: @event.Data.TopicID,
                            messageId: @event.Data.MessageID,
                            // don't track if this is a search engine
                            isCrawler: @event.Data.IsSearchEngine,
                            isMobileDevice: @event.Data.IsMobileDevice,
                            donttrack: @event.Data.DontTrack);
                //    }

                    // if the user doesn't exist...
                    if (userKey != null && pageRow == null)
                    {
                        // create the user...
                        if (
                            !RoleMembershipHelper.DidCreateForumUser(
                                YafContext.Current.User, YafContext.Current.PageBoardID))
                        {
                            throw new ApplicationException("Failed to create new user.");
                        }
                    }

                    if (tries++ < 2)
                    {
                        continue;
                    }

                    if (userKey != null && pageRow == null)
                    {
                        // probably no permissions, use guest user instead...
                        userKey = null;
                        continue;
                    }

                    // fail...
                    break;
                }
                while (pageRow == null && userKey != null);

                if (pageRow == null)
                {
                    throw new ApplicationException("Unable to find the Guest User!");
                }

                // add all loaded page data into our data dictionary...
                @event.DataDictionary.AddRange(pageRow.AnyToDictionary());
          
                // clear active users list
                if (@event.DataDictionary["ActiveUpdate"].ToType<bool>())
                {
                    // purge the cache if something has changed...
                    this.DataCache.Remove(Constants.Cache.UsersOnlineStatus);
                }
            }
            catch (Exception x)
            {
 #if !DEBUG

    // log the exception...
				this.Logger.Fatal(x, "Failure Initializing User/Page.");

				// log the user out...
				FormsAuthentication.SignOut();

				if (YafContext.Current.ForumPageType != ForumPages.info)
				{
					// show a failure notice since something is probably up with membership...
					YafBuildLink.RedirectInfoPage(InfoMessage.Failure);
				}
				else
				{
					// totally failing... just re-throw the exception...
					throw;
				}
#else
                // re-throw exception...
                throw;
#endif
            }
        }

        #endregion

        #endregion
    }
}