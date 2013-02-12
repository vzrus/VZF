<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.boards" Codebehind="boards.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
	<table cellspacing="1" cellpadding="0" width="100%" class="content">
		<tr>
			<td class="header1" colspan="3">
				<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_BOARDS" />
			</td>
		</tr>
		<tr>
			<td class="header2">
				<VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="ID" LocalizedPage="ADMIN_BOARDS" />
			</td>
			<td class="header2" colspan="2">
				<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="NAME" LocalizedPage="ADMIN_BOARDS" />
			</td>
		</tr>
		<asp:Repeater ID="List" runat="server">
			<ItemTemplate>
				<tr id="BoardRow" class='<%# Convert.ToInt32(Eval( "BoardID")) != PageContext.PageBoardID ? "post" : "post_res" %>' runat="server">
					<td >
						<%# Eval( "BoardID") %>
					</td>
					<td>
						<%# HtmlEncode(Eval( "Name")) %>
					</td>
					<td align="center">
                     <VZF:ThemeButton ID="ThemeButtonEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval( "BoardID") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server"></VZF:ThemeButton>
                     <VZF:ThemeButton ID="ThemeButtonDelete" CssClass="yaflittlebutton" OnLoad="Delete_Load"  CommandName='delete' CommandArgument='<%# Eval( "BoardID") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"></VZF:ThemeButton>						
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr class="footer1" align="center">
			<td colspan="3">
				<asp:Button ID="New" runat="server" CssClass="pbutton" />
			</td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
