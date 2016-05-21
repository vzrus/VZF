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
 * File AkismetSpamClient.cs created  on 2.6.2015 in  6:29 AM.
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

  using YAF.Types;

  #endregion

  /// <summary>
  /// The akismet spam client.
  /// </summary>
  public class AkismetSpamClient : CheckForSpamClientBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AkismetSpamClient"/> class.
    /// </summary>
    /// <param name="apiKey">
    /// The api key.
    /// </param>
    /// <param name="blogUrl">
    /// The blog url.
    /// </param>
    /// <param name="httpClient">
    /// The http client.
    /// </param>
    public AkismetSpamClient([NotNull] string apiKey, [NotNull] Uri blogUrl, [NotNull] HttpClient httpClient)
      : base(apiKey, blogUrl, httpClient)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AkismetSpamClient"/> class.
    /// </summary>
    /// <param name="apiKey">
    /// The api key.
    /// </param>
    /// <param name="rootUrl">
    /// The root url.
    /// </param>
    public AkismetSpamClient([NotNull] string apiKey, [NotNull] Uri rootUrl)
      : base(apiKey, rootUrl)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets CheckUrlFormat.
    /// </summary>
    [NotNull]
    protected override string CheckUrlFormat
    {
      get
      {
        return "http://{0}.rest.akismet.com/1.1/comment-check";
      }
    }

    /// <summary>
    /// Gets SubmitHamUrlFormat.
    /// </summary>
    [NotNull]
    protected override string SubmitHamUrlFormat
    {
      get
      {
        return "http://{0}.rest.akismet.com/1.1/submit-ham";
      }
    }

    /// <summary>
    /// Gets SubmitSpamUrlFormat.
    /// </summary>
    [NotNull]
    protected override string SubmitSpamUrlFormat
    {
      get
      {
        return "http://{0}.rest.akismet.com/1.1/submit-spam";
      }
    }

    /// <summary>
    /// Gets SubmitVerifyKeyFormat.
    /// </summary>
    [NotNull]
    protected override string SubmitVerifyKeyFormat
    {
      get
      {
        return "http://rest.akismet.com/1.1/verify-key";
      }
    }

    #endregion
  }
}