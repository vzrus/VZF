﻿#region copyright
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
 * File JavaScriptBuilder.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core
{
  #region Using

  using System.Collections.Generic;
  using System.Text;

  using YAF.Classes;
  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The java script builder.
  /// </summary>
  public class JavaScriptBuilder : IScriptBuilder
  {
    #region Constants and Fields

    /// <summary>
    /// The _statements.
    /// </summary>
    public List<IScriptStatement> _statements = new List<IScriptStatement>();

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the ScriptSelector.
    /// </summary>
    public string ScriptSelector
    {
      get
      {
        return Config.JQueryAlias;
      }
    }

    /// <summary>
    /// Gets Statements.
    /// </summary>
    public IList<IScriptStatement> Statements
    {
      get
      {
        return this._statements;
      }
    }

    #endregion

    #region Implemented Interfaces

    #region IScriptBuilder

    /// <summary>
    /// The build.
    /// </summary>
    /// <returns>
    /// The build.
    /// </returns>
    [NotNull]
    public string Build()
    {
      var built = new StringBuilder();

      foreach (var scriptStatement in this.Statements)
      {
        built.Append(scriptStatement.Build(this));
      }

      return built.ToString();
    }

    #endregion

    #endregion
  }
}