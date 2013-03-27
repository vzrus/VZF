namespace VZF
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Xml;

    using VZF.Data.Common;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Constants;
    using YAF.Utils;

    public class SitemapHandler : IHttpHandler
    {
        protected enum ChangeFrequency
        {
            Always,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly,
            Never
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
         /*   if (context.Request.UrlReferrer == null ||
                !context.Request.UrlReferrer.AbsoluteUri.Contains(BaseUrlBuilder.BaseUrl)) return; */

            using (
                TextWriter textWriter = new StreamWriter(context.Response.OutputStream, System.Text.Encoding.UTF8))
            {
                var writer = new XmlTextWriter(textWriter) {Formatting = Formatting.Indented};
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:image", "http://www.google.com/schemas/sitemap-image/1.1");
                writer.WriteAttributeString("xmlns:video", "http://www.google.com/schemas/sitemap-video/1.1");

                //Add home page
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", YafForumInfo.ForumBaseUrl);
                writer.WriteElementString("lastmod",
                                          DateTime.Now.ToString("yyy-MM-dd",
                                                                System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", ChangeFrequency.Always.ToString().ToLowerInvariant());
                writer.WriteElementString("priority", "0.8");
                writer.WriteEndElement(); // url

                // Forums here
                var dt = CommonDb.forum_simplelist(YafContext.Current.PageModuleID, 1, 1000);
                foreach (DataRow r in dt.Rows)
                {
                    DateTime lp;
                    
                    writer.WriteStartElement("url");
                    writer.WriteElementString(
                        "loc", YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}", r["ForumID"]).Replace("sitemap.axd", Config.BaseScriptFile));
                    writer.WriteElementString(
                        "lastmod",
                        DateTime.TryParse(r["LastPosted"].ToString(), out lp) ? lp.ToString("yyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue.AddYears(1902).ToString("yyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("changefreq", ChangeFrequency.Always.ToString());
                    writer.WriteElementString("priority", "0.8");
                    writer.WriteEndElement(); // url
                }

                writer.WriteEndElement(); // urlset 

            }
            context.Response.ContentType = "text/xml";
        }

        #endregion
    }
}
