using System.Configuration;
using System.Text;
// using VZF.Tools;
// using VZF.Tools;
using VZF.Tools;
using YAF.Utils;

namespace YAF.Classes.Data
{

    public partial class CommonSqlDbAccess
    {
        public enum ModuleTypeEnum
        {
            Generic = 0,
            Membership = 1,
            Roles = 2,
            Profile = 3,
            ForumBoard = 4
        }
        public static bool GetConnectionData(int boardId, string namePattern, out string dataEngine, out string connectionString)
        {
            // string [] patterns = new string[]{"",""};
            // Look for board special configs.
             
            string connectionStringName =
                Config.GetConfigValueAsString(string.Format("VZF.ConnectionStringNameBoard{0}", boardId));
            // They were not found gte default.
            if (string.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = Config.GetConfigValueAsString("YAF.ConnectionStringName") ?? "yafnet";
            }

            connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            dataEngine = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;

            return true;
        }
        public static bool GetConnectionData(ModuleConnection moduleClone, out string dataEngine, out string connectionString)
        {
          // connection string keys have a special format
          // with prefix VZF_ConnStrName 
          // ThenComes a  ModuleTypeEnum defined value 
          // and BoardId or ObjectInnerId which are not module instances number
          // but an inner codes for instance objects. In some cases they can be the same
          // Example^ VZF_ConnStrName_ForumBoard2_BoardID2
          string connectionStringName;
          StringBuilder sb = new StringBuilder("VZF_ConnStrName");
          if (moduleClone.ModuleType == (int)ModuleTypeEnum.ForumBoard)
          {
              sb.AppendFormat("_ForumBoard{0}", moduleClone.ObjectId);
              sb.AppendFormat("_BoardID{0}", moduleClone.ObjectInnerId);
          }
          // Look for board special configs.
          connectionStringName =
              Config.GetConfigValueAsString(sb.ToString());
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
