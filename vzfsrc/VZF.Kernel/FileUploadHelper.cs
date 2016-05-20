#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File FileUploadHelper.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20,.2016 in 2:56 PM.
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

namespace VZF.Kernel
{
    using System;
    using System.IO;
    using System.Web;

    using VZF.Data.Common;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;

    public static class FileUploadHelper
    {
        /// <summary>
        /// The check valid file.
        /// </summary>
        /// <param name="uploadedFile">
        /// The uploaded file.
        /// </param>
        /// <param name="allowedExtensions">
        /// The allowed extensions. If null all are allowed.
        /// </param>
        /// <returns>
        /// Returns if the File Is Valid
        /// </returns>
        public static bool CheckValidFileExtension([NotNull] HttpPostedFile uploadedFile, string[] allowedExtensions)
        {
            string filePath = uploadedFile.FileName.Trim();

            if (filePath.IsNotSet() || uploadedFile.ContentLength == 0)
            {
                return false;
            }

            string extension = Path.GetExtension(filePath).ToLower();

            bool bError = false;

            // remove the "period"
            extension = extension.Replace(".", string.Empty);

            if (allowedExtensions == null || Array.IndexOf(allowedExtensions, extension) >= 0)
            {
               

                // If we don't get a match from the db, then the extension is not allowed
                var dt = CommonDb.extension_list(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, extension);


                bool bInList = dt.Rows.Count > 0;


                if (YafContext.Current.Get<YafBoardSettings>().FileExtensionAreAllowed && !bInList)
                {
                    // since it's not in the list -- it's invalid
                    bError = true;
                }
                else if (!YafContext.Current.Get<YafBoardSettings>().FileExtensionAreAllowed && bInList)
                {
                    // since it's on the list -- it's invalid
                    bError = true;
                }

                if (filePath.Contains(";."))
                {
                    // IIS semicolon valnerabilty fix
                    bError = true;
                }

            }
            else
            {
                bError = true;
            }
            // also, check to see an image is being uploaded.
            if (!bError)
            {
                return true;
            }

            YafContext.Current.AddLoadMessage(YafContext.Current.Get<ILocalization>().GetTextFormatted("FILEERROR", extension));
            return false;
        }

        /// <summary>
        /// The check valid file.
        /// </summary>
        /// <param name="uploadedFile">
        /// The uploaded file.
        /// </param>
        /// <returns>
        /// Returns if the File Is Valid
        /// </returns>
        public static bool CheckValidImageFile([NotNull] HttpPostedFile uploadedFile)
        {
            string[] aImageExtensions = { "jpg", "gif", "png", "bmp" };
            return CheckValidFileExtension(uploadedFile, aImageExtensions);

        }

        /// <summary>
        /// The check valid file.
        /// </summary>
        /// <param name="uploadedFile">
        /// The uploaded file.
        /// </param>
        /// <returns>
        /// Returns if the File Is Valid
        /// </returns>
        public static bool CheckValidFile([NotNull] HttpPostedFile uploadedFile)
        {
            return CheckValidFileExtension(uploadedFile, null);
        }
    }
}
