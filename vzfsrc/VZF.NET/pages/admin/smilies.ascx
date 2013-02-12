<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.smilies"CodeBehind="smilies.ascx.cs" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Classes" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu runat="server">
    <asp:UpdatePanel ID="SmilesUpdatePanel" runat="server">
        <ContentTemplate>
            <VZF:Pager ID="Pager" runat="server" />
            <table width="100%" cellspacing="1" cellpadding="0" class="content">
                <asp:Repeater runat="server" ID="List">
                    <HeaderTemplate>
                        <tr>
                            <td class="header1" colspan="5">
                                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                        </tr>
                        <tr>
                            <td class="header2">
                                <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="ORDER" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                            <td class="header2">
                                <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="CODE" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                            <td class="header2" align="center">
                                <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="SMILEY" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                            <td class="header2">
                                <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="EMOTION" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                            <td class="header2">
                                <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="COMANDS" LocalizedPage="ADMIN_SMILIES" />
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="post">
                                <%# Eval("SortOrder") %>
                            </td>
                            <td class="post">
                                <%# Eval("Code") %>
                            </td>
                            <td class="post" align="center">
                                <img src="<%# YafForumInfo.ForumClientFileRoot + YafBoardFolders.Current.Emoticons %>/<%# Eval("Icon") %>" alt="<%# Eval("Icon") %>" />
                            </td>
                            <td class="post">
                                <%# Eval("Emoticon") %>
                            </td>
                            <td class="post">
                                <asp:LinkButton runat="server" CommandName="edit" CommandArgument='<%# Eval("SmileyID") %>'>
                                  <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="EDIT" LocalizedPage="COMMON" />
                                </asp:LinkButton>
                                |
                                <asp:LinkButton ID="MoveUp" runat="server" CommandName="moveup" CommandArgument='<%# Eval("SmileyID") %>'
                                    Text="▲" />
                                <asp:LinkButton ID="MoveDown" runat="server" CommandName="movedown" CommandArgument='<%# Eval("SmileyID") %>'
                                    Text="▼" />
                                |
                                <asp:LinkButton runat="server" OnLoad="Delete_Load" CommandName="delete" CommandArgument='<%# Eval("SmileyID") %>'>
                                  <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="DELETE" LocalizedPage="COMMON" />
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td class="footer1" colspan="5" align="center">
                                <asp:Button runat="server" CommandName="add" CssClass="pbutton" OnLoad="addLoad"> </asp:Button>
                                |
                                <asp:Button runat="server" CommandName="import" CssClass="pbutton" OnLoad="importLoad"></asp:Button>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <VZF:Pager ID="Pager1" runat="server" LinkedPager="Pager" />
        </ContentTemplate>
    </asp:UpdatePanel>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
