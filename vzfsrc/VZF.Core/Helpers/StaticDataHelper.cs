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
 * File StaticDataHelper.cs created  on 2.6.2015 in  6:29 AM.
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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using System.Collections.ObjectModel;
    using VZF.Types.Objects;

    /// <summary>
    /// The static data helper.
    /// </summary>
    public class StaticDataHelper
    {
        #region Public Methods

        /// <summary>
        /// The allow disallow.
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<AllowDisallow> AllowDisallow()
        {       

            string[] tTextArray = {
                                    YafContext.Current.Get<ILocalization>().GetText("COMMON", "ALLOWED"),
                                    YafContext.Current.Get<ILocalization>().GetText("COMMON", "DISALLOWED")
                                };

            for (int i = 0; i < tTextArray.Length; i++)
            {
               yield return new AllowDisallow() { Value = i, Text = tTextArray[i] };               
            }            
        }


        /// <summary>
        /// The country names list(localized).
        /// </summary>
        /// <param name="localization">
        /// The localization.
        /// </param>
        /// <param name="addEmptyRow">
        /// Add empty row to data table for dropdown lists with empty selection option.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumCountry> Country(ILocalization localization)
        {
         
            // Add an empty row to data table for dropdown lists with empty selection option.   
            yield return new ForumCountry();

            var countriesBefore =
             localization.GetCountryNodesUsingQuery("COUNTRY", x => x.tag.StartsWith(string.Empty)).ToList();

            // vzrus: a temporary hack - it returns all tags if the page is not found
            if (countriesBefore.Count <= 2000)
            {
                foreach (var node in countriesBefore)
                {
                    yield return new ForumCountry() { Name = node.Value, Value = node.tag };
                }
            }         
        }

        /// <summary>
        /// The country names list(localized).
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumCountry> Country()
        {
            return Country(YafContext.Current.Get<ILocalization>());
        }

        /// <summary>
        /// The country names list(localized).
        /// </summary>
        /// <param name="forceLanguage">
        /// The force a specific language.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumCountry> Country(string forceLanguage)
        {
            var localization = new YafLocalization();
            localization.LoadTranslation(forceLanguage);

            return Country(localization);
        }

        /// <summary>
        /// The country names list(localized).
        /// </summary>
        /// <param name="localization">
        /// The localization.
        /// </param>
        /// <param name="addEmptyRow">
        /// Add empty row to data table for dropdown lists with empty selection option.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<CountryRegion> Region(ILocalization localization, string culture)
        {
            var countryRegions = new List<CountryRegion>();
            // Add an empty row to data table for dropdown lists with empty selection option.   
            yield return new CountryRegion();

            var countries =
             localization.GetCountryNodesUsingQuery("REGION_{0}".FormatWith(culture), x => x.tag.StartsWith("RGN_{0}_".FormatWith(culture))).ToList();

            foreach (var node in countries)
            {
                yield return new CountryRegion()
                    {
                        Value = node.tag.Replace("RGN_{0}_".FormatWith(culture), string.Empty),
                        Name = node.Value
                    };
            }
        }

        /// <summary>
        /// The country names list(localized).
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<CountryRegion> Region(string culture)
        {
            return Region(YafContext.Current.Get<ILocalization>(), culture);
        }

        /// <summary>
        /// The cultures IetfLangTags (4-letter).
        /// </summary>
        /// <returns>
        /// The cultures filtered by first 2 letters in the language tag in a language file
        /// </returns>
        public static ReadOnlyCollection<ForumCultureInfo> Cultures()
        {
         
                // Get all language files info
                var dir =
                  new DirectoryInfo(
                    YafContext.Current.Get<HttpRequestBase>().MapPath("{0}languages".FormatWith(YafForumInfo.ForumServerFileRoot)));
                FileInfo[] files = dir.GetFiles("*.xml");

                // Create an array with tags
                string[,] tags = new string[2, files.Length];

                // Extract availabe language tags into the array          
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {

                        var doc = new XmlDocument();
                        doc.Load(files[i].FullName);
                        tags[0, i] = files[i].Name;
                        var attr = doc.DocumentElement.Attributes["code"];
                        if (attr != null)
                        {
                            tags[1, i] = attr.Value.Trim();
                        }
                        else
                        {
                            tags[1, i] = "en-US";
                        }

                    }
                    catch (Exception)
                    {
                    }
                }

                var lstCultures = new List<ForumCultureInfo>();

                foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                {
                    for (int j = 0; j < files.Length; j++)
                    {

                        if (!ci.IsNeutralCulture && tags[1, j].ToLower().Substring(0, 2).Contains(ci.TwoLetterISOLanguageName.ToLower()) /*&& ci.IetfLanguageTag.Length == 5*/)
                        {
                            lstCultures.Add(new ForumCultureInfo()
                                {
                                    IetfLanguageTag = ci.IetfLanguageTag,
                                    CultureFile = tags[0, j],
                                    DisplayName = ci.DisplayName,
                                    NativeName = ci.NativeName,
                                    EnglishName = ci.EnglishName
                                }
                                );
                        }
                    }
                }

                return lstCultures.AsReadOnly();           
        }

        /// <summary>
        /// Gets language tag info from language file tag.  
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A default full 4-letter culture from the existing language file.</returns>
        public static string CultureDefaultFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "en-US";

            string rawTag = null;
            // Get all language files info
            var dir =
              new DirectoryInfo(
                YafContext.Current.Get<HttpRequestBase>().MapPath("{0}languages".FormatWith(YafForumInfo.ForumServerFileRoot)));
            FileInfo[] files = dir.GetFiles(fileName);

            if (files.Length <= 0) return rawTag;
            try
            {
                var doc = new XmlDocument();
                doc.Load(files[0].FullName);
                rawTag = doc.DocumentElement.Attributes["code"].Value.Trim();

            }
            catch (Exception)
            {
            }

            System.Globalization.CultureInfo[] cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);
            foreach (System.Globalization.CultureInfo ci in cultures)
            {
                // We check only the language part as we need a default here.
                if (!ci.IsNeutralCulture && rawTag.ToLower().Substring(0, 2).Contains(ci.TwoLetterISOLanguageName.ToLower()) && ci.IetfLanguageTag.Length == 5)
                {
                    return ci.IetfLanguageTag;
                }
            }

            return "en-US";
        }

        /// <summary>
        /// The languages.
        /// </summary>
        /// <returns>
        /// </returns>
        public static ReadOnlyCollection<TranslationFileLanguage> Languages()
        {
            var languageFileLst = new List<TranslationFileLanguage>();

            var dir =
                 new DirectoryInfo(
                   YafContext.Current.Get<HttpRequestBase>().MapPath("{0}languages".FormatWith(YafForumInfo.ForumServerFileRoot)));
            
            FileInfo[] files = dir.GetFiles("*.xml");

            foreach (FileInfo file in files)
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.Load(file.FullName);
                    languageFileLst.Add(
                        new TranslationFileLanguage() { Language = doc.DocumentElement.Attributes["language"].Value, FileName = file.Name });
                   
                }
                catch (Exception)
                {
                }
            }        

            return languageFileLst.AsReadOnly();
        }   

        /// <summary>
        /// The themes.
        /// </summary>
        /// <returns>
        /// </returns>
        public static ReadOnlyCollection<ForumTheme> Themes()
        {
            var lstThemes = new List<ForumTheme>();
            var dir = new DirectoryInfo(YafContext.Current.Get<HttpRequestBase>().MapPath("{0}{1}".FormatWith(YafForumInfo.ForumServerFileRoot, YafBoardFolders.Current.Themes)));

            foreach (FileInfo file in dir.GetFiles("*.xml"))
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.Load(file.FullName);
                    var theme = new ForumTheme()
                        {
                            Theme = doc.DocumentElement.Attributes["theme"].Value,
                            FileName = file.Name,
                            IsMobile =  false
                        };

                    if (doc.DocumentElement.HasAttribute("ismobile"))
                    {
                        theme.IsMobile = Convert.ToBoolean(doc.DocumentElement.Attributes["ismobile"].Value ?? "false");
                    }

                    lstThemes.Add(theme);                 
                }
                catch (Exception)
                {
                }
            }

            return lstThemes.AsReadOnly();           
        }

        /// <summary>
        /// The themes.
        /// </summary>
        /// <returns>
        /// </returns>
        public static ReadOnlyCollection<JQueryUIThemes> JqueryUIThemes()
        {
            var lst = new List<JQueryUIThemes>();
            var themeDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(YafForumInfo.GetURLToResource("css/jquery-ui-themes")));

            foreach (DirectoryInfo dir in themeDir.GetDirectories())
            {
                try
                {
                    lst.Add(new JQueryUIThemes()
                    {
                        Theme = dir.Name
                    });
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return lst.AsReadOnly();      
        }

        /// <summary>
        /// The themes.
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<FancyTreeThemes> FancyTreeThemes()
        {
            var lst = new List<FancyTreeThemes>();
            var themeDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(YafForumInfo.GetURLToResource("css/fancytree")));

            foreach (DirectoryInfo dir in themeDir.GetDirectories())
            {
                try
                {
                    lst.Add(new FancyTreeThemes()
                    {
                        Theme = dir.Name
                    });                  
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return lst.AsReadOnly();          
        }
        /// <summary>
        /// The time zones.
        /// </summary>
        /// <param name="localization">
        /// The localization.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumTimeZones> TimeZones(ILocalization localization)
        {         
            var timezones =
                 localization.GetNodesUsingQuery("TIMEZONES", x => x.tag.StartsWith("UTC")).ToList();

            timezones.Sort(new YafLanguageNodeComparer());

            foreach (var node in timezones)
            {
               yield return new ForumTimeZones() { Name = node.Value, Value = node.GetHoursOffset() * 60 };              
            }                   
        }

        /// <summary>
        /// The time zones.
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumTimeZones> TimeZones()
        {
            return TimeZones(YafContext.Current.Get<ILocalization>());
        }

        /// <summary>
        /// The time zones.
        /// </summary>
        /// <param name="forceLanguage">
        /// The force language.
        /// </param>
        /// <returns>
        /// </returns>
        public static IEnumerable<ForumTimeZones> TimeZones(string forceLanguage)
        {
            var localization = new YafLocalization();
            localization.LoadTranslation(forceLanguage);

            return TimeZones(localization);
        }

        /// <summary>
        /// The topic times.
        /// </summary>
        /// <returns>
        /// </returns>
        public static IEnumerable<TopicTimes> TopicTimes()
        {         

                string[] tTextArray = {
                                "all", "last_day", "last_two_days", "last_week", "last_two_weeks", "last_month", 
                                "last_two_months", "last_six_months", "last_year"
                              };
                string[] tTextArrayProp = {
                                    "All", "Last Day", "Last Two Days", "Last Week", "Last Two Weeks", "Last Month", 
                                    "Last Two Months", "Last Six Months", "Last Year"
                                  };

                bool noTransPage = (YafContext.Current.Get<ILocalization>().TransPage == null);
                int countT = noTransPage
                                        ? tTextArrayProp.Count()
                                        : tTextArray.Count();

                for (int i = 0; i < countT; i++)
                {
                    yield return new TopicTimes()
                    {
                        TopicText = noTransPage
                                        ? tTextArrayProp[i]
                                        : YafContext.Current.Get<ILocalization>().GetText(tTextArray[i]),
                        TopicValue = i
                    };                    
                }                
        }

        #endregion
    }

    /// <summary>
    /// The yaf language node comparer.
    /// </summary>
    public class YafLanguageNodeComparer : IComparer<LanuageResourcesPageResource>
    {
        #region IComparer<XmlNode>

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The compare.
        /// </returns>
        public int Compare(LanuageResourcesPageResource x, LanuageResourcesPageResource y)
        {
            return x.GetHoursOffset().CompareTo(y.GetHoursOffset());
        }

        #endregion
    }
}