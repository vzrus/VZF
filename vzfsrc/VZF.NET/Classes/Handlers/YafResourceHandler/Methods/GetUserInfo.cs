namespace YAF
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;
    using VZF.Types.Objects;

    using YAF.Classes;

    using VZF.Controls;
    using YAF.Core;
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
        /// <summary>
        /// Gets the user info as JSON string
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetUserInfo([NotNull] HttpContext context)
        {
            try
            {
                var userId = context.Request.QueryString.GetFirstOrDefault("userinfo").ToType<int>();

                var user = UserMembershipHelper.GetMembershipUserById(userId);

                if (user == null || user.ProviderUserKey.ToString() == "0")
                {
                    context.Response.Write(
                        "Error: Resource has been moved or is unavailable. Please contact the forum admin.");

                    return;
                }

                // Check if user has access
                if (!this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ProfileViewPermissions))
                {
                    context.Response.Write(string.Empty);

                    return;
                }

                var userData = new CombinedUserDataHelper(user, userId);

                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetExpires(
                    DateTime.UtcNow.AddMilliseconds(YafContext.Current.Get<YafBoardSettings>().OnlineStatusCacheTimeout));
                context.Response.Cache.SetLastModified(DateTime.UtcNow);

                var avatarUrl = this.Get<IAvatars>().GetAvatarUrlForUser(userId);

                avatarUrl = avatarUrl.IsNotSet()
                                ? "{0}images/noavatar.gif".FormatWith(YafForumInfo.ForumClientFileRoot)
                                : avatarUrl;

                var activeUsers = this.Get<IDataCache>().GetOrSet(
                    Constants.Cache.UsersOnlineStatus,
                    () =>
                    this.Get<IDBBroker>().GetActiveList(
                        false, YafContext.Current.Get<YafBoardSettings>().ShowCrawlersInActiveList),
                    TimeSpan.FromMilliseconds(YafContext.Current.Get<YafBoardSettings>().OnlineStatusCacheTimeout));

                var userIsOnline =
                    activeUsers.AsEnumerable().Any(
                        x => x.Field<int>("UserId").Equals(userId) && !x.Field<bool>("IsHidden"));

                var userName = this.Get<YafBoardSettings>().EnableDisplayName ? userData.DisplayName : userData.UserName;

                userName = HttpUtility.HtmlEncode(userName);

                var location = userData.Profile.Country.IsSet()
                                   ? YafContext.Current.Get<IHaveLocalization>().GetText(
                                       "COUNTRY", userData.Profile.Country.Trim())
                                   : userData.Profile.Location;

                if (userData.Profile.Region.IsSet() && userData.Profile.Country.IsSet())
                {
                    var tag = "RGN_{0}_{1}".FormatWith(userData.Profile.Country.Trim(), userData.Profile.Region);
                    location += ", {0}".FormatWith(YafContext.Current.Get<IHaveLocalization>().GetText("REGION", tag));
                }
                var forumUrl = HttpUtility.UrlDecode(context.Request.QueryString.GetFirstOrDefault("forumUrl"));

                if (Config.IsMojoPortal)
                {
                    forumUrl = forumUrl + "&g={0}&u={1}".FormatWith(ForumPages.pmessage, userId);
                }
                else
                {
                    forumUrl = forumUrl.Replace(".aspx", ".aspx?g={0}&u={1}".FormatWith(ForumPages.pmessage, userId));
                }

                var pmButton = new ThemeButton
                {
                    ID = "PM",
                    CssClass = "yafcssimagebutton",
                    TextLocalizedPage = "POSTS",
                    TextLocalizedTag = "PM",
                    ImageThemeTag = "PM",
                    TitleLocalizedTag = "PM_TITLE",
                    TitleLocalizedPage = "POSTS",
                    NavigateUrl = Config.IsAnyPortal ? forumUrl : YafBuildLink.GetLinkNotEscaped(ForumPages.pmessage, true, "u={0}", userId).Replace("resource.ashx", "default.aspx"),
                    ParamTitle0 = userName,
                    Visible =
                        !userData.IsGuest && this.Get<YafBoardSettings>().AllowPrivateMessages
                        && !userId.Equals(YafContext.Current.PageUserID) && !YafContext.Current.IsGuest
                };

                var userInfo = new YafUserInfo
                {
                    name = userName,
                    realname = HttpUtility.HtmlEncode(userData.Profile.RealName),
                    avatar = avatarUrl,
                    /* profilelink =
                         YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId).Replace(
                             "resource.ashx", "default.aspx"), */
                    interests = HttpUtility.HtmlEncode(userData.Profile.Interests),
                    homepage = userData.Profile.Homepage,
                    posts = "{0:N0}".FormatWith(userData.NumPosts),
                    rank = userData.RankName,
                    location = location,
                    joined = this.Get<IDateTime>().FormatDateLong(userData.Joined),
                    online = userIsOnline,
                    actionButtons = pmButton.RenderToString()
                };

                if (YafContext.Current.Get<YafBoardSettings>().EnableUserReputation)
                {
                    userInfo.points = (userData.Points.ToType<int>() > 0 ? "+" : string.Empty) + userData.Points;
                }

                context.Response.Write(userInfo.ToJson());

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