<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.Albums" Codebehind="albums.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="AlbumList" Src="../controls/AlbumList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" width="100%" cellpadding="0">
    <tr>
        <td class="header1">
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="title" />
        </td>
    </tr>
    <tr>
        <td class="post">
            <VZF:AlbumList ID="AlbumList1" runat="server"></VZF:AlbumList>
        </td>
    </tr>
</table>
