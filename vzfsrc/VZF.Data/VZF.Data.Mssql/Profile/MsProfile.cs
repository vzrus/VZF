#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File MsProfile.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Data.Common
{
    using System.Text;

    using VZF.Data.MsSql.Mappers;
    using VZF.Utils;

    using YAF.Types;

    /// <summary>
    /// The ms profile.
    /// </summary>
    public static class MsProfile
    {       
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="setStr">
        /// The set str.
        /// </param>
        /// <param name="columnStr">
        /// The column str.
        /// </param>
        /// <param name="valueStr">
        /// The value str.
        /// </param>
        /// <param name="profileExists">
        /// The profile exists.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SetProfileProperties(
            [NotNull] string setStr,
            [NotNull] string columnStr,
            [NotNull] string valueStr,
            [NotNull] bool profileExists) 
        {            
            var sqlCommand = new StringBuilder();

            if (profileExists)
            {
                sqlCommand.Append("UPDATE {0}").Append(" SET ").Append(setStr.Trim(','));
                sqlCommand.Append(" WHERE UserID = @i_UserID AND ApplicationName = @i_ApplicationName");
            }
            else
            {
                sqlCommand.Append("INSERT {0}").Append(" (UserID,").Append(columnStr.Trim(','));
                sqlCommand.Append(") VALUES (@i_UserID,").Append(valueStr.Trim(',')).Append(
                  ")");
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
        /// The column type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        public static string AddProfileColumn([NotNull] string name, string type, int size, string tableName)
        {
            type = DataTypeMappers.typeToDbValueMap(name, type, size);

            return "ALTER TABLE {0} ADD [{1}] {2} NULL".FormatWith(
              tableName, name, type);
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
            // vzrus: here we replace MS SQL data types
            string paramName = DataTypeMappers.FromDbValueMap(chunk[1]);
            chunk[1] = paramName;      

            return chunk;
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <returns>
        /// </returns>
        public static string GetProfileStructure()
        {
            return @"SELECT TOP 1 1 * FROM {0}";
        }

        /// <summary>
        /// Gets the profile exists.
        /// </summary>
        public static string ProfileExists
        {
            get
            {
                return "SELECT top 1 1 FROM {0} WHERE UserID = @UserID AND ApplicationName = @ApplicationName";
            }
        }

        #endregion
    }
}
