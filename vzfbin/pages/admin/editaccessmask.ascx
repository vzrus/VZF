﻿<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="YAF.Pages.Admin.editaccessmask" Codebehind="editaccessmask.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="2">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EDITACCESSMASKS" />
            </td>
        </tr>
        <tr>
          <td class="header2" colspan="2" style="height:30px"></td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedTag="MASK_NAME" LocalizedPage="ADMIN_EDITACCESSMASKS" />
            </td>
            <td class="post" width="50%">
                <asp:TextBox runat="server" ID="Name" CssClass="edit" style="width:250px" /><asp:RequiredFieldValidator
                    runat="server" Text="<br />Enter name please!" ControlToValidate="Name" Display="Dynamic" /></td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel2" runat="server" LocalizedTag="MASK_ORDER" LocalizedPage="ADMIN_EDITACCESSMASKS" />
                <strong></strong><br />
                </td>
            <td class="post" width="50%">
                <asp:TextBox runat="server" ID="SortOrder" MaxLength="5" style="width:250px" CssClass="edit" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" Text="<br />Enter sort order please!" ControlToValidate="SortOrder" Display="Dynamic" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel3" runat="server" LocalizedTag="READ_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="ReadAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel4" runat="server" LocalizedTag="POST_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="PostAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel5" runat="server" LocalizedTag="REPLY_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="ReplyAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel6" runat="server" LocalizedTag="PRIORITY_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="PriorityAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel7" runat="server" LocalizedTag="POLL_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="PollAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel8" runat="server" LocalizedTag="VOTE_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="VoteAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel9" runat="server" LocalizedTag="MODERATOR_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="ModeratorAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel10" runat="server" LocalizedTag="EDIT_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="EditAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel11" runat="server" LocalizedTag="DELETE_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="DeleteAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel12" runat="server" LocalizedTag="UPLOAD_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="UploadAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel13" runat="server" LocalizedTag="DOWNLOAD_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="DownloadAccess" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel14" runat="server" LocalizedTag="CREATEUSERFORUM_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="UserForumAccess" /></td>
        </tr>
         <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel15" runat="server" LocalizedTag="ISADMINMASK" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="IsAdminMaskChk" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel16" runat="server" LocalizedTag="ISUSERMASK" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="IsUserMaskChk" /></td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel17" runat="server" LocalizedTag="CREATEDBYUSER" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
               <asp:Label ID="UserID" runat="server"></asp:Label>&nbsp;<asp:Label runat="server" ID="CreatedByUser" CssClass="edit" style="width:250px" /></td>
        </tr>
        <tr class="footer1">
            <td align="center" colspan="2">
                <asp:Button ID="Save" runat="server" OnClick="Save_Click" CssClass="pbutton" />
                <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" CausesValidation="false" CssClass="pbutton" />
            </td>
        </tr>
    </table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
