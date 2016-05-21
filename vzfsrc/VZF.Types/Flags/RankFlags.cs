
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


namespace YAF.Types.Flags
{
    using System;

    /// <summary>
    /// The rank flags.
    /// </summary>
    [Serializable]
    public class RankFlags : FlagsBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RankFlags"/> class.
        /// </summary>
        public RankFlags()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RankFlags"/> class.
        /// </summary>
        /// <param name="flags">
        /// The flags.
        /// </param>
        public RankFlags(Flags flags)
            : this((int)flags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RankFlags"/> class.
        /// </summary>
        /// <param name="bitValue">
        /// The bit value.
        /// </param>
        public RankFlags(object bitValue)
            : this((int)bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RankFlags"/> class.
        /// </summary>
        /// <param name="bitValue">
        /// The bit value.
        /// </param>
        public RankFlags(int bitValue)
            : base(bitValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RankFlags"/> class.
        /// </summary>
        /// <param name="bits">
        /// The bits.
        /// </param>
        public RankFlags(params bool[] bits)
            : base(bits)
        {
        }

        #endregion

        #region Operators

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="newBitValue">
        /// The new bit value.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator RankFlags(int newBitValue)
        {
            return new RankFlags(newBitValue);
        }

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator RankFlags(Flags flags)
        {
            return new RankFlags(flags);
        }

        #endregion

        #region Flags Enumeration

        /// <summary>
        /// Use for bit comparisons
        /// </summary>
        public enum Flags
        {
            /// <summary>
            /// The none.
            /// </summary>
            None = 0,

            /// <summary>
            /// The is start.
            /// </summary>
            IsStart = 1,

            /// <summary>
            /// The is ladder.
            /// </summary>
            IsLadder = 2,

            /// <summary>
            /// The is hidden(like guest).
            /// </summary>
            IsHidden = 4

            /* for future use                  
                  xxxxx = 8,
                  xxxxx = 16,
                  xxxxx = 32,
                  xxxxx = 64,
                  xxxxx = 128,
                  xxxxx = 256,
                  xxxxx = 512
                   */
        }


        #endregion

        #region Single Flags (can be 32 of them)

        /// <summary>
        /// Gets or sets whether rank is default starting rank of new users.
        /// </summary>
        public bool IsStart
        {
            // int value 1
            get
            {
                return this[0];
            }

            set
            {
                this[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets whether rank is ladder rank.
        /// </summary>
        public bool IsLadder
        {
            // int value 2
            get
            {
                return this[1];
            }

            set
            {
                this[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is guest.
        /// </summary>
        public bool IsGuest
        {
            // int value 4
            get
            {
                return this[2];
            }

            set
            {
                this[2] = value;
            }
        }

        #endregion
    }
}