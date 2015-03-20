﻿ using System;

  using System.Linq;
  using System.Threading;
 using System.Web;
 using VZF.Data.Common;

  
  using VZF.Utils;
  using VZF.Utils.Helpers;
  using VZF.Utils.Helpers.StringUtils;
 using YAF.Core;
 using YAF.Core.Nntp;
 using YAF.Core.Tasks;
 using YAF.Types.Interfaces;
 using YAF.Types.Objects;

/// <summary>
/// Gets nntp messages from active topics...
/// </summary>
public class SyncNntpTask : IntermittentBackgroundTask
{
    /// <summary>
    /// The _task name.
    /// </summary>
    private const string _taskName = "SyncNntpTask";

    /// <summary>
    /// The _application state base.
    /// </summary>
    private readonly HttpApplicationStateBase _applicationStateBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="SyncNntpTask"/> class.
    /// </summary>
    public SyncNntpTask()
    {
        // set interval values seconds... Runs every 10 minutes.
        this.RunPeriodMs = 10*60*1000; 
        this.StartDelayMs = 30*1000;

        //  this._applicationStateBase = applicationStateBase;
    }

    /// <summary>
    /// Gets TaskName.
    /// </summary>
    public static string TaskName
    {
        get { return _taskName; }
    }

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
        try
        {
            // 30 minutes by default
            int fetchEveryXMinutes = YafContext.Current.BoardSettings.NntpTopicProtectionPeriod;
            int retrieveSeconds = YafContext.Current.BoardSettings.NntpArticlesRetrieveTime;
            if (retrieveSeconds < 1)
            {
                retrieveSeconds = 1;
            }
            if (fetchEveryXMinutes < 1)
            {
                fetchEveryXMinutes = 1;
            }


            int guestUserId = UserMembershipHelper.GuestUserId; // Use guests user-id

            // string hostAddress = YafContext.Current.Get<HttpRequestBase>().UserHostAddress;     
            DateTime dateTimeStart = DateTime.UtcNow;
            int articleCount = 0;
            int count = 0;
            int boardId = YafContext.Current.PageBoardID;
            try
            {
                // this._applicationStateBase["WorkingInYafNNTP"] = true;

                // Only those not updated in the last 30 minutes
                foreach (
                    var nntpForum in
                        CommonDb.NntpForumList(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID,
                            fetchEveryXMinutes, null, true))
                {
                    using (var nntpConnection = YafNntp.GetNntpConnection(nntpForum))
                    {
                        Newsgroup group = nntpConnection.ConnectGroup(nntpForum.GroupName);

                        var lastMessageNo = nntpForum.LastMessageNo ?? 0;

                        // start at the bottom...
                        int currentMessage = lastMessageNo == 0 ? group.Low : lastMessageNo + 1;
                        var nntpForumId = nntpForum.NntpForumID;
                        var cutOffDate = nntpForum.DateCutOff ?? DateTimeHelper.SqlDbMinTime();

                        if (nntpForum.DateCutOff.HasValue)
                        {
                            bool behindCutOff = true;

                            // advance if needed...
                            do
                            {
                                var list = nntpConnection.GetArticleList(currentMessage,
                                    Math.Min(currentMessage + 500, group.High));

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
                            } while (behindCutOff);

                            // update the group lastMessage info...
                            CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID,
                                currentMessage, guestUserId);
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
                                    this.Logger.Debug("Skipped message id {0} due to date being {1}.", currentMessage,
                                        dateTime);
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

                                if (YafContext.Current.BoardSettings.CreateNntpUsers)
                                {
                                    guestUserId = CommonDb.user_nntp(YafContext.Current.PageModuleID, boardId, fromName,
                                        string.Empty, article.Header.TimeZoneOffset);
                                }

                                string body = article.Body.Text.Trim();

                                body = body.Replace("<br>", "<br />");
                                body = body.Replace("<hr>", "<hr />");

                                CommonDb.nntptopic_addmessage(YafContext.Current.PageModuleID, nntpForumId,
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
                                if ((DateTime.UtcNow - dateTimeStart).TotalSeconds > retrieveSeconds)
                                {
                                    break;
                                }

                                if (count++ > 1000)
                                {
                                    count = 0;
                                    CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID,
                                        lastMessageNo, guestUserId);
                                }
                                Thread.Sleep(1000);
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

                        CommonDb.nntpforum_update(YafContext.Current.PageModuleID, nntpForum.NntpForumID, lastMessageNo,
                            guestUserId);

                        // Total time x seconds for all groups
                        if ((DateTime.UtcNow - dateTimeStart).TotalSeconds > retrieveSeconds)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                // this._applicationStateBase["WorkingInYafNNTP"] = null;

                // display how many were fetched
               /* this.Logger.Info("Task {0}: {1} articles fetched in {2} seconds.".FormatWith(TaskName, articleCount,
                    retrieveSeconds)); */
            }
        }
        catch (Exception x)
        {
            this.Logger.Error(x, "Exception In {1}: {0}".FormatWith(TaskName, x.ToString()));
        }
    }
}