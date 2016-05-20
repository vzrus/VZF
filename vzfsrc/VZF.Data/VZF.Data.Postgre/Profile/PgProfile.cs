#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File PgProfile.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:20 PM.
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

namespace VZF.Data.Common
{
    using System.Text;

    using VZF.Data.Postgre.Mappers;
    using VZF.Utils;

    using YAF.Types;

    /// <summary>
    /// The pg profile.
    /// </summary>
    public class PgProfile
    {
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The boardId.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        /// <param name="dirtyOnly">The dirty only.</param>
        public static string SetProfileProperties(
            [NotNull] string setStr,
            [NotNull] string columnStr,
            [NotNull] string valueStr,
            [NotNull] bool profileExists)
        {

            StringBuilder sqlCommand = new StringBuilder();

            if (profileExists)
            {
                sqlCommand.Append("UPDATE {0}").Append(" SET ").Append(setStr.Trim(','));
                sqlCommand.Append(" WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName ");
            }
            else
            {
                sqlCommand.Append("INSERT INTO {0}").Append(" (UserID,").Append(columnStr.Trim(','));
                sqlCommand.Append(") VALUES (@i_UserID,").Append(valueStr.Trim(',')).Append(")");
            }

            return sqlCommand.ToString();
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        public static string AddProfileColumn([NotNull] string name, string type, int size, string tableName)
        {
            type = DataTypeMappers.typeToDbValueMap(name, type, size);

            return "ALTER TABLE {0} ADD {1} {2}".FormatWith(tableName, name, type);
        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="chunk">
        /// The chunk.
        /// </param>
        /// <returns>
        /// The get db type and size from string.
        /// </returns>
        public static string[] GetDbTypeAndSizeFromString(string[] chunk)
        {
            string paramName = DataTypeMappers.FromDbValueMap(chunk[1]);
            chunk[1] = paramName;

            return chunk;
        }

        /// <summary>
        /// Gets the profile exists.
        /// </summary>
        public static string ProfileExists
        {
            get
            {
                return @"SELECT 1 FROM {0}  WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName LIMIT 1";
            }
        }

        #endregion
    }
}
