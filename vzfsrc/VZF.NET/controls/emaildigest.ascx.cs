/* Yet Another Forum.NET
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
 */
namespace VZF.Controls
{
  #region Using

    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using VZF.Data.Common;
    using VZF.Utils;
    using VZF.Utils.Helpers;
    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

  /// <summary>
  /// The email_digest.
  /// </summary>
  public partial class emaildigest : BaseUserControl
  {
    #region Constants and Fields

    /// <summary>
    ///   The _combined user data.
    /// </summary>
    private CombinedUserDataHelper _combinedUserData;

      /// <summary>
    ///   The _language file.
    /// </summary>
    private string _languageFile;

    /// <summary>
    ///   The _yaf localization.
    /// </summary>
    private ILocalization _localization;

    /// <summary>
    ///   The _theme.
    /// </summary>
    private YafTheme _theme;

    /// <summary>
    ///   Numbers of hours to compute digest for...
    /// </summary>
    private int _topicHours = -24;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets ActiveTopics.
    /// </summary>
    [NotNull]
    public DataTable ActiveTopics
    {
        get;
        set;
    }

    /// <summary>
    ///   Gets or sets BoardID.
    /// </summary>
    public int BoardId { get; set; }

    /// <summary>
    ///   Gets or sets CurrentUserID.
    /// </summary>
    public int CurrentUserId { get; set; }

    /// <summary>
    ///   Gets NewTopics.
    /// </summary>
    [NotNull]
    public DataTable NewTopics
    {
        get; set;
    }

    /// <summary>
    ///   Gets UserData.
    /// </summary>
    [NotNull]
    public CombinedUserDataHelper UserData
    {
      get
      {
        if (this._combinedUserData == null)
        {
          this._combinedUserData = new CombinedUserDataHelper(this.CurrentUserId);
        }

        return this._combinedUserData;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The get text.
    /// </summary>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The get text.
    /// </returns>
    public string GetText([NotNull] string tag)
    {
      if (this._languageFile.IsSet() && this._localization == null)
      {
        this._localization = new YafLocalization();
        this._localization.LoadTranslation(this._languageFile);
      }
      else if (this._localization == null)
      {
        this._localization = this.Get<ILocalization>();
      }

      return this._localization.GetText("DIGEST", tag);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get message formatted and truncated.
    /// </summary>
    /// <param name="lastMessage">
    /// The last message.
    /// </param>
    /// <param name="maxlength">
    /// The maxlength.
    /// </param>
    /// <returns>
    /// The get message formatted and truncated.
    /// </returns>
    protected string GetMessageFormattedAndTruncated([NotNull] string lastMessage, int maxlength)
    {
      return
        StringExtensions.RemoveMultipleWhitespace(
            BBCodeHelper.StripBBCode(HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString(lastMessage)))).Truncate(maxlength);
    }

    /// <summary>
    /// The output error.
    /// </summary>
    /// <param name="errorString">
    /// The error string.
    /// </param>
    protected void OutputError([NotNull] string errorString)
    {
      this.Response.Write("<html><head>Error</head><body><h1>{0}</h1></body></html>".FormatWith(errorString));
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.Get<StartupInitializeDb>().Run();

      var token = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("token");

      bool showErrors = false;

      if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("showerror").IsSet())
      {
        showErrors = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("showerror").ToType<bool>();
      }

      if (token.IsNotSet() || !token.Equals(this.Get<YafBoardSettings>().WebServiceToken))
      {
        if (showErrors)
        {
          this.OutputError(
            "Invalid Web Service Token. Please go into your host settings and save them committing a unique web service token to the database.");
        }

        this.Response.End();
        return;
      }

      if (Config.BaseUrlMask.IsNotSet())
      {
        // fail... BaseUrlMask required for Digest.
        if (showErrors)
        {
          this.OutputError(
            "Cannot generate digest unless YAF.BaseUrlMask AppSetting is specified in your app.config (default). Please specify the full forward-facing url to this forum in the YAF.BaseUrlMask key.");
        }

        this.Response.End();
        return;
      }

      if (Config.ForceScriptName.IsNotSet())
      {
        // fail... ForceScriptName required for Digest.
        if (showErrors)
        {
          this.OutputError(
            @"Cannot generate digest unless YAF.ForceScriptName AppSetting is specified in your app.config (default). Please specify the full page name for YAF.NET -- usually ""default.aspx"".");
        }

        this.Response.End();
        return;
      }

      if (this.CurrentUserId == 0)
      {
        this.CurrentUserId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("UserID").ToType<int>();
      }

      if (this.BoardId == 0)
      {
        this.BoardId = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("BoardID").ToType<int>();
      }

      // get topic hours...
      this._topicHours = -this.Get<YafBoardSettings>().DigestSendEveryXHours;
     
      this.NewTopics = CommonDb.digest_topicnew(PageContext.PageModuleID, this.BoardId, this.CurrentUserId, DateTime.UtcNow.AddHours(this._topicHours), null, true);
      this.ActiveTopics = CommonDb.digest_topicactive(PageContext.PageModuleID, this.BoardId, this.CurrentUserId, DateTime.UtcNow.AddHours(this._topicHours), DateTime.UtcNow, true);

      if (this.NewTopics.Rows.Count <= 0 && this.ActiveTopics.Rows.Count <= 0)
      {
        if (showErrors)
        {
          this.OutputError(
            "No topics for the last {0} hours.".FormatWith(this.Get<YafBoardSettings>().DigestSendEveryXHours));
        }

        this.Response.End();
        return;
      }

      this._languageFile = UserHelper.GetUserLanguageFile(this.CurrentUserId);
      this._theme = new YafTheme(UserHelper.GetUserThemeFile(this.CurrentUserId));

      string subject = this.GetText("SUBJECT").FormatWith(this.Get<YafBoardSettings>().Name);

      string digestHead = this._theme.GetItem("THEME", "DIGESTHEAD", null);

      if (digestHead.IsSet())
      {
        this.YafHead.Controls.Add(new LiteralControl(digestHead));
      }

      if (subject.IsSet())
      {
        this.YafHead.Title = subject;
      }
    }

    #endregion
  }
}