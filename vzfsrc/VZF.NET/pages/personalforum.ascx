<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalforum.ascx.cs" Inherits="YAF.pages.personalforum" %>
<YAF:PageLinks ID="PageLinks" runat="server"></YAF:PageLinks>
<a id="top" name="top"></a>
 <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="3">
                <YAF:LocalizedLabel ID="LocalizedLabel17" runat="server" LocalizedTag="FORUMS" LocalizedPage="TEAM" />
            </td>
        </tr>
        <tr>
        <td colspan="3">
        </td>
        </tr>
                <asp:Repeater ID="ForumList" OnItemCommand="ForumList_ItemCommand" runat="server">
                    <ItemTemplate>
                        <tr class="post">
                            <td align="left">
                                <strong>
                                    <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Name\"]")) %></strong><br />
                                <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Description\"]")) %>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem, "[\"SortOrder\"]") %>
                            </td>
                            <td>
                             <YAF:ThemeButton ID="moderate1" CssClass="yaflittlebutton" CommandName='moderate' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TextLocalizedTag="BUTTON_MODERATE"  ImageThemePage="VOTE" ImageThemeTag="POLL_VOTED" runat="server" /> 
                             <YAF:ThemeButton ID="btnEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server"></YAF:ThemeButton>								
                             <YAF:ThemeButton ID="btnDelete" CssClass="yaflittlebutton" CommandName='delete' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"></YAF:ThemeButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
        <tr>
            <td class="footer1" colspan="3" align="center">
                <asp:Button ID="NewForum" runat="server" OnClick="NewForum_Click" CssClass="pbutton"></asp:Button>
            </td>
        </tr>
    </table>
<YAF:SmartScroller ID="SmartScroller1" runat="server" />