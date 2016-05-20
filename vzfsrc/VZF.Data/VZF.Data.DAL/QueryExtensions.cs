#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File QueryExtensions.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:17 PM.
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

namespace VZF.Data.DAL
{
    using System.Text;

    /// <summary>
    /// The query extensions.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// The append object query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, int? mid)
        {
            return sb.Append(SqlDbAccess.GetVzfObjectName(command, mid));
        }

        /// <summary>
        /// The append object query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, string connectionStringName)
        {
            return sb.Append(SqlDbAccess.GetVzfObjectNameFromConnectionString(command, connectionStringName));
        }

        /// <summary>
        /// The append query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendQuery(this StringBuilder sb, string command)
        {
            return sb.Append(command);
        }
    }
}
