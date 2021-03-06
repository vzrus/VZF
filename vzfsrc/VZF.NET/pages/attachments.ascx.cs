using System.Globalization;

namespace YAF.Pages
{
  // YAF.Pages
  #region Using

  using System;
  using System.Data;
  using System.IO;
  using System.Linq;
  using System.Web;
  using System.Web.UI.HtmlControls;
  using System.Web.UI.WebControls;

  using VZF.Data.Common;
  using VZF.Kernel;
  using VZF.Utils;

  using YAF.Classes;
  
  using YAF.Core;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Flags;
  using YAF.Types.Interfaces;

  using YAF.Types.Objects;

    #endregion

  /// <summary>
  /// The attachments Page Class.
  /// </summary>
  public partial class attachments : ForumPage
  {
    #region Constants and Fields

    /// <summary>
    ///   The _forum.
    /// </summary>
    private DataRow _forum;

    /// <summary>
    ///   The _topic.
    /// </summary>
    private DataRow _topic;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref = "attachments" /> class.
    /// </summary>
    public attachments()
      : base("ATTACHMENTS")
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The back_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Back_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
        string imageLink = null;
        if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ti").IsSet())
        {
            imageLink = "&ti={0}".FormatWith(this.Request.QueryString.GetFirstOrDefault("ti"));
        }
      if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ra").IsSet())
      {
        if (Config.IsRainbow)
        {
          YafBuildLink.Redirect(ForumPages.info, "i=1");
        }

        // string poll = string.Empty;
        string lnk;
        string fullflnk = string.Empty;
       
        if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f").IsSet())
        {
          lnk = this.Request.QueryString.GetFirstOrDefault("f");
          fullflnk = "f={0}&".FormatWith(lnk);
        }
        else
        {
          lnk = this.PageContext.PageForumID.ToString();
        }

        // Tell a user that his message will have to be approved by a moderator
        string url = YafBuildLink.GetLink(ForumPages.topics, "f={0}", lnk);

        // new topic variable
        if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t").IsSet())
        {
          url = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t");
          YafBuildLink.Redirect(ForumPages.polledit, "{0}t={1}&ra=1{2}", fullflnk, this.Server.UrlEncode(url), imageLink);
        }
        else
        {
          YafBuildLink.Redirect(ForumPages.info, "i=1&url={0}", this.Server.UrlEncode(url));
        }
      }

      // the post is already approved and we can view it
      if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t").IsSet())
      {
        YafBuildLink.Redirect(
          ForumPages.polledit, "t={0}{1}", this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t"), imageLink);
      }
      else
      {
          if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ti").IsSet())
          {
              YafBuildLink.Redirect(
         ForumPages.imageadd, "&ti={0}", this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ti"));
          }
          
          YafBuildLink.Redirect(
          ForumPages.posts, "m={0}#{0}", this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"));
      }
    }

    /// <summary>
    /// The delete_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
    {
      ((LinkButton)sender).Attributes["onclick"] = "return confirm('{0}')".FormatWith(this.GetText("ASK_DELETE"));
    }

    /// <summary>
    /// The list_ item command.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void List_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "delete":
          CommonDb.attachment_delete(PageContext.PageModuleID, e.CommandArgument);
          this.BindData();
          this.uploadtitletr.Visible = true;
          this.selectfiletr.Visible = true;
          break;
      }
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
      using (DataTable dt = CommonDb.forum_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this.PageContext.PageForumID))
      {
        this._forum = dt.Rows[0];
      }

      this._topic = CommonDb.topic_info(this.PageContext.PageModuleID, this.PageContext.PageTopicID, true, false);

      if (this.IsPostBack)
      {
        return;
      }
        
