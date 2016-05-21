
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
 * File ActiveLocation.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:07 PM.
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


namespace YAF.Types
{
  using System;

  /// <summary>
  /// Provides functions used for code contracts.
  /// </summary>
  public static class CodeContracts
  {
    /// <summary>
    /// Validates argument (obj) is not <see langword="null"/>. Throws exception
    /// if it is.
    /// </summary>
    /// <typeparam name="T">type of the argument that's being verified
    /// </typeparam>
    /// <param name="obj">value of argument to verify not null</param>
    /// <param name="argumentName">name of the argument</param>
    /// <exception cref="ArgumentNullException"><paramref name="obj" /> is 
    /// <c>null</c>.</exception>
    [AssertionMethod]
    public static void ArgumentNotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)]T obj, string argumentName) where T : class
    {
      if (obj == null)
      {
        throw new ArgumentNullException(argumentName, String.Format("{0} cannot be null", argumentName));
      }
    }
  }
}