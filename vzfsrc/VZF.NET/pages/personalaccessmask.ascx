<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalaccessmask.ascx.cs" Inherits="YAF.pages.personalaccessmask" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top" name="top"></a>
  <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="13">
                  <VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_ACCESSMASKS" />
            </td>
        </tr>
        <tr class="header2">
            <td>
                <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="NAME"  LocalizedPage="ADMIN_ACCESSMASKS" />
            </td>			
            <td>
                &nbsp;
            </td>
        </tr>
         
        <asp:Repeater ID="List" runat="server" OnItemCommand="AccessMaskList_ItemCommand">
            <ItemTemplate>
                <tr class="postheader">
                    <td>
                      <img alt='<%# Eval( "Name") %>'
                                    title='<%# Eval( "Name") %>'
                                    src='<%# this.Get<ITheme>().GetItem("VOTE","POLL_MASK") %>' />&nbsp;<%# Eval( "Name") %>
                    </td>					
                    <td width="15%" style="font-weight: normal">
                        <asp:LinkButton ID="LinkButton1" runat='server' CommandName='edit' Visible='<%# Eval( "IsUserMask").ToType<bool>() %>' CommandArgument='<%# Eval( "AccessMaskID") %>'>
                          <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="EDIT" />
                        </asp:LinkButton>
                        |
                        <asp:LinkButton ID="LinkButton2" runat='server' OnLoad="DeleteAccessMask_Load" CommandName='delete'  Visible='<%# Eval( "IsUserMask").ToType<bool>() %>' CommandArgument='<%# Eval( "AccessMaskID") %>'>
                          <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="DELETE" />
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr class="post">
                    <td align="center" colspan = 2>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="READ"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label1" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.ReadAccess)) %>'><%# GetItemName(BitSet(Eval("Flags"),(int)AccessFlags.Flags.ReadAccess)) %></asp:Label>&nbsp;|&nbsp; 
                       
                        <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="POST"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label2" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.PostAccess)) %>'><%# GetItemName(BitSet(Eval("Flags"),(int)AccessFlags.Flags.PostAccess)) %></asp:Label>&nbsp;|&nbsp;
                        
                        <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="REPLY"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label3" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.ReplyAccess)) %>'><%# GetItemName(BitSet(Eval("Flags"),(int)AccessFlags.Flags.ReplyAccess)) %></asp:Label>&nbsp;|&nbsp;
                        
                        <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="PRIORITY"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label4" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.PriorityAccess)) %>'><%# GetItemName(BitSet(Eval("Flags"),(int)AccessFlags.Flags.PriorityAccess)) %></asp:Label>&nbsp;|&nbsp;
                   <br />
                
                    <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="POLL"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                    <asp:Label ID="Label5" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.PollAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.PollAccess)) %></asp:Label>&nbsp;|&nbsp;
                
                    <VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="VOTE"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label6" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.VoteAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.VoteAccess)) %></asp:Label>&nbsp;|&nbsp;
                    
                <VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="MODERATOR"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label7" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.ModeratorAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.ModeratorAccess)) %></asp:Label>&nbsp;|&nbsp;
                
                    <VZF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="EDIT"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label8" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.EditAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.EditAccess)) %></asp:Label>&nbsp;|&nbsp;
                 <br />	
                <VZF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="DELETE"  LocalizedPage="ADMIN_ACCESSMASKS"/>&nbsp;
                        <asp:Label ID="Label9" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.DeleteAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.DeleteAccess)) %></asp:Label>&nbsp;|&nbsp;
                
                <VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="UPLOAD"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label10" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.UploadAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.UploadAccess)) %></asp:Label>&nbsp;|&nbsp;
                    
                <VZF:LocalizedLabel ID="LocalizedLabel15" runat="server" LocalizedTag="DOWNLOAD"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label11" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.DownloadAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.DownloadAccess)) %></asp:Label>
                
                <VZF:LocalizedLabel ID="LocalizedLabel16" runat="server" LocalizedTag="CREATEUSERFORUM"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <asp:Label ID="Label12" runat="server" ForeColor='<%# GetItemColor(BitSet(Eval("Flags"),(int)AccessFlags.Flags.UserForumAccess)) %>'><%# GetItemName(BitSet(Eval( "Flags"),(int)AccessFlags.Flags.UserForumAccess)) %></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="footer1" align="center">
            <td colspan="13">
                <asp:Button ID="New" runat="server" OnClick="NewAccessMask_Click" CssClass="pbutton" />
            </td>
        </tr>
    </table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
