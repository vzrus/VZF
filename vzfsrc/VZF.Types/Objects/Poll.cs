// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="Poll.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2016 Vladimir Zakharov
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
//   The Poll Question List functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Types.Objects
{
    using System;
    using System.Collections.Generic;

    using YAF.Types.Flags;

    /// <summary>
    /// The poll.
    /// </summary>
    public class Poll
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Poll"/> class.
        /// </summary>
        public Poll()
        {
        }

        /// <summary>
        /// Gets or sets the poll id.
        /// </summary>
        public int PollID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the closes.
        /// </summary>
        public DateTime? Closes { get; set; }

        /// <summary>
        /// Gets or sets the poll group id.
        /// </summary>
        public int PollGroupID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the object path.
        /// </summary>
        public string ObjectPath { get; set; }

        /// <summary>
        /// Gets or sets the mime type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public PollFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the choices.
        /// </summary>
        public List<PollChoice> Choices { get; set; }
    }
}
