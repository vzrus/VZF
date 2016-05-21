namespace YAF.Modules
{
  using System.Web.UI;

  using VZF.Controls;

  /// <summary>
  /// The example bb code module.
  /// </summary>
  public class ExampleBBCodeModule : YafBBCodeControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleBBCodeModule"/> class.
    /// </summary>
    public ExampleBBCodeModule()
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
      writer.Write("Hello, you wrote this: " + Parameters["inner"]);
    }
  }
}