<%@ Control Language="C#" AutoEventWireup="true" Inherits="YAF.Pages.Admin.taskmanager"
    CodeBehind="taskmanager.ascx.cs" %>
<%@ Import Namespace="YAF.Core.Tasks" %>
<%@ Import Namespace="VZF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<VZF:PageLinks ID="PageLinks" runat="server" />
<VZF:AdminMenu runat="server">
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="3">
                <asp:Label ID="lblTaskCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="header2">
            <td>
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="NAME" LocalizedPage="ADMIN_NNTPSERVERS" />
            </td>
            <td>
                <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="RUNNING" LocalizedPage="ADMIN_TASKMANAGER" />
            </td>
            <td>
               <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="DURATION" LocalizedPage="ADMIN_TASKMANAGER" />
            </td>
        </tr>
        <asp:Repeater ID="taskRepeater" runat="server" OnItemCommand="taskRepeater_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <strong>
                            <%# Eval("Key") %></strong>
                            <asp:PlaceHolder ID="StopTaskHolder" runat="server" Visible="<%# Container.ToDataItemType<KeyValuePair<string, IBackgroundTask>>().Value.IsStoppable() %>">
                            [<asp:LinkButton ID="stop" runat="server" CommandName="stop" CommandArgument='<%# Eval("Key") %>'><VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="STOP_TASK" LocalizedPage="ADMIN_TASKMANAGER" /></asp:LinkButton>]
                        </asp:PlaceHolder>
                    </td>
                    <td>
                        <span class='<%# Eval("Value.IsRunning").ToType<bool>() ? "wordyes" : "wordnow" %>'><asp:Label ID="Label1" runat="server" ><%# GetItemName(Eval("Value.IsRunning").ToType<bool>())%></asp:Label></span>
                   </td>
                    <td>
                        <%# FormatTimeSpan(Container.ToDataItemType<KeyValuePair<string, IBackgroundTask>>().Value.Started)%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
