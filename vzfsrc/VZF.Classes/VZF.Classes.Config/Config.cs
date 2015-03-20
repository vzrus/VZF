/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
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

namespace YAF.Classes
{
    #region Using

    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    using YAF.Types;

    #endregion

    /// <summary>
    ///     Static class that access the app settings in the web.config file
    /// </summary>
    public static class Config 
    {
        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether AllowLoginAndLogoff.
        /// </summary>
        public static bool AllowLoginAndLogoff
        {
            get
            {
                return GetConfigValueAsBool("YAF.AllowLoginAndLogoff", true);
            }
        }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        [NotNull]
        public static string ApplicationName
        {
            get
            {
                return GetConfigValueAsString("YAF.ApplicationName") ?? "YetAnotherForum";
            }
        }

        /// <summary>
        ///     Gets AppRoot.
        /// </summary>
        [NotNull]
        public static string AppRoot
        {
            get
            {
                return GetConfigValueAsString("YAF.AppRoot") ?? string.Empty;
            }
        }

        /// <summary>
        ///  Gets Loaded  assemblies allowed masks like VZF..
        /// </summary>
        [NotNull]
        public static string AllowedAssemblyMasks
        {
            get
            {
                return GetConfigValueAsString("VZF.AllowedAssemblyMasks") ?? "VZF*.dll";
            }
        }

        /// <summary>
        ///     Gets the Used for Url Rewriting -- default is "default.\.(.+)$\.(.+)$"
        /// </summary>
        [NotNull]
        public static string BaseScriptFile
        {
            get
            {
                return GetConfigValueAsString("YAF.BaseScriptFile") ?? "default.aspx";
            }
        }

