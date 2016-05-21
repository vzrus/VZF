namespace YAF.Editors
{
    #region Using

    using System;
    using System.Web.UI;

    using YAF.Types;

    #endregion

    /// <summary>
    /// The tiny mce editor.
    /// </summary>
    public abstract class CKEditor : TextEditor
    {
        #region Properties

        /// <summary>
        ///   Gets or sets Text.
        /// </summary>
        public override string Text
        {
            get
            {
                return this._textCtl.InnerText;
            }

            set
            {
                this._textCtl.InnerText = value;
            }
        }

        /// <summary>
        ///   Gets SafeID.
        /// </summary>
        [NotNull]
        protected new string SafeID
        {
            get
            {
                return this._textCtl.ClientID.Replace("$", "_");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The editor_ PreRender.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void Editor_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(
              this.Page, this.Page.GetType(), "ckeditor", this.ResolveUrl("ckeditor/ckeditor.js"));
            this.RegisterCKEditorCustomJS();
            this.RegisterSmilieyScript();
        }

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            base.OnInit(e);

            this._textCtl.Attributes.CssStyle.Add("width", "100%");
            this._textCtl.Attributes.CssStyle.Add("height", "350px");
        }

        /// <summary>
        /// The register ckeditor custom js.
        /// </summary>
        protected abstract void RegisterCKEditorCustomJS();

        #endregion
    }
}