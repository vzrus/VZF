using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using VZF.Data.DAL;
using YAF.Core;
using YAF.Providers.Utils;

namespace YAF.Providers.Profile
{
    public partial class PgProfileProvider 
    {


        /// <summary>
        /// Sets up the profile providers
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        /// <summary>
        /// Sets up the profile providers
        /// </summary>
        /// <param name="name">
        /// </param>
        /// <param name="config">
        /// </param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // verify that the configuration section was properly passed
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Connection String Name
            this._connStrName = config["connectionStringName"].ToStringDBNull();

            // application name
            this._appName = config["applicationName"];

            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "YetAnotherForum";
            }

            // is the connection string set?
            if (!String.IsNullOrEmpty(this._connStrName))
            {
                string connStr = ConfigurationManager.ConnectionStrings[this._connStrName].ConnectionString;
                ConnectionString = connStr;
                ConnectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connStr);
                // set the app variable...
                if (YafContext.Application[ConnStrAppKeyName] == null)
                {
                    YafContext.Application.Add(ConnectionString, connStr);
                }
                else
                {
                    YafContext.Application[ConnStrAppKeyName] = connStr;
                }
            }

            base.Initialize(name, config);
        }
    }
}
