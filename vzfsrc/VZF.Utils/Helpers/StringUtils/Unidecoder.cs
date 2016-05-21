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
 * File Unidecoder.cs created  on 2.6.2015 in  6:31 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
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

namespace VZF.Utils.Helpers.StringUtils
{
	#region Using

	using System;
	using System.Text;

	#endregion

	/// <summary>
	/// ASCII transliterations of Unicode text
	/// </summary>
	public static partial class Unidecoder
	{
		#region Public Methods

		/// <summary>
		/// Transliterate an Unicode object into an ASCII string
		/// </summary>
		/// <remarks>
		/// unidecode(u"\u5317\u4EB0") == "Bei Jing "
		/// </remarks>
		/// <param name="input">
		/// The input. 
		/// </param>
		/// <returns>
		/// The unidecode.
		/// </returns>
		public static string Unidecode(this string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}

			var output = new StringBuilder();
			foreach (var c in input.ToCharArray())
			{
				if (c < 0x80)
				{
					output.Append(c);
					continue;
				}

				var h = c >> 8;
				var l = c & 0xff;

				output.Append(characters.ContainsKey(h) ? characters[h][l] : string.Empty);
			}

			return output.ToString();
		}

		#endregion
	}
}