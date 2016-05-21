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
 * File DigestSendTask.cs created  on 2.6.2015 in  6:29 AM.
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

using System.Globalization;

namespace YAF.Core.Tasks
{
  #region Using

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using VZF.Data.Common;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    
    using YAF.Types;
    using YAF.Types.Interfaces;
    using YAF.Types.Objects;
    using VZF.Utils;

  #endregion

  /// <summary>
  /// The digest send task.
  /// </summary>
  public class DigestSendTask : IntermittentBackgroundTask
  {
    #region Constants and Fields

    /// <summary>
    ///   The _task name.
    /// </summary>
    private const string _TaskName = "DigestSendTask";

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "DigestSendTask" /> class.
    /// </summary>
    public DigestSendTask()
    {
      this.RunPeriodMs = 300 * 1000;
      this.StartDelayMs = 30 * 1000;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets TaskName.
    /// </summary>
    public static string TaskName
    {
      get
      {
        return _TaskName;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
      //// validate DB run...
      ////this.Get<StartupInitializeDb>().Run();

      this.SendDigest();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Determines whether is time to send digest for board.
    /// </summary>
    /// <param name="boardSettings">The board settings.</param>
    /// <returns>
    /// The is time to send digest for board.
    /// </returns>
    private bool IsTimeToSendDigestForBoard([NotNull] YafLoadBoardSettings boardSettings)
    {
      CodeContracts.ArgumentNotNull(boardSettings, "boardSettings");

      if (boardSettings.AllowDigestEmail)
      {
          DateTime lastSend = DateTimeHelper.SqlDbMinTime();
        bool sendDigest = false;
        int sendEveryXHours = boardSettings.DigestSendEveryXHours;

        if (boardSettings.LastDigestSend.IsSet())
        {           
        
            lastSend = DateTime.Parse(boardSettings.LastDigestSend, CultureInfo.InvariantCulture);
          
        }

#if (DEBUG)
        // haven't sent in X hours or more and it's 12 to 5 am.
        sendDigest = lastSend < DateTime.UtcNow.AddHours(-sendEveryXHours);      
#else
        // haven't sent in X hours or more and it's 12 to 5 am.
        sendDigest = lastSend < DateTime.Now.AddHours(-sendEveryXHours) && DateTime.Now < DateTime.Today.AddHours(6);
#endif
        if (sendDigest || boardSettings.ForceDigestSend)
        {
          // && DateTime.Now < DateTime.Today.AddHours(5))
          // we're good to send -- update latest send so no duplication...
            boardSettings.LastDigestSend = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
          boardSettings.ForceDigestSend = false;
          boardSettings.SaveRegistry(
              new Dictionary<string, object>
                {
                    {"ForceDigestSend", false},
                    {"LastDigestSend", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}
                },
                YafContext.Current.PageBoardID);

          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// The send digest.
    /// </summary>
    private void SendDigest()
    {
      try
      {
          var boardIds = CommonDb.board_list(YafContext.Current.PageModuleID, null).AsEnumerable().Select(b => b.Field<int>("BoardID"));

        foreach (var boardId in boardIds)
        {
          var boardSettings = new YafLoadBoardSettings(boardId);

          if (!this.IsTimeToSendDigestForBoard(boardSettings))
          {
            continue;
          }

          if (Config.BaseUrlMask.IsNotSet())
          {
            // fail...
              CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, "DigestSendTask", "Failed to send digest because BaseUrlMask value is not set in your appSettings.");
            return;
          }

          // get users with digest enabled...
          var usersWithDigest =
            CommonDb.UserFind(YafContext.Current.PageModuleID, boardId, false, null, null, null, null, true).Where(x => !x.IsGuest && (x.IsApproved ?? false));

          if (usersWithDigest.Any())
          {
            // start sending...
            this.SendDigestToUsers(usersWithDigest, boardId, boardSettings);
          }
        }
      }
      catch (Exception ex)
      {
          this.Logger.Error(ex, "Error In {0} Task.".FormatWith(TaskName));
      }
    }

    /// <summary>
    /// Sends the digest to users.
    /// </summary>
    /// <param name="usersWithDigest">The users with digest.</param>
    /// <param name="boardId">The board id.</param>
    /// <param name="boardSettings">The board settings.</param>
    private void SendDigestToUsers(IEnumerable<TypedUserFind> usersWithDigest, int boardId, YafLoadBoardSettings boardSettings)
    {
        foreach (var user in usersWithDigest)
        {
            string digestHtml = string.Empty;

            try
            {
                digestHtml = this.Get<IDigest>().GetDigestHtml(user.UserID ?? 0, boardId, boardSettings.WebServiceToken);
            }
            catch (Exception e)
            {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, TaskName, "Error In Creating Digest for User {0}: {1}".FormatWith(user.UserID, e.ToString()));
            }

            if (!digestHtml.IsSet())
            {
                continue;
            }

            if (user.ProviderUserKey == null)
            {
                continue;
            }

            var membershipUser = UserMembershipHelper.GetUser(user.ProviderUserKey);

            if (membershipUser == null || membershipUser.Email.IsNotSet())
            {
                continue;
            }

            // send the digest...
            this.Get<IDigest>().SendDigest(
                digestHtml, boardSettings.Name, boardSettings.ForumEmail, membershipUser.Email, user.DisplayName, true);
        }
    }

      #endregion
  }
}