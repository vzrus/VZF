<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalgroup.ascx.cs" Inherits="YAF.pages.personalgroup" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top" name="top"></a>
<table class="content" width="100%" cellspacing="1" cellpadding="0">
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
                        <asp:LinkButton ID="LinkButtonGroupMembers" runat="server" Visible="true"
							CommandName="users" CommandArgument='<%# Eval( "GroupID") %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedPage="MEMBERS" LocalizedTag="TITLE" />
                         </asp:LinkButton> |
						<asp:LinkButton ID="LinkButtonEdit" runat="server" Visible="true"
							CommandName="edit" CommandArgument='<%# Eval( "GroupID") %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="EDIT" />
                         </asp:LinkButton>|
						<asp:LinkButton  ID="LinkButtonDelete" runat="server" OnLoad="DeleteRole_Load" Visible='<%#(!this.Eval( "Flags" ).BinaryAnd(2))%>'
							CommandName="delete" CommandArgument='<%# Eval( "GroupID") %>'>
                            <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="DELETE" />
                        </asp:LinkButton>
					</td>
                     </tr>
                    <tr>           
					<td class="post" colspan="2">
                     <VZF:LocalizedLabel ID="HelpLabel6" Visible='<%# Eval("Description").ToString().IsSet() %>' runat="server" LocalizedTag="DESCRIPTION" LocalizedPage="ADMIN_EDITGROUP">
                         </VZF:LocalizedLabel>
                          &nbsp;<%# Eval("Description").ToString() %>&nbsp; 
                    <br />
                    <VZF:LocalizedLabel  ID="HelpLabel12" runat="server" LocalizedTag="PRIORITY" LocalizedPage="ADMIN_EDITGROUP" />
                    <asp:Label ID="Label11" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "SortOrder" ).ToString()) %>'><%# this.Eval("SortOrder").ToString()%></asp:Label>&nbsp;|&nbsp;
                    <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="IS_GUEST" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                    <asp:Label ID="Label2" runat="server" ForeColor='<%# GetItemColor(this.Eval( "Flags" ).BinaryAnd(2)) %>'><%# GetItemName(this.Eval( "Flags" ).BinaryAnd(2)) %></asp:Label>&nbsp;|&nbsp;
				    <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="IS_START" LocalizedPage="ADMIN_GROUPS" />&nbsp;
                    <asp:Label ID="Label1" runat="server" ForeColor='<%# GetItemColor(this.Eval( "Flags" ).BinaryAnd(4)) %>'><%# GetItemName(this.Eval( "Flags" ).BinaryAnd(4)) %></asp:Label>&nbsp;|&nbsp;
				    <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="IS_MOD" LocalizedPage="ADMIN_GROUPS" />&nbsp;
					<asp:Label ID="Label3" runat="server" ForeColor='<%# GetItemColor(this.Eval( "Flags" ).BinaryAnd(8)) %>'><%# GetItemName(this.Eval( "Flags" ).BinaryAnd(8)) %></asp:Label>&nbsp;|&nbsp;
					<VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="IS_ADMIN" LocalizedPage="ADMIN_GROUPS" />&nbsp;
					<asp:Label ID="Label4" runat="server" ForeColor='<%# GetItemColor(this.Eval( "Flags" ).BinaryAnd(1)) %>'><%# GetItemName(this.Eval( "Flags" ).BinaryAnd(1)) %></asp:Label>&nbsp;|&nbsp;
					<VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="PMS" LocalizedPage="ADMIN_GROUPS" />&nbsp;
					 <asp:Label ID="Label6" runat="server" ForeColor='<%# GetItemColorString((Convert.ToInt32(Eval("PMLimit")) == int.MaxValue) ? "\u221E" : Eval("PMLimit").ToString()) %>'><%# ((Convert.ToInt32(Eval("PMLimit")) == int.MaxValue) ? "\u221E" : Eval("PMLimit").ToString())%></asp:Label>&nbsp;|&nbsp;                   
                    <br />
                    <VZF:LocalizedLabel  ID="HelpLabel10" runat="server" LocalizedTag="ALBUM_NUMBER" LocalizedPage="ADMIN_EDITGROUP" />
                    <asp:Label ID="Label9" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "UsrAlbums" ).ToString()) %>'><%# this.Eval("UsrAlbums").ToString()%></asp:Label>&nbsp;|&nbsp;
                    <VZF:LocalizedLabel  ID="HelpLabel11" runat="server" LocalizedTag="IMAGES_NUMBER" LocalizedPage="ADMIN_EDITGROUP" />
                    <asp:Label ID="Label10" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "UsrAlbumImages" ).ToString()) %>'><%# this.Eval("UsrAlbumImages").ToString()%></asp:Label>&nbsp;|&nbsp;
                    <br />                   
                    <VZF:LocalizedLabel  ID="HelpLabel13" runat="server" LocalizedTag="STYLE" LocalizedPage="ADMIN_EDITGROUP" />&nbsp;  
                    <asp:Label ID="Label12" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "Style" ).ToString()) %>'><%# this.Eval("Style").ToString().IsSet() && (this.Eval("Style").ToString().Trim().Length > 0) ? "" : this.GetItemName(false)%></asp:Label>&nbsp;
                    <VZF:RoleRankStyles ID="RoleRankStylesGroups" RawStyles='<%# this.Eval( "Style" ).ToString() %>' runat="server" /> 
                     <br />
                    <VZF:LocalizedLabel ID="HelpLabel7" runat="server" LocalizedTag="SIGNATURE_LENGTH" LocalizedPage="ADMIN_EDITGROUP" />                   
                    <asp:Label ID="Label5" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "UsrSigChars" ).ToString()) %>'><%# this.Eval("UsrSigChars").ToString().IsSet() ? this.Eval("UsrSigChars").ToString() : this.GetItemName(false) %></asp:Label>&nbsp;|&nbsp;
                    <VZF:LocalizedLabel ID="HelpLabel8" runat="server" LocalizedTag="SIG_BBCODES" LocalizedPage="ADMIN_EDITGROUP" />
                    <asp:Label ID="Label7" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "UsrSigBBCodes" ).ToString()) %>'><%# this.Eval("UsrSigBBCodes").ToString().IsSet() ? this.Eval("UsrSigBBCodes").ToString() : this.GetItemName(false) %></asp:Label>&nbsp;|&nbsp;                   
                    <VZF:LocalizedLabel ID="HelpLabel9" runat="server"  LocalizedTag="SIG_HTML" LocalizedPage="ADMIN_EDITGROUP" />                
                    <asp:Label ID="Label8" runat="server" ForeColor='<%# GetItemColorString(this.Eval( "UsrSigHTMLTags" ).ToString()) %>'><%#  this.Eval("UsrSigHTMLTags").ToString().IsSet() ? this.Eval("UsrSigHTMLTags").ToString() : this.GetItemName(false)%></asp:Label>&nbsp;|&nbsp;
                    </td>
                    </tr>					
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td class="footer1" colspan="2" align="center">
				<asp:Button ID="NewGroup" runat="server" OnClick="NewGroup_Click" Visible="False" CssClass="pbutton"></asp:Button>
			</td>
		</tr>
	</table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
