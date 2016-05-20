#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DataRowConvert.cs created  on 2.6.2015 in  6:29 AM.
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
  #region Using

    using System;
    using System.Data;
using System.Text;

    #endregion

  /// <summary>
  /// Helper class to do basic data conversion for a DataRow.
  /// </summary>
  public class DataRowConvert
  {
    #region Constants and Fields

    /// <summary>
    ///   The _db row.
    /// </summary>
    private readonly DataRow _dbRow;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowConvert"/> class.
    /// </summary>
    /// <param name="dbRow">
    /// The db row.
    /// </param>
    public DataRowConvert(DataRow dbRow)
    {
      this._dbRow = dbRow;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The as bool.
    /// </summary>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The as bool.
    /// </returns>
    public bool? AsBool(string columnName)
    {
      if (this._dbRow[columnName] == DBNull.Value)
      {
        return null;
      }

      return Convert.ToBoolean(this._dbRow[columnName]);
    }

    /// <summary>
    /// The as date time.
    /// </summary>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// </returns>
    public DateTime? AsDateTime(string columnName)
    {
      if (this._dbRow[columnName] == DBNull.Value)
      {
        return null;
      }

      return Convert.ToDateTime(this._dbRow[columnName]);
    }

    /// <summary>
    /// The as int 32.
    /// </summary>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// </returns>
    public int? AsInt32(string columnName)
    {
      if (this._dbRow[columnName] == DBNull.Value)
      {
        return null;
      }

      return Convert.ToInt32(this._dbRow[columnName]);
    }

    /// <summary>
    /// The as int 64.
    /// </summary>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// </returns>
    public long? AsInt64(string columnName)
    {
      if (this._dbRow[columnName] == DBNull.Value)
      {
        return null;
      }

      return Convert.ToInt64(this._dbRow[columnName]);
    }

    /// <summary>
    /// The as string.
    /// </summary>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The as string.
    /// </returns>
    public string AsString(string columnName)
    {
      if (this._dbRow[columnName] == DBNull.Value)
      {
        return null;
      }

      return this._dbRow[columnName].ToString();
    }

    #endregion

 
  }
}