<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.im_xmpp" Codebehind="im_xmpp.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div align="center">
<table class="content"  width="600px" border="0" cellpadding="0" cellspacing="1">
  <tr>
        <td class="header1">
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" />
        </td>
  </tr>
  <tr>
        <td class="post">
            <asp:Label ID="NotifyLabel" runat="server"></asp:Label>    
        </td>
  </tr>
</table>  
</div>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
