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
 * File Comment.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Collections.Specialized;
  using System.Net;

  using YAF.Types;

  #endregion

  /// <summary>
  /// The comment.
  /// </summary>
  public class Comment : IComment
  {
    #region Constants and Fields

    /// <summary>
    /// The ip address.
    /// </summary>
    private readonly IPAddress ipAddress;

    /// <summary>
    /// The server environment variables.
    /// </summary>
    private readonly NameValueCollection serverEnvironmentVariables = new NameValueCollection();

    /// <summary>
    /// The user agent.
    /// </summary>
    private readonly string userAgent;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Comment"/> class.
    /// </summary>
    /// <param name="authorIpAddress">
    /// The author ip address.
    /// </param>
    /// <param name="authorUserAgent">
    /// The author user agent.
    /// </param>
    public Comment([NotNull] IPAddress authorIpAddress, [NotNull] string authorUserAgent)
    {
      this.ipAddress = authorIpAddress;
      this.userAgent = authorUserAgent;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   The name submitted with the comment.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///   The email submitted with the comment.
    /// </summary>
    public string AuthorEmail { get; set; }

    /// <summary>
    ///   The url submitted if provided.
    /// </summary>
    public Uri AuthorUrl { get; set; }

    /// <summary>
    ///   May be one of the following: {blank}, "comment", "trackback", "pingback", or a made-up value 
    ///   like "registration".
    /// </summary>
    public string CommentType { get; set; }

    /// <summary>
    ///   Content of the comment
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///   IPAddress of the submitter
    /// </summary>
    public IPAddress IPAddress
    {
      get
      {
        return this.ipAddress;
      }
    }

    /// <summary>
    ///   Permanent location of the entry the comment was 
    ///   submitted to.
    /// </summary>
    public Uri Permalink { get; set; }

    /// <summary>
    ///   The HTTP_REFERER header value of the 
    ///   originating comment.
    /// </summary>
    public string Referrer { get; set; }

    /// <summary>
    ///   Optional collection of various server environment variables.
    /// </summary>
    public NameValueCollection ServerEnvironmentVariables
    {
      get
      {
        return this.serverEnvironmentVariables;
      }
    }

    /// <summary>
    ///   User agent of the requester. (Required)
    /// </summary>
    public string UserAgent
    {
      get
      {
        return this.userAgent;
      }
    }

    #endregion
  }
}