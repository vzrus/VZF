// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="FileUploadHelper.cs">
//   VZF by vzrus
//   Copyright (C) 2015 Vladimir Zakharov
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
//   The FileUploadHelper functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

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
