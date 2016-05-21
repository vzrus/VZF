#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File StartupCheckBannedIps.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:05 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

namespace YAF.Core.Services
{
  #region Using

  using System.Collections.Generic;
  using System.Data;
  using System.Linq;
  using System.Web;

  using VZF.Data.Common;

  
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Interfaces;
  using VZF.Utils.Helpers;

  #endregion

  /// <summary>
  /// The yaf check banned ips.
  /// </summary>
  public class StartupCheckBannedIps : BaseStartupService
  {
    #region Constants and Fields

    /// <summary>
    ///   The _init var name.
    /// </summary>
    protected const string _initVarName = "YafCheckBannedIps_Init";

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StartupCheckBannedIps"/> class.
    /// </summary>
    /// <param name="dataCache">
    /// The data cache.
    /// </param>
    /// <param name="httpResponseBase">
    /// The http response base.
    /// </param>
    /// <param name="httpRequestBase">
    /// The http request base.
    /// </param>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public StartupCheckBannedIps(
      [NotNull] IDataCache dataCache, 
      [NotNull] HttpResponseBase httpResponseBase, 
      [NotNull] HttpRequestBase httpRequestBase, [NotNull] ILogger logger)
    {
      this.DataCache = dataCache;
      this.HttpResponseBase = httpResponseBase;
      this.HttpRequestBase = httpRequestBase;
      this.Logger = logger;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets DataCache.
    /// </summary>
    public IDataCache DataCache { get; set; }

    /// <summary>
    ///   Gets or sets HttpRequestBase.
    /// </summary>
    public HttpRequestBase HttpRequestBase { get; set; }

    /// <summary>
    ///   Gets or sets HttpResponseBase.
    /// </summary>
    public HttpResponseBase HttpResponseBase { get; set; }

    /// <summary>
    /// Gets or sets Logger.
    /// </summary>
    public ILogger Logger { get; set; }

    /// <summary>
    ///   Gets InitVarName.
    /// </summary>
    [NotNull]
    protected override string InitVarName
    {
      get
      {
        return "YafCheckBannedIps_Init";
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The run service.
    /// </summary>
    /// <returns>
    /// The run service.
    /// </returns>
    protected override bool RunService()
    {
        // TODO: The data cache needs a more fast string array check as number of banned ips can be huge, but current output is too demanding on perfomance in the cases.
        var bannedIPs = this.DataCache.GetOrSet(
            Constants.Cache.BannedIP, () =>
            {
                var dt = CommonDb.bannedip_list(YafContext.Current.PageModuleID, YafContext.Current.PageBoardID, null, 0, 1000000);
               
                if (dt != null && dt.Rows.Count > 0)
                {
                    var lst = new string[dt.Rows.Count];
                    int cntr = 0;
                    foreach (DataRow vv in dt.Rows)
                    {
                        lst[cntr] = vv["Mask"].ToString().Trim();
                        cntr++;
                    }

                    return lst.ToList();
                }

                return null;
            });
        
        // check for this user in the list...
        if (this.HttpRequestBase.ServerVariables["REMOTE_ADDR"] != null && bannedIPs != null && bannedIPs.Any(
            row => IPHelper.IsBanned(row, this.HttpRequestBase.ServerVariables["REMOTE_ADDR"])))
        {
            // we're done...
            this.Logger.Info(@"Ending Response for Banned User at IP ""{0}""", this.HttpRequestBase.ServerVariables["REMOTE_ADDR"]);
            this.HttpResponseBase.End();
            return false;
        }
        
        return true;
    }

    #endregion
  }
}