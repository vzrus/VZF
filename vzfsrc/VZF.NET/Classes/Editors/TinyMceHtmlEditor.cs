namespace YAF.Editors
{
   using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;

    /// <summary>
  /// The tiny mce html editor.
  /// </summary>
  public class TinyMceHtmlEditor : TinyMceEditor
  {
    #region Properties

    /// <summary>
    ///   Gets Description.
    /// </summary>
    [NotNull]
    public override string Description
    {
      get
      {
        return "TinyMCE (HTML)";
      }
    }

    /// <summary>
    ///   Gets ModuleId.
    /// </summary>
    public override string ModuleId
    {
      get
      {
        // backward compatibility...
        return "7";
      }
    }

    /// <summary>
    ///   Gets a value indicating whether UsesBBCode.
    /// </summary>
    public override bool UsesBBCode
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether UsesHTML.
    /// </summary>
    public override bool UsesHTML
    {
      get
      {
        return true;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The register smiliey script.
    /// </summary>
    protected override void RegisterSmilieyScript()
    {
      YafContext.Current.PageElements.RegisterJsBlock(
          "InsertSmileyJs",
          "function insertsmiley(code,img) {\n" +
          "	tinyMCE.execCommand('mceInsertContent',false,'<img src=\"' + img + '\" alt=\"\" />');\n" + "}\n");
    }

    /// <summary>
    /// The register tiny mce custom js.
    /// </summary>
    protected override void RegisterTinyMceCustomJS()
    {
        YafContext.Current.PageElements.RegisterJsBlock(
        "editorlang",
        @"var editorLanguage = ""{0}"";".FormatWith(YafContext.Current.CurrentUserData.CultureUser.IsSet() ? YafContext.Current.CurrentUserData.CultureUser.Substring(0, 2) : this.Get<YafBoardSettings>().Culture.Substring(0, 2)));

      YafContext.Current.PageElements.RegisterJsInclude("tinymceinit", this.ResolveUrl("tinymce/tinymce_init.js"));
    }

    #endregion
  }
}