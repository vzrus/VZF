#region copyright
// VZF 
// Copyright (C) 2014-2016 Vladimir Zakharov
//
// http://www.code.coolhobby.ru/

// File AssemblyInfo.cs created  on 2.6.2015 in  6:29 AM.
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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("VZF.Data.Postgre")]
[assembly: AssemblyDescription("Postgre specific data layer functionality.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Vladimir Zakharov")]
[assembly: AssemblyProduct("VZF.Data.Postgre")]
[assembly: AssemblyCopyright("©  2008-2016 Vladimir Zakharov")]
[assembly: AssemblyTrademark("vzrus")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b7a3009a-b76d-44f7-bd41-df94942bad72")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.9.6.4")]
[assembly: AssemblyFileVersion("1.9.6.4")]

#if (!COMPACT_FRAMEWORK)
[assembly: AllowPartiallyTrustedCallers]
#endif

[assembly: AssemblyDelaySign(false)]
// [assembly: AssemblyKeyFile("..\\..\\vzf2013.pfx")]