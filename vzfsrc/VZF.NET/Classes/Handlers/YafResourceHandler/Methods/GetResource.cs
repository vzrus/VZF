namespace YAF
{
    #region Using

    using System.Web;
    using System.Web.SessionState;

    using YAF.Types;
    using YAF.Types.Interfaces;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        /// <summary>
        /// The get resource.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private static void GetResource([NotNull] HttpContext context)
        {
            // redirect to the resource?
            context.Response.Redirect("resources/{0}".FormatWith(context.Request.QueryString.GetFirstOrDefault("r")));

            /*string resourceName = "YAF.App_GlobalResources." + context.Request.QueryString["r"];
            int lastIndex = resourceName.LastIndexOf('.');
            string extension = resourceName.Substring(lastIndex, resourceName.Length - lastIndex).ToLower();

            string resourceType = "text/plain";

            switch (extension)
            {
                case ".js":
                    resourceType = "application/x-javascript";
                    break;
                case ".css":
                    resourceType = "text/css";
                    break;
            }

            if (resourceType != string.Empty)
            {
                context.Response.Clear();
                context.Response.ContentType = resourceType;

                try
                {
                    // attempt to load the resource...
                    byte[] data = null;

                    Stream input = GetType().Assembly.GetManifestResourceStream(resourceName);

                    data = new byte[input.Length];
                    input.Read(data, 0, data.Length);
                    input.Close();

                    context.Response.OutputStream.Write(data, 0, data.Length);
                }
                catch
                {
                    YAF.Classes.Data.DB.eventlog_create(
                        null, GetType().ToString(), "Attempting to access invalid resource: " + resourceName, 1);
                    context.Response.Write("Error: Invalid forum resource. Please contact the forum admin.");
                }
            }*/
        }
    }
}