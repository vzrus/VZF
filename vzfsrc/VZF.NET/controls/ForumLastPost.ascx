<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="VZF.Controls.ForumLastPost" CodeBehind="ForumLastPost.ascx.cs" %>
<asp:PlaceHolder ID="LastPostedHolder" runat="server">
    <asp:PlaceHolder ID="TopicInPlaceHolder" runat="server">
         <img id="userAvatar" src="" alt="" title="" visible="False"
          runat="server"  align="left" class="albumimage" /> 
        <asp:HyperLink ID="topicLink" CssClass="forumTopicLink" runat="server"></asp:HyperLink>
    </asp:PlaceHolder>
    &nbsp;<asp:HyperLink ID="LastTopicImgLink" runat="server">
        <VZF:ThemeImage ID="Icon" runat="server" />
    </asp:HyperLink>
    <asp:HyperLink ID="ImageLastUnreadMessageLink" runat="server">
            <VZF:ThemeImage ID="LastUnreadImage" runat="server" Style="border: 0" />
    </asp:HyperLink>
    <br />
    <VZF:LocalizedLabel ID="ByLabel" runat="server" LocalizedTag="BY" LocalizedPage="TOPICS" />
    <VZF:UserLink ID="ProfileUserLink" runat="server" />
    <br />
    <VZF:DisplayDateTime ID="LastPostDate" runat="server" Format="BothTopic" />
</asp:PlaceHolder>
<VZF:LocalizedLabel ID="NoPostsLabel" runat="server" LocalizedTag="NO_POSTS" />
