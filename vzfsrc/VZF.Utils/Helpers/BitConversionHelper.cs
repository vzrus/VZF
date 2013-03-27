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
//   The personal forum list.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace YAF.Utils.Helpers
{
    using YAF.Types;

    /// <summary>
    /// The bit conversion helper.
    /// </summary>
    public static class BitConversionHelper
    {
        /// <summary>
        /// The bit set.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <param name="bitmask">
        /// The bitmask.
        /// </param>
        /// <returns>
        /// Is the bit set.
        /// </returns>
        public static bool IsBitSet([NotNull] object o, int bitmask)
        {
            var i = (int)o;
            return (i & bitmask) != 0;
        }
    }
}
