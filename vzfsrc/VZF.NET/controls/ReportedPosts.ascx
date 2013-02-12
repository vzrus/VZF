
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="VZF.Controls.ReportedPosts" Codebehind="ReportedPosts.ascx.cs" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="System.Data" %>
<asp:Repeater ID="ReportedPostsRepeater" runat="server" Visible="false">
    <HeaderTemplate>
        <table width="100%" class="content" style="width:100%">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="header2" colspan="2">
                <span class="YafReported_Complainer">
                    <VZF:LocalizedLabel ID="ReportedByLabel" runat="server" LocalizedTag="REPORTEDBY">
                    </VZF:LocalizedLabel>
                    <VZF:UserLink ID="UserLink1" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>'>
                    </VZF:UserLink>
		            <VZF:ThemeButton ID="PM" runat="server" CssClass="yaflittlebutton"            
				    TextLocalizedTag="PM"  ImageThemeTag="PM" 
				    NavigateUrl='<%# YafBuildLink.GetLinkNotEscaped(ForumPages.pmessage, "u={0}", Container.DataItemToField<int>("UserID"))%>' />
                </span>
            </td>
        </tr>
        <tr>
            <td>
                <span class="YafReported_DateTime">
                    <asp:Label runat="server" ID="ReportedDateTime"></asp:Label></span>
            </td>
            <td class="post">
                <asp:Literal ID="MessageBody" runat="server"></asp:Literal>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
