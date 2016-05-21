
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
 * File ActiveLocation.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
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


namespace VZF.Types.Objects
{
    using System;

    /// <summary>
    /// The connection string parameter.
    /// </summary>
    public class ConnectionStringParameter
    {
        /// <summary>
        /// The _name.
        /// </summary>
        private string name;

        /// <summary>
        /// The _type.
        /// </summary>
        private Type type;

        /// <summary>
        /// The _value.
        /// </summary>
        private string value;

        /// <summary>
        /// The _fixed Value.
        /// </summary>
        private bool fixedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringParameter"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="fixedValue">
        /// The fixed value.
        /// </param>
        public ConnectionStringParameter(string name, Type type, string value, bool fixedValue)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
            this.FixedValue = fixedValue;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether fixed value.
        /// </summary>
        public bool FixedValue
        {
            get { return this.fixedValue; }
            set { this.fixedValue = value; }
        }
    }
}
