<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.prune" Codebehind="prune.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
	<table class="content" cellspacing="1" cellpadding="0" width="100%">
		<tr>
			<td class="header1" colspan="2">
				<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_PRUNE" />
			</td>
		</tr>
		<tr>
			<td class="header2" colspan="2" height="30px">
				<asp:Label ID="lblPruneInfo" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td class="postheader" width="50%">
                <VZF:HelpLabel ID="LocalizedLabel4" runat="server" LocalizedTag="PRUNE_FORUM" LocalizedPage="ADMIN_PRUNE" />
			</td>
			<td class="post" width="50%">
				<asp:DropDownList ID="forumlist" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td class="postheader">
                <VZF:HelpLabel ID="LocalizedLabel3" runat="server" LocalizedTag="PRUNE_DAYS" LocalizedPage="ADMIN_PRUNE" />
			</td>
			<td class="post">
				<asp:TextBox ID="days" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="postheader">
                <VZF:HelpLabel ID="LocalizedLabel2" runat="server" LocalizedTag="PRUNE_PERMANENT1" LocalizedPage="ADMIN_PRUNE" />
			</td>
			<td class="post">
				<asp:CheckBox ID="permDeleteChkBox" runat="server" />
			</td>
		</tr>
        <tr>
			<td class="postheader">
                <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedTag="PRUNE_DELETED" LocalizedPage="ADMIN_PRUNE" />
			</td>
			<td class="post">
				<asp:CheckBox ID="deletedOnlyChkBox" runat="server" />
			</td>
		</tr>
		<tr>
			<td class="footer1" colspan="2" align="center">
				<asp:Button ID="commit" runat="server" class="pbutton" OnLoad="PruneButton_Load">
				</asp:Button>
			</td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
