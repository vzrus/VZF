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
    using System.Drawing.Imaging;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        /// <summary>
        /// The get response captcha.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseCaptcha([NotNull] HttpContext context)
        {
#if (!DEBUG)
            try
            {
#endif

            var captchaImage =
                new CaptchaImage(
                    CaptchaHelper.GetCaptchaText(new HttpSessionStateWrapper(context.Session), context.Cache, true),
                    250,
                    50,
                    "Century Schoolbook");
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            captchaImage.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
 #if (!DEBUG)
           }
            catch (Exception x)
           {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, this.GetType().ToString(), x, 1);
                context.Response.Write(
                    "Error: Resource has been moved or is unavailable. Please contact the forum admin.");
           }

#endif
        }
    }
}