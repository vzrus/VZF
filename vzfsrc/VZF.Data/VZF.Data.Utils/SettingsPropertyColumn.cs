#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SettingsPropertyColumn.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:21 PM.
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

namespace VZF.Data.Utils
{
    using System.Configuration;
    using System.Data;

    /// <summary>
  /// The settings property column.
  /// </summary>
  public class SettingsPropertyColumn
  {
    #region Constants and Fields

    /// <summary>
    /// The data type.
    /// </summary>
    public DbType DataType;

    /// <summary>
    /// The settings.
    /// </summary>
    public SettingsProperty Settings;

    /// <summary>
    /// The size.
    /// </summary>
    public int Size;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPropertyColumn"/> class.
    /// </summary>
    public SettingsPropertyColumn()
    {
      // empty for default constructor...
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPropertyColumn"/> class.
    /// </summary>
    /// <param name="settings">
    /// The settings.
    /// </param>
    /// <param name="dataType">
    /// The data type.
    /// </param>
    /// <param name="size">
    /// The size.
    /// </param>
    public SettingsPropertyColumn(SettingsProperty settings, DbType dataType, int size)
    {
      this.DataType = dataType;
      this.Settings = settings;
      this.Size = size;
    }

    #endregion
  }
}