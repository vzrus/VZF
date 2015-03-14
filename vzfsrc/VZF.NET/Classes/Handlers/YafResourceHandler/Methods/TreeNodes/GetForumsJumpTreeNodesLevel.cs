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
                bool links = true;
                // show access masks list
                int? amdd = null;
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

                if (context.Request.QueryString.GetFirstOrDefault("links") != null)
                {
                    if (context.Request.QueryString.GetFirstOrDefault("links").ToType<int>() == 0)
                    {
                        links = false;
                    }
                    else
                    {
                        links = true;
                    }
                }
                else
                {
                    links = false;
                }

                if (context.Request.QueryString.GetFirstOrDefault("amdd") != null)
                {
                    amdd = context.Request.QueryString.GetFirstOrDefault("amdd").ToType<int>();               
                   
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (context.Request.QueryString.GetFirstOrDefault("tjl") != "-100")
                {

                    var forumUrl = context.Request.QueryString.GetFirstOrDefault("forumUrl");
                    var s = FancyTree.GetForumsJumpTreeNodesLevel(
                        context.Request.QueryString.GetFirstOrDefault("tjl"),
                        view,
                        access,
                        context.Request.QueryString.GetFirstOrDefault("active"),
                        boardFirst,
                        forumUrl,
                        links,
                        amdd).ToJson();
                    context.Response.Write(s);
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