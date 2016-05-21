namespace YAF.Editors
{
  using YAF.Core;

  /// <summary>
  /// The free text box editorv 3.
  /// </summary>
  public class FreeTextBoxEditorv3 : FreeTextBoxEditor
  {
    #region Properties

    /// <summary>
    ///   Gets Description.
    /// </summary>
    public override string Description
    {
      get
      {
        return "Free Text Box v3 (HTML)";
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
        return "6";
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
        @"function insertsmiley(code,img){" + "FTB_API['" + this.SafeID +
        "'].InsertHtml('<img src=\"' + img + '\" alt=\"\" />');" + "}\n");
    }

    #endregion
  }
}