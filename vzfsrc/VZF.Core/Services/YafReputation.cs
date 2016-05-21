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
 * File YafReputation.cs created  on 2.6.2015 in  6:29 AM.
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

namespace YAF.Core.Services
{
    using System;
    using System.Globalization;

    using VZF.Utils;
    using VZF.Utils.Helpers;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Interfaces;

    /// <summary>
    /// Class to Generate The Reputation Bar
    /// </summary>
    public class YafReputation
    {
        /// <summary>
        /// Checks if allow reputation voting.
        /// </summary>
        /// <param name="voteDateToCheck">The last vote date to check.</param>
        /// <returns>
        /// Returns if the Users is allowed to Vote
        /// </returns>
        public static bool CheckIfAllowReputationVoting(object voteDateToCheck)
        {
            if (voteDateToCheck.IsNullOrEmptyDBField())
            {
                return true;
            }

            var reputationVoteDate = voteDateToCheck.ToType<DateTime>();

            return reputationVoteDate < DateTime.UtcNow.AddHours(-24);
        }

        /// <summary>
        /// Generate The Reputation Bar for the user
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// Returns the Html String
        /// </returns>
        public static string GenerateReputationBar([NotNull]int points, [NotNull]int userId)
        {
            var formatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            float percentage = ConvertPointsToPercentage(points);

            var pointsSign = string.Empty;

            if (points > 0)
            {
                pointsSign = "+";
            }
            else if (points < 0)
            {
                pointsSign = "-";
            }
            var repText = GetReputationBarText(percentage);
            return // <span>{3}{4}</span>
                "<div class=\"ReputationBar ReputationUser_{2}\" data-percent=\"{0}\" data-text=\"{1}\" title=\"{3}{4}\"></div>".FormatWith(
                        percentage.ToString(formatInfo), repText, userId, pointsSign, points);
        }

        /// <summary>
        /// Gets the reputation bar text.
        /// </summary>
        /// <param name="percentage">The percentage.</param>
        /// <returns>Returns the Text for the Current Value</returns>
        [NotNull]
        public static string GetReputationBarText([NotNull]float percentage)
        {
            string text;

            if (percentage.Equals(0))
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "HATED");
            }
            else if (percentage < 20)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "HOSTILE");
            }
            else if (percentage < 30)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "HOSTILE");
            }
            else if (percentage < 40)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "HOSTILE");
            }
            else if (percentage < 50)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "UNFRIENDLY");
            }
            else if (percentage < 60)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "NEUTRAL");
            }
            else if (percentage < 80)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "FRIENDLY");
            }
            else if (percentage < 90)
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "HONORED");
            }
            else
            {
                text = YafContext.Current.Get<ILocalization>().GetText("REPUTATION_VALUES", "EXALTED");
            }

            return text;
        }

        /// <summary>
        /// Converts the points to percentage.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>Returns the Percentage Value</returns>
        [NotNull]
        public static float ConvertPointsToPercentage([NotNull]int points)
        {
            int percantage = points;

            int minValue = YafContext.Current.Get<YafBoardSettings>().ReputationMaxNegative;

            int maxValue = YafContext.Current.Get<YafBoardSettings>().ReputationMaxPositive;

            if (!YafContext.Current.Get<YafBoardSettings>().ReputationAllowNegative)
            {
                minValue = 0;
            }

            int testValue = minValue + maxValue;

            if (percantage.Equals(0) && YafContext.Current.Get<YafBoardSettings>().ReputationAllowNegative)
            {
                return 50;
            }

            if (percantage >= maxValue)
            {
                return 100;
            }

            if (percantage <= minValue)
            {
                return 0;
            }

            //// ((100 / (float)(maxValue * 2)) * percantage) + 50;

            var returnValue = ((100 / (float)testValue) * percantage) + 50;

            return returnValue > 100 ? 100 : returnValue;
        }
    }
}

