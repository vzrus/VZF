/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using YAF.Classes.Data;

namespace YAF.Core
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;

    using YAF.Utils;

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