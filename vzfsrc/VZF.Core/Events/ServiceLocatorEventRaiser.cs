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
 * File ServiceLocatorEventRaiser.cs created  on 2.6.2015 in  6:29 AM.
 * Last changed on 5.21.2016 in 1:04 PM.
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

namespace YAF.Core
{
  #region Using

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using YAF.Types;
  using YAF.Types.Interfaces;

  #endregion

  /// <summary>
  /// The autofac event raiser.
  /// </summary>
  public class ServiceLocatorEventRaiser : IRaiseEvent
  {
    #region Constants and Fields

    /// <summary>
    /// The _service locator.
    /// </summary>
    private readonly IServiceLocator _serviceLocator;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceLocatorEventRaiser"/> class.
    /// </summary>
    /// <param name="serviceLocator">
    /// The service Locator.
    /// </param>
    public ServiceLocatorEventRaiser([NotNull] IServiceLocator serviceLocator)
    {
      this._serviceLocator = serviceLocator;
    }

    #endregion

    #region Implemented Interfaces

    #region IRaiseEvent

    /// <summary>
    /// The event raiser.
    /// </summary>
    /// <param name="eventObject">
    /// The event object.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public void Raise<T>(T eventObject) where T : IAmEvent
    {
      this._serviceLocator.Get<IEnumerable<IHandleEvent<T>>>().OrderBy(x => x.Order).ToList().ForEach(
        x => x.Handle(eventObject));
      this._serviceLocator.Get<IEnumerable<IFireEvent<T>>>().OrderBy(x => x.Order).ToList().ForEach(
        x => x.Handle(eventObject));
    }

    /// <summary>
    /// Raise all events using try/catch block.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="eventObject">
    /// </param>
    /// <param name="logExceptionAction">
    /// </param>
    public void RaiseIssolated<T>(T eventObject, [CanBeNull] Action<string, Exception> logExceptionAction)
      where T : IAmEvent
    {
      var eventItems = this._serviceLocator.Get<IEnumerable<IHandleEvent<T>>>().OrderBy(x => x.Order).ToList();

      foreach (var theHandler in eventItems)
      {
        try
        {
          theHandler.Handle(eventObject);
        }
        catch (Exception ex)
        {
          if (logExceptionAction != null)
          {
            logExceptionAction(theHandler.GetType().Name, ex);
          }
        }
      }

      var fireEventItems =
        this._serviceLocator.Get<IEnumerable<IFireEvent<T>>>().OrderBy(x => x.Order).ToList().ToList();

      foreach (var theFireEventHandler in fireEventItems)
      {
        try
        {
          theFireEventHandler.Handle(eventObject);
        }
        catch (Exception ex)
        {
          if (logExceptionAction != null)
          {
            logExceptionAction(theFireEventHandler.GetType().Name, ex);
          }
        }
      }
    }

    #endregion

    #endregion
  }
}