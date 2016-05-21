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
 * File ForumDeleteTask.cs created  on 2.6.2015 in  6:29 AM.
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

using YAF.Types.Constants;

namespace YAF.Core.Tasks
{
    using System;

    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Types.Interfaces;

    /// <summary>
    /// The forum delete task.
    /// </summary>
    public class ForumDeleteTask : LongBackgroundTask, ICriticalBackgroundTask
    {
        #region Constants and Fields

        /// <summary>
        /// The _task name.
        /// </summary>
        private const string _TaskName = "ForumDeleteTask";

        #endregion

        #region Properties

        /// <summary>
        /// Gets TaskName.
        /// </summary>
        public static string TaskName
        {
            get
            {
                return _TaskName;
            }
        }

        /// <summary>
        /// The Blocking Task Names.
        /// </summary>
        private static readonly string[] BlockingTaskNames = Constants.ForumRebuild.BlockingTaskNames;

        /// <summary>
        /// Gets or sets ForumId.
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets Forum New Id.
        /// </summary>
        public int ForumNewId { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the Forum Delete Task
        /// </summary>
        /// <param name="moduleId">
        /// The module id.
        /// </param> 
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="failureMessage"> 
        /// The failure message - is empty if task is launched successfully.
        /// </param>
        /// <returns>
        /// Returns if Task was Successfull
        /// </returns>
        public static bool Start(int moduleId, int boardId, int forumId, out string failureMessage)
        {

            failureMessage = string.Empty;
            if (YafContext.Current.Get<ITaskModuleManager>() == null)
            {
                return false;
            }

            if (!YafContext.Current.Get<ITaskModuleManager>().AreTasksRunning(BlockingTaskNames))
            {
                YafContext.Current.Get<ITaskModuleManager>().StartTask(
                    TaskName,
                    () => new ForumDeleteTask
                    {
                        Data = boardId,
                        Module = moduleId,
                        ForumId = forumId,
                        ForumNewId = -1
                    });
            }
            else
            {
                failureMessage = "You can't delete forum while blocking {0} tasks are running.".FormatWith(BlockingTaskNames.ToDelimitedString(","));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the Forum Delete Task and moves the Messages to a new Forum
        /// </summary>
        /// <param name="moduleId">
        /// The module id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="forumOldId">
        /// The forum Old Id.
        /// </param>
        /// <param name="forumNewId">
        /// The Forum New Id.
        /// </param>
        /// <param name="failureMessage"> 
        /// The failure message - is empty if task is launched successfully.
        /// </param>
        /// <returns>
        /// Returns if Task was Successfull
        /// </returns>
        public static bool Start(int moduleId, int boardId, int forumOldId, int forumNewId, out string failureMessage)
        {
            failureMessage = string.Empty;
            if (YafContext.Current.Get<ITaskModuleManager>() == null)
            {
                return false;
            }

            if (!YafContext.Current.Get<ITaskModuleManager>().IsTaskRunning("ForumSaveTask"))
            {
            YafContext.Current.Get<ITaskModuleManager>().StartTask(TaskName, () => 
                new ForumDeleteTask { Data = boardId, 
                    Module = moduleId, 
                    ForumId = forumOldId, 
                    ForumNewId = forumNewId });
            }
            else
            {
                failureMessage = "You can't delete forum while ForumSaveTask is running.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// The run once.
        /// </summary>
        public override void RunOnce()
        {
            try
            {
                if (this.ForumNewId.Equals(-1))
                {
                    // Move children will be implemented when all data layers will be ready for ns. 
                    CommonDb.forum_delete(YafContext.Current.PageModuleID, this.ForumId, false);
                    this.Logger.Info("Forum (ID: {0}) Delete Task Complete.".FormatWith(this.ForumId));
                    CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, TaskName, "Forum (ID: {0}) Delete Task Complete.".FormatWith(this.ForumId), EventLogTypes.Information);
                }
                else
                {
                    CommonDb.forum_move(YafContext.Current.PageModuleID, this.ForumId, this.ForumNewId);

                    CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, TaskName, "Forum (ID: {0}) Delete Task Complete, and Topics has been moved to Forum (ID: {1})".FormatWith(this.ForumId, this.ForumNewId), EventLogTypes.Information);
                }
            }
            catch (Exception x)
            {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, TaskName, "Error In Forum (ID: {0}) Delete Task: {1}".FormatWith(this.ForumId, x));
            }
        }

        #endregion
    }
}