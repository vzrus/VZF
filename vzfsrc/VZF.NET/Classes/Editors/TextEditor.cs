namespace YAF.Editors
{
  #region Using

  using System;
  using System.Web.UI.HtmlControls;

  using YAF.Core;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The text editor.
  /// </summary>
  public class TextEditor : ForumEditor
  {
    #region Constants and Fields

    /// <summary>
    ///   The _text ctl.
    /// </summary>
    protected HtmlTextArea _textCtl;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets a value indicating whether Active.
    /// </summary>
    public override bool Active
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Gets Description.
    /// </summary>
    [NotNull]
    public override string Description
    {
      get
      {
        return "Plain Text Editor";
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
        return "0";
      }
    }

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
        return false;
      }
    }

    /// <summary>
    ///   Gets SafeID.
    /// </summary>
    [NotNull]
    protected string SafeID
    {
      get
      {
        return this._textCtl.ClientID.Replace("$", "_");
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
    protected virtual void Editor_PreRender([NotNull] object sender, [NotNull] EventArgs e)
    {
      // Ederon : 9/6/2007
      /*if (this.Visible || this.)
			{*/
      YafContext.Current.PageElements.RegisterJsInclude("YafEditorJs", this.ResolveUrl("yafEditor/yafEditor.js"));

      YafContext.Current.PageElements.RegisterJsBlock(
        "CreateYafEditorJs", 
        "var " + this.SafeID + "=new yafEditor('" + this.SafeID + "');\n" + "function setStyle(style,option) {\n" + "	" +
        this.SafeID + ".FormatText(style,option);\n" + "}\n");

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
      this.PreRender += this.Editor_PreRender;

      this._textCtl = new HtmlTextArea();
      this._textCtl.ID = "YafTextEditor";
      this._textCtl.Rows = 15;
      this._textCtl.Cols = 100;
      this._textCtl.Attributes.Add("class", "YafTextEditor");

      this.AddEditorControl(this._textCtl);

      base.OnInit(e);
    }

    /// <summary>
    /// The register smiliey script.
    /// </summary>
    protected virtual void RegisterSmilieyScript()
    {
      YafContext.Current.PageElements.RegisterJsBlock(
        "InsertSmileyJs", "function insertsmiley(code) {\n" + "	" + this.SafeID + ".InsertSmiley(code);\n" + "}\n");
    }

    #endregion
  }
}