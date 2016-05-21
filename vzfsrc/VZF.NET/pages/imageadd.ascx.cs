using YAF.Classes;

namespace YAF.Pages
{
  #region Using

  using System;

  using VZF.Data.Common;

  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils;
  using VZF.Utils.Helpers;

    #endregion

  /// <summary>
  /// The cp_editavatar.
  /// </summary>
  public partial class imageadd : ForumPageRegistered
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="imageadd"/> class.
    /// </summary>
      public imageadd()
      : base("IMAGEADD")
    {
    }

    #endregion

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
        if (this.IsPostBack)
        {
            return;
        }

        this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
        if (this.Request.QueryString.GetFirstOrDefault("ti") != null)
        {
            var dt = CommonDb.topic_info(
                this.PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("ti").ToType<int>(), false, false);
            if (dt != null)
            {
                this.PageLinks.AddLink(
                    dt["Topic"].ToString(),
                    YafBuildLink.GetLink(ForumPages.posts, "t={0}".FormatWith(dt["TopicID"].ToString())));
            }
        }

        this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);
    }


    #endregion
  }
}