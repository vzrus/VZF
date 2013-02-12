<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false"
    Inherits="VZF.Controls.ForumSubForumList" Codebehind="ForumSubForumList.ascx.cs" %>
<asp:Repeater ID="SubforumList" runat="server" OnItemCreated="SubforumList_ItemCreated">
    <HeaderTemplate>        
        <div class="subForumList"><span class="subForumTitle"><VZF:LocalizedLabel ID="SubForums" LocalizedTag="SUBFORUMS" runat="server" />:</span>
 </HeaderTemplate>
    <ItemTemplate>
        <VZF:ThemeImage ID="ThemeSubforumIcon" runat="server" /> <%# GetForumLink((System.Data.DataRow)Container.DataItem) %></ItemTemplate>
    <SeparatorTemplate>, </SeparatorTemplate>
    <FooterTemplate>
				</div>        
    </FooterTemplate>
</asp:Repeater>
