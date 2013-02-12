<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.topicsbytags" Codebehind="topicsbytags.ascx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="VZF.Controls" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Register TagPrefix="VZF" TagName="TopicLine" Src="../controls/TopicLine.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumJumper" Src="../controls/ForumJumper.ascx" %>
<%@ Register TagPrefix="VZF" Namespace="VZF.Controls" Assembly="VZF.Controls" %>

<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator">
</div>  
<table class="command" width="100%">
    <tr>
        <td colspan="2">
            <VZF:Pager runat="server" ID="Pager" UsePostBack="False" />
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
        <th class="header2" colspan="6">
            <VZF:LocalizedLabel ID="TagsListLLbl" runat="server"/>
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
    <asp:Repeater ID="TopicList" runat="server">
        <ItemTemplate>
            <VZF:TopicLine runat="server" AltLastPost="<%# this.LastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </ItemTemplate>
        <AlternatingItemTemplate>
            <VZF:TopicLine runat="server" IsAlt="True" AltLastPost="<%# this.LastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
<table class="command" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="left" colspan="2">
            <VZF:Pager ID="PagerBottom" runat="server" LinkedPager="Pager" UsePostBack="False" />
        </td>
    </tr>
     <tr>
        <td align="center" colspan="5">
            <asp:Button ID="OKButon"  Text='<%# this.GetText("COMMON","OK") %>' CausesValidation="False" CommandName="Action" CommandArgument='<%# this.retBtnArgs %>' OnClick="okBtn_click"  runat="server"/>
        </td>
    </tr>
</table>
<asp:PlaceHolder ID="ForumJumpHolder" runat="server">
    <div id="DivForumJump">
        <VZF:ForumJumper  ID="fj1" runat="server"></VZF:ForumJumper>
    </div>
</asp:PlaceHolder>
<div class="clearItem"></div>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
