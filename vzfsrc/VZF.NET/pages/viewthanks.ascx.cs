/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 * 
 * The code was primarily written by Kamyar Payhan, modified by Jaben Cargman. 
 * The current paging logic was completely written by vzrus(Vladimir Zakharov).
 */

namespace YAF.Pages
{
  #region Using

  using System;
  using System.Data;
  
  using VZF.Controls;
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utilities;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for view thanks.
  /// </summary>
  public partial class ViewThanks : ForumPage
  {
      /// <summary>
      /// Indicates if the Active Tab was loaded
      /// </summary>
      private bool thankstoloaded;

      /// <summary>
      /// Indicates if the thanksfrom Tab was loaded
      /// </summary>
      private bool thanksfromloaded;
      
      /// <summary>
      ///  Gets or sets the current tab.
      ///  </summary>
      ///  <value>
      ///  The current tab.
      /// </value>
      private ThanksListMode CurrentTab
      {
          get
          {
              if (this.ViewState["CurrentTab"] != null)
              {
                  return (ThanksListMode)this.ViewState["CurrentTab"];
              }

              return ThanksListMode.FromUser;
          }

          set
          {
              this.ViewState["CurrentTab"] = value;
          }
      }
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "ViewThanks" /> class. 
    ///   Initializes a new instance of the viewthanks class.
    /// </summary>
    public ViewThanks()
      : base("VIEWTHANKS")
    {
    }

    #endregion

    /* Methods */
    #region Methods

    /// <summary>
    /// The On PreRender event.
    /// </summary>
    /// <param name="e">
    /// the Event Arguments
    /// </param>
    protected override void OnPreRender([NotNull] EventArgs e)
    {
        // setup jQuery and Jquery Ui Tabs.
        YafContext.Current.PageElements.RegisterJQuery();
        YafContext.Current.PageElements.RegisterJQueryUI();

        YafContext.Current.PageElements.RegisterJsBlock(
            "ThanksTabsJs",
            JavaScriptBlocks.JqueryUITabsLoadJs(
                this.ThanksTabs.ClientID,
                this.hidLastTab.ClientID,
                this.hidLastTabId.ClientID,
                this.Page.ClientScript.GetPostBackEventReference(ChangeTab, string.Empty),
                false,
                true));

        base.OnPreRender(e);
    }

    /// <summary>
    /// The Page_ Load Event.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
        var userId = (int)Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("u"));

        if (this.IsPostBack) return;

        this.PageLinks.Clear();
        this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
            this.PageContext.BoardSettings.EnableDisplayName  
                ? UserMembershipHelper.GetDisplayNameFromID(userId) : UserMembershipHelper.GetUserNameFromID(userId), 
            YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId));
        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);
        this.CurrentTab = ThanksListMode.FromUser;
        this.ThanksFromList.AutoDatabind = true;
        this.ThanksFromList.DataBind();
    }

    #endregion

    /// <summary>
    /// Load the Selected Tab Content
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ChangeTabClick(object sender, EventArgs e)
    {
        switch (this.hidLastTabId.Value)
        {
            case "ThanksFromTab":
                this.CurrentTab = ThanksListMode.FromUser;
                break;
            case "ThanksToTab":
                this.CurrentTab = ThanksListMode.ToUser;
                break;
            default:
                this.CurrentTab = ThanksListMode.FromUser;
                break; 
        }
      
       this.RefreshTab();
    }

    /// <summary>
    /// Refreshes the tab.
    /// </summary>
    private void RefreshTab()
    {
        switch (this.CurrentTab)
        {
            case ThanksListMode.FromUser:

                if (!this.thanksfromloaded)
                {
                    this.ThanksFromList.AutoDatabind = false;
                    this.ThanksFromList.BindData();
                    this.thanksfromloaded = true;
                }

                break;
            case ThanksListMode.ToUser:

                if (!this.thankstoloaded)
                {
                    this.ThanksToList.AutoDatabind = false;
                    this.ThanksToList.BindData();
                    this.thankstoloaded = true;
                }

                break;
        }
    }
  }
}