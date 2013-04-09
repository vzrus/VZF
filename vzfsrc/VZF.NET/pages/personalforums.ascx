<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalforums.ascx.cs" Inherits="YAF.pages.personalforums" %>
<%@ Register TagPrefix="VZF" TagName="ForumCategoryList" Src="../controls/ForumCategoryList.ascx" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top"></a>
<table class="content" cellspacing="1" cellpadding="0" width="100%">
    <thead>
        <tr>
            <td class="header1" colspan="3">
                <VZF:LocalizedLabel ID="LocalizedLabel17" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALFORUM" />
            </td>
        </tr>
        <tr>
        </tr>
    </thead>
    <tbody>
        <VZF:ForumCategoryList ID="ForumCategoryList" runat="server"/>
    </tbody>
    <tfoot>
        <tr>
            <td class="footer1" colspan="3" align="center">
                <VZF:ThemeButton ID="NewForum" CssClass="yafcssbigbutton centerItem" OnClick="NewForum_Click" Visible="False"  TextLocalizedPage="ADMIN_FORUMS" TextLocalizedTag="NEW_FORUM" TitleLocalizedPage="ADMIN_FORUMS" TitleLocalizedTag="NEW_FORUM" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="Cancel" CssClass="yafcssbigbutton centerItem"  OnClick="Cancel_Click"  TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="OK" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
