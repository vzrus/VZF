/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace VZF.Utilities
{
    #region Using

    using YAF.Classes;
    using YAF.Types;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        /// The spell checker load js.
        /// </summary>
        /// <param name="editorClientId">
        /// The Editor client Id.
        /// </param>
        /// <param name="spellCheckButtonId">
        /// The spell Check Button Id.
        /// </param>
        /// <param name="cultureCode">
        /// The culture Code.
        /// </param>
        /// <param name="spellCorrectTxt">
        /// The spell Correct Info Warning Text.
        /// </param>
        /// <returns>
        /// The load js.
        /// </returns>
        [NotNull]
        public static string SpellCheckerLoadJs(
            [NotNull] string editorClientId,
            [NotNull] string spellCheckButtonId,
            [CanBeNull] string cultureCode,
            [NotNull] string spellCorrectTxt)
        {
            return
                @"{0}(document).ready(function() {{ {0}('#{1}').spellchecker({{lang: ""{3}"", engine: ""google"", url: ""{5}YafAjax.asmx/SpellCheck""}}); }});
                {0}('#{2}').click(function(e){{
                    e.preventDefault();
                     var text = {0}('#{1}').val();
                     if (text == '') {{  
                         {0}('#{1}').spellchecker('remove');
                         alert('{4}');
                     }}
                     else
                    {{                           
                    {0}('#{1}').spellchecker('check', function(result){{
                    if (result) {{
                                   {0}('#{1}').spellchecker('remove');
                                   alert('{4}');
                                 }}
                  }});
                 }}
                }});
                "
                    .FormatWith(
                        Config.JQueryAlias,
                        editorClientId,
                        spellCheckButtonId,
                        cultureCode,
                        spellCorrectTxt,
                        YafForumInfo.ForumClientFileRoot);
        }

    }
}