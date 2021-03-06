﻿namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The edit users avatar.
    /// </summary>
    public partial class EditUsersAvatar : BaseUserControl
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
            YafBuildLink.Redirect(this._adminEditMode ? ForumPages.admin_users : ForumPages.cp_profile);
        }

        /// <summary>
        /// Delete The Current Avatar
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DeleteAvatar_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            CommonDb.user_deleteavatar(PageContext.PageModuleID, this._currentUserID);

            // clear the cache for this user...
            this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this._currentUserID));
            this.BindData();
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.PageContext.QueryIDs = new QueryStringIDHelper("u");

            if (this._adminEditMode && this.PageContext.IsAdmin && this.PageContext.QueryIDs.ContainsKey("u"))
            {
                this._currentUserID = this.PageContext.QueryIDs["u"].ToType<int>();
            }
            else
            {
                this._currentUserID = this.PageContext.PageUserID;
            }

            if (this.IsPostBack)
            {
                return;
            }

            // check if it's a link from the avatar picker
            if (this.Request.QueryString.GetFirstOrDefault("av") != null)
            {
                // save the avatar right now...
                CommonDb.user_saveavatar(PageContext.PageModuleID, this._currentUserID,
                    "{0}{1}".FormatWith(
                        BaseUrlBuilder.BaseUrl, this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("av")),
                    null,
                    null);

                // clear the cache for this user...
                this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this._currentUserID));
            }

            this.UpdateRemote.Text = this.GetText("COMMON", "UPDATE");
            this.UpdateUpload.Text = this.GetText("COMMON", "UPDATE");
            this.Back.Text = this.GetText("COMMON", "BACK");

            this.NoAvatar.Text = this.GetText("CP_EDITAVATAR", "NOAVATAR");

            this.DeleteAvatar.Text = this.GetText("CP_EDITAVATAR", "AVATARDELETE");
            this.DeleteAvatar.Attributes["onclick"] =
                "return confirm('{0}?')".FormatWith(this.GetText("CP_EDITAVATAR", "AVATARDELETE"));

            string addAdminParam = string.Empty;
            if (this._adminEditMode)
            {
                addAdminParam = "u={0}".FormatWith(this._currentUserID);
            }

            this.OurAvatar.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.avatar, addAdminParam);
            this.OurAvatar.Text = this.GetText("CP_EDITAVATAR", "OURAVATAR_SELECT");

            this.noteRemote.Text = this.GetTextFormatted(
                "NOTE_REMOTE",
                this.Get<YafBoardSettings>().AvatarWidth.ToString(),
                this.Get<YafBoardSettings>().AvatarHeight.ToString());
            this.noteLocal.Text = this.GetTextFormatted(
                "NOTE_LOCAL",
                this.Get<YafBoardSettings>().AvatarWidth.ToString(),
                this.Get<YafBoardSettings>().AvatarHeight,
                (this.Get<YafBoardSettings>().AvatarSize / 1024).ToString());

            this.BindData();
        }

        /// <summary>
        /// Saves the Remote Avatar
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void RemoteUpdate_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Avatar.Text.Length > 0 && !this.Avatar.Text.StartsWith("http://")
                && !this.Avatar.Text.StartsWith("https://"))
            {
                this.Avatar.Text = "http://{0}".FormatWith(this.Avatar.Text);
            }

            // update
            CommonDb.user_saveavatar(PageContext.PageModuleID, this._currentUserID, this.Avatar.Text.Trim(), null, null);

            // clear the cache for this user...
            this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this._currentUserID));

            // clear the URL out...
            this.Avatar.Text = string.Empty;

            this.BindData();
        }

        /// <summary>
        /// Saves the local Avatar
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

            long x = this.Get<YafBoardSettings>().AvatarWidth;
            long y = this.Get<YafBoardSettings>().AvatarHeight;
            int nAvatarSize = this.Get<YafBoardSettings>().AvatarSize;

            Stream resized = null;

            try
            {
                using (Image img = Image.FromStream(this.File.PostedFile.InputStream))
                {
                    if (img.Width > x || img.Height > y)
                    {
                        this.PageContext.AddLoadMessage(this.GetText("CP_EDITAVATAR", "WARN_TOOBIG").FormatWith(x, y));
                        this.PageContext.AddLoadMessage(
                            this.GetText("CP_EDITAVATAR", "WARN_SIZE").FormatWith(img.Width, img.Height));
                        this.PageContext.AddLoadMessage(this.GetText("CP_EDITAVATAR", "WARN_RESIZED"));

                        resized = ImageHelper.GetResizedImageStreamFromImage(img, x, y);
                    }
                }

                // Delete old first...
                CommonDb.user_deleteavatar(PageContext.PageModuleID, this._currentUserID);

                CommonDb.user_saveavatar(PageContext.PageModuleID, this._currentUserID,
                    null,
                    resized ?? this.File.PostedFile.InputStream,
                    this.File.PostedFile.ContentType);

                // clear the cache for this user...
                this.Get<IRaiseEvent>().Raise(new UpdateUserEvent(this._currentUserID));

                if (nAvatarSize > 0 && this.File.PostedFile.ContentLength >= nAvatarSize && resized == null)
                {
                    this.PageContext.AddLoadMessage(
                        this.GetText("CP_EDITAVATAR", "WARN_BIGFILE").FormatWith(nAvatarSize));
                    this.PageContext.AddLoadMessage(
                        this.GetText("CP_EDITAVATAR", "WARN_FILESIZE").FormatWith(this.File.PostedFile.ContentLength));
                }

                this.AvatarImg.ImageUrl = "{0}resource.ashx?u={1}&upd={2}".FormatWith(
                    YafForumInfo.ForumClientFileRoot, this._currentUserID, DateTime.Now.Ticks);

                if (this.AvatarImg.ImageUrl.IsSet())
                {
                    this.NoAvatar.Visible = false;
                }
            }
            catch (Exception)
            {
                // image is probably invalid...
                this.PageContext.AddLoadMessage(this.GetText("CP_EDITAVATAR", "INVALID_FILE"));
            }

            // this.BindData();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            DataRow row;

            using (DataTable dt = CommonDb.user_list(PageContext.PageModuleID, this.PageContext.PageBoardID, this._currentUserID, null))
            {
                row = dt.Rows[0];
            }

            this.AvatarImg.Visible = true;
            this.Avatar.Text = string.Empty;
            this.DeleteAvatar.Visible = false;
            this.NoAvatar.Visible = false;

            if (this.Get<YafBoardSettings>().AvatarUpload && row["HasAvatarImage"] != null
                && long.Parse(row["HasAvatarImage"].ToString()) > 0)
            {
                this.AvatarImg.ImageUrl = "{0}resource.ashx?u={1}".FormatWith(
                    YafForumInfo.ForumClientFileRoot, this._currentUserID);
                this.Avatar.Text = string.Empty;
                this.DeleteAvatar.Visible = true;
            }
            else if (row["Avatar"].ToString().Length > 0)
            {
                // Took out PageContext.BoardSettings.AvatarRemote
                this.AvatarImg.ImageUrl =
                    "{3}resource.ashx?url={0}&width={1}&height={2}".FormatWith(
                        this.Server.UrlEncode(row["Avatar"].ToString()),
                        this.Get<YafBoardSettings>().AvatarWidth,
                        this.Get<YafBoardSettings>().AvatarHeight,
                        YafForumInfo.ForumClientFileRoot);

                this.Avatar.Text = row["Avatar"].ToString();
                this.DeleteAvatar.Visible = true;
            }
            else if (this.Get<YafBoardSettings>().AvatarGravatar)
            {
                var x = new MD5CryptoServiceProvider();
                byte[] bs = Encoding.UTF8.GetBytes(this.PageContext.User.Email);
                bs = x.ComputeHash(bs);
                var s = new StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }

                string emailHash = s.ToString();

                string gravatarUrl = "http://www.gravatar.com/avatar/{0}.jpg?r={1}".FormatWith(
                    emailHash, this.Get<YafBoardSettings>().GravatarRating);

                this.AvatarImg.ImageUrl =
                    "{3}resource.ashx?url={0}&width={1}&height={2}".FormatWith(
                        this.Server.UrlEncode(gravatarUrl),
                        this.Get<YafBoardSettings>().AvatarWidth,
                        this.Get<YafBoardSettings>().AvatarHeight,
                        YafForumInfo.ForumClientFileRoot);

                this.NoAvatar.Text = "Gravatar Image";
                this.NoAvatar.Visible = true;
            }
            else
            {
                this.AvatarImg.ImageUrl = "../images/noavatar.gif";
                this.NoAvatar.Visible = true;
            }

            int rowSpan = 2;

            this.AvatarUploadRow.Visible = this._adminEditMode || this.Get<YafBoardSettings>().AvatarUpload;
            this.AvatarRemoteRow.Visible = this._adminEditMode || this.Get<YafBoardSettings>().AvatarRemote;
            this.AvatarOurs.Visible = this._adminEditMode || this.Get<YafBoardSettings>().AvatarGallery;

            if (this._adminEditMode || this.Get<YafBoardSettings>().AvatarUpload)
            {
                rowSpan++;
            }

            if (this._adminEditMode || this.Get<YafBoardSettings>().AvatarRemote)
            {
                rowSpan++;
            }

            this.avatarImageTD.RowSpan = rowSpan;
        }

        #endregion
    }
}