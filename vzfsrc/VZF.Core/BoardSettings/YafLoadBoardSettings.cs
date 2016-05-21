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
 * File YafLoadBoardSettings.cs created  on 2.6.2015 in  6:29 AM.
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

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace YAF.Core
{
  #region Using

  using System;
  using System.Data;
  using System.Web.Security;

  using VZF.Data.Common;

  using YAF.Classes;
  
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The yaf load board settings.
  /// </summary>
  public class YafLoadBoardSettings : YafBoardSettings
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="YafLoadBoardSettings"/> class.
    /// </summary>
    /// <param name="boardID">
    /// The board id.
    /// </param>
    /// <exception cref="Exception">
    /// </exception>
    /// <exception cref="EmptyBoardSettingException"><c>EmptyBoardSettingException</c>.</exception>
    public YafLoadBoardSettings([NotNull] object boardID)
    {
      this._boardID = boardID;

      // get the board table
      DataTable dataTable = CommonDb.board_list(YafContext.Current.PageModuleID, this._boardID);

      if (dataTable.Rows.Count == 0)
      {
        throw new EmptyBoardSettingException("No data for board ID: {0}".FormatWith(this._boardID));
      }

      // setup legacy board settings...
      this.SetupLegacyBoardSettings(dataTable.Rows[0]);

      // get all the registry values for the forum
      this.LoadBoardSettingsFromDB();
    }

    #endregion

    #region Public Methods

      /// <summary>
      /// Saves the whole setting registry to the database.
      /// </summary>
      public void SaveRegistry()
      {
          // loop through all values and commit them to the DB
          foreach (string key in this._reg.Keys)
          {
              CommonDb.registry_save(YafContext.Current.PageModuleID, key, this._reg[key]);
          }

          foreach (string key in this._regBoard.Keys)
          {
              CommonDb.registry_save(YafContext.Current.PageModuleID, key, this._regBoard[key], this._boardID);
          }
      }
     
      public void SaveRegistry(Dictionary<string,object> regEntry, int? boardId)
      {
          foreach (var entry in regEntry)
          {
              // loop through all values and commit them to the DB
              if (boardId.HasValue)
              {
                  foreach (string key in this._regBoard.Keys)
                  {
                      if (entry.Key.ToLowerInvariant().Equals(key.ToLowerInvariant()))
                      {
                          CommonDb.registry_save(YafContext.Current.PageModuleID, key, this._regBoard[key],
                              this._boardID);                         
                      }
                  }
              }
              else
              {
                  foreach (string key in this._reg.Keys)
                  {
                      if (entry.Key.ToLowerInvariant().Equals(key.ToLowerInvariant()))
                      {
                          CommonDb.registry_save(YafContext.Current.PageModuleID, key, this._reg[key]);                         
                      }
                  }
              }
          }
      }
    
    #endregion

    #region Methods

    /// <summary>
    /// The load board settings from db.
    /// </summary>
    /// <exception cref="Exception">
    /// </exception>
    protected void LoadBoardSettingsFromDB()
    {
      DataTable dataTable;

      using (dataTable = CommonDb.registry_list(YafContext.Current.PageModuleID, string.Empty))
      {
        // get all the registry settings into our hash table
        foreach (DataRow dr in dataTable.Rows)
        {
          this._reg.Add(dr["Name"].ToString().ToLower(), dr["Value"] == DBNull.Value ? null : dr["Value"]);
        }
      }

      using (dataTable = CommonDb.registry_list(YafContext.Current.PageModuleID, null, this._boardID))
      {
        // get all the registry settings into our hash table
        foreach (DataRow dr in dataTable.Rows)
        {
          this._regBoard.Add(dr["Name"].ToString().ToLower(), dr["Value"] == DBNull.Value ? null : dr["Value"]);
        }
      }
    }

    /// <summary>
    /// The setup legacy board settings.
    /// </summary>
    /// <param name="board">
    /// The board.
    /// </param>
    private void SetupLegacyBoardSettings([NotNull] DataRow board)
    {
      CodeContracts.ArgumentNotNull(board, "board");

      this._membershipAppName = board["MembershipAppName"].ToString().IsNotSet()
                                  ? YafContext.Current.Get<MembershipProvider>().ApplicationName
                                  : board["MembershipAppName"].ToString();

      this._rolesAppName = board["RolesAppName"].ToString().IsNotSet()
                             ? YafContext.Current.Get<RoleProvider>().ApplicationName
                             : board["RolesAppName"].ToString();

      this._legacyBoardSettings = new YafLegacyBoardSettings(
        board["Name"].ToString(), 
        Convert.ToString(board["SQLVersion"]), 
        board["AllowThreaded"].ToType<bool>(), 
        this._membershipAppName, 
        this._rolesAppName);
    }

    #endregion
  }

  [Serializable]
  public class EmptyBoardSettingException : Exception
  {
    public EmptyBoardSettingException(string message)
      : base(message)
    {
    }
  }
}