/* VZF by vzrus
 * Copyright (C) 2006-2013 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure was primarily based on MS SQL Server code,
 * created by YAF(YetAnotherForum) developers  * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace VZF.Data.Postgre
{
    using System;
    using System.Data;
    using System.Globalization;

    using Npgsql;

    using YAF.Classes;
    using YAF.Types.Handlers;

  /// <summary>
  /// Provides open/close management for DB Connections
  /// </summary>
  public static class PostgreDbConnectionManager
  {
    /// <summary>
    /// The info message.
    /// </summary>
    public static event YafDBConnInfoMessageEventHandler InfoMessage;

    /// <summary>
    /// Gets an open connection to the DB. Can be called any number of times.
    /// </summary>
    /// <param name="connectionString">
    /// The connection String.
    /// </param>
    /// <returns>
    /// The <see cref="NpgsqlConnection"/>.
    /// </returns>
    public static NpgsqlConnection OpenDBConnection(string connectionString)
    {
        // create the connection
        var connection = new NpgsqlConnection(connectionString);
             connection.Notification += new NotificationEventHandler(Connection_InfoMessage);
             connection.Open();
        if (connection.State == ConnectionState.Broken)
        {
            connection.Close();
            connection.Open();
        }

        return connection;
    }

    /// <summary>
    /// The connection_ info message.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public static void Connection_InfoMessage(object sender, NpgsqlNotificationEventArgs e)
    {
        if (InfoMessage != null)
        {
            InfoMessage(sender, e: new YafDBConnInfoMessageEventArgs(e.PID.ToString(CultureInfo.InvariantCulture) + ":::" + e.Condition));
        }
    }
  }
}
