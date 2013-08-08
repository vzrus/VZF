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
   
    using System.IO;
    using System.Net;
    using System.Web;
    using System.Web.SessionState;
   
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
        /// The get response google spell.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private static void GetResponseGoogleSpell([NotNull] HttpContext context)
        {
            string url =
                "https://www.google.com/tbproxy/spell?lang={0}".FormatWith(
                    context.Request.QueryString.GetFirstOrDefault("lang"));

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.KeepAlive = true;
            webRequest.Timeout = 100000;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = context.Request.InputStream.Length;

            Stream requestStream = webRequest.GetRequestStream();

            context.Request.InputStream.CopyTo(requestStream);

            requestStream.Close();

            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            responseStream.CopyTo(context.Response.OutputStream);
        }
    }
}