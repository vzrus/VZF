// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="TreeViewUtils.cs">
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
//   The parces wich splits _ -delimited strings and returns int values.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
using System;
using YAF.Types.Objects;

namespace VZF.Utils.Helpers
{
    /// <summary>
    /// The tree view utils.
    /// </summary>
    public class TreeViewUtils
    {
        public static TreeNodeKey GetParcedTreeNode(string stringToParce)
        {
            const string delimiter = "_";
            if (!stringToParce.IsSet()) return null;

            string[] nodePId = stringToParce.Split(delimiter.ToCharArray());
            switch (nodePId.LongLength)
            {
                case 1:
                    return new TreeNodeKey
                    {
                        BoardId = nodePId[0].ToType<int>()
                    };
                case 2:
                    return new TreeNodeKey
                    {
                        BoardId = nodePId[0].ToType<int>(), 
                        CategoryId = nodePId[1].ToType<int>()
                    };
                case 3:
                    return new TreeNodeKey
                    {
                        BoardId = nodePId[0].ToType<int>(), 
                        CategoryId = nodePId[1].ToType<int>(), 
                        ForumId = nodePId[2].ToType<int>()
                    };
            }
            return new TreeNodeKey();
        }
    }

}

