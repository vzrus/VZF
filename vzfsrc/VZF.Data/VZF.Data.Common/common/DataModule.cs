/* Yet Another Forum.NET
 * Copyright (C) 2006-2011 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;

namespace YAF.Classes.Data
{
  #region Using

  using Autofac;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The data module.
  /// </summary>
  public partial class DataModule : Module
  {
    #region Methods

    /// <summary>
    /// The load.
    /// </summary>
    /// <param name="builder">
    /// The builder.
    /// </param>
    protected override void Load([NotNull] ContainerBuilder builder)
    { string dataEngine = string.Empty;
            string connectionString = string.Empty;
            int connBoardOrObject = 1; string namePattern = string.Empty;
            CommonSqlDbAccess.GetConnectionData(connBoardOrObject, namePattern,out dataEngine, out connectionString);

         /*   switch (dataEngine)
            {
                    // case "System.Data.SqlClient":  builder.RegisterType<MsSqlSrvDbAccess>().As<IDbAccess>().InstancePerDependency();
                    // builder.RegisterType<MsSqlSrvDbConnectionManager>().As<IDbConnectionManager>().InstancePerLifetimeScope();
                case "Npgsql":
                    builder.RegisterType<PostgreDBAccess>().As<IDbAccess>().InstancePerDependency();
                    builder.RegisterType<PostgreDbConnectionManager>().As<IDbConnectionManager>().
                        InstancePerLifetimeScope();
                    break;
                case "MySql.Data.MySqlClient":
                    builder.RegisterType<MySqlDbAccess>().As<IDbAccess>().InstancePerDependency();
                    builder.RegisterType<MySqlDbConnectionManager>().As<IDbConnectionManager>().InstancePerLifetimeScope
                        ();
                    break;
                case "FirebirdSql.Data.FirebirdClient":
                    builder.RegisterType<FbDbAccess>().As<IDbAccess>().InstancePerDependency();
                    builder.RegisterType<FbDbConnectionManager>().As<IDbConnectionManager>().InstancePerLifetimeScope();
                    break; 
                    // case "oracle": return OracleLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case "db2": return Db2LegacyDb.Instance.accessmask_delete(connectionString,accessMaskID);
                    // case "other": return OtherLegacyDb.Instance.accessmask_delete(connectionString,accessMaskID); 
                default:
                    throw new ApplicationException("No config for Board or Object  '{0}' ");
                    break;
            } */
    }

    #endregion
  }
}