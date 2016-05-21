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
 * File CategorySaveTask.cs created  on 2.6.2015 in  6:29 AM.
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

  
  using YAF.Types.Interfaces;
  using VZF.Utils;

  /// <summary>
  /// Run when we want to do migration of users in the background...
  /// </summary>
    public class CategorySaveTask : LongBackgroundTask, ICriticalBackgroundTask
  {

      /// <summary>
      /// The _board id.
      /// </summary>
      private object _boardIdToSave;

      /// <summary>
      /// Gets or sets BoardIdToSave.
      /// </summary>
      public object BoardIdToSave
      {
          get
          {
              return this._boardIdToSave;
          }

          set
          {
              this._boardIdToSave = value;
          }
      }

      /// <summary>
      /// The _category Id.
      /// </summary>
      private object _categoryId;

      /// <summary>
      /// Gets or sets CategoryId.
      /// </summary>
      public object CategoryId
      {
          get
          {
              return this._categoryId;
          }

          set
          {
              this._categoryId = value;
          }
      }

      private object _categoryName;

      /// <summary>
      /// Gets or sets CategoryName.
      /// </summary>
      public object CategoryName
      {
          get
          {
              return this._categoryName;
          }

          set
          {
              this._categoryName = value;
          }
      }

      private object _categoryImage;

      /// <summary>
      /// Gets or sets CategoryImage.
      /// </summary>
      public object CategoryImage
      {
          get
          {
              return this._categoryImage;
          }

          set
          {
              this._categoryImage = value;
          }
      }

      private object _sortOrder;

      /// <summary>
      /// Gets or sets SortOrder.
      /// </summary>
      public object SortOrder
      {
          get
          {
              return this._sortOrder;
;
          }

          set
          {
              this._sortOrder = value;
          }
      }


      private object _adjacentCategoryId;

      /// <summary>
      /// Gets or sets AdjacentCategoryId.
      /// </summary>
      public object AdjacentCategoryId
      {
          get
          {
              return this._adjacentCategoryId;
              ;
          }

          set
          {
              this._adjacentCategoryId = value;
          }
      }

      private object _adjacentCategoryMode;

      /// <summary>
      /// Gets or sets AdjacentCategoryMode.
      /// </summary>
      public object AdjacentCategoryMode
      {
          get
          {
              return this._adjacentCategoryMode;
              ;
          }

          set
          {
              this._adjacentCategoryMode = value;
          }
      }

      private int? _mid;

      /// <summary>
      /// Gets or sets Mid.
      /// </summary>
      public int? Mid
      {
          get
          {
              return this._mid;
              ;
          }

          set
          {
              this._mid = value;
          }
      }

      private object _canHavePersonalForums;

      /// <summary>
      /// Gets or sets CanHavePersonalForums.
      /// </summary>
      public object CanHavePersonalForums
      {
          get
          {
              return this._canHavePersonalForums;
              ;
          }

          set
          {
              this._canHavePersonalForums = value;
          }
      }

      private  static long _categoryOut;
      /// <summary>
      /// Gets or sets CategoryOut.
      /// </summary>
      public static long CategoryOut
      {
          get
          {
              return _categoryOut;
          }

          set
          {
              _categoryOut = value;
          }
      }

   

    /// <summary>
    /// The _task name.
    /// </summary>
    private const string _taskName = "CategorySaveTask";

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
    /// The Blocking Task Names.
    /// </summary>
    private static readonly string[] BlockingTaskNames = Constants.ForumRebuild.BlockingTaskNames;
    

    /// <summary>
    /// Initializes a new instance of the <see cref="CategorySaveTask"/> class.
    /// </summary>
    public CategorySaveTask()
    {
    }

      /// <summary>
      ///  The start.
      ///  </summary>
      /// <param name="boardId"> The board Id.</param>
      /// <param name="categoryId"> The category Id.</param>
      /// <param name="categoryName"> The category Name.</param>
      /// <param name="categoryImage"> The category Image.</param>
      /// <param name="sortOrder"> The category sort order.</param>
      /// <param name="failureMessage"> The failure message to return.</param>
      /// <returns>
      ///  The start.
      ///  </returns>
    public static void Start(int? mid, object boardId, object categoryId, object categoryName, object categoryImage, object sortOrder, object canHavePersonalForums, object adjacentCategoryId, object adjacentCategoryMode, out string failureMessage)
      {

      failureMessage = String.Empty;
      if (YafContext.Current.Get<ITaskModuleManager>() == null)
      {
        return;
      }
     
      if (!YafContext.Current.Get<ITaskModuleManager>().AreTasksRunning(BlockingTaskNames))
      {
          YafContext.Current.Get<ITaskModuleManager>().StartTask(
              TaskName, () => new CategorySaveTask
                                  {
                                      Mid = mid,
                                      BoardIdToSave = boardId,
                                      CategoryId = categoryId,
                                      CategoryName = categoryName,
                                      CategoryImage = categoryImage,
                                      SortOrder = sortOrder,
                                      CanHavePersonalForums = canHavePersonalForums,
                                      AdjacentCategoryId = adjacentCategoryId,
                                      AdjacentCategoryMode = adjacentCategoryMode
                                  });
      }
      else
      {
          failureMessage = "You can't save the category while some of the blocking {0} tasks are running.".FormatWith(BlockingTaskNames.ToDelimitedString(","));
         
      }
    }

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
        try
        {
            this.Logger.Info("Starting Category Save Task for CategoryID {0}.",this.CategoryId);
            CommonDb.category_save(this.Mid, this.BoardIdToSave, this.CategoryId, this.CategoryName, this.CategoryImage, this.SortOrder, this.CanHavePersonalForums, this.AdjacentCategoryId, this.AdjacentCategoryMode); 
            this.Logger.Info("Category Save Task for CategoryID {0} is completed.", CategoryId);
        }
        catch (Exception x)
        {
            this.Logger.Error(x, "Error In Category Save Task: {0}", x);
        }
    }
  }
}