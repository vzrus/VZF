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
             <th>ForumName</th><th>RE</th><th>PO</th><th>RP</th><th>PR</th><th>PL</th><th>VO</th><th>MD</th><th>ED</th><th>DE</th><th>UP</th><th>DL</th>
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
						</tr>
					</ItemTemplate>
    </asp:Repeater>
        </table>
<br />
        RE - ReadAccess, PO - PostAccess, RP - ReplyAccess, PR - PriorityAccess, PL = PollAccess, VO - VoteAccess, MD - ModeratorAccess, ED - EditAccess, DE - DeleteAccess, UP - UploadAccess, DL - DownloadAccess