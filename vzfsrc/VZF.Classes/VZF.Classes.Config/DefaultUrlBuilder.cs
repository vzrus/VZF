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
 * File defaulturlbuilder.cs created  on 2.6.2015 in  6:29 AM.
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

  #endregion

  /// <summary>
  /// Implements URL Builder.
  /// </summary>
  public class DefaultUrlBuilder : BaseUrlBuilder
  {
    #region Public Methods

    /// <summary>
    /// Builds path for calling page with parameter URL as page's escaped parameter.
    /// </summary>
    /// <param name="url">
    /// URL to put into parameter.
    /// </param>
    /// <returns>
    /// URL to calling page with URL argument as page's parameter with escaped characters to make it valid parameter.
    /// </returns>
    public override string BuildUrl(string url)
    {
      // escape & to &amp;
      url = url.Replace("&", "&amp;");

      // return URL to current script with URL from parameter as script's parameter
      return String.Format("{0}{1}?{2}", AppPath, Config.ForceScriptName ?? ScriptName, url);
    }

    /// <summary>
    /// Builds Full URL for calling page with parameter URL as page's escaped parameter.
    /// </summary>
    /// <param name="url">
    /// URL to put into parameter.
    /// </param>
    /// <returns>
    /// URL to calling page with URL argument as page's parameter with escaped characters to make it valid parameter.
    /// </returns>
    public override string BuildUrlFull(string url)
    {
      // append the full base server url to the beginning of the url (e.g. http://mydomain.com)
      return String.Format("{0}{1}", BaseUrl, this.BuildUrl(url));
    }

    #endregion
  }
}