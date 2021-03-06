﻿#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SearchBuilder.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:19 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

namespace VZF.Data.MsSql.Search
{
  #region Using

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Types;
    using YAF.Types.Constants;

    #endregion

  #region Enums

  #endregion

  /// <summary>
  /// The search builder.
  /// </summary>
  public class SearchBuilder
  {
    #region Public Methods

    /// <summary>
    /// The build search sql.
    /// </summary>
    /// <param name="mid">
    /// The mid.
    /// </param>
    /// <param name="toSearchWhat">
    /// The to search what.
    /// </param>
    /// <param name="toSearchFromWho">
    /// The to search from who.
    /// </param>
    /// <param name="searchFromWhoMethod">
    /// The search from who method.
    /// </param>
    /// <param name="searchWhatMethod">
    /// The search what method.
    /// </param>
    /// <param name="userID">
    /// The user id.
    /// </param>
    /// <param name="searchDisplayName">
    /// The search display name.
    /// </param>
    /// <param name="boardId">
    /// The board id.
    /// </param>
    /// <param name="maxResults">
    /// The max results.
    /// </param>
    /// <param name="useFullText">
    /// The use full text.
    /// </param>
    /// <param name="ids">
    /// The ids.
    /// </param>
    /// <param name="forumIdToStartAt">
    /// The forum Id To Start At.
    /// </param>
    /// <returns>
    /// The build search sql.
    /// </returns>
    public string BuildSearchSql(
      int? mid,
      [NotNull] string toSearchWhat, 
      [NotNull] string toSearchFromWho, 
      SearchWhatFlags searchFromWhoMethod, 
      SearchWhatFlags searchWhatMethod, 
      int userID, 
      bool searchDisplayName, 
      int boardId, 
      int maxResults, 
      bool useFullText, 
      string ids,
      [NotNull] IEnumerable<int> forumIdToStartAt)
    {
      CodeContracts.ArgumentNotNull(toSearchWhat, "toSearchWhat");
      CodeContracts.ArgumentNotNull(toSearchFromWho, "toSearchFromWho");
      
      var builtStatements = new List<string>();

      if (maxResults > 0)
      {
        builtStatements.Add("SET ROWCOUNT {0};".FormatWith(maxResults));
      }

      string searchSql =
        "SELECT a.ForumID, a.TopicID, a.Topic, b.UserID, IsNull(c.Username, b.Name) as Name, c.MessageID, c.Posted, [Message] = '', c.Flags ";
      searchSql +=
        "\r\nfrom {databaseOwner}.{objectQualifier}topic a left join {databaseOwner}.{objectQualifier}message c on a.TopicID = c.TopicID left join {databaseOwner}.{objectQualifier}user b on c.UserID = b.UserID join {databaseOwner}.{objectQualifier}vaccess x on x.ForumID=a.ForumID ";
      searchSql +=
        "\r\nwhere x.ReadAccess<>0 AND x.UserID={0} AND c.IsApproved = 1 AND a.TopicMovedID IS NULL AND a.IsDeleted = 0 AND c.IsDeleted = 0"
          .FormatWith(userID);

      if (ids.IsSet())
      {
        searchSql += " AND a.ForumID IN ({0})".FormatWith(ids);
      }

      if (toSearchFromWho.IsSet())
      {
        searchSql +=
          "\r\nAND ({0})".FormatWith(
            this.BuildWhoConditions(toSearchFromWho, searchFromWhoMethod, searchDisplayName).BuildSql(true));
      }

      if (toSearchWhat.IsSet())
      {
        builtStatements.Add(searchSql);

        builtStatements.Add(
          "AND ({0})".FormatWith(
            this.BuildWhatConditions(toSearchWhat, searchWhatMethod, "c.Message", useFullText).BuildSql(true)));

        builtStatements.Add("UNION");

        builtStatements.Add(searchSql);

        builtStatements.Add(
          "AND ({0})".FormatWith(
            this.BuildWhatConditions(toSearchWhat, searchWhatMethod, "a.Topic", useFullText).BuildSql(true)));
      }
      else
      {
        builtStatements.Add(searchSql);
      }

      builtStatements.Add("ORDER BY c.Posted DESC");

      string builtSql = builtStatements.ToDelimitedString("\r\n");
      builtSql = builtSql.Replace(@"{databaseOwner}", Config.DatabaseOwner).Replace(@"{objectQualifier}", Config.DatabaseObjectQualifier); 
      Debug.WriteLine("Build Sql: [{0}]".FormatWith(builtSql));
      return builtSql; 
    }

