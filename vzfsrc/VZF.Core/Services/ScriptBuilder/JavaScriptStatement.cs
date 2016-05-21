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
 * File JavaScriptStatement.cs created  on 2.6.2015 in  6:29 AM.
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

  using System;
  using System.Text;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// Java Script Statement Builder
  /// </summary>
  public class JavaScriptStatement : IScriptStatement
  {
    #region Constants and Fields

    /// <summary>
    ///   The String builder used to build the script.
    /// </summary>
    protected StringBuilder builder = new StringBuilder();

    #endregion

    #region Implemented Interfaces

    #region IScriptStatement

    /// <summary>
    /// Append to the script
    /// </summary>
    /// <param name="js">
    /// The js.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public void Add(string js)
    {
      CodeContracts.ArgumentNotNull(js, "js");

      this.builder.Append(js);
    }

    /// <summary>
    /// Get the script's result as String
    /// </summary>
    /// <param name="scriptScriptBuilder">
    /// The script Builder.
    /// </param>
    /// <returns>
    /// The Complete Script
    /// </returns>
    [NotNull]
    public string Build([NotNull] IScriptBuilder scriptScriptBuilder)
    {
      return this.builder.ToString().Replace("$$$", scriptScriptBuilder.ScriptSelector);
    }

    /// <summary>
    /// Clears the entire script.
    /// </summary>
    public void Clear()
    {
      this.builder = new StringBuilder();
    }

    #endregion

    #endregion
  }
}