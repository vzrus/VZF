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
                this.forumTree.Visible = true;
                // register dynatree script. For large trees we load it lazily.
                YafContext.Current.PageElements.RegisterJsResourceInclude("fancytree", "js/jquery.fancytree-all.min.js");
                YafContext.Current.PageElements.RegisterCssIncludeResource(
                    "css/fancytree/{0}/ui.fancytree.css".FormatWith(
                        YafContext.Current.Get<YafBoardSettings>().FancyTreeTheme));
                YafContext.Current.PageElements.RegisterJsResourceInclude("ftreedeljs",
                    "js/fancytree.vzf.nodesjumplazy.min.js");
                YafContext.Current.PageElements.RegisterJsResourceInclude("ftreejumpjs",
                    "js/fancytree.vzf.togglejump.js");

                this.TreeTable.Visible = true;

                // The script hides/shows tree divider.
                //  YafContext.Current.PageElements.RegisterJsBlockStartup(
                //      "yafjumptreeButtonjs", JavaScriptBlocks.ToggleDivider(this.jumpList.ClientID));

                // TODO: Move it ot toggle divaider event later.
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "fancytreejumptreescr",
                    "fancyTreeForumJumpSingleNodeLazyJs('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');"
                        .FormatWith(
                            Config.JQueryAlias,
                            "fjumptree",
                            PageContext.PageUserID,
                            PageContext.PageBoardID,
                            "echoActive",
                            string.Empty,
                            // "&links=1&ff={0}".FormatWith(PageContext.PageForumID),
                            "&links=1",
                            "{0}resource.ashx?tjl".FormatWith(YafForumInfo.ForumClientFileRoot),
                            "&forumUrl={0}".FormatWith(
                                HttpUtility.UrlEncode(YafBuildLink.GetBasePath()))));

                this.imgJump.Alt = PageContext.Get<ILocalization>().GetText("COMMON", "FORUM_JUMP");
                this.imgJump.Src = YafContext.Current.Get<ITheme>().GetItem("ICONS", "FORUM_JUMP");
                this.imgJump.Attributes["onclick"] =
                    "toggle_visibility('{0}');"
                        .FormatWith(
                            this.jumpList.ClientID);

            }
            else
            {
                this.jholder.Controls.Add(new ForumJump {ID = "ForumJump1"});
                this.DataBind();
            }
        }
    }
}