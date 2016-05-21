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
 * File LocalizationSupportExtensions.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Controls
{
  using System.Web.UI;

  using YAF.Core;

  using YAF.Core.BBCode;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using YAF.Types;

  /// <summary>
  /// The localization support extensions.
  /// </summary>
  public static class LocalizationSupportExtensions
  {
    #region Public Methods

    /// <summary>
    /// The get current item.
    /// </summary>
    /// <param name="supportItem">
    /// The support Item.
    /// </param>
    /// <param name="currentControl">
    /// The current Control.
    /// </param>
    /// <returns>
    /// The get current item.
    /// </returns>
    public static string Localize([NotNull] this ILocalizationSupport supportItem, [NotNull] Control currentControl)
    {
      CodeContracts.ArgumentNotNull(supportItem, "supportItem");
      CodeContracts.ArgumentNotNull(currentControl, "currentControl");

      if (currentControl.Site != null && currentControl.Site.DesignMode == true)
      {
        return "[PAGE:{0}|TAG:{1}]".FormatWith(supportItem.LocalizedPage, supportItem.LocalizedTag);
      }
      else if (supportItem.LocalizedPage.IsSet() && supportItem.LocalizedTag.IsSet())
      {
        return YafContext.Current.Get<ILocalization>().GetText(supportItem.LocalizedPage, supportItem.LocalizedTag);
      }
      else if (supportItem.LocalizedTag.IsSet())
      {
        return YafContext.Current.Get<ILocalization>().GetText(supportItem.LocalizedTag);
      }

      return null;
    }

    /// <summary>
    /// The localize and render.
    /// </summary>
    /// <param name="supportedItem">
    /// The supported item.
    /// </param>
    /// <param name="currentControl">
    /// The current control.
    /// </param>
    /// <returns>
    /// The localize and render.
    /// </returns>
    public static string LocalizeAndRender([NotNull] this ILocalizationSupport supportedItem, [NotNull] BaseControl currentControl)
    {
      CodeContracts.ArgumentNotNull(supportedItem, "supportedItem");
      CodeContracts.ArgumentNotNull(currentControl, "currentControl");

      string localizedItem = supportedItem.Localize(currentControl);

      // convert from YafBBCode to HTML
      if (supportedItem.EnableBBCode)
      {
        localizedItem = currentControl.Get<IBBCode>().MakeHtml(localizedItem, true, false);
      }

      return localizedItem.FormatWith(supportedItem.Param0, supportedItem.Param1, supportedItem.Param2);
    }

    #endregion
  }
}