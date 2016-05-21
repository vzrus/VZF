
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


namespace YAF.Types.Constants
{
    /// <summary>
    /// The event log types. Always use the same numbers in the enumeration and NEVER change the pairs. 
    /// </summary>
    public enum EventLogTypes
    {
        /// <summary>
        /// The debug.
        /// </summary>
        Debug = -1000,

        /// <summary>
        /// The trace.
        /// </summary>
        Trace = -500,

        /// <summary>
        ///   The error.
        /// </summary>
        Error = 0,

        /// <summary>
        ///   The warning.
        /// </summary>
        Warning = 1,

        /// <summary>
        ///   The information.
        /// </summary>
        Information = 2,

        /// <summary>
        ///   The sql error.
        /// </summary>
        SqlError = 3,

        /// <summary>
        ///   The user suspended.
        /// </summary>
        UserSuspended = 1000,

        /// <summary>
        ///   The user unsuspended.
        /// </summary>
        UserUnsuspended = 1001,

        /// <summary>
        ///   The user deleted.
        /// </summary>
        UserDeleted = 1002,

        /// <summary>
        /// The ban set.
        /// </summary>
        IpBanSet = 1003,

        /// <summary>
        /// The Ip Ban Lifted.
        /// </summary>
        IpBanLifted = 1004
    }
}