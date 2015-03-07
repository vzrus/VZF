<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.deletecategory"
      CodeBehind="deletecategory.ascx.cs" %>
<%@ Register TagPrefix="YAF" Namespace="VZF.Controls" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server" ID="Adminmenu1">
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="2">            
                <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="HEADER1" LocalizedPage="ADMIN_DELETECATEGORY" />
                <asp:Label ID="CategoryNameTitle" runat="server"></asp:Label>                 
            </td>
        </tr>
        <tr>
            <td class="header2" height="30" colspan="2">
            </td>
        </tr>  
        <tr id="rowCatMove" visible="false" runat="server">
            <td class="postheader" height="30">
                <VZF:HelpLabel ID="HelpLabel2" runat="server" LocalizedTag="MOVE_FORUMS1" LocalizedPage="ADMIN_DELETECATEGORY" />
            </td>
            <td class="post" height="30">
               <div id="treedelcat" data-source="ajax"  ></div>
            </td>
        </tr>
             
        <tr>
            <td class="postfooter" align="center" colspan="2">
                <asp:Button ID="Delete" runat="server" CssClass="pbutton"></asp:Button>&nbsp;
                <asp:Button ID="Cancel" runat="server" CssClass="pbutton"></asp:Button>
            </td>
        </tr>
    </table>
</VZF:AdminMenu>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
		<asp:Timer ID="UpdateStatusTimer" runat="server" Enabled="false" Interval="4000" OnTick="UpdateStatusTimer_Tick" />
	
	</ContentTemplate>
</asp:UpdatePanel>

<div>
	<div id="DeleteCategoryMessage" style="display:none" class="ui-overlay">
		<div class="ui-widget ui-widget-content ui-corner-all">
		<h2><VZF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="DELETE_TITLE" LocalizedPage="ADMIN_DELETECATEGORY" /></h2>
		<p><VZF:LocalizedLabel ID="LocalizedLabel7" runat="server" LocalizedTag="DELETE_MSG" LocalizedPage="ADMIN_DELETECATEGORY" /></p>
		<div align="center">
			<asp:Image ID="LoadingImage" runat="server" alt="Processing..." />
		</div>
		<br />
		</div>
	</div>
</div>

<VZF:SmartScroller ID="SmartScroller1" runat="server" />

