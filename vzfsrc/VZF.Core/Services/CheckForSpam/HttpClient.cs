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
 * File HttpClient.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Services
{
  #region Using

  using System;
  using System.Diagnostics;
  using System.Globalization;
  using System.IO;
  using System.Net;
  using System.Text;

  using VZF.Utils;
  using VZF.Utils.Helpers.StringUtils;
  using YAF.Types;

  #endregion

  /// <summary>
  /// Class used to make the actual HTTP requests.
  /// </summary>
  public class HttpClient
  {
    #region Public Methods

    /// <summary>
    /// Gets the content of a web page.
    /// </summary>
    /// <param name="url">
    /// The URL.
    /// </param>
    /// <param name="userAgent">
    /// The user agent.
    /// </param>
    /// <param name="timeout">
    /// The timeout in miliseconds.
    /// </param>
    /// <param name="proxy">
    /// The proxy.
    /// </param>
    /// <returns>
    /// The string value of requested web page.
    /// </returns>
    /// <exception cref="InvalidResponseException"><c>InvalidResponseException</c>.</exception>
    [NotNull]
    public virtual string GetRequest([NotNull] Uri url, [NotNull] string userAgent, int timeout, [NotNull] IWebProxy proxy)
    {
      CodeContracts.ArgumentNotNull(url, "url");
      CodeContracts.ArgumentNotNull(userAgent, "userAgent");
      CodeContracts.ArgumentNotNull(proxy, "proxy");

      ServicePointManager.Expect100Continue = false;
      var request = WebRequest.Create(url) as HttpWebRequest;

      Debug.Assert(
        request != null, 
        "HttpWebRequest should not be null", 
        "Calling WebRequest.Create(url) produced a null HttpWebRequest instance for the URL '{0}'".FormatWith(url));

      if (proxy != null)
      {
        request.Proxy = proxy;
      }

      request.UserAgent = userAgent;
      request.Timeout = timeout;
      request.Method = "GET";
      request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
      request.KeepAlive = true;

      var response = (HttpWebResponse)request.GetResponse();
      if (response.StatusCode < HttpStatusCode.OK && response.StatusCode >= HttpStatusCode.Ambiguous)
      {
        throw new InvalidResponseException(
          string.Format(
            CultureInfo.InvariantCulture, 
            "The service was not able to handle our request. Http Status '{0}'.", 
            response.StatusCode), 
          response.StatusCode);
      }

      string responseText;
      using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
      {
        // They only return "true" or "false"
        responseText = reader.ReadToEnd();
      }

      return responseText;
    }

    /// <summary>
    /// Posts the request and returns a text response.  
    ///   This is all that is needed for Akismet.
    /// </summary>
    /// <param name="url">
    /// The URL.
    /// </param>
    /// <param name="userAgent">
    /// The user agent.
    /// </param>
    /// <param name="timeout">
    /// The timeout.
    /// </param>
    /// <param name="formParameters">
    /// The properly formatted parameters.
    /// </param>
    /// <returns>
    /// The post request.
    /// </returns>
    public virtual string PostRequest([NotNull] Uri url, [NotNull] string userAgent, int timeout, [NotNull] string formParameters)
    {
      return this.PostRequest(url, userAgent, timeout, formParameters, null);
    }

    /// <summary>
    /// Posts the request.
    /// </summary>
    /// <param name="url">
    /// The URL.
    /// </param>
    /// <param name="userAgent">
    /// The user agent.
    /// </param>
    /// <param name="timeout">
    /// The timeout in milliseconds.
    /// </param>
    /// <param name="formParameters">
    /// The form parameters.
    /// </param>
    /// <param name="proxy">
    /// The proxy.
    /// </param>
    /// <returns>
    /// The post request.
    /// </returns>
    /// <exception cref="InvalidResponseException"><c>InvalidResponseException</c>.</exception>
    [NotNull]
    public virtual string PostRequest([NotNull] Uri url, [NotNull] string userAgent, int timeout, [NotNull] string formParameters, [CanBeNull] IWebProxy proxy)
    {
      CodeContracts.ArgumentNotNull(url, "url");
      CodeContracts.ArgumentNotNull(userAgent, "userAgent");
      CodeContracts.ArgumentNotNull(formParameters, "formParameters");

      ServicePointManager.Expect100Continue = false;
      var request = WebRequest.Create(url) as HttpWebRequest;

      Debug.Assert(
        request != null, 
        "HttpWebRequest should not be null", 
        "Calling WebRequest.Create(url) produced a null HttpWebRequest instance for the URL '{0}'".FormatWith(url));

      if (proxy != null)
      {
        request.Proxy = proxy;
      }

      request.UserAgent = userAgent;
      request.Timeout = timeout;
      request.Method = "POST";
      request.ContentLength = formParameters.Length;
      request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
      request.KeepAlive = true;

      using (var myWriter = new StreamWriter(request.GetRequestStream()))
      {
        myWriter.Write(formParameters);
      }

      var response = (HttpWebResponse)request.GetResponse();
      if (response.StatusCode < HttpStatusCode.OK && response.StatusCode >= HttpStatusCode.Ambiguous)
      {
        throw new InvalidResponseException(
          string.Format(
            CultureInfo.InvariantCulture, 
            "The service was not able to handle our request. Http Status '{0}'.", 
            response.StatusCode), 
          response.StatusCode);
      }

      string responseText;
      using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
      {
        // They only return "true" or "false"
        responseText = reader.ReadToEnd();
      }

      return responseText;
    }

    #endregion
  }
}