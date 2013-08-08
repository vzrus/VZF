namespace YAF
{
    #region Using

    using System;

    using System.Text;
    using System.Web;

    using System.Web.SessionState;

    using VZF.Data.Common;
    using VZF.Kernel;

    using YAF.Core;

    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;


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
        /// <param name="context">The context.</param>
        private void GetForumsJumpTreeNodesAll([NotNull] HttpContext context)
        {
            try
            {
                var userId = YafContext.Current.CurrentUserData.UserID;

                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.Private);
                context.Response.Cache.SetExpires(
                    DateTime.UtcNow.AddMinutes(5));
                context.Response.Cache.SetLastModified(DateTime.UtcNow);
                context.Response.Write(Dynatree.GetAllUserAccessJumpTree(userId));

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