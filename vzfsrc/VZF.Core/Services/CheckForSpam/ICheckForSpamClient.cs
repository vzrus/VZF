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
 * File ICheckForSpamClient.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:05 PM.
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
  using System.Net;

  using YAF.Types;

  #endregion

  /// <summary>
  /// Interface that communicates with a spam client.
  /// </summary>
  public interface ICheckForSpamClient
  {
    #region Properties

    /// <summary>
    ///   Gets or sets the Akismet API key.
    /// </summary>
    /// <value>The API key.</value>
    string ApiKey { get; set; }

    /// <summary>
    ///   Gets or sets the proxy to use.
    /// </summary>
    /// <value>The proxy.</value>
    IWebProxy Proxy { get; set; }

    /// <summary>
    ///   Gets or sets the root URL to the blog.
    /// </summary>
    /// <value>The blog URL.</value>
    Uri RootUrl { get; set; }

    /// <summary>
    ///   Gets or sets the timeout in milliseconds for the http request to the client.
    /// </summary>
    /// <value>The timeout.</value>
    int Timeout { get; set; }

    /// <summary>
    ///   Gets or sets the User Agent for the Client.  
    ///   Do not confuse this with the user agent for the comment 
    ///   being checked.
    /// </summary>
    string UserAgent { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Checks the comment and returns true if it is spam, otherwise false.
    /// </summary>
    /// <param name="comment">
    /// </param>
    /// <returns>
    /// The check comment for spam.
    /// </returns>
    bool CheckCommentForSpam([NotNull] IComment comment);

    /// <summary>
    /// Submits a comment to the client that should not have been 
    ///   flagged as SPAM (a false positive).
    /// </summary>
    /// <param name="comment">
    /// </param>
    void SubmitHam([NotNull] IComment comment);

    /// <summary>
    /// Submits a comment to the client that should have been 
    ///   flagged as SPAM, but was not flagged.
    /// </summary>
    /// <param name="comment">
    /// </param>
    void SubmitSpam([NotNull] IComment comment);

    /// <summary>
    /// Verifies the API key.  You really only need to
    ///   call this once, perhaps at startup.
    /// </summary>
    /// <returns>
    /// The verify api key.
    /// </returns>
    bool VerifyApiKey();

    #endregion
  }
}