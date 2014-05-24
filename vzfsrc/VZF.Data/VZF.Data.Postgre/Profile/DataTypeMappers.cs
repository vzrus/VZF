namespace VZF.Data.Postgre.Mappers
{
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
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string typeToDbValueMap(string name, string type, int size)
        {
            if (type.ToLower().Contains("datetime"))
            {
                type = "timestamp";
            }

            if (type.Contains("String"))
            {
                if (size > 21844)
                {
                    type = "text";
                }
                else
                {
                    type = "varchar";
                }
            }

            if (type.Contains("Int32"))
            {
                type = "integer";
            }

            if (type.Contains("Boolean"))
            {
                type = "TINYINT";
            }

            if (size > 0)
            {
                type += "(" + size + ")";
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
            // vzrus: here we replace MS SQL data types
            if (value.ToLowerInvariant().IndexOf("varchar", System.StringComparison.Ordinal) >= 0
                    || value.ToLowerInvariant().IndexOf("text", System.StringComparison.Ordinal) >= 0)
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

            return value;
        }
    }
}
