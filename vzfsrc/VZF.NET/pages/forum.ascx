<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.forum" Codebehind="forum.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="ForumWelcome" Src="../controls/ForumWelcome.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumIconLegend" Src="../controls/ForumIconLegend.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumStatistics" Src="../controls/ForumStatistics.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumActiveDiscussion" Src="../controls/ForumActiveDiscussion.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumCategoryList" Src="../controls/ForumCategoryList.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ForumCategoryListNew" Src="../controls/ForumCategoryListNew.ascx" %>
<%@ Register TagPrefix="VZF" TagName="ShoutBox" Src="../controls/ShoutBox.ascx" %>
<%@ Register TagPrefix="VZF" TagName="PollList" Src="../controls/PollList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:ForumWelcome runat="server" ID="Welcome" />
<div class="DivTopSeparator">
</div>
<VZF:ShoutBox ID="ShoutBox1" runat="server" />
<VZF:PollList ID="PollList" runat="server"/>
<VZF:ForumCategoryListNew ID="ForumCategoryListNew" runat="server"/>
<VZF:ForumCategoryList ID="ForumCategoryList" runat="server"/>
<br />
<VZF:ForumActiveDiscussion ID="ActiveDiscussions" runat="server" />
<br />
<VZF:ForumStatistics ID="ForumStats" runat="Server" />
<VZF:ForumIconLegend ID="IconLegend" runat="server" />
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>

