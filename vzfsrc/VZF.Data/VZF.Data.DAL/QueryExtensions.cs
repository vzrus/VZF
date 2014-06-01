// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="QueryExtensions.cs">
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
//   The QueryExtensions.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.DAL
{
    using System.Text;

    /// <summary>
    /// The query extensions.
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// The append object query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, int? mid)
        {
            return sb.Append(SqlDbAccess.GetVzfObjectName(command, mid));
        }

        /// <summary>
        /// The append object query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, string connectionStringName)
        {
            return sb.Append(SqlDbAccess.GetVzfObjectNameFromConnectionString(command, connectionStringName));
        }

        /// <summary>
        /// The append query.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        public static StringBuilder AppendQuery(this StringBuilder sb, string command)
        {
            return sb.Append(command);
        }
    }
}
