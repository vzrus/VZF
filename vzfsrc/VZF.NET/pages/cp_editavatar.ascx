<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="YAF.Pages.cp_editavatar" Codebehind="cp_editavatar.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="ProfileEdit" Src="../controls/EditUsersAvatar.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<VZF:ProfileEdit runat="server" ID="ProfileEditor" />

<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
