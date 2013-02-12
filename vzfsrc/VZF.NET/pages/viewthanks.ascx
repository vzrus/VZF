<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.ViewThanks" Codebehind="ViewThanks.ascx.cs" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Register TagPrefix="VZF" TagName="ViewThanksList" Src="../controls/ViewThanksList.ascx" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<div class="DivTopSeparator">
</div>
        <br style="clear: both" />
       <asp:Panel id="ThanksTabs" runat="server">
               <ul>
                 <li><a href="#ThanksFromTab"><VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="ThanksFromUser" LocalizedPage="VIEWTHANKS" /></a></li>
		         <li><a href="#ThanksToTab"><VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="ThanksToUser" LocalizedPage="VIEWTHANKS" /></a></li>		        
               </ul>
                <div id="ThanksFromTab">
                   <VZF:ViewThanksList runat="server" ID="ThanksFromList" CurrentMode="FromUser" />
                </div>
                <div id="ThanksToTab">
                  <VZF:ViewThanksList runat="server" ID="ThanksToList" CurrentMode="ToUser" />
                </div>
             </asp:Panel>
        <asp:HiddenField runat="server" ID="hidLastTab" Value="0" /><asp:HiddenField runat="server" ID="hidLastTabId" Value="0" />
 <asp:Button id="ChangeTab" OnClick="ChangeTabClick" runat="server" style="display:none" />
<div id="Div1">
    <VZF:SmartScroller ID="SmartScroller2" runat="server" />
</div>
