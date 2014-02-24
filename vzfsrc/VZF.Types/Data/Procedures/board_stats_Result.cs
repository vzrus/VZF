/* VZF by vzrus
 * Copyright (C) 2006-2014 Vladimir Zakharov
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
    public class board_stats_Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="board_stats_Result"/> class.
        /// </summary>
        /// <param name="dr">
        /// The dr.
        /// </param>
        public board_stats_Result(DataRow dr)
        {
            this.NumPosts = dr.Field<int>("NumPosts");
            this.NumTopics = dr.Field<int>("NumTopics");
            this.NumUsers = dr.Field<int>("NumUsers");
            this.BoardStart = dr.Field<DateTime>("BoardStart");
        }

        /// <summary>
        /// Gets or sets the num posts.
        /// </summary>
        public int? NumPosts { get; set; }

        /// <summary>
        /// Gets or sets the num topics.
        /// </summary>
        public int? NumTopics { get; set; }

        /// <summary>
        /// Gets or sets the num users.
        /// </summary>
        public int? NumUsers { get; set; }

        /// <summary>
        /// Gets or sets the board start.
        /// </summary>
        public DateTime? BoardStart { get; set; }
    }
}
