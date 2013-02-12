<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.languages" Codebehind="languages.ascx.cs" %>
<%@ Register TagPrefix="YAF" Namespace="VZF.Controls" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
	<table class="content" width="100%" cellspacing="1" cellpadding="0">
		<tr>
			<td class="header1" colspan="8">
				<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_LANGUAGES" />
			</td>
		</tr>
		<asp:Repeater runat="server" ID="List">
			<HeaderTemplate>
				<tr class="header2">
                    <td>
						<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="LANG_NAME" LocalizedPage="ADMIN_LANGUAGES" />
					</td>
                    <td>
						<VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="CULTURE_TAG" LocalizedPage="ADMIN_LANGUAGES" />
					</td>
                     <td>
						<VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="NATIVE_NAME" LocalizedPage="ADMIN_LANGUAGES" />
					</td>
					<td>
						<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="FILENAME" />
					</td>
                    <td>
						&nbsp;
					</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr class="post">
                    <td>
						<%# Eval("CultureEnglishName")%>
					</td>
                    <td>
						<%# Eval("CultureTag")%>
					</td>
                     <td>
						<%# Eval("CultureNativeName")%>
					</td>
					<td>
						<%# Eval("CultureFile")%>
					</td>
                    <td>
						<asp:LinkButton runat="server" CommandName="edit" CommandArgument='<%# Eval("CultureFile")%>'><VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="EDIT" /></asp:LinkButton>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
         <tr>
           <td class="footer1" align="center" colspan="8" style="height:30px"></td>
         </tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
