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
        private void GetForumsAdminTreeNodeMove([NotNull] HttpContext context)
        {
            try
            {
                var userId = YafContext.Current.CurrentUserData.UserID;

                context.Response.Clear();

                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (context.Request.QueryString.GetFirstOrDefault("tnm") != null)
                {
                    if (this.Get<IYafSession>().ForumTreeChangerActiveNode.IsNotSet() || (this.Get<IYafSession>().ForumTreeChangerActiveTargetNode.IsSet() && this.Get<IYafSession>().ForumTreeChangerActiveNode.IsSet()))
                    {
                        this.Get<IYafSession>().ForumTreeChangerActiveNode =
                            context.Request.QueryString.GetFirstOrDefault("tnm");
                    }
                    else
                    {
                        this.Get<IYafSession>().ForumTreeChangerActiveTargetNode =
                          this.Get<IYafSession>().ForumTreeChangerActiveTargetNode;

                        this.Get<IYafSession>().ForumTreeChangerActiveTargetNode =
                           context.Request.QueryString.GetFirstOrDefault("tnm");
                    }
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