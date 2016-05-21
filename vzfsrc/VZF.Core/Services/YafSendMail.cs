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
 * File YafSendMail.cs created  on 2.6.2015 in  6:29 AM.
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
  using System.Collections.Generic;
  using System.Linq;
  using System.Net.Mail;
  using System.Threading;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Functions to send email via SMTP
  /// </summary>
  public class YafSendMail : ISendMail
  {
    #region Public Methods

    /// <summary>
    /// Sends all MailMessages via the SmtpClient. Doesn't handle any exceptions.
    /// </summary>
    /// <param name="messages">
    /// The messages.
    /// </param>
    public void SendAll([NotNull] IEnumerable<MailMessage> messages)
    {
      CodeContracts.ArgumentNotNull(messages, "messages");

      messages.ToList().ForEach(m => m.Send());
    }

    /// <summary>
    /// The send all isolated.
    /// </summary>
    /// <param name="messages">
    /// The messages.
    /// </param>
    /// <param name="handleExceptionAction">
    /// The handle exception action.
    /// </param>
    public void SendAllIsolated([NotNull] IEnumerable<MailMessage> messages, [CanBeNull] Action<MailMessage, Exception> handleExceptionAction)
    {
      CodeContracts.ArgumentNotNull(messages, "messages");

      foreach (var message in messages.ToList())
      {
        try
        {
          // send the message...
          message.Send();

          // sleep for a 1/20 of a second...
          Thread.Sleep(50);
        }
        catch (Exception ex)
        {
          if (handleExceptionAction != null)
          {
            handleExceptionAction(message, ex);
          }
        }
      }
    }

    #endregion
  }
}