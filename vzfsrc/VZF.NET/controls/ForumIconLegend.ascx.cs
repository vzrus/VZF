namespace VZF.Controls
{
  #region Using

  using System;

  using YAF.Core;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The forum icon legend.
  /// </summary>
  public partial class ForumIconLegend : BaseUserControl
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "ForumIconLegend" /> class.
    /// </summary>
    public ForumIconLegend()
    {
      this.PreRender += this.ForumIconLegend_PreRender;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the PreRender event of the ForumIconLegend control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void ForumIconLegend_PreRender([NotNull] object sender, [NotNull] EventArgs e)
    {
    }

    #endregion
  }
}