namespace YAF
{
    #region Using

    using System.Web;
    using System.Web.SessionState;
    using YAF.Classes;
    using YAF.Types;
  
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        #region Implemented Interfaces

        #region IHttpHandler

        /// <summary>
        /// The process request.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void ProcessRequest([NotNull] HttpContext context)
        {
            // resource no longer works with dynamic compile...
            if (context.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                // resource request
                GetResource(context);
            }
            else if (context.Session["lastvisit"] != null
                     ||
                     context.Request.UrlReferrer != null
                     && context.Request.UrlReferrer.AbsoluteUri.Contains(BaseUrlBuilder.BaseUrl))
            {
                // defaults
                var previewCropped = false;
                var localizationFile = "english.xml";

                if (context.Session["imagePreviewCropped"] is bool)
                {
                    previewCropped = (bool)context.Session["imagePreviewCropped"];
                }

                if (context.Session["localizationFile"] is string)
                {
                    localizationFile = context.Session["localizationFile"].ToString();
                }

                if (context.Session["localizationFile"] is string)
                {
                    localizationFile = context.Session["localizationFile"].ToString();
                }
                /////////////

                if (context.Request.QueryString.GetFirstOrDefault("userinfo") != null)
                {
                    this.GetUserInfo(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("u") != null)
                {
                    this.GetResponseLocalAvatar(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("ti") != null)
                {
                    if (context.Request.QueryString.GetFirstOrDefault("url") != null
                        && context.Request.QueryString.GetFirstOrDefault("width") != null
                        && context.Request.QueryString.GetFirstOrDefault("height") != null)
                    {
                        this.GetResponseRemoteTopicImage(context);
                    }
                    else
                    {
                        this.GetResponseLocalTopicImage(context);
                    }
                }
                else if (context.Request.QueryString.GetFirstOrDefault("ti") == null && context.Request.QueryString.GetFirstOrDefault("url") != null
                         && context.Request.QueryString.GetFirstOrDefault("width") != null
                         && context.Request.QueryString.GetFirstOrDefault("height") != null)
                {
                    this.GetResponseRemoteAvatar(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("a") != null)
                {
                    this.GetResponseAttachment(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("i") != null)
                {
                    // TommyB: Start MOD: Preview Images   ##########
                    this.GetResponseImage(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("p") != null)
                {
                    this.GetResponseImagePreview(
                        context, localizationFile, previewCropped);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("c") != null)
                {
                    // TommyB: End MOD: Preview Images   ##########
                    // captcha
                    this.GetResponseCaptcha(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("cover") != null
                         && context.Request.QueryString.GetFirstOrDefault("album") != null)
                {
                    // album cover
                    this.GetAlbumCover(context, localizationFile, previewCropped);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("imgprv") != null)
                {
                    // album image preview
                    this.GetAlbumImagePreview(context, localizationFile, previewCropped);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("image") != null)
                {
                    // album image
                    this.GetAlbumImage(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("s") != null
                         && context.Request.QueryString.GetFirstOrDefault("lang") != null)
                {
                    GetResponseGoogleSpell(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("fj") != null
                         && context.Request.QueryString.GetFirstOrDefault("node") == null)
                {
                    GetForumsJumpTreeNodesAll(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("tjl") != null || context.Request.QueryString.GetFirstOrDefault("tjls") != null)
                {
                    GetForumsJumpTreeNodesLevel(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("tnm") != null)
                {
                    GetForumsAdminTreeNodeMove(context);
                }
                else if (context.Request.QueryString.GetFirstOrDefault("fp") != null)
                {
                    GetForumPath(context);
                }

            }
            else
            {
                // they don't have a session...
                context.Response.Write(
                    "Please do not link directly to this resource. You must have a session in the forum.");
            }
        }

        #endregion

        #endregion

    }
}