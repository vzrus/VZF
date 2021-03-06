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
 * File BaseUrlBuilder.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:08 PM.
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

namespace YAF.Classes
{
    #region Using

    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;
    using System.Web.Hosting;

    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The base url builder.
    /// </summary>
    public abstract class BaseUrlBuilder : IUrlBuilder
    {
        #region Constants and Fields

        /// <summary>
        /// The _base urls.
        /// </summary>
        private static readonly StringDictionary _baseUrls = new StringDictionary();

        #endregion

        #region Properties

        /// <summary>
        /// Gets BaseUrl.
        /// </summary>
        /// <exception cref="BaseUrlMaskRequiredException">Since there is no active context, a base url mask is required. Please specify in the AppSettings in your web.config.</exception>
        public static string BaseUrl
        {
            get
            {
                string baseUrl;

                try
                {
                    if (HttpContext.Current != null)
                    {
                        // urlKey requires SERVER_NAME in case of systems that use HostNames for seperate sites or in our cases Boards as well as FilePath for multiboards in seperate folders.
                        var urlKey = string.Format(
                            "{0}{1}",
                            HttpContext.Current.Request.ServerVariables["SERVER_NAME"],
                            HttpContext.Current.Request.FilePath);

                        // Lookup the AppRoot based on the current host + path. 
                        baseUrl = _baseUrls[urlKey];

                        if (string.IsNullOrEmpty(baseUrl))
                        {
                            // Each different filepath (multiboard) will specify a AppRoot key in their own web.config in their directory.
                            baseUrl = !string.IsNullOrEmpty(Config.BaseUrlMask)
                                          ? TreatBaseUrl(Config.BaseUrlMask)
                                          : GetBaseUrlFromVariables();

                            // save to cache
                            _baseUrls[urlKey] = baseUrl;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Config.BaseUrlMask))
                        {
                            throw new BaseUrlMaskRequiredException(
                                "Since there is no active context, a base url mask is required. Please specify in the AppSettings in your web.config: YAF.BaseUrlMask");
                        }

                        baseUrl = TreatBaseUrl(Config.BaseUrlMask);
                    }
                }
                catch (Exception)
                {
                    baseUrl = GetBaseUrlFromVariables();
                }

                return baseUrl;
            }
        }

        /// <summary>
        /// Gets ClientFileRoot.
        /// </summary>
        public static string ClientFileRoot
        {
            get
            {
                string altRoot = Config.ClientFileRoot;

                if (string.IsNullOrEmpty(altRoot) && !string.IsNullOrEmpty(Config.AppRoot))
                {
                    // default to "AppRoot" if no file root specified and AppRoot specified...
                    altRoot = Config.AppRoot;
                }

                return TreatPathStr(altRoot);
            }
        }

        /// <summary>
        /// Gets ServerFileRoot.
        /// </summary>
        public static string ServerFileRoot
        {
            get
            {
                string altRoot = Config.ServerFileRoot;

                if (string.IsNullOrEmpty(altRoot) && !string.IsNullOrEmpty(Config.AppRoot))
                {
                    // default to "AppRoot" if no file root specified and AppRoot specified...
                    altRoot = Config.AppRoot;
                }

                return TreatPathStr(altRoot);
            }
        }

        /// <summary>
        /// Gets App Path.
        /// </summary>
        public static string AppPath
        {
            get
            {
                return TreatPathStr(Config.AppRoot);
            }
        }

        /// <summary>
        /// Gets ScriptName.
        /// </summary>
        public static string ScriptName
        {
            get
            {
                string scriptName = HttpContext.Current.Request.FilePath.ToLower();
                return scriptName.Substring(scriptName.LastIndexOf('/') + 1);
            }
        }

        /// <summary>
        /// Gets ScriptNamePath.
        /// </summary>
        public static string ScriptNamePath
        {
            get
            {
                string scriptName = HttpContext.Current.Request.FilePath.ToLower();
                return scriptName.Substring(0, scriptName.LastIndexOf('/'));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get base url from variables.
        /// </summary>
        /// <returns>
        /// The base url from variables.
        /// </returns>
        [NotNull]
        public static string GetBaseUrlFromVariables()
        {
            var url = new StringBuilder();

            long serverPort = long.Parse(HttpContext.Current.Request.ServerVariables["SERVER_PORT"]);
            bool isSecure = HttpContext.Current.Request.ServerVariables["HTTPS"].ToUpper() == "ON" || serverPort == 443;

            url.Append("http");

            if (isSecure)
            {
                url.Append("s");
            }

            url.AppendFormat("://{0}", HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);

            if ((!isSecure && serverPort != 80) || (isSecure && serverPort != 443))
            {
                url.AppendFormat(":{0}", serverPort);
            }

            return url.ToString();
        }

        #endregion

        #region Implemented Interfaces

        #region IUrlBuilder

        /// <summary>
        /// The build url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The build url.
        /// </returns>
        public abstract string BuildUrl(string url);

        /// <summary>
        /// The build url full.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The build url full.
        /// </returns>
        public virtual string BuildUrlFull(string url)
        {
            // append the full base server url to the beginning of the url (e.g. http://mydomain.com)
            return String.Format("{0}{1}", BaseUrl, this.BuildUrl(url));
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The treat base url.
        /// </summary>
        /// <param name="baseUrl">
        /// The base url.
        /// </param>
        /// <returns>
        /// The treat base url.
        /// </returns>
        protected static string TreatBaseUrl(string baseUrl)
        {
            if (baseUrl.EndsWith("/"))
            {
                // remove ending slash...
                baseUrl = baseUrl.Remove(baseUrl.Length - 1, 1);
            }

            return baseUrl;
        }

        /// <summary>
        /// The treat path str.
        /// </summary>
        /// <param name="altRoot">
        /// The alt root.
        /// </param>
        /// <returns>
        /// The treat path str.
        /// </returns>
        protected static string TreatPathStr(string altRoot)
        {
            string _path = string.Empty;

            try
            {
                _path = HostingEnvironment.ApplicationVirtualPath;

                if (!_path.EndsWith("/"))
                {
                    _path += "/";
                }

                if (!String.IsNullOrEmpty(altRoot))
                {
                    // use specified root
                    _path = altRoot;

                    if (_path.StartsWith("~"))
                    {
                        // transform with application path...
                        _path = _path.Replace("~", HostingEnvironment.ApplicationVirtualPath);
                    }

                    if (_path[0] != '/')
                    {
                        _path = _path.Insert(0, "/");
                    }
                }
                else if (Config.IsDotNetNuke)
                {
                    _path += "DesktopModules/YetAnotherForumDotNet/";
                }
                else if (Config.IsRainbow)
                {
                    _path += "DesktopModules/Forum/";
                }
                else if (Config.IsPortal)
                {
                    _path += "Modules/Forum/";
                }

                if (!_path.EndsWith("/"))
                {
                    _path += "/";
                }

                // remove redundant slashes...
                while (_path.Contains("//"))
                {
                    _path = _path.Replace("//", "/");
                }
            }
            catch (Exception)
            {
                _path = "/";
            }

            return _path;
        }

        #endregion
    }

    /// <summary>
    /// The base url mask required exception.
    /// </summary>
    [Serializable]
    public class BaseUrlMaskRequiredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUrlMaskRequiredException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BaseUrlMaskRequiredException(string message)
            : base(message)
        {
        }
    }
}