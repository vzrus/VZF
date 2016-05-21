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
 * File RegistryDictionary.cs created  on 2.6.2015 in  6:29 AM.
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

using System;
using System.Collections.Generic;

namespace YAF.Classes.Pattern
{
    /// <summary>
    /// Provides a method for automatic overriding of a base hash...
    /// </summary>
    public class RegistryDictionaryOverride : RegistryDictionary
    {
        /// <summary>
        /// The _default get override.
        /// </summary>
        private bool _defaultGetOverride = true;

        /// <summary>
        /// The _default set override.
        /// </summary>
        private bool _defaultSetOverride;

        /// <summary>
        /// Gets or sets a value indicating whether DefaultGetOverride.
        /// </summary>
        public bool DefaultGetOverride
        {
            get
            {
                return this._defaultGetOverride;
            }

            set
            {
                this._defaultGetOverride = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DefaultSetOverride.
        /// </summary>
        public bool DefaultSetOverride
        {
            get
            {
                return this._defaultSetOverride;
            }

            set
            {
                this._defaultSetOverride = value;
            }
        }

        /// <summary>
        /// Gets or sets OverrideDictionary.
        /// </summary>
        public RegistryDictionary OverrideDictionary { get; set; }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public override T GetValue<T>(string name, T defaultValue)
        {
            return GetValue<T>(name, defaultValue, DefaultGetOverride);
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <param name="allowOverride">
        /// The allow override.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public virtual T GetValue<T>(string name, T defaultValue, bool allowOverride)
        {
            if (allowOverride && this.OverrideDictionary != null && this.OverrideDictionary.ContainsKey(name.ToLower())
                && this.OverrideDictionary[name.ToLower()] != null)
            {
                return this.OverrideDictionary.GetValue<T>(name, defaultValue);
            }

            // just pull the value from this dictionary...
            return base.GetValue<T>(name, defaultValue);
        }

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        public override void SetValue<T>(string name, T value)
        {
            SetValue<T>(name, value, DefaultSetOverride);
        }

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="setOverrideOnly">
        /// The set override only.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        public virtual void SetValue<T>(string name, T value, bool setOverrideOnly)
        {
            if (this.OverrideDictionary != null)
            {
                if (setOverrideOnly)
                {
                    // just set the override dictionary...
                    this.OverrideDictionary.SetValue<T>(name, value);
                    return;
                }

                if (this.OverrideDictionary.ContainsKey(name.ToLower())
                    && this.OverrideDictionary[name.ToLower()] != null)
                {
                    // set the overriden value to null/erase it...
                    this.OverrideDictionary.SetValue<T>(name, (T)Convert.ChangeType(null, typeof(T)));
                }
            }

            // save new value in the base...
            base.SetValue<T>(name, value);
        }
    }

    /// <summary>
    /// The registry dictionary.
    /// </summary>
    [Serializable]
    public class RegistryDictionary : Dictionary<string, object>
    {
        /* Ederon : 6/16/2007 -- modified by Jaben 7/17/2009 */

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public virtual T GetValue<T>(string name, T defaultValue)
        {
            if (!ContainsKey(name.ToLower()))
            {
                return defaultValue;
            }

            object value = this[name.ToLower()];

            if (value == null)
            {
                return defaultValue;
            }

            Type objectType = typeof(T);

            if (objectType.BaseType == typeof(Enum))
            {
                return (T)Enum.Parse(objectType, value.ToString());
            }

            // special handling for boolean...
            if (objectType == typeof(bool))
            {
                int i;
                return int.TryParse(value.ToString(), out i)
                           ? (T)Convert.ChangeType(Convert.ToBoolean(i), typeof(T))
                           : (T)Convert.ChangeType(Convert.ToBoolean(value), typeof(T));
            }

            // special handling for int values...
            if (objectType == typeof(int))
            {
                return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));
            }

            return (T)Convert.ChangeType(this[name.ToLower()], typeof(T));
        }

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// The type parameter.
        /// </typeparam>
        public virtual void SetValue<T>(string name, T value)
        {
            var objectType = typeof(T);
            string stringValue = Convert.ToString(value);

            if (objectType == typeof(bool) || objectType.BaseType == typeof(Enum))
            {
                stringValue = Convert.ToString(Convert.ToInt32(value));
            }

            this[name.ToLower()] = stringValue;
        }

        /* 6/16/2007 */
    }
}