namespace YAF
{
    #region Using

    using System;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Classes;

    using YAF.Core;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Extensions;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {

        // Written by vzrus.

        /// <summary>
        /// Gets the forum tree nodes info as a json string
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="forumId"> The forumId. </param>
        private void GetForumPath([NotNull] HttpContext context)
        {
            try
            {
                var userId = YafContext.Current.CurrentUserData.UserID;

                MembershipUser user = UserMembershipHelper.GetMembershipUserById(userId);

                if (user == null || user.ProviderUserKey.ToString() == "0")
                {
                    context.Response.Write(
                   "Error: Resource has been moved or is unavailable. Please contact the forum admin.");

                    return;
                }

                var forumId = context.Request.QueryString.GetFirstOrDefault("fp").ToType<int>();
                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                using (DataTable dtLinks = CommonDb.forum_listpath(YafContext.Current.PageModuleID, forumId))
                {
                    context.Response.Write(dtLinks.ToJson());
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