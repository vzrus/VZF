<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalgroup.ascx.cs" Inherits="YAF.pages.personalgroup" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<%@ Import Namespace="VZF.Utils.Helpers" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top" name="top"></a>
<table class="content" width="100%" cellspacing="1" cellpadding="0">
		<asp:Repeater ID="RoleListYaf" runat="server" OnItemCommand="RoleListYaf_ItemCommand">
			<HeaderTemplate>
				<tr>
					<td class="header1" colspan="2">
						<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALGROUP" />
					</td>
				</tr>
				<tr>
					<td class="header2">
						<VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="NAME" LocalizedPage="ADMIN_GROUPS" />
					</td>	
					<td class="header2">&nbsp;
						
					</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td class="header2">
                    <img alt="" title="" src='<%# this.Get<ITheme>().GetItem("VOTE","VOTE_USERS") %>' />&nbsp;
						<%# Eval( "Name" ) %>
					</td>
                    <td class="header2" align="right">
                        <VZF:ThemeButton ID="moderate1" CssClass="yaflittlebutton" CommandName='users' CommandArgument='<%# Eval( "[\"GroupID\"]") %>' TextLocalizedPage="MEMBERS" TextLocalizedTag="TITLE" TitleLocalizedPage="MEMBERS" TitleLocalizedTag="TITLE" ImageThemePage="VOTE" ImageThemeTag="POLL_VOTED" runat="server" /> 
                        <VZF:ThemeButton ID="btnEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval( "[\"GroupID\"]") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server" />
                        <VZF:ThemeButton ID="LinkButtonDelete" CssClass="yaflittlebutton" OnLoad="DeleteRole_Load" CommandName='delete' CommandArgument='<%# Eval( "[\"GroupID\"]") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" Visible='<%# !BitConversionHelper.IsBitSet(Eval("[\"Flags\"]"),(int)GroupFlags.Flags.IsGuest) %>' runat="server"/>
					</td>
                     </tr>
                    <tr>           
					<td class="post" colspan="2">
                     <VZF:LocalizedLabel ID="HelpLabel6" Visible='<%# Eval("Description").ToString().IsSet() %>' runat="server" LocalizedTag="DESCRIPTION" LocalizedPage="ADMIN_EDITGROUP">
                         </VZF:LocalizedLabel>
                          &nbsp;<%# Eval("Description").ToString() %>&nbsp; 
                    <br />
                        <VZF:LocalizedLabel  ID="LocalizedLabel18" runat="server" LocalizedTag="PRIORITY" LocalizedPage="ADMIN_EDITGROUP" />
                        <span class="wordyes"><asp:Label ID="Label13" runat="server" ><%# this.Eval( "SortOrder" ) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel19" runat="server" LocalizedTag="IS_GUEST" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsGuest) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label14" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsGuest)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="IS_START" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsStart) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label15" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsStart)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="IS_MOD" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsModerator) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label16" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsModerator)) %></asp:Label></span>&nbsp;|&nbsp;
                        <br />
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="IS_ADMIN" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsAdmin) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label17" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsAdmin)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="PMS" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class="wordyes"><asp:Label ID="Label18" runat="server">
                                                  <%# (Convert.ToInt32(Eval("PMLimit")) == int.MaxValue ? "\u221E" : Eval("PMLimit").ToString()) %>
                                              </asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel  ID="LocalizedLabel12" runat="server" LocalizedTag="ALBUM_NUMBER" LocalizedPage="ADMIN_EDITGROUP" />
                        <span class="wordyes"><asp:Label ID="Label19" runat="server" ><%# this.Eval( "UsrAlbums" ) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel  ID="LocalizedLabel13" runat="server" LocalizedTag="IMAGES_NUMBER" LocalizedPage="ADMIN_EDITGROUP" />
                        <span class="wordyes"><asp:Label ID="Label20" runat="server" ><%# this.Eval( "UsrAlbumImages" ) %></asp:Label></span>&nbsp;|&nbsp;
                        <br />
                        <VZF:LocalizedLabel  ID="LocalizedLabel14" runat="server" LocalizedTag="STYLE" LocalizedPage="ADMIN_EDITGROUP" />&nbsp;
                        <span class="wordyes"><asp:Label ID="Label21" runat="server" ><%# this.Eval( "Style" ) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:RoleRankStyles ID="RoleRankStylesGroups" RawStyles='<%# this.Eval( "Style" ).ToString() %>' runat="server" /> 
                        <VZF:LocalizedLabel ID="LocalizedLabel15" runat="server" LocalizedTag="SIGNATURE_LENGTH" LocalizedPage="ADMIN_EDITGROUP" /> 
                        <span class="wordyes"><asp:Label ID="Label22" runat="server" ><%# this.Eval("UsrSigChars").ToString().IsSet() ? this.Eval("UsrSigChars").ToString() : this.GetItemName(false) %></asp:Label></span>&nbsp;|&nbsp;
                       <VZF:LocalizedLabel ID="LocalizedLabel16" runat="server" LocalizedTag="SIG_BBCODES" LocalizedPage="ADMIN_EDITGROUP" />
                        <span class="wordyes"><asp:Label ID="Label23" runat="server" ><%# this.Eval("UsrSigBBCodes").ToString().IsSet() ? this.Eval("UsrSigBBCodes").ToString() : this.GetItemName(false) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel17" runat="server"  LocalizedTag="SIG_HTML" LocalizedPage="ADMIN_EDITGROUP" />  
                        <span class="wordyes"><asp:Label ID="Label24" runat="server" ><%#  this.Eval("UsrSigHTMLTags").ToString().IsSet() ? this.Eval("UsrSigHTMLTags").ToString() : this.GetItemName(false) %></asp:Label></span>
					</td>
                    </tr>					
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td class="footer1" colspan="2" align="center">
			    <VZF:ThemeButton ID="NewGroup" CssClass="yafcssbigbutton centerItem" OnClick="NewGroup_Click" Visible="False"  TextLocalizedPage="PERSONALGROUP" TextLocalizedTag="NEW_GROUP" TitleLocalizedPage="PERSONALGROUP" TitleLocalizedTag="NEW_GROUP" runat="server"/>&nbsp;
                <VZF:ThemeButton ID="Cancel" CssClass="yafcssbigbutton centerItem"  OnClick="Cancel_Click"  TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="CANCEL" runat="server"/>
			</td>
		</tr>
	</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
