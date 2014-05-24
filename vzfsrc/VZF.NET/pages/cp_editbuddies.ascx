<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="YAF.Pages.cp_editbuddies" Codebehind="cp_editbuddies.ascx.cs" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Register TagPrefix="VZF" TagName="BuddyList" Src="../controls/BuddyList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table width="100%" cellspacing="1" cellpadding="0" class="content">
    <tr>
        <td colspan="2" class="header1">
            <VZF:LocalizedLabel ID="BuddyList" runat="server" LocalizedTag="YOUR_BUDDYLIST" />
        </td>
    </tr>
    <tr>
        <td valign="top" rowspan="2">
             <asp:Panel id="BuddiesTabs" runat="server">
               <ul>
                 <li><a href="#BuddyListTab"><VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="BUDDYLIST" LocalizedPage="CP_EDITBUDDIES" /></a></li>
		         <li><a href="#PendingRequestsTab"><VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="PENDING_REQUESTS" LocalizedPage="CP_EDITBUDDIES" /></a></li>
		         <li><a href="#YourRequestsTab"><VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="YOUR_REQUESTS" LocalizedPage="CP_EDITBUDDIES" /></a></li>
               </ul>
                <div id="BuddyListTab">
                  <VZF:BuddyList runat="server" ID="BuddyList1" />
                </div>
                <div id="PendingRequestsTab">
                  <VZF:BuddyList runat="server" ID="PendingBuddyList" />
                </div>
                <div id="YourRequestsTab">
                  <VZF:BuddyList runat="server" ID="BuddyRequested" />
                </div>
             </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="hidLastTab" Value="0" />
<asp:HiddenField runat="server" ID="hidLastTabId" Value="0" />
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
