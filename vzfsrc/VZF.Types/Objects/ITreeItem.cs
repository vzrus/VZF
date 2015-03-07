// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITreeItem.cs" company="Vladimir Zakharov">
//   VZF by vzrus
//   Copyright (C) 2012 Vladimir Zakharov
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
//   Dynatree tree node interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Types.Objects
{
    /// <summary>
    /// Dynatree tree node interface.
    /// </summary>
    public interface ITreeItem
    {
    }

    /// <summary>
    /// Dynatree TreeNode class
    /// </summary>
    public class TreeNode : ITreeItem
    {
        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string title;

        /// <summary>
        /// Gets the Tooltip.
        /// </summary>
        public string tooltip;

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string key;

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public string extraClasses;

        /// <summary>
        /// Gets the rel attr.
        /// </summary>
        // public string rel;    

        /// <summary>
        /// Gets if is folder. 
        /// </summary>
        public bool folder;   

        /// <summary>
        /// Gets if lazy loading.
        /// </summary>
        public bool lazy;

        public bool expanded;

        public bool selected;
    }
}
