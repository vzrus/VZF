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
        /// Generates jQuery UI DatePicker Script
        /// </summary>
        /// <param name="fieldId">
        /// The Id of the Control to Bind the DatePicker
        /// </param>
        /// <param name="dateFormat">
        /// Localized Date Format
        /// </param>
        /// <param name="culture">
        /// Current Culture
        /// </param>
        /// <returns>
        /// The load js.
        /// </returns>
        public static string DatePickerLoadJs(
            [NotNull] string fieldId, [NotNull] string dateFormat, [NotNull] string culture)
        {
            string cultureJs = string.Empty;

            dateFormat = dateFormat.ToLower();

            dateFormat = dateFormat.Replace("yyyy", "yy");

            if (!string.IsNullOrEmpty(culture))
            {
                cultureJs = @"{2}('#{0}').datepicker('option', {2}.datepicker.regional['{1}']);".FormatWith(
                    fieldId, culture, Config.JQueryAlias);
            }

            return
                @"Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(loadDatePicker);
                  function loadDatePicker() {{	{3}(document).ready(function() {{ {3}('#{0}').datepicker({{showButtonPanel: true,changeMonth:true,changeYear:true,yearRange: '-100:+0',maxDate:'+0d',dateFormat:'{1}',}}); {2} }});}} "
                    .FormatWith(fieldId, dateFormat, cultureJs, Config.JQueryAlias);
        }
    }
}