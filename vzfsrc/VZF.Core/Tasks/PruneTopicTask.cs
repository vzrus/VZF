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
 * File PruneTopicTask.cs created  on 2.6.2015 in  6:29 AM.
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
  using System;

  using VZF.Data.Common;

  
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers.StringUtils;

  /// <summary>
  /// Run when we want to do migration of users in the background...
  /// </summary>
  public class PruneTopicTask : LongBackgroundTask
  {
    /// <summary>
    /// The _task name.
    /// </summary>
    private const string _taskName = "PruneTopicTask";

    /// <summary>
    /// The _days.
    /// </summary>
    private int _days;

    /// <summary>
    /// The _forum id.
    /// </summary>
    private int _forumId;

    /// <summary>
    /// The _perm delete.
    /// </summary>
    private bool _permDelete;

    private bool _deletedOnly;

    /// <summary>
    /// Initializes a new instance of the <see cref="PruneTopicTask"/> class.
    /// </summary>
    public PruneTopicTask()
    {
    }

    /// <summary>
    /// Gets TaskName.
    /// </summary>
    public static string TaskName
    {
      get
      {
        return _taskName;
      }
    }

    /// <summary>
    /// Gets or sets ForumId.
    /// </summary>
    public int ForumId
    {
      get
      {
        return this._forumId;
      }

      set
      {
        this._forumId = value;
      }
    }

    /// <summary>
    /// Gets or sets Days.
    /// </summary>
    public int Days
    {
      get
      {
        return this._days;
      }

      set
      {
        this._days = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether PermDelete.
    /// </summary>
    public bool PermDelete
    {
      get
      {
        return this._permDelete;
      }

      set
      {
        this._permDelete = value;
      }
    }

    public bool DeletedOnly
    {
        get
        {
            return this._deletedOnly;
        }

        set
        {
            this._deletedOnly = value;
        }
    }

    /// <summary>
    /// The start.
    /// </summary>
    /// <param name="boardId">
    /// The board id.
    /// </param>
    /// <param name="forumId">
    /// The forum id.
    /// </param>
    /// <param name="days">
    /// The days.
    /// </param>
    /// <param name="permDelete">
    /// The perm delete.
    /// </param>
    /// <returns>
    /// The start.
    /// </returns>
    public static bool Start(int moduleId, int boardId, int forumId, int days, bool permDelete, bool deletedOnly)
    {
      if (YafContext.Current.Get<ITaskModuleManager>() == null)
      {
        return false;
      }
        YafContext.Current.Get<ITaskModuleManager>().StartTask(
    		TaskName, () => new PruneTopicTask { Module = moduleId, Data = boardId, ForumId = forumId, Days = days, PermDelete = permDelete, DeletedOnly = deletedOnly });

      return true;
    }

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
      try
      {
        this.Logger.Info("Starting Prune Task for ForumID {0}, {1} Days, Perm Delete {2}, DeletedOnly {3}.", this.ForumId, this.Days, this.PermDelete, this.DeletedOnly);

        int count = CommonDb.topic_prune(YafContext.Current.PageModuleID, (int)this.Data, this.ForumId, this.Days, this.PermDelete, this.DeletedOnly);

        this.Logger.Info("Prune Task Complete. Pruned {0} topics.", count);
      }
      catch (Exception x)
      {
        this.Logger.Error(x, "Error In Prune Topic Task: {0}", x);
      }
    }
  }
}