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
 * File ScriptBuilderExtensions.cs created  on 2.6.2015 in  6:29 AM.
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

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// the script builder extensions.
  /// </summary>
  public static class ScriptBuilderExtensions
  {
    #region Public Methods

    /// <summary>
    /// Creates a function statement. AddFunction is you want the function statement inserted into the Builder.
    /// </summary>
    /// <param name="scriptBuilder">
    /// The script builder.
    /// </param>
    /// <param name="addFunction">
    /// The add function.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static IScriptFunctionContext CreateFunction([NotNull] this IScriptBuilder scriptBuilder, bool addFunction)
    {
      CodeContracts.ArgumentNotNull(scriptBuilder, "scriptBuilder");

      var newFunction = new ScriptFunctionContext(scriptBuilder, new JavaScriptFunction());

      if (addFunction)
      {
        scriptBuilder.Statements.Add(newFunction.ScriptStatement);
      }

      return newFunction;
    }

    /// <summary>
    /// Creates a function statement and adds it to the builder.
    /// </summary>
    /// <param name="scriptBuilder">
    /// The script builder.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static IScriptFunctionContext CreateFunction([NotNull] this IScriptBuilder scriptBuilder)
    {
      CodeContracts.ArgumentNotNull(scriptBuilder, "scriptBuilder");

      return scriptBuilder.CreateFunction(true);
    }

    /// <summary>
    /// Creates a statement and optionally adds it to the builder.
    /// </summary>
    /// <param name="scriptBuilder">
    /// The script builder.
    /// </param>
    /// <param name="addStatement">
    /// The add statement.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static IScriptStatementContext CreateStatement(
      [NotNull] this IScriptBuilder scriptBuilder, bool addStatement)
    {
      CodeContracts.ArgumentNotNull(scriptBuilder, "scriptBuilder");

      var newStatement = new ScriptStatementContext(scriptBuilder, new JavaScriptStatement());

      if (addStatement)
      {
        scriptBuilder.Statements.Add(newStatement.ScriptStatement);
      }

      return newStatement;
    }

    /// <summary>
    /// Creates a statement and adds it to the builder.
    /// </summary>
    /// <param name="scriptBuilder">
    /// The script builder.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    public static IScriptStatementContext CreateStatement([NotNull] this IScriptBuilder scriptBuilder)
    {
      CodeContracts.ArgumentNotNull(scriptBuilder, "scriptBuilder");

      return scriptBuilder.CreateStatement(true);
    }

    #endregion
  }
}