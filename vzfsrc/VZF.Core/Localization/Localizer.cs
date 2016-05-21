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
 * File Localizer.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using VZF.Utils;

    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    ///  Summary description for Localizer./// </summary>
    public class Localizer
    {
        #region Constants and Fields

        /// <summary>
        /// The _current culture.
        /// </summary>
        private CultureInfo _currentCulture;

        /// <summary>
        ///   The _current page.
        /// </summary>
        private string _currentPage = string.Empty;

        /// <summary>
        ///   The _file name.
        /// </summary>
        private string _fileName = string.Empty;

        /// <summary>
        /// The _localization resources.
        /// </summary>
        private LanguageResources _localizationLanguageResources;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Localizer" /> class.
        /// </summary>
        public Localizer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Localizer"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public Localizer(string fileName)
        {
            this._fileName = fileName;
            this.LoadFile();
            this.InitCulture();
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets LanguageCode.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get
            {
                return this._currentCulture;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get nodes using query.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The Nodes.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<LanuageResourcesPageResource> GetNodesUsingQuery(
            Func<LanuageResourcesPageResource, bool> predicate)
        {
            var pagePointer =
                this._localizationLanguageResources.page.FirstOrDefault(p => p.name.ToUpper().Equals(this._currentPage));

            return pagePointer != null
                       ? pagePointer.Resource.Where(predicate)
                       : this._localizationLanguageResources.page.SelectMany(p => p.Resource).Where(predicate);
        }

        /// <summary>
        /// The get nodes using query.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The Nodes.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<LanuageResourcesPageResource> GetCountryNodesUsingQuery(
            Func<LanuageResourcesPageResource, bool> predicate)
        {
            var pagePointer =
                this._localizationLanguageResources.page.FirstOrDefault(p => p.name.ToUpper().Equals(this._currentPage));

            return pagePointer != null
                       ? pagePointer.Resource.Where(predicate)
                       : this._localizationLanguageResources.page.SelectMany(p => p.Resource).Where(predicate);
        }

        /// <summary>
        /// The get nodes using query.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The Nodes.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IEnumerable<LanuageResourcesPageResource> GetRegionNodesUsingQuery(
            Func<LanuageResourcesPageResource, bool> predicate)
        {
            var pagePointer =
                this._localizationLanguageResources.page.FirstOrDefault(p => p.name.ToUpper().Equals(this._currentPage));

            return pagePointer != null
                       ? pagePointer.Resource.Where(predicate)
                       : this._localizationLanguageResources.page.SelectMany(p => p.Resource).Where(predicate);
        }

        /// <summary>
        /// The get text.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <param name="localizedText">
        /// The localized text.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void GetText(string tag, out string localizedText)
        {
            // default the out parameters
            localizedText = string.Empty;

            tag = tag.ToUpper(); //ToUpper(this._currentCulture);

            var pagePointer =
                this._localizationLanguageResources.page.FirstOrDefault(p => p.name.Equals(this._currentPage));

            LanuageResourcesPageResource pageResource = null;

            if (pagePointer != null)
            {
                pageResource = pagePointer.Resource.FirstOrDefault(r => r.tag.Equals(tag));
            }

            if (pageResource == null)
            {
                // attempt to find the tag anywhere...
                pageResource =
                    this._localizationLanguageResources.page.SelectMany(p => p.Resource)
                        .FirstOrDefault(r => r.tag.Equals(tag));
            }

            if (pageResource != null && pageResource.Value.IsSet())
            {
                localizedText = pageResource.Value;
            }
        }

        /// <summary>
        /// The get text.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The get text.
        /// </returns>
        public string GetText(string page, string tag)
        {
            string text;

            this.SetPage(page);
            this.GetText(tag, out text);

            return text;
        }

        /// <summary>
        /// The load file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void LoadFile(string fileName)
        {
            this._fileName = fileName;
            this.LoadFile();
        }

        /// <summary>
        /// The set page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        public void SetPage(string page)
        {
            if (this._currentPage == page)
            {
                return;
            }

            if (page.IsNotSet())
            {
                page = "DEFAULT";
            }

            this._currentPage = page.ToUpper();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The init culture.
        /// </summary>
        private void InitCulture()
        {
            if (!YafContext.Current.Get<StartupInitializeDb>().Initialized)
            {
                return;
            }


            var langCode = this.CurrentCulture.TwoLetterISOLanguageName;

            // vzrus: Culture code is missing for a user until he saved his profile.
            // First set it to board culture
            try
            {
                if (langCode.Equals(YafContext.Current.BoardSettings.Culture.Substring(0, 2)))
                {
                    this._currentCulture = new CultureInfo(YafContext.Current.BoardSettings.Culture);
                }
            }
            catch (Exception)
            {
                this._currentCulture = new CultureInfo(YafContext.Current.BoardSettings.Culture);
            }

            string cultureUser = YafContext.Current.CurrentUserData.CultureUser;

            if (!cultureUser.IsSet())
            {
                return;
            }



            if (cultureUser.Trim().Substring(0, 2).Equals(langCode))
            {
                this._currentCulture =
                    new CultureInfo(
                        cultureUser.Trim().Length > 5 ? cultureUser.Trim().Substring(0, 2) : cultureUser.Trim());
            }
        }

        /// <summary>
        /// The load file.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        private void LoadFile()
        {
            if (this._fileName == string.Empty || !File.Exists(this._fileName))
            {
                throw new ApplicationException("Invalid language file {0}".FormatWith(this._fileName));
            }

            this._localizationLanguageResources = new LoadSerializedXmlFile<LanguageResources>().FromFile(
                this._fileName,
                "LOCALIZATIONFILE{0}".FormatWith(this._fileName),
                (r) =>
                    {
                        // transform the page and tag name ToUpper...
                        r.page.ForEach(p => p.name = p.name.ToUpper());
                        r.page.ForEach(p => p.Resource.ForEach(i => i.tag = i.tag.ToUpper()));
                    });

            var userLanguageCode = this._localizationLanguageResources.code.IsSet()
                                       ? this._localizationLanguageResources.code.Trim()
                                       : "en-US";

            if (userLanguageCode.Length > 5)
            {
                userLanguageCode = this._localizationLanguageResources.code.Trim().Substring(0, 2);
            }

            this._currentCulture = new CultureInfo(userLanguageCode);
        }

        #endregion
    }
}