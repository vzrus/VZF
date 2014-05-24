namespace VZF.Data.Common
{
    using System.Data;
    using System.Text;

    using VZF.Data.MySql.Mappers;
    using VZF.Utils;

    using YAF.Types;

    /// <summary>
    /// The my sql profile.
    /// </summary>
    public class MySqlProfile
    {
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="setStr">
        /// The set Str.
        /// </param>
        /// <param name="columnStr">
        /// The column Str.
        /// </param>
        /// <param name="valueStr">
        /// The value Str.
        /// </param>
        /// <param name="profileExists">
        /// The profile Exists.
        /// </param>
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
                sqlCommand.Append(" WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName");
            }
            else
            {
                sqlCommand.Append("INSERT {0}").Append(" (UserID,").Append(columnStr.Trim(','));
                sqlCommand.Append(") VALUES (@i_UserID,")
                          .Append(valueStr.Trim(','))
                          .Append(");");
            }

            return sqlCommand.ToString();
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <param name="mid">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        public static string GetProfileStructure()
        {
            return @"SELECT * FROM {0} LIMIT 1";
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
        public static string AddProfileColumn([NotNull] string name, string type, int size, string tableName)
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
        /// The profile exists.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ProfileExists()
        {
            return @"SELECT 1 FROM {0}  WHERE UserId = @i_UserID AND ApplicationName = @i_ApplicationName LIMIT 1";
        }

        #endregion  
    }
}
