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