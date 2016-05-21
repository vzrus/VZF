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
 * File InvalidResponseException.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Runtime.Serialization;
  using System.Security;
  using System.Security.Permissions;

  using YAF.Types;

  #endregion

  /// <summary>
  /// Exception thrown when a response other than 200 is returned.
  /// </summary>
  /// <remarks>
  /// This exception does not have any custom properties, 
  ///   thus it does not implement ISerializable.
  /// </remarks>
  [Serializable]
  public sealed class InvalidResponseException : Exception
  {
    #region Constants and Fields

    /// <summary>
    /// The status.
    /// </summary>
    private readonly HttpStatusCode status = 0;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "InvalidResponseException" /> class.
    /// </summary>
    public InvalidResponseException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public InvalidResponseException([NotNull] string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="innerException">
    /// The inner exception.
    /// </param>
    public InvalidResponseException([NotNull] string message, [NotNull] Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    public InvalidResponseException([NotNull] string message, HttpStatusCode status)
      : base(message)
    {
      this.status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    private InvalidResponseException([NotNull] SerializationInfo info, StreamingContext context)
    {
      this.status = (HttpStatusCode)info.GetValue("Status", typeof(HttpStatusCode));
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the HTTP status returned by the service.
    /// </summary>
    /// <value>The HTTP status.</value>
    public HttpStatusCode HttpStatus
    {
      get
      {
        return this.status;
      }
    }

    #endregion
  }
}