// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="Providers.cs">
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
//   The provider name constants.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace VZF.Types.Constants
{
    /// <summary>
    /// The providers.
    /// </summary>
    public static class Providers
    {
        /// <summary>
        /// The firebird.
        /// </summary>
        public const string Firebird = "FirebirdSql.Data.FirebirdClient";

        /// <summary>
        /// The postgre.
        /// </summary>
        public const string Postgre = "Npgsql";

        /// <summary>
        /// The mysql.
        /// </summary>
        public const string MySql = "MySql.Data.MySqlClient";

        /// <summary>
        /// The ms sql.
        /// </summary>
        public const string MsSql = "System.Data.SqlClient";
    }
}
