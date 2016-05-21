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
 * File ForumUsers.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Controls
{
  #region Using

  using System;
  using System.Web.UI;

  using VZF.Data.Common;

  using YAF.Core; 
  
  using VZF.Utils;
  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Summary description for ForumUsers.
  /// </summary>
  public class ForumUsers : BaseControl
  {
    #region Constants and Fields

    /// <summary>
    ///   The _active users.
    /// </summary>
    private readonly ActiveUsers _activeUsers = new ActiveUsers();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "ForumUsers" /> class.
    /// </summary>
    public ForumUsers()
    {
      this._activeUsers.ID = this.GetUniqueID("ActiveUsers");
      this.Load += this.ForumUsers_Load;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets a value indicating whether TreatGuestAsHidden.
    /// </summary>
    public bool TreatGuestAsHidden
    {
      get
      {
        return this._activeUsers.TreatGuestAsHidden;
      }

      set
      {
        this._activeUsers.TreatGuestAsHidden = value;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render([NotNull] HtmlTextWriter writer)
    {
      // Ederon : 07/14/2007
      if (!this.PageContext.BoardSettings.ShowBrowsingUsers)
      {
        return;
      }

      bool bTopic = this.PageContext.PageTopicID > 0;

      if (bTopic)
      {
        writer.WriteLine(@"<tr id=""{0}"" class=""header2"">".FormatWith(this.ClientID));
        writer.WriteLine(
          "<td colspan=\"3\">{0}</td>".FormatWith(this.GetText("TOPICBROWSERS")));
        writer.WriteLine("</tr>");
        writer.WriteLine("<tr class=\"post\">");
        writer.WriteLine("<td colspan=\"3\">");
      }
      else
      {
        writer.WriteLine(@"<tr id=""{0}"" class=""header2"">".FormatWith(this.ClientID));
        writer.WriteLine("<td colspan=\"6\">{0}</td>".FormatWith(this.GetText("FORUMUSERS")));
        writer.WriteLine("</tr>");
        writer.WriteLine("<tr class=\"post\">");
        writer.WriteLine("<td colspan=\"6\">");
      }

      base.Render(writer);

      writer.WriteLine("</td>");
      writer.WriteLine("</tr>");
    }

    /// <summary>
    /// The forum users_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ForumUsers_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      bool bTopic = this.PageContext.PageTopicID > 0;

      if (this._activeUsers.ActiveUserTable == null)
      {
        this._activeUsers.ActiveUserTable =
          this.Get<IDBBroker>().StyleTransformDataTable(
            bTopic
              ? CommonDb.active_listtopic(PageContext.PageModuleID, this.PageContext.PageTopicID, this.PageContext.BoardSettings.UseStyledNicks)
              : CommonDb.active_listforum(PageContext.PageModuleID, this.PageContext.PageForumID, this.PageContext.BoardSettings.UseStyledNicks));
      }

      // add it...
      this.Controls.Add(this._activeUsers);
    }

    #endregion
  }
}