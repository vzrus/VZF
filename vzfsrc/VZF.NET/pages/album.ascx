<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.Album" Codebehind="album.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="AlbumImageList" Src="../controls/AlbumImageList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" width="100%" cellpadding="0">
    <tr>
        <td class="header1">
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="Albums_Title" />
        </td>
    </tr>
    <tr>
        <td class="post">
            <VZF:AlbumImageList ID="AlbumImageList1" runat="server"></VZF:AlbumImageList>
        </td>
    </tr>
        <tr class="footer1">
		<td colspan="3" style="text-align: center">
			<asp:Button runat="server" CssClass="pbutton" ID="Back" OnClick="Back_Click" />
		</td>
	</tr>
</table>
