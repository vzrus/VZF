namespace YAF
{
    #region Using

    using System;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Core;
 
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Kernel;
    using VZF.Types.Constants;


    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {


        // Written by vzrus.

        /// <summary>
        /// Gets the forum tree nodes info as a JSON string
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void SetGroupAccess([NotNull] HttpContext context)
        {
            try
            {
                var userId = YafContext.Current.CurrentUserData.UserID;

                context.Response.Clear();

                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (context.Request.QueryString.GetFirstOrDefault("fgacc") != null)
                {

                    FancyTree.SetGroupAccess(context.Request.QueryString.GetFirstOrDefault("fid"), context.Request.QueryString.GetFirstOrDefault("gid"),
                                                 context.Request.QueryString.GetFirstOrDefault("fgacc"));
                }

                HttpContext.Current.ApplicationInstance.CompleteRequest();
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