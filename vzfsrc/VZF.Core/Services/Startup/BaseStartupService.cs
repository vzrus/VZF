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
 * File BaseStartupService.cs created  on 2.6.2015 in  6:29 AM.
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

  using System;

  using YAF.Types.Constants;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The root service.
  /// </summary>
  public abstract class BaseStartupService : IStartupService
  {
    #region Properties

    /// <summary>
    ///   Gets a value indicating whether Initialized.
    /// </summary>
    public virtual bool Initialized
    {
      get
      {
        if (YafContext.Current[this.InitVarName] == null)
        {
          return false;
        }

        return Convert.ToBoolean(YafContext.Current[this.InitVarName]);
      }

      private set
      {
        YafContext.Current[this.InitVarName] = value;
      }
    }

    /// <summary>
    ///   Gets Priority.
    /// </summary>
    public virtual int Priority
    {
      get
      {
        return 1000;
      }
    }

    /// <summary>
    ///   Gets InitVarName.
    /// </summary>
    protected abstract string InitVarName { get; }

    #endregion

    #region Implemented Interfaces

    #region IStartupService

    /// <summary>
    /// The run.
    /// </summary>
    public void Run()
    {
      if (!this.Initialized)
      {
        this.Initialized = this.RunService();
      }
    }

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// The run service.
    /// </summary>
    /// <returns>
    /// The run service.
    /// </returns>
    protected abstract bool RunService();

    #endregion
  }
}