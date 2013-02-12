<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="boardtags.ascx.cs" Inherits="YAF.Pages.boardtags" %>
<VZF:PageLinks ID="PageLinksTop" runat="server"></VZF:PageLinks>
<a id="top" name="top"></a>
<table class="command" width="100%">
    <tr>
        <td class="header1" colspan="1"> 
            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedPage="TAGSBOARD" LocalizedTag="TITLE" />
        </td>
    </tr>
     <tr>
        <td class="post" style="text-align: center">
            <asp:TextBox ID="UserSearchName" runat="server"></asp:TextBox>&nbsp;
            <asp:Button ID="SearchByUserName" runat="server" OnClick="Search_Click" Text='<%# this.GetText("SEARCH","BTNSEARCH") %>'  CssClass="pbutton" />&nbsp;
            <asp:Button ID="ResetUserSearch" runat="server" OnClick="Reset_Click" Text='<%# this.GetText("SEARCH","CLEAR") %>'  CssClass="pbutton" />
        </td>
    </tr>
    <tr>
        <td style="text-align:left">
            <VZF:AlphaSort ID="AlphaSort1" PagerPage="boardtags"  runat="server" />
            <VZF:Pager ID="PagerTop"  OnPageChange="Pager_PageChange" runat="server"/>
        </td>
    </tr>
    <tr>
        <td>
         <div ID="TagLinks" runat="server"></div>   
      <!-- <VZF:SimpleTagCloud ID="TagCloudBoard" BoardId='<%# PageContext.PageBoardID %>' runat="server"/> -->
        </td>
    </tr>
    <tr>
        <td>
            <VZF:Pager ID="PagerBottom" LinkedPager="PagerTop"  OnPageChange="Pager_PageChange" runat="server"/>
        </td>
    </tr>
    <tr>
        <td style="text-align:center">
            <asp:Button ID="OKButton" OnClick="OkButtonClick" runat="server"/>
        </td>
    </tr>
 </table>