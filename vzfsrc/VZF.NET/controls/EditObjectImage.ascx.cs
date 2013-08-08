// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="EditObjectImage.ascx.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The EditObjectImage.ascx.cs is mostly based on EditAvatarImage.ascx.cs.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.UI.HtmlControls;

    using VZF.Data.Common;

    using YAF.Classes;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The edit users avatar.
    /// </summary>
    public partial class EditObjectImage : BaseUserControl
    {
        #region Constants and Fields

        /// <summary>
        ///   The admin edit mode.
        /// </summary>
        private bool _adminEditMode;

        /// <summary>
        ///   The current user id.
        /// </summary>
        private int _currentUserID;

        /// <summary>
        /// The _current topic id.
        /// </summary>
        private int _currentTopicId;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets a value indicating whether InAdminPages.
        /// </summary>
        public bool InAdminPages
        {
            get
            {
                return this._adminEditMode;
            }

            set
            {
                this._adminEditMode = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cancel Editing
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Back_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var ti = this.Request.QueryString.GetFirstOrDefault("ti");
            int tii;

            // check if it's a link from topic edit
            if (ti != null && int.TryParse(this.Request.QueryString.GetFirstOrDefault("ti"), out tii))
            {
                YafBuildLink.Redirect(ForumPages.posts, "t={0}".FormatWith(tii));
            }
        }

        /// <summary>
        /// Delete The Current TopicImage
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DeleteImage_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.DeleteFiles();
            
            CommonDb.topic_imagesave(
                PageContext.PageModuleID, this.PageContext.QueryIDs["ti"].ToType<int>(), null, null, null);

            this.BindData();
        }

        /// <summary>
        /// The delete files.
        /// </summary>
        private void DeleteFiles()
        {
            var ti = this.Request.QueryString.GetFirstOrDefault("ti");
            int tii;

            if (ti == null || !int.TryParse(this.Request.QueryString.GetFirstOrDefault("ti"), out tii))
            {
                return;
            }

            DataRow row = CommonDb.topic_info(
                this.PageContext.PageModuleID, this.PageContext.QueryIDs["ti"].ToType<int>(), false);
            if (row != null)
            {
                string topicImageName = row["TopicImage"].ToString();
                string uploadDir =
                    HostingEnvironment.MapPath(
                        string.Concat(
                            BaseUrlBuilder.ServerFileRoot,
                            YafBoardFolders.Current.Uploads,
                            "/",
                            YafBoardFolders.Current.Topics));

                string fileName = string.Format(
                    "{0}/{1}.{2}.yafupload",
                    uploadDir,
                    this.PageContext.QueryIDs["ti"].ToType<int>(),
                    topicImageName);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                string fileNameThumb = string.Format(
                    "{0}/{1}.thumb.{2}.yafupload",
                    uploadDir,
                    this.PageContext.QueryIDs["ti"].ToType<int>(),
                    topicImageName);
                if (System.IO.File.Exists(fileNameThumb))
                {
                    System.IO.File.Delete(fileNameThumb);
                }
            }

            this.TopicImg1.Src = null;
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            // Setup Ceebox js
            YafContext.Current.PageElements.RegisterJsResourceInclude("ceeboxjs", "js/jquery.ceebox.js");
            YafContext.Current.PageElements.RegisterCssIncludeResource("css/jquery.ceebox.css");
            YafContext.Current.PageElements.RegisterJsBlock("ceeboxloadjs", JavaScriptBlocks.CeeBoxLoadJs);

            this.PageContext.QueryIDs = new QueryStringIDHelper(new[] { "mi", "ti", "fi", "ci", "u" }, false);

            if (this._adminEditMode && this.PageContext.IsAdmin && this.PageContext.QueryIDs.ContainsKey("u"))
            {
                this._currentUserID = this.PageContext.QueryIDs["u"].ToType<int>();
            }
            else
            {
                this._currentUserID = this.PageContext.PageUserID;
            }

            var ti = this.Request.QueryString.GetFirstOrDefault("ti");

            // check if it's a link from topic edit
            if (ti != null)
            {
                int.TryParse(ti, out this._currentTopicId);
            }

            if (this.IsPostBack)
            {
                return;
            }

            // check if it's a link from topic edit
            /*   if (ti != null && int.TryParse(ti, out tii))
               {
                   // save the image right now...
                   CommonDb.topic_imagesave(
                       PageContext.PageModuleID,
                       ti,
                       "{0}/{1}/{2}".FormatWith(
                           BaseUrlBuilder.BaseUrl,
                           YafBoardFolders.Current.Topics,
                           this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ti")),
                       null,
                       null);
               } */

            this.UpdateRemote.Text = this.GetText("COMMON", "UPDATE");
            this.UpdateUpload.Text = this.GetText("COMMON", "UPDATE");
            this.Back.Text = this.GetText("COMMON", "BACK");

            this.NoImage.Text = this.GetText("IMAGEADD", "NOIMAGE");

            this.DeleteImage.Text = this.GetText("IMAGEADD", "IMAGEDELETE");
            this.DeleteImage.Attributes["onclick"] =
                "return confirm('{0}?')".FormatWith(this.GetText("IMAGEADD", "IMAGEDELETE"));

            string addAdminParam = string.Empty;
            if (this._adminEditMode)
            {
                addAdminParam = "u={0}".FormatWith(this._currentUserID);
            }

            this.OurImage.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.imageadd, addAdminParam);
            this.OurImage.Text = this.GetText("IMAGEADD", "OURIMAGE_SELECT");
            if (this.Get<YafBoardSettings>().AllowRemoteTopicImages)
            {
                this.ImageRemoteRow.Visible = true;
                this.noteRemote.Text = this.GetTextFormatted(
                    "NOTE_IMG_REMOTE",
                    this.Get<YafBoardSettings>().TopicImageWidth.ToString(),
                    this.Get<YafBoardSettings>().TopicImageHeight.ToString());
                this.noteLocal.Text = this.GetTextFormatted(
                    "NOTE_IMG_LOCAL",
                    this.Get<YafBoardSettings>().TopicImageWidth.ToString(),
                    this.Get<YafBoardSettings>().TopicImageHeight,
                    (this.Get<YafBoardSettings>().AvatarSize / 1024).ToString());
            }

            this.BindData();
        }

        /// <summary>
        /// Saves the Remote TopicImage
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void RemoteUpdate_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.TopicImage.Text.Length > 0 && !this.TopicImage.Text.StartsWith("http://")
                && !this.TopicImage.Text.StartsWith("https://"))
            {
                this.TopicImage.Text = "http://{0}".FormatWith(this.TopicImage.Text);
            }
            var ti = this.Request.QueryString.GetFirstOrDefault("ti");

            // check if it's a link from topic edit
            if (ti != null)
            {
                int.TryParse(ti, out this._currentTopicId);
            }

            // update
            CommonDb.topic_imagesave(
                PageContext.PageModuleID, this._currentTopicId, this.TopicImage.Text.Trim(), null, null);

            // clear the URL out...
            this.TopicImage.Text = string.Empty;

            this.DeleteFiles();

            this.BindData();
        }

        /// <summary>
        /// Saves the local TopicImage
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void UploadUpdate_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.File.PostedFile == null || this.File.PostedFile.FileName.Trim().Length <= 0
                || this.File.PostedFile.ContentLength <= 0)
            {
                return;
            }

            int nImageSize = this.Get<YafBoardSettings>().AvatarSize;

            Stream resized = null;
            Stream resizedLarge = null;
            Image imgThumb = null;
            Image imgLarge = null;
            try
            {
                using (Image img = Image.FromStream(this.File.PostedFile.InputStream))
                {
                    long x = this.Get<YafBoardSettings>().TopicImageWidth;
                    long y = this.Get<YafBoardSettings>().TopicImageHeight;
                    if (img.Width > x || img.Height > y)
                    {
                        this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "WARN_IMG_TOOBIG").FormatWith(x, y));
                        this.PageContext.AddLoadMessage(
                            this.GetText("IMAGEADD", "WARN_IMG_SIZE").FormatWith(img.Width, img.Height));
                        this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "WARN_IMG_RESIZED"));

                        resized = ImageHelper.GetResizedImageStreamFromImage(img, x, y);

                        // Convert resized to Image. Resized is meant as a thimbnail  here
                        if (resized != null)
                        {
                            imgThumb = Image.FromStream(resized);
                        }
                    }

                    x = this.Get<YafBoardSettings>().ImageAttachmentResizeWidth;
                    y = this.Get<YafBoardSettings>().ImageAttachmentResizeHeight;
                    if (img.Width > x || img.Height > y)
                    {
                        this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "WARN_IMG_TOOBIG").FormatWith(x, y));
                        this.PageContext.AddLoadMessage(
                            this.GetText("IMAGEADD", "WARN_IMG_SIZE").FormatWith(img.Width, img.Height));
                        this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "WARN_IMG_RESIZED"));

                        resizedLarge = ImageHelper.GetResizedImageStreamFromImage(img, x, y);

                        // Convert resized to Image. Resized is meant as a thimbnail  here
                        if (resizedLarge != null)
                        {
                            imgLarge = Image.FromStream(resizedLarge);
                        }
                    }
                }
            


                // here we save an uploaded image
                if (this.PageContext.QueryIDs["ti"] != null)
                {
                    if (this.CheckValidFile(this.File))
                    {
                        this.SaveAttachment(
                            this.PageContext.QueryIDs["ti"].ToType<int>(),
                            this.File,
                            imgThumb ?? Image.FromStream(this.File.PostedFile.InputStream), imgLarge);
                    }

                    CommonDb.topic_imagesave(
                        PageContext.PageModuleID, this.PageContext.QueryIDs["ti"].ToType<int>(), null, null, null);

                    CommonDb.topic_imagesave(
                        PageContext.PageModuleID,
                        this.PageContext.QueryIDs["ti"].ToType<int>(),
                        this.File.PostedFile.FileName.Trim(),
                        resized ?? this.File.PostedFile.InputStream,
                        this.File.PostedFile.ContentType);

                }

                // clear the cache for this user...
                // this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this._currentUserID));

                if (nImageSize > 0 && this.File.PostedFile.ContentLength >= nImageSize && resized == null)
                {
                    this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "WARN_IMG_BIGFILE").FormatWith(nImageSize));
                    this.PageContext.AddLoadMessage(
                        this.GetText("IMAGEADD", "WARN_IMG_FILESIZE").FormatWith(this.File.PostedFile.ContentLength));
                }

                /* this.TopicImg.ImageUrl = "{0}resource.ashx?u={1}&upd={2}".FormatWith(
                     YafForumInfo.ForumClientFileRoot, this._currentUserID, DateTime.Now.Ticks); */
                if (this.PageContext.QueryIDs["ti"] != null)
                {
                    var dt = CommonDb.topic_info(
                        PageContext.PageModuleID, this.PageContext.QueryIDs["ti"].ToType<int>(), false);
                    if (dt != null)
                    {
                        this.TopicImg.ImageUrl =
                            "{0}resource.ashx?ti={1}&upd={2}".FormatWith(
                                YafForumInfo.ForumClientFileRoot,
                                this.PageContext.QueryIDs["ti"].ToType<int>(),
                                DateTime.Now.Ticks);
                    }
                }

                if (this.TopicImg.ImageUrl.IsSet())
                {
                    this.NoImage.Visible = false;
                }
            }
            catch (Exception)
            {
                // image is probably invalid...
                this.PageContext.AddLoadMessage(this.GetText("IMAGEADD", "INVALID_IMG_FILE"));
            }

            this.BindData();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            DataRow row = null;
            int topicId;

            if (this.PageContext.QueryIDs["ti"] != null)
            {
                row = CommonDb.topic_info(
                    PageContext.PageModuleID, this.PageContext.QueryIDs["ti"].ToType<int>(), false);
                if (row != null)
                {
                    topicId = row["TopicID"].ToType<int>();

                    this.TopicImg.Visible = true;
                    this.TopicImg1.Visible = true;
                    this.TopicImage.Text = string.Empty;
                    this.DeleteImage.Visible = false;
                    this.NoImage.Visible = false;

                    if (this.Get<YafBoardSettings>().UseFileTable && row["TopicImageBin"] != null && row["TopicImageBin"].ToString().Length > 0)
                    {
                        // database image
                        this.TopicImg1.Visible = true;
                        this.TopicImg1.Src = "data:" + row["TopicImageType"].ToString().Replace("/jpeg", "/jpg") + ";base64," + Convert.ToBase64String((byte[])row["TopicImageBin"]);
                        this.TopicImg.ImageUrl = "{0}resource.ashx?ti={1}".FormatWith(
                            YafForumInfo.ForumClientFileRoot, topicId);
                        this.TopicImage.Text = string.Empty;
                        this.DeleteImage.Visible = true;
                    }
                    else if (row["TopicImage"].ToString().Length > 0)
                    {
                        // remote
                        if (row["TopicImage"].ToString().TrimStart().IndexOf("ttp", System.StringComparison.Ordinal) > 0)
                        {
                            this.TopicImg.ImageUrl =
                                "{0}resource.ashx?ti={1}&url={2}&width={3}&height={4}&remote=1".FormatWith(
                                    YafForumInfo.ForumClientFileRoot,
                                    topicId,
                                    this.Server.UrlEncode(row["TopicImage"].ToString()),
                                    this.Get<YafBoardSettings>().TopicImageWidth,
                                    this.Get<YafBoardSettings>().TopicImageHeight);
                            this.TopicImage.Text = row["TopicImage"].ToString();
                            this.DeleteImage.Visible = true;
                        }
                        else
                        {
                            // image in folder
                            this.TopicImg.ImageUrl =
                                "{0}resource.ashx?ti={1}&width={3}&height={4}".FormatWith(
                                    YafForumInfo.ForumClientFileRoot,
                                    topicId,
                                    this.Server.UrlEncode(row["TopicImage"].ToString()),
                                    this.Get<YafBoardSettings>().TopicImageWidth,
                                    this.Get<YafBoardSettings>().TopicImageHeight);
                            this.TopicImage.Text = row["TopicImage"].ToString();
                            this.DeleteImage.Visible = true;
                        }
                    }
                    else
                    {
                        this.TopicImg.ImageUrl = this.Get<ITheme>().GetItem("ICONS", "NEW");
                        this.NoImage.Visible = true;
                    }

                    int rowSpan = 2;

                    this.ImageUploadRow.Visible = this._adminEditMode || this.Get<YafBoardSettings>().AllowTopicImages;
                    this.ImageRemoteRow.Visible = this._adminEditMode
                                                  || this.Get<YafBoardSettings>().AllowRemoteTopicImages;
                    this.ImageOurs.Visible = this._adminEditMode || this.Get<YafBoardSettings>().AvatarGallery;
                    this.ImageOurs.Visible = false;

                    //  if (this._adminEditMode || this.Get<YafBoardSettings>().TopicImage)
                    //   {
                    // rowSpan++;
                    //  }

                    //    if (this._adminEditMode || this.Get<YafBoardSettings>().TopicImage)
                    //    {
                    // rowSpan++;
                    //   }
                    if (this.Get<YafBoardSettings>().AllowRemoteTopicImages)
                    {
                        rowSpan++;
                    }
                    
                        this.topicImageTD.RowSpan = rowSpan;
                    
                }
            }

            #endregion
        }

        /// <summary>
        /// The save attachment.
        /// </summary>
        /// <param name="topicId">
        /// The message id.
        /// </param>
        /// <param name="file">
        /// The file.
        /// </param>
        private void SaveAttachment([NotNull] object topicId, [NotNull] HtmlInputFile file, [CanBeNull] Image resized, [CanBeNull] Image lImage)
        {
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

            string previousDirectory =
                this.Get<HttpRequestBase>()
                    .MapPath(
                        string.Concat(
                            BaseUrlBuilder.ServerFileRoot,
                            YafBoardFolders.Current.Uploads,
                            "/",
                            YafBoardFolders.Current.Topics));

            // check if Uploads folder exists
            if (!Directory.Exists(previousDirectory))
            {
                Directory.CreateDirectory(previousDirectory);
            }

            // save large image
            resized.Save("{0}/{1}.thumb.{2}.yafupload".FormatWith(previousDirectory, topicId, filename));

            // save thumbnail
            lImage.Save("{0}/{1}.{2}.yafupload".FormatWith(previousDirectory, topicId, filename));
        }

        /// <summary>
        /// The check valid file.
        /// </summary>
        /// <param name="uploadedFile">
        /// The uploaded file.
        /// </param>
        /// <returns>
        /// Returns if the File Is Valid
        /// </returns>
        private bool CheckValidFile([NotNull] HtmlInputFile uploadedFile)
        {
            string filePath = uploadedFile.PostedFile.FileName.Trim();

            if (filePath.IsNotSet() || uploadedFile.PostedFile.ContentLength == 0)
            {
                return false;
            }

            if (uploadedFile.PostedFile == null || uploadedFile.PostedFile.FileName.Trim().Length == 0
                || uploadedFile.PostedFile.ContentLength == 0)
            {
                return false;
            }

            // verify the size of the attachment
            if (this.Get<YafBoardSettings>().MaxFileSize > 0
                && uploadedFile.PostedFile.ContentLength > this.Get<YafBoardSettings>().MaxFileSize)
            {
                this.PageContext.AddLoadMessage(
                    this.GetTextFormatted(
                        "UPLOAD_TOOBIG",
                        uploadedFile.PostedFile.ContentLength / 1024,
                        this.Get<YafBoardSettings>().MaxFileSize / 1024));

                return false;
            }


            string extension = Path.GetExtension(filePath).ToLower();

            // remove the "period"
            extension = extension.Replace(".", string.Empty);

            // If we don't get a match from the db, then the extension is not allowed
            DataTable dt = CommonDb.extension_list(PageContext.PageModuleID, this.PageContext.PageBoardID, extension);

            bool bInList = dt.Rows.Count > 0;
            bool bError = false;

            if (this.Get<YafBoardSettings>().FileExtensionAreAllowed && !bInList)
            {
                // since it's not in the list -- it's invalid
                bError = true;
            }
            else if (!this.Get<YafBoardSettings>().FileExtensionAreAllowed && bInList)
            {
                // since it's on the list -- it's invalid
                bError = true;
            }

            if (filePath.Contains(";."))
            {
                // IIS semicolon valnerabilty fix
                bError = true;
            }

            if (bError)
            {
                // just throw an error that this file is invalid...
                this.PageContext.AddLoadMessage(this.GetTextFormatted("FILEERROR", extension));
                return false;
            }



            return true;
        }
    }
}