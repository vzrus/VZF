﻿namespace YAF
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
        /// The get album cover.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="localizationFile">
        /// The localization file.
        /// </param>
        /// <param name="previewCropped">
        /// if set to <c>true</c>  [preview cropped].
        /// </param>
        private void GetAlbumCover(
            [NotNull] HttpContext context,
            string localizationFile,
            bool previewCropped)
        {
            var eTag = @"""{0}{1}""".FormatWith(
                context.Request.QueryString.GetFirstOrDefault("cover"), localizationFile.GetHashCode());

            if (CheckETag(context, eTag))
            {
                // found eTag... no need to resend/create this image...
                return;
            }

            try
            {
                // CoverID
                string fileName = string.Empty;
                var data = new MemoryStream();
                if (context.Request.QueryString.GetFirstOrDefault("cover") == "0")
                {
                    fileName =
                        context.Server.MapPath(
                            "{0}/images/{1}".FormatWith(YafForumInfo.ForumClientFileRoot, "noCover.png"));
                }
                else
                {
                    using (
                        DataTable dt = CommonDb.album_image_list(YafContext.Current.PageModuleID, null, context.Request.QueryString.GetFirstOrDefault("cover")))
                    {

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            string sUpDir = YafBoardFolders.Current.Albums;

                            string oldFileName =
                                context.Server.MapPath(
                                    "{0}/{1}.{2}.{3}".FormatWith(sUpDir, row["UserID"], row["AlbumID"], row["FileName"]));
                            string newFileName =
                                context.Server.MapPath(
                                    "{0}/{1}.{2}.{3}.yafalbum".FormatWith(
                                        sUpDir, row["UserID"], row["AlbumID"], row["FileName"]));

                            // use the new fileName (with extension) if it exists...
                            fileName = File.Exists(newFileName) ? newFileName : oldFileName;
                        }
                    }
                }

                using (var input = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var buffer = new byte[input.Length];
                    input.Read(buffer, 0, buffer.Length);
                    data.Write(buffer, 0, buffer.Length);
                    input.Close();
                }

                // reset position...
                data.Position = 0;
                var imagesNumber =
                    CommonDb.album_getstats(YafContext.Current.PageModuleID, null, context.Request.QueryString.GetFirstOrDefault("album"))[1];
                var ms = GetAlbumOrAttachmentImageResized(
                    data, 200, 200, previewCropped, imagesNumber, localizationFile, "ALBUM");

                context.Response.ContentType = "image/png";

                // output stream...
                context.Response.OutputStream.Write(ms.ToArray(), 0, (int)ms.Length);
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(2));
                context.Response.Cache.SetLastModified(DateTime.UtcNow);
                context.Response.Cache.SetETag(eTag);

                data.Dispose();
                ms.Dispose();
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