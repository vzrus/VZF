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
        /// The get album image.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetAlbumImage([NotNull] HttpContext context)
        {
            try
            {
                string eTag = @"""{0}""".FormatWith(context.Request.QueryString.GetFirstOrDefault("image"));

                if (CheckETag(context, eTag))
                {
                    // found eTag... no need to resend/create this image -- just mark another view?
                    // VZF.Classes.Data.DB.album_image_download(YafContext.Current.PageModuleID,context.Request.QueryString.GetFirstOrDefault("image"));
                    return;
                }

                // ImageID
                using (
                    DataTable dt = CommonDb.album_image_list(YafContext.Current.PageModuleID, null, context.Request.QueryString.GetFirstOrDefault("image")))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        byte[] data;

                        string sUpDir = YafBoardFolders.Current.Albums;

                        string oldFileName =
                            context.Server.MapPath(
                                "{0}/{1}.{2}.{3}".FormatWith(sUpDir, row["UserID"], row["AlbumID"], row["FileName"]));
                        string newFileName =
                            context.Server.MapPath(
                                "{0}/{1}.{2}.{3}.yafalbum".FormatWith(
                                    sUpDir, row["UserID"], row["AlbumID"], row["FileName"]));

                        // use the new fileName (with extension) if it exists...
                        string fileName = File.Exists(newFileName) ? newFileName : oldFileName;

                        using (var input = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            data = new byte[input.Length];
                            input.Read(data, 0, data.Length);
                            input.Close();
                        }

                        context.Response.ContentType = row["ContentType"].ToString();

                        // context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        // context.Response.Cache.SetETag(eTag);
                        context.Response.OutputStream.Write(data, 0, data.Length);

                        // add a download count...
                        CommonDb.album_image_download(YafContext.Current.PageModuleID, context.Request.QueryString.GetFirstOrDefault("image"));
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