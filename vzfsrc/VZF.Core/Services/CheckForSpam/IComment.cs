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
 * File IComment.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Collections.Specialized;
  using System.Net;

  #endregion

  /// <summary>
  /// Defines the base information about a comment submitted to the client.
  /// </summary>
  public interface IComment
  {
    #region Properties

    /// <summary>
    ///   The name submitted with the comment.
    /// </summary>
    string Author { get; }

    /// <summary>
    ///   The email submitted with the comment.
    /// </summary>
    string AuthorEmail { get; }

    /// <summary>
    ///   The url submitted if provided.
    /// </summary>
    Uri AuthorUrl { get; }

    /// <summary>
    ///   May be one of the following: {blank}, "comment", "trackback", "pingback", or a made-up value 
    ///   like "registration".
    /// </summary>
    string CommentType { get; }

    /// <summary>
    ///   Content of the comment
    /// </summary>
    string Content { get; }

    /// <summary>
    ///   IPAddress of the submitter
    /// </summary>
    IPAddress IPAddress { get; }

    /// <summary>
    ///   Permanent location of the entry the comment was 
    ///   submitted to.
    /// </summary>
    Uri Permalink { get; }

    /// <summary>
    ///   The HTTP_REFERER header value of the 
    ///   originating comment.
    /// </summary>
    string Referrer { get; }

    /// <summary>
    ///   Optional collection of various server environment variables.
    /// </summary>
    NameValueCollection ServerEnvironmentVariables { get; }

    /// <summary>
    ///   User agent of the requester. (Required)
    /// </summary>
    string UserAgent { get; }

    #endregion
  }
}