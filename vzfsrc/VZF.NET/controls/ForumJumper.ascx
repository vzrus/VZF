﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumJumper.ascx.cs" Inherits="VZF.Сontrols.ForumJumper" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<table ID="TreeTable" Visible="False" runat="server">
 <tr id="forumTree" runat="server" Visible="False" >
     <td><div id="jumpList" style="display: none;" runat="server">
       <div class="container"><div id="fjumptree"></div></div></div></td>
     <td><div><img id="imgJump" src="" alt='<%# PageContext.Get<ILocalization>().GetText("COMMON", "FORUM_JUMP") %>' title='<%# PageContext.Get<ILocalization>().GetText("COMMON", "FORUM_JUMP") %>'  runat="server"/></div></td>
 </tr>   
</table>
    <asp:PlaceHolder ID="jholder" runat="server"></asp:PlaceHolder>



