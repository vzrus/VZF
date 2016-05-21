namespace YAF.Modules
{
  using System.Text;
  using System.Web.UI;

  using YAF.Core; 
  using YAF.Types.Interfaces; 
  using VZF.Controls;
  using YAF.Types.Constants;

  /// <summary>
  /// The spoiler bb code module.
  /// </summary>
  public class SpoilerBBCodeModule : YafBBCodeControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SpoilerBBCodeModule"/> class.
    /// </summary>
    public SpoilerBBCodeModule()
      : base()
    {
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      var sb = new StringBuilder();

      string spoilerTitle = this.HtmlEncode(LocalizedString("SPOILERMOD_TOOLTIP", "Click here to show or hide the hidden text (also known as a spoiler)"));

      sb.AppendLine("<!-- BEGIN spoiler -->");
      sb.AppendLine(@"<div class=""spoilertitle"">");
      sb.AppendFormat(
        @"<input type=""button"" value=""{2}"" class=""spoilerbutton"" name=""{0}"" onclick='toggleSpoiler(this,""{1}"");' title=""{3}"" /></div><div class=""spoilerbox"" id=""{1}"" style=""display:none"">", 
        this.GetUniqueID("spoilerBtn"), 
        this.GetUniqueID("spoil_"), 
        this.HtmlEncode(LocalizedString("SPOILERMOD_SHOW", "Show Spoiler")), 
        spoilerTitle);
      sb.AppendLine(Parameters["inner"]);
      sb.AppendLine("</div>");
      sb.AppendLine("<!-- END spoiler -->");

      writer.Write(sb.ToString());
    }
  }
}