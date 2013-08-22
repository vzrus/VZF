<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.topics" Codebehind="topics.ascx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="VZF.Controls" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Register TagPrefix="VZF" TagName="ForumList" Src="../controls/ForumList.ascx" %>
<%@ Register TagPrefix="VZF" TagName="TopicLine" Src="../controls/TopicLine.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumJumper" Src="../controls/ForumJumper.ascx" %>
<%@ Register TagPrefix="VZF" Namespace="VZF.Controls" Assembly="VZF.Controls" %>

<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<asp:PlaceHolder runat="server" ID="SubForums" Visible="false">
    <table class="content subForum"  width="100%">
        <tr class="topicTitle">
            <th colspan="6" class="header1">
                <%= GetSubForumTitle()%>
            </th>
        </tr>
        <tr class="topicSubTitle">
            <th width="1%" class="header2">
                &nbsp;
            </th>
            <th class="header2 headerForum">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="FORUM" />
            </th>
            <th width="15%"  runat="server" class="header2 headerModerators" visible='<%# PageContext.BoardSettings.ShowModeratorList  && PageContext.BoardSettings.ShowModeratorListAsColumn %>'>
                <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="moderators" />
            </th>
            <th width="4%" class="header2 headerTopics">
                <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="topics" />
            </th>
            <th width="4%" class="header2 headerPosts">
                <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="posts" />
            </th>
            <th width="25%" class="header2 headerLastPost">
                <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="lastpost" />
            </th>
        </tr>
        <VZF:ForumList AltLastPost="<%# this.LastPostImageTT %>" runat="server" ID="ForumList" />
        <tfoot visible="false">
            <tr>
                <td colspan="6" class="header2"></td>
            </tr>
        </tfoot>
    </table>
</asp:PlaceHolder>
<table class="content" width="100%">
    <asp:Repeater ID="Announcements" runat="server">
        <HeaderTemplate>
            <tr class="topicTitle">
                <th class="header1" colspan="6">
                    <VZF:ThemeImage ID="ai" ThemePage="ICONS" ThemeTag="ANOUNCEMENT_T_SICON" LocalizedTitlePage="TOPICS" LocalizedTitleTag="ANNOUNCEMENTS_TITLE"  runat="server"/>
                    <VZF:LocalizedLabel ID="LocalizedLabel16" runat="server" LocalizedTag="ANNOUNCEMENTS_TITLE" />
                </th>
            </tr>
            <tr class="topicSubTitle">
                <th class="header2" width="1%">
                    &nbsp;
                </th>
                <th class="header2 headerTopic" align="left">
                    <VZF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="topics" />
                </th>
                <th class="header2 headerReplies" align="right" width="7%">
                    <VZF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="replies" />
                </th>
                <th class="header2 headerViews" align="right" width="7%">
                    <VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="views" />
                </th>
                <th class="header2 headerLastPost" align="left" width="15%">
                    <VZF:LocalizedLabel ID="LocalizedLabel15" runat="server" LocalizedTag="lastpost" />
                </th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <VZF:TopicLine ID="TopicLine1" runat="server" AltLastPost="<%# this.LastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </ItemTemplate>
       <FooterTemplate>
           <tfoot visible="false">
               <tr>
                   <td colspan="6" class="header2"></td>
               </tr>
           </tfoot>
       </FooterTemplate>
    </asp:Repeater>
</table>
<table class="command" width="100%">
    <tr>
        <td>
            <VZF:Pager runat="server" ID="Pager" UsePostBack="False" />
        </td>
        <td>
            <VZF:ThemeButton ID="moderate1" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_MODERATE" TitleLocalizedTag="BUTTON_MODERATE_TT" />
            <VZF:ThemeButton ID="NewTopic1" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_NEWTOPIC" TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
        </td>
    </tr>
