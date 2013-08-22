<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalaccessmask.ascx.cs" Inherits="YAF.pages.personalaccessmask" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="YAF.Types.Flags" %>
<%@ Import Namespace="VZF.Utils.Helpers" %>
<VZF:PageLinks ID="PageLinks" runat="server"></VZF:PageLinks>
<a id="top"></a>
  <table class="content" cellspacing="1" cellpadding="0" width="100%">
      <thead>
        <tr>
            <td class="header1" colspan="13">
                  <VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="TITLE" LocalizedPage="PERSONALACCESSMASK" />
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
        </thead>
      <tbody>
        <asp:Repeater ID="List" runat="server" OnItemCommand="AccessMaskList_ItemCommand">
            <ItemTemplate>
                <tr class="postheader">
                    <td>
                      <img alt='<%# Eval( "Name") %>'
                                    title='<%# Eval( "Name") %>'
                                    src='<%# this.Get<ITheme>().GetItem("VOTE","POLL_MASK") %>' />&nbsp;<%# Eval( "Name") %>
                    </td>					
                    <td width="15%" style="font-weight: normal">
                           <VZF:ThemeButton ID="ThemeButton1"  CssClass="yaflittlebutton" TitleLocalizedPage="COMMON" TitleLocalizedTag="EDIT"  ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" 
                               CommandName="edit" CommandArgument='<%# Eval("AccessMaskID") %>'  Visible='<%# Eval( "IsUserMask").ToType<bool>() %>' runat="server"/>
                           <VZF:ThemeButton ID="ThemeButton2" OnLoad="DeleteAccessMask_Load" CssClass="yaflittlebutton" TitleLocalizedPage="COMMON" TitleLocalizedTag="DELETE"  ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON"
                               CommandName="delete" CommandArgument='<%# Eval("AccessMaskID") %>'   Visible='<%# Eval( "IsUserMask").ToType<bool>() %>' runat="server"/>
                    </td>
                </tr>
                <tr class="post">
                    <td align="center" colspan = 2>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="READ"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ReadAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label1" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ReadAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="POST"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PostAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label2" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PostAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="REPLY"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ReplyAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label13" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ReplyAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="PRIORITY"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PriorityAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label14" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PriorityAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <br />
                        <VZF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="POLL"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PollAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label15" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.PollAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="VOTE"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.VoteAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label16" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.VoteAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="MODERATOR"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ModeratorAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label17" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.ModeratorAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="EDIT"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.EditAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label18" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.EditAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <br />
                        <VZF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="DELETE"  LocalizedPage="ADMIN_ACCESSMASKS"/>&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.DeleteAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label19" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.DeleteAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel14" runat="server" LocalizedTag="UPLOAD"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.UploadAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label20" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.UploadAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel15" runat="server" LocalizedTag="DOWNLOAD"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.DownloadAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label21" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.DownloadAccess)) %></asp:Label></span>&nbsp;|&nbsp;
                        <VZF:LocalizedLabel ID="LocalizedLabel16" runat="server" LocalizedTag="CREATEUSERFORUM"  LocalizedPage="ADMIN_ACCESSMASKS" />&nbsp;
                        <span class='<%# BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.UserForumAccess) ? "wordyes" : "wordnow" %>'><asp:Label ID="Label22" runat="server" ><%# GetItemName(BitConversionHelper.IsBitSet(Eval("Flags"),(int)AccessFlags.Flags.UserForumAccess)) %></asp:Label></span>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
      </tbody>
      <tfoot>
        <tr class="footer1" align="center">
            <td colspan="13">
                <VZF:ThemeButton ID="NewAccessMaskBtn"  OnClick="NewAccessMask_Click" Visible="False" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="ADMIN_ACCESSMASKS" TextLocalizedTag="NEW_MASK" TitleLocalizedPage="ADMIN_ACCESSMASKS" TitleLocalizedTag="NEW_MASK" runat="server"/>
                <VZF:ThemeButton ID="Cancel" CssClass="yafcssbigbutton centerItem"   TextLocalizedPage="ADMIN_EDITFORUM" TextLocalizedTag="CANCEL" TitleLocalizedPage="ADMIN_EDITFORUM" TitleLocalizedTag="CANCEL" runat="server"/>
            </td>
        </tr>
      </tfoot>
    </table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
