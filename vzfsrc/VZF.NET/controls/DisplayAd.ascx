<%@ Control Language="c#" AutoEventWireup="True" Inherits="VZF.Controls.DisplayAd"
    EnableViewState="false" Codebehind="DisplayAd.ascx.cs" %>
<tr class="postheader">
    <td width="140" id="NameCell" runat="server">
        <strong>
            <VZF:LocalizedLabel ID="SponserName" runat="server" LocalizedTag="AD_USERNAME" />
        </strong>
    </td>
    <td width="80%" colspan="2">
    </td>
</tr>
<tr class="<%#GetPostClass()%>">
    <td valign="top" class="message" colspan='3'>
        <div class="postdiv AdMessage">
            <VZF:MessagePost ID="AdMessage" runat="server"></VZF:MessagePost>
        </div>
    </td>
</tr>
<tr class="postfooter">
    <td class="small postTop" colspan='2'>
        <a onclick="ScrollToTop();" class="postTopLink" href="javascript: void(0)">            
            <VZF:ThemeImage ID="ThemeImage1" LocalizedTitlePage="POSTS" LocalizedTitleTag="TOP"  runat="server" ThemeTag="TOTOPPOST" />
        </a>
    </td>
    <td class="postfooter">
        &nbsp;
    </td>
</tr>
<tr class="postsep">
    <td colspan="3">

    </td>
</tr>
