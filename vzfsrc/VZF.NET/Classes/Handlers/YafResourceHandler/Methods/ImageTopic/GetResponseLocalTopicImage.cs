// written by vzrus

using System.IO;
using System.Runtime.Remoting.Channels;
using System.Web.UI;
using VZF.Kernel;
using YAF.Classes;

namespace YAF
{
    #region Using

    using System;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;

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
        /// The get response local topic image.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseLocalTopicImage([NotNull] HttpContext context)
        {
            try
            {
                if (context.Request.QueryString.GetFirstOrDefault("full").IsSet() ||
                    context.Request.QueryString.GetFirstOrDefault("thumb").IsSet())
                {
                    byte[] data;

                    string path = null;
                    string name = string.Empty;
                    if (context.Request.QueryString.GetFirstOrDefault("full").IsSet())
                    {
                        name = context.Request.QueryString.GetFirstOrDefault("full");
                        path = ImagePathHelper.GetTopicImageUploadFullPath(
                            context.Request.QueryString.GetFirstOrDefault("ti").ToType<int>(),
                            context.Request.QueryString.GetFirstOrDefault("full"));
                    }
                    if (context.Request.QueryString.GetFirstOrDefault("thumb").IsSet())
                    {
                        name = context.Request.QueryString.GetFirstOrDefault("thumb");
                        path = ImagePathHelper.GetTopicImageUploadFullPath(
                            context.Request.QueryString.GetFirstOrDefault("ti").ToType<int>(),
                            HttpUtility.UrlDecode(context.Request.QueryString.GetFirstOrDefault("thumb")));
                    }

                    // use the new fileName (with extension) if it exists...
                    string fileName = File.Exists(path) ? path : string.Empty;

                    using (var input = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        data = new byte[input.Length];
                        input.Read(data, 0, data.Length);
                    }
                    string contentType = "image/jpeg";
                    if (context.Request.QueryString.GetFirstOrDefault("type").IsSet())
                    {
                        contentType = context.Request.QueryString.GetFirstOrDefault("type");
                    }
                    context.Response.ContentType = contentType;
                    context.Response.AppendHeader(
                        "Content-Disposition",
                        "topicimage; filename={0}".FormatWith(
                            HttpUtility.UrlPathEncode(name.Replace("+", "_"))));
                    context.Response.OutputStream.Write(data, 0, data.Length);
                }
                else
                {

                    var row = CommonDb.topic_info(
                        YafContext.Current.PageModuleID, context.Request.QueryString.GetFirstOrDefault("ti"), false,
                        false);

                    if (row != null)
                    {
                        var data = (byte[]) row["TopicImageBin"];
                        string contentType = row["TopicImageType"].ToString();

                        context.Response.Clear();
                        if (contentType.IsNotSet())
                        {
                            contentType = "image/jpeg";
                        }

                        context.Response.ContentType = contentType;
                        context.Response.Cache.SetCacheability(HttpCacheability.Public);
                        context.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(2));
                        context.Response.Cache.SetLastModified(DateTime.UtcNow);

                        // context.Response.Cache.SetETag( eTag );
                        context.Response.OutputStream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (Exception x)
            {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, this.GetType().ToString(), x,
                    EventLogTypes.Information);

                context.Response.Write(
                    "Error: Resource has been moved or is unavailable. Please contact the forum admin.");
            }


        }
    }
}