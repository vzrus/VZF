using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types.Constants;
using YAF.Utils;

namespace YAF
{
    public class SitemapHandler : IHttpHandler
    {
        protected enum ChangeFrequency
        {
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            using (TextWriter textWriter = new StreamWriter(context.Response.OutputStream, System.Text.Encoding.UTF8))
            {
                var writer = new XmlTextWriter(textWriter) {Formatting = Formatting.Indented};
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                writer.WriteAttributeString("xmlns:image", "http://www.google.com/schemas/sitemap-image/1.1");
                writer.WriteAttributeString("xmlns:video", "http://www.google.com/schemas/sitemap-video/1.1");

                //Add home page
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", "http://forum.yetanotherforum.net/");
                writer.WriteElementString("lastmod",
                                          DateTime.Now.ToString("yyy-MM-dd",
                                                                System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteElementString("changefreq", ChangeFrequency.always.ToString());
                writer.WriteElementString("priority", "0.8");
                writer.WriteEndElement(); // url

                // Forums here
               var dt = CommonDb.forum_simplelist(YafContext.Current.PageModuleID, 1,10);
                foreach (DataRow r in dt.Rows)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}", r["ForumID"]));
                    writer.WriteElementString("lastmod",
                                              DateTime.Now.ToString("yyy-MM-dd",
                                                                    System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteElementString("changefreq", ChangeFrequency.always.ToString());
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
