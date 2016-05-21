namespace YAF.Editors
{
  using System;
  using System.Reflection;

  using YAF.Core;
  using YAF.Types;

  /// <summary>
  /// The fck editor v 1.
  /// </summary>
  public class FCKEditorV1 : RichClassEditor
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "FCKEditorV1" /> class.
    /// </summary>
    public FCKEditorV1()
      : base("FredCK.FCKeditor,FredCK.FCKeditor")
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
        return "FCK Editor v1.6 (HTML)";
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
        return "4";
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
          PropertyInfo pInfo = this._typEditor.GetProperty("Value");
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
          PropertyInfo pInfo = this._typEditor.GetProperty("Value");
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
        PropertyInfo pInfo;
        pInfo = this._typEditor.GetProperty("BasePath");
        pInfo.SetValue(this._editor, this.ResolveUrl("FCKEditorV1/"), null);
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
        PropertyInfo pInfo = this._typEditor.GetProperty("ID");
        pInfo.SetValue(this._editor, "edit", null);
        this.Controls.Add(this._editor);
      }

      base.OnInit(e);
    }

    /// <summary>
    /// The On PreRender event.
    /// </summary>
    /// <param name="e">
    /// the Event Arguments
    /// </param>
    protected override void OnPreRender([NotNull] EventArgs e)
    {
      YafContext.Current.PageElements.RegisterJsInclude("FckEditorJs", this.ResolveUrl("FCKEditorV1/FCKEditor.js"));
    }

    #endregion
  }
}