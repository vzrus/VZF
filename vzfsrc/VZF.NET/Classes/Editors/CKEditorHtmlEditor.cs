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
    public class CKEditorHtmlEditor : CKEditor
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
                return "CKEditor (HTML)";
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
                "function insertsmiley(code,img) {{\n var ckEditor = CKEDITOR.instances.{0};\nif ( ckEditor.mode == 'wysiwyg' ) {{\nckEditor.insertHtml( '<img src=\"' + img + '\" alt=\"\" />' ); }}\nelse alert( 'You must be on WYSIWYG mode!' );\n}}\n".FormatWith(this._textCtl.ClientID));
        }

        /// <summary>
        /// The register ckeditor custom js.
        /// </summary>
        protected override void RegisterCKEditorCustomJS()
        {
            YafContext.Current.PageElements.RegisterJsBlock(
        "teditorlang",
        @"var editorLanguage = ""{0}"";".FormatWith(YafContext.Current.CurrentUserData.CultureUser.IsSet() ? YafContext.Current.CurrentUserData.CultureUser.Substring(0, 2) : this.Get<YafBoardSettings>().Culture.Substring(0, 2)));

            YafContext.Current.PageElements.RegisterJsInclude("ckeditorinit", this.ResolveUrl("ckeditor/ckeditor_init.js"));
        }

        #endregion
    }
}