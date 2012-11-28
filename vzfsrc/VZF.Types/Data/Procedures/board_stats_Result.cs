/* VZF by vzrus
 * Copyright (C) 2006-2013 Vladimir Zakharov
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
        public board_stats_Result(DataRow dr)
        {
            NumPosts = dr.Field<int>("NumPosts");
            NumTopics = dr.Field<int>("NumTopics");
            NumUsers = dr.Field<int>("NumUsers");
            BoardStart = dr.Field<DateTime>("BoardStart");
        }

        public int? NumPosts { get; set; }
        public int? NumTopics { get; set; }
        public int? NumUsers { get; set; }
        public DateTime? BoardStart { get; set; }
    }
}
