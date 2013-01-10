<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="boardtags.ascx.cs" Inherits="YAF.Pages.boardtags" %>
<YAF:PageLinks ID="PageLinksTop" runat="server"></YAF:PageLinks>
<a id="top" name="top"></a>
<table class="command" width="100%">
    <tr>
        <td class="header1" colspan="1"> 
            <YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedPage="TAGSBOARD" LocalizedTag="TITLE" />
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
            <YAF:AlphaSort ID="AlphaSort1" runat="server" />
            <YAF:Pager ID="PagerTop"  UsePostBack="False" runat="server"/>
        </td>
    </tr>
    <tr>
        <td> 
         <YAF:SimpleTagCloud ID="TagCloudBoard" BoardId='<%# PageContext.PageBoardID %>' runat="server"/> 
        </td>
    </tr>
    <tr>
        <td>
            <YAF:Pager ID="PagerBottom" LinkedPager="PagerTop"  UsePostBack="False" runat="server"/>
        </td>
    </tr>
    <tr>
        <td style="text-align:center">
            <asp:Button ID="OKButton" OnClick="OKButton_Click" runat="server"/>
        </td>
    </tr>
 </table>