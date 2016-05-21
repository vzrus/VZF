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
 * File YafSendMailThreaded.cs created  on 2.6.2015 in  6:29 AM.
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

    using VZF.Data.Common;
    
    using YAF.Types;
    using YAF.Types.Interfaces;
    using YAF.Types.Objects;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Separate class since SendThreaded isn't needed functionality for any instance except the 
    /// <see>
    /// <cref>HttpModule</cref>
    /// </see>
    ///     instance.
    /// </summary>
    public class YafSendMailThreaded : ISendMailThreaded
    {
        #region Constants and Fields

        /// <summary>
        ///   The _unique id.
        /// </summary>
        protected int _uniqueId;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YafSendMailThreaded"/> class.
        /// </summary>
        /// <param name="sendMail">
        /// The send mail. 
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public YafSendMailThreaded([NotNull] ISendMail sendMail, ILogger logger)
        {
            CodeContracts.ArgumentNotNull(sendMail, "sendMail");

            this.SendMail = sendMail;
            this.Logger = logger;
            var rand = new Random();
            this._uniqueId = rand.Next();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///   Gets or sets SendMail.
        /// </summary>
        public ISendMail SendMail { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The send threaded.
        /// </summary>
        public void SendThreaded()
        {
            var mailMessages = new Dictionary<MailMessage, TypedMailList>();

            try
            {
                IEnumerable<TypedMailList> mailList = this.GetMailListSafe();

                this.ConstructMessageList(mailMessages, mailList);

                this.SendMail.SendAllIsolated(
                    mailMessages.Select(x => x.Key),
                    (message, ex) =>
                    {
                        if (ex is FormatException)
                        {
#if (DEBUG)
                            // email address is no good -- delete this email...
                            this.Logger.Debug(ex, "Invalid Email Address: {0}".FormatWith(ex.Message));
#else                                // email address is no good -- delete this email...
                                this.Logger.Warn("Invalid Email Address: {0}".FormatWith(ex.ToString()));
#endif
                        }
                        else if (ex is SmtpFailedRecipientException)
                        {
                            var ex1 = (SmtpFailedRecipientException) ex;
                            SmtpStatusCode status = ex1.StatusCode;
                            if (status == SmtpStatusCode.MailboxBusy ||
                                status == SmtpStatusCode.MailboxUnavailable)
                            {
#if (DEBUG)
                                Logger.Debug("Delivery failed because mail box busy or unavalable.");
                                //  CommonDb.eventlog_create(0, 1, "Delivery failed because mail box busy or unavalable. :", ex1.ToString(), Types.Constants.EventLogTypes.Debug);
#endif
                            }
                            else
                            {
#if (DEBUG)
                                Logger.Debug(string.Format("Failed to deliver message to {0}\r\n{1}", ex1.Message,
                                    ex1.StackTrace));
                                CommonDb.eventlog_create(0, 1,
                                    string.Format("Failed to deliver message to {0}\r\n{1}", ex1.Message, ex1.StackTrace),
                                    Types.Constants.EventLogTypes.Debug);
#endif
                            }

                            if (mailMessages.ContainsKey(message) && mailMessages[message].SendTries < 2)
                            {
                                // remove from the collection so it doesn't get deleted...
                                mailMessages.Remove(message);
                            }
                        }
                        else if (ex is SmtpException)
                        {
#if (DEBUG)
                            this.Logger.Debug("SendMailThread SmtpException", ex.ToString());
                            CommonDb.eventlog_create(0, 1, "SmtpException ::", ex.ToString(),
                                Types.Constants.EventLogTypes.Debug);
#else
                                this.Logger.Warn("Send Exception: {0}".FormatWith(ex.ToString()));
#endif
                            if (mailMessages.ContainsKey(message) && mailMessages[message].SendTries < 2)
                            {
                                // remove from the collection so it doesn't get deleted...
                                mailMessages.Remove(message);
                            }
                        }
                        else
                        {
#if (DEBUG)
                            // general exception...
                            this.Logger.Debug("SendMailThread General Exception", ex.ToString());
                            // general exception...
                            this.Logger.Warn("Exception Thrown in SendMail Thread: {0}".FormatWith(ex.ToString()));
#endif
                        }
                    });

                foreach (var message in mailMessages.Values)
                {
                    // all is well, delete this message...
                    this.Logger.Debug("Deleting email to {0} (ID: {1})".FormatWith(message.ToUser, message.MailID));
                    CommonDb.mail_delete(YafContext.Current.PageModuleID, message.MailID);
                }
            }
            catch (Exception ex)
            {
#if (DEBUG)
                // general exception...
                this.Logger.Debug("SendMailThread General Exception", ex.ToString());
#else
                // general exception...
                this.Logger.Warn("Exception Thrown in SendMail Thread: {0}".FormatWith(ex));
#endif
            }
            finally
            {
                if (mailMessages.Any())
                {
                    // dispose of all mail messages
                    mailMessages.Where(x => x.Key != null).Select(m => m.Key).ToList().ForEach(m => m.Dispose());
                }
            }

            this.Logger.Debug("SendMailThread exiting");
        }

        #endregion

        #region Methods

        /// <summary>
        /// The construct message list.
        /// </summary>
        /// <param name="mailMessages">
        /// The mail messages.
        /// </param>
        /// <param name="mailList">
        /// The mail list.
        /// </param>
        private void ConstructMessageList(
            IDictionary<MailMessage, TypedMailList> mailMessages, IEnumerable<TypedMailList> mailList)
        {
            // construct mail message list...
            foreach (var mail in mailList.Where(x => x.MailID.HasValue))
            {
                // Build a MailMessage
                if (mail.FromUser.IsNotSet() || mail.ToUser.IsNotSet())
                {
                    CommonDb.mail_delete(YafContext.Current.PageModuleID, mail.MailID.Value);
                    continue;
                }

                try
                {
                    MailAddress toEmailAddress = mail.ToUserName.IsSet()
                                                     ? new MailAddress(mail.ToUser, mail.ToUserName)
                                                     : new MailAddress(mail.ToUser);
                    MailAddress fromEmailAddress = mail.FromUserName.IsSet()
                                                       ? new MailAddress(mail.FromUser, mail.FromUserName)
                                                       : new MailAddress(mail.FromUser);

                    var newMessage = new MailMessage();
                    mailMessages.Add(newMessage, mail);
                    newMessage.Populate(fromEmailAddress, toEmailAddress, mail.Subject, mail.Body, mail.BodyHtml);
                }
                catch (FormatException ex)
                {
                    // incorrect email format -- delete this message immediately
                    CommonDb.mail_delete(YafContext.Current.PageModuleID, mail.MailID);
#if (DEBUG)
                    this.Logger.Debug("Invalid Email Address: {0}".FormatWith(ex.ToString()), ex.ToString());
#else
                    this.Logger.Warn("Invalid Email Address: {0}".FormatWith(ex.ToString()));
#endif
                }
            }
        }

        /// <summary>
        /// Gets the mail list safe.
        /// </summary>
        /// <returns>
        /// The get mail list safe.
        /// </returns>
        private IEnumerable<TypedMailList> GetMailListSafe()
        {
            List<TypedMailList> mailList;

            try
            {
                this.Logger.Debug("Retrieving queued mail...");
                Thread.BeginCriticalRegion();

                mailList = CommonDb.MailList(YafContext.Current.PageModuleID, this._uniqueId).ToList();

                this.Logger.Debug("Got {0} Messages...".FormatWith(mailList.Count()));
            }
            finally
            {
                Thread.EndCriticalRegion();
            }

            return mailList;
        }

        #endregion
    }
}