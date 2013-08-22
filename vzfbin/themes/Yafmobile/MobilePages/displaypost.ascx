<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="../../../controls/displaypost.ascx.cs"
    Inherits="VZF.Controls.DisplayPost" EnableViewState="false" %>
<%@ Register TagPrefix="VZF" TagName="DisplayPostFooter" Src="../../../controls/DisplayPostFooter.ascx" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<tr class="postheader">		
    <%#GetIndentCell()%>
    <td id="NameCell" class="postUser" runat="server">
            <span id="<%# "dvThankBox" + DataRow["MessageID"] %>">
                <VZF:ThemeButton ID="Thank" runat="server" CssClass="yaflittlebutton" Visible="false" TextLocalizedTag="BUTTON_THANKS"
                    TitleLocalizedTag="BUTTON_THANKS_TT" />
            </span>    
            <asp:PlaceHolder ID="buttonsMobile" runat="server" Visible="false">
            <VZF:ThemeButton ID="Attach" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_ATTACH"
                TitleLocalizedTag="BUTTON_ATTACH_TT" />
            <VZF:ThemeButton ID="MovePost" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_MOVE"
                TitleLocalizedTag="BUTTON_MOVE_TT" />
            <VZF:ThemeButton ID="Delete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_DELETE"
                TitleLocalizedTag="BUTTON_DELETE_TT" />
            <VZF:ThemeButton ID="UnDelete" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_UNDELETE"
                TitleLocalizedTag="BUTTON_UNDELETE_TT" />
            </asp:PlaceHolder>
            <VZF:ThemeButton ID="Retweet" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_RETWEET"
                TitleLocalizedTag="BUTTON_RETWEET_TT" OnClick="Retweet_Click" />
            <VZF:ThemeButton ID="Edit" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_EDIT"
                TitleLocalizedTag="BUTTON_EDIT_TT" />
            <VZF:ThemeButton ID="Quote" runat="server" CssClass="yaflittlebutton" TextLocalizedTag="BUTTON_QUOTE"
                TitleLocalizedTag="BUTTON_QUOTE_TT" />
            <asp:CheckBox runat="server" ID="MultiQuote" CssClass="MultiQuoteButton"  />
        <a name="post<%# DataRow["MessageID"] %>" /><strong>						
            <VZF:OnlineStatusImage id="OnlineStatusImage" runat="server" Visible='<%# PageContext.BoardSettings.ShowUserOnlineStatus && !UserMembershipHelper.IsGuestUser( DataRow["UserID"] )%>' Style="vertical-align: bottom" UserID='<%# DataRow["UserID"].ToType<int>() %>'  />
            <VZF:ThemeImage ID="ThemeImgSuspended" ThemePage="ICONS" ThemeTag="USER_SUSPENDED"  UseTitleForEmptyAlt="True" Enabled='<%# DataRow["Suspended"] != DBNull.Value && DataRow["Suspended"].ToType<DateTime>() > DateTime.UtcNow %>' runat="server"></VZF:ThemeImage>
            <VZF:UserLink  ID="UserProfileLink" runat="server" UserID='<%# DataRow["UserID"].ToType<int>() %>' Style='<%# DataRow["Style"]%>' CssClass="UserPopMenuLink" EnableHoverCard="False" />            
        </strong>
        &nbsp;<VZF:ThemeButton ID="AddReputation" runat="server" ImageThemeTag="VOTE_UP" Visible="false" TitleLocalizedTag="VOTE_UP_TITLE" OnClick="AddUserReputation"></VZF:ThemeButton>
        <VZF:ThemeButton ID="RemoveReputation" runat="server" ImageThemeTag="VOTE_DOWN" Visible="false" TitleLocalizedTag="VOTE_DOWN_TITLE" OnClick="RemoveUserReputation"></VZF:ThemeButton>
        <br />
        <div class="leftItem postedLeft">        
            <strong><VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="POSTED" />
                :</strong>
            <VZF:DisplayDateTime id="DisplayDateTime" runat="server" DateTime='<%# DataRow["Posted"] %>'></VZF:DisplayDateTime>
            </div>
    </td>
</tr>
<tr class="<%#GetPostClass()%>">
    <td valign="top" class="message">
        <div class="postdiv">
            <asp:panel id="panMessage" runat="server">
                  <div id="mesDescr" class="messageHeader" visible='<%# PageContext.BoardSettings.AllowMessageDescription && DataRow["MessageDescription"].ToString().IsSet() %>' runat="server"><%# DataRow["MessageDescription"] %><br /><hr /></div>     
                <VZF:MessagePostData ID="MessagePost1" runat="server" DataRow="<%# DataRow %>" IsAltMessage="<%# this.IsAlt %>" ColSpan="<%#GetIndentSpan()%>" ShowEditMessage="True"></VZF:MessagePostData>
            </asp:panel>
        </div>
    </td>
</tr>
<tr class="postfooter" runat="server" visible="false">
		<td class="postfooter postInfoBottom">
			 <span id="IPSpan1" class="rightItem postInfoRight" runat="server" visible="false"> 
		&nbsp;&nbsp;
		<strong><%# this.GetText("IP") %>:</strong>&nbsp;<a id="IPLink1" target="_blank" runat="server"/>			   
	</span> &nbsp;&nbsp;	<VZF:DisplayPostFooter id="PostFooter" runat="server" DataRow="<%# DataRow %>"></VZF:DisplayPostFooter>
		</td>
</tr>
<tr class="<%#GetPostClass()%>" runat="server">
    <td>
        <div style="font-weight: bold;" id="<%# "dvThanksInfo" + DataRow["MessageID"] %>">
            <asp:Literal runat="server"  Visible="false" ID="ThanksDataLiteral"></asp:Literal></div>
        <div id="<%# "dvThanks" + DataRow["MessageID"] %>">
            <asp:Literal runat="server" Visible="false" ID="thanksDataExtendedLiteral"></asp:Literal>
        </div>
    </td>
</tr>
<tr class="postsep">
    <td colspan="1">
        <VZF:PopMenu runat="server" ID="PopMenu1" Control="UserName" />
    </td>
</tr>
