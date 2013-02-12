<%@ Control Language="c#" AutoEventWireup="True"
	Inherits="YAF.Pages.personalgroup_edituser" Codebehind="personalgroup_edituser.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" cellspacing="1" cellpadding="0" width="100%">
	<tr>
		<td class="header1" colspan="2">
			<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" />
		</td>
	</tr>
	<tr>
		<td class="postheader" width="50%">
			<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="USER" />
		</td>
		<td class="post" width="50%">
			<asp:TextBox runat="server" ID="UserName" /><asp:DropDownList runat="server" ID="ToList"
				Visible="false" />
			<asp:Button runat="server" ID="FindUsers" OnClick="FindUsers_Click" /></td>
	</tr>
	<tr class="footer1">
		<td colspan="2" align="center">
			<asp:Button runat="server" ID="Save" CssClass="pbutton" OnClick="Update_Click" />
			<asp:Button runat="server" ID="Cancel" CssClass="pbutton" OnClick="Cancel_Click" />
		</td>
	</tr>
</table>
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
