/* Yet Another Forum.NET
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation version 2
 * of the License only.
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

using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VZF.Kernel;
using YAF.Classes;
using YAF.Types.Constants;
using YAF.Types.Flags;
using YAF.Types.Interfaces;
using YAF.Utilities;
using YAF.Utils.Helpers;

namespace YAF.Controls
{
    #region Using

    using System;
    using System.Data;
    using System.Text;

    
    using YAF.Core;
    using YAF.Types;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The forum profile access.
    /// </summary>
    public partial class ForumProfileAccess : BaseUserControl
    {
        #region MyRegion

        private string yesImage;
        private string noImage;
      

        #endregion

        #region Methods

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.IsAdmin && !this.PageContext.IsForumModerator)
            {
                return;
            }

            yesImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASACCESS");
            noImage = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_HASNOACCESS");
            
            var userId = (int) Security.StringToLongOrRedirect(this.Request.QueryString.GetFirstOrDefault("u"));
            
            if (Config.LargeForumTree)
            {
                YafContext.Current.PageElements.RegisterJsResourceInclude("dynatree", "js/jquery.dynatree.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("js/skin/ui.dynatree.css");
                YafContext.Current.PageElements.RegisterJsBlock("dynatreescr",
                    JavaScriptBlocks.DynatreeGetNodesJumpLazyJS("tree",
                    PageContext.PageUserID,PageContext.PageBoardID,"&v=1", "{0}resource.ashx?tjl".FormatWith(
                    YafForumInfo.ForumClientFileRoot),"&root=0","&forumUrl={0}".FormatWith(HttpUtility.UrlDecode(YafBuildLink.GetLinkNotEscaped(ForumPages.forum).TrimEnd("&g={0}".FormatWith(ForumPages.forum).ToCharArray())))));
            }
            else
            {
                // return results sorted by name
                var valuesOrdered = UserForumAccess.GetUserAccessListSortedByForumName(userId);
                this.ForumList.DataSource = valuesOrdered;
                this.ForumList.DataBind();
            }
        } 

        #endregion

        protected void ForumList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) return;

            var drowv = (UserAccessMasksResult) e.Item.DataItem;
            // string tt = string.Empty;
            var amasks = drowv.AccessMaskData;
            var imgReadAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeReadAccess");
            var imgPostAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypePostAccess");
            var imgReplyAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeReplyAccess");
            var imgPriorityAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypePriorityAccess");
            var imgPollAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypePollAccess");
            var imgVoteAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeVoteAccess");
            var imgModeratorAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeModeratorAccess");
            var imgEditAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeEditAccess");
            var imgDeleteAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeDeleteAccess");
            var imgUploadAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeUploadAccess");
            var imgDownloadAccess = item.FindControlRecursiveAs<HtmlImage>("accessTypeDownloadAccess");
          
            var linkForumName = item.FindControlRecursiveAs<HtmlAnchor>("forumLink");

            bool iReadAccess = false;
            string iReadAccessLegend = string.Empty;

            bool iPostAccess = false;
            string iPostAccessLegend = string.Empty;

            bool iReplyAccess = false;
            string iReplyAccessLegend = string.Empty;

            bool iPriorityAccess = false;
            string iPriorityAccessLegend = string.Empty;

            bool iPollAccess = false;
            string iPollAccessLegend = string.Empty;

            bool iVoteAccess = false;
            string iVoteAccessLegend = string.Empty;

            bool iModeratorAccess = false;
            string iModeratorAccessLegend = string.Empty;

            bool iEditAccess = false;
            string iEditAccessLegend = string.Empty;

            bool iDeleteAccess = false;
            string iDeleteAccessLegend = string.Empty;

            bool iUploadAccess = false;
            string iUploadAccessLegend = string.Empty;

            bool iDownloadAccess = false;
            string iDownloadAccessLegend = string.Empty;

            foreach (var accessMask in amasks)
            {
                iReadAccess = iReadAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ReadAccess);
                iReadAccessLegend = iReadAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iReadAccess ? "+" : "-") + ","; 
                
                iPostAccess = iPostAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PostAccess);
                iPostAccessLegend = iPostAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iPostAccess ? "+" : "-") + ","; 
                
                iReplyAccess = iReplyAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ReplyAccess);
                iReplyAccessLegend = iReplyAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iReplyAccess ? "+" : "-") + ","; 
                
                iPriorityAccess = iPriorityAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PriorityAccess);
                iPriorityAccessLegend = iPriorityAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iPriorityAccess ? "+" : "-") + ","; 
               
                iPollAccess = iPollAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.PollAccess);
                iPollAccessLegend = iPollAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iPollAccess ? "+" : "-") + ","; 
               
                iVoteAccess = iVoteAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.VoteAccess);
                iVoteAccessLegend = iVoteAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iVoteAccess ? "+" : "-") + ","; 
                
                iModeratorAccess = iModeratorAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.ModeratorAccess);
                iModeratorAccessLegend = iModeratorAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iModeratorAccess ? "+" : "-") + ","; 
                
                iEditAccess = iEditAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.EditAccess);
                iEditAccessLegend = iEditAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iEditAccess ? "+" : "-") + ","; 
                
                iDeleteAccess = iDeleteAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.DeleteAccess);
                iDeleteAccessLegend = iDeleteAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iDeleteAccess ? "+" : "-") + ","; 

                iUploadAccess = iUploadAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.UploadAccess);
                iUploadAccessLegend = iUploadAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iUploadAccess ? "+" : "-") + ","; 
                
                iDownloadAccess = iDownloadAccess || accessMask.AccessMaskFlags.BinaryAnd(AccessFlags.Flags.DownloadAccess);
                iDownloadAccessLegend = iDownloadAccessLegend + (!accessMask.IsUserMask ? ("Group:" + accessMask.GroupName + ":") : " PersonalAccess:") + accessMask.AccessMaskName + ":" + "{0}".FormatWith(iDownloadAccess ? "+" : "-") + ","; 
            }

            linkForumName.InnerHtml = drowv.ForumName;
            linkForumName.Title = drowv.ForumName;
            linkForumName.HRef = YafBuildLink.GetLinkNotEscaped(ForumPages.topics, true, "f={0}", drowv.ForumId);

            imgReadAccess.Alt = "{0}".FormatWith(iReadAccess ? "+" : "-");
            imgReadAccess.Attributes["Title"] = iReadAccessLegend.TrimEnd(',');
            imgReadAccess.Src = iReadAccess ? yesImage : noImage;

            imgPostAccess.Alt = "{0}".FormatWith(iPostAccess ? "+" : "-");
            imgPostAccess.Attributes["Title"] = iPostAccessLegend;
            imgPostAccess.Src = iPostAccess ? yesImage : noImage;

            imgReplyAccess.Alt = "{0}".FormatWith(iReplyAccess ? "+" : "-");
            imgReplyAccess.Attributes["Title"] = iReplyAccessLegend;
            imgReplyAccess.Src = iReplyAccess ? yesImage : noImage;

            imgPriorityAccess.Alt = "{0}".FormatWith(iPriorityAccess ? "+" : "-");
            imgPriorityAccess.Attributes["Title"] = iPriorityAccessLegend;
            imgPriorityAccess.Src = iPriorityAccess ? yesImage : noImage;

            imgPollAccess.Alt = "{0}".FormatWith(iPollAccess ? "+" : "-");
            imgPollAccess.Attributes["Title"] = iPollAccessLegend;
            imgPollAccess.Src = iPollAccess ? yesImage : noImage;

            imgVoteAccess.Alt = "{0}".FormatWith(iVoteAccess ? "+" : "-");
            imgVoteAccess.Attributes["Title"] = iVoteAccessLegend;
            imgVoteAccess.Src = iVoteAccess ? yesImage : noImage;

            imgModeratorAccess.Alt = "{0}".FormatWith(iModeratorAccess ? "+" : "-");
            imgModeratorAccess.Attributes["Title"] = iModeratorAccessLegend;
            imgModeratorAccess.Src = iModeratorAccess ? yesImage : noImage;

            imgEditAccess.Alt = "{0}".FormatWith(iEditAccess ? "+" : "-");
            imgEditAccess.Attributes["Title"] = iEditAccessLegend;
            imgEditAccess.Src =  iEditAccess ? yesImage : noImage;

            imgDeleteAccess.Alt = "{0}".FormatWith(iDeleteAccess ? "+" : "-");
            imgDeleteAccess.Attributes["Title"] = iDeleteAccessLegend;
            imgDeleteAccess.Src = iDeleteAccess ? yesImage : noImage;

            imgUploadAccess.Attributes["Title"] = iUploadAccessLegend;
            imgUploadAccess.Alt = "{0}".FormatWith(iUploadAccess ? "+" : "-");
            imgUploadAccess.Src = iUploadAccess ? yesImage : noImage;

            imgDownloadAccess.Attributes["Title"] = iDownloadAccessLegend;
            imgDownloadAccess.Alt = "{0}".FormatWith(iDownloadAccess ? "+" : "-");
            imgDownloadAccess.Src = iDownloadAccess ? yesImage : noImage;
        }
        private static string GetNodeTitle(string title, string path, int userid, int forumId, int access)
        {
            string img = string.Empty;
            string title2 = string.Empty;
            // We add access images for each forum

            string title1 = (title.IsNotSet())
                                   ? ""
                                   : @"<a href='{0}' target='_top' title='{1}'>{1}</a>".FormatWith(
                                       path.Replace("resource.ashx", "default.aspx"), title);

            if (access == 1)
            {
                title = title1 + UserForumAccess.AddAccessImagesAndTips(userid, forumId);
            }
            else
            {
                title = title1;
            }



            return title;
        }
        
    }
}