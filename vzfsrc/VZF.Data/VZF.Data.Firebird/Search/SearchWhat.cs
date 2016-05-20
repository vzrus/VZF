#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File SearchWhat.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:18 PM.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAF.Types.Constants;

namespace VZF.Data.Firebird.Search
{
    public class SearchWhat
    {
        public string Build(string searchSql, SearchWhatFlags searchWhatMethod, string toSearchWhat, bool useFullText)
        {
            bool bFirst = true;
            // generate message and topic search sql...
            switch (searchWhatMethod)
            {
                case SearchWhatFlags.AllWords:
                    string[] words = toSearchWhat.Split(' ');
                    if (useFullText)
                    {
                        string ftInner = string.Empty;

                        // make the inner FULLTEXT search
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                ftInner += " AND ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            ftInner += string.Format(@"""{0}""", word);
                        }

                        // make final string...
                        searchSql +=
                            string.Format(
                                @"( CONTAINS (c.MESSAGE, ' {0} ') OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                ftInner);
                    }
                    else
                    {
                        foreach (string word in words)
                        {
                            if (!bFirst) searchSql += " AND ";
                            else bFirst = false;
                            searchSql += string.Format(@"(c.MESSAGE like '%{0}%' OR a.TOPIC LIKE '%{0}%' )", word);
                        }
                    }

                    break;
                case SearchWhatFlags.AnyWords:
                    words = toSearchWhat.Split(' ');

                    if (useFullText)
                    {
                        string ftInner = string.Empty;

                        // make the inner FULLTEXT search
                        foreach (string word in words)
                        {
                            if (!bFirst)
                            {
                                ftInner += " OR ";
                            }
                            else
                            {
                                bFirst = false;
                            }

                            ftInner += string.Format(@"""{0}""", word);
                        }

                        // make final string...
                        searchSql +=
                            string.Format(
                                @"( CONTAINS (c.MESSAGE, ' {0} ' ) OR CONTAINS (a.TOPIC, ' {0} ' ) )",
                                ftInner);
                    }
                    else
                    {
                        foreach (string word in words)
                        {
                            if (!bFirst) searchSql += " OR ";
                            else bFirst = false;
                            searchSql += string.Format(@"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%' ", word);
                        }
                    }

                    break;
                case SearchWhatFlags.ExactMatch:
                    if (useFullText)
                    {
                        // searchSql += string.Format(@"( CONTAINS (c.MESSAGE, ' \"{0}\" ' ) OR CONTAINS (a.Topic, ' \"{0}\" '  )", toSearchWhat);
                    }
                    else
                    {
                        searchSql += string.Format(
                            @"c.MESSAGE LIKE '%{0}%'  OR a.TOPIC LIKE '%{0}%'  ",
                            toSearchWhat);
                    }

                    break;
            }

            return searchSql;
        }
    }
}
