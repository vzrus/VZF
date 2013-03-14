<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../pages/mytopics.ascx.cs" Inherits="YAF.Pages.mytopics" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<%@ Import Namespace="VZF.Controls" %>
<%@ Register TagPrefix="VZF" TagName="MyTopicsList" Src="mytopicslist.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumJumper" Src="~/controls/ForumJumper.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator">
</div>
        <br style="clear: both" />
        <asp:Panel id="TopicsTabs" runat="server">
               <ul>
                 <li><a href="#ActiveTopicsTab"><VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="ActiveTopics" LocalizedPage="MyTopics" /></a></li>
                  <asp:PlaceHolder ID="UnansweredTopicsTabTitle" runat="server">
                     <li><a href="#UnansweredTopicsTab"><VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="UnansweredTopics" LocalizedPage="MyTopics" /></a></li>
                 </asp:PlaceHolder>
                 <asp:PlaceHolder ID="UnreadTopicsTabTitle" runat="server">
                   <li><a href="#UnreadTopicsTab"><VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="UnreadTopics" LocalizedPage="MyTopics" /></a></li>
                 </asp:PlaceHolder>
                 <asp:PlaceHolder ID="UserTopicsTabTitle" runat="server">
                   <li><a href="#MyTopicsTab"><VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="MyTopics" LocalizedPage="MyTopics" /></a></li>
		           <li><a href="#FavoriteTopicsTab"><VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="FavoriteTopics" LocalizedPage="MyTopics" /></a></li>
                 </asp:PlaceHolder>		        
               </ul>
                <div id="ActiveTopicsTab">
                   <VZF:MyTopicsList runat="server" ID="ActiveTopics" CurrentMode="Active" AutoDatabind="True"/>
                </div>
                <asp:PlaceHolder ID="UnansweredTopicsTabContent" runat="server">
                <div id="UnansweredTopicsTab">
                   <VZF:MyTopicsList runat="server" ID="UnansweredTopics" CurrentMode="Unanswered" AutoDatabind="False"/>
                </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="UnreadTopicsTabContent" runat="server">
                <div id="UnreadTopicsTab">
                  <VZF:MyTopicsList runat="server" ID="UnreadTopics" CurrentMode="Unread" />
                </div>
                </asp:PlaceHolder>
                 <asp:PlaceHolder ID="UserTopicsTabContent" runat="server">
                <div id="MyTopicsTab">
                  <VZF:MyTopicsList runat="server" ID="MyTopics" CurrentMode="User" />
                </div>
                <div id="FavoriteTopicsTab">
                  <VZF:MyTopicsList runat="server" ID="FavoriteTopics" CurrentMode="Favorite" />
                </div>
                </asp:PlaceHolder>
             </asp:Panel>
        <asp:HiddenField runat="server" ID="hidLastTab" Value="0" /><asp:HiddenField runat="server" ID="hidLastTabId" Value="0" />
        <asp:Button id="ChangeTab" OnClick="ChangeTabClick" runat="server" style="display:none" />
<asp:PlaceHolder ID="ForumJumpHolder" runat="server">
    <div id="DivForumJump" runat="server" visible="false">
      <VZF:ForumJumper  ID="fj1" runat="server"></VZF:ForumJumper> </div>
   
</asp:PlaceHolder>
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>


