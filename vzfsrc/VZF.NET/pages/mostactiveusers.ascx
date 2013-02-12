<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.mostactiveusers" CodeBehind="mostactiveusers.ascx.cs" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator"></div>
<table class="content" width="100%" cellspacing="1" cellpadding="0">
	<tr>
		<td class="header1" colspan="5"> 
			<asp:Label runat="server" ID="HeaderLbl"></asp:Label>
		</td>
	</tr>
	<tr>
		<td class="header2">
			<VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="USERNAME" />
		</td>
		<td class="header2">
			<VZF:LocalizedLabel ID="LocalizedLabelLatestActions" runat="server" LocalizedTag="JOINED" />
		</td>
		<td class="header2">
			<VZF:LocalizedLabel ID="DaysPeriodLbl" runat="server" LocalizedTag="NUM_PERIODPOSTS" />
		</td>
		<td class="header2">
			<VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="PERC_PERPERIOD" />
		</td>
	</tr>
	<asp:Repeater ID="UserList" OnItemDataBound="UserList_OnItemDataBound" runat="server">
		<ItemTemplate>
			<tr>
				<td class="post">		
					<VZF:UserLink ID="NameLink"  runat="server" />
					<asp:PlaceHolder ID="HiddenPlaceHolder" runat="server" Visible='<%# Convert.ToBoolean(Eval("IsHidden"))%>' >
					(<VZF:LocalizedLabel ID="Hidden" LocalizedTag="HIDDEN" runat="server" />)
					</asp:PlaceHolder>				    
				</td>
				<td class="post">				
					<%# Eval("Joined").ToString() %>
				</td>
				<td class="post">
					<%# Eval("NumOfPosts").ToString() %>
				</td>				
				<td class="post">
					 <asp:Label ID="PercentsOf" runat="server" ></asp:Label>
				</td>
			</tr>	
		</ItemTemplate>
		<FooterTemplate>
			<tr class="footer1">
			<td colspan="5" align="center">            
						<VZF:ThemeButton ID="btnReturn" runat="server" CssClass="yafcssbigbutton rightItem"
				TextLocalizedPage="COMMON" TextLocalizedTag="OK" TitleLocalizedPage="COMMON" TitleLocalizedTag="OK" OnClick="btnReturn_Click" /> 
			</td>
		   </tr>
		</FooterTemplate>
	</asp:Repeater>

</table>
<div id="DivSmartScroller">
	<VZF:SmartScroller ID="SmartScroller1" runat="server" />
</div>