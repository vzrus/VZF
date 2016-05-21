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
 * File BotScout.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Services.CheckForSpam
{
    #region

    using System;
    using System.IO;
    using System.Net;

    using YAF.Classes;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Spam Checking Class for the BotScout.com API
    /// </summary>
    public class BotScout : IBotCheck
    {
        /// <summary>
        /// Checks if user is a Bot.
        /// </summary>
        /// <param name="ipAddress">
        /// The ip address.
        /// </param>
        /// <param name="emailAddress">
        /// The email Address.
        /// </param>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <returns>
        /// Returns if user is a possible Bot or not
        /// </returns>
        public bool CheckForBot(object ipAddress, object emailAddress, object userName)
        {
            try
            {
                var apiKey = Config.BotScoutApiKey.IsSet() ? "&key={0}".FormatWith(Config.BotScoutApiKey) : string.Empty;

                var url = "http://botscout.com/test/?multi&ip={0}&mail={1}&name={2}{3}".FormatWith(
                    ipAddress, emailAddress, userName, apiKey);

                var req = (HttpWebRequest)WebRequest.Create(url);

                var res = (HttpWebResponse)req.GetResponse();
                var sr = new StreamReader(res.GetResponseStream());

                var value = sr.ReadToEnd();

                return value.StartsWith("Y|");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}