<%@ Control Language="c#" AutoEventWireup="True"
	Inherits="YAF.Pages.Admin.pageaccessedit" Codebehind="pageaccessedit.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
	<table class="content"  cellspacing="1" cellpadding="0" width="100%">
	     <tr class="header1">
	          <td class="header1" colspan="3">
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="TITLE"  LocalizedPage="ADMIN_PAGEACCESSEDIT" />
              </td>
         </tr>
         <tr>
             <td class="header2">
                     <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="HEADER" LocalizedPage="ADMIN_PAGEACCESSEDIT" />&nbsp;
                     <VZF:LocalizedLabel ID="UserNameLabel" runat="server" LocalizedTag="USERNAME" LocalizedPage="ADMIN_PAGEACCESSEDIT" />&nbsp;
                     <asp:Label ID="UserName" runat="server"  />&nbsp;
             </td>
             <td class="header2">
                     <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="PAGE" LocalizedPage="ADMIN_PAGEACCESSEDIT" />&nbsp;
             </td>
             <td class="header2">
                     <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="CANACCESS"  LocalizedPage="ADMIN_PAGEACCESSEDIT" />
            </td>
        </tr>

	      <asp:Repeater ID="AccessList" OnItemDataBound="AccessList_OnItemDataBound" runat="server">
            <ItemTemplate>
                <tr>
                     <td class="post">
                        <strong>
                           <asp:Label ID="PageText" runat="server" /> 
                        </strong>
                      </td>
                     <td class="post">
                        <strong>
                           <asp:Label ID="PageName" runat="server" /> 
                        </strong>
                    </td>
                    <td class="post">
                      <asp:CheckBox  ID="ReadAccess" runat="server"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
		<tr class="footer1">
			<td style="text-align: center" colspan="3">
				<asp:Button ID="Save" runat="server" OnClick="Save_Click" CssClass="pbutton" />
                <asp:Button ID="GrantAll" runat="server" OnClick="GrantAll_Click" CssClass="pbutton" />
                <asp:Button ID="RevokeAll" runat="server" OnClick="RevokeAll_Click" CssClass="pbutton" />
				<asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="pbutton" />
			</td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
