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
 * File YafUserIgnored.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Services
{
  #region Using

  using System.Collections.Generic;
  using System.Web;

  using VZF.Data.Common;

  
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// User Ignored Service for the current user.
  /// </summary>
  public class YafUserIgnored : IUserIgnored
  {
    #region Constants and Fields

    /// <summary>
    /// The _db broker.
    /// </summary>
    private readonly IDBBroker _dbBroker;

    /// <summary>
    ///   The _user ignore list.
    /// </summary>
    private List<int> _userIgnoreList;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="YafUserIgnored"/> class.
    /// </summary>
    /// <param name="sessionStateBase">
    /// The session state base.
    /// </param>
    /// <param name="dbBroker">
    /// The db broker.
    /// </param>
    public YafUserIgnored([NotNull] HttpSessionStateBase sessionStateBase, [NotNull] IDBBroker dbBroker)
    {
      this.SessionStateBase = sessionStateBase;
      this._dbBroker = dbBroker;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets SessionStateBase.
    /// </summary>
    public HttpSessionStateBase SessionStateBase { get; set; }

    #endregion

    #region Implemented Interfaces

    #region IUserIgnored

    /// <summary>
    /// The add ignored.
    /// </summary>
    /// <param name="ignoredUserId">
    /// The ignored user id.
    /// </param>
    public void AddIgnored(int ignoredUserId)
    {
        CommonDb.user_addignoreduser(YafContext.Current.PageModuleID, YafContext.Current.PageUserID, ignoredUserId);
      this.ClearIgnoreCache();
    }

    /// <summary>
    /// The clear ignore cache.
    /// </summary>
    public void ClearIgnoreCache()
    {
      // clear for the session
      this.SessionStateBase.Remove(Constants.Cache.UserIgnoreList.FormatWith(YafContext.Current.PageUserID));
    }

    /// <summary>
    /// The is ignored.
    /// </summary>
    /// <param name="ignoredUserId">
    /// The ignored user id.
    /// </param>
    /// <returns>
    /// The is ignored.
    /// </returns>
    public bool IsIgnored(int ignoredUserId)
    {
      if (this._userIgnoreList == null)
      {
        this._userIgnoreList = this._dbBroker.UserIgnoredList(YafContext.Current.PageUserID);
      }

      if (this._userIgnoreList.Count > 0)
      {
        return this._userIgnoreList.Contains(ignoredUserId);
      }

      return false;
    }

    /// <summary>
    /// The remove ignored.
    /// </summary>
    /// <param name="ignoredUserId">
    /// The ignored user id.
    /// </param>
    public void RemoveIgnored(int ignoredUserId)
    {
        CommonDb.user_removeignoreduser(YafContext.Current.PageModuleID, YafContext.Current.PageUserID, ignoredUserId);
      this.ClearIgnoreCache();
    }

    #endregion

    #endregion
  }
}