namespace VZF.Data.Common
{
    using System.Text;

    using VZF.Data.MsSql.Mappers;
    using VZF.Utils;

    using YAF.Types;

    /// <summary>
    /// The ms profile.
    /// </summary>
    public static class MsProfile
    {       
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="setStr">
        /// The set str.
        /// </param>
        /// <param name="columnStr">
        /// The column str.
        /// </param>
        /// <param name="valueStr">
        /// The value str.
        /// </param>
        /// <param name="profileExists">
        /// The profile exists.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SetProfileProperties(
            [NotNull] string setStr,
            [NotNull] string columnStr,
            [NotNull] string valueStr,
            [NotNull] bool profileExists) 
        {            
            var sqlCommand = new StringBuilder();

            if (profileExists)
            {
                sqlCommand.Append("UPDATE {0}").Append(" SET ").Append(setStr.Trim(','));
                sqlCommand.Append(" WHERE UserID = @i_UserID AND ApplicationName = @i_ApplicationName");
            }
            else
            {
                sqlCommand.Append("INSERT {0}").Append(" (UserID,").Append(columnStr.Trim(','));
                sqlCommand.Append(") VALUES (@i_UserID,").Append(valueStr.Trim(',')).Append(
                  ")");
            }

            return sqlCommand.ToString();
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetProfileStructure()
        {
            return "SELECT TOP 1 * FROM {0}";
        }

        /// <summary>
        /// The add profile column.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="columnType">
        /// The column type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        public static string AddProfileColumn([NotNull] string name, string columnType, int size, string tableName)
        {
            string type = DataTypeMappers.typeToDbValueMap(name, columnType, size);

            return "ALTER TABLE {0} ADD [{1}] {2} NULL".FormatWith(
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
            // vzrus: here we replace MS SQL data types
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
            return  "SELECT top 1 1 FROM {0} WHERE UserID = @UserID AND ApplicationName = @ApplicationName";
        }

        #endregion
    }
}
