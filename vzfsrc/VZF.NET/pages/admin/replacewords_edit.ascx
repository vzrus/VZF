<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="YAF.Pages.Admin.replacewords_edit" Codebehind="replacewords_edit.ascx.cs" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu ID="Adminmenu1" runat="server">
	
	<table class="content" cellspacing="1" cellpadding="0" width="100%">
		
		<tr>
			<td class="header1" colspan="2">
			  <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_REPLACEWORDS_EDIT" />
             </td>
		</tr>
        <tr>
	      <td class="header2" height="30" colspan="2"></td>
		</tr>
		<tr>
			<td class="postheader" width="50%">
              <VZF:HelpLabel ID="LocalizedLabel2" runat="server" LocalizedTag="BAD" LocalizedPage="ADMIN_REPLACEWORDS_EDIT" />
            </td>
			<td class="post" width="50%">
			  <asp:TextBox ID="badword" runat="server" Width="250"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="postheader" width="50%">
			  <VZF:HelpLabel ID="LocalizedLabel3" runat="server" LocalizedTag="GOOD" LocalizedPage="ADMIN_REPLACEWORDS_EDIT" />
            </td>
			<td class="post" width="50%">
			  <asp:TextBox ID="goodword" runat="server" Width="250"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="postfooter" align="center" colspan="2">
			  <asp:Button ID="save" runat="server" CssClass="pbutton"></asp:Button>
			  <asp:Button ID="cancel" runat="server" CssClass="pbutton"></asp:Button></td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
