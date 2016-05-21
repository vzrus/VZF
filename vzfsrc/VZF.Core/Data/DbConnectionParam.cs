#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File DbConnectionParam.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:04 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

namespace YAF.Core.Data
{
	using YAF.Types;
	using YAF.Types.Interfaces.Data;

	//public class MsSqlDbSetup
	//{
	//  /// <summary>
	//  /// Gets DbConnectionParameters.
	//  /// </summary>
	//  IDbConnectionParam DbConnectionParameters { get; protected set; }

	//  #region Constants and Fields

	//  /// <summary>
	//  ///   The _script list.
	//  /// </summary>
	//  private static readonly string[] _scriptList = 
	//      {
	//          "mssql/tables.sql", 
	//          "mssql/indexes.sql", 
	//          "mssql/views.sql",
	//          "mssql/constraints.sql", 
	//          "mssql/triggers.sql",
	//          "mssql/functions.sql", 
	//          "mssql/procedures.sql",
	//          "mssql/providers/tables.sql",
	//          "mssql/providers/indexes.sql",
	//          "mssql/providers/procedures.sql" 
	//      };

	//  /// <summary>
	//  ///   The _full text script.
	//  /// </summary>
	//  private static string _fullTextScript = "mssql/fulltext.sql";

	//  /// <summary>
	//  ///   The _full text supported.
	//  /// </summary>
	//  private static bool _fullTextSupported = true;

	//  #endregion

	//  #region Properties

	//  /// <summary>
	//  ///   Gets a value indicating whether IsForumInstalled.
	//  /// </summary>
	//  public static bool GetIsForumInstalled()
	//  {
	//    try
	//    {
	//      using (DataTable dt = board_list(DBNull.Value))
	//      {
	//        return dt.Rows.Count > 0;
	//      }
	//    }
	//    catch
	//    {
	//    }

	//    return false;
	//  }

	//  /// <summary>
	//  ///   Gets the database size
	//  /// </summary>
	//  /// <returns>intager value for database size</returns>
	//  public static int GetDBSize()
	//  {
	//    using (var cmd = new SqlCommand("select sum(cast(size as integer))/128 from sysfiles"))
	//    {
	//      cmd.CommandType = CommandType.Text;
	//      return (int)Current.ExecuteScalar(cmd);
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets DBVersion.
	//  /// </summary>
	//  public static int GetDBVersion()
	//  {
	//    try
	//    {
	//      using (DataTable dt = registry_list("version"))
	//      {
	//        if (dt.Rows.Count > 0)
	//        {
	//          // get the version...
	//          return dt.Rows[0]["Value"].ToType<int>();
	//        }
	//      }
	//    }
	//    catch
	//    {
	//      // not installed...
	//    }

	//    return -1;
	//  }

	//  /// <summary>
	//  ///   Gets or sets FullTextScript.
	//  /// </summary>
	//  public static string FullTextScript
	//  {
	//    get
	//    {
	//      return _fullTextScript;
	//    }

	//    set
	//    {
	//      _fullTextScript = value;
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets or sets a value indicating whether FullTextSupported.
	//  /// </summary>
	//  public static bool FullTextSupported
	//  {
	//    get
	//    {
	//      return _fullTextSupported;
	//    }

	//    set
	//    {
	//      _fullTextSupported = value;
	//    }
	//  }


	//  /// <summary>
	//  ///   Gets a value indicating whether PanelGetStats.
	//  /// </summary>
	//  public static bool PanelGetStats
	//  {
	//    get
	//    {
	//      return true;
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets a value indicating whether PanelRecoveryMode.
	//  /// </summary>
	//  public static bool PanelRecoveryMode
	//  {
	//    get
	//    {
	//      return true;
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets a value indicating whether PanelReindex.
	//  /// </summary>
	//  public static bool PanelReindex
	//  {
	//    get
	//    {
	//      return true;
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets a value indicating whether PanelShrink.
	//  /// </summary>
	//  public static bool PanelShrink
	//  {
	//    get
	//    {
	//      return true;
	//    }
	//  }



	//  /// <summary>
	//  /// Lists the UI parameters...
	//  /// </summary>
	//  public static DbConnectionUIParam[] DbUIParameters = new DbConnectionUIParam[]
	//      {
	//        new DbConnectionUIParam(1, "Data Source", "(local)", true), 
	//        new DbConnectionUIParam(2, "Initial Catalog", string.Empty, true), 
	//        new DbConnectionUIParam(11, "Use Integrated Security", "true", true),
	//      };

	//  /// <summary>
	//  ///   Gets a value indicating whether PasswordPlaceholderVisible.
	//  /// </summary>
	//  public static bool PasswordPlaceholderVisible
	//  {
	//    get
	//    {
	//      return false;
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets ProviderAssemblyName.
	//  /// </summary>
	//  [NotNull]
	//  public static string ProviderAssemblyName
	//  {
	//    get
	//    {
	//      return "System.Data.SqlClient";
	//    }
	//  }

	//  /// <summary>
	//  ///   Gets ScriptList.
	//  /// </summary>
	//  public static string[] ScriptList
	//  {
	//    get
	//    {
	//      return _scriptList;
	//    }
	//  }

	//  #endregion
	//}

	/// <summary>
	/// The db connection param.
	/// </summary>
	public class DbConnectionParam : IDbConnectionParam
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DbConnectionParam"/> class.
		/// </summary>
		/// <param name="id">
		/// The id.
		/// </param>
		/// <param name="name">
		/// The name.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <param name="visible">
		/// The visible.
		/// </param>
		public DbConnectionParam(int id, [NotNull] string name = null, [NotNull] string value = null, bool visible = false)
		{
			this.ID = id;
			this.Label = name ?? string.Empty;
			this.DefaultValue = value ?? string.Empty;
			this.Visible = visible;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets DefaultValue.
		/// </summary>
		public string DefaultValue { get; protected set; }

		/// <summary>
		/// Gets or sets ID.
		/// </summary>
		public int ID { get; protected set; }

		/// <summary>
		/// Gets or sets Label.
		/// </summary>
		public string Label { get; protected set; }

		/// <summary>
		/// Gets or sets a value indicating whether Visible.
		/// </summary>
		public bool Visible { get; protected set; }

		#endregion
	}
}