</table>
<table class="content" width="100%">
    <tr class="topicTitle">
        <th class="header1" colspan="6">
            <asp:Label ID="PageTitle" runat="server"></asp:Label>
        </th>
    </tr>
    <tr class="topicSubTitle">
        <th class="header2" width="1%">
            &nbsp;
        </th>
        <th class="header2 headerTopic" align="left">
            <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="topics" />
        </th>
        <th class="header2 headerReplies" align="right" width="7%">
            <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="replies" />
        </th>
        <th class="header2 headerViews" align="right" width="7%">
            <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="views" />
        </th>
        <th class="header2 headerLastPost" align="left" width="15%">
            <VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="lastpost" />
        </th>
    </tr>
    <tr>
        <td colspan="6" class="header2"></td>
    </tr>
    <asp:Repeater ID="TopicList" OnItemDataBound="TopicList_OnItemDataBound" runat="server">
        <ItemTemplate>
            <VZF:TopicLine runat="server" AltLastPost="<%# this.LastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </ItemTemplate>
        <AlternatingItemTemplate>
            <VZF:TopicLine runat="server" IsAlt="True" AltLastPost="<%# this.LastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
      </AlternatingItemTemplate>
    </asp:Repeater>
    <VZF:ForumUsers runat="server" />
    <tr>
        <td align="center" colspan="6" class="footer1">
             <VZF:SimpleTagCloud ID="Stc1" runat="server"/>
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="1%" style="white-space: nowrap">
                        <VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="showtopics" />
                        <asp:DropDownList ID="ShowList" runat="server" AutoPostBack="True" />
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="WatchForum" runat="server" /><span id="WatchForumID" runat="server"
                            visible="false" /><span id="delimiter1" runat="server" visible="<%# this.WatchForum.Text.Length > 0 %>"> | </span>
                        <asp:LinkButton runat="server" ID="MarkRead" />
                        <VZF:RssFeedLink ID="RssFeed" runat="server" FeedType="Topics" 
                            Visible="<%# PageContext.BoardSettings.ShowRSSLink && this.Get<IPermissions>().Check(PageContext.BoardSettings.TopicsFeedAccess) %>" TitleLocalizedTag="RSSICONTOOLTIPFORUM" />  
                          <VZF:RssFeedLink ID="AtomFeed" runat="server" FeedType="Topics" IsAtomFeed="true" Visible="<%# PageContext.BoardSettings.ShowAtomLink && this.Get<IPermissions>().Check(PageContext.BoardSettings.TopicsFeedAccess) %>" ImageThemeTag="ATOMFEED" TextLocalizedTag="ATOMFEED" TitleLocalizedTag="ATOMICONTOOLTIPACTIVE" />    
                                             
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="command" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="left">
            <VZF:Pager ID="PagerBottom" runat="server" LinkedPager="Pager" UsePostBack="False" />
        </td>
        <td>
            <VZF:ThemeButton ID="moderate2" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_MODERATE" TitleLocalizedTag="BUTTON_MODERATE_TT" />
            <VZF:ThemeButton ID="NewTopic2" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_NEWTOPIC" TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
        </td>
    </tr>
</table>
<asp:PlaceHolder ID="ForumSearchHolder" runat="server">
<div id="ForumSearchDiv">
        <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="SEARCH_FORUM" />
        &nbsp;<asp:TextBox id="forumSearch" runat="server"></asp:TextBox>
        &nbsp;<VZF:ThemeButton ID="forumSearchOK" runat="server" CssClass="yaflittlebutton"
                TextLocalizedTag="OK" TitleLocalizedTag="OK_TT" OnClick="ForumSearch_Click" />
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="ForumJumpHolder" runat="server">
    <div id="DivForumJump">
        <VZF:ForumJumper  ID="fj1" runat="server"></VZF:ForumJumper>
    </div>
</asp:PlaceHolder>
<div class="clearItem"></div>
<div id="DivIconLegend">
    <VZF:IconLegend ID="IconLegend1" runat="server" />
</div>
<div id="DivPageAccess" class="smallfont">
    <VZF:PageAccess ID="PageAccess1" runat="server" />
</div>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
