<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.Admin.restartapp" Codebehind="restartapp.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu ID="adminmenu1" runat="server">
	<table width="100%" cellspacing="0" cellpadding="0" class="content">
		<tr>
			<td class="header1">
				<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_RESTARTAPP" />
			</td>
		</tr>
        <tr>
	      <td class="header2" style="height:30px"></td>
		</tr>
		<tr class="post">
			<td>
				<p style="text-align:center">
					<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="INFO" LocalizedPage="ADMIN_RESTARTAPP" />
				</p>
			</td>
		</tr>
		<tr>
			<td colspan="2" class="postfooter" align="center">
				<asp:Button ID="RestartApp" runat="server" Text="Restart Application" CssClass="pbutton" OnClick="RestartApp_Click"></asp:Button>
			</td>
		</tr>
	</table>
</VZF:AdminMenu>
