namespace YAF.Modules.BBCode
{
    using System.Web.UI;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    /// <summary>
    /// Hidden BBCode Module
    /// </summary>
    public class HideBBCodeModule : YafBBCodeControl
    {
        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        protected override void Render(HtmlTextWriter writer)
        {
            var hiddenContent = Parameters["inner"];

            int postsCount = -1;

            if (Parameters.ContainsKey("posts"))
            {
                postsCount = int.Parse(Parameters["posts"]);
            }

            int thanksCount = -1;

            if (Parameters.ContainsKey("thanks"))
            {
                thanksCount = int.Parse(Parameters["thanks"]);
            }

            var messageId = this.MessageID;

            if (hiddenContent.IsNotSet())
            {
                return;
            }

            var description = LocalizedString(
                "HIDDENMOD_DESC",
                "The content of this post is hidden. After you THANK the poster, refresh the page to see the hidden content. You only need to thank the Current Post");

            var descriptionGuest = LocalizedString(
                "HIDDENMOD_GUEST",
                "This board requires you to be registered and logged-in before you can view hidden messages.");

            string shownContentGuest =
                "<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all  HiddenGuestBox\"><p><span class=\"ui-icon ui-icon-alert HiddenGuestBoxImage\"></span>{0}</p></div></div>"
                    .FormatWith(descriptionGuest);

            string shownContent = "<img src=\"{1}\" alt=\"{0}\" title=\"{0}\" />".FormatWith(
               description, YafForumInfo.GetURLToResource("images/HiddenWarnDescription.png"));


            if (YafContext.Current.IsAdmin)
            {
                writer.Write(hiddenContent);
                return;
            }

            var userId = YafContext.Current.CurrentUserData.UserID;

            if (postsCount > -1)
            {
                // Handle Hide Posts Count X BBCOde
                var descriptionPost = LocalizedString(
                    "HIDDENMOD_POST",
                    "Hidden Content (You must be registered and have {0} post(s) or more)").FormatWith(postsCount);

                string shownContentPost =
                    "<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all  HiddenGuestBox\"><p><span class=\"ui-icon ui-icon-alert HiddenGuestBoxImage\"></span>{0}</p></div></div>"
                        .FormatWith(descriptionPost);


                if (YafContext.Current.IsGuest)
                {
                    writer.Write(shownContentGuest);
                    return;
                }


                if (DisplayUserID == userId ||
                    YafContext.Current.CurrentUserData.NumPosts >= postsCount)
                {
                    shownContent = hiddenContent;
                }
                else
                {
                    shownContent = shownContentPost;
                }
            }
            else if (thanksCount > -1)
            {
                // Handle Hide Thanks Count X BBCode
                var descriptionPost = LocalizedString(
                    "HIDDENMOD_THANKS",
                    "Hidden Content (You must be registered and have at least {0} thank(s) received)").FormatWith(thanksCount);

                string shownContentPost =
                    "<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all  HiddenGuestBox\"><p><span class=\"ui-icon ui-icon-alert HiddenGuestBoxImage\"></span>{0}</p></div></div>"
                        .FormatWith(descriptionPost);


                if (YafContext.Current.IsGuest)
                {
                    writer.Write(shownContentGuest);
                    return;
                }


                if (DisplayUserID == userId ||
                    CommonDb.user_ThankFromCount(PageContext.PageModuleID, userId) >= thanksCount)
                {
                    shownContent = hiddenContent;
                }
                else
                {
                    shownContent = shownContentPost;
                }
            }
            else
            {
                // Handle Hide Thanks
                if (!this.Get<YafBoardSettings>().EnableThanksMod)
                {
                    writer.Write(hiddenContent);
                    return;
                }

                if (YafContext.Current.IsGuest)
                {
                    writer.Write(shownContentGuest);
                    return;
                }


                if (DisplayUserID == userId ||
                    CommonDb.user_ThankedMessage(PageContext.PageModuleID, messageId.ToType<int>(), userId))
                {
                    // Show hiddent content if user is the poster or have thanked the poster.
                    shownContent = hiddenContent;
                }
                else
                {
                    shownContent = "<img src=\"{1}\" alt=\"{0}\" title=\"{0}\" />".FormatWith(
                        description, YafForumInfo.GetURLToResource("images/HiddenWarnDescription.png"));
                }
            }

            writer.Write(shownContent);
        }
    }
}