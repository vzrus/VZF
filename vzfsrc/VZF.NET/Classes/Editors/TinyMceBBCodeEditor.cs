namespace YAF.Editors
{
    using System.Web.UI;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Interfaces;

    /// <summary>
  /// The tiny mce bb code editor.
  /// </summary>
  public class TinyMceBBCodeEditor : TinyMceEditor
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
        return "TinyMCE (BBCode)";
      }
    }

    /// <summary>
    ///   Gets ModuleId.
    /// </summary>
    public override string ModuleId
    {
      get
      {
        return this.Description.GetHashCode().ToString();
      }
    }

    /// <summary>
    ///   Gets a value indicating whether UsesBBCode.
    /// </summary>
    public override bool UsesBBCode
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether UsesHTML.
    /// </summary>
    public override bool UsesHTML
    {
      get
      {
        return false;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The register smiliey script.
    /// </summary>
    protected override void RegisterSmilieyScript()
    {
      ScriptManager.RegisterClientScriptBlock(
        this.Page, 
        this.Page.GetType(), 
        "insertsmiley", 
        "function insertsmiley(code,img) {\n" + "	tinyMCE.execCommand('mceInsertContent',false,code);\n" + "}\n", 
        true);
    }

    /// <summary>
    /// The register tiny mce custom js.
    /// </summary>
    protected override void RegisterTinyMceCustomJS()
    {
        YafContext.Current.PageElements.RegisterJsBlock(
         "editorlang",
         @"var editorLanguage = ""{0}"";".FormatWith(YafContext.Current.CurrentUserData.CultureUser.IsSet() ? YafContext.Current.CurrentUserData.CultureUser.Substring(0, 2) : this.Get<YafBoardSettings>().Culture.Substring(0, 2)));

       ScriptManager.RegisterClientScriptInclude(
        this.Page, this.Page.GetType(), "tinymceinit", this.ResolveUrl("tinymce/tinymce_initbbcode.js"));
    }

    #endregion
  }
}