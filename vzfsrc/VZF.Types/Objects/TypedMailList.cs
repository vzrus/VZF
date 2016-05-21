
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


namespace YAF.Types.Objects
{
  #region Using

  using System;
  using System.Data;

  #endregion

  /// <summary>
  /// The typed mail list.
  /// </summary>
  [Serializable]
  public class TypedMailList
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TypedMailList"/> class.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public TypedMailList([NotNull] DataRow row)
    {
      this.MailID = row.Field<int?>("MailID");
      this.FromUser = row.Field<string>("FromUser");
      this.ToUser = row.Field<string>("ToUser");
      this.Created = row.Field<DateTime?>("Created");
      this.Subject = row.Field<string>("Subject");
      this.Body = row.Field<string>("Body");
      this.FromUserName = row.Field<string>("FromUserName");
      this.ToUserName = row.Field<string>("ToUserName");
      this.BodyHtml = row.Field<string>("BodyHtml");
      this.SendTries = row.Field<int?>("SendTries");
      this.SendAttempt = row.Field<DateTime?>("SendAttempt");
      this.ProcessID = row.Field<int?>("ProcessID");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypedMailList"/> class.
    /// </summary>
    /// <param name="mailid">
    /// The mailid.
    /// </param>
    /// <param name="fromuser">
    /// The fromuser.
    /// </param>
    /// <param name="touser">
    /// The touser.
    /// </param>
    /// <param name="created">
    /// The created.
    /// </param>
    /// <param name="subject">
    /// The subject.
    /// </param>
    /// <param name="body">
    /// The body.
    /// </param>
    /// <param name="fromusername">
    /// The fromusername.
    /// </param>
    /// <param name="tousername">
    /// The tousername.
    /// </param>
    /// <param name="bodyhtml">
    /// The bodyhtml.
    /// </param>
    /// <param name="sendtries">
    /// The sendtries.
    /// </param>
    /// <param name="sendattempt">
    /// The sendattempt.
    /// </param>
    /// <param name="processid">
    /// The processid.
    /// </param>
    public TypedMailList(
      int? mailid, [NotNull] string fromuser, [NotNull] string touser,
      DateTime? created, [NotNull] string subject, [NotNull] string body, [NotNull] string fromusername, [NotNull] string tousername, [NotNull] string bodyhtml,
      int? sendtries,
      DateTime? sendattempt,
      int? processid)
    {
      this.MailID = mailid;
      this.FromUser = fromuser;
      this.ToUser = touser;
      this.Created = created;
      this.Subject = subject;
      this.Body = body;
      this.FromUserName = fromusername;
      this.ToUserName = tousername;
      this.BodyHtml = bodyhtml;
      this.SendTries = sendtries;
      this.SendAttempt = sendattempt;
      this.ProcessID = processid;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets Body.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Gets or sets BodyHtml.
    /// </summary>
    public string BodyHtml { get; set; }

    /// <summary>
    /// Gets or sets Created.
    /// </summary>
    public DateTime? Created { get; set; }

    /// <summary>
    /// Gets or sets FromUser.
    /// </summary>
    public string FromUser { get; set; }

    /// <summary>
    /// Gets or sets FromUserName.
    /// </summary>
    public string FromUserName { get; set; }

    /// <summary>
    /// Gets or sets MailID.
    /// </summary>
    public int? MailID { get; set; }

    /// <summary>
    /// Gets or sets ProcessID.
    /// </summary>
    public int? ProcessID { get; set; }

    /// <summary>
    /// Gets or sets SendAttempt.
    /// </summary>
    public DateTime? SendAttempt { get; set; }

    /// <summary>
    /// Gets or sets SendTries.
    /// </summary>
    public int? SendTries { get; set; }

    /// <summary>
    /// Gets or sets Subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets ToUser.
    /// </summary>
    public string ToUser { get; set; }

    /// <summary>
    /// Gets or sets ToUserName.
    /// </summary>
    public string ToUserName { get; set; }

    #endregion
  }
}