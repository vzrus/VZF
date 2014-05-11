using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZF.Data.DAL;

namespace VzfDalHelper
{
    public class DalHelper
    {
        string mySqlConnectionString = "";
        string fbConnectionString = "";
        string msSqlConnectionString = "";
        string pgSqlConnectionString = "";
        public DataTable CheckInputParameters()
        {             
            using (var sc = new SQLCommand(mySqlConnectionString))
            {
              /*  sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_BoardID", boardId));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_AccessMaskID", accessMaskID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_ExcludeFlags", excludeFlags));
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_PageUserID", pageUserID));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsUserMask", isUserMask));
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAdminMask", isAdminMask));

                sc.CommandText.AppendObjectQuery("accessmask_list", mid);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true); */
            }
            return null;
        }
    }
}
