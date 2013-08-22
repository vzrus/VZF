<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="VZF.Controls.DisplayPostFooter" Codebehind="DisplayPostFooter.ascx.cs" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<div class="displayPostFooter">
	<div class="leftItem postInfoLeft">
		<VZF:ThemeButton ID="btnTogglePost" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="TOGGLEPOST" Visible="false" />
        <VZF:ThemeButton ID="Albums" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="ALBUM"
			TextLocalizedTag="ALBUMS" ImageThemeTag="ALBUMS" TitleLocalizedTag="ALBUMS_HEADER_TEXT" Visible="false" />
		<VZF:ThemeButton ID="Pm" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="PM" ImageThemeTag="PM" TitleLocalizedTag="PM_TITLE" />
		<VZF:ThemeButton ID="Email" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="EMAIL" ImageThemeTag="EMAIL" TitleLocalizedTag="EMAIL_TITLE" />
		<VZF:ThemeButton ID="Home" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="HOME" ImageThemeTag="HOME" TitleLocalizedTag="HOME_TITLE" />
		<VZF:ThemeButton ID="Blog" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="BLOG" ImageThemeTag="BLOG" TitleLocalizedTag="BLOG_TITLE" />
		<VZF:ThemeButton ID="Msn" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="MSN" ImageThemeTag="MSN" Visible="false" TitleLocalizedTag="MSN_TITLE" />
		<VZF:ThemeButton ID="Aim" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="AIM" ImageThemeTag="AIM" Visible="false" TitleLocalizedTag="AIM_TITLE" />
		<VZF:ThemeButton ID="Yim" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="YIM" ImageThemeTag="YIM" Visible="false" TitleLocalizedTag="YIM_TITLE" />
		<VZF:ThemeButton ID="Icq" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="ICQ" ImageThemeTag="ICQ" Visible="false" TitleLocalizedTag="ICQ_TITLE" />
		<VZF:ThemeButton ID="Xmpp" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="XMPP" ImageThemeTag="XMPP" Visible="false" TitleLocalizedTag="XMPP_TITLE" />	
		<VZF:ThemeButton ID="Skype" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
			TextLocalizedTag="SKYPE" ImageThemeTag="SKYPE" Visible="false" TitleLocalizedTag="SKYPE_TITLE" />
        <VZF:ThemeButton ID="Facebook" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="FACEBOOK" ImageThemeTag="Facebook2" TitleLocalizedTag="FACEBOOK_TITLE" />
        <VZF:ThemeButton ID="Twitter" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="TWITTER" ImageThemeTag="Twitter2" TitleLocalizedTag="TWITTER_TITLE" />
	</div>
	<div class="rightItem postInfoRight">
        <VZF:ThemeButton ID="ReportPost" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="REPORTPOST" ImageThemeTag="REPORT_POST" TitleLocalizedTag="REPORTPOST_TITLE"></VZF:ThemeButton>		
	</div>
</div>
