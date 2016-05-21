
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


using System;

namespace YAF.Types.Objects
{
  #region Using

  using System.Data;

  #endregion

  /// <summary>
  /// The typed smiley list.
  /// </summary>
  [Serializable]
  public class TypedSmileyList
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TypedSmileyList"/> class.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public TypedSmileyList([NotNull] DataRow row)
    {
      this.SmileyID = row.Field<int?>("SmileyID");
      this.BoardID = row.Field<int?>("BoardID");
      this.Code = row.Field<string>("Code");
      this.Icon = row.Field<string>("Icon");
      this.Emoticon = row.Field<string>("Emoticon");
      this.SortOrder = Convert.ToInt32(row["SortOrder"]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypedSmileyList"/> class.
    /// </summary>
    /// <param name="smileyid">
    /// The smileyid.
    /// </param>
    /// <param name="boardid">
    /// The boardid.
    /// </param>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <param name="icon">
    /// The icon.
    /// </param>
    /// <param name="emoticon">
    /// The emoticon.
    /// </param>
    /// <param name="sortorder">
    /// The sortorder.
    /// </param>
    public TypedSmileyList(int? smileyid, int? boardid, [NotNull] string code, [CanBeNull] string icon, [CanBeNull] string emoticon, int? sortorder)
    {
      this.SmileyID = smileyid;
      this.BoardID = boardid;
      this.Code = code;
      this.Icon = icon;
      this.Emoticon = emoticon;
      this.SortOrder = sortorder;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets BoardID.
    /// </summary>
    public int? BoardID { get; set; }

    /// <summary>
    /// Gets or sets Code.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets Emoticon.
    /// </summary>
    public string Emoticon { get; set; }

    /// <summary>
    /// Gets or sets Icon.
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Gets or sets SmileyID.
    /// </summary>
    public int? SmileyID { get; set; }

    /// <summary>
    /// Gets or sets SortOrder.
    /// </summary>
    public int? SortOrder { get; set; }

    #endregion
  }
}