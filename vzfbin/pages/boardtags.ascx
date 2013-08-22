<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="boardtags.ascx.cs" Inherits="YAF.Pages.boardtags" %>
<VZF:PageLinks ID="PageLinksTop" runat="server"/>

<a id="top" name="top"></a>
<table class="command" width="100%">
    <thead>
        <tr>
            <th class="header1" colspan="1">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedPage="TAGSBOARD" LocalizedTag="TITLE" />
            </th>
        </tr>
    <tr>
        <td class="post" style="text-align: center">
            <asp:TextBox ID="UserSearchName" runat="server"></asp:TextBox>&nbsp;
            <VZF:ThemeButton ID="SearchByUserName" runat="server" CssClass="yafcssbigbutton centerItem"
                OnClick="Search_Click" TextLocalizedPage="SEARCH" TextLocalizedTag="BTNSEARCH" TitleLocalizedPage="SEARCH" TitleLocalizedTag="BTNSEARCH" />&nbsp;
            <VZF:ThemeButton ID="ThemeButton1" runat="server" CssClass="yafcssbigbutton centerItem"
                OnClick="Reset_Click" TextLocalizedPage="SEARCH" TextLocalizedTag="CLEAR" TitleLocalizedPage="SEARCH" TitleLocalizedTag="CLEAR" />&nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align:left">
            <VZF:AlphaSort ID="AlphaSort1" PagerPage="boardtags"  runat="server" />
            <VZF:Pager ID="PagerTop"  OnPageChange="Pager_PageChange" runat="server"/>
        </td>
    </tr>
    </thead>
    <tbody>
    <tr>
        <td>
         <div ID="TagLinks" class="tagcloud" runat="server"></div>   
      <!-- <VZF:SimpleTagCloud ID="TagCloudBoard" BoardId='<%# PageContext.PageBoardID %>' runat="server"/> -->
        </td>
    </tr>
    </tbody>
    <tfoot>
    <tr>
        <td>
            <VZF:Pager ID="PagerBottom" LinkedPager="PagerTop"  OnPageChange="Pager_PageChange" runat="server"/>
        </td>
    </tr>
    <tr>
        <td style="text-align:center">
            <VZF:ThemeButton ID="ThemeButton2" runat="server" CssClass="yafcssbigbutton centerItem"
                OnClick="OkButtonClick" TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="OK" />
        </td>
    </tr>
    </tfoot>
 </table>