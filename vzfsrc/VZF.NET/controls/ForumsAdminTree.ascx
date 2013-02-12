<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumsAdminTree.ascx.cs" Inherits="VZF.Controls.ForumsAdminTree" %>
<%@ Import namespace="System.Web.UI.WebControls"  %>
 <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr>
            <td class="header1" colspan="3">
                <VZF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="FORUMS" LocalizedPage="TEAM" />
            </td>
        </tr>
        <tr>
        <td colspan="3">
                  <div id="divactive" class="active" Visible="false" runat="server">
        Active node: <b><span id="echoActive">-</span></b><div id="treebuttons">
            <VZF:ThemeButton ID="DeleteForumBtn" CssClass="yaflittlebutton" ImageThemePage="ICONS" ImageThemeTag="DELETE_SMALL_ICON"  OnClick="DeleteForumBtn_Click"  runat="server"/>
            <VZF:ThemeButton ID="CopyForumBtn" CssClass="yaflittlebutton"  ImageThemePage="ICONS" ImageThemeTag="COPY_SMALL_ICON" OnClick="CopyForumBtn_Click"  runat="server"/>
            <VZF:ThemeButton ID="EditForumBtn" CssClass="yaflittlebutton"  ImageThemePage="ICONS" ImageThemeTag="EDIT_SMALL_ICON" OnClick="EditForumBtn_Click"  runat="server"/>
            <VZF:ThemeButton ID="MoveForumBeforeBtn" CssClass="yaflittlebutton"  ImageThemePage="ICONS" ImageThemeTag="MOVE_FORUMORCAT_BEFORE_SMALL_ICON" OnClick="MoveForumBeforeBtn_Click"  runat="server"/>
            <VZF:ThemeButton ID="MoveForumAfterBtn" CssClass="yaflittlebutton"  ImageThemePage="ICONS" ImageThemeTag="MOVE_FORUMORCAT_AFTER_SMALL_ICON" OnClick="MoveForumAfterBtn_Click"  runat="server"/>
            <VZF:ThemeButton ID="AddChildrenTo" CssClass="yaflittlebutton"  ImageThemePage="ICONS" ImageThemeTag="FORUM_ADDCHILDENTO_SMALL_ICON" OnClick="AddChildrenToBtn_Click"  runat="server"/>
                                                          </div></div>
        <div class="container">
    <div id="tree">
    </div>
    </div>

        </td>
        </tr>
        <asp:Repeater ID="CategoryList" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="header2">
                        <%# HttpUtility.HtmlEncode("Name") %>
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
                |
                <asp:Button ID="NewForum" runat="server" OnClick="NewForum_Click" CssClass="pbutton"></asp:Button>
            </td>
        </tr>
    </table>