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
 * File StreamExtensions.cs created  on 2.6.2015 in  6:31 AM.
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
  using System.IO;

  public static class StreamExtensions
  {
    /// <summary>
    /// Converts a Stream to a String.
    /// </summary>
    /// <param name="theStream">
    /// </param>
    /// <returns>
    /// The stream to string.
    /// </returns>
    public static string AsString(this Stream theStream)
    {
      var reader = new StreamReader(theStream);
      return reader.ReadToEnd();
    }

    /// <summary>
    /// The copy stream.
    /// </summary>
    /// <param name="input">
    /// The input.
    /// </param>
    /// <param name="output">
    /// The output.
    /// </param>
    public static void CopyTo(this Stream input, Stream output)
    {
      var buffer = new byte[1024];
      int count = buffer.Length;

      while (count > 0)
      {
        count = input.Read(buffer, 0, count);
        if (count > 0)
        {
          output.Write(buffer, 0, count);
        }
      }
    }
  }
}