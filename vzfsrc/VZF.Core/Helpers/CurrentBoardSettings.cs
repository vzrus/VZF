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
 * File CurrentBoardSettings.cs created  on 2.6.2015 in  6:29 AM.
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
  #region Using

  using System.Web;

  using YAF.Classes;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The current board settings.
  /// </summary>
  public class CurrentBoardSettings : IReadWriteProvider<YafBoardSettings>
  {
    #region Constants and Fields

    /// <summary>
    ///   The _application state base.
    /// </summary>
    private readonly HttpApplicationStateBase _applicationStateBase;

    /// <summary>
    ///   The _have board id.
    /// </summary>
    private readonly IHaveBoardId _haveBoardId;

    /// <summary>
    ///   The _inject services.
    /// </summary>
    private readonly IInjectServices _injectServices;

    /// <summary>
    /// The _treat cache key.
    /// </summary>
    private readonly ITreatCacheKey _treatCacheKey;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentBoardSettings"/> class.
    /// </summary>
    /// <param name="applicationStateBase">
    /// The application state base.
    /// </param>
    /// <param name="injectServices">
    /// The inject services.
    /// </param>
    /// <param name="haveBoardId">
    /// The have board id.
    /// </param>
    /// <param name="treatCacheKey">
    /// </param>
    public CurrentBoardSettings(
      [NotNull] HttpApplicationStateBase applicationStateBase, 
      [NotNull] IInjectServices injectServices, 
      [NotNull] IHaveBoardId haveBoardId, 
      [NotNull] ITreatCacheKey treatCacheKey)
    {
      CodeContracts.ArgumentNotNull(applicationStateBase, "applicationStateBase");
      CodeContracts.ArgumentNotNull(injectServices, "injectServices");
      CodeContracts.ArgumentNotNull(haveBoardId, "haveBoardId");
      CodeContracts.ArgumentNotNull(treatCacheKey, "treatCacheKey");

      this._applicationStateBase = applicationStateBase;
      this._injectServices = injectServices;
      this._haveBoardId = haveBoardId;
      this._treatCacheKey = treatCacheKey;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets Object.
    /// </summary>
    public YafBoardSettings Instance
    {
      get
      {
        return this._applicationStateBase.GetOrSet(
          this._treatCacheKey.Treat(Constants.Cache.BoardSettings), 
          () =>
            {
              var boardSettings = new YafLoadBoardSettings(this._haveBoardId.BoardId);

              // inject
              this._injectServices.Inject(boardSettings);

              return boardSettings;
            });
      }

      set
      {
        this._applicationStateBase.Set(this._treatCacheKey.Treat(Constants.Cache.BoardSettings), value);
      }
    }

    #endregion
  }
}