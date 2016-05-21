namespace VZF.Controls
{
  #region Using

  using System;
  using System.Collections;
  using System.Data;

  using YAF.Core;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The forum moderator list.
  /// </summary>
  public partial class ForumModeratorList : BaseUserControl
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "ForumModeratorList" /> class.
    /// </summary>
    public ForumModeratorList()
    {
      this.PreRender += this.ForumModeratorList_PreRender;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Sets DataSource.
    /// </summary>
    public IEnumerable DataSource
    {
      set
      {
        this.ModeratorList.DataSource = value;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The forum moderator list_ pre render.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ForumModeratorList_PreRender([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (((DataRow[])this.ModeratorList.DataSource).Length > 0)
      {
        // no need for the "blank dash"...
        this.BlankDash.Visible = false;
      }
    }

    #endregion
  }
}