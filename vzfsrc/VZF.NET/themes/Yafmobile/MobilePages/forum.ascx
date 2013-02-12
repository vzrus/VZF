<%@ Control Language="c#" CodeBehind="../../../pages/forum.ascx.cs" AutoEventWireup="True" Inherits="YAF.Pages.forum" %>
<%@ Register TagPrefix="YAF" TagName="ForumWelcome" Src="forumwelcome.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ForumIconLegend" Src="../../../controls/ForumIconLegend.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ForumStatistics" Src="../../../controls/ForumStatistics.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ForumActiveDiscussion" Src="forumactivediscussion.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ForumCategoryList" Src="forumcategorylist.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ShoutBox" Src="../../../controls/ShoutBox.ascx" %>
<%@ Register TagPrefix="YAF" TagName="PollList" Src="../../../controls/PollList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:ForumWelcome runat="server" ID="Welcome" />
<div class="DivTopSeparator">
</div>
<VZF:ShoutBox ID="ShoutBox1" Visible='<%# PageContext.BoardSettings.ShowShoutbox %>' runat="server" />
<VZF:PollList ID="PollList" runat="server"/>
<VZF:ForumCategoryList ID="ForumCategoryList" runat="server"></YAF:ForumCategoryList>
<br />
<VZF:ForumActiveDiscussion ID="ActiveDiscussions" runat="server" />
<br />
<VZF:ForumStatistics ID="ForumStats" runat="Server" />
<VZF:ForumIconLegend ID="IconLegend" runat="server" />
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
