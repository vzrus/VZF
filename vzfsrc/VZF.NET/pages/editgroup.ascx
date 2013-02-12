<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.editgroup"
    CodeBehind="editgroup.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="11">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="TITLE" LocalizedPage="ADMIN_EDITGROUP" />
            </td>
        </tr>
        <tr>
	      <td class="header2" colspan="11" style="height:30px"></td>
		</tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel1" runat="server" LocalizedTag="ROLE_NAME" LocalizedPage="ADMIN_EDITGROUP" />
            </td>
            <td class="post" width="50%">
                <asp:TextBox Style="width: 350px" ID="Name" runat="server" />
                <asp:RequiredFieldValidator ID="postNameRequired" runat="server" Display="Dynamic" ControlToValidate="Name" ErrorMessage="Role name is required."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel6" runat="server" LocalizedTag="DESCRIPTION" LocalizedPage="ADMIN_EDITGROUP" />
                
                
            </td>
            <td class="post" width="50%">
                <asp:TextBox Style="width: 350px" ID="Description" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="postheader" style="width: 50%">
                <VZF:HelpLabel ID="HelpLabel12" runat="server" LocalizedTag="PRIORITY" LocalizedPage="ADMIN_EDITGROUP" />
            </td>
            <td class="post" style="width: 50%">
                <asp:TextBox Style="width: 350px" ID="Priority" MaxLength="5" Text="0" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="postheader" width="50%">
                <VZF:HelpLabel ID="HelpLabel13" runat="server" LocalizedTag="STYLE" LocalizedPage="ADMIN_EDITGROUP" />            
            </td>
            <td class="post" width="50%">
                <asp:TextBox Style="width: 350px" ID="StyleTextBox" TextMode="MultiLine" runat="server" />
            </td>
        </tr>
        <tr runat="server" id="NewGroupRow">
            <td class="postheader">
                <VZF:HelpLabel ID="HelpLabel15" runat="server" LocalizedTag="INITIAL_MASK" LocalizedPage="ADMIN_EDITGROUP" />
            </td>
            <td class="post">
                <asp:DropDownList Style="width: 350px" runat="server" ID="AccessMaskID" OnDataBinding="BindData_AccessMaskID" />
            </td>
        </tr>
        <asp:Repeater ID="AccessList" runat="server">
            <HeaderTemplate>
                <tr>
                    <td class="header1" colspan="11">
                        <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="HEADER" LocalizedPage="ADMIN_EDITGROUP" />
                    </td>
                </tr>
                <tr>
                    <td class="header2">
                        <VZF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="FORUM"  LocalizedPage="ADMIN_EDITGROUP" />
                    </td>
                    <td class="header2">
                        <VZF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="ACCESS_MASK" />
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="postheader">
                        <asp:Label ID="ForumID" Visible="false" runat="server" Text='<%# Eval( "ForumID") %>'></asp:Label>
                        <strong>
                            <%# Eval( "ForumName") %>
                        </strong>
                        <br />
                        <em>
                        <VZF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="BOARD"  LocalizedPage="ADMIN_EDITGROUP" />
                        <%# Eval( "BoardName") %>
                        <br />
                        <VZF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="CATEGORY"  LocalizedPage="ADMIN_EDITGROUP" />
                        <%# Eval( "CategoryName") %>
                        </em>
                    </td>
                    <td class="post">
                        <asp:DropDownList Style="width: 350px" runat="server" ID="AccessMaskID" OnDataBinding="BindData_AccessMaskID"
                            OnPreRender="SetDropDownIndex" value='<%# Eval("AccessMaskID") %>' />
                        ...
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="footer1" align="center" colspan="11">
                <asp:Button ID="Save" runat="server" OnClick="Save_Click" CssClass="pbutton"></asp:Button>&nbsp;
                <asp:Button ID="Cancel" runat="server" OnClick="Cancel_Click" CssClass="pbutton"></asp:Button>
            </td>
        </tr>
    </table>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
