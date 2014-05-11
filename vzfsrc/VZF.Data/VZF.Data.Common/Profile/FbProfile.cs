using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VZF.Data.DAL;
using VZF.Utils;
using YAF.Types;


namespace VZF.Data.Common
{
    public class FbProfile
    {
        #region ProfileMirror

        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="mid">
        /// The connection string.
        /// </param>
        /// <param name="boardId">
        /// The board id.
        /// </param>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        /// <param name="dirtyOnly">
        /// The dirty only.
        /// </param>
        public static void SetProfileProperties(
            [NotNull] int? mid,
            [NotNull] int boardId,
            [NotNull] object appName,
            [NotNull] int userId,
            [NotNull] string userName,
            [NotNull] SettingsPropertyValueCollection values,
            [NotNull] List<SettingsPropertyColumn> settingsColumnsList,
            bool dirtyOnly)
        {
            using (var sc = new SQLCommand(mid))
            {
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userId));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));  
           
                string table = ObjectName.GetVzfObjectName("UserProfile", mid);
                var sqlCommand = new StringBuilder();
                /* StringBuilder sqlCommand = new StringBuilder("EXECUTE BLOCK AS BEGIN IF (EXISTS (SELECT 1 FROM ").Append(table);
                 sqlCommand.Append(" WHERE USERID = i_UserID AND ApplicationName = i_ApplicationName)) THEN "); 
               
                 cmd.Parameters.AddWithValue("i_UserID", userID);
                 cmd.Parameters.AddWithValue("i_ApplicationName", appName); */

                // Build up strings used in the query
                var columnStr = new StringBuilder();
                var valueStr = new StringBuilder();
                var setStr = new StringBuilder();
                int count = 0;

                foreach (SettingsPropertyColumn column in settingsColumnsList)
                {
                    // only write if it's dirty
                    if (!dirtyOnly || values[column.Settings.Name].IsDirty)
                    {
                        columnStr.Append(", ");
                        valueStr.Append(", ");
                        columnStr.Append(column.Settings.Name);
                        string valueParam = "@i_Value" + count;
                        valueStr.Append(valueParam);
                       
                        sc.Parameters.Add(sc.CreateParameter(column.DataType, valueParam, values[column.Settings.Name].PropertyValue)); 
 
                        if ((column.DataType != DbType.DateTime) || column.Settings.Name != "LastUpdatedDate"
                            || column.Settings.Name != "LastActivity")
                        {
                            if (count > 0)
                            {
                                setStr.Append(",");
                            }

                            setStr.Append(column.Settings.Name);
                            setStr.Append("=");
                            setStr.Append(valueParam);
                        }

                        count++;
                    }
                }

                columnStr.Append(",LastUpdatedDate ");
                valueStr.Append(",@i_LastUpdatedDate");
                setStr.Append(",LastUpdatedDate=@i_LastUpdatedDate");
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastUpdatedDate", DateTime.UtcNow));                 

                // MembershipUser mu = System.Web.Security.Membership.GetUser(userID);

                columnStr.Append(",LastActivity ");
                valueStr.Append(",@i_LastActivity");
                setStr.Append(",LastActivity=@i_LastActivity");               
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastActivity", DateTime.UtcNow));   

                columnStr.Append(",ApplicationName ");
                valueStr.Append(",@i_ApplicationName");
                setStr.Append(",ApplicationName=@i_ApplicationName");
               // sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));  
                
                columnStr.Append(",IsAnonymous ");
                valueStr.Append(",@i_IsAnonymous");
                setStr.Append(",IsAnonymous=@i_IsAnonymous");
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAnonymous", false));  
              

                columnStr.Append(",UserName ");
                valueStr.Append(",@i_UserName");
                setStr.Append(",UserName=@i_UserName");           
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));  


                /* if (profileExists)
                 {
                     sqlCommand.Append("UPDATE ").Append(table).Append(" SET ").Append(setStr.ToString());
                     sqlCommand.Append(" WHERE UserId = ").Append(userID.ToString()).Append(";");
                 }
                 else
                 {
                     sqlCommand.Append("INSERT INTO ").Append(table).Append(" (UserId").Append(columnStr.ToString());
                     sqlCommand.Append(") VALUES (").Append(userID.ToString()).Append("").Append(valueStr.ToString()).Append(
                       ");");
                 } */

                sqlCommand.Append("UPDATE OR INSERT INTO ")
                          .Append(table)
                          .Append(" (USERID")
                          .Append(columnStr.ToString());
                sqlCommand.Append(") VALUES (@i_UserID")
                          .Append(string.Empty)
                          .Append(valueStr.ToString())
                          .Append(") MATCHING (USERID);");

                sc.CommandText.AppendQuery(sqlCommand.ToString());
                sc.ExecuteNonQuery(CommandType.Text);                
            }
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <returns>
        /// </returns>
        public static DataTable GetProfileStructure([NotNull] int? mid)
        {
            string sql = @"SELECT FIRST 1 * FROM {0}".FormatWith(ObjectName.GetVzfObjectName("UserProfile", mid));

            using (var sc = new SQLCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.Text, true);
            }

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
        public static void AddProfileColumn(
            [NotNull] int? mid, [NotNull] string name, DbType columnType, int size)
        {
            // get column type...
            string type = columnType.ToString();

            if (type.ToLower() == "timestamp")
            { type = "TimeStamp"; }
            if (type.Contains("DateTime"))
            { type = "TIMESTAMP"; }
            if (type.Contains("String"))
            {
                if (size > 256)
                {
                    type = "BLOB SUB_TYPE TEXT";
                }
                else
                {
                    type = "VARCHAR";
                }
            }
            if (type.Contains("Boolean"))
            { type = "SMALLINT"; }
            if (type.Contains("Int32"))
            { type = "INT"; }

            if (size > 0 && size <= 256)
            {
                type += "(" + size.ToString() + ")";
            }
            if (type.ToLowerInvariant().Contains("varchar") && ObjectName.DatabaseEncoding != null)
            {
                type += " CHARACTER SET " + ObjectName.DatabaseEncoding;

                if (ObjectName.DatabaseCollation != null)
                {
                    type += " COLLATE " + ObjectName.DatabaseCollation;
                }
            }
            string sql = "ALTER TABLE {0} ADD {1} {2} ".FormatWith(ObjectName.GetVzfObjectName("UserProfile", mid), name, type);

            using (var sc = new SQLCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                sc.ExecuteNonQuery(CommandType.Text, true);
            }

        }

        /// <summary>
        /// The get db type and size from string.
        /// </summary>
        /// <param name="providerData">
        /// The provider data.
        /// </param>
        /// <param name="dbType">
        /// The db type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The get db type and size from string.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static bool GetDbTypeAndSizeFromString(string providerData, out DbType dbType, out int size)
        {
            size = -1;
            dbType = DbType.String;

            if (providerData.IsNotSet())
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new[] { ';' });

            // first item is the column name...
            string columnName = chunk[0];
            // vzrus addon convert values from mssql types...
            if (chunk[1].ToLowerInvariant().IndexOf("varchar") >= 0)
            {
                chunk[1] = "String";
            }
            if (chunk[1].ToLowerInvariant().IndexOf("int") >= 0)
            {
                chunk[1] = "Int32";
            }
            if (chunk[1].ToLowerInvariant().IndexOf("datetime") >= 0)
            {
                chunk[1] = "DateTime";
            }

            // get the datatype and ignore case...
            dbType = (DbType)Enum.Parse(typeof(DbType), chunk[1], true);

            if (chunk.Length > 2)
            {
                // handle size...
                if (!Int32.TryParse(chunk[2], out size))
                {
                    throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
                }
            }

            return true;
        }     
       
        #endregion
    }
}
