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
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Net;
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
        /// The get response remote avatar.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseRemoteAvatar([NotNull] HttpContext context)
        {
            if (General.GetCurrentTrustLevel() <= AspNetHostingPermissionLevel.Medium)
            {
                // don't bother... not supported.
                CommonDb.eventlog_create(
                    YafContext.Current.PageModuleID,
                    null,
                    this.GetType().ToString(),
                    "Remote Avatar is NOT supported on your Hosting Permission Level (must be High)",
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
           
            var webClient = new WebClient();

            if (General.GetCurrentTrustLevel() > AspNetHostingPermissionLevel.Medium)
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;
            }

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
    }
}