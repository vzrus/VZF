#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File GuidConverter.cs created  on 2.6.2015 in  6:31 AM.
// Last changed on 5.21.2016 in 1:12 PM.
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

namespace YAF.Providers
{
    public static class MySqlHelpers
    {
        /// <summary>
        /// The guid converter.
        /// </summary>
        /// <param name="gd">
        /// The gd.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public static Guid GuidConverter(Guid gd)
        {
            var barrIn = gd.ToByteArray();
            var barrOut = new byte[16];

            barrOut[0] = barrIn[3];
            barrOut[1] = barrIn[2];
            barrOut[2] = barrIn[1];
            barrOut[3] = barrIn[0];
            barrOut[4] = barrIn[5];
            barrOut[5] = barrIn[4];

            barrOut[6] = barrIn[7];
            barrOut[7] = barrIn[6];

            for (var i = 8; i < 16; i++)
            {
                barrOut[i] = barrIn[i];
            }

            return new Guid(barrOut);
        }
    }
}
