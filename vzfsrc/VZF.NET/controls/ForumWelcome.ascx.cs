namespace VZF.Controls
{
  #region Using

  using System;

  using VZF.Utils.Helpers;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// The forum welcome control which shows the current Time and the Last Visit Time of the Current User.
  /// </summary>
  public partial class ForumWelcome : BaseUserControl
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "ForumWelcome" /> class.
    /// </summary>
    public ForumWelcome()
    {
      this.PreRender += this.ForumWelcome_PreRender;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the PreRender event of the ForumWelcome control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void ForumWelcome_PreRender([NotNull] object sender, [NotNull] EventArgs e)
    {
      this.TimeNow.Text = this.GetTextFormatted(
        "Current_Time", this.Get<IDateTime>().FormatTime(DateTime.UtcNow));
        
      var lastVisit = this.Get<IYafSession>().LastVisit;

      if (lastVisit.HasValue && lastVisit.Value > DateTimeHelper.SqlDbMinTime())
      {
        this.TimeLastVisit.Visible = true;
        this.TimeLastVisit.Text = this.GetTextFormatted("last_visit", this.Get<IDateTime>().FormatDateTime(lastVisit.Value));
      }
      else
      {
        this.TimeLastVisit.Visible = false;
      }

      // tha_watcha Obsolete, we alread have notfications for that
      /*if (this.PageContext.UnreadPrivate > 0)
      {
        this.UnreadMsgs.Visible = true;
        this.UnreadMsgs.NavigateUrl = YafBuildLink.GetLink(ForumPages.cp_pm);
          this.UnreadMsgs.Text = this.GetTextFormatted(
              this.PageContext.UnreadPrivate == 1 ? "unread1" : "unread0", this.PageContext.UnreadPrivate);
      }*/
    }

    #endregion
  }
}