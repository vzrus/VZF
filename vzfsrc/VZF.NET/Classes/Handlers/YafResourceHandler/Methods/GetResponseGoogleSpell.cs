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