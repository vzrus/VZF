/* YetAnotherForum.NET
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

namespace YAF
{
    #region Using

    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Classes;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {

        /// <summary>
        /// Gest the Preview Image as Response
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="localizationFile">The localization file.</param>
        /// <param name="previewCropped">if set to <c>true</c> [preview cropped].</param>
        private void GetResponseImagePreview(
            [NotNull] HttpContext context,
            string localizationFile,
            bool previewCropped)
        {
            var eTag = @"""{0}{1}""".FormatWith(
                context.Request.QueryString.GetFirstOrDefault("p"), localizationFile.GetHashCode());

            if (CheckETag(context, eTag))
            {
                // found eTag... no need to resend/create this image...
                return;
            }

            // defaults
            var previewMaxWidth = 200;
            var previewMaxHeight = 200;

            if (context.Session["imagePreviewWidth"] is int)
            {
                previewMaxWidth = (int)context.Session["imagePreviewWidth"];
            }

            if (context.Session["imagePreviewHeight"] is int)
            {
                previewMaxHeight = (int)context.Session["imagePreviewHeight"];
            }

            try
            {
                // AttachmentID
                using (
                    DataTable dt = CommonDb.attachment_list(YafContext.Current.PageModuleID, null, context.Request.QueryString.GetFirstOrDefault("p"), null, 0, 1000))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // TODO : check download permissions here
                        if (!this.CheckAccessRights(row["BoardID"], row["MessageID"]))
                        {
                            // tear it down
                            // no permission to download
                            context.Response.Write(
                                "You have insufficient rights to download this resource. Contact forum administrator for further details.");
                            return;
                        }

                        var data = new MemoryStream();

                        if (row.IsNull("FileData"))
                        {
                            string sUpDir = YafBoardFolders.Current.TopicAttachments;

                            string oldFileName =
                                context.Server.MapPath(
                                    "{0}/{1}.{2}".FormatWith(sUpDir, row["MessageID"], row["FileName"]));
                            string newFileName =
                                context.Server.MapPath(
                                    "{0}/{1}.{2}.yafupload".FormatWith(sUpDir, row["MessageID"], row["FileName"]));

                            // use the new fileName (with extension) if it exists...
                            string fileName = File.Exists(newFileName) ? newFileName : oldFileName;

                            using (var input = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                var buffer = new byte[input.Length];
                                input.Read(buffer, 0, buffer.Length);
                                data.Write(buffer, 0, buffer.Length);
                                input.Close();
                            }
                        }
                        else
                        {
                            var buffer = (byte[])row["FileData"];
                            data.Write(buffer, 0, buffer.Length);
                        }

                        // reset position...
                        data.Position = 0;

                        MemoryStream ms = GetAlbumOrAttachmentImageResized(
                            data,
                            previewMaxWidth,
                            previewMaxHeight,
                            previewCropped,
                            (int)row["Downloads"],
                            localizationFile,
                            "POSTS");

                        context.Response.ContentType = "image/png";

                        // output stream...
                        context.Response.OutputStream.Write(ms.ToArray(), 0, (int)ms.Length);
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        context.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(2));
                        context.Response.Cache.SetLastModified(DateTime.UtcNow);
                        context.Response.Cache.SetETag(eTag);

                        data.Dispose();
                        ms.Dispose();

                        break;
                    }
                }
            }
            catch (Exception x)
            {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, this.GetType().ToString(), x, EventLogTypes.Information);

                context.Response.Write(
                    "Error: Resource has been moved or is unavailable. Please contact the forum admin.");
            }
        }
    }
}