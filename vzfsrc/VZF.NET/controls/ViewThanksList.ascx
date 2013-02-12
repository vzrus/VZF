<%@ Control Language="C#" AutoEventWireup="true" Inherits="VZF.Controls.ViewThanksList"
    CodeBehind="ViewThanksList.ascx.cs" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<table class="command" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td>
             <VZF:Pager runat="server" ID="PagerTop" OnPageChange="Pager_PageChange" />
        </td>
    </tr>
</table>
<asp:Repeater ID="ThanksRes" runat="server" OnItemCreated="ThanksRes_ItemCreated">
    <HeaderTemplate>
        <table class="content" cellspacing="1" cellpadding="0" width="100%">
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="header2">
            <td colspan="2">
                 <strong>
                      <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="topic" />
                        </strong><a title='<%# this.GetText("COMMON", "VIEW_TOPIC") %>' href="<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}", Container.DataItemToField<int>("TopicID")) %>">
                            <%# this.HtmlEncode(Container.DataItemToField<string>("Topic")) %>
                        </a>
                        <a id="AncPost"  href="<%# YafBuildLink.GetLink(ForumPages.posts,"m={0}#post{0}", Container.DataItemToField<int>("MessageID")) %>" runat="server">&nbsp;
                           <img id="ImgPost" runat="server" title='<%#  this.GetText("GO_LAST_POST") %>' alt='<%#  this.GetText("GO_TO_LASTPOST") %>' src='<%#  this.Get<ITheme>().GetItem("ICONS", "ICON_LATEST") %>' />
                        </a>
            </td>
        </tr>
        <tr class="postheader">
            <td width="140px" id="ThanksNumberCell" valign="top" runat="server">
               &nbsp;
            </td>
            <td width="140px" id="NameCell" valign="top" runat="server">
                <a name="<%# Container.DataItemToField<int>("MessageID") %>" /><b>
                    <VZF:UserLink ID="UserLink1" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %> ' />
                </b>
                <VZF:OnlineStatusImage ID="OnlineStatusImage" runat="server" Visible='<%# PageContext.BoardSettings.ShowUserOnlineStatus && !UserMembershipHelper.IsGuestUser( Container.DataItemToField<int>("UserID") )%>'
                    Style="vertical-align: bottom" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
            </td>
            <td width="80%" class="postheader">
                <strong>
                    <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="POSTED" />
                </strong>
                <VZF:DisplayDateTime ID="PostedDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Posted") %>'></VZF:DisplayDateTime>
            </td>
        </tr>
        <tr class="<%# this.IsOdd() ? "post_alt" : "post" %>">
            <td colspan="2">
                <VZF:MessagePostData ID="MessagePostPrimary" runat="server" ShowAttachments="false"
                    ShowSignature="false" DataRow='<%# Container.DataItem %>'>
                </VZF:MessagePostData>
            </td>
        </tr>
    </ItemTemplate>
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
            <td class="postheader" colspan="2" align="center">
                <br />
                <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="NO_THANKS" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="footer1" colspan="2">
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
<table class="command" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td>
 <VZF:Pager runat="server" ID="PagerBottom" LinkedPager="PagerTop" OnPageChange="Pager_PageChange" />
        </td>
    </tr>
</table>
