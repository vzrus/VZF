<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.editnntpforum"
    CodeBehind="editnntpforum.ascx.cs" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu runat="server">
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="2">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
        </tr>
        <tr>
            <td class="header2" height="30" colspan="2">
            </td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="LocalizedLabel2" runat="server" LocalizedTag="SERVER" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
            <td class="post" width="50%">
                <asp:DropDownList ID="NntpServerID" runat="server" Width="250" />
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="LocalizedLabel3" runat="server" LocalizedTag="GROUP" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
            <td class="post">
                <asp:TextBox ID="GroupName" runat="server" Width="250" />
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="LocalizedLabel4" runat="server" LocalizedTag="FORUM" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
            <td class="post">
                    <div id="divactive" class="active" runat="server">
        <b><span id="echoActive">-</span></b></div>
        <div class="container">
    <div id="tree">
       
    </div>
           <!-- <input id="btnshowtree" value="show tree" type="button"/> -->
        </div>
                <asp:PlaceHolder ID="phForums" runat="server"></asp:PlaceHolder>
              
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel10" runat="server" LocalizedTag="DATECUTOFF" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
            <td class="post">
                <asp:TextBox Width="250" ID="DateCutOff" runat="server" Enabled="true" />
            </td>
        </tr>
        <tr>
            <td class="postheader">
                <VZF:HelpLabel ID="LocalizedLabel5" runat="server" LocalizedTag="ACTIVE" LocalizedPage="ADMIN_EDITNNTPFORUM" />
            </td>
            <td class="post">
                <asp:CheckBox ID="Active" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="postfooter" align="center" colspan="2">
                <asp:Button ID="Save" runat="server" CssClass="pbutton" OnClick="Save_Click" />&nbsp;
                <asp:Button ID="Cancel" runat="server" CssClass="pbutton" OnClick="Cancel_Click" />
            </td>
        </tr>
    </table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
