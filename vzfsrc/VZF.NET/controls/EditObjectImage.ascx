<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="VZF.Controls.EditObjectImage" Codebehind="EditObjectImage.ascx.cs" %>
<table width="100%" class="content" cellspacing="1" cellpadding="4">
    <tr>
        <td class="header1" colspan="4">
            <VZF:LocalizedLabel runat="server" LocalizedPage="IMAGEADD" LocalizedTag="title" />
        </td>
    </tr>
    <tr runat="server" id="AvatarCurrentText">
        <td class="header2">
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedPage="IMAGEADD"
                LocalizedTag="IMAGECURRENT" />
        </td>
        <td class="header2" colspan="3">
            <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedPage="IMAGEADD"
                LocalizedTag="IMAGENEW" />
        </td>
    </tr>
    <tr>
        <td class="post" align="center" rowspan="4" runat="server" id="topicImageTD">
            <asp:Image ID="TopicImg" runat="server" Visible="true" AlternateText="Image" />
            <br />
            <br />
            <asp:Label runat="server" ID="NoImage" Visible="false" />
            <asp:Button runat="server" ID="DeleteImage" CssClass="pbutton" Visible="false" OnClick="DeleteAvatar_Click" /></td>
    </tr>
    <tr runat="server" id="ImageOurs">
        <td class="postheader">
            <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedPage="IMAGEADD"
                LocalizedTag="OURIMAGE" />
        </td>
        <td class="post" colspan="2">
            [
            <asp:HyperLink ID="OurImage" runat="server" />
            ]</td>
    </tr>
    <tr runat="server" id="ImageRemoteRow">
        <td class="postheader">
            <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedPage="IMAGEADD"
                LocalizedTag="IMAGEREMOTE" />
        </td>
        <td class="post">
            <asp:TextBox CssClass="edit" ID="TopicImage" runat="server" /> <br />
             <em><asp:Label id="noteRemote" runat="server"></asp:Label></em></td>
        <td class="post">
            <asp:Button ID="UpdateRemote" CssClass="pbutton" runat="server" OnClick="RemoteUpdate_Click" /></td>
    </tr>
    <tr runat="server" id="ImageUploadRow">
        <td class="postheader">
            <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedPage="IMAGEADD"
                LocalizedTag="IMAGEUPLOAD" />
        </td>
        <td class="post">
            <input type="file" id="File" runat="server" /> <br />
             <em><asp:Label id="noteLocal" runat="server"></asp:Label></em></td>
        <td class="post">
            <asp:Button ID="UpdateUpload" CssClass="pbutton" runat="server" OnClick="UploadUpdate_Click" /></td>
    </tr>
    <tr>
        <td class="footer1" colspan="4" align="center">
            <asp:Button ID="Back" CssClass="pbutton" runat="server" OnClick="Back_Click" />
        </td>
    </tr>
</table>
