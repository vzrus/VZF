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
    /// Hide Reply BBCode Module
    /// </summary>
    public class HideReplyModule : YafBBCodeControl
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

            var messageId = this.MessageID;

            if (hiddenContent.IsNotSet())
            {
                return;
            }

            var description = LocalizedString(
                    "HIDEMOD_REPLY",
                    "Hidden Content (You must be registered and reply to the message to see the hidden Content)");

            var descriptionGuest = LocalizedString(
                "HIDDENMOD_GUEST",
                "This board requires you to be registered and logged-in before you can view hidden messages.");

            string shownContentGuest =
                "<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all  HiddenGuestBox\"><p><span class=\"ui-icon ui-icon-alert HiddenGuestBoxImage\"></span>{0}</p></div></div>"
                    .FormatWith(descriptionGuest);

            string shownContent =
                "<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all  HiddenGuestBox\"><p><span class=\"ui-icon ui-icon-alert HiddenGuestBoxImage\"></span>{0}</p></div></div>"
                    .FormatWith(description);


            if (YafContext.Current.IsAdmin)
            {
                writer.Write(hiddenContent);
                return;
            }

            var userId = YafContext.Current.CurrentUserData.UserID;

            if (YafContext.Current.IsGuest)
            {
                writer.Write(shownContentGuest);
                return;
            }


            if (DisplayUserID == userId ||
                CommonDb.user_RepliedTopic(PageContext.PageModuleID, messageId.ToType<int>(), userId))
            {
                // Show hiddent content if user is the poster or have thanked the poster.
                shownContent = hiddenContent;
            }

            writer.Write(shownContent);
        }
    }
}