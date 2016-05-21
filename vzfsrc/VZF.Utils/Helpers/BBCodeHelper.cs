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
 * File BBCodeHelper.cs created  on 2.6.2015 in  6:31 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
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

namespace VZF.Utils.Helpers
{
  using System.Text.RegularExpressions;

  /// <summary>
  /// The bb code helper.
  /// </summary>
  public static class BBCodeHelper
  {
    /// <summary>
    /// The strip bb code.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <returns>
    /// The strip bb code.
    /// </returns>
    public static string StripBBCode(string text)
    {
      return Regex.Replace(text, @"\[(.|\n)*?\]", string.Empty);
    }

    /// <summary>
    /// Strip Quote BB Code Quotes including the quoted text
    /// </summary>
    /// <param name="text">Text to check
    /// </param>
    /// <returns>The Cleaned Text
    /// </returns>
    public static string StripBBCodeQuotes(string text)
    {
        return Regex.Replace(text, @"\[quote\b[^>]*](.|\n)*?\[/quote\]", string.Empty, RegexOptions.Multiline);
    }
  }
}/* Yet Another Forum.net
 * Copyright (C) 2006-2013 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Utils.Helpers
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The bb code helper.
    /// </summary>
    public static class BBCodeHelper
    {
        /// <summary>
        /// The strip bb code.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The strip bb code.
        /// </returns>
        public static string StripBBCode(string text)
        {
            return Regex.Replace(text, @"\[(.|\n)*?\]", string.Empty);
        }

        /// <summary>
        /// Strip Quote BB Code Quotes including the quoted text
        /// </summary>
        /// <param name="text">Text to check
        /// </param>
        /// <returns>The Cleaned Text
        /// </returns>
        public static string StripBBCodeQuotes(string text)
        {
            return Regex.Replace(text, @"\[quote[^\]]*](.|\n)*?\[/quote\]", string.Empty, RegexOptions.Multiline);
        }

        /// <summary>
        /// Strip BB Code Urls
        /// </summary>
        /// <param name="text">Text to check
        /// </param>
        /// <returns>The Cleaned Text
        /// </returns>
        public static string StripBBCodeUrls(string text)
        {
            return Regex.Replace(text, @"\[url[^\]]*](.|\n)*?\[/url\]", string.Empty, RegexOptions.Singleline);
        }
    }
}