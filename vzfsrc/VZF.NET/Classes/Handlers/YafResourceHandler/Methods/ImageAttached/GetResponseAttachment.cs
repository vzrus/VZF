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
        /// The get response attachment.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseAttachment([NotNull] HttpContext context)
        {
            try
            {
                // AttachmentID
                using (
                    DataTable dt = CommonDb.attachment_list(YafContext.Current.PageModuleID, null, context.Request.QueryString.GetFirstOrDefault("a"), null, 0, 1000))
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

                        byte[] data;

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
                                data = new byte[input.Length];
                                input.Read(data, 0, data.Length);
                            }
                        }
                        else
                        {
                            data = (byte[])row["FileData"];
                        }

                        context.Response.ContentType = row["ContentType"].ToString();
                        context.Response.AppendHeader(
                            "Content-Disposition",
                            "attachment; filename={0}".FormatWith(
                                HttpUtility.UrlPathEncode(row["FileName"].ToString()).Replace("+", "_")));
                        context.Response.OutputStream.Write(data, 0, data.Length);
                        CommonDb.attachment_download(YafContext.Current.PageModuleID, context.Request.QueryString.GetFirstOrDefault("a"));
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