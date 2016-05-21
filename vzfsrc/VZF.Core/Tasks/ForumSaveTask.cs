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
 * File ForumSaveTask.cs created  on 2.6.2015 in  6:29 AM.
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
  using VZF.Utils.Helpers.StringUtils;

  /// <summary>
  /// Run when we want to do migration of users in the background...
  /// </summary>
    public class ForumSaveTask : LongBackgroundTask, ICriticalBackgroundTask
  {
      /// <summary>
      /// The _adjacent Forum Id.
      /// </summary>
      private object _adjacentForumId;

      /// <summary>
      /// Gets or sets Adjacent Forum Id.
      /// </summary>
      public object AdjacentForumId
      {
          get
          {
              return this._adjacentForumId;
          }

          set
          {
              this._adjacentForumId = value;
          }
      }

      /// <summary>
      /// The _forum id.
      /// </summary>
      private object _adjacentForumPosition;

      /// <summary>
      /// Gets or sets AdjacentForumPosition.
      /// </summary>
      public object AdjacentForumPosition
      {
          get
          {
              return this._adjacentForumPosition;
          }

          set
          {
              this._adjacentForumPosition = value;
          }
      }

      /// <summary>
      /// The _forum id.
      /// </summary>
      private object _forumId;

      /// <summary>
      /// Gets or sets ForumId.
      /// </summary>
      public object ForumId
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

      private object _parentID;

      /// <summary>
      /// Gets or sets ParentId.
      /// </summary>
      public object ParentId
      {
          get
          {
              return this._parentID;
          }

          set
          {
              this._parentID = value;
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
          }

          set
          {
              this._mid = value;
          }
      }
      private object _name;

      /// <summary>
      /// Gets or sets Name.
      /// </summary>
      public object Name
      {
          get
          {
              return this._name;
          }

          set
          {
              this._name = value;
          }
      }

      private object _description;

      /// <summary>
      /// Gets or sets Description.
      /// </summary>
      public object Description
      {
          get
          {
              return this._description;
;
          }

          set
          {
              this._description = value;
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

      private object _locked;

      /// <summary>
      /// Gets or sets Locked.
      /// </summary>
      public object Locked
      {
          get
          {
              return this._locked;
              ;
          }

          set
          {
              this._locked = value;
          }
      }

      private object _hidden;

      /// <summary>
      /// Gets or sets Hidden.
      /// </summary>
      public object Hidden
      {
          get
          {
              return this._hidden;
              ;
          }

          set
          {
              this._hidden = value;
          }
      }

      private object _isTest;

      /// <summary>
      /// Gets or sets IsTest.
      /// </summary>
      public object IsTest
      {
          get
          {
              return this._isTest;
              ;
          }

          set
          {
              this._isTest = value;
          }
      }

      private object _moderated;

      /// <summary>
      /// Gets or sets Moderated.
      /// </summary>
      public object Moderated
      {
          get
          {
              return this._moderated;
          }

          set
          {
              this._moderated = value;
          }
      }

      private object _accessMaskID;

      /// <summary>
      /// Gets or sets AccessMaskId.
      /// </summary>
      public object AccessMaskId
      {
          get
          {
              return this._accessMaskID;
          }

          set
          {
              this._accessMaskID = value;
          }
      }

      private object _remoteURL;
      /// <summary>
      /// Gets or sets RemoteURL.
      /// </summary>
      public object RemoteURL
      {
          get
          {
              return this._remoteURL;
          }

          set
          {
              this._remoteURL = value;
          }
      }
      private object _themeURL;
      /// <summary>
      /// Gets or sets ThemeURL.
      /// </summary>
      public object ThemeURL
      {
          get
          {
              return this._themeURL;
          }

          set
          {
              this._themeURL = value;
          }
      }

      private object _imageURL;
      /// <summary>
      /// Gets or sets ImageURL.
      /// </summary>
      public object ImageURL
      {
          get
          {
              return this._imageURL;
          }

          set
          {
              this._imageURL = value;
          }
      }

      private object _styles;
      /// <summary>
      /// Gets or sets Styles.
      /// </summary>
      public object Styles
      {
          get
          {
              return this._styles;
          }

          set
          {
              this._styles = value;
          }
      }

      private  static long _forumOut;
      /// <summary>
      /// Gets or sets ForumOut.
      /// </summary>
      public static long ForumOut
      {
          get
          {
              return _forumOut;
          }

          set
          {
              _forumOut = value;
          }
      }

      private object _userId;
      /// <summary>
      /// Gets or sets ForumOut.
      /// </summary>
      public object UserId
      {
          get
          {
              return _userId;
          }

          set
          {
              _userId = value;
          }
      }

      private bool _dummy;
      /// <summary>
      /// Gets or sets Dummy.
      /// </summary>
      public bool Dummy
      {
          get
          {
              return this._dummy;
          }

          set
          {
              this._dummy = value;
          }
      }

      private bool _canHavePersonalForum;
     
      /// <summary>
      /// Gets or sets CanHavePersonalForum.
      /// </summary>
      public bool CanHavePersonalForum
      {
        get
          {
              return this._canHavePersonalForum;
          }

          set
          {
              this._canHavePersonalForum = value; 
          }
      }

      private bool _isUserForum;
     
      /// <summary>
      /// Gets or sets IsUserForum.
      /// </summary>
      public bool IsUserForum
      {
        get
          {
              return this._isUserForum;
          }

          set
          {
              this._isUserForum = value; 
          }
      }
    /// <summary>
    /// The _task name.
    /// </summary>
    private const string _taskName = "ForumSaveTask";

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
      /// The start.
      /// </summary>
      /// <param name="forumId">
      /// The forum Id.
      ///  </param>
      /// <param name="categoryId">
      /// The category Id.
      ///  </param>
      /// <param name="parentId">
      /// The parent Id. 
      ///  </param>
      /// <param name="name">
      /// The forum name.
      ///  </param>
      /// <param name="description">
      /// The description.
      ///  </param>
      /// <param name="sortOrder">
      /// The sort Order.
      ///  </param>
      /// <param name="locked"> 
      ///  The locked. </param>
      /// <returns>
      /// The start.
      /// </returns>
    public static long Start(int? mid, object forumId, object categoryId, object parentId, object name, object description, object sortOrder, object locked, object hidden, object isTest, object moderated, object accessMaskID, object remoteURL, object themeURL, object imageURL, object styles,
          bool dummy, object userId, bool isUserForum, bool canHavePersonalForum, object adjacentForumId, object adjacentForumPosition, out string failureMessage)
      {

      failureMessage = String.Empty;
      if (YafContext.Current.Get<ITaskModuleManager>() == null)
      {
        return 0;
      }
      //  long newForumId = (int)forumId;
      if (!YafContext.Current.Get<ITaskModuleManager>().AreTasksRunning(BlockingTaskNames))
      {
          YafContext.Current.Get<ITaskModuleManager>().StartTask(
              TaskName, () => new ForumSaveTask
                                  {
                                      ForumId = forumId,
                                      CategoryId = categoryId,
                                      ParentId = parentId,
                                      Name = name,
                                      Description = description,
                                      SortOrder = sortOrder,
                                      Locked = locked,
                                      Hidden = hidden,
                                      IsTest = isTest,
                                      Moderated = moderated,
                                      AccessMaskId = accessMaskID,
                                      RemoteURL = remoteURL,
                                      ThemeURL = themeURL,
                                      ImageURL = imageURL,
                                      Styles = styles,
                                      Dummy = dummy,
                                      UserId = userId,
                                      IsUserForum = isUserForum,
                                      CanHavePersonalForum = canHavePersonalForum,
                                      AdjacentForumId = adjacentForumId,
                                      AdjacentForumPosition = adjacentForumPosition,
                                      Mid = mid
                                  });
      }
      else
      {
          failureMessage = "You can't delete forum while blocking {0} tasks are running.".FormatWith(BlockingTaskNames.ToDelimitedString(","));
          _forumOut = -1;
      }
      return ForumOut;
    }

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
    
            try
            {
                this.Logger.Info(
                    "Starting Forum Update||Add Task for ForumID {0}, {1} CategoryID, ParentID {2}.",
                    this.ForumId,
                    this.CategoryId,
                    this.ParentId);
                _forumOut = CommonDb.forum_save(
                    this.Mid,
                    this.ForumId,
                    this.CategoryId,
                    this.ParentId,
                    this.Name,
                    this.Description,
                    this.SortOrder, 
                    this.Locked,
                    this.Hidden, 
                    this.IsTest, 
                    this.Moderated,
                    this.AccessMaskId, 
                    this.RemoteURL, 
                    this.ThemeURL, 
                    this.ImageURL,
                    this.Styles,
                    this.Dummy,
                    this.UserId,
                    this.IsUserForum,
                    this.CanHavePersonalForum,
                    this.AdjacentForumId,
                    this.AdjacentForumPosition);
                this.Logger.Info("Forum Update||Add Task is completed. Handled forum {0}.", _forumOut);
            }
            catch (Exception x)
            {
                _forumOut = -1;
                this.Logger.Error(x, "Error In Forum Update||Add Task: {0}", x);
            }
    }
  }
}