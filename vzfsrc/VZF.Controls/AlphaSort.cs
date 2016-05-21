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
 * File AlphaSort.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

  #endregion

    /// <summary>
  /// Control displaying list of letters and/or characters for filtering list of members.
  /// </summary>
  public class AlphaSort : BaseControl
  {
    #region Properties

    /// <summary>
    ///   Gets actually selected letter.
    /// </summary>
    public char CurrentLetter
    {
      get
      {
        char currentLetter = char.MinValue;

        if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("letter") != null)
        {
          // try to convert to char
          char.TryParse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("letter"), out currentLetter);

          // since we cannot use '#' in URL, we use '_' instead, this is to give it the right meaning
          if (currentLetter == '_')
          {
            currentLetter = '#';
          }
        }

        return currentLetter;
      }
    }

    /// <summary>
    ///   Gets pager redirect.
    /// </summary>
    public ForumPages PagerPage { get; set; }

    /// <summary>
    ///   Gets GroupID.
    /// </summary>
    public int? GroupID { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the Load event.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnLoad([NotNull] EventArgs e)
    {
      // IMPORTANT: call base implementation - calls event handlers
      base.OnLoad(e);

      var alphaSortDefList = new HtmlGenericControl("dl");
      alphaSortDefList.Attributes.Add("class", "AlphaSort");
      this.Controls.Add(alphaSortDefList);

      var headerTitle = new HtmlGenericControl("dt") { InnerText = this.GetText("ALPHABET_FILTER") };
      headerTitle.Attributes.Add("class", "header1");
      alphaSortDefList.Controls.Add(headerTitle);

      // get the localized character set
      string[] charSet = this.GetText("LANGUAGE", "CHARSET").Split('/');

      foreach (string t in charSet)
      {
          // get the current selected character (if there is one)
          char selectedLetter = this.CurrentLetter;

          // go through all letters in a set
          foreach (char letter in t)
          {
              var alphaListItem = new HtmlGenericControl("dd");

              // is letter selected?
              if (selectedLetter != char.MinValue && selectedLetter == letter)
              {
                  // current letter is selected, use specified style
                  alphaListItem.Attributes.Add("class", "SelectedLetter");
              }

              // create a link to this letter
              var link = new HyperLink
                  {
                      ToolTip = this.GetTextFormatted("ALPHABET_FILTER_BY", letter.ToString()),
                      Text = letter.ToString(),
                      NavigateUrl =
                          YafBuildLink.GetLinkNotEscaped(this.PagerPage, "letter={0}{1}", letter == '#' ? '_' : letter, GroupID !=null ? "&gr={0}".FormatWith(this.GroupID): string.Empty)
                  };

              alphaListItem.Controls.Add(link);

              alphaSortDefList.Controls.Add(alphaListItem);
          }
      }
    }

    #endregion
  }
}