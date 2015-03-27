/* Yet Another Forum.net
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
namespace VZF.Utils.Helpers
{
    #region Using

    using System;
    using System.Linq;
    using System.Web;

    using YAF.Classes;
    using YAF.Types;

    #endregion

    /// <summary>
    /// Helper for Figuring out the User Agent.
    /// </summary>
    public static class UserAgentHelper
    {
        #region Constants and Fields

        /// <summary>
        /// The spider contains.
        /// </summary>
        private static readonly string[] SpiderContains = Config.CrawlerUserAgentTokens;

        #endregion

        #region Public Methods

        /// <summary>
        /// Is this user agent IE v6?
        /// </summary>
        /// <returns>
        /// The is browser i e 6.
        /// </returns>
        public static bool IsBrowserIe6()
        {
            return HttpContext.Current.Request.Browser.Browser.Contains("IE")
                   && HttpContext.Current.Request.Browser.Version.StartsWith("6.");
        }

        /// <summary>
        /// Validates if the user agent owner is a feed reader
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The is feed reader.
        /// </returns>
        public static bool IsFeedReader([CanBeNull] string userAgent)
        {
            string[] agentContains = {"Windows-RSS-Platform","FeedDemon","Feedreader","Apple-PubSub"};

            return userAgent.IsSet()
                   && agentContains.Any(
                       agentContain => userAgent.ToLowerInvariant().Contains(agentContain.ToLowerInvariant()));
        }

        /// <summary>
        /// Validates if the user agent is a known ignored UA string
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The true if the UA string pattern should not be displayed in active users.
        /// </returns>
        public static bool IsIgnoredForDisplay([CanBeNull] string userAgent)
        {
            if (userAgent.IsSet())
            {
                // Apple-PubSub - Safary RSS reader
                string[] stringContains ={"PlaceHolder"};

                return stringContains.Any(x => userAgent.ToLowerInvariant().Contains(x.ToLowerInvariant()));
            }

            return false;
        }

        /// <summary>
        /// Tests if the user agent is a mobile device.
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The is mobile device.
        /// </returns>
        public static bool IsMobileDevice([CanBeNull] string userAgent)
        {
            var mobileContains =
                Config.MobileUserAgents.Split(',').Where(m => m.IsSet()).Select(m => m.Trim().ToLowerInvariant());

            return userAgent.IsSet()
                   && mobileContains.Any(s => userAgent.IndexOf(s, StringComparison.OrdinalIgnoreCase) > 0);
        }

        /// <summary>
        /// Sets if a user agent pattern is not checked against cookies support and JS.
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The Is Not Checked For Cookies And JS.
        /// </returns>
        public static bool IsNotCheckedForCookiesAndJs([CanBeNull] string userAgent)
        {
            if (userAgent.IsSet())
            {
                string[] userAgentContains = { "W3C_Validator" };
                return userAgentContains.Any(x => userAgent.ToLowerInvariant().Contains(x.ToLowerInvariant()));
            }

            return false;
        }

        /// <summary>
        /// Validates if the user agent is a search engine spider
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The is search engine spider.
        /// </returns>
        public static bool IsSearchEngineSpider([CanBeNull] string userAgent)
        {
            if (userAgent.IsNotSet())
            {
                return false;
            }

            string detectName;
            userAgent = userAgent.ToLowerInvariant();
            foreach (string spiderAll in SpiderContains)
            {
                if (spiderAll.Contains(":"))
                {
                    var namesArr = spiderAll.Split(new[] { ':' });
                    detectName = namesArr[0].Trim();

                }
                else
                {
                    detectName = spiderAll.Trim();

                }
                if (userAgent.Contains(detectName.ToLowerInvariant()))
                {
                    return true;

                }
            }

            return false;
        }

        /// <summary>
        /// Returns a platform user friendly name.
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <param name="isCrawler">
        /// Is Crawler.
        /// </param>
        /// <param name="platform">
        /// The platform.
        /// </param>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="isSearchEngine">
        /// Is search engine.
        /// </param>
        /// <param name="isIgnoredForDisplay">
        /// Is ignored for display. </param>
        public static void Platform(
            [CanBeNull] string userAgent,
            bool isCrawler,
            [NotNull] ref string platform,
            [NotNull] ref string browser,
            out bool isSearchEngine,
            out bool isIgnoredForDisplay)
        {
            CodeContracts.ArgumentNotNull(platform, "platform");

            isSearchEngine = false;

            if (userAgent.IsNotSet())
            {
                platform = "[Empty]";
                isIgnoredForDisplay = true;

                return;
            }

            if (userAgent.IndexOf("Windows NT 6.1", StringComparison.Ordinal) >= 0)
            {
                platform = "Win7";
            }
            else if (userAgent.IndexOf("Windows NT 6.2", StringComparison.Ordinal) >= 0)
            {
                platform = "Win8";
            }
            else if (userAgent.IndexOf("Windows NT 6.0", StringComparison.Ordinal) >= 0)
            {
                platform = "Vista";
            }
            else if (userAgent.IndexOf("Windows NT 5.1", StringComparison.Ordinal) >= 0)
            {
                platform = "WinXP";
            }
            else if (userAgent.IndexOf("Linux", StringComparison.Ordinal) >= 0)
            {
                platform = "Linux";
            }
            else if (userAgent.IndexOf("iPad", StringComparison.Ordinal) >= 0)
            {
                platform = "iPad(iOS)";
            }
            else if (userAgent.IndexOf("iPhone", StringComparison.Ordinal) >= 0)
            {
                platform = "iPhone(iOS)";
            }
            else if (userAgent.IndexOf("iPod", StringComparison.Ordinal) >= 0)
            {
                platform = "iPod(iOS)";
            }
            else if (userAgent.IndexOf("WindowsMobile", StringComparison.Ordinal) >= 0)
            {
                platform = "WindowsMobile";
            }
            else if (userAgent.IndexOf("webOS", StringComparison.Ordinal) >= 0)
            {
                platform = "WebOS";
            }
            else if (userAgent.IndexOf("Windows Phone OS", StringComparison.Ordinal) >= 0)
            {
                platform = "Windows Phone";
            }
            else if (userAgent.IndexOf("Android", StringComparison.Ordinal) >= 0)
            {
                platform = "Android";
            }
            else if (userAgent.IndexOf("Mac OS X", StringComparison.Ordinal) >= 0)
            {
                platform = "Mac OS X";
            }
            else if (userAgent.IndexOf("Windows NT 5.2", StringComparison.Ordinal) >= 0)
            {
                platform = "Win2003";
            }
            else if (userAgent.IndexOf("FreeBSD", StringComparison.Ordinal) >= 0)
            {
                platform = "FreeBSD";
            }

            // check if it's a search engine spider or an ignored UI string...
            var san = SearchEngineSpiderName(userAgent);
            if (san.IsSet())
            {
                browser = san;
            }

            isSearchEngine = san.IsSet() || isCrawler;
            isIgnoredForDisplay = IsIgnoredForDisplay(userAgent) | isSearchEngine;
        }

        /// <summary>
        /// Validates if the user agent is a search engine spider
        /// </summary>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <returns>
        /// The is search engine spider.
        /// </returns>
        public static string SearchEngineSpiderName([CanBeNull] string userAgent)
        {
            if (userAgent.IsNotSet())
            {
                return null;
            }

            string detectName;
            string displayName;
            userAgent = userAgent.ToLowerInvariant();
            foreach (string spiderAll in SpiderContains)
            {
                if (spiderAll.Contains(":"))
                {
                    var namesArr = spiderAll.Split(new[] {':'});
                    detectName = namesArr[0].Trim();
                    displayName = namesArr[1].Trim();

                }
                else
                {
                    detectName =
                        displayName = spiderAll.Trim();

                }
                if (userAgent.Contains(detectName.ToLowerInvariant()))
                {
                    return displayName;

                }
            }

            return null;
        }

        #endregion
    }
}