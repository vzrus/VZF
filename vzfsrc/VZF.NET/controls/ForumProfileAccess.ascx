<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Controls.ForumProfileAccess" Codebehind="ForumProfileAccess.ascx.cs" %>
<table width="100%" cellspacing="1" cellpadding="0">
    <tr class="header2">
        <td class="header2" colspan="2">
            Forum Access
        </td>
    </tr>
    <tr>
        <td class ></td>
    <asp:Literal runat="server" ID="AccessMaskRow" />
       </tr>
    </table>
     <div id="divTree" class="active" Visible="False" runat="server">
        Active node: <b><span id="echoActive">-</span></b></div>
        <div class="container">
    <div id="tree">
       
    </div>
    </div>   
        
    <table width="100%" cellspacing="1" cellpadding="0">
    <asp:Repeater ID="ForumList" OnItemDataBound="ForumList_OnItemDataBound" runat="server">
        <HeaderTemplate>
         <tr>
             <th>ForumName</th><th>RE</th><th>PO</th><th>RP</th><th>PR</th><th>PL</th><th>VO</th><th>MD</th><th>ED</th><th>DE</th><th>UP</th><th>DL</th><th>UF</th>
         </tr>
        </HeaderTemplate>
                            
        <ItemTemplate>
                        <tr class="post" runat="server">
                            <td align="left">
                                <strong>
                                    <a id="forumLink"  runat="server"></a></strong><br />
                            </td>
                            <td align="center">
                                <img id="accessTypeReadAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypePostAccess" alt="?" runat="server" /> 
                            </td>
                             <td align="center">
                                <img id="accessTypeReplyAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypePriorityAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypePollAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypeVoteAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypeModeratorAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypeEditAccess" alt="?" runat="server" /> 
                            </td>
                             <td align="center">
                                <img id="accessTypeDeleteAccess" alt="?" runat="server" /> 
                            </td>
                            <td align="center">
                                <img id="accessTypeUploadAccess" alt="?" runat="server" /> 
                            </td>
                              <td align="center">
                                <img id="accessTypeDownloadAccess" alt="?" runat="server" /> 
                            </td>
                              <td align="center">
                                <img id="accessTypeUserForumAccess" alt="?" runat="server" /> 
                            </td>
                        </tr>
                    </ItemTemplate>
    </asp:Repeater>
        </table>
<br />
RE - <YAF:LocalizedLabel ID="ReadAccessLabel" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="READ_ACCESS" runat="server" />, PO - <YAF:LocalizedLabel ID="LocalizedLabel1" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="POST_ACCESS" runat="server" />, RP  - <YAF:LocalizedLabel ID="LocalizedLabel2" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="REPLY_ACCESS" runat="server" />, PR  - <YAF:LocalizedLabel ID="LocalizedLabel3" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="PRIORITY_ACCESS" runat="server" />, PL  - <YAF:LocalizedLabel ID="LocalizedLabel4" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="POLL_ACCESS" runat="server" />, VO  - <YAF:LocalizedLabel ID="LocalizedLabel5" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="VOTE_ACCESS" runat="server" />, MD  - <YAF:LocalizedLabel ID="LocalizedLabel6" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="MODERATOR_ACCESS" runat="server" />, ED  - <YAF:LocalizedLabel ID="LocalizedLabel7" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="EDIT_ACCESS" runat="server" />, DE  - <YAF:LocalizedLabel ID="LocalizedLabel8" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="DELETE_ACCESS" runat="server" />, UP  - <YAF:LocalizedLabel ID="LocalizedLabel9" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="UPLOAD_ACCESS" runat="server" />, DL  - <YAF:LocalizedLabel ID="LocalizedLabel10" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="DOWNLOAD_ACCESS" runat="server" />, UF  - <YAF:LocalizedLabel ID="LocalizedLabel11" LocalizedPage="ADMIN_EDITACCESSMASKS" LocalizedTag="CREATEUSERFORUM_ACCESS" runat="server" />