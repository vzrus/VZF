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
 * File PostDataHelper.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:04 PM.
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

namespace YAF.Core
{
    using System;
    using System.Data;
    using System.Web;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    /// <summary>
  /// The post data helper wrapper.
  /// </summary>
  public class PostDataHelperWrapper
  {
    /// <summary>
    /// The _forum flags.
    /// </summary>
    private ForumFlags _forumFlags;

    /// <summary>
    /// The _message flags.
    /// </summary>
    private MessageFlags _messageFlags;

    /// <summary>
    /// The current data row for this post.
    /// </summary>
    private DataRow _row;

    /// <summary>
    /// The _topic flags.
    /// </summary>
    private TopicFlags _topicFlags;

    /// <summary>
    /// The _user profile.
    /// </summary>
    private YafUserProfile _userProfile;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostDataHelperWrapper"/> class.
    /// </summary>
    public PostDataHelperWrapper()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PostDataHelperWrapper"/> class.
    /// </summary>
    /// <param name="dataRow">
    /// The data row.
    /// </param>
    public PostDataHelperWrapper(DataRow dataRow)
      : this()
    {
      DataRow = dataRow;
    }

    /// <summary>
    /// Gets or sets DataRow.
    /// </summary>
    public DataRow DataRow
    {
      get
      {
        return this._row;
      }

      set
      {
        this._row = value;

        // get all flags for forum, topic and message
        if (this._row != null)
        {
          this._forumFlags = new ForumFlags(this._row["ForumFlags"]);
          this._topicFlags = new TopicFlags(this._row["TopicFlags"]);
          this._messageFlags = new MessageFlags(this._row["Flags"]);
        }
        else
        {
          this._forumFlags = new ForumFlags(0);
          this._topicFlags = new TopicFlags(0);
          this._messageFlags = new MessageFlags(0);
        }
      }
    }


    /// <summary>
    /// Gets UserProfile.
    /// </summary>
    public YafUserProfile UserProfile
    {
      get
      {
        if (this._userProfile == null)
        {
          // setup instance of the user profile...
          if (DataRow != null)
          {
            this._userProfile = YafUserProfile.GetProfile(UserMembershipHelper.GetUserNameFromID(UserId));
          }
        }

        return this._userProfile;
      }
    }

    /// <summary>
    /// Gets UserId.
    /// </summary>
    public int UserId
    {
      get
      {
        if (DataRow != null)
        {
          return Convert.ToInt32(DataRow["UserID"]);
        }

        return 0;
      }
    }

    /// <summary>
    /// Gets MessageId.
    /// </summary>
    public int MessageId
    {
      get
      {
        if (DataRow != null)
        {
          return Convert.ToInt32(DataRow["MessageID"]);
        }

        return 0;
      }
    }

    /// <summary>
    /// IsLocked flag should only be used for "ghost" posts such as the
    /// Sponser post that isn't really there.
    /// </summary>
    public bool IsLocked
    {
      get
      {
        if (this._messageFlags != null)
        {
          return this._messageFlags.IsLocked;
        }

        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether IsSponserMessage.
    /// </summary>
    public bool IsSponserMessage
    {
      get
      {
        return DataRow["IP"].ToString() == "none";
      }
    }

        /// <summary>
        /// Gets a value indicating whether is first message.
        /// </summary>
        public bool IsFirstMessage
    {
        get
        {
            return DataRow["Position"].ToType<int>() == 0;
        }
    }

    /// <summary>
    /// Gets a value indicating whether CanThankPost.
    /// </summary>
    public bool CanThankPost
    {
      get
      {
        return (int) DataRow["UserID"] != YafContext.Current.PageUserID;
      }
    }


    /// <summary>
    /// Gets a value indicating whether CanEditPost.
    /// </summary>
    public bool CanEditPost
    {
      get
      {
        // Ederon : 9/9/2007 - moderaotrs can edit locked posts
        // Ederon : 12/5/2007 - new flags implementation
          return ((!this.PostLocked && !this._forumFlags.IsLocked && !this._topicFlags.IsLocked && (((this.UserId == YafContext.Current.PageUserID) && !DataRow["IsGuest"].ToType<bool>()) || (DataRow["IsGuest"].ToType<bool>() && (DataRow["IP"].ToString() == YafContext.Current.Get<HttpRequestBase>().GetUserRealIPAddress())))) ||
                YafContext.Current.ForumModeratorAccess) && YafContext.Current.ForumEditAccess;
      }
    }

    /// <summary>
    /// Gets a value indicating whether PostLocked.
    /// </summary>
    public bool PostLocked
    {
      get
      {
        // post is explicitly locked
        if (this._messageFlags.IsLocked)
        {
          return true;
        }

        // there is auto-lock period defined
        if (!YafContext.Current.IsAdmin && YafContext.Current.Get<YafBoardSettings>().LockPosts > 0)
        {
          var edited = (DateTime)DataRow["Edited"];

          // check if post is locked according to this rule
          if (edited.AddDays(YafContext.Current.Get<YafBoardSettings>().LockPosts) < DateTime.UtcNow)
          {
            return true;
          }
        }

        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether PostDeleted.
    /// </summary>
    public bool PostDeleted
    {
      get
      {
        return this._messageFlags.IsDeleted;
      }
    }

    /// <summary>
    /// Gets a value indicating whether CanAttach.
    /// </summary>
    public bool CanAttach
    {
      get
      {
        // Ederon : 9/9/2007 - moderaotrs can attack to locked posts
        return ((!this.PostLocked && !this._forumFlags.IsLocked && !this._topicFlags.IsLocked && UserId == YafContext.Current.PageUserID) ||
                YafContext.Current.ForumModeratorAccess) && YafContext.Current.ForumUploadAccess;
      }
    }

    /// <summary>
    /// Gets a value indicating whether CanDeletePost.
    /// </summary>
    public bool CanDeletePost
    {
      get
      {
        // Ederon : 9/9/2007 - moderators can delete in locked posts
        // vzrus : only guests with the same IP can delete guest posts 
          return ((!this.PostLocked && !this._forumFlags.IsLocked && !this._topicFlags.IsLocked && (((this.UserId == YafContext.Current.PageUserID) && !DataRow["IsGuest"].ToType<bool>()) || (DataRow["IsGuest"].ToType<bool>() && (DataRow["IP"].ToString() == YafContext.Current.Get<HttpRequestBase>().GetUserRealIPAddress())))) ||
              YafContext.Current.ForumModeratorAccess) && YafContext.Current.ForumDeleteAccess
 
              // first post can't be deleted
              && (int)DataRow["Position"] > 0;
      }
    }

    /// <summary>
    /// Gets a value indicating whether CanUnDeletePost.
    /// </summary>
    public bool CanUnDeletePost
    {
      get
      {
        return PostDeleted && CanDeletePost;
      }
    }

    /// <summary>
    /// Gets a value indicating whether CanReply.
    /// </summary>
    public bool CanReply
    {
      get
      {
        // Ederon : 9/9/2007 - moderaotrs can reply in locked posts
        return ((!this._messageFlags.IsLocked && !this._forumFlags.IsLocked && !this._topicFlags.IsLocked) || YafContext.Current.ForumModeratorAccess) &&
               YafContext.Current.ForumReplyAccess;
      }
    }
  }
}