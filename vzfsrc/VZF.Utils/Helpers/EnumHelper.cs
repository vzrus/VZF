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
 * File EnumHelper.cs created  on 2.6.2015 in  6:31 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
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

namespace VZF.Utils
{
  #region Using

  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using YAF.Types.Attributes;

  #endregion

  /// <summary>
  /// The enum helper.
  /// </summary>
  public static class EnumHelper
  {
    #region Public Methods

    /// <summary>
    /// Converts an Enum to a Dictionary
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static Dictionary<int, string> EnumToDictionary<T>()
    {
      Type enumType = typeof(T);

      if (enumType.BaseType != typeof(Enum))
      {
        throw new ApplicationException("EnumToDictionary does not support non-enum types");
      }

      var list = new Dictionary<int, string>();

      foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
      {
        int value;
        string display;
        value = (int)field.GetValue(null);
        display = Enum.GetName(enumType, value);

        var attribs = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

        // Return the first if there was a match.
        if (attribs != null && attribs.Length > 0)
        {
          display = attribs[0].StringValue;
        }

        // add the value...
        list.Add(value, display);
      }

      return list;
    }

    /// <summary>
    /// Converts an Enum to a Dictionary
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// </returns>
    public static Dictionary<byte, string> EnumToDictionaryByte<T>()
    {
      Type enumType = typeof(T);

      if (enumType.BaseType != typeof(Enum))
      {
        throw new ApplicationException("EnumToDictionaryByte does not support non-enum types");
      }

      var list = new Dictionary<byte, string>();

      foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
      {
        byte value;
        string display;
        value = (byte)field.GetValue(null);
        display = Enum.GetName(enumType, value);

        var attribs = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

        // Return the first if there was a match.
        if (attribs != null && attribs.Length > 0)
        {
          display = attribs[0].StringValue;
        }

        // add the value...
        list.Add(value, display);
      }

      return list;
    }

    #endregion
  }
}