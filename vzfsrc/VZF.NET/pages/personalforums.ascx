<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalforums.ascx.cs" Inherits="YAF.pages.personalforums" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="VZF" TagName="ForumLastPost" Src="~/Controls/ForumLastPost.ascx" %>
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
            <th class="header2"> <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALFORUM" /></th>
            <th class="header2">&nbsp;</th>
        </tr>
    <tbody>
        <asp:Repeater ID="ForumList" OnItemCommand="ForumList_ItemCommand" runat="server">
            <ItemTemplate>
                <tr class="post">
                    <td align="left">
                        <strong>
                           <a id="Forum1" href='<%# YafBuildLink.GetLinkNotEscaped(ForumPages.topics,"f={0}".FormatWith(DataBinder.Eval(Container.DataItem, "[\"ForumID\"]")))  %>'  runat="server">
                            <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Forum\"]")) %></a>
                        </strong>
                        <br />
                        <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Description\"]")) %>
                    </td>
    </thead>
                    <td align="center">
                     	<VZF:ForumLastPost DataRow="<%# ((DataRowView)Container.DataItem).Row %>" Visible='<%# (((System.Data.DataRowView)Container.DataItem)["RemoteURL"] == DBNull.Value) %>'
					ID="lastPost" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
    <tfoot>
        <tr>
            <td class="footer1" colspan="3" align="center">
                <VZF:ThemeButton ID="Cancel" CssClass="yafcssbigbutton centerItem"  OnClick="Cancel_Click"  TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="OK" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
