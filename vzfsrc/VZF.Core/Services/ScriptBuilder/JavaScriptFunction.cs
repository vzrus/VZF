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
 * File JavaScriptFunction.cs created  on 2.6.2015 in  6:29 AM.
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
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  /// <summary>
  /// The js script class.
  /// </summary>
  public class JavaScriptFunction : IScriptFunction
  {
    #region Constants and Fields

    /// <summary>
    /// The _inner builder.
    /// </summary>
    private readonly IScriptStatement _inner;

    /// <summary>
    /// The _function parameters.
    /// </summary>
    private IList<object> _params = new List<object>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="JavaScriptFunction"/> class.
    /// </summary>
    /// <param name="inner">
    /// The inner builder.
    /// </param>
    public JavaScriptFunction()
    {
      this._inner = new JavaScriptStatement();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets Params.
    /// </summary>
    public IList<object> Params
    {
      get
      {
        return this._params;
      }

      set
      {
        this._params = value;
      }
    }

    #endregion

    #region Implementation of IScriptStatement

    /// <summary>
    /// Add to the script
    /// </summary>
    /// <param name="js">
    /// The js.
    /// </param>
    /// <returns>
    /// </returns>
    public void Add(string js)
    {
      this._inner.Add(js);
    }

    /// <summary>
    /// Get the script's result as String
    /// </summary>
    /// <returns>
    /// The Completed Script
    /// </returns>
    public string Build(IScriptBuilder scriptBuilder)
    {
      var builder = new StringBuilder();

      builder.Append("function");

      if (this.Name.IsSet())
      {
        builder.AppendFormat(" {0}", this.Name);
      }

      builder.Append("(");

      if (this.Params.Any())
      {
        this.Params.ForEachFirst(
          (param, isFirst) =>
          {
            if (!isFirst)
            {
              builder.Append(", ");
            }

            builder.Append(param);
          });
      }

      builder.Append(")");
      builder.AppendFormat("{{ {0} }}", this._inner.Build(scriptBuilder));

      return builder.ToString();
    }

    /// <summary>
    /// Clears the entire statment.
    /// </summary>
    public void Clear()
    {
      this._inner.Clear();
    }

    #endregion
  }
}