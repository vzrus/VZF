<%@ Control Language="c#" AutoEventWireup="True" EnableViewState="true" Inherits="YAF.Pages.Admin.eventlog"
    CodeBehind="eventlog.ascx.cs" %>
<%@ Import Namespace="YAF.Providers.Utils" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
   
<VZF:PageLinks runat="server" ID="PageLinks" />

<script type="text/javascript">
function toggleItem(detailId)
{
	var show = '<%# this.GetText("ADMIN_EVENTLOG", "SHOW")%>';
	var hide = '<%# this.GetText("ADMIN_EVENTLOG", "HIDE")%>';
	
	$('#Show'+ detailId).text($('#Show'+ detailId).text() == show ? hide : show);
	
	jQuery('#eventDetails' + detailId).slideToggle('slow'); 

	return false;
	
}
</script>

<VZF:AdminMenu runat="server" ID="AdminMenu1">
    <VZF:Pager ID="PagerTop" runat="server" OnPageChange="PagerTop_PageChange" />
    <table class="content" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td class="header1" colspan="3">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EVENTLOG" />
            </td>
        </tr>
        <tr class="header2">
            <td>
                <VZF:HelpLabel ID="SinceDateLabel" runat="server" LocalizedPage="ADMIN_EVENTLOG" LocalizedTag="SINCEDATE" Suffix=":" />&nbsp;
                <br />
                <asp:TextBox ID="SinceDate" runat="server" CssClass="edit"></asp:TextBox>
            </td>
            <td>
                <VZF:HelpLabel ID="ToDateLabel" runat="server" LocalizedPage="ADMIN_EVENTLOG" Suffix=":" LocalizedTag="TODATE" />&nbsp;
                <br />
                <asp:TextBox ID="ToDate" runat="server" CssClass="edit"></asp:TextBox>
            </td>
            <td>
                <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedPage="ADMIN_EVENTLOG" Suffix=":" LocalizedTag="TYPES" />&nbsp;
                <br />
                <asp:DropDownList ID="Types" runat="server" CssClass="edit"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="footer1" style="text-align:center">
                <VZF:ThemeButton ID="ApplyButton" CssClass="yaflittlebutton" OnClick="ApplyButton_Click" TextLocalizedPage="ADMIN_EVENTLOG" TextLocalizedTag="APPLY" runat="server"></VZF:ThemeButton>
            </td>
        </tr>
    </table>
    <br/>
    <table class="content" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td class="header1" colspan="8">
                <VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EVENTLOG" />
            </td>
        </tr>
        <asp:Repeater runat="server" ID="List">
            <HeaderTemplate>
                <tr class="header2" id="headerSize">
                    <td width="1%">
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="TYPE" LocalizedPage="ADMIN_EVENTLOG" />
                    </td>
                    <td width="5%">
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="USER" LocalizedPage="ADMIN_EVENTLOG" />
                    </td>
                    <td width="8%">
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="TIME" LocalizedPage="ADMIN_EVENTLOG" />
                    </td>
                    <td>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="SOURCE" LocalizedPage="ADMIN_EVENTLOG" />
                    </td>
                    <td>&nbsp;
                        
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td colspan="5">
                      <div class="<%# EventCssClass(Container.DataItem) %> ui-corner-all eventItem">
                        <table style="padding:0;margin:0;">
                          <tr>
                            <td width="1%">
                              <a name="event<%# Eval("EventLogID")%>" ></a>
                              <%# EventImageCode(Container.DataItem) %>
                              <asp:HiddenField ID="EventTypeID" Value='<%# Eval("Type")%>' runat="server"/>
                            </td>
                            <td width="5%">
                              <%# HtmlEncode(Eval( "Name")) %>
                            </td>
                            <td width="8%">
                              <%# this.Get<IDateTime>().FormatDateTimeShort((System.DateTime)Eval( "EventTime")) %>
                            </td>
                            <td>
                              <%# HtmlEncode(Eval( "Source")) %>
                            </td>
                            <td class="rightItem">
                              <a class="showEventItem" href="#event<%# Eval("EventLogID")%>" id="Show<%# Eval("EventLogID") %>" onclick="javascript:toggleItem(<%# Eval("EventLogID") %>);"><VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="SHOW" LocalizedPage="ADMIN_EVENTLOG" /></a>&nbsp;|&nbsp;<asp:LinkButton runat="server" OnLoad="Delete_Load" CssClass="deleteEventItem" CommandName="delete" CommandArgument='<%# Eval( "EventLogID") %>'>
                                  <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="DELETE" />
                                </asp:LinkButton>
                            </td>
                          </tr>
                        </table>     
                      </div>
                      <div class="EventDetails" id="eventDetails<%# Eval("EventLogID") %>" style="display: none;margin:0;padding:0;">  
                            <pre><%# Eval( "Description") %></pre>
                        </div>   
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr class="footer1">
                    <td colspan="5" align="center">
                        <VZF:ThemeButton runat="server" Visible="<%# this.List.Items.Count > 0 %>" OnLoad="DeleteAll_Load" CssClass="yaflittlebutton" OnClick="DeleteAll_Click" TextLocalizedPage="ADMIN_EVENTLOG" TextLocalizedTag="DELETE_ALLOWED">
                        </VZF:ThemeButton>
                    </td>
                </tr>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    <VZF:Pager ID="PagerBottom" runat="server" LinkedPager="PagerTop" />
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
