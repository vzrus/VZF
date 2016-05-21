namespace YAF.Modules
{
    using System.Linq;
    using System.Text;
    using System.Web.UI;

    using YAF.Classes;
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types.Interfaces;
    using VZF.Utils;
    using VZF.Utils.Helpers;

    /// <summary>
    /// The BB Code UserLink Module
    /// </summary>
    public class UserLinkBBCodeModule : YafBBCodeControl
  {
        /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      var userName = Parameters["inner"];

      if (userName.IsNotSet() || userName.Length > 50)
      {
        return;
      }

      var userId = this.Get<IUserDisplayName>().GetId(userName.Trim());

      if (userId.HasValue)
      {
        var stringBuilder = new StringBuilder();

          var userLink = new UserLink
              {
                  UserID = (int)userId,
                  CssClass = "UserLinkBBCode",
                  BlankTarget = true,
                  ID = "UserLinkBBCodeFor{0}".FormatWith(userId)
              };

          var showOnlineStatusImage = this.Get<YafBoardSettings>().ShowUserOnlineStatus &&
                                    !UserMembershipHelper.IsGuestUser(userId);

          var onlineStatusImage = new OnlineStatusImage { ID = "OnlineStatusImage", Style = "vertical-align: bottom", UserID = (int)userId };

        stringBuilder.AppendLine("<!-- BEGIN userlink -->");
        stringBuilder.AppendLine(@"<span class=""userLinkContainer"">");
        stringBuilder.AppendLine(userLink.RenderToString());

        if (showOnlineStatusImage)
        {
          stringBuilder.AppendLine(onlineStatusImage.RenderToString()); 
        }

        stringBuilder.AppendLine("</span>");
        stringBuilder.AppendLine("<!-- END userlink -->");

        writer.Write(stringBuilder.ToString());
      }
      else
      {
        writer.Write(this.HtmlEncode(userName));
      }
    }
  }
}