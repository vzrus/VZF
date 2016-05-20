#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File QueryCounter.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:21 PM.
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
//  "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
#endregion

namespace VZF.Data.Utils
{
    using System;
    using System.Diagnostics;
    using System.Web;

    using VZF.Utils;

    /// <summary>
  /// The query counter.
  /// </summary>
  public sealed class QueryCounter : IDisposable
  {
    /* Ederon : 6/16/2007 - conventions */
#if DEBUG

    /// <summary>
    /// The _stop watch.
    /// </summary>
    private Stopwatch _stopWatch = new Stopwatch();

    /// <summary>
    /// The _cmd.
    /// </summary>
    private string _cmd;
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryCounter"/> class.
    /// </summary>
    /// <param name="sql">
    /// The sql.
    /// </param>
    public QueryCounter(string sql)
    {
#if DEBUG
      this._cmd = sql;

      if (HttpContext.Current != null)
      {
        if (HttpContext.Current.Items["NumQueries"] == null)
        {
          HttpContext.Current.Items["NumQueries"] = 1;
        }
        else
        {
          HttpContext.Current.Items["NumQueries"] = 1 + (int)HttpContext.Current.Items["NumQueries"];
        }
      }

      this._stopWatch.Start();
#endif
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
#if DEBUG
      this._stopWatch.Stop();

        var duration = this._stopWatch.ElapsedMilliseconds / 1000.0;

        this._cmd = "{0}: {1:N3}".FormatWith(this._cmd, duration);

        if (HttpContext.Current == null)
        {
            return;
        }

        if (HttpContext.Current.Items["TimeQueries"] == null)
        {
            HttpContext.Current.Items["TimeQueries"] = duration;
        }
        else
        {
            HttpContext.Current.Items["TimeQueries"] = duration + (double)HttpContext.Current.Items["TimeQueries"];
        }

        if (HttpContext.Current.Items["CmdQueries"] == null)
        {
            HttpContext.Current.Items["CmdQueries"] = this._cmd;
        }
        else
        {
            HttpContext.Current.Items["CmdQueries"] += "<br />" + this._cmd;
        }

#endif
    }

#if DEBUG

    /// <summary>
    /// The reset.
    /// </summary>
    public static void Reset()
    {
        if (HttpContext.Current == null)
        {
            return;
        }

        HttpContext.Current.Items["NumQueries"] = 0;
        HttpContext.Current.Items["TimeQueries"] = (double)0;
        HttpContext.Current.Items["CmdQueries"] = string.Empty;
    }

    /// <summary>
    /// Gets Count.
    /// </summary>
    public static int Count
    {
      get
      {
          return (int)((HttpContext.Current == null) ? 0 : HttpContext.Current.Items["NumQueries"]);
      }
    }

    /// <summary>
    /// Gets Duration.
    /// </summary>
    public static double Duration
    {
      get
      {
          return (double)((HttpContext.Current == null) ? 0.0 : HttpContext.Current.Items["TimeQueries"]);
      }
    }

    /// <summary>
    /// Gets Commands.
    /// </summary>
    public static string Commands
    {
      get
      {
          return (string)((HttpContext.Current == null) ? string.Empty : HttpContext.Current.Items["CmdQueries"]);
      }
    }
#endif
  }
}