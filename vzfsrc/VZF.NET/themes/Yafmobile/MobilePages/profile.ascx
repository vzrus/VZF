<%@ Control Language="c#" CodeBehind="../../../pages/profile.ascx.cs" AutoEventWireup="True"
    Inherits="YAF.Pages.profile" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Register TagPrefix="YAF" TagName="SignatureEdit" Src="../../../controls/EditUsersSignature.ascx" %>
<%@ Register TagPrefix="YAF" TagName="SuspendUser" Src="../../../controls/EditUsersSuspend.ascx" %>
<%@ Register TagPrefix="YAF" TagName="ForumAccess" Src="../../../controls/ForumProfileAccess.ascx" %>
<%@ Register TagPrefix="YAF" TagName="BuddyList" Src="../../../controls/BuddyList.ascx" %>
<%@ Register TagPrefix="YAF" TagName="AlbumList" Src="../../../controls/AlbumList.ascx" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table class="content" width="100%" cellspacing="1" cellpadding="0">
	<tr>
		<td class="header1" colspan="2">
			<VZF:LocalizedLabel runat="server" LocalizedTag="profile" />
			<VZF:UserLabel ID="UserLabel1" runat="server" />
		</td>
	</tr>
	<tr class="post">
		<td colspan="2">
			<VZF:ThemeButton ID="PM" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="PM" />
			<VZF:ThemeButton ID="Email" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="EMAIL" />
			<VZF:ThemeButton ID="Home" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="HOME" />
			<VZF:ThemeButton ID="Blog" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="BLOG" />
			<VZF:ThemeButton ID="MSN" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="MSN" />
			<VZF:ThemeButton ID="AIM" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="AIM" />
			<VZF:ThemeButton ID="YIM" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="YIM" />
			<VZF:ThemeButton ID="ICQ" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="ICQ" />
			<VZF:ThemeButton ID="XMPP" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="XMPP" />	
			<VZF:ThemeButton ID="Skype" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="SKYPE" />
            <VZF:ThemeButton ID="Facebook" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="FACEBOOK" TitleLocalizedTag="FACEBOOK_TITLE" TitleLocalizedPage="POSTS" />
            <VZF:ThemeButton ID="Twitter" runat="server" CssClass="yafcssimagebutton" Visible="false" TextLocalizedPage="POSTS"
				TextLocalizedTag="TWITTER" TitleLocalizedTag="TWITTER_TITLE" TitleLocalizedPage="POSTS" />
			<VZF:ThemeButton ID="AdminUserButton" runat="server" CssClass="yaflittlebutton" Visible="false"
				TextLocalizedTag="ADMIN_USER" NavigateUrl='<%# YafBuildLink.GetLinkNotEscaped( ForumPages.admin_edituser,"u={0}", this.UserId ) %>'>
			</VZF:ThemeButton>
		</td>
	</tr>
	<tr class="post">
		<td valign="top" rowspan="2">
			<asp:Panel id="ProfileTabs" runat="server">
               <ul>
                 <li><a href="#AboutTab"><VZF:LocalizedLabel ID="LocalizedLabel40" runat="server" LocalizedTag="ABOUT" /></a></li>
		 <li><a href="#StatisticsTab"><VZF:LocalizedLabel ID="LocalizedLabel41" runat="server" LocalizedTag="STATISTICS" /></a></li>
		 <li runat="server" id="AvatarLi"><a href='#<%# this.AvatarTab.ClientID %>'><VZF:LocalizedLabel ID="LocalizedLabel42" runat="server" LocalizedTag="AVATAR" /></a></li>
		 <li><a href="#Last10PostsTab"><VZF:LocalizedLabel ID="LocalizedLabel43" runat="server" LocalizedTag="LAST10" /></a></li>
		 <li><a href="#BuddyListTab"><VZF:LocalizedLabel ID="LocalizedLabel44" runat="server" LocalizedTag='<%# this.UserId == this.PageContext.PageUserID ? "BUDDIES" : "BUDDIESTITLE"%>' /></a></li>		        
		 <li runat="server" id="AlbumListLi"><a href='#<%# this.AlbumListTab.ClientID %>'><VZF:LocalizedLabel ID="LocalizedLabel45" runat="server" LocalizedTag="ALBUMS" /></a></li>	
		 <li runat="server" id="ModerateLi"><a href='#<%# this.ModerateTab.ClientID %>'><VZF:LocalizedLabel ID="LocalizedLabel46" runat="server" LocalizedTag="MODERATION" /></a></li>	
               </ul>
                <div id="AboutTab">
                   <table width="100%" cellspacing="1" cellpadding="0">
							<tr>
								<td width="50%" class="postheader">
									<strong>
										<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="username" />
									</strong>
								</td>
								<td width="50%" class="post">
									<asp:Label ID="Name" runat="server" />
									<VZF:OnlineStatusImage id="OnlineStatusImage1" runat="server" Style="vertical-align: bottom" />
                            <asp:LinkButton ID="lnkBuddy" runat="server" OnCommand="lnk_AddBuddy"/>
                                <asp:literal ID="ltrApproval" runat="server" Text='<%# this.GetText("BUDDY","AWAIT_BUDDY_APPROVAL") %>'
                                Visible="false">
                                </asp:literal>
								</td>
							</tr>
							<tr runat="server" id="userGroupsRow" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="groups" />
								</td>
								<td class="post">
									<asp:Repeater ID="Groups" runat="server">
										<ItemTemplate>
											<%# Container.DataItem %>
										</ItemTemplate>
										<SeparatorTemplate>
											,
										</SeparatorTemplate>
									</asp:Repeater>
								</td>
							</tr>
							<tr runat="server" id="RankTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="rank" />
								</td>
								<td class="post">
									<asp:Label ID="Rank" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="RealNameTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="realname" />
								</td>
								<td class="post" runat="server" id="RealName" />
							</tr>
                            <tr runat="server" id="CountryTR" visible="false">
								<td class="postheader">
								<VZF:LocalizedLabel ID="LocalizedLabel27" runat="server" LocalizedTag="country" />
								</td>
								<td class="post">
									<img id="CountryFlagImage" runat="server" alt=""
                                    title=""
                                    src="" />&nbsp;<asp:Label ID="CountryLabel" runat="server" />
								</td>
							</tr>
                            <tr runat="server" id="RegionTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel28" runat="server" LocalizedTag="region" />
								</td>
								<td class="post">
									<asp:Label ID="RegionLabel" runat="server" />
								</td>
							</tr>
                            <tr runat="server" id="CityTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel26" runat="server" LocalizedTag="city" />
								</td>
								<td class="post">
									<asp:Label ID="CityLabel" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="LocationTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="location" />
								</td>
								<td class="post">
									<asp:Label ID="Location" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="BirthdayTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel23" runat="server" LocalizedTag="BIRTHDAY" />
								</td>
								<td class="post">
									<asp:Label ID="Birthday" runat="server" />
								</td>
							</tr>
							<tr  runat="server" id="OccupationTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="occupation" />
								</td>
								<td class="post" runat="server" id="Occupation" />
							</tr>
							<tr  runat="server" id="InterestsTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="interests" />
								</td>
								<td class="post" runat="server" id="Interests" />
							</tr>
							<tr runat="server" id="GenderTR">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="gender" />
								</td>
								<td class="post" runat="server" id="Gender" />
							</tr>
							<tr runat="server" id="MsnTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="MSN" />
								</td>
								<td class="post">
									<asp:Label ID="lblmsn" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="AimTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="AIM" />
								</td>
								<td class="post">
									<asp:Label ID="lblaim" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="YimTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel15" runat="server" LocalizedTag="YIM" />
								</td>
								<td class="post">
									<asp:Label ID="lblyim" runat="server" />
								</td>
							</tr>
							<tr  runat="server" id="IcqTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel18" runat="server" LocalizedTag="ICQ" />
								</td>
								<td class="post">
									<asp:Label ID="lblicq" runat="server" />
								</td>
							</tr>
							<tr  runat="server" id="XmppTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel22" runat="server" LocalizedTag="XMPP" />
								</td>
								<td class="post">
									<asp:Label ID="lblxmpp" runat="server" />
								</td>
							</tr>
							<tr runat="server" id="SkypeTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel19" runat="server" LocalizedTag="SKYPE" />
								</td>
								<td class="post">
									<asp:Label ID="lblskype" runat="server" />
								</td>
							</tr>
                            <tr  runat="server" id="FacebookTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel24" runat="server" LocalizedTag="Facebook" />
								</td>
								<td class="post">
									<asp:Label ID="lblfacebook" runat="server" />
								</td>
							</tr>
                            <tr  runat="server" id="TwitterTR" visible="false">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel25" runat="server" LocalizedTag="Twitter" />
								</td>
								<td class="post">
									<asp:Label ID="lbltwitter" runat="server" />
								</td>
							</tr>
						</table>
                </div>
                <div id="StatisticsTab">
                  <table width="100%" cellspacing="1" cellpadding="0">
							<tr>
								<td width="50%" class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="joined" />
								</td>
								<td width="50%" class="post">
									<asp:Label ID="Joined" runat="server" />
								</td>
							</tr>
							<tr>
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="lastvisit" />
								</td>
								<td class="post">
									<asp:Label ID="LastVisit" runat="server" Visible="false" />
                                    <VZF:DisplayDateTime id="LastVisitDateTime" runat="server" Visible="false"></VZF:DisplayDateTime>
								</td>
							</tr>
							<tr>
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="numposts" />
								</td>
								<td class="post" runat="server" id="Stats" />
							</tr>
							<tr id="divTF" runat="server" visible="<%# PageContext.BoardSettings.EnableThanksMod %>">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="THANKSFROM" />
								</td>
								<td class="post">
									<asp:Label ID="ThanksFrom" runat="server" />
                                    <asp:LinkButton ID="lnkThanks" runat="server" OnCommand="lnk_ViewThanks"/>
								</td>
							</tr>
							<tr id="divTTT" runat="server" visible="<%# PageContext.BoardSettings.EnableThanksMod %>">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel20" runat="server" LocalizedTag="THANKSTOTIMES" />
								</td>
								<td class="post">
									<asp:Label ID="ThanksToTimes" runat="server" />
								</td>
							</tr>
							<tr id="divTTP" runat="server" visible="<%# PageContext.BoardSettings.EnableThanksMod %>">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel21" runat="server" LocalizedTag="THANKSTOPOSTS" />
								</td>
								<td class="post">
									<asp:Label ID="ThanksToPosts" runat="server" />
								</td>
							</tr>
                            <tr id="divRR" runat="server" visible="<%# this.Get<YafBoardSettings>().EnableUserReputation %>">
								<td class="postheader">
									<VZF:LocalizedLabel ID="LocalizedLabel29" runat="server" LocalizedTag="REPUTATION_RECEIVED" />
								</td>
								<td class="post">
									<asp:Label ID="ReputationReceived" runat="server" />
								</td>
							</tr>
						</table>
                </div>
                <div runat="server" id="AvatarTab">
                  <table align="center" width="100%" cellspacing="1" cellpadding="0">
							<tr>
								<td class="post" colspan="2" align="center">
									<asp:Image ID="Avatar" runat="server" CssClass="avatarimage" />
								</td>
							</tr>
						</table>
                </div>
                <div id="Last10PostsTab">
                  <VZF:ThemeButton ID="SearchUser" runat="server" CssClass="yafcssimagebutton" TextLocalizedPage="POSTS"
							TextLocalizedTag="SEARCHUSER" ImageThemeTag="SEARCH" />
						<br style="clear: both" />
						<table width="100%" cellspacing="1" cellpadding="0">
							<asp:Repeater ID="LastPosts" runat="server">
								<ItemTemplate>
									<tr class="postheader">
										<td class="small" align="left" colspan="2">
											<strong>
												<VZF:LocalizedLabel ID="LocalizedLabel16" runat="server" LocalizedTag="topic" />
											</strong><a href='<%# YafBuildLink.GetLink(ForumPages.posts,"t={0}",Container.DataItemToField<int>("TopicID")) %>'>
												<%# this.Get<IBadWordReplace>().Replace(HtmlEncode(Container.DataItemToField<string>("Subject"))) %>
											</a>
											<br />
											<strong>
												<VZF:LocalizedLabel ID="LocalizedLabel17" runat="server" LocalizedTag="posted" />
											</strong>
											<%# this.Get<IDateTime>().FormatDateTime(Container.DataItemToField<DateTime>("Posted"))%>
										</td>
									</tr>
									<tr class="post">
										<td valign="top" class="message" colspan="2">
											<VZF:MessagePostData ID="MessagePost" runat="server" ShowAttachments="false" DataRow="<%# Container.DataItem %>">
											</VZF:MessagePostData>
										</td>
									</tr>
								</ItemTemplate>
							</asp:Repeater>
						</table>
                </div>
                <div id="BuddyListTab">
                  <VZF:BuddyList runat="server" ID="BuddyList" />
                </div>
                <div runat="server" id="AlbumListTab">
                  <VZF:AlbumList runat="server" ID="AlbumList1" Mode="1"/>
                </div>
                <div runat="server" id="ModerateTab">
                  <VZF:ForumAccess runat="server" ID="ForumAccessControl" />
						<table width="100%" cellspacing="1" cellpadding="0">
							<tr class="header2">
								<td class="header2" colspan="2">
									User Moderation
								</td>
							</tr>
						</table>
						<VZF:SuspendUser runat="server" ID="SuspendUserControl" ShowHeader="False" />
						<VZF:SignatureEdit runat="server" ID="SignatureEditControl" ShowHeader="False" />
                </div>
             </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLastTab" Value="0" />
		</td>
	</tr>
</table>
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>