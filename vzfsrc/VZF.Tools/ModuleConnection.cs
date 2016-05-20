#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File ModuleConnection.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.20.2016 in 5:13 PM.
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


namespace VZF.Tools
{
    /*  public class ModuleConnectionData
      {
          public ModuleConnection GetModuleConnectionData (ModuleClone moduleClone)
          {
              var mc = new ModuleConnection();
              ModuleConnection.ObjectId = moduleClone.ObjectId;
              ModuleConnection.ObjectInnerId = moduleClone.ObjectInnerId;
              ModuleConnection.ModuleType = moduleClone.ModuleType;
              return mc;
          }
       
      } */

    /// <summary>
    /// The module connection.
    /// </summary>
    public class ModuleConnection
    {
        /// <summary>
        /// Gets or sets the object id.
        /// </summary>
        public object ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the object inner id.
        /// </summary>
        public object ObjectInnerId { get; set; }

        /// <summary>
        /// Gets or sets the module type.
        /// </summary>
        public int ModuleType { get; set; }
    }
}
