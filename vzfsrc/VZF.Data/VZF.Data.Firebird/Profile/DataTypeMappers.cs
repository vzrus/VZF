namespace VZF.Data.Firebird.Mappers
{
    using VZF.Data.DAL;

    /// <summary>
    /// The data type mappers.
    /// </summary>
    public static class DataTypeMappers
    {
        /// <summary>
        /// The type to db value map.
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
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string typeToDbValueMap(string name, string type, int size)
        {

            if (type.Equals("TimeStamp"))
            {
                type = "TIMESTAMP";
            }

            if (type.Contains("DateTime"))
            {
                type = "TIMESTAMP";
            }

            if (type.Equals("String"))
            {
                if (size <= 8 || size > 255)
                {
                    type = "BLOB SUB_TYPE TEXT";
                }
                else
                {
                    type = "VARCHAR";
                }
            }

            if (type.Equals("Boolean"))
            {
                type = "SMALLINT";
            }

            if (type.Contains("Binary"))
            {
                type = "BLOB SUB_TYPE 0";
            }

            if (type.Contains("Int32"))
            {
                type = "INT";
            }

            if (size > 8 && size <= 256)
            {
                type += "(" + size + ")";
            }

            if (!type.ToLowerInvariant().Contains("varchar") || ObjectName.DatabaseEncoding == null)
            {
                return type;
            }

            type += " CHARACTER SET " + ObjectName.DatabaseEncoding;

            if (ObjectName.DatabaseCollation != null)
            {
                type += " COLLATE " + ObjectName.DatabaseCollation;
            }

            return type;
        }

        /// <summary>
        /// The from db value map.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FromDbValueMap(string value)
        {

            // vzrus addon convert values from mssql types...
            if (value.ToLowerInvariant().IndexOf("varchar", System.StringComparison.Ordinal) >= 0)
            {
                value = "String";
            }

            if (value.ToLowerInvariant().IndexOf("int", System.StringComparison.Ordinal) >= 0)
            {
                value = "Int32";
            }

            if (value.ToLowerInvariant().IndexOf("datetime", System.StringComparison.Ordinal) >= 0)
            {
                value = "DateTime";
            }

            if (value.ToLowerInvariant().IndexOf("bit", System.StringComparison.Ordinal) >= 0)
            {
                value = "Boolean";
            }

            if (value.ToLowerInvariant().IndexOf("blob sub_type 0", System.StringComparison.Ordinal) >= 0)
            {
                value = "Bynary";
            }

            if (value.ToLowerInvariant().IndexOf("blob sub_type 1", System.StringComparison.Ordinal) >= 0)
            {
                value = "String";
            }

            return value;
        }
    }
}
