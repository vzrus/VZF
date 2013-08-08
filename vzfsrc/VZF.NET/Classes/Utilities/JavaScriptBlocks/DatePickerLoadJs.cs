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
                  function loadDatePicker() {{	{3}(document).ready(function() {{ {3}('#{0}').datepicker({{showButtonPanel: true,changeMonth:true,changeYear:true,maxDate:'+0d',dateFormat:'{1}',}}); {2} }});}} "
                    .FormatWith(fieldId, dateFormat, cultureJs, Config.JQueryAlias);
        }
    }
}