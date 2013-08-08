// written by vzrus
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
                var row = CommonDb.topic_info(
                    YafContext.Current.PageModuleID, context.Request.QueryString.GetFirstOrDefault("ti"), false);

                if (row != null)
                {
                    var data = (byte[])row["TopicImageBin"];
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
            catch (Exception x)
            {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, this.GetType().ToString(), x, EventLogTypes.Information);

                context.Response.Write(
                    "Error: Resource has been moved or is unavailable. Please contact the forum admin.");
            }
        }
    }
}