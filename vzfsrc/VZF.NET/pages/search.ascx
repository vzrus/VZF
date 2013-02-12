<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.search" CodeBehind="search.ascx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Classes" %>
<%@ Register Namespace="nStuff.UpdateControls" Assembly="nStuff.UpdateControls" TagPrefix="nStuff" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<script type="text/javascript">
    function EndRequestHandler(sender, args) {
        jQuery().YafModalDialog.Close({ Dialog: '#<%=LoadingModal.ClientID%>' });
    }
    function ShowLoadingDialog() {
        jQuery().YafModalDialog.Show({ Dialog: '#<%=LoadingModal.ClientID%>', ImagePath: '<%=YafForumInfo.GetURLToResource("images/")%>' }); 
    }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
</script>
<nStuff:UpdateHistory ID="UpdateHistory" runat="server" OnNavigate="OnUpdateHistoryNavigate" />
<table cellpadding="0" cellspacing="1" class="content searchContent" style="width: 100%" >
    <tr>
        <td class="header1" colspan="3">
            <VZF:LocalizedLabel runat="server" LocalizedTag="title" />
        </td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="1">
             <div id="forumTree" visible="false" runat="server">
                <div class="container">
                    <div id="tree"></div>
                </div> 
            </div>
        </td>
        <td align="center" class="postheader" colspan="1">
            <asp:DropDownList ID="listForum" Visible="False" runat="server" />
            <asp:DropDownList ID="listResInPage" runat="server" />
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="1" class="content searchContent" style="width: 100%" >
    <tr>
        <td align="right" class="postheader" width="35%">
            <VZF:LocalizedLabel runat="server" LocalizedTag="postedby" />
        </td>
        <td align="left" class="postheader">
            <asp:TextBox ID="txtSearchStringFromWho" runat="server" Width="350px" />
            <asp:DropDownList ID="listSearchFromWho" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="right" class="postheader" width="35%">
            <VZF:LocalizedLabel runat="server" LocalizedTag="posts" />
        </td>
        <td align="left" class="postheader">
            <asp:TextBox ID="txtSearchStringWhat" runat="server" Width="350px" />
            <asp:DropDownList ID="listSearchWhat" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="center" class="postfooter" colspan="3">
            <asp:Button ID="btnSearch" runat="server" CssClass="pbutton" OnClick="btnSearch_Click"
                OnClientClick="ShowLoadingDialog(); return true;" Visible="false" />
            <asp:Button ID="btnSearchExt1" runat="server" CssClass="pbutton" Visible="false"
                OnClick="BtnExtSearch1_Click" />
            <asp:Button ID="btnSearchExt2" runat="server" CssClass="pbutton" Visible="false"
                OnClick="BtnExtSearch2_Click" />
        </td>
    </tr>
