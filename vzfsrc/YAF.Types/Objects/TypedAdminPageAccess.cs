/* Yet Another Forum.NET
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

namespace YAF.Types.Objects
{
  #region Using

  using System;
  using System.Data;

  #endregion

  /// <summary>
    /// The typed Admin Page Access.
  /// </summary>
  [Serializable]
  public class TypedAdminPageAccess
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TypedAdminPageAccess"/> class.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    public TypedAdminPageAccess(DataRow row)
    {
      this.UserId = row.Field<int>("UserID");
      this.PageName = row.Field<string>("PageName");
      this.ReadAccess = row.Field<bool>("ReadAccess");
    }
      
      /// <summary>
      /// Initializes a new instance of the <see cref="TypedAdminPageAccess"/> class.
      /// </summary>
      /// <param name="userId">
      /// The user Id.
      /// </param>
      /// <param name="pageName">
      /// The pageName.
      /// </param>
      /// <param name="userName"> 
      /// The user name. 
      /// </param>
      /// <param name="userDisplayName"> 
      /// The user display name. 
      /// </param>
      /// <param name="boardName"> 
      /// The board Name. 
      /// </param>
      /// <param name="readAccess"> 
      /// The read access. 
      /// </param>
      public TypedAdminPageAccess(int userId, string pageName, string userName = null, string userDisplayName = null, string boardName = null , bool readAccess = false)
      {
          this.UserId = userId;
          this.PageName = pageName;
          this.UserName = userName;
          this.UserDisplayName = userDisplayName;
          this.BoardName = boardName;
          this.ReadAccess = readAccess;
      }

    #endregion

    #region Properties

      /// <summary>
      /// Gets or sets UserId.
      /// </summary>
      public int UserId { get; set; }
    
      /// <summary>
      /// Gets or sets PageName
      /// </summary>
      public string PageName { get; set; }

      /// <summary>
      /// Gets or sets UserName
      /// </summary>
      public string UserName { get; set; }

      /// <summary>
      /// Gets or sets UserDisplayName
      /// </summary>
      public string UserDisplayName { get; set; }

      /// <summary>
      /// Gets or sets BoardName
      /// </summary>
      public string BoardName { get; set; }

      /// <summary>
      /// Gets or sets ReadAccess.
      /// </summary>
      public bool ReadAccess { get; set; }

    #endregion
  }
}