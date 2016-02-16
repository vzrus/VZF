// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="ParameterDictionary.cs">
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
//   The DataSourceInformation.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.DAL
{
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// The parameter dictionary.
    /// </summary>
    public class ParameterDictionary : Dictionary<string, DbParameter>
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(DbParameter item)
        {
            this.Add(item.ParameterName, item);
        }
        public void Add(params DbParameter[] items)
        {
            foreach (var p in items)
            {                
                this.Add(p.ParameterName, p);
            }
        }        
    }   
}