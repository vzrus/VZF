<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.messagehistory"CodeBehind="messagehistory.ascx.cs" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="VZF.Utils.Helpers" %>
<%@ Import Namespace="YAF.Classes" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" width="100%" cellspacing="1" cellpadding="0">
    <tr>
        <td class="header1" colspan="2">
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="title" />
        </td>
    </tr>
    <asp:Repeater ID="RevisionsList" runat="server">
        <ItemTemplate>
            <tr runat="server" id="history_tr" visible='<%# (Container.DataItemToField<DateTime>("Edited") != Container.DataItemToField<DateTime>("Posted")) %>'
                class="postheader">
                <td colspan="1" class="header2">
                    &nbsp;
                </td>
                <td id="history_column" colspan="1" class='<%# Container.DataItemToField<bool>("IsModeratorChanged") ?  "post_res" : "postheader" %>'
                    runat="server">
                    <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITED" />
                    </strong>:
                    <%# this.Get<IDateTime>().FormatDateTimeTopic( Container.DataItemToField<DateTime>("Edited") ) %>
                    <br />
                    <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITEDBY" />
                    </strong>
                    <VZF:UserLink ID="UserLink2" runat="server" UserID='<%# Container.DataItemToField<int>("EditedBy") %>' />
                    <br />
                    <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITREASON" />
                    </strong>
                    <%# Container.DataItemToField<string>("EditReason").IsNotSet() ? this.GetText("EDIT_REASON_NA") : Container.DataItemToField<string>("EditReason") %>
                    <br />
                    <span id="IPSpan1" runat="server" visible='<%# PageContext.IsAdmin || (this.Get<YafBoardSettings>().AllowModeratorsViewIPs && PageContext.ForumModeratorAccess)%>'>
                        <strong>
                            <%# this.GetText("IP") %>:</strong><a id="IPLink1" href='<%# this.Get<YafBoardSettings>().IPInfoPageURL.FormatWith(IPHelper.GetIp4Address(Container.DataItemToField<string>("IP"))) %>'
                                title='<%# this.GetText("COMMON","TT_IPDETAILS") %>'
                                target="_blank" runat="server"><%# IPHelper.GetIp4Address(Container.DataItemToField<string>("IP")) %></a>
                    </span>
                </td>
            </tr>
            <tr runat="server" id="original_tr" visible='<%# (Container.DataItemToField<DateTime>("Edited") == Container.DataItemToField<DateTime>("Posted")) %>'
                class="postheader">
                <td class="header2" colspan="1">
                    <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedPage="MESSAGEHISTORY"
                        LocalizedTag="ORIGINALMESSAGE">
                    </VZF:LocalizedLabel>
                </td>
                <td id="original_column" colspan="1" class='<%# Container.DataItemToField<bool>("IsModeratorChanged") ?  "post_res" : "postheader" %>'
                    runat="server">
                    <strong>
                        <VZF:UserLink ID="UserLink1" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                    </strong>
                    <VZF:OnlineStatusImage ID="OnlineStatusImage" runat="server" Visible='<%# this.Get<YafBoardSettings>().ShowUserOnlineStatus && !UserMembershipHelper.IsGuestUser( Container.DataItemToField<int>("UserID") )%>'
                        Style="vertical-align: bottom" UserID='<%# Container.DataItemToField<int>("UserID") %>' />
                    &nbsp; <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="POSTED" />
                    </strong>
                    <%# this.Get<IDateTime>().FormatDateTimeTopic( Container.DataItemToField<DateTime>("Posted") )%>
                    &nbsp; <span id="IPSpan2" runat="server" visible='<%# PageContext.IsAdmin || (this.Get<YafBoardSettings>().AllowModeratorsViewIPs && PageContext.IsModeratorInAnyForum)%>'>
                        <strong>
                            <%# this.GetText("IP") %>: </strong><a id="IPLink2" href='<%# this.Get<YafBoardSettings>().IPInfoPageURL.FormatWith(VZF.Utils.Helpers.IPHelper.GetIp4Address(Container.DataItemToField<string>("IP"))) %>'
                                title='<%# this.GetText("COMMON","TT_IPDETAILS") %>'
                                target="_blank" runat="server"><%# VZF.Utils.Helpers.IPHelper.GetIp4Address(Container.DataItemToField<string>("IP")) %></a>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="post" colspan="2" align="center">
                    <VZF:MessagePostData ID="MessagePostPrimary" runat="server" ShowAttachments="false"
                        ShowSignature="false" DataRow="<%# PageContext.IsAdmin || PageContext.IsModeratorInAnyForum ? Container.DataItem : null %>">
                    </VZF:MessagePostData>
                </td>
            </tr>
            <tr runat="server" id="historystart_tr" visible='<%# (Container.DataItemToField<DateTime>("Edited") == Container.DataItemToField<DateTime>("Posted")) && !singleReport %>'
                class="postheader">
                <td class="header2" colspan="2">
                    <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedPage="MESSAGEHISTORY"
                        LocalizedTag="HISTORYSTART">
                    </VZF:LocalizedLabel>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="CurrentMessageRpt" Visible="false" runat="server">
        <ItemTemplate>
            <tr class="postheader">
                <td class="header2" colspan="1" valign="top">
                    <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedPage="MESSAGEHISTORY"
                        LocalizedTag="CURRENTMESSAGE" />
                </td>
                <td colspan="1" class='<%# Container.DataItemToField<bool>("IsModeratorChanged") ?  "post_res" : "postheader" %>'
                    runat="server">
                    <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITED" /> </strong>
                    
                    <%# this.Get<IDateTime>().FormatDateTimeTopic( Container.DataItemToField<DateTime>("Edited") ) %>
                  
                    <br />
                    <strong>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITEDBY" />
                    </strong>
                    <VZF:UserLink ID="UserLink2" runat="server" UserID='<%# Container.DataItemToField<int>("EditedBy") %>' />
                    <br />                  
                        <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedPage="POSTMESSAGE"
                            LocalizedTag="EDITREASON" />                   
                    <%# Container.DataItemToField<string>("EditReason").IsNotSet() ? this.GetText("EDIT_REASON_NA") : Container.DataItemToField<string>("EditReason") %>
                    <br />
                    <span id="IPSpan3" runat="server" visible='<%# PageContext.IsAdmin || (this.Get<YafBoardSettings>().AllowModeratorsViewIPs && PageContext.IsModeratorInAnyForum)%>'>
                        <strong>
                            <%# this.GetText("IP") %>: </strong><a id="IPLink3" href='<%# this.Get<YafBoardSettings>().IPInfoPageURL.FormatWith(VZF.Utils.Helpers.IPHelper.GetIp4Address(Container.DataItemToField<string>("IP"))) %>'
                                title='<%# this.GetText("COMMON","TT_IPDETAILS") %>'
                                target="_blank" runat="server"><%# VZF.Utils.Helpers.IPHelper.GetIp4Address(Container.DataItemToField<string>("IP")) %></a>
                    </span>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="post" colspan="2" align="center">
                    <VZF:MessagePostData ID="MessagePostCurrent" runat="server" ShowAttachments="false"
                        ShowSignature="false" DataRow="<%# PageContext.IsAdmin || PageContext.IsModeratorInAnyForum ? Container.DataItem : null %>">
                    </VZF:MessagePostData>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr class="postfooter">
        <td class="post" colspan="2">
            <VZF:ThemeButton ID="ReturnBtn" CssClass="yafcssbigbutton leftItem" OnClick="ReturnBtn_OnClick"
                TextLocalizedTag="TOMESSAGE" Visible="false" runat="server">
            </VZF:ThemeButton>
            <VZF:ThemeButton ID="ReturnModBtn" CssClass="yafcssbigbutton leftItem" OnClick="ReturnModBtn_OnClick"
                TextLocalizedTag="GOMODERATE" Visible="false" runat="server">
            </VZF:ThemeButton>
        </td>
    </tr>
</table>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
