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
 * File DataImport.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:04 PM.
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

namespace YAF.Core
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;

    using VZF.Data.Common;

    using VZF.Utils;

    /// <summary>
    /// The data import.
    /// </summary>
    public static class DataImport
    {
        /// <summary>
        /// The bb code extension import.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="imputStream">
        /// The imput stream.
        /// </param>
        /// <returns>
        /// Returns How Many Extensions where imported.
        /// </returns>
        /// <exception cref="Exception">Import stream is not expected format.
        /// </exception>
        public static int BBCodeExtensionImport(int boardId, Stream imputStream)
        {
            int importedCount = 0;

            // import extensions...
            var dsBBCode = new DataSet();
            dsBBCode.ReadXml(imputStream);

            if (dsBBCode.Tables["YafBBCode"] != null && dsBBCode.Tables["YafBBCode"].Columns["Name"] != null &&
                dsBBCode.Tables["YafBBCode"].Columns["SearchRegex"] != null &&
                dsBBCode.Tables["YafBBCode"].Columns["ExecOrder"] != null)
            {
                var bbcodeList = CommonDb.BBCodeList(YafContext.Current.PageModuleID, boardId, null);

                // import any extensions that don't exist...
                foreach (var row in from DataRow row in dsBBCode.Tables["YafBBCode"].Rows
                                        let name = row["Name"].ToString()
                                        where bbcodeList.All(b => b.Name != name)
                                        select row)
                {
                    // add this bbcode...
                    CommonDb.bbcode_save(YafContext.Current.PageModuleID, null,
                        boardId,
                        row["Name"],
                        row["Description"],
                        row["OnClickJS"],
                        row["DisplayJS"],
                        row["EditJS"],
                        row["DisplayCSS"],
                        row["SearchRegex"],
                        row["ReplaceRegex"],
                        row["Variables"],
                        Convert.ToBoolean(row["UseModule"]),
                        row["ModuleClass"],
                        row["ExecOrder"]);
                    importedCount++;
                }
            }
            else
            {
                throw new Exception("Import stream is not expected format.");
            }

            return importedCount;
        }

        /// <summary>
        /// The file extension import.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="imputStream">
        /// The imput stream.
        /// </param>
        /// <returns>
        /// Returns How Many Extensions where imported.
        /// </returns>
        /// <exception cref="Exception">Import stream is not expected format.
        /// </exception>
        public static int FileExtensionImport(int boardId, Stream imputStream)
        {
            int importedCount = 0;

            var dsExtensions = new DataSet();
            dsExtensions.ReadXml(imputStream);

            if (dsExtensions.Tables["YafExtension"] != null &&
                dsExtensions.Tables["YafExtension"].Columns["Extension"] != null)
            {
                DataTable extensionList = CommonDb.extension_list((int?)YafContext.Current.PageModuleID, boardId);

                // import any extensions that don't exist...
                foreach (string ext in
                    dsExtensions.Tables["YafExtension"].Rows.Cast<DataRow>().Select(row => row["Extension"].ToString()).Where(
                    ext => extensionList.Select("Extension = '{0}'".FormatWith(ext)).Length == 0))
                {
                    // add this...
                    CommonDb.extension_save(YafContext.Current.PageModuleID, null, boardId, ext);
                    importedCount++;
                }
            }
            else
            {
                throw new Exception("Import stream is not expected format.");
            }

            return importedCount;
        }

        /// <summary>
        /// Topics the status import.
        /// </summary>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="imputStream">
        /// The imput stream.
        /// </param>
        /// <exception cref="Exception">
        /// Import stream is not expected format.
        /// </exception>
        /// <returns>
        /// Returns the Number of Imported Items.
        /// </returns>
        public static int TopicStatusImport(int boardId, Stream imputStream)
        {
            int importedCount = 0;

            // import extensions...
            var dsStates = new DataSet();
            dsStates.ReadXml(imputStream);

            if (dsStates.Tables["YafTopicStatus"] != null &&
                dsStates.Tables["YafTopicStatus"].Columns["TopicStatusName"] != null &&
                dsStates.Tables["YafTopicStatus"].Columns["DefaultDescription"] != null)
            {
                var topicStatusList = CommonDb.TopicStatus_List(YafContext.Current.PageModuleID, boardId);

                // import any topic status that don't exist...
                foreach (
                    DataRow row in
                        dsStates.Tables["YafTopicStatus"].Rows.Cast<DataRow>().Where(
                            row =>
                            topicStatusList.Select("TopicStatusName = '{0}'".FormatWith(row["TopicStatusName"])).Length ==
                            0))
                {
                    // add this...
                    CommonDb.TopicStatus_Save(YafContext.Current.PageModuleID, null, boardId, row["TopicStatusName"].ToString(), row["DefaultDescription"].ToString());
                    importedCount++;
                }
            }
            else
            {
                throw new Exception("Import stream is not expected format.");
            }

            return importedCount;
        }

        /// <summary>
        /// Import List of Banned Ip Adresses
        /// </summary>
        /// <param name="boardId">The board id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="imputStream">The imput stream.</param>
        /// <returns>
        /// Returns the Number of Imported Items.
        /// </returns>
        /// <exception cref="Exception">
        /// Import stream is not expected format.
        /// </exception>
        public static int BannedIpAdressesImport(int boardId, int userId, Stream imputStream)
        {
            int importedCount = 0;

            var existingBannedIPList = CommonDb.bannedip_list(YafContext.Current.PageModuleID, boardId, null, 0, 1000000);

            using (var streamReader = new StreamReader(imputStream))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    IPAddress importAddress;

                    if (string.IsNullOrEmpty(line) || !IPAddress.TryParse(line, out importAddress))
                    {
                        continue;
                    }

                    if (existingBannedIPList.Select("Mask = '{0}'".FormatWith(importAddress.ToString())).Length != 0)
                    {
                        continue;
                    }

                    CommonDb.bannedip_save(YafContext.Current.PageModuleID, null, boardId, importAddress.ToString(), "Imported IP Adress", userId);
                    importedCount++;
                }
            }
            
            /*else
            {
                throw new Exception("Import stream is not expected format.");
            }*/

            return importedCount;
        }
    }
}