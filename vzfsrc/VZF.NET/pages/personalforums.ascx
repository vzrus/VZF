<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalforums.ascx.cs" Inherits="YAF.pages.personalforums" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top"></a>
<table class="content" cellspacing="1" cellpadding="0" width="100%">
    <thead>
        <tr>
            <td class="header1" colspan="3">
                <VZF:LocalizedLabel ID="LocalizedLabel17" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALFORUM" />
            </td>
        </tr>
        <tr>
            <th class="header2"> <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALFORUM" /></th>
            <th class="header2"> <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="SORT_ORDER" LocalizedPage="ADMIN_EDITFORUM" /></th>
            <th class="header2">&nbsp;</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="ForumList" OnItemCommand="ForumList_ItemCommand" runat="server">
            <ItemTemplate>
                <tr class="post">
                    <td align="left">
                        <strong>
                            <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Name\"]")) %>
                        </strong>
                        <br />
                        <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Description\"]")) %>
                    </td>
                    <td align="center">
                        <%# DataBinder.Eval(Container.DataItem, "[\"SortOrder\"]") %>
                    </td>
                    <td>
                        <VZF:ThemeButton ID="moderate1" CssClass="yaflittlebutton" CommandName='moderate' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TextLocalizedTag="BUTTON_MODERATE"  ImageThemePage="VOTE" ImageThemeTag="POLL_VOTED" runat="server" /> 
                        <VZF:ThemeButton ID="btnEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server" />
                        <VZF:ThemeButton ID="btnDelete" CssClass="yaflittlebutton" CommandName='delete' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
    <tfoot>
        <tr>
            <td class="footer1" colspan="3" align="center">
                <VZF:ThemeButton ID="NewForum" CssClass="yafcssbigbutton centerItem" OnClick="NewForum_Click" Visible="False"  TextLocalizedPage="ADMIN_FORUMS" TextLocalizedTag="NEW_FORUM" TitleLocalizedPage="ADMIN_FORUMS" TitleLocalizedTag="NEW_FORUM" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="Cancel" CssClass="yafcssbigbutton centerItem"  OnClick="Cancel_Click"  TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="OK" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
