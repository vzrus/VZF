// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="PollGroup.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
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
//   The Poll Group functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Types.Objects
{
    using System.Collections.Generic;
    using YAF.Types.Flags;

    /// <summary>
    /// The poll group.
    /// </summary>
    public class PollGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PollGroup"/> class.
        /// </summary>
        public PollGroup()
        {
        }

        public int? mid { get; set; }

        /// <summary>
        /// Gets or sets the poll group id.
        /// </summary>
        public int? PollGroupID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the topic id.
        /// </summary>
        public int? TopicId { get; set; }

        /// <summary>
        /// Gets or sets the forum id.
        /// </summary>
        public int? ForumId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the board id.
        /// </summary>
        public int? BoardId { get; set; }

        /// <summary>
        /// Gets or sets the polls.
        /// </summary>
        public List<Poll> Polls { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public PollGroupFlags Flags { get; set; }
    }
}
