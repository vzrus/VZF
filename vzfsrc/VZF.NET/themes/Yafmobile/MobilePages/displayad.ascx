<%@ Control Language="c#" AutoEventWireup="True" Inherits="VZF.Controls.DisplayAd"
    EnableViewState="false" Codebehind="../../../controls/DisplayAd.ascx.cs" %>
<tr class="postheader">
    <td width="140" id="NameCell" runat="server">
        <strong>
            <VZF:LocalizedLabel ID="SponserName" runat="server" LocalizedTag="AD_USERNAME" />
        </strong>
    </td>
</tr>
<tr class="<%#GetPostClass()%>">
    <td valign="top" class="message" >
        <div class="postdiv AdMessage">
            <VZF:MessagePost ID="AdMessage" runat="server"></VZF:MessagePost>
        </div>
    </td>
</tr>