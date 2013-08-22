<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.Admin.topicstatus" Codebehind="topicstatus.ascx.cs" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu ID="Adminmenu1" runat="server">
	  <asp:Repeater ID="list" runat="server">
        <HeaderTemplate>
      	<table class="content" cellspacing="1" cellpadding="0" width="100%">
     
                <tr>
                    <td class="header1" colspan="4">
                          <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_TOPICSTATUS" />
                     </td>
                </tr>
                <tr>
                    <td class="header2" style="width:16px">
                        &nbsp;</td>
                    <td class="header2">
                        <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="TOPICSTATUS_NAME" LocalizedPage="ADMIN_TOPICSTATUS" />
                    </td>
                    <td class="header2">
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="DEFAULT_DESCRIPTION" LocalizedPage="ADMIN_TOPICSTATUS" />
                     </td>
                     <td class="header2">
                        &nbsp;</td>
                </tr>
        	 </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="post">
                        <VZF:ThemeImage runat="server" ID="TopicStatusIcon" ThemePage="TOPIC_STATUS" ThemeTag='<%# HtmlEncode(Eval("TopicStatusName")) %>'></VZF:ThemeImage>
                        
                </td>
                <td class="post">
                        <%# HtmlEncode(Eval("TopicStatusName")) %>
                </td>
                <td class="post">
						<%# HtmlEncode(Eval("DefaultDescription"))%>
				</td>
                <td class="post" style="text-align:right">
                    <VZF:ThemeButton ID="ThemeButtonEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval("TopicStatusId") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server"></VZF:ThemeButton>
                    <VZF:ThemeButton ID="ThemeButtonDelete" CssClass="yaflittlebutton" OnLoad="Delete_Load"  CommandName='delete' CommandArgument='<%# Eval("TopicStatusId") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"></VZF:ThemeButton>
                </td>
            </tr>
        	 </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td class="footer1" colspan="4" style="text-align:center">
                    <asp:Button runat="server" CommandName='add' ID="Linkbutton3" CssClass="pbutton" OnLoad="addLoad"></asp:Button>
                    |
                    <asp:Button runat="server" CommandName='import' ID="Linkbutton5" CssClass="pbutton" OnLoad="importLoad"> </asp:Button>
                    |
                    <asp:Button runat="server" CommandName='export' ID="Linkbutton4" CssClass="pbutton" OnLoad="exportLoad"></asp:Button>
                </td>
            </tr>
           	</table>
        	 </FooterTemplate>
    	 </asp:Repeater>

</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
