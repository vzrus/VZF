namespace YAF
{
  #region Using

  using System;
  using System.Web.UI;

  using YAF.Types;

  #endregion

  /// <summary>
  /// Summary description for error.
  /// </summary>
  public partial class error : Page
  {
    #region Methods

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      if (!this.IsPostBack)
      {
        string errorMessage = @"There has been a serious error loading the forum. No further information is available.";

        // show error message if one was provided...
        if (this.Session["StartupException"] != null)
        {
          errorMessage = "Startup Error: " + this.Session["StartupException"];
          this.Session["StartupException"] = null;
        }

        this.ErrorMsg.Text = Server.HtmlEncode(errorMessage) + "<br /><br />" +
                             "Please contact the administrator if this message persists.";
      }
    }

    #endregion
  }
}