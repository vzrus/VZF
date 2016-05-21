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
 * File RecaptchaResponse.cs created  on 2.6.2015 in  6:29 AM.
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

namespace VZF.Controls
{
  using YAF.Types;

  /// <summary>
  /// The recaptcha response.
  /// </summary>
  public class RecaptchaResponse
  {
    #region Constants and Fields

    /// <summary>
    ///   The invalid solution.
    /// </summary>
    public static readonly RecaptchaResponse InvalidSolution = new RecaptchaResponse(false, "incorrect-captcha-sol");

    /// <summary>
    ///   The recaptcha not reachable.
    /// </summary>
    public static readonly RecaptchaResponse RecaptchaNotReachable = new RecaptchaResponse(
      false, "recaptcha-not-reachable");

    /// <summary>
    ///   The valid.
    /// </summary>
    public static readonly RecaptchaResponse Valid = new RecaptchaResponse(true, string.Empty);

    /// <summary>
    ///   The error code.
    /// </summary>
    private readonly string errorCode;

    /// <summary>
    ///   The is valid.
    /// </summary>
    private readonly bool isValid;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RecaptchaResponse"/> class.
    /// </summary>
    /// <param name="isValid">
    /// The is valid.
    /// </param>
    /// <param name="errorCode">
    /// The error code.
    /// </param>
    internal RecaptchaResponse(bool isValid, [NotNull] string errorCode)
    {
      this.isValid = isValid;
      this.errorCode = errorCode;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets ErrorCode.
    /// </summary>
    public string ErrorCode
    {
      get
      {
        return this.errorCode;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether IsValid.
    /// </summary>
    public bool IsValid
    {
      get
      {
        return this.isValid;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The equals.
    /// </returns>
    public override bool Equals([NotNull] object obj)
    {
      var response = (RecaptchaResponse)obj;
      if (response == null)
      {
        return false;
      }

      return (response.IsValid == this.IsValid) && (response.ErrorCode == this.ErrorCode);
    }

    /// <summary>
    /// The get hash code.
    /// </summary>
    /// <returns>
    /// The get hash code.
    /// </returns>
    public override int GetHashCode()
    {
      return this.IsValid.GetHashCode() ^ this.ErrorCode.GetHashCode();
    }

    #endregion
  }
}