namespace YAF.Editors
{
  using System.Web.UI;

  using YAF.Classes;
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

    /// <summary>
    /// The CKEditor bb code editor.
  /// </summary>
    public class CKEditorBBCodeEditor : CKEditor
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
          return "CKEditor (BBCode)";
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
        YafContext.Current.PageElements.RegisterJsBlock(
            "InsertSmileyJs",
            "function insertsmiley(code,img) {{\n var ckEditor = CKEDITOR.instances.{0};\nif ( ckEditor.mode == 'wysiwyg' ) {{\nckEditor.insertHtml( '[img]' + '{1}' + img + '[/img]' ); }}\nelse alert( 'You must be on WYSIWYG mode!' );\n}}\n".FormatWith(
            this._textCtl.ClientID, 
            BaseUrlBuilder.BaseUrl));
    }

    /// <summary>
    /// The register ckeditor custom js.
    /// </summary>
    protected override void RegisterCKEditorCustomJS()
    {
        YafContext.Current.PageElements.RegisterJsBlock(
        "editorlang",
        @"var editorLanguage = ""{0}"";".FormatWith(YafContext.Current.CurrentUserData.CultureUser.IsSet() ? YafContext.Current.CurrentUserData.CultureUser.Substring(0, 2) : this.Get<YafBoardSettings>().Culture.Substring(0, 2)));

        ScriptManager.RegisterClientScriptInclude(
        this.Page, this.Page.GetType(), "ckeditorinitbbcode", this.ResolveUrl("ckeditor/ckeditor_initbbcode.js"));
    }

    #endregion
  }
}