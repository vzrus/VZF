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
 * File BoardDeleteTask.cs created  on 2.6.2015 in  6:29 AM.
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


// vzrus
namespace YAF.Core.Tasks
{
    #region Using
    using System;

    using VZF.Data.Common;

    using YAF.Types.Constants;
    
    using YAF.Types.Interfaces;
    using VZF.Utils; 
    #endregion

    /// <summary>
    /// The forum delete task.
    /// </summary>
    public class BoardDeleteTask : LongBackgroundTask, ICriticalBackgroundTask,IBlockableTask
    {
        #region Constants and Fields

        /// <summary>
        /// The _task name.
        /// </summary>
        private const string _TaskName = "BoardDeleteTask";

        /// <summary>
        /// The Blocking Task Names.
        /// </summary>
        private static readonly string[] BlockingTaskNames = Constants.ForumRebuild.BlockingTaskNames;

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
        /// Gets or sets BoardIdToDelete.
        /// </summary>
        public int BoardIdToDelete { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the Board Delete Task
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="failureMessage"> 
        /// The failure message - is empty if task is launched successfully.
        /// </param>
        /// <returns>
        /// Returns if Task was Successfull
        /// </returns>
        public static bool Start(int boardId, out string failureMessage)
        {
            failureMessage = string.Empty;
            if (YafContext.Current.Get<ITaskModuleManager>() == null)
            {
                return false;
            }
            if (!YafContext.Current.Get<ITaskModuleManager>().AreTasksRunning(BlockingTaskNames))
            {
            YafContext.Current.Get<ITaskModuleManager>().StartTask(TaskName, () => new BoardDeleteTask { BoardIdToDelete = boardId});
            }
            else
            {
                failureMessage = "You can't delete the board while some of the blocking {0} tasks are running.".FormatWith(BlockingTaskNames.ToDelimitedString(","));
               
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
                this.Logger.Info(
                  "Starting Board delete task for BoardId {0} delete task.",
                  this.BoardIdToDelete);
                CommonDb.board_delete(YafContext.Current.PageModuleID, this.BoardIdToDelete);
                this.Logger.Info(
                 "Board delete task for BoardId {0} delete task is completed.",
                 this.BoardIdToDelete);
            }
            catch (Exception x)
            {
                this.Logger.Error(x, "Error In Board (ID: {0}) Delete Task: {1}".FormatWith(this.BoardIdToDelete), TaskName);
            }
        }

        #endregion
    }
}