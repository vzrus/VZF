<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="YAF.Pages.Admin.editcategory" Codebehind="editcategory.ascx.cs" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu runat="server">
	<table class="content" cellspacing="1" cellpadding="0" width="100%">
		
		<tr>
			<td class="header1" colspan="2">
			<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="HEADER" LocalizedPage="ADMIN_EDITCATEGORY" />
			<asp:Label ID="CategoryNameTitle" runat="server"></asp:Label></td>
		</tr>
        <tr>
	      <td class="header2" height="30" colspan="2"></td>
		</tr>
		<tr>
			<td class="postheader">
			  <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedTag="CATEGORY_NAME" LocalizedPage="ADMIN_EDITCATEGORY" />
			</td>
			<td class="post">
			<asp:TextBox ID="Name" runat="server" MaxLength="50" Width="250"></asp:TextBox></td>
		</tr>
		<tr>
		<tr>
            <td class="postheader">
                <VZF:HelpLabel ID="CanHavePersForumsLbl" runat="server" LocalizedTag="CANHAVEUSERFORUMS" LocalizedPage="ADMIN_EDITFORUM" />
            </td>
            <td class="post">
                <asp:CheckBox ID="CanHavePersForums" runat="server"></asp:CheckBox>
            </td>
        </tr>   
			<td class="postheader">
			  <VZF:HelpLabel ID="HelpLabel2" runat="server" LocalizedTag="CATEGORY_IMAGE" LocalizedPage="ADMIN_EDITCATEGORY" />
			</td>
			<td class="post">
			<asp:DropDownList ID="CategoryImages" Width="250" runat="server" />
			<img align="middle" alt="Preview" runat="server" id="Preview" />
			</td>
		</tr>
		<tr id="rowSortOrder" runat="server">
			<td class="postheader">
			  <VZF:HelpLabel ID="HelpLabel3" runat="server" LocalizedTag="SORT_ORDER" LocalizedPage="ADMIN_EDITCATEGORY" />
			</td>
			<td class="post">
			<asp:TextBox ID="SortOrder" runat="server" Width="250" MaxLength="5"></asp:TextBox></td>
		</tr>
		<tr>
			<td class="postfooter" colspan="2" align="center">
			  <asp:Button ID="Save" runat="server" OnClick="Save_Click" CssClass="pbutton"></asp:Button>
			  <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" CssClass="pbutton"></asp:Button>
            </td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
