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
 * File MigrateUsersTask.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Tasks
{
  #region Using

  using System;

  using VZF.Data.Common;

  
  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Run when we want to do migration of users in the background...
  /// </summary>
  public class MigrateUsersTask : LongBackgroundTask
  {
    #region Constants and Fields

    /// <summary>
    ///   The _task name.
    /// </summary>
    private const string _taskName = "MigrateUsersTask";

    #endregion

    #region Properties

    /// <summary>
    ///   Gets TaskName.
    /// </summary>
    [NotNull]
    public static string TaskName
    {
      get
      {
        return _taskName;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The start.
    /// </summary>
    /// <param name="boardId">
    /// The board id.
    /// </param>
    /// <returns>
    /// The start.
    /// </returns>
    public static bool Start(int moduleId, int boardId)
    {
      if (YafContext.Current.Get<ITaskModuleManager>() == null)
      {
        return false;
      }

      YafContext.Current.Get<ITaskModuleManager>().StartTask(TaskName, () => new MigrateUsersTask { Data = boardId,Module = moduleId});

			return true;
    }

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
      try
      {
        // attempt to run the migration code...
        RoleMembershipHelper.SyncUsers((int)this.Module,(int)this.Data);
      }
      catch (Exception x)
      {
          CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, TaskName, "Error In MigrateUsers Task: {0}".FormatWith(x));
      }
    }

    #endregion
  }
}