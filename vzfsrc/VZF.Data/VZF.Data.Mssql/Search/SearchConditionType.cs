#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SearchConditionType.cs created  on 2.6.2015 in  6:29 AM.
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
  /// <summary>
  /// The search condition type.
  /// </summary>
  public enum SearchConditionType
  {
    /// <summary>
    ///   The and.
    /// </summary>
    AND, 

    /// <summary>
    ///   The or.
    /// </summary>
    OR
  }
}