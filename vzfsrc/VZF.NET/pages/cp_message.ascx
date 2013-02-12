<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.cp_message" Codebehind="cp_message.ascx.cs" %>
<%@ Import Namespace="YAF.Core"%>
<%@ Import Namespace="YAF.Core.Services" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<asp:Repeater ID="Inbox" runat="server" OnItemCommand="Inbox_ItemCommand">
    <HeaderTemplate>
        <table class="content" width="100%">
    </HeaderTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
    <SeparatorTemplate>
        <tr class="postsep">
            <td colspan="2" style="height: 7px">
            </td>
        </tr>
    </SeparatorTemplate>
    <ItemTemplate>
        <tr>
            <td class="header1" colspan="2">
                <%# HtmlEncode(Eval("Subject")) %>
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <b>
                    <VZF:UserLink ID="FromUserLink" runat="server" UserID='<%# Convert.ToInt32(Eval( "FromUserID" )) %>' />
                </b>
            </td>
            <td class="postheader" width="80%">
                <div class="leftItem postedLeft">
                    <b>
                        <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="posted" />
                    </b>
                    <VZF:DisplayDateTime ID="CreatedDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Created") %>'></VZF:DisplayDateTime>
                </div>
                <div class="rightItem postedRight">
                    <VZF:ThemeButton ID="DeleteMessage" runat="server" CssClass="yaflittlebutton" CommandName="delete"
                        CommandArgument='<%# Eval("UserPMessageID") %>' TextLocalizedTag="BUTTON_DELETE"
                        TitleLocalizedTag="BUTTON_DELETE_TT" OnLoad="ThemeButtonDelete_Load" />
                    <VZF:ThemeButton ID="ReplyMessage" runat="server" CssClass="yaflittlebutton" CommandName="reply"
                        CommandArgument='<%# Eval("UserPMessageID") %>' TextLocalizedTag="BUTTON_REPLY"
                        TitleLocalizedTag="BUTTON_REPLY_TT" />
                    <VZF:ThemeButton ID="QuoteMessage" runat="server" CssClass="yaflittlebutton" CommandName="quote"
                        CommandArgument='<%# Eval("UserPMessageID") %>' TextLocalizedTag="BUTTON_QUOTE"
                        TitleLocalizedTag="BUTTON_QUOTE_TT" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="post">
                &nbsp;
            </td>
            <td class="post" valign="top">
                <%# this.Get<IFormatMessage>().FormatMessage(Eval("Body") as string, new MessageFlags(Eval("Flags"))) %>
            </td>
        </tr>
        <tr class="postfooter">
            <td class="small postTop" colspan='2'>
                <a onclick="ScrollToTop();" class="postTopLink" href="javascript: void(0)">            
                  <VZF:ThemeImage ID="ThemeImage1" LocalizedTitlePage="POSTS" LocalizedTitleTag="TOP"  runat="server" ThemeTag="TOTOPPOST" />
                </a>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
