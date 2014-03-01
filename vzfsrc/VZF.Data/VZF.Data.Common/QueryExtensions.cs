using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VZF.Data.Common
{
    public static class QueryExtensions
    {
        public static StringBuilder AppendQuery(this StringBuilder sb, string command, int? mid)
        {
            return sb.Append(ObjectName.GetVzfObjectName(command, mid));
        }
    }
}
