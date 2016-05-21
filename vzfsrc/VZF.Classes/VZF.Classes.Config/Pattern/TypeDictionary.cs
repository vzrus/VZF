﻿#region copyright
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
 * File TypeDictionary.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:08 PM.
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

namespace YAF.Classes.Pattern
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Allows basic type conversion of Dictionary objects.
    /// </summary>
    [Serializable]
    public class TypeDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// The as type.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public T AsType<T>(string key, T defaultValue)
        {
            if (!this.ContainsKey(key))
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(this[key], typeof(T));
        }

        /// <summary>
        /// The as type.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public T AsType<T>(string key)
        {
            return (T)Convert.ChangeType(this[key], typeof(T));
        }

        /// <summary>
        /// The as boolean.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public bool? AsBoolean(string key)
        {
            if (!ContainsKey(key))
            {
                return null;
            }

            return AsType<bool>(key);
        }

        /// <summary>
        /// The as int.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// </returns>
        public int? AsInt(string key)
        {
            if (!ContainsKey(key))
            {
                return null;
            }

            return AsType<int>(key);
        }

        /// <summary>
        /// The as string.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The as string.
        /// </returns>
        public string AsString(string key)
        {
            if (!ContainsKey(key))
            {
                return null;
            }

            return AsType<string>(key);
        }
    }
}