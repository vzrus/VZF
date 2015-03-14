/* VZF by vzrus
 * Copyright (C) 2012 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace VZF.Сontrols
{
    using System;
    using System.Web;

    using YAF.Classes;
    using VZF.Controls;
    using YAF.Core;
    using YAF.Types.Interfaces;
    using VZF.Utilities;
    using VZF.Utils;

    public partial class ForumJumper : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // if (Page.IsPostBack) return;

            if (Config.LargeForumTree)
            {
                YafContext.Current.PageElements.RegisterJQueryUI();
                forumTree.Visible = true;
                // register dynatree script. For large trees we load it lazily.
                YafContext.Current.PageElements.RegisterJsResourceInclude("dynatree", "js/jquery.fancytree-all.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource("css/fancytree/{0}/ui.fancytree.css".FormatWith(YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
                this.TreeTable.Visible = true; 
                YafContext.Current.PageElements.RegisterJsBlock("dynatreescr",
                                                                JavaScriptBlocks.FancyTreeGetNodesJumpLazyJs("tree",
                                                                PageContext.PageUserID, PageContext.PageBoardID, "echoActive", "&links=1", "{0}resource.ashx?tjl".FormatWith(
                                                                                                            YafForumInfo.ForumClientFileRoot), string.Empty, "&forumUrl={0}".FormatWith(HttpUtility.UrlEncode(YafBuildLink.GetBasePath()))));
                // The script hides/shows tree divider.
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "yafmodaldialogJs", JavaScriptBlocks.ToggleDivider(this.jumpList.ClientID));

                this.imgJump.Alt = PageContext.Get<ILocalization>().GetText("COMMON", "FORUM_JUMP");
                this.imgJump.Src = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_JUMP");
                this.imgJump.Attributes["onclick"] =
                    "toggle_visibility('{0}');".FormatWith(this.jumpList.ClientID);
            }
            else
            {
                this.jholder.Controls.Add(new ForumJump{ID="ForumJump1"});
                this.DataBind();
                /* YafContext.Current.PageElements.RegisterJsBlock("dynatreescr",
                                                                JavaScriptBlocks.DynatreeGetNodesJumpAllJS("tree",
                                                                                                          PageContext.PageUserID, 
                                                                                                           "{0}resource.ashx?fj={1}".FormatWith(
                                                                                                               YafForumInfo.ForumClientFileRoot, PageContext.PageUserID))); */
            }


        }
    }
}