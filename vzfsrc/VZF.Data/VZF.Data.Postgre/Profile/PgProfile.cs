namespace VZF.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Text;

    using VZF.Data.DAL;
    using VZF.Data.Postgre.Mappers;
    using VZF.Utils;

    using YAF.Types;

    /// <summary>
    /// The pg profile.
    /// </summary>
    public class PgProfile
    {
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="boardId">
        /// The boardId.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        /// <param name="dirtyOnly">The dirty only.</param>
        public static string SetProfileProperties(
            [NotNull] string setStr,
            [NotNull] string columnStr,
            [NotNull] string valueStr,
            [NotNull] bool profileExists)
        {   

            StringBuilder sqlCommand = new StringBuilder();

            if (profileExists)
            {
                sqlCommand.Append("UPDATE {0}").Append(" SET ").Append(setStr.Trim(','));
                sqlCommand.Append(" WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName ");
            }
            else
            {
                sqlCommand.Append("INSERT INTO {0}").Append(" (UserID,").Append(columnStr.Trim(','));
                sqlCommand.Append(") VALUES (@i_UserID,")
                          .Append(valueStr.Trim(','))
                          .Append(")");
            }

           return sqlCommand.ToString();           
        }

        /// <summary>
        /// Gets the profile structure.
        /// </summary>
        public static string ProfileStructure
        {
            get
            {
                return @"SELECT * FROM {0} LIMIT 1";
            }
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        public static string AddProfileColumn(
            [NotNull] string name, string type, int size, string tableName)
        {
            type = DataTypeMappers.typeToDbValueMap(name, type, size);

            return "ALTER TABLE {0} ADD {1} {2}".FormatWith(
                tableName, name, type);
        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="chunk">
        /// The chunk.
        /// </param>
        /// <returns>
        /// The get db type and size from string.
        /// </returns>
        public static string[] GetDbTypeAndSizeFromString(string[] chunk)
        {
            string paramName = DataTypeMappers.FromDbValueMap(chunk[1]);
            chunk[1] = paramName;

            return chunk;
        }

        /// <summary>
        /// Gets the profile exists.
        /// </summary>
        public static string ProfileExists
        {
            get
            {
                return @"SELECT 1 FROM {0}  WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName LIMIT 1";
            }
        }

        #endregion
    }
}
