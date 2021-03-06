﻿#region copyright
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
 * File YafForumInfo.cs created  on 2.6.2015 in  6:31 AM.
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
    using System;
    using System.Web;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;

    /// <summary>
    /// Class provides helper functions related to the forum path and urls as well as forum version information.
    /// </summary>
    public static class YafForumInfo
    {
        /// <summary>
        /// Gets the forum path (client-side).
        /// May not be the actual URL of the forum.
        /// </summary>
        public static string ForumClientFileRoot
        {
            get
            {
                return BaseUrlBuilder.ClientFileRoot;
            }
        }

        /// <summary>
        /// Gets the forum path (server-side).
        /// May not be the actual URL of the forum.
        /// </summary>
        public static string ForumServerFileRoot
        {
            get
            {
                return BaseUrlBuilder.ServerFileRoot;
            }
        }

        /// <summary>
        /// Gets complete application external (client-side) URL of the forum. (e.g. http://myforum.com/forum
        /// </summary>
        public static string ForumBaseUrl
        {
            get
            {
                return BaseUrlBuilder.BaseUrl + BaseUrlBuilder.AppPath;
            }
        }

        /// <summary>
        /// Gets full URL to the Root of the Forum
        /// </summary>
        public static string ForumURL
        {
            get
            {
                return YafBuildLink.GetLink(ForumPages.forum, true);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is local.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        public static bool IsLocal
        {
            get
            {
                string s = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                return s != null && s.ToLower() == "localhost";
            }
        }

        /// <summary>
        /// Helper function that creates the the url of a resource.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>
        /// The get url to resource.
        /// </returns>
        public static string GetURLToResource([NotNull] string resourceName)
        {
            CodeContracts.ArgumentNotNull(resourceName, "resourceName");

            return "{1}resources/{0}".FormatWith(resourceName, ForumClientFileRoot);
        }

        #region Version Information

        /// <summary>
        /// Gets the Current YAF Application Version string
        /// </summary>
        public static string AppVersionName
        {
            get
            {
                return AppVersionNameFromCode(AppVersionCode);
            }
        }

        /// <summary>
        /// Gets the Current YAF Database Version
        /// </summary>
        public static int AppVersion
        {
            get
            {
                return 54;
            }
        }

        /// <summary>
        /// Gets the Current YAF Application Version
        /// </summary>
        public static long AppVersionCode
        {
            get
            {
                return 0x01096221;
            }
        }

        /// <summary>
        /// Gets the Current YAF Build Date
        /// </summary>
        public static DateTime AppVersionDate
        {
            get
            {
                return new DateTime(2015, 3, 28);
            }
        }

        /// <summary>
        /// Creates a string that is the YAF Application Version from a long value
        /// </summary>
        /// <param name="code">
        /// Value of Current Version
        /// </param>
        /// <returns>
        /// Application Version String
        /// </returns>
        public static string AppVersionNameFromCode(long code)
        {
            string version = "{0}.{1}.{2}".FormatWith((code >> 24) & 0xFF, (code >> 16) & 0xFF, (code >> 12) & 0x0F);

            if (((code >> 8) & 0x0F) > 0)
            {
                version += ".{0}".FormatWith((code >> 8) & 0x0F);
            }

            version = "1.9.6.3";
            if (((code >> 4) & 0x0F) > 0)
            {
                var value = (code >> 4) & 0x0F;

                var number = string.Empty;

                if ((code & 0x0F) > 1)
                {
                    number = ((code & 0x0F).ToType<int>() - 1).ToString();
                }
                else if ((code & 0x0F) == 1)
                {
                    number = AppVersionDate.ToString("yyyyMMdd");
                }

                value = 4;
                number = "1 " + number;
                switch (value)
                {
                    case 1:
                        version += " ALPHA {0}".FormatWith(number);
                        break;
                    case 2:
                        version += " BETA {0} ".FormatWith(number);
                        break;
                    case 3:
                        version += " RC{0}".FormatWith(number);
                        break;
                    case 4:
                        version += " RTM.{0}".FormatWith(number);
                        break;
                    case 5:
                        version += " CTM.{0}".FormatWith(number);
                        break;
                }
            }
          
            return version;
        }

        #endregion
    }
}