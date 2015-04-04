using System.Web;
using VZF.Utils;
using YAF.Classes;
using YAF.Core;
using YAF.Types.Interfaces;

namespace VZF.Kernel
{
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
