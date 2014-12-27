/* Yet Another Forum.net
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
 */

using VZF.Nntp;

namespace YAF.Core.Nntp
{
  #region Using

  using System;
 
  using System.Linq;
  using System.Web;

  using VZF.Data.Common;
  using VZF.Utils.Helpers;

  using YAF.Types;
  using YAF.Types.Interfaces;
  using YAF.Types.Objects;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The on request delegate.
  /// </summary>
  /// <param name="msg">
  /// The msg.
  /// </param>
  public delegate void OnRequestDelegate(string msg);

  /// <summary>
  /// The yaf nntp.
  /// </summary>
  public class YafNntp : INewsreader
  {
    #region Constants and Fields

    /// <summary>
    /// The _application state base.
    /// </summary>
    private readonly HttpApplicationStateBase _applicationStateBase;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="YafNntp"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="applicationStateBase">
    /// The application state base.
    /// </param>
    public YafNntp([NotNull] ILogger logger, [NotNull] HttpApplicationStateBase applicationStateBase)
    {
      this._applicationStateBase = applicationStateBase;
      this.Logger = logger;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets Logger.
    /// </summary>
    public ILogger Logger { get; set; }

    #endregion

    #region Implemented Interfaces

    #region INewsreader

    /// <summary>
    /// The read articles.
    /// </summary>
    /// <param name="boardID">
    /// The board id.
    /// </param>
    /// <param name="lastUpdate">
    /// The n last update.
    /// </param>
    /// <param name="timeToRun">
    /// The n time to run.
    /// </param>
    /// <param name="createUsers">
    /// The b create users.
    /// </param>
    /// <returns>
    /// The read articles.
    /// </returns>
    /// <exception cref="NntpException"><c>NntpException</c>.</exception>
    public int ReadArticles(int boardID, int lastUpdate, int timeToRun, bool createUsers)
    {
      if (this._applicationStateBase["WorkingInYafNNTP"] != null)
      {
        return 0;
      }

      int guestUserId = UserMembershipHelper.GuestUserId; // Use guests user-id

      // string hostAddress = YafContext.Current.Get<HttpRequestBase>().UserHostAddress;     
      DateTime dateTimeStart = DateTime.UtcNow;
      int articleCount = 0;
      int count = 0;

      try
      {
        this._applicationStateBase["WorkingInYafNNTP"] = true;

        // Only those not updated in the last 30 minutes
        foreach (var nntpForum in CommonDb.NntpForumList(YafContext.Current.PageModuleID, boardID, lastUpdate, null, true))
        {
          using (var nntpConnection = GetNntpConnection(nntpForum))
          {
            Newsgroup group = nntpConnection.ConnectGroup(nntpForum.GroupName);

            var lastMessageNo = nntpForum.LastMessageNo ?? 0;

            // start at the bottom...
            int currentMessage = lastMessageNo == 0 ? group.Low : lastMessageNo + 1;
            var nntpForumID = nntpForum.NntpForumID;
            var cutOffDate = nntpForum.DateCutOff ?? DateTimeHelper.SqlDbMinTime();

            if (nntpForum.DateCutOff.HasValue)
            {
              bool behindCutOff = true;

              // advance if needed...
              do
              {
                var list = nntpConnection.GetArticleList(currentMessage, Math.Min(currentMessage + 500, group.High));

                foreach (var article in list)
                {
                  if (article.Header.Date.Year < 1950 || article.Header.Date > DateTime.UtcNow)
                  {
                    article.Header.Date = DateTime.UtcNow;
                  }

                  if (article.Header.Date >= cutOffDate)
                  {
                    behindCutOff = false;
                    break;
                  }

                  currentMessage++;
                }
              }
              while (behindCutOff);

              // update the group lastMessage info...
              CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID, currentMessage, guestUserId);
            }

            for (; currentMessage <= group.High; currentMessage++)
            {
              Article article;

              try
              {
                try
                {
                  article = nntpConnection.GetArticle(currentMessage);
                }
                catch (InvalidOperationException ex)
                {
                  Logger.Error(ex, "Error Downloading Message ID {0}", currentMessage);

                  // just advance to the next message
                  currentMessage++;
                  continue;
                }

                string subject = article.Header.Subject.Trim();
                string originalName = article.Header.From.Trim();
                string fromName = originalName;
                DateTime dateTime = article.Header.Date;

                if (dateTime.Year < 1950 || dateTime > DateTime.UtcNow)
                {
                  dateTime = DateTime.UtcNow;
                }

                if (dateTime < cutOffDate)
                {
                  this.Logger.Debug("Skipped message id {0} due to date being {1}.", currentMessage, dateTime);
                  continue;
                }

                if (fromName.IsSet() && fromName.LastIndexOf('<') > 0)
                {
                  fromName = fromName.Substring(0, fromName.LastIndexOf('<') - 1);
                  fromName = fromName.Replace("\"", String.Empty).Trim();
                }
                else if (fromName.IsSet() && fromName.LastIndexOf('(') > 0)
                {
                  fromName = fromName.Substring(0, fromName.LastIndexOf('(') - 1).Trim();
                }

                if (fromName.IsNotSet())
                {
                  fromName = originalName;
                }

                string externalMessageId = article.MessageId;

                string referenceId = article.Header.ReferenceIds.LastOrDefault();

                if (createUsers)
                {
                    guestUserId = CommonDb.user_nntp(YafContext.Current.PageModuleID, boardID, fromName, string.Empty, article.Header.TimeZoneOffset);
                }

                string body = this.ReplaceBody(article.Body.Text.Trim());

                CommonDb.nntptopic_savemessage(YafContext.Current.PageModuleID, nntpForumID,
                  subject.Truncate(75),
                  body,
                  guestUserId,
                  fromName.Truncate(100, String.Empty),
                  "NNTP",
                  dateTime,
                  externalMessageId.Truncate(255, String.Empty),
                  referenceId.Truncate(255, String.Empty));

                lastMessageNo = currentMessage;

                articleCount++;

                // We don't wanna retrieve articles forever...
                // Total time x seconds for all groups
                if ((DateTime.UtcNow - dateTimeStart).TotalSeconds > timeToRun)
                {
                  break;
                }

                if (count++ > 1000)
                {
                  count = 0;
                  CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID, lastMessageNo, guestUserId);
                }
              }
              catch (NntpException exception)
              {
                if (exception.ErrorCode >= 900)
                {
                  throw;
                }
                else if (exception.ErrorCode != 423)
                {
                  this.Logger.Error(exception, "YafNntp");
                }
              }
            }

            CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID, lastMessageNo, guestUserId);

            // Total time x seconds for all groups
            if ((DateTime.UtcNow - dateTimeStart).TotalSeconds > timeToRun)
            {
              break;
            }
          }
        }
      }
      finally
      {
        this._applicationStateBase["WorkingInYafNNTP"] = null;
      }

      return articleCount;
    }

    [NotNull]
    public static NntpConnection GetNntpConnection([NotNull] TypedNntpForum nntpForum)
    {
      CodeContracts.ArgumentNotNull(nntpForum, "nntpForum");

      var nntpConnection = new NntpConnection();

      // call connect server
      nntpConnection.ConnectServer(nntpForum.Address.ToLower(), nntpForum.Port ?? 119);
      
      // provide authentication if required...
      if (nntpForum.UserName.IsSet() && nntpForum.UserPass.IsSet())
      {
        nntpConnection.ProvideIdentity(nntpForum.UserName, nntpForum.UserPass);
        nntpConnection.SendIdentity();
      }

      return nntpConnection;
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The replace body.
    /// </summary>
    /// <param name="body">
    /// The body.
    /// </param>
    /// <returns>
    /// The replace body.
    /// </returns>
    [NotNull]
    private string ReplaceBody([NotNull] string body)
    {
      // Incorrect tags fixes which are common in nntp messages and cause display problems.
      // These are spotted ones.
      body = body.Replace("<br>", "<br />");
      body = body.Replace("<hr>", "<hr />");
      return body;
    }

    #endregion
  }
}