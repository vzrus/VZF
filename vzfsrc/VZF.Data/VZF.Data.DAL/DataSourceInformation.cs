// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="DataSourceInformation.cs">
//   VZF by vzrus
//   Copyright (C) 2006-2014 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The DataSourceInformation.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Data.DAL
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Data;

    /// <summary>
    /// The data source information.
    /// </summary>
    public class DataSourceInformation 
    {
        /// <summary>
        /// The _ type.
        /// </summary>
        private static readonly Type _Type = typeof(DataSourceInformation);

        /// <summary>
        /// The _ identifier case type.
        /// </summary>
        private static readonly Type _IdentifierCaseType =
           Enum.GetUnderlyingType(typeof(IdentifierCase));

        /// <summary>
        /// The _ group by behavior type.
        /// </summary>
        private static readonly Type _GroupByBehaviorType =
           Enum.GetUnderlyingType(typeof(GroupByBehavior));

        /// <summary>
        /// The _ supported join operators type.
        /// </summary>
        private static readonly Type _SupportedJoinOperatorsType =
            Enum.GetUnderlyingType(typeof(SupportedJoinOperators));

        /// <summary>
        /// The _composite identifier separator pattern.
        /// </summary>
        private readonly string _compositeIdentifierSeparatorPattern = string.Empty;

        /// <summary>
        /// The _data source product name.
        /// </summary>
        private readonly string _dataSourceProductName = string.Empty;

        /// <summary>
        /// The _data source product version.
        /// </summary>
        private readonly string _dataSourceProductVersion = string.Empty;

        /// <summary>
        /// The _data source product version normalized.
        /// </summary>
        private readonly string _dataSourceProductVersionNormalized = string.Empty;

        /// <summary>
        /// The _group by behavior.
        /// </summary>
        private readonly GroupByBehavior _groupByBehavior;

        /// <summary>
        /// The _identifier pattern.
        /// </summary>
        private readonly string _identifierPattern = string.Empty;

        /// <summary>
        /// The _identifier case.
        /// </summary>
        private readonly IdentifierCase _identifierCase;

        /// <summary>
        /// The _order by columns in select.
        /// </summary>
        private readonly bool _orderByColumnsInSelect = false;

        /// <summary>
        /// The _parameter marker format.
        /// </summary>
        private readonly string _parameterMarkerFormat = string.Empty;

        /// <summary>
        /// The _parameter marker pattern.
        /// </summary>
        private readonly string _parameterMarkerPattern = string.Empty;

        /// <summary>
        /// The _parameter name max length.
        /// </summary>
        private readonly int _parameterNameMaxLength = 0;

        /// <summary>
        /// The _parameter name pattern.
        /// </summary>
        private readonly string _parameterNamePattern = string.Empty;

        /// <summary>
        /// The _quoted identifier pattern.
        /// </summary>
        private readonly string _quotedIdentifierPattern = string.Empty;

        /// <summary>
        /// The _quoted identifier case.
        /// </summary>
        private readonly Regex _quotedIdentifierCase;

        /// <summary>
        /// The _statement separator pattern.
        /// </summary>
        private readonly string _statementSeparatorPattern = string.Empty;

        /// <summary>
        /// The _string literal pattern.
        /// </summary>
        private readonly Regex _stringLiteralPattern;

        /// <summary>
        /// The _supported join operators.
        /// </summary>
        private readonly SupportedJoinOperators _supportedJoinOperators;

        /// <summary>
        /// The _parameter name pattern regex.
        /// </summary>
        private Regex _parameterNamePatternRegex;

        /// <summary>
        /// The _parameter prefix.
        /// </summary>
        private string _parameterPrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceInformation"/> class.
        /// </summary>
        /// <param name="dt">
        /// The dt.
        /// </param>
        public DataSourceInformation(DataTable dt)
        {
            // DataTable dt = Connection.GetSchema(
            // DbMetaDataCollectionNames.DataSourceInformation);
            foreach (DataRow r in dt.Rows)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    string s = c.ColumnName;
                    object o = r[c.ColumnName];

                    // if field is DBNull.Value convert it to null 
                    if (o == DBNull.Value)
                    {
                        o = null;
                    }

                    if (string.IsNullOrEmpty(s) || o == null)
                    {
                        continue;
                    }

                    switch (s)
                    {
                        case "QuotedIdentifierCase":
                            this._quotedIdentifierCase = new Regex(o.ToString());
                            break;
                        case "StringLiteralPattern":
                            this._stringLiteralPattern = new Regex(o.ToString());
                            break;
                        case "GroupByBehavior":
                            o = Convert.ChangeType(o, _GroupByBehaviorType);
                            this._groupByBehavior = (GroupByBehavior)o;
                            break;
                        case "IdentifierCase":
                            o = Convert.ChangeType(o, _IdentifierCaseType);
                            this._identifierCase = (IdentifierCase)o;
                            break;
                        case "SupportedJoinOperators":
                            o = Convert.ChangeType(o, _SupportedJoinOperatorsType);
                            this._supportedJoinOperators = (SupportedJoinOperators)o;

                            // (o as SupportedJoinOperators?) ??
                            //    SupportedJoinOperators.None;
                            break;
                        default:
                            FieldInfo fi = _Type.GetField(
                                "_" + s,
                                BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                            if (fi != null)
                            {
                                fi.SetValue(this, o);
                            }

                            break;
                    }
                }

                // there should only ever be a single row.
                break;
            }
        }

        /// <summary>
        /// Gets the composite identifier separator pattern.
        /// </summary>
        public string CompositeIdentifierSeparatorPattern
        {
            get { return this._compositeIdentifierSeparatorPattern; }
        }

        /// <summary>
        /// Gets the data source product name.
        /// </summary>
        public string DataSourceProductName
        {
            get { return this._dataSourceProductName; }
        }

        /// <summary>
        /// Gets the data source product version.
        /// </summary>
        public string DataSourceProductVersion
        {
            get { return this._dataSourceProductVersion; }
        }

        /// <summary>
        /// Gets the data source product version normalized.
        /// </summary>
        public string DataSourceProductVersionNormalized
        {
            get { return this._dataSourceProductVersionNormalized; }
        }

        /// <summary>
        /// Gets the group by behavior.
        /// </summary>
        public GroupByBehavior GroupByBehavior
        {
            get { return this._groupByBehavior; }
        }

        /// <summary>
        /// Gets the identifier pattern.
        /// </summary>
        public string IdentifierPattern
        {
            get { return this._identifierPattern; }
        }

        /// <summary>
        /// Gets the identifier case.
        /// </summary>
        public IdentifierCase IdentifierCase
        {
            get { return this._identifierCase; }
        }

        /// <summary>
        /// Gets a value indicating whether order by columns in select.
        /// </summary>
        public bool OrderByColumnsInSelect
        {
            get { return this._orderByColumnsInSelect; }
        }

        /// <summary>
        /// Gets the parameter marker format.
        /// </summary>
        public string ParameterMarkerFormat
        {
            get { return this._parameterMarkerFormat; }
        }

        /// <summary>
        /// Gets the parameter marker pattern.
        /// </summary>
        public string ParameterMarkerPattern
        {
            get { return this._parameterMarkerPattern; }
        }

        /// <summary>
        /// Gets the parameter name max length.
        /// </summary>
        public int ParameterNameMaxLength
        {
            get { return this._parameterNameMaxLength; }
        }

        /// <summary>
        /// Gets the parameter name pattern.
        /// </summary>
        public string ParameterNamePattern
        {
            get { return this._parameterNamePattern; }
        }

        /// <summary>
        /// Gets the quoted identifier pattern.
        /// </summary>
        public string QuotedIdentifierPattern
        {
            get { return this._quotedIdentifierPattern; }
        }

        /// <summary>
        /// Gets the quoted identifier case.
        /// </summary>
        public Regex QuotedIdentifierCase
        {
            get { return this._quotedIdentifierCase; }
        }

        /// <summary>
        /// Gets the statement separator pattern.
        /// </summary>
        public string StatementSeparatorPattern
        {
            get { return this._statementSeparatorPattern; }
        }

        /// <summary>
        /// Gets the string literal pattern.
        /// </summary>
        public Regex StringLiteralPattern
        {
            get { return this._stringLiteralPattern; }
        }

        /// <summary>
        /// Gets the supported join operators.
        /// </summary>
        public SupportedJoinOperators SupportedJoinOperators
        {
            get { return this._supportedJoinOperators; }
        }

        /// <summary>
        /// Gets the parameter name pattern regex.
        /// </summary>
        public Regex ParameterNamePatternRegex
        {
            get
            {
                return this._parameterNamePatternRegex ??
                    (this._parameterNamePatternRegex = new Regex(this.ParameterNamePattern));
            }
        }

        /// <summary>
        /// Gets the parameter marker.
        /// </summary>
        public string ParameterMarker
        {
            get
            {
                if (string.IsNullOrEmpty(this._parameterPrefix))
                {
                    this._parameterPrefix = this._parameterNameMaxLength != 0
                                        ? this.ParameterMarkerPattern.Substring(0, 1)
                                        : this.ParameterMarkerFormat;
                }

                return this._parameterPrefix;
            }
        }
    }
}
