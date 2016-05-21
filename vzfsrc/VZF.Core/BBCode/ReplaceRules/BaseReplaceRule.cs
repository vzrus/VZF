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
 * File BaseReplaceRule.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.BBCode.ReplaceRules
{
  using System;

  using YAF.Types.Interfaces;

  /// <summary>
  /// Base class for all replacement rules.
  ///   Provides compare functionality based on the rule rank.
  ///   Override replace to handle replacement differently.
  /// </summary>
  public abstract class BaseReplaceRule : IComparable, IReplaceRule
  {
    #region Constants and Fields

    /// <summary>
    ///   The rule rank.
    /// </summary>
    public int RuleRank = 50;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets RuleDescription.
    /// </summary>
    public virtual string RuleDescription
    {
      get
      {
        return string.Empty;
      }
    }

    #endregion

    #region Implemented Interfaces

    #region IBaseReplaceRule

    /// <summary>
    /// The replace.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="replacement">
    /// The replacement.
    /// </param>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public abstract void Replace(ref string text, IReplaceBlocks replacement);

    #endregion

    #region IComparable

    /// <summary>
    /// The compare to.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The compare to.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public int CompareTo(object obj)
    {
      if (obj is BaseReplaceRule)
      {
        var otherRule = obj as BaseReplaceRule;

        if (this.RuleRank > otherRule.RuleRank)
        {
          return 1;
        }
        else if (this.RuleRank < otherRule.RuleRank)
        {
          return -1;
        }

        return 0;
      }
      else
      {
        throw new ArgumentException("Object is not of type BaseReplaceRule.");
      }
    }

    #endregion

    #endregion
  }
}