
#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
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


//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VZF.Types.Data
{
    using System;
    
    public partial class user_listmedals_Result
    {
        public int MedalID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string MedalURL { get; set; }
        public string RibbonURL { get; set; }
        public string SmallMedalURL { get; set; }
        public string SmallRibbonURL { get; set; }
        public short SmallMedalWidth { get; set; }
        public short SmallMedalHeight { get; set; }
        public short SmallRibbonWidth { get; set; }
        public short SmallRibbonHeight { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<bool> Hide { get; set; }
        public Nullable<bool> OnlyRibbon { get; set; }
        public int Flags { get; set; }
        public Nullable<System.DateTime> DateAwarded { get; set; }
    }
}