</table>
<br />
<asp:UpdatePanel ID="SearchUpdatePanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
    </Triggers>
    <ContentTemplate>
        <VZF:Pager runat="server" ID="Pager" OnPageChange="Pager_PageChange" />
        <asp:Repeater ID="SearchRes" runat="server" OnItemDataBound="SearchRes_ItemDataBound">
            <HeaderTemplate>
                <table class="content" cellspacing="1" cellpadding="0" width="100%">
                    <tr>
                        <td class="header1" colspan="2">
                            <VZF:LocalizedLabel runat="server" LocalizedTag="RESULTS" />
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="header2">           
                    <td colspan="2">
                        <strong>
                            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="topic" />
                        </strong><a title='<%# this.GetText("COMMON", "VIEW_TOPIC") %>' href="<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}", Container.DataItemToField<int>("TopicID")) %>">
                            <%# HtmlEncode(Container.DataItemToField<string>("Topic")) %>
                        </a>
                        <a id="AncPost"  href="<%# YafBuildLink.GetLink(ForumPages.posts,"m={0}#post{0}", Container.DataItemToField<int>("MessageID")) %>" runat="server">&nbsp;
                           <img id="ImgPost" runat="server" title='<%#  this.GetText("GO_LAST_POST") %>' alt='<%#  this.GetText("GO_TO_LASTPOST") %>' src='<%#  GetThemeContents("ICONS", "ICON_LATEST") %>' />
                        </a>
                    </td>
                </tr>
                <tr class="postheader">
                    <td width="140px" id="NameCell" valign="top">
                        <a name="<%# Container.DataItemToField<int>("MessageID") %>" /><strong>
                            <VZF:UserLink ID="UserLink1" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                        </strong>
                        <VZF:OnlineStatusImage ID="onlineStatusImage1" runat="server" Visible='<%# this.Get<YafBoardSettings>().ShowUserOnlineStatus && !YAF.Core.UserMembershipHelper.IsGuestUser( Container.DataItemToField<int>("UserID") )%>'
                            Style="vertical-align: bottom" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                    </td>
                    <td width="80%" class="postheader">
                        <strong>
                            <VZF:LocalizedLabel runat="server" LocalizedTag="POSTED" />
                        </strong>
                        <VZF:DisplayDateTime id="LastVisitDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Posted") %>'></VZF:DisplayDateTime>
                    </td>
                </tr>
                <tr class="post">
                    <td colspan="2">
                        <VZF:MessagePostData ID="MessagePostPrimary" runat="server" ShowAttachments="false"
                            ShowSignature="false" HighlightWords="<%# this.HighlightSearchWords %>" DataRow='<%# (DataRow)Container.DataItem %>'>
                        </VZF:MessagePostData>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="header2">
                    <td colspan="2">
                        <strong>
                            <VZF:LocalizedLabel runat="server" LocalizedTag="topic" />
                        </strong><a title='<%# this.GetText("COMMON", "VIEW_TOPIC") %>' alt='<%# this.GetText("COMMON", "VIEW_TOPIC") %>' href="<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}", Container.DataItemToField<int>("TopicID")) %>">
                            <%# HtmlEncode(Container.DataItemToField<string>("Topic"))%>
                        </a>
                         <a id="AncAltPost"    href="<%# YafBuildLink.GetLink(ForumPages.posts,"m={0}#post{0}", Container.DataItemToField<int>("MessageID")) %>" >&nbsp;
                           <img id="ImgAltPost" title='<%#  this.GetText("GO_LAST_POST") %>' alt='<%#  this.GetText("GO_TO_LASTPOST") %>' src='<%#  GetThemeContents("ICONS", "ICON_LATEST") %>' />
                        </a>
                    </td>
                </tr>
                <tr class="postheader">
                    <td width="140px" id="NameCell" valign="top">
                        <a name="<%# Container.DataItemToField<int>("MessageID") %>" /><strong>
                            <VZF:UserLink ID="UserLink1" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                        </strong>
                        <VZF:OnlineStatusImage ID="onlineStatusImage1" runat="server" Visible='<%# this.Get<YafBoardSettings>().ShowUserOnlineStatus && !YAF.Core.UserMembershipHelper.IsGuestUser( Container.DataItemToField<int>("UserID") )%>'
                            Style="vertical-align: bottom" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                    </td>
                    <td width="80%" class="postheader">
                        <strong>
                            <VZF:LocalizedLabel runat="server" LocalizedTag="POSTED" />
                        </strong>
                        <VZF:DisplayDateTime id="LastVisitDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Posted") %>'></VZF:DisplayDateTime>
                    </td>
                </tr>
                <tr class="post_alt">
                    <td colspan="2">
                        <VZF:MessagePostData ID="MessagePostAlt" runat="server" ShowAttachments="false" ShowSignature="false"
                            HighlightWords="<%# this.HighlightSearchWords %>" DataRow="<%# (DataRow)Container.DataItem %>">
                        </VZF:MessagePostData>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <tr>
                    <td class="footer1" colspan="2">
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:PlaceHolder ID="NoResults" runat="Server" Visible="false">
            <table class="content" cellspacing="1" cellpadding="0" width="100%">
                <tr>
                    <td class="header1" colspan="2">
                        <VZF:LocalizedLabel runat="server" LocalizedTag="RESULTS" />
                    </td>
                </tr>
                <tr>
                    <td class="postheader" colspan="2" align="center">
                        <br />
                        <VZF:LocalizedLabel runat="server" LocalizedTag="NO_SEARCH_RESULTS" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="footer1" colspan="2">
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <VZF:Pager ID="Pager1" runat="server" LinkedPager="Pager" />
    </ContentTemplate>
</asp:UpdatePanel>

<VZF:MessageBox ID="LoadingModal" runat="server"></VZF:MessageBox>

<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
