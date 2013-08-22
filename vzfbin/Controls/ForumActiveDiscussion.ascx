<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="VZF.Controls.ForumActiveDiscussion"
    CodeBehind="ForumActiveDiscussion.ascx.cs" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<asp:UpdatePanel ID="UpdateStatsPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table border="0" class="content activeDiscussionContent" cellspacing="1" cellpadding="0" width="100%">
            <tr>
                <td class="header1" colspan="2">
                    <VZF:CollapsibleImage ID="CollapsibleImage" runat="server" BorderWidth="0" Style="vertical-align: middle"
                        PanelID='ActiveDiscussions' AttachedControlID="ActiveDiscussionPlaceHolder" />&nbsp;&nbsp;<VZF:LocalizedLabel
                            ID="ActiveDiscussionHeader" runat="server" LocalizedTag="ACTIVE_DISCUSSIONS" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="ActiveDiscussionPlaceHolder">
                <tr>
                    <td class="header2" colspan="2">
                        <VZF:LocalizedLabel ID="LatestPostsHeader" runat="server" LocalizedTag="LATEST_POSTS" />
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="LatestPosts" OnItemDataBound="LatestPosts_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="post" style="padding-left:10px">
                                <asp:Image ID="NewPostIcon" runat="server" style="border: 0;width:16px;height:16px" />
                                &nbsp;<strong><asp:HyperLink ID="TextMessageLink" runat="server" /></strong>
                                &nbsp;<VZF:LocalizedLabel ID="ByLabel" runat="server" LocalizedTag="BY" LocalizedPage="TOPICS" />
                                &nbsp;<VZF:UserLink ID="LastUserLink"  runat="server" />&nbsp;(<asp:HyperLink ID="ForumLink" runat="server" />)
                            </td>
                            <td class="post" style="width: 30em; text-align: right;">                            
                                <VZF:DisplayDateTime ID="LastPostDate" runat="server" Format="BothTopic" />
                                <asp:HyperLink ID="ImageMessageLink" runat="server">
                                    <VZF:ThemeImage ID="LastPostedImage" runat="server" Style="border: 0" />
                                </asp:HyperLink>
                                <asp:HyperLink ID="ImageLastUnreadMessageLink" runat="server">
                                 <VZF:ThemeImage ID="LastUnreadImage" runat="server"  Style="border: 0" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td class="footer1" align="right" colspan="2">
                        <VZF:RssFeedLink ID="RssFeed" runat="server" FeedType="LatestPosts"  TitleLocalizedTag="RSSICONTOOLTIPACTIVE" />&nbsp; 
                        <VZF:RssFeedLink ID="AtomFeed" runat="server" FeedType="LatestPosts" IsAtomFeed="true" ImageThemeTag="ATOMFEED" TitleLocalizedTag="ATOMICONTOOLTIPACTIVE" />                           
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
