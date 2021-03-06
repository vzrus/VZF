﻿<%@ Control Language="c#" AutoEventWireup="True" Inherits="VZF.Controls.DisplayPost" EnableViewState="false" Codebehind="DisplayPost.ascx.cs" %>
<%@ Register TagPrefix="VZF" TagName="DisplayPostFooter" Src="DisplayPostFooter.ascx" %>
<%@ Import Namespace="YAF.Core"%>
<%@ Import Namespace="YAF.Core.Services" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Classes" %>
<tr class="postheader">		
    <%#GetIndentCell()%>
    <td width="140" id="NameCell" class="postUser" runat="server">
        <a id="post<%# DataRow["MessageID"] %>" /><strong>
            <VZF:OnlineStatusImage id="OnlineStatusImage" runat="server" Visible='<%# this.Get<YafBoardSettings>().ShowUserOnlineStatus && !UserMembershipHelper.IsGuestUser( DataRow["UserID"] )%>' Style="vertical-align: bottom" UserID='<%# DataRow["UserID"].ToType<int>() %>'  />
            <VZF:ThemeImage ID="ThemeImgSuspended" ThemePage="ICONS" ThemeTag="USER_SUSPENDED"  UseTitleForEmptyAlt="True" Enabled='<%# DataRow["Suspended"] != DBNull.Value && DataRow["Suspended"].ToType<DateTime>() > DateTime.UtcNow %>' runat="server"></VZF:ThemeImage>
            <VZF:UserLink  ID="UserProfileLink" runat="server" UserID='<%# DataRow["UserID"].ToType<int>() %>' Style='<%# DataRow["Style"]%>' CssClass="UserPopMenuLink" EnableHoverCard="False" />
        </strong>
        &nbsp;<VZF:ThemeButton ID="AddReputation" CssClass='<%# "AddReputation_" + DataRow["UserID"]%>' runat="server" ImageThemeTag="VOTE_UP" Visible="false" TitleLocalizedTag="VOTE_UP_TITLE" OnClick="AddUserReputation"></VZF:ThemeButton>
        <VZF:ThemeButton ID="RemoveReputation" CssClass='<%# "RemoveReputation_" + DataRow["UserID"]%>' runat="server" ImageThemeTag="VOTE_DOWN" Visible="false" TitleLocalizedTag="VOTE_DOWN_TITLE" OnClick="RemoveUserReputation"></VZF:ThemeButton>
    </td>
    <td width="80%" class="postPosted" colspan='<%#GetIndentSpan()%>'>
        <div class="leftItem postedLeft">        
            <strong><a href='<%# YafBuildLink.GetLink(ForumPages.posts,"m={0}#post{0}",DataRow["MessageID"]) %>'>
                #<%# (CurrentPage * this.PageContext.PostsPerPage) + PostCount + 1%></a>
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="POSTED" />
                :</strong>
            <VZF:DisplayDateTime id="DisplayDateTime" runat="server" DateTime='<%# DataRow["Posted"] %>'/>
            </div>
        <div class="rightItem postedRight">
            <VZF:ThemeButton ID="Retweet" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_RETWEET"
                TitleLocalizedTag="BUTTON_RETWEET_TT" OnClick="Retweet_Click" />
            <span id="<%# "dvThankBox" + DataRow["MessageID"] %>">
                <VZF:ThemeButton ID="Thank" runat="server" CssClass="yaflittlebutton" Visible="false" TextLocalizedTag="BUTTON_THANKS"
                    TitleLocalizedTag="BUTTON_THANKS_TT" />
            </span>        
            <VZF:ThemeButton ID="Attach" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_ATTACH"
                TitleLocalizedTag="BUTTON_ATTACH_TT" />
            <VZF:ThemeButton ID="Edit" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_EDIT"
                TitleLocalizedTag="BUTTON_EDIT_TT" />
            <VZF:ThemeButton ID="MovePost" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_MOVE"
                TitleLocalizedTag="BUTTON_MOVE_TT" />
            <VZF:ThemeButton ID="Delete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_DELETE"
                TitleLocalizedTag="BUTTON_DELETE_TT" />
            <VZF:ThemeButton ID="UnDelete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_UNDELETE"
                TitleLocalizedTag="BUTTON_UNDELETE_TT" />
            <VZF:ThemeButton ID="Quote" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_QUOTE"
                TitleLocalizedTag="BUTTON_QUOTE_TT" />
                <asp:CheckBox runat="server" ID="MultiQuote" CssClass="MultiQuoteButton"  />
        </div>
                
    </td>
</tr>
<tr class="<%#GetPostClass()%>">
    <td <%# GetRowSpan() %> valign="top" height="<%# GetUserBoxHeight() %>" class="UserBox" colspan='<%# GetIndentSpan()%>'>
        <VZF:UserBox id="UserBox1" runat="server" Visible='<%# !PostData.IsSponserMessage %>' PageCache="<%# PageContext.CurrentForumPage.PageCache %>" DataRow='<%# DataRow %>'></VZF:UserBox>
  
    </td>
    <td valign="top" class="message">
        <div class="postdiv">
            <asp:panel id="panMessage" runat="server">
                <div id="mesDescr" class="messageHeader" visible='<%# PageContext.BoardSettings.AllowMessageDescription && DataRow["MessageDescription"].ToString().IsSet() %>' runat="server"><%# DataRow["MessageDescription"] %><br /><hr /></div>     
                <VZF:MessagePostData ID="MessagePost1" runat="server" DataRow="<%# DataRow %>" IsAltMessage="<%# this.IsAlt %>" ColSpan="<%#GetIndentSpan()%>" ShowDeleted='<%# PageContext.IsAdmin || PageContext.IsForumModerator %>'  ShowEditMessage="True"></VZF:MessagePostData>
            </asp:panel> 
        </div>
    </td>
</tr>
<tr class="postfooter">
    <td class="small postTop" colspan='<%#GetIndentSpan()%>'>
        <a onclick="ScrollToTop();" class="postTopLink" href="javascript: void(0)">            
            <VZF:ThemeImage LocalizedTitlePage="POSTS" LocalizedTitleTag="TOP"  runat="server" ThemeTag="TOTOPPOST" />
        </a>
     <span id="IPSpan1" class="rightItem postInfoRight" runat="server" visible="false"> 
		&nbsp;&nbsp;
		<b><%# this.GetText("IP") %>:</b>&nbsp;<a id="IPLink1" target="_blank" runat="server"/>			   
	</span> 		
    </td>
		<td class="postfooter postInfoBottom">
			<VZF:DisplayPostFooter id="PostFooter" runat="server" DataRow="<%# DataRow %>"></VZF:DisplayPostFooter>
		</td>
</tr>
<tr class="<%#GetPostClass()%> postThanksRow">
    <td style="padding: 5px;" colspan="2" valign="top">
        <div id="<%# "dvThanksInfo" + DataRow["MessageID"] %>" class="ThanksInfo">
            <asp:Literal runat="server"  Visible="false" ID="ThanksDataLiteral"></asp:Literal></div>
    </td>
    <td class="message" style="padding: 5px;" valign="top">
        <div id="<%# "dvThanks" + DataRow["MessageID"] %>" class="ThanksList">
            <asp:Literal runat="server" Visible="false" ID="thanksDataExtendedLiteral"></asp:Literal>
        </div>
    </td>
</tr>
<tr class="postsep">
    <td colspan="3">
        <VZF:PopMenu runat="server" ID="PopMenu1" Control="UserName" />     
    </td>
    
</tr>
