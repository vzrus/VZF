#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File ProviderBase.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:13 PM.
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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using VZF.Data.DAL;
using YAF.Core;
using YAF.Providers.Utils;
using YAF.Types.Interfaces;

namespace YAF.Providers.Profile
{
    using YAF.Classes;

    public partial class PgProfileProvider 
    {


        /// <summary>
        /// Sets up the profile providers
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        /// <summary>
        /// Sets up the profile providers
        /// </summary>
        /// <param name="name">
        /// </param>
        /// <param name="config">
        /// </param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // verify that the configuration section was properly passed
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Application Name
            this._appName = config["applicationName"].ToStringDBNull(Config.ApplicationName);

            // Connection String Name
            this._connStrName = config["connectionStringName"].ToStringDBNull(Config.ConnectionStringName);

            // is the connection string set?
            if (!String.IsNullOrEmpty(this._connStrName))
            {
                string connStr = ConfigurationManager.ConnectionStrings[this._connStrName].ConnectionString;
                ConnectionString = connStr;
                ConnectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connStr);

                // set the app variable...
                if (YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] == null)
                {
                    YafContext.Current.Get<HttpApplicationStateBase>().Add(ConnStrAppKeyName, connStr);
                }
                else
                {
                    YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] = connStr;
                }
            }

            base.Initialize(name, config);
        }
    }
}
