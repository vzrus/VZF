namespace YAF
{
    #region Using

    using System;
    using System.Drawing.Imaging;
    using System.Web;
    using System.Web.SessionState;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Types;
   using  YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        /// <summary>
        /// The get response captcha.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void GetResponseCaptcha([NotNull] HttpContext context)
        {
#if (!DEBUG)
            try
            {
#endif

            var captchaImage =
                new CaptchaImage(
                    CaptchaHelper.GetCaptchaText(new HttpSessionStateWrapper(context.Session), context.Cache, true),
                    250,
                    50,
                    "Century Schoolbook");
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            captchaImage.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
 #if (!DEBUG)
           }
            catch (Exception x)
           {
                CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, this.GetType().ToString(), x, EventLogTypes.Error);
                context.Response.Write(
                    "Error: Resource has been moved or is unavailable. Please contact the forum admin.");
           }

#endif
        }
    }
}