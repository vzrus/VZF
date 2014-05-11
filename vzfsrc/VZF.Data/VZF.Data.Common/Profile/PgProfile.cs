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
    public class PgProfile
    {
        #region ProfileMirror


        /// <summary>
        /// The set profile properties.
        /// </summary>
        /// <param name="appName">
        /// The app name.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="settingsColumnsList">
        /// The settings columns list.
        /// </param>
        public static void SetProfileProperties(
            [NotNull] int? mid,
            [NotNull] int boardId,
            [NotNull] object appName,
            [NotNull] int userID,
            [NotNull] string userName,
            [NotNull] SettingsPropertyValueCollection values,
            [NotNull] List<SettingsPropertyColumn> settingsColumnsList,
            bool dirtyOnly)
        {
            if (userName.IsNotSet())
            {
                return;
            }

            using (var sc = new SQLCommand(mid))
            {
                string table = ObjectName.GetVzfObjectName("UserProfile", mid);
                StringBuilder sqlCommand = new StringBuilder("SELECT 1 FROM ").Append(table);
                sqlCommand.Append(" WHERE UserID = :UserID AND ApplicationName = :ApplicationName");
                sc.Parameters.Add(sc.CreateParameter(DbType.Int32, "i_UserID", userID));
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));
                sc.CommandText.AppendQuery(sqlCommand.ToString());
                object o = Convert.ToInt32(sc.ExecuteScalar()) > 0;               
                int dd;

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
                        string valueParam = ":Value" + count;
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
                valueStr.Append(",:i_LastUpdatedDate");
                setStr.Append(",LastUpdatedDate=:i_LastUpdatedDate");
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastUpdatedDate", DateTime.UtcNow));     

                // MembershipUser mu = System.Web.Security.Membership.GetUser(userID);
                columnStr.Append(",LastActivity ");
                valueStr.Append(",:i_LastActivity");
                setStr.Append(",LastActivity=:i_LastActivity");
                sc.Parameters.Add(sc.CreateParameter(DbType.DateTime, "i_LastActivity", DateTime.UtcNow));   

                columnStr.Append(",ApplicationName ");
                valueStr.Append(",:ApplicationName");
                setStr.Append(",ApplicationName=:ApplicationName");
               // sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_ApplicationName", appName));  

                columnStr.Append(",IsAnonymous ");
                valueStr.Append(",:IsAnonymous");
                setStr.Append(",IsAnonymous=:IsAnonymous");
                sc.Parameters.Add(sc.CreateParameter(DbType.Boolean, "i_IsAnonymous", false));  

                columnStr.Append(",UserName ");
                valueStr.Append(",:UserName");
                setStr.Append(",UserName=:UserName");
                sc.Parameters.Add(sc.CreateParameter(DbType.String, "i_UserName", userName));  

                // the user  exists. 
                sqlCommand.Clear();
                if (o != null && int.TryParse(o.ToString(), out dd))
                {
                    sqlCommand.Append("UPDATE ").Append(table).Append(" SET ").Append(setStr.ToString());
                    sqlCommand.Append(" WHERE UserID = ").Append(userID.ToString()).Append(string.Empty);
                }
                else
                {
                    sqlCommand.Append("INSERT INTO ").Append(table).Append(" (UserID").Append(columnStr.ToString());
                    sqlCommand.Append(") VALUES (")
                              .Append(userID.ToString())
                              .Append(string.Empty)
                              .Append(valueStr.ToString())
                              .Append(")");
                }

                sc.CommandText.AppendQuery(sqlCommand.ToString());
                sc.ExecuteNonQuery();  
            }
        }

        /// <summary>
        /// The get profile structure.
        /// </summary>
        /// <returns>
        /// </returns>
        public static DataTable GetProfileStructure([NotNull] int? mid)
        {
            string sql = @"SELECT * FROM {0} LIMIT 1".FormatWith(ObjectName.GetVzfObjectName("UserProfile", mid));

            using (var sc = new SQLCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                return sc.ExecuteDataTableFromReader(CommandBehavior.Default, CommandType.StoredProcedure, true);
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
            // get column type..
            string type = columnType.ToString();

            if (type.ToLower() == "timestamp")
            { type = "TimeStamp"; }
            if (type.Contains("DateTime"))
            { type = "TIMESTAMP"; }
            if (type.Contains("String"))
            { type = "VARCHAR"; }
            if (type.Contains("Boolean"))
            { type = "SMALLINT"; }
            if (type.Contains("Int32"))
            { type = "INT"; }

            if (size > 0)
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

            string sql = "ALTER TABLE {0} ADD {1} {2}".FormatWith(
                ObjectName.GetVzfObjectName("UserProfile", mid), name, type);

            using (var sc = new SQLCommand(mid))
            {
                sc.CommandText.AppendQuery(sql);
                sc.ExecuteNonQuery(CommandType.StoredProcedure, true);
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

            if (string.IsNullOrEmpty(providerData))
            {
                return false;
            }

            // split the data
            string[] chunk = providerData.Split(new char[] { ';' });

            // first item is the column name..
            string columnName = chunk[0];

            // vzrus addon convert values from mssql types..
            if (chunk[1].IndexOf("varchar", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Varchar";
            }

            if (chunk[1].IndexOf("int", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Integer";
            }

            if (chunk[1].IndexOf("DateTime", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                chunk[1] = "Timestamp";
            }


            // get the datatype and ignore case..
            dbType = (DbType)Enum.Parse(typeof(DbType), chunk[1], true);

            if (chunk.Length <= 2)
            {
                return true;
            }

            // handle size..
            if (!int.TryParse(chunk[2], out size))
            {
                throw new ArgumentException("Unable to parse as integer: " + chunk[2]);
            }

            return true;
        }

        #endregion
    }
}
