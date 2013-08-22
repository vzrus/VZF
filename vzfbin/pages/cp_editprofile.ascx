<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="YAF.Pages.cp_editprofile" Codebehind="cp_editprofile.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="ProfileEdit" Src="../controls/EditUsersProfile.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<asp:UpdatePanel ID="ProfileUpdatePanel" runat="server">
    <ContentTemplate>
        <VZF:ProfileEdit runat="server" ID="ProfileEditor" />
    </ContentTemplate>
</asp:UpdatePanel>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
