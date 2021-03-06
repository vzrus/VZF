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
 * File SyntaxHighlightedCodeRegexReplaceRule.cs created  on 2.6.2015 in  6:29 AM.
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
    using System.Text.RegularExpressions;

    using YAF.Types.Interfaces;

    /// <summary>
    /// Syntax Highlighted code block regular express replace
    /// </summary>
    public class SyntaxHighlightedCodeRegexReplaceRule : SimpleRegexReplaceRule
    {
        #region Constants and Fields

        /// <summary>
        ///   The _syntax highlighter.
        /// </summary>
        private readonly HighLighter _syntaxHighlighter = new HighLighter();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxHighlightedCodeRegexReplaceRule"/> class.
        /// </summary>
        /// <param name="regExSearch">
        /// The reg ex search.
        /// </param>
        /// <param name="regExReplace">
        /// The reg ex replace.
        /// </param>
        public SyntaxHighlightedCodeRegexReplaceRule(Regex regExSearch, string regExReplace)
            : base(regExSearch, regExReplace)
        {
            this._syntaxHighlighter.ReplaceEnter = true;
            this.RuleRank = 1;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The replace.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="replacement">
        /// The replacement.
        /// </param>
        public override void Replace(ref string text, IReplaceBlocks replacement)
        {
            Match m = this._regExSearch.Match(text);
            while (m.Success)
            {
                string inner = this._syntaxHighlighter.ColorText(
                    this.GetInnerValue(m.Groups["inner"].Value), m.Groups["language"].Value);

                string replaceItem = this._regExReplace.Replace("${inner}", inner);

                // pulls the htmls into the replacement collection before it's inserted back into the main text
                int replaceIndex = replacement.Add(replaceItem);

                text = text.Substring(0, m.Groups[0].Index) + replacement.Get(replaceIndex)
                       + text.Substring(m.Groups[0].Index + m.Groups[0].Length);

                m = this._regExSearch.Match(text);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This just overrides how the inner value is handled
        /// </summary>
        /// <param name="innerValue">The inner value.</param>
        /// <returns>
        /// The get inner value.
        /// </returns>
        protected override string GetInnerValue(string innerValue)
        {
            return innerValue;
        }

        #endregion
    }
}