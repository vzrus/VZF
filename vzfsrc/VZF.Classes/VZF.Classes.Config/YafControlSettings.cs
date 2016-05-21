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
 * File YafControlSettings.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:08 PM.
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

namespace YAF.Classes
{
  using YAF.Classes.Pattern;
  using YAF.Types;

  /// <summary>
  /// Class provides glue/settings transfer between YAF forum control and base classes
  /// </summary>
  public class YafControlSettings
  {
    /* Ederon : 6/16/2007 - conventions */

    /// <summary>
    /// The _board id.
    /// </summary>
    private int _boardId;

    /// <summary>
    /// The _module id.
    /// </summary>
    private int _moduleId;

    /// <summary>
    /// The _category id.
    /// </summary>
    private int _categoryId;

    /// <summary>
    /// The _locked forum.
    /// </summary>
    private int _lockedForum;

    /// <summary>
    /// The _popup.
    /// </summary>
    private bool _popup;

    /// <summary>
    /// Initializes a new instance of the <see cref="YafControlSettings"/> class.
    /// </summary>
    public YafControlSettings()
    {
      if (!int.TryParse(Config.CategoryId, out this._categoryId))
      {
        this._categoryId = 0; // Ederon : 6/16/2007 - changed from 1 to 0
      }
      if (!int.TryParse(Config.BoardId, out this._boardId))
      {
        this._boardId = 1;
      }
    }

    /// <summary>
    /// Gets Current.
    /// </summary>
    public static YafControlSettings Current
    {
      get
      {
        return PageSingleton<YafControlSettings>.Instance;
      }
    }

    /// <summary>
    /// Gets or sets BoardID.
    /// </summary>
    public int BoardID
    {
      get
      {
        return this._boardId;
      }

      set
      {
        this._boardId = value;
      }
    }

    /// <summary>
    /// Gets or sets ModuleID.
    /// </summary>
    public int ModuleID
    {
        get
        {
            return this._moduleId;
        }

        set
        {
            this._moduleId = value;
        }
    }

    /// <summary>
    /// Gets or sets CategoryID.
    /// </summary>
    public int CategoryID
    {
      get
      {
        return this._categoryId;
      }

      set
      {
        this._categoryId = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Popup.
    /// </summary>
    public bool Popup
    {
      get
      {
        return this._popup;
      }

      set
      {
        this._popup = value;
      }
    }

    /// <summary>
    /// Gets or sets LockedForum.
    /// </summary>
    public int LockedForum
    {
      get
      {
        return this._lockedForum;
      }

      set
      {
        this._lockedForum = value;
      }
    }
  }
}