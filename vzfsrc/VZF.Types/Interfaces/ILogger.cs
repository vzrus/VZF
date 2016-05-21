
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
 * File ActiveLocation.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
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


namespace YAF.Types.Interfaces
{
  #region Using

  using System;

  #endregion

  /// <summary>
  /// The logger interface
  /// </summary>
  public interface ILogger
  {
    #region Properties

    /// <summary>
    /// Gets a value indicating whether IsDebugEnabled.
    /// </summary>
    bool IsDebugEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether IsErrorEnabled.
    /// </summary>
    bool IsErrorEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether IsFatalEnabled.
    /// </summary>
    bool IsFatalEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether IsInfoEnabled.
    /// </summary>
    bool IsInfoEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether IsTraceEnabled.
    /// </summary>
    bool IsTraceEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether IsWarnEnabled.
    /// </summary>
    bool IsWarnEnabled { get; }

    /// <summary>
    /// Gets a value indicating the logging type.
    /// </summary>
    Type Type { get; }

    #endregion

    #region Public Methods

    /// <summary>
    /// The debug.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Debug([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The debug.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Debug([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The error.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Error([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The error.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Error([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The fatal.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Fatal([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The fatal.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Fatal([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The info.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Info([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The info.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Info([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

   /// <summary>
   /// The info.
   /// </summary>
   /// <param name="userId">
   /// The userId.
   /// </param>
   /// <param name="format">
   /// The format.
   /// </param>
   /// <param name="args">
   /// The args.
   /// </param>
    void Info(int userId, string format, params object[] args);

    /// <summary>
    /// The trace.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Trace([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The trace.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Trace([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The warn.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Warn([NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The warn.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void Warn([NotNull] Exception exception, [NotNull] string format, [NotNull] params object[] args);

    /// <summary>
    /// The UserUnsuspended.
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void UserUnsuspended(int userId, string source, string format, params object[] args);

    /// <summary>
    /// The User Suspended.
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    void UserSuspended(int userId, string source, string format, params object[] args);

      /// <summary>
      /// The User Deleted.
      /// </summary>
      /// <param name="userId">
      /// The user Id.
      /// </param>
      /// <param name="source">
      /// The source.
      /// </param>
      /// <param name="format">
      /// The format.
      /// </param>
      /// <param name="args">
      /// The args.
      /// </param>
      void UserDeleted(int userId, string source, string format, params object[] args);

      /// <summary>
      /// The Ip Ban Set.
      /// </summary>
      /// <param name="userId">
      /// The user Id.
      /// </param>
      /// <param name="source">
      /// The source.
      /// </param>
      /// <param name="format">
      /// The format.
      /// </param>
      /// <param name="args">
      /// The args.
      /// </param>
      void IpBanSet(int userId, string source, string format, params object[] args);

      /// <summary>
      /// The Ip Ban Lifted.
      /// </summary>
      /// <param name="userId">
      /// The user Id.
      /// </param>
      /// <param name="source">
      /// The source.
      /// </param>
      /// <param name="format">
      /// The format.
      /// </param>
      /// <param name="args">
      /// The args.
      /// </param>
      void IpBanLifted(int userId, string source, string format, params object[] args);

      #endregion
  }
}