using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using VZF.Data.DAL;
using YAF.Core;
using YAF.Providers.Utils;
using YAF.Types.Interfaces;

namespace YAF.Providers.Profile
{
    using YAF.Classes;

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

            // Application Name
            this._appName = config["applicationName"].ToStringDBNull(Config.ApplicationName);

            // Connection String Name
            this._connStrName = config["connectionStringName"].ToStringDBNull(Config.ConnectionStringName);

            // is the connection string set?
            if (!String.IsNullOrEmpty(this._connStrName))
            {
                string connStr = ConfigurationManager.ConnectionStrings[this._connStrName].ConnectionString;
                ConnectionString = connStr;
                ConnectionStringName = SqlDbAccess.GetConnectionStringNameFromConnectionString(connStr);

                // set the app variable...
                if (YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] == null)
                {
                    YafContext.Current.Get<HttpApplicationStateBase>().Add(ConnStrAppKeyName, connStr);
                }
                else
                {
                    YafContext.Current.Get<HttpApplicationStateBase>()[ConnStrAppKeyName] = connStr;
                }
            }

            base.Initialize(name, config);
        }
    }
}
