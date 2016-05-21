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
 * File PageAccess.cs created  on 2.6.2015 in  6:29 AM.
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
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The Page Access Control
  /// </summary>
  public class PageAccess : BaseControl
  {
    #region Methods

      /// <summary>
      /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
      /// </summary>
      /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      base.OnInit(e);
    }

    /// <summary>
    /// Renders the Control
    /// </summary>
    /// <param name="writer">The writer.</param>
    protected override void Render([NotNull] HtmlTextWriter writer)
    {
      writer.WriteLine(this.GetText(PageContext.ForumPostAccess ? "can_post" : "cannot_post"));
      writer.WriteLine("<br />");
      writer.WriteLine(this.GetText(PageContext.ForumReplyAccess ? "can_reply" : "cannot_reply"));
      writer.WriteLine("<br />");
      writer.WriteLine(this.GetText(PageContext.ForumDeleteAccess ? "can_delete" : "cannot_delete"));
      writer.WriteLine("<br />");
      writer.WriteLine(this.GetText(PageContext.ForumEditAccess ? "can_edit" : "cannot_edit"));
      writer.WriteLine("<br />");
      writer.WriteLine(this.GetText(PageContext.ForumPollAccess ? "can_poll" : "cannot_poll"));
      writer.WriteLine("<br />");
      writer.WriteLine(this.GetText(PageContext.ForumVoteAccess ? "can_vote" : "cannot_vote"));
      writer.WriteLine("<br />");
    }

    #endregion
  }
}