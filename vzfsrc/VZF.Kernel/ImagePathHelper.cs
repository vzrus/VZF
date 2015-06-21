// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Vladimir Zakharov" file="ImagePathHelper.cs">
//   VZF by vzrus
//   Copyright (C) 2015 Vladimir Zakharov
//   https://github.com/vzrus
//   http://sourceforge.net/projects/yaf-datalayers/
//    This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; version 2 only 
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//    
//    You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. 
// </copyright>
// <summary>
//   The ImagePathHelper functionality.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace VZF.Kernel
{
    using System.Web;

    using VZF.Utils;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types.Interfaces;

    public static class ImagePathHelper
    {
        private static string GetTopicImageUploadPath(int topicId, string thumbClause, string topicImageName)
        {
            string uploadFileExtension = "yafupload";

            return "{0}/{1}{2}.{3}.{4}".FormatWith(
                TopicImageUploadDir, topicId, thumbClause, topicImageName, uploadFileExtension);
        }

        public static string GetTopicImageUploadFullPath(int topicId, string topicImageName)
        {
            return GetTopicImageUploadPath(topicId, string.Empty, topicImageName);
        }

        public static string GetTopicImageUploadThumbPath(int topicId, string topicImageName)
        {
            string thumbClause = ".thumb";
            return GetTopicImageUploadPath(topicId, thumbClause, topicImageName);
        }

        public static string TopicImageUploadDir
        {
            get
            {
                return YafContext.Current.Get<HttpRequestBase>()
                    .MapPath(
                        string.Concat(
                            BaseUrlBuilder.ServerFileRoot,
                            YafBoardFolders.Current.TopicImages));
            }
        }

    }
}
