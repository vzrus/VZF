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
        /// The get response local avatar.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseLocalAvatar([NotNull] HttpContext context)
        {
            try
            {
                using (DataTable dt = CommonDb.user_avatarimage(YafContext.Current.PageModuleID, context.Request.QueryString.GetFirstOrDefault("u")))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var data = (byte[])row["AvatarImage"];
                        string contentType = row["AvatarImageType"].ToString();

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