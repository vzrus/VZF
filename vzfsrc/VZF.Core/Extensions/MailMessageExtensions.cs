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
 * File MailMessageExtensions.cs created  on 2.6.2015 in  6:29 AM.
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

using System.Net;

namespace YAF.Core
{
    #region Using

    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;

    using YAF.Classes;
    using YAF.Types;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The mail message extensions.
    /// </summary>
    public static class MailMessageExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Does the string contain any unicode characters?
        /// From http://stackoverflow.com/questions/4459571/how-to-recognize-if-a-string-contains-unicode-chars
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The contains unicode character.
        /// </returns>
        public static bool ContainsUnicodeCharacter([NotNull] string input)
        {
            CodeContracts.ArgumentNotNull(input, "input");

            const int MaxAnsiCode = 255;

            return input.ToCharArray().Any(c => c > MaxAnsiCode);
        }

        /// <summary>
        /// The populate.
        /// </summary>
        /// <param name="mailMessage">
        /// The mail message. 
        /// </param>
        /// <param name="fromAddress">
        /// The from address. 
        /// </param>
        /// <param name="toAddress">
        /// The to address. 
        /// </param>
        /// <param name="subject">
        /// The subject. 
        /// </param>
        /// <param name="bodyText">
        /// The body text. 
        /// </param>
        /// <param name="bodyHtml">
        /// The body html. 
        /// </param>
        [NotNull]
        public static void Populate(
            [NotNull] this MailMessage mailMessage, 
            [NotNull] MailAddress fromAddress, 
            [NotNull] MailAddress toAddress, 
            [CanBeNull] string subject, 
            [CanBeNull] string bodyText, 
            [CanBeNull] string bodyHtml)
        {
            CodeContracts.ArgumentNotNull(mailMessage, "mailMessage");
            CodeContracts.ArgumentNotNull(fromAddress, "fromAddress");
            CodeContracts.ArgumentNotNull(toAddress, "toAddress");

            mailMessage.To.Add(toAddress);
            mailMessage.From = fromAddress;
            mailMessage.Subject = subject;

            // add default text view
            mailMessage.AlternateViews.Add(
                AlternateView.CreateAlternateViewFromString(
                    bodyText, ContainsUnicodeCharacter(bodyText) ? Encoding.Unicode : Encoding.UTF8, MediaTypeNames.Text.Plain));

            // see if html alternative is also desired...
            if (bodyHtml.IsSet())
            {
                mailMessage.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(
                        bodyHtml, ContainsUnicodeCharacter(bodyHtml) ? Encoding.Unicode : Encoding.UTF8, MediaTypeNames.Text.Html));
            }
        }

        /// <summary>
        /// Creates a SmtpClient and sends a MailMessage.
        /// </summary>
        /// <param name="message">
        /// The message. 
        /// </param>
        public static void Send([NotNull] this MailMessage message)
        {
            CodeContracts.ArgumentNotNull(message, "message");

            var smtpSend = new SmtpClient { EnableSsl = Config.UseSMTPSSL};

            // Tommy: solve random failure problem. Don't set this value to 1.
            // See this: http://stackoverflow.com...tem-net-mail-has-issues 
            smtpSend.ServicePoint.MaxIdleTime = 10;
            smtpSend.ServicePoint.ConnectionLimit = 1;

            // send the message...
            smtpSend.Send(message);
        }

        #endregion
    }
}