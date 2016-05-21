
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
 * File ActiveLocation.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
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


namespace YAF.Types.Interfaces
{
  /// <summary>
  /// The i have localization extensions.
  /// </summary>
  public static class IHaveLocalizationExtensions
  {
    #region Public Methods

    /// <summary>
    /// Gets a text localization using the page and tag name.
    /// </summary>
    /// <param name="haveLocalization">
    /// The have localization.
    /// </param>
    /// <param name="page">
    /// The page.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The get text.
    /// </returns>
    public static string GetText(
      [NotNull] this IHaveLocalization haveLocalization, [NotNull] string page, [NotNull] string tag)
    {
      CodeContracts.ArgumentNotNull(haveLocalization, "haveLocalization");
      CodeContracts.ArgumentNotNull(page, "page");
      CodeContracts.ArgumentNotNull(tag, "tag");

      return haveLocalization.Localization.GetText(page, tag);
    }

    /// <summary>
    /// Gets a text localization.
    /// </summary>
    /// <param name="haveLocalization">
    /// The have localization.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The get text.
    /// </returns>
    public static string GetText([NotNull] this IHaveLocalization haveLocalization, [NotNull] string tag)
    {
      CodeContracts.ArgumentNotNull(haveLocalization, "haveLocalization");
      CodeContracts.ArgumentNotNull(tag, "tag");

      return haveLocalization.Localization.GetText(tag);
    }

    /// <summary>
    /// Gets a text localization using formatting.
    /// </summary>
    /// <param name="haveLocalization">
    /// The have localization.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The get text formatted.
    /// </returns>
    public static string GetTextFormatted(
      [NotNull] this IHaveLocalization haveLocalization, [NotNull] string tag, [CanBeNull] params object[] args)
    {
      CodeContracts.ArgumentNotNull(haveLocalization, "haveLocalization");
      CodeContracts.ArgumentNotNull(tag, "tag");

      return haveLocalization.Localization.GetTextFormatted(tag, args);
    }

    #endregion
  }
}