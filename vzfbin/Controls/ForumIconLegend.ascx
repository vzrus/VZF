<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false"
	Inherits="VZF.Controls.ForumIconLegend" Codebehind="ForumIconLegend.ascx.cs" %>
<div class="forumIconLegend">
	<ul>
	    <li>
	        <VZF:ThemeImage ID="ForumNewImage" runat="server" LocalizedTitlePage="ICONLEGEND"
				LocalizedTitleTag="New_Posts" ThemeTag="FORUM_NEW" />&nbsp;
		    <span><VZF:LocalizedLabel ID="NewPostsLabel" runat="server" LocalizedPage="ICONLEGEND"
				LocalizedTag="New_Posts" /></span>
        </li>
		<li>
		    <VZF:ThemeImage ID="ForumRegularImage" runat="server"
				ThemeTag="FORUM" LocalizedTitlePage="ICONLEGEND" LocalizedTitleTag="No_New_Posts" />&nbsp;
		    <span><VZF:LocalizedLabel ID="NoNewPostsLabel" runat="server" LocalizedPage="ICONLEGEND"
				LocalizedTag="No_New_Posts" /></span>
		</li>
		<li>
		    <VZF:ThemeImage ID="ForumLockedImage" runat="server"
				ThemeTag="FORUM_LOCKED" LocalizedTitlePage="ICONLEGEND" LocalizedTitleTag="Forum_Locked" />&nbsp;
		    <span><VZF:LocalizedLabel ID="ForumLockedLabel" runat="server" LocalizedPage="ICONLEGEND"
				LocalizedTag="Forum_Locked" /></span>
	    </li>
	</ul>
</div>

