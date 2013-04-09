<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.groups" Codebehind="groups.ascx.cs" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<%@ Import Namespace="VZF.Utils.Helpers" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server" ID="AdminMenu">
	<table class="content" width="100%" cellspacing="1" cellpadding="0">
		<asp:Repeater ID="RoleListNet" runat="server" OnItemCommand="RoleListNet_ItemCommand">
			<HeaderTemplate>
				<tr>
					<td class="header1" colspan="2">
						<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="PROVIDER_ROLES" LocalizedPage="ADMIN_GROUPS" />
					</td>
				</tr>
				<tr>
					<td colspan="2" class="header2" style="text-align:center">
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="NOTE_DELETE" LocalizedPage="ADMIN_GROUPS" />
					</td>
				</tr>
				<tr>
					<td class="header2">
						<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="NAME" LocalizedPage="ADMIN_COMMON" />
					</td>
					<td class="header2">&nbsp;
						
					</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td class="post">                     
						<%# Container.DataItem %>
						(<VZF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedPage="ADMIN_GROUPS" LocalizedTag="UNLINKED" />)
					</td>
					<td class="post" align="right">
						<asp:LinkButton ID="LinkButtonAdd" runat="server" CommandName="add" CommandArgument='<%# Container.DataItem %>'>
                        <VZF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedPage="ADMIN_GROUPS" LocalizedTag="ADD_ROLETOYAF" />
                        </asp:LinkButton>
						|
						<asp:LinkButton ID="LinkButtonDelete" runat="server" OnLoad="Delete_Load" CommandName="delete"
							CommandArgument='<%# Container.DataItem %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedPage="ADMIN_GROUPS" LocalizedTag="DELETE_ROLEFROMYAF" />
                            </asp:LinkButton>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<asp:Repeater ID="RoleListYaf" runat="server" OnItemCommand="RoleListYaf_ItemCommand">
			<HeaderTemplate>
				<tr>
					<td class="header1" colspan="2">
						<VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="HEADER" LocalizedPage="ADMIN_GROUPS" />
					</td>
				</tr>
				<tr>
					<td colspan="2" class="header2" style="text-align:center">
						<VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="NOTE_DELETE_LINKED" LocalizedPage="ADMIN_GROUPS" />
					</td>
				</tr>
				<tr>
					<td class="header1">
						<VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="NAME" LocalizedPage="ADMIN_GROUPS" />
					</td>	
					<td class="header1">&nbsp;
						
					</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td class="header2">
                    <img alt="" title="" src='<%# this.Get<ITheme>().GetItem("VOTE","VOTE_USERS") %>' />&nbsp;
						<%# Eval( "Name" ) %>
						(<%# GetLinkedStatus( (DataRowView) Container.DataItem )%>)&nbsp;&nbsp;                        
					</td>
                    <td class="header2" align="right">
						<asp:LinkButton ID="LinkButtonEdit" runat="server" Visible="true"
							CommandName="edit" CommandArgument='<%# Eval( "GroupID") %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="EDIT" />
                         </asp:LinkButton>
						|
						<asp:LinkButton  ID="LinkButtonDelete" runat="server" OnLoad="Delete_Load" Visible='<%#(this.Eval( "Flags" ).BinaryAnd(2) ? false : true)%>'
							CommandName="delete" CommandArgument='<%# Eval( "GroupID") %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="DELETE" />
                        </asp:LinkButton>
					</td>
                     </tr>
                    <tr>           
					<td class="post" colspan="2">
                                             <VZF:LocalizedLabel  ID="LocalizedLabel18" runat="server" LocalizedTag="PRIORITY" LocalizedPage="ADMIN_EDITGROUP" />
                        <span class="wordyes"><asp:Label ID="Label13" runat="server" ><%# this.Eval( "SortOrder" ) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel19" runat="server" LocalizedTag="IS_GUEST" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsGuest) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label14" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsGuest)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="IS_START" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsStart) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label15" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsStart)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="IS_MOD" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsModerator) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label16" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)GroupFlags.Flags.IsModerator)) %></asp:Label></span>&nbsp;|&nbsp;
                        <br />
                        <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="IS_ADMIN" LocalizedPage="ADMIN_GROUPS" />&nbsp;
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
                        <span class="wordyes"><asp:Label ID="Label21" runat="server" ><%# this.Eval( "Style" ) %></asp:Label></span>&nbsp;:&nbsp;
                        <VZF:RoleRankStyles ID="RoleRankStylesGroups" RawStyles='<%# this.Eval( "Style" ).ToString() %>' runat="server" />&nbsp;:&nbsp; 
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
				<asp:Button ID="NewGroup" runat="server" OnClick="NewGroup_Click" CssClass="pbutton"></asp:Button>
			</td>
		</tr>
	</table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
