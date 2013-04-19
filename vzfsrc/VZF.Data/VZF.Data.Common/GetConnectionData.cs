
namespace VZF.Data.Common
{
    using System;
    using System.Configuration;
    using System.Text;

    using VZF.Tools;
    using VZF.Utils;

    using YAF.Classes;

    /// <summary>
    /// The common sql db access.
    /// </summary>
    public partial class CommonSqlDbAccess
    {
        /// <summary>
        /// The module type enum.
        /// </summary>
        public enum ModuleTypeEnum
        {
            /// <summary>
            /// The generic.
            /// </summary>
            Generic = 0,

            /// <summary>
            /// The membership.
            /// </summary>
            Membership = 1,

            /// <summary>
            /// The roles.
            /// </summary>
            Roles = 2,

            /// <summary>
            /// The profile.
            /// </summary>
            Profile = 3,

            /// <summary>
            /// The forum board.
            /// </summary>
            ForumBoard = 4
        }

        /// <summary>
        /// The ms sql.
        /// </summary>
        public const string MsSql = "System.Data.SqlClient";

        /// <summary>
        /// The npgsql.
        /// </summary>
        public const string Npgsql = "Npgsql";

        /// <summary>
        /// The my sql.
        /// </summary>
        public const  string MySql = "MySql.Data.MySqlClient";

        /// <summary>
        /// The firebird.
        /// </summary>
        public const string Firebird = "FirebirdSql.Data.FirebirdClient";

        /// <summary>
        /// The oracle.
        /// </summary>
        public const string Oracle = "System.Data.OracleClient";

        /// <summary>
        /// The db 2.
        /// </summary>
        public const string Db2 = "System.Data.Db2";

        public const string Other = "Other";

        /// <summary>
        /// The get connection data.
        /// </summary>
        /// <param name="mid">
        /// The mid.
        /// </param>
        /// <param name="namePattern">
        /// The name pattern.
        /// </param>
        /// <param name="dataEngine">
        /// The data engine.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static bool GetConnectionData(int? mid, string namePattern, out string dataEngine, out string connectionString)
        {
            // string [] patterns = new string[]{"",""};

            // Look for modules special configs.
            string connectionStringName =
                Config.GetConfigValueAsString(string.Format("VZF.ConnectionStringNameModule{0}", mid));
           
            // They were not found gte default.
            if (string.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            if (dataEngine.IsNotSet())
            {
                throw new ApplicationException("No data base engine name was supplied.");
            }

            return true;
        }

        /// <summary>
        /// The get connection data.
        /// </summary>
        /// <param name="moduleClone">
        /// The module clone.
        /// </param>
        /// <param name="dataEngine">
        /// The data engine.
        /// </param>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetConnectionData(ModuleConnection moduleClone, out string dataEngine, out string connectionString)
        {
          // connection string keys have a special format
          // with prefix VZF_ConnStrName 
          // ThenComes a  ModuleTypeEnum defined value 
          // and BoardId or ObjectInnerId which are not module instances number
          // but an inner codes for instance objects. In some cases they can be the same
          // Example^ VZF_ConnStrName_ForumBoard2_BoardID2

            var sb = new StringBuilder("VZF_ConnStrName");
          if (moduleClone.ModuleType == (int)ModuleTypeEnum.ForumBoard)
          {
              sb.AppendFormat("_ForumBoard{0}", moduleClone.ObjectId);
              sb.AppendFormat("_BoardID{0}", moduleClone.ObjectInnerId);
          }

          // Look for board special configs.
          string connectionStringName = Config.GetConfigValueAsString(sb.ToString());

          // They were not found gte default.
          if (string.IsNullOrEmpty(connectionStringName))
          {
              connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
          }

          connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
          dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName; 

          return true;
        }
    }
}
