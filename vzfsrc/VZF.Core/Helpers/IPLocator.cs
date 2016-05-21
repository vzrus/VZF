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
 * File IPLocator.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.Eventing.Reader;
    using System.Linq;
    using System.Net;
    using System.Xml.Serialization;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

  /// <summary>
  /// Summary description for IPLocater
  /// </summary>
    public class IPDetails
    {
        #region Public Methods

        /// <summary>
        /// IP Details From IP Address
        /// </summary>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="os">
        /// The os.
        /// </param>
        /// <returns>
        /// IPLocator Class
        /// </returns>
        public IDictionary<string, string> GetData([CanBeNull] string ip, [CanBeNull] string format, bool callback, string culture, string browser, string os)
        {
            CodeContracts.ArgumentNotNull(ip, "ip");

            IDictionary<string, string> res = new ConcurrentDictionary<string, string>();

            if (YafContext.Current.Get<YafBoardSettings>().IPLocatorResultsMap.IsNotSet() ||
                YafContext.Current.Get<YafBoardSettings>().IPLocatorUrlPath.IsNotSet())
            {
                return res;
            }

            if (YafContext.Current.Get<YafBoardSettings>().EnableIPInfoService)
            {
                switch (format)
                {
                    case "raw":
                        try
                        {
                            string path =
                                YafContext.Current.Get<YafBoardSettings>()
                                    .IPLocatorUrlPath.FormatWith(VZF.Utils.Helpers.IPHelper.GetIp4Address(ip));
                            var client = new WebClient();
                            string[] result = client.DownloadString(path).Split(';');
                            string[] sray =
                                YafContext.Current.Get<YafBoardSettings>().IPLocatorResultsMap.Trim().Split(',');
                            if (result.Length > 0 && result.Length == sray.Length)
                            {
                                int i = 0;
                                bool oldproperty = false;
                                foreach (string str in result)
                                {
                                    if (sray.Any(strDef => string.Equals(str, strDef, StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        res.Add(sray[i].Trim(), str);
                                        i++;
                                        oldproperty = true;
                                    }

                                    if (!oldproperty)
                                    {
                                        CommonDb.eventlog_create(YafContext.Current.PageModuleID, this, "Undefined property {0} in your HostSettings IP Info Geolocation Web Service arguments mapping string. Automatically added to the string.".FormatWith(str), EventLogTypes.Information);
                                        string oldProperties =
                                            YafContext.Current.Get<YafBoardSettings>().IPLocatorResultsMap;
                                        YafContext.Current.Get<YafBoardSettings>().IPLocatorResultsMap = oldProperties
                                                                                                             + ","
                                                                                                             + str;
                                    }

                                    oldproperty = false;
                                }
                            }
                        }
                        catch
                        {
                            return res;
                        }

                        break;
                    case "xml":
                        break;
                    case "json":
                        break;
                }
            }

            return res;
        }

        #endregion
    }

  /// <summary>
  /// The ip locator.
  /// </summary>
  [XmlRootAttribute(ElementName = "Response", IsNullable = false)]
  public class IpLocator
  {
      #region Constants and Fields

      /// <summary>
      /// The gmtoffset.
      /// </summary>
      private string gmtoffset;

      /// <summary>
      /// The isdst.
      /// </summary>
      private string isdst;

      /// <summary>
      /// The regioncode.
      /// </summary>
      private string regioncode;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets Status.
      /// </summary>
      public string Status { get; set; }

      /// <summary>
      /// Gets or sets Status.
      /// </summary>
      public string StatusMessage { get; set; }

      /// <summary>
      /// Gets or sets IP.
      /// </summary>
      public string IP { get; set; }

      /// <summary>
      /// Gets or sets CountryCode.
      /// </summary>
      public string CountryCode { get; set; }

      /// <summary>
      /// Gets or sets CountryName.
      /// </summary>
      public string CountryName { get; set; }

      /// <summary>
      /// Gets or sets RegionName.
      /// </summary>
      public string RegionName { get; set; }

      /// <summary>
      /// Gets or sets City.
      /// </summary>
      public string City { get; set; }

      /// <summary>
      /// Gets or sets Zip.
      /// </summary>
      public string Zip { get; set; }

      /// <summary>
      /// Gets or sets Latitude.
      /// </summary>
      public string Latitude { get; set; }

      /// <summary>
      /// Gets or sets Longitude.
      /// </summary>
      public string Longitude { get; set; }

      /// <summary>
      /// Gets or sets TimezoneName.
      /// </summary>
      public string TimezoneName { get; set; }

      #endregion
  }
}