<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="YAF.Pages.imageadd" Codebehind="imageadd.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="EditObjectImage" Src="../controls/EditObjectImage.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<VZF:EditObjectImage runat="server" ID="ProfileEditor" />

<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
