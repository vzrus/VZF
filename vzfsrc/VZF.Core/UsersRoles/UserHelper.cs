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
 * File UserHelper.cs created  on 17.12.2015 in  5:14 PM.
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
    using System.Data;
    using System.Linq;

    using YAF.Classes;
    using YAF.Types.Interfaces;

    /// <summary>
    /// The user helper.
    /// </summary>
    public static class UserHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets the user language file.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// language file name. If null -- use default language
        /// </returns>
        public static string GetUserLanguageFile(int userId)
        {
            // get the user information...
            DataRow row = UserMembershipHelper.GetUserRowForID(userId);

            if (row != null && row["LanguageFile"] != DBNull.Value
                && YafContext.Current.Get<YafBoardSettings>().AllowUserLanguage)
            {
                return row["LanguageFile"].ToString();
            }

            return null;
        }

        /// <summary>
        /// Gets the user theme file.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns User theme</returns>
        public static string GetUserThemeFile(int userId)
        {
            DataRow row = UserMembershipHelper.GetUserRowForID(userId);

            string themeFile = row["ThemeFile"] != DBNull.Value && YafContext.Current.Get<YafBoardSettings>().AllowUserTheme
                                   ? row["ThemeFile"].ToString()
                                   : YafContext.Current.Get<YafBoardSettings>().Theme;

            if (!YafTheme.IsValidTheme(themeFile))
            {
                themeFile = StaticDataHelper.Themes().First().FileName;
            }

            return themeFile;
        }

        #endregion
    }
}