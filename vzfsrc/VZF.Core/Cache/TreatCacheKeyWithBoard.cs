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
 * File TreatCacheKeyWithBoard.cs created  on 2.6.2015 in  6:29 AM.
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

  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The treat cache key with board.
  /// </summary>
  public class TreatCacheKeyWithBoard : ITreatCacheKey
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TreatCacheKeyWithBoard"/> class.
    /// </summary>
    /// <param name="haveBoardId">
    /// The board id.
    /// </param>
    public TreatCacheKeyWithBoard([NotNull] IHaveBoardId haveBoardId)
    {
      CodeContracts.ArgumentNotNull(haveBoardId, "haveBoardId");

      this.HaveBoardId = haveBoardId;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets HaveBoardId.
    /// </summary>
    public IHaveBoardId HaveBoardId { get; set; }

    #endregion

    #region Implemented Interfaces

    #region ITreatCacheKey

    /// <summary>
    /// The treat.
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <returns>
    /// The treat.
    /// </returns>
    public string Treat(string key)
    {
      return "{0}${1}".FormatWith(key, this.HaveBoardId.BoardId);
    }

    #endregion

    #endregion
  }
}