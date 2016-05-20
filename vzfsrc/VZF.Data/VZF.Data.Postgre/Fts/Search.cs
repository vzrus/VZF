#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File Search.cs created  on 2.6.2015 in  6:29 AM.
// Last changed on 5.20.2016 in 3:20 PM.
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

namespace VZF.Data.Postgre.Fts
{
    using System.Collections;
    using System.Data;

    public class Search
    {
        public DataTable SearchIt()
        {
            string key;
            string name = string.Empty;
            string str = string.Empty;

            Hashtable hashtable = new Hashtable();
            key = "ru";
            name = "russian";
            hashtable.Add(key, name);
            key = "en";
            name = "english";
            hashtable.Add(key, name);
            key = "sp";
            name = "spanish";
            hashtable.Add(key, name);
           // SELECT dictinitoption FROM pg_catalog.pg_ts_dict where dictname like 'russian%'
           // SELECT * FROM pg_catalog.pg_ts_dict
           // pg_ts_parser - exists
           //  http://www.sai.msu.su/~megera/postgres/fts/doc/fts-cfg.html
            /* SELECT message, messageid
FROM public.yaf_message
WHERE to_tsvector('russian', message) @@ to_tsquery('russian', 'живые'); 
SELECT * FROM pg_catalog.pg_class where relname ='pg_ts_dict_dictname_index'::regclass */
           return  new DataTable();
        }
    }
}
