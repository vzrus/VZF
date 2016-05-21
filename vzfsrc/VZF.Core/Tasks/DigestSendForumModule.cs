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
 * File DigestSendForumModule.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:05 PM.
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

namespace YAF.Core.Tasks
{
    #region Using

    using System;

    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The mail sending module.
    /// </summary>
    [YafModule("Digest Send Starting Module", "Tiny Gecko", 1)]
    public class DigestSendForumModule : BaseForumModule
    {
        #region Constants and Fields

        /// <summary>
        ///   The _key name.
        /// </summary>
        private const string _KeyName = "DigestSendTask";

        #endregion

        #region Public Methods

        /// <summary>
        /// The init.
        /// </summary>
        public override void Init()
        {
            // hook the page init for mail sending...
            YafContext.Current.AfterInit += this.Current_AfterInit;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the AfterInit event of the Current control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Current_AfterInit([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<ITaskModuleManager>().StartTask(_KeyName, () => new DigestSendTask());
        }

        #endregion
    }
}