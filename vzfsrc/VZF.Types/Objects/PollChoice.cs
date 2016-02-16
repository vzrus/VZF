// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="PollChoice.cs">
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
//   The Poll Choice List functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Types.Objects
{
    /// <summary>
    /// The poll choice list.
    /// </summary>
    public class PollChoice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PollChoice"/> class.
        /// </summary>
        public PollChoice()
        {
        }
       
        /// <summary>
        /// Gets or sets the choice id.
        /// </summary>
        public int ChoiceID { get; set; }

        /// <summary>
        /// Gets or sets the choice.
        /// </summary>
        public string Choice { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Gets or sets the object path.
        /// </summary>
        public string ObjectPath { get; set; }

        /// <summary>
        /// Gets or sets the mime type.
        /// </summary>
        public string MimeType { get; set; }
    }
}
