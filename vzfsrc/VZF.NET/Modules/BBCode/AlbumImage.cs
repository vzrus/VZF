namespace YAF.Modules.BBCode
{
    using System.Text;
    using System.Web.UI;

    using VZF.Controls;
    using VZF.Utils;

    /// <summary>
    /// The Album Image BB Code Module.
    /// </summary>
    public class AlbumImage : YafBBCodeControl
    {
        /// <summary>
        /// Render The Album Image as Link with Image
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void Render(HtmlTextWriter writer)
        {
            var sb = new StringBuilder();
            
            sb.AppendFormat(
                @"<a href=""{0}resource.ashx?image={1}"" class=""ceebox"">",
                YafForumInfo.ForumClientFileRoot,
                Parameters["inner"]);

            sb.AppendFormat(
                @"<img src=""{0}resource.ashx?imgprv={1}"" />",
                YafForumInfo.ForumClientFileRoot,
                Parameters["inner"]);

            sb.Append("</a>");

            writer.Write(sb.ToString());
        }
    }
}