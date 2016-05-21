#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File MessagePostData.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

namespace VZF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Shows a Message Post
    /// </summary>
    public class MessagePostData : MessagePost
    {
        #region Constants and Fields

        /// <summary>
        ///   The _row.
        /// </summary>
        private DataRow _row;

        /// <summary>
        ///   The _show attachments.
        /// </summary>
        private bool _showAttachments = true;

        /// <summary>
        /// The _show bb codes.
        /// </summary>
        private bool _showBbCodes = true;

        /// <summary>
        ///   The _show signature.
        /// </summary>
        private bool _showSignature = true;

        /// <summary>
        /// The _show deleted.
        /// </summary>
        private bool _showDeleted = false;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets a value indicating whether IsAlt.
        /// </summary>
        public bool IsAltMessage { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether Col Span is.
        /// </summary>
        public string ColSpan { get; set; }

        /// <summary>
        ///   Gets or sets DataRow.
        /// </summary>
        public DataRow DataRow
        {
            get
            {
                return this._row;
            }

            set
            {
                this._row = value;
                if (this._row != null)
                {
                    this.MessageFlags = new MessageFlags(this._row["Flags"]);
                }
            }
        }

        /// <summary>
        ///   Gets Edited.
        /// </summary>
        public DateTime Edited
        {
            get
            {
                return this.DataRow != null ? Convert.ToDateTime(this.DataRow["Edited"]) : DateTime.UtcNow;
            }
        }

        /// <summary>
        ///   Gets Message.
        /// </summary>
        public override string Message
        {
            get
            {
                if (this.DataRow != null)
                {
                    string message = this.DataRow["Message"].ToString();

                    return TruncateMessage(message);
                }

                return string.Empty;
            }
        }

        /// <summary>
        ///   Gets Message Id.
        /// </summary>
        public int? MessageId
        {
            get
            {
                if (this.DataRow != null)
                {
                    return this.DataRow["MessageID"].ToType<int>();
                }

                return null;
            }
        }

        /// <summary>
        ///   Gets Posted.
        /// </summary>
        public DateTime Posted
        {
            get
            {
                return this.DataRow != null ? Convert.ToDateTime(this.DataRow["Posted"]) : DateTime.UtcNow;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether Show the Edit Message if needed.
        /// </summary>
        public bool ShowEditMessage { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether ShowAttachments.
        /// </summary>
        public bool ShowAttachments
        {
            get
            {
                return this._showAttachments;
            }

            set
            {
                this._showAttachments = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show bb codes.
        /// </summary>
        public bool ShowBbCodes
        {
            get
            {
                return this._showBbCodes;
            }

            set
            {
                this._showBbCodes = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show deleted.
        /// </summary>
        public bool ShowDeleted
        {
            get
            {
                return this._showDeleted;
            }

            set
            {
                this._showDeleted = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ShowSignature.
        /// </summary>
        public bool ShowSignature
        {
            get
            {
                return this._showSignature;
            }

            set
            {
                this._showSignature = value;
            }
        }

        /// <summary>
        ///   Gets Signature.
        /// </summary>
        [CanBeNull]
        public override string Signature
        {
            get
            {
                if (this.DataRow != null && this.ShowSignature && this.Get<YafBoardSettings>().AllowSignatures
                    && this.DataRow["Signature"] != DBNull.Value
                    && this.DataRow["Signature"].ToString().ToLower() != "<p>&nbsp;</p>"
                    && this.DataRow["Signature"].ToString().Trim().Length > 0)
                {
                    return this.DataRow["Signature"].ToString();
                }

                return null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Truncates the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The truncate message.
        /// </returns>
        public static string TruncateMessage([NotNull] string message)
        {
            int maxPostSize = Math.Max(YafContext.Current.Get<YafBoardSettings>().MaxPostSize, 0);

            // 0 == unlimited
            return maxPostSize == 0 || message.Length <= maxPostSize ? message : message.Truncate(maxPostSize);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            if (this.DataRow != null && !this.MessageFlags.IsDeleted)
            {
                // populate DisplayUserID
                if (!UserMembershipHelper.IsGuestUser(this.DataRow["UserID"]))
                {
                    this.DisplayUserID = this.DataRow["UserID"].ToType<int>();
                }

                this.IsAlt = this.IsAltMessage;

                this.RowColSpan = this.ColSpan;

                if (this.ShowAttachments && long.Parse(this.DataRow["HasAttachments"].ToString()) > 0)
                {
                    // add attached files control...
                    var attached = new MessageAttached { MessageID = this.DataRow["MessageID"].ToType<int>() };

                    var displayNameO = this.DataRow.Table.Columns.Contains("UserDisplayName") ? this.DataRow["UserDisplayName"] : this.DataRow["DisplayName"];

                    if (displayNameO != DBNull.Value
                        && YafContext.Current.Get<YafBoardSettings>().EnableDisplayName)
                    {
                        attached.UserName =
                           displayNameO.ToString();
                    }
                    else
                    {
                        attached.UserName = this.DataRow["UserName"].ToString();
                    }

                    this.Controls.Add(attached);
                }
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// The render message.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void RenderMessage([NotNull] HtmlTextWriter writer)
        {
            if (this.DataRow == null)
            {
                return;
            }

            if (this.MessageFlags.IsDeleted && this.ShowDeleted)
            {
                if (this.DataRow.Table.Columns.Contains("IsModeratorChanged"))
                {
                    this.IsModeratorChanged = Convert.ToBoolean(this.DataRow["IsModeratorChanged"]);
                }

                var deleteText =
                    !string.IsNullOrEmpty(
                        this.Get<HttpContextBase>().Server.HtmlDecode(Convert.ToString(this.DataRow["DeleteReason"])))
                        ? this.Get<IFormatMessage>().RepairHtml((string)this.DataRow["DeleteReason"], true)
                        : this.GetText("EDIT_REASON_NA");

                // deleted message text...
                this.RenderDeletedMessage(writer, deleteText);
            }
            else if (this.MessageFlags.NotFormatted)
            {
                // just write out the message with no formatting...
                writer.Write(this.Message);
            }
            else if (this.DataRow.Table.Columns.Contains("Edited") && this.DataRow.Table.Columns.Contains("EditedBy"))
            {
                if (this.DataRow.Table.Columns.Contains("IsModeratorChanged"))
                {
                    this.IsModeratorChanged = Convert.ToBoolean(this.DataRow["IsModeratorChanged"]);
                }

                // handle a message that's been edited...
                var editedMessageDateTime = this.Posted;

                if (this.Edited > this.Posted && this.DataRow["EditedBy"] != DBNull.Value)
                {
                    editedMessageDateTime = this.Edited;
                    this.IsModeratorChanged = this.DataRow["IsModeratorChanged"].ToType<bool>();
                }

                var formattedMessage =
                    this.Get<IFormatMessage>().FormatMessage(
                        this.HighlightMessage(this.Message, true), this.MessageFlags, false, editedMessageDateTime.AddMinutes((double)PageContext.CurrentUserData.TimeZone * 60));
              
              //  if (this.ShowBbCodes)
              //  {
                    // tha_watcha : Since html message and bbcode can be mixed now, message should be always replace bbcode
                    this.RenderModulesInBBCode(
                        writer,
                        formattedMessage,
                        this.MessageFlags,
                        this.DisplayUserID,
                        this.MessageId);
              //  }

                // Render Edit Message
                if (this.ShowEditMessage
                    && this.Edited > this.Posted.AddSeconds(this.Get<YafBoardSettings>().EditTimeOut) 
                    && this.DataRow["EditedBy"] != DBNull.Value)
                {
                    this.RenderEditedMessage(
                        writer, this.Edited, Convert.ToString(this.DataRow["EditReason"]), this.MessageId);
                }
            }
            else
            {
                var formattedMessage =
                    this.Get<IFormatMessage>().FormatMessage(
                        this.HighlightMessage(this.Message, true), this.MessageFlags);
                if (this.ShowBbCodes)
                {
                    // tha_watcha : Since html message and bbcode can be mixed now, message should be always replace bbcode
                    this.RenderModulesInBBCode(
                        writer,
                        formattedMessage,
                        this.MessageFlags,
                        this.DisplayUserID,
                        this.MessageID);
                }
            }
        }

        #endregion
    }
}