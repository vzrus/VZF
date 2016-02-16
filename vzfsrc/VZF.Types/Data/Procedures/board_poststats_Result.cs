/* VZF by vzrus
 * Copyright (C) 2006-2016 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace VZF.Types.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// Returnes board_stats_Result row representation.
    /// </summary>
    public class board_poststats_Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="board_poststats_Result"/> class.
        /// </summary>
        /// <param name="dr">
        /// The dr.
        /// </param>
        public board_poststats_Result(DataRow dr)
        {
            this.Posts = dr.Field<int?>("Posts");
            this.Topics = dr.Field<int?>("Topics");
            this.Forums = dr.Field<int?>("Forums");
            this.LastPostInfoID = dr.Field<int?>("LastPostInfoID");
            this.LastPost = dr.Field<DateTime?>("LastPost");
            this.LastUserID = dr.Field<int?>("LastUserID");
            this.LastUser = dr.Field<string>("LastUser");
            this.LastUserDisplayName = dr.Field<string>("LastUserDisplayName");
            this.LastUserStyle = dr.Field<string>("LastUserStyle");
        }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public int? Posts { get; set; }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        public int? Topics { get; set; }

        /// <summary>
        /// Gets or sets the forums.
        /// </summary>
        public int? Forums { get; set; }

        /// <summary>
        /// Gets or sets the last post info id.
        /// </summary>
        public int? LastPostInfoID { get; set; }

        /// <summary>
        /// Gets or sets the last post.
        /// </summary>
        public DateTime? LastPost { get; set; }

        /// <summary>
        /// Gets or sets the last user id.
        /// </summary>
        public int? LastUserID { get; set; }

        /// <summary>
        /// Gets or sets the last user.
        /// </summary>
        public string LastUser { get; set; }

        /// <summary>
        /// Gets or sets the last user display name.
        /// </summary>
        public string LastUserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the last user style.
        /// </summary>
        public string LastUserStyle { get; set; }
    }
}
