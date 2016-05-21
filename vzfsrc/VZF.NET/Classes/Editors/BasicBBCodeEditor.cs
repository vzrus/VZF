namespace YAF.Editors
{
  #region Using

  using System;

  using YAF.Types;

  #endregion

  /// <summary>
  /// The same as the TextEditor except it adds YafBBCode support. Used for QuickReply
  ///   functionality.
  /// </summary>
  public class BasicBBCodeEditor : TextEditor
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
        return "Basic YafBBCode Editor";
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
        return "5";
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

    #endregion

    #region Methods

    /// <summary>
    /// The on init.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnInit([NotNull] EventArgs e)
    {
      base.OnInit(e);
      this._textCtl.Attributes.Add("class", "basicBBCodeEditor");
    }

    #endregion
  }
}