        /// <summary>
        ///     Gets BaseUrlMask.
        /// </summary>
        [NotNull]
        public static string BaseUrlMask
        {
            get
            {
                return GetConfigValueAsString("YAF.BaseUrlMask") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets the Current BoardID -- default is 1.
        /// </summary>
        [NotNull]
        public static string BoardId
        {
            get
            {
                return GetConfigValueAsString("YAF.BoardID") ?? "1";
            }
        }

        /// <summary>
        ///     Gets the Folder to use for board specific uploads, images, themes Example : /Boards/
        /// </summary>
        [NotNull]
        public static string BoardRoot
        {
            get
            {
                return GetConfigValueAsString("YAF.BoardRoot") ?? string.Empty; // Use / to signify root
            }
        }

        /// <summary>
        ///     Gets BotScout API Key.
        /// </summary>
        public static string BotScoutApiKey
        {
            get
            {
                return GetConfigValueAsString("YAF.BotScoutApiKey");
            }
        }

        /// <summary>
        ///     Gets the Allowed Crawler User Agent Tokens.
        /// </summary>
        public static string[] CrawlerUserAgentTokens
        {
            get
            {
                if (!string.IsNullOrEmpty(GetConfigValueAsString("VZF.CrawlerUserAgentTokens")))
                {
                    return GetConfigValueAsString("VZF.CrawlerUserAgentTokens").Split();
                }

                return new[]
                           {
                               "Googlebot", "abachoBOT", "abcdatos_botlink", "ah-ha.com crawler", "Alexa", "antibot",
                               "appie", "AltaVista-Intranet", "Acoon Robot", "Atomz", "Arachnoidea", "AESOP_com_SpiderMan", 
                               "AxmoRobot", "ArchitextSpider", "AlkalineBOT", "Aranha", "asterias", "Baidu", "Bingbot",
                               "Buscaplus Robi", "CanSeek", "ChristCRAWLER", "Clushbot", "Crawler", "CrawlerBoy",
                               "DeepIndex", "DefaultCrawler", "DittoSpyder", "DIIbot", "EZResult", "EARTHCOM.info",
                               "EuripBot", "ESISmartSpider", "FAST-WebCrawler", "FyberSearch", "Findexa Crawler", "Fluffy",
                               "Googlebot", "geckobot", "GenCrawler", "GeonaBot", "getRAX", "Gulliver", "Hubater",
                               "ia_archiver", "Slurp", "Scooter", "Mercator", "RaBot", "Jack", "Speedy Spider", "moget",
                               "Toutatis", "IlTrovatore-Setaccio", "IncyWincy", "UltraSeek", "InfoSeek Sidewinder",
                               "Mole2", "MP3Bot", "Knowledge.com", "kuloko-bot", "LNSpiderguy", "Linknzbot", "lookbot",
                               "MantraAgent", "NetResearchServer", "Lycos", "JoocerBot", "HenryTheMiragoRobot",
                               "MojeekBot", "mozDex", "MSNBOT", "Navadoo Crawler", "ObjectsSearch", "OnetSzukaj",
                               "PicoSearch", "PJspider", "nttdirectory_robot", "maxbot.com", "Openfind", "psbot",
                               "QweeryBot", "StackRambler", "SeznamBot", "Search-10", "Scrubby", "speedfind ramBot xtreme", 
                               "Kototoi", "SearchByUsa", "Searchspider", "SightQuestBot", "Sogou", "Spider_Monkey",
                               "Surfnomore", "teoma", "UK Searcher Spider", "Nazilla", "MuscatFerret", "ZyBorg",
                               "WIRE WebRefiner", "WSCbot", "Yandex", "Yellopet-Spider", "YBSbot", "OceanSpiders",
                               "MozSpider"
                           };
            }
        }

        /// <summary>
        ///     Gets the Allowed browser JS version.
        /// </summary>
        public static string BrowserJsVersion
        {
            get
            {
                return GetConfigValueAsString("YAF.BrowserJSVersion") ?? "1.0";
            }
        }

        /// <summary>
        ///     Gets the Current CategoryID -- default is null.
        /// </summary>
        public static string CategoryId
        {
            get
            {
                return GetConfigValueAsString("YAF.CategoryID");
            }
        }

        /// <summary>
        ///     Gets ClientFileRoot.
        /// </summary>
        [NotNull]
        public static string ClientFileRoot
        {
            get
            {
                return GetConfigValueAsString("YAF.ClientFileRoot") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether CSS Blocks should be compressed (minified)? -- default is true.
        /// </summary>
        public static bool CompressCssBlocks
        {
            get
            {
                return GetConfigValueAsBool("YAF.CompressCSSBlocks", true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether JavaScript Blocks should be compressed (minified) -- default is true.
        /// </summary>
        public static bool CompressJsBlocks
        {
            get
            {
                return GetConfigValueAsBool("YAF.CompressJSBlocks", true);
            }
        }

        /// <summary>
        /// Gets the connection provider name.
        /// </summary>
        public static string ConnectionProviderName
        {
            get
            {
                return ConnectionStringSettings.ProviderName;
            }
        }

        /// <summary>
        ///     Gets ConnectionString.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConnectionStringSettings.ConnectionString;
            }
        }

        /// <summary>
        ///     Gets ConnectionStringName.
        /// </summary>
        [NotNull]
        public static string ConnectionStringName
        {
            get
            {
                return GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }
        }

        /// <summary>
        /// Gets the connection string settings.
        /// </summary>
        public static ConnectionStringSettings ConnectionStringSettings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[ConnectionStringName];
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the Use it if distinct YAF role names are required. Used in integration environments only.
        /// </summary>
        public static bool CreateDistinctRoles
        {
            get
            {
                return GetConfigValueAsBool("YAF.CreateDistinctRoles", false);
            }
        }

        /// <summary>
        ///     Gets DatabaseCollation.
        /// </summary>
        public static string DatabaseCollation
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseCollation") ?? string.Empty;
            }
        }

        public static string DatabaseGranteeName
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseGranteeName") ?? string.Empty;
            }
        }

        public static string DatabaseHostName
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseHostName");
            }
        }

