<%@ Control Language="c#" AutoEventWireup="True"
	Inherits="YAF.Pages.personalgroup_users" Codebehind="personalgroup_users.ascx.cs" %>
<%@ Register TagPrefix="VZF" Namespace="VZF.Controls" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Classes" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<table cellspacing="0" cellpadding="0" class="content" width="100%">
    <thead>
        <tr>
            <td class="header1" colspan="4">
                <VZF:LocalizedLabel ID="SearchMembersLocalizedLabel" runat="server" LocalizedTag="Search_Members" />
            </td>
        </tr>
        <tr class="header2">
            <th>
                <VZF:LocalizedLabel ID="SearchRolesLocalizedLabel" runat="server" LocalizedTag="Search_Role" />
            </th>
            <th>
                <VZF:LocalizedLabel ID="SearchRankLocalizedLabel" runat="server" LocalizedTag="Search_Rank" />
            </th>
            <th>
                <VZF:LocalizedLabel ID="SearchMemberLocalizedLabel" runat="server" LocalizedTag="Search_Member" />
            </th>
             <th>
               &nbsp;
            </th>
        </tr>
    </thead>
    <tbody>
        <tr class="post">
            <td>
                <asp:DropDownList ID="Roles" runat="server" Width="95%" />
            </td>
            <td>
                <asp:DropDownList ID="Ranks" runat="server" Width="95%" />
            </td>
            <td colspan="2">
                <asp:TextBox ID="UserSearchName" runat="server" Width="95%"/>
            </td>
        </tr>
        <tr class="post">
            <td colspan="4">
                <VZF:LocalizedLabel ID="NumPostsLabel" runat="server" LocalizedTag="NUMPOSTS" />&nbsp;
                <asp:DropDownList ID="NumPostDDL" runat="server" Width="200px"/>&nbsp;
                <asp:TextBox ID="NumPostsTB" runat="server" Width="70px"/>
            </td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td class="footer1" colspan="4" >
                <VZF:ThemeButton ID="SearchByUserName" OnClick="Search_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="SEARCH" TextLocalizedTag="BTNSEARCH" TitleLocalizedPage="SEARCH" TitleLocalizedTag="BTNSEARCH" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="ResetUserSearch" OnClick="Reset_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="SEARCH" TextLocalizedTag="CLEAR" TitleLocalizedPage="SEARCH" TitleLocalizedTag="CLEAR" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<br />
<VZF:AlphaSort ID="AlphaSort1" runat="server" />
<VZF:Pager runat="server" ID="Pager" OnPageChange="Pager_PageChange" />
<table class="content" width="100%" cellspacing="1" cellpadding="0">
    <thead>
        <tr>
            <td class="header1" colspan="6">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="title" />
            </td>
        </tr>
        <tr>
            <td class="header2">
                <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="Avatar" />
            </td>
            <td class="header2">
                <img runat="server" id="SortUserName" alt="Sort User Name" style="vertical-align: middle" />
                <asp:LinkButton runat="server" ID="UserName" OnClick="UserName_Click" />
            </td>
            <td class="header2">
                <img runat="server" id="SortRank" alt="Sort Rank" style="vertical-align: middle" />
                <asp:LinkButton runat="server" ID="Rank" Enabled="false" OnClick="Rank_Click" />
            </td>
            <td class="header2">
                <img runat="server" id="SortJoined" alt="Sort Joined" style="vertical-align: middle" />
                <asp:LinkButton runat="server" ID="Joined" OnClick="Joined_Click" />
            </td>
            <td class="header2" style="text-align:center">
                <img runat="server" id="SortPosts" alt="Sort Posts" style="vertical-align: middle" Visible="False" />
                <asp:LinkButton runat="server" ID="Posts"  Enabled="false"  OnClick="Posts_Click" />
            </td>
            <td class="header2">&nbsp;
            </td>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="MemberList"  OnItemCommand="MemberList_ItemCommand" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="post">
                        <img src="<%# this.GetAvatarUrlFileName(this.Eval("UserID").ToType<int>(), Eval("Avatar").ToString(),Eval("AvatarImage").ToString().IsSet(), Eval("Email").ToString()) %>" alt="<%# this.HtmlEncode(DataBinder.Eval(Container.DataItem,"Name").ToString()) %>"
                        title="<%# this.HtmlEncode(this.Get<YafBoardSettings>().EnableDisplayName ? this.Eval("DisplayName").ToString() : this.Eval("Name").ToString()) %>" class="avatarimage" />
                    </td>
                    <td class="post">
                        <VZF:UserLink ID="UserProfileLink" runat="server" IsGuest="False" ReplaceName='<%# this.Get<YafBoardSettings>().EnableDisplayName ? this.Eval("DisplayName").ToString() : this.Eval("Name").ToString() %>'  UserID='<%# this.Eval("UserID").ToType<int>() %>'
                        Style='<%# Eval("Style") %>' />
                    </td>
                    <td class="post">
                        <%# Eval("RankName") %>
                    </td>
                    <td class="post">
                        <%# this.Get<IDateTime>().FormatDateLong((DateTime)((System.Data.DataRowView)Container.DataItem)["Joined"]) %>
                    </td>
                    <td class="post" style="text-align:center">
                        <%# "{0:N0}".FormatWith(((System.Data.DataRowView)Container.DataItem)["NumPosts"]) %>
                    </td>
                    <td class="post">
                         <VZF:ThemeButton ID="DeleteUserFromGroupBtn" OnLoad="Delete_Load" CssClass="yaflittlebutton" TitleLocalizedPage="COMMON" TitleLocalizedTag="DELETE"   ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" CommandName="delete"
                        CommandArgument='<%# Eval("UserID") %>'  runat="server"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="6" align="center">
                <VZF:ThemeButton ID="AddUserToGroupBtn" OnClick="AddUserToGroupBtn_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="PERSONALGROUP_USERS" TextLocalizedTag="ADD" TitleLocalizedPage="PERSONALGROUP_USERS" TitleLocalizedTag="ADD" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="Cancel" OnClick="Cancel_Click" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="PERSONALGROUP_USERS" TextLocalizedTag="CANCEL" TitleLocalizedPage="PERSONALGROUP_USERS" TitleLocalizedTag="CANCEL" runat="server"/>
            </td>
        </tr>
    </tfoot>
</table>
<VZF:Pager ID="Pager1" runat="server" LinkedPager="Pager" OnPageChange="Pager_PageChange" />
<div id="DivSmartScroller">
    <VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
