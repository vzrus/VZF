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
    using VZF.Utils;
    using VZF.Utils.Extensions;

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
        private void GetForumsJumpTreeNodesLevel([NotNull] HttpContext context)
        {
            try
            {
                int access = 0;
                int view = 0;
                bool boardFirst = false;

                // var userId = YafContext.Current.CurrentUserData.UserID;
                if (context.Request.QueryString.GetFirstOrDefault("tjls") != null)
                {
                    access = context.Request.QueryString.GetFirstOrDefault("tjls").ToType<int>();
                }

                if (context.Request.QueryString.GetFirstOrDefault("active") != null)
                {
                    this.Get<IYafSession>().NntpTreeActiveNode = context.Request.QueryString.GetFirstOrDefault("active");
                }

                if (context.Request.QueryString.GetFirstOrDefault("selected") != null)
                {
                    this.Get<IYafSession>().SearchTreeSelectedNodes = context.Request.QueryString.GetFirstOrDefault("selected").Split('!');
                }

                if (context.Request.QueryString.GetFirstOrDefault("v") != null)
                {
                    view = context.Request.QueryString.GetFirstOrDefault("v").ToType<int>();
                    if (view == 0)
                    {
                        access = 1;
                    }
                }

                if (context.Request.QueryString.GetFirstOrDefault("root") != null)
                {
                    if (context.Request.QueryString.GetFirstOrDefault("root").ToType<int>() == 0)
                    {
                        boardFirst = true;
                    }
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (context.Request.QueryString.GetFirstOrDefault("tjl") != "-100")
                {
                    var forumUrl = context.Request.QueryString.GetFirstOrDefault("forumUrl");
                    context.Response.Write(
                        Dynatree.GetForumsJumpTreeNodesLevel(
                            context.Request.QueryString.GetFirstOrDefault("tjl"),
                            view,
                            access,
                            context.Request.QueryString.GetFirstOrDefault("active"),
                            boardFirst,
                            forumUrl).ToJson());
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