    #endregion

    #region Methods

    /// <summary>
    /// The build search who conditions.
    /// </summary>
    /// <param name="toSearchWhat">
    /// The to Search What.
    /// </param>
    /// <param name="searchWhatMethod">
    /// The search What Method.
    /// </param>
    /// <param name="dbField">
    /// The db Field.
    /// </param>
    /// <param name="useFullText">
    /// The use Full Text.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    protected IEnumerable<SearchCondition> BuildWhatConditions(
      [NotNull] string toSearchWhat, SearchWhatFlags searchWhatMethod, [NotNull] string dbField, bool useFullText)
    {
      CodeContracts.ArgumentNotNull(toSearchWhat, "toSearchWhat");
      CodeContracts.ArgumentNotNull(dbField, "dbField");

      toSearchWhat = toSearchWhat.Replace("'", "''").Trim();

      var conditions = new List<SearchCondition>();
      string conditionSql = string.Empty;

      var conditionType = SearchConditionType.AND;

      if (searchWhatMethod == SearchWhatFlags.AnyWords)
      {
        conditionType = SearchConditionType.OR;
      }

      var wordList = new List<string> { toSearchWhat };

      if (searchWhatMethod == SearchWhatFlags.AllWords || searchWhatMethod == SearchWhatFlags.AnyWords)
      {
        wordList =
          toSearchWhat.Replace(@"""", string.Empty).Split(' ').Where(x => x.IsSet()).Select(x => x.Trim()).ToList();
      }

      if (useFullText)
      {
        var list = new List<SearchCondition>();

        list.AddRange(
          wordList.Select(
            word => new SearchCondition { Condition = @"""{0}""".FormatWith(word), ConditionType = conditionType }));

        conditions.Add(
          new SearchCondition
            {
              Condition = "CONTAINS ({1}, N' {0} ')".FormatWith(list.BuildSql(false), dbField), 
              ConditionType = conditionType
            });
      }
      else
      {
        conditions.AddRange(
          wordList.Select(
            word =>
            new SearchCondition
              {
                 Condition = "{1} LIKE N'%{0}%'".FormatWith(word, dbField), ConditionType = conditionType 
              }));
      }

      return conditions;
    }

    /// <summary>
    /// The build search who conditions.
    /// </summary>
    /// <param name="toSearchFromWho">
    /// The to search from who.
    /// </param>
    /// <param name="searchFromWhoMethod">
    /// The search from who method.
    /// </param>
    /// <param name="searchDisplayName">
    /// The search display name.
    /// </param>
    /// <returns>
    /// </returns>
    [NotNull]
    protected IEnumerable<SearchCondition> BuildWhoConditions(
      [NotNull] string toSearchFromWho, SearchWhatFlags searchFromWhoMethod, bool searchDisplayName)
    {
      CodeContracts.ArgumentNotNull(toSearchFromWho, "toSearchFromWho");

      toSearchFromWho = toSearchFromWho.Replace("'", "''").Trim();

      var conditions = new List<SearchCondition>();
      string conditionSql = string.Empty;

      var conditionType = SearchConditionType.AND;

      if (searchFromWhoMethod == SearchWhatFlags.AnyWords)
      {
        conditionType = SearchConditionType.OR;
      }

      var wordList = new List<string> { toSearchFromWho };

      if (searchFromWhoMethod == SearchWhatFlags.AllWords || searchFromWhoMethod == SearchWhatFlags.AnyWords)
      {
        wordList =
          toSearchFromWho.Replace(@"""", string.Empty).Split(' ').Where(x => x.IsSet()).Select(x => x.Trim()).ToList();
      }

      foreach (string word in wordList)
      {
        int userId;

        if (int.TryParse(word, out userId))
        {
          conditionSql = "c.UserID IN ({0})".FormatWith(userId);
        }
        else
        {
          if (searchFromWhoMethod == SearchWhatFlags.ExactMatch)
          {
            conditionSql = "(c.Username IS NULL AND b.{1} = N'{0}') OR (c.Username = N'{0}')".FormatWith(
              word, searchDisplayName ? "DisplayName" : "Name");
          }
          else
          {
            conditionSql = "(c.Username IS NULL AND b.{1} LIKE N'%{0}%') OR (c.Username LIKE N'%{0}%')".FormatWith(
              word, searchDisplayName ? "DisplayName" : "Name");
          }
        }

        conditions.Add(new SearchCondition { Condition = conditionSql, ConditionType = conditionType });
      }

      return conditions;
    }

    #endregion
  }
}