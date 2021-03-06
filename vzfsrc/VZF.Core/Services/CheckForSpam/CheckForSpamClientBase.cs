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
 * File CheckForSpamClientBase.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Globalization;
  using System.Linq;
  using System.Net;
  using System.Web;

  using VZF.Utils;
  using VZF.Utils.Helpers.StringUtils;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The client class used to communicate with the spam service.
  /// </summary>
  [Serializable]
  public abstract class CheckForSpamClientBase : ICheckForSpamClient
  {
    #region Constants and Fields

    /// <summary>
    ///   The http client.
    /// </summary>
    [NonSerialized]
    protected readonly HttpClient httpClient;

    /// <summary>
    ///   The api key.
    /// </summary>
    protected string apiKey;

    /// <summary>
    ///   The proxy.
    /// </summary>
    protected IWebProxy proxy;

    /// <summary>
    ///   The root url.
    /// </summary>
    protected Uri rootUrl;

    /// <summary>
    ///   The check url.
    /// </summary>
    protected Uri submitCheckUrl;

    /// <summary>
    ///   The submit ham url.
    /// </summary>
    protected Uri submitHamUrl;

    /// <summary>
    ///   The submit spam url.
    /// </summary>
    protected Uri submitSpamUrl;

    /// <summary>
    ///   The timeout.
    /// </summary>
    protected int timeout = 5000;

    /// <summary>
    ///   The user agent.
    /// </summary>
    protected string userAgent;

    /// <summary>
    ///   The verify url.
    /// </summary>
    protected Uri verifyUrl;

    /// <summary>
    ///   The version.
    /// </summary>
    private static readonly string version = typeof(HttpClient).Assembly.GetName().Version.ToString();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckForSpamClientBase"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor takes in all the dependencies to allow for 
    ///   dependency injection and unit testing. Seems like overkill, 
    ///   but it's worth it.
    /// </remarks>
    /// <param name="apiKey">
    /// The Akismet API key.
    /// </param>
    /// <param name="blogUrl">
    /// The root url of the blog.
    /// </param>
    /// <param name="httpClient">
    /// Client class used to make the underlying requests.
    /// </param>
    public CheckForSpamClientBase([NotNull] string apiKey, [NotNull] Uri blogUrl, [NotNull] HttpClient httpClient)
    {
      CodeContracts.ArgumentNotNull(apiKey, "apiKey");
      CodeContracts.ArgumentNotNull(blogUrl, "blogUrl");
      CodeContracts.ArgumentNotNull(httpClient, "httpClient");

      this.apiKey = apiKey;
      this.rootUrl = blogUrl;
      this.httpClient = httpClient;

      this.SetServiceUrls();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckForSpamClientBase"/> class.
    /// </summary>
    /// <param name="apiKey">
    /// The Akismet API key.
    /// </param>
    /// <param name="rootUrl">
    /// The root url of the site using Akismet.
    /// </param>
    public CheckForSpamClientBase([NotNull] string apiKey, [NotNull] Uri rootUrl)
      : this(apiKey, rootUrl, new HttpClient())
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the Akismet API key.
    /// </summary>
    /// <value>The API key.</value>
    [NotNull]
    public string ApiKey
    {
      get
      {
        return this.apiKey ?? string.Empty;
      }

      set
      {
        this.apiKey = value ?? string.Empty;
        this.SetServiceUrls();
      }
    }

    /// <summary>
    ///   Gets or sets the proxy to use.
    /// </summary>
    /// <value>The proxy.</value>
    public IWebProxy Proxy
    {
      get
      {
        return this.proxy;
      }

      set
      {
        this.proxy = value;
      }
    }

    /// <summary>
    ///   Gets or sets the root URL to the blog.
    /// </summary>
    /// <value>The blog URL.</value>
    public Uri RootUrl
    {
      get
      {
        return this.rootUrl;
      }

      set
      {
        this.rootUrl = value;
      }
    }

    /// <summary>
    ///   Gets or sets the timeout in milliseconds for the http request to Akismet. 
    ///   By default 5000 (5 seconds).
    /// </summary>
    /// <value>The timeout.</value>
    public int Timeout
    {
      get
      {
        return this.timeout;
      }

      set
      {
        this.timeout = value;
      }
    }

    /// <summary>
    ///   Gets or sets the Usera Agent for the Akismet Client.  
    ///   Do not confuse this with the user agent for the comment 
    ///   being checked.
    /// </summary>
    /// <value>The API key.</value>
    [NotNull]
    public string UserAgent
    {
      get
      {
        return this.userAgent ?? BuildUserAgent("Subkismet");
      }

      set
      {
        this.userAgent = value;
      }
    }

    /// <summary>
    /// Gets CheckUrlFormat.
    /// </summary>
    protected abstract string CheckUrlFormat { get; }

    /// <summary>
    /// Gets SubmitHamUrlFormat.
    /// </summary>
    protected abstract string SubmitHamUrlFormat { get; }

    /// <summary>
    /// Gets SubmitSpamUrlFormat.
    /// </summary>
    protected abstract string SubmitSpamUrlFormat { get; }

    /// <summary>
    /// Gets SubmitVerifyKeyFormat.
    /// </summary>
    protected abstract string SubmitVerifyKeyFormat { get; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Helper method for building a user agent string in the format 
    ///   preferred by Akismet.
    /// </summary>
    /// <param name="applicationName">
    /// Name of the application.
    /// </param>
    /// <returns>
    /// The build user agent.
    /// </returns>
    [NotNull]
    public static string BuildUserAgent([NotNull] string applicationName)
    {
      CodeContracts.ArgumentNotNull(applicationName, "applicationName");

      return string.Format(CultureInfo.InvariantCulture, "{0}/{1} | Akismet/1.11", applicationName, version);
    }

    #endregion

    #region Implemented Interfaces

    #region ICheckForSpamClient

    /// <summary>
    /// Checks the comment and returns true if it is spam, otherwise false.
    /// </summary>
    /// <param name="comment">
    /// </param>
    /// <returns>
    /// The check comment for spam.
    /// </returns>
    /// <exception cref="InvalidResponseException">
    /// Akismet returned an empty response
    /// </exception>
    public bool CheckCommentForSpam(IComment comment)
    {
      CodeContracts.ArgumentNotNull(comment, "comment");

      string result = this.SubmitComment(comment, this.submitCheckUrl);

      if (result.IsNotSet())
      {
        throw new InvalidResponseException("Akismet returned an empty response");
      }

      if (result != "true" && result != "false")
      {
        throw new InvalidResponseException(
          string.Format(
            CultureInfo.InvariantCulture, "Received the response '{0}' from Akismet. Probably a bad API key.", result));
      }

      return bool.Parse(result);
    }

    /// <summary>
    /// Submits a comment to Akismet that should not have been 
    ///   flagged as SPAM (a false positive).
    /// </summary>
    /// <param name="comment">
    /// </param>
    public void SubmitHam(IComment comment)
    {
      this.SubmitComment(comment, this.submitHamUrl);
    }

    /// <summary>
    /// Submits a comment to Akismet that should have been 
    ///   flagged as SPAM, but was not flagged by Akismet.
    /// </summary>
    /// <param name="comment">
    /// </param>
    public void SubmitSpam(IComment comment)
    {
      this.SubmitComment(comment, this.submitSpamUrl);
    }

    /// <summary>
    /// Verifies the API key.  You really only need to
    ///   call this once, perhaps at startup.
    /// </summary>
    /// <returns>
    /// The verify api key.
    /// </returns>
    /// <exception type="Sytsem.Web.WebException">
    /// If it cannot make a request of Akismet.
    /// </exception>
    /// <exception cref="InvalidResponseException">
    /// Akismet returned an empty response
    /// </exception>
    public bool VerifyApiKey()
    {
      string parameters = "key=" + HttpUtility.UrlEncode(this.ApiKey) + "&blog=" +
                          HttpUtility.UrlEncode(this.RootUrl.ToString());
      string result = this.httpClient.PostRequest(this.verifyUrl, this.UserAgent, this.Timeout, parameters, this.proxy);

      if (result.IsNotSet())
      {
        throw new InvalidResponseException("Akismet returned an empty response");
      }

      return String.Equals("valid", result, StringComparison.InvariantCultureIgnoreCase);
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The set service urls.
    /// </summary>
    protected void SetServiceUrls()
    {
      this.submitHamUrl = new Uri(String.Format(CultureInfo.InvariantCulture, this.SubmitHamUrlFormat, this.apiKey));
      this.submitSpamUrl = new Uri(String.Format(CultureInfo.InvariantCulture, this.SubmitSpamUrlFormat, this.apiKey));
      this.submitCheckUrl = new Uri(String.Format(CultureInfo.InvariantCulture, this.CheckUrlFormat, this.apiKey));
      this.verifyUrl = new Uri(String.Format(CultureInfo.InvariantCulture, this.SubmitVerifyKeyFormat, this.apiKey));
    }

    /// <summary>
    /// The submit comment.
    /// </summary>
    /// <param name="comment">
    /// The comment.
    /// </param>
    /// <param name="url">
    /// The url.
    /// </param>
    /// <returns>
    /// The submit comment.
    /// </returns>
    private string SubmitComment([NotNull] IComment comment, [NotNull] Uri url)
    {
      // Not too many concatenations.  Might not need a string builder.
      CodeContracts.ArgumentNotNull(comment, "comment");
      CodeContracts.ArgumentNotNull(url, "url");

      string parameters = "blog=" + HttpUtility.UrlEncode(this.rootUrl.ToString()) + "&user_ip=" + comment.IPAddress +
                          "&user_agent=" + HttpUtility.UrlEncode(comment.UserAgent);

      if (comment.Referrer.IsSet())
      {
        parameters += "&referer=" + HttpUtility.UrlEncode(comment.Referrer);
      }

      if (comment.Permalink != null)
      {
        parameters += "&permalink=" + HttpUtility.UrlEncode(comment.Permalink.ToString());
      }

      if (comment.CommentType.IsSet())
      {
        parameters += "&comment_type=" + HttpUtility.UrlEncode(comment.CommentType);
      }

      if (comment.Author.IsSet())
      {
        parameters += "&comment_author=" + HttpUtility.UrlEncode(comment.Author);
      }

      if (comment.AuthorEmail.IsSet())
      {
        parameters += "&comment_author_email=" + HttpUtility.UrlEncode(comment.AuthorEmail);
      }

      if (comment.AuthorUrl != null)
      {
        parameters += "&comment_author_url=" + HttpUtility.UrlEncode(comment.AuthorUrl.ToString());
      }

      if (comment.Content.IsSet())
      {
        parameters += "&comment_content=" + HttpUtility.UrlEncode(comment.Content);
      }

      if (comment.ServerEnvironmentVariables != null)
      {
        parameters = comment.ServerEnvironmentVariables.Cast<string>().Aggregate(
          parameters, 
          (current, key) => current + ("&" + key + "=" + HttpUtility.UrlEncode(comment.ServerEnvironmentVariables[key])));
      }

      string response = this.httpClient.PostRequest(url, this.UserAgent, this.Timeout, parameters, this.proxy);
      if (response == null)
      {
        return string.Empty;
      }

      return response.ToLower(CultureInfo.InvariantCulture);
    }

    #endregion
  }
}