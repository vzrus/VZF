namespace YAF
{
    #region Using

    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Net;
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
        #region Methods

        /// <summary>
        /// The get response remote topic.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseRemoteTopicImage([NotNull] HttpContext context)
        {
            if (General.GetCurrentTrustLevel() <= AspNetHostingPermissionLevel.Medium)
            {
                // don't bother... not supported.
                CommonDb.eventlog_create(
                    YafContext.Current.PageModuleID,
                    null,
                    this.GetType().ToString(),
                    "Remote Topic Image is NOT supported on your Hosting Permission Level (must be High)",
                    EventLogTypes.Error);
                return;
            }

            string avatarUrl = context.Request.QueryString.GetFirstOrDefault("url");

            int maxwidth = int.Parse(context.Request.QueryString.GetFirstOrDefault("width"));
            int maxheight = int.Parse(context.Request.QueryString.GetFirstOrDefault("height"));

            string eTag =
                @"""{0}""".FormatWith(
                    (context.Request.QueryString.GetFirstOrDefault("url") + maxheight + maxwidth).GetHashCode());

            if (CheckETag(context, eTag))
            {
                // found eTag... no need to download this image...
                return;
            }

            var webClient = new WebClient { Credentials = CredentialCache.DefaultCredentials };

            try
            {
                using (var avatarStream = webClient.OpenRead(avatarUrl))
                {
                    if (avatarStream == null)
                    {
                        return;
                    }

                    using (var img = new Bitmap(avatarStream))
                    {
                        int width = img.Width;
                        int height = img.Height;

                        if (width <= maxwidth && height <= maxheight)
                        {
                            context.Response.Redirect(avatarUrl);
                        }

                        if (width > maxwidth)
                        {
                            height = (height / (double)width * maxwidth).ToType<int>();
                            width = maxwidth;
                        }

                        if (height > maxheight)
                        {
                            width = (width / (double)height * maxheight).ToType<int>();
                            height = maxheight;
                        }

                        // Create the target bitmap
                        using (var bmp = new Bitmap(width, height))
                        {
                            // Create the graphics object to do the high quality resizing
                            using (var gfx = Graphics.FromImage(bmp))
                            {
                                gfx.CompositingQuality = CompositingQuality.HighQuality;
                                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                gfx.SmoothingMode = SmoothingMode.HighQuality;

                                // Draw the source image
                                gfx.DrawImage(img, new Rectangle(new Point(0, 0), new Size(width, height)));
                            }

                            // Output the data
                            context.Response.ContentType = "image/jpeg";
                            context.Response.Cache.SetCacheability(HttpCacheability.Public);
                            context.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(2));
                            context.Response.Cache.SetLastModified(DateTime.UtcNow);
                            context.Response.Cache.SetETag(eTag);
                            bmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (WebException)
            {
                // issue getting access to the avatar...
            }
        }

        #endregion

 
    }
}