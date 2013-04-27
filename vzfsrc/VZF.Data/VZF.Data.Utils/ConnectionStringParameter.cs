// <copyright company="Vladimir Zakharov" file="ConnectionStringParameter.cs">
//   VZF by vzrus
//   Copyright (C) 2013 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The ConnectionStringParameter.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace VZF.Data.Utils
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
