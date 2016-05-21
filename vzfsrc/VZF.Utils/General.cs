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
 * File General.cs created  on 2.6.2015 in  6:31 AM.
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

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Security;
    using System.Web;

    using YAF.Classes;
    using YAF.Types;

  #endregion

    /// <summary>
    /// Summary description for General Utils.
    /// </summary>
    public static class General
    {
        /* Ederon : 9/12/2007 */

        #region Public Methods

        /// <summary>
        /// Compares two messages.
        /// </summary>
        /// <param name="originalMessage">
        /// Original message text.
        /// </param>
        /// <param name="newMessage">
        /// New message text.
        /// </param>
        /// <returns>
        /// True if messages differ, <see langword="false"/> if they are identical.
        /// </returns>
        public static bool CompareMessage([NotNull] object originalMessage, [NotNull] object newMessage)
        {
            CodeContracts.ArgumentNotNull(originalMessage, "originalMessage");
            CodeContracts.ArgumentNotNull(newMessage, "newMessage");

            return originalMessage.ToString() != newMessage.ToString();
        }

        /// <summary>
        /// The encode message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The encoded message.
        /// </returns>
        public static string EncodeMessage([NotNull] string message)
        {
            CodeContracts.ArgumentNotNull(message, "message");

            return message.IndexOf('<') >= 0 ? HttpUtility.HtmlEncode(message) : message;
        }

        /// <summary>
        /// Gets the current ASP.NET Hosting Security Level.
        /// </summary>
        /// <returns>
        /// The get current trust level.
        /// </returns>
        public static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            // Gets an override value, useful for Custom Trust Levels
            if (!string.IsNullOrEmpty(Config.OverrideTrustLevel)) 
            {
                return
                    (AspNetHostingPermissionLevel)
                    Enum.Parse(typeof(AspNetHostingPermissionLevel), Config.OverrideTrustLevel, true); // return non custom trust level
            }

            foreach (AspNetHostingPermissionLevel trustLevel in
                new[]
                    {
                        AspNetHostingPermissionLevel.Unrestricted, AspNetHostingPermissionLevel.High,
                        AspNetHostingPermissionLevel.Medium, AspNetHostingPermissionLevel.Low,
                        AspNetHostingPermissionLevel.Minimal
                    })
            {
                try
                {
                    new AspNetHostingPermission(trustLevel).Demand();
                }
                catch (SecurityException)
                {
                    continue;
                }

                return trustLevel;
            }

            return AspNetHostingPermissionLevel.None;
        }

        /// <summary>
        /// The get safe raw url.
        /// </summary>
        /// <returns>
        /// The get safe raw url.
        /// </returns>
        public static string GetSafeRawUrl()
        {
            return GetSafeRawUrl(HttpContext.Current.Request.RawUrl);
        }

        /// <summary>
        /// Cleans up a URL so that it doesn't contain any problem characters.
        /// </summary>
        /// <param name="url">
        /// </param>
        /// <returns>
        /// The get safe raw url.
        /// </returns>
        [NotNull]
        public static string GetSafeRawUrl([NotNull] string url)
        {
            CodeContracts.ArgumentNotNull(url, "url");

            string tProcessedRaw = url;
            tProcessedRaw = tProcessedRaw.Replace("\"", string.Empty);
            tProcessedRaw = tProcessedRaw.Replace("<", "%3C");
            tProcessedRaw = tProcessedRaw.Replace(">", "%3E");
            tProcessedRaw = tProcessedRaw.Replace("&", "%26");
            return tProcessedRaw.Replace("'", string.Empty);
        }

        /// <summary>
        /// The trace resources.
        /// </summary>
        /// <returns>
        /// The trace resources result string.
        /// </returns>
        public static string TraceResources()
        {
            Assembly a = Assembly.GetExecutingAssembly();

            // get a list of resource names from the manifest
            string[] resNames = a.GetManifestResourceNames();

            // populate the textbox with information about our resources
            // also look for images and put them in our arraylist
            string txtInfo = string.Empty;

            txtInfo += "Found {0} resources\r\n".FormatWith(resNames.Length);
            txtInfo += "----------\r\n";

            txtInfo = resNames.Aggregate(txtInfo, (current, s) => current + (s + "\r\n"));

            txtInfo += "----------\r\n";

            return txtInfo;
        }

        #endregion
    }
}