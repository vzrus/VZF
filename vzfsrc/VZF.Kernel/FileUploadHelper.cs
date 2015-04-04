using System;
using System.IO;
using System.Web;
using VZF.Data.Common;
using VZF.Utils;
using YAF.Classes;
using YAF.Core;
using YAF.Types;
using YAF.Types.Interfaces;

namespace VZF.Kernel
{
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


            if (allowedExtensions == null || Array.IndexOf(allowedExtensions, extension) >= 0)
            {
                // remove the "period"
                extension = extension.Replace(".", string.Empty);

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
