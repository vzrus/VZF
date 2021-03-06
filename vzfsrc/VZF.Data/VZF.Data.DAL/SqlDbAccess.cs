﻿#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SqlDbAccess.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:17 PM.
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

namespace VZF.Data.DAL
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Text;

    using VZF.Tools;
    using VZF.Utils;

    using YAF.Classes;

    /// <summary>
    /// The common sql db access.
    /// </summary>
    public class SqlDbAccess
    {
        /// <summary>
        /// The module type enum.
        /// </summary>
        public enum ModuleTypeEnum
        {
            /// <summary>
            /// The generic.
            /// </summary>
            Generic = 0,

            /// <summary>
            /// The membership.
            /// </summary>
            Membership = 1,

            /// <summary>
            /// The roles.
            /// </summary>
            Roles = 2,

            /// <summary>
            /// The profile.
            /// </summary>
            Profile = 3,

            /// <summary>
            /// The forum board.
            /// </summary>
            ForumBoard = 4
        }

        /// <summary>
        /// The ms sql.
        /// </summary>
        public const string MsSql = "System.Data.SqlClient";

        /// <summary>
        /// The npgsql.
        /// </summary>
        public const string Npgsql = "Npgsql";

        /// <summary>
        /// The my sql.
        /// </summary>
        public const  string MySql = "MySql.Data.MySqlClient";

        /// <summary>
        /// The firebird.
        /// </summary>
        public const string Firebird = "FirebirdSql.Data.FirebirdClient";

        /// <summary>
        /// The oracle.
        /// </summary>
        public const string Oracle = "System.Data.OracleClient";

        /// <summary>
        /// The db 2.
        /// </summary>
        public const string Db2 = "System.Data.Db2";

        /// <summary>
        /// The other.
        /// </summary>
        public const string Other = "Other";

        public static ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _providerInformation;

        private ConcurrentDictionary<string, string> _providerInformationDetails;

        public ConcurrentDictionary<string, string> ProviderInformationDetails
        {
            get { return this._providerInformationDetails; }
            set { this._providerInformationDetails = value; }
        }
        
        public static ConcurrentDictionary<string, ConcurrentDictionary<string, string>> ProviderInformation
        {
            get
            {
                return _providerInformation;
            }

            set
            {
                _providerInformation = value;
            }
        }

        /// <summary>
        /// The get connection data.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="namePattern">
        /// The name pattern.
        /// </param>
        /// <param name="dataEngine">
        /// The data engine.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool GetConnectionData(int? mid, string namePattern, out string dataEngine, out string connectionString)
        {
            // string [] patterns = new string[]{"",""};

            // Look for modules special configs.
            string connectionStringName =
                Config.GetConfigValueAsString(String.Format("VZF.ConnectionStringNameModule{0}", mid));
           
            // They were not found gte default.
            if (String.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            if (dataEngine.IsNotSet())
            {
                throw new ApplicationException("No data base engine name was supplied.");
            }

            return true;
        }
       

        /// <summary>
        /// The get connection data.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="namePattern">
        /// The name pattern.
        /// </param>
        /// <param name="dataEngine">
        /// The data engine.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool GetConnectionData(int? mid, string namePattern, out string dataEngine, out string connectionString, out string connectionStringName)
        {
            // string [] patterns = new string[]{"",""};

            // Look for modules special configs.
            connectionStringName =
                Config.GetConfigValueAsString(String.Format("VZF.ConnectionStringNameModule{0}", mid));

            // They were not found gte default.
            if (String.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            if (dataEngine.IsNotSet())
            {
                throw new ApplicationException("No data base engine name was supplied.");
            }

            return true;
        }

        /// <summary>
        /// The get connection string name.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="namePattern">
        /// The name pattern.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionStringName(int? mid, string namePattern)
        {
            // string [] patterns = new string[]{"",""};

            // Look for modules special configs.
            string connectionStringName =
                Config.GetConfigValueAsString(String.Format("VZF.ConnectionStringNameModule{0}", mid));

            // They were not found gte default.
            if (String.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            return connectionStringName;
        }

        /// <summary>
        /// The get connection string name from connection string.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionStringNameFromConnectionString(string connectionString)
        {
           foreach (ConnectionStringSettings constr in  ConfigurationManager.ConnectionStrings)
           {
              if (constr.ConnectionString.Equals(connectionString))
              {
                  return constr.Name;
              }
           }

           return null;             
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="namePattern">
        /// The name pattern.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetConnectionString(int? mid, string namePattern)
        {
            // string [] patterns = new string[]{"",""};

            // Look for modules special configs.
            string connectionStringName =
                Config.GetConfigValueAsString(String.Format("VZF.ConnectionStringNameModule{0}", mid));

            // They were not found gte default.
            if (String.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /// <summary>
        /// The get provider name.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static string GetProviderName(string connectionStringName)
        {
            var providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            if (providerName.IsNotSet())
            {
                throw new ApplicationException("No data base engine name was supplied. Check connection data.");
            }

            return providerName;
        }



        /// <summary>
        /// The get connection data.
        /// </summary>
        /// <param name="moduleClone">
        /// The module clone.
        /// </param>
        /// <param name="dataEngine">
        /// The data engine.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetConnectionData(ModuleConnection moduleClone, out string dataEngine, out string connectionString)
        {
          // connection string keys have a special format
          // with prefix VZF_ConnStrName 
          // ThenComes a  ModuleTypeEnum defined value 
          // and BoardId or ObjectInnerId which are not module instances number
          // but an inner codes for instance objects. In some cases they can be the same
          // Example^ VZF_ConnStrName_ForumBoard2_BoardID2

            var sb = new StringBuilder("VZF_ConnStrName");
          if (moduleClone.ModuleType == (int)ModuleTypeEnum.ForumBoard)
          {
              sb.AppendFormat("_ForumBoard{0}", moduleClone.ObjectId);
              sb.AppendFormat("_BoardID{0}", moduleClone.ObjectInnerId);
          }

          // Look for board special configs.
          string connectionStringName = Config.GetConfigValueAsString(sb.ToString());

          // They were not found gte default.
          if (String.IsNullOrEmpty(connectionStringName))
          {
              connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
          }

          connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
          dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName; 

          return true;
        }

        /// <summary>
        /// The data engine name.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DataEngineName(int? mid)
        {
            string dataEngine;
            string connectionString;

            string namePattern = String.Empty;
            GetConnectionData(mid, namePattern, out dataEngine, out connectionString);

            return dataEngine;
        }

        /// <summary>
        /// The get provider name from connection string.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetProviderNameFromConnectionString(string connectionString)
        {
            string defaultName = String.Empty;
          
            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                if (defaultName.IsNotSet())
                {
                    defaultName = cs.ProviderName;
                }

                if (cs.ConnectionString == connectionString)
                {
                    defaultName = cs.ProviderName;
                    break;
                }
            }

            return defaultName;
        }

        public static string GetVzfObjectName(string objectName, int? mid)
        {
            return new VzfSqlCommand(GetConnectionStringName(mid, String.Empty)).DataSource.WrapObjectName(objectName);
        }

        public static string GetVzfObjectNameFromConnectionString(string objectName, string connectionStringName)
        {
            return new VzfSqlCommand(connectionStringName).DataSource.WrapObjectName(objectName);
        }
    }
}
