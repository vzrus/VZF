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
 * File YafTemplateEmail.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Collections.Specialized;
    using System.Net.Mail;

    using VZF.Data.Common;

    using YAF.Core;

    using VZF.Utils;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The yaf template email.
    /// </summary>
    public class YafTemplateEmail
    {
        #region Properties

        /// <summary>
        /// The _html enabled.
        /// </summary>
        private bool _htmlEnabled;

        /// <summary>
        /// The _template language file.
        /// </summary>
        private string _templateLanguageFile;

        /// <summary>
        /// The _template name.
        /// </summary>
        private string _templateName;

        /// <summary>
        /// The _template params.
        /// </summary>
        private StringDictionary _templateParams = new StringDictionary();

        /// <summary>
        /// Gets or sets TemplateName.
        /// </summary>
        public string TemplateName
        {
            get { return this._templateName; }

            set { this._templateName = value; }
        }

        /// <summary>
        /// Gets or sets TemplateLanguageFile.
        /// </summary>
        public string TemplateLanguageFile
        {
            get { return this._templateLanguageFile; }

            set { this._templateLanguageFile = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HtmlEnabled.
        /// </summary>
        public bool HtmlEnabled
        {
            get { return this._htmlEnabled; }

            set { this._htmlEnabled = value; }
        }

        /// <summary>
        /// Gets or sets TemplateParams.
        /// </summary>
        public StringDictionary TemplateParams
        {
            get { return this._templateParams; }

            set { this._templateParams = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="YafTemplateEmail"/> class.
        /// </summary>
        public YafTemplateEmail()
            : this(null, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YafTemplateEmail"/> class.
        /// </summary>
        /// <param name="templateName">
        /// The template name.
        /// </param>
        public YafTemplateEmail(string templateName)
            : this(templateName, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YafTemplateEmail"/> class.
        /// </summary>
        /// <param name="templateName">
        /// The template name.
        /// </param>
        /// <param name="htmlEnabled">
        /// The html enabled.
        /// </param>
        public YafTemplateEmail(string templateName, bool htmlEnabled)
        {
            this._templateName = templateName;
            this._htmlEnabled = htmlEnabled;
        }

        /// <summary>
        /// Reads a template from the language file
        /// </summary>
        /// <param name="templateName">
        /// The template Name.
        /// </param>
        /// <param name="templateLanguageFile">
        /// The template Language File.
        /// </param>
        /// <returns>
        /// The template
        /// </returns>
        private string ReadTemplate(string templateName, string templateLanguageFile)
        {
            if (templateName.IsSet())
            {
                if (!templateLanguageFile.IsSet())
                    return YafContext.Current.Get<ILocalization>().GetText("TEMPLATES", templateName);
               
                var localization = new YafLocalization();
                localization.LoadTranslation(templateLanguageFile);
                return localization.GetText("TEMPLATES", templateName);
            }

            return null;
        }

        /// <summary>
        /// Creates an email from a template
        /// </summary>
        /// <param name="templateName">
        /// The template Name.
        /// </param>
        /// <returns>
        /// The process template.
        /// </returns>
        public string ProcessTemplate(string templateName)
        {
            string email = ReadTemplate(templateName, TemplateLanguageFile);

            if (email.IsSet())
            {
                foreach (string key in this._templateParams.Keys)
                {
                    email = email.Replace(key, this._templateParams[key]);
                }
            }

            return email;
        }

        /// <summary>
        /// The send email.
        /// </summary>
        /// <param name="toAddress">
        /// The to address.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="useSendThread">
        /// The use send thread.
        /// </param>
        public void SendEmail(MailAddress toAddress, string subject, bool useSendThread)
        {
            SendEmail(
                new MailAddress(
                    YafContext.Current.BoardSettings.ForumEmail, 
                    YafContext.Current.BoardSettings.Name),
                    toAddress, 
                    subject, 
                    useSendThread);
        }

        /// <summary>
        /// The send email.
        /// </summary>
        /// <param name="fromAddress">
        /// The from address.
        /// </param>
        /// <param name="toAddress">
        /// The to address.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="useSendThread">
        /// The use send thread.
        /// </param>
        public void SendEmail(MailAddress fromAddress, MailAddress toAddress, string subject, bool useSendThread)
        {
            string textBody = ProcessTemplate(TemplateName + "_TEXT").Trim();
            string htmlBody = ProcessTemplate(TemplateName + "_HTML").Trim();

            // null out html if it's not desired
            if (!HtmlEnabled || htmlBody.IsNotSet())
            {
                htmlBody = null;
            }

            if (useSendThread)
            {
                // create this email in the send mail table...
                CommonDb.mail_create(
                    YafContext.Current.PageModuleID, 
                    fromAddress.Address, 
                    fromAddress.DisplayName,
                    toAddress.Address, 
                    toAddress.DisplayName, 
                    subject, 
                    textBody, 
                    htmlBody);
            }
            else
            {
                // just send directly
                YafContext.Current.Get<YafSendMail>().Send(fromAddress, toAddress, subject, textBody, htmlBody);
            }
        }

        /// <summary>
        /// The create watch.
        /// </summary>
        /// <param name="topicId">
        /// The topic id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="fromAddress">
        /// The from address.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        public void CreateWatch(int topicId, int userId, MailAddress fromAddress, string subject)
        {
            string textBody = ProcessTemplate(TemplateName + "_TEXT").Trim();
            string htmlBody = ProcessTemplate(TemplateName + "_HTML").Trim();

            // null out html if it's not desired
            if (!HtmlEnabled || htmlBody.IsNotSet())
            {
                htmlBody = null;
            }

            CommonDb.mail_createwatch(
                YafContext.Current.PageModuleID,
                topicId, fromAddress.Address,
                fromAddress.DisplayName,
                subject,
                textBody,
                htmlBody,
                userId);
        }
    }
}