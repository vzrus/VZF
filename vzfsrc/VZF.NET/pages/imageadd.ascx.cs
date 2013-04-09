/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

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
                PageContext.PageModuleID, this.Request.QueryString.GetFirstOrDefault("ti").ToType<int>(), false);
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