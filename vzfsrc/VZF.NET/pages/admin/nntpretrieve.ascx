<%@ Control Language="c#" AutoEventWireup="True"
	Inherits="YAF.Pages.Admin.nntpretrieve" Codebehind="nntpretrieve.ascx.cs" %>
<%@ Register TagPrefix="YAF" Namespace="VZF.Controls" %>
<%@ Import Namespace="YAF.Core"%>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
	<table class="content" width="100%" cellspacing="1" cellpadding="0">
		<tr>
			<td colspan="3" class="header1">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="HEADER" LocalizedPage="ADMIN_NNTPRETRIEVE" />
			</td>
		</tr>
		<asp:Repeater runat="server" ID="List">  
			<HeaderTemplate>  
				<tr class="header2">
					<td>
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="GROUPS" LocalizedPage="ADMIN_NNTPRETRIEVE" />
					</td>
					<td align="right">
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="LAST_MESSAGE" LocalizedPage="ADMIN_NNTPRETRIEVE" />
					</td>
					<td>
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="LAST_UPDATE" LocalizedPage="ADMIN_NNTPRETRIEVE" />
					</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr class="post">
					<td>
						<%# Eval("GroupName") %>
					</td>
					<td align="right">
						<%# LastMessageNo(Container.DataItem) %>
					</td>
					<td>
						<%# this.Get<IDateTime>().FormatDateTime(Eval("LastUpdate")) %>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan="2" class="postheader" width="50%">
                <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="TIME" LocalizedPage="ADMIN_NNTPRETRIEVE" />
				</td>
			<td class="post" width="50%">
				<asp:TextBox runat="server" ID="Seconds" Text="30" />&nbsp;<VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="SECONDS" LocalizedPage="ADMIN_NNTPRETRIEVE" /></td>
		</tr>
		<tr>
			<td colspan="3" align="center" class="footer1">
				<asp:Button runat="server" ID="Retrieve" Text="Retrieve" CssClass="pbutton" OnClick="Retrieve_Click" /></td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
