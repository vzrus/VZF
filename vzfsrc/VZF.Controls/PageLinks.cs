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
 * File PageLinks.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Data;
    using System.Web.UI;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

  /// <summary>
  /// Summary description for PageLinks.
  /// </summary>
  public class PageLinks : BaseControl
  {
    #region Properties

    /// <summary>
    ///   Gets or sets LinkedPageLinkID.
    /// </summary>
    [CanBeNull]
    public string LinkedPageLinkID
    {
      get
      {
        if (this.ViewState["LinkedPageLinkID"] != null)
        {
          return this.ViewState["LinkedPageLinkID"].ToString();
        }

        return null;
      }

      set
      {
        this.ViewState["LinkedPageLinkID"] = value;
      }
    }

    /// <summary>
    ///   Gets or sets PageLinkDT.
    /// </summary>
    [CanBeNull]
    protected DataTable PageLinkDT
    {
      get
      {
        if (this.ViewState["PageLinkDT"] != null)
        {
          return this.ViewState["PageLinkDT"] as DataTable;
        }

        return null;
      }

      set
      {
        this.ViewState["PageLinkDT"] = value;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The add forum links.
    /// </summary>
    /// <param name="forumID">
    /// The forum id.
    /// </param>
    public void AddForumLinks(int forumID)
    {
      this.AddForumLinks(forumID, false);
    }

    /// <summary>
    /// The add forum links.
    /// </summary>
    /// <param name="forumID">
    /// The forum id.
    /// </param>
    /// <param name="noForumLink">
    /// The no forum link.
    /// </param>
    public void AddForumLinks(int forumID, bool noForumLink)
    {
      using (DataTable dtLinks = CommonDb.forum_listpath(PageContext.PageModuleID, forumID))
      {
        foreach (DataRow row in dtLinks.Rows)
        {
          if (noForumLink && Convert.ToInt32(row["ForumID"]) == forumID)
          {
            this.AddLink(row["Name"].ToString(), string.Empty);
          }
          else
          {
            this.AddLink(row["Name"].ToString(), YafBuildLink.GetLink(ForumPages.topics, "f={0}", row["ForumID"]));
          }
        }
      }
    }

    /// <summary>
    /// The add link.
    /// </summary>
    /// <param name="title">
    /// The title.
    /// </param>
    public void AddLink([NotNull] string title)
    {
      this.AddLink(title, string.Empty);
    }

    /// <summary>
    /// The add link.
    /// </summary>
    /// <param name="title">
    /// The title.
    /// </param>
    /// <param name="url">
    /// The url.
    /// </param>
    public void AddLink([NotNull] string title, [NotNull] string url)
    {
      DataTable dt = this.PageLinkDT;

      if (dt == null)
      {
        dt = new DataTable();
        dt.Columns.Add("Title", typeof(string));
        dt.Columns.Add("URL", typeof(string));
        this.PageLinkDT = dt;
      }

      DataRow dr = dt.NewRow();
      dr["Title"] = title;
      dr["URL"] = url;
      dt.Rows.Add(dr);
    }

    /// <summary>
    /// Clear all Links
    /// </summary>
    public void Clear()
    {
      if (this.PageLinkDT != null)
      {
        this.PageLinkDT = null;
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
      DataTable linkDataTable = null;

      if (this.LinkedPageLinkID.IsSet())
      {
        // attempt to get access to the other control...
        var plControl = this.Parent.FindControl(this.LinkedPageLinkID) as PageLinks;

        if (plControl != null)
        {
          // use the other data stream...
          linkDataTable = plControl.PageLinkDT;
        }
      }
      else
      {
        // use the data table from this control...
        linkDataTable = this.PageLinkDT;
      }

      if (linkDataTable == null || linkDataTable.Rows.Count == 0)
      {
        return;
      }

      writer.WriteLine(@"<div id=""{0}"" class=""yafPageLink"">".FormatWith(this.ClientID));

      bool bFirst = true;
      foreach (DataRow row in linkDataTable.Rows)
      {
        if (!bFirst)
        {
          writer.WriteLine(@"<span class=""linkSeperator"">&nbsp;&#187;&nbsp;</span>");
        }
        else
        {
          bFirst = false;
        }

        string title = this.HtmlEncode(row["Title"].ToString().Trim());
        string url = row["URL"].ToString().Trim();

        if (url.IsNotSet())
        {
          writer.WriteLine(@"<span class=""currentPageLink"">{0}</span>".FormatWith(title));
        }
        else
        {
          writer.WriteLine(@"<a href=""{0}"">{1}</a>".FormatWith(url, title));
        }
      }

      writer.WriteLine("</div>");
    }

    #endregion
  }
}