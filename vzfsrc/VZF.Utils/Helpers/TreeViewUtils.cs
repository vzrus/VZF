// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="TreeViewUtils.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2013 Vladimir Zakharov
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
//   The parces wich splits _ -delimited strings and returns int values.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace VZF.Utils.Helpers
{
    /// <summary>
    /// The tree view utils.
    /// </summary>
    public class TreeViewUtils
    {
        /// <summary>
        /// The tree node id parser.
        /// </summary>
        /// <param name="nodeIds">
        /// The node ids.
        /// </param>
        /// <param name="forumId">
        /// The forum id.
        /// </param>
        /// <param name="categoryId">
        /// The category id.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        public static void TreeNodeIdParser(string nodeIds, out int forumId, out int categoryId, out int boardId)
        {
            forumId = 0;
            categoryId = 0;
            boardId = 0;
            if (!nodeIds.IsSet())
            {
                return;
            }

            string[] nodeId = nodeIds.Split('_');
            switch (nodeId.Length)
            {
                case 1:
                    boardId = nodeId[0].ToType<int>();
                    break;
                case 2:
                    categoryId = nodeId[1].ToType<int>();
                    break;
                case 3:
                    forumId = nodeId[2].ToType<int>();
                    break;
            }
        }
    }
}
