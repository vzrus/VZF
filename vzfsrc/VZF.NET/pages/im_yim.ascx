<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.im_yim" Codebehind="im_yim.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div align="center">
    <asp:HyperLink runat="server" ID="Msg">
        <img runat="server" id="Img" border="0" /></asp:HyperLink>
</div>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
