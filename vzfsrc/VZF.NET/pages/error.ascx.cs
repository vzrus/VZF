namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Web;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Interfaces;
  using VZF.Utils;

  #endregion

  /// <summary>
  /// Summary description for error.
  /// </summary>
  public partial class error : ForumPage
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "error" /> class.
    /// </summary>
    public error()
      : base("ERROR")
    {
      this.NoDataBase = true;
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
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      this.InitializeComponent();
      base.OnInit(e);
    }

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
      this.errormsg.InnerText =
        "An error has occured in '{0}'.".FormatWith(
          this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("aspxerrorpath"));
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    #endregion
  }
}