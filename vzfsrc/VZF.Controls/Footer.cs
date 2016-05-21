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
 * File Footer.cs created  on 2.6.2015 in  6:29 AM.
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
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using VZF.Data.DAL;
    using VZF.Data.Utils;
    using VZF.Types.Constants;
    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// Summary description for Footer.
    /// </summary>
    public class Footer : BaseControl
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether SimpleRender.
        /// </summary>
        public bool SimpleRender { get; set; }

        /// <summary>
        ///   Gets ThisControl.
        /// </summary>
        [NotNull]
        public Control ThisControl
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void Render([NotNull] HtmlTextWriter writer)
        {
            if (!this.SimpleRender)
            {
                this.RenderRegular(ref writer);
            }

            base.Render(writer);
        }

        /// <summary>
        /// The render regular.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected void RenderRegular([NotNull] ref HtmlTextWriter writer)
        {
            // BEGIN FOOTER
            var footer = new StringBuilder();

            this.Get<IStopWatch>().Stop();

            footer.Append(@"<br /><div class=""content"" style=""text-align:right;font-size:7pt"">");

            if (this.PageContext.CurrentForumPage.IsAdminPage)
            {
                // show admin icons license...
                footer.Append(
                    @"<span style=""color:#999999""><a target=""_blank"" href=""http://www.pinvoke.com/"">Fugue Icons</a> &copy; 2009 Yusuke Kamiyamane</span>");
                footer.Append("<br />");
            }

            this.RenderMobileLink(footer);

            this.RenderVersion(footer);

            this.RenderGeneratedAndDebug(footer);

            // write CSS, Refresh, then header...
            writer.Write(footer);
        }

        /// <summary>
        /// The render generated and debug.
        /// </summary>
        /// <param name="footer">
        /// The footer.
        /// </param>
        private void RenderGeneratedAndDebug([NotNull] StringBuilder footer)
        {
            if (this.Get<YafBoardSettings>().ShowPageGenerationTime)
            {
                footer.Append("<br />");
                footer.AppendFormat(this.GetText("COMMON", "GENERATED"), this.Get<IStopWatch>().Duration);
            }

            footer.Append(@"</div>");

#if DEBUG
            if (!this.PageContext.IsAdmin)
            {
                return;
            }

            footer.AppendFormat(
                @"<br /><br /><div style=""width:500px;margin:auto;padding:5px;text-align:right;font-size:7pt;""><span style=""color:#990000"">YAF Compiled in <strong>DEBUG MODE</strong></span>.<br />Recompile in <strong>RELEASE MODE</strong> to remove this information:");
            footer.Append(@"<br /><br /><a href=""http://validator.w3.org/check?uri=referer"" >XHTML</a> | ");
            footer.Append(@"<a href=""http://jigsaw.w3.org/css-validator/check/referer"" >CSS</a><br /><br />");

            var extensions = this.Get<IList<Assembly>>("ExtensionAssemblies").Select(a => a.FullName).ToList();

            if (extensions.Any(x => x.Contains("PublicKeyToken=f3828393ba2d803c")))
            {
                footer.Append("Offical VZF Release: Modules with Public Key of f3828393ba2d803c Loaded.");
            }

            if (extensions.Any(x => x.Contains(".Module")))
            {
                footer.AppendFormat(
                    @"<br /><br />Extensions Loaded: <span style=""color: green"">{0}</span>",
                    extensions.Where(x => x.Contains(".Module")).ToDelimitedString("<br />"));
            }

            footer.AppendFormat(
                @"<br /><br /><b>{0}</b> SQL Queries: <b>{1:N3}</b> Seconds (<b>{2:N2}%</b> of Total Page Load Time).<br />{3}",
                QueryCounter.Count,
                QueryCounter.Duration,
                (100 * QueryCounter.Duration) / this.Get<IStopWatch>().Duration,
                QueryCounter.Commands);
            footer.Append("</div>");
#endif
        }

        /// <summary>
        /// The render mobile link.
        /// </summary>
        /// <param name="footer">
        /// The footer.
        /// </param>
        private void RenderMobileLink([NotNull] StringBuilder footer)
        {
            if (this.Get<IYafSession>().UseMobileTheme ?? false)
            {
                footer.Append(
                    @"<a target=""_top"" title=""{1}"" href=""{0}"">{1}</a> | ".FormatWith(
                        YafBuildLink.GetLink(ForumPages.forum, "fullsite=true"),
                        this.GetText("COMMON", "MOBILE_FULLSITE")));
            }
            else if (this.PageContext.Vars.ContainsKey("IsMobile") && this.PageContext.Vars["IsMobile"] != null
                     && this.PageContext.Vars["IsMobile"].ToType<bool>())
            {
                footer.Append(
                    @"<a target=""_top"" title=""{1}"" href=""{0}"">{1}</a> | ".FormatWith(
                        YafBuildLink.GetLink(ForumPages.forum, "mobilesite=true"),
                        this.GetText("COMMON", "MOBILE_VIEWSITE")));
            }
        }

        /// <summary>
        /// The render version.
        /// </summary>
        /// <param name="footer">
        /// The footer.
        /// </param>
        private void RenderVersion([NotNull] StringBuilder footer)
        {
            CodeContracts.ArgumentNotNull(footer, "footer");

            // Copyright Linkback Algorithm
            // Please keep if you haven't purchased a removal or commercial license.
            var domainKey = this.Get<YafBoardSettings>().CopyrightRemovalDomainKey;

            if (domainKey.IsSet())
            {
                var currentDomainHash = HashHelper.Hash(
                    this.Get<HttpRequestBase>().Url.DnsSafeHost.ToLower(),
                    HashHelper.HashAlgorithmType.SHA1,
                    this.GetType().GetSigningKey().ToString(),
                    false);

                if (domainKey.Equals(currentDomainHash))
                {
                    return;
                }
            }

            // get the theme credit info from the theme file
            // it's not really an error if it doesn't exist
            string themeCredit = this.Get<ITheme>().GetItem("THEME", "CREDIT", null);

            // append theme Credit if it exists...
            if (themeCredit.IsSet())
            {
                footer.AppendFormat(@"<span id=""themecredit"" style=""color:#999999"">{0}</span>", themeCredit);
                footer.Append("<br />");
            }

            using (var sc = new VzfSqlCommand(YafContext.Current.PageModuleID))
            {

                switch (sc.DataSource.Information.DataSourceProductName)
                {
                    case "Microsoft SQL Server":
                        footer.Append(
                            @"<a><img src=""{0}"" alt=""{1}"" title=""{1}"" /></a>".FormatWith(
                                this.PageContext.Get<ITheme>()
                                    .GetItem(
                                        "ICONS",
                                        "MSSQLSERVER_SMALL",
                                        YafForumInfo.GetURLToResource("images/mssqlserver_small.png")),
                                " {0} MsSQL".FormatWith(this.GetText("COMMON", "POWERED_BY"))));
                        break;
                    case "Npgsql":
                        footer.Append(
                            @"<a><img src=""{0}"" alt=""{1}"" title=""{1}"" /></a>".FormatWith(
                                this.PageContext.Get<ITheme>()
                                    .GetItem(
                                        "ICONS",
                                        "POSTGRESQL_SMALL",
                                        YafForumInfo.GetURLToResource("images/postgresql_small.png")),
                                " {0} PostgreSQL ".FormatWith(this.GetText("COMMON", "POWERED_BY"))));
                        break;
                    case "MySQL":
                        footer.Append(
                            @"<a><img src=""{0}"" alt=""{1}"" title=""{1}"" /></a>".FormatWith(
                                this.PageContext.Get<ITheme>()
                                    .GetItem(
                                        "ICONS",
                                        "MYSQL_SMALL",
                                        YafForumInfo.GetURLToResource("images/mysql_small.png")),
                                " {0} MySQL ".FormatWith(this.GetText("COMMON", "POWERED_BY"))));
                        break;
                    case "Firebird":
                        footer.Append(
                            @"<a><img src=""{0}"" alt=""{1}"" title=""{1}"" /></a>".FormatWith(
                                this.PageContext.Get<ITheme>()
                                    .GetItem(
                                        "ICONS",
                                        "FIREBIRD_SMALL",
                                        YafForumInfo.GetURLToResource("images/firebird_small.png")),
                                " {0} Firebird ".FormatWith(this.GetText("COMMON", "POWERED_BY"))));
                        break;
                    default:
                        footer.Append(string.Empty);
                        break;
                }
            }

            footer.Append(@"<a target=""_top"" title=""VZF"" href=""https://github.com/vzrus/VZF"">");
            footer.Append(this.GetText("COMMON", "POWERED_BY"));
            footer.Append(@" VZF");

            if (this.Get<YafBoardSettings>().ShowYAFVersion)
            {
                footer.AppendFormat(" {0} ", YafForumInfo.AppVersionName);
                if (Config.IsDotNetNuke)
                {
                    footer.Append(" Under DNN ");
                }
                else if (Config.IsRainbow)
                {
                    footer.Append(" Under Rainbow ");
                }
                else if (Config.IsMojoPortal)
                {
                    footer.Append(" Under MojoPortal ");
                }
                else if (Config.IsPortalomatic)
                {
                    footer.Append(" Under Portalomatic ");
                }
            }

            footer.AppendFormat(
                @"</a> | <a target=""_top"" title=""{0}"" href=""{1}"">VZF &copy; 2012-{2}, VZF</a>",
                "VZF",
                "https://github.com/vzrus/VZF",
                DateTime.UtcNow.Year);
        }

        #endregion
    }
}