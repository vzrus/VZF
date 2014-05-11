using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace VZF.Data.DAL
{
    public static class QueryExtensions
    {
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, int? mid)
        {
            return sb.Append(ObjectName.GetVzfObjectName(command, mid));
        }
        public static StringBuilder AppendObjectQuery(this StringBuilder sb, string command, string connectionStringName)
        {
            return sb.Append(ObjectName.GetVzfObjectName(command, SqlDbAccess.GetProviderName(connectionStringName)));
        }
        public static StringBuilder AppendQuery(this StringBuilder sb, string command)
        {
            return sb.Append(command);
        }
       
    }
}
