<%@ Control Language="c#" AutoEventWireup="True"
    Inherits="YAF.Pages.cp_signature" Codebehind="cp_signature.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="SignatureEdit" Src="../controls/EditUsersSignature.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<asp:UpdatePanel ID="SignatureUpdatePanel" runat="server">
    <ContentTemplate>
        <VZF:SignatureEdit runat="server" ID="SignatureEditor" />
    </ContentTemplate>
</asp:UpdatePanel>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
