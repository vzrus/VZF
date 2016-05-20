#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File DataTypeMappers.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Data.MySql.Mappers
{
    using VZF.Data.DAL;

    using YAF.Classes;

    /// <summary>
    /// The data type mappers.
    /// </summary>
    public static class DataTypeMappers
    {
        /// <summary>
        /// The type to db value map.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string typeToDbValueMap(string name, string type, int size)
        {
            if (type.Contains("DateTime"))
            {
                type = "DATETIME";
            }

            if (type.Contains("String"))
            {
                if (size > 21844)
                {
                    type = "TEXT";
                }
                else
                {
                    type = "VARCHAR";
                }
            }

            if (type.Contains("Int32"))
            {
                type = "INT";
            }

            if (type.Contains("Boolean"))
            {
                type = "TINYINT";
            }

            if (size > 0)
            {
                type += "(" + size + ")";
            }

            if (type.ToLowerInvariant().Contains("varchar") && Config.DatabaseEncoding != null)
            {
                type += " CHARACTER SET " + Config.DatabaseEncoding;

                if (Config.DatabaseCollation != null)
                {
                    type += " COLLATE " + Config.DatabaseCollation;
                }
            }

            return type;
        }

        /// <summary>
        /// The from db value map.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FromDbValueMap(string value)
        {
            // vzrus: here we replace MS SQL data types
            if (value.ToLowerInvariant().IndexOf("varchar", System.StringComparison.Ordinal) >= 0
                || value.ToLowerInvariant().IndexOf("nvarchar", System.StringComparison.Ordinal) >= 0
                || value.ToLowerInvariant().IndexOf("text", System.StringComparison.Ordinal) >= 0)
            {
                value = "String";
            }

            if (value.ToLowerInvariant().IndexOf("int", System.StringComparison.Ordinal) >= 0)
            {
                value = "Int32";
            }

            if (value.ToLowerInvariant().IndexOf("datetime", System.StringComparison.Ordinal) >= 0)
            {
                value = "DateTime";
            }

            if (value.ToLowerInvariant().IndexOf("bit", System.StringComparison.Ordinal) >= 0)
            {
                value = "Boolean";
            }

            return value;
        }
    }
}
