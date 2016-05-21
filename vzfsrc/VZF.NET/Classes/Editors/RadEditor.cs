namespace YAF.Editors
{
  #region Using

  using System;
  using System.Reflection;
  using System.Web.UI.WebControls;

  using YAF.Classes;
  using YAF.Core;
  using YAF.Types;

  #endregion

  #region "Telerik RadEditor"

  /// <summary>
  /// The rad editor.
  /// </summary>
  public class RadEditor : RichClassEditor
  {
    // base("Namespace,AssemblyName")
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "RadEditor" /> class.
    /// </summary>
    public RadEditor()
      : base("Telerik.Web.UI.RadEditor,Telerik.Web.UI")
    {
      this.InitEditorObject();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets Description.
    /// </summary>
    [NotNull]
    public override string Description
    {
      get
      {
        return "Telerik RAD Editor (HTML)";
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
        return "8";
      }
    }

    /// <summary>
    ///   Gets or sets Text.
    /// </summary>
    [NotNull]
    public override string Text
    {
      get
      {
        if (this._init)
        {
          PropertyInfo pInfo = this._typEditor.GetProperty("Html");
          return Convert.ToString(pInfo.GetValue(this._editor, null));
        }
        else
        {
          return string.Empty;
        }
      }

      set
      {
        if (this._init)
        {
          PropertyInfo pInfo = this._typEditor.GetProperty("Html");
          pInfo.SetValue(this._editor, value, null);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The editor_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected virtual void Editor_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (this._init && this._editor.Visible)
      {
        PropertyInfo pInfo = this._typEditor.GetProperty("ID");
        pInfo.SetValue(this._editor, "edit", null);
        pInfo = this._typEditor.GetProperty("Skin");

        pInfo.SetValue(this._editor, Config.RadEditorSkin, null);
        pInfo = this._typEditor.GetProperty("Height");

        pInfo.SetValue(this._editor, Unit.Pixel(400), null);
        pInfo = this._typEditor.GetProperty("Width");

        pInfo.SetValue(this._editor, Unit.Percentage(100), null);

        if (Config.UseRadEditorToolsFile)
        {
          pInfo = this._typEditor.GetProperty("ToolsFile");
          pInfo.SetValue(this._editor, Config.RadEditorToolsFile, null);
        }

        // Add Editor
        this.AddEditorControl(this._editor);
      }
    }

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      if (this._init)
      {
        this.Load += this.Editor_Load;
        base.OnInit(e);
      }
    }

    /// <summary>
    /// The On PreRender event.
    /// </summary>
    /// <param name="e">
    /// the Event Arguments
    /// </param>
    protected override void OnPreRender([NotNull] EventArgs e)
    {
      // Register smiley JavaScript
      this.RegisterSmilieyScript();
      base.OnPreRender(e);
    }

    /// <summary>
    /// The register smiliey script.
    /// </summary>
    protected virtual void RegisterSmilieyScript()
    {
      YafContext.Current.PageElements.RegisterJsBlock(
        "InsertSmileyJs", 
        @"function insertsmiley(code,img){" + "\n" + "var editor = $find('" + this._editor.ClientID + "');" +
        "editor.pasteHtml('<img src=\"' + img + '\" alt=\"\" />');\n" + "}\n");
    }

    #endregion
  }

  #endregion
}