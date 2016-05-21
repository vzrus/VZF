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
 * File StartupInitializeDb.cs created  on 2.6.2015 in  6:29 AM.
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

  using System.Web;

  using VZF.Data.Common;

  
  using VZF.Utils;
  using YAF.Core.Tasks;
  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The yaf initialize db.
  /// </summary>
  public class StartupInitializeDb : BaseStartupService, ICriticalStartupService
  {
    #region Properties

    /// <summary>
    ///   Gets InitVarName.
    /// </summary>
    [NotNull]
    protected override string InitVarName
    {
      get
      {
        return "YafInitializeDb_Init";
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The run service.
    /// </summary>
    /// <returns>
    /// The run service.
    /// </returns>
    protected override bool RunService()
    {
      // init the db...
      string errorStr = string.Empty;
      bool debugging = false;

#if DEBUG
      debugging = true;
#endif

      if (HttpContext.Current != null)
      {
        // attempt to init the db...
        if (!CommonDb.forumpage_initdb(YafContext.Current.PageModuleID, out errorStr, debugging))
        {
          // unable to connect to the DB...
          YafContext.Current.Get<HttpSessionStateBase>()["StartupException"] = errorStr;
          YafContext.Current.Get<HttpResponseBase>().Redirect(YafForumInfo.ForumClientFileRoot + "error.aspx");
        }

        // step 2: validate the database version...
        string redirectStr = CommonDb.forumpage_validateversion(YafContext.Current.PageModuleID, YafForumInfo.AppVersion);
        if (redirectStr.IsSet())
        {
          YafContext.Current.Get<HttpResponseBase>().Redirect(YafForumInfo.ForumClientFileRoot + redirectStr);
        }
      }

      return true;
    }

    #endregion
  }
}