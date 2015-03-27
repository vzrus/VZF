<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.Admin.forums" Codebehind="forums.ascx.cs" %>
<VZF:PageLinks runat="server" ID="PageLinks" />
<VZF:AdminMenu runat="server">  
 <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="3">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="FORUMS" LocalizedPage="TEAM" />
            </td>
        </tr>
        <tr>
        <td colspan="3">
            <div class="container" ID="tviewcontainer" visible="false" runat="server">    
           
                <VZF:LocalizedLabel ID="TreeMenuTip" runat="server"></VZF:LocalizedLabel>
            <VZF:PageLinks runat="server" ID="PageLinks1" />
                
             <br />  
</div>
   <div id="ftree3" data-source="ajax"></div> 
        </td>
        </tr>
        <asp:Repeater ID="CategoryList" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="header2">
                        <%# HttpUtility.HtmlEncode(Eval("Name")) %>
                    </td>
                    <td align="center" class="header2" width="8%">
                        <%# Eval( "SortOrder") %>
                    </td>
                    <td class="header2" width="17%" style="font-weight: normal">
                    <VZF:ThemeButton ID="ThemeButtonEdit" CssClass="yaflittlebutton"  CommandName='edit' CommandArgument='<%# Eval( "CategoryID") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server"></VZF:ThemeButton>
                    <VZF:ThemeButton ID="ThemeButtonDelete" CssClass="yaflittlebutton" OnLoad="DeleteCategory_Load"  CommandName='delete' CommandArgument='<%# Eval( "CategoryID") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"></VZF:ThemeButton>
                    </td>
                </tr>
                <asp:Repeater ID="ForumList" OnItemCommand="ForumList_ItemCommand" runat="server"
                    DataSource='<%# ((System.Data.DataRowView)Container.DataItem).Row.GetChildRows("FK_Forum_Category") %>'>
                    <ItemTemplate>
                        <tr class="post">
                            <td align="left">
                                <VZF:ThemeImage ID="ForumTypeImg" ThemePage="ICONS" ThemeTag='<%# DataBinder.Eval(Container.DataItem, "[\"IsUserForum\"]").ToType<bool>() ? "BLOG_GENERAL" : "MULTIPAGES_SMALL" %>' LocalizedTitleTag='<%# DataBinder.Eval(Container.DataItem, "[\"IsUserForum\"]").ToType<bool>() ? "ISUSERFORUM" : "ISGENERALFORUM" %>' runat="server"/>
                                <strong>
                                    <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Name\"]")) %></strong><br />
                                <%#  HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "[\"Description\"]")) %>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem, "[\"SortOrder\"]") %>
                            </td>
                            <td>
                             <VZF:ThemeButton ID="btnEdit" CssClass="yaflittlebutton" CommandName='edit' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="EDIT" ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" runat="server"></VZF:ThemeButton>								
                             <VZF:ThemeButton ID="btnDuplicate" CssClass="yaflittlebutton" CommandName='copy' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="COPY" ImageThemePage="ICONS" ImageThemeTag="COPY_SMALL_ICON" runat="server"></VZF:ThemeButton>
                             <VZF:ThemeButton ID="btnDelete" CssClass="yaflittlebutton" CommandName='delete' CommandArgument='<%# Eval( "[\"ForumID\"]") %>' TitleLocalizedTag="DELETE" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON" runat="server"></VZF:ThemeButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td class="footer1" colspan="3" align="center">
                <asp:Button ID="NewCategory" runat="server" OnClick="NewCategory_Click" CssClass="pbutton"></asp:Button>
                <asp:Button ID="NewForum" runat="server" OnClick="NewForum_Click" CssClass="pbutton"></asp:Button>
            </td>
        </tr>
    </table>
</VZF:AdminMenu>
<VZF:SmartScroller ID="SmartScroller1" runat="server" />
