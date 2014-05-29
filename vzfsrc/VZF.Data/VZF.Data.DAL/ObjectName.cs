// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectName.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ObjectName type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using VZF.Utils;
    using YAF.Classes;

    public class ObjectName
    {
        /// <summary>
        /// The _db owner.
        /// </summary>
        private static string _dbOwner;

        /// <summary>
        /// The _object qualifier.
        /// </summary>
        private static string _objectQualifier;

        /// <summary>
        /// The _schema name.
        /// </summary>
        private static string _databaseSchemaName;

        /// <summary>
        /// The _database name.
        /// </summary>
        private static string _databaseName;

        /// <summary>
        /// The _grantee name.
        /// </summary>
        private static string _granteeName;

        private static Dictionary<string, string> objectNameDictionary;

        /// <summary>
        /// The _host name.
        /// </summary>
        private static string _hostName;

        /// <summary>
        /// The _database encoding.
        /// </summary>
        private static string _databaseEncoding;

        /// <summary>
        /// The database collation.
        /// </summary>
        private static string databaseCollation;

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        public static string DatabaseOwner
        {
            get
            {
                return _dbOwner ?? (_dbOwner = Config.DatabaseOwner);
            }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        public static string ObjectQualifier
        {
            get
            {
                return _objectQualifier ?? (_objectQualifier = Config.DatabaseObjectQualifier);
            }
        }

        /// <summary>
        /// Gets the schema name.
        /// </summary>
        public static string DatabaseSchemaName
        {
            get
            {
                return _databaseSchemaName ?? (_databaseSchemaName = Config.DatabaseSchemaName);
            }
        }

        /// <summary>
        /// Gets the database encoding.
        /// </summary>
        public static string DatabaseEncoding
        {
            get
            {
                return _databaseEncoding ?? (_databaseEncoding = Config.DatabaseEncoding);
            }
        }

        /// <summary>
        /// Gets the database collation.
        /// </summary>
        public static string DatabaseCollation
        {
            get
            {
                return databaseCollation
                       ?? (databaseCollation = Config.GetConfigValueAsString("YAF.DatabaseCollation"));
            }
        }

        /// <summary>
        /// The get vzf object name.
        /// </summary>
        /// <param name="objectName">
        /// The object name.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string GetVzfObjectName(string objectName, string providerName)
        {
            
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    return "[{0}].[{1}{2}]".FormatWith(Config.DatabaseOwner, Config.DatabaseObjectQualifier, objectName);
                case "Npgsql":
                    return string.Format("{0}.{1}{2}", DatabaseSchemaName, ObjectQualifier, objectName);
                case "MySql.Data.MySqlClient":
                    return string.Format(
                        "`{0}`.`{1}{2}`",
                        DatabaseSchemaName,
                        Config.DatabaseObjectQualifier,
                        objectName);
                case "FirebirdSql.Data.FirebirdClient":
                    return string.Format("{0}{1}", ObjectQualifier, objectName);
                default:
                    throw new ArgumentOutOfRangeException(providerName);
            }
        }

        public static string GetVzfObjectName(string objectName, int? mid)
        {
            var providerName =
                SqlDbAccess.GetProviderName(SqlDbAccess.GetConnectionStringName(mid, string.Empty));
            return GetVzfObjectName(objectName, providerName);           
        }

        public static string GetVzfObjectNameFromConnectionString(string objectName, string connectionStringName)
        {
            var providerName =
                SqlDbAccess.GetProviderName(connectionStringName);
            return GetVzfObjectName(objectName, providerName);
            return new SQLCommand(connectionStringName, providerName).DataSource.WrapObjectName(objectName);
        }

       /* public static void AddWithValue(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }   */    
    }
}