        /// <summary>
        ///     Gets DatabaseEncoding.
        /// </summary>
        public static string DatabaseEncoding
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseEncoding");
            }
        }

        /// <summary>
        ///     Gets DatabaseObjectQualifier.
        /// </summary>
        [NotNull]
        public static string DatabaseObjectQualifier
        {
            get
            {          

                return GetConfigValueAsString("YAF.DatabaseObjectQualifier");
            }
        }     

        /// <summary>
        ///     Gets DatabaseOwner.
        /// </summary>
        [Obsolete("Legacy: Phasing out, use DatabaseSchemaName")] 
        [NotNull]
        public static string DatabaseOwner
        {
            get
            {
                var schema = GetConfigValueAsString("YAF.DatabaseSchemaName");
                if (string.IsNullOrEmpty(schema))
                {
                    return GetConfigValueAsString("YAF.DatabaseOwner") ?? "dbo";
                }

                return schema;
            }
        }

        /// <summary>
        ///     Gets DatabaseSchemaName.
        /// </summary>
        [NotNull]
        public static string DatabaseSchemaName
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseSchemaName") ?? "public";
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Is JQuery Registration disabled? -- default is false.
        /// </summary>
        public static bool DisableJQuery
        {
            get
            {
                return GetConfigValueAsBool("YAF.DisableJQuery", false);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Is Url Rewriting enabled? -- default is false.
        /// </summary>
        public static bool EnableUrlRewriting
        {
            get
            {
                return GetConfigValueAsBool("YAF.EnableUrlRewriting", false);
            }
        }

        /// <summary>
        /// Gets Facebook API Key.
        /// </summary>
        public static string FacebookApiKey
        {
            get
            {
                return GetConfigValueAsString("YAF.FacebookAPIKey");
            }
        }

        /// <summary>
        ///     Gets Facebook Secret Key.
        /// </summary>
        public static string FacebookSecretKey
        {
            get
            {
                return GetConfigValueAsString("YAF.FacebookSecretKey");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Used for URL Rewriting -- default is null -- used to define what the forum file name is for URL's.
        /// </summary>
        public static string ForceScriptName
        {
            get
            {
                return GetConfigValueAsString("YAF.ForceScriptName");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsAnyPortal.
        /// </summary>
        public static bool IsAnyPortal
        {
            get
            {
                return IsDotNetNuke || IsMojoPortal || IsRainbow || IsPortal || IsPortalomatic;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsDotNetNuke.
        /// </summary>
        public static bool IsDotNetNuke
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    object obj = HttpContext.Current.Items["PortalSettings"];
                    return obj != null && obj.GetType().ToString().ToLower().IndexOf("dotnetnuke", StringComparison.Ordinal) >= 0;
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsMojoPortal.
        /// </summary>
        public static bool IsMojoPortal
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    object obj = HttpContext.Current.Items["SiteSettings"];
                    return obj != null && obj.GetType().ToString().ToLower().IndexOf("mojoportal", StringComparison.Ordinal) >= 0;
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsPortal.
        /// </summary>
        public static bool IsPortal
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session["YetAnotherPortal.net"] != null;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsPortalomatic.
        /// </summary>
        public static bool IsPortalomatic
        {
            get
            {
                return HttpContext.Current != null && HttpContext.Current.Session["Portalomatic.NET"] != null;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether IsRainbow.
        /// </summary>
        public static bool IsRainbow
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    object obj = HttpContext.Current.Items["PortalSettings"];
                    return obj != null && obj.GetType().ToString().ToLower().IndexOf("rainbow", StringComparison.Ordinal) >= 0;
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets jQuery Alias
        /// </summary>
        [NotNull]
        public static string JQueryAlias
        {
            get
            {
                if (IsDotNetNuke)
                {
                    return "jQuery";
                }

                string jQueryAlias = GetConfigValueAsString("YAF.JQueryAlias") ?? "jQuery";

                if (!jQueryAlias.Equals("jQuery") || !jQueryAlias.Equals("$"))
                {
                    return "jQuery";
                }

                return jQueryAlias;
            }
        }

        /// <summary>
        ///     Gets JQuery File Name.
        /// </summary>
        [NotNull]
        public static string JQueryFile
        {
            get
            {
                return GetConfigValueAsString("YAF.JQuery") ?? "js/jquery.min.js";
            }
        }

        /// <summary>
        /// Gets JQuery UI File Name.
        /// </summary>
        [NotNull]
        public static string JQueryUIFile
        {
            get
            {
                return GetConfigValueAsString("YAF.JQueryUIFile") ??
                       "js/jquery-ui.min.js";
            }
        }

        /// <summary>
        ///     Gets jQuery UI date picker lang File Name.
        /// </summary>
        [NotNull]
        public static string JQueryUILangFile
        {
            get
            {
                return GetConfigValueAsString("YAF.JQueryUILangFile") ??
                       "js/jquery-ui-i18n.min.js";
            }
        }

        /// <summary>
        ///     Gets a value indicating whether YAF should be optimized for use with very large number of forums. Before enabling you should know exactly what you do and what's it all for. Ask developers.
        /// </summary>
        [NotNull]
        public static bool LargeForumTree
        {
            get
            {
                return GetConfigValueAsBool("VZF.LargeForumTree", true);
            }
        }

        /// <summary>
        ///     Gets LogToMail.
        /// </summary>
        [Obsolete("Legacy: Phasing out")]
        public static string LogToMail
        {
            get
            {
                return GetConfigValueAsString("YAF.LogToMail");
            }
        }

        /// <summary>
        ///     Gets MembershipProvider.
        /// </summary>
        [NotNull]
        public static string MembershipProvider
        {
            get
            {
                return GetConfigValueAsString("YAF.MembershipProvider") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets MobileUserAgents.
        /// </summary>
        [NotNull]
        public static string MobileUserAgents
        {
            get
            {
                return GetConfigValueAsString("YAF.MobileUserAgents") ??
                       "iphone,ipad,midp,windows ce,windows phone,android,blackberry,opera mini,mobile,palm,portable,webos,htc,armv,lg/u,elaine,nokia,playstation,symbian,sonyericsson,mmp,hd_mini";
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Boolean to force uploads, and images, themes etc.. from a specific BoardID folder within BoardRoot Example : true /false
        /// </summary>
        public static bool MultiBoardFolders
        {
            get
            {
                return GetConfigValueAsBool("YAF.MultiBoardFolders", false);
            }
        }

        /// <summary>
        ///     Gets the NNTP post domain.
        /// </summary>
        [NotNull]
        public static string NntpPostDomain
        {
            get
            {
                return GetConfigValueAsString("YAF.NntpPostDomain") ?? "myforum.com";
            }
        }

        /// <summary>
        ///     Gets OverrideTrustLevel.
        /// </summary>
        public static string OverrideTrustLevel
        {
            get
            {
                return GetConfigValueAsString("YAF.OverrideTrustLevel");
            }
        }

        /// <summary>
        ///     Gets ProviderKeyType.
        /// </summary>
        [NotNull]
        public static string ProviderKeyType
        {
            get
            {
                return GetConfigValueAsString("YAF.ProviderKeyType") ?? "System.Guid";
            }
        }

        /// <summary>
        ///     Gets ProfileProvider.
        /// </summary>
        [NotNull]
        public static string ProviderProvider
        {
            get
            {
                return GetConfigValueAsString("YAF.ProfileProvider") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets RadEditorSkin.
        /// </summary>
        [NotNull]
        public static string RadEditorSkin
        {
            get
            {
                return GetConfigValueAsString("YAF.RadEditorSkin") ?? "Vista";
            }
        }

        /// <summary>
        ///     Gets RadEditorToolsFile.
        /// </summary>
        [NotNull]
        public static string RadEditorToolsFile
        {
            get
            {
                return GetConfigValueAsString("YAF.RadEditorToolsFile") ??
                       string.Format("{0}/editors/RadEditor/ToolsFile.xml", ServerFileRoot);
            }
        }

        /// <summary>
        ///     Gets RoleProvider.
        /// </summary>
        [NotNull]
        public static string RoleProvider
        {
            get
            {
                return GetConfigValueAsString("YAF.RoleProvider") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets ServerFileRoot.
        /// </summary>
        [NotNull]
        public static string ServerFileRoot
        {
            get
            {
                return GetConfigValueAsString("YAF.FileRoot") ?? GetConfigValueAsString("YAF.ServerFileRoot") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Display the footer at the bottom of the page -- default is "true"
        /// </summary>
        public static bool ShowFooter
        {
            get
            {
                return GetConfigValueAsBool("YAF.ShowFooter", true);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Display the default toolbar at the top -- default is "true"
        /// </summary>
        public static bool ShowToolBar
        {
            get
            {
                return GetConfigValueAsBool("YAF.ShowToolBar", true);
            }
        }

        /// <summary>
        ///     Gets the Current BoardID -- default is 1.
        /// </summary>
        [NotNull]
        public static string SqlCommandTimeout
        {
            get
            {
                return GetConfigValueAsString("YAF.SqlCommandTimeout") ?? "99999";
            }
        }

        /// <summary>
        /// Gets the comma-delimited net adapters with which we don't close connection.
        /// </summary>
        public static string NetAdaptersOpenedConnection
        {
            get
            {
                return GetConfigValueAsString("VZF.NetAdaptersOpenedConnection") ?? "Npgsql";
            }
        }

        /// <summary>
        ///     Gets TwitterConsumerKey
        /// </summary>
        public static string TwitterConsumerKey
        {
            get
            {
                return GetConfigValueAsString("YAF.TwitterConsumerKey");
            }
        }

        /// <summary>
        ///     Gets TwitterConsumerSecret
        /// </summary>
        public static string TwitterConsumerSecret
        {
            get
            {
                return GetConfigValueAsString("YAF.TwitterConsumerSecret");
            }
        }

        /// <summary>
        ///     Gets the Url Rewriting URLRewritingMode? -- default is Unicode.
        /// </summary>
        [NotNull]
        public static string UrlRewritingMode
        {
            get
            {
                return GetConfigValueAsString("YAF.URLRewritingMode") ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets the Prefix used for Url Rewriting -- default is "vzf_"
        /// </summary>
        public static string UrlRewritingPrefix
        {
            get
            {
                return GetConfigValueAsString("YAF.UrlRewritingPrefix") ?? "vzf_";
            }
        }

        /// <summary>
        ///     Gets a value indicating whether UseRadEditorToolsFile.
        /// </summary>
        public static bool UseRadEditorToolsFile
        {
            get
            {
                string value = GetConfigValueAsString("YAF.UseRadEditorToolsFile");

                if (!string.IsNullOrEmpty(value))
                {
                    switch (value.ToLower().Substring(0, 1))
                    {
                        case "1":
                        case "t":
                        case "y":
                            return true;
                        case "0":
                        case "f":
                        case "n":
                            return false;
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether Use an SSL connection for the SMTP server -- default is "false"
        /// </summary>
        public static bool UseSMTPSSL
        {
            get
            {
                return GetConfigValueAsBool("YAF.UseSMTPSSL", false);
            }
        }

        /// <summary>
        ///     Gets WithOIDs.
        /// </summary>
        public static string WithOIDs
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseWithOIDs");
            }
        }

        #endregion 

        #region Public Methods and Operators

        /// <summary>
        ///     Gets the config value as bool.
        /// </summary>
        /// <param name="configKey"> The config key. </param>
        /// <param name="defaultValue"> if set to <c>true</c> [default value]. </param>
        /// <returns> Returns Bool Value </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static bool GetConfigValueAsBool([NotNull] string configKey, bool defaultValue)
        {
            string value = GetConfigValueAsString(configKey);

            return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value.ToLower()) : defaultValue;
        }

        /// <summary>
        ///     Gets the config value as string.
        /// </summary>
        /// <param name="configKey"> The config key. </param>
        /// <returns> Returns String Value </returns>
        public static string GetConfigValueAsString([NotNull] string configKey)
        {
            return (from key in WebConfigurationManager.AppSettings.AllKeys
                    where key.Equals(configKey, StringComparison.CurrentCultureIgnoreCase)
                    select WebConfigurationManager.AppSettings[key]).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the config value as string.
        /// </summary>
        /// <param name="configKey"> The config key. </param>
        /// <returns> Returns String Value </returns>
        public static string GetConfigValueAsStringByPattern([NotNull] string configKey, string pattern)
        {
            return (from key in WebConfigurationManager.AppSettings.AllKeys
                    where (key.Contains(configKey) && key.Contains(pattern))
                    select WebConfigurationManager.AppSettings[key]).FirstOrDefault();
        }

        /// <summary>
        ///     Gets a Provider type string from the config.
        /// </summary>
        /// <param name="providerName"> The provider Name. </param>
        /// <returns> Provider type string or <see langword="null" /> if none exist. </returns>
        public static string GetProvider([NotNull] string providerName)
        {
            string key = string.Format("YAF.Provider.{0}", providerName);
            return GetConfigValueAsString(key);
        }

        #endregion
    }
}