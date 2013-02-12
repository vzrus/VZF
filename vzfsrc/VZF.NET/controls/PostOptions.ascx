<%@ Control Language="C#" AutoEventWireup="true" Inherits="VZF.Controls.PostOptions"
    CodeBehind="PostOptions.ascx.cs" %>

<tr id="OptionsRow" runat="server" class="postOptions">
    <td class="post" colspan="2">
        <b><VZF:LocalizedLabel ID="NewPostOptionsLabel" runat="server" LocalizedTag="NEWPOSTOPTIONS" /></b>
        <br />
        <ul style="list-style-type: none; padding-left: 0px;">
            <li id="liAddPoll" runat="server">
                <asp:CheckBox ID="AddPollCheckBox" runat="server" />
                <VZF:LocalizedLabel ID="AddPollLabel" runat="server" LocalizedPage="POSTMESSAGE"
                    LocalizedTag="POLLADD" />? </li>
            <li id="liQuestion" runat="server" visible="false">
                <asp:CheckBox ID="chkIsQuestion" runat="server" />
                <VZF:LocalizedLabel ID="IsQuestionLael" runat="server" LocalizedTag="ISQUESTION"/>
            </li>
            <li id="liPersistency" runat="server">
                <asp:CheckBox ID="Persistency" runat="server" />
                <VZF:LocalizedLabel ID="PersistencyLabel" runat="server" LocalizedTag="PERSISTENCY" /> (<VZF:LocalizedLabel ID="PersistencyLabel2" runat="server" LocalizedTag="PERSISTENCY_INFO" />)
            </li>
            <li id="liTopicWatch" runat="server">
                <asp:CheckBox ID="TopicWatch" runat="server" />
                <VZF:LocalizedLabel ID="TopicWatchLabel" runat="server" LocalizedTag="TOPICWATCH" />
            </li>
            <li id="liTopicAttach" runat="server">
                <asp:CheckBox ID="TopicAttach" runat="server" />
                <VZF:LocalizedLabel ID="TopicAttachLabel" runat="server" LocalizedTag="TOPICATTACH" />
            </li>
        </ul>
    </td>
</tr>
