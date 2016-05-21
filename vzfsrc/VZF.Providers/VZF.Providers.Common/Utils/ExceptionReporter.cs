#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File ExceptionReporter.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:10 PM.
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

namespace YAF.Providers.Utils
{
  #region Using

  using System;
  using System.Configuration.Provider;
  using System.Web;
  using System.Xml;

  using YAF.Classes;
  using VZF.Utils;
  using YAF.Types;

  #endregion

  /// <summary>
  /// The exception reporter.
  /// </summary>
  public static class ExceptionReporter
  {
    #region Properties

    /// <summary>
    ///   Get Exception XML File Name from AppSettings
    /// </summary>
    [NotNull]
    private static string ProviderExceptionFile
    {
        get
        {
            return Config.ProviderExceptionXML;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Get Exception String
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The get report.
    /// </returns>
    public static string GetReport([NotNull] string providerSection, [NotNull] string tag)
    {
      string select = "//provider[@name='{0}']/Resource[@tag='{1}']".FormatWith(
        providerSection.ToUpper(), tag.ToUpper());
      XmlNode node = ExceptionXML().SelectSingleNode(select);

      if (node != null)
      {
        return node.InnerText;
      }
      else
      {
        return "Exception({1}:{0}) cannot be found in Exception file!".FormatWith(tag, providerSection);
      }
    }

    /// <summary>
    /// Throw Exception
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The throw.
    /// </returns>
    public static string Throw([NotNull] string providerSection, [NotNull] string tag)
    {
      throw new ApplicationException(GetReport(providerSection, tag));
    }

    /// <summary>
    /// Throw ArgumentException
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The throw argument.
    /// </returns>
    public static string ThrowArgument([NotNull] string providerSection, [NotNull] string tag)
    {
      throw new ArgumentException(GetReport(providerSection, tag));
    }

    /// <summary>
    /// Throw ArgumentNullException
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The throw argument null.
    /// </returns>
    public static string ThrowArgumentNull([NotNull] string providerSection, [NotNull] string tag)
    {
      throw new ArgumentNullException(GetReport(providerSection, tag));
    }

    /// <summary>
    /// Throw NotSupportedException
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The throw not supported.
    /// </returns>
    public static string ThrowNotSupported([NotNull] string providerSection, [NotNull] string tag)
    {
      throw new NotSupportedException(GetReport(providerSection, tag));
    }

    /// <summary>
    /// Throw ProviderException
    /// </summary>
    /// <param name="providerSection">
    /// The provider Section.
    /// </param>
    /// <param name="tag">
    /// The tag.
    /// </param>
    /// <returns>
    /// The throw provider.
    /// </returns>
    public static string ThrowProvider([NotNull] string providerSection, [NotNull] string tag)
    {
      throw new ProviderException(GetReport(providerSection, tag));
    }

    #endregion

    #region Methods

    /// <summary>
    /// Return XMLDocument containing text for the Exceptions
    /// </summary>
    [NotNull]
    private static XmlDocument ExceptionXML()
    {
      if (ProviderExceptionFile.IsNotSet())
      {
        throw new ApplicationException("Exceptionfile cannot be null or empty!");
      }

      var exceptionXmlDoc = new XmlDocument();
      exceptionXmlDoc.Load(
        HttpContext.Current.Server.MapPath(
          "{0}resources/{1}".FormatWith(YafForumInfo.ForumServerFileRoot, ProviderExceptionFile)));

      return exceptionXmlDoc;
    }

    #endregion
  }
}