<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserNickStyleEditor.ascx.cs" Inherits="VZF.Controls.UserNickStyleEditor" %>
<table width="100%" class="content" style="width:100%">
<asp:Repeater ID="StylesRepeater" runat="server" Visible="true">
    <ItemTemplate>
        <tr>
            <td class="postheader">
                <asp:TextBox runat="server" ID="SkinName"></asp:TextBox>
            </td>
            <td class="post">
                 <asp:TextBox runat="server" ID="SkinStyle"></asp:TextBox>
            </td>
        </tr>
    </ItemTemplate>
  
</asp:Repeater>
     </table>
