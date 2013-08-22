<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="editaccessmask.ascx.cs" Inherits="YAF.pages.editaccessmask" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" cellspacing="1" cellpadding="0" width="100%">
    <thead>
        <tr>
            <td class="header1" colspan="2">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EDITACCESSMASKS" />
            </td>
        </tr>
        <tr>
            <td class="header2" colspan="2" style="height:30px">&nbsp;</td>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedTag="MASK_NAME" LocalizedPage="ADMIN_EDITACCESSMASKS" />
            </td>
            <td class="post" width="50%">
                <asp:TextBox runat="server" ID="Name" CssClass="edit" style="width:250px" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" Text="<br />Enter name please!" ControlToValidate="Name" Display="Dynamic" />&nbsp;</td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel2" runat="server" LocalizedTag="MASK_ORDER" LocalizedPage="ADMIN_EDITACCESSMASKS" />
                <strong></strong><br />
            </td>
            <td class="post" width="50%">
                <asp:TextBox runat="server" ID="SortOrder" MaxLength="5" style="width:250px" CssClass="edit" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" Text="<br />Enter sort order please!" ControlToValidate="SortOrder" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel3" runat="server" LocalizedTag="READ_ACCESS" LocalizedPage="ADMIN_EDITACCESSMASKS" Suffix=":" />
            </td>
            <td class="post">
                <asp:CheckBox runat="server" ID="ReadAccess" />
            </td>
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
    </tbody>
    <tfoot>
        <tr class="footer1">
            <td align="center" colspan="2">
                <VZF:ThemeButton ID="Save" OnClick="Save_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="COMMON" TextLocalizedTag="SAVE" TitleLocalizedPage="COMMON" TitleLocalizedTag="SAVE" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="Cancel" OnClick="Cancel_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="COMMON" TextLocalizedTag="CANCEL" TitleLocalizedPage="COMMON" TitleLocalizedTag="CANCEL" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