        int messageId;
        if (!int.TryParse(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"), out messageId))
        {
            YafBuildLink.AccessDenied();
        }
      if (!this.PageContext.ForumModeratorAccess && !this.PageContext.ForumUploadAccess)
      {
        YafBuildLink.AccessDenied();
      }

      if (!this.PageContext.ForumReadAccess)
      {
        YafBuildLink.AccessDenied();
      }

      // Ederon : 9/9/2007 - moderaotrs can attach in locked posts
      if (this._topic["Flags"].BinaryAnd(TopicFlags.Flags.IsLocked) && !this.PageContext.ForumModeratorAccess)
      {
        YafBuildLink.AccessDenied(/*"The topic is closed."*/);
      }

      if (this._forum["Flags"].BinaryAnd(ForumFlags.Flags.IsLocked))
      {
        YafBuildLink.AccessDenied(/*"The forum is closed."*/);
      }

      // Check that non-moderators only edit messages they have written
      if (!this.PageContext.ForumModeratorAccess)
      {
          var dt = CommonDb.MessageList(
              PageContext.PageModuleID, messageId).FirstOrDefault();
       
         if (dt == null || (dt.UserID != this.PageContext.PageUserID))
          {
            YafBuildLink.AccessDenied(/*"You didn't post this message."*/);
          }
      }

      if (this.PageContext.Settings.LockedForum == 0)
      {
        this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
        this.PageLinks.AddLink(
          this.PageContext.PageCategoryName, 
          YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
      }

      this.PageLinks.AddForumLinks(this.PageContext.PageForumID);
      this.PageLinks.AddLink(
        this.PageContext.PageTopicName, YafBuildLink.GetLink(ForumPages.posts, "t={0}", this.PageContext.PageTopicID));
      this.PageLinks.AddLink(this.GetText("TITLE"), string.Empty);

        this.Back.Text = this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("t").IsNotSet()
                             ? this.GetText("BACK")
                             : this.GetText("COMMON", "CONTINUE");

      this.Upload.Text = this.GetText("UPLOAD");

      // MJ : 10/14/2007 - list of allowed file extensions
      DataTable extensionTable = CommonDb.extension_list(PageContext.PageModuleID, this.PageContext.PageBoardID);

      string types = string.Empty;
      bool bFirst = true;

      foreach (DataRow row in extensionTable.Rows)
      {
        types += "{1}*.{0}".FormatWith(row["Extension"].ToString(), bFirst ? string.Empty : ", ");
        if (bFirst)
        {
          bFirst = false;
        }
      }

      if (types.IsSet())
      {
        this.ExtensionsList.Text = types;
      }

      if (this.Get<YafBoardSettings>().MaxFileSize > 0)
      {
        this.UploadNodePlaceHold.Visible = true;
        this.UploadNote.Text = this.GetTextFormatted(
          "UPLOAD_NOTE", (this.Get<YafBoardSettings>().MaxFileSize / 1024).ToString(CultureInfo.InvariantCulture));
      }
      else
      {
        this.UploadNodePlaceHold.Visible = false;
      }

      this.BindData();
    }

    /// <summary>
    /// The upload_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Upload_Click([NotNull] object sender, [NotNull] EventArgs e)
    {
      try
      {
        if (FileUploadHelper.CheckValidFile(this.File.PostedFile))
        {
          this.SaveAttachment(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"), this.File);
        }

        this.BindData();
      }
      catch (Exception x)
      {
        CommonDb.eventlog_create(PageContext.PageModuleID, this.PageContext.PageUserID, this, x);
        this.PageContext.AddLoadMessage(x.Message);
      }
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      DataTable dt = CommonDb.attachment_list(PageContext.PageModuleID, this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("m"), null, null,0,1000);
      this.List.DataSource = dt;

      this.List.Visible = dt.Rows.Count > 0;

      // show disallowed or allowed localized text depending on the Board Setting
      this.ExtensionTitle.LocalizedTag = this.Get<YafBoardSettings>().FileExtensionAreAllowed
                                           ? "ALLOWED_EXTENSIONS"
                                           : "DISALLOWED_EXTENSIONS";

      if (this.Get<YafBoardSettings>().MaxNumberOfAttachments > 0)
      {
        if (dt.Rows.Count > (this.Get<YafBoardSettings>().MaxNumberOfAttachments - 1))
        {
          this.uploadtitletr.Visible = false;
          this.selectfiletr.Visible = false;
        }
      }

      this.DataBind();
    }

    

    /// <summary>
    /// The save attachment.
    /// </summary>
    /// <param name="messageId">
    /// The message id.
    /// </param>
    /// <param name="file">
    /// The file.
    /// </param>
    private void SaveAttachment([NotNull] object messageId, [NotNull] HtmlInputFile file)
    {
      if (file.PostedFile == null || file.PostedFile.FileName.Trim().Length == 0 || file.PostedFile.ContentLength == 0)
      {
        return;
      }

      string filename = file.PostedFile.FileName;

      int pos = filename.LastIndexOfAny(new[] { '/', '\\' });
      if (pos >= 0)
      {
        filename = filename.Substring(pos + 1);
      }

      // filename can be only 255 characters long (due to table column)
      if (filename.Length > 255)
      {
        filename = filename.Substring(filename.Length - 255);
      }

      // verify the size of the attachment
      if (this.Get<YafBoardSettings>().MaxFileSize > 0 &&
          file.PostedFile.ContentLength > this.Get<YafBoardSettings>().MaxFileSize)
      {
        this.PageContext.AddLoadMessage(
          this.GetTextFormatted(
            "UPLOAD_TOOBIG", file.PostedFile.ContentLength / 1024, this.Get<YafBoardSettings>().MaxFileSize / 1024));

        return;
      }

      Stream instream = null;
  
      if (this.Get<YafBoardSettings>().UseFileTable)
      {
        instream = file.PostedFile.InputStream;        
      }
      else
      {
          string previousDirectory =
              this.Get<HttpRequestBase>().MapPath(
                  string.Concat(BaseUrlBuilder.ServerFileRoot, YafBoardFolders.Current.TopicAttachments));
         
          // check if Uploads folder exists
          if (!Directory.Exists(previousDirectory))
          {
              Directory.CreateDirectory(previousDirectory);
          }

        file.PostedFile.SaveAs("{0}/{1}.{2}.yafupload".FormatWith(previousDirectory, messageId, filename));     
      }

      CommonDb.attachment_save(PageContext.PageModuleID, messageId, filename, file.PostedFile.ContentLength, file.PostedFile.ContentType, instream);
    }

    #endregion
  }
}