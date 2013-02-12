<%@ Control Language="C#" AutoEventWireup="true" Inherits="VZF.Controls.LastPosts"
    CodeBehind="LastPosts.ascx.cs" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Classes" %>
<asp:Timer ID="LastPostUpdateTimer" runat="server" Interval="30000" OnTick="LastPostUpdateTimer_Tick">
</asp:Timer>
<div style="overflow: scroll; height: 400px;">
    <asp:UpdatePanel ID="LastPostUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel ID="InnerUpdatePanel" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LastPostUpdateTimer" />
                </Triggers>
                <ContentTemplate>
                    <table class="content LastPosts" width="100%" align="center">
                        <asp:Repeater ID="repLastPosts" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <td class="header2" align="center" colspan="2">
                                        <VZF:LocalizedLabel ID="Last10" LocalizedTag="LAST10" runat="server" />
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                            <ItemTemplate>
                                <tr class="postheader">
                                    <td width="20%">
                                        <strong>
                      <VZF:UserLink ID="ProfileLink" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>' 
                      ReplaceName='<%# this.Get<YafBoardSettings>().EnableDisplayName ? Container.DataItemToField<string>("DisplayName") : Container.DataItemToField<string>("UserName") %>' 
                                               BlankTarget="true" />
                                        </strong>
                                    </td>
                                    <td width="80%" class="small" align="left">
                                        <strong>
                                            <VZF:LocalizedLabel ID="Posted" LocalizedTag="POSTED" runat="server" />
                                        </strong>
                                        <VZF:DisplayDateTime id="DisplayDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Posted") %>'></VZF:DisplayDateTime>
                                    </td>
                                </tr>
                                <tr class="post">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top" class="message">
                                        <VZF:MessagePostData ID="MessagePostPrimary" runat="server" DataRow="<%# Container.DataItem %>"
                                            ShowAttachments="false">
                                        </VZF:MessagePostData>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="postheader">
                                    <td width="20%">
                                        <strong>
                                            <VZF:UserLink ID="ProfileLink" runat="server" UserID='<%# Container.DataItemToField<int>("UserID") %>' ReplaceName='<%# this.Get<YafBoardSettings>().EnableDisplayName ? Container.DataItemToField<string>("DisplayName") : Container.DataItemToField<string>("UserName") %>' 
                                                 BlankTarget="true" />
                                        </strong>
                                    </td>
                                    <td width="80%" class="small" align="left">
                                        <strong>
                                            <VZF:LocalizedLabel ID="PostedAlt" LocalizedTag="POSTED" runat="server" />
                                        </strong>
                                        <VZF:DisplayDateTime id="DisplayDateTime" runat="server" DateTime='<%# Container.DataItemToField<DateTime>("Posted") %>'></VZF:DisplayDateTime>
                                    </td>
                                </tr>
                                <tr class="post_alt">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td valign="top" class="message">
                                        <VZF:MessagePostData ID="MessagePostAlt" runat="server" DataRow="<%# Container.DataItem %>"
                                            ShowAttachments="false">
                                        </VZF:MessagePostData>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
