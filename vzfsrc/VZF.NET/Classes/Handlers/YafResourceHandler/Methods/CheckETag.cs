namespace YAF
{
    #region Using

    using System;
    using System.Net;
    using System.Web;
    using System.Web.SessionState;

    using YAF.Types;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        #region Methods

        /// <summary>
        /// Check if the ETag that sent from the client is match to the current ETag.
        ///   If so, set the status code to 'Not Modified' and stop the response.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="eTagCode">
        /// The e Tag Code.
        /// </param>
        /// <returns>
        /// The check e tag.
        /// </returns>
        private static bool CheckETag([NotNull] HttpContext context, [NotNull] string eTagCode)
        {
            string ifNoneMatch = context.Request.Headers["If-None-Match"];

            if (!eTagCode.Equals(ifNoneMatch, StringComparison.Ordinal))
            {
                return false;
            }

            context.Response.AppendHeader("Content-Length", "0");
            context.Response.StatusCode = (int)HttpStatusCode.NotModified;
            context.Response.StatusDescription = "Not modified";
            context.Response.SuppressContent = true;
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetETag(eTagCode);
            context.Response.Flush();
            return true;
        }

        #endregion
    }
}